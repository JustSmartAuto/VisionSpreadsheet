using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000014 RID: 20
	public class CvDisplayGraphicsLine : CvDisplayGraphicsDot
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00006A11 File Offset: 0x00004C11
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00006A19 File Offset: 0x00004C19
		public virtual double K { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00006A24 File Offset: 0x00004C24
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00006A70 File Offset: 0x00004C70
		public virtual double Angle
		{
			get
			{
				return double.IsNaN(this.K) ? 90.0 : (Math.Atan(this.K) * 180.0 / 3.141592653589793);
			}
			set
			{
				bool flag = Math.Abs(value) == 90.0;
				if (flag)
				{
					this.K = double.NaN;
				}
				else
				{
					this.K = Math.Tan(value);
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public double B
		{
			get
			{
				return base.Y - this.K * base.X;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006ADA File Offset: 0x00004CDA
		public CvDisplayGraphicsLine()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public CvDisplayGraphicsLine(Point2d p, double k) : base(p.X, p.Y)
		{
			this.K = k;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006B04 File Offset: 0x00004D04
		public double Distance(Point2d p)
		{
			bool flag = double.IsNaN(this.K);
			double result;
			if (flag)
			{
				result = Math.Abs(p.X - this.Location.X);
			}
			else
			{
				result = Math.Abs(this.K * p.X - p.Y + this.B) / Math.Sqrt(this.K * this.K + 1.0);
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006B7C File Offset: 0x00004D7C
		public override void OnPaint(PaintEventArgs e)
		{
			Point2d screenPos = new Point2d(base.X, base.Y);
			bool flag = Math.Abs(this.K) < 0.0001;
			Point2d pStart;
			Point2d pEnd;
			if (flag)
			{
				pStart = new Point2d(0.0, screenPos.Y);
				pEnd = new Point2d((double)e.ClipRectangle.Width, screenPos.Y);
			}
			else
			{
				bool flag2 = this.K > 0.0;
				if (flag2)
				{
					Point2d p = this.CaculateLocationByY(0.0);
					Point2d p2 = this.CaculateLocationByY((double)this.ParentMat.Image.Height);
					pStart = this.TranslateToScreenPos(p);
					pEnd = this.TranslateToScreenPos(p2);
				}
				else
				{
					bool flag3 = this.K < 0.0 && this.K > -1000.0;
					if (flag3)
					{
						Point2d p3 = this.CaculateLocationByX(0.0);
						Point2d p4 = this.CaculateLocationByX((double)this.ParentMat.Image.Width);
						pStart = this.TranslateToScreenPos(p3);
						pEnd = this.TranslateToScreenPos(p4);
					}
					else
					{
						pStart = new Point2d(screenPos.X, 0.0);
						pEnd = new Point2d(screenPos.X, (double)e.ClipRectangle.Height);
					}
				}
			}
			Pen pen = new Pen(base.GetDrawColor(), base.GetDrawSize());
			e.Graphics.DrawLine(pen, (float)pStart.X, (float)pStart.Y, (float)pEnd.X, (float)pEnd.Y);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006D28 File Offset: 0x00004F28
		public Point2d CaculateLocationByX(double x)
		{
			return new Point2d(x, this.K * x + this.B);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006D50 File Offset: 0x00004F50
		public Point2d CaculateLocationByY(double y)
		{
			return new Point2d((this.K == 0.0) ? y : ((y - this.B) / this.K), y);
		}
	}
}
