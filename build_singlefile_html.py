#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
将 EditRegionMock/index.html 打包为单文件 HTML：
把 index.html 的两处外部资源（p5.min.js 与测试图像）分别内联，
输出结果可脱离目录独立分发，且 file:// 打开时画布不再被污染（RGB 读数可用）。

用法（在任意目录执行均可）：
    python build_singlefile_html.py
    python build_singlefile_html.py --input index.html --image "../图像_0000_20261102055654.png" --output index.single.html
"""

import argparse
import base64
import mimetypes
import re
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent

# index.html 里引用测试图像的那一行：const IMG_SRC = "../图像_0000_20261102055654.png";
IMG_SRC_RE = re.compile(r'(const\s+IMG_SRC\s*=\s*")([^"]+)(")')
# p5 库引用：<script src="./p5.min.js"></script>
P5_RE = re.compile(r'<script\s+src="\./p5\.min\.js"\s*></script>')


def to_data_uri(image_path: Path) -> str:
    mime, _ = mimetypes.guess_type(image_path.name)
    if not mime:
        mime = "image/png"
    payload = base64.b64encode(image_path.read_bytes()).decode("ascii")
    return f"data:{mime};base64,{payload}"


def main() -> int:
    ap = argparse.ArgumentParser(description="把 ROI 编辑器 Mock 打包成单文件 HTML")
    ap.add_argument("--input", default=str(HERE / "index.html"), help="源 HTML")
    ap.add_argument(
        "--image",
        default=str(HERE.parent / "图像_0000_20261102055654.png"),
        help="要内联的测试图像",
    )
    ap.add_argument("--output", default=str(HERE / "index.single.html"), help="输出 HTML")
    ap.add_argument("--p5", default=str(HERE / "p5.min.js"), help="要内联的 p5.min.js")
    args = ap.parse_args()

    html_path = Path(args.input).resolve()
    image_path = Path(args.image).resolve()
    out_path = Path(args.output).resolve()
    p5_path = Path(args.p5).resolve()

    if not html_path.is_file():
        print(f"[错误] 找不到源 HTML: {html_path}", file=sys.stderr)
        return 1
    if not image_path.is_file():
        print(f"[错误] 找不到测试图像: {image_path}", file=sys.stderr)
        return 1
    if not p5_path.is_file():
        print(f"[错误] 找不到 p5.min.js: {p5_path}", file=sys.stderr)
        return 1

    html = html_path.read_text(encoding="utf-8")
    matches = IMG_SRC_RE.findall(html)
    if not matches:
        print("[错误] 未在 HTML 中找到 `const IMG_SRC = \"...\"`", file=sys.stderr)
        return 1
    if len(matches) > 1:
        print(f"[错误] 找到 {len(matches)} 处 IMG_SRC，无法确定替换目标", file=sys.stderr)
        return 1

    data_uri = to_data_uri(image_path)
    single = IMG_SRC_RE.sub(lambda m: m.group(1) + data_uri + m.group(3), html, count=1)

    lib = p5_path.read_text(encoding="utf-8")
    p5_count = len(P5_RE.findall(single))
    if p5_count != 1:
        print(f"[错误] 找到 {p5_count} 处 p5.min.js 引用，无法确定替换目标", file=sys.stderr)
        return 1
    single = P5_RE.sub(lambda _m: "<script>\n" + lib + "\n</script>", single, count=1)

    out_path.write_text(single, encoding="utf-8")

    print(f"已生成: {out_path}")
    print(f"  图像: {image_path.name} ({image_path.stat().st_size / 1024 / 1024:.2f} MB)"
          f" -> base64 内联")
    print(f"  p5  : {p5_path.name} ({p5_path.stat().st_size / 1024 / 1024:.2f} MB) -> 内联")
    print(f"  输出大小: {out_path.stat().st_size / 1024 / 1024:.2f} MB")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
