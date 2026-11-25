# SKILL: Tauri 2.x 前端资源热替换（零 Rust 编译）

## 触发条件

当你需要修改 Tauri 2.x 应用（包括本 ROI 编辑器 `EditRegionTauri`）的前端 HTML/CSS/JS 时，
**不要跑 `cargo build --release`**（首次 7min，增量 2min+）。
直接用本技能的流程在二进制层面 brotli 块替换，**秒级完成**。

## 前置工具链

| 工具 | 位置 | 用途 |
|------|------|------|
| **tauri-dumper.exe** | `ROI区域编辑器_2606091329/tauri-dumper-x86_64-pc-windows-msvc/tauri-dumper.exe` | 资产表扫描 + brotli 替换 |
| **rg.exe (ripgrep)** | `ROI区域编辑器_2606091329/ripgrep-15.2.0-x86_64-pc-windows-msvc/rg.exe` | 快速搜索代码 / Cargo.lock |
| **python (3.13)** | 系统 PATH 已有 | 打包单文件 HTML + 构建脚本 |

> 为什么 ripgrep 比 PowerShell `Select-String` 好用？rg 自动跳过 `.git/`、`target/`、`node_modules/`，
> `-n --fixed-strings` 搜字面量（不要正则转义），`--json` 可机器解析。

## 一键脚本

```bash
# EditRegionTauri 专用：内联 p5.min.js + 测试图 → 单文件 HTML → repack → 时间戳 exe
cd ROI区域编辑器_2606091329/EditRegionTauri
python build_exe.py
# → dist/EditRegionTauri_26.11.25.15.exe
```

## 原理速查

```
Tauri 2.x 嵌入格式（PE .rdata section）:
┌───────────────────────────────────────────────┐
│ tauri.conf.json → tauri-build 在 build.rs    │
│   里用 brotli.compress(frontendDist)           │
│   生成 phf::Map<&'static str, &'static [u8]>   │
│   每个 value 是 raw brotli stream（无 magic）   │
└───────────────────────────────────────────────┘
         ▲
         │ tauri-dumper 扫资产表
         │ (搜 "tauri://localhost" 定位区域)
         │
    manifest 示例:
    {
      "assets": [{
        "name": "/index.html",
        "location": {
          "data_offset": 6369639,       ← exe 内 brotli 块起点 (file offset)
          "original_compressed_size": 5619918
        },
        "decompressed_size": 8325923
      }]
    }
         ▼
    repack 做的事:
    brotli.compress(新内容) → new_compressed_size
    如果 new_compressed_size <= original_compressed_size
      原地写入 data_offset..data_offset+new_compressed_size
      （超出部分自动 zero-fill，exe 大小不变）
    否则 → 失败（--skip-oversized 跳过）
```

## 通用 Tauri 2.x 替换流程（非本项目也适用）

```powershell
$TD = "ROI区域编辑器_2606091329/tauri-dumper-x86_64-pc-windows-msvc/tauri-dumper.exe"
$EXE = "path/to/YourApp.exe"
$ASSETS = "path/to/replace_assets"  # 目录结构匹配资产表的路径名

# 1. 列出 exe 里嵌了什么
& $TD list $EXE
# Assets: 3
# ├── index.html (1.2 MiB compressed, 7.5 MiB decompressed)
# ├── _app/assets/app.css (120 KiB compressed, 400 KiB decompressed)
# └── _app/assets/app.js  (500 KiB compressed, 1.8 MiB decompressed)

# 2. 提取一份（拿到 manifest）
& $TD extract $EXE -o $ASSETS --overwrite

# 3. 修改 $ASSETS 里的对应文件（保持路径名完全匹配！）
#    比如要改 index.html → 直接覆盖 $ASSETS/index.html
#    要改 _app/assets/app.js → 覆盖 $ASSETS/_app/assets/app.js

# 4. 写回 patched exe
& $TD repack $EXE --assets $ASSETS -o "YourApp.patched.exe" --allow-source-mismatch

# 5. 验证
& $TD list "YourApp.patched.exe"
```

### Size Rule 注意事项

repack 的**硬约束**：`new_compressed_size <= original_compressed_size`

常见坑：
- 你加了图片/字体 base64，新 HTML 变大 → brotli 后也变大 → 失败
  - 解决：`--skip-oversized` 跳过超标的，或用压缩率更高的图片
- JS/CSS 加了大量代码 → 压缩块变大
  - 解决：先 `--skip-oversized` 看哪几个超了，逐个优化

## ripgrep 在本项目的使用范式

```powershell
$RG = "ROI区域编辑器_2606091329/ripgrep-15.2.0-x86_64-pc-windows-msvc/rg.exe"

# 1. 查 Cargo.lock 里某个依赖的版本 / 依赖树
& $RG -n --fixed-strings "name = \"tauri-utils\"" "src-tauri/Cargo.lock"
# 再看它带了哪些 feature（往下找 dependencies 块）
& $RG -A 20 --fixed-strings "name = \"tauri-utils\"" "src-tauri/Cargo.lock"

# 2. 在 src-tauri/ 下搜前端路径引用
& $RG -n "frontend_dist|frontendDist|index.html" "src-tauri/"

# 3. 搜 Cargo.toml 里启用了哪些 tauri feature
& $RG -n "tauri" "src-tauri/Cargo.toml"
```

## 故障排查速查表

| 现象 | 可能原因 | 解决 |
|------|----------|------|
| `tauri-dumper verify` 说 no assets found | 不是 Tauri 应用；或 Tauri < 1.0；或 bundle 类型是 dmg/appimage | 先 `list` 确认资产表存在 |
| repack 失败：compressed too large | 新内容 brotli 后超了 size rule | 1. `--skip-oversized` 跳过 2. 优化图片/代码 3. 找原 exe 更大的版本替换 |
| repack 后 patched exe 启动不了 | macOS 签名被破坏 | `codesign --force --deep --sign - App.patched`；或加 `--ad-hoc-sign` |
| 替换后 HTML 没变 | 资产表路径不匹配！你覆盖了文件但名字/路径对不上 | `tauri-dumper extract` 看 manifest 里的 `name` 字段，**必须完全一致** |
| `allow-source-mismatch` 是什么 | manifest 里有原 exe 的 SHA-256，默认要求源 exe 没被手动改过 | 我们每次都要加——因为我们就是要替换内容 |
| ripgrep `rg: regex parse error` | PowerShell 转义 `\` 很糟 | 用 `--fixed-strings`（字面量），或单引号包整个 pattern |

## 与本项目的关系

- **EditRegionTauri**：本技能的**直接受益者**，HTML 改动不再触发 7min Rust 编译
- **ripgrep**：项目自带，比 PowerShell `Select-String` 快 50x，自动跳过 target/.git
- **tauri-dumper**：来自 [Mas0nShi/tauri-dumper](https://github.com/Mas0nShi/tauri-dumper)，MIT

## build_exe.py 当前行为

```
输入:  src-tauri/assets/index.html（原始 HTML，引用 p5.min.js + 测试图路径）
      p5.min.js
      ../图像_0000_20261102055654.png
步骤:  ① build_singlefile_html.py → index.single.html（内联 100%）
      ② 拷到 _repack_assets/index.html
      ③ tauri-dumper repack（brotli 压缩 → 写回 dist/EditRegionTauri.exe 的 data_offset=0x613827）
输出:  dist/EditRegionTauri_YY.MM.DD.HH.exe
耗时:  < 5s（跳过 brotli 压缩的 CPU 时间）
```
