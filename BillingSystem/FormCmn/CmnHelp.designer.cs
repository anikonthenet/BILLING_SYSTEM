namespace BillingSystem.FormCmn
{
    partial class CmnHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CmnHelp));
            this.grpHelpDisplay = new System.Windows.Forms.GroupBox();
            this.dgrdHelpGrid = new DGControl.DGControl();
            this.grpHelpCriteria = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.txtSearchColumn = new System.Windows.Forms.TextBox();
            this.pnlRbn = new System.Windows.Forms.Panel();
            this.rbnEmbddSearch = new System.Windows.Forms.RadioButton();
            this.rbnIncrSearch = new System.Windows.Forms.RadioButton();
            this.cmbSearchOnColumn = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.grpHelpDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdHelpGrid)).BeginInit();
            this.grpHelpCriteria.SuspendLayout();
            this.pnlRbn.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHelpDisplay
            // 
            this.grpHelpDisplay.Controls.Add(this.dgrdHelpGrid);
            this.grpHelpDisplay.Location = new System.Drawing.Point(12, 2);
            this.grpHelpDisplay.Name = "grpHelpDisplay";
            this.grpHelpDisplay.Size = new System.Drawing.Size(585, 354);
            this.grpHelpDisplay.TabIndex = 0;
            this.grpHelpDisplay.TabStop = false;
            // 
            // dgrdHelpGrid
            // 
            this.dgrdHelpGrid.CaptionFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgrdHelpGrid.CaptionVisible = false;
            this.dgrdHelpGrid.DataMember = "";
            this.dgrdHelpGrid.HeaderBackColor = System.Drawing.Color.Silver;
            this.dgrdHelpGrid.HeaderFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgrdHelpGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgrdHelpGrid.Location = new System.Drawing.Point(2, 8);
            this.dgrdHelpGrid.Name = "dgrdHelpGrid";
            this.dgrdHelpGrid.ReadOnly = true;
            this.dgrdHelpGrid.Size = new System.Drawing.Size(581, 344);
            this.dgrdHelpGrid.TabIndex = 0;
            this.dgrdHelpGrid.DoubleClick += new System.EventHandler(this.dgrdHelpGrid_DoubleClick);
            this.dgrdHelpGrid.CurrentCellChanged += new System.EventHandler(this.dgrdHelpGrid_CurrentCellChanged);
            this.dgrdHelpGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgrdHelpGrid_MouseMove);
            this.dgrdHelpGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgrdHelpGrid_KeyUp);
            this.dgrdHelpGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdHelpGrid_KeyDown);
            this.dgrdHelpGrid.Click += new System.EventHandler(this.dgrdHelpGrid_Click);
            // 
            // grpHelpCriteria
            // 
            this.grpHelpCriteria.Controls.Add(this.BtnCancel);
            this.grpHelpCriteria.Controls.Add(this.BtnOk);
            this.grpHelpCriteria.Controls.Add(this.txtSearchColumn);
            this.grpHelpCriteria.Controls.Add(this.pnlRbn);
            this.grpHelpCriteria.Controls.Add(this.cmbSearchOnColumn);
            this.grpHelpCriteria.Controls.Add(this.Label1);
            this.grpHelpCriteria.Location = new System.Drawing.Point(12, 354);
            this.grpHelpCriteria.Name = "grpHelpCriteria";
            this.grpHelpCriteria.Size = new System.Drawing.Size(585, 113);
            this.grpHelpCriteria.TabIndex = 1;
            this.grpHelpCriteria.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnCancel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Location = new System.Drawing.Point(504, 80);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancelOk);
            // 
            // BtnOk
            // 
            this.BtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnOk.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOk.Location = new System.Drawing.Point(428, 80);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 3;
            this.BtnOk.Text = "&Ok";
            this.BtnOk.UseVisualStyleBackColor = false;
            this.BtnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtSearchColumn
            // 
            this.txtSearchColumn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchColumn.Location = new System.Drawing.Point(219, 47);
            this.txtSearchColumn.MaxLength = 20;
            this.txtSearchColumn.Name = "txtSearchColumn";
            this.txtSearchColumn.Size = new System.Drawing.Size(360, 20);
            this.txtSearchColumn.TabIndex = 2;
            this.txtSearchColumn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchKeyPress);
            this.txtSearchColumn.TextChanged += new System.EventHandler(this.txtSearchTextChanged);
            // 
            // pnlRbn
            // 
            this.pnlRbn.Controls.Add(this.rbnEmbddSearch);
            this.pnlRbn.Controls.Add(this.rbnIncrSearch);
            this.pnlRbn.Location = new System.Drawing.Point(219, 12);
            this.pnlRbn.Name = "pnlRbn";
            this.pnlRbn.Size = new System.Drawing.Size(360, 30);
            this.pnlRbn.TabIndex = 1;
            // 
            // rbnEmbddSearch
            // 
            this.rbnEmbddSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnEmbddSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnEmbddSearch.ForeColor = System.Drawing.Color.Blue;
            this.rbnEmbddSearch.Location = new System.Drawing.Point(195, 4);
            this.rbnEmbddSearch.Name = "rbnEmbddSearch";
            this.rbnEmbddSearch.Size = new System.Drawing.Size(154, 24);
            this.rbnEmbddSearch.TabIndex = 1;
            this.rbnEmbddSearch.Text = "&Embedded Search";
            this.rbnEmbddSearch.CheckedChanged += new System.EventHandler(this.radio_Checked);
            // 
            // rbnIncrSearch
            // 
            this.rbnIncrSearch.Checked = true;
            this.rbnIncrSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbnIncrSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnIncrSearch.ForeColor = System.Drawing.Color.Blue;
            this.rbnIncrSearch.Location = new System.Drawing.Point(8, 4);
            this.rbnIncrSearch.Name = "rbnIncrSearch";
            this.rbnIncrSearch.Size = new System.Drawing.Size(169, 24);
            this.rbnIncrSearch.TabIndex = 0;
            this.rbnIncrSearch.TabStop = true;
            this.rbnIncrSearch.Text = "&Incremental Search";
            this.rbnIncrSearch.CheckedChanged += new System.EventHandler(this.radio_Checked);
            // 
            // cmbSearchOnColumn
            // 
            this.cmbSearchOnColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchOnColumn.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchOnColumn.Location = new System.Drawing.Point(6, 46);
            this.cmbSearchOnColumn.Name = "cmbSearchOnColumn";
            this.cmbSearchOnColumn.Size = new System.Drawing.Size(207, 22);
            this.cmbSearchOnColumn.TabIndex = 0;
            this.cmbSearchOnColumn.SelectedIndexChanged += new System.EventHandler(this.cmbSearchOnColumn_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.ForeColor = System.Drawing.Color.Blue;
            this.Label1.Location = new System.Drawing.Point(6, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 16);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "Search on";
            // 
            // CmnHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 481);
            this.Controls.Add(this.grpHelpCriteria);
            this.Controls.Add(this.grpHelpDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CmnHelp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Activated += new System.EventHandler(this.CmnHelp_Activated);
            this.grpHelpDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdHelpGrid)).EndInit();
            this.grpHelpCriteria.ResumeLayout(false);
            this.grpHelpCriteria.PerformLayout();
            this.pnlRbn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHelpDisplay;
        private System.Windows.Forms.GroupBox grpHelpCriteria;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox cmbSearchOnColumn;
        private System.Windows.Forms.Panel pnlRbn;
        internal System.Windows.Forms.RadioButton rbnEmbddSearch;
        internal System.Windows.Forms.RadioButton rbnIncrSearch;
        internal System.Windows.Forms.TextBox txtSearchColumn;
        internal System.Windows.Forms.Button BtnCancel;
        internal System.Windows.Forms.Button BtnOk;
        private DGControl.DGControl dgrdHelpGrid;
    }
}