# EditRegionTauri-ROI区域编辑器

浏览器端的 ROI（感兴趣区域）标注工具，支持旋转矩形、圆形和多边形。使用 Canvas 绘制并支持拖动、缩放、旋转及顶点编辑；拖动旋转矩形右端外侧的圆弧箭头图标可形成上下边为同心圆弧、左右端边为直线的弯曲矩形，弯曲后导出为 **polygon 多点拟合**。图像以 `imageData`（base64）内嵌，`imagePath` 留空。

## 文件说明

| 文件 | 说明 |
|------|------|
| `index.html` | 编辑器源码（p5.js 负责画布，绘制走 `drawingContext`），图像通过相对路径 `../图像_0000_20261102055654.png` 引用 |
| `p5.min.js` | p5.js v1.9.4（MIT），画布创建与像素密度管理 |
| `index.single.html` | 单文件版，p5.js 与图像均已内联，可直接分发/双击打开 |
| `build_singlefile_html.py` | 由 `index.html` 生成 `index.single.html` 的打包脚本 |
| `README.md` | 本文档 |

## 运行与构建

```bash
# 方式一：本地 HTTP 服务（推荐，画布不被跨域污染，RGB 读数与图像导出均可用）
python -m http.server 8080
# 浏览器打开 http://localhost:8080/EditRegionMock/index.html

# 方式二：直接打开单文件版（无需服务器）
# 双击 index.single.html 即可

# 重新打包单文件版（在任意目录执行均可）
python build_singlefile_html.py
python build_singlefile_html.py --input index.html --image ../图像_0000_20261102055654.png --output index.single.html
```

## 操作说明

### 画布与视图

- **滚轮**：以鼠标位置为中心缩放图像。
- **拖动空白区域**：平移视图。
- **工具栏适应按钮**或 **`0`**：重置缩放并适应窗口。
- **`+` / `-`**：放大或缩小视图。
- 状态栏显示图像来源、标注数量、图像与画布尺寸、缩放比例及鼠标位置等信息。

### 新建、选择与通用编辑

- 在画布空白处**右键**，选择“添加旋转矩形”“添加多边形”或“添加圆形”。
- 单击区域可选中；拖动区域内部可整体移动。
- 选中区域后**右键**，可使用“编辑区域”“锁定 / 解锁”“删除标注”。
- “编辑区域”：多边形进入顶点和边编辑模式；矩形、圆形打开属性编辑窗口。
- **`Insert`**：新建旋转矩形；**`Delete`** 或工具栏 **✕**：删除当前区域。
- **`L`**：锁定或解锁当前区域。锁定后仍可右键选择并解锁。
- **双击区域**或 **`F2`**：打开属性页，可修改名称、位置、大小、旋转角、显示模式和允许操作项。
- 顶部参数栏可直接输入 X、Y、宽、高和旋转角，按回车或输入框失焦后生效，单位为图像像素。

### 旋转矩形与弯曲矩形

- 拖动四个角手柄：双向调整宽和高。
- 拖动四边中点手柄：普通矩形单独调整宽或高；弯曲矩形的左右直边在保持扇环圆心、半径和另一侧直边位置不变的前提下，仅调整当前一侧的弧长；上下曲边调整厚度。
- 拖动底边中点外侧的 **`↻`** 手柄：旋转矩形（带阻尼）。
- 拖动右端外侧的**圆弧箭头图标**：调整整体弯曲，使上、下边形成同心圆弧，左右端边保持直线。
- 顶部参数栏右侧“拟合”显示当前表示形式：`矩形 4 点` 或 `多边形 N 点`（弯曲后）。

### 圆形

- 拖动圆形内部：移动圆心。
- 拖动缩放手柄：以圆心为中心调整直径，圆心位置保持不变。
- 也可通过顶部参数栏或属性页精确调整位置和尺寸。

### 多边形

- 新建多边形时默认生成一个闭合三角形。
- **悬停边**：当前边加粗高亮；**单击边**：在该位置插入顶点。
- **悬停顶点**：顶点放大高亮；**拖动顶点**：调整顶点位置。
- 在顶点或边上双击不会结束编辑，也不会重复插点。
- **在边和顶点以外区域双击**或**右键**：结束绘制并自动闭合首尾点。
- **`Esc`**：结束当前多边形编辑。
- 已保存的多边形可通过右键“编辑区域”再次进入编辑；结束后更新原区域，不会创建副本。
- 非编辑状态下，拖动多边形内部可整体移动全部顶点。

