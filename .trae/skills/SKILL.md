# SKILL: Tauri 2.x 前端资源热替换（零 Rust 编译）

## 触发条件

修改 Tauri 2.x 应用的前端 HTML/CSS/JS 时，**不要跑 `cargo build --release`**（首次 7min+）。
用本技能在二进制层面替换 brotli 压缩块，**秒级完成**（支持任意大小）。

## 一键脚本

```bash
cd ROI区域编辑器_2606091329/EditRegionTauri
python build_exe.py
# → dist/EditRegionTauri_26.11.25.16.exe
```

脚本自动选择两种模式:
- **[Fast] tauri-dumper repack** — brotli 块 ≤ 原大小时，原地覆盖，秒级
- **[Fallback] PE 注入 + 新 section** — brotli 块变大时，文件末尾追加 + 节表加 section + 改 data_ptr，支持任意大小

## 前置工具链

| 工具 | 位置 | 用途 |
|------|------|------|
| **tauri-dumper.exe** | `ROI区域编辑器_2606091329/tauri-dumper-x86_64-pc-windows-msvc/` | 资产表扫描（只走 Fast Path 时需要） |
| **rg.exe** | `ROI区域编辑器_2606091329/ripgrep-15.2.0-x86_64-pc-windows-msvc/` | 快速搜 Cargo.lock / 代码 |
| **python 3.13 + brotli** | 系统 PATH | brotli 压缩/解压 + PE 操作 |

## 原理速查（关键！）

```
Tauri 2.x 资产表在 .rdata section 末尾（build.rs 生成的 struct AssetEntry）:

偏移  字段        本项目值                    说明
───   ────       ────────                    ────
+0    name_ptr   0x000000014061435c          64bit VA = ImageBase(0x140000000) + RVA(0x61435c)
+8    name_len   11                          len("/index.html")
+16   data_ptr   0x0000000140614367  ← 改!  brotli 块起点的 64bit VA
+24   comp_size  5619918              ← 改!  brotli 压缩块大小（只用低 32bit）
```

**指针格式**（反编译 + 脚本验证通过）：
```
64bit VA = ImageBase (PE64 默认 0x140000000) + 32bit RVA
low32 = 0x40614367 → RVA = 0x40614367 - 0x40000000 = 0x614367
```

**brotli 块**是 raw brotli stream（无 magic header），直接嵌在 data_offset 处。

### Fast Path: tauri-dumper repack

```
tauri-dumper repack --assets repack_dir -o out.exe
内部: 扫 .rdata 找资产表 header_offset → data_offset → brotli.decompress
      新内容 brotli.compress → 若 ≤ 原块大小 → 原地覆盖 + 零填充
约束: new_compressed_size ≤ original_compressed_size
```

### Fallback: PE 注入 + 新 section（任意大小）

```python
# 1. 追加新 brotli 块到文件末尾（对齐 FileAlignment=0x200）
pad_to = (cur_end + fa - 1) & ~(fa - 1)
exe.extend(b"\x00" * (pad_to - cur_end))
exe.extend(new_brotli_block)

# 2. 节表末尾加 "TAUR" section header（40 字节）
#    VSIZE 对齐 SectionAlignment=0x1000, RSIZE=真实大小
#    characteristics = 0x40000040 (READ + INITIALIZED_DATA)
new_sec_off = sh_off + num_sections * 40
exe[new_sec_off : new_sec_off + 40] = header_bytes

# 3. NumberOfSections +1 (CoffHeader @ pe_pos + 2)
# 4. SizeOfImage 扩大覆盖新 section
# 5. AssetEntry.data_ptr → ImageBase + 新 section VADDR
# 6. AssetEntry.comp_size → 新 brotli 大小
```

## build_exe.py 流程

