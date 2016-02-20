namespace BillingSystem.FormTrn.PopUp
{
    partial class TrnOnlineOfflineEmailVerification
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.txtEmailID = new System.Windows.Forms.TextBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtContactPersonName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPartyName = new System.Windows.Forms.TextBox();
            this.txtInvAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.mskInvoiceDate = new System.Windows.Forms.MaskedTextBox();
            this.btnPreviewBill = new System.Windows.Forms.Button();
            this.grpInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.grpPartyDetails = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbPartyCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.grpSMSEmail = new System.Windows.Forms.GroupBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.chkSMS = new System.Windows.Forms.CheckBox();
            this.grpInvoiceDetails.SuspendLayout();
            this.grpPartyDetails.SuspendLayout();
            this.grpSMSEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnSave.Location = new System.Drawing.Point(281, 417);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(115, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "&Save && Send ";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.BtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BtnExit.Location = new System.Drawing.Point(404, 417);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(115, 23);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "&Cancel";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // txtEmailID
            // 
            this.txtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailID.Location = new System.Drawing.Point(117, 234);
            this.txtEmailID.MaxLength = 100;
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(369, 21);
            this.txtEmailID.TabIndex = 9;
            this.txtEmailID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailID_KeyDown);
            this.txtEmailID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailID_KeyPress);
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(117, 211);
            this.txtMobileNo.MaxLength = 50;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(187, 21);
            this.txtMobileNo.TabIndex = 8;
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 17);
            this.label11.TabIndex = 62;
            this.label11.Text = "Mobile No";
            // 
            // txtContactPersonName
            // 
            this.txtContactPersonName.BackColor = System.Drawing.SystemColors.Window;
            this.txtContactPersonName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPersonName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPersonName.Location = new System.Drawing.Point(117, 188);
            this.txtContactPersonName.MaxLength = 50;
            this.txtContactPersonName.Name = "txtContactPersonName";
            this.txtContactPersonName.Size = new System.Drawing.Size(369, 21);
            this.txtContactPersonName.TabIndex = 7;
            this.txtContactPersonName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 61;
            this.label2.Text = "Email Id";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 189);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 17);
            this.label13.TabIndex = 58;
            this.label13.Text = "Contact Person";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 52;
            this.label8.Text = "Inv Date";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 53;
            this.label3.Text = "Party Name";
            // 
            // txtPartyName
            // 
            this.txtPartyName.BackColor = System.Drawing.SystemColors.Info;
            this.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyName.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartyName.ForeColor = System.Drawing.Color.Blue;
            this.txtPartyName.Location = new System.Drawing.Point(117, 27);
            this.txtPartyName.MaxLength = 50;
            this.txtPartyName.Name = "txtPartyName";
            this.txtPartyName.ReadOnly = true;
            this.txtPartyName.Size = new System.Drawing.Size(369, 21);
            this.txtPartyName.TabIndex = 0;
            this.txtPartyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // txtInvAmount
            // 
            this.txtInvAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtInvAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvAmount.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvAmount.Location = new System.Drawing.Point(115, 41);
            this.txtInvAmount.MaxLength = 50;
            this.txtInvAmount.Name = "txtInvAmount";
            this.txtInvAmount.ReadOnly = true;
            this.txtInvAmount.Size = new System.Drawing.Size(187, 21);
            this.txtInvAmount.TabIndex = 1;
            this.txtInvAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInvAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Control_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 25);
            this.label1.TabIndex = 52;
            this.label1.Text = "Step : 2  Verification ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 63;
            this.label4.Text = "Inv Amount";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(201, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 15);
            this.label19.TabIndex = 206;
            this.label19.Text = "(DD/MM/YYYY)";
            // 
            // mskInvoiceDate
            // 
            this.mskInvoiceDate.BackColor = System.Drawing.SystemColors.Info;
            this.mskInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskInvoiceDate.Location = new System.Drawing.Point(115, 19);
            this.mskInvoiceDate.Mask = "00/00/0000";
            this.mskInvoiceDate.Name = "mskInvoiceDate";
            this.mskInvoiceDate.ReadOnly = true;
            this.mskInvoiceDate.Size = new System.Drawing.Size(83, 21);
            this.mskInvoiceDate.TabIndex = 0;
            this.mskInvoiceDate.ValidatingType = typeof(System.DateTime);
            // 
            // btnPreviewBill
            // 
            this.btnPreviewBill.BackColor = System.Drawing.Color.AliceBlue;
            this.btnPreviewBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPreviewBill.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviewBill.ForeColor = System.Drawing.Color.Maroon;
            this.btnPreviewBill.Location = new System.Drawing.Point(309, 41);
            this.btnPreviewBill.Name = "btnPreviewBill";
            this.btnPreviewBill.Size = new System.Drawing.Size(116, 24);
            this.btnPreviewBill.TabIndex = 2;
            this.btnPreviewBill.Text = "Preview";
            this.btnPreviewBill.UseVisualStyleBackColor = false;
            this.btnPreviewBill.Click += new System.EventHandler(this.btnPreviewBill_Click);
            // 
            // grpInvoiceDetails
            // 
            this.grpInvoiceDetails.Controls.Add(this.btnPreviewBill);
            this.grpInvoiceDetails.Controls.Add(this.txtInvAmount);
            this.grpInvoiceDetails.Controls.Add(this.label19);
            this.grpInvoiceDetails.Controls.Add(this.label8);
            this.grpInvoiceDetails.Controls.Add(this.mskInvoiceDate);
            this.grpInvoiceDetails.Controls.Add(this.label4);
            this.grpInvoiceDetails.Font = new System.Drawing.Font("Courier New", 9F);
            this.grpInvoiceDetails.Location = new System.Drawing.Point(7, 42);
            this.grpInvoiceDetails.Name = "grpInvoiceDetails";
            this.grpInvoiceDetails.Size = new System.Drawing.Size(512, 72);
            this.grpInvoiceDetails.TabIndex = 0;
            this.grpInvoiceDetails.TabStop = false;
            this.grpInvoiceDetails.Text = "Invoice Details";
            // 
            // grpPartyDetails
            // 
            this.grpPartyDetails.Controls.Add(this.label10);
            this.grpPartyDetails.Controls.Add(this.label7);
            this.grpPartyDetails.Controls.Add(this.label14);
            this.grpPartyDetails.Controls.Add(this.cmbPartyCategory);
            this.grpPartyDetails.Controls.Add(this.label5);
            this.grpPartyDetails.Controls.Add(this.txtCity);
            this.grpPartyDetails.Controls.Add(this.txtPin);
            this.grpPartyDetails.Controls.Add(this.label12);
            this.grpPartyDetails.Controls.Add(this.label9);
            this.grpPartyDetails.Controls.Add(this.txtAddress3);
            this.grpPartyDetails.Controls.Add(this.txtAddress2);
            this.grpPartyDetails.Controls.Add(this.label6);
            this.grpPartyDetails.Controls.Add(this.txtAddress1);
            this.grpPartyDetails.Controls.Add(this.txtPartyName);
            this.grpPartyDetails.Controls.Add(this.label3);
            this.grpPartyDetails.Controls.Add(this.txtEmailID);
            this.grpPartyDetails.Controls.Add(this.txtMobileNo);
            this.grpPartyDetails.Controls.Add(this.label13);
            this.grpPartyDetails.Controls.Add(this.label11);
            this.grpPartyDetails.Controls.Add(this.label2);
            this.grpPartyDetails.Controls.Add(this.txtContactPersonName);
            this.grpPartyDetails.Font = new System.Drawing.Font("Courier New", 9F);
            this.grpPartyDetails.Location = new System.Drawing.Point(7, 125);
            this.grpPartyDetails.Name = "grpPartyDetails";
            this.grpPartyDetails.Size = new System.Drawing.Size(512, 268);
            this.grpPartyDetails.TabIndex = 1;
            this.grpPartyDetails.TabStop = false;
            this.grpPartyDetails.Text = "Party Details";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(105, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 14);
            this.label10.TabIndex = 264;
            this.label10.Text = "*";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(105, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 14);
            this.label7.TabIndex = 263;
            this.label7.Text = "*";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(105, 190);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(10, 14);
            this.label14.TabIndex = 262;
            this.label14.Text = "*";
            // 
            // cmbPartyCategory
            // 
            this.cmbPartyCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartyCategory.FormattingEnabled = true;
            this.cmbPartyCategory.Location = new System.Drawing.Point(117, 49);
            this.cmbPartyCategory.Name = "cmbPartyCategory";
            this.cmbPartyCategory.Size = new System.Drawing.Size(187, 23);
            this.cmbPartyCategory.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 73;
            this.label5.Text = "Category";
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.SystemColors.Info;
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCity.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(117, 142);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.ReadOnly = true;
            this.txtCity.Size = new System.Drawing.Size(187, 21);
            this.txtCity.TabIndex = 5;
            // 
            // txtPin
            // 
            this.txtPin.BackColor = System.Drawing.SystemColors.Info;
            this.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPin.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.Location = new System.Drawing.Point(117, 165);
            this.txtPin.MaxLength = 50;
            this.txtPin.Name = "txtPin";
            this.txtPin.ReadOnly = true;
            this.txtPin.Size = new System.Drawing.Size(187, 21);
            this.txtPin.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 167);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 17);
            this.label12.TabIndex = 72;
            this.label12.Text = "Pin";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 17);
            this.label9.TabIndex = 71;
            this.label9.Text = "City";
            // 
            // txtAddress3
            // 
            this.txtAddress3.BackColor = System.Drawing.SystemColors.Info;
            this.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress3.Location = new System.Drawing.Point(117, 119);
            this.txtAddress3.MaxLength = 50;
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.ReadOnly = true;
            this.txtAddress3.Size = new System.Drawing.Size(369, 21);
            this.txtAddress3.TabIndex = 4;
            // 
            // txtAddress2
            // 
            this.txtAddress2.BackColor = System.Drawing.SystemColors.Info;
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(117, 96);
            this.txtAddress2.MaxLength = 50;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.ReadOnly = true;
            this.txtAddress2.Size = new System.Drawing.Size(369, 21);
            this.txtAddress2.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 70;
            this.label6.Text = "Address";
            // 
            // txtAddress1
            // 
            this.txtAddress1.BackColor = System.Drawing.SystemColors.Info;
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(117, 73);
            this.txtAddress1.MaxLength = 50;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.ReadOnly = true;
            this.txtAddress1.Size = new System.Drawing.Size(369, 21);
            this.txtAddress1.TabIndex = 2;
            // 
            // grpSMSEmail
            // 
            this.grpSMSEmail.Controls.Add(this.chkEmail);
            this.grpSMSEmail.Controls.Add(this.chkSMS);
            this.grpSMSEmail.Location = new System.Drawing.Point(7, 397);
            this.grpSMSEmail.Name = "grpSMSEmail";
            this.grpSMSEmail.Size = new System.Drawing.Size(143, 42);
            this.grpSMSEmail.TabIndex = 261;
            this.grpSMSEmail.TabStop = false;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Checked = true;
            this.chkEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmail.Location = new System.Drawing.Point(72, 15);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(56, 17);
            this.chkEmail.TabIndex = 1;
            this.chkEmail.Text = "Email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // chkSMS
            // 
            this.chkSMS.AutoSize = true;
            this.chkSMS.Checked = true;
            this.chkSMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSMS.Location = new System.Drawing.Point(14, 15);
            this.chkSMS.Name = "chkSMS";
            this.chkSMS.Size = new System.Drawing.Size(52, 17);
            this.chkSMS.TabIndex = 0;
            this.chkSMS.Text = "SMS";
            this.chkSMS.UseVisualStyleBackColor = true;
            // 
            // TrnOnlineOfflineEmailVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 446);
            this.Controls.Add(this.grpSMSEmail);
            this.Controls.Add(this.grpPartyDetails);
            this.Controls.Add(this.grpInvoiceDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrnOnlineOfflineEmailVerification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TrnOnlineOfflineEmailVerification_Load);
            this.grpInvoiceDetails.ResumeLayout(false);
            this.grpInvoiceDetails.PerformLayout();
            this.grpPartyDetails.ResumeLayout(false);
            this.grpPartyDetails.PerformLayout();
            this.grpSMSEmail.ResumeLayout(false);
            this.grpSMSEmail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button BtnSave;
        public System.Windows.Forms.Button BtnExit;
        public System.Windows.Forms.TextBox txtEmailID;
        public System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtContactPersonName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtPartyName;
        public System.Windows.Forms.TextBox txtInvAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.MaskedTextBox mskInvoiceDate;
        private System.Windows.Forms.Button btnPreviewBill;
        private System.Windows.Forms.GroupBox grpInvoiceDetails;
        private System.Windows.Forms.GroupBox grpPartyDetails;
        private System.Windows.Forms.ComboBox cmbPartyCategory;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtCity;
        public System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtAddress3;
        public System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox grpSMSEmail;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.CheckBox chkSMS;

    }
}