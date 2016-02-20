
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Arup 
Module Name		: MstItem
Version			: 2.0
Start Date		: 09-06-2010
End Date		: 09-06-2010
Last Modified   : 
Tables Used     : MST_INVOICE_SERIES, 
Module Desc		: ENTRY OF THE COMPANY MASTER
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

// System Namespaces
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

// User Namespaces
using JAYA.VB;
using BillingSystem.FormRpt;
using BillingSystem.Classes;

// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace BillingSystem.FormMst.NormalEntries
{
    public partial class MstInvoiceSeries : BillingSystem.FormGen.GenForm
    {

        #region System Generated Code
        public MstInvoiceSeries()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        JVBCommon mainVB = new JVBCommon();

        DataSet dsetGridClone = new DataSet();
        
        // For Storing the Id
        long lngSearchId;

        // For Storing the Local SQL Query
        string strSQL;

        // For Storing the general SQL Query
        string strQuery;

        // For Sotring the Order By Values
        string strOrderBy;

        // For Sotring the Where Values
        string strCheckFields;
        
        string strTempMode;
        string[,] strMatrix = null;
        
        #endregion

        #region User Defined Events

        #region MstInvoiceSeries_Load
        private void MstInvoiceSeries_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ControlVisible(false);
                ClearControls();
                
                // set the Help Grid Column Header Text & behavior
                // (0) Header Text
                // (1) Width
                // (2) Format
                // (3) Alignment
                // (4) NullToText
                // (5) Visible
                // (6) AutoSizeMode
                string[,] strMatrix1 = {{"InvoiceSeriesID", "0", "", "Right", "", "", ""},
                                        {"CompanyID", "0", "", "Right", "", "", ""}, 
							            {"Prefix", "250", "S", "", "", "", ""},
                                        {"Company Name", "250", "", "", "", "", ""},
							            {"Start No", "80", "", "Right", "", "", ""},
                                        {"Last No", "80", "", "Right", "", "", ""},
                                        {"Last Date", "100", "", "", "", "", ""},
                                        {"Display Text", "190", "", "", "", "", ""}};
                strMatrix = strMatrix1;

                strOrderBy = "MST_INVOICE_SERIES.PREFIX";
                strQuery = "SELECT MST_INVOICE_SERIES.INVOICE_SERIES_ID AS INVOICE_SERIES_ID," +
                    "              MST_COMPANY.COMPANY_ID               AS COMPANY_ID," +
                    "              MST_INVOICE_SERIES.PREFIX            AS PREFIX," +
                    "              MST_COMPANY.COMPANY_NAME             AS COMPANY_NAME," +                    
                    "              MST_INVOICE_SERIES.START_NO          AS START_NO," +
                    "              MST_INVOICE_SERIES.LAST_NO           AS LAST_NO," +
                    "            " + cmnService.J_SQLDBFormat("MST_INVOICE_SERIES.LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS LAST_DATE," +
                    "              MST_INVOICE_SERIES.HEADER_DISPLAY_TEXT AS HEADER_DISPLAY_TEXT" +
                    "       FROM   MST_INVOICE_SERIES," +
                    "              MST_COMPANY"+
                    "       WHERE  MST_INVOICE_SERIES.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "       AND    MST_INVOICE_SERIES.BRANCH_ID  = " + J_Var.J_pBranchId + " ";
                
                strSQL = strQuery + "ORDER BY " + strOrderBy;                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                
                lblTitle.Text = this.Text;
                ViewGrid_Click(sender, e);
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                
                strCheckFields = "";
                cmbCompanyName.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnEdit_Click
        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    ControlVisible(true);
                    ClearControls();
                    
                    //=============== EDIT RESTRICTION ==================

                    // RESTRICT RECORD MODIFICATION WHEN START NO & LAST NO NOT MATCH
                    if (dmlService.J_IsRecordExist("MST_INVOICE_SERIES",
                           "    START_NO           <> LAST_NO " +
                           "AND INVOICE_SERIES_ID  = " + lngSearchId + "") == true)                    {
                        cmnService.J_UserMessage("Modification not possible");
                        
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                        return ;
                    }

                    // RESTRICT RECORD MODIFICATION WHEN IF TRANSACTION IS OVER
                    if (dmlService.J_IsRecordExist("TRN_INVOICE_HEADER",
                           " INVOICE_SERIES_ID  = " + lngSearchId + "") == true)
                    {
                        cmnService.J_UserMessage("Modification not possible");                       
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                        return;
                    }
                    
                    
                    // A particular id wise retrieving the data from database
                    if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                    }
                    
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    
                    strCheckFields = "";
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ControlVisible(false);
                ClearControls();     //Clear all the Controls
                
                strSQL = strQuery + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId) == false)
                    BtnAdd.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSort_Click
        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Sorting;
                
                if (ValidateFields() == false) return;
                
                grpSort.Visible = true;
                grpSearch.Visible = false;
                
                rbnSortPrefixName.Checked = false;              
                rbnSortAsEntered.Checked = false;

                if (strOrderBy == "MST_INVOICE_SERIES.PREFIX")
                    rbnSortPrefixName.Select();
                else if (strOrderBy == "MST_INVOICE_SERIES.INVOICE_SERIES_ID")
                    rbnSortAsEntered.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rbnSortPrefixName.Checked == true)
                    strOrderBy = "MST_INVOICE_SERIES.PREFIX";             
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "MST_INVOICE_SERIES.INVOICE_SERIES_ID";
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_KeyPress
        private void BtnSortOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortCancel_KeyPress
        private void BtnSortCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Searching;
                
                if (ValidateFields() == false) return;
                
                grpSort.Visible = false;
                grpSearch.Visible = true;
                
                txtPrefixSearch.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Validate Fields
                if (ValidateFields() == false) return;
                
                strCheckFields = "";
                
                // Prefix name
                if (txtPrefixSearch.Text.Trim() != "")
                    strCheckFields = "AND MST_INVOICE_SERIES.PREFIX like '%" + cmnService.J_ReplaceQuote(txtPrefixSearch.Text.Trim().ToUpper()) + "%' ";

                // START_NO name
                if (Convert.ToInt32(cmnService.J_NumericData(txtStartNoSearch)) != 0)
                    strCheckFields += "AND MST_INVOICE_SERIES.START_NO =" + Convert.ToInt32(cmnService.J_NumericData(txtStartNoSearch)) + " ";


                // LAST_NO name
                if (Convert.ToInt32(cmnService.J_NumericData(txtLastNoSearch)) != 0)
                    strCheckFields += "AND MST_INVOICE_SERIES.LAST_NO =" + Convert.ToInt32(cmnService.J_NumericData(txtLastNoSearch)) + " ";

                // LAST_DATE name
                if (mskLastDateSearch.Text != "  /  /")
                {
                   string strLastDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskLastDateSearch.Text) + cmnService.J_DateOperator();

                   strCheckFields += "AND MST_INVOICE_SERIES.LAST_DATE =" + strLastDate + " ";

                }

                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId) == false)
                {
                    txtPrefixSearch.Select();
                    return;
                }
                
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion 

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                lblSearchMode.Text = J_Mode.General;
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ClearControls();
                
                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.InvoiceSeries, this.Text);
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }
        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
        }
        #endregion

        #region ViewGrid_MouseMove
        private void ViewGrid_MouseMove(object sender, MouseEventArgs e)
        {
            cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region ComboBox_KeyUp
        private void ComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox cmbComboBox = (ComboBox)sender;
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbComboBox, e);
        }
        #endregion

        #region ComboBox_Leave
        private void ComboBox_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            ComboBox cmbComboBox = (ComboBox)sender;

            cmnService.J_AutoCompleteCombo_Leave(ref cmbComboBox);
            object objRowView = cmbComboBox.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound + " : " + cmbComboBox.Text);
                cmbComboBox.Focus();
            }
        }
        #endregion

        #region txtStartNo_KeyPress
        private void txtStartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtStartNo, "") == false)
                e.Handled = true;
        }

        #endregion              
       
        #region txtStartNo_Leave
        private void txtStartNo_Leave(object sender, EventArgs e)
        {
            txtStartNo.Text = cmnService.J_FormatToString(Convert.ToInt32(cmnService.J_NumericData(txtStartNo)), "0");
        }
        #endregion

        #region txtStartNoSearch_KeyPress
        private void txtStartNoSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtStartNo, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtStartNoSearch_Leave
        private void txtStartNoSearch_Leave(object sender, EventArgs e)
        {
            txtStartNoSearch.Text = cmnService.J_FormatToString(Convert.ToInt32(cmnService.J_NumericData(txtStartNoSearch)), "0");
        }

        #endregion
        
        #region txtLastNoSearch_KeyPress
        private void txtLastNoSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtStartNo, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtLastNoSearch_Leave
        private void txtLastNoSearch_Leave(object sender, EventArgs e)
        {
            txtLastNoSearch.Text = cmnService.J_FormatToString(Convert.ToInt32(cmnService.J_NumericData(txtLastNoSearch)), "0");
        }

        #endregion
        
        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        
        #endregion

        #region User Define Functions

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            strSQL = "SELECT COMPANY_ID," +
               "             COMPANY_NAME " +
               "     FROM    MST_COMPANY " +
               "     ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompanyName) == false) return;

            txtPrefix.Text = "";
            txtStartNo.Text = "0";
            //txtLastNo.Text = "0";
            mskLastDate.Text = "";
            txtDisplayText.Text = "";
            grpSort.Visible   = false;
            grpSearch.Visible = false;
            
            txtPrefixSearch.Text = "";
            txtStartNoSearch.Text = "0";
            txtLastNoSearch.Text = "0";
            mskLastDateSearch.Text = "";
            
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;   
            
            try
            {
                strSQL = "SELECT MST_INVOICE_SERIES.INVOICE_SERIES_ID   AS INVOICE_SERIES_ID," +
                    "            MST_COMPANY.COMPANY_ID                 AS COMPANY_ID," +
                    "            MST_COMPANY.COMPANY_NAME               AS COMPANY_NAME," + 
                    "            MST_INVOICE_SERIES.PREFIX              AS PREFIX," +
                    "            MST_INVOICE_SERIES.START_NO            AS START_NO," +
                    "            MST_INVOICE_SERIES.LAST_NO             AS LAST_NO," +
                    "            " + cmnService.J_SQLDBFormat("MST_INVOICE_SERIES.LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS LAST_DATE," +       
                    "            MST_INVOICE_SERIES.HEADER_DISPLAY_TEXT AS HEADER_DISPLAY_TEXT" +
                    "     FROM   MST_INVOICE_SERIES," +
                    "            MST_COMPANY" +
                    "     WHERE  MST_INVOICE_SERIES.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "     AND    MST_INVOICE_SERIES.BRANCH_ID   = " + J_Var.J_pBranchId + " " +
                    "     AND    MST_INVOICE_SERIES.INVOICE_SERIES_ID  = " + Id + " ";
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    cmbCompanyName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtPrefix.Text      = Convert.ToString(drdShowRecord["PREFIX"]);
                    txtStartNo.Text = Convert.ToString(drdShowRecord["START_NO"]);
                    //txtLastNo.Text      = Convert.ToInt32(drdShowRecord["LAST_NO"]);
                    mskLastDate.Text    = Convert.ToString(drdShowRecord["LAST_DATE"]);
                    txtDisplayText.Text = Convert.ToString(drdShowRecord["HEADER_DISPLAY_TEXT"]);

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();                    
                    
                    cmbCompanyName.Select();
                    return true;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    {
                        cmnService.J_UserMessage(J_Msg.DataNotFound);
                        if (dsetGridClone == null) return false;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                        return false;
                    }
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtPrefixSearch.Text.Trim() == "" &&
                            Convert.ToInt32(cmnService.J_NumericData(txtStartNoSearch)) == 0 &&
                            Convert.ToInt32(cmnService.J_NumericData(txtLastNoSearch)) == 0 &&
                            mskLastDateSearch.Text == "  /  /")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtPrefixSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    // COMPANY NAME VALIDATION
                    if (cmbCompanyName.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please select the Company.");
                        cmbCompanyName.Select();
                        return false;
                    }

                    // Prefix Name
                    if (txtPrefix.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Prefix.");
                        txtPrefix.Select();
                        return false;
                    }
                    // START_NO 
                    if (txtStartNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Start No.");
                        txtStartNo.Select();
                        return false;
                    }
                    else
                    {
                        if (Convert.ToInt32(txtStartNo.Text) <= 0)
                        {
                            cmnService.J_UserMessage("Start can't be zero.");
                            txtStartNo.Select();
                            return false;

                        }
                    }
                    // LAST DATE VALIDATION 
                    if (mskLastDate.Text != "  /  /")
                    {
                        if (dtService.J_IsDateValid(mskLastDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter the valid Last date.");
                            mskLastDate.Select();
                            return false;
                        }
                    }
                    // DISPLAY TEXT
                    if (txtDisplayText.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Header Display Text.");
                        txtDisplayText.Select();
                        return false;
                    }
                    

                    // Duplicacy check with repect to the mode
                    if (lblMode.Text == J_Mode.Add)
                    {
                        // Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_INVOICE_SERIES",
                            "    PREFIX          = '" + cmnService.J_ReplaceQuote(txtPrefix.Text) + "' " +
                            "AND COMPANY_ID         = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId ) == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtPrefix.Select();
                            return false;
                        }
                        
                    }
                    else if (lblMode.Text == J_Mode.Edit)
                    {
                        //  Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_INVOICE_SERIES",
                            "    PREFIX          = '" + cmnService.J_ReplaceQuote(txtPrefix.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId + " " +
                            "AND COMPANY_ID         = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                            "AND INVOICE_SERIES_ID           <>  " + lngSearchId + "") == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtPrefix.Select();
                            return false;
                        }
                    }                  
                   
                    return true;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {

                string strLastDate = string.Empty;
                //-----------------------------------------------------------
                if (mskLastDate.Text == "  /  /")
                {
                    strLastDate = "null";
                }
                else
                {
                    strLastDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskLastDate.Text) + cmnService.J_DateOperator();

                }

                switch (lblMode.Text)
                {
                       
                    case J_Mode.Add:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtPrefix) == true) return;
                        
                        // set the transaction as begin
                        dmlService.J_BeginTransaction();                       

                        
                        // Area Code Logic
                        //mf.MF_GetAreaCode(dmlService.J_pCommand, J_Var.J_pBranchCode)

                        // insert query & execution
                        strSQL = "INSERT INTO MST_INVOICE_SERIES (" +
                                 "            BRANCH_ID," +
                                 "            COMPANY_ID," +
                                 "            PREFIX," +
                                 "            START_NO," +
                                 "            LAST_NO," + 
                                 "            LAST_DATE," +
                                 "            HEADER_DISPLAY_TEXT," +
                                 "            USER_ID," +
                                 "            CREATE_DATE) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "            " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtPrefix.Text.Trim()) + "'," +
                                 "            " + Convert.ToInt32(cmnService.J_NumericData(txtStartNo)) + "," +
                                 "            " + Convert.ToInt32(cmnService.J_NumericData(txtStartNo)) + "," +
                                 "            " + strLastDate + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtDisplayText.Text.Trim()) + "'," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            return;
                        }
                        
                        // get last inserted area id as per branch & user
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_INVOICE_SERIES", "INVOICE_SERIES_ID",
                            "    BRANCH_ID         = " + J_Var.J_pBranchId + " " +                           
                            "AND USER_ID           = " + J_Var.J_pUserId + "");
                        if (lngSearchId == 0) return;
                        
                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        // all controls are cleared
                        ClearControls();
                        cmbCompanyName.Select();
                        
                        break;
                    case J_Mode.Edit:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtPrefix) == true) return;

                        // set the transaction as begin
                        dmlService.J_BeginTransaction();

                        // update query & execution
                        strSQL = "UPDATE MST_INVOICE_SERIES " +
                                 "SET    COMPANY_ID          =  " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "       PREFIX              = '" + cmnService.J_ReplaceQuote(txtPrefix.Text.Trim()) + "'," +
                                 "       START_NO            =  " + Convert.ToInt32(cmnService.J_NumericData(txtStartNo)) + "," +
                                 "       LAST_NO             =  " + Convert.ToInt32(cmnService.J_NumericData(txtStartNo)) + "," +
                                 "       LAST_DATE           = "  + strLastDate + "," +
                                 "       HEADER_DISPLAY_TEXT = '" + cmnService.J_ReplaceQuote(txtDisplayText.Text.Trim()) + "' " +
                                 "WHERE  INVOICE_SERIES_ID   =  " + lngSearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            return;
                        }

                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.EditModeSave);

                        // all controls are cleared
                        ClearControls();
                        
                        // Refresh the view grid
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        
                        // change the buttons status
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        
                        BtnDelete.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        
                        ControlVisible(false);
                        
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_SERIES_ID", lngSearchId);
                        break;
                    case J_Mode.Delete:
                        break;
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion     

       #endregion               
      

    }
}

