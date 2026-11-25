using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using Tool_View;

namespace Display
{
	// Token: 0x02000003 RID: 3
	public class U_DisPlay_Pen : PictureBox
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002E00 File Offset: 0x00001000
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002E38 File Offset: 0x00001038
		
		public event EventHandler<PosChangedEventArgs> PositionChanged;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E70 File Offset: 0x00001070
		public CvDisplayGraphicsMat GraphicsMat
		{
			get
			{
				return this._cdgMat;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002E88 File Offset: 0x00001088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public CvDisplayGraphicsShapeCollection GraphicsShapes
		{
			get
			{
				return this.GraphicsMat.GraphicsShapes;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002EA5 File Offset: 0x000010A5
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002EAD File Offset: 0x000010AD
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("CvDisplay")]
		[Description("自动显示图片模式")]
		public U_DisPlay_Pen.AutoDisplayMode AutoDisplay { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002ED8 File Offset: 0x000010D8
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("CvDisplay")]
		[Description("OpenCv2 Mat图片数据类")]
		public new Mat Image
		{
			get
			{
				return this._cdgMat.Image;
			}
			set
			{
				this._cdgMat.Image = value;
				bool flag = value != null;
				if (flag)
				{
					this.org = this._cdgMat.Image.Clone();
					this.Mask = new Mat(this._cdgMat.Image.Size(), MatType.CV_8UC1, new Scalar(0.0));
					this.Mask1 = new Mat(this._cdgMat.Image.Size(), MatType.CV_8UC1, new Scalar(0.0));
					this.Redimg = new Mat(this._cdgMat.Image.Size(), MatType.CV_8UC3, new Scalar(0.0, 0.0, 255.0));
					this.Fit();
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002154 File Offset: 0x00000354
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = null;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002FD4 File Offset: 0x000011D4
		public U_DisPlay_Pen()
		{
			this._cdgMat = new CvDisplayGraphicsMat(false);
			this.DoubleBuffered = true;
			this.AutoDisplay = U_DisPlay_Pen.AutoDisplayMode.Fit;
			this.BackColor = Color.Navy;
			this.toolTip1 = new ToolTip();
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 1000;
			this.toolTip1.ReshowDelay = 500;
			this.toolTip1.ShowAlways = true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000030AC File Offset: 0x000012AC
		public void SetMask(Mat mask)
		{
			bool flag = mask != null;
			if (flag)
			{
				this.Mask = mask.Clone();
				this.Redimg.CopyTo(this._cdgMat.Image, this.Mask);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000030F2 File Offset: 0x000012F2
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.Fit();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003104 File Offset: 0x00001304
		protected virtual void OnFitImageClick(object sender, EventArgs e)
		{
			this.Fit();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000310E File Offset: 0x0000130E
		protected virtual void OnOriginalImageClick(object sender, EventArgs e)
		{
			this.OriginalSize();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003118 File Offset: 0x00001318
		protected virtual void OnFullImageClick(object sender, EventArgs e)
		{
			this.Full();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003124 File Offset: 0x00001324
		protected virtual void OnSaveAsClick(object sender, EventArgs e)
		{
			bool flag = this.Image == null;
			if (!flag)
			{
				using (SaveFileDialog ofd = new SaveFileDialog())
				{
					ofd.Filter = "Bitmap|*.bmp";
					bool flag2 = ofd.ShowDialog() == DialogResult.OK;
					if (flag2)
					{
						this.SaveAs(ofd.FileName);
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003190 File Offset: 0x00001390
		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool shapeMove = false;
			foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
			{
				shape.OnMouseDown(e);
				shapeMove |= shape.Selected;
			}
			bool flag = e.Button == MouseButtons.Left && !shapeMove;
			if (flag)
			{
				this.Cursor = Cursors.SizeAll;
				this._isMouseMoving = true;
				this._mouseDownLocation = new OpenCvSharp.Point(e.Location.X, e.Location.Y);
			}
			this.Refresh();
			base.OnMouseDown(e);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000325C File Offset: 0x0000145C
		protected virtual void ImageResize()
		{
			switch (this.AutoDisplay)
			{
			case U_DisPlay_Pen.AutoDisplayMode.Original:
				this.OriginalSize();
				break;
			case U_DisPlay_Pen.AutoDisplayMode.Fit:
				this.Fit();
				break;
			case U_DisPlay_Pen.AutoDisplayMode.Full:
				this.Full();
				break;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032A4 File Offset: 0x000014A4
		protected override void OnResize(EventArgs e)
		{
			bool flag = base.Width != 0 && base.Height != 0;
			if (flag)
			{
				this.ImageResize();
			}
			base.OnResize(e);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000032DC File Offset: 0x000014DC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
			{
				shape.OnMouseUp(e);
			}
			this.Cursor = Cursors.Default;
			this._isMouseMoving = false;
			base.OnMouseUp(e);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003350 File Offset: 0x00001550
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			bool flag = e.Delta > 0;
			if (flag)
			{
				this.Zoom(2.0, 2.0, new PointF((float)e.X, (float)e.Y));
			}
			else
			{
				this.Zoom(0.5, 0.5, new PointF((float)e.X, (float)e.Y));
			}
			foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
			{
				shape.OnMouseUp(e);
			}
			base.OnMouseWheel(e);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000341C File Offset: 0x0000161C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this._mouseLocation = e.Location;
			bool flag = this._isMouseMoving && this.Image != null;
			if (flag)
			{
				bool flag2 = this.Type_S == 0;
				if (flag2)
				{
					OpenCvSharp.Point nowLocation = new OpenCvSharp.Point(e.X, e.Y);
					OpenCvSharp.Point move = nowLocation - this._mouseDownLocation;
					this.SyncUpdateOrigin(this._cdgMat.DisplayOrigin + move);
					this._mouseDownLocation = nowLocation;
				}
				else
				{
					bool flag3 = this.Type_S == 1;
					if (flag3)
					{
						this.Cursor = Cursors.Hand;
						OpenCvSharp.Point p = this._cdgMat.TransformPixelPostion(e.Location);
						Cv2.Circle(this.Mask, p.X, p.Y, this.Size_Pen, new Scalar(255.0), this.Size_Pen + 2, LineTypes.Link8, 0);
						Cv2.Circle(this.Mask1, p.X, p.Y, this.Size_Pen, new Scalar(0.0), this.Size_Pen + 2, LineTypes.Link8, 0);
						this.Redimg.CopyTo(this._cdgMat.Image, this.Mask);
						this._mousePixcelLocation = p;
					}
					else
					{
						bool flag4 = this.Type_S == 2;
						if (flag4)
						{
							this.Cursor = Cursors.Hand;
							OpenCvSharp.Point p2 = this._cdgMat.TransformPixelPostion(e.Location);
							Cv2.Circle(this.Mask, p2.X, p2.Y, this.Size_Pen, new Scalar(0.0), this.Size_Pen + 2, LineTypes.Link8, 0);
							Cv2.Circle(this.Mask1, p2.X, p2.Y, this.Size_Pen, new Scalar(255.0), this.Size_Pen + 2, LineTypes.Link8, 0);
							this.org.CopyTo(this._cdgMat.Image, this.Mask1);
							this._mousePixcelLocation = p2;
						}
					}
				}
			}
			else
			{
				this._mousePixcelLocation = new OpenCvSharp.Point(-1, -1);
			}
			this.Refresh();
			base.OnMouseMove(e);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003680 File Offset: 0x00001880
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics gh = e.Graphics;
			gh.Clear(this.BackColor);
			bool flag = this.Image != null;
			if (flag)
			{
				this._cdgMat.OnPaint(e);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000036C6 File Offset: 0x000018C6
		protected void SyncUpdateOrigin(Point2d p)
		{
			this._cdgMat.DisplayOrigin = p;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000036D8 File Offset: 0x000018D8
		private static System.Drawing.Point ConvertCvPoint2DrawingPoint(OpenCvSharp.Point p)
		{
			return new System.Drawing.Point(p.X, p.Y);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000036FC File Offset: 0x000018FC
		private static OpenCvSharp.Point ConvertDrawingPoint2CvPoint(System.Drawing.Point p)
		{
			return new OpenCvSharp.Point(p.X, p.Y);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003721 File Offset: 0x00001921
		public void Zoom(double scale)
		{
			this.Zoom(scale, scale);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003730 File Offset: 0x00001930
		public void SaveAs(string filepath)
		{
            bool flag = this.Image == null;
            if (!flag)
            {
                Cv2.ImWrite(filepath, this.Image);
            }
        }

		// Token: 0x06000041 RID: 65 RVA: 0x0000375B File Offset: 0x0000195B
		public void Zoom(double xScale, double yScale)
		{
			this.Zoom(xScale, yScale, new PointF(0f, 0f));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003778 File Offset: 0x00001978
		public void Zoom(double xScale, double yScale, PointF zoomOrign)
		{
			bool flag = this.Image == null;
			if (!flag)
			{
				double newXPixelSize = Math.Abs(xScale) * this._cdgMat.PixelSize.Width;
				double newYPixelSize = Math.Abs(yScale) * this._cdgMat.PixelSize.Height;
				bool flag2 = newXPixelSize > 0.0 && newYPixelSize > 0.0;
				if (flag2)
				{
					int dispPixelX = (int)((double)base.Width / newXPixelSize);
					int dispPixelY = (int)((double)base.Height / newYPixelSize);
					bool flag3 = dispPixelX < 1 || dispPixelY < 1;
					if (!flag3)
					{
						bool flag4 = this._cdgMat.IsMouseIn(zoomOrign);
						if (flag4)
						{
							double disX = (double)zoomOrign.X - this._cdgMat.DisplayOrigin.X;
							double disY = (double)zoomOrign.Y - this._cdgMat.DisplayOrigin.Y;
							disX *= xScale;
							disY *= yScale;
							this.SyncUpdateOrigin(new Point2d((double)zoomOrign.X - disX, (double)zoomOrign.Y - disY));
						}
						this._cdgMat.PixelSize = new Size2d(newXPixelSize, newYPixelSize);
						this.Refresh();
					}
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000038B0 File Offset: 0x00001AB0
		public virtual void Full()
		{
			bool flag = this.Image == null;
			if (!flag)
			{
				this._cdgMat.PixelSize = new Size2d((double)base.Width / (double)this.Image.Width, (double)base.Height / (double)this.Image.Height);
				this._cdgMat.DisplayOrigin = new Point2d(0.0, 0.0);
				this.Refresh();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003930 File Offset: 0x00001B30
		public virtual void Fit()
		{
			bool flag = this.Image == null;
			if (!flag)
			{
				Size2d newsize = default(Size2d);
				double hvScale = (double)base.Width / (double)base.Height;
				double hvScale2 = (double)this.Image.Width / (double)this.Image.Height;
				bool flag2 = hvScale > hvScale2;
				if (flag2)
				{
					newsize.Height = (double)base.Height;
					newsize.Width = (double)this.Image.Width * (newsize.Height / (double)this.Image.Height);
				}
				else
				{
					newsize.Width = (double)base.Width;
					newsize.Height = (double)this.Image.Height * (newsize.Width / (double)this.Image.Width);
				}
				this._cdgMat.PixelSize = new Size2d(newsize.Width / (double)this.Image.Width, newsize.Height / (double)this.Image.Height);
				this.SyncUpdateOrigin(new Point2d(((double)base.Width - this._cdgMat.DispRect.Width) / 2.0, ((double)base.Height - this._cdgMat.DispRect.Height) / 2.0));
				this.Refresh();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A8C File Offset: 0x00001C8C
		public virtual void OriginalSize()
		{
			bool flag = this.Image == null;
			if (!flag)
			{
				this._cdgMat.PixelSize = new Size2d(1f, 1f);
				this.SyncUpdateOrigin(new Point2d(0.0, 0.0));
				this.Refresh();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003AE9 File Offset: 0x00001CE9
		public virtual void Clear()
		{
			this.Image = null;
			this.GraphicsShapes.Clear();
			this.Refresh();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DDF File Offset: 0x00000FDF
		private void InitializeComponent()
		{
			((ISupportInitialize)this).BeginInit();
			base.SuspendLayout();
			((ISupportInitialize)this).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400000B RID: 11
		public Mat org;

		// Token: 0x0400000C RID: 12
		public Mat Mask;

		// Token: 0x0400000D RID: 13
		public Mat Mask1;

		// Token: 0x0400000E RID: 14
		public Mat Redimg;

		// Token: 0x0400000F RID: 15
		public int Size_Pen = 2;

		// Token: 0x04000010 RID: 16
		public int Type_S = 0;

		// Token: 0x04000011 RID: 17

		// Token: 0x04000012 RID: 18
		protected CvDisplayGraphicsMat _cdgMat;

		// Token: 0x04000013 RID: 19
		protected bool _isMouseMoving = false;

		// Token: 0x04000014 RID: 20
		protected OpenCvSharp.Point _mouseDownLocation;

		// Token: 0x04000015 RID: 21
		protected System.Drawing.Point _mouseLocation;

		// Token: 0x04000016 RID: 22
		private IContainer components;

		// Token: 0x04000017 RID: 23
		protected OpenCvSharp.Point _mousePixcelLocation;

		// Token: 0x0400001A RID: 26
		private ToolTip toolTip1;

		// Token: 0x02000020 RID: 32
		public enum AutoDisplayMode
		{
			// Token: 0x04000093 RID: 147
			Original,
			// Token: 0x04000094 RID: 148
			Fit,
			// Token: 0x04000095 RID: 149
			Full
		}
	}
}
