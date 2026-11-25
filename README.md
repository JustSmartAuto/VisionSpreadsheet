# OpenCV 机器视觉助手

基于 **C#、WinForms 和 OpenCvSharp** 的机器视觉实验与检测框架。项目将图像显示、ROI 编辑、视觉算法、参数数据、工业相机和业务界面分层组织，可用于离线图像算法验证，也可连接工业相机进行采集、定位、标定与识别。

> 当前主程序为 `visionForm`，目标框架为 **.NET Framework 4.7.2**，仅支持 Windows。

## 主要功能

- **图像采集**：通过统一的 `Icam` 接口支持海康、大华、Basler 相机，提供单帧、连续采集、停止、曝光和增益设置。
- **图像显示与 ROI**：图像缩放、平移、像素坐标/灰度显示，以及矩形、旋转矩形、圆、扇形、多边形和卡尺区域的绘制与编辑。
- **定位与测量**：NCC/Canny/轮廓模板匹配、直线与圆拟合、霍夫直线/圆、直线/圆卡尺和多种 Blob 检测。
- **图像处理**：滤波、增强、形态学、轮廓操作、双图运算、ROI 掩膜与坐标转换。
- **标定**：九点像素—物理坐标标定、仿射变换、旋转中心计算和物理刻度显示。
- **识别**：PaddleOCR 文本识别，以及 ZXing.Net 二维码/条码解码。
- **业务功能**：检测流程配置、配方管理、TCP 通信、日志、胶路检测和结果输出。

## 项目结构

| 项目 | 职责 |
| --- | --- |
| `visionForm` | 主 WinForms 程序；相机、定位、标定、配方、OCR、二维码及胶路检测界面 |
| `FuncToolLib` | OpenCV 视觉算法与工具基类，包含定位、测量、标定、预处理和胶路检测 |
| `ParamDataLib` | 各视觉工具的参数与结果数据，提供矩阵和图像读写辅助方法 |
| `DeviceLib` | 相机抽象及海康、大华、Basler 实现 |
| `VisionShowLib` | 图像显示、缩放平移、ROI 绘制编辑和检测结果叠加控件 |
| `UCLib` | 通用 WinForms 自定义控件 |
| `WindowsFormsApp2` | 算法试验界面，可单独验证模板、卡尺、Blob、标定等功能 |
| `WindowsFormsApp1`、`pictureboxTest` | 其他界面与显示实验项目 |
| `Dll1` | C++ 形状匹配 DLL 项目 |
| `testimages`、`WindowsFormsApp2/TestImages` | 离线测试图片 |
| `Dll`、`visionForm/dll` | 项目直接引用的第三方二进制依赖 |

解决方案入口为 [`OpencvSharpDemo.sln`](OpencvSharpDemo.sln)。

## 构建与运行

### 环境要求

- Windows
- Visual Studio 2019 或兼容的更新版本
- .NET Framework 4.7.2 Developer Pack
- 如需编译 `Dll1`，安装 Visual Studio 的“使用 C++ 的桌面开发”工作负载

### 步骤

1. 使用 Visual Studio 打开 `OpencvSharpDemo.sln`。
2. 在解决方案上执行 **还原 NuGet 程序包**。项目同时包含传统 `packages.config` 和 `PackageReference` 引用，请勿手动混用不同版本的 OpenCvSharp 包。
3. 将 `visionForm` 设为启动项目，选择 `Debug` 配置后运行。其 Debug 配置使用 x64。
4. 无相机时，可在图像控件中加载本地图片；仓库内的 `testimages` 可作为算法测试输入。
5. 使用真实相机前，安装对应厂商的驱动/运行时，并确认设备可被厂商工具识别。

也可以在安装了 .NET Framework MSBuild 的开发者命令行中构建：

```bat
nuget restore OpencvSharpDemo.sln
msbuild OpencvSharpDemo.sln /t:Build /p:Configuration=Debug /p:Platform="Any CPU"
```

注意：`visionForm` 的 Release 配置目标为 x86，而 Debug 为 x64；切换配置时需同时核对原生依赖和相机运行时的位数。

## 基本使用流程

1. **准备图像**：连接相机并采集图像，或直接加载本地图像。
2. **设置 ROI**：在显示控件中绘制并调整检测区域。
3. **配置工具**：在“定位检测”中设置模板、直线、圆或 Blob 工具及其参数，按需添加图像前处理。
4. **运行验证**：查看叠加结果、检测状态与耗时，使用测试图片反复调整阈值和搜索范围。
5. **保存配方**：将模板、ROI、检测参数等保存到配方，后续可切换或导入配方。
6. **坐标标定**：需要物理坐标时，采集至少九组对应点，计算并保存坐标变换矩阵。
7. **文字或二维码识别**：在当前图像上执行 OCR 或“二维码识别”，识别文本显示在对应文本框中。

### OCR 模型路径

主界面加载时会初始化 PaddleOCR，代码中的模型路径为：

```text
../Debug/inference/ch_PP-OCRv4_det_infer
../Debug/inference/ch_PP-OCRv4_rec_infer
../Debug/inference/ch_ppocr_mobile_v2.0_cls_infer
../Debug/inference/ppocr_keys.txt
```

这些路径相对于程序当前工作目录。运行主界面前，需要按上述目录放置 PP-OCR 检测、识别、方向分类模型和字典；如果输出目录不同，请同步调整 `visionForm/frmCalibration.cs` 中的路径。

## 核心代码导航

- 主入口：`visionForm/Program.cs`
- 主业务界面：`visionForm/frmCalibration.cs`
- OCR 与二维码：`visionForm/frmCalibration.cs` 中的 `button1_Click`、`btqrcode_Click`
- 算法统一接口：`FuncToolLib/IRunTool.cs`
- 工具与结果基类：`FuncToolLib/ToolBase.cs`
- 参数基类：`ParamDataLib/ParmasDataBase.cs`
- 相机统一接口：`DeviceLib/Cam/Icam.cs`
- 图像显示与 ROI：`VisionShowLib/VisionShowControl.cs`
- 算法试验入口：`WindowsFormsApp2/Form1.cs`

视觉工具通常继承 `ToolBase`，接收 `OpenCvSharp.Mat` 和对应的参数对象，返回包含运行状态、异常信息、耗时及结果图像的 `Result`：

```csharp
IRunTool tool = new BlobTool();
BlobData data = new BlobData
{
    ROI = new OpenCvSharp.Rect(100, 100, 300, 200)
};

Result result = tool.Run(inputImage, data);
```

## 二次开发

新增视觉工具时，建议按现有分层方式实现：

1. 在 `ParamDataLib` 中定义参数和结果数据。
2. 在 `FuncToolLib` 中继承 `ToolBase`，实现 `Run<T>`。
3. 在 `visionForm` 中接入参数编辑、ROI、执行和结果展示。
4. 将对应参数纳入配方保存/加载流程。

仓库还保留了未编入当前 `DeviceLib.csproj` 的 Halcon DataMatrix/QR 读取实验源码 `DeviceLib/CodeReader/FindDataCode2d.cs`；主程序当前二维码识别实际使用 ZXing.Net，不依赖该 Halcon 文件。

## 许可证

见 [LICENSE](LICENSE)。
