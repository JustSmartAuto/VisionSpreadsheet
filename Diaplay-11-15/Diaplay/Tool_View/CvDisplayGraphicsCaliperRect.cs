using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x02000019 RID: 25
	public class CvDisplayGraphicsCaliperRect : CvDisplayGraphicsDot
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00007C2C File Offset: 0x00005E2C
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00007C44 File Offset: 0x00005E44
		public override CvDisplayGraphicsMat ParentMat
		{
			get
			{
				return base.ParentMat;
			}
			set
			{
				base.ParentMat = value;
				bool flag = this.AB != null;
				if (flag)
				{
					this.AB.ParentMat = value;
				}
				bool flag2 = this.BC != null;
				if (flag2)
				{
					this.BC.ParentMat = value;
				}
				bool flag3 = this.CD != null;
				if (flag3)
				{
					this.CD.ParentMat = value;
				}
				bool flag4 = this.AD != null;
				if (flag4)
				{
					this.AD.ParentMat = value;
				}
				bool flag5 = this.RotatLine != null;
				if (flag5)
				{
					this.AD.ParentMat = value;
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public CvDisplayGraphicsCaliperRect(Point2d center, Size2d size, double angle)
		{
			this.Location = center;
			this.RectSize = size;
			this.Angle = angle;
			this.AB = new CvDisplayGraphicsLineSegment();
			this.BC = new CvDisplayGraphicsLineSegment();
			this.CD = new CvDisplayGraphicsLineSegment();
			this.AD = new CvDisplayGraphicsLineSegment();
			this._focusRange = 10.0;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007D5C File Offset: 0x00005F5C
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007D8A File Offset: 0x00005F8A
		public double Angle
		{
			get
			{
				this._Angle %= 360.0;
				return this._Angle;
			}
			set
			{
				this._Angle = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007D94 File Offset: 0x00005F94
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00007DB1 File Offset: 0x00005FB1
		public double Width
		{
			get
			{
				return this.RectSize.Width;
			}
			set
			{
				this.RectSize.Width = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007DC0 File Offset: 0x00005FC0
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00007DDD File Offset: 0x00005FDD
		public double Height
		{
			get
			{
				return this.RectSize.Height;
			}
			set
			{
				this.RectSize.Height = value;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007DEC File Offset: 0x00005FEC
		public override bool IsMouseIn(PointF pos)
		{
			bool bCenter = base.IsMouseIn(pos);
			this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.None;
			bool flag = bCenter;
			if (flag)
			{
				this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.Center;
			}
			else
			{
				Point2d[] points = this.GetPoints(false);
				this.AB.Start = points[0];
				this.AB.End = points[1];
				this.BC.Start = points[1];
				this.BC.End = points[2];
				this.CD.Start = points[2];
				this.CD.End = points[3];
				this.AD.Start = points[3];
				this.AD.End = points[0];
				bool flag2 = new CvDisplayGraphicsDot(this.CD.Mid)
				{
					ParentMat = this.ParentMat
				}.IsMouseIn(pos);
				if (flag2)
				{
					this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.Rotate;
				}
				else
				{
					bool flag3 = new CvDisplayGraphicsDot(this.AB.Start)
					{
						ParentMat = this.ParentMat
					}.IsMouseIn(pos);
					if (flag3)
					{
						this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.AB;
					}
					else
					{
						bool flag4 = new CvDisplayGraphicsDot(this.BC.Start)
						{
							ParentMat = this.ParentMat
						}.IsMouseIn(pos);
						if (flag4)
						{
							this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.BC;
						}
						else
						{
							bool flag5 = new CvDisplayGraphicsDot(this.CD.Start)
							{
								ParentMat = this.ParentMat
							}.IsMouseIn(pos);
							if (flag5)
							{
								this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.CD;
							}
							else
							{
								bool flag6 = new CvDisplayGraphicsDot(this.AD.Start)
								{
									ParentMat = this.ParentMat
								}.IsMouseIn(pos);
								if (flag6)
								{
									this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.DA;
								}
								else
								{
									bool flag7 = this.AB.IsMouseIn(pos) || this.CD.IsMouseIn(pos);
									if (flag7)
									{
										this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.WidthEdge;
									}
									else
									{
										bool flag8 = this.BC.IsMouseIn(pos) || this.AD.IsMouseIn(pos);
										if (flag8)
										{
											this._SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.HeightEdge;
										}
									}
								}
							}
						}
					}
				}
			}
			return this._SelectedType > CvDisplayGraphicsCaliperRect.SelectedType.None;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008038 File Offset: 0x00006238
		public void UpDataABCD()
		{
			Point2d[] points = this.GetPoints(false);
			this.AB.Start = points[0];
			this.AB.End = points[1];
			this.BC.Start = points[1];
			this.BC.End = points[2];
			this.CD.Start = points[2];
			this.CD.End = points[3];
			this.AD.Start = points[3];
			this.AD.End = points[0];
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000080E8 File Offset: 0x000062E8
		public override void OnMouseMove(MouseEventArgs e)
		{
			bool isMove = this.IsMove;
			if (!isMove)
			{
				bool isLeftMouseDown = this.IsLeftMouseDown;
				if (isLeftMouseDown)
				{
					double xmove = (double)(e.Location.X - this.MouseDownPos.X);
					double ymove = (double)(e.Location.Y - this.MouseDownPos.Y);
					Point2d RoPoint = new Point2d(xmove / this.PixelSize.Width, ymove / this.PixelSize.Width);
					bool flag = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.BC;
					if (flag)
					{
						Point2d First = this.BC.Start + RoPoint;
						Point2d Cen = this.AD.Start;
						Point2d Second = this.CD.Start;
						double angle = Caculate.Angle(Cen, First, Second);
						CvDisplayGraphicsLineSegment LineSeg = new CvDisplayGraphicsLineSegment(First, Cen);
						Point2d CenR = LineSeg.Mid;
						double Dis = LineSeg.Length;
						double a = Math.Sin(angle) * Dis;
						double b = Math.Cos(angle) * Dis;
						this.MouseDownPos = e.Location;
						bool flag2 = a < 10.0 || b < 10.0;
						if (!flag2)
						{
							this.Width = a;
							this.Height = b;
							base.X = CenR.X;
							base.Y = CenR.Y;
							this.UpDataABCD();
						}
					}
					else
					{
						bool flag3 = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.CD;
						if (flag3)
						{
							Point2d First2 = this.CD.Start + RoPoint;
							Point2d Cen2 = this.AB.Start;
							Point2d Second2 = this.BC.Start;
							double angle2 = Caculate.Angle(Cen2, First2, Second2);
							CvDisplayGraphicsLineSegment LineSeg2 = new CvDisplayGraphicsLineSegment(First2, Cen2);
							Point2d CenR2 = LineSeg2.Mid;
							double Dis2 = LineSeg2.Length;
							double a2 = Math.Sin(angle2) * Dis2;
							double b2 = Math.Cos(angle2) * Dis2;
							this.MouseDownPos = e.Location;
							bool flag4 = a2 < 10.0 || b2 < 10.0;
							if (!flag4)
							{
								this.Width = a2;
								this.Height = b2;
								base.X = CenR2.X;
								base.Y = CenR2.Y;
								this.UpDataABCD();
							}
						}
						else
						{
							bool flag5 = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.DA;
							if (flag5)
							{
								Point2d First3 = this.AD.Start + RoPoint;
								Point2d Cen3 = this.BC.Start;
								Point2d Second3 = this.AB.Start;
								double angle3 = Caculate.Angle(Cen3, First3, Second3);
								CvDisplayGraphicsLineSegment LineSeg3 = new CvDisplayGraphicsLineSegment(First3, Cen3);
								Point2d CenR3 = LineSeg3.Mid;
								double Dis3 = LineSeg3.Length;
								double a3 = Math.Sin(angle3) * Dis3;
								double b3 = Math.Cos(angle3) * Dis3;
								this.MouseDownPos = e.Location;
								bool flag6 = a3 < 10.0 || b3 < 10.0;
								if (!flag6)
								{
									this.Width = a3;
									this.Height = b3;
									base.X = CenR3.X;
									base.Y = CenR3.Y;
									this.UpDataABCD();
								}
							}
							else
							{
								bool flag7 = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.AB;
								if (flag7)
								{
									Point2d First4 = this.AB.Start + RoPoint;
									Point2d Cen4 = this.CD.Start;
									Point2d Second4 = this.AD.Start;
									double angle4 = Caculate.Angle(Cen4, First4, Second4);
									CvDisplayGraphicsLineSegment LineSeg4 = new CvDisplayGraphicsLineSegment(First4, Cen4);
									Point2d CenR4 = LineSeg4.Mid;
									double Dis4 = LineSeg4.Length;
									double a4 = Math.Sin(angle4) * Dis4;
									double b4 = Math.Cos(angle4) * Dis4;
									this.MouseDownPos = e.Location;
									bool flag8 = a4 < 10.0 || b4 < 10.0;
									if (!flag8)
									{
										this.Width = a4;
										this.Height = b4;
										base.X = CenR4.X;
										base.Y = CenR4.Y;
										this.UpDataABCD();
									}
								}
								else
								{
									bool flag9 = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.Rotate;
									if (flag9)
									{
										Point2d center = this.TranslateToScreenPos(this.Location);
										CvDisplayGraphicsLineSegment line = new CvDisplayGraphicsLineSegment(center, new Point2d((double)this.MouseDownPos.X, (double)this.MouseDownPos.Y));
										CvDisplayGraphicsLineSegment line2 = new CvDisplayGraphicsLineSegment(center, new Point2d((double)e.X, (double)e.Y));
										this.Angle += Caculate.Angle(line.K, line2.K);
										this.MouseDownPos = e.Location;
										this.UpDataABCD();
									}
									else
									{
										bool flag10 = this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.Center || this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.WidthEdge || this._SelectedType == CvDisplayGraphicsCaliperRect.SelectedType.HeightEdge;
										if (flag10)
										{
											base.OnMouseMove(e);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008600 File Offset: 0x00006800
		public Point2d[] GetPoints(bool isSreen)
		{
			List<Point2d> ps = new List<Point2d>();
			foreach (Point2f p in new RotatedRect(new Point2f((float)base.X, (float)base.Y), new Size2f((float)this.Width, (float)this.Height), (float)this.Angle).Points())
			{
				Point2d sp = isSreen ? this.TranslateToScreenPos(new Point2d((double)p.X, (double)p.Y)) : new Point2d((double)p.X, (double)p.Y);
				ps.Add(sp);
			}
			return ps.ToArray();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000086B8 File Offset: 0x000068B8
		public override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			List<PointF> ps = new List<PointF>();
			foreach (Point2f p in new RotatedRect(new Point2f((float)base.X, (float)base.Y), new Size2f((float)this.Width, (float)this.Height), (float)this.Angle).Points())
			{
				Point2d sp = this.TranslateToScreenPos(new Point2d((double)p.X, (double)p.Y));
				ps.Add(new PointF((float)sp.X, (float)sp.Y));
			}
			Pen pen = new Pen(base.GetDrawColor(), base.GetDrawSize() + 2f);
			e.Graphics.DrawPolygon(pen, ps.ToArray());
			Pen pen2 = new Pen(Color.Red, base.GetDrawSize());
			AdjustableArrowCap LineCap = new AdjustableArrowCap(base.GetDrawSize() * 2f, base.GetDrawSize() * 2f, true);
			pen2.CustomEndCap = LineCap;
			e.Graphics.DrawLine(pen2, ps[1].X, ps[1].Y, ps[2].X, ps[2].Y);//画到中间位置，不要画边
			bool flag = this._SelectedType != CvDisplayGraphicsCaliperRect.SelectedType.None && !this.IsMove;
			if (flag)
			{
				int r = 6;
				for (int i = 0; i < ps.Count; i++)
				{
					base.DrawCircle(e.Graphics, new Pen(Color.Yellow, 3f), ps[i], (float)r);
				}
				base.DrawCircle(e.Graphics, new Pen(Color.Green, 3f), new PointF((ps[3].X + ps[2].X) / 2f, (ps[3].Y + ps[2].Y) / 2f), (float)r);
			}
			//bool flag2 = this.UpDataRoi != null;
			//if (flag2)
			//{
			//	this.UpDataRoi();
			//}
		}

		// Token: 0x04000073 RID: 115
		private CvDisplayGraphicsLineSegment AB;

		// Token: 0x04000074 RID: 116
		private CvDisplayGraphicsLineSegment BC;

		// Token: 0x04000075 RID: 117
		private CvDisplayGraphicsLineSegment CD;

		// Token: 0x04000076 RID: 118
		private CvDisplayGraphicsLineSegment AD;

		// Token: 0x04000077 RID: 119
		private CvDisplayGraphicsLineSegment RotatLine;

		// Token: 0x04000078 RID: 120
		public new D_UpData UpDataRoi = null;

		// Token: 0x04000079 RID: 121
		private double _Angle;

		// Token: 0x0400007A RID: 122
		public Size2d RectSize;

		// Token: 0x0400007B RID: 123
		protected CvDisplayGraphicsCaliperRect.SelectedType _SelectedType = CvDisplayGraphicsCaliperRect.SelectedType.None;

		// Token: 0x0400007C RID: 124
		public bool IsShowReast = false;

		// Token: 0x02000024 RID: 36
		protected enum SelectedType
		{
			// Token: 0x040000A6 RID: 166
			None,
			// Token: 0x040000A7 RID: 167
			Center,
			// Token: 0x040000A8 RID: 168
			WidthEdge,
			// Token: 0x040000A9 RID: 169
			HeightEdge,
			// Token: 0x040000AA RID: 170
			Rotate,
			// Token: 0x040000AB RID: 171
			AB,
			// Token: 0x040000AC RID: 172
			BC,
			// Token: 0x040000AD RID: 173
			CD,
			// Token: 0x040000AE RID: 174
			DA
		}
	}
}
