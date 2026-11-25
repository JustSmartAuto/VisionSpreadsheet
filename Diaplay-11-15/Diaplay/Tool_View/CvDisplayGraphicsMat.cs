using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Tool_View
{
	// Token: 0x02000016 RID: 22
	public class CvDisplayGraphicsMat : CvDisplayGraphicsObject
	{
		public Mat Image
		{
			get
			{
				return this._Image;
			}
			set
			{
				bool flag = this._Image != null;
				if (flag)
				{
					this._Image.Dispose();
					this._Image = null;
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this._Image = new Mat(value, new Rect(0, 0, value.Width, value.Height));
				}
				bool isReast = this.IsReast;
				if (isReast)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00007348 File Offset: 0x00005548
		public Rect2d DispRect
		{
			get
			{
				return new Rect2d(this.DisplayOrigin, this._displaySize);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000736B File Offset: 0x0000556B
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00007373 File Offset: 0x00005573
		public Rect ShowMatRect { get; protected set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000737C File Offset: 0x0000557C
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00007394 File Offset: 0x00005594
		public override Size2d PixelSize
		{
			get
			{
				return this._pixelSize;
			}
			set
			{
				this._pixelSize = value;
				bool flag = this.Image == null;
				if (flag)
				{
					this._displaySize = new Size2d(0f, 0f);
				}
				else
				{
					this._displaySize = new Size2d((double)this.Image.Width * this._pixelSize.Width, (double)this.Image.Height * this._pixelSize.Height);
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007408 File Offset: 0x00005608
		public Size2d DisplaySize
		{
			get
			{
				return this._displaySize;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007420 File Offset: 0x00005620
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00007428 File Offset: 0x00005628
		public CvDisplayGraphicsShapeCollection GraphicsShapes { get; protected set; }

		// Token: 0x060000CD RID: 205 RVA: 0x00007431 File Offset: 0x00005631
		public CvDisplayGraphicsMat()
		{
			this.PixelSize = new Size2d(1f, 1f);
			this.GraphicsShapes = new CvDisplayGraphicsShapeCollection(this);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000746C File Offset: 0x0000566C
		public CvDisplayGraphicsMat(bool Is)
		{
			this.PixelSize = new Size2d(1f, 1f);
			this.GraphicsShapes = new CvDisplayGraphicsShapeCollection(this);
			this.IsReast = Is;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000074B9 File Offset: 0x000056B9
		public override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000074C4 File Offset: 0x000056C4
		public override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000074CF File Offset: 0x000056CF
		public override void Reset()
		{
			base.Reset();
			this.PixelSize = new Size2d(1f, 1f);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000074F0 File Offset: 0x000056F0
		public override void Dispose()
		{
			bool flag = this._Image != null;
			if (flag)
			{
				this._Image.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007520 File Offset: 0x00005720
		public override void OnPaint(PaintEventArgs e)
		{
			bool flag = this.Image != null;
			if (flag)
			{
				try
				{
					Rect showMatRect = default(Rect);
					PointF drawImageStartPos = default(PointF);
					bool flag2 = this.DispRect.X < 0.0;
					if (flag2)
					{
						showMatRect.X = (int)(Math.Abs(this.DispRect.X) / this.PixelSize.Width);
						drawImageStartPos.X = (float)((double)showMatRect.X * this.PixelSize.Width + this.DispRect.X);
					}
					else
					{
						showMatRect.X = 0;
						drawImageStartPos.X = (float)this.DispRect.X;
					}
					showMatRect.Width = (int)((double)((float)e.ClipRectangle.Width - drawImageStartPos.X) / this.PixelSize.Width) + 1;
					bool flag3 = this.DispRect.Y < 0.0;
					if (flag3)
					{
						showMatRect.Y = (int)(Math.Abs(this.DispRect.Y) / this.PixelSize.Height);
						drawImageStartPos.Y = (float)((double)showMatRect.Y * this.PixelSize.Height + this.DispRect.Y);
					}
					else
					{
						showMatRect.Y = 0;
						drawImageStartPos.Y = (float)this.DispRect.Y;
					}
					showMatRect.Height = (int)((double)((float)e.ClipRectangle.Height - drawImageStartPos.Y) / this.PixelSize.Height) + 1;
					this.AdjustMatRect(this.Image, ref showMatRect);
					using (Mat displayMat = new Mat(this.Image, showMatRect))
					{
						OpenCvSharp.Size drawSize = new OpenCvSharp.Size((int)((double)displayMat.Width * this.PixelSize.Width), (int)((double)displayMat.Height * this.PixelSize.Height));
						bool flag4 = drawSize.Width < 1;
						if (flag4)
						{
							drawSize.Width = 1;
						}
						bool flag5 = drawSize.Height < 1;
						if (flag5)
						{
							drawSize.Height = 1;
						}
						Mat resizeMat = new Mat();
						Cv2.Resize(displayMat, resizeMat, drawSize, 0.0, 0.0, InterpolationFlags.Nearest);
						Image drawImage = resizeMat.ToBitmap();
						e.Graphics.DrawImage(drawImage, drawImageStartPos);
					}
					this.ShowMatRect = showMatRect;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
			{
				if(shape.Visble)
                {
					shape.OnPaint(e);
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007840 File Offset: 0x00005A40
		public override bool IsMouseIn(PointF pos)
		{
			return this.DispRect.Contains((double)pos.X, (double)pos.Y);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007870 File Offset: 0x00005A70
		public OpenCvSharp.Point TransformPixelPostion(System.Drawing.Point pos)
		{
			OpenCvSharp.Point res = new OpenCvSharp.Point(-1, -1);
			bool flag = base.IsMouseIn(pos);
			if (flag)
			{
				res.X = (int)(((double)pos.X - this.DispRect.X) / this.PixelSize.Width);
				res.Y = (int)(((double)pos.Y - this.DispRect.Y) / this.PixelSize.Height);
			}
			return res;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000078EC File Offset: 0x00005AEC
		protected void AdjustMatRect(Mat mt, ref Rect rect)
		{
			bool flag = rect.X < 0;
			if (flag)
			{
				rect.X = 0;
			}
			bool flag2 = rect.X >= mt.Width;
			if (flag2)
			{
				rect.X = mt.Width - 1;
			}
			bool flag3 = rect.Y < 0;
			if (flag3)
			{
				rect.Y = 0;
			}
			bool flag4 = rect.Y >= mt.Height;
			if (flag4)
			{
				rect.Y = mt.Height - 1;
			}
			bool flag5 = rect.Width + rect.X > mt.Width;
			if (flag5)
			{
				rect.Width = mt.Width - rect.X;
			}
			bool flag6 = rect.Height + rect.Y > mt.Height;
			if (flag6)
			{
				rect.Height = mt.Height - rect.Y;
			}
		}

		// Token: 0x04000066 RID: 102
		protected Mat _Image = null;

		// Token: 0x04000068 RID: 104
		protected Size2d _displaySize;

		// Token: 0x0400006A RID: 106
		public bool IsReast = true;
	}
}
