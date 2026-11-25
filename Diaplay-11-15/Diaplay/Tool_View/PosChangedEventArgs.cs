using System;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x0200001E RID: 30
	public class PosChangedEventArgs : EventArgs
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000A06A File Offset: 0x0000826A
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000A072 File Offset: 0x00008272
		public Point Pos { get; protected set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000A07B File Offset: 0x0000827B
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000A083 File Offset: 0x00008283
		public object[] ChannelValues { get; protected set; }

		// Token: 0x0600013A RID: 314 RVA: 0x0000A08C File Offset: 0x0000828C
		public PosChangedEventArgs(Point pos, object[] channelsValue)
		{
			this.Pos = pos;
			this.ChannelValues = channelsValue;
		}
	}
}
