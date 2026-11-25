# 仿 In-Sight 电子表格编辑器设计文档

> 目标：在现有 WPF + VisionPro 架构上引入电子表格编程模式，降低 VisionPro ToolBlock + C# Script 的学习门槛，打通 In-Sight 智能相机用户与 VisionPro 桌面软件用户的认知鸿沟。
> 版本：v0.4（Vproj 旧配方向前兼容 + RecipeConverter 自动转换）
> 日期：2026-11-17

---

## 1. 背景与动机

### 1.1 为什么要做电子表格模式

当前项目使用 VisionPro ToolBlock + C# Script 编写检测程序，存在以下问题：

| 问题 | 具体表现 |
|------|----------|
| 学习门槛高 | 需要理解 C#、VisionPro 终端绑定、ToolBlock 执行顺序 |
| 调试困难 | 单步调试需要附加到进程，VisionPro 设计器和自定义运行器之间切换 |
| 状态不可见 | 各工具的中间结果散落在 ToolBlock 各个子块，没有统一可观察界面 |
| 认知断层 | 已有 In-Sight 智能相机使用经验的工程师面对 VisionPro 完全不同的编程模型 |

电子表格模式的优势：

- **所见即所得**：公式立即计算，依赖关系隐式表达（单元格引用）
- **线性思维**：从上到下 = 执行顺序，符合视觉检测流水线直觉
- **统一界面**：参数配置、ROI 拖拽、实时预览都在同一张表上完成
- **可移植性**：In-Sight 用户几乎零成本迁移到 VisionPro 桌面软件

### 1.2 与现有架构的关系

```
现有架构：
  9相机 → ManualInspectionRunner / PlcTriggerLoop → ToolBlock.Run() → 读取 Output_PanDuan

引入电子表格后的架构（增量，不替换）：
  9相机 ──→ 电子表格 Sheet.Evaluate() ──→ VisionPro ToolBlock.Run()
              ↑                                    ↑
              └── 表达式引擎 + 函数库 + Tag 系统 ──┘
                   （本项目新增，独立模块）
```

两种模式共存，通过配方配置切换：
- **VisionPro 模式**（默认）：使用 `.Vproj` 配方 + ToolBlock 检测
- **电子表格模式**：使用 `.jobx` 配方 + Sheet 检测

---

## 2. In-Sight 逆向工程架构分析

> 分析对象：`C:\Program Files\Cognex\In-Sight\In-Sight Vision Suite Editors\In-Sight\In-Sight 26.1.0`

### 2.1 关键 DLL 与职责

```
Cognex.InSight.Liger.SpreadsheetEditor.Shell.dll    ← 电子表格编辑器 UI 壳（薄壳）
  └── SpreadsheetEditor : InSightDocumentBase      ← 主窗口（WPF）
  └── SpreadsheetEditorControlInjector             ← 单例 Control 注入器

Cognex.Designer.Core.dll                           ← ★ 核心框架
  ├── Expressions/                                 ← 表达式引擎
  │   ├── ExpressionData                           ← 单元格表达式容器
  │   ├── Expression                               ← 表达式树根节点
  │   ├── Internal.TokenResolver                   ← 符号解析器（Tag 引用、函数调用）
  │   ├── Internal.StringTokenizer                 ← 词法分析器
  │   ├── Internal.Nodes.*                         ← 语法树节点
  │   ├── Internal.ExpressionDataEvaluator         ← 表达式求值器
  │   └── Internal.HelperFunctions                 ← 内置函数集合
  ├── Functions/                                   ← 函数管理
  │   ├── Internal.IFunctionManager                ← 函数注册表接口
  │   ├── Internal.IFunction                       ← 函数接口
  │   └── Internal.FunctionDescriptor              ← 函数描述符
  ├── Tags/                                        ← 变量/Tag 系统
  │   ├── ITag                                     ← Tag 接口
  │   ├── Internal.Tag                             ← Tag 实现
  │   ├── Internal.TagManager                      ← Tag 管理器
  │   └── Internal.PropertyTag / DotFieldTag       ← 属性 Tag 变体
  ├── Project/                                     ← 项目模型
  │   ├── IProject                                 ← 项目接口
  │   ├── ZipProject                               ← ZIP 格式 Job 文件
  │   └── ProjectContent                           ← 项目内容容器
  └── ObjectModel/                                 ← MVVM 对象基类
      └── NotifyObject                             ← INotifyPropertyChanged 基类

Cognex.Designer.Implementation.dll                 ← 框架实现
  ├── Function.FunctionService                     ← 函数服务（注册、解析、调用）
  ├── Tags.TagService                              ← Tag 服务（解析、订阅、通知）
  ├── Runtime                                      ← 运行时
  └── Project.ZipProject                           ← ZIP 项目读写

Cognex.Designer.Ide.Controls.dll                   ← IDE 控件（编辑器、属性面板）
Cognex.InSight.Runtime.ViewModels.dll              ← Runtime ViewModel
Cognex.InSight.Runtime.Wpf.dll                     ← Runtime WPF 控件
Cognex.InSight.IDE.Common.dll                      ← IDE 基础设施
```

### 2.2 表达式引擎架构（核心）

```
单元格输入：  BlobDetect($A$0, 128, 0, 2)
                │
                ▼
        StringTokenizer（词法分析）
        ├── 标识符：BlobDetect
        ├── 左括号：(
        ├── TokenResolver（符号解析）
        │   ├── $A$0 → Tag/TagPath → 求值 → Image 对象
        │   ├── 128 → LiteralNode
        │   ├── FunctionCallNode
        │   └── 构造 Expression Tree
                │
                ▼
        ExpressionDataEvaluator（表达式求值）
        ├── 前序遍历表达式树
        ├── 遇到 Tag 节点 → TagManager.GetValue(tagPath)
        ├── 遇到 FunctionNode → FunctionManager.Invoke(name, args)
        └── 返回 Blobs 数据结构
```

**关键发现**：In-Sight 的电子表格核心是 **Designer 框架**（表达式 + 函数 + Tag 三元组），SpreadsheetEditor 只是这个框架的一个 UI 宿主。这意味着我们可以**重用同样的架构模式**，而不需要复制 UI。

### 2.3 Job 文件格式

`.jobx` 是 ZIP 容器，内部结构：

```
Job.json.sig           ← 签名
Job.json               ← Job 元数据 + 配置（AcqSettings、JobSettings、Metadata）
  └── Sheets[SheetName].cells[cellName] = [cellName, expression, ... , displayName, {resultBlob}, ...]
```

参考 `WebAPISampleApp/Jobs/ExampleHmiSpreadsheetCells.jobx` 中的真实结构：

```json
{
  "$type": "Sheet",
  "cells": [
    ["A0", "AcquireImage()", 1, {"$type": "Image", ...}],
    ["B1", "Count($A$0,999999,0,0)", 1, 0.0, "AcqCount", {"$type": "Byte[]", "base64": "..."}],
    ["A401", "EditInt(0,255)", 1, {"min": 0, "max": 255, "value": 0}]
  ],
  "columnWidths": [...],
  "rowHeights": [...],
  "timeout": 60000
}
```

### 2.4 函数调用机制

FunctionService（反编译 1264 行）是函数管理核心：

- `RegisterFunction(name, delegate, metadata)` — 注册函数
- `GetFunction(name)` — 查找函数（含元数据、签名、分类）
- `Invoke(name, args)` — 调用函数，处理异常 → 返回 `#ERR`
- 函数按 category 分组（2D/通用、3D、HMI、Comm 等）

参考 `CognexJobxLanguageServerProtocol/src/main/resources/functions.json`：
约 **350+ 内置函数**，涵盖：
- 图像处理：AcquireImage、BlobDetect、FindPattern、ReadID、Caliper ...
- 几何测量：Distance、Angle、LineToLine、BlobToBlob ...
- ROI 定义：Annulus、Circle、Rectangle、Line、Point ...
- 数据访问：GetX、GetY、GetScore、GetNFound ...
- HMI 交互：Button、EditInt、EditFloat、EditString ...
- 系统/通信：ReadDevice、WriteDevice、Delay、Count ...

---

## 3. 设计目标

| # | 目标 | 约束 |
|---|------|------|
| G1 | 电子表格作为**可选**编程模式引入，不替换现有 Vproj 模式 | 新增项目、新配方格式 |
| G2 | 表达式引擎独立、可测试、不依赖 VisionPro UI | 纯 C# 类库，无 WPF 依赖 |
| G3 | **完整实现 functions.json 中全部 2D 函数（218 个）** | 每个函数底层用 VisionPro 对应 API 实现，用户只需 In-Sight 认知即可编程 |
| G4 | 单元格引用支持 A1 记号（相对/绝对/混合） | `A1`、`$A$1`、`$A1`、`A$1`、范围 `A1:B5` |
| G5 | 表达式求值**增量更新**：只重算依赖变更的单元格 | 依赖图 + 拓扑排序 |
| G6 | 支持 VisionPro ToolBlock 作为函数调用 | `RunToolBlock("检测", Image, ...)` |
| G7 | Sheet 结果能直接驱动 PLC 回写 | Sheet 输出单元格 → 2002/2003/2004 寄存器 |
| G8 | 提供 VS Code 插件（可选）语法高亮 + 补全 | 复用 LSP 协议（参考 CognexJobxLanguageServerProtocol） |

---

## 4. 整体架构

```
┌──────────────────────────────────────────────────────────────────────┐
│                         JustSmart.App (WPF)                          │
│  ┌─────────────────────┐  ┌─────────────────────┐  ┌──────────────┐ │
│  │  VisionPro 模式      │  │  电子表格模式 (新)    │  │  PLC 回写     │ │
│  │  ToolBlock.Run()     │  │  Sheet.Evaluate()    │  │  PlcTrigger   │ │
│  └──────────┬──────────┘  └──────────┬──────────┘  └──────┬───────┘ │
│             │                         │                      │         │
└─────────────┼─────────────────────────┼──────────────────────┼─────────┘
              │                         │                      │
              ▼                         ▼                      ▼
┌──────────────────────────────────────────────────────────────────────┐
│              JustSmart.Insight (新增模块)                              │
│  ┌────────────┐  ┌────────────┐  ┌────────────┐  ┌────────────┐      │
│  │ Expression │  │ Function   │  │ Tag        │  │ Job         │      │
│  │ Engine     │  │ Registry   │  │ Manager    │  │ Serializer  │      │
│  └─────┬──────┘  └─────┬──────┘  └─────┬──────┘  └─────┬──────┘      │
│        │               │               │               │              │
│  ┌─────┴───────────────┴───────────────┴───────────────┴──────┐      │
│  │                   CellGraph (依赖图 + 求值调度)              │      │
│  └─────────────────────────────────────────────────────────────┘      │
└──────────────────────────────────────────────────────────────────────┘
              │                         │
              ▼                         ▼
┌──────────────────────────────────────────────────────────────────────┐
│  ┌─────────────────────┐  ┌──────────────────────────────────────┐  │
│  │   VisionPro Runtime  │  │   数据模型 (Image, Blobs, Patterns,  │  │
│  │   CogToolBlock.Run() │  │   Shape, ...) — 桥接到 VisionPro     │  │
│  └─────────────────────┘  └──────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────────────┘
```

### 4.1 模块划分

| 模块 | 职责 | 依赖 |
|------|------|------|
| `JustSmart.Insight.Core` | 表达式引擎 + 词法/语法分析 + 求值器 | 无 WPF 依赖 |
| `JustSmart.Insight.Functions` | 内置函数库（Blob、Pattern、ID、Shape...） | Core + VisionPro |
| `JustSmart.Insight.Job` | Job 文件读写 + Sheet 模型 + Cell 模型 | Core |
| `JustSmart.Insight.UI` | WPF 电子表格控件（DataGrid + 属性面板） | Core + Job + HandyControl |
| `JustSmart.Insight.Bridge` | VisionPro 桥接层（ToolBlock 作为函数、终端绑定） | Functions + VisionPro |

---

## 5. 核心模块详细设计

### 5.1 表达式引擎（`JustSmart.Insight.Core`）

#### 5.1.1 Token 类型

```csharp
internal enum TokenType
{
    Identifier,   // BlobDetect, Count, A1
    Number,       // 128, 3.14, -5
    String,       // "hello"
    CellRef,      // A1, $A$0, $A1, A$1, A1:B5
    LParen,       // (
    RParen,       // )
    Comma,        // ,
    Plus, Minus, Star, Slash, Percent,   // + - * / %
    Less, Greater, LessEq, GreaterEq, Eq, NotEq,  // < > <= >= = <>
    And, Or, Not, // && || ! 或 In-Sight 风格 AND/OR/NOT
    Hash,         // #ERR 标记
    Eof
}
```

#### 5.1.2 语法树节点

```csharp
internal abstract class ExprNode { public abstract object Evaluate(EvalContext ctx); }

internal class LiteralNode    : ExprNode { object Value; }
internal class CellRefNode    : ExprNode { string CellRef; }          // $A$0
internal class FunctionCallNode : ExprNode { string Name; List<ExprNode> Args; }
internal class BinaryNode     : ExprNode { BinaryOp Op; ExprNode Left; ExprNode Right; }
internal class UnaryNode      : ExprNode { UnaryOp Op; ExprNode Operand; }
internal class ConditionalNode : ExprNode { ExprNode Condition; ExprNode Then; ExprNode Else; }  // IF(a,b,c)
internal class RangeNode      : ExprNode { string Start; string End; }    // $A$0:$A$9
```

