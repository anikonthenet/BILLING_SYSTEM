
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: ReportService
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Class & methods
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

//~~ System Namespaces
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic.Compatibility.VB6;

//~~ User Namespaces
using BillingSystem;
using BillingSystem.FormRpt;

#endregion

namespace BillingSystem.Classes
{
    public class ReportService : IDisposable
    {
        
        #region PRIVATE OBJECTS DECLERATION

        private DMLService dmlService;
        private CommonService commonService;
        private DateService dateService;

        private DataSet dsDataGrid;
        private DataSet dsDataPrint;
        
        private DataGridView ViewGrid;
        private ComboBox SearchCombo;
        
        #endregion

        #region PRIVATE VARIABLES DECLERATION

        private string J_strSearchCol;
        private J_ColumnType J_emnSearchColType;

        private bool J_blnDefaultCol;
        private bool J_blnDefaultColumnFlag;
        private bool J_blnGridMultipleColumnStyle;
        
        #endregion

        #region CONSTRUCTOR

        #region ReportService [1]
        public ReportService()
        {
            this.dmlService = new DMLService();
            this.commonService = new CommonService();
            this.dateService = new DateService();

            this.ViewGrid = new DataGridView();
            this.SearchCombo = new ComboBox();

            this.J_strSearchCol               = string.Empty;
            this.J_blnDefaultCol              = true;
            this.J_blnDefaultColumnFlag       = false; // set defafult
            this.J_blnGridMultipleColumnStyle = false;
        }
        #endregion

        #endregion
        
        #region PRIVATE METHODS

        #region DISPOSE
        private void Dispose(bool Disposing)
        {
            this.dmlService.Dispose();
            this.commonService.Dispose();
            this.dsDataGrid = null;
            this.dsDataPrint = null;
            this.ViewGrid.Dispose();
            this.SearchCombo.Dispose();
        }
        #endregion

        
        #endregion
        
        #region PUBLIC METHODS

        #region DISPOSE
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion

        #region POPULATE DATA GRID VIEW [ OVERLOADED METHOD ]

        #region J_PopulateGridView [1]
        public bool J_PopulateGridView(DataGridView grdView, string SqlText)
        {
            if (this.dsDataGrid != null) this.dsDataGrid.Clear();
            this.dsDataGrid = dmlService.J_ExecSqlReturnDataSet(SqlText);
            if (this.dsDataGrid == null) return false;

            DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
            checkbox.ThreeState = false;

            grdView.Columns.Add(checkbox);
            grdView.DataSource = this.dsDataGrid.Tables[0];
            grdView.Columns[0].Width = 25;

            this.ViewGrid = grdView;

            // Id Column
            grdView.Columns[1].DataPropertyName = this.dsDataGrid.Tables[0].Columns[0].ColumnName.ToString();
            grdView.Columns[1].ReadOnly = true;
            grdView.Columns[1].Visible = false;

            // Description Column
            grdView.Columns[2].DataPropertyName = this.dsDataGrid.Tables[0].Columns[1].ColumnName.ToString();
            grdView.Columns[2].ReadOnly = true;
            grdView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            return true;
        }
        #endregion

