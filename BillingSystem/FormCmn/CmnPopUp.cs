
#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using BillingSystem;
using BillingSystem.FormMst;
using BillingSystem.Classes;

using JAYA.VB;

#endregion

namespace BillingSystem.FormCmn
{
    public partial class CmnPopUp : Form
    {

        #region PRIVATE OBJECTS DECLERATION

        private JVBCommon mainVB;
        private DMLService dml;
        private CommonService cmn;
        private DateService dtService;
        private DataSet dsData;

        #endregion

        #region PRIVATE VARIABLES DECLERATION

        private J_ColumnType J_emnSearchColType;
        private J_SearchType J_enmSearchType;

        private string strSearchColHelp;
        private bool blnDefaultCol = false;
        private bool blnDefaultColumnFlag = false;

        #endregion

        #region CONSTRUCTOR

        #region CmnPopUp [1]
        public CmnPopUp()
        {
            InitializeComponent();

            this.mainVB = new JVBCommon();
            this.dml = new DMLService();
            this.cmn = new CommonService();
            this.dtService = new DateService();
            this.dsData = new DataSet();

            this.Text = "Blank Header Text";
            blnDefaultCol = true;
        }
        #endregion

        #region CmnPopUp [2]
        public CmnPopUp(string FormHeaderText, bool blnDefaultSrhColType)
        {
            InitializeComponent();

            this.mainVB = new JVBCommon();
            this.dml = new DMLService();
            this.cmn = new CommonService();
            this.dtService = new DateService();
            this.dsData = new DataSet();

            this.Text = FormHeaderText;
            blnDefaultCol = blnDefaultSrhColType;
        }
        #endregion

        #region CmnPopUp [3]
        public CmnPopUp(string FormHeaderText, bool blnDefaultSrhColType, int iWidth)
        {
            InitializeComponent();

            this.mainVB = new JVBCommon();
            this.dml = new DMLService();
            this.cmn = new CommonService();
            this.dtService = new DateService();
            this.dsData = new DataSet();

            this.Text = FormHeaderText;
            blnDefaultCol = blnDefaultSrhColType;
            
            // Resizing the Help Grid
            this.Width = iWidth;
            this.grpHelpDisplay.Width = this.Width - 30;
            this.dgrdHelpGrid.Width = this.grpHelpDisplay.Width - 2;
            this.grpHelpCriteria.Width = this.Width - 30;
            
            pnlRbn.Left = this.grpHelpCriteria.Width - 340;
            txtSearchColumn.Width = this.grpHelpCriteria.Width - 225;

            BtnCancel.Left = this.grpHelpCriteria.Width - 82;
        }
        #endregion

        #region CmnPopUp [4]
        public CmnPopUp(string FormHeaderText, bool blnDefaultSrhColType, int iHeight, int iWidth)
        {
            InitializeComponent();

            this.mainVB = new JVBCommon();
            this.dml = new DMLService();
            this.cmn = new CommonService();
            this.dtService = new DateService();
            this.dsData = new DataSet();

            this.Text = FormHeaderText;
            blnDefaultCol = blnDefaultSrhColType;

            // Resizing the Help Grid
            // Height
            this.Height = iHeight;
            this.grpHelpDisplay.Height = this.Height - (this.grpHelpCriteria.Height + 40);
            this.dgrdHelpGrid.Height = this.grpHelpDisplay.Height - 10;
            this.grpHelpCriteria.Top = this.dgrdHelpGrid.Height + 10;

            // Width
            this.Width = iWidth;
            this.grpHelpDisplay.Width = this.Width - 30;
            this.dgrdHelpGrid.Width = this.grpHelpDisplay.Width - 2;
            this.grpHelpCriteria.Width = this.Width - 30;

            pnlRbn.Left = this.grpHelpCriteria.Width - 340;
            txtSearchColumn.Width = this.grpHelpCriteria.Width - 225;

            BtnCancel.Left = this.grpHelpCriteria.Width - 82;
        }
        #endregion

        #endregion

        #region USER DEFINED METHODS

        #region J_ShowDataInHelpGrid
        public bool J_ShowDataInHelpGrid(string SqlText, string[,] arrColumns)
        {
            if (this.dsData != null) this.dsData.Clear();
            this.dsData = dml.J_ExecSqlReturnDataSet(SqlText);
            if (this.dsData == null) return false;

            this.dgrdHelpGrid.DataSource = this.dsData.Tables[0];
            this.J_setCustomHelpGridColumn(this.dsData, arrColumns);

            return true;
        }
        #endregion

