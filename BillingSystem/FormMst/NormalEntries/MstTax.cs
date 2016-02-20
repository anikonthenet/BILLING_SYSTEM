
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: SHREY KEJRIWAL
Module Name		: MstTax
Version			: 2.0
Start Date		: 09-06-2011
End Date		: 
Tables Used     : MST_TAX
Module Desc		: Tax data is to be stored
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
    public partial class MstTax : BillingSystem.FormGen.GenForm
    {
        #region System Generated Code
        public MstTax()
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

        #region MstTax_Load
        private void MstTax_Load(object sender, System.EventArgs e)
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
                string[,] strMatrix1 = {{"TaxId", "0", "", "Right", "", "", ""},
							            {"Tax Description", "650", "S", "", "", "", ""},
							            {"Tax Percentage.", "100", "0.00", "Right", "", "", ""},
							            {"", "1", "", "Right", "", "", ""},
							            {"Created By", "200", "S", "", "", "", ""}};
                strMatrix = strMatrix1;

                strOrderBy = "MST_TAX.TAX_DESC";
                strQuery = "SELECT MST_TAX.TAX_ID        AS TAX_ID," +
                    "              MST_TAX.TAX_DESC      AS TAX_DESC," +
                    "              MST_TAX.TAX_RATE      AS TAX_RATE," +
                    "              '' ," +
                    "              MST_USER.DISPLAY_NAME AS DISPLAY_NAME" +
                    "       FROM   MST_TAX," +
                    "              MST_USER" +
                    "       WHERE  MST_TAX.USER_ID = MST_USER.USER_ID" +
                    "       AND    MST_TAX.BRANCH_ID  = " + J_Var.J_pBranchId + " ";
                
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
                txtTaxDesc.Select();
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
                    
                    // A particular id wise retrieving the data from database
                    if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId) == false)
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
                
                rbnTaxDesc.Checked = false;
                rbnTaxPercentage.Checked = false;
                rbnSortAsEntered.Checked = false;

                if (strOrderBy == "MST_TAX.TAX_DESC")
                    rbnTaxDesc.Select();
                else if (strOrderBy == "MST_TAX.TAX_RATE, MST_TAX.TAX_DESC")
                    rbnTaxPercentage.Select();
                else if (strOrderBy == "MST_TAX.TAX_ID")
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
                if (rbnTaxDesc.Checked == true)
                    strOrderBy = "MST_TAX.TAX_DESC";
                else if (rbnTaxPercentage.Checked == true)
                    strOrderBy = "MST_TAX.TAX_RATE, MST_TAX.TAX_DESC";
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "MST_TAX.TAX_ID";
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                    strSQL = strQuery + "ORDER BY " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                
                txtTaxDescSearch.Select();
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
                
                // Area name
                if (txtTaxDescSearch.Text.Trim() != "")
                    strCheckFields = "AND TAX_DESC like '%" + cmnService.J_ReplaceQuote(txtTaxDescSearch.Text.Trim().ToUpper()) + "%' ";
                
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId) == false)
                {
                    txtTaxDescSearch.Select();
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
            //cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.Tax, this.Text);
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

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTaxPercentage_KeyPress
        private void txtTaxPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,4,2", txtTaxPercentage, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtTaxPercentage_Leave
        private void txtTaxPercentage_Leave(object sender, EventArgs e)
        {
            if (txtTaxPercentage.Text.Trim() == "" || txtTaxPercentage.Text.Trim() == ".")
                txtTaxPercentage.Text = "0.00";
            else
                txtTaxPercentage.Text = string.Format("{0:0.00}", Convert.ToDouble(txtTaxPercentage.Text));
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
            txtTaxDesc.Text = "";
            txtTaxPercentage.Text = "0.00";

            grpSort.Visible   = false;
            grpSearch.Visible = false;
            
            txtTaxDescSearch.Text = "";
        }

        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            
            try
            {
                strSQL = "SELECT TAX_ID    AS TAX_ID," +
                    "            TAX_DESC  AS TAX_DESC," +
                    "            TAX_RATE  AS TAX_RATE" +
                    "     FROM   MST_TAX" +
                    "     WHERE  BRANCH_ID = " + J_Var.J_pBranchId+
                    "     AND    TAX_ID    = " + lngSearchId;
                
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    txtTaxDesc.Text = Convert.ToString(drdShowRecord["TAX_DESC"]);

                    txtTaxPercentage.Text = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowRecord["TAX_RATE"]) == "" ? "0.00" : Convert.ToString(drdShowRecord["TAX_RATE"])));
                    
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    txtTaxDesc.Select();
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
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtTaxDescSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtTaxDescSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    // Tax Description
                    if (txtTaxDesc.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter tax description");
                        txtTaxDesc.Select();
                        return false;
                    }

                    // Duplicacy check with repect to the tax description
                    if (lblMode.Text == J_Mode.Add)
                    {
                        if (dmlService.J_IsRecordExist("MST_TAX",
                            "    TAX_DESC  = '" + cmnService.J_ReplaceQuote(txtTaxDesc.Text) + "' " +
                            "AND BRANCH_ID =  " + J_Var.J_pBranchId + " ") == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateName);
                            txtTaxDesc.Select();
                            return false;
                        }
                    }
                    else if (lblMode.Text == J_Mode.Edit)
                    {
                        //  Duplicacy check with respect to district id
                        if (dmlService.J_IsRecordExist("MST_TAX",
                            "    TAX_DESC  = '" + cmnService.J_ReplaceQuote(txtTaxDesc.Text) + "' " +
                            "AND BRANCH_ID =  " + J_Var.J_pBranchId + " " +
                            "AND TAX_ID    <> " + lngSearchId) == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateName);
                            txtTaxDesc.Select();
                            return false;
                        }
                    }

                    // Tax Percentage
                    if (cmnService.J_ReturnDoubleValue(txtTaxPercentage.Text.Trim()) == 0)
                    {
                        cmnService.J_UserMessage("Please enter tax percentage");
                        txtTaxPercentage.Select();
                        return false;
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
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtTaxDesc) == true) return;
                        
                        // set the transaction as begin
                        dmlService.J_BeginTransaction();
                        
                        // Area Code Logic
                        //mf.MF_GetAreaCode(dmlService.J_pCommand, J_Var.J_pBranchCode)

                        // insert query & execution
                        strSQL = "INSERT INTO MST_TAX (" +
                                 "            BRANCH_ID," +
                                 "            TAX_DESC," +
                                 "            TAX_RATE," +
                                 "            USER_ID," +
                                 "            CREATE_DATE) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtTaxDesc.Text.Trim().ToUpper()) + "'," +
                                 "            " + Convert.ToDouble(cmnService.J_ReplaceQuote(txtTaxPercentage.Text.Trim())) + "," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtTaxDesc.Select();
                            return;
                        }
                        
                        // get last inserted tax id as per branch & user
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_TAX", "TAX_ID",
                            "    BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                            "AND USER_ID           = " + J_Var.J_pUserId + "");
                        if (lngSearchId == 0) return;
                        
                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        // all controls are cleared
                        ClearControls();
                        txtTaxDesc.Select();
                        
                        break;
                    case J_Mode.Edit:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtTaxDesc) == true) return;

                        // set the transaction as begin
                        dmlService.J_BeginTransaction();

                        // update query & execution
                        strSQL = "UPDATE MST_TAX " +
                                 "SET    TAX_DESC     = '" + cmnService.J_ReplaceQuote(txtTaxDesc.Text.Trim().ToUpper()) + "'," +
                                 "       TAX_RATE     =  " + Convert.ToDouble(cmnService.J_ReplaceQuote(txtTaxPercentage.Text.Trim())) + " " +
                                 "WHERE  TAX_ID       =  " + lngSearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtTaxDesc.Select();
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
                        
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "TAX_ID", lngSearchId);
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

