namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstInvoiceSeries
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
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrefixSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnSortPrefixName = new System.Windows.Forms.RadioButton();
            this.rbnSortAsEntered = new System.Windows.Forms.RadioButton();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDisplayText = new System.Windows.Forms.TextBox();
            this.mskLastDate = new System.Windows.Forms.MaskedTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mskLastDateSearch = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStartNoSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLastNoSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
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
            this.grpSort.Location = new System.Drawing.Point(644, 453);
            this.grpSort.Size = new System.Drawing.Size(280, 141);
            this.grpSort.TabIndex = 1;
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.panel1, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(196, 96);
            this.BtnSortCancel.Size = new System.Drawing.Size(69, 23);
            this.BtnSortCancel.TabIndex = 1;
            this.BtnSortCancel.Click += new System.EventHandler(this.BtnSortCancel_Click);
            this.BtnSortCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSortCancel_KeyPress);
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(125, 96);
            this.BtnSortOK.Size = new System.Drawing.Size(69, 23);
            this.BtnSortOK.TabIndex = 0;
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
            this.grpSearch.Controls.Add(this.label12);
            this.grpSearch.Controls.Add(this.txtLastNoSearch);
            this.grpSearch.Controls.Add(this.label11);
            this.grpSearch.Controls.Add(this.txtStartNoSearch);
            this.grpSearch.Controls.Add(this.mskLastDateSearch);
            this.grpSearch.Controls.Add(this.label10);
            this.grpSearch.Controls.Add(this.label6);
            this.grpSearch.Controls.Add(this.label4);
            this.grpSearch.Controls.Add(this.txtPrefixSearch);
            this.grpSearch.Location = new System.Drawing.Point(587, 419);
            this.grpSearch.Size = new System.Drawing.Size(339, 177);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtPrefixSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label4, 0);
            this.grpSearch.Controls.SetChildIndex(this.label6, 0);
            this.grpSearch.Controls.SetChildIndex(this.label10, 0);
            this.grpSearch.Controls.SetChildIndex(this.mskLastDateSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtStartNoSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label11, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtLastNoSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label12, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(254, 134);
            this.BtnSearchCancel.TabIndex = 5;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(183, 134);
            this.BtnSearchOK.TabIndex = 4;
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
            this.grpButton.TabIndex = 1;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.label13);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.mskLastDate);
            this.pnlControls.Controls.Add(this.label20);
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.txtDisplayText);
            this.pnlControls.Controls.Add(this.label7);
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Controls.Add(this.cmbCompanyName);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label27);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.txtPrefix);
            this.pnlControls.Controls.Add(this.txtStartNo);
            this.pnlControls.TabIndex = 0;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 48);
            this.ViewGrid.Size = new System.Drawing.Size(1004, 548);
            this.ViewGrid.TabIndex = 0;
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Start No";
            // 
            // txtStartNo
            // 
            this.txtStartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartNo.Location = new System.Drawing.Point(366, 146);
            this.txtStartNo.MaxLength = 5;
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(54, 21);
            this.txtStartNo.TabIndex = 2;
            this.txtStartNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStartNo.Leave += new System.EventHandler(this.txtStartNo_Leave);
            this.txtStartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartNo_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Prefix";
            // 
            // txtPrefixSearch
            // 
            this.txtPrefixSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrefixSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrefixSearch.Location = new System.Drawing.Point(85, 25);
            this.txtPrefixSearch.Name = "txtPrefixSearch";
            this.txtPrefixSearch.Size = new System.Drawing.Size(209, 21);
            this.txtPrefixSearch.TabIndex = 0;
            this.txtPrefixSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbnSortPrefixName);
            this.panel1.Controls.Add(this.rbnSortAsEntered);
            this.panel1.Location = new System.Drawing.Point(12, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 63);
            this.panel1.TabIndex = 0;
            // 
            // rbnSortPrefixName
            // 
            this.rbnSortPrefixName.Location = new System.Drawing.Point(15, 9);
            this.rbnSortPrefixName.Name = "rbnSortPrefixName";
            this.rbnSortPrefixName.Size = new System.Drawing.Size(164, 18);
            this.rbnSortPrefixName.TabIndex = 0;
            this.rbnSortPrefixName.Text = "Item Name";
            this.rbnSortPrefixName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // rbnSortAsEntered
            // 
            this.rbnSortAsEntered.Location = new System.Drawing.Point(15, 33);
            this.rbnSortAsEntered.Name = "rbnSortAsEntered";
            this.rbnSortAsEntered.Size = new System.Drawing.Size(106, 18);
            this.rbnSortAsEntered.TabIndex = 1;
            this.rbnSortAsEntered.Text = "As Entered";
            this.rbnSortAsEntered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // txtPrefix
            // 
            this.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrefix.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrefix.Location = new System.Drawing.Point(366, 122);
            this.txtPrefix.MaxLength = 20;
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(83, 21);
            this.txtPrefix.TabIndex = 1;
            this.txtPrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(223, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Prefix";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(210, 129);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(10, 14);
            this.label27.TabIndex = 13;
            this.label27.Text = "*";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Company Name";
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(366, 98);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(396, 21);
            this.cmbCompanyName.TabIndex = 0;
            this.cmbCompanyName.Leave += new System.EventHandler(this.ComboBox_Leave);
            this.cmbCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.cmbCompanyName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComboBox_KeyUp);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(211, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "*";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(222, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "Last Date";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(222, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 17);
            this.label9.TabIndex = 24;
            this.label9.Text = "Header Display Text";
            // 
            // txtDisplayText
            // 
            this.txtDisplayText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayText.Location = new System.Drawing.Point(366, 194);
            this.txtDisplayText.MaxLength = 50;
            this.txtDisplayText.Name = "txtDisplayText";
            this.txtDisplayText.Size = new System.Drawing.Size(396, 21);
            this.txtDisplayText.TabIndex = 4;
            this.txtDisplayText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // mskLastDate
            // 
            this.mskLastDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskLastDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskLastDate.Location = new System.Drawing.Point(366, 170);
            this.mskLastDate.Mask = "00/00/0000";
            this.mskLastDate.Name = "mskLastDate";
            this.mskLastDate.Size = new System.Drawing.Size(83, 20);
            this.mskLastDate.TabIndex = 3;
            this.mskLastDate.ValidatingType = typeof(System.DateTime);
            this.mskLastDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(454, 173);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 15);
            this.label20.TabIndex = 206;
            this.label20.Text = "(DD/MM/YYYY)";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(12, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Last Date";
            // 
            // mskLastDateSearch
            // 
            this.mskLastDateSearch.BackColor = System.Drawing.SystemColors.Window;
            this.mskLastDateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskLastDateSearch.Location = new System.Drawing.Point(85, 95);
            this.mskLastDateSearch.Mask = "00/00/0000";
            this.mskLastDateSearch.Name = "mskLastDateSearch";
            this.mskLastDateSearch.Size = new System.Drawing.Size(83, 21);
            this.mskLastDateSearch.TabIndex = 3;
            this.mskLastDateSearch.ValidatingType = typeof(System.DateTime);
            this.mskLastDateSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(169, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 15);
            this.label10.TabIndex = 208;
            this.label10.Text = "(DD/MM/YYYY)";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label11.Location = new System.Drawing.Point(12, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 210;
            this.label11.Text = "Start No";
            // 
            // txtStartNoSearch
            // 
            this.txtStartNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartNoSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartNoSearch.Location = new System.Drawing.Point(85, 48);
            this.txtStartNoSearch.MaxLength = 5;
            this.txtStartNoSearch.Name = "txtStartNoSearch";
            this.txtStartNoSearch.Size = new System.Drawing.Size(49, 21);
            this.txtStartNoSearch.TabIndex = 1;
            this.txtStartNoSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStartNoSearch.Leave += new System.EventHandler(this.txtStartNoSearch_Leave);
            this.txtStartNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartNoSearch_KeyPress);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(12, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 16);
            this.label12.TabIndex = 212;
            this.label12.Text = "Last No";
            // 
            // txtLastNoSearch
            // 
            this.txtLastNoSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastNoSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastNoSearch.Location = new System.Drawing.Point(85, 71);
            this.txtLastNoSearch.MaxLength = 5;
            this.txtLastNoSearch.Name = "txtLastNoSearch";
            this.txtLastNoSearch.Size = new System.Drawing.Size(49, 21);
            this.txtLastNoSearch.TabIndex = 2;
            this.txtLastNoSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastNoSearch.Leave += new System.EventHandler(this.txtLastNoSearch_Leave);
            this.txtLastNoSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLastNoSearch_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(210, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 14);
            this.label3.TabIndex = 207;
            this.label3.Text = "*";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(210, 197);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(10, 14);
            this.label13.TabIndex = 208;
            this.label13.Text = "*";
            // 
            // MstInvoiceSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 669);
            this.Name = "MstInvoiceSeries";
            this.Load += new System.EventHandler(this.MstInvoiceSeries_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrefixSearch;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtStartNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbnSortPrefixName;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.RadioButton rbnSortAsEntered;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtDisplayText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mskLastDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLastNoSearch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStartNoSearch;
        private System.Windows.Forms.MaskedTextBox mskLastDateSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
    }
}
