#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnSerialNoStatus
Version			: 2.0
Start Date		: 07-03-2015
End Date		: 
Tables Used     : 
Module Desc		: Serial No. Status
____________________________________________________________________________________________________________________

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
using System.Text;
using System.Net.Mail;

// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.FormCmn;
using BillingSystem.Classes;
using BillingSystem.FormTrn.PopUp;

using JAYA.VB;

#endregion

namespace BillingSystem.FormTrn.NormalEntries
{
    public partial class TrnDepstchCD : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnDepstchCD()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        // Variables and objects declaration
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();

        DataSet dsetGridClone = new DataSet();

        JVBCommon mainVB = new JVBCommon();
        BS BillingSystem = new BS(); 

        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        

        int intTempGridPosition = 0;
        bool blnExit = false;

        long lngSearchId = 0;

        string[,] strMatrix;
        //
        string SearchAllText = "Press Alt + F";
        //
        string strSearch = "";
        //string strCheckBox = "";
        string strCD = ""; string strSerialNo = ""; string strInvoice = "";
        //--
                
        #endregion

        #region User Defined Events

        #region BankEntry_Load
        private void BankEntry_Load(object sender, EventArgs e)
        {
            ViewGrid.Height = 468;
            //
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //
            BtnAdd.Enabled = false;
            BtnSearch.Enabled = false;
            BtnSort.Enabled = false;
            BtnPrint.Enabled = false;
            BtnDelete.Enabled = false;
            BtnAdd.BackColor = Color.LightGray;
            BtnSort.BackColor = Color.LightGray;
            BtnSearch.BackColor = Color.LightGray;
            BtnPrint.BackColor = Color.LightGray;
            BtnDelete.BackColor = Color.LightGray;
            //
            lblTitle.Text = this.Text;

            strSQL = "SELECT COMPANY_ID," +
                         "       COMPANY_NAME " +
                         "FROM   MST_COMPANY " +
                         "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                         "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                         "ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;

            //cmbCompany.Enabled = true;
            BtnSort.BackColor = Color.LightGray;
            BtnSort.Enabled = false;
            BtnPrint.BackColor = Color.LightGray;
            BtnPrint.Enabled = false;

            //chkCancelledEntry.Checked = false;

            cmbCompany.Select();
        }
        #endregion

