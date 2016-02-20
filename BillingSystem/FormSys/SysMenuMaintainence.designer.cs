namespace BillingSystem.FormSys
{
    partial class SysMenuMaintainence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysMenuMaintainence));
            this.BtnMenuMaintainence = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.dlgSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.txtInformation1 = new System.Windows.Forms.TextBox();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // BtnMenuMaintainence
            // 
            this.BtnMenuMaintainence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnMenuMaintainence.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnMenuMaintainence.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnMenuMaintainence.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnMenuMaintainence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnMenuMaintainence.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnMenuMaintainence.Location = new System.Drawing.Point(185, 212);
            this.BtnMenuMaintainence.Name = "BtnMenuMaintainence";
            this.BtnMenuMaintainence.Size = new System.Drawing.Size(155, 25);
            this.BtnMenuMaintainence.TabIndex = 6;
            this.BtnMenuMaintainence.Text = "&Menu Maintenance";
            this.BtnMenuMaintainence.UseVisualStyleBackColor = false;
            this.BtnMenuMaintainence.Click += new System.EventHandler(this.BtnMenuMaintainence_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnCancel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnCancel.Location = new System.Drawing.Point(340, 212);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(117, 25);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.Black;
            this.txtInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.ForeColor = System.Drawing.Color.White;
            this.txtInfo.Location = new System.Drawing.Point(11, 123);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(446, 53);
            this.txtInfo.TabIndex = 8;
            this.txtInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInformation
            // 
            this.txtInformation.BackColor = System.Drawing.Color.Black;
            this.txtInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformation.ForeColor = System.Drawing.Color.Yellow;
            this.txtInformation.Location = new System.Drawing.Point(11, 25);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.ReadOnly = true;
            this.txtInformation.Size = new System.Drawing.Size(446, 23);
            this.txtInformation.TabIndex = 9;
            this.txtInformation.Text = "Make Sure that you are maintaining the SERVER itself.";
            this.txtInformation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInformation1
            // 
            this.txtInformation1.BackColor = System.Drawing.Color.Black;
            this.txtInformation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformation1.ForeColor = System.Drawing.Color.Lime;
            this.txtInformation1.Location = new System.Drawing.Point(11, 64);
            this.txtInformation1.Multiline = true;
            this.txtInformation1.Name = "txtInformation1";
            this.txtInformation1.ReadOnly = true;
            this.txtInformation1.Size = new System.Drawing.Size(446, 43);
            this.txtInformation1.TabIndex = 11;
            this.txtInformation1.Text = "Make sure that in all Client Machine\'s eMicrofinance-Branch v1.0 Application shou" +
                "ld be closed";
            this.txtInformation1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpLogin
            // 
            this.grpLogin.BackColor = System.Drawing.Color.DimGray;
            this.grpLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpLogin.Location = new System.Drawing.Point(11, 200);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(446, 1);
            this.grpLogin.TabIndex = 131;
            this.grpLogin.TabStop = false;
            // 
            // SysMenuMaintainence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(470, 251);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.txtInformation1);
            this.Controls.Add(this.txtInformation);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.BtnMenuMaintainence);
            this.Controls.Add(this.BtnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SysMenuMaintainence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Maintainence";
            this.Load += new System.EventHandler(this.SysMenuMaintainence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BtnMenuMaintainence;
        internal System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.SaveFileDialog dlgSaveFileDialog;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.TextBox txtInformation;
        private System.Windows.Forms.TextBox txtInformation1;
        private System.Windows.Forms.GroupBox grpLogin;
    }
}