using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000013 RID: 19
	public class CvDisplayGraphicsDot : CvDisplayGraphicsShape
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000065FC File Offset: 0x000047FC
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00006614 File Offset: 0x00004814
		public virtual Point2d Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				this._Location = value;
				this.DisplayOrigin = this.TranslateToScreenPos(new Point2d(value.X, value.Y));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000663C File Offset: 0x0000483C
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00006659 File Offset: 0x00004859
		public double X
		{
			get
			{
				return this.Location.X;
			}
			set
			{
				this.Location = new Point2d(value, this.Y);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00006670 File Offset: 0x00004870
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000668D File Offset: 0x0000488D
		public double Y
		{
			get
			{
				return this.Location.Y;
			}
			set
			{
				this.Location = new Point2d(this.X, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00005EDE File Offset: 0x000040DE
		public override Point2d DisplayOrigin
		{
			get
			{
				return this.TranslateToScreenPos(this.Location);
			}
			set
			{
				base.DisplayOrigin = value;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000066C2 File Offset: 0x000048C2
		public CvDisplayGraphicsDot() : this(-1.0, -1.0)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000066DE File Offset: 0x000048DE
		public CvDisplayGraphicsDot(double x, double y)
		{
			this.IsMove = false;
			this.UpDataRoi = null;
			this._focusRange = 5.0;
			this.Location = new Point2d(x, y);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006713 File Offset: 0x00004913
		public CvDisplayGraphicsDot(Point2d p)
		{
			this.IsMove = false;
			this.UpDataRoi = null;
			this._focusRange = 5.0;
			this.Location = p;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006742 File Offset: 0x00004942
		protected override void OnDisplayOriginChanged(Point2d oldPos, Point2d newPos)
		{
			this._Location = this.TranslateToMatPos(newPos);
			base.OnDisplayOriginChanged(oldPos, newPos);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000675C File Offset: 0x0000495C
		public override bool IsMouseIn(PointF pos)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * 2.0, this._focusRange * 2.0).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000067D0 File Offset: 0x000049D0
		public bool IsMouseIn(PointF pos, int r)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * (double)r, this._focusRange * (double)r).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006838 File Offset: 0x00004A38
		public bool IsMouseInAngle(PointF pos, float r)
		{
			return Math.Sqrt((this.DisplayOrigin.X - (double)pos.X) * (this.DisplayOrigin.X - (double)pos.X) + (this.DisplayOrigin.Y - (double)pos.Y) * (this.DisplayOrigin.Y - (double)pos.Y)) < (double)r;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000068A8 File Offset: 0x00004AA8
		public override void OnMouseMove(MouseEventArgs e)
		{
			bool isMove = this.IsMove;
			if (!isMove)
			{
				base.OnMouseMove(e);
				bool flag = base.Selected && this.IsLeftMouseDown;
				if (flag)
				{
					this.DisplayOrigin += new Point2d((double)(e.X - this.MouseDownPos.X), (double)(e.Y - this.MouseDownPos.Y));
					this.MouseDownPos = e.Location;
				}
			}
		}

		public override void OnPaint(PaintEventArgs e)
		{
			Size2d pixelSize = this.PixelSize;
			double dispX = base.MatDisplayOrigin.X + this.X * pixelSize.Width;
			double dispY = base.MatDisplayOrigin.Y + this.Y * pixelSize.Height;
			bool flag = new Rect2d(0.0, 0.0, (double)e.ClipRectangle.Width, (double)e.ClipRectangle.Height).Contains(dispX, dispY);
			if (flag)
			{
				base.DrawCross(e.Graphics, new PointF((float)dispX, (float)dispY), (base.IsFocused || base.Selected) ? base.FocusedColor : base.Color, 10f);
			}
			bool flag2 = this.UpDataRoi != null;
			if (flag2)
			{
				this.UpDataRoi();
			}
		}

		// Token: 0x0400005D RID: 93
		protected Point2d _Location;

		// Token: 0x0400005E RID: 94
		public bool IsMove;

		// Token: 0x0400005F RID: 95
		public D_UpData UpDataRoi;

		// Token: 0x04000060 RID: 96
		protected double _focusRange;
	}
}
