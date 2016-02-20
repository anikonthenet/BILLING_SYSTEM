namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnAdjustment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnAdjustment));
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpEntryPanel = new System.Windows.Forms.GroupBox();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpSortOrder = new System.Windows.Forms.GroupBox();
            this.rdbnCollectionAmt = new System.Windows.Forms.RadioButton();
            this.rdbnReferenceNo = new System.Windows.Forms.RadioButton();
            this.rdbnPartyName = new System.Windows.Forms.RadioButton();
            this.rdbnInvoiceNo = new System.Windows.Forms.RadioButton();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.lblAutoCollectionPostFlag = new System.Windows.Forms.Label();
            this.lblSundryPartyFlag = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mskReconcileDate = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mskCollectionDate = new System.Windows.Forms.MaskedTextBox();
            this.grpSearchDetailPanel = new System.Windows.Forms.GroupBox();
            this.txtSearchDetailAll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grpBottomPanel = new System.Windows.Forms.GroupBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.flxgrdDetails = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
            this.grpSearchPanel = new System.Windows.Forms.GroupBox();
            this.txtSearchAll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpEntryPanel.SuspendLayout();
            this.grpSortOrder.SuspendLayout();
            this.grpSearchDetailPanel.SuspendLayout();
            this.grpBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).BeginInit();
            this.grpSearchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(158, 663);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(692, 658);
            this.grpSearch.Size = new System.Drawing.Size(314, 231);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(227, 193);
            this.BtnSearchCancel.TabIndex = 8;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(156, 193);
            this.BtnSearchOK.TabIndex = 7;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpBottomPanel);
            this.pnlControls.Controls.Add(this.label11);
            this.pnlControls.Controls.Add(this.grpSearchDetailPanel);
            this.pnlControls.Controls.Add(this.grpEntryPanel);
            this.pnlControls.Location = new System.Drawing.Point(9, 102);
            this.pnlControls.Size = new System.Drawing.Size(1003, 498);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 104);
            this.ViewGrid.Size = new System.Drawing.Size(1001, 10);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(284, 44);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(556, 22);
            this.cmbCompany.TabIndex = 140;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(188, 46);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(94, 18);
            this.label27.TabIndex = 141;
            this.label27.Text = "Company Name";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(998, 1);
            this.label1.TabIndex = 143;
            this.label1.Text = "Company";
            // 
            // grpEntryPanel
            // 
            this.grpEntryPanel.Controls.Add(this.txtReferenceNo);
            this.grpEntryPanel.Controls.Add(this.label3);
            this.grpEntryPanel.Controls.Add(this.grpSortOrder);
            this.grpEntryPanel.Controls.Add(this.chkShowAll);
            this.grpEntryPanel.Controls.Add(this.lblAutoCollectionPostFlag);
            this.grpEntryPanel.Controls.Add(this.lblSundryPartyFlag);
            this.grpEntryPanel.Controls.Add(this.label6);
            this.grpEntryPanel.Controls.Add(this.label7);
            this.grpEntryPanel.Controls.Add(this.mskReconcileDate);
            this.grpEntryPanel.Controls.Add(this.label12);
            this.grpEntryPanel.Controls.Add(this.txtRemarks);
            this.grpEntryPanel.Controls.Add(this.label4);
            this.grpEntryPanel.Controls.Add(this.label21);
            this.grpEntryPanel.Controls.Add(this.label19);
            this.grpEntryPanel.Controls.Add(this.mskCollectionDate);
            this.grpEntryPanel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEntryPanel.Location = new System.Drawing.Point(22, 3);
            this.grpEntryPanel.Name = "grpEntryPanel";
            this.grpEntryPanel.Size = new System.Drawing.Size(958, 193);
            this.grpEntryPanel.TabIndex = 0;
            this.grpEntryPanel.TabStop = false;
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceNo.Location = new System.Drawing.Point(138, 84);
            this.txtReferenceNo.MaxLength = 20;
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(167, 20);
            this.txtReferenceNo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 312;
            this.label3.Text = "Reference No.";
            // 
            // grpSortOrder
            // 
            this.grpSortOrder.Controls.Add(this.rdbnCollectionAmt);
            this.grpSortOrder.Controls.Add(this.rdbnReferenceNo);
            this.grpSortOrder.Controls.Add(this.rdbnPartyName);
            this.grpSortOrder.Controls.Add(this.rdbnInvoiceNo);
            this.grpSortOrder.Location = new System.Drawing.Point(817, 37);
            this.grpSortOrder.Name = "grpSortOrder";
            this.grpSortOrder.Size = new System.Drawing.Size(137, 108);
            this.grpSortOrder.TabIndex = 5;
            this.grpSortOrder.TabStop = false;
            this.grpSortOrder.Text = "Sort On";
            // 
            // rdbnCollectionAmt
            // 
            this.rdbnCollectionAmt.AutoSize = true;
            this.rdbnCollectionAmt.Location = new System.Drawing.Point(8, 85);
            this.rdbnCollectionAmt.Name = "rdbnCollectionAmt";
            this.rdbnCollectionAmt.Size = new System.Drawing.Size(124, 20);
            this.rdbnCollectionAmt.TabIndex = 3;
            this.rdbnCollectionAmt.TabStop = true;
            this.rdbnCollectionAmt.Text = "Collection Amt";
            this.rdbnCollectionAmt.UseVisualStyleBackColor = true;
            this.rdbnCollectionAmt.CheckedChanged += new System.EventHandler(this.rdbnInvoiceNo_CheckedChanged);
            // 
            // rdbnReferenceNo
            // 
            this.rdbnReferenceNo.AutoSize = true;
            this.rdbnReferenceNo.Location = new System.Drawing.Point(8, 63);
            this.rdbnReferenceNo.Name = "rdbnReferenceNo";
            this.rdbnReferenceNo.Size = new System.Drawing.Size(110, 20);
            this.rdbnReferenceNo.TabIndex = 2;
            this.rdbnReferenceNo.TabStop = true;
            this.rdbnReferenceNo.Text = "Reference No";
            this.rdbnReferenceNo.UseVisualStyleBackColor = true;
            this.rdbnReferenceNo.CheckedChanged += new System.EventHandler(this.rdbnInvoiceNo_CheckedChanged);
            // 
            // rdbnPartyName
            // 
            this.rdbnPartyName.AutoSize = true;
            this.rdbnPartyName.Location = new System.Drawing.Point(8, 42);
            this.rdbnPartyName.Name = "rdbnPartyName";
            this.rdbnPartyName.Size = new System.Drawing.Size(89, 20);
            this.rdbnPartyName.TabIndex = 1;
            this.rdbnPartyName.TabStop = true;
            this.rdbnPartyName.Text = "PartyName";
            this.rdbnPartyName.UseVisualStyleBackColor = true;
            this.rdbnPartyName.CheckedChanged += new System.EventHandler(this.rdbnInvoiceNo_CheckedChanged);
            // 
            // rdbnInvoiceNo
            // 
            this.rdbnInvoiceNo.AutoSize = true;
            this.rdbnInvoiceNo.Location = new System.Drawing.Point(8, 19);
            this.rdbnInvoiceNo.Name = "rdbnInvoiceNo";
            this.rdbnInvoiceNo.Size = new System.Drawing.Size(96, 20);
            this.rdbnInvoiceNo.TabIndex = 0;
            this.rdbnInvoiceNo.TabStop = true;
            this.rdbnInvoiceNo.Text = "Invoice No";
            this.rdbnInvoiceNo.UseVisualStyleBackColor = true;
            this.rdbnInvoiceNo.CheckedChanged += new System.EventHandler(this.rdbnInvoiceNo_CheckedChanged);
            // 
            // chkShowAll
            // 
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowAll.Location = new System.Drawing.Point(728, 122);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(83, 20);
            this.chkShowAll.TabIndex = 4;
            this.chkShowAll.Text = "Show All";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // lblAutoCollectionPostFlag
            // 
            this.lblAutoCollectionPostFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAutoCollectionPostFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoCollectionPostFlag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoCollectionPostFlag.Location = new System.Drawing.Point(335, 38);
            this.lblAutoCollectionPostFlag.Name = "lblAutoCollectionPostFlag";
            this.lblAutoCollectionPostFlag.Size = new System.Drawing.Size(10, 18);
            this.lblAutoCollectionPostFlag.TabIndex = 311;
            this.lblAutoCollectionPostFlag.Visible = false;
            // 
            // lblSundryPartyFlag
            // 
            this.lblSundryPartyFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSundryPartyFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSundryPartyFlag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSundryPartyFlag.Location = new System.Drawing.Point(325, 38);
            this.lblSundryPartyFlag.Name = "lblSundryPartyFlag";
            this.lblSundryPartyFlag.Size = new System.Drawing.Size(10, 18);
            this.lblSundryPartyFlag.TabIndex = 310;
            this.lblSundryPartyFlag.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 18);
            this.label6.TabIndex = 309;
            this.label6.Text = "Reconcile Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(224, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 308;
            this.label7.Text = "(DD/MM/YYYY)";
            // 
            // mskReconcileDate
            // 
            this.mskReconcileDate.BackColor = System.Drawing.SystemColors.Info;
            this.mskReconcileDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskReconcileDate.Enabled = false;
            this.mskReconcileDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskReconcileDate.Location = new System.Drawing.Point(138, 61);
            this.mskReconcileDate.Mask = "00/00/0000";
            this.mskReconcileDate.Name = "mskReconcileDate";
            this.mskReconcileDate.Size = new System.Drawing.Size(83, 20);
            this.mskReconcileDate.TabIndex = 1;
            this.mskReconcileDate.ValidatingType = typeof(System.DateTime);
            this.mskReconcileDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(24, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 17);
            this.label12.TabIndex = 300;
            this.label12.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(138, 108);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(519, 20);
            this.txtRemarks.TabIndex = 3;
            this.txtRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(11, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 14);
            this.label4.TabIndex = 287;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(24, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 18);
            this.label21.TabIndex = 208;
            this.label21.Text = "Adjustment Date";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(224, 41);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(97, 13);
            this.label19.TabIndex = 207;
            this.label19.Text = "(DD/MM/YYYY)";
            // 
            // mskCollectionDate
            // 
            this.mskCollectionDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskCollectionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskCollectionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCollectionDate.Location = new System.Drawing.Point(138, 37);
            this.mskCollectionDate.Mask = "00/00/0000";
            this.mskCollectionDate.Name = "mskCollectionDate";
            this.mskCollectionDate.Size = new System.Drawing.Size(83, 20);
            this.mskCollectionDate.TabIndex = 0;
            this.mskCollectionDate.ValidatingType = typeof(System.DateTime);
            this.mskCollectionDate.Leave += new System.EventHandler(this.mskCollectionDate_Leave);
            this.mskCollectionDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // grpSearchDetailPanel
            // 
            this.grpSearchDetailPanel.Controls.Add(this.txtSearchDetailAll);
            this.grpSearchDetailPanel.Controls.Add(this.label2);
            this.grpSearchDetailPanel.Location = new System.Drawing.Point(186, 198);
            this.grpSearchDetailPanel.Name = "grpSearchDetailPanel";
            this.grpSearchDetailPanel.Size = new System.Drawing.Size(630, 38);
            this.grpSearchDetailPanel.TabIndex = 153;
            this.grpSearchDetailPanel.TabStop = false;
            this.grpSearchDetailPanel.Visible = false;
            // 
            // txtSearchDetailAll
            // 
            this.txtSearchDetailAll.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSearchDetailAll.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchDetailAll.ForeColor = System.Drawing.Color.Black;
            this.txtSearchDetailAll.Location = new System.Drawing.Point(51, 13);
            this.txtSearchDetailAll.Name = "txtSearchDetailAll";
            this.txtSearchDetailAll.Size = new System.Drawing.Size(566, 21);
            this.txtSearchDetailAll.TabIndex = 153;
            this.txtSearchDetailAll.TabStop = false;
            this.txtSearchDetailAll.TextChanged += new System.EventHandler(this.txtSearchDetailAll_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 152;
            this.label2.Text = "&Find";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(24, 199);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(956, 1);
            this.label11.TabIndex = 213;
            this.label11.Text = "Request Date";
            // 
            // grpBottomPanel
            // 
            this.grpBottomPanel.Controls.Add(this.txtQty);
            this.grpBottomPanel.Controls.Add(this.flxgrdDetails);
            this.grpBottomPanel.Location = new System.Drawing.Point(22, 203);
            this.grpBottomPanel.Name = "grpBottomPanel";
            this.grpBottomPanel.Size = new System.Drawing.Size(958, 291);
            this.grpBottomPanel.TabIndex = 214;
            this.grpBottomPanel.TabStop = false;
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(631, 132);
            this.txtQty.MaxLength = 0;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(38, 21);
            this.txtQty.TabIndex = 10;
            this.txtQty.Visible = false;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // flxgrdDetails
            // 
            this.flxgrdDetails.DataSource = null;
            this.flxgrdDetails.Location = new System.Drawing.Point(9, 16);
            this.flxgrdDetails.Name = "flxgrdDetails";
            this.flxgrdDetails.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flxgrdDetails.OcxState")));
            this.flxgrdDetails.Size = new System.Drawing.Size(943, 266);
            this.flxgrdDetails.TabIndex = 11;
            this.flxgrdDetails.ClickEvent += new System.EventHandler(this.flxgrdDetails_ClickEvent);
            this.flxgrdDetails.Scroll += new System.EventHandler(this.flxgrdDetails_Scroll);
            this.flxgrdDetails.MouseMoveEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEventHandler(this.flxgrdDetails_MouseMoveEvent);
            this.flxgrdDetails.KeyPressEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEventHandler(this.flxgrdDetails_KeyPressEvent);
            // 
            // grpSearchPanel
            // 
            this.grpSearchPanel.Controls.Add(this.txtSearchAll);
            this.grpSearchPanel.Controls.Add(this.label5);
            this.grpSearchPanel.Location = new System.Drawing.Point(12, 66);
            this.grpSearchPanel.Name = "grpSearchPanel";
            this.grpSearchPanel.Size = new System.Drawing.Size(998, 35);
            this.grpSearchPanel.TabIndex = 154;
            this.grpSearchPanel.TabStop = false;
            // 
            // txtSearchAll
            // 
            this.txtSearchAll.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSearchAll.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAll.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAll.Location = new System.Drawing.Point(240, 11);
            this.txtSearchAll.Name = "txtSearchAll";
            this.txtSearchAll.Size = new System.Drawing.Size(566, 21);
            this.txtSearchAll.TabIndex = 153;
            this.txtSearchAll.TabStop = false;
            this.txtSearchAll.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(201, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 152;
            this.label5.Text = "&Find";
            // 
            // TrnAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.grpSearchPanel);
            this.Name = "TrnAdjustment";
            this.Load += new System.EventHandler(this.BankEntry_Load);
            this.Controls.SetChildIndex(this.grpSearchPanel, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pnlControls, 0);
            this.Controls.SetChildIndex(this.lblMode, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.grpButton, 0);
            this.Controls.SetChildIndex(this.pnlFooter, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Controls.SetChildIndex(this.lblSearchMode, 0);
            this.Controls.SetChildIndex(this.grpSort, 0);
            this.Controls.SetChildIndex(this.ViewGrid, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpEntryPanel.ResumeLayout(false);
            this.grpEntryPanel.PerformLayout();
            this.grpSortOrder.ResumeLayout(false);
            this.grpSortOrder.PerformLayout();
            this.grpSearchDetailPanel.ResumeLayout(false);
            this.grpSearchDetailPanel.PerformLayout();
            this.grpBottomPanel.ResumeLayout(false);
            this.grpBottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).EndInit();
            this.grpSearchPanel.ResumeLayout(false);
            this.grpSearchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpEntryPanel;
        private System.Windows.Forms.GroupBox grpSearchDetailPanel;
        public System.Windows.Forms.TextBox txtSearchDetailAll;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox mskCollectionDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.GroupBox grpBottomPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mskReconcileDate;
        internal System.Windows.Forms.TextBox txtQty;
        private AxMSHierarchicalFlexGridLib.AxMSHFlexGrid flxgrdDetails;
        private System.Windows.Forms.GroupBox grpSearchPanel;
        public System.Windows.Forms.TextBox txtSearchAll;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkShowAll;
        private System.Windows.Forms.GroupBox grpSortOrder;
        private System.Windows.Forms.RadioButton rdbnCollectionAmt;
        private System.Windows.Forms.RadioButton rdbnReferenceNo;
        private System.Windows.Forms.RadioButton rdbnPartyName;
        private System.Windows.Forms.RadioButton rdbnInvoiceNo;
        private System.Windows.Forms.Label lblAutoCollectionPostFlag;
        private System.Windows.Forms.Label lblSundryPartyFlag;
        public System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label label3;
    }
}
