using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x0200000F RID: 15
	public class CvDisplayGraphicsCalipersLine : CvDisplayGraphicsLineSegment
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000057AD File Offset: 0x000039AD
		public CvDisplayGraphicsCalipersLine(Point2d pStart, Point2d pEnd)
		{
			base.Start = pStart;
			base.End = pEnd;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000057E8 File Offset: 0x000039E8
		public override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			RotatedRect ROI = new RotatedRect(new Point2f((float)base.X, (float)base.Y), new Size2f((float)this.W, (float)this.H), (float)this.Angle);
			ROI.Angle = Convert.ToSingle(Math.Atan2(base.End.Y - base.Start.Y, base.End.X - base.Start.X) * 180.0 / 3.141592653589793);
			float IntervalW = (float)((base.End.X - base.Start.X) / ((double)this.Number - 1.0));
			float IntervalH = (float)((base.End.Y - base.Start.Y) / ((double)this.Number - 1.0));
			Pen pen = new Pen(base.GetDrawColor(), base.GetDrawSize());
			for (int num = 0; num < this.Number; num++)
			{
				ROI.Center = new Point2f((float)(base.Start.X + (double)(IntervalW * (float)num)), (float)(base.Start.Y + (double)(IntervalH * (float)num)));
				List<PointF> ps = new List<PointF>();
				foreach (Point2f p in ROI.Points())
				{
					Point2d sp = this.TranslateToScreenPos(new Point2d((double)p.X, (double)p.Y));
					ps.Add(new PointF((float)sp.X, (float)sp.Y));
				}
				e.Graphics.DrawPolygon(pen, ps.ToArray());
			}
			Pen pen2 = new Pen(Color.Red, base.GetDrawSize());
			AdjustableArrowCap LineCap = new AdjustableArrowCap(base.GetDrawSize() * 2f, base.GetDrawSize() * 2f, true);
			pen2.CustomEndCap = LineCap;
			double DirectionAngle = (double)(ROI.Angle + 90f);
			bool flag = this.Direction == enumCaliperDirection.从外向内;
			if (flag)
			{
				DirectionAngle = (double)(ROI.Angle - 90f);
			}
			PointF Arrowed = new PointF((float)((base.Start.X + base.End.X) / 2.0), (float)((base.Start.Y + base.End.Y) / 2.0));
			PointF Trigonometeic = Caculate.GetEndPointByTrigonometeic(DirectionAngle, Arrowed, (double)this.H);
			Point2d Arrowed2 = this.TranslateToScreenPos(new Point2d((double)Arrowed.X, (double)Arrowed.Y));
			Point2d Trigonometeic2 = this.TranslateToScreenPos(new Point2d((double)Trigonometeic.X, (double)Trigonometeic.Y));
			e.Graphics.DrawLine(pen2, (float)Arrowed2.X, (float)Arrowed2.Y, (float)Trigonometeic2.X, (float)Trigonometeic2.Y);
			GC.Collect();
		}

		// Token: 0x04000049 RID: 73
		public int Number = 10;

		// Token: 0x0400004A RID: 74
		public int H = 100;

		// Token: 0x0400004B RID: 75
		public int W = 10;

		// Token: 0x0400004C RID: 76
		public enumCaliperDirection Direction = enumCaliperDirection.从内向外;

		// Token: 0x0400004D RID: 77
		public new D_UpData UpDataRoi;
	}
}
