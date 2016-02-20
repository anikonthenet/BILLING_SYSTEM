namespace BillingSystem.FormMst.NormalEntries
{
    partial class MstSetup
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
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmailId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSaveConfirmationMessage = new System.Windows.Forms.CheckBox();
            this.grpChecked = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mskSundryCutoffDate = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSundryOpening = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mskLastReconcileDate = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtOnlineSmsMessage = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSmsSenderName = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtOfflineSmsMessage = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSmsWorkingKey = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.grpMailSetup = new System.Windows.Forms.GroupBox();
            this.txtNetPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNetHost = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNetPort = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNetUsername = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grpSort.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).BeginInit();
            this.grpCompany.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpChecked.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpMailSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSort
            // 
            this.grpSort.Location = new System.Drawing.Point(505, 658);
            // 
            // BtnSave
            // 
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(510, 657);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.TabIndex = 0;
            // 
            // grpButton
            // 
            this.grpButton.TabIndex = 2;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.grpMailSetup);
            this.pnlControls.Controls.Add(this.groupBox4);
            this.pnlControls.Controls.Add(this.groupBox3);
            this.pnlControls.Controls.Add(this.groupBox2);
            this.pnlControls.Controls.Add(this.grpChecked);
            this.pnlControls.Controls.Add(this.groupBox1);
            this.pnlControls.Controls.Add(this.grpCompany);
            this.pnlControls.ForeColor = System.Drawing.Color.Blue;
            this.pnlControls.Size = new System.Drawing.Size(1007, 554);
            this.pnlControls.TabIndex = 1;
            // 
            // ViewGrid
            // 
            this.ViewGrid.Location = new System.Drawing.Point(9, 658);
            this.ViewGrid.Size = new System.Drawing.Size(1003, 18);
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.label15);
            this.grpCompany.Controls.Add(this.mskStartDate);
            this.grpCompany.Controls.Add(this.label23);
            this.grpCompany.Controls.Add(this.txtCompanyName);
            this.grpCompany.Controls.Add(this.label2);
            this.grpCompany.Controls.Add(this.txtBranchName);
            this.grpCompany.Controls.Add(this.label1);
            this.grpCompany.Controls.Add(this.txtBranchCode);
            this.grpCompany.Controls.Add(this.label17);
            this.grpCompany.Font = new System.Drawing.Font("Courier New", 9F);
            this.grpCompany.Location = new System.Drawing.Point(15, 10);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(985, 88);
            this.grpCompany.TabIndex = 0;
            this.grpCompany.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(272, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 15);
            this.label15.TabIndex = 211;
            this.label15.Text = "(DD/MM/YYYY)";
            // 
            // mskStartDate
            // 
            this.mskStartDate.BackColor = System.Drawing.SystemColors.Info;
            this.mskStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskStartDate.ForeColor = System.Drawing.Color.Blue;
            this.mskStartDate.Location = new System.Drawing.Point(192, 61);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.ReadOnly = true;
            this.mskStartDate.Size = new System.Drawing.Size(77, 20);
            this.mskStartDate.TabIndex = 5;
            this.mskStartDate.TabStop = false;
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            this.mskStartDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(26, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 15);
            this.label23.TabIndex = 210;
            this.label23.Text = "Start Date";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BackColor = System.Drawing.SystemColors.Info;
            this.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.ForeColor = System.Drawing.Color.Blue;
            this.txtCompanyName.Location = new System.Drawing.Point(192, 39);
            this.txtCompanyName.MaxLength = 0;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.ReadOnly = true;
            this.txtCompanyName.Size = new System.Drawing.Size(519, 20);
            this.txtCompanyName.TabIndex = 2;
            this.txtCompanyName.TabStop = false;
            this.txtCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 134;
            this.label2.Text = "Company Name";
            // 
            // txtBranchName
            // 
            this.txtBranchName.BackColor = System.Drawing.SystemColors.Info;
            this.txtBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranchName.ForeColor = System.Drawing.Color.Blue;
            this.txtBranchName.Location = new System.Drawing.Point(364, 17);
            this.txtBranchName.MaxLength = 0;
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.ReadOnly = true;
            this.txtBranchName.Size = new System.Drawing.Size(347, 20);
            this.txtBranchName.TabIndex = 1;
            this.txtBranchName.TabStop = false;
            this.txtBranchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 132;
            this.label1.Text = "Branch Name";
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.BackColor = System.Drawing.SystemColors.Info;
            this.txtBranchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranchCode.ForeColor = System.Drawing.Color.Blue;
            this.txtBranchCode.Location = new System.Drawing.Point(192, 17);
            this.txtBranchCode.MaxLength = 0;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.ReadOnly = true;
            this.txtBranchCode.Size = new System.Drawing.Size(77, 20);
            this.txtBranchCode.TabIndex = 0;
            this.txtBranchCode.TabStop = false;
            this.txtBranchCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(26, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 15);
            this.label17.TabIndex = 130;
            this.label17.Text = "Branch Code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtContactNo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtWebsite);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtEmailId);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtPin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCity);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 9F);
            this.groupBox1.Location = new System.Drawing.Point(15, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(986, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtContactNo
            // 
            this.txtContactNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNo.ForeColor = System.Drawing.Color.Black;
            this.txtContactNo.Location = new System.Drawing.Point(192, 62);
            this.txtContactNo.MaxLength = 50;
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(183, 20);
            this.txtContactNo.TabIndex = 3;
            this.txtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(28, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 170;
            this.label13.Text = "Contact No.";
            // 
            // txtWebsite
            // 
            this.txtWebsite.BackColor = System.Drawing.SystemColors.Window;
            this.txtWebsite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsite.ForeColor = System.Drawing.Color.Black;
            this.txtWebsite.Location = new System.Drawing.Point(192, 84);
            this.txtWebsite.MaxLength = 50;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(519, 20);
            this.txtWebsite.TabIndex = 5;
            this.txtWebsite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(28, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 15);
            this.label8.TabIndex = 165;
            this.label8.Text = "Website";
            // 
            // txtEmailId
            // 
            this.txtEmailId.BackColor = System.Drawing.SystemColors.Window;
            this.txtEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailId.ForeColor = System.Drawing.Color.Black;
            this.txtEmailId.Location = new System.Drawing.Point(444, 62);
            this.txtEmailId.MaxLength = 50;
            this.txtEmailId.Name = "txtEmailId";
            this.txtEmailId.Size = new System.Drawing.Size(267, 20);
            this.txtEmailId.TabIndex = 4;
            this.txtEmailId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(378, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 15);
            this.label9.TabIndex = 163;
            this.label9.Text = "Email Id";
            // 
            // txtPin
            // 
            this.txtPin.BackColor = System.Drawing.SystemColors.Window;
            this.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.ForeColor = System.Drawing.Color.Black;
            this.txtPin.Location = new System.Drawing.Point(588, 40);
            this.txtPin.MaxLength = 20;
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(123, 20);
            this.txtPin.TabIndex = 2;
            this.txtPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(555, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 15);
            this.label3.TabIndex = 157;
            this.label3.Text = "Pin";
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.SystemColors.Window;
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(192, 40);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(359, 20);
            this.txtCity.TabIndex = 1;
            this.txtCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(28, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 155;
            this.label4.Text = "City";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.Location = new System.Drawing.Point(192, 18);
            this.txtAddress.MaxLength = 100;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(519, 20);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(28, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 153;
            this.label5.Text = "Address";
            // 
            // chkSaveConfirmationMessage
            // 
            this.chkSaveConfirmationMessage.AutoSize = true;
            this.chkSaveConfirmationMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSaveConfirmationMessage.Font = new System.Drawing.Font("Courier New", 9F);
            this.chkSaveConfirmationMessage.ForeColor = System.Drawing.Color.Black;
            this.chkSaveConfirmationMessage.Location = new System.Drawing.Point(26, 14);
            this.chkSaveConfirmationMessage.Name = "chkSaveConfirmationMessage";
            this.chkSaveConfirmationMessage.Size = new System.Drawing.Size(201, 19);
            this.chkSaveConfirmationMessage.TabIndex = 0;
            this.chkSaveConfirmationMessage.Text = "Save Confirmation Message";
            this.chkSaveConfirmationMessage.UseVisualStyleBackColor = true;
            this.chkSaveConfirmationMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // grpChecked
            // 
            this.grpChecked.Controls.Add(this.chkSaveConfirmationMessage);
            this.grpChecked.Font = new System.Drawing.Font("Courier New", 9F);
            this.grpChecked.ForeColor = System.Drawing.Color.Blue;
            this.grpChecked.Location = new System.Drawing.Point(16, 214);
            this.grpChecked.Name = "grpChecked";
            this.grpChecked.Size = new System.Drawing.Size(610, 36);
            this.grpChecked.TabIndex = 2;
            this.grpChecked.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mskSundryCutoffDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtSundryOpening);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Font = new System.Drawing.Font("Courier New", 9F);
            this.groupBox2.Location = new System.Drawing.Point(16, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(986, 48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // mskSundryCutoffDate
            // 
            this.mskSundryCutoffDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskSundryCutoffDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSundryCutoffDate.Location = new System.Drawing.Point(512, 15);
            this.mskSundryCutoffDate.Mask = "00/00/0000";
            this.mskSundryCutoffDate.Name = "mskSundryCutoffDate";
            this.mskSundryCutoffDate.Size = new System.Drawing.Size(83, 21);
            this.mskSundryCutoffDate.TabIndex = 1;
            this.mskSundryCutoffDate.ValidatingType = typeof(System.DateTime);
            this.mskSundryCutoffDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(599, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 214;
            this.label6.Text = "(DD/MM/YYYY)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(371, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 15);
            this.label7.TabIndex = 213;
            this.label7.Text = "Sundry Cutoff date";
            // 
            // txtSundryOpening
            // 
            this.txtSundryOpening.BackColor = System.Drawing.SystemColors.Window;
            this.txtSundryOpening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSundryOpening.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSundryOpening.ForeColor = System.Drawing.Color.Black;
            this.txtSundryOpening.Location = new System.Drawing.Point(192, 18);
            this.txtSundryOpening.MaxLength = 100;
            this.txtSundryOpening.Name = "txtSundryOpening";
            this.txtSundryOpening.Size = new System.Drawing.Size(165, 20);
            this.txtSundryOpening.TabIndex = 0;
            this.txtSundryOpening.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSundryOpening.Leave += new System.EventHandler(this.txtSundryOpening_Leave);
            this.txtSundryOpening.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSundryOpening_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(28, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(154, 15);
            this.label14.TabIndex = 153;
            this.label14.Text = "Sundry Opening Amount";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mskLastReconcileDate);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Font = new System.Drawing.Font("Courier New", 9F);
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(16, 497);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(735, 43);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // mskLastReconcileDate
            // 
            this.mskLastReconcileDate.BackColor = System.Drawing.SystemColors.Window;
            this.mskLastReconcileDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskLastReconcileDate.Location = new System.Drawing.Point(173, 13);
            this.mskLastReconcileDate.Mask = "00/00/0000";
            this.mskLastReconcileDate.Name = "mskLastReconcileDate";
            this.mskLastReconcileDate.Size = new System.Drawing.Size(83, 21);
            this.mskLastReconcileDate.TabIndex = 0;
            this.mskLastReconcileDate.ValidatingType = typeof(System.DateTime);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(260, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 15);
            this.label12.TabIndex = 217;
            this.label12.Text = "(DD/MM/YYYY)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(27, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 15);
            this.label16.TabIndex = 216;
            this.label16.Text = "Last Reconcile date";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtOnlineSmsMessage);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.txtSmsSenderName);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.txtOfflineSmsMessage);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.txtSmsWorkingKey);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Font = new System.Drawing.Font("Courier New", 9F);
            this.groupBox4.Location = new System.Drawing.Point(16, 377);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(986, 119);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SMS Setup";
            // 
            // txtOnlineSmsMessage
            // 
            this.txtOnlineSmsMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtOnlineSmsMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOnlineSmsMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnlineSmsMessage.ForeColor = System.Drawing.Color.Black;
            this.txtOnlineSmsMessage.Location = new System.Drawing.Point(189, 61);
            this.txtOnlineSmsMessage.MaxLength = 1000;
            this.txtOnlineSmsMessage.Name = "txtOnlineSmsMessage";
            this.txtOnlineSmsMessage.Size = new System.Drawing.Size(786, 20);
            this.txtOnlineSmsMessage.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(12, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(168, 15);
            this.label20.TabIndex = 167;
            this.label20.Text = "Online Invoice SMS Text";
            // 
            // txtSmsSenderName
            // 
            this.txtSmsSenderName.BackColor = System.Drawing.SystemColors.Window;
            this.txtSmsSenderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSmsSenderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmsSenderName.ForeColor = System.Drawing.Color.Black;
            this.txtSmsSenderName.Location = new System.Drawing.Point(189, 83);
            this.txtSmsSenderName.MaxLength = 100;
            this.txtSmsSenderName.Name = "txtSmsSenderName";
            this.txtSmsSenderName.Size = new System.Drawing.Size(245, 20);
            this.txtSmsSenderName.TabIndex = 3;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(11, 84);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 15);
            this.label21.TabIndex = 165;
            this.label21.Text = "Sender Name";
            // 
            // txtOfflineSmsMessage
            // 
            this.txtOfflineSmsMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtOfflineSmsMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOfflineSmsMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfflineSmsMessage.ForeColor = System.Drawing.Color.Black;
            this.txtOfflineSmsMessage.Location = new System.Drawing.Point(189, 40);
            this.txtOfflineSmsMessage.MaxLength = 1000;
            this.txtOfflineSmsMessage.Name = "txtOfflineSmsMessage";
            this.txtOfflineSmsMessage.Size = new System.Drawing.Size(786, 20);
            this.txtOfflineSmsMessage.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(12, 39);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(175, 15);
            this.label22.TabIndex = 155;
            this.label22.Text = "Offline Invoice SMS Text";
            // 
            // txtSmsWorkingKey
            // 
            this.txtSmsWorkingKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtSmsWorkingKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSmsWorkingKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmsWorkingKey.ForeColor = System.Drawing.Color.Black;
            this.txtSmsWorkingKey.Location = new System.Drawing.Point(189, 18);
            this.txtSmsWorkingKey.MaxLength = 1000;
            this.txtSmsWorkingKey.Name = "txtSmsWorkingKey";
            this.txtSmsWorkingKey.Size = new System.Drawing.Size(786, 20);
            this.txtSmsWorkingKey.TabIndex = 0;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(12, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(84, 15);
            this.label24.TabIndex = 153;
            this.label24.Text = "Working Key";
            // 
            // grpMailSetup
            // 
            this.grpMailSetup.Controls.Add(this.txtNetPassword);
            this.grpMailSetup.Controls.Add(this.label10);
            this.grpMailSetup.Controls.Add(this.txtNetHost);
            this.grpMailSetup.Controls.Add(this.label11);
            this.grpMailSetup.Controls.Add(this.txtNetPort);
            this.grpMailSetup.Controls.Add(this.label18);
            this.grpMailSetup.Controls.Add(this.txtNetUsername);
            this.grpMailSetup.Controls.Add(this.label19);
            this.grpMailSetup.Font = new System.Drawing.Font("Courier New", 9F);
            this.grpMailSetup.Location = new System.Drawing.Point(16, 308);
            this.grpMailSetup.Name = "grpMailSetup";
            this.grpMailSetup.Size = new System.Drawing.Size(986, 67);
            this.grpMailSetup.TabIndex = 4;
            this.grpMailSetup.TabStop = false;
            this.grpMailSetup.Text = "Mail Setup";
            // 
            // txtNetPassword
            // 
            this.txtNetPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtNetPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPassword.ForeColor = System.Drawing.Color.Black;
            this.txtNetPassword.Location = new System.Drawing.Point(458, 16);
            this.txtNetPassword.MaxLength = 100;
            this.txtNetPassword.Name = "txtNetPassword";
            this.txtNetPassword.Size = new System.Drawing.Size(245, 20);
            this.txtNetPassword.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(372, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 15);
            this.label10.TabIndex = 170;
            this.label10.Text = "Password";
            // 
            // txtNetHost
            // 
            this.txtNetHost.BackColor = System.Drawing.SystemColors.Window;
            this.txtNetHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetHost.ForeColor = System.Drawing.Color.Black;
            this.txtNetHost.Location = new System.Drawing.Point(458, 38);
            this.txtNetHost.MaxLength = 200;
            this.txtNetHost.Name = "txtNetHost";
            this.txtNetHost.Size = new System.Drawing.Size(245, 20);
            this.txtNetHost.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(372, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 15);
            this.label11.TabIndex = 165;
            this.label11.Text = "Host";
            // 
            // txtNetPort
            // 
            this.txtNetPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtNetPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPort.ForeColor = System.Drawing.Color.Black;
            this.txtNetPort.Location = new System.Drawing.Point(112, 40);
            this.txtNetPort.MaxLength = 100;
            this.txtNetPort.Name = "txtNetPort";
            this.txtNetPort.Size = new System.Drawing.Size(245, 20);
            this.txtNetPort.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(28, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 15);
            this.label18.TabIndex = 155;
            this.label18.Text = "Port";
            // 
            // txtNetUsername
            // 
            this.txtNetUsername.BackColor = System.Drawing.SystemColors.Window;
            this.txtNetUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetUsername.ForeColor = System.Drawing.Color.Black;
            this.txtNetUsername.Location = new System.Drawing.Point(112, 18);
            this.txtNetUsername.MaxLength = 200;
            this.txtNetUsername.Name = "txtNetUsername";
            this.txtNetUsername.Size = new System.Drawing.Size(245, 20);
            this.txtNetUsername.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(28, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 15);
            this.label19.TabIndex = 153;
            this.label19.Text = "Username";
            // 
            // MstSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 703);
            this.Name = "MstSetup";
            this.Load += new System.EventHandler(this.MstSetup_Load);
            this.grpSort.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewGrid)).EndInit();
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpChecked.ResumeLayout(false);
            this.grpChecked.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpMailSetup.ResumeLayout(false);
            this.grpMailSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmailId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox chkSaveConfirmationMessage;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox grpChecked;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSundryOpening;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox mskSundryCutoffDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox mskLastReconcileDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSmsSenderName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtOfflineSmsMessage;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtSmsWorkingKey;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox grpMailSetup;
        private System.Windows.Forms.TextBox txtNetPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNetHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNetPort;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNetUsername;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtOnlineSmsMessage;
        private System.Windows.Forms.Label label20;

    }
}
