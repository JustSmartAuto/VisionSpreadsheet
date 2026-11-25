
namespace OpencvDemo
{
    partial class Form1
    {
        /// <summary>
        /// ±ØÐèµÄÉè¼ÆÆ÷±äÁ¿¡£
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ÇåÀíËùÓÐÕýÔÚÊ¹ÓÃµÄ×ÊÔ´¡£
        /// </summary>
        /// <param name="disposing">Èç¹ûÓ¦ÊÍ·ÅÍÐ¹Ü×ÊÔ´£¬Îª true£»·ñÔòÎª false¡£</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ´°ÌåÉè¼ÆÆ÷Éú³ÉµÄ´úÂë

        /// <summary>
        /// Éè¼ÆÆ÷Ö§³ÖËùÐèµÄ·½·¨ - ²»ÒªÐÞ¸Ä
        /// Ê¹ÓÃ´úÂë±à¼­Æ÷ÐÞ¸Ä´Ë·½·¨µÄÄÚÈÝ¡£
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbShowMsg = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSearchRect = new System.Windows.Forms.Button();
            this.btnMakeModel = new System.Windows.Forms.Button();
            this.btnOpenSearchPic = new System.Windows.Forms.Button();
            this.btnOpenModelPic = new System.Windows.Forms.Button();
            this.btnFindModel = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.btnMakeRect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboSubPixel = new System.Windows.Forms.ComboBox();
            this.numUDFNumMatchs = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numUDFMaxOverlap = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numUDFMaxScale = new System.Windows.Forms.NumericUpDown();
            this.numUDFEndNumLevels = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numUDFModelEndAngle = new System.Windows.Forms.NumericUpDown();
            this.numUDFGreediness = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numUDFMinScore = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numUDFMinScale = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numUDFModelStartAngle = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIgnorePolarity = new System.Windows.Forms.CheckBox();
            this.numUDContrastHigh = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numUDCModelMinContrast = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numUDCMinEdge = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numUDCNumLevels = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numUDContrastLow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numUDCModelEndAngle = new System.Windows.Forms.NumericUpDown();
            this.numUDCModelStartAngle = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.u_DisPlay1 = new Display.U_DisPlay();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbShowCircleCaliperMsg = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtCircleCaliperY = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtCircleCaliperR = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtCircleCaliperX = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnFindCircleCallipers = new System.Windows.Forms.Button();
            this.btnCircleCallipersPic = new System.Windows.Forms.Button();
            this.btnAddCircleCallipers = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.combCircleCalipersDir = new System.Windows.Forms.ComboBox();
            this.combCircleCalipersPolarity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numUDCircleCalipersWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numUDCircleCalipersThreshold = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.numUDCircleCalipersIgnoreDots = new System.Windows.Forms.NumericUpDown();
            this.numUDCircleCalipersNum = new System.Windows.Forms.NumericUpDown();
            this.numUDCircleCalipersLength = new System.Windows.Forms.NumericUpDown();
            this.numUDCircleCalipersFiterSize = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.u_DisPlay2 = new Display.U_DisPlay();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.txtCrossY = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtCrossA = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.txtCrossX = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnFindCross = new System.Windows.Forms.Button();
            this.btnLineCallipersPic = new System.Windows.Forms.Button();
            this.btnAddLine12Callipers = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.combLine2CalipersDir = new System.Windows.Forms.ComboBox();
            this.combLine2CalipersPolarity = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.numUDLine2CalipersWidth = new System.Windows.Forms.NumericUpDown();
            this.label43 = new System.Windows.Forms.Label();
            this.numUDLine2CalipersThreshold = new System.Windows.Forms.NumericUpDown();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.numUDLine2CalipersMaxError = new System.Windows.Forms.NumericUpDown();
            this.numUDLine2CalipersIgnoreDots = new System.Windows.Forms.NumericUpDown();
            this.numUDLine2CalipersNum = new System.Windows.Forms.NumericUpDown();
            this.numUDLine2CalipersLength = new System.Windows.Forms.NumericUpDown();
            this.label49 = new System.Windows.Forms.Label();
            this.numUDLine2CalipersFiterSize = new System.Windows.Forms.NumericUpDown();
            this.label50 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.combLine1CalipersDir = new System.Windows.Forms.ComboBox();
            this.combLine1CalipersPolarity = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.numUDLine1CalipersWidth = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.numUDLine1CalipersThreshold = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.numUDLine1CalipersMaxError = new System.Windows.Forms.NumericUpDown();
            this.numUDLine1CalipersIgnoreDots = new System.Windows.Forms.NumericUpDown();
            this.numUDLine1CalipersNum = new System.Windows.Forms.NumericUpDown();
            this.numUDLine1CalipersLength = new System.Windows.Forms.NumericUpDown();
            this.label41 = new System.Windows.Forms.Label();
            this.numUDLine1CalipersFiterSize = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.u_DisPlay3 = new Display.U_DisPlay();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtRectCaliperY = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.txtRectCaliperA = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.txtRectCaliperX = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.btnFindRectCallipers = new System.Windows.Forms.Button();
            this.btnRectCallipersPic = new System.Windows.Forms.Button();
            this.btnAddRectCallipers = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.combRectCalipersDir = new System.Windows.Forms.ComboBox();
            this.combRectCalipersPolarity = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.numUDRectCalipersWidth = new System.Windows.Forms.NumericUpDown();
            this.label55 = new System.Windows.Forms.Label();
            this.numUDRectCalipersThreshold = new System.Windows.Forms.NumericUpDown();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.numUDRectCalipersMaxError = new System.Windows.Forms.NumericUpDown();
            this.numUDRectCalipersIgnoreDots = new System.Windows.Forms.NumericUpDown();
            this.numUDRectCalipersNum = new System.Windows.Forms.NumericUpDown();
            this.numUDRectCalipersLength = new System.Windows.Forms.NumericUpDown();
            this.label65 = new System.Windows.Forms.Label();
            this.numUDRectCalipersFiterSize = new System.Windows.Forms.NumericUpDown();
            this.label61 = new System.Windows.Forms.Label();
            this.u_DisPlay4 = new Display.U_DisPlay();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnCreateCheckBoardParam = new System.Windows.Forms.Button();
            this.btnCheckCheckBoardParam = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numUDCheckerBoardHeight = new System.Windows.Forms.NumericUpDown();
            this.numUDCornerPixlSize = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.numUDCheckerBoardDis = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.numUDCheckerBoardWidth = new System.Windows.Forms.NumericUpDown();
            this.u_DisPlay6 = new Display.U_DisPlay();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.txtDistanceEndY = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.txtDistanceStartY = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.txtDistanceEndX = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.txtDistanceStartX = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.btnGetDistance = new System.Windows.Forms.Button();
            this.btnDistanceCallipersPic = new System.Windows.Forms.Button();
            this.btnAddDistanceCallipers = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.combEndCalipersSearchNumber = new System.Windows.Forms.ComboBox();
            this.combEndCalipersSearchMode = new System.Windows.Forms.ComboBox();
            this.combStartCalipersSearchNumber = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.combStartCalipersSearchMode = new System.Windows.Forms.ComboBox();
            this.label67 = new System.Windows.Forms.Label();
            this.numUDDistanceCalipersThreshold = new System.Windows.Forms.NumericUpDown();
            this.label66 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.numUDDistanceCalipersFiterSize = new System.Windows.Forms.NumericUpDown();
            this.u_DisPlay7 = new Display.U_DisPlay();
            this.btnOpenPrj = new System.Windows.Forms.Button();
            this.btnSavePrj = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFNumMatchs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMaxOverlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMaxScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFEndNumLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFModelEndAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFGreediness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMinScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFModelStartAngle)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDContrastHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelMinContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCMinEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCNumLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDContrastLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelEndAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersIgnoreDots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersFiterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersMaxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersIgnoreDots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersFiterSize)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersMaxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersIgnoreDots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersFiterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay3)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersMaxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersIgnoreDots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersFiterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay4)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCornerPixlSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay6)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDistanceCalipersThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDistanceCalipersFiterSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay7)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(5, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1382, 852);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage1.Controls.Add(this.lbShowMsg);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.u_DisPlay1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1374, 826);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ä£°åÆ¥Åä";
            // 
            // lbShowMsg
            // 
            this.lbShowMsg.BackColor = System.Drawing.Color.DarkGray;
            this.lbShowMsg.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShowMsg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbShowMsg.Location = new System.Drawing.Point(6, 786);
            this.lbShowMsg.Name = "lbShowMsg";
            this.lbShowMsg.Size = new System.Drawing.Size(908, 36);
            this.lbShowMsg.TabIndex = 3;
            this.lbShowMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.BackColor = System.Drawing.Color.Gainsboro;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(920, 564);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(449, 261);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 1;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ÐòºÅ";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "X";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Y";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Angle";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Ëõ·Å±È";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "·ÖÖµ";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 45;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox3.Controls.Add(this.btnSearchRect);
            this.groupBox3.Controls.Add(this.btnMakeModel);
            this.groupBox3.Controls.Add(this.btnOpenSearchPic);
            this.groupBox3.Controls.Add(this.btnOpenModelPic);
            this.groupBox3.Controls.Add(this.btnFindModel);
            this.groupBox3.Controls.Add(this.btnMask);
            this.groupBox3.Controls.Add(this.btnMakeRect);
            this.groupBox3.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox3.Location = new System.Drawing.Point(920, 420);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 138);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ä£°åÖÆ×÷";
            // 
            // btnSearchRect
            // 
            this.btnSearchRect.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchRect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchRect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSearchRect.Location = new System.Drawing.Point(120, 86);
            this.btnSearchRect.Name = "btnSearchRect";
            this.btnSearchRect.Size = new System.Drawing.Size(97, 34);
            this.btnSearchRect.TabIndex = 0;
            this.btnSearchRect.Text = "ËÑË÷ÇøÓò";
            this.btnSearchRect.UseVisualStyleBackColor = false;

            // 
            // btnMakeModel
            // 
            this.btnMakeModel.BackColor = System.Drawing.SystemColors.Control;
            this.btnMakeModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeModel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMakeModel.Location = new System.Drawing.Point(342, 33);
            this.btnMakeModel.Name = "btnMakeModel";
            this.btnMakeModel.Size = new System.Drawing.Size(97, 34);
            this.btnMakeModel.TabIndex = 0;
            this.btnMakeModel.Text = "ÖÆ×÷Ä£°å";
            this.btnMakeModel.UseVisualStyleBackColor = false;

            // 
            // btnOpenSearchPic
            // 
            this.btnOpenSearchPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenSearchPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSearchPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnOpenSearchPic.Location = new System.Drawing.Point(9, 86);
            this.btnOpenSearchPic.Name = "btnOpenSearchPic";
            this.btnOpenSearchPic.Size = new System.Drawing.Size(97, 34);
            this.btnOpenSearchPic.TabIndex = 0;
            this.btnOpenSearchPic.Text = "ËÑË÷Í¼Æ¬";
            this.btnOpenSearchPic.UseVisualStyleBackColor = false;

            // 
            // btnOpenModelPic
            // 
            this.btnOpenModelPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenModelPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenModelPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnOpenModelPic.Location = new System.Drawing.Point(9, 33);
            this.btnOpenModelPic.Name = "btnOpenModelPic";
            this.btnOpenModelPic.Size = new System.Drawing.Size(97, 34);
            this.btnOpenModelPic.TabIndex = 0;
            this.btnOpenModelPic.Text = "Ä£°æÍ¼Æ¬";
            this.btnOpenModelPic.UseVisualStyleBackColor = false;

            // 
            // btnFindModel
            // 
            this.btnFindModel.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindModel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnFindModel.Location = new System.Drawing.Point(342, 86);
            this.btnFindModel.Name = "btnFindModel";
            this.btnFindModel.Size = new System.Drawing.Size(97, 34);
            this.btnFindModel.TabIndex = 0;
            this.btnFindModel.Text = "²éÕÒÄ£°æ";
            this.btnFindModel.UseVisualStyleBackColor = false;

            // 
            // btnMask
            // 
            this.btnMask.BackColor = System.Drawing.SystemColors.Control;
            this.btnMask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMask.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMask.Location = new System.Drawing.Point(231, 33);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(97, 34);
            this.btnMask.TabIndex = 0;
            this.btnMask.Text = "ÑÚÄ¤";
            this.btnMask.UseVisualStyleBackColor = false;

            // 
            // btnMakeRect
            // 
            this.btnMakeRect.BackColor = System.Drawing.SystemColors.Control;
            this.btnMakeRect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeRect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMakeRect.Location = new System.Drawing.Point(120, 33);
            this.btnMakeRect.Name = "btnMakeRect";
            this.btnMakeRect.Size = new System.Drawing.Size(97, 34);
            this.btnMakeRect.TabIndex = 0;
            this.btnMakeRect.Text = "Ä£°åÇøÓò";
            this.btnMakeRect.UseVisualStyleBackColor = false;

            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox2.Controls.Add(this.comboSubPixel);
            this.groupBox2.Controls.Add(this.numUDFNumMatchs);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.numUDFMaxOverlap);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.numUDFMaxScale);
            this.groupBox2.Controls.Add(this.numUDFEndNumLevels);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.numUDFModelEndAngle);
            this.groupBox2.Controls.Add(this.numUDFGreediness);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.numUDFMinScore);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numUDFMinScale);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numUDFModelStartAngle);
            this.groupBox2.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox2.Location = new System.Drawing.Point(920, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 228);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ä£°å²éÕÒ²ÎÊý";
            // 
            // comboSubPixel
            // 
            this.comboSubPixel.FormattingEnabled = true;
            this.comboSubPixel.Items.AddRange(new object[] {
            "None",
            "Interpolation",
            "Least-squares"});
            this.comboSubPixel.Location = new System.Drawing.Point(344, 185);
            this.comboSubPixel.Name = "comboSubPixel";
            this.comboSubPixel.Size = new System.Drawing.Size(84, 30);
            this.comboSubPixel.TabIndex = 2;
            // 
            // numUDFNumMatchs
            // 
            this.numUDFNumMatchs.Location = new System.Drawing.Point(344, 146);
            this.numUDFNumMatchs.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDFNumMatchs.Name = "numUDFNumMatchs";
            this.numUDFNumMatchs.Size = new System.Drawing.Size(84, 29);
            this.numUDFNumMatchs.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label16.Location = new System.Drawing.Point(241, 189);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 22);
            this.label16.TabIndex = 0;
            this.label16.Text = "ÑÇÏñËØÀàÐÍ:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label15.Location = new System.Drawing.Point(11, 150);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 22);
            this.label15.TabIndex = 0;
            this.label15.Text = "Ì°À·¶È:";
            // 
            // numUDFMaxOverlap
            // 
            this.numUDFMaxOverlap.DecimalPlaces = 1;
            this.numUDFMaxOverlap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDFMaxOverlap.Location = new System.Drawing.Point(344, 108);
            this.numUDFMaxOverlap.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDFMaxOverlap.Name = "numUDFMaxOverlap";
            this.numUDFMaxOverlap.Size = new System.Drawing.Size(84, 29);
            this.numUDFMaxOverlap.TabIndex = 1;
            this.numUDFMaxOverlap.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(11, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 22);
            this.label13.TabIndex = 0;
            this.label13.Text = "×îÐ¡µÃ·Ö:";
            // 
            // numUDFMaxScale
            // 
            this.numUDFMaxScale.DecimalPlaces = 1;
            this.numUDFMaxScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDFMaxScale.Location = new System.Drawing.Point(344, 70);
            this.numUDFMaxScale.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numUDFMaxScale.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numUDFMaxScale.Name = "numUDFMaxScale";
            this.numUDFMaxScale.Size = new System.Drawing.Size(84, 29);
            this.numUDFMaxScale.TabIndex = 1;
            this.numUDFMaxScale.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // numUDFEndNumLevels
            // 
            this.numUDFEndNumLevels.Location = new System.Drawing.Point(127, 185);
            this.numUDFEndNumLevels.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numUDFEndNumLevels.Name = "numUDFEndNumLevels";
            this.numUDFEndNumLevels.Size = new System.Drawing.Size(84, 29);
            this.numUDFEndNumLevels.TabIndex = 1;
            this.numUDFEndNumLevels.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label17.Location = new System.Drawing.Point(11, 189);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 22);
            this.label17.TabIndex = 0;
            this.label17.Text = "ÖÕÖ¹½ð×ÖËþ:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label14.Location = new System.Drawing.Point(241, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 22);
            this.label14.TabIndex = 0;
            this.label14.Text = "Ä¿±êÊýÁ¿:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label11.Location = new System.Drawing.Point(11, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 22);
            this.label11.TabIndex = 0;
            this.label11.Text = "×îÐ¡±ÈÀý:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label12.Location = new System.Drawing.Point(241, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 22);
            this.label12.TabIndex = 0;
            this.label12.Text = "×î´óÖØµþ:";
            // 
            // numUDFModelEndAngle
            // 
            this.numUDFModelEndAngle.Location = new System.Drawing.Point(344, 32);
            this.numUDFModelEndAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUDFModelEndAngle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numUDFModelEndAngle.Name = "numUDFModelEndAngle";
            this.numUDFModelEndAngle.Size = new System.Drawing.Size(84, 29);
            this.numUDFModelEndAngle.TabIndex = 1;
            this.numUDFModelEndAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // numUDFGreediness
            // 
            this.numUDFGreediness.DecimalPlaces = 1;
            this.numUDFGreediness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDFGreediness.Location = new System.Drawing.Point(127, 146);
            this.numUDFGreediness.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDFGreediness.Name = "numUDFGreediness";
            this.numUDFGreediness.Size = new System.Drawing.Size(84, 29);
            this.numUDFGreediness.TabIndex = 1;
            this.numUDFGreediness.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(241, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 22);
            this.label10.TabIndex = 0;
            this.label10.Text = "×î´ó±ÈÀý:";
            // 
            // numUDFMinScore
            // 
            this.numUDFMinScore.DecimalPlaces = 2;
            this.numUDFMinScore.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDFMinScore.Location = new System.Drawing.Point(127, 108);
            this.numUDFMinScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUDFMinScore.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.numUDFMinScore.Name = "numUDFMinScore";
            this.numUDFMinScore.Size = new System.Drawing.Size(84, 29);
            this.numUDFMinScore.TabIndex = 1;
            this.numUDFMinScore.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(11, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "ÆðÊ¼½Ç¶È:";
            // 
            // numUDFMinScale
            // 
            this.numUDFMinScale.DecimalPlaces = 1;
            this.numUDFMinScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numUDFMinScale.Location = new System.Drawing.Point(127, 70);
            this.numUDFMinScale.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numUDFMinScale.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numUDFMinScale.Name = "numUDFMinScale";
            this.numUDFMinScale.Size = new System.Drawing.Size(84, 29);
            this.numUDFMinScale.TabIndex = 1;
            this.numUDFMinScale.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(241, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 22);
            this.label9.TabIndex = 0;
            this.label9.Text = "ÖÕÖ¹½Ç¶È:";
            // 
            // numUDFModelStartAngle
            // 
            this.numUDFModelStartAngle.Location = new System.Drawing.Point(127, 32);
            this.numUDFModelStartAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUDFModelStartAngle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numUDFModelStartAngle.Name = "numUDFModelStartAngle";
            this.numUDFModelStartAngle.Size = new System.Drawing.Size(84, 29);
            this.numUDFModelStartAngle.TabIndex = 1;
            this.numUDFModelStartAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox1.Controls.Add(this.chkIgnorePolarity);
            this.groupBox1.Controls.Add(this.numUDContrastHigh);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numUDCModelMinContrast);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numUDCMinEdge);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numUDCNumLevels);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numUDContrastLow);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numUDCModelEndAngle);
            this.groupBox1.Controls.Add(this.numUDCModelStartAngle);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox1.Location = new System.Drawing.Point(920, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 184);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ä£°å´´½¨²ÎÊý";
            // 
            // chkIgnorePolarity
            // 
            this.chkIgnorePolarity.AutoSize = true;
            this.chkIgnorePolarity.ForeColor = System.Drawing.SystemColors.Highlight;
            this.chkIgnorePolarity.Location = new System.Drawing.Point(245, 150);
            this.chkIgnorePolarity.Name = "chkIgnorePolarity";
            this.chkIgnorePolarity.Size = new System.Drawing.Size(93, 26);
            this.chkIgnorePolarity.TabIndex = 2;
            this.chkIgnorePolarity.Text = "ºöÂÔ¼«ÐÔ";
            this.chkIgnorePolarity.UseVisualStyleBackColor = true;
            // 
            // numUDContrastHigh
            // 
            this.numUDContrastHigh.Location = new System.Drawing.Point(344, 35);
            this.numUDContrastHigh.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDContrastHigh.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUDContrastHigh.Name = "numUDContrastHigh";
            this.numUDContrastHigh.Size = new System.Drawing.Size(84, 29);
            this.numUDContrastHigh.TabIndex = 1;
            this.numUDContrastHigh.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(241, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "¶Ô±È¶È(¸ß):";
            // 
            // numUDCModelMinContrast
            // 
            this.numUDCModelMinContrast.Location = new System.Drawing.Point(344, 73);
            this.numUDCModelMinContrast.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUDCModelMinContrast.Name = "numUDCModelMinContrast";
            this.numUDCModelMinContrast.Size = new System.Drawing.Size(84, 29);
            this.numUDCModelMinContrast.TabIndex = 1;
            this.numUDCModelMinContrast.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(241, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "×îÐ¡¶Ô±È¶È:";
            // 
            // numUDCMinEdge
            // 
            this.numUDCMinEdge.Location = new System.Drawing.Point(127, 149);
            this.numUDCMinEdge.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDCMinEdge.Name = "numUDCMinEdge";
            this.numUDCMinEdge.Size = new System.Drawing.Size(84, 29);
            this.numUDCMinEdge.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(11, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "×îÐ¡×é¼þ³ß´ç:";
            // 
            // numUDCNumLevels
            // 
            this.numUDCNumLevels.Location = new System.Drawing.Point(127, 73);
            this.numUDCNumLevels.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numUDCNumLevels.Name = "numUDCNumLevels";
            this.numUDCNumLevels.Size = new System.Drawing.Size(84, 29);
            this.numUDCNumLevels.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(11, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "½ð×ÖËþµÈ¼¶:";
            // 
            // numUDContrastLow
            // 
            this.numUDContrastLow.Location = new System.Drawing.Point(127, 35);
            this.numUDContrastLow.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDContrastLow.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUDContrastLow.Name = "numUDContrastLow";
            this.numUDContrastLow.Size = new System.Drawing.Size(84, 29);
            this.numUDContrastLow.TabIndex = 1;
            this.numUDContrastLow.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(11, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "¶Ô±È¶È(µÍ):";
            // 
            // numUDCModelEndAngle
            // 
            this.numUDCModelEndAngle.Location = new System.Drawing.Point(344, 111);
            this.numUDCModelEndAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUDCModelEndAngle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numUDCModelEndAngle.Name = "numUDCModelEndAngle";
            this.numUDCModelEndAngle.Size = new System.Drawing.Size(84, 29);
            this.numUDCModelEndAngle.TabIndex = 1;
            this.numUDCModelEndAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // numUDCModelStartAngle
            // 
            this.numUDCModelStartAngle.Location = new System.Drawing.Point(127, 111);
            this.numUDCModelStartAngle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUDCModelStartAngle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numUDCModelStartAngle.Name = "numUDCModelStartAngle";
            this.numUDCModelStartAngle.Size = new System.Drawing.Size(84, 29);
            this.numUDCModelStartAngle.TabIndex = 1;
            this.numUDCModelStartAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label18.Location = new System.Drawing.Point(241, 115);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 22);
            this.label18.TabIndex = 0;
            this.label18.Text = "ÖÕÖ¹½Ç¶È:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label19.Location = new System.Drawing.Point(11, 115);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 22);
            this.label19.TabIndex = 0;
            this.label19.Text = "ÆðÊ¼½Ç¶È:";
            // 
            // u_DisPlay1
            // 
            this.u_DisPlay1.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay1.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay1.Image = null;
            this.u_DisPlay1.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay1.Name = "u_DisPlay1";
            this.u_DisPlay1.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay1.TabIndex = 0;
            this.u_DisPlay1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage2.Controls.Add(this.lbShowCircleCaliperMsg);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.u_DisPlay2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1374, 826);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ô²¿¨³ß";
            // 
            // lbShowCircleCaliperMsg
            // 
            this.lbShowCircleCaliperMsg.BackColor = System.Drawing.Color.DarkGray;
            this.lbShowCircleCaliperMsg.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShowCircleCaliperMsg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbShowCircleCaliperMsg.Location = new System.Drawing.Point(6, 786);
            this.lbShowCircleCaliperMsg.Name = "lbShowCircleCaliperMsg";
            this.lbShowCircleCaliperMsg.Size = new System.Drawing.Size(908, 36);
            this.lbShowCircleCaliperMsg.TabIndex = 4;
            this.lbShowCircleCaliperMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox6.Controls.Add(this.txtCircleCaliperY);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.txtCircleCaliperR);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.txtCircleCaliperX);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox6.Location = new System.Drawing.Point(920, 301);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(449, 119);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "²éÕÒ½á¹û";
            // 
            // txtCircleCaliperY
            // 
            this.txtCircleCaliperY.Location = new System.Drawing.Point(344, 33);
            this.txtCircleCaliperY.Name = "txtCircleCaliperY";
            this.txtCircleCaliperY.Size = new System.Drawing.Size(84, 29);
            this.txtCircleCaliperY.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label28.Location = new System.Drawing.Point(273, 37);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(57, 22);
            this.label28.TabIndex = 0;
            this.label28.Text = "ÖÐÐÄY:";
            // 
            // txtCircleCaliperR
            // 
            this.txtCircleCaliperR.Location = new System.Drawing.Point(99, 73);
            this.txtCircleCaliperR.Name = "txtCircleCaliperR";
            this.txtCircleCaliperR.Size = new System.Drawing.Size(84, 29);
            this.txtCircleCaliperR.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label29.Location = new System.Drawing.Point(32, 77);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 22);
            this.label29.TabIndex = 0;
            this.label29.Text = "°ë¾¶R:";
            // 
            // txtCircleCaliperX
            // 
            this.txtCircleCaliperX.Location = new System.Drawing.Point(99, 33);
            this.txtCircleCaliperX.Name = "txtCircleCaliperX";
            this.txtCircleCaliperX.Size = new System.Drawing.Size(84, 29);
            this.txtCircleCaliperX.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label22.Location = new System.Drawing.Point(32, 37);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 22);
            this.label22.TabIndex = 0;
            this.label22.Text = "ÖÐÐÄX:";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox5.Controls.Add(this.btnFindCircleCallipers);
            this.groupBox5.Controls.Add(this.btnCircleCallipersPic);
            this.groupBox5.Controls.Add(this.btnAddCircleCallipers);
            this.groupBox5.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox5.Location = new System.Drawing.Point(920, 203);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(449, 96);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "¿¨³ßÖÆ×÷";
            // 
            // btnFindCircleCallipers
            // 
            this.btnFindCircleCallipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindCircleCallipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindCircleCallipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnFindCircleCallipers.Location = new System.Drawing.Point(310, 34);
            this.btnFindCircleCallipers.Name = "btnFindCircleCallipers";
            this.btnFindCircleCallipers.Size = new System.Drawing.Size(108, 34);
            this.btnFindCircleCallipers.TabIndex = 0;
            this.btnFindCircleCallipers.Text = "²éÕÒ¿¨³ß";
            this.btnFindCircleCallipers.UseVisualStyleBackColor = false;

            // 
            // btnCircleCallipersPic
            // 
            this.btnCircleCallipersPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnCircleCallipersPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircleCallipersPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCircleCallipersPic.Location = new System.Drawing.Point(36, 34);
            this.btnCircleCallipersPic.Name = "btnCircleCallipersPic";
            this.btnCircleCallipersPic.Size = new System.Drawing.Size(108, 34);
            this.btnCircleCallipersPic.TabIndex = 0;
            this.btnCircleCallipersPic.Text = "¿¨³ßÍ¼Æ¬";
            this.btnCircleCallipersPic.UseVisualStyleBackColor = false;

            // 
            // btnAddCircleCallipers
            // 
            this.btnAddCircleCallipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddCircleCallipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCircleCallipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddCircleCallipers.Location = new System.Drawing.Point(173, 34);
            this.btnAddCircleCallipers.Name = "btnAddCircleCallipers";
            this.btnAddCircleCallipers.Size = new System.Drawing.Size(108, 34);
            this.btnAddCircleCallipers.TabIndex = 0;
            this.btnAddCircleCallipers.Text = "Ìí¼Ó¿¨³ß";
            this.btnAddCircleCallipers.UseVisualStyleBackColor = false;

            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox4.Controls.Add(this.combCircleCalipersDir);
            this.groupBox4.Controls.Add(this.combCircleCalipersPolarity);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersWidth);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersThreshold);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersIgnoreDots);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersNum);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersLength);
            this.groupBox4.Controls.Add(this.numUDCircleCalipersFiterSize);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox4.Location = new System.Drawing.Point(920, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 201);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "¿¨³ß²ÎÊý";
            // 
            // combCircleCalipersDir
            // 
            this.combCircleCalipersDir.FormattingEnabled = true;
            this.combCircleCalipersDir.Items.AddRange(new object[] {
            "ÄÚµ½Íâ",
            "Íâµ½ÄÚ"});
            this.combCircleCalipersDir.Location = new System.Drawing.Point(344, 146);
            this.combCircleCalipersDir.Name = "combCircleCalipersDir";
            this.combCircleCalipersDir.Size = new System.Drawing.Size(84, 30);
            this.combCircleCalipersDir.TabIndex = 2;

            // 
            // combCircleCalipersPolarity
            // 
            this.combCircleCalipersPolarity.FormattingEnabled = true;
            this.combCircleCalipersPolarity.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combCircleCalipersPolarity.Location = new System.Drawing.Point(99, 146);
            this.combCircleCalipersPolarity.Name = "combCircleCalipersPolarity";
            this.combCircleCalipersPolarity.Size = new System.Drawing.Size(84, 30);
            this.combCircleCalipersPolarity.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(11, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "¿¨³ßÊýÁ¿:";
            // 
            // numUDCircleCalipersWidth
            // 
            this.numUDCircleCalipersWidth.Location = new System.Drawing.Point(99, 68);
            this.numUDCircleCalipersWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDCircleCalipersWidth.Name = "numUDCircleCalipersWidth";
            this.numUDCircleCalipersWidth.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersWidth.TabIndex = 1;
            this.numUDCircleCalipersWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(252, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "ËÑË÷·½Ïò:";
            // 
            // numUDCircleCalipersThreshold
            // 
            this.numUDCircleCalipersThreshold.Location = new System.Drawing.Point(344, 68);
            this.numUDCircleCalipersThreshold.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUDCircleCalipersThreshold.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDCircleCalipersThreshold.Name = "numUDCircleCalipersThreshold";
            this.numUDCircleCalipersThreshold.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersThreshold.TabIndex = 1;
            this.numUDCircleCalipersThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label26.Location = new System.Drawing.Point(252, 72);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(79, 22);
            this.label26.TabIndex = 0;
            this.label26.Text = "±ßÔµãÐÖµ:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label20.Location = new System.Drawing.Point(252, 33);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 22);
            this.label20.TabIndex = 0;
            this.label20.Text = "¿¨³ß³¤¶È:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label21.Location = new System.Drawing.Point(11, 150);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 22);
            this.label21.TabIndex = 0;
            this.label21.Text = "ËÑË÷¼«ÐÔ:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label23.Location = new System.Drawing.Point(252, 111);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 22);
            this.label23.TabIndex = 0;
            this.label23.Text = "ÂË²¨³ß´ç:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label24.Location = new System.Drawing.Point(11, 72);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 22);
            this.label24.TabIndex = 0;
            this.label24.Text = "¿¨³ß¿í¶È:";
            // 
            // numUDCircleCalipersIgnoreDots
            // 
            this.numUDCircleCalipersIgnoreDots.Location = new System.Drawing.Point(99, 107);
            this.numUDCircleCalipersIgnoreDots.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDCircleCalipersIgnoreDots.Name = "numUDCircleCalipersIgnoreDots";
            this.numUDCircleCalipersIgnoreDots.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersIgnoreDots.TabIndex = 1;
            // 
            // numUDCircleCalipersNum
            // 
            this.numUDCircleCalipersNum.Location = new System.Drawing.Point(99, 29);
            this.numUDCircleCalipersNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numUDCircleCalipersNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDCircleCalipersNum.Name = "numUDCircleCalipersNum";
            this.numUDCircleCalipersNum.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersNum.TabIndex = 1;
            this.numUDCircleCalipersNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});

            // 
            // numUDCircleCalipersLength
            // 
            this.numUDCircleCalipersLength.Location = new System.Drawing.Point(344, 29);
            this.numUDCircleCalipersLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDCircleCalipersLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDCircleCalipersLength.Name = "numUDCircleCalipersLength";
            this.numUDCircleCalipersLength.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersLength.TabIndex = 1;
            this.numUDCircleCalipersLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});

            // 
            // numUDCircleCalipersFiterSize
            // 
            this.numUDCircleCalipersFiterSize.Location = new System.Drawing.Point(344, 107);
            this.numUDCircleCalipersFiterSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDCircleCalipersFiterSize.Name = "numUDCircleCalipersFiterSize";
            this.numUDCircleCalipersFiterSize.Size = new System.Drawing.Size(84, 29);
            this.numUDCircleCalipersFiterSize.TabIndex = 1;
            this.numUDCircleCalipersFiterSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label27.Location = new System.Drawing.Point(11, 111);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(79, 22);
            this.label27.TabIndex = 0;
            this.label27.Text = "ºöÂÔµãÊý:";
            // 
            // u_DisPlay2
            // 
            this.u_DisPlay2.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay2.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay2.Image = null;
            this.u_DisPlay2.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay2.Name = "u_DisPlay2";
            this.u_DisPlay2.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay2.TabIndex = 1;
            this.u_DisPlay2.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.groupBox12);
            this.tabPage3.Controls.Add(this.groupBox10);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Controls.Add(this.u_DisPlay3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1374, 826);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "½»Ïß¿¨³ß";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox11.Controls.Add(this.txtCrossY);
            this.groupBox11.Controls.Add(this.label51);
            this.groupBox11.Controls.Add(this.txtCrossA);
            this.groupBox11.Controls.Add(this.label52);
            this.groupBox11.Controls.Add(this.txtCrossX);
            this.groupBox11.Controls.Add(this.label53);
            this.groupBox11.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox11.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox11.Location = new System.Drawing.Point(920, 560);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(449, 119);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "²éÕÒ½á¹û";
            // 
            // txtCrossY
            // 
            this.txtCrossY.Location = new System.Drawing.Point(344, 33);
            this.txtCrossY.Name = "txtCrossY";
            this.txtCrossY.Size = new System.Drawing.Size(84, 29);
            this.txtCrossY.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label51.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label51.Location = new System.Drawing.Point(273, 37);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(57, 22);
            this.label51.TabIndex = 0;
            this.label51.Text = "½»µãY:";
            // 
            // txtCrossA
            // 
            this.txtCrossA.Location = new System.Drawing.Point(99, 73);
            this.txtCrossA.Name = "txtCrossA";
            this.txtCrossA.Size = new System.Drawing.Size(84, 29);
            this.txtCrossA.TabIndex = 1;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label52.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label52.Location = new System.Drawing.Point(1, 77);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(89, 22);
            this.label52.TabIndex = 0;
            this.label52.Text = "¿¨³ß1½Ç¶È:";
            // 
            // txtCrossX
            // 
            this.txtCrossX.Location = new System.Drawing.Point(99, 33);
            this.txtCrossX.Name = "txtCrossX";
            this.txtCrossX.Size = new System.Drawing.Size(84, 29);
            this.txtCrossX.TabIndex = 1;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label53.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label53.Location = new System.Drawing.Point(32, 37);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(58, 22);
            this.label53.TabIndex = 0;
            this.label53.Text = "½»µãX:";
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox12.Controls.Add(this.btnFindCross);
            this.groupBox12.Controls.Add(this.btnLineCallipersPic);
            this.groupBox12.Controls.Add(this.btnAddLine12Callipers);
            this.groupBox12.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox12.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox12.Location = new System.Drawing.Point(920, 467);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(449, 91);
            this.groupBox12.TabIndex = 5;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "¿¨³ßÖÆ×÷";
            // 
            // btnFindCross
            // 
            this.btnFindCross.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindCross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindCross.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnFindCross.Location = new System.Drawing.Point(309, 33);
            this.btnFindCross.Name = "btnFindCross";
            this.btnFindCross.Size = new System.Drawing.Size(108, 34);
            this.btnFindCross.TabIndex = 0;
            this.btnFindCross.Text = "²éÕÒ½»µã";
            this.btnFindCross.UseVisualStyleBackColor = false;
            this.btnFindCross.Click += new System.EventHandler(this.btnFindCross_Click);
            // 
            // btnLineCallipersPic
            // 
            this.btnLineCallipersPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnLineCallipersPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineCallipersPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnLineCallipersPic.Location = new System.Drawing.Point(36, 33);
            this.btnLineCallipersPic.Name = "btnLineCallipersPic";
            this.btnLineCallipersPic.Size = new System.Drawing.Size(108, 34);
            this.btnLineCallipersPic.TabIndex = 0;
            this.btnLineCallipersPic.Text = "¿¨³ßÍ¼Æ¬";
            this.btnLineCallipersPic.UseVisualStyleBackColor = false;
            this.btnLineCallipersPic.Click += new System.EventHandler(this.btnLineCallipersPic_Click);
            // 
            // btnAddLine12Callipers
            // 
            this.btnAddLine12Callipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddLine12Callipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLine12Callipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddLine12Callipers.Location = new System.Drawing.Point(173, 33);
            this.btnAddLine12Callipers.Name = "btnAddLine12Callipers";
            this.btnAddLine12Callipers.Size = new System.Drawing.Size(108, 34);
            this.btnAddLine12Callipers.TabIndex = 0;
            this.btnAddLine12Callipers.Text = "Ìí¼ÓË«¿¨³ß";
            this.btnAddLine12Callipers.UseVisualStyleBackColor = false;
            this.btnAddLine12Callipers.Click += new System.EventHandler(this.btnAddLine12Callipers_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox10.Controls.Add(this.combLine2CalipersDir);
            this.groupBox10.Controls.Add(this.combLine2CalipersPolarity);
            this.groupBox10.Controls.Add(this.label42);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersWidth);
            this.groupBox10.Controls.Add(this.label43);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersThreshold);
            this.groupBox10.Controls.Add(this.label44);
            this.groupBox10.Controls.Add(this.label45);
            this.groupBox10.Controls.Add(this.label46);
            this.groupBox10.Controls.Add(this.label47);
            this.groupBox10.Controls.Add(this.label48);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersMaxError);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersIgnoreDots);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersNum);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersLength);
            this.groupBox10.Controls.Add(this.label49);
            this.groupBox10.Controls.Add(this.numUDLine2CalipersFiterSize);
            this.groupBox10.Controls.Add(this.label50);
            this.groupBox10.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox10.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox10.Location = new System.Drawing.Point(920, 234);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(449, 231);
            this.groupBox10.TabIndex = 3;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Ö±Ïß2²ÎÊý";
            // 
            // combLine2CalipersDir
            // 
            this.combLine2CalipersDir.FormattingEnabled = true;
            this.combLine2CalipersDir.Items.AddRange(new object[] {
            "ÄÚµ½Íâ",
            "Íâµ½ÄÚ"});
            this.combLine2CalipersDir.Location = new System.Drawing.Point(344, 146);
            this.combLine2CalipersDir.Name = "combLine2CalipersDir";
            this.combLine2CalipersDir.Size = new System.Drawing.Size(84, 30);
            this.combLine2CalipersDir.TabIndex = 2;
            this.combLine2CalipersDir.SelectedIndexChanged += new System.EventHandler(this.combLine2CalipersDir_SelectedIndexChanged);
            // 
            // combLine2CalipersPolarity
            // 
            this.combLine2CalipersPolarity.FormattingEnabled = true;
            this.combLine2CalipersPolarity.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combLine2CalipersPolarity.Location = new System.Drawing.Point(99, 185);
            this.combLine2CalipersPolarity.Name = "combLine2CalipersPolarity";
            this.combLine2CalipersPolarity.Size = new System.Drawing.Size(84, 30);
            this.combLine2CalipersPolarity.TabIndex = 2;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label42.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label42.Location = new System.Drawing.Point(11, 33);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(79, 22);
            this.label42.TabIndex = 0;
            this.label42.Text = "¿¨³ßÊýÁ¿:";
            // 
            // numUDLine2CalipersWidth
            // 
            this.numUDLine2CalipersWidth.Location = new System.Drawing.Point(99, 68);
            this.numUDLine2CalipersWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine2CalipersWidth.Name = "numUDLine2CalipersWidth";
            this.numUDLine2CalipersWidth.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersWidth.TabIndex = 1;
            this.numUDLine2CalipersWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine2CalipersWidth.ValueChanged += new System.EventHandler(this.numUDLine2CalipersWidth_ValueChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label43.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label43.Location = new System.Drawing.Point(252, 150);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(79, 22);
            this.label43.TabIndex = 0;
            this.label43.Text = "ËÑË÷·½Ïò:";
            // 
            // numUDLine2CalipersThreshold
            // 
            this.numUDLine2CalipersThreshold.Location = new System.Drawing.Point(344, 68);
            this.numUDLine2CalipersThreshold.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUDLine2CalipersThreshold.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDLine2CalipersThreshold.Name = "numUDLine2CalipersThreshold";
            this.numUDLine2CalipersThreshold.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersThreshold.TabIndex = 1;
            this.numUDLine2CalipersThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label44.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label44.Location = new System.Drawing.Point(252, 72);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(79, 22);
            this.label44.TabIndex = 0;
            this.label44.Text = "±ßÔµãÐÖµ:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label45.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label45.Location = new System.Drawing.Point(252, 33);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(79, 22);
            this.label45.TabIndex = 0;
            this.label45.Text = "¿¨³ß³¤¶È:";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label46.Location = new System.Drawing.Point(11, 189);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(79, 22);
            this.label46.TabIndex = 0;
            this.label46.Text = "ËÑË÷¼«ÐÔ:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label47.Location = new System.Drawing.Point(252, 111);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(79, 22);
            this.label47.TabIndex = 0;
            this.label47.Text = "ÂË²¨³ß´ç:";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label48.Location = new System.Drawing.Point(11, 72);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(79, 22);
            this.label48.TabIndex = 0;
            this.label48.Text = "¿¨³ß¿í¶È:";
            // 
            // numUDLine2CalipersMaxError
            // 
            this.numUDLine2CalipersMaxError.Location = new System.Drawing.Point(99, 146);
            this.numUDLine2CalipersMaxError.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine2CalipersMaxError.Name = "numUDLine2CalipersMaxError";
            this.numUDLine2CalipersMaxError.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersMaxError.TabIndex = 1;
            this.numUDLine2CalipersMaxError.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numUDLine2CalipersIgnoreDots
            // 
            this.numUDLine2CalipersIgnoreDots.Location = new System.Drawing.Point(99, 107);
            this.numUDLine2CalipersIgnoreDots.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine2CalipersIgnoreDots.Name = "numUDLine2CalipersIgnoreDots";
            this.numUDLine2CalipersIgnoreDots.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersIgnoreDots.TabIndex = 1;
            // 
            // numUDLine2CalipersNum
            // 
            this.numUDLine2CalipersNum.Location = new System.Drawing.Point(99, 29);
            this.numUDLine2CalipersNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numUDLine2CalipersNum.Name = "numUDLine2CalipersNum";
            this.numUDLine2CalipersNum.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersNum.TabIndex = 1;
            this.numUDLine2CalipersNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUDLine2CalipersNum.ValueChanged += new System.EventHandler(this.numUDLine2CalipersNum_ValueChanged);
            // 
            // numUDLine2CalipersLength
            // 
            this.numUDLine2CalipersLength.Location = new System.Drawing.Point(344, 29);
            this.numUDLine2CalipersLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine2CalipersLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine2CalipersLength.Name = "numUDLine2CalipersLength";
            this.numUDLine2CalipersLength.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersLength.TabIndex = 1;
            this.numUDLine2CalipersLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUDLine2CalipersLength.ValueChanged += new System.EventHandler(this.numUDLine2CalipersLength_ValueChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label49.Location = new System.Drawing.Point(11, 150);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(79, 22);
            this.label49.TabIndex = 0;
            this.label49.Text = "ÔÊÐíÆ«²î:";
            // 
            // numUDLine2CalipersFiterSize
            // 
            this.numUDLine2CalipersFiterSize.Location = new System.Drawing.Point(344, 107);
            this.numUDLine2CalipersFiterSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine2CalipersFiterSize.Name = "numUDLine2CalipersFiterSize";
            this.numUDLine2CalipersFiterSize.Size = new System.Drawing.Size(84, 29);
            this.numUDLine2CalipersFiterSize.TabIndex = 1;
            this.numUDLine2CalipersFiterSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label50.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label50.Location = new System.Drawing.Point(11, 111);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(79, 22);
            this.label50.TabIndex = 0;
            this.label50.Text = "ºöÂÔµãÊý:";
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox9.Controls.Add(this.combLine1CalipersDir);
            this.groupBox9.Controls.Add(this.combLine1CalipersPolarity);
            this.groupBox9.Controls.Add(this.label31);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersWidth);
            this.groupBox9.Controls.Add(this.label34);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersThreshold);
            this.groupBox9.Controls.Add(this.label35);
            this.groupBox9.Controls.Add(this.label36);
            this.groupBox9.Controls.Add(this.label37);
            this.groupBox9.Controls.Add(this.label38);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersMaxError);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersIgnoreDots);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersNum);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersLength);
            this.groupBox9.Controls.Add(this.label41);
            this.groupBox9.Controls.Add(this.numUDLine1CalipersFiterSize);
            this.groupBox9.Controls.Add(this.label40);
            this.groupBox9.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox9.Location = new System.Drawing.Point(920, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(449, 231);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ö±Ïß1²ÎÊý";
            // 
            // combLine1CalipersDir
            // 
            this.combLine1CalipersDir.FormattingEnabled = true;
            this.combLine1CalipersDir.Items.AddRange(new object[] {
            "ÄÚµ½Íâ",
            "Íâµ½ÄÚ"});
            this.combLine1CalipersDir.Location = new System.Drawing.Point(344, 146);
            this.combLine1CalipersDir.Name = "combLine1CalipersDir";
            this.combLine1CalipersDir.Size = new System.Drawing.Size(84, 30);
            this.combLine1CalipersDir.TabIndex = 2;
            this.combLine1CalipersDir.SelectedIndexChanged += new System.EventHandler(this.combLine1CalipersDir_SelectedIndexChanged);
            // 
            // combLine1CalipersPolarity
            // 
            this.combLine1CalipersPolarity.FormattingEnabled = true;
            this.combLine1CalipersPolarity.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combLine1CalipersPolarity.Location = new System.Drawing.Point(99, 185);
            this.combLine1CalipersPolarity.Name = "combLine1CalipersPolarity";
            this.combLine1CalipersPolarity.Size = new System.Drawing.Size(84, 30);
            this.combLine1CalipersPolarity.TabIndex = 2;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label31.Location = new System.Drawing.Point(11, 33);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(79, 22);
            this.label31.TabIndex = 0;
            this.label31.Text = "¿¨³ßÊýÁ¿:";
            // 
            // numUDLine1CalipersWidth
            // 
            this.numUDLine1CalipersWidth.Location = new System.Drawing.Point(99, 68);
            this.numUDLine1CalipersWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine1CalipersWidth.Name = "numUDLine1CalipersWidth";
            this.numUDLine1CalipersWidth.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersWidth.TabIndex = 1;
            this.numUDLine1CalipersWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine1CalipersWidth.ValueChanged += new System.EventHandler(this.numUDLine1CalipersWidth_ValueChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label34.Location = new System.Drawing.Point(252, 150);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(79, 22);
            this.label34.TabIndex = 0;
            this.label34.Text = "ËÑË÷·½Ïò:";
            // 
            // numUDLine1CalipersThreshold
            // 
            this.numUDLine1CalipersThreshold.Location = new System.Drawing.Point(344, 68);
            this.numUDLine1CalipersThreshold.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUDLine1CalipersThreshold.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDLine1CalipersThreshold.Name = "numUDLine1CalipersThreshold";
            this.numUDLine1CalipersThreshold.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersThreshold.TabIndex = 1;
            this.numUDLine1CalipersThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label35.Location = new System.Drawing.Point(252, 72);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 22);
            this.label35.TabIndex = 0;
            this.label35.Text = "±ßÔµãÐÖµ:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label36.Location = new System.Drawing.Point(252, 33);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(79, 22);
            this.label36.TabIndex = 0;
            this.label36.Text = "¿¨³ß³¤¶È:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label37.Location = new System.Drawing.Point(11, 189);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(79, 22);
            this.label37.TabIndex = 0;
            this.label37.Text = "ËÑË÷¼«ÐÔ:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label38.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label38.Location = new System.Drawing.Point(252, 111);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(79, 22);
            this.label38.TabIndex = 0;
            this.label38.Text = "ÂË²¨³ß´ç:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label39.Location = new System.Drawing.Point(11, 72);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(79, 22);
            this.label39.TabIndex = 0;
            this.label39.Text = "¿¨³ß¿í¶È:";
            // 
            // numUDLine1CalipersMaxError
            // 
            this.numUDLine1CalipersMaxError.Location = new System.Drawing.Point(99, 146);
            this.numUDLine1CalipersMaxError.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine1CalipersMaxError.Name = "numUDLine1CalipersMaxError";
            this.numUDLine1CalipersMaxError.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersMaxError.TabIndex = 1;
            this.numUDLine1CalipersMaxError.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numUDLine1CalipersIgnoreDots
            // 
            this.numUDLine1CalipersIgnoreDots.Location = new System.Drawing.Point(99, 107);
            this.numUDLine1CalipersIgnoreDots.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine1CalipersIgnoreDots.Name = "numUDLine1CalipersIgnoreDots";
            this.numUDLine1CalipersIgnoreDots.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersIgnoreDots.TabIndex = 1;
            // 
            // numUDLine1CalipersNum
            // 
            this.numUDLine1CalipersNum.Location = new System.Drawing.Point(99, 29);
            this.numUDLine1CalipersNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numUDLine1CalipersNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine1CalipersNum.Name = "numUDLine1CalipersNum";
            this.numUDLine1CalipersNum.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersNum.TabIndex = 1;
            this.numUDLine1CalipersNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUDLine1CalipersNum.ValueChanged += new System.EventHandler(this.numUDLine1CalipersNum_ValueChanged);
            // 
            // numUDLine1CalipersLength
            // 
            this.numUDLine1CalipersLength.Location = new System.Drawing.Point(344, 29);
            this.numUDLine1CalipersLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine1CalipersLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDLine1CalipersLength.Name = "numUDLine1CalipersLength";
            this.numUDLine1CalipersLength.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersLength.TabIndex = 1;
            this.numUDLine1CalipersLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUDLine1CalipersLength.ValueChanged += new System.EventHandler(this.numUDLine1CalipersLength_ValueChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label41.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label41.Location = new System.Drawing.Point(11, 150);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(79, 22);
            this.label41.TabIndex = 0;
            this.label41.Text = "ÔÊÐíÆ«²î:";
            // 
            // numUDLine1CalipersFiterSize
            // 
            this.numUDLine1CalipersFiterSize.Location = new System.Drawing.Point(344, 107);
            this.numUDLine1CalipersFiterSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDLine1CalipersFiterSize.Name = "numUDLine1CalipersFiterSize";
            this.numUDLine1CalipersFiterSize.Size = new System.Drawing.Size(84, 29);
            this.numUDLine1CalipersFiterSize.TabIndex = 1;
            this.numUDLine1CalipersFiterSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label40.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label40.Location = new System.Drawing.Point(11, 111);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(79, 22);
            this.label40.TabIndex = 0;
            this.label40.Text = "ºöÂÔµãÊý:";
            // 
            // u_DisPlay3
            // 
            this.u_DisPlay3.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay3.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay3.Image = null;
            this.u_DisPlay3.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay3.Name = "u_DisPlay3";
            this.u_DisPlay3.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay3.TabIndex = 2;
            this.u_DisPlay3.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage4.Controls.Add(this.groupBox14);
            this.tabPage4.Controls.Add(this.groupBox15);
            this.tabPage4.Controls.Add(this.groupBox13);
            this.tabPage4.Controls.Add(this.u_DisPlay4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1374, 826);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "¾ØÐÎ¿¨³ß";
            // 
            // groupBox14
            // 
            this.groupBox14.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox14.Controls.Add(this.txtRectCaliperY);
            this.groupBox14.Controls.Add(this.label62);
            this.groupBox14.Controls.Add(this.txtRectCaliperA);
            this.groupBox14.Controls.Add(this.label63);
            this.groupBox14.Controls.Add(this.txtRectCaliperX);
            this.groupBox14.Controls.Add(this.label64);
            this.groupBox14.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox14.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox14.Location = new System.Drawing.Point(920, 328);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(449, 119);
            this.groupBox14.TabIndex = 4;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "²éÕÒ½á¹û";
            // 
            // txtRectCaliperY
            // 
            this.txtRectCaliperY.Location = new System.Drawing.Point(344, 33);
            this.txtRectCaliperY.Name = "txtRectCaliperY";
            this.txtRectCaliperY.Size = new System.Drawing.Size(84, 29);
            this.txtRectCaliperY.TabIndex = 1;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label62.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label62.Location = new System.Drawing.Point(273, 37);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(57, 22);
            this.label62.TabIndex = 0;
            this.label62.Text = "ÖÐÐÄY:";
            // 
            // txtRectCaliperA
            // 
            this.txtRectCaliperA.Location = new System.Drawing.Point(99, 73);
            this.txtRectCaliperA.Name = "txtRectCaliperA";
            this.txtRectCaliperA.Size = new System.Drawing.Size(84, 29);
            this.txtRectCaliperA.TabIndex = 1;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label63.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label63.Location = new System.Drawing.Point(12, 77);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(79, 22);
            this.label63.TabIndex = 0;
            this.label63.Text = "¾ØÐÎ½Ç¶È:";
            // 
            // txtRectCaliperX
            // 
            this.txtRectCaliperX.Location = new System.Drawing.Point(99, 33);
            this.txtRectCaliperX.Name = "txtRectCaliperX";
            this.txtRectCaliperX.Size = new System.Drawing.Size(84, 29);
            this.txtRectCaliperX.TabIndex = 1;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label64.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label64.Location = new System.Drawing.Point(32, 37);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(58, 22);
            this.label64.TabIndex = 0;
            this.label64.Text = "ÖÐÐÄX:";
            // 
            // groupBox15
            // 
            this.groupBox15.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox15.Controls.Add(this.btnFindRectCallipers);
            this.groupBox15.Controls.Add(this.btnRectCallipersPic);
            this.groupBox15.Controls.Add(this.btnAddRectCallipers);
            this.groupBox15.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox15.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox15.Location = new System.Drawing.Point(920, 230);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(449, 96);
            this.groupBox15.TabIndex = 5;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "¿¨³ßÖÆ×÷";
            // 
            // btnFindRectCallipers
            // 
            this.btnFindRectCallipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindRectCallipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindRectCallipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnFindRectCallipers.Location = new System.Drawing.Point(310, 34);
            this.btnFindRectCallipers.Name = "btnFindRectCallipers";
            this.btnFindRectCallipers.Size = new System.Drawing.Size(108, 34);
            this.btnFindRectCallipers.TabIndex = 0;
            this.btnFindRectCallipers.Text = "²éÕÒ¿¨³ß";
            this.btnFindRectCallipers.UseVisualStyleBackColor = false;

            // 
            // btnRectCallipersPic
            // 
            this.btnRectCallipersPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnRectCallipersPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRectCallipersPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRectCallipersPic.Location = new System.Drawing.Point(36, 34);
            this.btnRectCallipersPic.Name = "btnRectCallipersPic";
            this.btnRectCallipersPic.Size = new System.Drawing.Size(108, 34);
            this.btnRectCallipersPic.TabIndex = 0;
            this.btnRectCallipersPic.Text = "¿¨³ßÍ¼Æ¬";
            this.btnRectCallipersPic.UseVisualStyleBackColor = false;

            // 
            // btnAddRectCallipers
            // 
            this.btnAddRectCallipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddRectCallipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRectCallipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddRectCallipers.Location = new System.Drawing.Point(173, 34);
            this.btnAddRectCallipers.Name = "btnAddRectCallipers";
            this.btnAddRectCallipers.Size = new System.Drawing.Size(108, 34);
            this.btnAddRectCallipers.TabIndex = 0;
            this.btnAddRectCallipers.Text = "Ìí¼Ó¿¨³ß";
            this.btnAddRectCallipers.UseVisualStyleBackColor = false;

            // 
            // groupBox13
            // 
            this.groupBox13.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox13.Controls.Add(this.combRectCalipersDir);
            this.groupBox13.Controls.Add(this.combRectCalipersPolarity);
            this.groupBox13.Controls.Add(this.label54);
            this.groupBox13.Controls.Add(this.numUDRectCalipersWidth);
            this.groupBox13.Controls.Add(this.label55);
            this.groupBox13.Controls.Add(this.numUDRectCalipersThreshold);
            this.groupBox13.Controls.Add(this.label56);
            this.groupBox13.Controls.Add(this.label57);
            this.groupBox13.Controls.Add(this.label58);
            this.groupBox13.Controls.Add(this.label59);
            this.groupBox13.Controls.Add(this.label60);
            this.groupBox13.Controls.Add(this.numUDRectCalipersMaxError);
            this.groupBox13.Controls.Add(this.numUDRectCalipersIgnoreDots);
            this.groupBox13.Controls.Add(this.numUDRectCalipersNum);
            this.groupBox13.Controls.Add(this.numUDRectCalipersLength);
            this.groupBox13.Controls.Add(this.label65);
            this.groupBox13.Controls.Add(this.numUDRectCalipersFiterSize);
            this.groupBox13.Controls.Add(this.label61);
            this.groupBox13.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox13.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox13.Location = new System.Drawing.Point(920, 0);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(449, 227);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "¿¨³ß²ÎÊý";
            // 
            // combRectCalipersDir
            // 
            this.combRectCalipersDir.FormattingEnabled = true;
            this.combRectCalipersDir.Items.AddRange(new object[] {
            "ÄÚµ½Íâ",
            "Íâµ½ÄÚ"});
            this.combRectCalipersDir.Location = new System.Drawing.Point(344, 146);
            this.combRectCalipersDir.Name = "combRectCalipersDir";
            this.combRectCalipersDir.Size = new System.Drawing.Size(84, 30);
            this.combRectCalipersDir.TabIndex = 2;

            // 
            // combRectCalipersPolarity
            // 
            this.combRectCalipersPolarity.FormattingEnabled = true;
            this.combRectCalipersPolarity.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combRectCalipersPolarity.Location = new System.Drawing.Point(99, 185);
            this.combRectCalipersPolarity.Name = "combRectCalipersPolarity";
            this.combRectCalipersPolarity.Size = new System.Drawing.Size(84, 30);
            this.combRectCalipersPolarity.TabIndex = 2;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label54.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label54.Location = new System.Drawing.Point(11, 33);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(79, 22);
            this.label54.TabIndex = 0;
            this.label54.Text = "¿¨³ßÊýÁ¿:";
            // 
            // numUDRectCalipersWidth
            // 
            this.numUDRectCalipersWidth.Location = new System.Drawing.Point(99, 68);
            this.numUDRectCalipersWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDRectCalipersWidth.Name = "numUDRectCalipersWidth";
            this.numUDRectCalipersWidth.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersWidth.TabIndex = 1;
            this.numUDRectCalipersWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label55.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label55.Location = new System.Drawing.Point(252, 150);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(79, 22);
            this.label55.TabIndex = 0;
            this.label55.Text = "ËÑË÷·½Ïò:";
            // 
            // numUDRectCalipersThreshold
            // 
            this.numUDRectCalipersThreshold.Location = new System.Drawing.Point(344, 68);
            this.numUDRectCalipersThreshold.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUDRectCalipersThreshold.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDRectCalipersThreshold.Name = "numUDRectCalipersThreshold";
            this.numUDRectCalipersThreshold.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersThreshold.TabIndex = 1;
            this.numUDRectCalipersThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label56.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label56.Location = new System.Drawing.Point(252, 72);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(79, 22);
            this.label56.TabIndex = 0;
            this.label56.Text = "±ßÔµãÐÖµ:";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label57.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label57.Location = new System.Drawing.Point(252, 33);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(79, 22);
            this.label57.TabIndex = 0;
            this.label57.Text = "¿¨³ß³¤¶È:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label58.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label58.Location = new System.Drawing.Point(11, 189);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(79, 22);
            this.label58.TabIndex = 0;
            this.label58.Text = "ËÑË÷¼«ÐÔ:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label59.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label59.Location = new System.Drawing.Point(252, 111);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(79, 22);
            this.label59.TabIndex = 0;
            this.label59.Text = "ÂË²¨³ß´ç:";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label60.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label60.Location = new System.Drawing.Point(11, 72);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(79, 22);
            this.label60.TabIndex = 0;
            this.label60.Text = "¿¨³ß¿í¶È:";
            // 
            // numUDRectCalipersMaxError
            // 
            this.numUDRectCalipersMaxError.Location = new System.Drawing.Point(99, 146);
            this.numUDRectCalipersMaxError.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDRectCalipersMaxError.Name = "numUDRectCalipersMaxError";
            this.numUDRectCalipersMaxError.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersMaxError.TabIndex = 1;
            this.numUDRectCalipersMaxError.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numUDRectCalipersIgnoreDots
            // 
            this.numUDRectCalipersIgnoreDots.Location = new System.Drawing.Point(99, 107);
            this.numUDRectCalipersIgnoreDots.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDRectCalipersIgnoreDots.Name = "numUDRectCalipersIgnoreDots";
            this.numUDRectCalipersIgnoreDots.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersIgnoreDots.TabIndex = 1;
            // 
            // numUDRectCalipersNum
            // 
            this.numUDRectCalipersNum.Location = new System.Drawing.Point(99, 29);
            this.numUDRectCalipersNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numUDRectCalipersNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDRectCalipersNum.Name = "numUDRectCalipersNum";
            this.numUDRectCalipersNum.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersNum.TabIndex = 1;
            this.numUDRectCalipersNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});

            // 
            // numUDRectCalipersLength
            // 
            this.numUDRectCalipersLength.Location = new System.Drawing.Point(344, 29);
            this.numUDRectCalipersLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDRectCalipersLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDRectCalipersLength.Name = "numUDRectCalipersLength";
            this.numUDRectCalipersLength.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersLength.TabIndex = 1;
            this.numUDRectCalipersLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});

            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label65.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label65.Location = new System.Drawing.Point(11, 150);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(79, 22);
            this.label65.TabIndex = 0;
            this.label65.Text = "ÔÊÐíÆ«²î:";
            // 
            // numUDRectCalipersFiterSize
            // 
            this.numUDRectCalipersFiterSize.Location = new System.Drawing.Point(344, 107);
            this.numUDRectCalipersFiterSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDRectCalipersFiterSize.Name = "numUDRectCalipersFiterSize";
            this.numUDRectCalipersFiterSize.Size = new System.Drawing.Size(84, 29);
            this.numUDRectCalipersFiterSize.TabIndex = 1;
            this.numUDRectCalipersFiterSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label61.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label61.Location = new System.Drawing.Point(11, 111);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(79, 22);
            this.label61.TabIndex = 0;
            this.label61.Text = "ºöÂÔµãÊý:";
            // 
            // u_DisPlay4
            // 
            this.u_DisPlay4.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay4.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay4.Image = null;
            this.u_DisPlay4.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay4.Name = "u_DisPlay4";
            this.u_DisPlay4.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay4.TabIndex = 2;
            this.u_DisPlay4.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1374, 826);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "ÆåÅÌ¸ñ±ê¶¨";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage6.Controls.Add(this.groupBox8);
            this.tabPage6.Controls.Add(this.groupBox7);
            this.tabPage6.Controls.Add(this.u_DisPlay6);
            this.tabPage6.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1374, 826);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "»û±ä½ÃÕý";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox8.Controls.Add(this.btnCreateCheckBoardParam);
            this.groupBox8.Controls.Add(this.btnCheckCheckBoardParam);
            this.groupBox8.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox8.Location = new System.Drawing.Point(920, 134);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(449, 92);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Éú³É¾ØÕó";
            // 
            // btnCreateCheckBoardParam
            // 
            this.btnCreateCheckBoardParam.BackColor = System.Drawing.SystemColors.Control;
            this.btnCreateCheckBoardParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateCheckBoardParam.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCreateCheckBoardParam.Location = new System.Drawing.Point(36, 34);
            this.btnCreateCheckBoardParam.Name = "btnCreateCheckBoardParam";
            this.btnCreateCheckBoardParam.Size = new System.Drawing.Size(127, 34);
            this.btnCreateCheckBoardParam.TabIndex = 0;
            this.btnCreateCheckBoardParam.Text = "×Ô¶¯Éú³É¾ØÕó";
            this.btnCreateCheckBoardParam.UseVisualStyleBackColor = false;

            // 
            // btnCheckCheckBoardParam
            // 
            this.btnCheckCheckBoardParam.BackColor = System.Drawing.SystemColors.Control;
            this.btnCheckCheckBoardParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckCheckBoardParam.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCheckCheckBoardParam.Location = new System.Drawing.Point(292, 34);
            this.btnCheckCheckBoardParam.Name = "btnCheckCheckBoardParam";
            this.btnCheckCheckBoardParam.Size = new System.Drawing.Size(127, 34);
            this.btnCheckCheckBoardParam.TabIndex = 0;
            this.btnCheckCheckBoardParam.Text = "½ÃÕýÐ§¹û²é¿´";
            this.btnCheckCheckBoardParam.UseVisualStyleBackColor = false;

            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.numUDCheckerBoardHeight);
            this.groupBox7.Controls.Add(this.numUDCornerPixlSize);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.numUDCheckerBoardDis);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.numUDCheckerBoardWidth);
            this.groupBox7.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox7.Location = new System.Drawing.Point(920, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(449, 132);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "ÆåÅÌ¸ñ²ÎÊý";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label25.Location = new System.Drawing.Point(231, 76);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(111, 22);
            this.label25.TabIndex = 0;
            this.label25.Text = "½ÇµãÏñËØ³ß´ç:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label30.Location = new System.Drawing.Point(11, 74);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(111, 22);
            this.label30.TabIndex = 0;
            this.label30.Text = "ÕæÊµ·½¿é¼ä¾à:";
            // 
            // numUDCheckerBoardHeight
            // 
            this.numUDCheckerBoardHeight.Location = new System.Drawing.Point(344, 32);
            this.numUDCheckerBoardHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUDCheckerBoardHeight.Name = "numUDCheckerBoardHeight";
            this.numUDCheckerBoardHeight.Size = new System.Drawing.Size(84, 29);
            this.numUDCheckerBoardHeight.TabIndex = 1;
            this.numUDCheckerBoardHeight.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numUDCornerPixlSize
            // 
            this.numUDCornerPixlSize.Location = new System.Drawing.Point(344, 70);
            this.numUDCornerPixlSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUDCornerPixlSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDCornerPixlSize.Name = "numUDCornerPixlSize";
            this.numUDCornerPixlSize.Size = new System.Drawing.Size(84, 29);
            this.numUDCornerPixlSize.TabIndex = 1;
            this.numUDCornerPixlSize.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label32.Location = new System.Drawing.Point(11, 36);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(111, 22);
            this.label32.TabIndex = 0;
            this.label32.Text = "ÆåÅÌ¸ñ¿íµãÊý:";
            // 
            // numUDCheckerBoardDis
            // 
            this.numUDCheckerBoardDis.DecimalPlaces = 1;
            this.numUDCheckerBoardDis.Location = new System.Drawing.Point(127, 70);
            this.numUDCheckerBoardDis.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUDCheckerBoardDis.Name = "numUDCheckerBoardDis";
            this.numUDCheckerBoardDis.Size = new System.Drawing.Size(84, 29);
            this.numUDCheckerBoardDis.TabIndex = 1;
            this.numUDCheckerBoardDis.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label33.Location = new System.Drawing.Point(231, 36);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(111, 22);
            this.label33.TabIndex = 0;
            this.label33.Text = "ÆåÅÌ¸ñ¸ßµãÊý:";
            // 
            // numUDCheckerBoardWidth
            // 
            this.numUDCheckerBoardWidth.Location = new System.Drawing.Point(127, 32);
            this.numUDCheckerBoardWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUDCheckerBoardWidth.Name = "numUDCheckerBoardWidth";
            this.numUDCheckerBoardWidth.Size = new System.Drawing.Size(84, 29);
            this.numUDCheckerBoardWidth.TabIndex = 1;
            this.numUDCheckerBoardWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // u_DisPlay6
            // 
            this.u_DisPlay6.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay6.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay6.Image = null;
            this.u_DisPlay6.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay6.Name = "u_DisPlay6";
            this.u_DisPlay6.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay6.TabIndex = 1;
            this.u_DisPlay6.TabStop = false;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tabPage7.Controls.Add(this.groupBox18);
            this.tabPage7.Controls.Add(this.groupBox17);
            this.tabPage7.Controls.Add(this.groupBox16);
            this.tabPage7.Controls.Add(this.u_DisPlay7);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1374, 826);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "¿¨³ß²â¾à";
            // 
            // groupBox18
            // 
            this.groupBox18.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox18.Controls.Add(this.txtDistanceEndY);
            this.groupBox18.Controls.Add(this.label76);
            this.groupBox18.Controls.Add(this.txtDistanceStartY);
            this.groupBox18.Controls.Add(this.label72);
            this.groupBox18.Controls.Add(this.txtDistance);
            this.groupBox18.Controls.Add(this.txtDistanceEndX);
            this.groupBox18.Controls.Add(this.label73);
            this.groupBox18.Controls.Add(this.label75);
            this.groupBox18.Controls.Add(this.txtDistanceStartX);
            this.groupBox18.Controls.Add(this.label74);
            this.groupBox18.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox18.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox18.Location = new System.Drawing.Point(920, 248);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(449, 166);
            this.groupBox18.TabIndex = 7;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "²éÕÒ½á¹û";
            // 
            // txtDistanceEndY
            // 
            this.txtDistanceEndY.Location = new System.Drawing.Point(344, 74);
            this.txtDistanceEndY.Name = "txtDistanceEndY";
            this.txtDistanceEndY.Size = new System.Drawing.Size(84, 29);
            this.txtDistanceEndY.TabIndex = 1;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label76.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label76.Location = new System.Drawing.Point(273, 78);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(57, 22);
            this.label76.TabIndex = 0;
            this.label76.Text = "ÖÕµãY:";
            // 
            // txtDistanceStartY
            // 
            this.txtDistanceStartY.Location = new System.Drawing.Point(344, 33);
            this.txtDistanceStartY.Name = "txtDistanceStartY";
            this.txtDistanceStartY.Size = new System.Drawing.Size(84, 29);
            this.txtDistanceStartY.TabIndex = 1;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label72.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label72.Location = new System.Drawing.Point(273, 37);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(57, 22);
            this.label72.TabIndex = 0;
            this.label72.Text = "ÆðµãY:";
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(99, 116);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(84, 29);
            this.txtDistance.TabIndex = 1;
            // 
            // txtDistanceEndX
            // 
            this.txtDistanceEndX.Location = new System.Drawing.Point(99, 74);
            this.txtDistanceEndX.Name = "txtDistanceEndX";
            this.txtDistanceEndX.Size = new System.Drawing.Size(84, 29);
            this.txtDistanceEndX.TabIndex = 1;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label73.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label73.Location = new System.Drawing.Point(43, 120);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(47, 22);
            this.label73.TabIndex = 0;
            this.label73.Text = "³¤¶È:";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label75.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label75.Location = new System.Drawing.Point(32, 78);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(58, 22);
            this.label75.TabIndex = 0;
            this.label75.Text = "ÖÕµãX:";
            // 
            // txtDistanceStartX
            // 
            this.txtDistanceStartX.Location = new System.Drawing.Point(99, 33);
            this.txtDistanceStartX.Name = "txtDistanceStartX";
            this.txtDistanceStartX.Size = new System.Drawing.Size(84, 29);
            this.txtDistanceStartX.TabIndex = 1;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label74.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label74.Location = new System.Drawing.Point(32, 37);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(58, 22);
            this.label74.TabIndex = 0;
            this.label74.Text = "ÆðµãX:";
            // 
            // groupBox17
            // 
            this.groupBox17.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox17.Controls.Add(this.btnGetDistance);
            this.groupBox17.Controls.Add(this.btnDistanceCallipersPic);
            this.groupBox17.Controls.Add(this.btnAddDistanceCallipers);
            this.groupBox17.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox17.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox17.Location = new System.Drawing.Point(920, 155);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(449, 91);
            this.groupBox17.TabIndex = 6;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "¿¨³ßÖÆ×÷";
            // 
            // btnGetDistance
            // 
            this.btnGetDistance.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetDistance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDistance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGetDistance.Location = new System.Drawing.Point(309, 33);
            this.btnGetDistance.Name = "btnGetDistance";
            this.btnGetDistance.Size = new System.Drawing.Size(108, 34);
            this.btnGetDistance.TabIndex = 0;
            this.btnGetDistance.Text = "¾àÀë²âÁ¿";
            this.btnGetDistance.UseVisualStyleBackColor = false;

            // 
            // btnDistanceCallipersPic
            // 
            this.btnDistanceCallipersPic.BackColor = System.Drawing.SystemColors.Control;
            this.btnDistanceCallipersPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDistanceCallipersPic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDistanceCallipersPic.Location = new System.Drawing.Point(36, 33);
            this.btnDistanceCallipersPic.Name = "btnDistanceCallipersPic";
            this.btnDistanceCallipersPic.Size = new System.Drawing.Size(108, 34);
            this.btnDistanceCallipersPic.TabIndex = 0;
            this.btnDistanceCallipersPic.Text = "¿¨³ßÍ¼Æ¬";
            this.btnDistanceCallipersPic.UseVisualStyleBackColor = false;

            // 
            // btnAddDistanceCallipers
            // 
            this.btnAddDistanceCallipers.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddDistanceCallipers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDistanceCallipers.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddDistanceCallipers.Location = new System.Drawing.Point(173, 33);
            this.btnAddDistanceCallipers.Name = "btnAddDistanceCallipers";
            this.btnAddDistanceCallipers.Size = new System.Drawing.Size(108, 34);
            this.btnAddDistanceCallipers.TabIndex = 0;
            this.btnAddDistanceCallipers.Text = "Ìí¼Ó¿¨³ß";
            this.btnAddDistanceCallipers.UseVisualStyleBackColor = false;

            // 
            // groupBox16
            // 
            this.groupBox16.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox16.Controls.Add(this.combEndCalipersSearchNumber);
            this.groupBox16.Controls.Add(this.combEndCalipersSearchMode);
            this.groupBox16.Controls.Add(this.combStartCalipersSearchNumber);
            this.groupBox16.Controls.Add(this.label69);
            this.groupBox16.Controls.Add(this.combStartCalipersSearchMode);
            this.groupBox16.Controls.Add(this.label67);
            this.groupBox16.Controls.Add(this.numUDDistanceCalipersThreshold);
            this.groupBox16.Controls.Add(this.label66);
            this.groupBox16.Controls.Add(this.label68);
            this.groupBox16.Controls.Add(this.label70);
            this.groupBox16.Controls.Add(this.label71);
            this.groupBox16.Controls.Add(this.numUDDistanceCalipersFiterSize);
            this.groupBox16.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox16.ForeColor = System.Drawing.Color.SaddleBrown;
            this.groupBox16.Location = new System.Drawing.Point(920, 0);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(449, 154);
            this.groupBox16.TabIndex = 4;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "²â¾à²ÎÊý";
            // 
            // combEndCalipersSearchNumber
            // 
            this.combEndCalipersSearchNumber.FormattingEnabled = true;
            this.combEndCalipersSearchNumber.Items.AddRange(new object[] {
            "µÚ1¸ö",
            "µÚ2¸ö",
            "µÚ3¸ö",
            "µ¹Êý1",
            "µ¹Êý2",
            "µ¹Êý3"});
            this.combEndCalipersSearchNumber.Location = new System.Drawing.Point(344, 113);
            this.combEndCalipersSearchNumber.Name = "combEndCalipersSearchNumber";
            this.combEndCalipersSearchNumber.Size = new System.Drawing.Size(84, 30);
            this.combEndCalipersSearchNumber.TabIndex = 2;
            // 
            // combEndCalipersSearchMode
            // 
            this.combEndCalipersSearchMode.FormattingEnabled = true;
            this.combEndCalipersSearchMode.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combEndCalipersSearchMode.Location = new System.Drawing.Point(131, 113);
            this.combEndCalipersSearchMode.Name = "combEndCalipersSearchMode";
            this.combEndCalipersSearchMode.Size = new System.Drawing.Size(84, 30);
            this.combEndCalipersSearchMode.TabIndex = 2;
            // 
            // combStartCalipersSearchNumber
            // 
            this.combStartCalipersSearchNumber.FormattingEnabled = true;
            this.combStartCalipersSearchNumber.Items.AddRange(new object[] {
            "µÚ1¸ö",
            "µÚ2¸ö",
            "µÚ3¸ö",
            "µ¹Êý1",
            "µ¹Êý2",
            "µ¹Êý3",
            "",
            "\t"});
            this.combStartCalipersSearchNumber.Location = new System.Drawing.Point(344, 70);
            this.combStartCalipersSearchNumber.Name = "combStartCalipersSearchNumber";
            this.combStartCalipersSearchNumber.Size = new System.Drawing.Size(84, 30);
            this.combStartCalipersSearchNumber.TabIndex = 2;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label69.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label69.Location = new System.Drawing.Point(225, 116);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(111, 22);
            this.label69.TabIndex = 0;
            this.label69.Text = "ÖÕµãËÑË÷´ÎÐò:";
            // 
            // combStartCalipersSearchMode
            // 
            this.combStartCalipersSearchMode.FormattingEnabled = true;
            this.combStartCalipersSearchMode.Items.AddRange(new object[] {
            "°µµ½Ã÷",
            "Ã÷µ½°µ",
            "ËùÓÐ"});
            this.combStartCalipersSearchMode.Location = new System.Drawing.Point(131, 69);
            this.combStartCalipersSearchMode.Name = "combStartCalipersSearchMode";
            this.combStartCalipersSearchMode.Size = new System.Drawing.Size(84, 30);
            this.combStartCalipersSearchMode.TabIndex = 2;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label67.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label67.Location = new System.Drawing.Point(225, 74);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(111, 22);
            this.label67.TabIndex = 0;
            this.label67.Text = "ÆðµãËÑË÷´ÎÐò:";
            // 
            // numUDDistanceCalipersThreshold
            // 
            this.numUDDistanceCalipersThreshold.Location = new System.Drawing.Point(131, 29);
            this.numUDDistanceCalipersThreshold.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numUDDistanceCalipersThreshold.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUDDistanceCalipersThreshold.Name = "numUDDistanceCalipersThreshold";
            this.numUDDistanceCalipersThreshold.Size = new System.Drawing.Size(84, 29);
            this.numUDDistanceCalipersThreshold.TabIndex = 1;
            this.numUDDistanceCalipersThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label66.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label66.Location = new System.Drawing.Point(11, 116);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(111, 22);
            this.label66.TabIndex = 0;
            this.label66.Text = "ÖÕµãËÑË÷Ä£Ê½:";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label68.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label68.Location = new System.Drawing.Point(11, 33);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(79, 22);
            this.label68.TabIndex = 0;
            this.label68.Text = "±ßÔµãÐÖµ:";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label70.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label70.Location = new System.Drawing.Point(11, 73);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(111, 22);
            this.label70.TabIndex = 0;
            this.label70.Text = "ÆðµãËÑË÷Ä£Ê½:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label71.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label71.Location = new System.Drawing.Point(252, 33);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(79, 22);
            this.label71.TabIndex = 0;
            this.label71.Text = "ÂË²¨³ß´ç:";
            // 
            // numUDDistanceCalipersFiterSize
            // 
            this.numUDDistanceCalipersFiterSize.Location = new System.Drawing.Point(344, 29);
            this.numUDDistanceCalipersFiterSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUDDistanceCalipersFiterSize.Name = "numUDDistanceCalipersFiterSize";
            this.numUDDistanceCalipersFiterSize.Size = new System.Drawing.Size(84, 29);
            this.numUDDistanceCalipersFiterSize.TabIndex = 1;
            this.numUDDistanceCalipersFiterSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // u_DisPlay7
            // 
            this.u_DisPlay7.AutoDisplay = Display.U_DisPlay.AutoDisplayMode.Fit;
            this.u_DisPlay7.BackColor = System.Drawing.Color.Black;
            this.u_DisPlay7.Image = null;
            this.u_DisPlay7.Location = new System.Drawing.Point(6, 6);
            this.u_DisPlay7.Name = "u_DisPlay7";
            this.u_DisPlay7.Size = new System.Drawing.Size(908, 777);
            this.u_DisPlay7.TabIndex = 2;
            this.u_DisPlay7.TabStop = false;
            // 
            // btnOpenPrj
            // 
            this.btnOpenPrj.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPrj.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnOpenPrj.Location = new System.Drawing.Point(4, 861);
            this.btnOpenPrj.Name = "btnOpenPrj";
            this.btnOpenPrj.Size = new System.Drawing.Size(689, 43);
            this.btnOpenPrj.TabIndex = 1;
            this.btnOpenPrj.Text = "´ò¿ª¹¤³Ì";
            this.btnOpenPrj.UseVisualStyleBackColor = true;
            this.btnOpenPrj.Click += new System.EventHandler(this.btnOpenPrj_Click);
            // 
            // btnSavePrj
            // 
            this.btnSavePrj.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSavePrj.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSavePrj.Location = new System.Drawing.Point(696, 861);
            this.btnSavePrj.Name = "btnSavePrj";
            this.btnSavePrj.Size = new System.Drawing.Size(689, 43);
            this.btnSavePrj.TabIndex = 1;
            this.btnSavePrj.Text = "±£´æ¹¤³Ì";
            this.btnSavePrj.UseVisualStyleBackColor = true;
            this.btnSavePrj.Click += new System.EventHandler(this.btnSavePrj_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 909);
            this.Controls.Add(this.btnSavePrj);
            this.Controls.Add(this.btnOpenPrj);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Opencv¹¤¾ßÏä";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFNumMatchs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMaxOverlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMaxScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFEndNumLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFModelEndAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFGreediness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFMinScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDFModelStartAngle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDContrastHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelMinContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCMinEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCNumLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDContrastLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelEndAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCModelStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersIgnoreDots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCircleCalipersFiterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersMaxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersIgnoreDots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine2CalipersFiterSize)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersMaxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersIgnoreDots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDLine1CalipersFiterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersMaxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersIgnoreDots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRectCalipersFiterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay4)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCornerPixlSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDCheckerBoardWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay6)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDistanceCalipersThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDDistanceCalipersFiterSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.u_DisPlay7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Display.U_DisPlay u_DisPlay1;
        private System.Windows.Forms.Label lbShowMsg;
        private System.Windows.Forms.CheckBox chkIgnorePolarity;
        private System.Windows.Forms.NumericUpDown numUDContrastHigh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUDCModelMinContrast;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numUDCMinEdge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numUDCNumLevels;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numUDContrastLow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchRect;
        private System.Windows.Forms.Button btnMakeModel;
        private System.Windows.Forms.Button btnOpenSearchPic;
        private System.Windows.Forms.Button btnOpenModelPic;
        private System.Windows.Forms.Button btnFindModel;
        private System.Windows.Forms.Button btnMakeRect;
        private System.Windows.Forms.NumericUpDown numUDFNumMatchs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numUDFMaxOverlap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numUDFMaxScale;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numUDFModelEndAngle;
        private System.Windows.Forms.NumericUpDown numUDFGreediness;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numUDFMinScore;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numUDFMinScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numUDFModelStartAngle;
        private System.Windows.Forms.ComboBox comboSubPixel;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.NumericUpDown numUDFEndNumLevels;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numUDCModelEndAngle;
        private System.Windows.Forms.NumericUpDown numUDCModelStartAngle;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnMask;
        private Display.U_DisPlay u_DisPlay2;
        private System.Windows.Forms.Label lbShowCircleCaliperMsg;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnFindCircleCallipers;
        private System.Windows.Forms.Button btnCircleCallipersPic;
        private System.Windows.Forms.Button btnAddCircleCallipers;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersWidth;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersIgnoreDots;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersNum;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersLength;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersFiterSize;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numUDCircleCalipersThreshold;
        private System.Windows.Forms.ComboBox combCircleCalipersPolarity;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numUDCheckerBoardHeight;
        private System.Windows.Forms.NumericUpDown numUDCornerPixlSize;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.NumericUpDown numUDCheckerBoardDis;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.NumericUpDown numUDCheckerBoardWidth;
        private Display.U_DisPlay u_DisPlay6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnCreateCheckBoardParam;
        private System.Windows.Forms.Button btnCheckCheckBoardParam;
        private System.Windows.Forms.Button btnOpenPrj;
        private System.Windows.Forms.Button btnSavePrj;
        private System.Windows.Forms.ComboBox combCircleCalipersDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtCircleCaliperY;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtCircleCaliperR;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtCircleCaliperX;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabPage7;
        private Display.U_DisPlay u_DisPlay3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox combLine1CalipersDir;
        private System.Windows.Forms.ComboBox combLine1CalipersPolarity;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersWidth;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersThreshold;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersMaxError;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersIgnoreDots;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersNum;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersLength;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown numUDLine1CalipersFiterSize;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox txtCrossY;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtCrossA;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txtCrossX;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnFindCross;
        private System.Windows.Forms.Button btnLineCallipersPic;
        private System.Windows.Forms.Button btnAddLine12Callipers;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox combLine2CalipersDir;
        private System.Windows.Forms.ComboBox combLine2CalipersPolarity;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersWidth;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersThreshold;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersMaxError;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersIgnoreDots;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersNum;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersLength;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.NumericUpDown numUDLine2CalipersFiterSize;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.ComboBox combRectCalipersDir;
        private System.Windows.Forms.ComboBox combRectCalipersPolarity;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersWidth;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersThreshold;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersIgnoreDots;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersNum;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersLength;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersFiterSize;
        private System.Windows.Forms.Label label61;
        private Display.U_DisPlay u_DisPlay4;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtRectCaliperY;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtRectCaliperA;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox txtRectCaliperX;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Button btnFindRectCallipers;
        private System.Windows.Forms.Button btnRectCallipersPic;
        private System.Windows.Forms.Button btnAddRectCallipers;
        private System.Windows.Forms.NumericUpDown numUDRectCalipersMaxError;
        private System.Windows.Forms.Label label65;
        private Display.U_DisPlay u_DisPlay7;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button btnGetDistance;
        private System.Windows.Forms.Button btnDistanceCallipersPic;
        private System.Windows.Forms.Button btnAddDistanceCallipers;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.ComboBox combStartCalipersSearchNumber;
        private System.Windows.Forms.ComboBox combStartCalipersSearchMode;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.NumericUpDown numUDDistanceCalipersThreshold;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.NumericUpDown numUDDistanceCalipersFiterSize;
        private System.Windows.Forms.ComboBox combEndCalipersSearchNumber;
        private System.Windows.Forms.ComboBox combEndCalipersSearchMode;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.TextBox txtDistanceEndY;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox txtDistanceStartY;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.TextBox txtDistanceEndX;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txtDistanceStartX;
        private System.Windows.Forms.Label label74;
    }
}

