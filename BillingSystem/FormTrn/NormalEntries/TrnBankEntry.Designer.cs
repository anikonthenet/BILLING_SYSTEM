namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnBankEntry
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPartyName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbPaymentType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mskAccountEntryDate = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.mskBankDate = new System.Windows.Forms.MaskedTextBox();
            this.cmbFilterPaymentType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIncompleteRecords = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPartyNameSearch = new System.Windows.Forms.TextBox();
            this.txtReferenceSearch = new System.Windows.Forms.TextBox();
            this.mskInvoiceDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.mskAccountEntryDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mskBankEntryDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbPartyCategory = new System.Windows.Forms.ComboBox();
            this.lblPartyCategory = new System.Windows.Forms.Label();
            this.lblACEntryDateDifference = new System.Windows.Forms.Label();
            this.lblBankDateDifference = new System.Windows.Forms.Label();
            this.rdbtnMaxDaysPermit = new System.Windows.Forms.RadioButton();
            this.rdbtnAsEntered = new System.Windows.Forms.RadioButton();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Controls.Add(this.rdbtnAsEntered);
            this.grpSort.Controls.Add(this.rdbtnMaxDaysPermit);
            this.grpSort.Location = new System.Drawing.Point(586, 458);
            this.grpSort.Size = new System.Drawing.Size(280, 130);
            this.grpSort.Controls.SetChildIndex(this.rdbtnMaxDaysPermit, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.rdbtnAsEntered, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(196, 98);
            this.BtnSortCancel.Click += new System.EventHandler(this.BtnSortCancel_Click);
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(125, 98);
            this.BtnSortOK.Click += new System.EventHandler(this.BtnSortOK_Click);
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
            this.grpSearch.Controls.Add(this.mskBankEntryDateSearch);
            this.grpSearch.Controls.Add(this.label15);
            this.grpSearch.Controls.Add(this.mskAccountEntryDateSearch);
            this.grpSearch.Controls.Add(this.label7);
            this.grpSearch.Controls.Add(this.mskInvoiceDateSearch);
            this.grpSearch.Controls.Add(this.txtReferenceSearch);
            this.grpSearch.Controls.Add(this.txtPartyNameSearch);
            this.grpSearch.Controls.Add(this.label14);
            this.grpSearch.Controls.Add(this.label5);
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Location = new System.Drawing.Point(511, 392);
            this.grpSearch.Size = new System.Drawing.Size(358, 199);
            this.grpSearch.Controls.SetChildIndex(this.label3, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.label5, 0);
            this.grpSearch.Controls.SetChildIndex(this.label14, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtPartyNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtReferenceSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskInvoiceDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label7, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskAccountEntryDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label15, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskBankEntryDateSearch, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(274, 167);
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(203, 167);
            this.BtnSearchOK.TabIndex = 3;
            this.BtnSearchOK.Click += new System.EventHandler(this.BtnSearchOK_Click);
            this.BtnSearchOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchOK_KeyPress);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblBankDateDifference);
            this.pnlControls.Controls.Add(this.lblACEntryDateDifference);
            this.pnlControls.Controls.Add(this.cmbPartyCategory);
            this.pnlControls.Controls.Add(this.lblPartyCategory);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Controls.Add(this.label13);
            this.pnlControls.Controls.Add(this.mskBankDate);
            this.pnlControls.Controls.Add(this.label21);
            this.pnlControls.Controls.Add(this.label19);
            this.pnlControls.Controls.Add(this.mskAccountEntryDate);
            this.pnlControls.Controls.Add(this.txtReference);
            this.pnlControls.Controls.Add(this.cmbBank);
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.label10);
            this.pnlControls.Controls.Add(this.cmbPaymentType);
            this.pnlControls.Controls.Add(this.label11);
            this.pnlControls.Controls.Add(this.lblAmount);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.lblPartyName);
            this.pnlControls.Controls.Add(this.label6);
            this.pnlControls.Controls.Add(this.lblInvoiceDate);
            this.pnlControls.Controls.Add(this.label4);
            this.pnlControls.Controls.Add(this.lblInvoiceNo);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Location = new System.Drawing.Point(9, 88);
            this.pnlControls.Size = new System.Drawing.Size(1003, 508);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackColor = System.Drawing.Color.Black;
            this.ViewGrid.GridLineColor = System.Drawing.Color.Black;
            this.ViewGrid.Location = new System.Drawing.Point(9, 88);
            this.ViewGrid.Size = new System.Drawing.Size(1000, 11);
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(129, 51);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(333, 21);
            this.cmbCompany.TabIndex = 140;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(13, 53);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(112, 18);
            this.label27.TabIndex = 141;
            this.label27.Text = "Company";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice No.";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.BackColor = System.Drawing.SystemColors.Info;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNo.Location = new System.Drawing.Point(107, 41);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(115, 23);
            this.lblInvoiceNo.TabIndex = 1;
            this.lblInvoiceNo.Text = "Invoice No. Value";
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.BackColor = System.Drawing.SystemColors.Info;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceDate.Location = new System.Drawing.Point(107, 75);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(115, 23);
            this.lblInvoiceDate.TabIndex = 3;
            this.lblInvoiceDate.Text = "Date Value";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(39, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Date";
            // 
            // lblPartyName
            // 
            this.lblPartyName.BackColor = System.Drawing.SystemColors.Info;
            this.lblPartyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartyName.Location = new System.Drawing.Point(326, 40);
            this.lblPartyName.Name = "lblPartyName";
            this.lblPartyName.Size = new System.Drawing.Size(509, 23);
            this.lblPartyName.TabIndex = 5;
            this.lblPartyName.Text = "Party Name Value";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(257, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "Party Name";
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.SystemColors.Info;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(325, 75);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(86, 23);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Text = "Amount Value";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(256, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "Amount";
            // 
            // txtReference
            // 
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.Location = new System.Drawing.Point(149, 182);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(171, 21);
            this.txtReference.TabIndex = 1;
            this.txtReference.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbBank
            // 
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(149, 205);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(171, 21);
            this.cmbBank.TabIndex = 2;
            this.cmbBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(33, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 18);
            this.label9.TabIndex = 258;
            this.label9.Text = "Bank";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(33, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 18);
            this.label10.TabIndex = 256;
            this.label10.Text = "Reference No.";
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(149, 156);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(171, 21);
            this.cmbPaymentType.TabIndex = 0;
            this.cmbPaymentType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(33, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 18);
            this.label11.TabIndex = 255;
            this.label11.Text = "Payment Type";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(33, 248);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 18);
            this.label21.TabIndex = 262;
            this.label21.Text = "A/c Entry Date";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(235, 250);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 15);
            this.label19.TabIndex = 261;
            this.label19.Text = "(DD/MM/YYYY)";
            // 
            // mskAccountEntryDate
            // 
            this.mskAccountEntryDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskAccountEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskAccountEntryDate.Location = new System.Drawing.Point(149, 248);
            this.mskAccountEntryDate.Mask = "00/00/0000";
            this.mskAccountEntryDate.Name = "mskAccountEntryDate";
            this.mskAccountEntryDate.Size = new System.Drawing.Size(83, 20);
            this.mskAccountEntryDate.TabIndex = 3;
            this.mskAccountEntryDate.ValidatingType = typeof(System.DateTime);
            this.mskAccountEntryDate.Leave += new System.EventHandler(this.mskAccountEntryDate_Leave);
            this.mskAccountEntryDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.mskAccountEntryDate.TextChanged += new System.EventHandler(this.mskAccountEntryDate_Leave);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(33, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 18);
            this.label12.TabIndex = 265;
            this.label12.Text = "Bank Date";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(235, 282);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 15);
            this.label13.TabIndex = 264;
            this.label13.Text = "(DD/MM/YYYY)";
            // 
            // mskBankDate
            // 
            this.mskBankDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskBankDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskBankDate.Location = new System.Drawing.Point(149, 280);
            this.mskBankDate.Mask = "00/00/0000";
            this.mskBankDate.Name = "mskBankDate";
            this.mskBankDate.Size = new System.Drawing.Size(83, 20);
            this.mskBankDate.TabIndex = 4;
            this.mskBankDate.ValidatingType = typeof(System.DateTime);
            this.mskBankDate.Leave += new System.EventHandler(this.mskBankDate_Leave);
            this.mskBankDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.mskBankDate.TextChanged += new System.EventHandler(this.mskBankDate_Leave);
            // 
            // cmbFilterPaymentType
            // 
            this.cmbFilterPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterPaymentType.FormattingEnabled = true;
            this.cmbFilterPaymentType.Location = new System.Drawing.Point(568, 51);
            this.cmbFilterPaymentType.Name = "cmbFilterPaymentType";
            this.cmbFilterPaymentType.Size = new System.Drawing.Size(175, 21);
            this.cmbFilterPaymentType.TabIndex = 142;
            this.cmbFilterPaymentType.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(473, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 18);
            this.label2.TabIndex = 143;
            this.label2.Text = "Payment Type";
            // 
            // chkIncompleteRecords
            // 
            this.chkIncompleteRecords.AutoSize = true;
            this.chkIncompleteRecords.Location = new System.Drawing.Point(761, 53);
            this.chkIncompleteRecords.Name = "chkIncompleteRecords";
            this.chkIncompleteRecords.Size = new System.Drawing.Size(157, 17);
            this.chkIncompleteRecords.TabIndex = 144;
            this.chkIncompleteRecords.Text = "Show only Incomplete Entry";
            this.chkIncompleteRecords.UseVisualStyleBackColor = true;
            this.chkIncompleteRecords.CheckedChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Party Name";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Invoice Date";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(17, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "Reference No";
            // 
            // txtPartyNameSearch
            // 
            this.txtPartyNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyNameSearch.Location = new System.Drawing.Point(144, 23);
            this.txtPartyNameSearch.Name = "txtPartyNameSearch";
            this.txtPartyNameSearch.Size = new System.Drawing.Size(197, 21);
            this.txtPartyNameSearch.TabIndex = 0;
            this.txtPartyNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtReferenceSearch
            // 
            this.txtReferenceSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceSearch.Location = new System.Drawing.Point(144, 79);
            this.txtReferenceSearch.Name = "txtReferenceSearch";
            this.txtReferenceSearch.Size = new System.Drawing.Size(197, 21);
            this.txtReferenceSearch.TabIndex = 2;
            this.txtReferenceSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // mskInvoiceDateSearch
            // 
            this.mskInvoiceDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskInvoiceDateSearch.Location = new System.Drawing.Point(144, 52);
            this.mskInvoiceDateSearch.Mask = "00/00/0000";
            this.mskInvoiceDateSearch.Name = "mskInvoiceDateSearch";
            this.mskInvoiceDateSearch.Size = new System.Drawing.Size(86, 21);
            this.mskInvoiceDateSearch.TabIndex = 1;
            this.mskInvoiceDateSearch.ValidatingType = typeof(System.DateTime);
            this.mskInvoiceDateSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // mskAccountEntryDateSearch
            // 
            this.mskAccountEntryDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskAccountEntryDateSearch.Location = new System.Drawing.Point(144, 107);
            this.mskAccountEntryDateSearch.Mask = "00/00/0000";
            this.mskAccountEntryDateSearch.Name = "mskAccountEntryDateSearch";
            this.mskAccountEntryDateSearch.Size = new System.Drawing.Size(86, 21);
            this.mskAccountEntryDateSearch.TabIndex = 18;
            this.mskAccountEntryDateSearch.ValidatingType = typeof(System.DateTime);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "A/c Entry Date";
            // 
            // mskBankEntryDateSearch
            // 
            this.mskBankEntryDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskBankEntryDateSearch.Location = new System.Drawing.Point(144, 133);
            this.mskBankEntryDateSearch.Mask = "00/00/0000";
            this.mskBankEntryDateSearch.Name = "mskBankEntryDateSearch";
            this.mskBankEntryDateSearch.Size = new System.Drawing.Size(86, 21);
            this.mskBankEntryDateSearch.TabIndex = 20;
            this.mskBankEntryDateSearch.ValidatingType = typeof(System.DateTime);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(16, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 15);
            this.label15.TabIndex = 21;
            this.label15.Text = "Bank Entry Date";
            // 
            // cmbPartyCategory
            // 
            this.cmbPartyCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartyCategory.FormattingEnabled = true;
            this.cmbPartyCategory.Location = new System.Drawing.Point(149, 129);
            this.cmbPartyCategory.Name = "cmbPartyCategory";
            this.cmbPartyCategory.Size = new System.Drawing.Size(171, 21);
            this.cmbPartyCategory.TabIndex = 266;
            // 
            // lblPartyCategory
            // 
            this.lblPartyCategory.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartyCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartyCategory.Location = new System.Drawing.Point(33, 129);
            this.lblPartyCategory.Name = "lblPartyCategory";
            this.lblPartyCategory.Size = new System.Drawing.Size(112, 18);
            this.lblPartyCategory.TabIndex = 267;
            this.lblPartyCategory.Text = "Party Category";
            // 
            // lblACEntryDateDifference
            // 
            this.lblACEntryDateDifference.AutoSize = true;
            this.lblACEntryDateDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblACEntryDateDifference.ForeColor = System.Drawing.Color.Blue;
            this.lblACEntryDateDifference.Location = new System.Drawing.Point(353, 252);
            this.lblACEntryDateDifference.Name = "lblACEntryDateDifference";
            this.lblACEntryDateDifference.Size = new System.Drawing.Size(198, 18);
            this.lblACEntryDateDifference.TabIndex = 268;
            this.lblACEntryDateDifference.Text = "lblACEntryDateDifference";
            // 
            // lblBankDateDifference
            // 
            this.lblBankDateDifference.AutoSize = true;
            this.lblBankDateDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBankDateDifference.ForeColor = System.Drawing.Color.Blue;
            this.lblBankDateDifference.Location = new System.Drawing.Point(353, 281);
            this.lblBankDateDifference.Name = "lblBankDateDifference";
            this.lblBankDateDifference.Size = new System.Drawing.Size(175, 18);
            this.lblBankDateDifference.TabIndex = 269;
            this.lblBankDateDifference.Text = "lblBankDateDifference";
            // 
            // rdbtnMaxDaysPermit
            // 
            this.rdbtnMaxDaysPermit.AutoSize = true;
            this.rdbtnMaxDaysPermit.Location = new System.Drawing.Point(23, 35);
            this.rdbtnMaxDaysPermit.Name = "rdbtnMaxDaysPermit";
            this.rdbtnMaxDaysPermit.Size = new System.Drawing.Size(186, 19);
            this.rdbtnMaxDaysPermit.TabIndex = 12;
            this.rdbtnMaxDaysPermit.TabStop = true;
            this.rdbtnMaxDaysPermit.Text = "Crossed Max Days Permit";
            this.rdbtnMaxDaysPermit.UseVisualStyleBackColor = true;
            // 
            // rdbtnAsEntered
            // 
            this.rdbtnAsEntered.AutoSize = true;
            this.rdbtnAsEntered.Location = new System.Drawing.Point(23, 62);
            this.rdbtnAsEntered.Name = "rdbtnAsEntered";
            this.rdbtnAsEntered.Size = new System.Drawing.Size(95, 19);
            this.rdbtnAsEntered.TabIndex = 13;
            this.rdbtnAsEntered.TabStop = true;
            this.rdbtnAsEntered.Text = "As Entered";
            this.rdbtnAsEntered.UseVisualStyleBackColor = true;
            // 
            // TrnBankEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.chkIncompleteRecords);
            this.Controls.Add(this.cmbFilterPaymentType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label27);
            this.Name = "TrnBankEntry";
            this.Load += new System.EventHandler(this.BankEntry_Load);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbFilterPaymentType, 0);
            this.Controls.SetChildIndex(this.chkIncompleteRecords, 0);
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
            this.grpSort.ResumeLayout(false);
            this.grpSort.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPartyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbPaymentType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox mskBankDate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox mskAccountEntryDate;
        private System.Windows.Forms.ComboBox cmbFilterPaymentType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIncompleteRecords;
        private System.Windows.Forms.TextBox txtReferenceSearch;
        private System.Windows.Forms.TextBox txtPartyNameSearch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mskInvoiceDateSearch;
        private System.Windows.Forms.MaskedTextBox mskBankEntryDateSearch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mskAccountEntryDateSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPartyCategory;
        private System.Windows.Forms.Label lblPartyCategory;
        private System.Windows.Forms.Label lblBankDateDifference;
        private System.Windows.Forms.Label lblACEntryDateDifference;
        private System.Windows.Forms.RadioButton rdbtnMaxDaysPermit;
        private System.Windows.Forms.RadioButton rdbtnAsEntered;
    }
}
