namespace BillingSystem.FormSys
{
    partial class SysServerInfoLocal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysServerInfoLocal));
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.txtSQLDatabaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServerName
            // 
            this.txtServerName.BackColor = System.Drawing.Color.White;
            this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtServerName.ForeColor = System.Drawing.Color.Black;
            this.txtServerName.Location = new System.Drawing.Point(165, 30);
            this.txtServerName.MaxLength = 0;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(247, 21);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label1.Font = new System.Drawing.Font("Arial", 9F);
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(164, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(186, 19);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "Server Machine Name";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnSubmit.Font = new System.Drawing.Font("Arial", 9F);
            this.BtnSubmit.ForeColor = System.Drawing.Color.Black;
            this.BtnSubmit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnSubmit.Location = new System.Drawing.Point(196, 191);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(108, 24);
            this.BtnSubmit.TabIndex = 4;
            this.BtnSubmit.Text = "&Connect";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.BtnCancel.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(304, 191);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(108, 24);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDatabaseName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDatabaseName.ForeColor = System.Drawing.Color.Black;
            this.lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDatabaseName.Location = new System.Drawing.Point(164, 127);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(186, 19);
            this.lblDatabaseName.TabIndex = 32;
            this.lblDatabaseName.Text = "Database Name";
            this.lblDatabaseName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSQLDatabaseName
            // 
            this.txtSQLDatabaseName.BackColor = System.Drawing.Color.White;
            this.txtSQLDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLDatabaseName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtSQLDatabaseName.ForeColor = System.Drawing.Color.Black;
            this.txtSQLDatabaseName.Location = new System.Drawing.Point(165, 145);
            this.txtSQLDatabaseName.MaxLength = 0;
            this.txtSQLDatabaseName.Name = "txtSQLDatabaseName";
            this.txtSQLDatabaseName.Size = new System.Drawing.Size(247, 21);
            this.txtSQLDatabaseName.TabIndex = 3;
            this.txtSQLDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(164, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 19);
            this.label3.TabIndex = 32;
            this.label3.Text = "User Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDatabaseUserName
            // 
            this.txtDatabaseUserName.BackColor = System.Drawing.Color.White;
            this.txtDatabaseUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabaseUserName.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtDatabaseUserName.ForeColor = System.Drawing.Color.Black;
            this.txtDatabaseUserName.Location = new System.Drawing.Point(165, 68);
            this.txtDatabaseUserName.MaxLength = 0;
            this.txtDatabaseUserName.Name = "txtDatabaseUserName";
            this.txtDatabaseUserName.Size = new System.Drawing.Size(247, 21);
            this.txtDatabaseUserName.TabIndex = 1;
            this.txtDatabaseUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(164, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDatabasePassword
            // 
            this.txtDatabasePassword.BackColor = System.Drawing.Color.White;
            this.txtDatabasePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabasePassword.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtDatabasePassword.ForeColor = System.Drawing.Color.Black;
            this.txtDatabasePassword.Location = new System.Drawing.Point(165, 106);
            this.txtDatabasePassword.MaxLength = 0;
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            this.txtDatabasePassword.PasswordChar = '*';
            this.txtDatabasePassword.Size = new System.Drawing.Size(247, 21);
            this.txtDatabasePassword.TabIndex = 2;
            this.txtDatabasePassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BillingSystem.Properties.Resources.database_previous_icon;
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // grpLogin
            // 
            this.grpLogin.BackColor = System.Drawing.Color.Gainsboro;
            this.grpLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpLogin.Location = new System.Drawing.Point(13, 180);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(399, 1);
            this.grpLogin.TabIndex = 128;
            this.grpLogin.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(150, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(3, 154);
            this.groupBox1.TabIndex = 132;
            this.groupBox1.TabStop = false;
            // 
            // SysServerInfoLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(425, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.txtDatabasePassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDatabaseUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSQLDatabaseName);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SysServerInfoLocal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config Info";
            this.Load += new System.EventHandler(this.SysServerInfoLocal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button BtnSubmit;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.Label lblDatabaseName;
        internal System.Windows.Forms.TextBox txtSQLDatabaseName;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtDatabaseUserName;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}