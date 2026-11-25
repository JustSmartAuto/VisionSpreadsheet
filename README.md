# EditRegion Mock — ROI 区域编辑器

浏览器端的 ROI（感兴趣区域）标注工具 Mock，用 Canvas 绘制、拖拽、缩放、旋转旋转矩形 ROI，并导出 **X-AnyLabeling** 标注 JSON。图像以 `imageData`（base64）内嵌，`imagePath` 留空。

## 文件说明

| 文件 | 说明 |
|------|------|
| `index.html` | 编辑器源码，图像通过相对路径 `../图像_0000_20261102055654.png` 引用 |
| `index.single.html` | 单文件版，图像已内联为 data URI，可直接分发/双击打开 |
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
- 拖 ROI 内部的四个角/边手柄缩放，拖底部 `↻` 手柄旋转（已加阻尼）
- `Insert`：新建 ROI 并选中；`Delete`（或工具栏 ✕）：删除当前 ROI
- 双击区域 / `F2`：打开属性页（可改位置、大小、旋转角、显示模式与允许操作项）
- 工具栏 `✓`：显示/隐藏 ROI 图形；工具栏 `⇩`：导出 X-AnyLabeling 标注 JSON
- 顶部参数栏可直接输入 X / Y / 宽 / 高 / 旋转角，回车或失焦生效（图像坐标 px）
- 状态栏可查看 ROI 的位置 / 大小 / 旋转角，`#` 序号与名称可直接编辑

---

# X-AnyLabeling 标注 JSON 格式说明

导出的 JSON 对标 X-AnyLabeling 3.x 版本（参考样例：`图像_0000_20261102055654.json`，`version = 3.2.1`）。
本编辑器只负责**旋转矩形** ROI，对应 X-AnyLabeling 的 `rotation` 形状。

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
| `points` | array | 四角点坐标，`[[x0,y0],[x1,y1],[x2,y2],[x3,y3]]`，图像坐标（px） |
| `group_id` | null | 分组 ID，固定 `null` |
| `description` | string | 描述，固定 `""` |
| `difficult` | boolean | 是否困难样本，固定 `false` |
| `shape_type` | string | 形状类型，固定 `"rotation"`（旋转矩形） |
| `flags` | object | 形状级标记，固定 `{}` |
| `attributes` | object | 形状属性，固定 `{}` |
| `kie_linking` | array | 关键信息抽取关联，固定 `[]` |
| `direction` | number | 旋转角度（度），等于编辑器中的 ROI 旋转角，取值 `(-180, 180]` |

### points 顺序

四点按**顺时针**给出，起点为 ROI 的左上角（未旋转坐标系下）：

```
p0 = 左上 (nw)  →  p1 = 右上 (ne)  →  p2 = 右下 (se)  →  p3 = 左下 (sw)
```

坐标为**图像坐标系**（原点在图像左上角，x 向右、y 向下，单位 px），已包含旋转，因此四个点一般不是轴对齐矩形。

### direction 与形状的关系

- `direction` 为该 ROI 的旋转角度（度），与状态栏「旋转」值一致，取值 `(-180, 180]`
- `shape_type: "rotation"` 时，X-AnyLabeling 以 `points` 还原旋转矩形，`direction` 供界面显示与再编辑
- 角度为 0 时同样导为 `rotation`（四点即轴对齐矩形的四角），保证同一种结构可回读

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

导出时按以下顺序获取 base64，确保画布不被跨域污染（`file://` 直开 `index.html` 时画布会被污染，故优先走前两步）：

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