        #region J_setCustomHelpGridColumn
        private void J_setCustomHelpGridColumn(DataSet dataset, string[,] GridColumns)
        {
            cmbSearchOnColumn.Items.Clear();
            
            DataGridTableStyle dgtsTableStyle = new DataGridTableStyle();
            DataGridTextBoxColumn[] dgTextBoxColumn = new DataGridTextBoxColumn[GridColumns.GetUpperBound(0) + 1];
            dgtsTableStyle.MappingName = dataset.Tables[0].TableName;
            
            for (int intCounter = 0; intCounter <= GridColumns.GetUpperBound(0); intCounter++)
            {
                //==========================================================
                //== Start section of Data Grid
                //==========================================================
                
                // set the Header text & Width of respective column
                dgTextBoxColumn[intCounter] = new DataGridTextBoxColumn();
                dgTextBoxColumn[intCounter].MappingName = dataset.Tables[0].Columns[intCounter].ColumnName;
                dgTextBoxColumn[intCounter].HeaderText = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                dgTextBoxColumn[intCounter].Width = int.Parse(GridColumns[intCounter, (int)J_GridColumnSetting.Width]);
                
                // set the Data Format of respective column
                if (GridColumns[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                    dgTextBoxColumn[intCounter].Format = GridColumns[intCounter, (int)J_GridColumnSetting.Format];
                
                // set the Alignment of respective column
                if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" | GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Left;
                else
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Right;
                dgtsTableStyle.GridColumnStyles.Add(dgTextBoxColumn[intCounter]);
                
                //==========================================================
                //== End section of Data Grid
                //==========================================================

                //==========================================================
                //== Start section of Searching Combo Box
                //==========================================================
                // Filling Combo Box With Data(Column & DataType as Index) As.........
                // 1 means String Data Type Value
                // 2 means Integer DataType Value
                // 3 means Date Time Value
                // 1 means Char Value
                // 2 means Double Value
                // 2 means Decimal Value
                //==========================================================
                
                // Check whether Column Width value is ZERO or GREATER
                if (Convert.ToInt64(GridColumns[intCounter, (int)J_GridColumnSetting.Width]) > 0)
                {
                    // for String type data is stored as 1 
                    if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.String")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 1));
                    
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.String;
                            blnDefaultColumnFlag = true;
                        }
                    }
                    
                    // for integer type data is stored as 2 
                    else if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int16" |
                        dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int32" |
                        dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int64")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 2));
                      
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.Integer;
                            blnDefaultColumnFlag = true;
                        }
                    }
                    
                    // for date type data is stored as 3 
                    else if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.DateTime")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 3));
                      
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.Date;
                            blnDefaultColumnFlag = true;
                        }
                    }
                    
