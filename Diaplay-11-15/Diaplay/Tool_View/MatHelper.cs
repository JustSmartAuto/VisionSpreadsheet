using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace Tool_View
{
	// Token: 0x0200001D RID: 29
	public static class MatHelper
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static int GetMatChannelValues(Mat mt, int x, int y, out object[] channelValues)
		{
			List<object> objres = new List<object>();
			bool flag = x >= 0 && x < mt.Width && y >= 0 && y < mt.Height;
			if (flag)
			{
				string typeString = mt.Type().ToString();
				bool flag2 = typeString.Contains("CV_8");
				if (flag2)
				{
					foreach (byte obj in MatHelper._GetMatChannelValues_Byte(mt, x, y))
					{
						objres.Add(obj);
					}
				}
				else
				{
					bool flag3 = typeString.Contains("CV_16U");
					if (flag3)
					{
						foreach (ushort obj2 in MatHelper._GetMatChannelValues_UInt16(mt, x, y))
						{
							objres.Add(obj2);
						}
					}
					else
					{
						bool flag4 = typeString.Contains("CV_16S");
						if (flag4)
						{
							foreach (short obj3 in MatHelper._GetMatChannelValues_Int16(mt, x, y))
							{
								objres.Add(obj3);
							}
						}
						else
						{
							bool flag5 = typeString.Contains("CV_32S");
							if (flag5)
							{
								foreach (int obj4 in MatHelper._GetMatChannelValues_Int32(mt, x, y))
								{
									objres.Add(obj4);
								}
							}
							else
							{
								bool flag6 = typeString.Contains("CV_32F");
								if (flag6)
								{
									foreach (float obj5 in MatHelper._GetMatChannelValues_Float(mt, x, y))
									{
										objres.Add(obj5);
									}
								}
								else
								{
									bool flag7 = typeString.Contains("CV_64F");
									if (flag7)
									{
										foreach (double obj6 in MatHelper._GetMatChannelValues_Double(mt, x, y))
										{
											objres.Add(obj6);
										}
									}
								}
							}
						}
					}
				}
			}
			channelValues = objres.ToArray();
			return objres.Count;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009DE8 File Offset: 0x00007FE8
		private unsafe static byte[] _GetMatChannelValues_Byte(Mat mt, int x, int y)
		{
			List<byte> lstRes = new List<byte>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				byte* tPtr = (byte*)mt.Ptr(y).ToPointer();
				lstRes.Add(tPtr[x * channels + i]);
			}
			return lstRes.ToArray();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009E50 File Offset: 0x00008050
		private unsafe static ushort[] _GetMatChannelValues_UInt16(Mat mt, int x, int y)
		{
			List<ushort> lstRes = new List<ushort>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				ushort* tPtr = (ushort*)mt.Ptr(x).ToPointer();
				lstRes.Add(tPtr[y * channels + i]);
			}
			return lstRes.ToArray();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009EBC File Offset: 0x000080BC
		private unsafe static short[] _GetMatChannelValues_Int16(Mat mt, int x, int y)
		{
			List<short> lstRes = new List<short>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				short* tPtr = (short*)mt.Ptr(x).ToPointer();
				lstRes.Add(tPtr[y * channels + i]);
			}
			return lstRes.ToArray();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009F28 File Offset: 0x00008128
		private unsafe static int[] _GetMatChannelValues_Int32(Mat mt, int x, int y)
		{
			List<int> lstRes = new List<int>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				int* tPtr = (int*)mt.Ptr(x).ToPointer();
				lstRes.Add(tPtr[y * channels + i]);
			}
			return lstRes.ToArray();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009F94 File Offset: 0x00008194
		private unsafe static float[] _GetMatChannelValues_Float(Mat mt, int x, int y)
		{
			List<float> lstRes = new List<float>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				float* tPtr = (float*)mt.Ptr(x).ToPointer();
				lstRes.Add(tPtr[y * channels + i]);
			}
			return lstRes.ToArray();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A000 File Offset: 0x00008200
		private unsafe static double[] _GetMatChannelValues_Double(Mat mt, int x, int y)
		{
			List<double> lstRes = new List<double>();
			int channels = mt.Type().Channels;
			for (int i = 0; i < channels; i++)
			{
				double* tPtr = (double*)mt.Ptr(x).ToPointer();
				lstRes.Add(tPtr[y * channels + i]);
			}
			return lstRes.ToArray();
		}
	}
}
