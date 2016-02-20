namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstUser
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoginId = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserNameSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnSortUserName = new System.Windows.Forms.RadioButton();
            this.rbnSortAsEntered = new System.Windows.Forms.RadioButton();
            this.grpPassword = new System.Windows.Forms.GroupBox();
            this.lblConfrm = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkActiveInactive = new System.Windows.Forms.CheckBox();
            this.chkResetPassword = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Controls.Add(this.panel1);
            this.grpSort.Location = new System.Drawing.Point(648, 480);
            this.grpSort.TabIndex = 4;
            this.grpSort.Controls.SetChildIndex(this.BtnSortOK, 0);
            this.grpSort.Controls.SetChildIndex(this.BtnSortCancel, 0);
            this.grpSort.Controls.SetChildIndex(this.panel1, 0);
            // 
            // BtnSortCancel
            // 
            this.BtnSortCancel.Location = new System.Drawing.Point(190, 79);
            this.BtnSortCancel.TabIndex = 2;
            this.BtnSortCancel.Click += new System.EventHandler(this.BtnSortCancel_Click);
            this.BtnSortCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSortCancel_KeyPress);
            // 
            // BtnSortOK
            // 
            this.BtnSortOK.Location = new System.Drawing.Point(119, 79);
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
            this.grpSearch.Controls.Add(this.txtUserNameSearch);
            this.grpSearch.Location = new System.Drawing.Point(648, 480);
            this.grpSearch.TabIndex = 3;
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchOK, 0);
            this.grpSearch.Controls.SetChildIndex(this.BtnSearchCancel, 0);
            this.grpSearch.Controls.SetChildIndex(this.txtUserNameSearch, 0);
            this.grpSearch.Controls.SetChildIndex(this.label4, 0);
            // 
            // BtnSearchCancel
            // 
            this.BtnSearchCancel.TabIndex = 2;
            this.BtnSearchCancel.Click += new System.EventHandler(this.BtnSearchCancel_Click);
            this.BtnSearchCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BtnSearchCancel_KeyPress);
            // 
            // BtnSearchOK
            // 
            this.BtnSearchOK.TabIndex = 1;
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
            // BtnDelete
            // 
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
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
            this.pnlControls.Controls.Add(this.label5);
            this.pnlControls.Controls.Add(this.label27);
            this.pnlControls.Controls.Add(this.chkResetPassword);
            this.pnlControls.Controls.Add(this.chkActiveInactive);
            this.pnlControls.Controls.Add(this.grpPassword);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.txtLoginId);
            this.pnlControls.Controls.Add(this.txtUserName);
            this.pnlControls.TabIndex = 2;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 48);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 548);
            this.ViewGrid.TabIndex = 0;
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(304, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Login Id";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(304, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Display Name";
            // 
            // txtLoginId
            // 
            this.txtLoginId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginId.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginId.Location = new System.Drawing.Point(428, 180);
            this.txtLoginId.MaxLength = 10;
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(261, 21);
            this.txtLoginId.TabIndex = 1;
            this.txtLoginId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(428, 156);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(261, 21);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(12, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Display Name";
            // 
            // txtUserNameSearch
            // 
            this.txtUserNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserNameSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserNameSearch.Location = new System.Drawing.Point(107, 31);
            this.txtUserNameSearch.Name = "txtUserNameSearch";
            this.txtUserNameSearch.Size = new System.Drawing.Size(155, 21);
            this.txtUserNameSearch.TabIndex = 0;
            this.txtUserNameSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserNameSearch_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbnSortUserName);
            this.panel1.Controls.Add(this.rbnSortAsEntered);
            this.panel1.Location = new System.Drawing.Point(12, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 56);
            this.panel1.TabIndex = 0;
            // 
            // rbnSortUserName
            // 
            this.rbnSortUserName.Location = new System.Drawing.Point(15, 8);
            this.rbnSortUserName.Name = "rbnSortUserName";
            this.rbnSortUserName.Size = new System.Drawing.Size(113, 18);
            this.rbnSortUserName.TabIndex = 0;
            this.rbnSortUserName.Text = "Display Name";
            this.rbnSortUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbnSort_KeyPress);
            // 
            // rbnSortAsEntered
            // 
            this.rbnSortAsEntered.Location = new System.Drawing.Point(15, 30);
            this.rbnSortAsEntered.Name = "rbnSortAsEntered";
            this.rbnSortAsEntered.Size = new System.Drawing.Size(106, 18);
            this.rbnSortAsEntered.TabIndex = 1;
            this.rbnSortAsEntered.Text = "As Entered";
            this.rbnSortAsEntered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbnSort_KeyPress);
            // 
            // grpPassword
            // 
            this.grpPassword.Controls.Add(this.label7);
            this.grpPassword.Controls.Add(this.label6);
            this.grpPassword.Controls.Add(this.lblConfrm);
            this.grpPassword.Controls.Add(this.txtPassword2);
            this.grpPassword.Controls.Add(this.label3);
            this.grpPassword.Controls.Add(this.txtPassword);
            this.grpPassword.Location = new System.Drawing.Point(288, 230);
            this.grpPassword.Name = "grpPassword";
            this.grpPassword.Size = new System.Drawing.Size(414, 66);
            this.grpPassword.TabIndex = 4;
            this.grpPassword.TabStop = false;
            // 
            // lblConfrm
            // 
            this.lblConfrm.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfrm.Location = new System.Drawing.Point(14, 42);
            this.lblConfrm.Name = "lblConfrm";
            this.lblConfrm.Size = new System.Drawing.Size(119, 13);
            this.lblConfrm.TabIndex = 17;
            this.lblConfrm.Text = "Confirm Password";
            // 
            // txtPassword2
            // 
            this.txtPassword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword2.Location = new System.Drawing.Point(140, 38);
            this.txtPassword2.MaxLength = 10;
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.PasswordChar = '*';
            this.txtPassword2.Size = new System.Drawing.Size(261, 21);
            this.txtPassword2.TabIndex = 1;
            this.txtPassword2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(140, 14);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(261, 21);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // chkActiveInactive
            // 
            this.chkActiveInactive.AutoSize = true;
            this.chkActiveInactive.Checked = true;
            this.chkActiveInactive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActiveInactive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkActiveInactive.Font = new System.Drawing.Font("Courier New", 9F);
            this.chkActiveInactive.Location = new System.Drawing.Point(428, 209);
            this.chkActiveInactive.Name = "chkActiveInactive";
            this.chkActiveInactive.Size = new System.Drawing.Size(68, 19);
            this.chkActiveInactive.TabIndex = 2;
            this.chkActiveInactive.Text = "Active";
            this.chkActiveInactive.UseVisualStyleBackColor = true;
            this.chkActiveInactive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // chkResetPassword
            // 
            this.chkResetPassword.AutoSize = true;
            this.chkResetPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkResetPassword.Font = new System.Drawing.Font("Courier New", 9F);
            this.chkResetPassword.Location = new System.Drawing.Point(428, 307);
            this.chkResetPassword.Name = "chkResetPassword";
            this.chkResetPassword.Size = new System.Drawing.Size(124, 19);
            this.chkResetPassword.TabIndex = 3;
            this.chkResetPassword.Text = "Reset Password";
            this.chkResetPassword.UseVisualStyleBackColor = true;
            this.chkResetPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            this.chkResetPassword.CheckedChanged += new System.EventHandler(this.chkResetPassword_CheckedChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(292, 158);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(10, 14);
            this.label27.TabIndex = 12;
            this.label27.Text = "*";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(292, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(4, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(4, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "*";
            // 
            // MstUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1018, 669);
            this.Name = "MstUser";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.MstUser_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grpPassword.ResumeLayout(false);
            this.grpPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserNameSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtLoginId;
        public System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbnSortUserName;
        private System.Windows.Forms.RadioButton rbnSortAsEntered;
        private System.Windows.Forms.GroupBox grpPassword;
        private System.Windows.Forms.Label lblConfrm;
        public System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkResetPassword;
        private System.Windows.Forms.CheckBox chkActiveInactive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}
