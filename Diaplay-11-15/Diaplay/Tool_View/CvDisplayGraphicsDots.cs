using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000012 RID: 18
	public class CvDisplayGraphicsDots : CvDisplayGraphicsShape
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00006278 File Offset: 0x00004478
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00006290 File Offset: 0x00004490
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000062B8 File Offset: 0x000044B8
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000062D5 File Offset: 0x000044D5
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000062EC File Offset: 0x000044EC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00006309 File Offset: 0x00004509
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006320 File Offset: 0x00004520
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00005EDE File Offset: 0x000040DE
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

		// Token: 0x06000092 RID: 146 RVA: 0x0000633E File Offset: 0x0000453E
		public CvDisplayGraphicsDots(List<Point2f> points)
		{
			this.Points = points;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006365 File Offset: 0x00004565
		protected override void OnDisplayOriginChanged(Point2d oldPos, Point2d newPos)
		{
			this._Location = this.TranslateToMatPos(newPos);
			base.OnDisplayOriginChanged(oldPos, newPos);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006380 File Offset: 0x00004580
		public override bool IsMouseIn(PointF pos)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * 2.0, this._focusRange * 2.0).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000063F4 File Offset: 0x000045F4
		public bool IsMouseIn(PointF pos, int r)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * (double)r, this._focusRange * (double)r).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000645C File Offset: 0x0000465C
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

		// Token: 0x06000097 RID: 151 RVA: 0x000064E0 File Offset: 0x000046E0
		public override void OnPaint(PaintEventArgs e)
		{
			Size2d pixelSize = this.PixelSize;
			foreach (Point2f point in this.Points)
			{
				double dispX = base.MatDisplayOrigin.X + (double)point.X * pixelSize.Width;
				double dispY = base.MatDisplayOrigin.Y + (double)point.Y * pixelSize.Height;
				bool flag = new Rect2d(0.0, 0.0, (double)e.ClipRectangle.Width, (double)e.ClipRectangle.Height).Contains(dispX, dispY);
				if (flag)
				{
					base.DrawCross(e.Graphics, new PointF((float)dispX, (float)dispY), (base.IsFocused || base.Selected) ? base.FocusedColor : base.Color, 10f);
				}
			}
		}

		// Token: 0x04000059 RID: 89
		protected Point2d _Location;

		// Token: 0x0400005A RID: 90
		public bool IsMove = true;

		// Token: 0x0400005B RID: 91
		protected double _focusRange = 5.0;

		// Token: 0x0400005C RID: 92
		public List<Point2f> Points;
	}
}
