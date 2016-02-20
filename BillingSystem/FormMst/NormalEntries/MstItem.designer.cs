namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstItem
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtItemNameSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnSortItemName = new System.Windows.Forms.RadioButton();
            this.rbnSortAsEntered = new System.Windows.Forms.RadioButton();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbActiveInactive = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRate2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSalePercent = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNonSalePercent = new System.Windows.Forms.TextBox();
            this.cmbDispatchEmailByProductName = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtReOrderLevel = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDownloadLink1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDownloadLink2 = new System.Windows.Forms.TextBox();
            this.chkDefaultItem = new System.Windows.Forms.CheckBox();
            this.chkAvailableItem = new System.Windows.Forms.CheckBox();
            this.cmbEmailCategory = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPDFDocumentName = new System.Windows.Forms.TextBox();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Controls.Add(this.panel1);
            this.grpSort.Location = new System.Drawing.Point(732, 647);
            this.grpSort.Size = new System.Drawing.Size(280, 141);
            this.grpSort.TabIndex = 4;
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.panel1, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(196, 96);
            this.BtnSortCancel.Size = new System.Drawing.Size(69, 23);
            this.BtnSortCancel.TabIndex = 2;
            this.BtnSortCancel.Click += new System.EventHandler(this.BtnSortCancel_Click);
            this.BtnSortCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSortCancel_KeyPress);
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(125, 96);
            this.BtnSortOK.Size = new System.Drawing.Size(69, 23);
            this.BtnSortOK.TabIndex = 1;
            this.BtnSortOK.Click += new System.EventHandler(this.BtnSortOK_Click);
            this.BtnSortOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSortOK_KeyPress);
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
            this.grpSearch.Controls.Add(this.label4);
            this.grpSearch.Controls.Add(this.txtItemNameSearch);
            this.grpSearch.Location = new System.Drawing.Point(673, 652);
            this.grpSearch.Size = new System.Drawing.Size(339, 134);
            this.grpSearch.TabIndex = 3;
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtItemNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label4, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(254, 89);
            this.BtnSearchCancel.TabIndex = 4;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(183, 89);
            this.BtnSearchOK.TabIndex = 3;
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
            // BtnSort
            // 
            this.BtnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.chkActive);
            this.grpButton.TabIndex = 1;
            this.grpButton.Controls.SetChildIndex(this.chkActive, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnAdd, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnEdit, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSave, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnCancel, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSort, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnSearch, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnDelete, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnRefresh, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnPrint, 0);
            this.grpButton.Controls.SetChildIndex(this.BtnExit, 0);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.label18);
            this.pnlControls.Controls.Add(this.txtPDFDocumentName);
            this.pnlControls.Controls.Add(this.cmbEmailCategory);
            this.pnlControls.Controls.Add(this.label17);
            this.pnlControls.Controls.Add(this.chkAvailableItem);
            this.pnlControls.Controls.Add(this.chkDefaultItem);
            this.pnlControls.Controls.Add(this.label16);
            this.pnlControls.Controls.Add(this.txtDownloadLink2);
            this.pnlControls.Controls.Add(this.label15);
            this.pnlControls.Controls.Add(this.txtDownloadLink1);
            this.pnlControls.Controls.Add(this.txtReOrderLevel);
            this.pnlControls.Controls.Add(this.label14);
            this.pnlControls.Controls.Add(this.cmbDispatchEmailByProductName);
            this.pnlControls.Controls.Add(this.label13);
            this.pnlControls.Controls.Add(this.label12);
            this.pnlControls.Controls.Add(this.txtNonSalePercent);
            this.pnlControls.Controls.Add(this.label11);
            this.pnlControls.Controls.Add(this.txtSalePercent);
            this.pnlControls.Controls.Add(this.label10);
            this.pnlControls.Controls.Add(this.txtRate2);
            this.pnlControls.Controls.Add(this.cmbActiveInactive);
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.label7);
            this.pnlControls.Controls.Add(this.label6);
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.cmbCompanyName);
            this.pnlControls.Controls.Add(this.txtUnit);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label27);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.txtItemName);
            this.pnlControls.Controls.Add(this.txtRate);
            this.pnlControls.TabIndex = 0;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 48);
            this.ViewGrid.Size = new System.Drawing.Size(1004, 12);
            this.ViewGrid.TabIndex = 0;
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(217, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Rate";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRate
            // 
            this.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRate.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Location = new System.Drawing.Point(395, 146);
            this.txtRate.MaxLength = 12;
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(73, 21);
            this.txtRate.TabIndex = 2;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.Leave += new System.EventHandler(this.txtRate_Leave);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Item Name";
            // 
            // txtItemNameSearch
            // 
            this.txtItemNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemNameSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemNameSearch.Location = new System.Drawing.Point(115, 40);
            this.txtItemNameSearch.Name = "txtItemNameSearch";
            this.txtItemNameSearch.Size = new System.Drawing.Size(209, 21);
            this.txtItemNameSearch.TabIndex = 0;
            this.txtItemNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbnSortItemName);
            this.panel1.Controls.Add(this.rbnSortAsEntered);
            this.panel1.Location = new System.Drawing.Point(12, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 63);
            this.panel1.TabIndex = 0;
            // 
            // rbnSortItemName
            // 
            this.rbnSortItemName.Location = new System.Drawing.Point(15, 9);
            this.rbnSortItemName.Name = "rbnSortItemName";
            this.rbnSortItemName.Size = new System.Drawing.Size(164, 18);
            this.rbnSortItemName.TabIndex = 0;
            this.rbnSortItemName.Text = "Item Name";
            this.rbnSortItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // rbnSortAsEntered
            // 
            this.rbnSortAsEntered.Location = new System.Drawing.Point(15, 33);
            this.rbnSortAsEntered.Name = "rbnSortAsEntered";
            this.rbnSortAsEntered.Size = new System.Drawing.Size(106, 18);
            this.rbnSortAsEntered.TabIndex = 3;
            this.rbnSortAsEntered.Text = "As Entered";
            this.rbnSortAsEntered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // txtItemName
            // 
            this.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(395, 124);
            this.txtItemName.MaxLength = 50;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(393, 21);
            this.txtItemName.TabIndex = 1;
            this.txtItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(217, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Item Name";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(380, 127);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(10, 14);
            this.label27.TabIndex = 13;
            this.label27.Text = "*";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Company Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtUnit
            // 
            this.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnit.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnit.Location = new System.Drawing.Point(395, 168);
            this.txtUnit.MaxLength = 20;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(73, 21);
            this.txtUnit.TabIndex = 3;
            this.txtUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(395, 101);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(393, 21);
            this.cmbCompanyName.TabIndex = 0;
            this.cmbCompanyName.Leave += new System.EventHandler(this.ComboBox_Leave);
            this.cmbCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.cmbCompanyName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComboBox_KeyUp);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(216, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Unit";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(381, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(380, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 14);
            this.label6.TabIndex = 20;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(381, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 14);
            this.label7.TabIndex = 20;
            this.label7.Text = "*";
            // 
            // cmbActiveInactive
            // 
            this.cmbActiveInactive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActiveInactive.FormattingEnabled = true;
            this.cmbActiveInactive.Location = new System.Drawing.Point(394, 190);
            this.cmbActiveInactive.Name = "cmbActiveInactive";
            this.cmbActiveInactive.Size = new System.Drawing.Size(124, 21);
            this.cmbActiveInactive.TabIndex = 4;
            this.cmbActiveInactive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(216, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Status";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRate2
            // 
            this.txtRate2.BackColor = System.Drawing.SystemColors.Window;
            this.txtRate2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate2.ForeColor = System.Drawing.Color.Black;
            this.txtRate2.Location = new System.Drawing.Point(394, 212);
            this.txtRate2.Name = "txtRate2";
            this.txtRate2.Size = new System.Drawing.Size(74, 20);
            this.txtRate2.TabIndex = 5;
            this.txtRate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate2.Leave += new System.EventHandler(this.txtRate2_Leave);
            this.txtRate2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate2_KeyPress);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(217, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Rate 2";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(217, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(161, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "Sale Percent";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSalePercent
            // 
            this.txtSalePercent.BackColor = System.Drawing.SystemColors.Window;
            this.txtSalePercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalePercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalePercent.ForeColor = System.Drawing.Color.Black;
            this.txtSalePercent.Location = new System.Drawing.Point(394, 233);
            this.txtSalePercent.Name = "txtSalePercent";
            this.txtSalePercent.Size = new System.Drawing.Size(74, 20);
            this.txtSalePercent.TabIndex = 6;
            this.txtSalePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSalePercent.Leave += new System.EventHandler(this.txtSalePercent_Leave);
            this.txtSalePercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSalePercent_KeyPress);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(217, 257);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(161, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "Nonsale Percent";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNonSalePercent
            // 
            this.txtNonSalePercent.BackColor = System.Drawing.SystemColors.Window;
            this.txtNonSalePercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNonSalePercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNonSalePercent.ForeColor = System.Drawing.Color.Black;
            this.txtNonSalePercent.Location = new System.Drawing.Point(394, 254);
            this.txtNonSalePercent.Name = "txtNonSalePercent";
            this.txtNonSalePercent.Size = new System.Drawing.Size(74, 20);
            this.txtNonSalePercent.TabIndex = 7;
            this.txtNonSalePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNonSalePercent.Leave += new System.EventHandler(this.txtNonSalePercent_Leave);
            this.txtNonSalePercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNonSalePercent_KeyPress);
            // 
            // cmbDispatchEmailByProductName
            // 
            this.cmbDispatchEmailByProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDispatchEmailByProductName.FormattingEnabled = true;
            this.cmbDispatchEmailByProductName.Location = new System.Drawing.Point(395, 415);
            this.cmbDispatchEmailByProductName.Name = "cmbDispatchEmailByProductName";
            this.cmbDispatchEmailByProductName.Size = new System.Drawing.Size(124, 21);
            this.cmbDispatchEmailByProductName.TabIndex = 14;
            this.cmbDispatchEmailByProductName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(218, 418);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(161, 17);
            this.label13.TabIndex = 30;
            this.label13.Text = "Despatch Email By Item";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(217, 277);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(161, 17);
            this.label14.TabIndex = 31;
            this.label14.Text = "Reorder Level";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtReOrderLevel
            // 
            this.txtReOrderLevel.BackColor = System.Drawing.SystemColors.Window;
            this.txtReOrderLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReOrderLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReOrderLevel.ForeColor = System.Drawing.Color.Black;
            this.txtReOrderLevel.Location = new System.Drawing.Point(394, 276);
            this.txtReOrderLevel.Name = "txtReOrderLevel";
            this.txtReOrderLevel.Size = new System.Drawing.Size(74, 20);
            this.txtReOrderLevel.TabIndex = 8;
            this.txtReOrderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReOrderLevel.Leave += new System.EventHandler(this.txtReOrderLevel_Leave);
            this.txtReOrderLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReOrderLevel_KeyPress);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Location = new System.Drawing.Point(924, 19);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(62, 17);
            this.chkActive.TabIndex = 10;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(217, 350);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "Download Link 1";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDownloadLink1
            // 
            this.txtDownloadLink1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDownloadLink1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownloadLink1.Location = new System.Drawing.Point(395, 346);
            this.txtDownloadLink1.MaxLength = 200;
            this.txtDownloadLink1.Name = "txtDownloadLink1";
            this.txtDownloadLink1.Size = new System.Drawing.Size(586, 21);
            this.txtDownloadLink1.TabIndex = 11;
            this.txtDownloadLink1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(217, 373);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(161, 17);
            this.label16.TabIndex = 35;
            this.label16.Text = "Download Link 2";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDownloadLink2
            // 
            this.txtDownloadLink2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDownloadLink2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownloadLink2.Location = new System.Drawing.Point(395, 369);
            this.txtDownloadLink2.MaxLength = 200;
            this.txtDownloadLink2.Name = "txtDownloadLink2";
            this.txtDownloadLink2.Size = new System.Drawing.Size(586, 21);
            this.txtDownloadLink2.TabIndex = 12;
            this.txtDownloadLink2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // chkDefaultItem
            // 
            this.chkDefaultItem.AutoSize = true;
            this.chkDefaultItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDefaultItem.Location = new System.Drawing.Point(395, 324);
            this.chkDefaultItem.Name = "chkDefaultItem";
            this.chkDefaultItem.Size = new System.Drawing.Size(196, 17);
            this.chkDefaultItem.TabIndex = 10;
            this.chkDefaultItem.Text = "Default Item for Online/Offline Billing";
            this.chkDefaultItem.UseVisualStyleBackColor = true;
            // 
            // chkAvailableItem
            // 
            this.chkAvailableItem.AutoSize = true;
            this.chkAvailableItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAvailableItem.Location = new System.Drawing.Point(395, 300);
            this.chkAvailableItem.Name = "chkAvailableItem";
            this.chkAvailableItem.Size = new System.Drawing.Size(204, 17);
            this.chkAvailableItem.TabIndex = 9;
            this.chkAvailableItem.Text = "Item available for Online/Offline Billing";
            this.chkAvailableItem.UseVisualStyleBackColor = true;
            // 
            // cmbEmailCategory
            // 
            this.cmbEmailCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmailCategory.FormattingEnabled = true;
            this.cmbEmailCategory.Location = new System.Drawing.Point(395, 392);
            this.cmbEmailCategory.Name = "cmbEmailCategory";
            this.cmbEmailCategory.Size = new System.Drawing.Size(393, 21);
            this.cmbEmailCategory.TabIndex = 13;
            this.cmbEmailCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(218, 395);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(161, 17);
            this.label17.TabIndex = 39;
            this.label17.Text = "Email Category";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(5, 442);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(373, 15);
            this.label18.TabIndex = 41;
            this.label18.Text = "Full web path of PDF file to be attached with email";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPDFDocumentName
            // 
            this.txtPDFDocumentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPDFDocumentName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPDFDocumentName.Location = new System.Drawing.Point(395, 438);
            this.txtPDFDocumentName.MaxLength = 100;
            this.txtPDFDocumentName.Name = "txtPDFDocumentName";
            this.txtPDFDocumentName.Size = new System.Drawing.Size(393, 21);
            this.txtPDFDocumentName.TabIndex = 40;
            this.txtPDFDocumentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // MstItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 669);
            this.Name = "MstItem";
            this.Load += new System.EventHandler(this.MstItem_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.grpButton.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtItemNameSearch;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbnSortItemName;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.RadioButton rbnSortAsEntered;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        public System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbActiveInactive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNonSalePercent;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSalePercent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRate2;
        private System.Windows.Forms.ComboBox cmbDispatchEmailByProductName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtReOrderLevel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtDownloadLink2;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox txtDownloadLink1;
        private System.Windows.Forms.CheckBox chkDefaultItem;
        private System.Windows.Forms.CheckBox chkAvailableItem;
        private System.Windows.Forms.ComboBox cmbEmailCategory;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtPDFDocumentName;
    }
}
