using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000015 RID: 21
	public class CvDisplayGraphicsLineSegment : CvDisplayGraphicsLine
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00006D8B File Offset: 0x00004F8B
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00006D93 File Offset: 0x00004F93
		public Point2d Start { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006D9C File Offset: 0x00004F9C
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public Point2d End { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public override Point2d Location
		{
			get
			{
				return new Point2d((this.Start.X + this.End.X) / 2.0, (this.Start.Y + this.End.Y) / 2.0);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006E0C File Offset: 0x0000500C
		public override double K
		{
			get
			{
				return (this.End.X == this.Start.X) ? double.NaN : ((this.End.Y - this.Start.Y) / (this.End.X - this.Start.X));
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00006E70 File Offset: 0x00005070
		public Point2d Mid
		{
			get
			{
				return new Point2d((this.Start.X + this.End.X) / 2.0, (this.Start.Y + this.End.Y) / 2.0);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00006ECC File Offset: 0x000050CC
		public double Length
		{
			get
			{
				return Caculate.Distance(this.Start, this.End);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00006EF0 File Offset: 0x000050F0
		public override double Angle
		{
			get
			{
				return base.Angle;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006F08 File Offset: 0x00005108
		public CvDisplayGraphicsLineSegment() : this(0.0, 0.0, 0.0, 0.0)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006F36 File Offset: 0x00005136
		public CvDisplayGraphicsLineSegment(Point2d pStart, Point2d pEnd)
		{
			this.Start = pStart;
			this.End = pEnd;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006F5E File Offset: 0x0000515E
		public CvDisplayGraphicsLineSegment(double x1, double y1, double x2, double y2) : this(new Point2d(x1, y1), new Point2d(x2, y2))
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006F78 File Offset: 0x00005178
		public override bool IsMouseIn(PointF pos)
		{
			bool isMove = this.IsMove;
			bool result;
			if (isMove)
			{
				result = false;
			}
			else
			{
				CvDisplayGraphicsDot startDot = new CvDisplayGraphicsDot(this.Start);
				startDot.ParentMat = this.ParentMat;
				CvDisplayGraphicsDot endDot = new CvDisplayGraphicsDot(this.End);
				endDot.ParentMat = this.ParentMat;
				Point2d rectPos = this.TranslateToScreenPos(this.Location);
				double length = Caculate.Distance(this.TranslateToScreenPos(this.Start), this.TranslateToScreenPos(this.End));
				RotatedRect rotateRect = new RotatedRect(new Point2f((float)rectPos.X, (float)rectPos.Y), new Size2f(length, 6.0), (float)this.Angle);
				this._SelectedType = CvDisplayGraphicsLineSegment.SelectedType.None;
				bool flag = startDot.IsMouseIn(pos);
				if (flag)
				{
					this._SelectedType = CvDisplayGraphicsLineSegment.SelectedType.Start;
				}
				else
				{
					bool flag2 = endDot.IsMouseIn(pos);
					if (flag2)
					{
						this._SelectedType = CvDisplayGraphicsLineSegment.SelectedType.End;
					}
					else
					{
						bool flag3 = Caculate.RotateRectContains(rotateRect, new Point2d((double)pos.X, (double)pos.Y));
						if (flag3)
						{
							this._SelectedType = CvDisplayGraphicsLineSegment.SelectedType.Line;
						}
					}
				}
				result = (this._SelectedType > CvDisplayGraphicsLineSegment.SelectedType.None);
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007094 File Offset: 0x00005294
		public override void OnMouseMove(MouseEventArgs e)
		{
			bool isMove = this.IsMove;
			if (!isMove)
			{
				bool isLeftMouseDown = this.IsLeftMouseDown;
				if (isLeftMouseDown)
				{
					double xmove = (double)(e.X - this.MouseDownPos.X);
					double ymove = (double)(e.Y - this.MouseDownPos.Y);
					bool flag = this._SelectedType == CvDisplayGraphicsLineSegment.SelectedType.Start || this._SelectedType == CvDisplayGraphicsLineSegment.SelectedType.Line;
					if (flag)
					{
						Point2d screenStart = this.TranslateToScreenPos(this.Start);
						screenStart.X += xmove;
						screenStart.Y += ymove;
						this.Start = this.TranslateToMatPos(screenStart);
						this.MouseDownPos = e.Location;
					}
					bool flag2 = this._SelectedType == CvDisplayGraphicsLineSegment.SelectedType.End || this._SelectedType == CvDisplayGraphicsLineSegment.SelectedType.Line;
					if (flag2)
					{
						Point2d screenEnd = this.TranslateToScreenPos(this.End);
						screenEnd.X += xmove;
						screenEnd.Y += ymove;
						this.End = this.TranslateToMatPos(screenEnd);
						this.MouseDownPos = e.Location;
					}
				}
				base.OnMouseMove(e);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000071B0 File Offset: 0x000053B0
		public override void OnPaint(PaintEventArgs e)
		{
			Point2d start = this.TranslateToScreenPos(this.Start);
			Point2d end = this.TranslateToScreenPos(this.End);
			Pen pen = new Pen(base.GetDrawColor(), base.GetDrawSize());
            Pen pen3 = new Pen(Color.Green, base.GetDrawSize());
            System.Drawing.Drawing2D.AdjustableArrowCap LineCap = new System.Drawing.Drawing2D.AdjustableArrowCap(base.GetDrawSize() * 2f, base.GetDrawSize() * 2f, true);
            pen.CustomEndCap = LineCap;
            pen3.CustomEndCap = LineCap;
            if (IsFocusedOrSelected)
            {
                e.Graphics.DrawLine(pen3, new PointF((float)start.X, (float)start.Y), new PointF((float)end.X, (float)end.Y));
            }
            else
            {
                e.Graphics.DrawLine(pen, new PointF((float)start.X, (float)start.Y), new PointF((float)end.X, (float)end.Y));
            }

            
			bool flag = this._SelectedType > CvDisplayGraphicsLineSegment.SelectedType.None;
			if (flag)
			{
				SolidBrush brush = new SolidBrush(base.GetDrawColor());
                SolidBrush brush2 = new SolidBrush(Color.Green);
                float radius = 4f;
                if (IsFocusedOrSelected)
                {
                    e.Graphics.FillRectangle(brush2, new RectangleF(new PointF((float)start.X - radius, (float)start.Y - radius), new SizeF(radius * 2f, radius * 2f)));
                    e.Graphics.FillRectangle(brush2, new RectangleF(new PointF((float)end.X - radius, (float)end.Y - radius), new SizeF(radius * 2f, radius * 2f)));
                }
                else
                {
                    e.Graphics.FillRectangle(brush, new RectangleF(new PointF((float)start.X - radius, (float)start.Y - radius), new SizeF(radius * 2f, radius * 2f)));
                    e.Graphics.FillRectangle(brush, new RectangleF(new PointF((float)end.X - radius, (float)end.Y - radius), new SizeF(radius * 2f, radius * 2f)));
                }
               
			}
		}

		// Token: 0x04000064 RID: 100
		public new bool IsMove = false;

		// Token: 0x04000065 RID: 101
		protected CvDisplayGraphicsLineSegment.SelectedType _SelectedType = CvDisplayGraphicsLineSegment.SelectedType.None;

		// Token: 0x02000023 RID: 35
		protected enum SelectedType
		{
			// Token: 0x040000A1 RID: 161
			None,
			// Token: 0x040000A2 RID: 162
			Start,
			// Token: 0x040000A3 RID: 163
			End,
			// Token: 0x040000A4 RID: 164
			Line
		}
	}
}
