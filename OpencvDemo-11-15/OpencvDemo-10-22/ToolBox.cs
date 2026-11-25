using System;
using System.Runtime.InteropServices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using Tool_View;

namespace OpencvDemo
{
    public struct Info
    {
        public Info(int no, double value)
        {
            this.No = no;
            this.Value = value;
        }

        public int No;
        public double Value;
    }
    public struct floatPoint
    {
        // Token: 0x040000A8 RID: 168
        public float X;

        // Token: 0x040000A9 RID: 169
        public float Y;
    }
    public enum enumImageChannel
    {
        MONO = 0,
        RED = 1,
        GREEN = 2,
        BLUE = 3
    }

    struct TemplateI
    {
        public int tl_x;
        public int tl_y;
        public int br_x;
        public int br_y;
        public int width;
        public int height;
        public int num_features;
        public int mem_id;
        public IntPtr features;
    };

    struct TemplateF
    {
        public int tl_x;
        public int tl_y;
        public int br_x;
        public int br_y;
        public int width;
        public int height;
        public int num_features;
        public int mem_id;
        public IntPtr features;
    };

    struct Match
    {
        public int x;
        public int y;
        public int label;
        public float angle;
        public float scale;
        public float scale2;
        public float score;
        public char polarity;
        public float dx;
        public float dy;
    };

    struct FeatureF
    {
        public float x;
        public float y;
        public float nx;
        public float ny;
    };
    struct FeatureI
    {
        public int x;
        public int y;
        public float nx;
        public float ny;
        public int label;
    };

    public struct paramMatchModel     //模板匹配参数
    {
        public float fCModelStartAngle;//模板起始角度
        public float fCModelEndAngle;
        public int nCModelContrastLow;
        public int nCModelContrastHigh;
        public int nCModelMinContrast;
        public int nCNumLevels;//金字塔等级
        public int nCMinEdge;
        public bool bIgnorePolarity;
        public float fFModelStartAngle;//模板起始角度
        public float fFModelEndAngle;
        public float fFMinScale;
        public float fFMaxScale;
        public float fFMinScore;
        public int nFNumMatchs;
        public float fFMaxOverlap;
        public float fFGreediness;
        public int nFEndNumLevels;//金字塔等级
        public int nFSubPix;//拟合类型
    };

    public struct paramCheckBoard
    {
        public int nCheckBoardWidth;
        public int nCheckBoardHeight;
        public float fCheckerBoardDis;
        public int nCornerPixlSize;
    };

    public struct paramGrab
    {
        public enumImageChannel ImageChannel;
        public double dbShutter;
        public double dbGain;
    };

    public struct paramFindLine      //找线参数
    {
        public int nLineCalipersNum;
        public int nLineCalipersLength;
        public int nLineCalipersWidth;
        public int nLineCalipersThreshold;
        public int nLineCalipersMaxError;
        public int nLineCalipersIgnorePoints;
        public int nLineCalipersFilterSize;
        public enumCaliperDirection enumLineCalipersDirection;
        public enumCaliperSearchModel enumLineCalipersSearchModel;
        public double dLineX1;
        public double dLineY1;
        public double dLineX2;
        public double dLineY2;
    };

    public struct paramDistance      //距离测量参数
    {
        public int nDistanceCalipersThreshold;
        public int nDistanceCalipersFilterSize;
        public enumCaliperSearchNumber enumDistanceCalipersStartSearchNumber;
        public enumCaliperSearchModel enumDistanceCalipersStartSearchModel;
        public enumCaliperSearchNumber enumDistanceCalipersEndSearchNumber;
        public enumCaliperSearchModel enumDistanceCalipersEndSearchModel;
    };
    public struct paramFindCircle//找圆参数
    {
        public int nCircleCalipersNum;
        public int nCircleCalipersLength;
        public int nCircleCalipersWidth;
        public int nCircleCalipersThreshold;
        public double dCircleX;
        public double dCircleY;
        public double dCircleR;
        public int nCircleCalipersFilterSize;
        public int nCircleCalipersIgnorePoints;
        public enumCaliperDirection enumCircleCalipersDirection;
        public enumCaliperSearchModel enumCircleCalipersSearchModel;

    };

    public struct paramFindRectangle2//测量参数
    {
        public int nRectCalipersNum;
        public int nRectCalipersLength;
        public int nRectCalipersWidth;
        public int nRectCalipersThreshold;
        public int nRectCalipersMaxError;
        public int nRectCalipersIgnorePoints;
        public int nRectCalipersFilterSize;
        public enumCaliperDirection enumRectCalipersDirection;
        public enumCaliperSearchModel enumRectCalipersSearchModel;
        public double dRectX;
        public double dRectY;
        public double dRectR;
        public double dRectL1;
        public double dRectL2;
    };