```
[1/3] build_singlefile_html.py → index.single.html (7.95 MB, 内联 p5+图)
[2/3] brotli q=7 → new_compressed_size (5.63 MB) vs orig (5.62 MB)
      差值 +9,541B → 触发 Fallback
[3/3] PE 注入:
      ① 文件末尾追加 brotli + FileAlignment 填充
      ② 节表加 "TAUR" section + SectionAlignment 对齐 VSIZE
      ③ SizeOfImage 扩大
      ④ data_ptr/comp_size 改指针
输出: dist/EditRegionTauri_YY.MM.DD.HH.exe (20 MB 左右)
验证: 手动 brotli.decompress 新块 == 输入 HTML ✅
```

## 通用 Tauri 2.x repack（非 PE 注入）

```powershell
$TD = "tauri-dumper-x86_64-pc-windows-msvc/tauri-dumper.exe"
& $TD list  "App.exe"                          # 看嵌了什么
& $TD extract "App.exe" -o ./assets --overwrite  # 提取
# 修改 ./assets/index.html 等（路径必须匹配 manifest 里的 name 字段！）
& $TD repack  "App.exe" --assets ./assets -o "App.patched.exe" --allow-source-mismatch
```

## ripgrep 使用范式

```powershell
$RG = "ripgrep-15.2.0-x86_64-pc-windows-msvc/rg.exe"

# 搜 Cargo.lock 里某个依赖的完整块
& $RG -A 20 -n --fixed-strings 'name = "tauri-utils"' src-tauri/Cargo.lock

# 搜 src-tauri/ 下前端路径
& $RG -n "frontend_dist|frontendDist" src-tauri/

# 搜 Cargo.toml 里 tauri feature
& $RG -n "tauri" src-tauri/Cargo.toml
```

## 故障排查

| 现象 | 可能原因 | 解决 |
|------|----------|------|
| PE 注入后 exe 启动不了 | 新 section 未按 SectionAlignment=0x1000 对齐 | 必须对齐到 4KB |
| 或 AssetEntry 指针没改成新 VA | 再跑 `_verify_pointers.py` 验证 data_ptr |
| brotli decompress 失败 | data_offset 指针指错位置 | 新块 file_offset → RVA → VA 计算错误 |
| tauri-dumper extract 在 PE 注入后报错 | 它只认识原布局 | 正常，我们手动验证 brotli |
| ripgrep `regex parse error` | PowerShell 转义糟 | 用 `--fixed-strings` 字面量 |
| repack 失败: compressed too large | size rule | 让 build_exe.py 自动 fallback PE 注入 |

## 本项目硬编码值（编译结果稳定）

这些值来自 build.rs 生成，只要不重新编译就稳定：

| 字段 | 值 | 位置 |
|------|----|------|
| ImageBase | `0x0000000140000000` | PE opt header |
| SectionAlignment | `0x1000` | PE opt header |
| FileAlignment | `0x200` | PE opt header |
| AssetEntry.hdr_off | `0x00B6F238` | brotli_end + 3 |
| AssetEntry.data_ptr | `hdr_off + 16` (8 bytes) | |
| AssetEntry.comp_size | `hdr_off + 24` (4 bytes LE) | |
| 原 data_offset | `6369639` (0x613167) | brotli 块起点 |
| 原 comp_size | `5619918` (0x55C0CE) | brotli 块大小 |

> 如果将来**重新编译 exe**（改了 src-tauri 代码），这些偏移会变。
> 用 `tauri-dumper extract -o _tmp` 重新拿 manifest.json 更新 build_exe.py 里的硬编码值。

## 与本项目的关系

- **EditRegionTauri**：技能的直接受益者，HTML 改动零 Rust 编译
- **ripgrep**：项目自带，比 PowerShell Select-String 快 50x，自动跳过 target/.git
- **tauri-dumper**：来自 [Mas0nShi/tauri-dumper](https://github.com/Mas0nShi/tauri-dumper)，MIT
- **PE 注入方案**：2026-11-25 逆向成果，突破 size rule 限制
