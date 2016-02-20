
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Arup 
Module Name		: MstCompany
Version			: 2.0
Start Date		: 09-06-2010
End Date		: 09-06-2010
Last Modified   : 
Tables Used     : MST_COMPANY, 
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
    public partial class MstCompany : BillingSystem.FormGen.GenForm
    {
        #region System Generated Code
        public MstCompany()
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

        #region MstCompany_Load
        private void MstCompany_Load(object sender, System.EventArgs e)
        {
            try
            {
                //ADDED BY DHRUB ON 03/05/2014 FOR Default Grid Size
                ViewGrid.Size = new Size(1004, 554);

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
                string[,] strMatrix1 = {{"CompanyID", "0", "", "Right", "", "", ""},
							            {"Company Name", "280", "S", "", "", "", ""},
                                        {"PAN", "100", "S", "", "", "", ""},
                                        {"Address", "200", "", "", "", "", ""},
							            {"VAT No.", "90", "S", "", "", "", ""},
                                        {"CST No.", "90", "", "", "", "", ""},
							            {"Service Tax No.", "130", "S", "", "", "", ""},
                                        {"CIN No.", "90", "", "", "", "", ""}};
                strMatrix = strMatrix1;
                
                strOrderBy = "COMPANY_NAME";
                strQuery = "SELECT COMPANY_ID     AS COMPANY_ID," +
                    "              COMPANY_NAME   AS COMPANY_NAME," +
                    "              PAN            AS PAN," + 
                    "              ADDRESS1       AS ADDRESS1," +
                    "              VAT_NO         AS VAT_NO," +
                    "              CST_NO         AS CST_NO," +
                    "              SERVICE_TAX_NO AS SERVICE_TAX_NO," +
                    "              CIN_NO         AS CIN_NO," +    
                    "              CONTACT_NO     AS CONTACT_NO"+
                    "       FROM   MST_COMPANY " +                  
                    "       WHERE  MST_COMPANY.BRANCH_ID         = " + J_Var.J_pBranchId + " ";
                
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
                txtCompanyName.Select();
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
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId) == false)
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
                
                rbnSortCompanyName.Checked = false;              
                rbnSortAsEntered.Checked = false;
                
                if (strOrderBy == "COMPANY_NAME")
                    rbnSortCompanyName.Select();              
                else if (strOrderBy == "COMPANY_ID")
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
                if (rbnSortCompanyName.Checked == true)
                    strOrderBy = "COMPANY_NAME";             
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "COMPANY_ID";
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                
                txtCompanyNameSearch.Select();
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
                if (txtCompanyNameSearch.Text.Trim() != "")
                    strCheckFields = "AND MST_COMPANY.COMPANY_NAME like '%" + cmnService.J_ReplaceQuote(txtCompanyNameSearch.Text.Trim().ToUpper()) + "%' ";
                     
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId) == false)
                {
                    txtCompanyNameSearch.Select();
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.Company, this.Text);
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

        #region txtMaxPermitedDays_KeyPress
        private void txtMaxPermitedDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,3,0", txtMaxPermitedDays, "") == false)
                e.Handled = true;
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
            txtCompanyName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtPin.Text = "";
            txtContactNo.Text = "";
            txtFax.Text = "";
            txtEmailID.Text = "";
            txtWebsite.Text = "";
            txtContactPersonName.Text = "";
            txtSignatory.Text = "";
            txtVATNo.Text = "";
            txtCSTNo.Text = "";
            txtServiceTaxNo.Text = "";
            txtPAN.Text = "";
            txtCINNo.Text = "";
            //-- 2016/02/18 ANIK
            txtBankDetails1.Text = "";
            txtBankDetails2.Text = "";
            txtBankDetails3.Text = "";
            txtBankDetails4.Text = "";
            txtBankDetails5.Text = "";

            grpSort.Visible   = false;
            grpSearch.Visible = false;
            
            txtCompanyNameSearch.Text = "";
            
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            
            string strStateName    = string.Empty;
            string strDistrictName = string.Empty;
            
            try
            {
                strSQL = "SELECT COMPANY_ID     AS COMPANY_ID," +                   
                    "            COMPANY_NAME   AS COMPANY_NAME," +
                    "            ADDRESS1       AS ADDRESS1," +
                    "            ADDRESS2       AS ADDRESS2," +
                    "            ADDRESS3       AS ADDRESS3," +
                    "            CITY           AS CITY," +
                    "            PIN            AS PIN," +
                    "            CONTACT_NO     AS CONTACT_NO," +
                    "            FAX            AS FAX," +
                    "            EMAIL_ID       AS EMAIL_ID," +
                    "            WEB_SITE       AS WEB_SITE," +
                    "            CONTACT_PERSON AS CONTACT_PERSON," +
                    "            SIGNATORY      AS SIGNATORY," +
                    "            VAT_NO         AS VAT_NO," +
                    "            CST_NO         AS CST_NO," +
                    "            SERVICE_TAX_NO AS SERVICE_TAX_NO," +
                    "            PAN            AS PAN, " +
                    "            CIN_NO         AS CIN_NO, "+ 
                    "            MAX_DAYS_PERMIT AS MAX_DAYS_PERMIT,"+
                    "            BANK_DETAIL1   AS BANK_DETAIL1," + //-- 2016/02/18 ANIK
                    "            BANK_DETAIL2   AS BANK_DETAIL2," +
                    "            BANK_DETAIL3   AS BANK_DETAIL3," +
                    "            BANK_DETAIL4   AS BANK_DETAIL4," +
                    "            BANK_DETAIL5   AS BANK_DETAIL5 " +
                    "     FROM   MST_COMPANY" +                    
                    "     WHERE  MST_COMPANY.BRANCH_ID   = " + J_Var.J_pBranchId + " " +
                    "     AND    MST_COMPANY.COMPANY_ID  = " + Id + " ";
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    txtCompanyName.Text       = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtAddress1.Text          = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text          = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text          = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtCity.Text              = Convert.ToString(drdShowRecord["CITY"]);
                    txtPin.Text               = Convert.ToString(drdShowRecord["PIN"]);
                    txtContactNo.Text         = Convert.ToString(drdShowRecord["CONTACT_NO"]);
                    txtFax.Text               = Convert.ToString(drdShowRecord["FAX"]);
                    txtEmailID.Text           = Convert.ToString(drdShowRecord["EMAIL_ID"]);
                    txtWebsite.Text           = Convert.ToString(drdShowRecord["WEB_SITE"]);
                    txtContactPersonName.Text = Convert.ToString(drdShowRecord["CONTACT_PERSON"]);
                    txtSignatory.Text         = Convert.ToString(drdShowRecord["SIGNATORY"]);
                    txtVATNo.Text             = Convert.ToString(drdShowRecord["VAT_NO"]);
                    txtCSTNo.Text             = Convert.ToString(drdShowRecord["CST_NO"]);
                    txtServiceTaxNo.Text      = Convert.ToString(drdShowRecord["SERVICE_TAX_NO"]);
                    txtPAN.Text               = Convert.ToString(drdShowRecord["PAN"]);
                    txtCINNo.Text             = Convert.ToString(drdShowRecord["CIN_NO"]);
                    txtMaxPermitedDays.Text = Convert.ToString(drdShowRecord["MAX_DAYS_PERMIT"]);
                    //-- 2016/02/18 ANIK
                    txtBankDetails1.Text = Convert.ToString(drdShowRecord["BANK_DETAIL1"]);
                    txtBankDetails2.Text = Convert.ToString(drdShowRecord["BANK_DETAIL2"]); ;
                    txtBankDetails3.Text = Convert.ToString(drdShowRecord["BANK_DETAIL3"]); ;
                    txtBankDetails4.Text = Convert.ToString(drdShowRecord["BANK_DETAIL4"]); ;
                    txtBankDetails5.Text = Convert.ToString(drdShowRecord["BANK_DETAIL5"]); ;
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    
                    txtCompanyName.Select();
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
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtCompanyNameSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtCompanyNameSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    // Company Name
                    if (txtCompanyName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Company Name.");
                        txtCompanyName.Select();
                        return false;
                    }
                    
                    

                    // Duplicacy check with repect to the mode
                    if (lblMode.Text == J_Mode.Add)
                    {
                        // Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_COMPANY",
                            "    COMPANY_NAME          = '" + cmnService.J_ReplaceQuote(txtCompanyName.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId ) == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtCompanyName.Select();
                            return false;
                        }
                        
                    }
                    else if (lblMode.Text == J_Mode.Edit)
                    {
                        //  Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_COMPANY",
                            "    COMPANY_NAME          = '" + cmnService.J_ReplaceQuote(txtCompanyName.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId + " " +                           
                            "AND COMPANY_ID           <>  " + lngSearchId + "") == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtCompanyName.Select();
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
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtCompanyName) == true) return;
                        
                        // set the transaction as begin
                        dmlService.J_BeginTransaction();
                        
                        // insert query & execution
                        strSQL = "INSERT INTO MST_COMPANY (" +
                                 "            BRANCH_ID," +
                                 "            COMPANY_NAME," +
                                 "            ADDRESS1," +
                                 "            ADDRESS2," +
                                 "            ADDRESS3," +
                                 "            CITY," +
                                 "            PIN," +
                                 "            CONTACT_NO," +
                                 "            FAX," +
                                 "            EMAIL_ID," +
                                 "            WEB_SITE," +
                                 "            CONTACT_PERSON," +
                                 "            SIGNATORY," +
                                 "            VAT_NO," +
                                 "            CST_NO," +
                                 "            SERVICE_TAX_NO," +
                                 "            PAN," +
                                 "            USER_ID," +
                                 "            CREATE_DATE, " +
                                 "            CIN_NO, " +
                                 "            MAX_DAYS_PERMIT, " +
                                 "            BANK_DETAIL1, " + //-- 2016/02/18 ANIK
                                 "            BANK_DETAIL2, " +
                                 "            BANK_DETAIL3, " +
                                 "            BANK_DETAIL4, " +
                                 "            BANK_DETAIL5)" +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtCompanyName.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtCity.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtPin.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtContactNo.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtFax.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtWebsite.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtSignatory.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtVATNo.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtCSTNo.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtServiceTaxNo.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtPAN.Text.Trim()) + "'," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ", " +
                                 "           '" + cmnService.J_ReplaceQuote(txtCINNo.Text.Trim()) + "'," +
                                 "            " + cmnService.J_ReplaceQuote(txtMaxPermitedDays.Text.Trim()) + ", " +
                                 "           '" + cmnService.J_ReplaceQuote(txtBankDetails1.Text.Trim()) + "', " + //-- 2016/02/18 ANIK
                                 "           '" + cmnService.J_ReplaceQuote(txtBankDetails2.Text.Trim()) + "', " +
                                 "           '" + cmnService.J_ReplaceQuote(txtBankDetails3.Text.Trim()) + "', " +
                                 "           '" + cmnService.J_ReplaceQuote(txtBankDetails4.Text.Trim()) + "', " +
                                 "           '" + cmnService.J_ReplaceQuote(txtBankDetails5.Text.Trim()) + "')";

                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtCompanyName.Select();
                            return;
                        }
                        
                        // get last inserted area id as per branch & user
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_COMPANY", "COMPANY_ID",
                            "    BRANCH_ID         = " + J_Var.J_pBranchId + " " +                           
                            "AND USER_ID           = " + J_Var.J_pUserId + "");
                        if (lngSearchId == 0) return;
                        
                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        // all controls are cleared
                        ClearControls();
                        txtCompanyName.Select();
                        
                        break;
                    case J_Mode.Edit:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtCompanyName) == true) return;

                        // set the transaction as begin
                        dmlService.J_BeginTransaction();

                        // update query & execution
                        strSQL = "UPDATE MST_COMPANY " +
                                 "SET    COMPANY_NAME   = '" + cmnService.J_ReplaceQuote(txtCompanyName.Text.Trim()) + "'," +
                                 "       ADDRESS1       = '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "       ADDRESS2       = '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "       ADDRESS3       = '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "       CITY           = '" + cmnService.J_ReplaceQuote(txtCity.Text.Trim()) + "'," +
                                 "       PIN            = '" + cmnService.J_ReplaceQuote(txtPin.Text.Trim()) + "'," +
                                 "       CONTACT_NO     = '" + cmnService.J_ReplaceQuote(txtContactNo.Text.Trim()) + "'," +
                                 "       FAX            = '" + cmnService.J_ReplaceQuote(txtFax.Text.Trim()) + "'," +
                                 "       EMAIL_ID       = '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + "'," +
                                 "       WEB_SITE       = '" + cmnService.J_ReplaceQuote(txtWebsite.Text.Trim()) + "'," +
                                 "       CONTACT_PERSON = '" + cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim()) + "'," +
                                 "       SIGNATORY      = '" + cmnService.J_ReplaceQuote(txtSignatory.Text.Trim()) + "'," +
                                 "       VAT_NO         = '" + cmnService.J_ReplaceQuote(txtVATNo.Text.Trim()) + "'," +
                                 "       CST_NO         = '" + cmnService.J_ReplaceQuote(txtCSTNo.Text.Trim()) + "'," +
                                 "       SERVICE_TAX_NO = '" + cmnService.J_ReplaceQuote(txtServiceTaxNo.Text.Trim()) + "'," +
                                 "       PAN            = '" + cmnService.J_ReplaceQuote(txtPAN.Text.Trim()) + "', " +
                                 "       CIN_NO         = '" + cmnService.J_ReplaceQuote(txtCINNo.Text.Trim()) + "', " +
                                 "       MAX_DAYS_PERMIT = " + cmnService.J_ReplaceQuote(txtMaxPermitedDays.Text.Trim()) + ", " +
                                 "       BANK_DETAIL1    = '" + cmnService.J_ReplaceQuote(txtBankDetails1.Text.Trim()) + "', " + //-- 2016/02/18 ANIK
                                 "       BANK_DETAIL2    = '" + cmnService.J_ReplaceQuote(txtBankDetails2.Text.Trim()) + "', " +
                                 "       BANK_DETAIL3    = '" + cmnService.J_ReplaceQuote(txtBankDetails3.Text.Trim()) + "', " +
                                 "       BANK_DETAIL4    = '" + cmnService.J_ReplaceQuote(txtBankDetails4.Text.Trim()) + "', " +
                                 "       BANK_DETAIL5    = '" + cmnService.J_ReplaceQuote(txtBankDetails5.Text.Trim()) + "' " +
                                 "WHERE  COMPANY_ID     =  " + lngSearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtCompanyName.Select();
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
                        
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
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

