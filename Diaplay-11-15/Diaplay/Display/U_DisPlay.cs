using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using Tool_View;

namespace Display
{
	public class U_DisPlay : PictureBox
	{
		public event EventHandler<PosChangedEventArgs> PositionChanged;
		public CvDisplayGraphicsMat GraphicsMat
		{
			get
			{
				return this._cdgMat;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public CvDisplayGraphicsShapeCollection GraphicsShapes
		{
			get
			{
				return this.GraphicsMat.GraphicsShapes;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("CvDisplay")]
		[Description("自动显示图片模式")]
		public U_DisPlay.AutoDisplayMode AutoDisplay { get; set; }


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
				this.ImageResize();
			}
		}

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

		public U_DisPlay()
		{
			this._cdgMat = new CvDisplayGraphicsMat();
			this.DoubleBuffered = true;
			this.AutoDisplay = U_DisPlay.AutoDisplayMode.Fit;
            this.ContextMenuStrip = new ContextMenuStrip();
            this.ContextMenuStrip.Items.Add("Fit image", null, new EventHandler(this.OnFitImageClick));
            this.ContextMenuStrip.Items.Add("Original image", null, new EventHandler(this.OnOriginalImageClick));
            this.ContextMenuStrip.Items.Add("Full image", null, new EventHandler(this.OnFullImageClick));
            this.ContextMenuStrip.Items.Add("Save as", null, new EventHandler(this.OnSaveAsClick));
            this.BackColor = Color.Navy;
			this.toolTip1 = new ToolTip();
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 1000;
			this.toolTip1.ReshowDelay = 500;
			this.toolTip1.ShowAlways = true;
			this.toolTip1.ForeColor = Color.Blue;
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			this.Fit();
		}
		protected virtual void OnFitImageClick(object sender, EventArgs e)
		{
			this.Fit();
		}
		protected virtual void OnOriginalImageClick(object sender, EventArgs e)
		{
			this.OriginalSize();
		}
		protected virtual void OnFullImageClick(object sender, EventArgs e)
		{
			this.Full();
		}
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
		protected override void OnMouseDown(MouseEventArgs e)
		{
			bool shapeMove = false;
			bool Once = false;
			foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
			{
				shape.OnMouseDown(e);
				shapeMove |= shape.Selected;

				bool flag = shapeMove && !Once;
				if (flag)
				{
					Once = true;
				}
				else
				{
					shape.Selected = false;
				}
			}
			bool flag2 = e.Button == MouseButtons.Left && !shapeMove;
			if (flag2)
			{
				this.Cursor = Cursors.SizeAll;
				this._isMouseMoving = true;
				this._mouseDownLocation = new OpenCvSharp.Point(e.Location.X, e.Location.Y);
			}
			this.Refresh();
			base.OnMouseDown(e);
		}
		protected virtual void ImageResize()
		{
			switch (this.AutoDisplay)
			{
			case U_DisPlay.AutoDisplayMode.Original:
				this.OriginalSize();
				break;
			case U_DisPlay.AutoDisplayMode.Fit:
				this.Fit();
				break;
			case U_DisPlay.AutoDisplayMode.Full:
				this.Full();
				break;
			}
		}
		protected override void OnResize(EventArgs e)
		{
			bool flag = base.Width != 0 && base.Height != 0;
			if (flag)
			{
				this.ImageResize();
			}
			base.OnResize(e);
		}
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
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this._mouseLocation = e.Location;
			bool IsShow = false;
			bool flag = !this.IsUsedtoolTip;
			if (flag)
			{
				foreach (CvDisplayGraphicsShape shape in this.GraphicsShapes)
				{
					shape.OnMouseMove(e);
				}
				bool flag2 = this._isMouseMoving && this.Image != null;
				if (flag2)
				{
					OpenCvSharp.Point nowLocation = new OpenCvSharp.Point(e.X, e.Y);
					OpenCvSharp.Point move = nowLocation - this._mouseDownLocation;
					this.SyncUpdateOrigin(this._cdgMat.DisplayOrigin + move);
					this._mouseDownLocation = nowLocation;
				}

				
				OpenCvSharp.Point p = this._cdgMat.TransformPixelPostion(e.Location);
				bool flag6 = !p.Equals(this._mouseLocation) && !p.Equals(this._mousePixcelLocation);
				if (flag6)
				{
					string tip = string.Format("({0},{1})", p.X, p.Y);
					object[] res = null;
					MatHelper.GetMatChannelValues(this.Image, p.X, p.Y, out res);
					tip += " [";
					foreach (object obj in res)
					{
						tip = tip + ((obj != null) ? obj.ToString() : null) + ",";
					}
					tip = tip.Substring(0, tip.Length - 1) + "]";
					this.toolTip1.Show(tip, this, 0, base.Height - 20);
				}	
			}
			else
			{
				foreach (CvDisplayGraphicsShape shape2 in this.GraphicsShapes)
				{
					bool flag3 = shape2.IsMouseIn(new PointF((float)e.X, (float)e.Y)) && shape2.Info != "";
					if (flag3)
					{
						this.toolTip1.Show(shape2.Info, this, e.X, e.Y);						
						IsShow = true;
					}
				}
				bool flag4 = this._isMouseMoving && this.Image != null;
				if (flag4)
				{
					OpenCvSharp.Point nowLocation2 = new OpenCvSharp.Point(e.X, e.Y);
					OpenCvSharp.Point move2 = nowLocation2 - this._mouseDownLocation;
					this.SyncUpdateOrigin(this._cdgMat.DisplayOrigin + move2);
					this._mouseDownLocation = nowLocation2;
				}
				else
				{
					bool flag5 = this._cdgMat.IsMouseIn(e.Location);
					if (flag5)
					{
						this.Cursor = Cursors.Cross;
						OpenCvSharp.Point p = this._cdgMat.TransformPixelPostion(e.Location);
						bool flag6 = !p.Equals(this._mouseLocation) && !p.Equals(this._mousePixcelLocation);
						if (flag6)
						{
							string tip = string.Format("({0},{1})", p.X, p.Y);
							object[] res = null;
							MatHelper.GetMatChannelValues(this.Image, p.X, p.Y, out res);
							tip += " [";
							foreach (object obj in res)
							{
								tip = tip + ((obj != null) ? obj.ToString() : null) + ",";
							}
							tip = tip.Substring(0, tip.Length - 1) + "]";
							bool flag7 = !IsShow;
							if (flag7)
							{
								this.toolTip1.Show(tip, this, 0, base.Height - 20);
							}
							bool flag8 = this.PositionChanged != null;
							if (flag8)
							{
								this.PositionChanged(this, new PosChangedEventArgs(p, res));
							}
						}
						this._mousePixcelLocation = p;
					}
					else
					{
						this._mousePixcelLocation = new OpenCvSharp.Point(-1, -1);
					}
				}
			}
			this.Refresh();
			base.OnMouseMove(e);
		}
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
		protected void SyncUpdateOrigin(Point2d p)
		{
			this._cdgMat.DisplayOrigin = p;
		}
		private static System.Drawing.Point ConvertCvPoint2DrawingPoint(OpenCvSharp.Point p)
		{
			return new System.Drawing.Point(p.X, p.Y);
		}
		private static OpenCvSharp.Point ConvertDrawingPoint2CvPoint(System.Drawing.Point p)
		{
			return new OpenCvSharp.Point(p.X, p.Y);
		}
		public void Zoom(double scale)
		{
			this.Zoom(scale, scale);
		}
		public void SaveAs(string filepath)
		{
            bool flag = this.Image == null;
            if (!flag)
            {
                bool v = Cv2.ImWrite(filepath, this.Image);
            }
        }
		public void Zoom(double xScale, double yScale)
		{
			this.Zoom(xScale, yScale, new PointF(0f, 0f));
		}
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
		public virtual void Clear()
		{
			this.Image = null;
			this.GraphicsShapes.Clear();
			this.Refresh();
		}
		private void InitializeComponent()
		{
			((ISupportInitialize)this).BeginInit();
			base.SuspendLayout();
			((ISupportInitialize)this).EndInit();
			base.ResumeLayout(false);
		}

		protected CvDisplayGraphicsMat _cdgMat;

		protected bool _isMouseMoving = false;

		protected OpenCvSharp.Point _mouseDownLocation;

		protected System.Drawing.Point _mouseLocation;

		private IContainer components;

		protected OpenCvSharp.Point _mousePixcelLocation;

		private ToolTip toolTip1;

		public bool IsUsedtoolTip = false;
		public string szShowTip = "";


		public enum AutoDisplayMode
		{
			Original,
			Fit,
			Full
		}
	}
}