        #region BtnAdd_Click
        public void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompany.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select Comapany");
                    cmbCompany.Select();
                    return;
                }
                
                //cmbCompany.Enabled = false;                
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;

                ControlVisible(true);
                ClearControls();					//Clear all the Controls

                strCheckFields = "";
                //---------------------------------------------
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
                    // A particular id wise retriving the data from database
                    if (ShowRecord(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                        //
                        //cmbItemName.Enabled = true;
                    }
                    //
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    //
                    grpTopPanel.Enabled = false;
                    cmbInvoiceSeries.Enabled = false;
                    cmbCompany.Enabled = false;
                    grpAllPending.Enabled = false;
                    mskDespatchDate.Select();
                    strCheckFields = "";
                }
                else
                {
                    cmnService.J_UserMessage("No record selected");
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId) == false)
                    BtnCancel.Select();

                //cmbCompany.Enabled = true;
                //
                BtnAdd.Enabled = false;
                BtnSearch.Enabled = false;
                BtnSort.Enabled = false;
                BtnPrint.Enabled = false;
                BtnDelete.Enabled = false;
                BtnAdd.BackColor = Color.LightGray; 
                BtnSort.BackColor = Color.LightGray;
                BtnSearch.BackColor = Color.LightGray;
                BtnPrint.BackColor = Color.LightGray;
                BtnDelete.BackColor = Color.LightGray;
                //

                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }

        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    lblMode.Text = J_Mode.Delete;

                    //Insert_Update_Delete_Data();

                    lblSearchMode.Text = J_Mode.General;
                    grpSort.Visible = false;
                    grpSearch.Visible = false;

                    ViewGrid_Click(sender, e);
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EXCISE_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data();
        }
        #endregion

        #region cmbItemName_SelectedIndexChanged
        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnExit == false)
                return;
            //--
            //LoadGrid();
            //--
        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
            {
                BtnExit.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());

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
                lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);

                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

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
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
        }
        #endregion

        #region ViewGrid_MouseMove

        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                // set view mode
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                lblSearchMode.Text = J_Mode.General;
                //
                BtnAdd.Enabled = false;
                BtnSearch.Enabled = false;
                BtnSort.Enabled = false;
                BtnPrint.Enabled = false;
                BtnDelete.Enabled = false;
                BtnAdd.BackColor = Color.LightGray; 
                BtnSort.BackColor = Color.LightGray;
                BtnSearch.BackColor = Color.LightGray;
                BtnPrint.BackColor = Color.LightGray;
                BtnDelete.BackColor = Color.LightGray;
                // clear controls
                ClearControls();

                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                //cmbCompany.Enabled = true;
                BtnSort.BackColor = Color.LightGray;
                BtnSort.Enabled = false;
                BtnPrint.BackColor = Color.LightGray;
                BtnPrint.Enabled = false;


                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
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

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // set searching mode
                lblSearchMode.Text = J_Mode.Searching;

                // validate fields
                //if (ValidateFields() == false) return;

                grpSort.Visible = false;
                grpSearch.Visible = true;

                mskDespatchDate.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region txtAmountSearch_KeyPress
        private void txtAmountSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtAmountSearch, "") == false)
                e.Handled = true;
        }
        #endregion

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate fields
                //if (ValidateFields() == false) return;

                strCheckFields = "";

                if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDateSearch) + cmnService.J_DateOperator() + " ";
                if (dtService.J_IsBlankDateCheck(ref mskAccountEntryDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.BANK_STATEMENT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDateSearch) + cmnService.J_DateOperator() + " ";
                if (dtService.J_IsBlankDateCheck(ref mskBankDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankDateSearch) + cmnService.J_DateOperator() + " ";
                if (txtBankSearch.Text.Trim() != "")
                    strCheckFields += "AND MST_BANK.BANK_NAME like '%" + cmnService.J_ReplaceQuote(txtBankSearch.Text.Trim().ToUpper()) + "%' ";
                if(cmnService.J_ReturnDoubleValue(txtAmountSearch.Text == ""? "0":txtAmountSearch.Text) > 0)
                    strCheckFields += "AND TRN_INVOICE_HEADER.NET_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtAmountSearch.Text) + " ";
                if (txtReferenceSearch.Text.Trim() != "")
                    strCheckFields += strCheckFields + "AND TRN_INVOICE_HEADER.REFERENCE_NO like '%" + cmnService.J_ReplaceQuote(txtReferenceSearch.Text.Trim().ToUpper()) + "%' ";
                if (txtSearchRemarks.Text.Trim() != "")
                    strCheckFields += strCheckFields + "AND TRN_INVOICE_HEADER.REMARKS like '%" + cmnService.J_ReplaceQuote(txtSearchRemarks.Text.Trim().ToUpper()) + "%' ";

                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId) == false)
                {
                    mskDespatchDate.Select();
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
        private void BtnSearchOK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // set general mode
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;

                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion


        #region txtSearchAll_TextChanged
        private void txtSearchAll_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //----------------------------------------------------------------------
                //if (blnSearchAll == false)
                //    return;
                //--
                if (txtSearchAll.Text == SearchAllText)
                    return;
                //--
                if (txtSearchAll.Text.Length > 0)
                    txtSearchAll.BackColor = Color.White;
                else
                    txtSearchAll.BackColor = Color.AliceBlue;
                //--
                strSearch = "AND (TRN_INVOICE_HEADER.INVOICE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.INVOICE_DATE LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.PARTY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.REFERENCE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.CONTACT_PERSON LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.EMAIL_ID LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.MOBILE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
                //--
                strSQL = strQuery + strSearch + " ORDER BY " + strOrderBy;
                //--
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //--
                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "DEDUCTEE_ID", lngSearchId) == false)
                //{
                //    txtPANSearch.Select();
                //    return;
                //}
                //----------------------------------------------------------------------
            }
            catch
            {
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                txtSearchAll.Text = "";
                chkCD.Checked = false;
                chkSerialNo.Checked = false;
                chkInvoice.Checked = false;
                cmnService.J_ClearComboBox(ref cmbInvoiceSeries);
            }

            strSQL = "SELECT INVOICE_SERIES_ID," +
                     "       PREFIX " +
                     "FROM   MST_INVOICE_SERIES " +
                     "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                     "AND    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "ORDER BY PREFIX DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbInvoiceSeries, 1) == false) return;
        }
        #endregion 

        #region cmbInvoiceSeries_SelectedIndexChanged
        private void cmbInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                txtSearchAll.Text = "";
                chkCD.Checked = false;
                chkSerialNo.Checked = false;
                chkInvoice.Checked = false;                
                LoadGrid(0, 0);
                return;
            }
            //--
            if (cmbInvoiceSeries.SelectedIndex <= 0)
            {
                txtSearchAll.Text = "";
                chkCD.Checked = false;
                chkSerialNo.Checked = false;
                chkInvoice.Checked = false; 
                LoadGrid(0, 0);
                return;
            }
            //--
            txtSearchAll.Text = "";
            chkCD.Checked = false;
            chkSerialNo.Checked = false;
            chkInvoice.Checked = false; 
            //--
            LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex), cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex));
        }
        #endregion


        #region chkCD_CheckedChanged
        private void chkCD_CheckedChanged(object sender, EventArgs e)
        {
            string CD="0";
            //
            if (chkCD.Checked == true)
                CD = "1";
            else
                CD = "0,1";
            //
            strCD = " AND TRN_INVOICE_HEADER.SEND_CD IN (" + CD + ")";
            //--
            strSQL = strQuery + strSearch + strCD + strSerialNo + strInvoice + " ORDER BY " + strOrderBy;
            //--
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return;            
        }
        #endregion

        #region chkSerialNo_CheckedChanged
        private void chkSerialNo_CheckedChanged(object sender, EventArgs e)
        {
            string SerialNo = "0";
            //
            if (chkSerialNo.Checked == true)
                SerialNo = "1";
            else
                SerialNo = "0,1";
            //
            strSerialNo = " AND TRN_INVOICE_HEADER.SEND_SERIAL IN (" + SerialNo + ")";
            //--
            strSQL = strQuery + strSearch + strCD + strSerialNo + strInvoice + " ORDER BY " + strOrderBy;
            //--
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return;
        }
        #endregion

        #region chkInvoice_CheckedChanged
        private void chkInvoice_CheckedChanged(object sender, EventArgs e)
        {
            string Invoice = "0";
            //
            if (chkInvoice.Checked == true)
                Invoice = "1";
            else
                Invoice = "0,1";
            //
            strInvoice = " AND TRN_INVOICE_HEADER.SEND_INVOICE IN (" + Invoice + ")";
            //--
            strSQL = strQuery + strSearch + strCD + strSerialNo + strInvoice + " ORDER BY " + strOrderBy;
            //--
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return;
        }
        #endregion

        #endregion

        #region User Defined Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region CurrencyControl_KeyPress
        private void CurrencyControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtNumeric = (TextBox)sender;

            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtNumeric, "") == false)
                e.Handled = true;
        }
        #endregion

        #region NumericCurrencyControl_Leave
        private void NumericCurrencyControl_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            if (txtBox.Text == "." || txtBox.Text == "") txtBox.Text = "0.00";
            txtBox.Text = string.Format("{0:0.00}", Convert.ToDouble(cmnService.J_NumericData(txtBox)));

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
            mskInvoiceDateSearch.Text = "";
            mskAccountEntryDateSearch.Text = "";
            mskBankDateSearch.Text = "";
            txtBankSearch.Text = "";
            txtAmountSearch.Text = "";
            txtReferenceSearch.Text = "";
            txtSearchRemarks.Text = "";
            //
            mskRequestDate.Text = "";
            mskDespatchDate.Text = "";
            txtInvoiceDate.Text = "";
            txtInvoiceNo.Text = "";
            txtReferenceNo.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtPartyName.Text = "";
            txtContactPerson.Text = "";
            //
            chkRequestCD.Checked = false;
            chkRequestSerialNo.Checked = false;
            chkRequestInvoice.Checked = false;
            grpTopPanel.Enabled = true;
            cmbInvoiceSeries.Enabled = true;
            cmbCompany.Enabled = true; 
            grpAllPending.Enabled = true;
            //
            txtTrackingNo.Text = "";
            strSQL = "SELECT COURIER_ID, COURIER_DESC FROM MST_COURIER ORDER BY COURIER_ID";
            dmlService.J_PopulateComboBox(strSQL, ref cmbCourierName);
            cmbCourierName.SelectedIndex = 0;
            //--
            //chkCancelled.Visible = false;
            //chkCancelled.Checked = false;

        }
        #endregion

        #region ControlSearch_KeyPress
        private void ControlSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region LoadGrid
        private void LoadGrid(long CompanyID, long InvoiceSeriesID)
        {
            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode
            //txtSearchAll.Text = "";
            string[,] strMatrix1 =  {{"INVOICE_HEADER_ID", "0", "", "Left", "", "", ""},
                                     {"Invoice No.", "110", "", "Left", "", "", ""},
							         {"Invoice Date", "70", "", "Left", "", "", ""},
                                     {"Reference No.", "110", "", "Left", "", "", ""},
                                     {"Party Name", "120", "", "Left", "", "", ""},
                                     {"Contact Person", "120", "", "Left", "", "", ""},
                                     {"Email", "140", "", "Left", "", "", ""},
                                     {"Mobile", "74", "", "Right", "", "", ""},
							         {"Desp Dt", "70", "", "Left", "", "", ""},
                                     {"", "20", "", "CENTER", "", "", ""},
                                     {"", "50", "", "CENTER", "", "", ""},
                                     {"", "60", "", "CENTER", "", "", ""}};
            //
            string[,] strCDMatrix = {{"=0", "N", "", "T"},
                                     {"=1", "N", "CD", "T"}};
            //
            string[,] strInvoiceMatrix = {{"=0", "N", "", "T"},
                                     {"=1", "N", "INV", "T"}};
            //
            string[,] strSerialMatrix = {{"=0", "N", "", "T"},
                                     {"=1", "N", "SLNO", "T"}};
            //
            strMatrix = strMatrix1;
            //
            strOrderBy = " TRN_INVOICE_HEADER.INVOICE_NO DESC";
            //
            strQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                TRN_INVOICE_HEADER.INVOICE_NO,
                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE,
                                TRN_INVOICE_HEADER.REFERENCE_NO,
                                MST_PARTY.PARTY_NAME,
                                TRN_INVOICE_HEADER.CONTACT_PERSON,
                                TRN_INVOICE_HEADER.EMAIL_ID,
                                TRN_INVOICE_HEADER.MOBILE_NO,
                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.DESPATCH_DATE,103) AS DESPATCH_DATE,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.SEND_CD", J_SQLColFormat.Case_End, strCDMatrix) + @" AS SEND_CD,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.SEND_INVOICE", J_SQLColFormat.Case_End, strInvoiceMatrix) + @" AS SEND_INVOICE,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.SEND_SERIAL", J_SQLColFormat.Case_End, strSerialMatrix) + @" AS SEND_SERIAL
                        FROM    TRN_INVOICE_HEADER,
                                MST_PARTY 
                        WHERE   TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID 
                        AND     TRN_INVOICE_HEADER.COMPANY_ID        = " + CompanyID + @"
                        AND     TRN_INVOICE_HEADER.INVOICE_SERIES_ID = " + InvoiceSeriesID + @"
                        AND     TRN_INVOICE_HEADER.DELIVERY_MODE_ID  > 0
                        AND     TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'
                        AND     TRN_INVOICE_HEADER.REQUEST_DATE IS NOT NULL ";
            if (rbnPending.Checked == true)
                strQuery = strQuery + " AND TRN_INVOICE_HEADER.DESPATCH_DATE IS NULL ";
            else if (rbnDespatched.Checked == true)
                strQuery = strQuery + " AND TRN_INVOICE_HEADER.DESPATCH_DATE IS NOT NULL ";
            //
            //if (chkCD.Checked == true)
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_CD = 1 ";
            //else
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_CD = 0 ";
            ////
            //if (chkInvoice.Checked == true)
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_INVOICE = 1 ";
            //else
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_INVOICE = 0 ";
            ////
            //if (chkSerialNo.Checked == true)
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_SERIAL = 1 ";
            //else
            //    strQuery = strQuery + " AND TRN_INVOICE_HEADER.SEND_SERIAL = 0 ";
            //
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //
        }

        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            try
            {
                // SQL Query
                strSQL = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID AS INVOICE_HEADER_ID,              
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS INVOICE_DATE, 
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PARTY_NAME,
                              TRN_INVOICE_HEADER.DELIVERY_MODE_ID      AS DELIVERY_MODE_ID,
                              TRN_INVOICE_HEADER.INVOICE_NO            AS INVOICE_NO,
                              TRN_INVOICE_HEADER.REFERENCE_NO          AS REFERENCE_NO,
                              MST_PARTY.CONTACT_PERSON                 AS CONTACT_PERSON,
                              MST_PARTY.EMAIL_ID                       AS EMAIL_ID,
                              MST_PARTY.MOBILE_NO                      AS MOBILE_NO,
                              TRN_INVOICE_HEADER.SEND_CD               AS SEND_CD,
                              TRN_INVOICE_HEADER.SEND_INVOICE          AS SEND_INVOICE,
                              TRN_INVOICE_HEADER.SEND_SERIAL           AS SEND_SERIAL,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.REQUEST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS REQUEST_DATE,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.DESPATCH_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS DESPATCH_DATE,
                              PAR_DELIVERY_MODE.DELIVERY_MODE_DESC    AS DELIVERY_MODE_DESC,
                              MST_COURIER.COURIER_DESC                AS COURIER_DESC,
                              TRN_INVOICE_HEADER.TRACKING_NO          AS TRACKING_NO
                       FROM   (((TRN_INVOICE_HEADER
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID         = MST_PARTY.PARTY_ID)
                       INNER JOIN PAR_DELIVERY_MODE 
                               ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID = PAR_DELIVERY_MODE.DELIVERY_MODE_ID)
                       LEFT JOIN MST_COURIER 
                               ON TRN_INVOICE_HEADER.COURIER_ID = MST_COURIER.COURIER_ID)
                       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + Id;
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;

                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    txtInvoiceDate.Text = drdShowRecord["INVOICE_DATE"].ToString();
                    txtInvoiceNo.Text = drdShowRecord["INVOICE_NO"].ToString();
                    txtReferenceNo.Text = drdShowRecord["REFERENCE_NO"].ToString();
                    txtPartyName.Text = drdShowRecord["PARTY_NAME"].ToString();
                    txtContactPerson.Text = drdShowRecord["CONTACT_PERSON"].ToString();
                    txtMobile.Text = drdShowRecord["MOBILE_NO"].ToString();
                    txtEmail.Text = drdShowRecord["EMAIL_ID"].ToString();
                    //--
                    if (drdShowRecord["SEND_CD"].ToString() == "1")
                        chkRequestCD.Checked = true;
                    else
                        chkRequestCD.Checked = false;
                    //--
                    if (drdShowRecord["SEND_INVOICE"].ToString() == "1")
                        chkRequestInvoice.Checked = true;
                    else
                        chkRequestInvoice.Checked = false;
                    //--
                    if (drdShowRecord["SEND_SERIAL"].ToString() == "1")
                        chkRequestSerialNo.Checked = true;
                    else
                        chkRequestSerialNo.Checked = false;
                    //
                    mskRequestDate.Text = drdShowRecord["REQUEST_DATE"].ToString();
                    //
                    mskDespatchDate.Text = drdShowRecord["DESPATCH_DATE"].ToString();
                    if(drdShowRecord["DESPATCH_DATE"].ToString() == "")
                        chkSendEmail.Checked = true;
                    else
                        chkSendEmail.Checked = false;
                    //
                    lblBillingType.Text = drdShowRecord["DELIVERY_MODE_DESC"].ToString();
                    //
                    cmbCourierName.Text = drdShowRecord["COURIER_DESC"].ToString();
                    txtTrackingNo.Text = drdShowRecord["TRACKING_NO"].ToString();
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    return true;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();

                cmnService.J_UserMessage(J_Msg.RecNotExist);

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid

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

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;

                        //--Assigning Cancellation Status to a variable

                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------                        
                        //int intRequestCD = 0; int intRequestSerialNo = 0; int intRequestInvoiceNo = 0;
                        //string strRequestDate = "null";
                        string strDespatchDate = "null";
                        string strRequestDate = "null";
                        //
                        //if (chkRequestCD.Checked == true)
                        //    intRequestCD = 1;
                        //if (chkRequestSerialNo.Checked == true)
                        //    intRequestSerialNo = 1;
                        //if (chkRequestInvoice.Checked == true)
                        //    intRequestInvoiceNo = 1;
                        //
                        ////if(intRequestCD == 1 || intRequestSerialNo == 1 || intRequestInvoiceNo == 1)
                        ////    if (!dtService.J_IsBlankDateCheck(ref mskRequestDate, J_ShowMessage.NO))
                        ////        strRequestDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskRequestDate.Text) + cmnService.J_DateOperator();

                        if (!dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                            strDespatchDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDespatchDate.Text) + cmnService.J_DateOperator();

                        if (!dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                            strRequestDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskRequestDate.Text) + cmnService.J_DateOperator();


                        //UPDATING HEADER RECORD
                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    DESPATCH_DATE =  " + strDespatchDate + ", " +
                                 "       REQUEST_DATE  =  " + strRequestDate + ", " +
                                 "       COURIER_ID    =  " + cmnService.J_GetComboBoxItemId(ref cmbCourierName, cmbCourierName.SelectedIndex) + ", " +
                                 "       TRACKING_NO   = '" + cmnService.J_ReplaceQuote(txtTrackingNo.Text) + "', " +
                                 "       DESPATCH_USER_ID = " + J_Var.J_pUserId + " " +
                                 "WHERE  INVOICE_HEADER_ID = " + lngSearchId;
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            mskDespatchDate.Select();
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
                        if (chkSendEmail.Checked == true && strDespatchDate!= "null")
                        {
                            string strFROM_EMAIL = ""; string strDISPLAY_NAME = ""; string strEMAIL_BCC = "";
                            string strEMAIL_SUBJECT_DESPATCH = ""; string strEMAIL_BODY_DESPATCH = ""; 
                            string strProduct_Name = "";string strCourier_Name = ""; string strCourier_Site = "";
                            string strTracking_No = "";
                            strSQL = @"SELECT MST_EMAIL_CATEGORY.FROM_EMAIL,
                                              MST_EMAIL_CATEGORY.DISPLAY_NAME,
                                              MST_EMAIL_CATEGORY.EMAIL_BCC,
                                              MST_EMAIL_CATEGORY.EMAIL_SUBJECT_DESPATCH,
                                              MST_EMAIL_CATEGORY.EMAIL_BODY_DESPATCH,
                                              MST_ITEM.ITEM_NAME,
                                              MST_COURIER.COURIER_DESC,
                                              MST_COURIER.COURIER_TRACK_SITE,
                                              TRN_INVOICE_HEADER.TRACKING_NO
                                       FROM   TRN_INVOICE_HEADER, 
                                              TRN_INVOICE_DETAIL, 
                                              MST_EMAIL_CATEGORY, 
                                              MST_ITEM,
                                              MST_COURIER 
                                       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID 
                                       AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID 
                                       AND    MST_ITEM.EMAIL_TYPE_ID               = MST_EMAIL_CATEGORY.EMAIL_TYPE_ID 
                                       AND    TRN_INVOICE_HEADER.COURIER_ID        = MST_COURIER.COURIER_ID
                                       AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + lngSearchId;
                            //
                            IDataReader drdShowRecord = null;
                            drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                            //
                            if (drdShowRecord == null) return;
                            //
                            while (drdShowRecord.Read())
                            {
                                strFROM_EMAIL = drdShowRecord["FROM_EMAIL"].ToString();
                                strDISPLAY_NAME = drdShowRecord["DISPLAY_NAME"].ToString();
                                strEMAIL_BCC = drdShowRecord["EMAIL_BCC"].ToString();
                                strEMAIL_SUBJECT_DESPATCH = drdShowRecord["EMAIL_SUBJECT_DESPATCH"].ToString();
                                strEMAIL_BODY_DESPATCH = drdShowRecord["EMAIL_BODY_DESPATCH"].ToString();
                                strProduct_Name = drdShowRecord["ITEM_NAME"].ToString();
                                strCourier_Name = drdShowRecord["COURIER_DESC"].ToString();
                                strCourier_Site = drdShowRecord["COURIER_TRACK_SITE"].ToString();
                                strTracking_No  = drdShowRecord["TRACKING_NO"].ToString();
                            } 
                            drdShowRecord.Close();
                            drdShowRecord.Dispose();
                            //
                            SendEmail(strFROM_EMAIL, 
                                      strDISPLAY_NAME, 
                                      strEMAIL_BCC, 
                                      strEMAIL_SUBJECT_DESPATCH, 
                                      strEMAIL_BODY_DESPATCH, 
                                      txtEmail.Text, 
                                      txtContactPerson.Text,
                                      strProduct_Name,
                                      strCourier_Name,
                                      strCourier_Site,
                                      strTracking_No,
                                      chkRequestCD.Checked,
                                      chkRequestInvoice.Checked,
                                      chkRequestSerialNo.Checked);
                        }
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //--
                        ClearControls();  
                        //
                        BtnAdd.Enabled = false;
                        BtnSearch.Enabled = false;
                        BtnSort.Enabled = false;
                        BtnPrint.Enabled = false;
                        BtnDelete.Enabled = false;
                        BtnAdd.BackColor = Color.LightGray; 
                        BtnSort.BackColor = Color.LightGray;
                        BtnSearch.BackColor = Color.LightGray;
                        BtnPrint.BackColor = Color.LightGray;
                        BtnDelete.BackColor = Color.LightGray;                      
                        //cmbCompany.Enabled = true;
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
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

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {                    
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {                    
                    return true;
                }
                else
                {
                    if (dtService.J_IsBlankDateCheck(ref mskRequestDate, J_ShowMessage.NO))
                    {
                        cmnService.J_UserMessage("Despatch is not possible if Request has not been made !!");
                        BtnCancel.Select();
                        return false;
                        
                    }

                    if (dtService.J_IsDateValid(mskRequestDate)== false)
                    {
                        cmnService.J_UserMessage("Please Enter a valid Request Date !!");
                        mskRequestDate.Select();
                        return false;
                    }
                    //
                    if (!dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                    {
                        if (dtService.J_IsDateValid(mskDespatchDate.Text) == false)
                        {
                            cmnService.J_UserMessage("Enter the valid date.");
                            mskDespatchDate.Select();
                            return false;
                        }
                    }

                    //else
                    //{
                    //    cmnService.J_UserMessage("Enter the Despatch date.");
                    //    mskRequestDate.Select();
                    //    return false;
                    //}
                    //--
                    //if (chkRequestCD.Checked == false && chkRequestInvoice.Checked == false && chkRequestSerialNo.Checked == false)
                    //{
                    //    cmnService.J_UserMessage("Atleast one among CD / Invoice / Serial No. should be checked");
                    //    chkRequestCD.Select();
                    //    return false;
                    //}
                    //--
                    //if (!dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                    //{
                    //    if (dtService.J_IsDateValid(mskDespatchDate.Text) == false)
                    //    {
                    //        cmnService.J_UserMessage("Enter the valid date.");
                    //        mskDespatchDate.Select();
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        if (chkRequestCD.Checked == false && chkRequestInvoice.Checked == false && chkSerialNo.Checked == false)
                    //        {
                    //            cmnService.J_UserMessage("Any of the CD / Invoice / Serial No. should be checked for Despatch Date");
                    //            chkRequestCD.Select();
                    //            return false;
                    //        }

                    //    }
                    //}
                    
                    // Despatch Date can never be earlier than Request Date
                    if (!dtService.J_IsBlankDateCheck(ref mskRequestDate, J_ShowMessage.NO) &&
                        !dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                    {
                        if (dtService.J_IsDateGreater(ref mskRequestDate, ref mskDespatchDate, "", "", "", J_ShowMessage.NO) == false)
                        {
                            cmnService.J_UserMessage("Despatch Date can never be earlier than Request Date");
                            mskDespatchDate.Select();
                            return false;
                        }
                    }
                    //
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

        #region SendEmail
        private bool SendEmail(string EmailFrom, 
                               string DisplayName, 
                               string EmailIdBcc,
                               string EmailSubject,
                               string EmailBody,
                               string EmailIdTo,
                               string ContactPerson,
                               string ProductName,
                               string CourierName,
                               string CourierTrackingSite,
                               string CourierTrackingNo,
                               bool CD,
                               bool Invoice,
                               bool SerialNo)
        {
            //------------------------------------------------------------------
            SmtpClient SmtpServer = new SmtpClient();
            //------------------------------------------------------------------
            MailMessage mail = new MailMessage();
            //--
            try
            {
                SmtpServer.Credentials = new System.Net.NetworkCredential
                         (Convert.ToString(J_Var.J_pNetworkCredential_Username), Convert.ToString(J_Var.J_pNetworkCredential_Password));
                SmtpServer.Port = Convert.ToInt32(J_Var.J_pNetworkCredential_Port);
                SmtpServer.Host = J_Var.J_pNetworkCredential_Host;
                //--------------------------------------------------------------------
                if (EmailIdTo.EndsWith(",") == true)
                    EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
                //--------------------------------------------------------------------
                if (EmailIdBcc.EndsWith(",") == true)
                    EmailIdBcc = EmailIdBcc.Substring(0, EmailIdBcc.Length - 1);
                //--------------------------------------------------------------------
                if (EmailIdTo.Trim() == "") return false;
                //--------------------------------------------------------------------
                //this.Cursor = Cursors.Default;
                //if (cmnService.J_UserMessage("Do you want to send email to [" + EmailIdTo + "] ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return false;
                this.Cursor = Cursors.WaitCursor;
                //
                //this.Cursor = Cursors.WaitCursor;
                //
                //strEmail = EmailFrom;
                ////strWebSite = ProductWebsite;
                //strDisplayName = DisplayName;
                //strImageURL = ProductImageUrl;
                //


                //------------------------------------------------------------------------------
                //MODIFIED BY DHRUB FOR THOSE WHO HAVE THE SAME PARTY_NAME AND CONTACT PERSON
                //------------------------------------------------------------------------------
                if (txtPartyName.Text.Trim() == ContactPerson.Trim())
                    EmailBody = EmailBody.Replace("CUST_NAME,", "");
                else
                    EmailBody = EmailBody.Replace("CUST_NAME", ContactPerson);

                if (ContactPerson.Trim() == "")
                    ContactPerson = "Sir";
                else
                    ContactPerson = BillingSystem.ToTitleCase(ContactPerson.Trim());
                //
                
                EmailBody = EmailBody.Replace("CONTAT_PERSON", txtPartyName.Text.Trim());
                //EmailBody = EmailBody.Replace("ORDER_NO", OrderNO);
                EmailBody = EmailBody.Replace("PRODUCT_NAME", ProductName);
                EmailBody = EmailBody.Replace("COURIER_DESC", CourierName);
                EmailBody = EmailBody.Replace("DESPATCH_DATE", mskDespatchDate.Text.Trim());
                EmailBody = EmailBody.Replace("TRACKING_NO", CourierTrackingNo);
                EmailBody = EmailBody.Replace("COURIER_TRACK_SITE", CourierTrackingSite);
                //
                //strHTMLText = strHTMLText.Replace("PRODUCT_NAME", ProductName);
                //strHTMLText = strHTMLText.Replace("DWNLD_LINK", DownloadLink1);
                if (CD == false)
                    EmailBody = EmailBody.Replace("<li><p>Software CD</p></li>", "");
                //
                if (Invoice == false)
                    EmailBody = EmailBody.Replace("<li><p>Invoice</p></li>", "");
                //
                if (SerialNo == false)
                    EmailBody = EmailBody.Replace("<li><p>License key of the software</p></li>", "");
                //
                //
                mail.From = new MailAddress(EmailFrom, DisplayName, System.Text.Encoding.UTF8);
                //mail.Priority = MailPriority.High;
                mail.ReplyTo = new MailAddress(EmailFrom);
                //
                if (EmailIdBcc.Trim() != "")
                {
                    //mail.Bcc.Add("mukherjee.dhrub@gmail.com");
                    mail.Bcc.Add(EmailIdBcc);
                }
                //
                mail.To.Add(EmailIdTo);
                //-- BILL ATTACHMENT
                //FileStream fs = new FileStream(strPdfPath, FileMode.Open, FileAccess.Read);
                //Attachment a = new Attachment(fs, InvoiceNo.Replace("/", "-").Trim() + ".PDF", MediaTypeNames.Application.Octet);
                //mail.Attachments.Add(a);
                //--
                //if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                //{
                //    //-- PDF ATTACHMENT
                //    PDFFileName = Path.Combine(Application.StartupPath, PDFFileName);
                //    if (File.Exists(PDFFileName) == true)
                //    {
                //        FileStream fs1 = new FileStream(PDFFileName, FileMode.Open, FileAccess.Read);
                //        Attachment a1 = new Attachment(fs1, Path.GetFileName(PDFFileName), MediaTypeNames.Application.Octet);
                //        mail.Attachments.Add(a1);
                //    }
                //}
                //
                mail.Subject = EmailSubject;
                StringBuilder htmlString = new StringBuilder();
                // 
                #region EMAIL BODY
                //
                htmlString.Append(EmailBody);
                //
                #endregion
                //
                mail.Body = htmlString.ToString();
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Timeout = 300000;//(5min) 
                SmtpServer.Send(mail);
                //
                this.Cursor = Cursors.Default;
                //
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage(err.ToString());
                //this.Cursor = Cursors.Default;
                return false;
            }
        }
        #endregion

        #endregion




}
}

