using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000010 RID: 16
	public class CvDisplayGraphicsCircle : CvDisplayGraphicsDot
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00005B03 File Offset: 0x00003D03
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00005B0B File Offset: 0x00003D0B
		public double Radius { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x00005B14 File Offset: 0x00003D14
		public CvDisplayGraphicsCircle(double x, double y, double radius) : base(x, y)
		{
			this.Radius = radius;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005B50 File Offset: 0x00003D50
		public override bool IsMouseIn(PointF pos)
		{
			bool centerin = base.IsMouseIn(pos);
			double distance = Caculate.Distance(this.DisplayOrigin, new Point2d((double)pos.X, (double)pos.Y));
			double r = this.Radius * this.PixelSize.Width;
			bool edgein = distance <= r + this._focusRange && distance >= r - this._focusRange;
			CvDisplayGraphicsDot startDot = new CvDisplayGraphicsDot(this.Location.X + this.Radius, this.Location.Y);
			startDot.ParentMat = this.ParentMat;
			bool flag = centerin;
			if (flag)
			{
				this._SelectedType = CvDisplayGraphicsCircle.SelectedType.Center;
			}
			else
			{
				bool flag2 = startDot.IsMouseIn(pos, this.radius);
				if (flag2)
				{
					this._SelectedType = CvDisplayGraphicsCircle.SelectedType.Edge;
				}
				else
				{
					bool flag3 = edgein;
					if (flag3)
					{
						this._SelectedType = CvDisplayGraphicsCircle.SelectedType.Center;
					}
					else
					{
						this._SelectedType = CvDisplayGraphicsCircle.SelectedType.None;
					}
				}
			}
			return centerin || edgein;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005C38 File Offset: 0x00003E38
		public override void OnMouseMove(MouseEventArgs e)
		{
			bool isMove = this.IsMove;
			if (!isMove)
			{
				bool flag = this._SelectedType == CvDisplayGraphicsCircle.SelectedType.Center;
				if (flag)
				{
					base.OnMouseMove(e);
				}
				else
				{
					bool flag2 = base.Selected && this.IsLeftMouseDown && this._SelectedType == CvDisplayGraphicsCircle.SelectedType.Edge;
					if (flag2)
					{
						Point2d NowPoint = this.TranslateToMatPos(new Point2d((double)e.X, (double)e.Y));
						this.Radius = Math.Sqrt((NowPoint.X - base.X) * (NowPoint.X - base.X) + (NowPoint.Y - base.Y) * (NowPoint.Y - base.Y));
						this.MouseDownPos = e.Location;
					}
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005CFC File Offset: 0x00003EFC
		public override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Point2d p = this.TranslateToScreenPos(this.Location);
			Size2d size = this.PixelSize;
			double rw = size.Width * this.Radius;
			double rh = size.Height * this.Radius;
			float drawSize = base.GetDrawSize();
			Pen pen = new Pen(base.GetDrawColor(), drawSize);
			RectangleF rect = new RectangleF((float)(p.X - rw), (float)(p.Y - rh), (float)(2.0 * rw), (float)(2.0 * rh));
			Point2d p2 = new Point2d(this.Location.X + this.Radius, this.Location.Y);
			Point2d p3 = this.TranslateToScreenPos(p2);
			e.Graphics.FillRectangle(this.brush, new RectangleF(new PointF((float)p3.X, (float)p3.Y), new SizeF((float)(this.radius * 2), (float)(this.radius * 2))));
			e.Graphics.DrawArc(pen, rect, 0f, 360f);
		}

		// Token: 0x0400004E RID: 78
		protected CvDisplayGraphicsCircle.SelectedType _SelectedType = CvDisplayGraphicsCircle.SelectedType.None;

		// Token: 0x0400004F RID: 79
		private int radius = 5;

		// Token: 0x04000050 RID: 80
		public new bool IsMove = false;

		// Token: 0x04000052 RID: 82
		private SolidBrush brush = new SolidBrush(Color.Yellow);

		// Token: 0x02000022 RID: 34
		protected enum SelectedType
		{
			// Token: 0x0400009D RID: 157
			None,
			// Token: 0x0400009E RID: 158
			Center,
			// Token: 0x0400009F RID: 159
			Edge
		}
	}
}
