using System;
using System.Drawing;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x0200001B RID: 27
	public abstract class CvDisplayGraphicsShape : CvDisplayGraphicsObject
	{
		public Color Color { get; set; }
		public float Size { get; set; }

		public Color FocusedColor { get; set; }

		public Point2d MatDisplayOrigin
		{
			get
			{
				return (this.ParentMat == null) ? new Point2d(0.0, 0.0) : this.ParentMat.DisplayOrigin;
			}
		}
		public override Size2d PixelSize
		{
			get
			{
				return (this.ParentMat == null) ? new Size2d(1f, 1f) : this.ParentMat.PixelSize;
			}
		}
		public virtual CvDisplayGraphicsMat ParentMat { get; set; }
		public CvDisplayGraphicsShape()
		{
			this.ParentMat = null;
			this.Color = Color.Black;
			this.FocusedColor = Color.LightBlue;
			this.Size = 1f;
		}
		protected bool IsFocusedOrSelected
		{
			get
			{
				return base.IsFocused || base.Selected;
			}
		}
		protected Color GetDrawColor()
		{
			return this.IsFocusedOrSelected ? this.FocusedColor : this.Color;
		}
		protected float GetDrawSize()
		{
			return this.IsFocusedOrSelected ? (this.Size + 2f) : (this.Size + 2f);
		}
		protected virtual Point2d TranslateToMatPos(Point2d p)
		{
			return this.TranslateToPixelPos(this.MatDisplayOrigin, p);
		}
		protected virtual Point2d TranslateToScreenPos(Point2d p)
		{
			return this.TranslateToScreenPos(this.MatDisplayOrigin, p);
		}
		protected virtual void DrawCircle(Graphics g, Pen pen, PointF p, float radius)
		{
			g.DrawEllipse(pen, new RectangleF(new PointF(p.X - radius, p.Y - radius), new SizeF(radius * 2f, radius * 2f)));
		}
		protected virtual void DrawRectangle(Graphics g, Pen pen, Rectangle rect)
		{
			g.DrawRectangle(pen, rect);
		}
		protected virtual void DrawLine(Graphics g, Pen pen, float x1, float x2, float y1, float y2)
		{
			g.DrawLine(pen, x1, x2, y1, y2);
		}
		protected void DrawCross(Graphics g, PointF p, Color color, float lineLength = 10f)
		{
			Pen pen = new Pen(color, this.GetDrawSize());
			float halfLength = lineLength / 2f;
			g.DrawLine(pen, p.X - halfLength, p.Y - halfLength, p.X + halfLength, p.Y + halfLength);
			g.DrawLine(pen, p.X - halfLength, p.Y + halfLength, p.X + halfLength, p.Y - halfLength);
		}
	}
}
