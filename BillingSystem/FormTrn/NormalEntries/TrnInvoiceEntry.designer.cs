namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnInvoiceEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnInvoiceEntry));
            this.panel1 = new System.Windows.Forms.Panel();
            this.flxgrdDetails = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.mskInvoiceDate = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtChallanRefNo = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbParty = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbInvoiceSeries = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmountWithTax = new System.Windows.Forms.TextBox();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.txtDiscountRate = new System.Windows.Forms.TextBox();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.txtAmountWithDiscount = new System.Windows.Forms.TextBox();
            this.txtAdditionalCost = new System.Windows.Forms.TextBox();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAmountWithAdditionalCost = new System.Windows.Forms.TextBox();
            this.txtRoundedOff = new System.Windows.Forms.TextBox();
            this.txtAdditionalCostText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNetAmountInwords = new System.Windows.Forms.TextBox();
            this.flxgrdTax = new AxMSHierarchicalFlexGridLib.AxMSHFlexGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.BtnEditInvoice = new System.Windows.Forms.Button();
            this.txtDiscountText = new System.Windows.Forms.TextBox();
            this.cmbPaymentType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblPartyCategory = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.mskCollectionDate = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSundryPartyFlag = new System.Windows.Forms.Label();
            this.lblAutoCollectionPostFlag = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdTax)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Controls.Add(this.panel1);
            this.grpSort.Location = new System.Drawing.Point(648, 658);
            this.grpSort.Size = new System.Drawing.Size(280, 174);
            this.grpSort.TabIndex = 4;
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.panel1, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(196, 134);
            this.BtnSortCancel.Size = new System.Drawing.Size(69, 23);
            this.BtnSortCancel.TabIndex = 2;
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(125, 134);
            this.BtnSortOK.Size = new System.Drawing.Size(69, 23);
            this.BtnSortOK.TabIndex = 1;
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
            this.grpSearch.Location = new System.Drawing.Point(191, 658);
            this.grpSearch.Size = new System.Drawing.Size(339, 160);
            this.grpSearch.TabIndex = 3;
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(254, 119);
            this.BtnSearchCancel.TabIndex = 4;
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(183, 119);
            this.BtnSearchOK.TabIndex = 3;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.Size = new System.Drawing.Size(173, 24);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // grpButton
            // 
            this.grpButton.TabIndex = 2;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblAutoCollectionPostFlag);
            this.pnlControls.Controls.Add(this.lblSundryPartyFlag);
            this.pnlControls.Controls.Add(this.label14);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Controls.Add(this.label13);
            this.pnlControls.Controls.Add(this.mskCollectionDate);
            this.pnlControls.Controls.Add(this.lblPartyCategory);
            this.pnlControls.Controls.Add(this.txtReference);
            this.pnlControls.Controls.Add(this.cmbBank);
            this.pnlControls.Controls.Add(this.label4);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.cmbPaymentType);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.BtnEditInvoice);
            this.pnlControls.Controls.Add(this.flxgrdTax);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label22);
            this.pnlControls.Controls.Add(this.label30);
            this.pnlControls.Controls.Add(this.label28);
            this.pnlControls.Controls.Add(this.label26);
            this.pnlControls.Controls.Add(this.txtAmountWithDiscount);
            this.pnlControls.Controls.Add(this.txtNetAmountInwords);
            this.pnlControls.Controls.Add(this.txtNetAmount);
            this.pnlControls.Controls.Add(this.txtRoundedOff);
            this.pnlControls.Controls.Add(this.txtDiscountText);
            this.pnlControls.Controls.Add(this.txtAdditionalCostText);
            this.pnlControls.Controls.Add(this.txtAdditionalCost);
            this.pnlControls.Controls.Add(this.txtDiscountAmount);
            this.pnlControls.Controls.Add(this.txtAmountWithAdditionalCost);
            this.pnlControls.Controls.Add(this.txtTaxAmount);
            this.pnlControls.Controls.Add(this.txtAmountWithTax);
            this.pnlControls.Controls.Add(this.txtTotalAmount);
            this.pnlControls.Controls.Add(this.label24);
            this.pnlControls.Controls.Add(this.label23);
            this.pnlControls.Controls.Add(this.label18);
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Controls.Add(this.txtDiscountRate);
            this.pnlControls.Controls.Add(this.txtOrderNo);
            this.pnlControls.Controls.Add(this.txtChallanRefNo);
            this.pnlControls.Controls.Add(this.txtInvoiceNo);
            this.pnlControls.Controls.Add(this.txtRemarks);
            this.pnlControls.Controls.Add(this.label21);
            this.pnlControls.Controls.Add(this.label19);
            this.pnlControls.Controls.Add(this.mskInvoiceDate);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.txtQty);
            this.pnlControls.Controls.Add(this.flxgrdDetails);
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.label10);
            this.pnlControls.Controls.Add(this.label7);
            this.pnlControls.Controls.Add(this.label6);
            this.pnlControls.Controls.Add(this.cmbInvoiceSeries);
            this.pnlControls.Controls.Add(this.label29);
            this.pnlControls.Controls.Add(this.cmbCompany);
            this.pnlControls.Controls.Add(this.label27);
            this.pnlControls.Controls.Add(this.cmbParty);
            this.pnlControls.Controls.Add(this.label25);
            this.pnlControls.Size = new System.Drawing.Size(1004, 554);
            this.pnlControls.TabIndex = 0;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 593);
            this.ViewGrid.Size = new System.Drawing.Size(1004, 8);
            this.ViewGrid.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 105);
            this.panel1.TabIndex = 0;
            // 
            // flxgrdDetails
            // 
            this.flxgrdDetails.DataSource = null;
            this.flxgrdDetails.Location = new System.Drawing.Point(3, 207);
            this.flxgrdDetails.Name = "flxgrdDetails";
            this.flxgrdDetails.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flxgrdDetails.OcxState")));
            this.flxgrdDetails.Size = new System.Drawing.Size(997, 137);
            this.flxgrdDetails.TabIndex = 9;
            this.flxgrdDetails.ClickEvent += new System.EventHandler(this.flxgrdDetails_ClickEvent);
            this.flxgrdDetails.MouseMoveEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEventHandler(this.flxgrdDetails_MouseMoveEvent);
            this.flxgrdDetails.KeyPressEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEventHandler(this.flxgrdDetails_KeyPressEvent);
            this.flxgrdDetails.KeyDownEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEventHandler(this.flxgrdDetails_KeyDownEvent);
            this.flxgrdDetails.DblClick += new System.EventHandler(this.flxgrdDetails_DblClick);
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtQty.Location = new System.Drawing.Point(790, 294);
            this.txtQty.MaxLength = 0;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(113, 21);
            this.txtQty.TabIndex = 8;
            this.txtQty.Visible = false;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // mskInvoiceDate
            // 
            this.mskInvoiceDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskInvoiceDate.Location = new System.Drawing.Point(136, 83);
            this.mskInvoiceDate.Mask = "00/00/0000";
            this.mskInvoiceDate.Name = "mskInvoiceDate";
            this.mskInvoiceDate.Size = new System.Drawing.Size(83, 20);
            this.mskInvoiceDate.TabIndex = 3;
            this.mskInvoiceDate.ValidatingType = typeof(System.DateTime);
            this.mskInvoiceDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 210;
            this.label5.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(136, 177);
            this.txtRemarks.MaxLength = 200;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(333, 21);
            this.txtRemarks.TabIndex = 7;
            this.txtRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemarks_KeyPress);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalAmount.Location = new System.Drawing.Point(562, 352);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmount.TabIndex = 10;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNo.ForeColor = System.Drawing.Color.Blue;
            this.txtInvoiceNo.Location = new System.Drawing.Point(136, 61);
            this.txtInvoiceNo.MaxLength = 200;
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(171, 21);
            this.txtInvoiceNo.TabIndex = 2;
            this.txtInvoiceNo.TabStop = false;
            this.txtInvoiceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemarks_KeyPress);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(20, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(112, 17);
            this.label18.TabIndex = 210;
            this.label18.Text = "Invoice No.";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(222, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 15);
            this.label19.TabIndex = 204;
            this.label19.Text = "(DD/MM/YYYY)";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(20, 83);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(112, 18);
            this.label21.TabIndex = 205;
            this.label21.Text = "Invoice Date";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(9, 83);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(10, 14);
            this.label22.TabIndex = 246;
            this.label22.Text = "*";
            // 
            // txtChallanRefNo
            // 
            this.txtChallanRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChallanRefNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChallanRefNo.Location = new System.Drawing.Point(136, 106);
            this.txtChallanRefNo.MaxLength = 30;
            this.txtChallanRefNo.Name = "txtChallanRefNo";
            this.txtChallanRefNo.Size = new System.Drawing.Size(171, 21);
            this.txtChallanRefNo.TabIndex = 4;
            this.txtChallanRefNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(20, 108);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 17);
            this.label23.TabIndex = 210;
            this.label23.Text = "Challan Ref. No.";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(136, 129);
            this.txtOrderNo.MaxLength = 30;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(171, 21);
            this.txtOrderNo.TabIndex = 5;
            this.txtOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(20, 131);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(112, 17);
            this.label24.TabIndex = 210;
            this.label24.Text = "Order No.";
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Location = new System.Drawing.Point(20, 155);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(112, 18);
            this.label25.TabIndex = 139;
            this.label25.Text = "Party";
            // 
            // cmbParty
            // 
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.Location = new System.Drawing.Point(136, 153);
            this.cmbParty.Name = "cmbParty";
            this.cmbParty.Size = new System.Drawing.Size(333, 21);
            this.cmbParty.TabIndex = 6;
            this.cmbParty.SelectedIndexChanged += new System.EventHandler(this.cmbParty_SelectedIndexChanged);
            this.cmbParty.Leave += new System.EventHandler(this.cmbParty_Leave);
            this.cmbParty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.cmbParty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbParty_KeyUp);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(9, 155);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(10, 14);
            this.label26.TabIndex = 246;
            this.label26.Text = "*";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(20, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(112, 18);
            this.label27.TabIndex = 139;
            this.label27.Text = "Company";
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(136, 13);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(333, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            this.cmbCompany.Leave += new System.EventHandler(this.cmbCompany_Leave);
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            this.cmbCompany.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbCompany_KeyUp);
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(9, 15);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(10, 14);
            this.label28.TabIndex = 246;
            this.label28.Text = "*";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.Location = new System.Drawing.Point(20, 38);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(112, 18);
            this.label29.TabIndex = 139;
            this.label29.Text = "Invoice Series";
            // 
            // cmbInvoiceSeries
            // 
            this.cmbInvoiceSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceSeries.FormattingEnabled = true;
            this.cmbInvoiceSeries.Location = new System.Drawing.Point(136, 36);
            this.cmbInvoiceSeries.Name = "cmbInvoiceSeries";
            this.cmbInvoiceSeries.Size = new System.Drawing.Size(171, 21);
            this.cmbInvoiceSeries.TabIndex = 1;
            this.cmbInvoiceSeries.SelectedIndexChanged += new System.EventHandler(this.cmbInvoiceSeries_SelectedIndexChanged);
            this.cmbInvoiceSeries.Leave += new System.EventHandler(this.cmbInvoiceSeries_Leave);
            this.cmbInvoiceSeries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.cmbInvoiceSeries.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbInvoiceSeries_KeyUp);
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(9, 38);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(10, 14);
            this.label30.TabIndex = 246;
            this.label30.Text = "*";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(9, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 14);
            this.label2.TabIndex = 246;
            this.label2.Text = "*";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(412, 419);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 18);
            this.label6.TabIndex = 139;
            this.label6.Text = "TOTAL TAX AMOUNT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountWithTax
            // 
            this.txtAmountWithTax.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmountWithTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountWithTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountWithTax.ForeColor = System.Drawing.Color.Blue;
            this.txtAmountWithTax.Location = new System.Drawing.Point(562, 440);
            this.txtAmountWithTax.Name = "txtAmountWithTax";
            this.txtAmountWithTax.ReadOnly = true;
            this.txtAmountWithTax.Size = new System.Drawing.Size(100, 20);
            this.txtAmountWithTax.TabIndex = 17;
            this.txtAmountWithTax.TabStop = false;
            this.txtAmountWithTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAmount.ForeColor = System.Drawing.Color.Black;
            this.txtTaxAmount.Location = new System.Drawing.Point(562, 418);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTaxAmount.TabIndex = 16;
            this.txtTaxAmount.TabStop = false;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDiscountRate
            // 
            this.txtDiscountRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscountRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtDiscountRate.Location = new System.Drawing.Point(507, 374);
            this.txtDiscountRate.MaxLength = 200;
            this.txtDiscountRate.Name = "txtDiscountRate";
            this.txtDiscountRate.Size = new System.Drawing.Size(53, 20);
            this.txtDiscountRate.TabIndex = 12;
            this.txtDiscountRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscountRate.TextChanged += new System.EventHandler(this.txtDiscountRate_TextChanged);
            this.txtDiscountRate.Leave += new System.EventHandler(this.txtDiscountRate_Leave);
            this.txtDiscountRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountRate_KeyPress);
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiscountAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.ForeColor = System.Drawing.Color.Black;
            this.txtDiscountAmount.Location = new System.Drawing.Point(562, 374);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(100, 20);
            this.txtDiscountAmount.TabIndex = 13;
            this.txtDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscountAmount.TextChanged += new System.EventHandler(this.txtDiscountAmount_TextChanged);
            this.txtDiscountAmount.Leave += new System.EventHandler(this.txtDiscountAmount_Leave);
            this.txtDiscountAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountAmount_KeyPress);
            // 
            // txtAmountWithDiscount
            // 
            this.txtAmountWithDiscount.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmountWithDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountWithDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountWithDiscount.ForeColor = System.Drawing.Color.Blue;
            this.txtAmountWithDiscount.Location = new System.Drawing.Point(562, 396);
            this.txtAmountWithDiscount.Name = "txtAmountWithDiscount";
            this.txtAmountWithDiscount.ReadOnly = true;
            this.txtAmountWithDiscount.Size = new System.Drawing.Size(100, 20);
            this.txtAmountWithDiscount.TabIndex = 14;
            this.txtAmountWithDiscount.TabStop = false;
            this.txtAmountWithDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAdditionalCost
            // 
            this.txtAdditionalCost.BackColor = System.Drawing.SystemColors.Window;
            this.txtAdditionalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdditionalCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalCost.ForeColor = System.Drawing.Color.Black;
            this.txtAdditionalCost.Location = new System.Drawing.Point(562, 462);
            this.txtAdditionalCost.Name = "txtAdditionalCost";
            this.txtAdditionalCost.Size = new System.Drawing.Size(100, 20);
            this.txtAdditionalCost.TabIndex = 19;
            this.txtAdditionalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdditionalCost.TextChanged += new System.EventHandler(this.txtAdditionalCost_TextChanged);
            this.txtAdditionalCost.Leave += new System.EventHandler(this.txtAdditionalCost_Leave);
            this.txtAdditionalCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdditionalCost_KeyPress);
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BackColor = System.Drawing.Color.Black;
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtNetAmount.ForeColor = System.Drawing.Color.Lime;
            this.txtNetAmount.Location = new System.Drawing.Point(562, 528);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.ReadOnly = true;
            this.txtNetAmount.Size = new System.Drawing.Size(100, 20);
            this.txtNetAmount.TabIndex = 22;
            this.txtNetAmount.TabStop = false;
            this.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(396, 506);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 18);
            this.label8.TabIndex = 139;
            this.label8.Text = "Rounded Off (+/-)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountWithAdditionalCost
            // 
            this.txtAmountWithAdditionalCost.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmountWithAdditionalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountWithAdditionalCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountWithAdditionalCost.ForeColor = System.Drawing.Color.Blue;
            this.txtAmountWithAdditionalCost.Location = new System.Drawing.Point(562, 484);
            this.txtAmountWithAdditionalCost.Name = "txtAmountWithAdditionalCost";
            this.txtAmountWithAdditionalCost.ReadOnly = true;
            this.txtAmountWithAdditionalCost.Size = new System.Drawing.Size(100, 20);
            this.txtAmountWithAdditionalCost.TabIndex = 20;
            this.txtAmountWithAdditionalCost.TabStop = false;
            this.txtAmountWithAdditionalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRoundedOff
            // 
            this.txtRoundedOff.BackColor = System.Drawing.SystemColors.Window;
            this.txtRoundedOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoundedOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoundedOff.ForeColor = System.Drawing.Color.Black;
            this.txtRoundedOff.Location = new System.Drawing.Point(562, 506);
            this.txtRoundedOff.Name = "txtRoundedOff";
            this.txtRoundedOff.Size = new System.Drawing.Size(100, 20);
            this.txtRoundedOff.TabIndex = 21;
            this.txtRoundedOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRoundedOff.TextChanged += new System.EventHandler(this.txtRoundedOff_TextChanged);
            this.txtRoundedOff.Leave += new System.EventHandler(this.txtRoundedOff_Leave);
            this.txtRoundedOff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoundedOff_KeyPress);
            // 
            // txtAdditionalCostText
            // 
            this.txtAdditionalCostText.BackColor = System.Drawing.SystemColors.Window;
            this.txtAdditionalCostText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdditionalCostText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalCostText.ForeColor = System.Drawing.Color.Black;
            this.txtAdditionalCostText.Location = new System.Drawing.Point(300, 462);
            this.txtAdditionalCostText.MaxLength = 100;
            this.txtAdditionalCostText.Name = "txtAdditionalCostText";
            this.txtAdditionalCostText.Size = new System.Drawing.Size(260, 21);
            this.txtAdditionalCostText.TabIndex = 18;
            this.txtAdditionalCostText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(446, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 18);
            this.label7.TabIndex = 139;
            this.label7.Text = "TOTAL AMOUNT";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(446, 527);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 18);
            this.label9.TabIndex = 139;
            this.label9.Text = "NET AMOUNT";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNetAmountInwords
            // 
            this.txtNetAmountInwords.BackColor = System.Drawing.Color.Black;
            this.txtNetAmountInwords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmountInwords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtNetAmountInwords.ForeColor = System.Drawing.Color.Cyan;
            this.txtNetAmountInwords.Location = new System.Drawing.Point(664, 528);
            this.txtNetAmountInwords.Name = "txtNetAmountInwords";
            this.txtNetAmountInwords.ReadOnly = true;
            this.txtNetAmountInwords.Size = new System.Drawing.Size(336, 20);
            this.txtNetAmountInwords.TabIndex = 23;
            this.txtNetAmountInwords.TabStop = false;
            // 
            // flxgrdTax
            // 
            this.flxgrdTax.DataSource = null;
            this.flxgrdTax.Location = new System.Drawing.Point(3, 364);
            this.flxgrdTax.Name = "flxgrdTax";
            this.flxgrdTax.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flxgrdTax.OcxState")));
            this.flxgrdTax.Size = new System.Drawing.Size(377, 91);
            this.flxgrdTax.TabIndex = 15;
            this.flxgrdTax.MouseMoveEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEventHandler(this.flxgrdTax_MouseMoveEvent);
            this.flxgrdTax.KeyPressEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEventHandler(this.flxgrdTax_KeyPressEvent);
            this.flxgrdTax.KeyDownEvent += new AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEventHandler(this.flxgrdTax_KeyDownEvent);
            this.flxgrdTax.DblClick += new System.EventHandler(this.flxgrdTax_DblClick);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(7, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 18);
            this.label10.TabIndex = 139;
            this.label10.Text = "Tax Details";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnEditInvoice
            // 
            this.BtnEditInvoice.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnEditInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEditInvoice.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEditInvoice.ForeColor = System.Drawing.Color.Black;
            this.BtnEditInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnEditInvoice.Location = new System.Drawing.Point(613, 12);
            this.BtnEditInvoice.Name = "BtnEditInvoice";
            this.BtnEditInvoice.Size = new System.Drawing.Size(165, 24);
            this.BtnEditInvoice.TabIndex = 0;
            this.BtnEditInvoice.Text = "&View Invoice";
            this.BtnEditInvoice.UseVisualStyleBackColor = false;
            this.BtnEditInvoice.Click += new System.EventHandler(this.BtnEditInvoice_Click);
            // 
            // txtDiscountText
            // 
            this.txtDiscountText.BackColor = System.Drawing.SystemColors.Window;
            this.txtDiscountText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountText.ForeColor = System.Drawing.Color.Black;
            this.txtDiscountText.Location = new System.Drawing.Point(384, 374);
            this.txtDiscountText.MaxLength = 100;
            this.txtDiscountText.Name = "txtDiscountText";
            this.txtDiscountText.Size = new System.Drawing.Size(121, 20);
            this.txtDiscountText.TabIndex = 11;
            this.txtDiscountText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Location = new System.Drawing.Point(613, 61);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(171, 21);
            this.cmbPaymentType.TabIndex = 247;
            this.cmbPaymentType.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(497, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 248;
            this.label1.Text = "Payment Type";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(497, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 250;
            this.label3.Text = "Reference No.";
            // 
            // cmbBank
            // 
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(613, 110);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(171, 21);
            this.cmbBank.TabIndex = 251;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(497, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 18);
            this.label4.TabIndex = 252;
            this.label4.Text = "Bank";
            // 
            // txtReference
            // 
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.Location = new System.Drawing.Point(613, 86);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(171, 21);
            this.txtReference.TabIndex = 253;
            // 
            // lblPartyCategory
            // 
            this.lblPartyCategory.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartyCategory.ForeColor = System.Drawing.Color.Blue;
            this.lblPartyCategory.Location = new System.Drawing.Point(472, 156);
            this.lblPartyCategory.Name = "lblPartyCategory";
            this.lblPartyCategory.Size = new System.Drawing.Size(190, 17);
            this.lblPartyCategory.TabIndex = 254;
            this.lblPartyCategory.Text = "lblPartyCategory";
            this.lblPartyCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(497, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 18);
            this.label12.TabIndex = 257;
            this.label12.Text = "Payment Date";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(699, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 15);
            this.label13.TabIndex = 256;
            this.label13.Text = "(DD/MM/YYYY)";
            // 
            // mskCollectionDate
            // 
            this.mskCollectionDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskCollectionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskCollectionDate.Location = new System.Drawing.Point(613, 133);
            this.mskCollectionDate.Mask = "00/00/0000";
            this.mskCollectionDate.Name = "mskCollectionDate";
            this.mskCollectionDate.Size = new System.Drawing.Size(83, 20);
            this.mskCollectionDate.TabIndex = 255;
            this.mskCollectionDate.ValidatingType = typeof(System.DateTime);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(486, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(10, 14);
            this.label14.TabIndex = 259;
            this.label14.Text = "*";
            // 
            // lblSundryPartyFlag
            // 
            this.lblSundryPartyFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSundryPartyFlag.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSundryPartyFlag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSundryPartyFlag.Location = new System.Drawing.Point(790, 64);
            this.lblSundryPartyFlag.Name = "lblSundryPartyFlag";
            this.lblSundryPartyFlag.Size = new System.Drawing.Size(10, 18);
            this.lblSundryPartyFlag.TabIndex = 260;
            this.lblSundryPartyFlag.Visible = false;
            // 
            // lblAutoCollectionPostFlag
            // 
            this.lblAutoCollectionPostFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAutoCollectionPostFlag.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoCollectionPostFlag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoCollectionPostFlag.Location = new System.Drawing.Point(806, 64);
            this.lblAutoCollectionPostFlag.Name = "lblAutoCollectionPostFlag";
            this.lblAutoCollectionPostFlag.Size = new System.Drawing.Size(10, 18);
            this.lblAutoCollectionPostFlag.TabIndex = 261;
            this.lblAutoCollectionPostFlag.Visible = false;
            // 
            // TrnInvoiceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1019, 669);
            this.Name = "TrnInvoiceEntry";
            this.Load += new System.EventHandler(this.TrnInvoiceEntry_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flxgrdTax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private AxMSHierarchicalFlexGridLib.AxMSHFlexGrid flxgrdDetails;
        internal System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.MaskedTextBox mskInvoiceDate;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox txtChallanRefNo;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbParty;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cmbInvoiceSeries;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.TextBox txtAmountWithTax;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        public System.Windows.Forms.TextBox txtDiscountRate;
        private System.Windows.Forms.TextBox txtAmountWithDiscount;
        private System.Windows.Forms.TextBox txtAdditionalCost;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.TextBox txtRoundedOff;
        private System.Windows.Forms.TextBox txtAmountWithAdditionalCost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAdditionalCostText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNetAmountInwords;
        private AxMSHierarchicalFlexGridLib.AxMSHFlexGrid flxgrdTax;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button BtnEditInvoice;
        private System.Windows.Forms.TextBox txtDiscountText;
        public System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPaymentType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPartyCategory;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox mskCollectionDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSundryPartyFlag;
        private System.Windows.Forms.Label lblAutoCollectionPostFlag;
    }
}
