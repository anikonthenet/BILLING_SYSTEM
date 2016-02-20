namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnRequestCD
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
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mskInvoiceDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mskBankDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.mskAccountEntryDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.txtAmountSearch = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtReferenceSearch = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBankSearch = new System.Windows.Forms.TextBox();
            this.chkCancelledEntry = new System.Windows.Forms.CheckBox();
            this.txtSearchRemarks = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbInvoiceSeries = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBillingType = new System.Windows.Forms.Label();
            this.chkSendEmail = new System.Windows.Forms.CheckBox();
            this.lblDespatched = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInvoiceDate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.mskDespatchDate = new System.Windows.Forms.MaskedTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mskRequestDate = new System.Windows.Forms.MaskedTextBox();
            this.chkRequestInvoice = new System.Windows.Forms.CheckBox();
            this.chkRequestSerialNo = new System.Windows.Forms.CheckBox();
            this.chkRequestCD = new System.Windows.Forms.CheckBox();
            this.grpTopPanel = new System.Windows.Forms.GroupBox();
            this.chkInvoice = new System.Windows.Forms.CheckBox();
            this.chkSerialNo = new System.Windows.Forms.CheckBox();
            this.chkCD = new System.Windows.Forms.CheckBox();
            this.txtSearchAll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpTopPanel.SuspendLayout();
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
            this.grpSearch.Controls.Add(this.txtSearchRemarks);
            this.grpSearch.Controls.Add(this.label22);
            this.grpSearch.Controls.Add(this.txtAmountSearch);
            this.grpSearch.Controls.Add(this.label16);
            this.grpSearch.Controls.Add(this.txtBankSearch);
            this.grpSearch.Controls.Add(this.txtReferenceSearch);
            this.grpSearch.Controls.Add(this.label17);
            this.grpSearch.Controls.Add(this.label18);
            this.grpSearch.Controls.Add(this.label5);
            this.grpSearch.Controls.Add(this.label6);
            this.grpSearch.Controls.Add(this.mskInvoiceDateSearch);
            this.grpSearch.Controls.Add(this.label7);
            this.grpSearch.Controls.Add(this.label8);
            this.grpSearch.Controls.Add(this.mskBankDateSearch);
            this.grpSearch.Controls.Add(this.label14);
            this.grpSearch.Controls.Add(this.label15);
            this.grpSearch.Controls.Add(this.mskAccountEntryDateSearch);
            this.grpSearch.Location = new System.Drawing.Point(692, 658);
            this.grpSearch.Size = new System.Drawing.Size(314, 231);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskAccountEntryDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label15, 0);
            this.grpSearch.Controls.SetChildIndex(this.label14, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskBankDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label8, 0);
            this.grpSearch.Controls.SetChildIndex(this.label7, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskInvoiceDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label6, 0);
            this.grpSearch.Controls.SetChildIndex(this.label5, 0);
            this.grpSearch.Controls.SetChildIndex(this.label18, 0);
            this.grpSearch.Controls.SetChildIndex(this.label17, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtReferenceSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtBankSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label16, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtAmountSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label22, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtSearchRemarks, 0);
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
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Location = new System.Drawing.Point(9, 122);
            this.pnlControls.Size = new System.Drawing.Size(1003, 470);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 122);
            this.ViewGrid.Size = new System.Drawing.Size(1001, 40);
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
            this.cmbCompany.Location = new System.Drawing.Point(212, 46);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(246, 22);
            this.cmbCompany.TabIndex = 140;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(116, 48);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(94, 18);
            this.label27.TabIndex = 141;
            this.label27.Text = "Company Name";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 18);
            this.label5.TabIndex = 277;
            this.label5.Text = "Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(212, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 276;
            this.label6.Text = "(DD/MM/YYYY)";
            // 
            // mskInvoiceDateSearch
            // 
            this.mskInvoiceDateSearch.BackColor = System.Drawing.SystemColors.Window;
            this.mskInvoiceDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskInvoiceDateSearch.Location = new System.Drawing.Point(126, 28);
            this.mskInvoiceDateSearch.Mask = "00/00/0000";
            this.mskInvoiceDateSearch.Name = "mskInvoiceDateSearch";
            this.mskInvoiceDateSearch.Size = new System.Drawing.Size(83, 21);
            this.mskInvoiceDateSearch.TabIndex = 0;
            this.mskInvoiceDateSearch.ValidatingType = typeof(System.DateTime);
            this.mskInvoiceDateSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(16, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 18);
            this.label7.TabIndex = 275;
            this.label7.Text = "Bank Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(212, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 274;
            this.label8.Text = "(DD/MM/YYYY)";
            // 
            // mskBankDateSearch
            // 
            this.mskBankDateSearch.BackColor = System.Drawing.SystemColors.Window;
            this.mskBankDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskBankDateSearch.Location = new System.Drawing.Point(126, 74);
            this.mskBankDateSearch.Mask = "00/00/0000";
            this.mskBankDateSearch.Name = "mskBankDateSearch";
            this.mskBankDateSearch.Size = new System.Drawing.Size(83, 21);
            this.mskBankDateSearch.TabIndex = 2;
            this.mskBankDateSearch.ValidatingType = typeof(System.DateTime);
            this.mskBankDateSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(16, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 18);
            this.label14.TabIndex = 273;
            this.label14.Text = "A/c Entry Date";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(212, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 15);
            this.label15.TabIndex = 272;
            this.label15.Text = "(DD/MM/YYYY)";
            // 
            // mskAccountEntryDateSearch
            // 
            this.mskAccountEntryDateSearch.BackColor = System.Drawing.SystemColors.Window;
            this.mskAccountEntryDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskAccountEntryDateSearch.Location = new System.Drawing.Point(126, 51);
            this.mskAccountEntryDateSearch.Mask = "00/00/0000";
            this.mskAccountEntryDateSearch.Name = "mskAccountEntryDateSearch";
            this.mskAccountEntryDateSearch.Size = new System.Drawing.Size(83, 21);
            this.mskAccountEntryDateSearch.TabIndex = 1;
            this.mskAccountEntryDateSearch.ValidatingType = typeof(System.DateTime);
            this.mskAccountEntryDateSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // txtAmountSearch
            // 
            this.txtAmountSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountSearch.Location = new System.Drawing.Point(126, 120);
            this.txtAmountSearch.MaxLength = 30;
            this.txtAmountSearch.Name = "txtAmountSearch";
            this.txtAmountSearch.Size = new System.Drawing.Size(171, 21);
            this.txtAmountSearch.TabIndex = 4;
            this.txtAmountSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(16, 122);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 18);
            this.label16.TabIndex = 283;
            this.label16.Text = "Amount";
            // 
            // txtReferenceSearch
            // 
            this.txtReferenceSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceSearch.Location = new System.Drawing.Point(126, 143);
            this.txtReferenceSearch.MaxLength = 20;
            this.txtReferenceSearch.Name = "txtReferenceSearch";
            this.txtReferenceSearch.Size = new System.Drawing.Size(171, 21);
            this.txtReferenceSearch.TabIndex = 5;
            this.txtReferenceSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(16, 99);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 18);
            this.label17.TabIndex = 282;
            this.label17.Text = "Bank";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(16, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(110, 18);
            this.label18.TabIndex = 281;
            this.label18.Text = "Reference No.";
            // 
            // txtBankSearch
            // 
            this.txtBankSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankSearch.Location = new System.Drawing.Point(126, 97);
            this.txtBankSearch.MaxLength = 20;
            this.txtBankSearch.Name = "txtBankSearch";
            this.txtBankSearch.Size = new System.Drawing.Size(171, 21);
            this.txtBankSearch.TabIndex = 3;
            this.txtBankSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlSearch_KeyPress);
            // 
            // chkCancelledEntry
            // 
            this.chkCancelledEntry.AutoSize = true;
            this.chkCancelledEntry.Location = new System.Drawing.Point(860, 51);
            this.chkCancelledEntry.Name = "chkCancelledEntry";
            this.chkCancelledEntry.Size = new System.Drawing.Size(138, 17);
            this.chkCancelledEntry.TabIndex = 142;
            this.chkCancelledEntry.Text = "Include Cancelled Entry";
            this.chkCancelledEntry.UseVisualStyleBackColor = true;
            this.chkCancelledEntry.Visible = false;
            this.chkCancelledEntry.CheckedChanged += new System.EventHandler(this.cmbItemName_SelectedIndexChanged);
            // 
            // txtSearchRemarks
            // 
            this.txtSearchRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchRemarks.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchRemarks.Location = new System.Drawing.Point(126, 166);
            this.txtSearchRemarks.MaxLength = 20;
            this.txtSearchRemarks.Name = "txtSearchRemarks";
            this.txtSearchRemarks.Size = new System.Drawing.Size(171, 21);
            this.txtSearchRemarks.TabIndex = 6;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(16, 168);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(110, 18);
            this.label22.TabIndex = 285;
            this.label22.Text = "Remarks.";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(998, 1);
            this.label1.TabIndex = 143;
            this.label1.Text = "Company";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(13, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(998, 1);
            this.label3.TabIndex = 146;
            this.label3.Text = "Company";
            // 
            // cmbInvoiceSeries
            // 
            this.cmbInvoiceSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceSeries.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInvoiceSeries.FormattingEnabled = true;
            this.cmbInvoiceSeries.Location = new System.Drawing.Point(574, 46);
            this.cmbInvoiceSeries.Name = "cmbInvoiceSeries";
            this.cmbInvoiceSeries.Size = new System.Drawing.Size(186, 22);
            this.cmbInvoiceSeries.TabIndex = 147;
            this.cmbInvoiceSeries.SelectedIndexChanged += new System.EventHandler(this.cmbInvoiceSeries_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(464, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 18);
            this.label4.TabIndex = 148;
            this.label4.Text = "Invoice Series";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBillingType);
            this.groupBox1.Controls.Add(this.chkSendEmail);
            this.groupBox1.Controls.Add(this.lblDespatched);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.txtContactPerson);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtPartyName);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.txtReferenceNo);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtInvoiceDate);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtInvoiceNo);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.mskDespatchDate);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.mskRequestDate);
            this.groupBox1.Controls.Add(this.chkRequestInvoice);
            this.groupBox1.Controls.Add(this.chkRequestSerialNo);
            this.groupBox1.Controls.Add(this.chkRequestCD);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(215, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblBillingType
            // 
            this.lblBillingType.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingType.ForeColor = System.Drawing.Color.Red;
            this.lblBillingType.Location = new System.Drawing.Point(290, 63);
            this.lblBillingType.Name = "lblBillingType";
            this.lblBillingType.Size = new System.Drawing.Size(216, 18);
            this.lblBillingType.TabIndex = 230;
            this.lblBillingType.Text = "Reference No.";
            this.lblBillingType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkSendEmail
            // 
            this.chkSendEmail.AutoSize = true;
            this.chkSendEmail.Checked = true;
            this.chkSendEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendEmail.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendEmail.Location = new System.Drawing.Point(413, 224);
            this.chkSendEmail.Name = "chkSendEmail";
            this.chkSendEmail.Size = new System.Drawing.Size(97, 20);
            this.chkSendEmail.TabIndex = 229;
            this.chkSendEmail.Text = "Send Email";
            this.chkSendEmail.UseVisualStyleBackColor = true;
            // 
            // lblDespatched
            // 
            this.lblDespatched.BackColor = System.Drawing.Color.Black;
            this.lblDespatched.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDespatched.Font = new System.Drawing.Font("Courier New", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatched.ForeColor = System.Drawing.Color.Lime;
            this.lblDespatched.Location = new System.Drawing.Point(310, 162);
            this.lblDespatched.Name = "lblDespatched";
            this.lblDespatched.Size = new System.Drawing.Size(202, 53);
            this.lblDespatched.TabIndex = 228;
            this.lblDespatched.Text = "Already Despatched";
            this.lblDespatched.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDespatched.Visible = false;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(242, 135);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(44, 17);
            this.label28.TabIndex = 227;
            this.label28.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.Info;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Blue;
            this.txtEmail.Location = new System.Drawing.Point(288, 134);
            this.txtEmail.MaxLength = 200;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(218, 21);
            this.txtEmail.TabIndex = 226;
            this.txtEmail.TabStop = false;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(5, 135);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 17);
            this.label26.TabIndex = 225;
            this.label26.Text = "Mobile No.";
            // 
            // txtMobile
            // 
            this.txtMobile.BackColor = System.Drawing.SystemColors.Info;
            this.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.ForeColor = System.Drawing.Color.Blue;
            this.txtMobile.Location = new System.Drawing.Point(89, 134);
            this.txtMobile.MaxLength = 200;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(147, 21);
            this.txtMobile.TabIndex = 224;
            this.txtMobile.TabStop = false;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(5, 111);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(106, 17);
            this.label25.TabIndex = 223;
            this.label25.Text = "Contact Person";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.BackColor = System.Drawing.SystemColors.Info;
            this.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPerson.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPerson.ForeColor = System.Drawing.Color.Blue;
            this.txtContactPerson.Location = new System.Drawing.Point(113, 110);
            this.txtContactPerson.MaxLength = 200;
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.ReadOnly = true;
            this.txtContactPerson.Size = new System.Drawing.Size(393, 21);
            this.txtContactPerson.TabIndex = 222;
            this.txtContactPerson.TabStop = false;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(5, 87);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(104, 17);
            this.label24.TabIndex = 221;
            this.label24.Text = "Party Name";
            // 
            // txtPartyName
            // 
            this.txtPartyName.BackColor = System.Drawing.SystemColors.Info;
            this.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartyName.ForeColor = System.Drawing.Color.Blue;
            this.txtPartyName.Location = new System.Drawing.Point(113, 86);
            this.txtPartyName.MaxLength = 200;
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.ReadOnly = true;
            this.txtPartyName.Size = new System.Drawing.Size(393, 21);
            this.txtPartyName.TabIndex = 220;
            this.txtPartyName.TabStop = false;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(5, 63);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(104, 17);
            this.label23.TabIndex = 219;
            this.label23.Text = "Reference No.";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceNo.ForeColor = System.Drawing.Color.Blue;
            this.txtReferenceNo.Location = new System.Drawing.Point(113, 62);
            this.txtReferenceNo.MaxLength = 200;
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.ReadOnly = true;
            this.txtReferenceNo.Size = new System.Drawing.Size(171, 21);
            this.txtReferenceNo.TabIndex = 218;
            this.txtReferenceNo.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(287, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 16);
            this.label20.TabIndex = 217;
            this.label20.Text = "(DD/MM/YYYY)";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(5, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 17);
            this.label13.TabIndex = 216;
            this.label13.Text = "Invoice Date";
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceDate.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceDate.ForeColor = System.Drawing.Color.Blue;
            this.txtInvoiceDate.Location = new System.Drawing.Point(113, 38);
            this.txtInvoiceDate.MaxLength = 200;
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.ReadOnly = true;
            this.txtInvoiceDate.Size = new System.Drawing.Size(171, 21);
            this.txtInvoiceDate.TabIndex = 215;
            this.txtInvoiceDate.TabStop = false;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 17);
            this.label12.TabIndex = 214;
            this.label12.Text = "Invoice No.";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNo.ForeColor = System.Drawing.Color.Blue;
            this.txtInvoiceNo.Location = new System.Drawing.Point(113, 14);
            this.txtInvoiceNo.MaxLength = 200;
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(171, 21);
            this.txtInvoiceNo.TabIndex = 213;
            this.txtInvoiceNo.TabStop = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(508, 1);
            this.label11.TabIndex = 212;
            this.label11.Text = "Request Date";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 18);
            this.label9.TabIndex = 211;
            this.label9.Text = "Despatch Date";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(212, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 16);
            this.label10.TabIndex = 210;
            this.label10.Text = "(DD/MM/YYYY)";
            // 
            // mskDespatchDate
            // 
            this.mskDespatchDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskDespatchDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskDespatchDate.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskDespatchDate.Location = new System.Drawing.Point(126, 223);
            this.mskDespatchDate.Mask = "00/00/0000";
            this.mskDespatchDate.Name = "mskDespatchDate";
            this.mskDespatchDate.Size = new System.Drawing.Size(83, 21);
            this.mskDespatchDate.TabIndex = 209;
            this.mskDespatchDate.ValidatingType = typeof(System.DateTime);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(29, 169);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 18);
            this.label21.TabIndex = 208;
            this.label21.Text = "Request Date";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(212, 169);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(92, 16);
            this.label19.TabIndex = 207;
            this.label19.Text = "(DD/MM/YYYY)";
            // 
            // mskRequestDate
            // 
            this.mskRequestDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskRequestDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskRequestDate.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskRequestDate.Location = new System.Drawing.Point(126, 167);
            this.mskRequestDate.Mask = "00/00/0000";
            this.mskRequestDate.Name = "mskRequestDate";
            this.mskRequestDate.ReadOnly = true;
            this.mskRequestDate.Size = new System.Drawing.Size(83, 21);
            this.mskRequestDate.TabIndex = 206;
            this.mskRequestDate.ValidatingType = typeof(System.DateTime);
            this.mskRequestDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // chkRequestInvoice
            // 
            this.chkRequestInvoice.AutoSize = true;
            this.chkRequestInvoice.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRequestInvoice.Location = new System.Drawing.Point(108, 196);
            this.chkRequestInvoice.Name = "chkRequestInvoice";
            this.chkRequestInvoice.Size = new System.Drawing.Size(76, 20);
            this.chkRequestInvoice.TabIndex = 154;
            this.chkRequestInvoice.Text = "Invoice";
            this.chkRequestInvoice.UseVisualStyleBackColor = true;
            // 
            // chkRequestSerialNo
            // 
            this.chkRequestSerialNo.AutoSize = true;
            this.chkRequestSerialNo.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRequestSerialNo.Location = new System.Drawing.Point(212, 196);
            this.chkRequestSerialNo.Name = "chkRequestSerialNo";
            this.chkRequestSerialNo.Size = new System.Drawing.Size(97, 20);
            this.chkRequestSerialNo.TabIndex = 153;
            this.chkRequestSerialNo.Text = "Serial No.";
            this.chkRequestSerialNo.UseVisualStyleBackColor = true;
            // 
            // chkRequestCD
            // 
            this.chkRequestCD.AutoSize = true;
            this.chkRequestCD.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRequestCD.Location = new System.Drawing.Point(39, 196);
            this.chkRequestCD.Name = "chkRequestCD";
            this.chkRequestCD.Size = new System.Drawing.Size(41, 20);
            this.chkRequestCD.TabIndex = 152;
            this.chkRequestCD.Text = "CD";
            this.chkRequestCD.UseVisualStyleBackColor = true;
            // 
            // grpTopPanel
            // 
            this.grpTopPanel.Controls.Add(this.chkInvoice);
            this.grpTopPanel.Controls.Add(this.chkSerialNo);
            this.grpTopPanel.Controls.Add(this.chkCD);
            this.grpTopPanel.Controls.Add(this.txtSearchAll);
            this.grpTopPanel.Controls.Add(this.label2);
            this.grpTopPanel.Location = new System.Drawing.Point(15, 72);
            this.grpTopPanel.Name = "grpTopPanel";
            this.grpTopPanel.Size = new System.Drawing.Size(991, 38);
            this.grpTopPanel.TabIndex = 152;
            this.grpTopPanel.TabStop = false;
            // 
            // chkInvoice
            // 
            this.chkInvoice.AutoSize = true;
            this.chkInvoice.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInvoice.Location = new System.Drawing.Point(842, 14);
            this.chkInvoice.Name = "chkInvoice";
            this.chkInvoice.Size = new System.Drawing.Size(75, 19);
            this.chkInvoice.TabIndex = 156;
            this.chkInvoice.Text = "Invoice";
            this.chkInvoice.UseVisualStyleBackColor = true;
            this.chkInvoice.CheckedChanged += new System.EventHandler(this.chkInvoice_CheckedChanged);
            // 
            // chkSerialNo
            // 
            this.chkSerialNo.AutoSize = true;
            this.chkSerialNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSerialNo.Location = new System.Drawing.Point(739, 14);
            this.chkSerialNo.Name = "chkSerialNo";
            this.chkSerialNo.Size = new System.Drawing.Size(96, 19);
            this.chkSerialNo.TabIndex = 155;
            this.chkSerialNo.Text = "Serial No.";
            this.chkSerialNo.UseVisualStyleBackColor = true;
            this.chkSerialNo.CheckedChanged += new System.EventHandler(this.chkSerialNo_CheckedChanged);
            // 
            // chkCD
            // 
            this.chkCD.AutoSize = true;
            this.chkCD.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCD.Location = new System.Drawing.Point(692, 14);
            this.chkCD.Name = "chkCD";
            this.chkCD.Size = new System.Drawing.Size(40, 19);
            this.chkCD.TabIndex = 154;
            this.chkCD.Text = "CD";
            this.chkCD.UseVisualStyleBackColor = true;
            this.chkCD.CheckedChanged += new System.EventHandler(this.chkCD_CheckedChanged);
            // 
            // txtSearchAll
            // 
            this.txtSearchAll.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSearchAll.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAll.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAll.Location = new System.Drawing.Point(110, 13);
            this.txtSearchAll.Name = "txtSearchAll";
            this.txtSearchAll.Size = new System.Drawing.Size(566, 21);
            this.txtSearchAll.TabIndex = 153;
            this.txtSearchAll.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(71, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 152;
            this.label2.Text = "&Find";
            // 
            // TrnRequestCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.grpTopPanel);
            this.Controls.Add(this.cmbInvoiceSeries);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.chkCancelledEntry);
            this.Name = "TrnRequestCD";
            this.Load += new System.EventHandler(this.BankEntry_Load);
            this.Controls.SetChildIndex(this.chkCancelledEntry, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbInvoiceSeries, 0);
            this.Controls.SetChildIndex(this.grpTopPanel, 0);
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
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTopPanel.ResumeLayout(false);
            this.grpTopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.TextBox txtAmountSearch;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtReferenceSearch;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mskInvoiceDateSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox mskBankDateSearch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mskAccountEntryDateSearch;
        public System.Windows.Forms.TextBox txtBankSearch;
        private System.Windows.Forms.CheckBox chkCancelledEntry;
        public System.Windows.Forms.TextBox txtSearchRemarks;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbInvoiceSeries;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRequestInvoice;
        private System.Windows.Forms.CheckBox chkRequestSerialNo;
        private System.Windows.Forms.CheckBox chkRequestCD;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox mskRequestDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox mskDespatchDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtInvoiceDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox txtPartyName;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDespatched;
        private System.Windows.Forms.CheckBox chkSendEmail;
        private System.Windows.Forms.GroupBox grpTopPanel;
        private System.Windows.Forms.CheckBox chkInvoice;
        private System.Windows.Forms.CheckBox chkSerialNo;
        private System.Windows.Forms.CheckBox chkCD;
        public System.Windows.Forms.TextBox txtSearchAll;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBillingType;
    }
}
