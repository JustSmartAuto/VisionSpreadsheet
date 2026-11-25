using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000011 RID: 17
	public class CvDisplayGraphicsLines : CvDisplayGraphicsShape
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00005E18 File Offset: 0x00004018
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00005E30 File Offset: 0x00004030
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005E58 File Offset: 0x00004058
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00005E75 File Offset: 0x00004075
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005E8C File Offset: 0x0000408C
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00005EA9 File Offset: 0x000040A9
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005EC0 File Offset: 0x000040C0
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00005EDE File Offset: 0x000040DE
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

		// Token: 0x06000082 RID: 130 RVA: 0x00005EEC File Offset: 0x000040EC
		public CvDisplayGraphicsLines(List<Point2f> points, List<float> angles)
		{
			this.IsMove = false;
			this.Points = points;
			this.Angles = angles;
			base.Color = Color.Green;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005F40 File Offset: 0x00004140
		public CvDisplayGraphicsLines(List<Point2f> points, List<float> angles, int Step)
		{
			this.LineStep = Step;
			this.IsMove = false;
			this.Points = points;
			this.Angles = angles;
			base.Color = Color.Green;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005F9A File Offset: 0x0000419A
		protected override void OnDisplayOriginChanged(Point2d oldPos, Point2d newPos)
		{
			this._Location = this.TranslateToMatPos(newPos);
			base.OnDisplayOriginChanged(oldPos, newPos);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005FB4 File Offset: 0x000041B4
		public override bool IsMouseIn(PointF pos)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * 2.0, this._focusRange * 2.0).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006028 File Offset: 0x00004228
		public bool IsMouseIn(PointF pos, int r)
		{
			return new Rect2d(this.DisplayOrigin.X - this._focusRange, this.DisplayOrigin.Y - this._focusRange, this._focusRange * (double)r, this._focusRange * (double)r).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006090 File Offset: 0x00004290
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

		// Token: 0x06000088 RID: 136 RVA: 0x00006114 File Offset: 0x00004314
		public Point2d GetEndPointByTrigonometeic(double angle, Point2f startPoint, double distance)
		{
			double radian = angle * 3.141592653589793 / 180.0;
			return new Point2d((double)Convert.ToSingle((double)startPoint.X + distance * Math.Cos(radian)), (double)Convert.ToSingle((double)startPoint.Y + distance * Math.Sin(radian)));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006170 File Offset: 0x00004370
		public override void OnPaint(PaintEventArgs e)
		{
			Size2d pixelSize = this.PixelSize;
			int i = 0;
			Pen pen = new Pen(base.GetDrawColor(), base.GetDrawSize() + 2f);
			foreach (Point2f point in this.Points)
			{
				Point2d pStart = this.GetEndPointByTrigonometeic((double)(this.Angles[i] + 90f), point, (double)this.LineStep);
				Point2d pEnd = this.GetEndPointByTrigonometeic((double)(this.Angles[i] + 270f), point, (double)this.LineStep);
				pStart = this.TranslateToScreenPos(pStart);
				pEnd = this.TranslateToScreenPos(pEnd);
				e.Graphics.DrawLine(pen, (float)pStart.X, (float)pStart.Y, (float)pEnd.X, (float)pEnd.Y);
				i++;
			}
		}

		// Token: 0x04000053 RID: 83
		protected Point2d _Location;

		// Token: 0x04000054 RID: 84
		public bool IsMove = true;

		// Token: 0x04000055 RID: 85
		protected double _focusRange = 5.0;

		// Token: 0x04000056 RID: 86
		public List<Point2f> Points;

		// Token: 0x04000057 RID: 87
		public List<float> Angles;

		// Token: 0x04000058 RID: 88
		public int LineStep = 1;
	}
}
