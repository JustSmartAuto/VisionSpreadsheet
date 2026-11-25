# VisionSpreadsheet

**VisionSpreadsheet 是一个以电子表格为交互形式的机器视觉程序框架。** 它面向工业视觉检测与自动化场景，目标是让用户像填写电子表格一样组织视觉工具、配置参数、传递数据并查看执行结果，交互方式类似 Cognex In-Sight Vision Suite。

本项目在开源电子表格 [GridCraft](https://github.com/storytold/gridcraft) 的 Rust 技术基础上演进，并融合了原 NodesFrame 项目在视觉算法、相机采集、运动控制和流程执行方面的设计经验。

## 产品定位

传统电子表格擅长结构化数据与公式计算，机器视觉软件则强调图像采集、算法工具链、检测结果和工业设备协同。VisionSpreadsheet 将两者结合：

- **单元格即配置与结果**：输入参数、工具输出、检测判定和统计数据均以清晰的表格结构呈现。
- **公式驱动的工具组合**：利用公式和单元格引用串联视觉步骤，让数据依赖关系可见、可追踪。
- **表格化视觉工程**：通过工作表组织采图、预处理、定位、测量、检测和结果输出等环节。
- **开放与可自动化**：保留命令、CLI、MCP 和控制通道能力，便于调试、集成和二次开发。

## 目标能力

- 图像源与工业相机采集配置
- 图像预处理、定位、测量、识别与缺陷检测工具
- 基于单元格引用的视觉工具链和依赖计算
- 实时图像、区域、轮廓及检测结果叠加显示
- OK/NG 判定、统计分析与检测记录导出
- 数字 I/O、运动控制及外部触发协同
- 视觉工程的保存、加载、复用与版本管理

> 当前代码库主要提供 GridCraft 的电子表格、公式计算和命令基础设施。上述视觉能力是 VisionSpreadsheet 的产品方向，尚不代表均已实现；实际能力以代码和后续版本说明为准。

## 现有基础

GridCraft 基础框架目前提供：

- Rust 原生桌面应用及 WebAssembly 支持
- 电子表格编辑、样式、图表和 XLSX 文件支持
- 依赖图公式计算、动态数组和 500 多个工作表函数
- 命令化操作、撤销/重做、CLI、MCP 与 JSON 控制通道
- 分层、可替换 UI 的引擎优先架构

## 快速开始

需要安装 [Rust](https://www.rust-lang.org/tools/install)。

```sh
# 运行桌面程序
cargo run --release -p gridcraft

# 打开示例工作簿
cargo run --release -p gridcraft -- --sample sales

# 打开已有工作簿
cargo run --release -p gridcraft -- book.xlsx

# 启动程序并开启本地控制通道
cargo run --release -p gridcraft -- --sample sales --control 7979
```

Web 版本：

```sh
cd apps/gridcraft-web
trunk build --release
# 或
trunk serve --release
```

Web 构建需要 [trunk](https://trunk-rs.github.io/trunk/) 和 `wasm32-unknown-unknown` 编译目标。

## 架构

| 层级 | 模块 |
|---|---|
| L0 | `core`：地址、值、错误、日期和输入解析；`numfmt`：数字格式 |
| L1 | `formula`：公式词法与语法；`functions`：工作表函数库 |
| L2 | `model`：工作簿模型；`calc`：依赖图与计算；`xlsx`：文件读写 |
| L3 | `chart`：图表布局和渲染 |
| L5 | `engine`：会话、命令、历史记录、剪贴板和文件操作 |
| L6 | `ui-egui`：桌面界面与控制通道；`mcp`：MCP 服务 |
| L7 | `apps/gridcraft`、`apps/gridcraft-cli`、`apps/gridcraft-web` |

后续视觉模块将在此基础上接入图像数据、视觉工具、设备抽象与检测执行流程，避免将视觉算法直接耦合到界面层。

## 开发与验证

```sh
# 格式化、Clippy、测试、资源检查、分层检查及 WASM 检查
cargo xtask ci

# 单独检查模块依赖分层
cargo xtask layers
```

贡献代码前请阅读 [AGENTS.md](AGENTS.md)。资源及许可信息见 [ATTRIBUTION.md](ATTRIBUTION.md)，基础框架的路线图见 [ROADMAP.md](ROADMAP.md)。

## 许可证与致谢

代码采用 [MIT](LICENSE-MIT) 或 [Apache-2.0](LICENSE-APACHE) 双许可证，可任选其一。保留 [NOTICE](NOTICE) 中的必要声明；随附资源沿用各自许可证，详见 [ATTRIBUTION.md](ATTRIBUTION.md)。

感谢 GridCraft / ArtCraft 项目提供电子表格基础，感谢 NodesFrame 项目提供视觉与运动控制流程设计参考。Cognex、In-Sight 和 Vision Suite 是其各自权利人的商标；本项目仅用于说明交互与产品形态，不表示与其存在隶属或授权关系。