### 越界警告

- 任一区域有任何部分超出图像边界时，图像四周会出现 **6 像素粉红色粗边框**。
- 边框以 **1Hz** 频率闪烁；所有区域回到图像范围内后自动消失。

### 导入、导出与显示

- 工具栏图片按钮：打开本地图片；启用自动保存时，可载入或创建同名 JSON。
- 工具栏上传按钮 **⇧**：选择 X-AnyLabeling `.json` 标注文件；也可将图片或 JSON 直接拖入画布。
- 工具栏 **⇩**：导出 X-AnyLabeling 标注 JSON。
- 工具栏 **`A`**：显示或隐藏标注名称。
- 工具栏 **✓**：显示或隐藏 ROI 图形。
- 工具栏橡皮按钮：卸载图像并清空全部标注。

### 载入既有标注 JSON

上传/拖入 X-AnyLabeling JSON 后即可继续编辑，映射规则：

- 图像：优先使用 `imageData`（base64）作为当前图像；无 `imageData` 时沿用当前图像，仅套用标注
- `shape_type: "rotation"`：取四点还原为旋转 ROI（宽高取相邻点距离，角度取首边方向）
- `shape_type: "polygon"`：以首边方向为参考轴，对点集求方向包围盒（还原为旋转 ROI，弯曲本身不还原）
- `shape_type: "rectangle"` 及其它形状：取点集外接矩形（轴对齐）
- 导出时若图像来自 JSON，`imageData` 原样复用，文件名基于该 JSON 名
- 导出文件名统一带时间戳后缀 `_YYYYMMDD_HHMMSS`，例如 `图像_0000_20261102055654_20261125_111754.json`，便于区分多次导出

---

# X-AnyLabeling 标注 JSON 格式说明

导出的 JSON 对标 X-AnyLabeling 3.x 版本（参考样例：`图像_0000_20261102055654.json`，`version = 3.2.1`）。
本编辑器支持**旋转矩形**（`rotation`）、**圆形**（`circle`）、**多边形**（`polygon`）与**弯曲矩形**（导出为 `polygon` 多点拟合）。

## 顶层结构

| 字段 | 类型 | 说明 |
|------|------|------|
| `version` | string | X-AnyLabeling 文件版本，固定 `"3.2.1"` |
| `flags` | object | 文件级标记，固定 `{}` |
| `shapes` | array | 标注形状数组，每个 ROI 一项 |
| `imagePath` | string | **固定空字符串 `""`**，本工具不使用该字段定位图像 |
| `imageData` | string | 图像 base64 载荷（**不含** `data:image/png;base64,` 前缀） |
| `imageHeight` | int | 图像高度（像素），取自原图 |
| `imageWidth` | int | 图像宽度（像素），取自原图 |

> 字段顺序与 X-AnyLabeling 原生导出一致，便于逐行比对 diff。

## shapes 元素结构

每个 ROI 生成一个形状对象，字段顺序固定：

| 字段 | 类型 | 说明 |
|------|------|------|
| `label` | string | 标注名称，默认 `EditRegion1`、`EditRegion2`…（可在工具栏改名） |
| `score` | null | 标注置信度，人工标注固定 `null` |
| `points` | array | 未弯曲：四角点 `[[x0,y0],…,[x3,y3]]`；弯曲后：多边形拟合点序列（≥4 点，总数 ≤512） |
| `group_id` | null | 分组 ID，固定 `null` |
| `description` | string | 描述，固定 `""` |
| `difficult` | boolean | 是否困难样本，固定 `false` |
| `shape_type` | string | 未弯曲固定 `"rotation"`（旋转矩形）；弯曲后为 `"polygon"`（多点拟合） |
| `flags` | object | 形状级标记，固定 `{}` |
| `attributes` | object | 形状属性，固定 `{}` |
| `kie_linking` | array | 关键信息抽取关联，固定 `[]` |
| `direction` | number | `rotation` 时为旋转角度（度，`(-180, 180]`）；`polygon` 时为 `0` |

### points 顺序

四点按**顺时针**给出，起点为 ROI 的左上角（未旋转坐标系下）：

```
p0 = 左上 (nw)  →  p1 = 右上 (ne)  →  p2 = 右下 (se)  →  p3 = 左下 (sw)
```

