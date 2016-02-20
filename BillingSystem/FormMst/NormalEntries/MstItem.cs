
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Arup 
Module Name		: MstItem
Version			: 2.0
Start Date		: 09-06-2010
End Date		: 09-06-2010
Last Modified   : 
Tables Used     : MST_ITEM, 
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
    public partial class MstItem : BillingSystem.FormGen.GenForm
    {
        #region System Generated Code
        public MstItem()
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
        long lngDispatchEmailDetailId = 0; 
        //
        string    strActiveOnly="";
        #endregion

        #region User Defined Events

        #region MstItem_Load
        private void MstItem_Load(object sender, System.EventArgs e)
        {
            try
            {
                ViewGrid.Size = new Size(1004, 548);
                //-------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ControlVisible(false);
                ClearControls();
                //
                // set the Help Grid Column Header Text & behavior
                // (0) Header Text
                // (1) Width
                // (2) Format
                // (3) Alignment
                // (4) NullToText
                // (5) Visible
                // (6) AutoSizeMode
                string[,] strMatrix1 = {{"ItemID", "0", "", "Right", "", "", ""},
                                        {"CompanyID", "0", "", "Right", "", "", ""}, 
							            {"Item Name", "250", "S", "", "", "", ""},
                                        {"Company Name", "250", "", "", "", "", ""},
							            {"Rate", "80", "0.00", "Right", "", "", ""},
                                        {"Unit", "40", "", "", "", "", ""},
                                        {"Status", "75", "", "", "", "", ""},
                                        {"Online", "50", "", "", "", "", ""},
                                        {"", "70", "", "", "", "", ""},
                                        {"Email Category", "120", "", "", "", "", ""}};
                strMatrix = strMatrix1;

                strOrderBy = "MST_ITEM.ITEM_NAME";
                strQuery = "SELECT MST_ITEM.ITEM_ID         AS ITEM_ID," +
                    "              MST_COMPANY.COMPANY_ID   AS COMPANY_ID," +
                    "              MST_ITEM.ITEM_NAME       AS ITEM_NAME," +
                    "              MST_COMPANY.COMPANY_NAME AS COMPANY_NAME," +
                    "              MST_ITEM.RATE            AS RATE," +
                    "              MST_ITEM.UNIT            AS UNIT, " +
                    "              CASE WHEN MST_ITEM.INACTIVE_FLAG = 0 THEN 'ACTIVE' " +
                    "                   ELSE 'INACTIVE' " +
                    "              END AS ACTIVE, " +
                    "              CASE WHEN MST_ITEM.ONLINE_FLAG = 0 THEN '' " +
                    "                   ELSE 'YES' " +
                    "              END AS ACTIVE, " +
                    "              CASE WHEN MST_ITEM.DEFAULT_ITEM_ONLINE_OFFLINE_BILLING = 0 THEN '' " +
                    "                   ELSE 'DEFAULT' " +
                    "              END AS ACTIVE, " +
                    "              MST_EMAIL_CATEGORY.EMAIL_TYPE_DESC AS EMAIL_TYPE_DESC " +
                    "       FROM   MST_ITEM INNER JOIN MST_COMPANY " +
                    "              ON MST_ITEM.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                    "              LEFT JOIN MST_EMAIL_CATEGORY " +
                    "              ON MST_ITEM.EMAIL_TYPE_ID = MST_EMAIL_CATEGORY.EMAIL_TYPE_ID " +
                    "       WHERE  MST_ITEM.BRANCH_ID  = " + J_Var.J_pBranchId + " ";
                //
                strActiveOnly = " AND MST_ITEM.INACTIVE_FLAG = 0 ";
                //if (chkActive.Checked == true)
                //    strQuery = strQuery + " AND MST_ITEM.INACTIVE_FLAG = 0 ";
                //
                 strSQL = strQuery + strActiveOnly + " ORDER BY " + strOrderBy;                
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
                    
                    // A particular id wise retrieving the data from database
                    if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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

                strSQL = strQuery + strActiveOnly + " order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId) == false)
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

                grpSort.Location = new Point(648, 454);

                rbnSortItemName.Checked = false;              
                rbnSortAsEntered.Checked = false;

                if (strOrderBy == "MST_ITEM.ITEM_NAME")
                    rbnSortItemName.Select();
                else if (strOrderBy == "MST_ITEM.ITEM_ID")
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
                if (rbnSortItemName.Checked == true)
                    strOrderBy = "MST_ITEM.ITEM_NAME";             
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "MST_ITEM.ITEM_ID";
                
                if (strCheckFields == "")
                    strSQL = strQuery + strActiveOnly + " order by " + strOrderBy;
                else
                    strSQL = strQuery + strActiveOnly + strCheckFields + " order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
                    strSQL = strQuery + strActiveOnly + " order by " + strOrderBy;
                else
                    strSQL = strQuery + strActiveOnly + strCheckFields + " order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
                grpSearch.Location = new Point(589, 459);
                txtItemNameSearch.Select();
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
                if (txtItemNameSearch.Text.Trim() != "")
                    strCheckFields = " AND MST_ITEM.ITEM_NAME like '%" + cmnService.J_ReplaceQuote(txtItemNameSearch.Text.Trim().ToUpper()) + "%' ";

                strSQL = strQuery + strActiveOnly + strCheckFields + " ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId) == false)
                {
                    txtItemNameSearch.Select();
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
                    strSQL = strQuery + strActiveOnly + " order by " + strOrderBy;
                else
                    strSQL = strQuery + strActiveOnly + strCheckFields + " order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
                strSQL = strQuery + strActiveOnly + " order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.Item, this.Text);
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

        #region txtRate_KeyPress
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtRate, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtRate_Leave
        private void txtRate_Leave(object sender, EventArgs e)
        {
            txtRate.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtRate)), "0.00");
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


        #region txtRate2_KeyPress
        private void txtRate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtRate2, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtNonSalePercent_KeyPress
        private void txtNonSalePercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,4,2", txtNonSalePercent, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtSalePercent_KeyPress
        private void txtSalePercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,4,2", txtSalePercent, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtRate2_Leave
        private void txtRate2_Leave(object sender, EventArgs e)
        {
            txtRate2.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtRate2)), "0.00");
        }
        #endregion

        #region txtSalePercent_Leave
        private void txtSalePercent_Leave(object sender, EventArgs e)
        {
            txtSalePercent.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtSalePercent)), "0.00");
        }
        #endregion

        #region txtSalePercent_Leave
        private void txtNonSalePercent_Leave(object sender, EventArgs e)
        {
            txtNonSalePercent.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtNonSalePercent)), "0.00");
        }
        #endregion


        #region txtReOrderLevel_KeyPress
        private void txtReOrderLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,0", txtReOrderLevel, "") == false)
                e.Handled = true;
        }

        #endregion

        #region txtReOrderLevel_Leave
        private void txtReOrderLevel_Leave(object sender, EventArgs e)
        {
            txtReOrderLevel.Text = cmnService.J_FormatToString(Convert.ToInt32(cmnService.J_NumericData(txtReOrderLevel)), "0");
        }
        #endregion

        #region chkActive_CheckedChanged
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                strActiveOnly = " AND MST_ITEM.INACTIVE_FLAG = 0 ";
            else
                strActiveOnly = " AND MST_ITEM.INACTIVE_FLAG >= 0 ";
            //
            //
            strSQL = strQuery + strActiveOnly + " ORDER BY " + strOrderBy;
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);
        }
        #endregion

        #endregion

        #region User Define Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

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
            //-----------------------------------------------------------------------------------
            strSQL = @" SELECT INVOICE_STATUS_ID,INVOICE_STATUS
                        FROM 
                        (	SELECT 0 AS INVOICE_STATUS_ID,'ACTIVE' AS INVOICE_STATUS
	                        UNION 
	                        SELECT 1 AS INVOICE_STATUS_ID,'INACTIVE' AS INVOICE_STATUS
                        )STATUS ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbActiveInactive) == false) return;
            //-----------------------------------------------------------------------------------
            strSQL = @"SELECT EMAIL_DETAIL_ID,
                              DISPLAYNAME
                       FROM   MST_EMAIL_DETAIL 
                       ORDER BY DISPLAYNAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbDispatchEmailByProductName) == false) return;
            //-----------------------------------------------------------------------------------
            txtItemName.Text = "";
            txtRate.Text = "0.00";
            txtUnit.Text = "";
            cmbActiveInactive.SelectedIndex = 1;
            txtRate2.Text = "0.00";
            txtNonSalePercent.Text = "0.00";
            txtSalePercent.Text = "0.00";
            //
            txtDownloadLink1.Text = "";
            txtDownloadLink2.Text = "";
            chkDefaultItem.Checked = false; 
            chkAvailableItem.Checked = false;
            //
            strSQL = @"SELECT EMAIL_TYPE_ID,
                              EMAIL_TYPE_DESC
                       FROM   MST_EMAIL_CATEGORY 
                       ORDER BY EMAIL_TYPE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbEmailCategory) == false) return;
            txtPDFDocumentName.Text = "";
            //
            grpSort.Visible   = false;
            grpSearch.Visible = false;
            
            txtItemNameSearch.Text = "";
            
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;   
            
            try
            {
                strSQL = "SELECT MST_ITEM.ITEM_ID             AS ITEM_ID," +
                    "            MST_COMPANY.COMPANY_ID       AS COMPANY_ID," +
                    "            MST_COMPANY.COMPANY_NAME     AS COMPANY_NAME," + 
                    "            MST_ITEM.ITEM_NAME           AS ITEM_NAME," +
                    "            MST_ITEM.RATE                AS RATE," +
                    "            MST_ITEM.UNIT                AS UNIT," +
                    "            MST_ITEM.RATE_2              AS RATE_2," +
                    "            MST_ITEM.SALE_PERCENT        AS SALE_PERCENT," +
                    "            MST_ITEM.NON_SALE_PERCENT    AS NON_SALE_PERCENT," +
                    "            MST_ITEM.EMAIL_DETAIL_ID     AS EMAIL_DETAIL_ID,"+
                    "            MST_EMAIL_DETAIL.DISPLAYNAME AS DISPLAYNAME,"+
                    "            MST_ITEM.INACTIVE_FLAG       AS INACTIVE_FLAG," +
                    "            MST_ITEM.REORDER_LEVEL       AS REORDER_LEVEL," +
                    "            MST_ITEM.DOWNLOAD_LINK1      AS DOWNLOAD_LINK1," +
                    "            MST_ITEM.DOWNLOAD_LINK2      AS DOWNLOAD_LINK2," +
                    "            MST_ITEM.ONLINE_FLAG         AS ONLINE_FLAG," +
                    "            MST_ITEM.DEFAULT_ITEM_ONLINE_OFFLINE_BILLING AS DEFAULT_ITEM_ONLINE_OFFLINE_BILLING," +
                    "            MST_EMAIL_CATEGORY.EMAIL_TYPE_DESC AS EMAIL_TYPE_DESC," +
                    "            MST_ITEM.PDF_DOC_NAME        AS PDF_DOC_NAME " +
                    "     FROM   MST_ITEM " +
                    "            INNER JOIN MST_COMPANY " +
                    "            ON MST_ITEM.COMPANY_ID       = MST_COMPANY.COMPANY_ID " +
                    "            LEFT JOIN MST_EMAIL_DETAIL " +
                    "            ON MST_ITEM.EMAIL_DETAIL_ID  = MST_EMAIL_DETAIL.EMAIL_DETAIL_ID " +
                    "            LEFT JOIN MST_EMAIL_CATEGORY " +
                    "            ON MST_ITEM.EMAIL_TYPE_ID  = MST_EMAIL_CATEGORY.EMAIL_TYPE_ID " + 
                    "     WHERE  MST_ITEM.BRANCH_ID        = " + J_Var.J_pBranchId + " " +
                    "     AND    MST_ITEM.ITEM_ID          = " + Id + " ";


                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    cmbCompanyName.Text = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtItemName.Text    = Convert.ToString(drdShowRecord["ITEM_NAME"]);
                    txtRate.Text        = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["RATE"])); 
                    txtUnit.Text        = Convert.ToString(drdShowRecord["UNIT"]);
                    txtRate2.Text       = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["RATE_2"]));
                    txtSalePercent.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["SALE_PERCENT"]));
                    txtNonSalePercent.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["NON_SALE_PERCENT"]));
                    cmbDispatchEmailByProductName.Text = Convert.ToString(drdShowRecord["DISPLAYNAME"]);
                    txtReOrderLevel.Text = string.Format("{0:0}", Convert.ToDouble(drdShowRecord["REORDER_LEVEL"]));
                    txtDownloadLink1.Text = Convert.ToString(drdShowRecord["DOWNLOAD_LINK1"]);
                    txtDownloadLink2.Text = Convert.ToString(drdShowRecord["DOWNLOAD_LINK2"]);
                    //
                    if (Convert.ToInt32(drdShowRecord["INACTIVE_FLAG"]) == 0)
                        cmbActiveInactive.SelectedIndex = 1;
                    else
                        cmbActiveInactive.SelectedIndex = 2;

                    if (Convert.ToInt32(drdShowRecord["ONLINE_FLAG"]) == 0)
                        chkAvailableItem.Checked = false;
                    else
                        chkAvailableItem.Checked = true;

                    if (Convert.ToInt32(drdShowRecord["DEFAULT_ITEM_ONLINE_OFFLINE_BILLING"]) == 0)
                        chkDefaultItem.Checked = false;
                    else
                        chkDefaultItem.Checked = true;

                    cmbEmailCategory.Text = Convert.ToString(drdShowRecord["EMAIL_TYPE_DESC"]);
                    txtPDFDocumentName.Text = Convert.ToString(drdShowRecord["PDF_DOC_NAME"]);

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
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtItemNameSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtItemNameSearch.Select();
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
                        cmnService.J_UserMessage("Please select the company.");
                        cmbCompanyName.Select();
                        return false;
                    }

                    // Item Name
                    if (txtItemName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the item name.");
                        txtItemName.Select();
                        return false;
                    }

                    // Duplicacy check with repect to the mode
                    if (lblMode.Text == J_Mode.Add)
                    {
                        // Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_ITEM",
                            "    ITEM_NAME          = '" + cmnService.J_ReplaceQuote(txtItemName.Text) + "' " +
                            "AND COMPANY_ID         = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId) == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtItemName.Select();
                            return false;
                        }

                    }
                    else if (lblMode.Text == J_Mode.Edit)
                    {
                        //  Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_ITEM",
                            "    ITEM_NAME          = '" + cmnService.J_ReplaceQuote(txtItemName.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId + " " +
                            "AND COMPANY_ID         = " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + " " +
                            "AND ITEM_ID           <>  " + lngSearchId + "") == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtItemName.Select();
                            return false;
                        }
                    }

                    // Rate 
                    if (txtRate.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the rate.");
                        txtRate.Select();
                        return false;
                    }
                    else if (Convert.ToDouble(txtRate.Text) <= 0)
                    {
                        cmnService.J_UserMessage("Rate can't be zero.");
                        txtRate.Select();
                        return false;

                    }
                    
                    // Unit 
                    if (txtUnit.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the unit.");
                        txtUnit.Select();
                        return false;
                    }

                    if (cmbActiveInactive.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please select the Invoice Status.");
                        cmbActiveInactive.Select();
                        return false;
                    }
                    //--
                    if (chkAvailableItem.Checked == true)
                    {
                        if (txtDownloadLink1.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Download Link 1 cannot be blank when 'Item available for Online/Offline Billing' is checked");
                            txtDownloadLink1.Select();
                            return false;
                        }
                        //
                        if (txtDownloadLink2.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Download Link 2 cannot be blank when 'Item available for Online/Offline Billing' is checked");
                            txtDownloadLink2.Select();
                            return false;
                        }
                        //
                        if (cmbEmailCategory.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Email Category cannot be blank when 'Item available for Online/Offline Billing' is checked");
                            cmbEmailCategory.Select();
                            return false;
                        }
                        //
                        if (cmbDispatchEmailByProductName.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Despatch Email By Item cannot be blank when 'Item available for Online/Offline Billing' is checked");
                            cmbDispatchEmailByProductName.Select();
                            return false;
                        }
                        //                        
                    }
                    if (chkDefaultItem.Checked == true)
                    {
                        if (chkAvailableItem.Checked == false)
                        {
                            cmnService.J_UserMessage("Item should be available first for Online/Offline Billing before making it default");
                            chkDefaultItem.Select();
                            return false;
                        }
                        //--
                        strSQL = "SELECT ITEM_NAME FROM MST_ITEM WHERE DEFAULT_ITEM_ONLINE_OFFLINE_BILLING = 1 ";                        
                        if (lblMode.Text == J_Mode.Edit)
                            strSQL = strSQL + " AND ITEM_ID <>  " + lngSearchId + "";
                        //--
                        if (Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) != "")
                        {
                            cmnService.J_UserMessage("Item : " + Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) + " already default");
                            chkDefaultItem.Select();
                            return false;
                        }          
                    }
                    //
                    if (cmbActiveInactive.Text == "INACTIVE")
                    {
                        if (chkAvailableItem.Checked == true)
                        {
                            cmnService.J_UserMessage("Item which is available for Online/Offline Billing can't be Inactivated");
                            chkAvailableItem.Select();
                            return false;
                        }
                    }
                    //--
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
            string strDefaultItem = "0";
            string strAvailableItem = "0";
            //
            try
            {
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        //
                        if (chkDefaultItem.Checked == true)
                            strDefaultItem = "1";
                        //
                        if (chkAvailableItem.Checked == true)
                            strAvailableItem = "1";
                        //
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtItemName) == true) return;
                        
                        // set the transaction as begin
                        dmlService.J_BeginTransaction();
                        
                        // Area Code Logic
                        //mf.MF_GetAreaCode(dmlService.J_pCommand, J_Var.J_pBranchCode)

                        // insert query & execution
                        strSQL = "INSERT INTO MST_ITEM (" +
                                 "            BRANCH_ID," +
                                 "            COMPANY_ID," +
                                 "            ITEM_NAME," +
                                 "            RATE," +
                                 "            UNIT," +
                                 "            RATE_2," +
                                 "            SALE_PERCENT," +
                                 "            NON_SALE_PERCENT," +
                                 "            EMAIL_DETAIL_ID," +
                                 "            REORDER_LEVEL,"+
                                 "            USER_ID," +
                                 "            CREATE_DATE," +
                                 "            INACTIVE_FLAG," +
                                 "            DOWNLOAD_LINK1," +
                                 "            DOWNLOAD_LINK2," +
                                 "            ONLINE_FLAG," +
                                 "            DEFAULT_ITEM_ONLINE_OFFLINE_BILLING," +
                                 "            EMAIL_TYPE_ID," +
                                 "            PDF_DOC_NAME) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "            " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtItemName.Text.Trim()) + "'," +
                                 "            " + Convert.ToDouble(txtRate.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtUnit.Text.Trim()) + "'," +
                                 "            " + Convert.ToDouble(txtRate2.Text) + "," +
                                 "            " + Convert.ToDouble(txtSalePercent.Text) + "," +
                                 "            " + Convert.ToDouble(txtNonSalePercent.Text) + "," +
                                 "            " + Convert.ToInt32(Support.GetItemData(cmbDispatchEmailByProductName, cmbDispatchEmailByProductName.SelectedIndex)) + "," +
                                 "            " + Convert.ToInt32(txtReOrderLevel.Text) + "," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + "," +
                                 "            " + Convert.ToInt32(Support.GetItemData(cmbActiveInactive, cmbActiveInactive.SelectedIndex)) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtDownloadLink1.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtDownloadLink2.Text.Trim()) + "'," +
                                 "            " + Convert.ToInt32(strAvailableItem) + "," +
                                 "            " + Convert.ToInt32(strDefaultItem) + "," +
                                 "            " + Convert.ToInt32(Support.GetItemData(cmbEmailCategory, cmbEmailCategory.SelectedIndex)) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtPDFDocumentName.Text.Trim()) + "')";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbCompanyName.Select();
                            return;
                        }
                        
                        // get last inserted area id as per branch & user
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_ITEM", "ITEM_ID",
                                                                  " BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                                                                  " AND USER_ID       = " + J_Var.J_pUserId + "");
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
                        //
                        if (chkDefaultItem.Checked == true)
                            strDefaultItem = "1";
                        //
                        if (chkAvailableItem.Checked == true)
                            strAvailableItem = "1";
                        //
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtItemName) == true) return;

                        // set the transaction as begin
                        dmlService.J_BeginTransaction();

                        // update query & execution
                        strSQL = "UPDATE MST_ITEM " +
                                 "SET    COMPANY_ID         =  " + Convert.ToInt32(Support.GetItemData(cmbCompanyName, cmbCompanyName.SelectedIndex)) + "," +
                                 "       ITEM_NAME          = '" + cmnService.J_ReplaceQuote(txtItemName.Text.Trim()) + "'," +
                                 "       RATE               =  " + Convert.ToDouble(txtRate.Text.Trim()) + "," +
                                 "       UNIT               = '" + cmnService.J_ReplaceQuote(txtUnit.Text.Trim()) + "'," +
                                 "       RATE_2             =  " + Convert.ToDouble(txtRate2.Text.Trim()) + "," +
                                 "       SALE_PERCENT       =  " + Convert.ToDouble(txtSalePercent.Text.Trim()) + "," +
                                 "       NON_SALE_PERCENT   =  " + Convert.ToDouble(txtNonSalePercent.Text.Trim()) + "," +
                                 "       EMAIL_DETAIL_ID    =  " + Convert.ToInt32(Support.GetItemData(cmbDispatchEmailByProductName, cmbDispatchEmailByProductName.SelectedIndex)) + "," +
                                 "       REORDER_LEVEL      =  " + Convert.ToDouble(txtReOrderLevel.Text.Trim()) + "," +
                                 "       INACTIVE_FLAG      =  " + Convert.ToInt32(Support.GetItemData(cmbActiveInactive, cmbActiveInactive.SelectedIndex)) + "," +
                                 "       DOWNLOAD_LINK1     = '" + cmnService.J_ReplaceQuote(txtDownloadLink1.Text.Trim()) + "'," +
                                 "       DOWNLOAD_LINK2     = '" + cmnService.J_ReplaceQuote(txtDownloadLink2.Text.Trim()) + "'," +
                                 "       ONLINE_FLAG        =  " + Convert.ToInt32(strAvailableItem) + "," +
                                 "       DEFAULT_ITEM_ONLINE_OFFLINE_BILLING = " + Convert.ToInt32(strDefaultItem) + "," +
                                 "       EMAIL_TYPE_ID      =  " + Convert.ToInt32(Support.GetItemData(cmbEmailCategory, cmbEmailCategory.SelectedIndex)) + "," +
                                 "       PDF_DOC_NAME       = '" + cmnService.J_ReplaceQuote(txtPDFDocumentName.Text.Trim()) + "' " +
                                 "WHERE  ITEM_ID            =  " + lngSearchId + "";
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
                        strSQL = strQuery + strActiveOnly + " ORDER BY " + strOrderBy;
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        
                        // change the buttons status
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        
                        BtnDelete.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        
                        ControlVisible(false);
                        
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "ITEM_ID", lngSearchId);
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

