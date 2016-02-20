namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnSendEmailAgainstInvoice
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
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPartyNameSearch = new System.Windows.Forms.TextBox();
            this.mskInvoiceDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.rdbtnMaxDaysPermit = new System.Windows.Forms.RadioButton();
            this.rdbtnAsEntered = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtEmailID = new System.Windows.Forms.TextBox();
            this.grpPartyInfo = new System.Windows.Forms.GroupBox();
            this.grpInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.lblEmailSendCounter = new System.Windows.Forms.Label();
            this.lblEmailSendCounterCaption = new System.Windows.Forms.Label();
            this.lblLastSendDate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.lblPartyName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailCategoryDesc = new System.Windows.Forms.TextBox();
            this.grpEmailExists = new System.Windows.Forms.GroupBox();
            this.rbnNewEmailFormat = new System.Windows.Forms.RadioButton();
            this.rbnExistingEmailCategory = new System.Windows.Forms.RadioButton();
            this.lblNewEmailFormatDesc = new System.Windows.Forms.Label();
            this.grpViewPdf = new System.Windows.Forms.GroupBox();
            this.wbsViewPdf = new System.Windows.Forms.WebBrowser();
            this.btnPreviewBill = new System.Windows.Forms.Button();
            this.txtPDFPath = new System.Windows.Forms.TextBox();
            this.cmbEmailCategoryDesc = new System.Windows.Forms.ComboBox();
            this.lblExistingEmailFormatDesc = new System.Windows.Forms.Label();
            this.pnlEmailTextFormat = new System.Windows.Forms.Panel();
            this.rtxtEmailBody = new System.Windows.Forms.RichTextBox();
            this.txtEmailIDBcc = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.grpPopulateData = new System.Windows.Forms.GroupBox();
            this.rbnLoadPending = new System.Windows.Forms.RadioButton();
            this.rbnLoadAll = new System.Windows.Forms.RadioButton();
            this.txtInvoiceNoSearch = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpPartyInfo.SuspendLayout();
            this.grpInvoiceDetails.SuspendLayout();
            this.grpEmailExists.SuspendLayout();
            this.grpViewPdf.SuspendLayout();
            this.pnlEmailTextFormat.SuspendLayout();
            this.grpPopulateData.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Controls.Add(this.rdbtnAsEntered);
            this.grpSort.Controls.Add(this.rdbtnMaxDaysPermit);
            this.grpSort.Location = new System.Drawing.Point(727, 652);
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
            this.grpSearch.Controls.Add(this.mskInvoiceDateSearch);
            this.grpSearch.Controls.Add(this.txtInvoiceNoSearch);
            this.grpSearch.Controls.Add(this.txtPartyNameSearch);
            this.grpSearch.Controls.Add(this.label14);
            this.grpSearch.Controls.Add(this.label5);
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Location = new System.Drawing.Point(653, 467);
            this.grpSearch.Size = new System.Drawing.Size(358, 135);
            this.grpSearch.Controls.SetChildIndex(this.label3, 0);
            this.grpSearch.Controls.SetChildIndex(this.label5, 0);
            this.grpSearch.Controls.SetChildIndex(this.label14, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtPartyNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtInvoiceNoSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskInvoiceDateSearch, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(281, 106);
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(210, 106);
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
            this.pnlControls.Controls.Add(this.grpPartyInfo);
            this.pnlControls.Location = new System.Drawing.Point(9, 85);
            this.pnlControls.Size = new System.Drawing.Size(1004, 512);
            // 
            // ViewGrid
            // 
            this.ViewGrid.BackColor = System.Drawing.Color.Black;
            this.ViewGrid.GridLineColor = System.Drawing.Color.Black;
            this.ViewGrid.Location = new System.Drawing.Point(9, 77);
            this.ViewGrid.Size = new System.Drawing.Size(1004, 11);
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
            this.cmbCompany.TabIndex = 0;
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
            // txtPartyNameSearch
            // 
            this.txtPartyNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyNameSearch.Location = new System.Drawing.Point(144, 23);
            this.txtPartyNameSearch.Name = "txtPartyNameSearch";
            this.txtPartyNameSearch.Size = new System.Drawing.Size(197, 21);
            this.txtPartyNameSearch.TabIndex = 0;
            this.txtPartyNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(10, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 2);
            this.panel1.TabIndex = 142;
            // 
            // txtEmailID
            // 
            this.txtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailID.Location = new System.Drawing.Point(120, 91);
            this.txtEmailID.MaxLength = 100;
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(467, 21);
            this.txtEmailID.TabIndex = 1;
            // 
            // grpPartyInfo
            // 
            this.grpPartyInfo.Controls.Add(this.grpInvoiceDetails);
            this.grpPartyInfo.Controls.Add(this.txtEmailCategoryDesc);
            this.grpPartyInfo.Controls.Add(this.grpEmailExists);
            this.grpPartyInfo.Controls.Add(this.lblNewEmailFormatDesc);
            this.grpPartyInfo.Controls.Add(this.grpViewPdf);
            this.grpPartyInfo.Controls.Add(this.cmbEmailCategoryDesc);
            this.grpPartyInfo.Controls.Add(this.lblExistingEmailFormatDesc);
            this.grpPartyInfo.Controls.Add(this.pnlEmailTextFormat);
            this.grpPartyInfo.Controls.Add(this.txtEmailIDBcc);
            this.grpPartyInfo.Controls.Add(this.label19);
            this.grpPartyInfo.Controls.Add(this.txtEmailID);
            this.grpPartyInfo.Controls.Add(this.label2);
            this.grpPartyInfo.Controls.Add(this.label13);
            this.grpPartyInfo.Location = new System.Drawing.Point(2, 2);
            this.grpPartyInfo.Name = "grpPartyInfo";
            this.grpPartyInfo.Size = new System.Drawing.Size(1000, 504);
            this.grpPartyInfo.TabIndex = 0;
            this.grpPartyInfo.TabStop = false;
            // 
            // grpInvoiceDetails
            // 
            this.grpInvoiceDetails.Controls.Add(this.lblEmailSendCounter);
            this.grpInvoiceDetails.Controls.Add(this.lblEmailSendCounterCaption);
            this.grpInvoiceDetails.Controls.Add(this.lblLastSendDate);
            this.grpInvoiceDetails.Controls.Add(this.label9);
            this.grpInvoiceDetails.Controls.Add(this.lblInvoiceNo);
            this.grpInvoiceDetails.Controls.Add(this.label1);
            this.grpInvoiceDetails.Controls.Add(this.lblAmount);
            this.grpInvoiceDetails.Controls.Add(this.label4);
            this.grpInvoiceDetails.Controls.Add(this.label8);
            this.grpInvoiceDetails.Controls.Add(this.lblInvoiceDate);
            this.grpInvoiceDetails.Controls.Add(this.lblPartyName);
            this.grpInvoiceDetails.Controls.Add(this.label6);
            this.grpInvoiceDetails.Location = new System.Drawing.Point(8, 9);
            this.grpInvoiceDetails.Name = "grpInvoiceDetails";
            this.grpInvoiceDetails.Size = new System.Drawing.Size(578, 74);
            this.grpInvoiceDetails.TabIndex = 0;
            this.grpInvoiceDetails.TabStop = false;
            // 
            // lblEmailSendCounter
            // 
            this.lblEmailSendCounter.BackColor = System.Drawing.SystemColors.Control;
            this.lblEmailSendCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailSendCounter.ForeColor = System.Drawing.Color.Maroon;
            this.lblEmailSendCounter.Location = new System.Drawing.Point(363, 45);
            this.lblEmailSendCounter.Name = "lblEmailSendCounter";
            this.lblEmailSendCounter.Size = new System.Drawing.Size(126, 16);
            this.lblEmailSendCounter.TabIndex = 265;
            this.lblEmailSendCounter.Text = "No of Email Send";
            this.lblEmailSendCounter.Visible = false;
            // 
            // lblEmailSendCounterCaption
            // 
            this.lblEmailSendCounterCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEmailSendCounterCaption.ForeColor = System.Drawing.Color.Teal;
            this.lblEmailSendCounterCaption.Location = new System.Drawing.Point(239, 44);
            this.lblEmailSendCounterCaption.Name = "lblEmailSendCounterCaption";
            this.lblEmailSendCounterCaption.Size = new System.Drawing.Size(118, 15);
            this.lblEmailSendCounterCaption.TabIndex = 264;
            this.lblEmailSendCounterCaption.Text = "No of Times Send :";
            this.lblEmailSendCounterCaption.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLastSendDate
            // 
            this.lblLastSendDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblLastSendDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastSendDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblLastSendDate.Location = new System.Drawing.Point(110, 45);
            this.lblLastSendDate.Name = "lblLastSendDate";
            this.lblLastSendDate.Size = new System.Drawing.Size(126, 16);
            this.lblLastSendDate.TabIndex = 263;
            this.lblLastSendDate.Text = "Email Send Date";
            this.lblLastSendDate.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Teal;
            this.label9.Location = new System.Drawing.Point(5, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 15);
            this.label9.TabIndex = 262;
            this.label9.Text = "Last Send Date:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblInvoiceNo.Location = new System.Drawing.Point(110, 11);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(123, 16);
            this.lblInvoiceNo.TabIndex = 8;
            this.lblInvoiceNo.Text = "Invoice No. Value";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Invoice No :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.SystemColors.Control;
            this.lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.Maroon;
            this.lblAmount.Location = new System.Drawing.Point(472, 11);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(100, 16);
            this.lblAmount.TabIndex = 11;
            this.lblAmount.Text = "Amount Value";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Teal;
            this.label4.Location = new System.Drawing.Point(239, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Inv Date:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(387, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Inv Amount :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblInvoiceDate.Location = new System.Drawing.Point(305, 11);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(76, 16);
            this.lblInvoiceDate.TabIndex = 9;
            this.lblInvoiceDate.Text = "Date Value";
            // 
            // lblPartyName
            // 
            this.lblPartyName.BackColor = System.Drawing.SystemColors.Control;
            this.lblPartyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartyName.ForeColor = System.Drawing.Color.Maroon;
            this.lblPartyName.Location = new System.Drawing.Point(110, 28);
            this.lblPartyName.Name = "lblPartyName";
            this.lblPartyName.Size = new System.Drawing.Size(462, 16);
            this.lblPartyName.TabIndex = 12;
            this.lblPartyName.Text = "Party Name Value";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(5, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Party Name :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmailCategoryDesc
            // 
            this.txtEmailCategoryDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailCategoryDesc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailCategoryDesc.Location = new System.Drawing.Point(119, 186);
            this.txtEmailCategoryDesc.MaxLength = 255;
            this.txtEmailCategoryDesc.Name = "txtEmailCategoryDesc";
            this.txtEmailCategoryDesc.Size = new System.Drawing.Size(332, 21);
            this.txtEmailCategoryDesc.TabIndex = 4;
            this.txtEmailCategoryDesc.Visible = false;
            // 
            // grpEmailExists
            // 
            this.grpEmailExists.Controls.Add(this.rbnNewEmailFormat);
            this.grpEmailExists.Controls.Add(this.rbnExistingEmailCategory);
            this.grpEmailExists.Location = new System.Drawing.Point(120, 150);
            this.grpEmailExists.Name = "grpEmailExists";
            this.grpEmailExists.Size = new System.Drawing.Size(331, 34);
            this.grpEmailExists.TabIndex = 3;
            this.grpEmailExists.TabStop = false;
            // 
            // rbnNewEmailFormat
            // 
            this.rbnNewEmailFormat.AutoSize = true;
            this.rbnNewEmailFormat.Enabled = false;
            this.rbnNewEmailFormat.Location = new System.Drawing.Point(166, 10);
            this.rbnNewEmailFormat.Name = "rbnNewEmailFormat";
            this.rbnNewEmailFormat.Size = new System.Drawing.Size(110, 17);
            this.rbnNewEmailFormat.TabIndex = 1;
            this.rbnNewEmailFormat.TabStop = true;
            this.rbnNewEmailFormat.Text = "New Email Format";
            this.rbnNewEmailFormat.UseVisualStyleBackColor = true;
            this.rbnNewEmailFormat.Visible = false;
            this.rbnNewEmailFormat.CheckedChanged += new System.EventHandler(this.rbnExistingEmailCategory_CheckedChanged);
            // 
            // rbnExistingEmailCategory
            // 
            this.rbnExistingEmailCategory.AutoSize = true;
            this.rbnExistingEmailCategory.Location = new System.Drawing.Point(10, 11);
            this.rbnExistingEmailCategory.Name = "rbnExistingEmailCategory";
            this.rbnExistingEmailCategory.Size = new System.Drawing.Size(124, 17);
            this.rbnExistingEmailCategory.TabIndex = 0;
            this.rbnExistingEmailCategory.TabStop = true;
            this.rbnExistingEmailCategory.Text = "Existing Email Format";
            this.rbnExistingEmailCategory.UseVisualStyleBackColor = true;
            this.rbnExistingEmailCategory.CheckedChanged += new System.EventHandler(this.rbnExistingEmailCategory_CheckedChanged);
            // 
            // lblNewEmailFormatDesc
            // 
            this.lblNewEmailFormatDesc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewEmailFormatDesc.ForeColor = System.Drawing.Color.Teal;
            this.lblNewEmailFormatDesc.Location = new System.Drawing.Point(11, 188);
            this.lblNewEmailFormatDesc.Name = "lblNewEmailFormatDesc";
            this.lblNewEmailFormatDesc.Size = new System.Drawing.Size(98, 15);
            this.lblNewEmailFormatDesc.TabIndex = 292;
            this.lblNewEmailFormatDesc.Text = "Category Desc :";
            this.lblNewEmailFormatDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblNewEmailFormatDesc.Visible = false;
            // 
            // grpViewPdf
            // 
            this.grpViewPdf.Controls.Add(this.wbsViewPdf);
            this.grpViewPdf.Controls.Add(this.btnPreviewBill);
            this.grpViewPdf.Controls.Add(this.txtPDFPath);
            this.grpViewPdf.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpViewPdf.Location = new System.Drawing.Point(593, 8);
            this.grpViewPdf.Name = "grpViewPdf";
            this.grpViewPdf.Size = new System.Drawing.Size(402, 226);
            this.grpViewPdf.TabIndex = 7;
            this.grpViewPdf.TabStop = false;
            this.grpViewPdf.Text = "Preview Pdf";
            // 
            // wbsViewPdf
            // 
            this.wbsViewPdf.Location = new System.Drawing.Point(6, 17);
            this.wbsViewPdf.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbsViewPdf.Name = "wbsViewPdf";
            this.wbsViewPdf.Size = new System.Drawing.Size(394, 176);
            this.wbsViewPdf.TabIndex = 0;
            // 
            // btnPreviewBill
            // 
            this.btnPreviewBill.BackColor = System.Drawing.Color.AliceBlue;
            this.btnPreviewBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreviewBill.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviewBill.ForeColor = System.Drawing.Color.Maroon;
            this.btnPreviewBill.Location = new System.Drawing.Point(295, 194);
            this.btnPreviewBill.Name = "btnPreviewBill";
            this.btnPreviewBill.Size = new System.Drawing.Size(107, 24);
            this.btnPreviewBill.TabIndex = 1;
            this.btnPreviewBill.Text = "Preview";
            this.btnPreviewBill.UseVisualStyleBackColor = false;
            this.btnPreviewBill.Click += new System.EventHandler(this.btnPreviewBill_Click);
            // 
            // txtPDFPath
            // 
            this.txtPDFPath.Location = new System.Drawing.Point(10, 195);
            this.txtPDFPath.Name = "txtPDFPath";
            this.txtPDFPath.Size = new System.Drawing.Size(4, 21);
            this.txtPDFPath.TabIndex = 16;
            this.txtPDFPath.Visible = false;
            // 
            // cmbEmailCategoryDesc
            // 
            this.cmbEmailCategoryDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmailCategoryDesc.FormattingEnabled = true;
            this.cmbEmailCategoryDesc.Location = new System.Drawing.Point(118, 210);
            this.cmbEmailCategoryDesc.Name = "cmbEmailCategoryDesc";
            this.cmbEmailCategoryDesc.Size = new System.Drawing.Size(333, 21);
            this.cmbEmailCategoryDesc.TabIndex = 5;
            this.cmbEmailCategoryDesc.Visible = false;
            this.cmbEmailCategoryDesc.SelectedIndexChanged += new System.EventHandler(this.cmbEmailCategoryDesc_SelectedIndexChanged);
            // 
            // lblExistingEmailFormatDesc
            // 
            this.lblExistingEmailFormatDesc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistingEmailFormatDesc.ForeColor = System.Drawing.Color.Teal;
            this.lblExistingEmailFormatDesc.Location = new System.Drawing.Point(11, 212);
            this.lblExistingEmailFormatDesc.Name = "lblExistingEmailFormatDesc";
            this.lblExistingEmailFormatDesc.Size = new System.Drawing.Size(98, 15);
            this.lblExistingEmailFormatDesc.TabIndex = 288;
            this.lblExistingEmailFormatDesc.Text = "Email Category :";
            this.lblExistingEmailFormatDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblExistingEmailFormatDesc.Visible = false;
            // 
            // pnlEmailTextFormat
            // 
            this.pnlEmailTextFormat.AutoScroll = true;
            this.pnlEmailTextFormat.Controls.Add(this.rtxtEmailBody);
            this.pnlEmailTextFormat.Location = new System.Drawing.Point(5, 241);
            this.pnlEmailTextFormat.Name = "pnlEmailTextFormat";
            this.pnlEmailTextFormat.Size = new System.Drawing.Size(994, 262);
            this.pnlEmailTextFormat.TabIndex = 6;
            // 
            // rtxtEmailBody
            // 
            this.rtxtEmailBody.Location = new System.Drawing.Point(111, 6);
            this.rtxtEmailBody.Name = "rtxtEmailBody";
            this.rtxtEmailBody.Size = new System.Drawing.Size(859, 252);
            this.rtxtEmailBody.TabIndex = 0;
            this.rtxtEmailBody.Text = "";
            // 
            // txtEmailIDBcc
            // 
            this.txtEmailIDBcc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailIDBcc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailIDBcc.Location = new System.Drawing.Point(120, 129);
            this.txtEmailIDBcc.MaxLength = 100;
            this.txtEmailIDBcc.Name = "txtEmailIDBcc";
            this.txtEmailIDBcc.Size = new System.Drawing.Size(467, 21);
            this.txtEmailIDBcc.TabIndex = 2;
            this.txtEmailIDBcc.Visible = false;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Teal;
            this.label19.Location = new System.Drawing.Point(11, 132);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 15);
            this.label19.TabIndex = 284;
            this.label19.Text = "Bcc :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label19.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(11, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 261;
            this.label2.Text = "To :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(264, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(322, 15);
            this.label13.TabIndex = 280;
            this.label13.Text = "( For multiple email ids, separate with \',\' )";
            // 
            // grpPopulateData
            // 
            this.grpPopulateData.Controls.Add(this.rbnLoadPending);
            this.grpPopulateData.Controls.Add(this.rbnLoadAll);
            this.grpPopulateData.Location = new System.Drawing.Point(807, 42);
            this.grpPopulateData.Name = "grpPopulateData";
            this.grpPopulateData.Size = new System.Drawing.Size(203, 30);
            this.grpPopulateData.TabIndex = 1;
            this.grpPopulateData.TabStop = false;
            // 
            // rbnLoadPending
            // 
            this.rbnLoadPending.AutoSize = true;
            this.rbnLoadPending.Location = new System.Drawing.Point(103, 8);
            this.rbnLoadPending.Name = "rbnLoadPending";
            this.rbnLoadPending.Size = new System.Drawing.Size(96, 17);
            this.rbnLoadPending.TabIndex = 1;
            this.rbnLoadPending.TabStop = true;
            this.rbnLoadPending.Text = "Load Pendings";
            this.rbnLoadPending.UseVisualStyleBackColor = true;
            this.rbnLoadPending.CheckedChanged += new System.EventHandler(this.rbnLoadAll_CheckedChanged);
            // 
            // rbnLoadAll
            // 
            this.rbnLoadAll.AutoSize = true;
            this.rbnLoadAll.Location = new System.Drawing.Point(10, 8);
            this.rbnLoadAll.Name = "rbnLoadAll";
            this.rbnLoadAll.Size = new System.Drawing.Size(63, 17);
            this.rbnLoadAll.TabIndex = 0;
            this.rbnLoadAll.TabStop = true;
            this.rbnLoadAll.Text = "Load All";
            this.rbnLoadAll.UseVisualStyleBackColor = true;
            this.rbnLoadAll.CheckedChanged += new System.EventHandler(this.rbnLoadAll_CheckedChanged);
            // 
            // txtInvoiceNoSearch
            // 
            this.txtInvoiceNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceNoSearch.Location = new System.Drawing.Point(144, 79);
            this.txtInvoiceNoSearch.Name = "txtInvoiceNoSearch";
            this.txtInvoiceNoSearch.Size = new System.Drawing.Size(197, 21);
            this.txtInvoiceNoSearch.TabIndex = 2;
            this.txtInvoiceNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(17, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "Invoice No";
            // 
            // TrnSendEmailAgainstInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.grpPopulateData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.label27);
            this.Name = "TrnSendEmailAgainstInvoice";
            this.Load += new System.EventHandler(this.TrnSendEmailAgainstInvoice_Load);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
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
            this.Controls.SetChildIndex(this.grpPopulateData, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSort.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpPartyInfo.ResumeLayout(false);
            this.grpPartyInfo.PerformLayout();
            this.grpInvoiceDetails.ResumeLayout(false);
            this.grpEmailExists.ResumeLayout(false);
            this.grpEmailExists.PerformLayout();
            this.grpViewPdf.ResumeLayout(false);
            this.grpViewPdf.PerformLayout();
            this.pnlEmailTextFormat.ResumeLayout(false);
            this.grpPopulateData.ResumeLayout(false);
            this.grpPopulateData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtPartyNameSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mskInvoiceDateSearch;
        private System.Windows.Forms.RadioButton rdbtnMaxDaysPermit;
        private System.Windows.Forms.RadioButton rdbtnAsEntered;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpPartyInfo;
        private System.Windows.Forms.TextBox txtEmailID;
        private System.Windows.Forms.Panel pnlEmailTextFormat;
        private System.Windows.Forms.Label lblExistingEmailFormatDesc;
        private System.Windows.Forms.ComboBox cmbEmailCategoryDesc;
        private System.Windows.Forms.RichTextBox rtxtEmailBody;
        private System.Windows.Forms.GroupBox grpViewPdf;
        private System.Windows.Forms.TextBox txtPDFPath;
        private System.Windows.Forms.Button btnPreviewBill;
        private System.Windows.Forms.Label lblNewEmailFormatDesc;
        private System.Windows.Forms.TextBox txtEmailCategoryDesc;
        private System.Windows.Forms.WebBrowser wbsViewPdf;
        private System.Windows.Forms.GroupBox grpInvoiceDetails;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label lblPartyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpEmailExists;
        private System.Windows.Forms.RadioButton rbnNewEmailFormat;
        private System.Windows.Forms.RadioButton rbnExistingEmailCategory;
        private System.Windows.Forms.TextBox txtEmailIDBcc;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEmailSendCounter;
        private System.Windows.Forms.Label lblEmailSendCounterCaption;
        private System.Windows.Forms.Label lblLastSendDate;
        private System.Windows.Forms.GroupBox grpPopulateData;
        private System.Windows.Forms.RadioButton rbnLoadPending;
        private System.Windows.Forms.RadioButton rbnLoadAll;
        private System.Windows.Forms.TextBox txtInvoiceNoSearch;
        private System.Windows.Forms.Label label14;
    }
}