    public static class Glovar
    {
        // Token: 0x0600004B RID: 75 RVA: 0x00008F00 File Offset: 0x00007100
        public static Mat Getdifference(Mat src1, Mat src2)
        {
            int rowsLeft = src1.Rows;
            int colsLeft = src1.Cols;
            int rowsRight = src2.Rows;
            int colsRight = src2.Cols;
            bool flag = rowsLeft != rowsRight || colsLeft != colsRight;
            Mat result;
            if (flag)
            {
                result = null;
            }
            else
            {
                Mat dst = new Mat(src1.Rows, src1.Cols, MatType.CV_32FC1);
                for (int i = 0; i < src1.Width; i++)
                {
                    for (int j = 0; j < src1.Height; j++)
                    {
                        double a = (double)((Math.Abs(src1.Get<double>(j, i) - src2.Get<double>(j, i)) < 120.0) ? 255 : 0);
                        dst.Set<double>(j, i, a);
                        bool flag2 = a == 255.0;
                        if (flag2)
                        {
                            a += 1.0;
                        }
                    }
                }
                result = dst;
            }
            return result;
        }

        // Token: 0x0600004C RID: 76 RVA: 0x00009008 File Offset: 0x00007208
        public static Mat conv2(Mat img, Mat ikernel, string type)
        {
            Mat dest = new Mat();
            Mat kernel = new Mat();
            Cv2.Flip(ikernel, kernel, FlipMode.XY);
            Mat source = img.Clone();
            bool flag = "CONVOLUTION_FULL" == type;
            if (flag)
            {
                int additionalRows = kernel.Rows - 1;
                int additionalCols = kernel.Cols - 1;
                Cv2.CopyMakeBorder(img, source, (additionalRows + 1) / 2, additionalRows / 2, (additionalCols + 1) / 2, additionalCols / 2, BorderTypes.Constant, new Scalar?(new Scalar(0.0)));
            }
            OpenCvSharp.Point anchor = new OpenCvSharp.Point(kernel.Cols - kernel.Cols / 2 - 1, kernel.Rows - kernel.Rows / 2 - 1);
            Cv2.Filter2D(img, dest, MatType.CV_64FC1, kernel, new OpenCvSharp.Point?(anchor), 0.0, BorderTypes.Constant);
            bool flag2 = "CONVOLUTION_VALID" == type;
            Mat result;
            if (flag2)
            {
                int colS = (kernel.Cols - 1) / 2;
                int colNum = dest.Cols - kernel.Cols / 2;
                int rowS = (kernel.Rows - 1) / 2;
                int rowNum = dest.Rows - kernel.Rows / 2;
                Mat dest2 = new Mat(colNum, rowNum, MatType.CV_64FC1);
                for (int x = 0; x < colNum; x++)
                {
                    for (int y = 0; y < rowNum; y++)
                    {
                        dest2.Set<double>(y, x, dest.Get<double>(y + rowS, x + colNum));
                    }
                }
                result = dest2;
            }
            else
            {
                result = dest;
            }
            return result;
        }

        // Token: 0x0600004D RID: 77 RVA: 0x000091B0 File Offset: 0x000073B0
        public static floatPoint[] GetRectanglePoint_Cap(Mat Ro, Point2f Center, int RoiW, int RoiH)
        {
            floatPoint[] points = new floatPoint[RoiW * RoiH];
            floatPoint StartPoint;
            StartPoint.X = (float)((double)Center.X - (double)RoiW / 2.0);
            StartPoint.Y = (float)((double)Center.Y - (double)RoiH / 2.0);
            for (int i = 0; i < RoiH; i++)
            {
                floatPoint NowPoint;
                NowPoint.Y = StartPoint.Y + (float)i;
                for (int j = 0; j < RoiW; j++)
                {
                    NowPoint.X = StartPoint.X + (float)j;
                    floatPoint p = Glovar.PointToRotatPoint(NowPoint, Ro);
                    points[j + i * RoiW] = p;
                }
            }
            return points;
        }

