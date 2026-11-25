using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000007 RID: 7
	public static class Caculate
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00004660 File Offset: 0x00002860
		public static Point2d GetCirclePoint(Point2d Circle, double CA, double Radius)
		{
			return new Point2d(Circle.X + Radius * Math.Cos(CA * 3.141592653589793 / 180.0), Circle.Y + Radius * Math.Sin(CA * 3.141592653589793 / 180.0));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000046C0 File Offset: 0x000028C0
		public static double CalculateAngle(double x, double y)
		{
			return Math.Atan2(y, x) / (2.0 * Math.Acos(-1.0)) * 360.0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004700 File Offset: 0x00002900
		public static bool FitCircle(List<PointF> RestPoint, out double[] Reast)
		{
			int N = RestPoint.Count;
			bool flag = N < 3;
			bool result;
			if (flag)
			{
				Reast = new double[3];
				result = false;
			}
			else
			{
				double sumx = 0.0;
				double sumy = 0.0;
				double sumx2 = 0.0;
				double sumy2 = 0.0;
				double sumx3 = 0.0;
				double sumy3 = 0.0;
				double sumxy = 0.0;
				double sumx1y2 = 0.0;
				double sumx2y = 0.0;
				for (int i = 0; i < N; i++)
				{
					double x = (double)RestPoint[i].X;
					double y = (double)RestPoint[i].Y;
					double x2 = x * x;
					double y2 = y * y;
					sumx += x;
					sumy += y;
					sumx2 += x2;
					sumy2 += y2;
					sumx3 += x2 * x;
					sumy3 += y2 * y;
					sumxy += x * y;
					sumx1y2 += x * y2;
					sumx2y += x2 * y;
				}
				double C = (double)N * sumx2 - sumx * sumx;
				double D = (double)N * sumxy - sumx * sumy;
				double E = (double)N * sumx3 + (double)N * sumx1y2 - (sumx2 + sumy2) * sumx;
				double G = (double)N * sumy2 - sumy * sumy;
				double H = (double)N * sumx2y + (double)N * sumy3 - (sumx2 + sumy2) * sumy;
				double a = (H * D - E * G) / (C * G - D * D);
				double b = (H * C - E * D) / (D * D - G * C);
				double c = -(a * sumx + b * sumy + sumx2 + sumy2) / (double)N;
				Reast = new double[]
				{
					a / -2.0,
					b / -2.0,
					Math.Sqrt(a * a + b * b - 4.0 * c) / 2.0
				};
				result = true;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004914 File Offset: 0x00002B14
		public static PointF GetEndPointByTrigonometeic(double angle, PointF startPoint, double distance)
		{
			double radian = angle * 3.141592653589793 / 180.0;
			return new PointF(Convert.ToSingle((double)startPoint.X + distance * Math.Cos(radian)), Convert.ToSingle((double)startPoint.Y + distance * Math.Sin(radian)));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004970 File Offset: 0x00002B70
		public static Point2f GetEndPoint2fByTrigonometeic(double angle, PointF startPoint, double distance)
		{
			double radian = angle * 3.141592653589793 / 180.0;
			return new Point2f(Convert.ToSingle((double)startPoint.X + distance * Math.Cos(radian)), Convert.ToSingle((double)startPoint.Y + distance * Math.Sin(radian)));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000049CC File Offset: 0x00002BCC
		public static Point2d GetEndPoint2fByTrigonometeic(double angle, Point2d startPoint, double distance)
		{
			double radian = angle * 3.141592653589793 / 180.0;
			return new Point2d(startPoint.X + distance * Math.Cos(radian), startPoint.Y + distance * Math.Sin(radian));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004A18 File Offset: 0x00002C18
		public static double Angle(Point2d cen, Point2d first, Point2d second)
		{
			double ma_x = first.X - cen.X;
			double ma_y = first.Y - cen.Y;
			double mb_x = second.X - cen.X;
			double mb_y = second.Y - cen.Y;
			double v = ma_x * mb_x + ma_y * mb_y;
			double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
			double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
			double cosM = v / (ma_val * mb_val);
			return Math.Acos(cosM);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004A9C File Offset: 0x00002C9C
		public static Point2f PointToRotatPoint(double X, double Y, Mat Rotat)
		{
			return new Point2f
			{
				X = (float)((double)Rotat.Get<float>(0, 0) * X + (double)Rotat.Get<float>(0, 1) * Y + (double)Rotat.Get<float>(0, 2)),
				Y = (float)((double)Rotat.Get<float>(1, 0) * X + (double)Rotat.Get<float>(1, 1) * Y + (double)Rotat.Get<float>(1, 2))
			};
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004B0C File Offset: 0x00002D0C
		public static bool DotIsMouseIn(PointF pos, Point2d now, int _focusRange = 5)
		{
			return new Rect2d(now.X - (double)_focusRange, now.Y - (double)_focusRange, (double)(_focusRange * 2), (double)(_focusRange * 2)).Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004B58 File Offset: 0x00002D58
		public static double Distance(Point2d p1, Point2d p2)
		{
			return Caculate.Hypotenuse(p2.X - p1.X, p2.Y - p1.Y);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static double Hypotenuse(double edg1, double edg2)
		{
			return Math.Sqrt(Math.Pow(edg1, 2.0) + Math.Pow(edg2, 2.0));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public static bool RotateRectContains(RotatedRect rRect, Point2d p)
		{
			Point2f[] points = rRect.Points();
			double ab = new CvDisplayGraphicsLineSegment((double)points[0].X, (double)points[0].Y, (double)points[1].X, (double)points[1].Y).Distance(p);
			double bc = new CvDisplayGraphicsLineSegment((double)points[2].X, (double)points[2].Y, (double)points[1].X, (double)points[1].Y).Distance(p);
			double cd = new CvDisplayGraphicsLineSegment((double)points[2].X, (double)points[2].Y, (double)points[3].X, (double)points[3].Y).Distance(p);
			double ad = new CvDisplayGraphicsLineSegment((double)points[3].X, (double)points[3].Y, (double)points[0].X, (double)points[0].Y).Distance(p);
			return (float)((int)ab + (int)cd) <= rRect.Size.Width && (float)((int)bc + (int)ad) <= rRect.Size.Height;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D10 File Offset: 0x00002F10
		public static double Angle(double k1, double k2)
		{
			bool flag = double.IsNaN(k1);
			double result;
			if (flag)
			{
				result = 90.0 - k2;
			}
			else
			{
				bool flag2 = double.IsNaN(k2);
				if (flag2)
				{
					result = 90.0 - k1;
				}
				else
				{
					double tan_k = (k2 - k1) / (1.0 + k2 * k1);
					double lines_arctan = Math.Atan(tan_k) * 180.0 / 3.1415926;
					result = lines_arctan;
				}
			}
			return result;
		}
	}
}