#### 5.1.3 EvalContext

```csharp
public class EvalContext
{
    // 单元格值查找（处理绝对/相对引用偏移）
    public Func<string, CellValue> GetCellValue { get; set; }
    
    // Tag 查找（跨 Sheet 的命名空间引用，如 Sheet1!Result）
    public Func<string, object> GetTag { get; set; }
    
    // 函数调用
    public Func<string, object[], object> InvokeFunction { get; set; }
    
    // 当前求值单元格（用于相对引用偏移计算）
    public string CurrentCell { get; set; }
    
    // 视觉运行时（VisionPro 图像等）
    public IRuntime Runtime { get; set; }
}
```

#### 5.1.4 求值结果约定

| 情况 | 返回值 | 单元格显示 |
|------|--------|------------|
| 正常计算 | 结果对象（Image、Blobs、double、string...） | 类型相关（数值、图形、表格...） |
| 输入为空/未依赖 | `null` | 空 |
| 计算异常 | `ExprError { Code, Message }` | `#ERR` |
| 函数返回空值 | `ExprEmpty` | 空 / 占位文本 |

#### 5.1.5 增量求值（依赖图）

```csharp
public class CellGraph
{
    // 单元格 → 依赖集合（如 A1 → { B1, C2 }）
    Dictionary<string, HashSet<string>> _deps;
    
    // 反向依赖（谁依赖我）
    Dictionary<string, HashSet<string>> _reverseDeps;
    
    // 当 B1 变化时：
    // 1. 从 _reverseDeps[B1] 找到所有直接依赖者
    // 2. 拓扑排序得到需要重算的完整集合
    // 3. 按拓扑序求值，避免重复计算
    public IReadOnlyList<string> TopoSortForRecompute(string changedCell);
}
```

### 5.2 函数注册表（`JustSmart.Insight.Functions`）

#### 5.2.1 函数接口

```csharp
public interface IInsightFunction
{
    string Name { get; }                    // BlobDetect
    string Category { get; }                // "2D/Blob"
    string Description { get; }
    IReadOnlyList<ParameterInfo> Parameters { get; }
    IReadOnlyList<ResultColumnInfo> ResultColumns { get; }  // 结果表（如 X、Y、Score 列）
    object Invoke(object[] args, EvalContext ctx);
}

public class ParameterInfo
{
    public string Name;        // Image, X, Y, ...
    public Type DataType;      // typeof(Image), typeof(double)...
    public object DefaultValue;
    public bool IsRequired;
    public string Description;
}
```

#### 5.2.2 核心原则：用 VisionPro API 实现全部 functions.json 中的 2D 函数

> 🎯 **用户只需学习 In-Sight 电子表格认知**，不需要理解 VisionPro 的 ToolBlock/终端绑定/C# Script 体系。
> 所有 218 个 2D 函数底层用 VisionPro API 实现，对上层用户透明。

