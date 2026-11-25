using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace Tool_View
{
	enum enumShapeType
    {
		rectangle1,
		rectangle2,
	}
	// Token: 0x02000018 RID: 24
	public abstract class CvDisplayGraphicsObject : ICvDisplayGraphics, IDisposable
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000079C4 File Offset: 0x00005BC4
		public CvDisplayGraphicsObject()
		{
			this.Name = string.Empty;
			this.IsFocused = false;
			this.Actived = true;
			this.Visble = true;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00007A11 File Offset: 0x00005C11
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00007A19 File Offset: 0x00005C19
		public string Name { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00007A22 File Offset: 0x00005C22
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00007A2A File Offset: 0x00005C2A
		public string Info { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007A34 File Offset: 0x00005C34
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00007A4C File Offset: 0x00005C4C
		public virtual Point2d DisplayOrigin
		{
			get
			{
				return this._DisplayOrigin;
			}
			set
			{
				Point2d old = this._DisplayOrigin;
				this._DisplayOrigin = value;
				this.OnDisplayOriginChanged(old, this._DisplayOrigin);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00007A76 File Offset: 0x00005C76
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00007A7E File Offset: 0x00005C7E
		public bool IsFocused { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007A87 File Offset: 0x00005C87
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00007A8F File Offset: 0x00005C8F
		public bool Selected { get; set; }

		public bool Actived { get; set; }
		public bool Visble { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007A98 File Offset: 0x00005C98
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public virtual Size2d PixelSize
		{
			get
			{
				return this._pixelSize;
			}
			set
			{
				this._pixelSize = value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007ABA File Offset: 0x00005CBA
		public void Select()
		{
			this.Selected = true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007AC5 File Offset: 0x00005CC5
		public void Focus()
		{
			this.IsFocused = true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public virtual void OnMouseDown(MouseEventArgs e)
		{
			if(!Actived)
            {
				this.Selected = false;
				return;
            }
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.IsLeftMouseDown = true;
				this.MouseDownPos = e.Location;
				this.Selected = this.IsMouseIn(e.Location);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004E31 File Offset: 0x00003031
		public virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007B18 File Offset: 0x00005D18
		public virtual void OnMouseUp(MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				this.IsLeftMouseDown = false;
			}
		}

		// Token: 0x060000EE RID: 238
		public abstract void OnPaint(PaintEventArgs e);

		// Token: 0x060000EF RID: 239 RVA: 0x00007B40 File Offset: 0x00005D40
		public virtual void Reset()
		{
			this.DisplayOrigin = new Point2d(0.0, 0.0);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E31 File Offset: 0x00003031
		public virtual void Dispose()
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007B64 File Offset: 0x00005D64
		protected virtual Point2d TranslateToPixelPos(Point2d origin, Point2d pos)
		{
			Point2d res = new Point2d((pos.X - origin.X) / this.PixelSize.Width, (pos.Y - origin.Y) / this.PixelSize.Height);
			return res;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007BB0 File Offset: 0x00005DB0
		protected virtual Point2d TranslateToScreenPos(Point2d origin, Point2d pos)
		{
			Point2d res = new Point2d(origin.X + this.PixelSize.Width * pos.X, origin.Y + this.PixelSize.Height * pos.Y);
			return res;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E31 File Offset: 0x00003031
		protected virtual void OnDisplayOriginChanged(Point2d oldPos, Point2d newPos)
		{
		}

		// Token: 0x060000F4 RID: 244
		public abstract bool IsMouseIn(PointF pos);

		// Token: 0x060000F5 RID: 245 RVA: 0x00007BFC File Offset: 0x00005DFC
		public bool IsMouseIn(System.Drawing.Point pos)
		{
			return this.IsMouseIn(new PointF((float)pos.X, (float)pos.Y));
		}

		// Token: 0x0400006D RID: 109
		protected Point2d _DisplayOrigin = new Point2d(0.0, 0.0);

		// Token: 0x04000070 RID: 112
		protected Size2d _pixelSize;

		// Token: 0x04000071 RID: 113
		protected bool IsLeftMouseDown = false;

		// Token: 0x04000072 RID: 114
		protected System.Drawing.Point MouseDownPos;
	}
}