        #region J_PopulateGridView [2]
        public bool J_PopulateGridView(DataGridView grdView, string SqlText, string[,] arrColumns)
        {

            try
            {
                if (this.dsDataGrid != null) this.dsDataGrid.Clear();
                this.dsDataGrid = dmlService.J_ExecSqlReturnDataSet(SqlText);
                if (this.dsDataGrid == null) return false;

                // Add Checkbox in  Column no 1
                DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
                grdView.Columns.Clear();
                
                checkbox.ThreeState = false;

                grdView.Columns.Add(checkbox);
                grdView.DataSource = this.dsDataGrid.Tables[0];
                grdView.Columns[0].Width = 25;
                
                // Column index of Dgv
                int intColumnIndex = 1;

                for (int intCounter = 0; intCounter <= arrColumns.GetUpperBound(0); intCounter++)
                {
                    // set the Header text & Width of respective column
                    grdView.Columns[intColumnIndex].DataPropertyName = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                    grdView.Columns[intColumnIndex].HeaderText       = arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                    grdView.Columns[intColumnIndex].Width            = int.Parse(arrColumns[intCounter, (int)J_GridColumnSetting.Width]);
                    grdView.Columns[intColumnIndex].ReadOnly         = true;
                    
                    // set the Data Format of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Format = arrColumns[intCounter, (int)J_GridColumnSetting.Format];
                    
                    // set the Alignment of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" | arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                    {
                        grdView.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    else
                    {
                        grdView.Columns[intColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdView.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    
                    // set the Visibility of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim() == "" | arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim().ToUpper() == "T")
                        grdView.Columns[intColumnIndex].Visible = true;
                    else
                        grdView.Columns[intColumnIndex].Visible = false;
                    
                    // set the AutoSize Mode  of respective column Default None
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim() == "" | arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim().ToUpper() == "NONE")
                        grdView.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    else
                        grdView.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    
                    intColumnIndex += 1;
                }
                return true;
                
            }
            catch (Exception exception)
            {
                commonService.J_UserMessage(exception.Message);
                return false;
            }
        }
        #endregion

        #region J_PopulateGridView [3]
        public bool J_PopulateGridView(DataGridView grdView, string SqlText, string[,] arrColumns, ref ComboBox SerchOnComboBox, bool MultipleColumns)
        {

            try
            {
                if (this.dsDataGrid != null) this.dsDataGrid.Clear();
                this.dsDataGrid = dmlService.J_ExecSqlReturnDataSet(SqlText);
                if (this.dsDataGrid == null) return false;

                // Add Checkbox in  Column no 1
                DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn();
                checkbox.ThreeState = false;

                grdView.Columns.Clear();
                grdView.Columns.Add(checkbox);
                grdView.DataSource = this.dsDataGrid.Tables[0];
                grdView.Columns[0].Width = 25;

                SerchOnComboBox.Items.Clear();

                this.ViewGrid = grdView;
                this.SearchCombo = SerchOnComboBox;

                this.J_blnGridMultipleColumnStyle = MultipleColumns;

                // Column index of Dgv
                int intColumnIndex = 1;

                for (int intCounter = 0; intCounter <= arrColumns.GetUpperBound(0); intCounter++)
                {
                    // set the Header text & Width of respective column
                    grdView.Columns[intColumnIndex].DataPropertyName = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                    grdView.Columns[intColumnIndex].HeaderText = arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                    grdView.Columns[intColumnIndex].Width = int.Parse(arrColumns[intCounter, (int)J_GridColumnSetting.Width]);
                    grdView.Columns[intColumnIndex].ReadOnly = true;
                    
                    // set the Data Format of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Format = arrColumns[intCounter, (int)J_GridColumnSetting.Format];
                    
                    // set the Alignment of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" 
                        | arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                    {
                        grdView.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    else
                    {
                        grdView.Columns[intColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                        grdView.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        grdView.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    
                    // set the Visibility of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim() == "" 
                        | arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim().ToUpper() == "T")
                        grdView.Columns[intColumnIndex].Visible = true;
                    else
                        grdView.Columns[intColumnIndex].Visible = false;
                    
                    // set the AutoSize Mode  of respective column Default None
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim() == "" 
                        | arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim().ToUpper() == "NONE")
                        grdView.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    else
                        grdView.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    //==========================================================
                    //==========================================================
                    //== End section of Data Grid
                    //==========================================================

                    //==========================================================
                    //== Start section of Searching Combo Box
                    //==========================================================
                    // Filling Combo Box With Data(Column & DataType as Index)
                    // 1 means String Data Type Value
                    // 2 means Integer DataType Value
                    // 3 means Date Time Value
                    // 1 means Char Value
                    // 2 means Double Value
                    // 2 means Decimal Value
                    //==========================================================
                    
                    // Check whether Column Width value is ZERO or GREATER
                    if (Convert.ToInt32(arrColumns[intCounter, (int)J_GridColumnSetting.Width]) > 0)
                    {
                        // for String type data is stored as 1 
                        if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.String")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 1));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.String;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                        
                        // for integer type data is stored as 2 
                        else if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int16" |
                                 this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int32" |
                                 this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Int64")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 2));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.Integer;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                        
                        // for date type data is stored as 3 
                        else if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.DateTime")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 3));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.Date;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                        
                        // for character type data is stored as 1 
                        else if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Char")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 1));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.String;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                        
                        // for double type data is stored as 2 
                        else if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Double")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 4));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.Double;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                        
                        // for decimal type data is stored as 2 
                        else if (this.dsDataGrid.Tables[0].Columns[intCounter].DataType.ToString() == "System.Decimal")
                        {
                            SerchOnComboBox.Items.Add(new ListBoxItem(arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText], 4));

                            // Setting Default Searching Column based on default setting in constructor parameter true
                            if (this.J_blnDefaultColumnFlag == false & this.J_blnDefaultCol == true)
                            {
                                this.J_strSearchCol = this.dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                                this.J_emnSearchColType = J_ColumnType.Double;
                                this.J_blnDefaultColumnFlag = true;
                            }
                        }
                    }
                    //========================================================== 
                    //== End section of Searching Combo Box 
                    //========================================================== 				
                    intColumnIndex += 1;
                }
                
                // Setting Default values of Search Column & Type 
                if (grdView.RowCount > 0)
                    if (this.J_blnDefaultCol == true && this.J_blnDefaultColumnFlag == true)
                        SerchOnComboBox.SelectedIndex = 0;

                return true;
            }
            catch (Exception exception)
            {
                commonService.J_UserMessage(exception.Message);
                return false;
            }
        }
        #endregion

        #endregion

        #region SEARCH DATA GRID VIEW [ OVERLOADED METHOD ]

        #region J_SearchOnDataGridView [1]
        public void J_SearchOnDataGridView(ref DataGridView grdView, ref ComboBox SearchComboBox, string strValue)
        {
            this.J_SearchOnDataGridView(ref grdView, ref SearchComboBox, strValue, J_SearchType.Incremental);
        }
        #endregion

        #region J_SearchOnDataGridView [2]
        public void J_SearchOnDataGridView(ref DataGridView grdView, ref ComboBox SearchComboBox, string strValue, J_SearchType enmSearchType)
        {
            DataView dvwFilter = this.dsDataGrid.Tables[0].DefaultView;

            if (this.J_emnSearchColType == J_ColumnType.String)
            {
                if (strValue.IndexOf("[") < 0)
                {
                    if (enmSearchType == J_SearchType.Incremental)
                        dvwFilter.RowFilter = this.J_strSearchCol + " LIKE '" + commonService.J_ReplaceQuote(strValue.Trim()) + "%' ";
                    else if (enmSearchType == J_SearchType.Embedded)
                        dvwFilter.RowFilter = this.J_strSearchCol + " LIKE '%" + commonService.J_ReplaceQuote(strValue.Trim()) + "%' ";
                }
                else
                    dvwFilter.RowFilter = this.J_strSearchCol + " = 'JCS-GLOBAL-CLASS-METHODS-EVENTS' ";
                grdView.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Integer)
            {
                if (strValue != "")
                {
                    if (commonService.J_IsNumeric(strValue) == true)
                    {
                        if(strValue.IndexOf(".") < 0)
                            dvwFilter.RowFilter = this.J_strSearchCol + " = " + commonService.J_ReturnInt64Value(strValue) + " ";
                        else
                            dvwFilter.RowFilter = this.J_strSearchCol + " <= 0 ";
                    }
                    else
                        dvwFilter.RowFilter = this.J_strSearchCol + " <= 0 ";
                }
                else
                    dvwFilter.RowFilter = this.J_strSearchCol + " <> 0 ";
                grdView.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Double)
            {
                if (strValue != "")
                {
                    if (commonService.J_IsNumeric(strValue) == true)
                        dvwFilter.RowFilter = this.J_strSearchCol + " = " + commonService.J_ReturnDoubleValue(strValue) + " ";
                    else
                        dvwFilter.RowFilter = this.J_strSearchCol + " <= 0 ";
                }
                else
                    dvwFilter.RowFilter = this.J_strSearchCol + " <> 0 ";
                grdView.DataSource = dvwFilter;
            }
            else if (this.J_emnSearchColType == J_ColumnType.Date)
            {
                if (strValue != "")
                {
                    if (strValue.Trim().ToUpper() != "NULL")
                    {
                        if (dateService.J_IsDateValid(strValue) == true)
                            dvwFilter.RowFilter = this.J_strSearchCol + " = '" + strValue + "' ";
                        else
                            dvwFilter.RowFilter = this.J_strSearchCol + " = '01/01/1800' ";
                    }
                    else
                        dvwFilter.RowFilter = this.J_strSearchCol + " IS NULL ";
                    grdView.DataSource = dvwFilter;
                }
                else
                {
                    dvwFilter.RowFilter = null;
                    grdView.DataSource = this.dsDataGrid.Tables[0];
                }
            }
        }
        #endregion

        #endregion

        #region POPULATE LIST VIEW [ OVERLOADED METHOD ]

        #region J_PopulateListView [1]
        public void J_PopulateListView(ref ListView listView, string SqlText, string DescriptionName, int DescriptionWidth)
        {
            this.dsDataGrid = dmlService.J_ExecSqlReturnDataSet(SqlText);
            this.J_PopulateListView(ref listView, this.dsDataGrid, DescriptionName, DescriptionWidth);
        }
        #endregion

        #region J_PopulateListView [2]
        public void J_PopulateListView(ref ListView listView, DataSet dataset, string DescriptionName, int DescriptionWidth)
        {
            try
            {
                listView.Items.Clear();
                listView.BeginUpdate();

                for (int intCounter = 0; intCounter <= dataset.Tables[0].Rows.Count - 1; intCounter++)
                {
                    DataRow dataRow = dataset.Tables[0].Rows[intCounter];
                    ListViewItem lvwItem = new ListViewItem();

                    lvwItem.SubItems.Add(dataRow[0].ToString());
                    lvwItem.SubItems.Add(dataRow[1].ToString());

                    listView.Items.Add(lvwItem);
                }
                listView.EndUpdate();

                listView.Columns[2].Text = DescriptionName;
                listView.Columns[2].Width = DescriptionWidth;
            }
            catch (Exception err_handler)
            {
                commonService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #endregion

        #region PREVIEW REPORT [ OVERLOADED METHODS ]

        #region J_PreviewReport [1]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, null, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [2]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string[,] ReportParameterNameANDValue)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, ReportParameterNameANDValue, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [3]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, J_PrintType PrintType)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, null, PrintType);
        }
        #endregion

        #region J_PreviewReport [4]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string[,] ReportParameterNameANDValue, J_PrintType PrintType)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, ReportParameterNameANDValue, PrintType);
        }
        #endregion

        #region J_PreviewReport [5]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, null, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [6]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string[,] ReportParameterNameANDValue)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, ReportParameterNameANDValue, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [7]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, J_PrintType PrintType)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, null, PrintType);
        }
        #endregion

