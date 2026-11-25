#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
一键把最新 index.html 打包进 Tauri exe，零 Rust 编译，秒级完成。

流程:
  1. 调用 build_singlefile_html.py 生成 index.single.html
     （把 p5.min.js 与测试图像都内联进单一 HTML 文件）
  2. 用 tauri-dumper repack 把 single.html brotli 压缩后写回 exe
     （Tauri 2.x 前端资产就是 brotli 流，tauri-dumper 扫资产表定位 data_offset）
  3. 输出带时间戳后缀的 exe（格式 EditRegionTauri_YY.MM.DD.HH.exe）

前置条件:
  - tauri-dumper.exe 在 ROI区域编辑器_2606091329/tauri-dumper-x86_64-pc-windows-msvc/
  - 原 EditRegionTauri.exe 在 dist/ 下
  - 替换内容 brotli 压缩后大小 <= 原 brotli 块大小
    （single.html 比 assets/index.html 大 0-2% 是正常的，因为内联了图像 base64；
     但 brotli 会让压缩块变小或持平——我们实测每次替换都能过）

原理:
  Tauri 2.x 前端资产嵌入格式:
    PE .rdata section 末尾有一个 phf::Map<&'static str, &'static [u8]>
    每个 &'static [u8] 指向 brotli 原始压缩流（无 magic header）
  tauri-dumper 流程:
    1. 在 .rdata 里搜 "tauri://localhost" 定位资产表区域
    2. 扫资产表 header_offset → data_offset → compressed_size 链
    3. brotli.decompress(data_offset..data_offset+compressed_size) 拿到原始文件
    4. repack 时 brotli.compress(新内容) → 如果 <= original_compressed_size → 原地写入

用法:
  cd EditRegionTauri
  python build_exe.py
"""

import subprocess
import sys
import shutil
from pathlib import Path
from datetime import datetime

HERE = Path(__file__).resolve().parent
DIST = HERE / "dist"
EXE_ORIGINAL = DIST / "EditRegionTauri.exe"

# tauri-dumper 路径（项目根目录的 ripgrep 旁边）
TDUMPER = (
    HERE.parent
    / "tauri-dumper-x86_64-pc-windows-msvc"
    / "tauri-dumper.exe"
)

# repack 临时目录（也放了 .gitignore 里）
REPACK_DIR = HERE / "_repack_assets"


def _check_prereqs() -> str:
    """检查前置条件，返回错误消息（空字符串表示 OK）"""
    if not TDUMPER.is_file():
        return f"找不到 tauri-dumper: {TDUMPER}"
    if not EXE_ORIGINAL.is_file():
        return f"找不到原 exe: {EXE_ORIGINAL}"
    if not (HERE / "build_singlefile_html.py").is_file():
        return f"找不到 build_singlefile_html.py"
    if not (HERE / "index.html").is_file():
        return f"找不到 index.html"
    if not (HERE / "p5.min.js").is_file():
        return f"找不到 p5.min.js"
    return ""


def step1_build_single_html() -> bool:
    """调用 build_singlefile_html.py 生成 index.single.html"""
    print(f"\n[1/3] build_singlefile_html.py ...")
    r = subprocess.run(
        [sys.executable, str(HERE / "build_singlefile_html.py")],
        cwd=str(HERE),
    )
    if r.returncode != 0:
        print("  ❌ build_singlefile_html.py 返回非零")
        return False
    single = HERE / "index.single.html"
    if not single.is_file():
        print("  ❌ index.single.html 未生成")
        return False
    print(f"  ✅ {single.name}: {single.stat().st_size / 1024 / 1024:.2f} MB")
    return True


def step2_prepare_repack_dir() -> bool:
    """准备 repack 目录：single.html → _repack_assets/index.html"""
    print(f"\n[2/3] 准备 repack 目录 ...")
    if REPACK_DIR.is_dir():
        shutil.rmtree(REPACK_DIR)
    REPACK_DIR.mkdir()
    shutil.copy(HERE / "index.single.html", REPACK_DIR / "index.html")
    print(f"  ✅ {REPACK_DIR}/index.html ({(REPACK_DIR/'index.html').stat().st_size/1024/1024:.2f} MB)")
    return True


def step3_repack() -> bool:
    """tauri-dumper repack → 带时间戳后缀的新 exe"""
    print(f"\n[3/3] tauri-dumper repack ...")
    ts = datetime.now().strftime("%y.%m.%d.%H")  # 26.11.25.14
    patched = DIST / f"EditRegionTauri_{ts}.exe"

    if patched.is_file():
        patched.unlink()

    result = subprocess.run(
        [
            str(TDUMPER), "repack",
            str(EXE_ORIGINAL),
            "--assets", str(REPACK_DIR),
            "-o", str(patched),
            "--allow-source-mismatch",
        ],
        capture_output=True, text=True, cwd=str(HERE),
    )
    print("  " + result.stdout.strip().replace("\n", "\n  "))
    if result.returncode != 0:
        print(f"  ❌ repack 失败:\n  {result.stderr.strip()}")
        return False

    # 验证
    v = subprocess.run(
        [str(TDUMPER), "list", str(patched)],
        capture_output=True, text=True, cwd=str(HERE),
    )
    print("  " + v.stdout.strip().replace("\n", "\n  "))

    print(f"\n{'='*60}")
    print(f"✅ 完成！")
    print(f"  输出: {patched}")
    print(f"  大小: {patched.stat().st_size:,} bytes")
    print(f"{'='*60}")
    return True


def main() -> int:
    err = _check_prereqs()
    if err:
        print(f"[错误] {err}", file=sys.stderr)
        return 1

    if not step1_build_single_html():
        return 1
    if not step2_prepare_repack_dir():
        return 1
    if not step3_repack():
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
