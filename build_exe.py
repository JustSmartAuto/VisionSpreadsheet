#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
一键把最新 index.html 打包进 Tauri exe，零 Rust 编译。

两种模式自动选择:
  [Fast]  tauri-dumper repack           —— 秒级，brotli 必须 ≤ 原压缩块大小
  [Fallback] PE 注入 + 新 section       —— 任意大小，自动计算指针

流程:
  1. build_singlefile_html.py → index.single.html
  2. brotli q=7 压缩，比较原 brotli 块大小
     ├─ 变小 → tauri-dumper repack（零改动最快）
     └─ 变大 → PE 注入：文件末尾追加 + 节表加 "TAUR" section + 改 data_ptr/comp_size
  3. 输出 EditRegionTauri_YY.MM.DD.HH.exe

PE 注入原理:
  Tauri 2.x 资产表是硬编码在 .rdata 里的 struct AssetEntry:
    +0  name_ptr    : 64bit VA = ImageBase + name RVA
    +8  name_len    : usize
    +16 data_ptr    : 64bit VA = ImageBase + data RVA  ← 只改这个
    +24 compressed  : u32 compressed_size                ← 只改这个
  brotli 块在原 .rdata 的 data_offset（不移动它）。
  如果 brotli 变大，我们:
    ① 在文件末尾追加新 brotli 块（对齐 FileAlignment=0x200）
    ② 节表末尾加 "TAUR" section header（VSIZE 对齐 0x1000）
    ③ 改 SizeOfImage 覆盖新 section
    ④ AssetEntry.data_ptr → ImageBase + 新 section RVA
    ⑤ AssetEntry.comp_size → 新 brotli 大小

用法:
  cd EditRegionTauri
  python build_exe.py