        public static floatPoint[] GetDistancePoint_Cap(Mat Ro, Point2d pStart, Point2d pEnd)
        {
            int nNum = (int)Math.Sqrt((pStart.X - pEnd.X) * (pStart.X - pEnd.X) + (pStart.Y - pEnd.Y) * (pStart.Y - pEnd.Y));
            if(nNum == 0)
            {
                return null;
            }

            floatPoint[] points = new floatPoint[nNum];
            floatPoint StartPoint;        
            for (int i = 0; i < nNum; i++)
            {
                StartPoint.X = (float)(pStart.X+i);
                StartPoint.Y = (float)(pStart.Y);
                floatPoint p = Glovar.PointToRotatPoint(StartPoint, Ro);
                points[i] = p;    
            }
            return points;
        }

        // Token: 0x0600004E RID: 78 RVA: 0x0000926C File Offset: 0x0000746C
        public static floatPoint[] GetRectanglePoint(Mat Ro, Point2f Center, int RoiW, int RoiH)
        {
            floatPoint[] points = new floatPoint[RoiW * RoiH];
            floatPoint StartPoint;
            StartPoint.X = (float)((double)Center.X - (double)RoiW / 2.0);
            StartPoint.Y = (float)((double)Center.Y - (double)RoiH / 2.0);
            for (int i = 0; i < RoiH; i++)
            {
                floatPoint NowPoint;
                NowPoint.Y = StartPoint.Y + (float)i;
                for (int j = 0; j < RoiW; j++)
                {
                    NowPoint.X = StartPoint.X + (float)j;
                    floatPoint p = Glovar.PointToRotatPoint(NowPoint, Ro);
                    points[j + i * RoiW] = p;
                }
            }
            return points;
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00009328 File Offset: 0x00007528
        public static floatPoint PointToRotatPoint(floatPoint pPoint, Mat Rotat)
        {
            floatPoint tPoint;
            tPoint.X = (float)(Rotat.Get<double>(0, 0) * (double)pPoint.X + Rotat.Get<double>(0, 1) * (double)pPoint.Y + Rotat.Get<double>(0, 2));
            tPoint.Y = (float)(Rotat.Get<double>(1, 0) * (double)pPoint.X + Rotat.Get<double>(1, 1) * (double)pPoint.Y + Rotat.Get<double>(1, 2));
            return tPoint;
        }

        // Token: 0x06000050 RID: 80 RVA: 0x000093A0 File Offset: 0x000075A0
        public static Point2d PointToRotatPoint(double X, double Y, Mat Rotat)
        {
            double a = Rotat.Get<double>(0, 0);
            double a2 = Rotat.Get<double>(0, 1);
            double a3 = Rotat.Get<double>(0, 2);
            double a4 = Rotat.Get<double>(1, 0);
            double a5 = Rotat.Get<double>(1, 1);
            double a6 = Rotat.Get<double>(1, 2);
            Point2d tPoint;
            tPoint.X = Rotat.Get<double>(0, 0) * X + Rotat.Get<double>(0, 1) * Y + Rotat.Get<double>(0, 2);
            tPoint.Y = Rotat.Get<double>(1, 0) * X + Rotat.Get<double>(1, 1) * Y + Rotat.Get<double>(1, 2);
            return tPoint;
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00009438 File Offset: 0x00007638
        public static object DeformatFile(string FileName)
        {
            object result;
            try
            {
                bool flag = !File.Exists(FileName);
                if (flag)
                {
                    result = null;
                }
                else
                {
                    bool flag2 = !(FileName != "");
                    if (flag2)
                    {
                        result = null;
                    }
                    else
                    {
                        Stream serializationStream = File.Open(FileName, FileMode.Open);
                        object obj = new BinaryFormatter().Deserialize(serializationStream);
                        serializationStream.Close();
                        result = obj;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = "载入异常";
                Exception ex2 = ex;
                MessageBox.Show(str + ((ex2 != null) ? ex2.ToString() : null));
                result = null;
            }
            return result;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x000094C8 File Offset: 0x000076C8
        public static string FormatFile(string FileName, object obj)
        {
            string result;
            try
            {
                bool flag = !(FileName != "");
                if (flag)
                {
                    result = "文件路径出错";
                }
                else
                {
                    Stream serializationStream = File.Open(FileName, FileMode.Create);
                    new BinaryFormatter().Serialize(serializationStream, obj);
                    serializationStream.Close();
                    result = "";
                }
            }
            catch (Exception ex)
            {
                string str = "保存异常";
                Exception ex2 = ex;
                MessageBox.Show(str + ((ex2 != null) ? ex2.ToString() : null));
                Exception ex3 = ex;
                result = (((ex3 != null) ? ex3.ToString() : null) ?? "");
            }
            return result;
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00009560 File Offset: 0x00007760
        public static double GetLineToXYAngle(PointF pt1, PointF pt2, bool IsX = true)
        {
            double angle;
            if (IsX)
            {
                angle = Math.Atan2((double)(pt2.Y - pt1.Y), (double)(pt2.X - pt1.X));
            }
            else
            {
                angle = Math.Atan2((double)(pt2.X - pt1.X), (double)(pt2.Y - pt1.Y));
            }
            return angle;
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000095CC File Offset: 0x000077CC
        public unsafe static Mat CutMat(Mat scr, System.Drawing.Point TopL, System.Drawing.Point RightB)
        {
            int R = RightB.X - TopL.X;
            int C = RightB.Y - TopL.Y;
            int rowsCount = R;
            int colsCount = C * scr.Channels();
            int AddY = scr.Width - RightB.X + TopL.X;
            Mat cut = new Mat(new OpenCvSharp.Size(R, C), MatType.CV_8UC1);
            int i = 0;
            byte[] Data = new byte[rowsCount * colsCount];
            byte* head = scr.DataPointer;
            head = head + TopL.Y * scr.Width + TopL.X;
            for (int Y = TopL.Y; Y < RightB.Y; Y++)
            {
                int X = TopL.X;
                while (X < RightB.X)
                {
                    Data[i] = (byte)(*head/20*20);
                    i++;
                    X++;
                    head++;
                }
                head += AddY;
            }
            Marshal.Copy((byte[])Data.Clone(), 0, cut.Data, Data.Length);
            return cut;
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000096F4 File Offset: 0x000078F4
        public unsafe static Mat CutColoImage_Points(Mat InputImage, floatPoint[,] Points)
        {
            Mat pMag = new Mat(new OpenCvSharp.Size(Points.GetLength(1), Points.GetLength(0)), InputImage.Type());
            byte* ptr = InputImage.DataPointer;
            byte* ptr2 = pMag.DataPointer;
            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for (int j = 0; j < Points.GetLength(1); j++)
                {
                    floatPoint point = Points[i, j];
                    int x = ((int)point.X >= 0) ? ((int)point.X) : 0;
                    int y = ((int)point.Y >= 0) ? ((int)point.Y) : 0;
                    int index = (InputImage.Width * y + x) * 3;
                    int index2 = (i * Points.GetLength(1) + j) * 3;
                    ptr2[index2] = ptr[index];
                    ptr2[index2 + 1] = ptr[index + 1];
                    ptr2[index2 + 2] = ptr[index + 2];
                }
            }
            return pMag;
        }

        // Token: 0x06000056 RID: 86 RVA: 0x00009804 File Offset: 0x00007A04
        public unsafe static Mat CutGrayImage_Points(Mat InputImage, floatPoint[,] Points)
        {
            Mat pMag = new Mat(new OpenCvSharp.Size(Points.GetLength(1), Points.GetLength(0)), InputImage.Type());
            byte* ptr = InputImage.DataPointer;
            byte* ptr2 = pMag.DataPointer;
            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for (int j = 0; j < Points.GetLength(1); j++)
                {
                    floatPoint point = Points[i, j];
                    int x = ((int)point.X >= 0) ? ((int)point.X) : 0;
                    int y = ((int)point.Y >= 0) ? ((int)point.Y) : 0;
                    int index = InputImage.Width * y + x;
                    int index2 = i * Points.GetLength(1) + j;
                    ptr2[index2] = ptr[index];
                }
            }
            return pMag;
        }

        // Token: 0x06000057 RID: 87 RVA: 0x000098E8 File Offset: 0x00007AE8
        public static floatPoint[,] PointsinRoi(Point2f cen, OpenCvSharp.Size RoiSize, double angle)
        {
            int W = RoiSize.Width;
            int H = RoiSize.Height;
            floatPoint[,] Points = new floatPoint[H, W];
            floatPoint NowPoint;
            NowPoint.X = cen.X - (float)(W / 2);
            NowPoint.Y = cen.Y - (float)(H / 2);
            Mat Rotat = Cv2.GetRotationMatrix2D(cen, angle, 1.0);
            floatPoint Now = Glovar.PointToRotatPoint(NowPoint, Rotat);
            NowPoint.X = cen.X + 1f;
            NowPoint.Y = cen.Y + 0f;
            floatPoint AddOneX = Glovar.PointToRotatPoint(NowPoint, Rotat);
            AddOneX.X -= cen.X;
            AddOneX.Y -= cen.Y;
            NowPoint.X = cen.X + 0f;
            NowPoint.Y = cen.Y + 1f;
            floatPoint AddOneY = Glovar.PointToRotatPoint(NowPoint, Rotat);
            AddOneY.X -= cen.X;
            AddOneY.Y -= cen.Y;
            floatPoint NowAPoint;
            NowAPoint.X = Now.X;
            NowAPoint.Y = Now.Y;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    NowPoint.X = NowAPoint.X + (float)i * AddOneY.X + (float)j * AddOneX.X;
                    NowPoint.Y = NowAPoint.Y + (float)i * AddOneY.Y + (float)j * AddOneX.Y;
                    Points[i, j] = NowPoint;
                }
            }
            return Points;
        }

        // Token: 0x06000058 RID: 88 RVA: 0x00009AB0 File Offset: 0x00007CB0
        public static Point2f GetPointForLinePoint(double[] Line, float X1, float Y1, out double[] ReastLine)
        {
            bool flag = Line[0] == 0.0;
            Point2f result;
            if (flag)
            {
                float Y2 = (float)(-(float)Line[2] / Line[1]);
                ReastLine = new double[]
                {
                    1.0,
                    0.0,
                    (double)X1
                };
                result = new Point2f(X1, Y2);
            }
            else
            {
                bool flag2 = Line[1] == 0.0;
                if (flag2)
                {
                    float X2 = (float)(-(float)Line[2] / Line[0]);
                    ReastLine = new double[]
                    {
                        1.0,
                        0.0,
                        (double)Y1
                    };
                    result = new Point2f(X2, Y1);
                }
                else
                {
                    bool flag3 = Line[0] * (double)X1 + Line[1] * (double)Y1 + Line[2] == 0.0;
                    if (flag3)
                    {
                        ReastLine = null;
                        result = new Point2f(X1, Y1);
                    }
                    else
                    {
                        double i = -Line[0] / Line[1];
                        double k = -1.0 / i;
                        double b = (double)Y1 - k * (double)X1;
                        ReastLine = new double[]
                        {
                            k,
                            -1.0,
                            b
                        };
                        string warning;
                        result = Glovar.GetPointForTwoLine(Line, ReastLine, out warning);
                    }
                }
            }
            return result;
        }

        // Token: 0x06000059 RID: 89 RVA: 0x00009BC4 File Offset: 0x00007DC4
        public static Point2f GetPointForTwoLine(double[] Line1, double[] Line2, out string warning)
        {
            warning = "";
            bool flag = Line1[0] != Line2[0] && Line1[1] != Line2[0];
            Point2f result;
            if (flag)
            {
                bool flag2 = Line1[0] == 0.0;
                double y;
                double x;
                if (flag2)
                {
                    y = -Line1[2] / Line1[1];
                    x = -(Line2[1] * y + Line2[2]) / Line2[0];
                }
                else
                {
                    bool flag3 = Line1[1] == 0.0;
                    if (flag3)
                    {
                        x = -Line1[2] / Line1[0];
                        y = (-Line2[0] * x - Line2[2]) / Line2[1];
                    }
                    else
                    {
                        bool flag4 = Line2[0] - Line1[0] * Line2[1] / Line1[1] != 0.0;
                        if (flag4)
                        {
                            x = (Line1[2] * Line2[1] / Line1[1] - Line2[2]) / (Line2[0] - Line1[0] * Line2[1] / Line1[1]);
                            y = (-Line1[2] - Line1[0] * x) / Line1[1];
                        }
                        else
                        {
                            bool flag5 = Line2[1] - Line2[0] * Line1[1] / Line1[0] != 0.0;
                            if (!flag5)
                            {
                                warning = "异常情况,未找到交点";
                                return new Point2f(0f, 0f);
                            }
                            y = (Line2[0] * Line1[2] / Line1[0] - Line2[2]) / (Line2[1] - Line2[0] * Line1[1] / Line1[0]);
                            x = (-Line1[1] * y - Line1[2]) / Line1[0];
                        }
                    }
                }
                result = new Point2f((float)x, (float)y);
            }
            else
            {
                warning = "两直线平行";
                result = new Point2f(0f, 0f);
            }
            return result;
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00009D50 File Offset: 0x00007F50
        public static double[] GetLineForTwoPoint(float X1, float Y1, float X2, float Y2)
        {
            bool flag = X1 == X2 && Y1 == Y2;
            double[] result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = X1 == X2;
                if (flag2)
                {
                    result = new double[]
                    {
                        1.0,
                        0.0,
                        (double)(-(double)X1)
                    };
                }
                else
                {
                    bool flag3 = Y1 == Y2;
                    if (flag3)
                    {
                        result = new double[]
                        {
                            0.0,
                            1.0,
                            (double)(-(double)Y1)
                        };
                    }
                    else
                    {
                        double K = (double)((Y1 - Y2) / (X1 - X2));
                        double B = (double)Y1 - K * (double)X1;
                        result = new double[]
                        {
                            K,
                            -1.0,
                            B
                        };
                    }
                }
            }
            return result;
        }
    }

    public class ToolBox
	{
        //模板匹配
		private int m_modelID = -1;
		private IntPtr pMatches   = IntPtr.Zero;
		public  Mat m_modelMat    = null;
		public  Mat m_maskMat     = null;
		public  Rect m_rectRoi    = new Rect(0, 0, 0, 0);
		public  Rect m_rectSearch = new Rect(0, 0, 0, 0);

        //圆卡尺变量
        public Mat m_circleCalliperImage = null;

        //直线卡尺
        public Mat m_lineCalliperImage = null;
        
        //畸变矫正
        public Mat m_cameraMatrix = null;
		public Mat m_distCoeffs = null;

		public paramMatchModel    m_paramMatchModel = new paramMatchModel();    //模板匹配参数
		public  paramGrab         m_paramGrab = new paramGrab();
		public  paramFindLine     m_paramFindCrossLine1 = new paramFindLine();   //找线参数
        public  paramFindLine     m_paramFindCrossLine2 = new paramFindLine();   //找线参数
        public  paramFindCircle   m_paramFindCircle = new paramFindCircle();//找圆参数
		public  paramFindRectangle2 m_paramFindRectangle2 = new paramFindRectangle2();//测量参数
		public  paramCheckBoard    m_paramCheckBoard = new paramCheckBoard();
        public paramDistance      m_paramDistance = new paramDistance();
        public void InitToolBox()
		{
			m_modelID = -1;
			pMatches = IntPtr.Zero;
			if (m_modelMat != null)
			{
				m_modelMat.Dispose();
				m_modelMat = null;
			}
			if (m_maskMat != null)
			{
				m_maskMat.Dispose();
				m_maskMat = null;
			}

            if (m_circleCalliperImage != null)
            {
                m_circleCalliperImage.Dispose();
                m_circleCalliperImage = null;
            }
     
            m_rectRoi = new Rect(0, 0, 0, 0);
			m_rectSearch = new Rect(0, 0, 0, 0);

			m_paramGrab.ImageChannel = enumImageChannel.MONO;
			m_paramGrab.dbShutter = 100;
			m_paramGrab.dbGain = 0;
			
			m_paramFindCircle.nCircleCalipersNum = 50;
			m_paramFindCircle.nCircleCalipersLength = 20;
			m_paramFindCircle.nCircleCalipersWidth = 10;
			m_paramFindCircle.nCircleCalipersThreshold = 10;
			m_paramFindCircle.dCircleX = 0;
			m_paramFindCircle.dCircleY = 0;
			m_paramFindCircle.dCircleR = 0;
            m_paramFindCircle.nCircleCalipersFilterSize = 2;
            m_paramFindCircle.nCircleCalipersIgnorePoints=0;
            m_paramFindCircle.enumCircleCalipersDirection =enumCaliperDirection.从内向外;
            m_paramFindCircle.enumCircleCalipersSearchModel = enumCaliperSearchModel.由明到暗;


            m_paramFindCrossLine1.nLineCalipersNum = 50;
			m_paramFindCrossLine1.nLineCalipersLength = 20;
			m_paramFindCrossLine1.nLineCalipersWidth = 10;
			m_paramFindCrossLine1.nLineCalipersThreshold = 10;
			m_paramFindCrossLine1.nLineCalipersMaxError = 10;
            m_paramFindCrossLine1.nLineCalipersIgnorePoints=0;
            m_paramFindCrossLine1.nLineCalipersFilterSize=2;
            m_paramFindCrossLine1.enumLineCalipersDirection = enumCaliperDirection.从内向外;
			m_paramFindCrossLine1.enumLineCalipersSearchModel = enumCaliperSearchModel.由明到暗;
			m_paramFindCrossLine1.dLineX1 = 0;
			m_paramFindCrossLine1.dLineY1 = 0;
			m_paramFindCrossLine1.dLineX2 = 0;
			m_paramFindCrossLine1.dLineY2 = 0;

            m_paramFindCrossLine2.nLineCalipersNum = 50;
            m_paramFindCrossLine2.nLineCalipersLength = 20;
            m_paramFindCrossLine2.nLineCalipersWidth = 10;
            m_paramFindCrossLine2.nLineCalipersThreshold = 10;
            m_paramFindCrossLine2.nLineCalipersMaxError = 10;
            m_paramFindCrossLine2.nLineCalipersIgnorePoints = 0;
            m_paramFindCrossLine2.nLineCalipersFilterSize = 2;
            m_paramFindCrossLine2.enumLineCalipersDirection = enumCaliperDirection.从内向外;
            m_paramFindCrossLine2.enumLineCalipersSearchModel = enumCaliperSearchModel.由明到暗;
            m_paramFindCrossLine2.dLineX1 = 0;
            m_paramFindCrossLine2.dLineY1 = 0;
            m_paramFindCrossLine2.dLineX2 = 0;
            m_paramFindCrossLine2.dLineY2 = 0;


            m_paramFindRectangle2.nRectCalipersNum = 50;
            m_paramFindRectangle2.nRectCalipersLength = 20;
            m_paramFindRectangle2.nRectCalipersWidth = 10;
            m_paramFindRectangle2.nRectCalipersThreshold = 10;
            m_paramFindRectangle2.nRectCalipersMaxError = 10;
            m_paramFindRectangle2.nRectCalipersIgnorePoints = 0;
            m_paramFindRectangle2.nRectCalipersFilterSize = 2;
            m_paramFindRectangle2.enumRectCalipersDirection = enumCaliperDirection.从内向外;
            m_paramFindRectangle2.enumRectCalipersSearchModel = enumCaliperSearchModel.由明到暗;
            m_paramFindRectangle2.dRectX = 0;
			m_paramFindRectangle2.dRectY = 0;
			m_paramFindRectangle2.dRectR = 0;
			m_paramFindRectangle2.dRectL1 = 0;
			m_paramFindRectangle2.dRectL2 = 0;

			m_paramMatchModel.fCModelStartAngle=-180;//模板起始角度
			m_paramMatchModel.fCModelEndAngle = 180;
			m_paramMatchModel.nCModelContrastLow=10;
			m_paramMatchModel.nCModelContrastHigh=20;
			m_paramMatchModel.nCModelMinContrast=3;
			m_paramMatchModel.nCNumLevels=0;//金字塔等级
			m_paramMatchModel.nCMinEdge=0;
			m_paramMatchModel.bIgnorePolarity=false;
			m_paramMatchModel.fFModelStartAngle=-180;//模板起始角度
			m_paramMatchModel.fFModelEndAngle=180;
			m_paramMatchModel.fFMinScale=1.0f;
			m_paramMatchModel.fFMaxScale=1.0f;
			m_paramMatchModel.fFMinScore=0.6f;
			m_paramMatchModel.nFNumMatchs=0;
			m_paramMatchModel.fFMaxOverlap = 0.5f ;
			m_paramMatchModel.fFGreediness=0.8f;
			m_paramMatchModel.nFEndNumLevels=3;//金字塔等级
			m_paramMatchModel.nFSubPix=2;//拟合类型

			m_paramCheckBoard.nCheckBoardWidth = 10;
			m_paramCheckBoard.nCheckBoardHeight = 10;
			m_paramCheckBoard.nCornerPixlSize = 11;
			m_paramCheckBoard.fCheckerBoardDis = 30.0f;


            m_paramDistance.nDistanceCalipersThreshold=10;
            m_paramDistance.nDistanceCalipersFilterSize=2;
            m_paramDistance.enumDistanceCalipersStartSearchNumber=enumCaliperSearchNumber.第一个;
            m_paramDistance.enumDistanceCalipersStartSearchModel=enumCaliperSearchModel.由暗到明;
            m_paramDistance.enumDistanceCalipersEndSearchNumber = enumCaliperSearchNumber.第一个;
            m_paramDistance.enumDistanceCalipersEndSearchModel = enumCaliperSearchModel.由暗到明;

        }
		public bool WritePrjParam(string szDirName)
        {
			try
			{
				string szCurDir = szDirName;
				if (!Directory.Exists(szDirName))
				{
					Directory.CreateDirectory(szDirName);
				}

				string filename = szDirName + ".prj";
				csBinaryReaderWriter.OpenWriter(filename);
				csBinaryReaderWriter.Write<paramGrab>(m_paramGrab);
				csBinaryReaderWriter.Write<paramFindLine>(m_paramFindCrossLine1);
                csBinaryReaderWriter.Write<paramFindLine>(m_paramFindCrossLine2);
                csBinaryReaderWriter.Write<paramFindCircle>(m_paramFindCircle);
				csBinaryReaderWriter.Write<paramFindRectangle2>(m_paramFindRectangle2);
				csBinaryReaderWriter.Write<paramMatchModel>(m_paramMatchModel);
				csBinaryReaderWriter.Write<Rect>(m_rectRoi);
				csBinaryReaderWriter.Write<Rect>(m_rectSearch);
                csBinaryReaderWriter.Write<paramCheckBoard>( m_paramCheckBoard);
                csBinaryReaderWriter.CloseWriter();




				string maskImage  = szCurDir + "\\model_mask.bmp";//数据
				string model      = szCurDir + "\\model.mod";//模板
				string modelImage = szCurDir + "\\model_image.bmp";//图片

				if(m_modelMat!=null)
                {
					m_modelMat.ToBitmap().Save(modelImage, ImageFormat.Bmp);
				}
				if (m_maskMat != null)
				{
					m_maskMat.ToBitmap().Save(maskImage, ImageFormat.Bmp);
				}


                string circleCalliperImage = szCurDir + "\\circleCalliperImage.bmp";//图片
                if (m_circleCalliperImage != null)
                {
                    m_circleCalliperImage.ToBitmap().Save(circleCalliperImage, ImageFormat.Bmp);
                }
                

                string matFile = szCurDir + "\\distor.xml";//数据
                FileStorage fileOut = new FileStorage(matFile, FileStorage.Modes.Write);
                fileOut.Write("m_cameraMatrix", m_cameraMatrix);
                fileOut.Write("m_distCoeffs", m_distCoeffs);
                fileOut.Release();

            }
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
		public bool ReadPrjParam(string szDirName)
        {
			try
			{
				string szCurDir = szDirName;
				if (!Directory.Exists(szDirName))
				{
					return false;
				}
				if (!Directory.Exists(szCurDir))
				{
					return false;
				}

				string filename = szCurDir + ".prj";
				csBinaryReaderWriter.OpenReader(filename);
				csBinaryReaderWriter.Read<paramGrab>(ref m_paramGrab);
                csBinaryReaderWriter.Read<paramFindLine>(ref m_paramFindCrossLine1);
                csBinaryReaderWriter.Read<paramFindLine>(ref m_paramFindCrossLine2);
                csBinaryReaderWriter.Read<paramFindCircle>(ref m_paramFindCircle);
				csBinaryReaderWriter.Read<paramFindRectangle2>(ref m_paramFindRectangle2);
				csBinaryReaderWriter.Read<paramMatchModel>(ref m_paramMatchModel);
				csBinaryReaderWriter.Read<Rect>(ref m_rectRoi);
				csBinaryReaderWriter.Read<Rect>(ref m_rectSearch);
                csBinaryReaderWriter.Read<paramCheckBoard>(ref m_paramCheckBoard);
                csBinaryReaderWriter.CloseReader();


				string maskImage = szCurDir + "\\model_mask.bmp";//数据
				string model = szCurDir + "\\model.mod";//模板
				string modelImage = szCurDir + "\\model_image.bmp";//图片

				if(m_modelMat!=null)
                {
					m_modelMat.Dispose();
					m_modelMat = null;

				}
				if (m_maskMat != null)
				{
					m_maskMat.Dispose();
					m_maskMat = null;
				}

				m_modelMat = Cv2.ImRead(modelImage, ImreadModes.Grayscale);
				m_maskMat = Cv2.ImRead(maskImage, ImreadModes.Grayscale);


                //圆卡尺
                string circleCalliperImage = szCurDir + "\\circleCalliperImage.bmp";//图片
                if (m_circleCalliperImage != null)
                {
                    m_circleCalliperImage.Dispose();
                    m_circleCalliperImage = null;

                }
                m_circleCalliperImage = Cv2.ImRead(circleCalliperImage, ImreadModes.Grayscale);

                string matFile = szCurDir + "\\distor.xml";//数据
                FileStorage fileOut = new FileStorage(matFile, FileStorage.Modes.Read);
                m_cameraMatrix = fileOut["m_cameraMatrix"].ToMat().Clone();
                m_distCoeffs = fileOut["m_distCoeffs"].ToMat().Clone();
                fileOut.Release();

            }
			catch (Exception ex)
			{
				return false;
			}
		
			return m_modelID>=0;
		}
	}
}
