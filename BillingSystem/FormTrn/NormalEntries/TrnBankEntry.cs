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
    public partial class TrnBankEntry : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnBankEntry()
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

        long lngPartyId = 0;
        long lngMaxDaysPermit = 0;
        
        #endregion

        #region User Defined Events

        #region BankEntry_Load
        private void BankEntry_Load(object sender, EventArgs e)
        {
            //ADDED BY DHRUB ON 07/05/2014
            ViewGrid.Size = new Size(1000, 506);

            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);

            lblTitle.Text = this.Text;

            //Populating Payment Type Combo
            strSQL = "SELECT PAYMENT_TYPE_ID," +
                     "       PAYMENT_TYPE_DESCRIPTION " +
                     "FROM   MST_PAYMENT_TYPE " +
                     "ORDER BY PAYMENT_TYPE_ID ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbFilterPaymentType) == false) return;

            
            strSQL = "SELECT COMPANY_ID," +
                         "       COMPANY_NAME " +
                         "FROM   MST_COMPANY " +
                         "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                         "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                         "ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;

            chkIncompleteRecords.Checked = true;

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
                    }

                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;

                    strCheckFields = "";

                    mskAccountEntryDate_Leave(sender, e);
                    mskBankDate_Leave(sender, e);
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
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
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

                txtPartyNameSearch.Select();
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

                // Storing the Criteria Fiels & Values 
                strCheckFields = "";

                // Party name search
                if (txtPartyNameSearch.Text.Trim() != "")
                    strCheckFields += "AND MST_PARTY.PARTY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtPartyNameSearch.Text.Trim().ToUpper()) + "%' ";

                // Invoice Date Search
                if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDateSearch) + cmnService.J_DateOperator() + " ";

                // Reference Search
                if (txtReferenceSearch.Text.Trim() != "")
                    strCheckFields += "AND TRN_INVOICE_HEADER.REFERENCE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtReferenceSearch.Text.Trim().ToUpper()) + "%' ";

                // Account Date Search
                if (dtService.J_IsBlankDateCheck(ref mskAccountEntryDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDateSearch) + cmnService.J_DateOperator() + " ";


                // Bank Date Search
                if (dtService.J_IsBlankDateCheck(ref mskBankEntryDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.BANK_STATEMENT_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankEntryDateSearch) + cmnService.J_DateOperator() + " ";

                
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId) == false)
                {
                    txtPartyNameSearch.Select();
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
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


                ClearControls();
                strCheckFields = "";
                strSQL = strQuery + "ORDER BY " + strOrderBy;

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

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, EventArgs e)
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Invoice Header ID
                if (rdbtnAsEntered.Checked == true)
                    strOrderBy = "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

                // STAR Marked 
                else if (rdbtnMaxDaysPermit.Checked == true)
                    strOrderBy = @"CASE WHEN 
                                       (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                    OR (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                  THEN '*' ELSE '' END DESC";

                strCheckFields = "";
                if (strCheckFields == "")
                    strSQL = strQuery + "ORDER BY " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSort_Click
        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Sorting;
                if (ValidateFields() == false) return;

                grpSort.Visible = true;
                grpSearch.Visible = false;

                rdbtnAsEntered.Checked = false;
                rdbtnMaxDaysPermit.Checked = false;

                if (strOrderBy == "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC")
                    rdbtnAsEntered.Select();
                else if (strOrderBy == @"CASE WHEN 
                                       (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                    OR (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                  THEN '*' ELSE '' END DESC")
                    rdbtnMaxDaysPermit.Select();

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
            //ADDED BY DHRUB ON 28/05/2014
            if (cmbCompany.SelectedIndex > 0)
            {
                strSQL = "SELECT MAX_DAYS_PERMIT FROM MST_COMPANY WHERE COMPANY_ID =  " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex);
                lngMaxDaysPermit = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL));
            }

            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode

            string[,] strMatrix1 =  {{"Invoice Header Id", "0", "", "Right", "", "false", ""},
							         {"Invoice No.", "110", "", "", "", "", ""},
							         {"Invoice Date", "70", "dd/MM/yyyy", "", "", "", ""},
							         {"Party", "190", "", "", "", "", "fill"},
							         {"Category", "50", "", "", "", "", ""},
							         {"Net Amount.", "80", "0.00", "Right", "", "", ""},
                                     {"Payment Type.", "100", "", "", "", "", ""},
                                     {"Bank", "60", "", "", "", "", ""},
                                     {"Reference No", "70", "", "", "", "", ""},
                                     {"A/c Entry Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                     {"Bank Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                     {"Max Diff", "50","","CENTER","","",""}};

            strMatrix = strMatrix1;

            strQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID        AS INVOICE_HEADER_ID,              
                              TRN_INVOICE_HEADER.INVOICE_NO                 AS INVOICE_NO,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS INVOICE_DATE, 
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck, null, "********* UNKNOWN DEPOSIT *********") + @" AS PARTY_NAME,
                              MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION,
                              TRN_INVOICE_HEADER.NET_AMOUNT                 AS NET_AMOUNT,
                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION     AS PAYMENT_TYPE_DESCRIPTION,
                              MST_BANK.BANK_NAME                            AS BANK_NAME,
                              TRN_INVOICE_HEADER.REFERENCE_NO               AS REFERENCE_NO,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS ACCOUNT_ENTRY_DATE, 
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.BANK_STATEMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS BANK_STATEMENT_DATE,
                              CASE WHEN 
                                    (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE))> "  + lngMaxDaysPermit+ @" THEN '*' ELSE '' END) = '*'
                                 OR (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE))> " + lngMaxDaysPermit+ @" THEN '*' ELSE '' END) = '*'
                              THEN '*' ELSE '' END STARED 
                       FROM   (((((TRN_INVOICE_HEADER
                       LEFT JOIN (SELECT INVOICE_HEADER_ID 
                                  FROM TRN_INVOICE_HEADER 
                                  WHERE PAYMENT_TYPE_ID = 4
                                  AND   ACCOUNT_ENTRY_DATE IS NOT NULL) AS CASH_RECONCILED
                               ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = CASH_RECONCILED.INVOICE_HEADER_ID)
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID)
                       LEFT JOIN MST_PARTY_CATEGORY 
                               ON MST_PARTY.PARTY_CATEGORY_ID        = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID)
                       LEFT JOIN MST_PAYMENT_TYPE
                               ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID)
                       LEFT JOIN MST_BANK
                               ON TRN_INVOICE_HEADER.BANK_ID         = MST_BANK.BANK_ID)";
            
            if (cmbCompany.SelectedIndex <= 0)
            {
                strQuery += "WHERE 1 = 2 ";
            }
            else
            {
                strQuery += "WHERE  TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                            "AND    TRN_INVOICE_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                            "AND    CANCELLATION_FLAG = 0 ";

                if (cmbFilterPaymentType.SelectedIndex > 0)
                {
                    strQuery += "AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + Support.GetItemData(cmbFilterPaymentType, cmbFilterPaymentType.SelectedIndex) + " ";
                }

                if (chkIncompleteRecords.Checked == true)
                {
                    strQuery += "AND ( TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE IS NULL " +
                                "      OR TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL )  " +
                                "AND CASH_RECONCILED.INVOICE_HEADER_ID IS NULL ";
                }
            }

            strOrderBy = "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid

            //int j = 0;
            //for (int i = j; i <= ViewGrid.VisibleRowCount - 1; i++)
            //{
            //    //if (cmnService.J_ReturnInt32Value(Convert.ToString(dgvDeductees.Rows[i].Cells[intVerifyId].Value)) > 0)
            //    if (Convert.ToString(ViewGrid.CurrentCell)== "*")
            //    {

            //    }
            //    j = j + 1;
            //}
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
            //cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
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

        #region mskAccountEntryDate_Leave
        private void mskAccountEntryDate_Leave(object sender, EventArgs e)
        {
            if (mskAccountEntryDate.Text == "  /  /")
            {
                lblACEntryDateDifference.Text = "";
                return;
            }

            if (dtService.J_IsDateValid(mskAccountEntryDate.Text) == false)
            {
                lblACEntryDateDifference.Text = "";
                return;
            }

            lblACEntryDateDifference.Text = GetDateDifference(lblInvoiceDate.Text,mskAccountEntryDate.Text); 
        }
        #endregion 

        #region mskBankDate_Leave
        private void mskBankDate_Leave(object sender, EventArgs e)
        {
            if (mskBankDate.Text == "  /  /")
            {
                lblBankDateDifference.Text = "";
                return;
            }
            if (dtService.J_IsDateValid(mskBankDate.Text) == false)
            {
                lblBankDateDifference.Text = "";
                return;
            }

            lblBankDateDifference.Text = GetDateDifference(lblInvoiceDate.Text, mskBankDate.Text); 
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

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            lblInvoiceNo.Text = "";
            lblInvoiceDate.Text = "";
            lblPartyName.Text = "";
            lblAmount.Text = "";
            lblACEntryDateDifference.Text = "";
            lblBankDateDifference.Text = "";

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

            txtPartyNameSearch.Text = "";
            txtReferenceSearch.Text = "";
            mskInvoiceDateSearch.Text = "";
            mskAccountEntryDateSearch.Text = "";
            mskBankEntryDateSearch.Text = "";

            strSQL = "SELECT PARTY_CATEGORY_ID, " +
                     "       PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   MST_PARTY_CATEGORY " +
                     "WHERE  INACTIVE_FLAG = 0 " +
                     "ORDER BY PARTY_CATEGORY_ID ";

            dmlService.J_PopulateComboBox(strSQL, ref cmbPartyCategory, J_ComboBoxDefaultText.NO);

            

        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            IDataReader reader = null;

            try
            {
               // SQL Query
                strSQL = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID      AS INVOICE_HEADER_ID,              
                              TRN_INVOICE_HEADER.INVOICE_NO                 AS INVOICE_NO,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS INVOICE_DATE, 
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PARTY_NAME,
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS PARTY_ID,
                              MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION,
                              TRN_INVOICE_HEADER.NET_AMOUNT                 AS NET_AMOUNT,
                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION     AS PAYMENT_TYPE_DESCRIPTION,
                              MST_BANK.BANK_NAME                            AS BANK_NAME,
                              TRN_INVOICE_HEADER.REFERENCE_NO               AS REFERENCE_NO,
                              " + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE", J_SQLColFormat.DateFormatDDMMYYYY),J_ColumnType.String,J_SQLColFormat.NullCheck)  + @" AS ACCOUNT_ENTRY_DATE, 
                              " + cmnService.J_SQLDBFormat(cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.BANK_STATEMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY), J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS BANK_STATEMENT_DATE,
                              DELIVERY_MODE_ID
                       FROM   ((((TRN_INVOICE_HEADER
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID)
                       LEFT JOIN MST_PARTY_CATEGORY 
                               ON MST_PARTY.PARTY_CATEGORY_ID        = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID)
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

                    mskAccountEntryDate.Text = Convert.ToString(drdShowRecord["ACCOUNT_ENTRY_DATE"]);
                    mskBankDate.Text = Convert.ToString(drdShowRecord["BANK_STATEMENT_DATE"]);

                    lngPartyId = Convert.ToInt64(drdShowRecord["PARTY_ID"].ToString());

                    lblInvoiceNo.Text = drdShowRecord["INVOICE_NO"].ToString();
                    lblInvoiceDate.Text = drdShowRecord["INVOICE_DATE"].ToString();
                    lblPartyName.Text = drdShowRecord["PARTY_NAME"].ToString();

                    cmbPartyCategory.Text = drdShowRecord["PARTY_CATEGORY_DESCRIPTION"].ToString();

                    lblAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["NET_AMOUNT"].ToString()));

                    cmbPaymentType.Text = drdShowRecord["PAYMENT_TYPE_DESCRIPTION"].ToString();
                    cmbBank.Text = drdShowRecord["BANK_NAME"].ToString();
                    txtReference.Text = drdShowRecord["REFERENCE_NO"].ToString();                    
                    //
                    if (lngPartyId == 0)
                    {
                        cmbPartyCategory.Visible = false;
                        lblPartyCategory.Visible = false;
                    }
                    else
                    {
                        cmbPartyCategory.Visible = true;
                        lblPartyCategory.Visible = true;
                    }
                    //
                    if (drdShowRecord["DELIVERY_MODE_ID"].ToString() == "1")
                    {
                        cmbPartyCategory.Enabled = false;
                        cmbPaymentType.Enabled = false;
                        txtReference.Enabled = false;
                        cmbBank.Enabled = false;
                    }
                    else
                    {
                        cmbPartyCategory.Enabled = true;
                        cmbPaymentType.Enabled = true;
                        txtReference.Enabled = true;
                        cmbBank.Enabled = true;
                    }
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

                        string strAccountEntryDate = "null";
                        string strBankDate = "null";

                        if (!dtService.J_IsBlankDateCheck(ref mskAccountEntryDate, J_ShowMessage.NO))
                        {
                            strAccountEntryDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskAccountEntryDate.Text) + cmnService.J_DateOperator();
                        }

                        if (!dtService.J_IsBlankDateCheck(ref mskBankDate, J_ShowMessage.NO))
                        {
                            strBankDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBankDate.Text) + cmnService.J_DateOperator();
                        }
                        //UPDATING HEADER RECORD
                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    PAYMENT_TYPE_ID     =  " + Support.GetItemData(cmbPaymentType, cmbPaymentType.SelectedIndex) + ", " +
                                 "       BANK_ID             =  " + Support.GetItemData(cmbBank, cmbBank.SelectedIndex) + ", " +
                                 "       REFERENCE_NO        = '" + cmnService.J_ReplaceQuote(txtReference.Text) + "', " +
                                 "       ACCOUNT_ENTRY_DATE  =  " + strAccountEntryDate + ", " +
                                 "       BANK_STATEMENT_DATE =  " + strBankDate + " " +
                                 "WHERE  INVOICE_HEADER_ID = " + lngSearchId;
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            cmbPaymentType.Select();
                            return;
                        }

                        if (lngPartyId > 0)
                        {
                            //Updating the Party Category
                            strSQL = "UPDATE MST_PARTY " +
                                     "SET    PARTY_CATEGORY_ID   =  " + Support.GetItemData(cmbPartyCategory, cmbPartyCategory.SelectedIndex) + " " +
                                     "WHERE  PARTY_ID = " + lngPartyId;
                            //----------------------------------------------------------
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                cmbPaymentType.Select();
                                return;
                            }
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
                    //if (Convert.ToInt64((ViewGrid.CurrentRowIndex).ToString()) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskInvoiceDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Enter valid Invoice Date to search.");
                                mskInvoiceDateSearch.Select();
                                return false;
                            }
                        }

                        if (dtService.J_IsBlankDateCheck(ref mskAccountEntryDate, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskInvoiceDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Enter valid Account Entry Date to search.");
                                mskAccountEntryDateSearch.Select();
                                return false;
                            }
                        }

                        if (dtService.J_IsBlankDateCheck(ref mskBankEntryDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskBankEntryDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Enter valid Bank Entry Date to search.");
                                mskBankEntryDateSearch.Select();
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {

                    //if (dtService.J_IsDateValid(mskAccountEntryDate) == false)
                    //{
                    //    cmnService.J_UserMessage("Enter valid Account Entry Date.");
                    //    mskAccountEntryDate.Select();
                    //    return false;
                    //}

                    //if (dtService.J_IsDateValid(mskBankDate) == false)
                    //{
                    //    cmnService.J_UserMessage("Enter valid Bank Date.");
                    //    mskBankDate.Select();
                    //    return false;
                    //}

                    //Added by Shrey Kejriwal on 20-05-2013
                    if (cmbPaymentType.Text == "CASH RECEIVED")
                    {
                        if (dtService.J_IsBlankDateCheck(ref mskBankDate, J_ShowMessage.NO) == false)
                        {
                            cmnService.J_UserMessage("Bank Date should not be provided for \"Cash Received\" payment type.");
                            mskBankDate.Select();
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

        #region GetDateDifference(string Date1, string Date2)
        public string GetDateDifference(string Date1, string Date2)
        {
            string strDaysWithText = "";
            try
            {
                int intDaysDifference = 0;

                Date1 = Convert.ToString(dtService.J_ConvertToIntYYYYMMDD(Date1));
                Date1 = Convert.ToString(Date1.Substring(0, 4) + "-" + Date1.Substring(4, 2) + "-" + Date1.Substring(6,2));


                Date2 = Convert.ToString(dtService.J_ConvertToIntYYYYMMDD(Date2));
                Date2 = Convert.ToString(Date2.Substring(0, 4) + "-" + Date2.Substring(4, 2) + "-" + Date2.Substring(6, 2));

                strSQL = "SELECT DATEDIFF(DAY,'" + Date1 + "','" + Date2 + "') AS DiffDate";

                intDaysDifference = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (Convert.ToInt32(intDaysDifference) >= 0)
                {
                    strDaysWithText = Convert.ToString(Math.Abs(intDaysDifference) + " Days Later.");
                    return strDaysWithText;
                }
                else if (Convert.ToInt32(intDaysDifference) < 0)
                {
                    strDaysWithText = Convert.ToString(Math.Abs(intDaysDifference) + " Days Earlier.");
                    return strDaysWithText;
                }
                return strDaysWithText;
            }
            catch (Exception err)
            {
                return strDaysWithText;
            }
        }
        #endregion 

        


        #endregion

    }
}

