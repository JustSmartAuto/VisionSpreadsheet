# EditRegion Mock — ROI 区域编辑器

浏览器端的 ROI（感兴趣区域）标注工具 Mock，用 Canvas 绘制、拖拽、缩放、旋转旋转矩形 ROI；拖动右端外侧的圆弧箭头图标可形成上下边为同心圆弧、左右端边为直线的弯曲矩形，弯曲后导出为 **polygon 多点拟合**，否则导出为旋转矩形。图像以 `imageData`（base64）内嵌，`imagePath` 留空。

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

## 操作

- 滚轮：以光标为锚点缩放；拖拽空白：平移
- 拖 ROI 的四个角手柄双向缩放；拖四条边的中点手柄可单独调整宽或高；仅拖右端外侧的**圆弧箭头图标**控制整体弯曲，使上、下边形成同心圆弧，左、右端边保持直线；边上不显示弯曲按钮，其他三边也不提供弯曲入口
- 拖底边中点外侧的 `↻` 圆形手柄旋转（已加阻尼）
- 顶部参数栏右侧「拟合」显示当前 ROI 的表示形式：`矩形 4 点` 或 `多边形 N 点`（弯曲后）
- `Insert`：新建 ROI 并选中；`Delete`（或工具栏 ✕）：删除当前 ROI
- 双击区域 / `F2`：打开属性页（可改位置、大小、旋转角、显示模式与允许操作项）
- 工具栏上传按钮（⇧）：选择 `.json` 标注文件；也可把 JSON 直接拖到画布
- 工具栏适应按钮：重置缩放为适应窗口；`A` 按钮：显示/隐藏标注名称
- 工具栏橡皮按钮：清除（卸载图像并清空全部标注）
- 工具栏 `✓`：显示/隐藏 ROI 图形；工具栏 `⇩`：导出 X-AnyLabeling 标注 JSON
- 顶部参数栏可直接输入 X / Y / 宽 / 高 / 旋转角，回车或失焦生效（图像坐标 px）
- 状态栏显示当前图像来源、标注数量、图像尺寸、画布尺寸与缩放比例

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
本编辑器支持**旋转矩形**（`rotation`）与**弯曲矩形**（`polygon` 多点拟合）两种形状。

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