**实现策略**：
- **每个 In-Sight 函数** = 一个 `IInsightFunction` 实现类
- 函数内部 **直接 new VisionPro 对应 Tool**，配置参数，调用 `Run()`
- 结果转换为 In-Sight 风格的数据结构（Blobs、Patterns、Edges...）
- 参数签名与 [functions.json](file:///d:/JustSmart/261010优易达安全带/CodeBase/SeatBeltInspection_2611161017/Reference/CognexJobxLanguageServerProtocol/src/main/resources/functions.json) 完全一致

#### 5.2.3 函数 → VisionPro API 完整映射表

##### A. 图像与 ROI（纯数据结构，无 VisionPro Tool）

| In-Sight 函数 | VisionPro 对应类型 | 说明 |
|--------------|-------------------|------|
| `Annulus` | `CogRectangleAffine` (inner/outer radius 计算) | 环形 ROI 结构体 |
| `Circle` | `CogCircle` | 圆心 + 半径 |
| `Cross` | `CogCross` | 十字标记 |
| `Line` | `CogLine` | 两点直线 |
| `Point` | `CogPoint2D` | 点坐标 |
| `Polygon` | `CogPolygon` | 多边形（点数组） |
| `Region` | `CogRegion` | 区域 |
| `Image` | `ICogImage` | 包装 VisionPro 图像 |

##### B. 检测类（核心 → VisionPro Tool.Run()）

| In-Sight 函数 | VisionPro Tool | 关键参数映射 |
|--------------|----------------|-------------|
| `Blob` / `DetectBlobs` / `FindBlobs` / `FindBlobs` | `CogBlobTool` | Polarity→ThresholdPolarity, Threshold→ThresholdMode.Absolute, MinArea→MinArea, MaxArea→MaxArea |
| `SortBlobs` | 对 Blob 结果排序 | Area/Score/Position/Radial |
| `FindPatMaxRedLine` / `FindPattern` | `CogPMAlignTool` | Region→SearchRegion, PatternFile→TrainedPattern, AcceptThreshold→AcceptScore |
| `Caliper` / `FindEdge` / `Edge` | `CogCaliperTool` | Region→SearchRegion, EdgeMode→Polarity, MinContrast→ContrastThreshold |
| `FindLine` | `CogLineFindTool` 或 Caliper + 拟合 | |
| `FindMultiLine` | 多 Caliper 并行 + 分组 | |
| `FindCircle` | `CogCircleFindTool` | |
| `ReadIDMax` / `ID` / `ReadID` | `CogBarCodeTool` + `CogQRCodeTool` + `CogDataMatrixTool` | CodeType→Symbology, StringLength→MinStringLength/MaxStringLength |
| `OCRMax` | `CogOCRMaxTool` | CharacterSet→AllowedChars, MinConfidence→Confidence |
| `DetectFlaw` | `CogDetectFlawTool`（若可用）或 Blob + 形态学组合 | |
| `EdgeAIAnomalyDetect` / `EdgeAIClassify` / `EdgeAISegment` | VisionPro ViDi 深度学习工具（ViDiEL/ViDiRT/ViDiFR） | |
| `SurfaceFlaw` | `CogSurfaceFlawTool`（若可用）或 ViDi | |

##### C. 几何测量类（纯 C# 计算，部分用 VisionPro CogTools.Geometry）

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `PointToPoint` / `PointToPointDistance` / `PointToPointAngle` | `CogMath.Distance()` / `CogMath.Angle()` |
| `PointToLine` | 点到直线距离公式 |
| `PointToCircle` | 点到圆心距离 ± R |
| `LineToLine` / `LineToCircle` / `CircleToCircle` | `CogMath.LineLineDistance()` 等 |
| `BlobToBlob` / `BlobToLine` / `BlobToPoint` | 取 Blob Centroid + 几何计算 |
| `LineFromNPoints` / `CircleFromNPoints` | 最小二乘拟合 `CogFitLineTool` / `CogFitCircleTool` |
| `MidLineToMidLine` | 取两条 Line 的中点后计算 |
| `PairDistance` / `PairMeanDistance` / `PairMaxDistance` / `PairMinDistance` / `PairSDevDistance` | N 对 Blob/Pattern 之间距离的统计 |
| `PairsToEdges` | 配对关系 → 边线 |

##### D. 标定与坐标变换

| In-Sight 函数 | VisionPro API |
|--------------|--------------|
| `Calibrate` / `CalibratePoints` / `CalibrateGrid` / `CalibrateImage` | `CogCalibCheckerboardTool` / `CogCalibNPointToNPointTool` / `CogCalibSimpleCalibrationTool` |
| `ExtractCalibration` / `ExtractCalibrationPoints` | 读取标定对象 |
| `TransPixelToWorld` / `TransWorldToPixel` / `TransFixtureToPixel` / `TransPixelToFixture` | `ICogTransform2DLinear.MapPoint()` |
| `TransBlobsToWorld` / `TransPatternsToWorld` / `TransEdgesToWorld` | 遍历每个元素，MapPoint 变换 |
| `TransBlobsToFixture` / `TransEdgesToFixture` / `TransPatternsToFixture` | Fixture 坐标变换 |
| `TransFixtureToWorld` / `TransWorldToFixture` | Fixture↔世界坐标系 |
| `TransIDToWorld` / `TransOCRMaxToWorld` / `TransInspEdgeWidthToWorld` | 特定结果类型的坐标变换 |

##### E. 统计与数学（纯函数，无 VisionPro Tool）

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `Count` / `CountPassFail` | 结果集合 `.Count` / LINQ |
| `Minimum` / `Maximum` | `Enumerable.Min()` / `.Max()` |
| `ComputeStats` / `Statistics` | Mean/Min/Max/SDev/Histogram 计算 |
| `Histogram` / `HistMin` / `HistMax` / `HistMean` / `HistSDev` / `HistSum` / `HistSumSquare` / `HistContrast` / `HistThresh` / `HistHead` / `HistTail` / `HistHeadPercentage` / `HistTailPercentage` | 对 Image 像素遍历，构建直方图 |
| `Accumulate` / `ClockedSum` / `ClockedMax` / `ClockedMin` | 内置状态机 + 事件触发 |
| `Math` / `Trigonometry` / `Operators` / `Logic` | 直接 C# 运算符和 `Math.Sin/Cos/Sqrt/Abs/...` |
| `CompareImage` / `ImageMath` / `ScaleImage` / `Colorize` / `TransformImage` | `CogImage.ColorToGray()` / `CogImage.Subtract()` / 像素级操作 |

##### F. 图形绘制（Plot* 系列 → VisionPro 覆盖层）

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `PlotLine` / `PlotCircle` / `PlotPoint` / `PlotCross` / `PlotRectangle` / `PlotAnnulus` | 在图像对象上 `Graphics.DrawLine()` / `DrawEllipse()` 或 VisionPro `CogGraphicLine` / `CogGraphicCircle` 叠加 |
| `PlotText` / `PlotString` | 叠加文字标签 |
| `PlotImage` / `PlotData` / `PlotGraphics` / `PlotPolygon` / `PlotRegion` / `PlotCompositeRegion` / `PlotArc` | 各种叠加方式 |

##### G. Fixture 定位

| In-Sight 函数 | VisionPro 实现 |
|--------------|---------------|
| `Fixture` | 读取 Pattern 匹配结果 → `CogTransform2DLinear` |
| `Fixtures` | 多个 Fixture 列表 |
| `TransBlobsToFixture` / `TransEdgesToFixture` | Fixture 变换 |

##### H. HMI 控件（UI 层实现，无 VisionPro Tool）

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `Button` | 单元格内 WPF Button |
| `EditInt` / `EditFloat` / `EditString` | 单元格内 WPF Slider + TextBox |
| `CheckBox` | 单元格内 WPF CheckBox |
| `ListBox` | 单元格内 WPF ComboBox |
| `Dialog` | 弹出窗口（设计时） |
| `EditAnnulus` / `EditCircle` / `EditLine` / `EditMaskedRegion` / `EditPoint` / `EditPolygon` / `EditPolylinePath` / `EditRegion` / `EditCompositeRegion` / `EditMultiGraphics` | 在图像上拖 ROI 编辑 → 回写单元格参数 |
| `EditPassword` | 密码输入框 |

##### I. 通信与 PLC

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `ReadDevice` / `WriteDevice` | 桥接现有 `HslCommunication` Modbus TCP 客户端 |
| `ReadDiscrete` / `WriteDiscrete` | Discrete 类型 PLC 读写 |
| `TCPClient` / `TCPServer` | System.Net.Sockets |
| `FormatInputBuffer` / `FormatOutputBuffer` / `GetBufferData` / `ReadUserDataBuffer` / `WriteResultsBuffer` / `ClearResultsBuffer` / `StoreData` / `WriteData` / `ExportData` / `ImportData` | 字节数组操作 + PLC 寄存器打包 |

##### J. 控制流与状态（内置逻辑）

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `Event` / `SetEvent` | 事件对象（ManualResetEvent） |
| `Latch` / `LatchImage` / `LatchString` | 状态锁存（首次触发后保持值） |
| `ShiftRegister` | 移位寄存器（队列） |
| `Repeat` / `RepeatUntil` | 循环控制 |
| `Jump` | 跳过单元格（设置 Enabled=false） |
| `Sleep` / `Timer` / `Now` / `GetClock` / `GetTimeValue` | 系统时间 + Thread.Sleep |
| `Script` / `Scripting` | Roslyn JIT 编译 / Jint JS 引擎 |

##### K. 状态与查询

| In-Sight 函数 | 实现方式 |
|--------------|---------|
| `GetOnline` / `GetConnectionStatus` / `GetTemperature` / `GetImage` | 相机/PLC 查询 |
| `GetErrorCode` / `GetErrorString` | 错误码查表（本项目 101-399 分段） |
| `Status` / `MultiStatus` | 多工位状态汇总 |
| `ValidateIDData` | ID 字符串格式校验 |
| `VerifyString` | 字符串比对 |

#### 5.2.4 实现策略：Tool 对象池复用

VisionPro Tool 创建成本较高（涉及内部缓存、图像分配器），采用**对象池**策略：

```csharp
public class ToolPool
{
    // BlobTool 可复用（修改参数后 Run 即可）
    ConcurrentBag<CogBlobTool> _blobPool = new();
    ConcurrentBag<CogPMAlignTool> _pmAlignPool = new();
    ConcurrentBag<CogCaliperTool> _caliperPool = new();
    // ... 各类型 Tool 的池

    public CogBlobTool GetBlob();
    public void Return(CogBlobTool tool);
}
```

**性能对比**：
- 每次 new CogBlobTool() ≈ 2-5ms
- 对象池复用 ≈ 0.01ms

对 9 相机 + 10 Blob 检测的典型作业：
- ❌ 每次 new: 9 × 10 × 5ms = 450ms/轮
- ✅ 对象池: 9 × 10 × 0.01ms = 0.9ms/轮

#### 5.2.5 函数注册方式

```csharp
// functions.json → 自动批量注册
// 每个实现类用 [InsightFunction("Blob", Category="2D/通用", ...)] 特性标注
// 启动时 DI 扫描 → 自动填充 FunctionRegistry

[InsightFunction("Blob", Category="2D/通用", 
    Description="Detect blobs in an image",
    Parameters = new[] { 
        new Param("Image", typeof(Image), Required=true),
        new Param("MinArea", typeof(double), Default=2),
        new Param("MaxArea", typeof(double), Default=5000),
        new Param("Polarity", typeof(int), Default=0)
    })]
public class BlobFunction : IInsightFunction
{
    public object Invoke(object[] args, EvalContext ctx)
    {
        var image = (Image)args[0];
        double minArea = (double)args[1];
        double maxArea = (double)args[2];
        int polarity = (int)args[3];

        var tool = ToolPool.GetBlob();
        try {
            tool.InputImage = image.CogImage;
            tool.MinArea = minArea;
            tool.MaxArea = maxArea;
            tool.ThresholdPolarity = (CogBlobThresholdPolarity)polarity;
            tool.Run();
            return new Blobs(tool.ResultCollection);
        } finally {
            ToolPool.Return(tool);
        }
    }
}
```

### 5.3 Shape 与数据结构

```csharp
// 与 VisionPro 数据结构的映射桥接
public abstract class ShapeBase { public double X, Y, Angle; }

public class Circle    : ShapeBase { public double Radius; }
public class Annulus   : ShapeBase { public double InnerRadius, OuterRadius; }
public class Rectangle : ShapeBase { public double Width, Height, Curve; }
public class Point     { public double X, Y; }
public class Line      { public double X0, Y0, X1, Y1; }

public class Blobs
{
    // 桥接 VisionPro CogBlobResultCollection
    public int Count { get; }
    public double GetX(int i);
    public double GetY(int i);
    public double GetScore(int i);
    public double GetArea(int i);
    public Blob this[int i] { get; }
}

public class Image
{
    // 包装 Cognex.VisionPro.ICogImage
    public ICogImage CogImage { get; }
    public int Width { get; }
    public int Height { get; }
}
```

### 5.4 Job 文件格式（`.jobx`）

采用 **JSON + ZIP** 容器（与 In-Sight 26.1.0 兼容）：

```
MyRecipe.jobx（ZIP）
└── Job.json
    {
      "JobVersion": "22.2",
      "Metadata": {
        "CameraType": "JustSmart 9Cam",
        "JobType": "Spreadsheet",
        "ProductType": "安全带_型号A"
      },
      "AcqSettings": { "exposure": 20, "gain": 1.0, ... },
      "JobSettings": { ... },
      "Sheets": {
        "Inspection": {
          "cells": [
            ["A0", "AcquireImage()", 1, {"$type": "Image", ...}, null, null],
            ["B1", "BlobDetect($A$0, 128, 0, 2)", 1, null, null, null],
            ["C1", "Count(B1)", 1, 23.0, ...]
          ],
          "columnWidths": [...],
          "rowHeights": [...],
          "timeout": 60000
        },
        "Results": { ... }
      }
    }
```

#### 5.4.1 Cell JSON 结构

```json
{
  "type": "Sheet",
  "cells": [
    // [cellName, expression, enabled, result, graphic, label, ...]
    ["A0", "AcquireImage()", 1, {"$type": "Image", "base64": "..."}, null, null],
    ["B1", "BlobDetect($A$0, 128, 0, 2)", 1, null, null, null],
    ["B1$X", "GetX(B1,0)", 1, 125.3, null, "质心X"],
    ["B1$Y", "GetY(B1,0)", 1, 200.7, null, "质心Y"],
    ["C1", "IF(Count(B1)>0, 1, 0)", 1, 1.0, null, "判定"],
    ["Results", "IF(C1=1,\"OK\",\"NG\")", 1, "OK", null, "结果"]
  ]
}
```

每个 cell 数组元素：
| 索引 | 含义 |
|------|------|
| 0 | 单元格名（A1、B2...） |
| 1 | 表达式 |
| 2 | 是否启用（1=启用，0=禁用） |
| 3 | 计算结果（结构体 JSON 或数值） |
| 4 | 图形对象（可空） |
| 5 | 显示标签（可空） |

#### 5.4.2 与 PLC 协议的映射约定

在 Sheet 的 **Results Sheet** 或 Inspection Sheet 的**特定命名单元格**与 PLC 寄存器映射：

```json
"JobSettings": {
  "PlcMappings": {
    "Register2000": { "type": "Input",  "cell": "Trig" },
    "Register2001": { "type": "Output", "cell": "AcquireDone" },
    "Register2002": { "type": "Output", "cell": "CycleDone" },
    "Register2003": { "type": "Output", "cell": "ResultCode" },
    "Register2004": { "type": "Output", "cell": "ErrorCode" }
  }
}
```

或者约定**固定命名单元格**（更简单）：

| 单元格 | 含义 |
|--------|------|
| `Output_OK` | 判定结果（1=OK, 2=NG, ≥101=异常）→ PLC 2003 |
| `Output_ErrorCode` | 异常错误码 → PLC 2004 |
| `Output_BLXM` | 不良描述 → 日志/MQTT |

### 5.5 Cell 引用解析

```
A1       → 相对引用（复制公式时行列会变）
$A$1     → 绝对引用（行列都锁定）
$A1      → 混合引用（列锁定，行相对）
A$1      → 混合引用（列相对，行锁定）
A1:B5    → 范围引用（展开为数组参数）
Sheet1!A1 → 跨 Sheet 引用
```

```csharp
public static class CellRefParser
{
    // 解析 "$A$1" → { Col:'A', Row:1, ColFixed:true, RowFixed:true }
    public static CellRef Parse(string refStr);
    
    // 计算相对引用的实际单元格（根据当前单元格偏移）
    public static CellRef Resolve(CellRef relativeRef, string currentCell);
}
```

### 5.6 增量求值调度

```
依赖图：B1 依赖 A0（BlobDetect 输入是 AcquireImage 输出）
       B1$X 依赖 B1（GetX 依赖 BlobDetect 结果）
       B1$Y 依赖 B1
       C1  依赖 B1$X 和 B1$Y

当 A0 更新（新图像）时：
  反向遍历 → B1、C1、B1$X、B1$Y 都需要重算
  拓扑排序 → [B1] → [B1$X, B1$Y] → [C1]
  按序求值，每个只算一次
```

### 5.7 错误处理

| 阶段 | 错误 | 处理 |
|------|------|------|
| 词法分析 | 未识别 Token | 单元格显示 `#SYNTAX` |
| 语法分析 | 括号不匹配、参数缺失 | 单元格显示 `#SYNTAX` |
| 符号解析 | 引用不存在的单元格/Tag | 单元格显示 `#REF` |
| 函数调用 | 函数不存在、参数类型不匹配 | 单元格显示 `#NAME` / `#VALUE` |
| 运行时 | VisionPro 工具异常、空引用 | 单元格显示 `#ERR`，记录错误栈 |

### 5.8 Script 函数（VPP C# Script 平替方案）

> 🎯 **目标**：用 In-Sight Script（JavaScript）**完全替代** VisionPro ToolBlock 里的 C# Script（VisionBlockAdvancedScript）。
> 用户写一份 JS 脚本，在**智能相机**和**桌面软件**（本项目）两边都能跑。
> 参考 [COGNEX_INSIGHT_PROGRAMMING.md](file:///d:/JustSmart/261010优易达安全带/CodeBase/SeatBeltInspection_2611161017/Reference/CognexJobxLanguageServerProtocol/COGNEX_INSIGHT_PROGRAMMING.md) 完整编程指南。

#### 5.8.1 为什么用 In-Sight Script 而不是 Roslyn C#

| 维度 | In-Sight Script (JS + Jint) | Roslyn (C#) | VisionPro C# Script (现状) |
|------|----------------------------|-------------|---------------------------|
| **两边都能跑** | ✅ 智能相机 + 桌面软件 | ❌ 只有桌面 | ❌ 只有桌面 |
| **ECMA 版本** | ES5（In-Sight 26.1.0 原生） | C# 7.3 | C# 7.3 |
| **跨平台** | ✅ JS 无处不在 | .NET 限定 | 仅限 VisionPro 宿主 |
| **内存限制** | 4 MB（原生约束） | 无显式限制 | 无显式限制 |
| **学习成本** | 低（JS 生态广泛） | 中 | 高（VisionPro API 耦合） |
| **调试** | 困难（Jint 无源映射） | ✅ VS 附加调试 | ✅ VisionPro 设计器内调试 |

**决策**：**Script 函数用 Jint 引擎跑 In-Sight 原生 JS**。Roslyn 作为未来可选增强（纯 C# 计算场景），但不作为 Script 的主要实现。

#### 5.8.2 In-Sight Script 生命周期

每个 Script 单元格是**一个独立的 JS 模块**，基于 CommonJS/NodeJS 风格：

```js
"use strict";

// ===== 构造函数（初始化内部状态）=====
function Script() {
    this.total = 0;        // 跨 run 调用持久化
    this.lines = [];
    this.enabled = true;
}
module.exports = Script;

// ===== 必需方法 =====
Script.prototype.run = function(arg0, arg1, arg2) {
    // 每次电子表格更新时调用
    // arg0..argN = Script 函数的输入参数（Blobs, Image, Patterns, number, string...）
    // 返回值可以是：number, string, bool, Image, Blobs, Patterns, Shape, Object, Array
    var blobs = arg0;          // Blob 数据结构
    var image = arg1;          // Image
    var threshold = arg2;      // number
    
    if (blobs.getNFound() > threshold) {
        return { ok: true, count: blobs.getNFound() };
    }
    return { ok: false, count: 0 };
};

// ===== 可选方法 =====
Script.prototype.draw = function(gr) {
    // run 之后调用（不是每次 run 都会触发），gr = 图形上下文
    gr.plotCircle(this.x, this.y, 30, "Found", 0x00FF00);   // 绿色圆
    gr.plotString("OK", this.x + 40, this.y, 0x00FF00);
};

Script.prototype.save = function() {
    // 保存 job 时调用 → 序列化内部状态
    return { total: this.total, enabled: this.enabled };
};

Script.prototype.load = function(saved) {
    // 加载 job 时调用 → 恢复内部状态
    this.total = saved.total;
    this.enabled = saved.enabled;
};
```

**内存限制**（与 In-Sight 26.1.0 对齐）：

| 项目 | 限制 |
|------|------|
| 脚本内存总量 | **4 MB** |
| 栈内存 | **64 kB** |
| 超时保护 | 5 秒（可配置）→ 超时自动 `#ERR` |

#### 5.8.3 Jint 引擎实现

```csharp
// 单例脚本引擎工厂（每个 Script 单元格独立 ScriptInstance）
public class ScriptEngineFactory
{
    // 为每个 Script 单元格创建隔离的 Jint 实例
    public ScriptInstance Create(string scriptSource)
    {
        var engine = new Jint.Engine(cfg => cfg
            .AllowClr(true)                          // 允许访问 Jint 提供的 CLR 对象
            .LimitMemory(4 * 1024 * 1024)            // 4 MB
            .LimitStackSize(64 * 1024)               // 64 kB
            .TimeoutInterval(TimeSpan.FromSeconds(5)) // 超时保护
        );
        
        // 注入 Blobs/Patterns/Edges/Image/Shape 数据结构的 JS 包装
        engine.SetValue("InSightAPI", new InsightJsApi());
        
        // 加载脚本
        var result = engine.Execute(scriptSource);
        
        // 构造 Script 对象实例
        var scriptCtor = engine.GetValue("Script");
        var scriptInstance = scriptCtor.Invoke();
        
        return new ScriptInstance(engine, scriptInstance);
    }
}

public class ScriptInstance
{
    private readonly Jint.Engine _engine;
    private readonly Jint.Native.JsObject _instance;

    public object Run(params object[] args)
    {
        var run = _instance.Get("run");
        try {
            var result = run.Invoke(ToJsArgs(args));
            return FromJsValue(result);  // JsValue → C# 对象（number, string, Blobs, Patterns...）
        } catch (Exception ex) {
            throw new ScriptRuntimeException("Script run 异常", ex);
        }
    }

    public void Draw(IGraphicsContext gr)
    {
        var draw = _instance.Get("draw");
        if (draw != null && draw.Type == Jint.Native.JsValueType.Function)
            draw.Invoke(gr);
    }
    
    public object Save() { /* ... */ }
    public void Load(object saved) { /* ... */ }
}
```

#### 5.8.4 Blob/Patterns/Edges 数据结构的 JS 包装

关键：**JS 侧调用 `blobs.getNFound()` 时实际调用 C# wrapper 方法**：

```csharp
// 让 Jint 可调用的 Blob 包装
public class BlobJsProxy
{
    private readonly Blobs _inner;  // 本项目 Blobs 结构（封装 VisionPro CogBlobResultCollection）
    
    public int getNFound() => _inner.Count;
    public double getX(int i) => _inner.GetX(i);
    public double getY(int i) => _inner.GetY(i);
    public double getArea(int i) => _inner.GetArea(i);
    public double getPerimeter(int i) => _inner.GetPerimeter(i);
    public double getScore(int i) => _inner.GetScore(i);
    public double getWidth(int i) => _inner.GetWidth(i);
    public double getHeight(int i) => _inner.GetHeight(i);
    public double getAngle(int i) => _inner.GetAngle(i);
    public double getCenterOfMassX(int i) => _inner.GetX(i);
    public double getCenterOfMassY(int i) => _inner.GetY(i);
    public double getMinX(int i) => _inner.MinX(i);
    public double getMaxX(int i) => _inner.MaxX(i);
    public double getMinY(int i) => _inner.MinY(i);
    public double getMaxY(int i) => _inner.MaxY(i);
    public double getThresh() => _inner.Threshold;
    public int getHoles(int i) => _inner.Holes(i);
    // ... 完整映射 COGNEX_INSIGHT_PROGRAMMING.md 6.2 节
}
```

同样为 Patterns、Edges、Histogram、Image 提供 JS Proxy。

#### 5.8.5 VPP C# Script → In-Sight Script (JS) 平替对照

**场景 1：判定逻辑（最常见，高覆盖率自动翻译）**

VisionPro 原 C# Script（ToolBlock 内 VisionBlockAdvancedScript）：
```csharp
// 安全带前视检测判定
public override bool Run(ICogRecord[] inputs, out ICogRecord[] outputs)
{
    var blobs = inputs[0] as CogBlobResultCollection;
    var pattern = inputs[1] as CogPMAlignResultCollection;
    var edges = inputs[2] as CogCaliperResultCollection;

    int panDuan = 1;
    string blxm = "";

    if (blobs.Count < 3) { panDuan = 101; blxm = "前方卡扣缺失"; }
    else if (pattern.Count == 0) { panDuan = 102; blxm = "锚点定位失败"; }
    else if (pattern[0].Score < 0.7) { panDuan = 2; blxm = "锚点Score低"; }
    else if (edges.Count < 2) { panDuan = 3; blxm = "边缘不足"; }

    outputs[0] = new CogToolBlockTerminal("PanDuan", panDuan);
    outputs[1] = new CogToolBlockTerminal("BLXM", blxm);
    outputs[2] = new CogToolBlockTerminal("AnchorX", pattern.Count > 0 ? pattern[0].GetPose().TranslationX : 0);
    outputs[3] = new CogToolBlockTerminal("AnchorY", pattern.Count > 0 ? pattern[0].GetPose().TranslationY : 0);
    return true;
}
```

**平替为 In-Sight Script (JS)**：
```js
// Script(BlobResult, PatternResult, EdgeResult) → Object { panDuan, blxm, anchorX, anchorY }
"use strict";
function Script() {
    this.minBlobCount = 3;
    this.patternThresh = 0.7;
    this.minEdgeCount = 2;
}
module.exports = Script;

Script.prototype.run = function(blobs, pattern, edges) {
    var panDuan = 1;
    var blxm = "";
    
    if (blobs.getNFound() < this.minBlobCount) { panDuan = 101; blxm = "前方卡扣缺失"; }
    else if (pattern.getNFound() === 0)     { panDuan = 102; blxm = "锚点定位失败"; }
    else if (pattern.getScore(0) < 0.7)     { panDuan = 2;   blxm = "锚点Score低"; }
    else if (edges.getNFound() < this.minEdgeCount) { panDuan = 3; blxm = "边缘不足"; }
    
    return {
        panDuan: panDuan,
        blxm: blxm,
        anchorX: pattern.getNFound() > 0 ? pattern.getX(0) : 0,
        anchorY: pattern.getNFound() > 0 ? pattern.getY(0) : 0
    };
};

Script.prototype.draw = function(gr) {
    var p = ScriptInstance_Input2_p;  // 由宿主注入 pattern 引用
    if (p && p.getNFound() > 0) {
        var color = this._panDuan === 1 ? 0x00FF00 : 0xFF0000;
        gr.plotCircle(p.getX(0), p.getY(0), 30, "Anchor", color);
        gr.plotString("Score: " + p.getScore(0).toFixed(2), p.getX(0) + 40, p.getY(0), color);
    }
};
```

**调用方**（Sheet 中）：
```
A1 = Blob($A$0, 10, 5000, 0)                              // 卡扣 Blob
A2 = FindPatMaxRedLine($A$0, "anchor.pat", 0.7, Region(...)) // 锚点 Pattern
A3 = Caliper($A$0, ...)                                    // 边缘检测
A4 = Script($A$1, $A$2, $A$3)                              // JS 判定脚本
A5 = A4.panDuan                                            // Object 解构 → 数值
A6 = A4.blxm                                               // 不良描述字符串
Output_OK = $A$5                                           // 映射 PLC 2003
Output_BLXM = $A$6                                         // 映射日志/MQTT
```

**场景 2：状态锁存 / 计数器**

VisionPro C# Script 里常用静态变量做计数器：
```csharp
private static int _okCount = 0;   // C# static 变量
if (panDuan == 1) _okCount++;
```

In-Sight Script 平替（用 `this` 属性 + `save/load`）：
```js
function Script() { this.okCount = 0; this.totalCount = 0; }
module.exports = Script;
Script.prototype.run = function(panDuan) {
    this.totalCount++;
    if (panDuan === 1) this.okCount++;
    return { okRate: this.okCount / this.totalCount, ok: this.okCount, total: this.totalCount };
};
Script.prototype.save = function() { 
    return { okCount: this.okCount, totalCount: this.totalCount }; 
};
Script.prototype.load = function(saved) { 
    this.okCount = saved.okCount; this.totalCount = saved.totalCount; 
};
```

**场景 3：VisionPro 特定 API 依赖（无法直接平替）**

若原 C# Script 调用了 VisionPro 特定 API（如 `CogSerialize`、`CogTransform2D` 操作）：
```csharp
// VisionPro 特定
var tfm = new CogTransform2DLinear();
tfm.SetTranslation(...);
var fixture = tfm.MapPoint(...);
```

**平替方案**：用 In-Sight Script 调用 VisionPro 桥接层（宿主注入）：
```js
Script.prototype.run = function(image) {
    var tfm = new InSightAPI.Transform2D();  // 宿主 JS Proxy → 内部调 CogTransform2DLinear
    tfm.setTranslation(100, 200);
    var pt = tfm.mapPoint(50, 50);
    return { x: pt.x, y: pt.y };
};
```

**映射表**：

| VisionPro API | In-Sight Script 平替 | 覆盖度 |
|--------------|---------------------|--------|
| `CogBlobResultCollection` + `.Count`, `.CentroidX`, `.Area` | `blobs.getNFound()`, `blobs.getX(i)`, `blobs.getArea(i)` | ✅ 100% |
| `CogPMAlignResultCollection` + `.Score`, `.GetPose()` | `patterns.getScore(i)`, `patterns.getX(i)`, `patterns.getAngle(i)` | ✅ 95% |
| `CogCaliperResultCollection` + `.Count`, `.GetEdge()` | `edges.getNFound()`, `edges.getX(i, endpoint)` | ✅ 90% |
| `if (...) { PanDuan = 1; }` | `if (...) { panDuan = 1; }` | ✅ 100% 语法同构 |
| `static int _counter` | `this.counter` + `save/load` | ✅ 行为一致 |
| `CogTransform2DLinear.MapPoint()` | `InSightAPI.Transform2D.mapPoint()` | ✅ 通过宿主 Proxy |
| `CogSerialize.LoadObjectFromFile()` | `InSightAPI.loadVproj(path)` 或 RunToolBlock | ⚠ 建议 RunToolBlock |
| `Cognex.VisionPro.ToolBlock.CogToolBlockTerminal` | JS 返回 Object → 宿主自动映射 | ✅ 自动处理 |

#### 5.8.6 自动翻译工具（RecipeConverter 子模块）

RecipeConverter 可自动将 VisionPro C# Script（VisionBlockAdvancedScript）翻译为 In-Sight JS 模板：

```csharp
// C# Script AST → JS AST 转换器
public class ScriptTranslator
{
    public string Translate(string csharpSource)
    {
        // Roslyn CSharpSyntaxTree → ANTLR / hand-written JS emitter
        // 覆盖：if/else, 算术, 比较, blobs.Count, pattern[0].Score → 对应 JS
    }
}
```

**自动翻译覆盖度**：

| C# 模式 | JS 平替 | 覆盖度 |
|---------|---------|--------|
| `if (...) {} else {}` | `if (...) {} else {}` | ✅ 99% |
| `inputs[i] as CogBlobResultCollection` | `Script` 的第 i 个参数（同名） | ✅ 100% |
| `blobs.Count` | `blobs.getNFound()` | ✅ 100% |
| `pattern[0].Score` | `pattern.getScore(0)` | ✅ 100% |
| `pattern[0].GetPose().TranslationX` | `pattern.getX(0)` | ✅ 90% |
| `outputs[n] = new CogToolBlockTerminal("Name", val)` | `return { Name: val, ... }` | ✅ 100% |
| `static int _counter` | `this.counter` + 生成 save/load 模板 | ✅ 80% |
| `new CogBlobTool() { ... }.Run()` | ⚠ 标记为 RunToolBlock 调用，不自动翻译 | 0% |

#### 5.8.7 Jint vs Roslyn 最终决策

| 维度 | Jint (JS) | Roslyn (C#) |
|------|-----------|-------------|
| **两边都能跑** | ✅ **智能相机 + 桌面** | ❌ 只有桌面 |
| **ECMA 版本** | ES5（In-Sight 26.1.0 原生） | C# 7.3 |
| **性能** | 中等（~100 万次简单运算/秒） | 高（编译后接近原生） |
| **内存** | 4 MB 硬限制 | 无显式限制 |
| **VisionPro 桥接** | 宿主注入 JS Proxy 包装 | 直接引用 |
| **调试** | 困难（Jint 无 source map） | ✅ VS 附加调试 |

**决策**：

- **Script 函数** → Jint 引擎跑 **In-Sight ES5 Script**（与智能相机代码通用）
- **ToolPool / VisionPro 桥接层** → 继续用项目原生 C# 代码（性能关键路径）
- **RecipeConverter 自动翻译** → Roslyn 解析 C# Script AST，输出 JS 模板
- **未来可能**：Roslyn 做纯 C# 计算 Script（如复杂数学），与 Jint 并存

### 5.9 LSP 服务（可选增强）

参考 `CognexJobxLanguageServerProtocol`（Java LSP 实现），为 VS Code / Rider 提供 `.jobx` 文件的语言支持：

```
VS Code .jobx 文件
    │
    ▼
Language Server（本项目可移植：C# LSP 库如 OmniSharp）
    ├── 诊断（语法/类型/引用错误下划线）
    ├── 补全（函数名、参数、单元格引用）
    ├── 悬停提示（函数签名、参数说明）
    └── Go-to-Definition（跳转到被引用的单元格）
```

函数元数据来源：`functions.json`（从 In-Sight 26.1.0 帮助文档提取的 350+ 函数签名）

### 5.10 坐标系约定（与智能相机一致）

> **核心原则**：所有视觉函数默认使用**像素坐标系**，与康耐视智能相机（In-Sight）保持完全一致，降低从智能相机迁移到桌面软件的认知负担。

#### 5.10.1 坐标系定义

```
(0, 0)─────────────────── (W-1, 0)
  │                          │
  │      图像坐标系            │  ← X 向右增加
  │      (像素单位)            │  ← Y 向下增加
  │                          │
(0, H-1)────────────────── (W-1, H-1)
```

| 属性 | 约定值 | 说明 |
|------|--------|------|
| 原点 (0, 0) | 图像**左上角** | 与相机 sensor 物理像素对齐 |
| X 轴方向 | 向右为正 | |
| Y 轴方向 | **向下为正** | 注意：VisionPro 部分工具默认 Y 轴向上，需做内部转换 |
| 单位 | **像素 (px)** | 默认不引入 mm 物理单位（如需标定显式调用 Calibrate 函数） |
| 行号起始 | **0** | 电子表格行号从 0 开始（In-Sight 风格，区别于 Excel 从 1 开始） |

#### 5.10.2 与 VisionPro 的坐标桥接

VisionPro 内部使用 `CogPoint2D`（X 右为正，**Y 向下为正**——经核实 VisionPro 默认也是 Y 向下），因此**不需要额外转换**：

```csharp
// VisionPro CogBlobResult.Centroid → 直接使用，无需转换
// 两者坐标系天然一致（原点左上角，像素单位，Y 向下）
var insightX = blob.CentroidX;    // 直接等于 VisionPro blob.Centroid.X
var insightY = blob.CentroidY;    // 直接等于 VisionPro blob.Centroid.Y
```

⚠️ **注意事项**：若 VisionPro 某些工具配置了 `CogRectangleAffine` 的旋转或 affine 变换，返回的 `CogTransform2DLinear` 结果需要转换回图像坐标系。桥接层统一处理，对上层电子表格函数透明。

#### 5.10.3 物理单位扩展（可选）

当用户调用 `Calibrate()` 函数后，Sheet 进入**已标定模式**：

```
未标定模式（默认）：
  BlobDetect() → 返回像素坐标 (px)
  Distance()   → 返回像素距离 (px)

已标定模式：
  Calibrate(points, realDistances) → 保存 Transform2D
  BlobDetect() → 返回物理坐标 (mm)  ← 自动转换
  Distance()   → 返回物理距离 (mm)
```

**函数内部实现**：Shape/Blob/Pattern 等结构体在构造时记录当前 Sheet 标定状态，物理坐标计算延迟到属性访问时执行。

---

## 6. UI 设计（`JustSmart.Insight.UI`）

### 6.1 对标康耐视 In-Sight 编辑器

本界面设计**完全对齐康耐视 In-Sight 26.1.0 电子表格编辑器**的布局和交互，参考 [编辑器界面.png](file:///d:/JustSmart/261010优易达安全带/CodeBase/SeatBeltInspection_2611161017/Reference/编辑器界面.png) 截图。

对标原则：
- **像素级布局一致**：菜单、工具栏、公式栏、网格、函数面板、记录回放面板的位置和尺寸比例
- **交互行为一致**：函数双击/拖入单元格、ROI 在图像上拖拽编辑、结果表点击跳转源单元格
- **术语一致**：沿用康耐视中文界面的术语（如"采集"、"EasyView"、"Job Validation"）

### 6.2 界面完整布局（对齐 In-Sight 26.1.0）

```
┌─────────────────────────────────────────────────────────────────────────────────────────┐
│  标题栏：JustSmart Vision Suite  [未保存作业]@thinkbook14 ×              admin    ─ □ × │
├─────────────────────────────────────────────────────────────────────────────────────────┤
│  菜单栏：  文件(F)  编辑(E)  视图(V)  帮助(H)                                            │
├─────────────────────────────────────────────────────────────────────────────────────────┤
│  工具栏 1： 新建│打开│保存│打印  ↶撤销 ↷重做  相机▼:Arial  联机  缩放▼:100%  [?]  图标集 │
│  工具栏 2： 对齐[A][L][C][R] 字体[Arial▼] [9▼] [B][I][U]  视觉工具快速图标 ─────────── │
│            □ ┯ ○ ╱ ↘ T 📏 🔳 ⚙ 🔲 △ ⬡ ╪ ╦ ╩  ← ROI/测量/图形快速入口                    │
├─────────────────────────────────────────────────────────────────────────────────────────┤
│  公式栏：  [ A0 ▼ ]  AcquireImage()                                          [ fx ]    │
│  (可折叠)  单元格名  │  公式编辑区 (AvalonEdit 语法高亮)                      插入函数按钮   │
├──────────────────────┬────────────────────────────────────────────────────────────────┤
│                      │  ┌──────────────────────────────────────────────────────────┐ │
│   电子表格网格        │  │  函数                                    [-]    [×]      │ │
│   (Xceed DataGrid)   │  │  ┌─────────────────────────────────────────────────────┐ │ │
│                      │  │  │ Filter: [__________________________________         │ │ │
│     │ A │ B │ C │    │  │  │                                                     │ │ │
│  ───┼───┼───┼───┼── │  │  │  ▼ Advanced AI                                       │ │ │
│   0 │▼ │   │   │    │  │  │  ▼ 视觉工具   🔍                                      │ │ │
│   1 │   │   │   │    │  │  │    BlobDetect                                       │ │ │
│   2 │   │   │   │    │  │  │    FindPattern                                      │ │ │
│   3 │   │   │   │    │  │  │    ReadID                                           │ │ │
│   4 │   │   │   │    │  │  │    OCRMax                                           │ │ │
│  ...│...│...│...│... │  │  │    Caliper                                          │ │ │
│  27 │   │   │   │    │  │  │  ▼ 几何   📐                                        │ │ │
│                      │  │  │    Distance    Angle    LineToLine                   │ │ │
│                      │  │  │  ▼ 图形   🖌                                        │ │ │
│   选中单元格显示       │  │  │    Annulus    Circle    Rectangle    Line           │ │ │
│   类型图标 + 表达式    │  │  │  ▼ 数学   ∑                                        │ │ │
│   + 计算结果          │  │  │    IF   AND   OR   NOT   +   -   *   /              │ │ │
│                      │  │  │  ▼ Robotics String Functions                        │ │ │
│   ─────────────────  │  │  │  ▼ 文本                                             │ │ │
│   图像预览面板         │  │  │  ▼ 坐标变换                                        │ │ │
│   (选中 Image 单元格  │  │  │  ▼ 输入/输出   ┌────────────────────────────┐       │ │ │
│    或 Shape 单元格    │  │  │  │    ReadDevice    WriteDevice    Button    │       │ │ │
│    时显示带 ROI 叠加) │  │  │  ▼ 定时数据存储  │    EditInt    EditFloat    │       │ │ │
│                      │  │  │  ▼ 视觉数据访问  │    EditString  Indicator   │       │ │ │
│                      │  │  │    GetX   GetY   │                            │       │ │ │
│                      │  │  │    GetScore GetNFound └────────────────────┘       │ │ │
│                      │  │  │  ▼ 白平衡                                          │ │ │
│                      │  │  │  ▼ 结构                                            │ │ │
│                      │  │  │  ▼ 程序编写   Script                                │ │ │
│                      │  │  │  ▼ Arrays and Objects                               │ │ │
│                      │  │  │                                                     │ │ │
│                      │  │  │  ──── 所有函数 ────                                  │ │ │
│                      │  │  │                                                     │ │ │
│                      │  │  └─────────────────────────────────────────────────────┘ │ │
│                      │  └──────────────────────────────────────────────────────────┘ │
├──────────────────────┴────────────────────────────────────────────────────────────────┤
│  记录/回放  [▼  202611] [▼] [×]                                                       │
│  ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐                                        │
│  │ 📷   │ │ 📷   │ │ 📷   │ │ 📷   │ │ 📷   │   ← 历史采集缩略图                      │
│  │202611│ │202611│ │202611│ │202611│ │202611│                                        │
│  └──────┘ └──────┘ └──────┘ └──────┘ └──────┘                                        │
│                                                                                      │
│  ⚙ 📁 [图像 ▼] [5 ▼] [Bmf ▼] ●  延迟(ms): [ 200 ]  [▶] [⏵] [⏪] [▶] [⏮]            │
├──────────────────────────────────────────────────────────────────────────────────────┤
│  函数 | 采集 | EasyView | Job Validation    ← 底部标签页（AvalonDock 可切换）          │
├──────────────────────────────────────────────────────────────────────────────────────┤
│  状态栏：合计: 0.0 毫秒                                    [联机 ▼]  [脱机]            │
└──────────────────────────────────────────────────────────────────────────────────────┘
```

### 6.3 控件选型

| 组件 | 选型 | 理由 |
|------|------|------|
| 电子表格主控件 | **Xceed WPF DataGrid** | In-Sight 逆向确认使用；支持冻结列、合并单元格、内联编辑、自定义单元格渲染 |
| 公式栏编辑器 | **ICSharpCode.AvalonEdit** | In-Sight 安装目录自带此 DLL；支持语法高亮、括号匹配、自动补全 |
| 菜单栏/工具栏 | Prism + HandyControl | 项目已有 Prism；工具栏图标参考 In-Sight 视觉工具图标集 |
| 函数分类面板 | TreeView + Expander | 模拟 In-Sight 函数面板 ▼ 展开交互 |
| 记录/回放面板 | ListBox + Image 缩略图 | 历史采集浏览 |
| 停靠布局 | **AvalonDock** | In-Sight 安装目录自带 Xceed.Wpf.AvalonDock.dll；底部标签页、浮动面板 |
| 图像预览控件 | **CognexDisplayControl**（项目已有） | VisionPro 图像显示 + ROI 叠加 |
| 属性面板 | HandyControl PropertyGrid / 自定义 | 参数编辑弹窗（选中函数时自动弹出） |
| 单元格类型 | 自定义 `InsightCell` 继承 `DataGridCell` | 承载 Image/Blobs/Patterns/Shape/控件等特殊值的显示 |

### 6.4 工具栏详细设计

#### 工具栏 1：文件/操作/运行控制

| 按钮 | 图标参考 | 功能 |
|------|----------|------|
| 新建作业 | 📄 | 新建空白 Sheet（A0=AcquireImage()） |
| 打开作业 | 📂 | 打开 .jobx 文件 |
| 保存作业 | 💾 | 保存 .jobx |
| 撤销 | ↶ | 依赖图历史回退 |
| 重做 | ↷ | |
| 相机选择 | 下拉框 | "Arial" → 切换输入相机（模拟 In-Sight 连接的相机名） |
| 联机 | 🖱 | 切换联机/脱机模式（脱机时用本地图像文件） |
| 缩放 | 下拉框 | 100%、75%、150%、200% |
| 运行 | ▶ | 全表重新求值 |
| 单步执行 | ⏭ | 单步求值当前单元格及其下游 |

#### 工具栏 2：视觉工具快速入口（ROI/测量图形）

| 图标 | 功能 | 对应函数 | 说明 |
|------|------|----------|------|
| □ | 矩形 | `Rectangle()` | 点击后在图像上拖拽创建 ROI |
| ┯ | 圆矩形 | `Rectangle(curve=N)` | 带圆角 |
| ○ | 圆形 | `Circle()` | |
| ╱ | 直线 | `Line()` | |
| ↘ | 箭头 | `Line(arrowEnd=true)` | 测量箭头 |
| T | 文本 | - | 在图像上叠加文字标签 |
| 📏 | 距离 | `Distance()` | 测量工具 |
| 🔲 | 圆角矩形 | | |
| 🔳 | 环形 | `Annulus()` | |
| ⬡ | 六边形 | `Polygon()` | |

**交互**：点击工具栏图标 → 在图像预览区按住左键拖拽 → 自动在当前活动单元格插入对应 Shape 函数调用。

### 6.5 函数分类面板（右侧）

完全对齐 In-Sight 26.1.0 中文界面的函数分类树：

```
┌─ 函数 ─────────────────────────────────────────────┐
│ Filter: [__________________________________         │
│                                                     │
│ ▼ Advanced AI                                       │
│    深度学习分类/检测相关函数                           │
│                                                     │
│ ▼ 视觉工具                                          │
│    BlobDetect / FindPattern / ReadID / OCRMax       │
│    Caliper / FindLine / FindCircle / Count          │
│                                                     │
│ ▼ 几何                                              │
│    Distance / Angle / LineToLine / PointToLine      │
│    BlobToBlob / PatternToPattern                    │
│                                                     │
│ ▼ 图形                                              │
│    Annulus / Circle / Rectangle / Line / Point      │
│    Polygon / TextOverlay                            │
│                                                     │
│ ▼ 数学                                              │
│    IF / AND / OR / NOT + - * / %                    │
│    Sqrt / Abs / Min / Max / Round / Ceil / Floor    │
│    Cos / Sin / Tan / Atan2                          │
│                                                     │
│ ▼ Robotics String Functions                         │
│                                                     │
│ ▼ 文本                                              │
│                                                     │
│ ▼ 坐标变换                                          │
│                                                     │
│ ▼ 输入/输出                                         │
│    Button / EditInt / EditFloat / EditString        │
│    Indicator / ReadDevice / WriteDevice             │
│                                                     │
│ ▼ 定时数据存储                                      │
│                                                     │
│ ▼ 视觉数据访问                                      │
│    GetX / GetY / GetScore / GetNFound               │
│    GetArea / GetPerimeter / GetWidth / GetHeight    │
│                                                     │
│ ▼ 白平衡                                            │
│                                                     │
│ ▼ 结构                                              │
│                                                     │
│ ▼ 程序编写                                          │
│    Script(...)                                      │
│    RunToolBlock(name, Image, ...)                    │
│                                                     │
│ ▼ Arrays and Objects                                │
│                                                     │
│ ──── 所有函数 ────                                  │
│    列出全部已注册函数 + Quick Search                  │
└─────────────────────────────────────────────────────┘
```

**交互**：
- 函数名上**双击** → 自动插入到当前光标所在单元格
- **拖拽**函数到单元格 → 插入并弹出参数配置对话框
- 鼠标**悬停**函数 → Tooltip 显示函数签名、参数说明、返回值类型
- `Filter` 搜索框 → 实时过滤函数列表（支持中文名、英文名、函数全名）

### 6.6 单元格显示类型

| 单元格值类型 | 显示方式 | 选中时行为 |
|--------------|----------|-----------|
| `double` | 数值，可设置小数位 | 下方图像面板不变 |
| `string` | 文本 | |
| `bool` | True/False 或 OK/NG | |
| `Image` | **缩略图**（A0 典型场景） | 下方图像面板显示完整图像 |
| `Blobs` | `Blob x N` + 计数 | 下方图像面板显示 ROI 叠加 + 结果表（X/Y/Area/Score） |
| `Patterns` | `Pattern x N` + 计数 | 下方图像面板显示匹配框叠加 |
| `Edges` | `Edge x N` + 计数 | 下方图像面板显示边线叠加 |
| `Shape` | 图形图标 | 下方图像面板显示 ROI，可**拖拽编辑**调整位置/大小/角度 |
| `Script` 函数 | `Script(...)` 文本 | 显示 run 输出 + draw 图形叠加 |
| HMI 控件 | 直接渲染控件（见 6.7） | |
| `#ERR` | 红色背景 + ❌ 图标 | Tooltip 显示完整错误信息 + 堆栈 |

### 6.7 HMI 控件单元格

完全对齐 In-Sight 的单元格内嵌控件：

```
┌──────────┬──────────────────────────────────────────────┐
│ Button   │ [▶ Start]  ← 按钮（单元格内嵌 WPF Button）   │
├──────────┼──────────────────────────────────────────────┤
│ EditInt  │ [====128====] ← 滑块 + 数值输入               │
├──────────┼──────────────────────────────────────────────┤
│ EditFloat│ [====3.14====]                               │
├──────────┼──────────────────────────────────────────────┤
│EditString│ [  Hello  ] ← 文本框                          │
├──────────┼──────────────────────────────────────────────┤
│Indicator │ ● OK ← 彩色圆点（OK绿 / NG红 / Error橙）       │
└──────────┴──────────────────────────────────────────────┘
```

**运行时 vs 设计时**：
- **设计时**（在编辑器中）：所有 HMI 控件可编辑、可点击测试
- **联机自动运行时**：HMI 单元格变为只读/禁用，由外部信号驱动

### 6.8 底部标签页

| 标签 | 内容 |
|------|------|
| **函数** | 默认显示当前 Sheet 的函数列表（所有包含函数调用的单元格） |
| **采集** | 相机设置（曝光、增益、触发模式）+ 实时帧率显示 |
| **EasyView** | 简化视图——只显示 Image 单元格和判定结果，适合现场操作员使用 |
| **Job Validation** | 作业完整性校验：检查所有 A0-A1 相机配置、所有单元格表达式可求值、引用链无断裂 |

### 6.9 状态栏

```
合计: 0.0 毫秒                       [联机状态▼] [脱机]
 ↑ 整表一次求值耗时                    ↑ 相机连接状态  ↑ PLC 连接状态
```

- 每次 `Evaluate()` 完成后更新耗时
- 联机状态：🟢 绿色 = 已连接所有配置相机 / ⚪ 灰色 = 脱机
- PLC 状态：🟢 绿色 = Modbus TCP 已连接 / 🔴 红色 = 断开

### 6.10 记录/回放面板

```
┌──────────────────────────────────────────────────────────────┐
│  记录/回放                                         [×]       │
│  ┌────┐ ┌────┐ ┌────┐ ┌────┐ ┌────┐ ┌────┐                  │
│  │ 📷 │ │ 📷 │ │ 📷 │ │ 📷 │ │ 📷 │ │ ＋ │  ← 历史采集 + 新建 │
│  │2026│ │2026│ │2026│ │2026│ │2026│ │    │                    │
│  │11 │ │11 │ │11 │ │11 │ │11 │ │    │                    │
│  └────┘ └────┘ └────┘ └────┘ └────┘ └────┘                  │
│                                                              │
│  ⚙ 📁 [图像▼] [5▼] [Bmf▼] ●  延迟(ms): [200]  [▶] [⏭] [⏪] │
│                               [▶] [⏮]                       │
└──────────────────────────────────────────────────────────────┘
```

| 功能 | 说明 |
|------|------|
| 缩略图列表 | 显示最近 N 次采集（默认 5，可在 Image ▼ 中改），点击加载到 A0-A8 |
| 回放按钮 | ▶ 连续回放 / ⏭ 单帧前进 / ⏪ 单帧后退 / ⏮ 跳到首帧 |
| 延迟 | 回放时帧间隔，模拟 PLC 触发节拍 |
| ⚙ 设置 | 历史图像保存路径、保留数量、压缩格式 |
| 📁 导入 | 从文件夹批量导入历史 BMP/PNG 图像用于回放调试 |
| ● 录制 | 实时模式下点击开始保存每轮采集到磁盘 |

---

## 7. 与 VisionPro 的桥接（底层 API 直连，非 ToolBlock）

### 7.1 核心理念

电子表格模式 **不依赖 VisionPro ToolBlock + C# Script 体系**，每个 In-Sight 函数在实现层直接调用 VisionPro 的 Tool API。
用户侧只使用 In-Sight 的函数名和参数，**完全不接触 VisionPro 概念**。

```
用户代码（In-Sight 认知）         ← 用户只需懂这一层
    BlobDetect($A$0, 128, 0, 2)
         │
         ▼
JustSmart.Insight.Functions 层    ← 本项目新增
    BlobFunction.Invoke()
         │
         ▼
VisionPro API 层                  ← 直接调用，无 ToolBlock
    new CogBlobTool()
    tool.InputImage = image
    tool.Run()
         │
         ▼
Blobs 结果 → 包装成 In-Sight 风格数据结构
```

### 7.2 典型函数实现：Blob → CogBlobTool

```csharp
// functions.json 中 Blob 的签名：Blob(Image, MinArea, MaxArea, Polarity, Show)
[InsightFunction("Blob", Category="2D/通用", ...)]
public class BlobFunction : IInsightFunction
{
    public object Invoke(object[] args, EvalContext ctx)
    {
        var image = (Image)args[0];
        double minArea = args[1].ToDouble();
        double maxArea = args[2].ToDouble();
        int polarity = (int)args[3];

        var tool = ToolPool.GetBlob();
        try {
            tool.InputImage = image.CogImage;
            tool.MinArea = minArea;
            tool.MaxArea = maxArea;
            tool.ThresholdPolarity = (CogBlobThresholdPolarity)polarity;
            tool.Run();
            return new Blobs(tool.ResultCollection);  // 包装成 In-Sight Blobs
        } finally {
            ToolPool.Return(tool);
        }
    }
}
```

### 7.3 典型函数实现：FindPattern → CogPMAlignTool

```csharp
[InsightFunction("FindPatMaxRedLine", Category="2D/通用", ...)]
public class FindPatMaxRedLineFunction : IInsightFunction
{
    public object Invoke(object[] args, EvalContext ctx)
    {
        var image = (Image)args[0];
        var patternFile = (string)args[1];
        double acceptThresh = args[2].ToDouble();
        var region = args[3] as ShapeBase;

        var tool = ToolPool.GetPMAlign();
        try {
            tool.InputImage = image.CogImage;
            tool.TrainedPattern = LoadPattern(patternFile);
            tool.RunParams.AcceptThreshold = acceptThresh;
            if (region != null)
                tool.SearchRegion = region.ToCogRectangleAffine();
            tool.Run();
            return new Patterns(tool.ResultCollection);
        } finally {
            ToolPool.Return(tool);
        }
    }
}
```

### 7.4 结果结构体包装

VisionPro 原始结果对象（如 `CogBlobResultCollection`）对用户暴露的是 VisionPro 命名空间。我们包装成 In-Sight 风格：

```csharp
public class Blobs
{
    private readonly CogBlobResultCollection _inner;

    public int Count => _inner.Count;
    
    // In-Sight 风格 API（非 VisionPro 风格）
    public double GetX(int i) => _inner[i].CentroidX;
    public double GetY(int i) => _inner[i].CentroidY;
    public double GetArea(int i) => _inner[i].Area;
    public double GetPerimeter(int i) => _inner[i].Perimeter;
    public double GetScore(int i) => _inner[i].Score;
    public double GetWidth(int i) => _inner[i].Width;
    public double GetHeight(int i) => _inner[i].Height;
    public double GetAngle(int i) => _inner[i].Angle;
}
```

### 7.5 RunToolBlock（可选的兼容性通道）

虽然主要函数直连 VisionPro Tool API，但保留 `RunToolBlock` 作为**向后兼容**通道——允许用户在电子表格里调用已有的 Vproj ToolBlock：

```
RunToolBlock("检测", $A$0, $A$1, ...)
    → ctx.Runtime.LoadToolBlock("Recipes/型号A.Vproj", "检测")
    → 设置 Inputs
    → Run()
    → 读取 Outputs
```

**使用场景**：
- 客户已有 Vproj 配方不想重写
- 某些 VisionPro 高级功能暂无 In-Sight 等价函数
- 与现有 `PlcTriggerLoop` / `ManualInspectionRunner` 混合模式运行

### 7.6 图像共享

电子表格模式与 VisionPro 模式共享**同一套相机驱动**和**同一套 ICamera 抽象**：

```
JustSmart.VisionCenter.Core.Dev.Camera.ICamera（现有）
    │
    ├── VisionPro 模式：ManualInspectionRunner 取像后写入 ToolBlock.Inputs
    │
    └── 电子表格模式：AcquireImage() 函数取像 → 包装成 Image → 后续函数参数
```

**9 相机映射**：
```
A0 = TopCam1 (上方1)
A1 = TopCam2 (上方2)
A2 = FrontCam (前视)
A3 = RearCam (后视)
... (9 相机按 RecipeCollection.json 配置映射)
```

---

## 8. 配方与模式切换

### 8.1 RecipeCollection.json 扩展

```json
{
  "ParameterIndexs": [
    {
      "Index": 1,
      "ProductType": "安全带_型号A",
      "RecipeMode": "VisionPro",
      "VisionProRecipe": "Recipes/型号A.Vproj",
      "SheetRecipe": null
    },
    {
      "Index": 2,
      "ProductType": "安全带_型号B",
      "RecipeMode": "Spreadsheet",
      "VisionProRecipe": null,
      "SheetRecipe": "Recipes/型号B.jobx"
    }
  ]
}
```

### 8.2 运行时切换

```csharp
// PlcTriggerLoop / ManualInspectionRunner 中
switch (currentRecipe.Mode)
{
    case RecipeMode.VisionPro:
        var runner = new ToolBlockRunner(toolBlock);
        result = runner.Run(inputImages);
        break;
    case RecipeMode.Spreadsheet:
        var sheet = new SheetEngine(jobFile);
        sheet.SetInput("A0", inputImages[0]);  // TopCam1
        sheet.SetInput("A1", inputImages[1]);  // TopCam2
        // ... 9 相机
        sheet.Evaluate();
        result = sheet.GetResult("Output_OK");
        break;
    case RecipeMode.Hybrid:
        // 部分 Sheet 单元格用 In-Sight 函数，部分用 RunToolBlock 调用旧 ToolBlock
        var hybridSheet = new HybridSheetEngine(jobFile, vprojFile);
        result = hybridSheet.Run(inputImages);
        break;
}
```

### 8.3 向前兼容旧配方（`.Vproj`）

**原则**：电子表格模式的引入**不破坏任何现有 Vproj 配方**。已积累的配方资产全部可运行、可渐进迁移。

#### 8.3.1 兼容层次概览（由浅入深）

```
层次 1：双模式共存（默认）
  RecipeCollection.json 中旧配方保持 VisionPro 模式，直接照常运行
  新配方或转换的配方使用 Spreadsheet 模式

层次 2：混合模式（Hybrid）
  Sheet 中部分单元格用 In-Sight 函数，部分单元格用 RunToolBlock 调用旧 ToolBlock
  典型场景：9 相机中 6 个相机用新 In-Sight 函数重写，3 个相机暂时沿用 Vproj 中已调试好的 ToolBlock

层次 3：Vproj → Sheet 自动转换（RecipeConverter）
  将 Vproj 中的 ToolBlock 结构翻译成 Sheet 中的 In-Sight 函数链
  80% 自动翻译，20% 人工微调

层次 4：渐进式替换
  转换后的 Sheet 逐步用原生 In-Sight 函数替换 RunToolBlock 调用
  最终完全摆脱 ToolBlock 依赖
```

#### 8.3.2 层次 1 — 双模式共存

**配方管理界面**：扫描 `Recipes/` 目录，自动识别两种格式

```
配方管理界面
┌──────────────────────────────────────────────────────────────┐
│ Index  ProductType        Mode          RecipeFile           │
│ 1      安全带_型号A       VisionPro ⚙    型号A.Vproj          │
│ 2      安全带_型号B       Spreadsheet 📊 型号B.jobx          │
│ 3      安全带_型号C       VisionPro ⚙    型号C.Vproj  [升级]│
│                                                      ↑       │
│                                              点此转换为 Sheet │
└──────────────────────────────────────────────────────────────┘
```

点击 **[升级]** 按钮弹出 RecipeConverter：
- 自动加载对应 `.Vproj`（STAThread + BinaryFormatter，按项目 memory 记录的反序列化规范）
- 显示"可翻译 / 需人工处理"的 ToolBlock 列表
- 预览转换后的 Sheet 结构
- 用户确认后生成 `.jobx`，自动将 RecipeMode 改为 Spreadsheet

#### 8.3.3 层次 2 — 混合模式（Hybrid）

Sheet 中同时使用 In-Sight 原生函数和 RunToolBlock：

```
单元格        表达式                                    来源
─────────────────────────────────────────────────────────────────
A0           AcquireImage()                              In-Sight 原生
B0           RunToolBlock("前视_CheckSeatBelt", A0)     ← 调用旧 Vproj ToolBlock
C0           RunToolBlock("前视_ReadQRCode",    A0)     ← 调用旧 Vproj ToolBlock
D0           FindPatMaxRedLine(A0, "pat_anchor.pat", 0.7, ...)  In-Sight 原生重写
E0           IF(B0.PanDuan=1 AND C0.Str="OK" ...)       In-Sight 原生逻辑
Output_OK    IF(E0, 1, 2)                               → PLC 2003
```

**RunToolBlock 返回值统一**：

```csharp
// Vproj 中 ToolBlock 输出端字典 → Sheet 可用对象
public class ToolBlockResult
{
    public Dictionary<string, object> Outputs;   // 原始 Outputs["PanDuan"], Outputs["BLXM"]...
    
    // 常用字段便捷访问（与 Sheet 输出约定对齐）
    public int PanDuan => (int)Outputs.GetValueOrDefault("Output_PanDuan", 0);
    public string BLXM => (string)Outputs.GetValueOrDefault("Output_BLXM", "");
    public bool IsOK => PanDuan == 1;
    public bool IsNG => PanDuan >= 2 && PanDuan < 100;
    public bool IsError => PanDuan >= 101;
}
```

**HybridSheetEngine 执行流程**：

```
SetInput("A0", topCamImage)
RunToolBlock("前视_CheckSeatBelt", A0)
    → STAThread 加载 Vproj（缓存，不重复加载）
    → 设置 ToolBlock.Inputs
    → Run()（复用现有 VisionBlocks 配置 + Script）
    → 返回 ToolBlockResult
FindPatMaxRedLine(A0, ...)
    → CogPMAlignTool 直连（不走 ToolBlock）
IF(...)
    → 纯表达式求值
Output_OK = ...
→ 统一写入 PLC 2002/2003/2004（协议不变）
```

#### 8.3.4 层次 3 — Vproj → Sheet 自动转换（RecipeConverter）

**输入**：`型号A.Vproj`（VisionPro 二进制配置文件）
**输出**：`型号A.jobx`（In-Sight Spreadsheet JSON）

**转换规则表**：

| Vproj ToolBlock 子工具 | 自动翻译为 Sheet 函数 | 翻译置信度 |
|------------------------|----------------------|-----------|
| CogBlobTool | `Blob(Image, MinArea, MaxArea, Polarity)` | 95%（参数直接映射） |
| CogPMAlignTool | `FindPatMaxRedLine(Image, PatternFile, AcceptThreshold, Region)` | 90%（需提取 PatternFile 路径） |
| CogCaliperTool | `Caliper(Image, Region, Polarity, MinContrast)` | 85% |
| CogLineFindTool | `FindLine(Image, Region, ...)` | 80% |
| CogCircleFindTool | `FindCircle(Image, Region, ...)` | 80% |
| CogBarCodeTool + CogQRCodeTool | `ReadIDMax(Image, CodeType, MinStringLen)` | 85% |
| CogOCRMaxTool | `OCRMax(Image, CharacterSet, MinConfidence)` | 75%（字符集较复杂） |
| CogTransform2DLinear | `Calibrate()` + TransPixelToWorld 系列 | 70% |
| C# Script（VisionBlock 内） | ⚠ **不自动翻译** → 保留为 `RunToolBlock` 调用 | 0%（语义无法自动推断） |
| VisionPro Terminal 绑定 | 翻译为单元格引用（`$A$0`, `$B$1` 等） | 95% |
| VisionPro ToolBlock 输入/输出端 | 翻译为 Sheet 中命名单元格（`Output_OK` → 特殊单元格） | 90% |

**Converter 执行步骤**：

```
1. STAThread + BinaryFormatter.LoadFromFile(vprojPath)
   ← 按项目 memory 记录的 VisionPro 反序列化规范
   ← 失败时回退 Default.Vproj（防止 VisionBlocks 为空）

2. 遍历 VisionBlocks.Root.SubBlocks
   → 为每个 ToolBlock 创建 Sheet 区域（如 A0-A9 = 前视检测块，B0-B9 = 后视检测块）
   → 为每个子工具创建一个单元格，填入对应 In-Sight 函数

3. 分析每个子工具的 InputTerminals
   → 翻译成单元格引用（$A$0 = TopCam1 输入图）
   → 识别 Terminal.Value 是图像还是数字/字符串

4. 分析每个子工具的 OutputTerminals
   → 翻译成 Get* 系列函数调用（GetX(Blob, 0), GetScore(Pattern, 0)...）
   → PanDuan/BLXM 等终端 → 翻译为命名输出单元格

5. 识别 ToolBlock 中的 C# Script（VisionBlockAdvancedScript）
   → 检测 Script 是否是纯计算 / 还是依赖 VisionPro 特定 API
   → 纯计算 Script → 翻译为 Roslyn Script(...) 函数
   → 依赖 VisionPro API 的 Script → 保留为 RunToolBlock 调用（标记为需人工审核）

6. 生成 Job.json（Sheet 结构）
   → 保留原 VisionBlocks 的执行顺序
   → 保留原终端的 PanDuan 判定逻辑
   → 保留原 Output_BLXM 拼接逻辑

7. 校验 + 输出
   → 跑一遍 Sheet.Evaluate()，对比原 Vproj 同输入图的 PanDuan 输出
   → 差异 > 1% 的单元格标记 ⚠，生成人工审核清单
```

**转换示例**：

原 Vproj ToolBlock（前视_检测）：
```
前视_CheckSeatBelt (ToolBlock)
  ├─ ImageInputTerminal("InputImage") ← 接 TopCam1
  ├─ CogBlobTool "Blob1" (MinArea=10, Polarity=0)
  ├─ CogPMAlignTool "Pattern1" (PatternFile="pat_belt.pat", AcceptScore=0.7)
  ├─ CogCaliperTool "Caliper1" (FindEdgeInRegion, Polarity=1)
  ├─ CogBlobTool "Blob2" (MinArea=5, Polarity=1)
  ├─ Script Block "DecisionScript" (C#: if(Blob1.Count>0 && Pattern1.Count>0 && Caliper1.Count>0) PanDuan=1;)
  └─ IntegerOutputTerminal("PanDuan")
  └─ StringOutputTerminal("BLXM")
```

转换后 Sheet：
```
A0 = AcquireImage()                                          // TopCam1 取像
A1 = Blob($A$0, 10, 5000, 0)                                 // Blob1 → In-Sight 原生
A2 = FindPatMaxRedLine($A$0, "pat_belt.pat", 0.7, Rectangle(0,0,1280,720))
A3 = Caliper($A$0, Rectangle(0,100,1280,520), 1, 30)         // Caliper1 → In-Sight 原生
A4 = Blob($A$0, 5, 5000, 1)                                  // Blob2 → In-Sight 原生
A5 = IF(Count($A$1)>0 AND Count($A$2)>0 AND Count($A$3)>0,   // DecisionScript → IF 原生
       1, 2)
A6 = IF($A$5=1, "合格", "不合格")                             // BLXM
Output_OK = $A$5                                             // 映射到 PLC 2003
```

#### 8.3.5 层次 4 — Script 代码复用

Vproj 中 ToolBlock 里的 C# Script（VisionBlockAdvancedScript）有两种处理方式：

**方式 A：RunToolBlock 调用（零改动，立即可用）**
```
ScriptBlock("DecisionScript") → RunToolBlock("前视_CheckSeatBelt", A0)
```
不修改任何 Script 代码，ToolBlock 内部 Script 照常运行。

**方式 B：Roslyn 直接翻译（需要人工确认）**

VisionPro Script 中常见的模式：
```csharp
// 原 VisionPro Script（ToolBlock 内 C#）
public override bool Run(Cognex.VisionPro.ICogRecord[] inputs, 
    out Cognex.VisionPro.ICogRecord[] outputs)
{
    var blobs = inputs[0] as CogBlobResultCollection;
    var patterns = inputs[1] as CogPMAlignResultCollection;
    
    int panDuan = 0;
    string blxm = "";
    
    if (blobs.Count > 0 && patterns.Count > 0)
    {
        if (patterns[0].Score > 0.7) panDuan = 1;
        else { panDuan = 2; blxm = "Pattern Score 低"; }
    }
    else { panDuan = 101; blxm = "Blob 未找到"; }
    
    outputs[0] = new Cognex.VisionPro.ToolBlock.CogToolBlockTerminal("PanDuan", panDuan);
    outputs[1] = new Cognex.VisionPro.ToolBlock.CogToolBlockTerminal("BLXM", blxm);
    return true;
}
```

自动翻译为 Sheet 表达式：
```
A5 = IF(AND(Count($A$1)>0, Count($A$2)>0),
         IF(GetScore($A$2,0)>0.7,
            1,                        // OK
            2),                       // NG
         101)                         // Error
A6 = IF($A$5=1, "合格", IF($A$5=2, "Pattern Score 低", "Blob 未找到"))
```

**自动翻译覆盖度**：
- `IF/THEN/ELSE` → IF/AND/OR 表达式（高覆盖）
- `inputs[N] as CogXxx` → `$A$N` + 对应函数（高覆盖）
- `blobs.Count` → `Count($A$N)`（高覆盖）
- 复杂循环/反射/外部 API 调用 → ⚠ 保留为 RunToolBlock 调用（低覆盖）

#### 8.3.6 Vproj 加载细节（结合项目 memory 经验）

```csharp
public class VprojLoader
{
    public static Cognex.VisionPro.ToolBlock.CogToolBlock Load(string path)
    {
        // 按项目 memory 记录的反序列化规范
        // 必须 STAThread + SynchronizationContext，否则 VisionBlocks 变空字典
        return StaThread.Run(() =>
        {
            var recipe = new Cognex.VisionPro.ToolBlock.CogToolBlock();
            try
            {
                var settings = new Cognex.VisionPro.Core.CogSerializer();
                recipe = (Cognex.VisionPro.ToolBlock.CogToolBlock)
                    settings.LoadObjectFromFile(path);
            }
            catch (Exception ex)
            {
                // Vproj 缺失或损坏 → 回退 Default.Vproj（项目 memory 记录的降级逻辑）
                Serilog.Log.Warning(ex, "Vproj {Path} 加载失败，回退 Default.Vproj", path);
                var fallback = Path.Combine(
                    Path.GetDirectoryName(path), "Default.Vproj");
                recipe = settings.LoadObjectFromFile(fallback);
            }
            return recipe;
        });
    }
}
```

#### 8.3.7 运行时统一入口

无论 VisionPro / Spreadsheet / Hybrid 模式，**PLC 自动循环的对外行为完全一致**：

```
PlcTriggerLoop.RunCycle()  // 代码不变，只是内部 RecipeRunner 按模式分派
    │
    ├── VisionPro 模式 → ToolBlockRunner.Run(images)
    ├── Spreadsheet 模式 → SheetEngine.Evaluate()
    └── Hybrid 模式 → HybridSheetEngine.Run(images)
    
统一输出 → PlcTriggerLoop 读取结果写入 2002/2003/2004
统一记录 → MQTT / 日志 / 图像保存（代码不变）
```

#### 8.3.8 渐进式迁移路线图

```
Week 1-2：上线 RecipeMode 扩展 + 运行时双模式分派
  → 旧配方继续 VisionPro 运行，新开发的配方选 Spreadsheet

Week 3-4：上线 RunToolBlock 混合模式
  → 选择 1 个已有配方尝试 Sheet + ToolBlock 混合运行
  → 验证稳定性

Week 5-6：上线 RecipeConverter（自动转换）
  → 逐个把已有 Vproj 配方转为 Sheet
  → 人工审核差异 > 1% 的单元格，用原生 In-Sight 函数替换

Week 7+：持续替换 RunToolBlock 调用
  → 每个 ToolBlock 的 C# Script 逐步用原生 IF/AND/OR/Count 重写
  → 最终所有配方都是纯 Spreadsheet 模式
```

---

## 9. 开发计划（Phase 划分 + 218 函数分批次）

> 总目标：用 VisionPro API 实现 functions.json 中全部 218 个 2D 函数，对用户透明。

### Phase 1：核心表达式引擎（2-3 周）

| 任务 | 产出 |
|------|------|
| Tokenizer | `StringTokenizer.cs` |
| Parser + AST | `ExprNode` 体系、`Parse()` |
| Evaluator | `EvalContext`、`ExprNode.Evaluate()` |
| 内置函数（基础数学/逻辑） | IF, AND, OR, NOT, +, -, *, /, % |
| CellRef 解析 | 相对/绝对/混合引用 + 范围 |
| 依赖图 + 增量求值 | `CellGraph.TopoSortForRecompute()` |
| 单元测试 | 覆盖 100+ 表达式用例 |

**验收**：`IF(Count(B1)>0 AND Score(B1,0)>50, "OK", "NG")` 能正确求值

### Phase 2：批次 1 — ROI + Shape + 核心检测（~50 个函数）

| 类别 | 函数 | VisionPro API |
|------|------|--------------|
| **ROI/Shape** | Annulus, Circle, Cross, Line, Point, Polygon, Region | CogRectangleAffine, CogCircle, CogLine... |
| **Blob** | Blob, DetectBlobs, FindBlobs, SortBlobs, Count | CogBlobTool |
| **Pattern** | FindPatMaxRedLine | CogPMAlignTool |
| **Caliper/Edge** | Caliper, Edge, FindEdge, InspectEdge, InspectEdgePosition | CogCaliperTool |
| **Line/Circle Find** | FindLine, FindMultiLine, FindCircle, FindCircleMinMax | CogLineFindTool / CogCircleFindTool |
| **ID/OCR** | ReadIDMax, ID, OCRMax | CogBarCodeTool + CogQRCodeTool + CogOCRMaxTool |
| **Acquire** | Image, AcquireImage, StoreData, WriteImage | 相机驱动 + ICamera |
| **Script** | Script, Scripting | **Jint ES5（In-Sight Script，与智能相机通用）** + BlobJsProxy/PatternsJsProxy 数据结构包装 |
| **基础几何** | PointToPoint, PointToLine, LineToLine, BlobToBlob, BlobToLine, BlobToPoint | CogMath / 纯计算 |
| **Fixture** | Fixture, Fixtures | CogTransform2DLinear |

同时实现：ToolPool 对象池、Image/Blobs/Patterns/Edges 结果结构体包装

**验收**：能跑通安全带检测的完整流水线（AcquireImage → Blob → FindPattern → Caliper → 几何测量 → IF 判定）

**同步交付**：RunToolBlock 兼容通道（层次 2 混合模式立即可用）+ VprojLoader（STAThread + Default.Vproj 降级，按项目 memory 反序列化规范）

### Phase 3：批次 2 — 几何测量 + 统计 + 标定（~60 个函数）

| 类别 | 函数 |
|------|------|
| **几何测量** | PointToPointDistance, PointToPointAngle, PointToCircle, LineToCircle, CircleToCircle, BlobToRadian, MidLineToMidLine, LineFromNPoints, CircleFromNPoints |
| **配对测量** | PairDistance, PairMeanDistance, PairMaxDistance, PairMinDistance, PairSDevDistance, PairsToEdges, PairEdges |
| **边缘测量** | InspectEdgeWidth, InspectEdgeForDefect, FindCurve, FindSegment, SegmentFromLines |
| **统计/数学** | CountPassFail, Minimum, Maximum, ComputeStats, Statistics, Histogram, HistMin/Max/Mean/SDev/Sum, Accumulate, ClockedSum/Max/Min |
| **标定** | Calibrate, CalibratePoints, CalibrateGrid, CalibrateImage, ExtractCalibration, ExtractCalibrationPoints |
| **坐标变换** | TransPixelToWorld, TransWorldToPixel, TransFixtureToPixel, TransPixelToFixture, TransBlobsToWorld, TransPatternsToWorld, TransEdgesToWorld, TransBlobsToFixture, TransEdgesToFixture, TransPatternsToFixture |

### Phase 4：批次 3 — HMI + Plot + PLC/Comm + 控制流（~70 个函数）

| 类别 | 函数 |
|------|------|
| **HMI 控件** | Button, CheckBox, ListBox, Dialog, EditInt, EditFloat, EditString, EditAnnulus, EditCircle, EditLine, EditPoint, EditPolygon, EditPolylinePath, EditRegion, EditMaskedRegion, EditCompositeRegion, EditMultiGraphics, EditPassword |
| **Plot 绘图** | PlotLine, PlotCircle, PlotCross, PlotPoint, PlotRectangle, PlotAnnulus, PlotArc, PlotPolygon, PlotRegion, PlotCompositeRegion, PlotImage, PlotData, PlotGraphics, PlotString |
| **PLC/Comm** | ReadDevice, WriteDevice, ReadDiscrete, WriteDiscrete, TCPClient, TCPServer, FormatInputBuffer, FormatOutputBuffer, GetBufferData, ReadUserDataBuffer, ReadLatchedUserDataBuffer, WriteResultsBuffer, ClearResultsBuffer |
| **控制流/状态** | Event, SetEvent, Latch, LatchImage, LatchString, ShiftRegister, Repeat, RepeatUntil, Jump, Sleep, Timer, Now, GetClock, GetTimeValue |
| **系统状态** | GetOnline, GetConnectionStatus, GetTemperature, GetImage, GetErrorCode, GetErrorString, Status, MultiStatus, ValidateIDData, VerifyString |

### Phase 5：批次 4 — 剩余函数 + ViDi AI + 图像增强（~40 个函数）

| 类别 | 函数 |
|------|------|
| **图像增强** | CompareImage, ImageMath, ScaleImage, Colorize, Color, ColorLabel, ColorToGreyscaleFilter, ExtractColor, ExtractColorHistogram, TransformImage, Filter, Mask |
| **ViDi AI** | EdgeAIAnomalyDetect, EdgeAIClassify, EdgeAISegment, EdgeAIRead, DetectFlaw, SurfaceFlaw |
| **匹配训练** | TrainPatMaxRedLine, TrainFlawModel, TrainMatchColor, TrainExtractColor |
| **数据持久化** | WriteData, ExportData, ImportData, WriteMessage, ReadMessage, ReadImage, WriteImageSerial, ImportData |
| **辅助** | Image, ComputeImageSharpness, Stitch, Lookup, Wizard, SideCarTool |

### Phase 6：Job 序列化 + UI（3-4 周）

| 任务 | 产出 |
|------|------|
| Job.json 读写 | `JobSerializer.cs`（兼容 In-Sight 格式） |
| Sheet 模型 | `Sheet`, `Cell`, `CellGraph` |
| WPF DataGrid 控件 | 电子表格主视图（对齐 In-Sight 布局） |
| 函数面板 + 属性面板 | 拖拽函数 → 单元格 → 参数配置 |
| 公式栏（AvalonEdit） | 语法高亮 + 补全 |
| 图像预览 + ROI 叠加 | Image/Blobs/Patterns/Shape 显示 |
| 记录/回放面板 | 历史采集缩略图 + 回放控制 |
| 底部标签页 | 函数 / 采集 / EasyView / Job Validation |

**验收**：能在 WPF 设计器界面上从零创建 jobx，运行并看到检测结果

### Phase 7：系统集成 + 旧配方向前兼容（3 周）

| 任务 | 产出 |
|------|------|
| PLC 映射 | Sheet → 2002/2003/2004 寄存器 |
| 配方切换集成 | RecipeCollection.json 扩展（RecipeMode=VisionPro/Spreadsheet/Hybrid） |
| MQTT 日志集成 | Sheet 级别的执行日志 |
| 9 相机映射 | A0-A8 对应 RecipeCollection 配置的 9 路输入 |
| RecipeConverter（Vproj→Sheet 自动转换） | 见 8.3.4 转换规则表，CogBlobTool/PMAlign/Caliper 自动翻译，Script 保留为 RunToolBlock |
| 配方管理界面升级按钮 | 扫描 Recipes/ 自动识别格式 → 弹出 Converter → 生成 jobx |
| HybridSheetEngine | Sheet 中同时使用 In-Sight 原生函数 + RunToolBlock 调用旧 ToolBlock |
| 统一运行时入口 | PlcTriggerLoop.RunCycle() 分派三种模式，对外 PLC 协议不变 |

**验收**：PLC 自动循环模式下，VisionPro / Spreadsheet / Hybrid 三种模式都能完整驱动取像 → 检测 → 回写；选择一个已有 Vproj 配方转换成功，对比输出 PanDuan 差异 ≤ 1%

### Phase 8（可选）：LSP + In-Sight Script 兼容层

| 任务 | 产出 |
|------|------|
| LSP Server | OmniSharp 风格 C# LSP 服务 |
| VS Code 插件 | `.jobx` 文件语法高亮 + 补全 |
| In-Sight Script 兼容层 | Jint 引擎桥接 |
| 函数清单导入 | functions.json → 函数注册表自动填充 |

### 函数批次进度汇总

```
functions.json 2D/通用 总数：218
├── Phase 2 批次1（核心）        ~50  ← Blob/Pattern/Caliper/ID/OCR/基础几何
├── Phase 3 批次2（测量统计）     ~60  ← 配对测量/标定/坐标变换/统计
├── Phase 4 批次3（HMI/Comm）     ~70  ← HMI控件/Plot/PLC/控制流/状态查询
├── Phase 5 批次4（剩余+AI）     ~38  ← 图像增强/ViDi AI/训练/持久化
└── Phase 6-7 UI + 集成
```

---

## 10. 风险与约束

| 风险 | 概率 | 影响 | 缓解措施 |
|------|------|------|----------|
| VisionPro ToolBlock 与电子表格数据格式不兼容 | 中 | 高 | 统一 Image/Blobs 包装桥接层，Phase 2 优先解决 |
| In-Sight jobx 格式有隐藏的加密/校验 | 低（参考项目已成功读写） | 中 | 先用最小子集 Sheet 结构，兼容层渐进补齐 |
| 增量求值在大表（1000+ 单元格）性能差 | 中 | 中 | 依赖图 + 拓扑排序 + 缓存结果，测量后再优化 |
| 与现有 ManualInspectionRunner 互斥锁冲突 | 低 | 高 | Phase 4 集成时统一 CycleLock 入口 |
| HMI 控件函数在 PLC 自动模式下交互冲突 | 中 | 中 | HMI 单元格区分"设计时可编辑"和"运行时锁定"状态 |
| 维护两套函数库成本（VisionPro 原生 vs 电子表格） | 高 | 高 | 优先实现 Script 函数 + RunToolBlock 桥接，减少重复 |

---

## 附录 A：In-Sight 逆向类型清单

> 来自 `ilspycmd -l cide` 输出（2026-11-17）

### Designer.Core 表达式子系统关键类型

| 类型 | 作用 |
|------|------|
| `ExpressionData` | 单元格表达式容器（序列化 DataContract） |
| `Expression` | 表达式树根节点 |
| `StringTokenizer` | 词法分析器 |
| `TokenResolver` | 符号解析器（Tag 引用、函数调用、常量） |
| `ExpressionDataEvaluator` | 表达式求值器 |
| `FunctionCallNode` | 函数调用节点 |
| `TagNode` | Tag/单元格引用节点 |
| `LiteralNode` | 字面量节点 |
| `BinaryOperatorNode` | 二元运算符 |
| `ConditionalFunctionInfo` | IF 条件函数 |
| `CompareFunctionInfo` | 比较运算符函数 |

### Designer.Core Functions 子系统

| 类型 | 作用 |
|------|------|
| `IFunctionManager` | 函数注册表接口 |
| `IFunction` | 函数接口 |
| `IFunctionProxy` | 函数代理（动态调用） |
| `FunctionDescriptor` | 函数元数据 |
| `DynamicCommand` | 动态命令 |
| `ParameterAttribute` | 参数特性（标记函数参数） |

### Designer.Core Tags 子系统

| 类型 | 作用 |
|------|------|
| `ITag` | Tag 接口 |
| `Tag` | Tag 实现 |
| `DotFieldTag` | 属性 Tag（如 `Blobs.X(0)`） |
| `TagProxy` | Tag 代理（弱引用） |
| `ITagManager` | Tag 管理器接口 |
| `TagAddress` | Tag 地址（如 `Sheet1!Result`） |

### Designer.Core Project 子系统

| 类型 | 作用 |
|------|------|
| `IProject` | 项目接口 |
| `IWriteableProject` | 可写项目 |
| `ProjectContent` | 项目内容容器 |
| `ZipProject` | ZIP 格式项目 |
| `BinaryObject` | 二进制对象（图像等大对象嵌入） |

---

## 附录 B：参考项目索引

| 项目 | 路径 | 用途 |
|------|------|------|
| CognexJobxLanguageServerProtocol | `Reference/CognexJobxLanguageServerProtocol/` | LSP 参考 + functions.json 函数清单（350+） |
| WebAPISampleApp | `Reference/WebAPISampleApp/` | jobx 文件真实示例（HMI Spreadsheet Cells） |
| Cognex In-Sight 3D 函数参考 | `Reference/cameratinymes/reference/Cognex In-Sight 3D 相机电子表格函数参考（整理版）.md` | 函数 API 速查 |
| COGNEX_INSIGHT_PROGRAMMING.md | `Reference/CognexJobxLanguageServerProtocol/COGNEX_INSIGHT_PROGRAMMING.md` | Script JS 编程指南（run/draw/save/load） |

---

## 附录 C：技术选型参考

| 需求 | 选型 | 依据 |
|------|------|------|
| WPF DataGrid | Xceed.Wpf.DataGrid | In-Sight 26.1.0 自身使用；支持冻结列、合并单元格 |
| 停靠窗口 | AvalonDock | In-Sight 自身使用 |
| 公式编辑 | ICSharpCode.AvalonEdit | In-Sight 自身使用（`ICSharpCode.AvalonEdit.dll` 在安装目录） |
| Script 引擎 | Roslyn（初期）→ Jint（后续） | 项目已有 .NET 4.8 + C# 7.3 环境 |
| 图表/依赖图 | OxyPlot | In-Sight 使用 |
| MVVM | Prism（现有） | 项目已有 |
| 日志 | Serilog（现有） | 项目已有 |
| JSON | Newtonsoft.Json（现有） | 项目已有，与 In-Sight 格式兼容 |
| 压缩 | ICSharpCode.SharpZipLib | In-Sight 使用，jobx 是 ZIP |

---

> 文档完成后下一步：启动 Phase 1，先写 Tokenizer + Parser + Evaluator 单元测试，搭好表达式引擎骨架。
