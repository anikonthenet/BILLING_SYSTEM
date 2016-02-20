namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnExcelImportIncremental
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dlgOpenFVU = new System.Windows.Forms.OpenFileDialog();
            this.tbcExcelImport = new System.Windows.Forms.TabControl();
            this.tbpValidateExcelFile = new System.Windows.Forms.TabPage();
            this.lblCreateBlankExcelSheet = new System.Windows.Forms.Label();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpImportType = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rbnIncremental = new System.Windows.Forms.RadioButton();
            this.rbnNewImport = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProgressDisplayMessage = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkColorCodingExcelsheet = new System.Windows.Forms.CheckBox();
            this.btnSelectExcelPath = new System.Windows.Forms.Button();
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.tbpImportExcelFile = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.prgImportBar = new System.Windows.Forms.ProgressBar();
            this.dgcViewOfflineSerial = new DGControl.DGControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNoofAvailableSerial = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalRecords = new System.Windows.Forms.Label();
            this.lblLabel1 = new System.Windows.Forms.Label();
            this.lblHide = new System.Windows.Forms.Label();
            this.tmrLoginRefresh = new System.Windows.Forms.Timer(this.components);
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.tbcExcelImport.SuspendLayout();
            this.tbpValidateExcelFile.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.grpImportType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tbpImportExcelFile.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewOfflineSerial)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(455, 658);
            this.grpSort.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(925, 15);
            this.BtnCancel.Size = new System.Drawing.Size(7, 23);
            this.BtnCancel.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.Lavender;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.BtnSave.Location = new System.Drawing.Point(354, 13);
            this.BtnSave.Size = new System.Drawing.Size(147, 23);
            this.BtnSave.Text = "&Validate Excel file";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(95, 658);
            this.grpSearch.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(920, 15);
            this.BtnEdit.Size = new System.Drawing.Size(5, 23);
            this.BtnEdit.Visible = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(915, 15);
            this.BtnAdd.Size = new System.Drawing.Size(5, 23);
            this.BtnAdd.Visible = false;
            // 
            // BtnSort
            // 
            this.BtnSort.Location = new System.Drawing.Point(962, 16);
            this.BtnSort.Size = new System.Drawing.Size(15, 23);
            this.BtnSort.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Lavender;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.BtnExit.Location = new System.Drawing.Point(502, 13);
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(979, 15);
            this.BtnPrint.Size = new System.Drawing.Size(15, 23);
            this.BtnPrint.Visible = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(946, 15);
            this.BtnRefresh.Size = new System.Drawing.Size(11, 23);
            this.BtnRefresh.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Location = new System.Drawing.Point(939, 15);
            this.BtnDelete.Size = new System.Drawing.Size(7, 23);
            this.BtnDelete.Visible = false;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(932, 15);
            this.BtnSearch.Size = new System.Drawing.Size(7, 23);
            this.BtnSearch.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.Text = "Import Mode";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tbcExcelImport);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 588);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 10);
            this.ViewGrid.Visible = false;
            // 
            // dlgOpenFVU
            // 
            this.dlgOpenFVU.FileName = "dlgOpenFVU";
            // 
            // tbcExcelImport
            // 
            this.tbcExcelImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcExcelImport.Controls.Add(this.tbpValidateExcelFile);
            this.tbcExcelImport.Controls.Add(this.tbpImportExcelFile);
            this.tbcExcelImport.Location = new System.Drawing.Point(10, 6);
            this.tbcExcelImport.Name = "tbcExcelImport";
            this.tbcExcelImport.SelectedIndex = 0;
            this.tbcExcelImport.Size = new System.Drawing.Size(990, 534);
            this.tbcExcelImport.TabIndex = 0;
            this.tbcExcelImport.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcExcelImport_Selecting);
            // 
            // tbpValidateExcelFile
            // 
            this.tbpValidateExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpValidateExcelFile.Controls.Add(this.lblCreateBlankExcelSheet);
            this.tbpValidateExcelFile.Controls.Add(this.grpMain);
            this.tbpValidateExcelFile.Controls.Add(this.grpImportType);
            this.tbpValidateExcelFile.Controls.Add(this.groupBox1);
            this.tbpValidateExcelFile.Controls.Add(this.groupBox4);
            this.tbpValidateExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpValidateExcelFile.Name = "tbpValidateExcelFile";
            this.tbpValidateExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpValidateExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpValidateExcelFile.TabIndex = 0;
            // 
            // lblCreateBlankExcelSheet
            // 
            this.lblCreateBlankExcelSheet.AutoSize = true;
            this.lblCreateBlankExcelSheet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCreateBlankExcelSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateBlankExcelSheet.ForeColor = System.Drawing.Color.Blue;
            this.lblCreateBlankExcelSheet.Location = new System.Drawing.Point(742, 76);
            this.lblCreateBlankExcelSheet.Name = "lblCreateBlankExcelSheet";
            this.lblCreateBlankExcelSheet.Size = new System.Drawing.Size(152, 13);
            this.lblCreateBlankExcelSheet.TabIndex = 149;
            this.lblCreateBlankExcelSheet.Text = "Create Blank Excel Sheet";
            this.lblCreateBlankExcelSheet.Click += new System.EventHandler(this.lblCreateBlankExcelSheet_Click);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.cmbItem);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMain.ForeColor = System.Drawing.Color.Black;
            this.grpMain.Location = new System.Drawing.Point(89, 92);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(805, 76);
            this.grpMain.TabIndex = 74;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Select";
            // 
            // cmbItem
            // 
            this.cmbItem.BackColor = System.Drawing.Color.White;
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(112, 25);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(538, 21);
            this.cmbItem.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select Item";
            // 
            // grpImportType
            // 
            this.grpImportType.Controls.Add(this.label13);
            this.grpImportType.Controls.Add(this.rbnIncremental);
            this.grpImportType.Controls.Add(this.rbnNewImport);
            this.grpImportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImportType.ForeColor = System.Drawing.Color.Black;
            this.grpImportType.Location = new System.Drawing.Point(89, 258);
            this.grpImportType.Name = "grpImportType";
            this.grpImportType.Size = new System.Drawing.Size(363, 89);
            this.grpImportType.TabIndex = 73;
            this.grpImportType.TabStop = false;
            this.grpImportType.Text = "Import Type";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(46, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(301, 13);
            this.label13.TabIndex = 149;
            this.label13.Text = "Note : In excel Offline serial no. should start from 1.";
            // 
            // rbnIncremental
            // 
            this.rbnIncremental.AutoSize = true;
            this.rbnIncremental.Location = new System.Drawing.Point(49, 43);
            this.rbnIncremental.Name = "rbnIncremental";
            this.rbnIncremental.Size = new System.Drawing.Size(160, 17);
            this.rbnIncremental.TabIndex = 1;
            this.rbnIncremental.Text = "Add to the existing data";
            this.rbnIncremental.UseVisualStyleBackColor = true;
            // 
            // rbnNewImport
            // 
            this.rbnNewImport.AutoSize = true;
            this.rbnNewImport.Checked = true;
            this.rbnNewImport.Enabled = false;
            this.rbnNewImport.Location = new System.Drawing.Point(49, 19);
            this.rbnNewImport.Name = "rbnNewImport";
            this.rbnNewImport.Size = new System.Drawing.Size(187, 17);
            this.rbnNewImport.TabIndex = 0;
            this.rbnNewImport.TabStop = true;
            this.rbnNewImport.Text = "New / Replace existing data";
            this.rbnNewImport.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProgressDisplayMessage);
            this.groupBox1.Controls.Add(this.prgBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(89, 353);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 64);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblProgressDisplayMessage
            // 
            this.lblProgressDisplayMessage.AutoSize = true;
            this.lblProgressDisplayMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressDisplayMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblProgressDisplayMessage.Location = new System.Drawing.Point(19, 47);
            this.lblProgressDisplayMessage.Name = "lblProgressDisplayMessage";
            this.lblProgressDisplayMessage.Size = new System.Drawing.Size(88, 13);
            this.lblProgressDisplayMessage.TabIndex = 148;
            this.lblProgressDisplayMessage.Text = "Excel file path";
            this.lblProgressDisplayMessage.Visible = false;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(16, 23);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(676, 21);
            this.prgBar.TabIndex = 147;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkColorCodingExcelsheet);
            this.groupBox4.Controls.Add(this.btnSelectExcelPath);
            this.groupBox4.Controls.Add(this.txtExcelPath);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(89, 176);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(805, 76);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Excel file";
            // 
            // chkColorCodingExcelsheet
            // 
            this.chkColorCodingExcelsheet.AutoSize = true;
            this.chkColorCodingExcelsheet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkColorCodingExcelsheet.ForeColor = System.Drawing.Color.Black;
            this.chkColorCodingExcelsheet.Location = new System.Drawing.Point(112, 48);
            this.chkColorCodingExcelsheet.Name = "chkColorCodingExcelsheet";
            this.chkColorCodingExcelsheet.Size = new System.Drawing.Size(436, 17);
            this.chkColorCodingExcelsheet.TabIndex = 148;
            this.chkColorCodingExcelsheet.Text = "Color coding in Excel sheet for errors (this may take a few more minutes)";
            this.chkColorCodingExcelsheet.UseVisualStyleBackColor = true;
            this.chkColorCodingExcelsheet.Visible = false;
            // 
            // btnSelectExcelPath
            // 
            this.btnSelectExcelPath.BackColor = System.Drawing.Color.Blue;
            this.btnSelectExcelPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectExcelPath.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectExcelPath.ForeColor = System.Drawing.Color.White;
            this.btnSelectExcelPath.Location = new System.Drawing.Point(607, 22);
            this.btnSelectExcelPath.Name = "btnSelectExcelPath";
            this.btnSelectExcelPath.Size = new System.Drawing.Size(43, 22);
            this.btnSelectExcelPath.TabIndex = 1;
            this.btnSelectExcelPath.Text = ". . .";
            this.btnSelectExcelPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectExcelPath.UseVisualStyleBackColor = false;
            this.btnSelectExcelPath.Click += new System.EventHandler(this.btnSelectExcelPath_Click);
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.BackColor = System.Drawing.SystemColors.Info;
            this.txtExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelPath.Location = new System.Drawing.Point(112, 22);
            this.txtExcelPath.MaxLength = 75;
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(492, 20);
            this.txtExcelPath.TabIndex = 0;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(25, 25);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(88, 13);
            this.label107.TabIndex = 0;
            this.label107.Text = "Excel file path";
            // 
            // tbpImportExcelFile
            // 
            this.tbpImportExcelFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpImportExcelFile.Controls.Add(this.groupBox3);
            this.tbpImportExcelFile.Controls.Add(this.dgcViewOfflineSerial);
            this.tbpImportExcelFile.Controls.Add(this.groupBox2);
            this.tbpImportExcelFile.Location = new System.Drawing.Point(4, 22);
            this.tbpImportExcelFile.Name = "tbpImportExcelFile";
            this.tbpImportExcelFile.Padding = new System.Windows.Forms.Padding(3);
            this.tbpImportExcelFile.Size = new System.Drawing.Size(982, 508);
            this.tbpImportExcelFile.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.prgImportBar);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(6, 451);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(969, 45);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress";
            // 
            // prgImportBar
            // 
            this.prgImportBar.Location = new System.Drawing.Point(16, 16);
            this.prgImportBar.Name = "prgImportBar";
            this.prgImportBar.Size = new System.Drawing.Size(940, 21);
            this.prgImportBar.TabIndex = 147;
            // 
            // dgcViewOfflineSerial
            // 
            this.dgcViewOfflineSerial.AlternatingBackColor = System.Drawing.Color.White;
            this.dgcViewOfflineSerial.BackColor = System.Drawing.Color.White;
            this.dgcViewOfflineSerial.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgcViewOfflineSerial.CaptionBackColor = System.Drawing.Color.Honeydew;
            this.dgcViewOfflineSerial.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewOfflineSerial.CaptionForeColor = System.Drawing.Color.Black;
            this.dgcViewOfflineSerial.CaptionText = "                                                                                 " +
                "                                                      Offline Serial Details";
            this.dgcViewOfflineSerial.DataMember = "";
            this.dgcViewOfflineSerial.FlatMode = true;
            this.dgcViewOfflineSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgcViewOfflineSerial.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgcViewOfflineSerial.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgcViewOfflineSerial.Location = new System.Drawing.Point(6, 88);
            this.dgcViewOfflineSerial.Name = "dgcViewOfflineSerial";
            this.dgcViewOfflineSerial.ReadOnly = true;
            this.dgcViewOfflineSerial.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dgcViewOfflineSerial.Size = new System.Drawing.Size(970, 358);
            this.dgcViewOfflineSerial.TabIndex = 76;
            this.dgcViewOfflineSerial.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgcViewOfflineSerial_MouseClick);
            this.dgcViewOfflineSerial.DoubleClick += new System.EventHandler(this.dgcViewOfflineSerial_DoubleClick);
            this.dgcViewOfflineSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgcViewOfflineSerial_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNoofAvailableSerial);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblItemName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTotalRecords);
            this.groupBox2.Controls.Add(this.lblLabel1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(7, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(970, 75);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Summary";
            // 
            // lblNoofAvailableSerial
            // 
            this.lblNoofAvailableSerial.BackColor = System.Drawing.SystemColors.Info;
            this.lblNoofAvailableSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoofAvailableSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofAvailableSerial.Location = new System.Drawing.Point(149, 48);
            this.lblNoofAvailableSerial.Name = "lblNoofAvailableSerial";
            this.lblNoofAvailableSerial.Size = new System.Drawing.Size(44, 20);
            this.lblNoofAvailableSerial.TabIndex = 201;
            this.lblNoofAvailableSerial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 200;
            this.label3.Text = "No of Serial available ";
            // 
            // lblItemName
            // 
            this.lblItemName.BackColor = System.Drawing.SystemColors.Info;
            this.lblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(89, 21);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(476, 20);
            this.lblItemName.TabIndex = 199;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 198;
            this.label2.Text = "Item Name";
            // 
            // txtTotalRecords
            // 
            this.txtTotalRecords.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRecords.Location = new System.Drawing.Point(404, 48);
            this.txtTotalRecords.Name = "txtTotalRecords";
            this.txtTotalRecords.Size = new System.Drawing.Size(44, 17);
            this.txtTotalRecords.TabIndex = 189;
            this.txtTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel1.Location = new System.Drawing.Point(225, 51);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(167, 13);
            this.lblLabel1.TabIndex = 187;
            this.lblLabel1.Text = "Total records to be imported";
            // 
            // lblHide
            // 
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(20, 48);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(164, 21);
            this.lblHide.TabIndex = 50;
            // 
            // tmrLoginRefresh
            // 
            this.tmrLoginRefresh.Interval = 5000;
            this.tmrLoginRefresh.Tick += new System.EventHandler(this.tmrLoginRefresh_Tick);
            // 
            // TrnExcelImportIncremental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.lblHide);
            this.Name = "TrnExcelImportIncremental";
            this.Load += new System.EventHandler(this.TrnExcelImport_Load);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.lblHide, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.tbcExcelImport.ResumeLayout(false);
            this.tbpValidateExcelFile.ResumeLayout(false);
            this.tbpValidateExcelFile.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpImportType.ResumeLayout(false);
            this.grpImportType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tbpImportExcelFile.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcViewOfflineSerial)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpenFVU;
        private System.Windows.Forms.TabControl tbcExcelImport;
        private System.Windows.Forms.TabPage tbpValidateExcelFile;
        private System.Windows.Forms.TabPage tbpImportExcelFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkColorCodingExcelsheet;
        private System.Windows.Forms.Button btnSelectExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label lblHide;
        private DGControl.DGControl dgcViewOfflineSerial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar prgImportBar;
        private System.Windows.Forms.Label lblProgressDisplayMessage;
        private System.Windows.Forms.Label txtTotalRecords;
        private System.Windows.Forms.Label lblLabel1;
        private System.Windows.Forms.Timer tmrLoginRefresh;
        private System.Windows.Forms.GroupBox grpImportType;
        private System.Windows.Forms.RadioButton rbnIncremental;
        private System.Windows.Forms.RadioButton rbnNewImport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNoofAvailableSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCreateBlankExcelSheet;
    }
}