"""

import subprocess, sys, shutil, struct, brotli
from pathlib import Path
from datetime import datetime

HERE = Path(__file__).resolve().parent
DIST = HERE / "dist"
EXE_ORIGINAL = DIST / "EditRegionTauri.exe"
SINGLE_HTML  = HERE / "index.single.html"
TDUMPER = HERE.parent / "tauri-dumper-x86_64-pc-windows-msvc" / "tauri-dumper.exe"
REPACK_DIR = HERE / "_repack_assets"


# ==============================================================
# PE 解析 & 资产表定位（只算一次，两种模式共用）
# ==============================================================
def parse_pe(exe_bytes):
    """返回 (sections, image_base, sect_align, file_align, size_of_image, asset_hdr_off,
             orig_data_offset, orig_comp_size)"""
    pe_pos   = exe_bytes.find(b"PE\x00\x00")
    opt_off  = pe_pos + 24
    opt_size = struct.unpack_from("<H", exe_bytes, pe_pos + 20 + 16)[0]
    sh_off   = pe_pos + 24 + opt_size
    num_sec  = struct.unpack_from("<H", exe_bytes, pe_pos + 4 + 2)[0]

    assert struct.unpack_from("<H", exe_bytes, opt_off)[0] == 0x20B, "需要 PE32+"

    image_base  = struct.unpack_from("<Q", exe_bytes, opt_off + 24)[0]
    sect_align  = struct.unpack_from("<I", exe_bytes, opt_off + 32)[0]
    file_align  = struct.unpack_from("<I", exe_bytes, opt_off + 36)[0]
    size_of_img_off = opt_off + 56

    sections = []
    for i in range(num_sec):
        so = sh_off + i * 40
        sections.append({
            "name":  exe_bytes[so:so+8].rstrip(b"\x00").decode("ascii", "replace"),
            "vsize": struct.unpack_from("<I", exe_bytes, so + 8)[0],
            "vaddr": struct.unpack_from("<I", exe_bytes, so + 12)[0],
            "rsize": struct.unpack_from("<I", exe_bytes, so + 16)[0],
            "rptr":  struct.unpack_from("<I", exe_bytes, so + 20)[0],
            "off":   so,
        })

    # 资产表定位（这些值从 manifest 硬编码，稳定不变）
    # 如果将来换了编译结果，改这里即可
    data_offset = 6369639              # file offset: brotli 块起点
    comp_size   = 5619918              # 原 brotli 块大小
    hdr_off     = data_offset + comp_size + 3  # AssetEntry struct 起点

    return {
        "sections": sections, "num_sec": num_sec,
        "image_base": image_base,
        "sect_align": sect_align, "file_align": file_align,
        "sh_off": sh_off, "size_of_img_off": size_of_img_off,
        "data_offset": data_offset, "comp_size": comp_size, "hdr_off": hdr_off,
        "pe_pos": pe_pos,
    }


# ==============================================================
# Fast Path: tauri-dumper repack
# ==============================================================
def try_tauri_dumper_repack(new_compressed_size, orig_comp_size, ts) -> Path | None:
    """尝试 tauri-dumper repack（需要新 brotli ≤ 原 brotli）"""
    if new_compressed_size > orig_comp_size:
        return None  # 太大了，tauri-dumper 不会跳过 oversized

    if REPACK_DIR.is_dir():
        shutil.rmtree(REPACK_DIR)
    REPACK_DIR.mkdir()
    shutil.copy(SINGLE_HTML, REPACK_DIR / "index.html")

    out = DIST / f"EditRegionTauri_{ts}.exe"
    if out.is_file():
        out.unlink()

    r = subprocess.run(
        [str(TDUMPER), "repack", str(EXE_ORIGINAL),
         "--assets", str(REPACK_DIR), "-o", str(out)],
        capture_output=True, text=True, cwd=str(HERE),
    )
    if r.returncode != 0:
        return None

    v = subprocess.run([str(TDUMPER), "list", str(out)], capture_output=True, text=True)
    print("  tauri-dumper:", v.stdout.strip())
    return out


# ==============================================================
# Fallback: PE 注入 + 新 section
# ==============================================================
def build_new_section_exe(html_in: bytes, ts: str) -> Path:
    """文件末尾追加新 brotli 块 + 节表加 "TAUR" section + 改 data_ptr/comp_size"""
    print(f"  [Fallback] PE 注入方案 ...")

    new_compressed = brotli.compress(html_in, quality=7)
    print(f"  brotli q=7: {len(new_compressed):,} bytes")

    exe = bytearray(EXE_ORIGINAL.read_bytes())
    info = parse_pe(bytes(exe))
    ibase = info["image_base"]
    sa    = info["sect_align"]
    fa    = info["file_align"]
    secs  = info["sections"]
    hdr   = info["hdr_off"]

    # 对齐追加起点
    cur_end = len(exe)
    pad_to  = (cur_end + fa - 1) & ~(fa - 1)
    exe.extend(b"\x00" * (pad_to - cur_end))
    new_rptr = len(exe)
    exe.extend(new_compressed)
    while len(exe) % fa:
        exe.append(0)

    # 新 section 的 VA 接在现有 VA 最大值之后（对齐 SA）
    max_va_end = max(s["vaddr"] + s["vsize"] for s in secs)
    new_vaddr = (max_va_end + sa - 1) & ~(sa - 1)
    new_rsize = len(new_compressed)
    new_vsize = (new_rsize + sa - 1) & ~(sa - 1)

    # 新 section header 写到节表末尾（原 num_sec * 40 后）
    new_sec_off = info["sh_off"] + info["num_sec"] * 40
    header = (
        b"TAUR\x00\x00\x00\x00" +
        struct.pack("<I", new_vsize) +
        struct.pack("<I", new_vaddr) +
        struct.pack("<I", new_rsize) +
        struct.pack("<I", new_rptr) +
        b"\x00" * 12 +   # relocs ptr + line nums ptr + counts
        struct.pack("<I", 0x40000040)  # IMAGE_SCN_MEM_READ | IMAGE_SCN_CNT_INITIALIZED_DATA
    )
    exe[new_sec_off : new_sec_off + 40] = header

    # NumberOfSections +1
    struct.pack_into("<H", exe, info["pe_pos"] + 4 + 2, info["num_sec"] + 1)

    # SizeOfImage 扩大
    new_size_of_img = (new_vaddr + new_vsize + sa - 1) & ~(sa - 1)
    struct.pack_into("<I", exe, info["size_of_img_off"], new_size_of_img)

    # 新 brotli 块 64bit VA
    new_brotli_va = ibase + new_vaddr

    # AssetEntry.data_ptr (hdr_off + 16, 8 bytes)
    struct.pack_into("<Q", exe, hdr + 16, new_brotli_va)
    # AssetEntry.comp_size (hdr_off + 24, 低 4 字节)
    struct.pack_into("<I", exe, hdr + 24, new_rsize)

    # 写出
    out = DIST / f"EditRegionTauri_{ts}.exe"
    out.write_bytes(bytes(exe))

    # 手动 brotli 解压验证
    with open(out, "rb") as f:
        f.seek(new_rptr)
        c = f.read(new_rsize)
    assert brotli.decompress(c) == html_in, "brotli 解压不匹配！"
    print(f"  ✅ brotli 解压验证通过")
    return out


# ==============================================================
# 主流程
# ==============================================================
def main() -> int:
    if not TDUMPER.is_file():
        print(f"[错误] 找不到 tauri-dumper: {TDUMPER}", file=sys.stderr); return 1
    if not EXE_ORIGINAL.is_file():
        print(f"[错误] 找不到原 exe: {EXE_ORIGINAL}", file=sys.stderr); return 1
    if not (HERE / "build_singlefile_html.py").is_file():
        print(f"[错误] 找不到 build_singlefile_html.py", file=sys.stderr); return 1

    # 1. 生成 single.html
    print(f"[1/3] build_singlefile_html.py ...")
    r = subprocess.run([sys.executable, str(HERE / "build_singlefile_html.py")], cwd=str(HERE))
    if r.returncode != 0:
        print("  ❌ 失败"); return 1
    html_in = SINGLE_HTML.read_bytes()
    print(f"  ✅ {SINGLE_HTML.name}: {len(html_in)/1024/1024:.2f} MB")

    # 2. 算 brotli 大小 + 解析 PE
    print(f"\n[2/3] brotli 压缩 + PE 解析 ...")
    new_compressed = brotli.compress(html_in, quality=7)
    pe_info = parse_pe(EXE_ORIGINAL.read_bytes())
    orig_comp = pe_info["comp_size"]
    print(f"  新 brotli q=7: {len(new_compressed):,} bytes")
    print(f"  原 brotli:    {orig_comp:,} bytes")
    diff = len(new_compressed) - orig_comp
    print(f"  差值:         {diff:+,} bytes  "
          f"({'可 Fast Path' if diff <= 0 else '⚠ 变大了，Fallback PE 注入'})")

    # 3. 执行
    print(f"\n[3/3] 生成 exe ...")
    ts = datetime.now().strftime("%y.%m.%d.%H")

    if diff <= 0:
        print("  → Fast Path: tauri-dumper repack ...")
        out = try_tauri_dumper_repack(len(new_compressed), orig_comp, ts)
        if out:
            mode = "tauri-dumper repack"
        else:
            print("  tauri-dumper 失败，回退 PE 注入 ...")
            out = build_new_section_exe(html_in, ts)
            mode = "PE 注入 + TAUR section (fallback)"
    else:
        print("  → Fallback: PE 注入（新 brotli 超 size rule）...")
        out = build_new_section_exe(html_in, ts)
        mode = "PE 注入 + TAUR section"

    print(f"\n{'='*60}")
    print(f"✅ 完成！")
    print(f"  模式: {mode}")
    print(f"  输出: {out}")
    print(f"  大小: {out.stat().st_size:,} bytes")
    print(f"  原 exe: {EXE_ORIGINAL.stat().st_size:,} bytes")
    print(f"{'='*60}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