坐标为**图像坐标系**（原点在图像左上角，x 向右、y 向下，单位 px），已包含旋转，因此四个点一般不是轴对齐矩形。

### 弯曲 ROI 的 polygon 拟合

右端外侧的弯曲图标被拖动后，该 ROI 改以 `shape_type: "polygon"` 导出，点为沿弯曲轮廓的采样序列：

- ROI 中心线建模为**圆弧**，上、下边为同心圆弧，左、右端边为径向直线，形成完整的弯曲矩形
- 右端外侧弯曲图标的拖动方向控制圆心角；支持大于 180° 的扇环，并限制最大角度以避免内外圈交叉
- 每段点数按圆弧弦高误差 ≤0.5px 计算全保真点数，再取 **80%**（`POLY_RATIO = 0.8`）以近似原始曲率
- 全部曲边点数总和限制在 **512 点**以内，超限时按比例压缩（每条曲边至少 2 点；直边仍只贡献 1 个角点）
- 点序仍按 左上 → 右上 → 右下 → 左下 的四边方向闭环排列

### direction 与形状的关系

- `shape_type: "rotation"` 时，`direction` 为该 ROI 的旋转角度（度），与状态栏「旋转」值一致，取值 `(-180, 180]`，X-AnyLabeling 以 `points` 还原旋转矩形
- `shape_type: "polygon"` 时，`direction` 固定 `0`，形状完全由 `points` 多点拟合表示

## 示例

```json
{
  "version": "3.2.1",
  "flags": {},
  "shapes": [
    {
      "label": "EditRegion1",
      "score": null,
      "points": [
        [1402.838, 827.0],
        [2268.838, 827.0],
        [2268.838, 1649.0],
        [1402.838, 1649.0]
      ],
      "group_id": null,
      "description": "",
      "difficult": false,
      "shape_type": "rotation",
      "flags": {},
      "attributes": {},
      "kie_linking": [],
      "direction": 0
    }
  ],
  "imagePath": "",
  "imageData": "iVBORw0KGgoAAAANSUhEUg...（base64 载荷，此处省略）",
  "imageHeight": 3000,
  "imageWidth": 4096
}
```

## imageData 的取图规则

导出时按以下顺序获取 base64，确保画布不被跨域污染（`file://` 直开 `index.html` 时画布会被污染，故优先走前几步）：

0. 图像是由 JSON 的 `imageData` 载入的 → 直接复用该 base64 载荷（不重新编码）
1. `IMG_SRC` 本身是 `data:` URI（单文件版 `index.single.html`）→ 直接截取 `base64,` 之后的载荷
2. 通过 HTTP 打开 → `fetch(IMG_SRC)` 读取原始字节，分块转 base64（不重新编码，保持原图字节）
3. 兜底 → `canvas.toDataURL("image/png")`（仅在同源/未污染时可用）

## 与 Java 模型 `XAnyLabelingJson` 的对应

`XAnyLabelingJson` / `Shape`（`com.honda.paroledipinte.model`）通过 Gson 反序列化本格式：

| JSON 字段 | Java 字段 | 说明 |
|-----------|-----------|------|
| `version` | `version` | 文件版本 |
| `flags` | `flags` | 文件级标记 |
| `shapes` | `shapes: List<Shape>` | 形状列表 |
| `imageData` | `imageData` | base64 载荷，优先于 `imagePath` 使用 |
| `imageHeight` / `imageWidth` | 同名 int 字段 | 图像尺寸 |
| `label` / `points` / `description` / `difficult` | 同名 | 标注内容与点位 |
| `shape_type` / `group_id` | `shapeType` / `groupId` | 命名风格不同，`Shape` 里 `shapeType` 默认值即 `"rotation"`，不依赖该键；下游仅使用 `label` 与 `points` |

因此 `imagePath` 留空不影响 Java 端读取：图像完全由 `imageData` 提供。

## 兼容性注意事项

- 文件编码 UTF-8，JSON 为 2 空格缩进的格式化输出
- `imageData` 载荷较大（示例图 5.09 MB → base64 约 6.8 MB），导出文件约 6.9 MB，属正常
- 图像未加载完成或没有 ROI 时不会导出，会给出提示
- 若需要浏览器以外的程序消费，可先用 `python -m http.server` 提供页面，避免 `file://` 限制

## 许可证

MIT License
