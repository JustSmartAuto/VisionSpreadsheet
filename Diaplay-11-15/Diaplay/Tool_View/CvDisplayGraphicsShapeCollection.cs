using System;
using System.Collections.Generic;

namespace Tool_View
{
	// Token: 0x0200001C RID: 28
	public class CvDisplayGraphicsShapeCollection : List<CvDisplayGraphicsShape>
	{
		// Token: 0x06000126 RID: 294 RVA: 0x000099E8 File Offset: 0x00007BE8
		public CvDisplayGraphicsShapeCollection() : this(null)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000099F3 File Offset: 0x00007BF3
		public CvDisplayGraphicsShapeCollection(CvDisplayGraphicsMat gMat)
		{
			this.ParentMat = gMat;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00009A08 File Offset: 0x00007C08
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00009A20 File Offset: 0x00007C20
		public CvDisplayGraphicsMat ParentMat
		{
			get
			{
				return this._ParentMat;
			}
			set
			{
				this._ParentMat = value;
				foreach (CvDisplayGraphicsShape shape in this)
				{
					shape.ParentMat = this.ParentMat;
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009A80 File Offset: 0x00007C80
		public new void Add(CvDisplayGraphicsShape shape)
		{
			base.Add(shape);
			shape.ParentMat = this.ParentMat;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009A98 File Offset: 0x00007C98
		public new void AddRange(IEnumerable<CvDisplayGraphicsShape> range)
		{
			base.AddRange(range);
			foreach (CvDisplayGraphicsShape shape in range)
			{
				shape.ParentMat = this.ParentMat;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public new void Insert(int index, CvDisplayGraphicsShape shape)
		{
			base.Insert(index, shape);
			shape.ParentMat = this.ParentMat;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009B10 File Offset: 0x00007D10
		public new void InsertRange(int index, IEnumerable<CvDisplayGraphicsShape> range)
		{
			base.InsertRange(index, range);
			foreach (CvDisplayGraphicsShape shape in range)
			{
				shape.ParentMat = this.ParentMat;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009B6C File Offset: 0x00007D6C
		public new void Clear()
		{
			foreach (CvDisplayGraphicsShape obj in this)
			{
				obj.Dispose();
			}
			base.Clear();
		}

		// Token: 0x0400008B RID: 139
		private CvDisplayGraphicsMat _ParentMat;
	}
}
