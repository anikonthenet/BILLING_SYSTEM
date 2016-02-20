#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: TrnInvoiceEntry
Version			: 2.0
Start Date		: 09-06-2011
End Date		: 
Tables Used     : 
Module Desc		: Invoice Entry
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
    public partial class TrnUnknownPaymentEntry : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnUnknownPaymentEntry()
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

        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        

        int intTempGridPosition = 0;
        bool blnAmount;

        long lngSearchId = 0;

        string[,] strMatrix;

        
        #endregion

        #region User Defined Events

        #region BankEntry_Load
        private void BankEntry_Load(object sender, EventArgs e)
        {
            //Added By dhrub on 03/05/2014 for Set the Grid Size
            ViewGrid.Size = new Size(1000, 510);

            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);

            lblTitle.Text = this.Text;

            strSQL = "SELECT COMPANY_ID," +
                     "       COMPANY_NAME " +
                     "FROM   MST_COMPANY " +
                     "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                     "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                     "ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;


            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbSelectBank) == false) return;


            cmbCompany.Enabled = true;
            BtnSort.BackColor = Color.LightGray;
            BtnSort.Enabled = false;
            BtnPrint.BackColor = Color.LightGray;
            BtnPrint.Enabled = false;

            chkCancelledEntry.Checked = false;


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
                
                cmbCompany.Enabled = false;
                
                chkCancelled.Visible = false;

                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;

                ControlVisible(true);
                ClearControls();					//Clear all the Controls


                mskInvoiceDate.Text = dmlService.J_ReturnServerDate();

                //---------------------------------------------
                strCheckFields = "";

                mskInvoiceDate.Select();
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

                    chkCancelled.Visible = true;

                    // A particular id wise retriving the data from database
                    if (ShowRecord(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);

                        cmbCompany.Enabled = true;
                    }

                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;

                    cmbCompany.Enabled = false;

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

                cmbCompany.Enabled = true;
                BtnSort.BackColor = Color.LightGray;
                BtnSort.Enabled = false;
                BtnPrint.BackColor = Color.LightGray;
                BtnPrint.Enabled = false;

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

                    Insert_Update_Delete_Data();

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

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode

            string[,] strMatrix1 =  {{"Invoice Header Id", "0", "", "Right", "", "false", ""},
                                     {"Date", "80", "dd/MM/yyyy", "", "", "", ""},
							         {"Bank Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                     {"A/c Entry Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                     {"Bank", "80", "", "", "", "", ""},
                                     {"Payment Type.", "80", "", "", "", "", ""},
							         {"Net Amount.", "80", "0.00", "Right", "", "", ""},
                                     {"Reference No", "80", "", "", "", "", ""},
                                     {"Remarks", "200", "", "", "", "", ""},
                                     {"Status", "80", "", "", "", "", ""}};

            string[,] strCaseEndMatrix = {{"=0", "N", "", "T"},
							              {"=1", "N", "Cancelled", "T"}};


            strMatrix = strMatrix1;

            strOrderBy = cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatYYYYMMDD) + " DESC, MST_BANK.BANK_ID, TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

            strQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID       AS INVOICE_HEADER_ID,              
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS INVOICE_DATE, 
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.BANK_STATEMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS BANK_STATEMENT_DATE,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS ACCOUNT_ENTRY_DATE, 
                                MST_BANK.BANK_NAME                         AS BANK_NAME,
                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION  AS PAYMENT_TYPE_DESCRIPTION,
                                TRN_INVOICE_HEADER.NET_AMOUNT              AS NET_AMOUNT,
                                TRN_INVOICE_HEADER.REFERENCE_NO            AS REFERENCE_NO,
                                TRN_INVOICE_HEADER.REMARKS                 AS REMARKS,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.CANCELLATION_FLAG", J_SQLColFormat.Case_End, strCaseEndMatrix) + @" AS CANCELLATION_FLAG 
                         FROM   (((TRN_INVOICE_HEADER 
                         LEFT JOIN MST_PARTY 
                         ON     TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID)
                         LEFT JOIN MST_PAYMENT_TYPE
                         ON     TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID)
                         LEFT JOIN MST_BANK
                         ON     TRN_INVOICE_HEADER.BANK_ID         = MST_BANK.BANK_ID) ";
            
            if (cmbCompany.SelectedIndex <= 0)
                strQuery += "WHERE 1 = 2 ";
            else
            {
                strQuery += "WHERE  TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                            "AND    TRN_INVOICE_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                            "AND    TRN_INVOICE_HEADER.TRAN_TYPE  = 'UN' ";
            }

            //Added by Shrey Kejriwal on 20-05-2013
            
            // Checking if the cancelled entries has to be included
            if (chkCancelledEntry.Checked == false)
            {
                strQuery += "AND   TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0 ";
            }

            //Checking if the entries has to be filtered according to bank
            if (cmbSelectBank.SelectedIndex > 0)
            {
                strQuery += "AND MST_BANK.BANK_ID = " + Support.GetItemData(cmbSelectBank, cmbSelectBank.SelectedIndex) + " ";
            }


            strSQL = strQuery + "ORDER BY " + strOrderBy;
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid

        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
            {
                BtnAdd.Focus();
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

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                // set view mode
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                lblSearchMode.Text = J_Mode.General;

                // clear controls
                ClearControls();

                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                cmbCompany.Enabled = true;
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
                if (ValidateFields() == false) return;

                grpSort.Visible = false;
                grpSearch.Visible = true;

                mskInvoiceDateSearch.Select();
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
                if (ValidateFields() == false) return;

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
                    mskInvoiceDateSearch.Select();
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

        #region mskInvoiceDate_Leave
        private void mskInvoiceDate_Leave(object sender, EventArgs e)
        {
            mskAccountEntryDate.Text = mskInvoiceDate.Text;
            mskBankDate.Text  = mskInvoiceDate.Text;
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
            //Populating Payment Type Combo
            strSQL = "SELECT PAYMENT_TYPE_ID," +
                     "       PAYMENT_TYPE_DESCRIPTION " +
                     "FROM   MST_PAYMENT_TYPE " +
                     "ORDER BY PAYMENT_TYPE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType) == false) return;

            //Populating Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBank) == false) return;

            txtReference.Text = "";
            mskAccountEntryDate.Text = "";
            mskBankDate.Text = "";

            txtAmount.Text = "0.00";
            txtRemarks.Text = "";


            // Added by Ripan Paul on 07-05-2013
            mskInvoiceDateSearch.Text = "";
            mskAccountEntryDateSearch.Text = "";
            mskBankDateSearch.Text = "";
            txtBankSearch.Text = "";
            txtAmountSearch.Text = "";
            txtReferenceSearch.Text = "";
            txtSearchRemarks.Text = ""; 

            chkCancelled.Visible = false;
            chkCancelled.Checked = false;

        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;

            try
            {
               // SQL Query
                strSQL = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID       AS INVOICE_HEADER_ID,              
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS INVOICE_DATE, 
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PARTY_NAME,
                              TRN_INVOICE_HEADER.NET_AMOUNT              AS NET_AMOUNT,
                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION  AS PAYMENT_TYPE_DESCRIPTION,
                              MST_BANK.BANK_NAME                         AS BANK_NAME,
                              TRN_INVOICE_HEADER.REFERENCE_NO            AS REFERENCE_NO,
                              TRN_INVOICE_HEADER.REMARKS                 AS REMARKS,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS ACCOUNT_ENTRY_DATE, 
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.BANK_STATEMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS BANK_STATEMENT_DATE,
                              TRN_INVOICE_HEADER.CANCELLATION_FLAG       AS CANCELLATION_FLAG
                       FROM   (((TRN_INVOICE_HEADER
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID)
                       LEFT JOIN MST_PAYMENT_TYPE
                               ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID)
                       LEFT JOIN MST_BANK
                               ON TRN_INVOICE_HEADER.BANK_ID         = MST_BANK.BANK_ID)
                       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + Id;
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;

                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    mskInvoiceDate.Text = drdShowRecord["INVOICE_DATE"].ToString();
                    mskAccountEntryDate.Text = drdShowRecord["ACCOUNT_ENTRY_DATE"].ToString();
                    mskBankDate.Text = drdShowRecord["BANK_STATEMENT_DATE"].ToString();

                    cmbPaymentType.Text = drdShowRecord["PAYMENT_TYPE_DESCRIPTION"].ToString();
                    cmbBank.Text = drdShowRecord["BANK_NAME"].ToString();
                    txtReference.Text = drdShowRecord["REFERENCE_NO"].ToString();
                    
                    txtAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["NET_AMOUNT"].ToString()));
                    txtRemarks.Text = drdShowRecord["REMARKS"].ToString();

                    if (drdShowRecord["CANCELLATION_FLAG"].ToString() == "1")
                        chkCancelled.Checked = true;
                    else
                        chkCancelled.Checked = false;


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

                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        if (cmnService.J_SaveConfirmationMessage(ref mskInvoiceDate) == true) return;
                        //-----------------------------------------------------------

                        //Generating inwords for the amounts
                        string strAccountEntryDate = "null";
                        string strBankDate = "null";

                        if (!dtService.J_IsBlankDateCheck(ref mskAccountEntryDate, J_ShowMessage.NO))
                            strAccountEntryDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDate.Text) + cmnService.J_DateOperator();

                        if (!dtService.J_IsBlankDateCheck(ref mskBankDate, J_ShowMessage.NO))
                            strBankDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankDate.Text) + cmnService.J_DateOperator();
                        
                        
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------

                        strSQL = @"INSERT INTO TRN_INVOICE_HEADER
                                             (BRANCH_ID,
                                              FAYEAR_ID,
                                              COMPANY_ID,
                                              INVOICE_DATE,
                                              TRAN_TYPE,
                                              NET_AMOUNT,
                                              BANK_STATEMENT_DATE,
                                              ACCOUNT_ENTRY_DATE,
                                              BANK_ID,
                                              PAYMENT_TYPE_ID,
                                              REFERENCE_NO,
                                              REMARKS,
                                              USER_ID) " +
                                 "    VALUES " +
                                 "           (" + J_Var.J_pBranchId + "," +
                                 "            " + J_Var.J_pFAYearId + "," +
                                 "            " + Support.GetItemData(cmbCompany, cmbCompany.SelectedIndex) + ", " +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate.Text) + cmnService.J_DateOperator() + ", " +
                                 "            'UN', " +
                                 "            " + Convert.ToDouble(txtAmount.Text) + ", " +
                                 "            " + strBankDate + ", " +
                                 "            " + strAccountEntryDate + ", " +
                                 "            " + Support.GetItemData(cmbBank, cmbBank.SelectedIndex) + ", " +
                                 "            " + Support.GetItemData(cmbPaymentType, cmbPaymentType.SelectedIndex) + ", " +
                                 "           '" + cmnService.J_ReplaceQuote(txtReference.Text) + "', " +
                                 "           '" + cmnService.J_ReplaceQuote(txtRemarks.Text) + "', " +
                                 "            " + Convert.ToInt32(J_Var.J_pUserId) + " )";
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        //-----------------------------------------------------------
                        
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        //-----------------------------------------------------------
                        ClearControls();
                        mskInvoiceDate.Text = dmlService.J_ReturnServerDate();

                        //-----------------------------------------------------------
                        cmnService.J_UserMessage("Record Saved");
                        //-----------------------------------------------------------

                        mskInvoiceDate.Select();

                        break;
                    case J_Mode.Edit:
                        //*****  For Modify
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;

                        //--Assigning Cancellation Status to a variable

                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------

                        strAccountEntryDate = "null";
                        strBankDate = "null";

                        if (!dtService.J_IsBlankDateCheck(ref mskAccountEntryDate, J_ShowMessage.NO))
                        {
                            strAccountEntryDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDate.Text) + cmnService.J_DateOperator();
                        }

                        if (!dtService.J_IsBlankDateCheck(ref mskBankDate, J_ShowMessage.NO))
                        {
                            strBankDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankDate.Text) + cmnService.J_DateOperator();
                        }

                        int intCancellationFlag = 0;

                        if (chkCancelled.Checked == true)
                            intCancellationFlag = 1;

                        //UPDATING HEADER RECORD

                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    PAYMENT_TYPE_ID     =  " + Support.GetItemData(cmbPaymentType, cmbPaymentType.SelectedIndex) + ", " +
                                 "       BANK_ID             =  " + Support.GetItemData(cmbBank, cmbBank.SelectedIndex) + ", " +
                                 "       REFERENCE_NO        = '" + cmnService.J_ReplaceQuote(txtReference.Text) + "', " +
                                 "       ACCOUNT_ENTRY_DATE  =  " + strAccountEntryDate + ", " +
                                 "       BANK_STATEMENT_DATE =  " + strBankDate + ", " +
                                 "       INVOICE_DATE        =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate.Text) + cmnService.J_DateOperator() + ", " +
                                 "       NET_AMOUNT          =  " + Convert.ToDouble(txtAmount.Text) + ", " +
                                 "       REMARKS             = '" + cmnService.J_ReplaceQuote(txtRemarks.Text) + "', " +
                                 "       CANCELLATION_FLAG   =  " + intCancellationFlag + " " +
                                 "WHERE  INVOICE_HEADER_ID = " + lngSearchId;
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbPaymentType.Select();
                            return;
                        }

                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
                        ClearControls();

                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);

                        cmbCompany.Enabled = true;

                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                        break;
                    case J_Mode.Delete:
                        
                        if (cmnService.J_UserMessage("Are you sure you want to delete the selected Payment entry?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;

                        dmlService.J_BeginTransaction();

                        strSQL = "DELETE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + lngSearchId;

                        dmlService.J_ExecSql(strSQL);

                        
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);


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
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COMPANY_ID", lngSearchId);
                    //    return false;
                    //}
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == true &&
                            dtService.J_IsBlankDateCheck(ref mskAccountEntryDateSearch, J_ShowMessage.NO) == true &&
                            dtService.J_IsBlankDateCheck(ref mskBankDateSearch, J_ShowMessage.NO) == true &&
                            txtBankSearch.Text.Trim() == "" &&
                            cmnService.J_ReturnDoubleValue(txtAmountSearch.Text == ""? "0": txtAmountSearch.Text) == 0 &&
                            txtReferenceSearch.Text.Trim() == "" &&
                            txtSearchRemarks.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            mskInvoiceDateSearch.Select();
                            return false;
                        }
                        if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskInvoiceDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Invalid date.");
                                mskInvoiceDateSearch.Select();
                                return false;
                            }
                        }
                        if (dtService.J_IsBlankDateCheck(ref mskAccountEntryDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskAccountEntryDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Invalid date.");
                                mskAccountEntryDateSearch.Select();
                                return false;
                            }
                        }
                        if (dtService.J_IsBlankDateCheck(ref mskBankDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskBankDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Invalid date.");
                                mskBankDateSearch.Select();
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                   
                    if (dtService.J_IsBlankDateCheck(ref mskInvoiceDate,J_ShowMessage.NO)==true)
                    {
                        cmnService.J_UserMessage("Enter the date1");
                        mskInvoiceDate.Select();
                        return false;
                    }
                    if (dtService.J_IsDateValid(mskInvoiceDate.Text) == false)
                    {
                        cmnService.J_UserMessage("Enter the valid date.");
                        mskInvoiceDate.Select();
                        return false;
                    }
                    // Invoice Date within FA Year Date or not
                    if (dtService.J_IsDateGreater(J_Var.J_pFABegDate, ref mskInvoiceDate, "", "", "", J_ShowMessage.NO) == false
                        || dtService.J_IsDateGreater(ref mskInvoiceDate, J_Var.J_pFAEndDate, "", "", "", J_ShowMessage.NO) == false)
                    {
                        cmnService.J_UserMessage("Invoice date should be within FA Year date." +
                            "\n\nBegining Date : " + J_Var.J_pFABegDate +
                            "\nEnding Date    : " + J_Var.J_pFAEndDate);
                        mskInvoiceDate.Select();
                        return false;
                    }
                    if (Convert.ToDouble(txtAmount.Text) == 0)
                    {
                        cmnService.J_UserMessage("Enter the Amount of the payment");
                        txtAmount.Select();
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

        #region ControlSearch_KeyPress
        private void ControlSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #endregion

    }
}