        #region J_PreviewReport [8]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string[,] ReportParameterNameANDValue, J_PrintType PrintType)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, ReportParameterNameANDValue, PrintType);
        }
        #endregion

        #region J_PreviewReport [9]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, null, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [10]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, J_PrintType PrintType)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, null, PrintType);
        }
        #endregion

        #region J_PreviewReport [11]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, PreviewFormText, null, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [12]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, J_PrintType PrintType)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, PreviewFormText, null, PrintType);
        }
        #endregion

        #region J_PreviewReport [13]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, string[,] ReportParameterNameANDValue)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, ReportParameterNameANDValue, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [14]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, string[,] ReportParameterNameANDValue, J_PrintType PrintType)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, ReportParameterNameANDValue, PrintType);
        }
        #endregion

        #region J_PreviewReport [15]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, string[,] ReportParameterNameANDValue)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, PreviewFormText, ReportParameterNameANDValue, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [16]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, string[,] ReportParameterNameANDValue, J_PrintType PrintType)
        {
            if (dataset == null) return false;

            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);

                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                if (ReportParameterNameANDValue != null)
                {
                    for (int iCounter = 0; iCounter <= ReportParameterNameANDValue.GetUpperBound(0); iCounter++)
                        reportClass.SetParameterValue(ReportParameterNameANDValue[iCounter, 0], ReportParameterNameANDValue[iCounter, 1]);
                }

                frm.Text = "Report : " + PreviewFormText;
                if (PrintType == J_PrintType.Direct)
                    frm.CRViewer.PrintReport();
                else if (PrintType == J_PrintType.Preview)
                {
                    frm.CRViewer.Refresh();
                    frm.MdiParent = ReportDialog.MdiParent;
                    frm.Show();
                }
                return true;
            }
            else
            {
                commonService.J_UserMessage("Record not found.\nPreview not available");
                return false;
            }
        }
        #endregion

        #region J_PreviewReport [17]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string CompanyName, string CompanyAddress, string ReportTitle)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, CompanyName, CompanyAddress, ReportTitle, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [18]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string CompanyName, string CompanyAddress, string ReportTitle)
        {
            return this.J_PreviewReport(ref reportClass, ReportDialog, dataset, CompanyName, CompanyAddress, ReportTitle, J_PrintType.Preview);
        }
        #endregion

        #region J_PreviewReport [19]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string CompanyName, string CompanyAddress, string ReportTitle, J_PrintType enmPrintType)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_PreviewReport(ref reportClass, ReportDialog, this.dsDataPrint, CompanyName, CompanyAddress, ReportTitle, enmPrintType);
        }
        #endregion

        #region J_PreviewReport [20]
        public bool J_PreviewReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string CompanyName, string CompanyAddress, string ReportTitle, J_PrintType enmPrintType)
        {
            if (dataset == null) return false;
            
            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);


                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                // check company name exist
                if (CompanyName == "" || CompanyName == null)
                    CompanyName = "";
                reportClass.SetParameterValue("txtCompanyName", CompanyName);

                // check company address exist
                if (CompanyAddress == "" || CompanyAddress == null)
                    CompanyAddress = "";
                reportClass.SetParameterValue("txtBranch", CompanyAddress);

                // check report title exist
                if (ReportTitle == "" || ReportTitle == null)
                    ReportTitle = "";
                reportClass.SetParameterValue("txtReportTitle", ReportTitle);

                frm.Text = "Report : " + ReportTitle;

                //reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:\\report.pdf");   

                if (enmPrintType == J_PrintType.Direct)
                    frm.CRViewer.PrintReport();

                    

                else if (enmPrintType == J_PrintType.Preview)
                {
                    frm.CRViewer.Refresh();
                    frm.MdiParent = ReportDialog.MdiParent;
                    frm.Show();
                }
                return true;
            }
            else
            {
                commonService.J_UserMessage("Record not found.\nPreview not available");
                return false;
            }
        }
        #endregion


        #endregion

        #region SET TEXT TO REPORT [ OVERLOADED METHODS ]

        #region J_SetTextToReport [1]
        public void J_SetTextToReport(ref ReportClass reportClass, int SectionIndex, string TextObjectName, string TextObjectValue)
        {
            this.J_SetTextToReport(ref reportClass, SectionIndex, TextObjectName, null, TextObjectValue);
        }
        #endregion

        #region J_SetTextToReport [2]
        public void J_SetTextToReport(ref ReportClass reportClass, int SectionIndex, string TextObjectName, string TextObjectValueFormat, string TextObjectValue)
        {
            TextObject objtxtDate = (TextObject)reportClass.ReportDefinition.Sections[SectionIndex].ReportObjects[TextObjectName];

            if (TextObjectValueFormat == "" || TextObjectValueFormat == null)
                objtxtDate.Text = TextObjectValue;
            else
                objtxtDate.Text = string.Format(TextObjectValueFormat, TextObjectValue);
            
            objtxtDate.Dispose();
        }
        #endregion

        #endregion


        #region  ExportReport [OVERLOADED METHODS]

        #region J_ExportReport [1]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, null, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [2]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string[,] ReportParameterNameANDValue, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, ReportParameterNameANDValue, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [3]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, null, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [4]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string[,] ReportParameterNameANDValue, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, ReportDialog.Text, ReportParameterNameANDValue, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [5]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, null, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [6]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string[,] ReportParameterNameANDValue, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, ReportParameterNameANDValue, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [7]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, null, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [8]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string[,] ReportParameterNameANDValue, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, ReportDialog.Text, ReportParameterNameANDValue, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [9]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, null, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [10]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, null, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [11]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, PreviewFormText, null, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [12]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, PreviewFormText, null, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [13]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, string[,] ReportParameterNameANDValue, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, ReportParameterNameANDValue, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [14]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string PreviewFormText, string[,] ReportParameterNameANDValue, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, PreviewFormText, ReportParameterNameANDValue, PrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [15]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, string[,] ReportParameterNameANDValue, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, PreviewFormText, ReportParameterNameANDValue, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [16]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string PreviewFormText, string[,] ReportParameterNameANDValue, J_PrintType PrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            string strFilePath = ExportFilePath;
            string strFileWithPath = "";
            if (dataset == null) return false;

            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);

                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                if (ReportParameterNameANDValue != null)
                {
                    for (int iCounter = 0; iCounter <= ReportParameterNameANDValue.GetUpperBound(0); iCounter++)
                        reportClass.SetParameterValue(ReportParameterNameANDValue[iCounter, 0], ReportParameterNameANDValue[iCounter, 1]);
                }

                frm.Text = "Report : " + PreviewFormText;

                switch (ExportReportFormat)
                {
                    case BS_ExportReportFormat.PortableDocFormat:
                        strFileWithPath = Path.Combine(strFilePath, ExportFileName) + "." + BS_ExportReportFormat.PortableDocFormat;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                        break;
                    case BS_ExportReportFormat.Excel:
                        strFileWithPath = Path.Combine(Path.Combine(strFilePath, ExportFileName), "." + BS_ExportReportFormat.Excel);
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strFileWithPath);
                        break;
                    case BS_ExportReportFormat.Word:
                        strFileWithPath = Path.Combine(Path.Combine(strFilePath, ExportFileName), "." + BS_ExportReportFormat.Word);
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strFileWithPath);
                        break;
                }

                //if (PrintType == J_PrintType.Direct)
                //    frm.CRViewer.PrintReport();
                //else if (PrintType == J_PrintType.Preview)
                //{
                //    frm.CRViewer.Refresh();
                //    frm.MdiParent = ReportDialog.MdiParent;
                //    frm.Show();
                //}
                return true;
            }
            else
            {
                commonService.J_UserMessage("Record not found.\nPreview not available");
                return false;
            }
        }
        #endregion

        #region J_ExportReport [17]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string CompanyName, string CompanyAddress, string ReportTitle, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, CompanyName, CompanyAddress, ReportTitle, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [18]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string CompanyName, string CompanyAddress, string ReportTitle, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            return this.J_ExportReport(ref reportClass, ReportDialog, dataset, CompanyName, CompanyAddress, ReportTitle, J_PrintType.Preview, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [19]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, string SqlText, string CompanyName, string CompanyAddress, string ReportTitle, J_PrintType enmPrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return this.J_ExportReport(ref reportClass, ReportDialog, this.dsDataPrint, CompanyName, CompanyAddress, ReportTitle, enmPrintType, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [20]
        public bool J_ExportReport(ref ReportClass reportClass, Form ReportDialog, DataSet dataset, string CompanyName, string CompanyAddress, string ReportTitle, J_PrintType enmPrintType, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            string strFilePath = ExportFilePath;
            string strFileWithPath = "";
            if (dataset == null) return false;


            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);

                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                // check company name exist
                if (CompanyName == "" || CompanyName == null)
                    CompanyName = "";
                reportClass.SetParameterValue("txtCompanyName", CompanyName);

                // check company address exist
                if (CompanyAddress == "" || CompanyAddress == null)
                    CompanyAddress = "";
                reportClass.SetParameterValue("txtBranch", CompanyAddress);

                // check report title exist
                if (ReportTitle == "" || ReportTitle == null)
                    ReportTitle = "";
                reportClass.SetParameterValue("txtReportTitle", ReportTitle);

                frm.Text = "Report : " + ReportTitle;

                strFileWithPath = strFilePath + "1" + "." + BS_ExportReportFormat.PortableDocFormat;
                reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                switch (ExportReportFormat)
                {
                    case BS_ExportReportFormat.PortableDocFormat:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.PortableDocFormat;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                        break;
                    case BS_ExportReportFormat.Excel:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Excel;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strFileWithPath);
                        break;
                    case BS_ExportReportFormat.Word:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Word;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strFileWithPath);
                        break;
                }

                //if (enmPrintType == J_PrintType.Direct)
                //    frm.CRViewer.PrintReport();
                //else if (enmPrintType == J_PrintType.Preview)
                //{
                //    frm.CRViewer.Refresh();
                //    frm.MdiParent = ReportDialog.MdiParent;
                //    frm.Show();
                //}
                return true;
            }
            else
            {
                //commonService.J_UserMessage("Record not found.\nPreview not available");
                commonService.J_UserMessage("Please make sure you entered a valid file path", MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Commented CODE FOR EXPPORT TO PDF BY DHRUB

        //#region J_ExportReport [1]
        //public string J_ExportReport(ref ReportClass reportClass, string ExportReportFormat, string ExportFileName)
        //{
        //    return J_ExportReport(dmlService.J_pCommand, ref reportClass, ExportReportFormat, ExportFileName);
        //}
        //#endregion

        //#region J_ExportReport [2]
        //public string J_ExportReport(IDbCommand command, ref ReportClass reportClass, string ExportReportFormat, string ExportFileName)
        //{
        //    string strFilePath = "C:\\";
        //    string strFileWithPath = "";

        //    switch (ExportReportFormat)
        //    {
        //        case T_ExportReportFormat.PortableDocFormat:
        //            strFileWithPath = strFilePath + ExportFileName + "." +T_ExportReportFormat.PortableDocFormat;
        //            reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

        //            break;
        //        case T_ExportReportFormat.Excel:
        //            strFileWithPath = strFilePath + ExportFileName + "." +T_ExportReportFormat.Excel;
        //            reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strFileWithPath);
        //            break;
        //        case T_ExportReportFormat.Word:
        //            strFileWithPath = strFilePath + ExportFileName + "." +T_ExportReportFormat.Word;
        //            reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strFileWithPath);
        //            break;
        //    }
        //    return strFileWithPath;
        //}
        //#endregion 

        #endregion

        #endregion


        #endregion

        #region USER DEFINE PROPERTIES

        #region SEARCH COLUMN NAME
        public string J_pSearchColumnName
        {
            get
            {
                return this.J_strSearchCol;
            }
            set
            {
                string strSearchName = value;

                for (int intCounter = 0; intCounter <= this.ViewGrid.ColumnCount - 1; intCounter++)
                {
                    if (strSearchName.ToUpper() == this.ViewGrid.Columns[intCounter].HeaderText.ToString().Trim().ToUpper())
                    {
                        this.J_strSearchCol = this.ViewGrid.Columns[intCounter].DataPropertyName;
                        this.SearchCombo.Text = strSearchName;
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

        #region GRID MULTIPLE COLUMN STYLE
        public bool J_pGridMultipleColumnStyle
        {
            get
            {
                return this.J_blnGridMultipleColumnStyle;
            }
        }
        #endregion

        #region GET REPORT DATA SET
        public DataSet J_pGridDataSet
        {
            get
            {
                return this.dsDataGrid;
            }
        }
        public DataSet J_pPrintDataSet
        {
            get
            {
                return this.dsDataPrint;
            }
        }
        #endregion



        #endregion

    }
}
