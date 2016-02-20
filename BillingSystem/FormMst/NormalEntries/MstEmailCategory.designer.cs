namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstEmailCategory
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserNameSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbnSortUserName = new System.Windows.Forms.RadioButton();
            this.rbnSortAsEntered = new System.Windows.Forms.RadioButton();
            this.grpContainer = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBCCEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFromEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailSubjectOffline = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkInactive = new System.Windows.Forms.CheckBox();
            this.lblEmailSubject = new System.Windows.Forms.Label();
            this.txtEmailSubjectOnline = new System.Windows.Forms.TextBox();
            this.txtOfflineMailBody = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOnlineMailBody = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmailSubjectItemDispatched = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtItemsDispatchedMailBody = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpContainer.SuspendLayout();
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
            this.pnlControls.Controls.Add(this.grpContainer);
            this.pnlControls.TabIndex = 2;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 48);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 7);
            this.ViewGrid.TabIndex = 0;
            this.ViewGrid.DoubleClick += new System.EventHandler(this.ViewGrid_DoubleClick);
            this.ViewGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseUp);
            this.ViewGrid.CurrentCellChanged += new System.EventHandler(this.ViewGrid_CurrentCellChanged);
            this.ViewGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewGrid_MouseMove);
            this.ViewGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewGrid_KeyDown);
            this.ViewGrid.Click += new System.EventHandler(this.ViewGrid_Click);
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
            // grpContainer
            // 
            this.grpContainer.Controls.Add(this.label10);
            this.grpContainer.Controls.Add(this.txtEmailSubjectItemDispatched);
            this.grpContainer.Controls.Add(this.label11);
            this.grpContainer.Controls.Add(this.txtItemsDispatchedMailBody);
            this.grpContainer.Controls.Add(this.label12);
            this.grpContainer.Controls.Add(this.label9);
            this.grpContainer.Controls.Add(this.txtDisplayName);
            this.grpContainer.Controls.Add(this.label8);
            this.grpContainer.Controls.Add(this.txtBCCEmail);
            this.grpContainer.Controls.Add(this.label7);
            this.grpContainer.Controls.Add(this.label5);
            this.grpContainer.Controls.Add(this.txtFromEmail);
            this.grpContainer.Controls.Add(this.label6);
            this.grpContainer.Controls.Add(this.txtEmailSubjectOffline);
            this.grpContainer.Controls.Add(this.label3);
            this.grpContainer.Controls.Add(this.label2);
            this.grpContainer.Controls.Add(this.chkInactive);
            this.grpContainer.Controls.Add(this.lblEmailSubject);
            this.grpContainer.Controls.Add(this.txtEmailSubjectOnline);
            this.grpContainer.Controls.Add(this.txtOfflineMailBody);
            this.grpContainer.Controls.Add(this.label1);
            this.grpContainer.Controls.Add(this.txtOnlineMailBody);
            this.grpContainer.Location = new System.Drawing.Point(32, 10);
            this.grpContainer.Name = "grpContainer";
            this.grpContainer.Size = new System.Drawing.Size(937, 539);
            this.grpContainer.TabIndex = 4;
            this.grpContainer.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(122, 511);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayName.Location = new System.Drawing.Point(212, 508);
            this.txtDisplayName.MaxLength = 100;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(472, 20);
            this.txtDisplayName.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(141, 488);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "BCC Email";
            // 
            // txtBCCEmail
            // 
            this.txtBCCEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBCCEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBCCEmail.Location = new System.Drawing.Point(212, 485);
            this.txtBCCEmail.MaxLength = 100;
            this.txtBCCEmail.Name = "txtBCCEmail";
            this.txtBCCEmail.Size = new System.Drawing.Size(472, 20);
            this.txtBCCEmail.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 442);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(925, 2);
            this.label7.TabIndex = 13;
            this.label7.Text = "label7";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "From Email";
            // 
            // txtFromEmail
            // 
            this.txtFromEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromEmail.Location = new System.Drawing.Point(212, 462);
            this.txtFromEmail.MaxLength = 100;
            this.txtFromEmail.Name = "txtFromEmail";
            this.txtFromEmail.Size = new System.Drawing.Size(472, 20);
            this.txtFromEmail.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Email Subject for Offline Purchase";
            // 
            // txtEmailSubjectOffline
            // 
            this.txtEmailSubjectOffline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailSubjectOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailSubjectOffline.Location = new System.Drawing.Point(212, 155);
            this.txtEmailSubjectOffline.MaxLength = 100;
            this.txtEmailSubjectOffline.Name = "txtEmailSubjectOffline";
            this.txtEmailSubjectOffline.Size = new System.Drawing.Size(472, 20);
            this.txtEmailSubjectOffline.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Email body for Offline Purchase : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Email body for Online Purchase : ";
            // 
            // chkInactive
            // 
            this.chkInactive.AutoSize = true;
            this.chkInactive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInactive.ForeColor = System.Drawing.Color.Blue;
            this.chkInactive.Location = new System.Drawing.Point(840, 497);
            this.chkInactive.Name = "chkInactive";
            this.chkInactive.Size = new System.Drawing.Size(83, 17);
            this.chkInactive.TabIndex = 5;
            this.chkInactive.TabStop = false;
            this.chkInactive.Text = "INACTIVE";
            this.chkInactive.UseVisualStyleBackColor = true;
            // 
            // lblEmailSubject
            // 
            this.lblEmailSubject.AutoSize = true;
            this.lblEmailSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailSubject.Location = new System.Drawing.Point(9, 17);
            this.lblEmailSubject.Name = "lblEmailSubject";
            this.lblEmailSubject.Size = new System.Drawing.Size(200, 13);
            this.lblEmailSubject.TabIndex = 4;
            this.lblEmailSubject.Text = "Email Subject for Online Purchase";
            // 
            // txtEmailSubjectOnline
            // 
            this.txtEmailSubjectOnline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailSubjectOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailSubjectOnline.Location = new System.Drawing.Point(212, 14);
            this.txtEmailSubjectOnline.MaxLength = 100;
            this.txtEmailSubjectOnline.Name = "txtEmailSubjectOnline";
            this.txtEmailSubjectOnline.Size = new System.Drawing.Size(472, 20);
            this.txtEmailSubjectOnline.TabIndex = 0;
            // 
            // txtOfflineMailBody
            // 
            this.txtOfflineMailBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOfflineMailBody.Location = new System.Drawing.Point(6, 196);
            this.txtOfflineMailBody.Multiline = true;
            this.txtOfflineMailBody.Name = "txtOfflineMailBody";
            this.txtOfflineMailBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOfflineMailBody.Size = new System.Drawing.Size(925, 93);
            this.txtOfflineMailBody.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(925, 2);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // txtOnlineMailBody
            // 
            this.txtOnlineMailBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOnlineMailBody.Location = new System.Drawing.Point(6, 53);
            this.txtOnlineMailBody.Multiline = true;
            this.txtOnlineMailBody.Name = "txtOnlineMailBody";
            this.txtOnlineMailBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOnlineMailBody.Size = new System.Drawing.Size(925, 93);
            this.txtOnlineMailBody.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 302);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(205, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Email Subject for Items Dispatched";
            // 
            // txtEmailSubjectItemDispatched
            // 
            this.txtEmailSubjectItemDispatched.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailSubjectItemDispatched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailSubjectItemDispatched.Location = new System.Drawing.Point(219, 299);
            this.txtEmailSubjectItemDispatched.MaxLength = 100;
            this.txtEmailSubjectItemDispatched.Name = "txtEmailSubjectItemDispatched";
            this.txtEmailSubjectItemDispatched.Size = new System.Drawing.Size(472, 20);
            this.txtEmailSubjectItemDispatched.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(9, 322);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Email body for Items Dispatched : ";
            // 
            // txtItemsDispatchedMailBody
            // 
            this.txtItemsDispatchedMailBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemsDispatchedMailBody.Location = new System.Drawing.Point(6, 340);
            this.txtItemsDispatchedMailBody.Multiline = true;
            this.txtItemsDispatchedMailBody.Name = "txtItemsDispatchedMailBody";
            this.txtItemsDispatchedMailBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtItemsDispatchedMailBody.Size = new System.Drawing.Size(925, 93);
            this.txtItemsDispatchedMailBody.TabIndex = 93;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 292);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(925, 2);
            this.label12.TabIndex = 18;
            this.label12.Text = "label12";
            // 
            // MstEmailCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1018, 669);
            this.Name = "MstEmailCategory";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.MstUser_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grpContainer.ResumeLayout(false);
            this.grpContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserNameSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbnSortUserName;
        private System.Windows.Forms.RadioButton rbnSortAsEntered;
        private System.Windows.Forms.GroupBox grpContainer;
        private System.Windows.Forms.TextBox txtOnlineMailBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOfflineMailBody;
        private System.Windows.Forms.TextBox txtEmailSubjectOnline;
        private System.Windows.Forms.Label lblEmailSubject;
        private System.Windows.Forms.CheckBox chkInactive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmailSubjectOffline;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFromEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBCCEmail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEmailSubjectItemDispatched;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtItemsDispatchedMailBody;
        private System.Windows.Forms.Label label12;
    }
}
