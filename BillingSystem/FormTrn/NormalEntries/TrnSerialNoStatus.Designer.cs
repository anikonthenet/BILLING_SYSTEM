namespace BillingSystem.FormTrn.NormalEntries
{
    partial class TrnSerialNoStatus
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
            this.cmbItemName = new System.Windows.Forms.ComboBox();
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
            this.txtSearchAll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(158, 469);
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
            this.grpSearch.Location = new System.Drawing.Point(692, 365);
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
            this.pnlControls.Location = new System.Drawing.Point(9, 117);
            this.pnlControls.Size = new System.Drawing.Size(1003, 479);
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 117);
            this.ViewGrid.Size = new System.Drawing.Size(1001, 476);
            // 
            // cmbItemName
            // 
            this.cmbItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.Location = new System.Drawing.Point(272, 49);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(566, 21);
            this.cmbItemName.TabIndex = 140;
            this.cmbItemName.SelectedIndexChanged += new System.EventHandler(this.cmbItemName_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(190, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(82, 18);
            this.label27.TabIndex = 141;
            this.label27.Text = "Item Name";
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
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(998, 1);
            this.label1.TabIndex = 143;
            this.label1.Text = "Company";
            // 
            // txtSearchAll
            // 
            this.txtSearchAll.BackColor = System.Drawing.Color.AliceBlue;
            this.txtSearchAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAll.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAll.Location = new System.Drawing.Point(272, 85);
            this.txtSearchAll.Name = "txtSearchAll";
            this.txtSearchAll.Size = new System.Drawing.Size(566, 21);
            this.txtSearchAll.TabIndex = 145;
            this.txtSearchAll.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(239, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 144;
            this.label2.Text = "&Find";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(15, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(998, 1);
            this.label3.TabIndex = 146;
            this.label3.Text = "Company";
            // 
            // TrnSerialNoStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 672);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSearchAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbItemName);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.chkCancelledEntry);
            this.Name = "TrnSerialNoStatus";
            this.Load += new System.EventHandler(this.BankEntry_Load);
            this.Controls.SetChildIndex(this.chkCancelledEntry, 0);
            this.Controls.SetChildIndex(this.label27, 0);
            this.Controls.SetChildIndex(this.cmbItemName, 0);
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
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtSearchAll, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbItemName;
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
        public System.Windows.Forms.TextBox txtSearchAll;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
