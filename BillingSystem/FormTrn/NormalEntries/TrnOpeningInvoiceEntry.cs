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
    public partial class TrnOpeningInvoiceEntry : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnOpeningInvoiceEntry()
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
        string strSQL;						    //For Storing the Local SQL Query
        string strQuery;			            //For Storing the general SQL Query
        string strOrderBy;					    //For Sotring the Order By Values
        string strCheckFields;				    //For Sotring the Where Values
        //
        string strDetailQuery;			        //For Storing the general SQL Query
        string strDetailQuery_UnionAl;          //For Storing the general SQL Query
        string strDetailSearchQuery;	        //For Storing the Detail Filter Query
        string strDetailSearchQuery_UnionAll;   //For Storing the Detail Filter Query
        string strDetailOrderBy;		    	//For Sorting the Order By Values
        string strDetailGroupBy;				//For Sorting the Order By Values
        string strDetailGroupBy_UnionAl;        //For Sorting the Order By Values
        string strDetailHaving;					//For Sorting the Order By Values
        string strDetailCheckFields;			//For Sorting the Where Values
        string[,] strMatrix;
        string strTaggedInvoiveId = "";
        string strTaggedCollectionDetailId = "";
        string SearchAllText = "Press Alt + F";
        string strSearch = "";
        //string strCheckBox = "";
        string strCD = ""; string strSerialNo = ""; string strInvoice = "";
        //-----------------------------------------------------------------------
        int intTempGridPosition = 0;
        int lngDetailGridColumns = 10;
        int intBankInCollectionFlag = 0;           
        int intReferenceNoFlag = 0;                
        //-----------------------------------------------------------------------
        long lngSearchId = 0;
        //-----------------------------------------------------------------------
        
        bool blnExit = false;
        bool blnIsAmount =  false;
        bool blnTagDeductee = false;
        bool blnPaymentType = true;
        //-----------------------------------------------------------------------

        #endregion
        
        #region User Defined Events

        #region BankEntry_Load
        private void BankEntry_Load(object sender, EventArgs e)
        {
            ViewGrid.Height = 494;
            //
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //
            strSQL = " SELECT COMPANY_ID," +
                    "        COMPANY_NAME " +
                    " FROM   MST_COMPANY " +
                    " WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                    " AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech +
                    " ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;
            //--

            ClearControls(); 
            ControlVisible(true);
            //
            lblTitle.Text = this.Text;

           
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

                blnExit = true;
                //cmbCompany.Enabled = false;                
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;

                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                cmbCompany.Enabled = false;
                grpSearchPanel.Enabled = false;
                strCheckFields = "";
                txtInvoiceNo.Select(); 
                blnExit = false;
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
                    //txtInvoiceNo.Enabled = false;
                    //txtInvoiceNo.BackColor = Color.LightYellow; 

                    cmbCompany.Enabled = false;
                    grpSearchPanel.Enabled = false;
                    //
                    txtInvoiceNo.Select();
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
                    BtnAdd.Select();
                //cmbCompany.Enabled = true;
                //
                //BtnAdd.Enabled = false;
                //BtnEdit.Enabled = false;
                BtnSearch.Enabled = false;
                BtnSort.Enabled = false;
                BtnPrint.Enabled = false;
                BtnDelete.Enabled = false;
                //BtnAdd.BackColor = Color.LightGray; 
                //BtnEdit.BackColor = Color.LightGray;
                BtnSort.BackColor = Color.LightGray;
                BtnSearch.BackColor = Color.LightGray;
                BtnPrint.BackColor = Color.LightGray;
                BtnDelete.BackColor = Color.LightGray;
                //
                grpSearchPanel.Enabled = true;
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
            //if (blnExit == false)
            //    return;
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
                //BtnAdd.Enabled = false;
                BtnSearch.Enabled = false;
                BtnSort.Enabled = false;
                BtnPrint.Enabled = false;
                BtnDelete.Enabled = false;
                //BtnAdd.BackColor = Color.LightGray; 
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

                //mskInvoiceDateSearch.Select();
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
            //if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtAmountSearch, "") == false)
            //    e.Handled = true;
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

                //if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                //    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDateSearch) + cmnService.J_DateOperator() + " ";
                //if (dtService.J_IsBlankDateCheck(ref mskAccountEntryDateSearch, J_ShowMessage.NO) == false)
                //    strCheckFields += "AND TRN_INVOICE_HEADER.BANK_STATEMENT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDateSearch) + cmnService.J_DateOperator() + " ";
                //if (dtService.J_IsBlankDateCheck(ref mskBankDateSearch, J_ShowMessage.NO) == false)
                //    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankDateSearch) + cmnService.J_DateOperator() + " ";
                //if (txtBankSearch.Text.Trim() != "")
                //    strCheckFields += "AND MST_BANK.BANK_NAME like '%" + cmnService.J_ReplaceQuote(txtBankSearch.Text.Trim().ToUpper()) + "%' ";
                //if(cmnService.J_ReturnDoubleValue(txtAmountSearch.Text == ""? "0":txtAmountSearch.Text) > 0)
                //    strCheckFields += "AND TRN_INVOICE_HEADER.NET_AMOUNT = " + cmnService.J_ReturnDoubleValue(txtAmountSearch.Text) + " ";
                //if (txtReferenceSearch.Text.Trim() != "")
                //    strCheckFields += strCheckFields + "AND TRN_INVOICE_HEADER.REFERENCE_NO like '%" + cmnService.J_ReplaceQuote(txtReferenceSearch.Text.Trim().ToUpper()) + "%' ";
                //if (txtSearchRemarks.Text.Trim() != "")
                //    strCheckFields += strCheckFields + "AND TRN_INVOICE_HEADER.REMARKS like '%" + cmnService.J_ReplaceQuote(txtSearchRemarks.Text.Trim().ToUpper()) + "%' ";

                //strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;

                //if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId) == false)
                //{
                //    mskInvoiceDateSearch.Select();
                //    return;
                //}

                //lblSearchMode.Text = J_Mode.General;
                //grpSearch.Visible = false;
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
                strSearch = "AND (   TRN_INVOICE_HEADER.INVOICE_NO                         LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.PARTY_NAME                                  LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION             LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_BANK.BANK_NAME                                    LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.REFERENCE_NO                       LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.NET_AMOUNT                         LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
                //--
                strSQL = strQuery + strSearch + " ORDER BY " + strOrderBy;
                //--
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //--
            }
            catch(Exception err)
            {
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex));
        }
        #endregion 


        #region cmbPaymentType_SelectedIndexChanged
        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentType.SelectedIndex <= 0)
                return;

            intBankInCollectionFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT BANK_IN_COLLECTION_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            intReferenceNoFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT REFERENCE_NO_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
        }
        #endregion


        #region cmbParty_KeyUp
        private void cmbParty_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbParty, e);
        }
        #endregion

        #region cmbParty_Leave
        private void cmbParty_Leave(object sender, EventArgs e)
        {
            //// Get the Selected Record from my Data Bound Combo (Return Type is Object)
            //cmnService.J_AutoCompleteCombo_Leave(ref cmbParty);
            //object objRowView = cmbParty.SelectedItem;
            //if (objRowView == null)
            //{
            //    // Added by Ripan Paul on 23-07-2011
            //    BS.BS_PartyName = cmbParty.Text;

            //    TrnPartySave frmPartySave = new TrnPartySave();
            //    frmPartySave.txtPartyName.Text = BS.BS_PartyName;
            //    frmPartySave.Text = "Party Master";
            //    frmPartySave.ShowDialog();

            //    strSQL = "SELECT PARTY_ID," +
            //             "       PARTY_NAME " +
            //             "FROM   MST_PARTY " +
            //             "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
            //             "ORDER BY PARTY_NAME ";

            //    DMLService dml = new DMLService();
            //    if (dml.J_PopulateComboBox(strSQL, ref cmbParty) == false) return;
            //    cmbParty.Text = BS.BS_PartyName;
            //    dml.Dispose();

            //    cmbParty.Focus();
            //}
        }
        #endregion

        #region cmbParty_SelectedIndexChanged
        private void cmbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParty.SelectedIndex <= 0)
            {
                lblPartyCategory.Text = "";
                return;
            }

            //Checking the Party category of the party selected
            strSQL = "SELECT PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   MST_PARTY_CATEGORY, " +
                     "       MST_PARTY " +
                     "WHERE MST_PARTY_CATEGORY.PARTY_CATEGORY_ID  = MST_PARTY.PARTY_CATEGORY_ID " +
                     "AND   MST_PARTY.PARTY_ID = " + Support.GetItemData(cmbParty, cmbParty.SelectedIndex);

            lblPartyCategory.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

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
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtNumeric, "") == false)
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
            //BtnAdd.Enabled = false;
            //BtnEdit.Enabled = false;
            BtnSearch.Enabled = false;
            BtnSort.Enabled = false;
            BtnPrint.Enabled = false;
            BtnDelete.Enabled = false;
            //BtnAdd.BackColor = Color.LightGray;
            //BtnEdit.BackColor = Color.LightGray;
            BtnSort.BackColor = Color.LightGray;
            BtnSearch.BackColor = Color.LightGray;
            BtnPrint.BackColor = Color.LightGray;
            BtnDelete.BackColor = Color.LightGray;

            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtInvoiceNo.Text = "";
            //txtInvoiceNo.Enabled = true;
            //txtInvoiceNo.BackColor = Color.White;

            mskInvoiceDate.Text = "";

            strSQL = "SELECT PARTY_ID," +
                     "       PARTY_NAME " +
                     "FROM   MST_PARTY " +
                     "       INNER JOIN MST_PARTY_CATEGORY " +
                     "       ON MST_PARTY.PARTY_CATEGORY_ID = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID " +
                     "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                     "AND    PARTY_CATEGORY_DESCRIPTION = '" + BS_PartyCategory.Sundry + "' " +
                     "ORDER BY PARTY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbParty) == false) return;
            //--
            //Populating Payment Type Combo
            strSQL = "SELECT PAYMENT_TYPE_ID," +
                     "       PAYMENT_TYPE_DESCRIPTION " +
                     "FROM   MST_PAYMENT_TYPE " +
                     "WHERE  INACTIVE_FLAG = 0 " +
                     "AND    HIDE_IN_INVOICE_FLAG = 0 " +
                     "ORDER BY PAYMENT_TYPE_DESCRIPTION ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType) == false) return;
            //--
            txtReference.Text = "";
            //--
            //Populating Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBank) == false) return;
            //--
            txtNetAmount.Text = "0.00";
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
        private void LoadGrid(long CompanyID)
        {
            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode
            //txtSearchAll.Text = "";
            string[,] strMatrix1 =  {{"Invoice_Header_id", "0", "", "Left", "", "", ""},
                			         {"Invoice No", "120", "", "Left", "", "", ""},
							         {"Invoice Date", "80", "", "Left", "", "", ""},
                                     {"Party Name", "250", "", "Left", "", "", ""},
                                     {"Payment Desc", "100", "", "Left", "", "", ""},
                                     {"Bank Name", "100", "", "Left", "", "", ""},
							         {"Reference No", "100", "", "Left", "", "", ""},
                                     {"Net Amt. ", "180", "0.00", "Right", "", "", ""}};
            //
            strMatrix = strMatrix1;
            //
            string[,] strAutoFlagMatrix = {{"=0", "N", "Manual", "T"},
                                           {"=1", "N", "", "T"}};
            //
            strOrderBy = " INVOICE_DATE DESC, INVOICE_HEADER_ID DESC ";
            //
            strQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                TRN_INVOICE_HEADER.INVOICE_NO,
                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DT,
                                MST_PARTY.PARTY_NAME,
                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                MST_BANK.BANK_NAME,
                                TRN_INVOICE_HEADER.REFERENCE_NO,
                                TRN_INVOICE_HEADER.NET_AMOUNT
                         FROM   TRN_INVOICE_HEADER
                         INNER  JOIN MST_PARTY    ON  TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID
                         INNER  JOIN MST_PAYMENT_TYPE ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                         LEFT   JOIN  MST_BANK ON TRN_INVOICE_HEADER.BANK_ID = MST_BANK.BANK_ID
                         WHERE  TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV'
                         AND    TRN_INVOICE_HEADER.RECON_FLAG = 0
                         AND    TRN_INVOICE_HEADER.COMPANY_ID = " + CompanyID ;
            //
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //
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
                strSQL = @"  SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                TRN_INVOICE_HEADER.INVOICE_NO,
                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE,
                                MST_PARTY.PARTY_NAME,
                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                MST_BANK.BANK_NAME,
                                TRN_INVOICE_HEADER.REFERENCE_NO,
                                TRN_INVOICE_HEADER.NET_AMOUNT
                         FROM   TRN_INVOICE_HEADER
                         INNER  JOIN MST_PARTY    ON  TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID
                         INNER  JOIN MST_PAYMENT_TYPE ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                         LEFT   JOIN  MST_BANK ON TRN_INVOICE_HEADER.BANK_ID = MST_BANK.BANK_ID
                         WHERE  TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV'
                         AND    TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany,cmbCompany.SelectedIndex) + @"
                         AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + Id;
                //--
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowRecord == null) return false;
                //--
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    //--
                    txtInvoiceNo.Text   = Convert.ToString(drdShowRecord["INVOICE_NO"]);
                    mskInvoiceDate.Text = Convert.ToString(drdShowRecord["INVOICE_DATE"]);
                    BS.T_Selected_Party = Convert.ToString(drdShowRecord["PARTY_NAME"]);
                    BS.T_Selected_PaymentType = Convert.ToString(drdShowRecord["PAYMENT_TYPE_DESCRIPTION"]);
                    txtReference.Text   = Convert.ToString(drdShowRecord["REFERENCE_NO"]);
                    cmbBank.Text = Convert.ToString(drdShowRecord["BANK_NAME"]);
                    txtNetAmount.Text   = string.Format("{0:0.00}",Convert.ToDouble(Convert.ToString(drdShowRecord["NET_AMOUNT"])));
                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //--
                    cmbPaymentType.Text = BS.T_Selected_PaymentType;
                    cmbParty.Text = BS.T_Selected_Party;
                    //--
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
                long lngInvoiceHeaderId = 0;
                long lngBankId = 0;
                long lngPartyId = 0;
                long lngPaymentTypeId = 0;
                string strTranType = "OINV";
                switch (lblMode.Text)
                {
                    #region Add
                    case J_Mode.Add:
                        //*****  For Add
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //
                        if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                        //
                        lngBankId = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                        lngPartyId = cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex);
                        lngPaymentTypeId = cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex);
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //
                        strSQL = @"INSERT INTO TRN_INVOICE_HEADER (
                                                                   COMPANY_ID, 
                                                                   PARTY_ID,
                                                                   INVOICE_NO,
                                                                   INVOICE_DATE,
                                                                   TRAN_TYPE,
                                                                   NET_AMOUNT,
                                                                   PAYMENT_TYPE_ID,
                                                                   BANK_ID,
                                                                   REFERENCE_NO,
                                                                   USER_ID) 
                                   VALUES(     " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + @", 
                                               " + lngPartyId + @", 
                                              '" + cmnService.J_ReplaceQuote(txtInvoiceNo.Text.Trim()) + @"', 
                                               " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + @", 
                                              '" + strTranType + @"',
                                               " + cmnService.J_ReturnDoubleValue(txtNetAmount.Text) + @", 
                                               " + lngPaymentTypeId + @", 
                                               " + lngBankId + @", 
                                              '" + cmnService.J_ReplaceQuote(txtReference.Text.Trim()) + @"', 
                                               " + J_Var.J_pUserId   + @")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            //Rollback Transaction
                            this.Cursor = Cursors.Default;
                            dmlService.J_Rollback();
                            txtInvoiceNo.Select();
                            return;
                        }
                        //
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
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
                        //BtnEdit.Enabled = false;
                        BtnSearch.Enabled = false;
                        BtnSort.Enabled = false;
                        BtnPrint.Enabled = false;
                        BtnDelete.Enabled = false;
                        //BtnEdit.BackColor = Color.LightGray;
                        BtnSort.BackColor = Color.LightGray;
                        BtnSearch.BackColor = Color.LightGray;
                        BtnPrint.BackColor = Color.LightGray;
                        BtnDelete.BackColor = Color.LightGray;
                        //cmbCompany.Enabled = true;
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue("TRN_INVOICE_HEADER", "INVOICE_HEADER_ID");
                        //-------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                        //-----------------------------------------------------------                        
                        
                        break;
                    #endregion

                    #region Edit
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;

                        if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;

                        dmlService.J_BeginTransaction();

                        //UPDATING COLLECTION HEADER RECORD
                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    COMPANY_ID           = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + ", "+
                                 "       PARTY_ID             = " + cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex) + ", " +
                                 "       INVOICE_NO           ='" + cmnService.J_ReplaceQuote(txtInvoiceNo.Text) + "', " +
                                 "       INVOICE_DATE         = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + ", " +
                                 "       TRAN_TYPE            ='" + strTranType + "', " +
                                 "       NET_AMOUNT           ='" + cmnService.J_ReturnDoubleValue(cmnService.J_ReplaceQuote(txtNetAmount.Text)) + "', "+
                                 "       PAYMENT_TYPE_ID      = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + ", " +
                                 "       BANK_ID              = " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex) + ", " +
                                 "       REFERENCE_NO         ='" + cmnService.J_ReplaceQuote(txtReference.Text) + "' " +
                                 "WHERE  INVOICE_HEADER_ID = " + lngSearchId;
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            //chkRequestCD.Select();
                            return;
                        }

                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------

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
                        //BtnAdd.Enabled = false;
                        BtnSearch.Enabled = false;
                        BtnSort.Enabled = false;
                        BtnPrint.Enabled = false;
                        //BtnDelete.Enabled = false;
                        //BtnAdd.BackColor = Color.LightGray; 
                        BtnSort.BackColor = Color.LightGray;
                        BtnSearch.BackColor = Color.LightGray;
                        BtnPrint.BackColor = Color.LightGray;
                        //BtnDelete.BackColor = Color.LightGray;                      
                        //cmbCompany.Enabled = true;
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                        break;
                    #endregion

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

                    //Invoice No
                    if (txtInvoiceNo.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Invoice No cannot be blank.");
                        txtInvoiceNo.Select();
                        return false;
                    }
                    else
                    {
                        if (lblMode.Text == J_Mode.Add && dmlService.J_IsRecordExist("TRN_INVOICE_HEADER"," COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCompany,cmbCompany.SelectedIndex) + 
                                                                                                          " AND INVOICE_NO  ='" + cmnService.J_ReplaceQuote(txtInvoiceNo.Text.Trim()) + "' " +
                                                                                                          " AND TRAN_TYPE   ='OINV'" ) == true)
                        {
                            cmnService.J_UserMessage("Invoice No already exists.");
                            txtInvoiceNo.Select();
                            return false;
                        
                        }
                        else if (lblMode.Text == J_Mode.Edit)
                        {
                            //IF EXISTS
                            if (dmlService.J_IsRecordExist("TRN_INVOICE_HEADER", " COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + 
                                                                                 " AND INVOICE_NO  = '" + cmnService.J_ReplaceQuote(txtInvoiceNo.Text.Trim()) + "' " +
                                                                                 " AND TRAN_TYPE   = 'OINV' AND INVOICE_HEADER_ID <> " + lngSearchId) == true)
                            {
                                cmnService.J_UserMessage("Invoice No already exists into the ");
                                txtInvoiceNo.Select();
                                return false;
                            }

                            //CANNOT MODIFY IF EXISTS INTO THE TRANSACTION 
                            int intNoOfInvoiceExists = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(" SELECT COUNT(*) " +
                                                                                                        " FROM   TRN_COLLECTION_DETAIL " +
                                                                                                        "        INNER JOIN TRN_COLLECTION_HEADER "+
                                                                                                        "        ON  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID "+
                                                                                                        " WHERE  COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) +
                                                                                                        " AND    INVOICE_HEADER_ID  = " + lngSearchId));
                            if (intNoOfInvoiceExists > 0)
                            {
                                cmnService.J_UserMessage("You cannot modify this Invoice.\nAlready exists into the Transaction.");
                                txtInvoiceNo.Select();
                                return false;
                            }
                        }
                    }

                    //INVOICE DATE 
                    if (!dtService.J_IsBlankDateCheck(ref mskInvoiceDate, J_ShowMessage.NO))
                    {
                        if (dtService.J_IsDateValid(mskInvoiceDate.Text) == false)
                        {
                            cmnService.J_UserMessage("Enter the valid Invoice date.");
                            mskInvoiceDate.Select();
                            return false;
                        }
                        //-- 
                        string strReconCutoffDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT CONVERT(CHAR(10),RECON_CUTOFF_DATE,103)  AS RECON_CUTOFF_DATE FROM MST_SETUP " ));
                        if (dtService.J_IsDateGreater(ref mskInvoiceDate, strReconCutoffDate, "", "", "", J_ShowMessage.NO) == false)
                        {
                            cmnService.J_UserMessage("Invoice date should be before the Reconcilation Cutoff Date\n i.e. : " + strReconCutoffDate);
                            mskInvoiceDate.Select();
                            return false;
                        }
                    }
                    else
                    {
                        cmnService.J_UserMessage("Enter the Invoice date.");
                        mskInvoiceDate.Select();
                        return false;
                    }
                    ////--
                    // PARTY
                    if (cmbParty.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please select the Party.");
                        cmbParty.Select();
                        return false;
                    }

                    // Payment Type
                    if (cmbPaymentType.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please select the Payment Type");
                        cmbPaymentType.Select();
                        return false;
                    }
                    else
                    {
                        //Reference No
                        if (intReferenceNoFlag == 1 && txtReference.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Reference No : Mandatory for this Payment Type");
                            txtReference.Select();
                            return false;
                        }

                        //Added By dhrub on 09/04/2015
                        //------------------------------------------------------------------
                        //-- If Bank is Compulsory for a Payment Type, User has to Select a Bank
                        //--- If Bank is NOT Compulsory for a Payment Type, User cannot Select a Bank
                        //------------------------------------------------------------------
                        if (intBankInCollectionFlag == 0)
                        {
                            if (cmbBank.SelectedIndex > 0)
                            {
                                cmnService.J_UserMessage("Bank not allowed for this Payment Type");
                                cmbBank.Select();
                                return false;
                            }
                        }
                        else
                        {
                            if (cmbBank.SelectedIndex <= 0)
                            {
                                cmnService.J_UserMessage("Bank Mandatory for this Payment Type");
                                cmbBank.Select();
                                return false;
                            }

                            //--------------------------------------------------------------------------------------------------------------------
                            if (cmbPaymentType.Text == BS_PaymentType.Cc_Avenue && Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT CC_AVENUE_FLAG FROM MST_BANK WHERE BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex))) == 0)
                            {
                                cmnService.J_UserMessage("Improper bank selected.");
                                cmbBank.Select();
                                return false;
                            }
                        }

                       
                    }

                    //////--
                    if (cmbPaymentType.Text.ToUpper() == "CC AVENUE")
                    {
                        if (cmbBank.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("For Payment Type " + cmbPaymentType.Text + ", Bank should be selected");
                            cmbBank.Select();
                            return false;
                        }
                    }
                    
                    ////--------------------------------------------------------------------------------------------------------------------
                    //// Collection Date within FA Year Date or not
                    ////if (dtService.J_IsDateGreater(J_Var.J_pFABegDate, ref mskCollectionDate, "", "", "", J_ShowMessage.NO) == false
                    ////    || dtService.J_IsDateGreater(ref mskCollectionDate, J_Var.J_pFAEndDate, "", "", "", J_ShowMessage.NO) == false)
                    ////{
                    ////    cmnService.J_UserMessage("Invoice date is outside FA Year date." +
                    ////        "\n\nBegining Date : " + J_Var.J_pFABegDate +
                    ////        "\nEnding Date    : " + J_Var.J_pFAEndDate);
                    ////        mskCollectionDate.Select();
                    ////        return false;
                    ////}
                    ////-- RECONCILE DATE
                    ////if (cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) > 0)
                    ////{
                    //if (!dtService.J_IsBlankDateCheck(ref mskReconcileDate, J_ShowMessage.NO))
                    //{
                    //    if (dtService.J_IsDateValid(mskReconcileDate.Text) == false)
                    //    {
                    //        cmnService.J_UserMessage("Enter the valid Reconcile date.");
                    //        mskReconcileDate.Select();
                    //        return false;
                    //    }
                    //}
                    //    else
                    //    {
                    //        cmnService.J_UserMessage("Enter the Reconcile date.");
                    //        mskReconcileDate.Select();
                    //        return false;
                    //    }
                    //    //
                    //    if (dtService.J_IsDateGreater(ref mskCollectionDate, ref mskReconcileDate, "", "", "", J_ShowMessage.NO) == false)
                    //    {
                    //        cmnService.J_UserMessage("Reconcile Date can never be earlier than Collection Date");
                    //        mskReconcileDate.Select();
                    //        return false;
                    //    }
                    //}
                    //
                    //if (lblMode.Text == J_Mode.Add)
                    //{
                    //    if (lblAutoCollectionPostFlag.Text == "1")
                    //    {
                    //        cmnService.J_UserMessage("Payment Type : " + cmbPaymentType.Text + " is not allowed in 'ADD MODE'");
                    //        cmbPaymentType.Select();
                    //        return false;
                    //    }
                    //}
                    //
                    if (cmnService.J_ReturnDoubleValue(txtNetAmount.Text) == 0)
                    {
                        cmnService.J_UserMessage("Net Amount should not be 0");
                        txtNetAmount.Select();
                        return false;
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

        #endregion

    }
}

