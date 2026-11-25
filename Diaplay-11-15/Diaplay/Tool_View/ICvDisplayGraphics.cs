using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tool_View
{
	// Token: 0x02000017 RID: 23
	public interface ICvDisplayGraphics
	{
		// Token: 0x060000D7 RID: 215
		void OnPaint(PaintEventArgs e);

		// Token: 0x060000D8 RID: 216
		bool IsMouseIn(PointF pos);

		// Token: 0x060000D9 RID: 217
		void OnMouseMove(MouseEventArgs e);

		// Token: 0x060000DA RID: 218
		void OnMouseDown(MouseEventArgs e);

		// Token: 0x060000DB RID: 219
		void OnMouseUp(MouseEventArgs e);
	}
}
