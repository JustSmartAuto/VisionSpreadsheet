using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Tool_View;
using System.IO;
using static OpencvDemo.ToolBox;
using Display;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace OpencvDemo
{
    public partial class Form1 : Form
    {
        private ToolBox m_ToolBox = new ToolBox();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboSubPixel.SelectedIndex = 2;
            combCircleCalipersPolarity.SelectedIndex = 0;
            combCircleCalipersDir.SelectedIndex = 0;
            combLine1CalipersDir.SelectedIndex = 0;
            combLine1CalipersPolarity.SelectedIndex = 0;
            combLine2CalipersDir.SelectedIndex = 0;
            combLine2CalipersPolarity.SelectedIndex = 0;
            combRectCalipersDir.SelectedIndex = 0;
            combRectCalipersPolarity.SelectedIndex = 0;
            combStartCalipersSearchMode.SelectedIndex = 0;
            combStartCalipersSearchNumber.SelectedIndex = 0;
            combEndCalipersSearchMode.SelectedIndex = 0;
            combEndCalipersSearchNumber.SelectedIndex = 0;
            m_ToolBox.InitToolBox();
        }
        private void btnOpenPrj_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "工程 文件 | *.prj";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_szPrjName = openFileDialog.FileName;
                string szDir = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                if (Directory.Exists(szDir))
                {
                    if (szDir.Substring(szDir.Length - 2) != "\\")
                    {
                        szDir += "\\";
                    }
                    string str = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    m_ToolBox.ReadPrjParam(szDir + str);

                    //卡尺参数


                    numUDCircleCalipersIgnoreDots.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersIgnorePoints);
                    numUDCircleCalipersLength.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersLength);
                    numUDCircleCalipersNum.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersNum);
                    numUDCircleCalipersThreshold.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersThreshold);
                    numUDCircleCalipersWidth.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersWidth);
                    numUDCircleCalipersFiterSize.Value = Convert.ToInt32(m_ToolBox.m_paramFindCircle.nCircleCalipersFilterSize);
                    combCircleCalipersDir.SelectedIndex = (short)m_ToolBox.m_paramFindCircle.enumCircleCalipersDirection;
                    combCircleCalipersPolarity.SelectedIndex = (short)m_ToolBox.m_paramFindCircle.enumCircleCalipersSearchModel;


                    //直线卡尺参数
                    numUDLine1CalipersIgnoreDots.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersIgnorePoints);
                    numUDLine1CalipersLength.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersLength);
                    numUDLine1CalipersNum.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersNum);
                    numUDLine1CalipersThreshold.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersThreshold);
                    numUDLine1CalipersWidth.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersWidth);
                    numUDLine1CalipersFiterSize.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersFilterSize);
                    numUDLine1CalipersMaxError.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine1.nLineCalipersMaxError);
                    combLine1CalipersDir.SelectedIndex = (short)m_ToolBox.m_paramFindCrossLine1.enumLineCalipersDirection;
                    combLine1CalipersPolarity.SelectedIndex = (short)m_ToolBox.m_paramFindCrossLine1.enumLineCalipersSearchModel;

                    numUDLine2CalipersIgnoreDots.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersIgnorePoints);
                    numUDLine2CalipersLength.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersLength);
                    numUDLine2CalipersNum.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersNum);
                    numUDLine2CalipersThreshold.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersThreshold);
                    numUDLine2CalipersWidth.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersWidth);
                    numUDLine2CalipersFiterSize.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersFilterSize);
                    numUDLine2CalipersMaxError.Value = Convert.ToInt32(m_ToolBox.m_paramFindCrossLine2.nLineCalipersMaxError);
                    combLine2CalipersDir.SelectedIndex = (short)m_ToolBox.m_paramFindCrossLine2.enumLineCalipersDirection;
                    combLine2CalipersPolarity.SelectedIndex = (short)m_ToolBox.m_paramFindCrossLine2.enumLineCalipersSearchModel;

                    if (m_ToolBox.m_circleCalliperImage != null)
                    {
                        m_lineCalliperImage = m_ToolBox.m_lineCalliperImage.Clone();
                        u_DisPlay3.Image = m_lineCalliperImage;
                    }

                    //矩形卡尺
                    numUDRectCalipersIgnoreDots.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersIgnorePoints);
                    numUDRectCalipersLength.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersLength);
                    numUDRectCalipersNum.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersNum);
                    numUDRectCalipersThreshold.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersThreshold);
                    numUDRectCalipersWidth.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersWidth);
                    numUDRectCalipersFiterSize.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersFilterSize);
                    numUDRectCalipersMaxError.Value = Convert.ToInt32(m_ToolBox.m_paramFindRectangle2.nRectCalipersMaxError);
                    combRectCalipersDir.SelectedIndex = (short)m_ToolBox.m_paramFindRectangle2.enumRectCalipersDirection;
                    combRectCalipersPolarity.SelectedIndex = (short)m_ToolBox.m_paramFindRectangle2.enumRectCalipersSearchModel;


                    //2.畸变参数


                    numUDCheckerBoardWidth.Value = Convert.ToInt32(m_ToolBox.m_paramCheckBoard.nCheckBoardWidth);
                    numUDCheckerBoardHeight.Value = Convert.ToInt32(m_ToolBox.m_paramCheckBoard.nCheckBoardHeight);
                    numUDCheckerBoardDis.Value = Convert.ToDecimal(m_ToolBox.m_paramCheckBoard.fCheckerBoardDis);
                    numUDCornerPixlSize.Value = Convert.ToInt32(m_ToolBox.m_paramCheckBoard.nCornerPixlSize);


                    //卡尺测距
                    numUDDistanceCalipersThreshold.Value = Convert.ToInt32(m_ToolBox.m_paramDistance.nDistanceCalipersThreshold);
                    numUDDistanceCalipersFiterSize.Value = Convert.ToInt32(m_ToolBox.m_paramDistance.nDistanceCalipersFilterSize);
                    combStartCalipersSearchNumber.SelectedIndex = (short)m_ToolBox.m_paramDistance.enumDistanceCalipersStartSearchNumber;
                    combStartCalipersSearchMode.SelectedIndex = (short)m_ToolBox.m_paramDistance.enumDistanceCalipersStartSearchModel;

                    combEndCalipersSearchNumber.SelectedIndex = (short)m_ToolBox.m_paramDistance.enumDistanceCalipersEndSearchNumber;
                    combEndCalipersSearchMode.SelectedIndex = (short)m_ToolBox.m_paramDistance.enumDistanceCalipersEndSearchModel;

                }
            }
        }
        private void btnSavePrj_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Filter = "工程 文件 | *.prj";
            saveImageDialog.FileName = m_szPrjName;
            saveImageDialog.RestoreDirectory = true;
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                string szDir = Path.GetDirectoryName(saveImageDialog.FileName);
                if (!Directory.Exists(szDir))
                {
                    Directory.CreateDirectory(szDir);
                }

                if (szDir.Substring(szDir.Length - 2) != "\\")
                {
                    szDir += "\\";
                }
                string str = Path.GetFileNameWithoutExtension(saveImageDialog.FileName);

               
                //圆卡尺参数


                m_ToolBox.m_paramFindCircle.nCircleCalipersIgnorePoints = Convert.ToInt32(numUDCircleCalipersIgnoreDots.Value);
                m_ToolBox.m_paramFindCircle.nCircleCalipersLength = Convert.ToInt32(numUDCircleCalipersLength.Value);
                m_ToolBox.m_paramFindCircle.nCircleCalipersNum = Convert.ToInt32(numUDCircleCalipersNum.Value);
                m_ToolBox.m_paramFindCircle.nCircleCalipersThreshold = Convert.ToInt32(numUDCircleCalipersThreshold.Value);
                m_ToolBox.m_paramFindCircle.nCircleCalipersWidth = Convert.ToInt32(numUDCircleCalipersWidth.Value);
                m_ToolBox.m_paramFindCircle.nCircleCalipersFilterSize = Convert.ToInt32(numUDCircleCalipersFiterSize.Value);
                m_ToolBox.m_paramFindCircle.enumCircleCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combCircleCalipersDir.SelectedIndex.ToString());
                m_ToolBox.m_paramFindCircle.enumCircleCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combCircleCalipersPolarity.SelectedIndex.ToString());


                //直线卡尺
                if (m_lineCalliperImage != null)
                {
                    m_ToolBox.m_lineCalliperImage = m_lineCalliperImage.Clone();
                }

                m_ToolBox.m_paramFindCrossLine1.nLineCalipersIgnorePoints = Convert.ToInt32(numUDLine1CalipersIgnoreDots.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersLength = Convert.ToInt32(numUDLine1CalipersLength.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersNum = Convert.ToInt32(numUDLine1CalipersNum.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersThreshold = Convert.ToInt32(numUDLine1CalipersThreshold.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersWidth = Convert.ToInt32(numUDLine1CalipersWidth.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersFilterSize = Convert.ToInt32(numUDLine1CalipersFiterSize.Value);
                m_ToolBox.m_paramFindCrossLine1.nLineCalipersMaxError = Convert.ToInt32(numUDLine1CalipersMaxError.Value);
                m_ToolBox.m_paramFindCrossLine1.enumLineCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combLine1CalipersDir.SelectedIndex.ToString());
                m_ToolBox.m_paramFindCrossLine1.enumLineCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combLine1CalipersPolarity.SelectedIndex.ToString());

                m_ToolBox.m_paramFindCrossLine2.nLineCalipersIgnorePoints = Convert.ToInt32(numUDLine2CalipersIgnoreDots.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersLength = Convert.ToInt32(numUDLine2CalipersLength.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersNum = Convert.ToInt32(numUDLine2CalipersNum.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersThreshold = Convert.ToInt32(numUDLine2CalipersThreshold.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersWidth = Convert.ToInt32(numUDLine2CalipersWidth.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersFilterSize = Convert.ToInt32(numUDLine2CalipersFiterSize.Value);
                m_ToolBox.m_paramFindCrossLine2.nLineCalipersMaxError = Convert.ToInt32(numUDLine2CalipersMaxError.Value);
                m_ToolBox.m_paramFindCrossLine2.enumLineCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combLine2CalipersDir.SelectedIndex.ToString());
                m_ToolBox.m_paramFindCrossLine2.enumLineCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combLine2CalipersPolarity.SelectedIndex.ToString());

                //矩形卡尺

                m_ToolBox.m_paramFindRectangle2.nRectCalipersNum = Convert.ToInt32(numUDRectCalipersNum.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersLength = Convert.ToInt32(numUDRectCalipersLength.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersWidth = Convert.ToInt32(numUDRectCalipersWidth.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersThreshold = Convert.ToInt32(numUDRectCalipersThreshold.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersMaxError = Convert.ToInt32(numUDRectCalipersMaxError.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersIgnorePoints = Convert.ToInt32(numUDRectCalipersIgnoreDots.Value);
                m_ToolBox.m_paramFindRectangle2.nRectCalipersFilterSize = Convert.ToInt32(numUDRectCalipersFiterSize.Value);
                m_ToolBox.m_paramFindRectangle2.enumRectCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combRectCalipersDir.SelectedIndex.ToString());
                m_ToolBox.m_paramFindRectangle2.enumRectCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combRectCalipersPolarity.SelectedIndex.ToString());
                m_ToolBox.m_paramFindRectangle2.dRectX = 0;
                m_ToolBox.m_paramFindRectangle2.dRectY = 0;
                m_ToolBox.m_paramFindRectangle2.dRectR = 0;
                m_ToolBox.m_paramFindRectangle2.dRectL1 = 0;
                m_ToolBox.m_paramFindRectangle2.dRectL2 = 0;

          
                //2.畸变参数
               

                m_ToolBox.m_paramCheckBoard.nCheckBoardWidth = Convert.ToInt32(numUDCheckerBoardWidth.Value);
                m_ToolBox.m_paramCheckBoard.nCheckBoardHeight = Convert.ToInt32(numUDCheckerBoardHeight.Value);
                m_ToolBox.m_paramCheckBoard.fCheckerBoardDis = Convert.ToSingle(numUDCheckerBoardDis.Value);
                m_ToolBox.m_paramCheckBoard.nCornerPixlSize = Convert.ToInt32(numUDCornerPixlSize.Value);

                //卡尺测距
                m_ToolBox.m_paramDistance.nDistanceCalipersThreshold = Convert.ToInt32(numUDDistanceCalipersThreshold.Value);
                m_ToolBox.m_paramDistance.nDistanceCalipersFilterSize = Convert.ToInt32(numUDDistanceCalipersFiterSize.Value);
                m_ToolBox.m_paramDistance.enumDistanceCalipersStartSearchNumber = (enumCaliperSearchNumber)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchNumber), combStartCalipersSearchNumber.SelectedIndex.ToString());
                m_ToolBox.m_paramDistance.enumDistanceCalipersStartSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combStartCalipersSearchMode.SelectedIndex.ToString());
                m_ToolBox.m_paramDistance.enumDistanceCalipersEndSearchNumber = (enumCaliperSearchNumber)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchNumber), combEndCalipersSearchNumber.SelectedIndex.ToString());
                m_ToolBox.m_paramDistance.enumDistanceCalipersEndSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combEndCalipersSearchMode.SelectedIndex.ToString());

                m_ToolBox.WritePrjParam(szDir + str);
            }
        }


        private string m_szPrjName = "";

        #region 直线卡尺

        private Mat m_lineCalliperImage = null;
        private CvDisplayGraphicsCalipersLine m_line1Caliper = null;
        private CvDisplayGraphicsCalipersLine m_line2Caliper = null;

        public  bool calcLinePara(List<floatPoint> pts, ref double a, ref double b, ref double c, ref double res)
        {
            res = 0.0;
            List<floatPoint> ptsF = new List<floatPoint>();
            for (int i = 0; i < pts.Count; i++)
            {
                ptsF.Add(pts[i]);
            }
            double[] ReastLine = new double[3];
            bool flag = FitLine(ptsF, out ReastLine);
            bool result;
            if (flag)
            {
                a = ReastLine[0];
                b = ReastLine[1];
                c = ReastLine[2];
                for (int j = 0; j < pts.Count; j++)
                {
                    res += Math.Abs((double)pts[j].X * a + (double)pts[j].Y * b + c);
                }
                res /= (double)pts.Count;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public  bool fitline(List<floatPoint> ptSet, int errorCount, double MaxError, out List<floatPoint> UsedPont, out List<floatPoint> ErrorPoint, out double[] ReastLine)
        {
            double a = 0.0;
            double b = 0.0;
            double c = 0.0;
            double Mina = 0.0;
            double Minb = 0.0;
            double Minc = 0.0;
            int NeddCount = ptSet.Count - errorCount;
            UsedPont = new List<floatPoint>();
            ErrorPoint = new List<floatPoint>();
            ReastLine = new double[3];
            int sample_count = 0;
            int N = ptSet.Count * ptSet.Count;
            double res = 0.0;
            bool stop_loop = false;
            double MinRMS = 9999.0;
            List<bool> inlierFlag = new List<bool>();
            List<double> resids_ = new List<double>();
            List<int> ptsID = new List<int>();
            bool result;
            try
            {
                for (int i = 0; i < ptSet.Count; i++)
                {
                    ptsID.Add(i);
                    inlierFlag.Add(false);
                    resids_.Add(3.0);
                }
                int a2 = 0;
                int a4 = 1;
                while (N > sample_count && !stop_loop)
                {
                    List<bool> inliterstemp = new List<bool>();
                    List<double> residualstemp = new List<double>();
                    List<int> ptss = new List<int>();
                    List<floatPoint> pt_sam = new List<floatPoint>();
                    pt_sam.Add(ptSet[a2]);
                    pt_sam.Add(ptSet[a4]);
                    bool flag = a4 == ptSet.Count - 1;
                    if (flag)
                    {
                        bool flag2 = a2 == ptSet.Count - 2;
                        if (flag2)
                        {
                            stop_loop = true;
                            continue;
                        }
                        a2++;
                        a4 = a2 + 1;
                    }
                    else
                    {
                        a4++;
                    }
                    calcLinePara(pt_sam, ref a, ref b, ref c, ref res);
                    List<double> RMSList = new List<double>();
                    for (int j = 0; j < ptSet.Count; j++)
                    {
                        floatPoint pt = ptSet[j];
                        double resid_ = Math.Abs((double)pt.X * a + (double)pt.Y * b + c);
                        residualstemp.Add(resid_);
                        inliterstemp.Add(false);
                        RMSList.Add(resid_);
                    }
                    RMSList.Sort();
                    double RMS = 0.0;
                    for (int RmsCount = 0; RmsCount < NeddCount; RmsCount++)
                    {
                        RMS = RMSList[RmsCount] + RMS;
                    }
                    bool flag3 = RMS <= MinRMS;
                    if (flag3)
                    {
                        MinRMS = RMS;
                        Mina = a;
                        Minb = b;
                        Minc = c;
                    }
                    bool flag4 = MinRMS < 0.5 * (double)NeddCount;
                    if (flag4)
                    {
                        N = 10;
                    }
                    sample_count++;
                }
                bool flag5 = MinRMS == 9999.0;
                if (flag5)
                {
                    result = false;
                }
                else
                {
                    List<Info> resid_All = new List<Info>();
                    for (int k = 0; k < ptSet.Count; k++)
                    {
                        resid_All.Add(new Info(k, Math.Abs((double)ptSet[k].X * Mina + (double)ptSet[k].Y * Minb + Minc)));
                    }
                    List<Info> ReastAll = (from a3 in resid_All
                                           orderby a3.Value descending
                                           select a3).ToList<Info>();
                    for (int l = 0; l < ptSet.Count; l++)
                    {
                        bool flag6 = l < errorCount;
                        if (flag6)
                        {
                            ErrorPoint.Add(ptSet[ReastAll[l].No]);
                        }
                        else
                        {
                            UsedPont.Add(ptSet[ReastAll[l].No]);
                        }
                    }
                    bool flag7 = FitLine(UsedPont, out ReastLine);
                    if (flag7)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public  bool FitLine(List<floatPoint> ReastPoints, out double[] FindLine)
        {
            FindLine = new double[3];
            bool flag = ReastPoints.Count < 2;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                double averagex = 0.0;
                double averagey = 0.0;
                foreach (floatPoint p in ReastPoints)
                {
                    averagex += (double)p.X;
                    averagey += (double)p.Y;
                }
                averagex /= (double)ReastPoints.Count;
                averagey /= (double)ReastPoints.Count;
                double sXX = 0.0;
                double sYY = 0.0;
                double sXY = 0.0;
                foreach (floatPoint p2 in ReastPoints)
                {
                    sXY += ((double)p2.X - averagex) * ((double)p2.Y - averagey);
                    sXX += ((double)p2.X - averagex) * ((double)p2.X - averagex);
                    sYY += ((double)p2.Y - averagey) * ((double)p2.Y - averagey);
                }
                bool isVertical = sXY == 0.0 && sXX < sYY;
                bool isHorizontal = sXY == 0.0 && sXX > sYY;
                bool isIndeterminate = sXY == 0.0 && sXX == sYY;
                bool flag2 = isVertical;
                if (flag2)
                {
                    FindLine = new double[]
                    {
                        1.0,
                        0.0,
                        averagex
                    };
                    result = true;
                }
                else
                {
                    bool flag3 = isHorizontal;
                    if (flag3)
                    {
                        FindLine = new double[]
                        {
                            0.0,
                            1.0,
                            averagex
                        };
                        result = true;
                    }
                    else
                    {
                        bool flag4 = isIndeterminate;
                        if (flag4)
                        {
                            FindLine = new double[]
                            {
                                double.NaN,
                                double.NaN,
                                double.NaN
                            };
                            result = false;
                        }
                        else
                        {
                            double slope = (sYY - sXX + Math.Sqrt((sYY - sXX) * (sYY - sXX) + 4.0 * sXY * sXY)) / (2.0 * sXY);
                            double intercept = averagey - slope * averagex;
                            double normFactor = ((intercept >= 0.0) ? 1.0 : -1.0) * Math.Sqrt(slope * slope + 1.0);
                            FindLine[0] = -slope / normFactor;
                            FindLine[1] = 1.0 / normFactor;
                            double C = 0.0;
                            foreach (floatPoint p3 in ReastPoints)
                            {
                                C = C - FindLine[0] * (double)p3.X - FindLine[1] * (double)p3.Y;
                            }
                            FindLine[2] = C / (double)ReastPoints.Count;
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public  bool ClearErrorPointLine(List<floatPoint> RestPoint, int ClearNum, out List<floatPoint> UsedPont, out List<floatPoint> ErrorPoint, out double[] ReastLine)
        {
            UsedPont = new List<floatPoint>();
            ErrorPoint = new List<floatPoint>();
            ReastLine = new double[3];
            bool flag = FitLine(RestPoint, out ReastLine);
            bool result;
            if (flag)
            {
                bool flag2 = ClearNum + 2 >= RestPoint.Count;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    List<double> ErrorY = new List<double>();
                    List<double> ErrorMax = new List<double>();
                    for (int i = 0; i < RestPoint.Count; i++)
                    {
                        ErrorY.Add(Math.Abs(ReastLine[0] * (double)RestPoint[i].X + ReastLine[1] * (double)RestPoint[i].Y + ReastLine[2]) / Math.Sqrt(ReastLine[0] * ReastLine[0] + ReastLine[1] * ReastLine[1]));
                    }
                    for (int j = 0; j < ClearNum; j++)
                    {
                        int num = ErrorY.IndexOf(ErrorY.Max());
                        ErrorMax.Add((double)num);
                        ErrorY[num] = 0.0;
                    }
                    for (int k = 0; k < RestPoint.Count; k++)
                    {
                        bool flag3 = ErrorMax.Contains((double)k);
                        if (flag3)
                        {
                            ErrorPoint.Add(RestPoint[k]);
                        }
                        else
                        {
                            UsedPont.Add(RestPoint[k]);
                        }
                    }
                    result = FitLine(UsedPont, out ReastLine);
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public  floatPoint GetReast(paramFindLine paramFL, List<int> peaks, Mat filteredMap)
        {
            float dist = 0f;
            bool found = false;
            bool flag = paramFL.enumLineCalipersSearchModel == enumCaliperSearchModel.所有;
            if (flag)
            {
                float maxDist = 0f;
                int maxPos = 0;
                found = true;
                for (int peek = 0; peek < peaks.Count; peek++)
                {
                    bool flag2 = Math.Abs(filteredMap.Get<float>(0, peaks[peek])) > maxDist;
                    if (flag2)
                    {
                        maxDist = Math.Abs(filteredMap.Get<float>(0, peaks[peek]));
                        maxPos = peaks[peek];
                    }
                }
                dist = (float)maxPos;
            }
            else
            {
                bool flag3 = paramFL.enumLineCalipersSearchModel == enumCaliperSearchModel.由明到暗;
                if (flag3)
                {
                    int maxPos2 = 0;
                    for (int peek2 = 0; peek2 < peaks.Count; peek2++)
                    {
                        double a = filteredMap.Get<double>(0, peaks[peek2]);
                        bool flag4 = filteredMap.Get<float>(0, peaks[peek2]) > 0f;
                        if (flag4)
                        {
                            maxPos2 = peaks[peek2];
                            found = true;
                            break;
                        }
                    }
                    dist = (float)maxPos2;
                }
                else
                {
                    bool flag5 = paramFL.enumLineCalipersSearchModel == enumCaliperSearchModel.由暗到明;
                    if (flag5)
                    {
                        int maxPos3 = 0;
                        for (int peek3 = 0; peek3 < peaks.Count; peek3++)
                        {
                            bool flag6 = filteredMap.Get<float>(0, peaks[peek3]) < 0f;
                            if (flag6)
                            {
                                maxPos3 = peaks[peek3];
                                found = true;
                                break;
                            }
                        }
                        dist = (float)maxPos3;
                    }
                }
            }
            floatPoint Reast;
            Reast.X = -1f;
            Reast.Y = -1f;
            bool flag7 = found;
            floatPoint result;
            if (flag7)
            {
                int i = (int)dist;
                int j = (i - 1 < 0) ? i : (i - 1);
                int r = (i + 1 >= paramFL.nLineCalipersLength) ? i : (i + 1);
                double a2 = (double)filteredMap.Get<float>(0, j);
                double b = (double)filteredMap.Get<float>(0, i);
                double c = (double)filteredMap.Get<float>(0, r);
                double u = 0.5 * (a2 - c) / (a2 - b - b + c);
                Reast.X = 0f;
                Reast.Y = (float)((double)dist + u);
                result = Reast;
            }
            else
            {
                result = Reast;
            }
            return result;
        }
        public List<int> FindPeak(int iSize, Mat filteredMap, float fAmpThreshold)
        {
            List<int> sign = new List<int>();
            List<int> peaks = new List<int>();
            for (int i = 1; i < iSize; i++)
            {
                float diff = filteredMap.Get<float>(0, i) - filteredMap.Get<float>(0, i - 1);
                bool flag = diff < 0f;
                if (flag)
                {
                    sign.Add(-1);
                }
                else
                {
                    bool flag2 = diff > 0f;
                    if (flag2)
                    {
                        sign.Add(1);
                    }
                    else
                    {
                        sign.Add(0);
                    }
                }
            }
            for (int j = 1; j < sign.Count; j++)
            {
                int diff2 = sign[j] - sign[j - 1];
                float val = Math.Abs(filteredMap.Get<float>(0, j));
                bool flag3 = diff2 != 0 && Math.Abs(filteredMap.Get<float>(0, j)) > fAmpThreshold * 3;
                if (flag3)
                {
                    peaks.Add(j);
                }
            }
            return peaks;
        }
        public unsafe  bool FindLine(Mat img, CvDisplayGraphicsCalipersLine Line, paramFindLine paramFL, out double[] Reast, out List<floatPoint> UsedPont, out List<floatPoint> ErrorPoint)
        {
            Reast = null;
            UsedPont = null;
            ErrorPoint = null;
            bool result;
            try
            {
                UsedPont = new List<floatPoint>();
                ErrorPoint = new List<floatPoint>();
                List<floatPoint> ReastPoints = new List<floatPoint>();
                Mat whalf = new Mat(new OpenCvSharp.Size(2 * paramFL.nLineCalipersFilterSize + 1, 1), MatType.CV_32FC1);
                for (int i = 0; i < paramFL.nLineCalipersFilterSize; i++)
                {
                    whalf.Set<float>(0, i, 1f);
                    whalf.Set<float>(0, 2 * paramFL.nLineCalipersFilterSize - i, -1f);
                }
                whalf.Set<float>(0, paramFL.nLineCalipersFilterSize, 0f);
                RotatedRect ROI = new RotatedRect(new Point2f((float)Line.Start.X, (float)Line.Start.Y), new Size2f((float)Line.W, (float)Line.H), (float)Line.Angle);
                float IntervalW = (float)((Line.End.X - Line.Start.X) / ((double)Line.Number - 1.0));
                float IntervalH = (float)((Line.End.Y - Line.Start.Y) / ((double)Line.Number - 1.0));
                for (int num = 0; num < Line.Number; num++)
                {
                    ROI.Center = new Point2f((float)Line.Start.X + (float)num * IntervalW, (float)Line.Start.Y + (float)num * IntervalH);
                    ROI.Angle = Convert.ToSingle(Math.Atan2(Line.End.Y - Line.Start.Y, Line.End.X - Line.Start.X) * 180.0 / 3.141592653589793);
                    Mat Rotat = Cv2.GetRotationMatrix2D(ROI.Center, (double)(-(double)ROI.Angle), 1.0);
                    floatPoint[] NumPoints = Glovar.GetRectanglePoint(Rotat, ROI.Center, Line.W, Line.H);
                    Mat pMag = new Mat(new OpenCvSharp.Size(paramFL.nLineCalipersLength, paramFL.nLineCalipersWidth), MatType.CV_32FC1);
                    byte* head = img.DataPointer;
                    int c = img.Channels();
                    for (int j = 0; j < paramFL.nLineCalipersLength; j++)
                    {
                        for (int k = 0; k < paramFL.nLineCalipersWidth; k++)
                        {
                            int Length = k + j * paramFL.nLineCalipersWidth;
                            floatPoint point = NumPoints[Length];
                            int x = (int)Math.Round((double)point.X);
                            int y = (int)Math.Round((double)point.Y);
                            float u = (float)Math.Abs((double)point.X - ((double)((float)x) + 0.5));
                            float v = (float)Math.Abs((double)point.Y - ((double)((float)y) - 0.5));
                            bool flag = img.Width < x + 1 || img.Height < y + 1 || x < 1 || y < 1;
                            if (flag)
                            {
                                return false;
                            }
                            bool flag2 = paramFL.enumLineCalipersDirection == enumCaliperDirection.从内向外;
                            if (flag2)
                            {
                                byte a = (byte)(*head / 20 * 20);
                                byte b = img.Get<byte>(0, 0);
                                byte* now = head + ((y - 1) * img.Width + x - 1);
                                byte* now2 = now + 1;
                                byte* now3 = now + img.Width;
                                byte* now4 = now2 + img.Width;
                                pMag.Set<float>(k, j, (1f - v) * ((float)(*now2 / 20 * 20) * (1f - u) + (float)(*now / 20 * 20) * u) + v * ((float)(*now4 / 20 * 20) * (1f - u) + (float)(*now3 / 20 * 20) * u));
                            }
                            else
                            {
                                byte* now5 = head + ((y - 1) * img.Width + x - 1);
                                byte* now6 = now5 + 1;
                                byte* now7 = now5 + img.Width;
                                byte* now8 = now6 + img.Width;
                                pMag.Set<float>(k, paramFL.nLineCalipersLength - 1 - j, (1f - v) * ((float)(*now6 / 20 * 20) * (1f - u) + (float)(*now5 / 20 * 20) * u) + v * ((float)(*now8 / 20 * 20) * (1f - u) + (float)(*now7 / 20 * 20) * u));
                            }
                        }
                    }


                    Mat projectedMap = new Mat(new OpenCvSharp.Size(Line.W, Line.H), MatType.CV_32FC1);
                    Cv2.Reduce(pMag, projectedMap, ReduceDimension.Row, ReduceTypes.Avg, MatType.CV_32FC1);
                    Mat filteredMap = new Mat(new OpenCvSharp.Size(Line.H, 1), MatType.CV_32FC1);
                    Cv2.Filter2D(projectedMap, filteredMap, MatType.CV_32FC1, whalf, null, 0.0, BorderTypes.Reflect101);
                    float diff = filteredMap.Get<float>(0, 3);
                    List<int> peaks = FindPeak(Line.H, filteredMap, (float)paramFL.nLineCalipersThreshold);
                    bool flag3 = peaks.Count != 0;
                    if (flag3)
                    {
                        floatPoint Reat = GetReast(paramFL, peaks, filteredMap);
                        bool flag4 = Reat.Y != -1f;
                        if (flag4)
                        {
                            Reat.X = ROI.Center.X;
                            bool flag5 = paramFL.enumLineCalipersDirection == enumCaliperDirection.从内向外;
                            if (flag5)
                            {
                                Reat.Y = (float)((double)(ROI.Center.Y + Reat.Y) - (double)paramFL.nLineCalipersLength / 2.0);
                            }
                            else
                            {
                                Reat.Y = (float)((double)(ROI.Center.Y - Reat.Y) + (double)paramFL.nLineCalipersLength / 2.0);
                            }
                            floatPoint a2 = Glovar.PointToRotatPoint(Reat, Rotat);
                            ReastPoints.Add(a2);
                        }
                    }
                    pMag.Dispose();
                    projectedMap.Dispose();
                    filteredMap.Dispose();
                }
                img.Dispose();
                bool flag6 = paramFL.nLineCalipersIgnorePoints == 0;
                if (flag6)
                {
                    result = ClearErrorPointLine(ReastPoints, paramFL.nLineCalipersIgnorePoints, out UsedPont, out ErrorPoint, out Reast);
                }
                else
                {
                    result = fitline(ReastPoints, paramFL.nLineCalipersIgnorePoints, paramFL.nLineCalipersMaxError, out UsedPont, out ErrorPoint, out Reast);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }


      
        private void btnLineCallipersPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件(*.bmp;*.png)|*.bmp;*.png|所有文件|*.*";
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_lineCalliperImage = Cv2.ImRead(openFileDialog.FileName, ImreadModes.Grayscale);
                u_DisPlay3.Image = m_lineCalliperImage;
                u_DisPlay3.Invalidate();
            }
        }

        private void btnAddLine12Callipers_Click(object sender, EventArgs e)
        {
            if (m_lineCalliperImage == null)
            {
                return;
            }
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsUseLine1");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsErrLine1");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "findLine1");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotCross");
            m_line1Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper");
            if (m_line1Caliper == null)
            {
                double x1 = m_lineCalliperImage.Width  / 4;
                double y1 = m_lineCalliperImage.Height / 2 ;
                double x2 = m_lineCalliperImage.Width / 4*3;
                double y2 = m_lineCalliperImage.Height / 2;
                m_line1Caliper = new CvDisplayGraphicsCalipersLine(new Point2d(x1,y1), new Point2d(x2, y2));
                m_line1Caliper.Color = Color.LimeGreen;
                m_line1Caliper.Number = Convert.ToInt32(numUDLine1CalipersNum.Value);
                m_line1Caliper.W = Convert.ToInt32(numUDLine1CalipersWidth.Value);
                m_line1Caliper.H = Convert.ToInt32(numUDLine1CalipersLength.Value);
                m_line1Caliper.Name = "m_line1Caliper";
                u_DisPlay3.GraphicsShapes.Add(m_line1Caliper);
            }
            else
            {
                m_line1Caliper.Actived = true;
                m_line1Caliper.Visble = true;
            }

            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsUseLine2");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsErrLine2");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "findLine2");
            m_line2Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper");
            if (m_line2Caliper == null)
            {
                double x1 = m_lineCalliperImage.Width / 2;
                double y1 = m_lineCalliperImage.Height / 4;
                double x2 = m_lineCalliperImage.Width / 2;
                double y2 = m_lineCalliperImage.Height / 4*3;
                m_line2Caliper = new CvDisplayGraphicsCalipersLine(new Point2d(x1, y1), new Point2d(x2, y2));
                m_line2Caliper.Color = Color.LimeGreen;
                m_line2Caliper.Number = Convert.ToInt32(numUDLine2CalipersNum.Value);
                m_line2Caliper.W = Convert.ToInt32(numUDLine2CalipersWidth.Value);
                m_line2Caliper.H = Convert.ToInt32(numUDLine2CalipersLength.Value);
                m_line2Caliper.Name = "m_line2Caliper";
                u_DisPlay3.GraphicsShapes.Add(m_line2Caliper);
            }
            else
            {
                m_line2Caliper.Actived = true;
                m_line2Caliper.Visble = true;
            }

            u_DisPlay3.Invalidate();
        }

        private bool getCrossPoint(PointF S1,PointF E1, PointF S2, PointF E2,out PointF  CrossP)
        {
            CrossP = new PointF(0, 0);
            double dbCrossX = 0;
            double dbCrossY = 0;
            double x1 = S1.X;
            double y1 = S1.Y;
            double x2 = E1.X;
            double y2 = E1.Y;

            double x3 = S2.X;
            double y3 = S2.Y;
            double x4 = E2.X;
            double y4 = E2.Y;
            bool bSuc = false;

            double k1=0, b1=0;
            bool k1_None = false;
            if (x2 - x1 == 0)
            {
                k1_None = true;
            }
            else
            {
                k1 = (double)(y2 - y1) / (double)(x2 - x1); //求出LineA斜率
                b1 = (double)y1 - (double)x1 * k1;
            }


            double k2=0, b2=0;
            bool k2_None = false;
            if (x4 - x3 == 0)
            {
                k2_None = true;
                b2 = 0;
            }
            else
            {
                k2 = (double)(y4 - y3) / (double)(x4 - x3); //求出LineB斜率
                b2 = (double)y3 - (double)x3 * k2;
            }

            if (k1_None)
            {
                if (!k2_None)
                {
                    dbCrossX = x1;
                    dbCrossY = k2 * x1 + b2;
                    bSuc= true;
                }
            }
            else if (k2_None)
            {
                dbCrossX = x3;
                dbCrossY = k1 * x3 + b1;
                bSuc = true;
            }
            else if (k2 != k1)
            {
                dbCrossX = (double)(b2 - b1) / (double)(k1 - k2);
                dbCrossY = k1 * dbCrossX + b1;
                bSuc = true;
            }

            if(bSuc)
            {
                CrossP.X = (float)dbCrossX;
                CrossP.Y = (float)dbCrossY;
            }
            return bSuc;
        }

        private void btnFindCross_Click(object sender, EventArgs e)
        {
            if (m_lineCalliperImage == null)
            {
                MessageBox.Show("卡尺图片不存在");
                return;
            }

            if (m_line1Caliper == null)
            {
                MessageBox.Show("直线1卡尺不存在");
                return;
            }

            if (m_line2Caliper == null)
            {
                MessageBox.Show("直线2卡尺不存在");
                return;
            }

            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsUseLine1");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsErrLine1");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "findLine1");

            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsUseLine2");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotsErrLine2");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "findLine2");
            u_DisPlay3.GraphicsShapes.RemoveAll(p => p.Name == "dotCross");

            
            ((CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper")).Visble = false;
            ((CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper")).Visble = false;

            txtCrossX.Text = "";
            txtCrossY.Text = "";
            txtCrossA.Text = "";

            u_DisPlay3.Invalidate();

            bool bFindLine1 = false;
            bool bFindLine2 = false;
            PointF S1 = new PointF(0, 0);
            PointF E1 = new PointF(0, 0);

            PointF S2 = new PointF(0, 0);
            PointF E2 = new PointF(0, 0);
            double dbLineA = 0;

            try
            {
               
                paramFindLine paramFindLine1 = new paramFindLine();
                paramFindLine1.nLineCalipersIgnorePoints = Convert.ToInt32(numUDLine1CalipersIgnoreDots.Value);
                paramFindLine1.nLineCalipersLength = Convert.ToInt32(numUDLine1CalipersLength.Value);
                paramFindLine1.nLineCalipersNum = Convert.ToInt32(numUDLine1CalipersNum.Value);
                paramFindLine1.nLineCalipersThreshold = Convert.ToInt32(numUDLine1CalipersThreshold.Value);
                paramFindLine1.nLineCalipersWidth = Convert.ToInt32(numUDLine1CalipersWidth.Value);
                paramFindLine1.nLineCalipersFilterSize = Convert.ToInt32(numUDLine1CalipersFiterSize.Value);
                paramFindLine1.nLineCalipersMaxError = Convert.ToInt32(numUDLine1CalipersMaxError.Value);
                paramFindLine1.enumLineCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combLine1CalipersDir.SelectedIndex.ToString());
                paramFindLine1.enumLineCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combLine1CalipersPolarity.SelectedIndex.ToString());

                double[] Reast;
                List<floatPoint> UsedPont;
                List<floatPoint> ErrorPoint;
                bool flag2 = FindLine(m_lineCalliperImage.Clone(), m_line1Caliper, paramFindLine1, out Reast, out UsedPont, out ErrorPoint);
                if (flag2)
                {
                    bFindLine1 = true;
                    List<Point2f> Points = new List<Point2f>();
                    foreach (floatPoint point in UsedPont)
                    {
                        Points.Add(new Point2f(point.X, point.Y));
                    }
                    CvDisplayGraphicsDots dotsUseLine1 = new CvDisplayGraphicsDots(Points);
                    dotsUseLine1.Name = "dotsUseLine1";
                    dotsUseLine1.Color = Color.LimeGreen;
                    dotsUseLine1.IsMove = true;
                    u_DisPlay3.GraphicsShapes.Add(dotsUseLine1);
                    List<Point2f> Points2 = new List<Point2f>();
                    foreach (floatPoint point2 in ErrorPoint)
                    {
                        Points2.Add(new Point2f(point2.X, point2.Y));
                    }
                    CvDisplayGraphicsDots dotsErrLine1 = new CvDisplayGraphicsDots(Points2);
                    dotsErrLine1.Name = "dotsErrLine1";
                    dotsErrLine1.Color = Color.Red;
                    dotsErrLine1.IsMove = true;
                    u_DisPlay3.GraphicsShapes.Add(dotsErrLine1);
                    double a = Reast[0];
                    double b = Reast[1];
                    double c = Reast[2];
                    double y = (-a * (double)this.m_lineCalliperImage.Width / 2.0 - c) / b;
                    bool flag3 = y >= 0.0 && y <= (double)this.m_lineCalliperImage.Height;
                    Point2d p;
                    if (flag3)
                    {
                        p = new Point2d((double)(this.m_lineCalliperImage.Width / 2), y);
                    }
                    else
                    {
                        double x = (-b * (double)this.m_lineCalliperImage.Height / 2.0 - c) / a;
                        p = new Point2d(x, (double)(this.m_lineCalliperImage.Height / 2));
                    }
                    double ka = double.IsNaN(-a / b) ? 90.0 : (Math.Atan(-a / b) * 180.0 / 3.141592653589793);
                    double dbSEDis = Math.Sqrt((m_line1Caliper.Start.X - m_line1Caliper.End.X) * (m_line1Caliper.Start.X - m_line1Caliper.End.X) + (m_line1Caliper.Start.Y - m_line1Caliper.End.Y) * (m_line1Caliper.Start.Y - m_line1Caliper.End.Y));
                    //double dbStartDis = Math.Sqrt((m_line1Caliper.Start.X - p.X) * (m_line1Caliper.Start.X - p.X) + (m_line1Caliper.Start.Y - p.Y) * (m_line1Caliper.Start.Y - p.Y));
                    //double dbEndDis = Math.Sqrt((m_line1Caliper.End.X - p.X) * (m_line1Caliper.End.X - p.X) + (m_line1Caliper.End.Y - p.Y) * (m_line1Caliper.End.Y - p.Y));
                    S1 = Caculate.GetEndPointByTrigonometeic(ka, new PointF((float)p.X, (float)p.Y), dbSEDis);
                    E1 = Caculate.GetEndPointByTrigonometeic(ka + 180.0, new PointF((float)p.X, (float)p.Y), dbSEDis);


                    double dbL1 = Math.Sqrt((m_line1Caliper.Start.X - S1.X) * (m_line1Caliper.Start.X - S1.X) + (m_line1Caliper.Start.Y - S1.Y) * (m_line1Caliper.Start.Y - S1.Y));
                    double dbL2 = Math.Sqrt((m_line1Caliper.Start.X - E1.X) * (m_line1Caliper.Start.X - E1.X) + (m_line1Caliper.Start.Y - E1.Y) * (m_line1Caliper.Start.Y - E1.Y));

                    if(dbL1> dbL2)
                    {
                        CvDisplayGraphicsLineSegment findLine1 = new CvDisplayGraphicsLineSegment(new Point2d((double)E1.X, (double)E1.Y), new Point2d((double)S1.X, (double)S1.Y));
                        findLine1.Name = "findLine1";
                        findLine1.Info = "Line1";
                        findLine1.IsMove = true;
                        findLine1.Color = Color.Green;
                        u_DisPlay3.GraphicsShapes.Add(findLine1);
                    }
                    else
                    {
                        CvDisplayGraphicsLineSegment findLine1 = new CvDisplayGraphicsLineSegment(new Point2d((double)S1.X, (double)S1.Y), new Point2d((double)E1.X, (double)E1.Y));
                        findLine1.Name = "findLine1";
                        findLine1.Info = "Line1";
                        findLine1.IsMove = true;
                        findLine1.Color = Color.Green;
                        u_DisPlay3.GraphicsShapes.Add(findLine1);
                    }



                    dbLineA = -ka;
                    
                  
                    u_DisPlay3.Invalidate(); 
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {

                paramFindLine paramFindLine2 = new paramFindLine();
                paramFindLine2.nLineCalipersIgnorePoints = Convert.ToInt32(numUDLine2CalipersIgnoreDots.Value);
                paramFindLine2.nLineCalipersLength = Convert.ToInt32(numUDLine2CalipersLength.Value);
                paramFindLine2.nLineCalipersNum = Convert.ToInt32(numUDLine2CalipersNum.Value);
                paramFindLine2.nLineCalipersThreshold = Convert.ToInt32(numUDLine2CalipersThreshold.Value);
                paramFindLine2.nLineCalipersWidth = Convert.ToInt32(numUDLine2CalipersWidth.Value);
                paramFindLine2.nLineCalipersFilterSize = Convert.ToInt32(numUDLine2CalipersFiterSize.Value);
                paramFindLine2.nLineCalipersMaxError = Convert.ToInt32(numUDLine2CalipersMaxError.Value);
                paramFindLine2.enumLineCalipersDirection = (enumCaliperDirection)csEnumReadWriter.getEnumByValue(typeof(enumCaliperDirection), combLine2CalipersDir.SelectedIndex.ToString());
                paramFindLine2.enumLineCalipersSearchModel = (enumCaliperSearchModel)csEnumReadWriter.getEnumByValue(typeof(enumCaliperSearchModel), combLine2CalipersPolarity.SelectedIndex.ToString());

                double[] Reast;
                List<floatPoint> UsedPont;
                List<floatPoint> ErrorPoint;
                bool flag2 = FindLine(m_lineCalliperImage.Clone(), m_line2Caliper, paramFindLine2, out Reast, out UsedPont, out ErrorPoint);
                if (flag2)
                {
                    bFindLine2 = true;
                    List<Point2f> Points = new List<Point2f>();
                    foreach (floatPoint point in UsedPont)
                    {
                        Points.Add(new Point2f(point.X, point.Y));
                    }
                    CvDisplayGraphicsDots dotsUseLine2 = new CvDisplayGraphicsDots(Points);
                    dotsUseLine2.Name = "dotsUseLine2";
                    dotsUseLine2.Color = Color.LimeGreen;
                    dotsUseLine2.IsMove = true;
                    u_DisPlay3.GraphicsShapes.Add(dotsUseLine2);
                    List<Point2f> Points2 = new List<Point2f>();
                    foreach (floatPoint point2 in ErrorPoint)
                    {
                        Points2.Add(new Point2f(point2.X, point2.Y));
                    }
                    CvDisplayGraphicsDots dotsErrLine2 = new CvDisplayGraphicsDots(Points2);
                    dotsErrLine2.Name = "dotsErrLine2";
                    dotsErrLine2.Color = Color.Red;
                    dotsErrLine2.IsMove = true;
                    u_DisPlay3.GraphicsShapes.Add(dotsErrLine2);
                    double a = Reast[0];
                    double b = Reast[1];
                    double c = Reast[2];
                    double y = (-a * (double)this.m_lineCalliperImage.Width / 2.0 - c) / b;
                    bool flag3 = y >= 0.0 && y <= (double)this.m_lineCalliperImage.Height;
                    Point2d p;
                    if (flag3)
                    {
                        p = new Point2d((double)(this.m_lineCalliperImage.Width / 2), y);
                    }
                    else
                    {
                        double x = (-b * (double)this.m_lineCalliperImage.Height / 2.0 - c) / a;
                        p = new Point2d(x, (double)(this.m_lineCalliperImage.Height / 2));
                    }
                    double ka = double.IsNaN(-a / b) ? 90.0 : (Math.Atan(-a / b) * 180.0 / 3.141592653589793);
                    double dbSEDis= Math.Sqrt((m_line2Caliper.Start.X - m_line2Caliper.End.X) * (m_line2Caliper.Start.X - m_line2Caliper.End.X) + (m_line2Caliper.Start.Y - m_line2Caliper.End.Y) * (m_line2Caliper.Start.Y - m_line2Caliper.End.Y));
                    //double dbStartDis = Math.Sqrt((m_line2Caliper.Start.X - p.X) * (m_line2Caliper.Start.X - p.X) + (m_line2Caliper.Start.Y - p.Y) * (m_line2Caliper.Start.Y - p.Y));
                    //double dbEndDis = Math.Sqrt((m_line2Caliper.End.X - p.X) * (m_line2Caliper.End.X - p.X) + (m_line2Caliper.End.Y - p.Y) * (m_line2Caliper.End.Y - p.Y));
                    S2 = Caculate.GetEndPointByTrigonometeic(ka, new PointF((float)p.X, (float)p.Y), dbSEDis);
                    E2 = Caculate.GetEndPointByTrigonometeic(ka + 180.0, new PointF((float)p.X, (float)p.Y), dbSEDis);


                    double dbL1 = Math.Sqrt((m_line2Caliper.Start.X - S2.X) * (m_line2Caliper.Start.X - S2.X) + (m_line2Caliper.Start.Y - S2.Y) * (m_line2Caliper.Start.Y - S2.Y));
                    double dbL2 = Math.Sqrt((m_line2Caliper.Start.X - E2.X) * (m_line2Caliper.Start.X - E2.X) + (m_line2Caliper.Start.Y - E2.Y) * (m_line2Caliper.Start.Y - E2.Y));

                    if (dbL1 > dbL2)
                    {
                        CvDisplayGraphicsLineSegment findLine2 = new CvDisplayGraphicsLineSegment(new Point2d((double)E2.X, (double)E2.Y), new Point2d((double)S2.X, (double)S2.Y));
                        findLine2.Name = "findLine2";
                        findLine2.Info = "Line2";
                        findLine2.IsMove = true;
                        findLine2.Color = Color.Green;
                        u_DisPlay3.GraphicsShapes.Add(findLine2);
                    }
                    else
                    {
                        CvDisplayGraphicsLineSegment findLine2 = new CvDisplayGraphicsLineSegment(new Point2d((double)S2.X, (double)S2.Y), new Point2d((double)E2.X, (double)E2.Y));
                        findLine2.Name = "findLine2";
                        findLine2.Info = "Line2";
                        findLine2.IsMove = true;
                        findLine2.Color = Color.Green;
                        u_DisPlay3.GraphicsShapes.Add(findLine2);
                    }


           
                    u_DisPlay3.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if(bFindLine1&&bFindLine2)
            {
                PointF pCross;
                if(getCrossPoint(S1, E1, S2, E2, out pCross))
                {
                    txtCrossX.Text = pCross.X.ToString();
                    txtCrossY.Text = pCross.Y.ToString();
                    txtCrossA.Text = dbLineA.ToString();

                    CvDisplayGraphicsDot dotCross = new CvDisplayGraphicsDot(pCross.X, pCross.Y);
                    dotCross.Name = "dotCross";
                    dotCross.Color = Color.Blue;
                    dotCross.IsMove = true;
                    u_DisPlay3.GraphicsShapes.Add(dotCross);
                    u_DisPlay3.Invalidate();
                }


               

            }

        }

        private void numUDLine1CalipersNum_ValueChanged(object sender, EventArgs e)
        {
            m_line1Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper");
            if (m_line1Caliper != null)
            {
                m_line1Caliper.Number = Convert.ToInt32(numUDLine1CalipersNum.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void numUDLine1CalipersLength_ValueChanged(object sender, EventArgs e)
        {
            m_line1Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper");
            if (m_line1Caliper != null)
            {
                m_line1Caliper.H = Convert.ToInt32(numUDLine1CalipersLength.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void numUDLine1CalipersWidth_ValueChanged(object sender, EventArgs e)
        {
            m_line1Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper");
            if (m_line1Caliper != null)
            {
                m_line1Caliper.W = Convert.ToInt32(numUDLine1CalipersWidth.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void combLine1CalipersDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_line1Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line1Caliper");
            if (m_line1Caliper != null)
            {
                int index = combLine1CalipersDir.SelectedIndex;
                if (index != -1)
                {
                    enumCaliperDirection Direction = index == 0 ? enumCaliperDirection.从内向外 : enumCaliperDirection.从外向内;
                    m_line1Caliper.Direction = Direction;
                    u_DisPlay3.Invalidate();
                }
            }
        }

        private void numUDLine2CalipersNum_ValueChanged(object sender, EventArgs e)
        {
            m_line2Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper");
            if (m_line2Caliper != null)
            {
                m_line2Caliper.Number = Convert.ToInt32(numUDLine2CalipersNum.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void numUDLine2CalipersLength_ValueChanged(object sender, EventArgs e)
        {
            m_line2Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper");
            if (m_line2Caliper != null)
            {
                m_line2Caliper.H = Convert.ToInt32(numUDLine2CalipersLength.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void numUDLine2CalipersWidth_ValueChanged(object sender, EventArgs e)
        {
            m_line2Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper");
            if (m_line2Caliper != null)
            {
                m_line2Caliper.W = Convert.ToInt32(numUDLine2CalipersWidth.Value);
                u_DisPlay3.Invalidate();
            }
        }

        private void combLine2CalipersDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_line2Caliper = (CvDisplayGraphicsCalipersLine)u_DisPlay3.GraphicsShapes.Find(p => p.Name == "m_line2Caliper");
            if (m_line2Caliper != null)
            {
                int index = combLine2CalipersDir.SelectedIndex;
                if (index != -1)
                {
                    enumCaliperDirection Direction = index == 0 ? enumCaliperDirection.从内向外 : enumCaliperDirection.从外向内;
                    m_line2Caliper.Direction = Direction;
                    u_DisPlay3.Invalidate();
                }
            }
        }



        #endregion

    }
}
