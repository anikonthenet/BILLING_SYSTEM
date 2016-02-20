namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstTax
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
            this.txtTaxPercentage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTaxDescSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnTaxPercentage = new System.Windows.Forms.RadioButton();
            this.rbnTaxDesc = new System.Windows.Forms.RadioButton();
            this.rbnSortAsEntered = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaxDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.grpSort.Location = new System.Drawing.Point(668, 459);
            this.grpSort.Size = new System.Drawing.Size(260, 139);
            this.grpSort.TabIndex = 4;
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.panel1, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(183, 107);
            this.BtnSortCancel.Size = new System.Drawing.Size(69, 23);
            this.BtnSortCancel.TabIndex = 2;
            this.BtnSortCancel.Click += new System.EventHandler(this.BtnSortCancel_Click);
            this.BtnSortCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSortCancel_KeyPress);
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(112, 107);
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
            this.grpSearch.Controls.Add(this.txtTaxDescSearch);
            this.grpSearch.Location = new System.Drawing.Point(589, 493);
            this.grpSearch.Size = new System.Drawing.Size(339, 100);
            this.grpSearch.TabIndex = 3;
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtTaxDescSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.label4, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.Location = new System.Drawing.Point(244, 61);
            this.BtnSearchCancel.TabIndex = 4;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.Location = new System.Drawing.Point(173, 61);
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
            this.grpButton.TabIndex = 1;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.label9);
            this.pnlControls.Controls.Add(this.label27);
            this.pnlControls.Controls.Add(this.label3);
            this.pnlControls.Controls.Add(this.label8);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.txtTaxDesc);
            this.pnlControls.Controls.Add(this.txtTaxPercentage);
            this.pnlControls.TabIndex = 2;
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
            this.label1.Location = new System.Drawing.Point(243, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tax Percentage";
            // 
            // txtTaxPercentage
            // 
            this.txtTaxPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxPercentage.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxPercentage.Location = new System.Drawing.Point(367, 181);
            this.txtTaxPercentage.MaxLength = 7;
            this.txtTaxPercentage.Name = "txtTaxPercentage";
            this.txtTaxPercentage.Size = new System.Drawing.Size(71, 21);
            this.txtTaxPercentage.TabIndex = 1;
            this.txtTaxPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxPercentage.Leave += new System.EventHandler(this.txtTaxPercentage_Leave);
            this.txtTaxPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxPercentage_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(18, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Tax Description";
            // 
            // txtTaxDescSearch
            // 
            this.txtTaxDescSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxDescSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxDescSearch.Location = new System.Drawing.Point(138, 26);
            this.txtTaxDescSearch.Name = "txtTaxDescSearch";
            this.txtTaxDescSearch.Size = new System.Drawing.Size(182, 21);
            this.txtTaxDescSearch.TabIndex = 0;
            this.txtTaxDescSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Searching_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbnTaxPercentage);
            this.panel1.Controls.Add(this.rbnTaxDesc);
            this.panel1.Controls.Add(this.rbnSortAsEntered);
            this.panel1.Location = new System.Drawing.Point(12, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 82);
            this.panel1.TabIndex = 0;
            // 
            // rbnTaxPercentage
            // 
            this.rbnTaxPercentage.Location = new System.Drawing.Point(15, 32);
            this.rbnTaxPercentage.Name = "rbnTaxPercentage";
            this.rbnTaxPercentage.Size = new System.Drawing.Size(164, 18);
            this.rbnTaxPercentage.TabIndex = 1;
            this.rbnTaxPercentage.Text = "Tax Percentage";
            this.rbnTaxPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // rbnTaxDesc
            // 
            this.rbnTaxDesc.Location = new System.Drawing.Point(15, 8);
            this.rbnTaxDesc.Name = "rbnTaxDesc";
            this.rbnTaxDesc.Size = new System.Drawing.Size(143, 18);
            this.rbnTaxDesc.TabIndex = 0;
            this.rbnTaxDesc.Text = "Tax Description";
            this.rbnTaxDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // rbnSortAsEntered
            // 
            this.rbnSortAsEntered.Location = new System.Drawing.Point(15, 56);
            this.rbnSortAsEntered.Name = "rbnSortAsEntered";
            this.rbnSortAsEntered.Size = new System.Drawing.Size(106, 18);
            this.rbnSortAsEntered.TabIndex = 3;
            this.rbnSortAsEntered.Text = "As Entered";
            this.rbnSortAsEntered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sorting_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(438, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTaxDesc
            // 
            this.txtTaxDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxDesc.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxDesc.Location = new System.Drawing.Point(367, 156);
            this.txtTaxDesc.MaxLength = 50;
            this.txtTaxDesc.Name = "txtTaxDesc";
            this.txtTaxDesc.Size = new System.Drawing.Size(404, 21);
            this.txtTaxDesc.TabIndex = 0;
            this.txtTaxDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(243, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Description";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(231, 159);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(10, 14);
            this.label27.TabIndex = 13;
            this.label27.Text = "*";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(231, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 14);
            this.label9.TabIndex = 14;
            this.label9.Text = "*";
            // 
            // MstTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 669);
            this.Name = "MstTax";
            this.Load += new System.EventHandler(this.MstTax_Load);
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
        private System.Windows.Forms.TextBox txtTaxDescSearch;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTaxPercentage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbnTaxDesc;
        private System.Windows.Forms.RadioButton rbnSortAsEntered;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbnTaxPercentage;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtTaxDesc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label27;
    }
}