                    // for character type data is stored as 1 
                    else if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Char")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 1));
                        
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.String;
                            blnDefaultColumnFlag = true;
                        }
                    }
                    
                    // for double type data is stored as 2 
                    else if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Double")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 4));
                      
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.Double;
                            blnDefaultColumnFlag = true;
                        }
                    }
                    
                    // for decimal type data is stored as 2 
                    else if (dataset.Tables[0].Columns[intCounter].DataType.ToString() == "System.Decimal")
                    {
                        cmbSearchOnColumn.Items.Add(new ListBoxItem(GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 4));
                    
                        // Setting Default Searching Column based on default setting in constructor parameter true
                        if (blnDefaultColumnFlag == false & blnDefaultCol == true)
                        {
                            strSearchColHelp = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                            this.J_emnSearchColType = J_ColumnType.Double;
                            blnDefaultColumnFlag = true;
                        }
                    }
                }

                //========================================================== 
                //== End section of Searching Combo Box 
                //==========================================================
            }
           
            dgrdHelpGrid.TableStyles.Clear();
            dgrdHelpGrid.TableStyles.Add(dgtsTableStyle);
            
            // Setting Default values of Search Column & Type 
            J_Var.J_pMatrix = new string[dataset.Tables[0].Columns.Count];
            
            if (dataset.Tables[0].Rows.Count > 0)
                if (blnDefaultColumnFlag == true && blnDefaultCol == true)
                    cmbSearchOnColumn.SelectedIndex = 0;
            
        }

        #endregion

        #endregion

        #region USER DEFINE PROPERTIES

        #region SEARCH COLUMN NAME
        public string J_pSearchColumnName
        {
            get
            {
                return this.strSearchColHelp;
            }
            set
            {
                for (int intCounter = 0; intCounter <= this.dsData.Tables[0].Columns.Count - 1; intCounter++)
                {
                    if (value.ToUpper() == dgrdHelpGrid.TableStyles[0].GridColumnStyles[intCounter].HeaderText.ToUpper())
                    {
                        this.strSearchColHelp = dgrdHelpGrid.TableStyles[0].GridColumnStyles[intCounter].MappingName;
                        cmbSearchOnColumn.Text = value;
                        break;
                    }
                }
            }
        }
        #endregion

        #region SEARCH COLUMN TYPE
        public J_ColumnType J_pSearchColumnType
        {
            get
            {
                return this.J_emnSearchColType;
            }
            set
            {
                this.J_emnSearchColType = value;
            }
        }
        #endregion

        #endregion

        #region Event Handler of Controls

        #region CmnPopUp_Activated
        private void CmnPopUp_Activated(object sender, EventArgs e)
        {
            rbnIncrSearch.Checked = true;
            this.J_enmSearchType = J_SearchType.Incremental;
            txtSearchColumn.Text = "";
            txtSearchColumn.Focus();
        }
        #endregion

        #region BtnCancelOk
        private void BtnCancelOk(object sender, System.EventArgs e)
        {
            this.dsData.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region txtSearchTextChanged
        private void txtSearchTextChanged(object sender, System.EventArgs e)
        {
            if (this.dsData == null) return;
            if (this.dsData.Tables[0].Rows.Count <= 0) return;

            DataView dvwFilter = this.dsData.Tables[0].DefaultView;

            if (this.J_emnSearchColType == J_ColumnType.String)
            {
                if (txtSearchColumn.Text.Trim().IndexOf("[") < 0)
                {
                    if (this.J_enmSearchType == J_SearchType.Incremental)
                        dvwFilter.RowFilter = this.strSearchColHelp + " LIKE '" + cmn.J_ReplaceQuote(txtSearchColumn.Text.Trim()) + "%' ";
                    else if (this.J_enmSearchType == J_SearchType.Embedded)
                        dvwFilter.RowFilter = this.strSearchColHelp + " LIKE '%" + cmn.J_ReplaceQuote(txtSearchColumn.Text.Trim()) + "%' ";
                }
                else
                    dvwFilter.RowFilter = this.strSearchColHelp + " = 'JCS-GLOBAL-CLASS-METHODS-EVENTS' ";
                dgrdHelpGrid.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Integer)
            {
                if (txtSearchColumn.Text.Trim() != "")
                {
                    if (cmn.J_IsNumeric(txtSearchColumn.Text.Trim()) == true)
                    {
                        if (txtSearchColumn.Text.Trim().IndexOf(".") < 0) 
                            dvwFilter.RowFilter = this.strSearchColHelp + " = " + cmn.J_ReturnInt64Value(txtSearchColumn.Text.Trim()) + " ";
                        else
                            dvwFilter.RowFilter = this.strSearchColHelp + " <= 0 ";
                    }
                    else
                        dvwFilter.RowFilter = this.strSearchColHelp + " <= 0 ";
                }
                else
                    dvwFilter.RowFilter = this.strSearchColHelp + " <> 0 ";
                dgrdHelpGrid.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Double)
            {
                if (txtSearchColumn.Text.Trim() != "")
                {
                    if (cmn.J_IsNumeric(txtSearchColumn.Text.Trim()) == true)
                        dvwFilter.RowFilter = this.strSearchColHelp + " = " + cmn.J_ReturnDoubleValue(txtSearchColumn.Text.Trim()) + " ";
                    else
                        dvwFilter.RowFilter = this.strSearchColHelp + " <= 0 ";
                }
                else
                    dvwFilter.RowFilter = this.strSearchColHelp + " <> 0 ";
                dgrdHelpGrid.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Date)
            {
                if (txtSearchColumn.Text.Trim() != "")
                {
                    if (txtSearchColumn.Text.Trim().ToUpper() != "NULL")
                    {
                        if (dtService.J_IsDateValid(txtSearchColumn.Text.Trim()) == true)
                            dvwFilter.RowFilter = this.strSearchColHelp + " = " + cmn.J_DateOperator() + txtSearchColumn.Text.Trim() + cmn.J_DateOperator() + " ";
                        else
                            dvwFilter.RowFilter = this.strSearchColHelp + " = " + cmn.J_DateOperator() + "01/01/1800" + cmn.J_DateOperator() + " ";
                    }
                    else
                        dvwFilter.RowFilter = this.strSearchColHelp + " IS NULL ";
                    dgrdHelpGrid.DataSource = dvwFilter;
                }
                else
                {
                    dvwFilter.RowFilter = null;
                    dgrdHelpGrid.DataSource = this.dsData.Tables[0];
                }
            }
        }
        #endregion

        #region txtSearchKeyPress
        private void txtSearchKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 27)
            {
                this.Close();
                this.Dispose();
                return;
            }
            
            if (dgrdHelpGrid.CurrentRowIndex < 0)
                return;
            
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                dgrdHelpGrid.Select((int)dgrdHelpGrid.CurrentRowIndex);
                dgrdHelpGrid.Select();
                dgrdHelpGrid.Focus();
            }
        }
        #endregion
        
        #region dgrdHelpGrid_Click
        private void dgrdHelpGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(dgrdHelpGrid.CurrentRowIndex) > 0)
            {
                dgrdHelpGrid.Select(dgrdHelpGrid.CurrentRowIndex);
                dgrdHelpGrid.Select();
                dgrdHelpGrid.Focus();
            }
        }
        #endregion

        #region dgrdHelpGrid_CurrentCellChanged
        private void dgrdHelpGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            dgrdHelpGrid.Select(dgrdHelpGrid.CurrentRowIndex);
            dgrdHelpGrid.Select();
            dgrdHelpGrid.Focus();
        }
        #endregion

        #region dgrdHelpGrid_KeyUp
        private void dgrdHelpGrid_KeyUp(object sender, KeyEventArgs e)
        {
            dgrdHelpGrid_Click(sender, e);
        }
        #endregion

        #region dgrdHelpGrid_MouseMove
        private void dgrdHelpGrid_MouseMove(object sender, MouseEventArgs e)
        {
            cmn.J_GridToolTip(dgrdHelpGrid, e.X, e.Y);
        }
        #endregion


        #region cmbSearchOnColumn_SelectedIndexChanged
        private void cmbSearchOnColumn_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cmbSearchOnColumn.Items.Count > 0)
            {
                txtSearchColumn.Text = "";
                int intItemIndex = Convert.ToInt32(Support.GetItemData(cmbSearchOnColumn, cmbSearchOnColumn.SelectedIndex));
                    
                if (intItemIndex == 1)
                    this.J_emnSearchColType = J_ColumnType.String;
                else if (intItemIndex == 2)
                    this.J_emnSearchColType = J_ColumnType.Integer;
                else if (intItemIndex == 3)
                    this.J_emnSearchColType = J_ColumnType.Date;
                else if (intItemIndex == 4)
                    this.J_emnSearchColType = J_ColumnType.Double;
            }
            try
            {
                for (int intCounter = 0; intCounter <= this.dsData.Tables[0].Columns.Count - 1; intCounter++)
                    if (cmbSearchOnColumn.SelectedItem.ToString() == dgrdHelpGrid.TableStyles[0].GridColumnStyles[intCounter].HeaderText)
                        strSearchColHelp = dgrdHelpGrid.TableStyles[0].GridColumnStyles[intCounter].MappingName;
            }
            catch (Exception ex)
            {
                cmn.J_UserMessage(ex.Message);
            }
        }
        #endregion

        #region radio_Checked
        private void radio_Checked(object sender, EventArgs e)
        {
            RadioButton rbnWho = (RadioButton)sender;
            if (rbnWho.Name == "rbnIncrSearch")
                this.J_enmSearchType = J_SearchType.Incremental;
            else if (rbnWho.Name == "rbnEmbddSearch")
                this.J_enmSearchType = J_SearchType.Embedded;
        }
        #endregion


        #endregion


    }
}