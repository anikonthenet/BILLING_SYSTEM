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
    public partial class TrnCollection : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnCollection()
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
        string strTaggedInvoiveId = "";
        string strTaggedCollectionDetailId = "";
        string[,] strMatrix;
        string SearchAllText = "Press Alt + F";
        string strSearch = "";
        //string strCheckBox = "";
        string strCD = ""; string strSerialNo = ""; string strInvoice = "";
        //-----------------------------------------------------------------------
        int intTempGridPosition = 0;
        int lngDetailGridColumns = 10;
        //Added By dhrub On 10/04/2015
        int intBankInCollectionFlag = 0;
        int intReferenceNoFlag = 0;
        //-----------------------------------------------------------------------
        bool blnExit = false;
        bool blnIsAmount =  false;
        bool blnTagDeductee = false;
        bool blnPaymentType = true;
        bool blnCollectionMode = true;
        bool blnReconDateIsEnabled = true; 
        //-----------------------------------------------------------------------
        long lngSearchId = 0;
        //-----------------------------------------------------------------------
        double dblCollectionAmt = 0;

        #endregion

        #region ENUM decleration of Detail Grid Column
        //-- enum for setting detail grid column
        enum enmInvoiceCollection
        {

            INVOICE_HEADER_ID = 1,
            INVOICE_DATE = 2,
            INVOICE_NO = 3,
            PARTY_NAME = 4,
            TYPE = 5,
            REFERENCE_NO = 6,
            BILL_AMOUNT = 7,
            BALANCE_AMOUNT = 8,
            BALANCE_AMOUNT_HIDDEN = 9,
            COLLECTION_AMOUNT = 10,
            INVOICE_DATE_YYYYMMDD = 11
        }
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
            //-- SETUP DETAILS 
            BillingSystem.T_GetSetupDetails();
            //--
            ClearControls(); 
            ControlVisible(true);
            //
            lblTitle.Text = this.Text;

            strSQL = " SELECT COMPANY_ID," +
                     "        COMPANY_NAME " +
                     " FROM   MST_COMPANY " +
                     " WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                     " AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + 
                     " ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;
            //
            //--
            cmbCollectionMode.Items.Clear();
            cmbCollectionMode.Items.Add(BS_Collection_Mode.All);
            cmbCollectionMode.Items.Add(BS_Collection_Mode.Mismatch);
            cmbCollectionMode.Items.Add(BS_Collection_Mode.Unknown);
            cmbCollectionMode.Items.Add(BS_Collection_Mode.Unreconcile);
            cmbCollectionMode.Items.Add(BS_Collection_Mode.Tallied);
            cmbCollectionMode.SelectedIndex = 0;
            //
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
                LoadDetailGrid(0, lblMode.Text);
                cmbCompany.Enabled = false;
                cmbCollectionMode.Enabled = false;
                grpSearchPanel.Enabled = false;
                mskCollectionDate.Select();
                strCheckFields = "";
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
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
                        //
                        //cmbItemName.Enabled = true;
                    }
                    //
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    //
                    grpSearchDetailPanel.Enabled = false;
                    cmbCompany.Enabled = false;
                    cmbCollectionMode.Enabled = false;
                    grpSearchPanel.Enabled = false;

                    //Modified By Dhrub On 07/04/2015 
                    //Payment type will be enabled Untill any invoices tagged against a collection[Unknown Payments]
                    if (strTaggedInvoiveId != "")
                        cmbPaymentType.Enabled = false;
                    //
                    chkShowAll.Checked = false;
                    //chkShowAll.Visible = true;
                    //
                    mskCollectionDate.Select();
                    strCheckFields = "";
                }
                else
                {
                    cmnService.J_UserMessage("No record selected");
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
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
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId) == false)
                    BtnAdd.Select();
                //cmbCompany.Enabled = true;
                //
                //BtnAdd.Enabled = false;
                //BtnEdit.Enabled = false;
                BtnSearch.Enabled = false;
                BtnSort.Enabled = false;
                BtnPrint.Enabled = false;
                
                //BtnAdd.BackColor = Color.LightGray; 
                //BtnEdit.BackColor = Color.LightGray;
                BtnSort.BackColor = Color.LightGray;
                BtnSearch.BackColor = Color.LightGray;
                BtnPrint.BackColor = Color.LightGray;

                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;
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
                
                //BtnAdd.BackColor = Color.LightGray; 
                BtnSort.BackColor = Color.LightGray;
                BtnSearch.BackColor = Color.LightGray;
                BtnPrint.BackColor = Color.LightGray;

                //BtnDelete.Enabled = false;
                //BtnDelete.BackColor = Color.LightGray;
                // clear controls
                ClearControls();

                txtSearchAll.Text = "";
 
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


                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
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


        #region txtSearchDetailAll_TextChanged
        private void txtSearchDetailAll_TextChanged(object sender, EventArgs e)
        {
            ADODB._Recordset rsDetailGrid = null;
            try
            {
                //----------------------------------------------------------------------
                //if (blnSearchAll == false)
                //    return;
                //--
                if (txtSearchDetailAll.Text == SearchAllText)
                    return;
                //--
                if (txtSearchDetailAll.Text.Length > 0)
                    txtSearchDetailAll.BackColor = Color.White;
                else
                    txtSearchDetailAll.BackColor = Color.AliceBlue;
                //--
                strSearch = " AND (TRN_INVOICE_HEADER.INVOICE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),'') LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.PARTY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.REFERENCE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.NET_AMOUNT LIKE '%" + cmnService.J_ReplaceQuote(txtSearchDetailAll.Text.Trim().ToUpper()) + "%') ";
                //--
                strSQL = strDetailQuery + strSearch + strDetailGroupBy + " ORDER BY " + strDetailOrderBy;
                //-----------------------------------------------------------
                blnTagDeductee = false;
                //--
                rsDetailGrid = dmlService.J_ExecSqlReturnADODBRecordset(strSQL);
                if (rsDetailGrid == null) return;
                //
                setDetailsGridRefresh(flxgrdDetails);
                //
                if (rsDetailGrid.RecordCount > 0)
                {
                    //-- Clear the Flexgrid data
                    flxgrdDetails.Clear();
                    flxgrdDetails.DataSource = (msdatasrc.DataSource)rsDetailGrid;
                    //-- Formatting Amount as 0.00
                    NumberFormatDetail(flxgrdDetails);
                }
                else
                    flxgrdDetails.FixedRows = 1;
                //--
                rsDetailGrid.Close();
                setDetailsGridColumns(flxgrdDetails);
                //--
                blnTagDeductee = true;
                //
                //----------------------------------------------------------------------
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
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
                LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex));
                //--
                
                
                ////--
                //strSearch = "AND (TRN_COLLECTION_HEADER.COLLECTION_DATE LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR MST_BANK.BANK_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR TRN_COLLECTION_HEADER.REFERENCE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR TRN_COLLECTION_HEADER.NET_AMT LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR TRN_COLLECTION_HEADER.DUE_AMT LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR ISNULL(INVREF.INVOICE_NO,'')  LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR ISNULL(INVREF.PARTY_NAME,'')  LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR TRN_COLLECTION_HEADER.COLLECTION_REMARKS LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                //            "     OR ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
                ////--
                //strSQL = strQuery + strSearch + " ORDER BY " + strOrderBy;
                ////--
                //if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                //if (dsetGridClone == null) return;
                ////--
            }
            catch(Exception err)
            {
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
            {
                //txtSearchAll.Text = "";
                //chkCD.Checked = false;
                //chkSerialNo.Checked = false;
                //chkInvoice.Checked = false;
                //cmnService.J_ClearComboBox(ref cmbInvoiceSeries);
            }
            //--
            LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex));
        }
        #endregion 

        #region cmbInvoiceSeries_SelectedIndexChanged
        private void cmbInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbCompany.SelectedIndex <= 0)
            //{
            //    txtSearchAll.Text = "";
            //    chkCD.Checked = false;
            //    chkSerialNo.Checked = false;
            //    chkInvoice.Checked = false;                
            //    LoadGrid(0, 0);
            //    return;
            //}
            ////--
            //if (cmbInvoiceSeries.SelectedIndex <= 0)
            //{
            //    txtSearchAll.Text = "";
            //    chkCD.Checked = false;
            //    chkSerialNo.Checked = false;
            //    chkInvoice.Checked = false; 
            //    LoadGrid(0, 0);
            //    return;
            //}
            ////--
            //txtSearchAll.Text = "";
            //chkCD.Checked = false;
            //chkSerialNo.Checked = false;
            //chkInvoice.Checked = false; 
            ////--
            //LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex), cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex));
        }
        #endregion

        #region cmbPaymentType_SelectedIndexChanged
        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentType.SelectedIndex <= 0)
            {
                lblAutoCollectionPostFlag.Text = "0";
                lblSundryPartyFlag.Text = "0";
                //
                if (lblMode.Text == J_Mode.Add)
                    LoadDetailGrid(0, lblMode.Text);
                else if (lblMode.Text == J_Mode.Edit)
                    LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
                return;
            }
            //
            //--
            if (blnPaymentType == true)
            {
                if (cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) == BS_PaymentTypeId.Cc_Avenue)
                {
                    txtLessAmount.Enabled = true;
                    txtLessAmount.Text = "0.00";
                }
                else
                {
                    txtLessAmount.Enabled = false;
                    txtLessAmount.Text = "0.00";
                }
            }
            //--
            lblSundryPartyFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUNDRY_PARTY_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            lblAutoCollectionPostFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT AUTO_COLLECTION_POST_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            intBankInCollectionFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT BANK_IN_COLLECTION_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            intReferenceNoFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT REFERENCE_NO_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            //--
            if(lblMode.Text == J_Mode.Add)
                LoadDetailGrid(0, lblMode.Text);
            else if(lblMode.Text == J_Mode.Edit)
                LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
        }
        #endregion

        //--
        #region cmbCollectionMode_SelectedIndexChanged
        private void cmbCollectionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex));
            }
            catch (Exception err)
            { 
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion 

        //--
        #region flxgrdDetails_ClickEvent
        private void flxgrdDetails_ClickEvent(object sender, EventArgs e)
        {
            if (flxgrdDetails.Row == 1
                && (flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT))
                flxgrdDetails.RowSel = 1;
            setTextBoxInGrid();
        }
        #endregion

        #region flxgrdDetails_KeyPressEvent
        private void flxgrdDetails_KeyPressEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent e)
        {
            if (e.keyAscii == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceCollection.INVOICE_DATE
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.PARTY_NAME
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.TYPE
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.REFERENCE_NO
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.BILL_AMOUNT
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT
                    || flxgrdDetails.Col == (int)enmInvoiceCollection.INVOICE_DATE_YYYYMMDD)
                {
                    flxgrdDetails.Col = (int)enmInvoiceCollection.COLLECTION_AMOUNT;
                    setTextBoxInGrid();
                }
            }
        }
        #endregion

        #region flxgrdDetails_MouseMoveEvent
        private void flxgrdDetails_MouseMoveEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEvent e)
        {
            if (blnExit == true) return;
            cmnService.J_GridToolTip(flxgrdDetails, e.x, e.y);
        }
        #endregion

        #region flxgrdDetails_KeyDownEvent
        private void flxgrdDetails_KeyDownEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEvent e)
        {
            //if (e.keyCode == 112 && flxgrdDetails.Col == (int)enmInvoiceCollection.ITEM_NAME) PopulateItemMaster();
            //if (e.keyCode == 46)
            //{
            //    if (cmnService.J_UserMessage(J_Msg.WantToRemove,
            //        J_Var.J_pProjectName,
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button2) == DialogResult.No)
            //    {
            //        flxgrdDetails.Row = flxgrdDetails.RowSel;
            //        flxgrdDetails.Col = flxgrdDetails.ColSel;
            //        flxgrdDetails.Select();

            //        return;
            //    }

            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.ITEM_ID, "0");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.ITEM_NAME, "");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.QUANTITY, "");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.UNIT, "");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.RATE, "");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.AMOUNT, "");
            //    flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceCollection.REMARKS, "");

            //    CalculateNetAmount();
            //}
        }
        #endregion

        #region flxgrdDetails_DblClick
        private void flxgrdDetails_DblClick(object sender, EventArgs e)
        {
            //if (flxgrdDetails.Col == (int)enmInvoiceCollection.ITEM_NAME) PopulateItemMaster();
        }
        #endregion

        #region flxgrdDetails_DblClick
        private void flxgrdDetails_Scroll(object sender, EventArgs e)
        {
            txtQty.Visible = false;
        }
        #endregion

        //--
        #region txtQty_KeyPress
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == Convert.ToChar(Keys.Insert))
            {
                e.Handled = true;
                blnIsAmount = false;
                return;
            }
            //-----------------------------------
            blnIsAmount = true;
            //-----------------------------------
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT)
                {
                    if (intTempGridPosition == flxgrdDetails.Rows - 1)
                    {
                        BtnSave.Focus();
                    }
                    else
                    {
                        flxgrdDetails.Col = (int)enmInvoiceCollection.COLLECTION_AMOUNT;
                        flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row + 1);
                        flxgrdDetails.Select();
                        setTextBoxInGrid();
                    }
                }
            }
            else
            {
                if (flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "V,10,2", txtQty, "") == false) e.Handled = true;
                }
            }
        }
        #endregion

        #region txtQty_TextChanged
        private void txtQty_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (blnIsAmount == true)
                {
                    if (flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT)
                    {
                        if (txtQty.Text.Trim() == "" || txtQty.Text.Trim() == "-" || txtQty.Text.Trim() == "-." || txtQty.Text.Trim() == ".")
                            flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.COLLECTION_AMOUNT, "0.00");
                        else
                            flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.COLLECTION_AMOUNT, (txtQty.Text == "" ? "0.00" : txtQty.Text));
                        //
                        //if(cmnService.J_ReturnDoubleValue(txtQty.Text) > cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition,(int)enmInvoiceCollection.BALANCE_AMOUNT)))
                        //    txtQty.Text = "0.00";
                        //if (cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN)) > 0)
                        //|| Convert.ToString(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT)) == "")

                        //if (cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN)) > 0)
                        //{

                            flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT,
                                string.Format("{0:0.00}",
                                             ((cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN)) -
                                               cmnService.J_ReturnDoubleValue(txtQty.Text)))));
                        //}
                        //else
                        //{
                        //   //txtQty.Text = "0";
                        //    flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT,
                        //        string.Format("{0:0.00}",
                        //                     (cmnService.J_ReturnDoubleValue(txtQty.Text) - cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT)))));
                        //}
                        CalculateTaggedAmount();
                    }
                    //
                    NumberFormatDetail(flxgrdDetails);
                    //CalculateNetAmount();
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region txtQty_Leave
        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Visible = false;
        }
        #endregion

        #region txtQty_KeyDown
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                blnIsAmount = true;
                if (flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BILL_AMOUNT) == flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN))
                    txtQty.Text = flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BILL_AMOUNT);
                else
                    txtQty.Text = flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN);
            }
            //else if (e.KeyCode == Keys.Space)
            //{
            //    return;
            //}
        }
        #endregion

        //--
        #region chkShowAll_CheckedChanged
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            //Blocked By Dhruba On 10/04/2015
            //if (chkShowAll.Checked == true)
            //{
            //    //LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
            //}
            //else if (chkShowAll.Checked == false)
            //{
            //    //LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
            //}

            //Added By Dhruba On 10/04/2015
            if (lblMode.Text == J_Mode.Add)
                LoadDetailGrid(0, lblMode.Text);
            else if (lblMode.Text == J_Mode.Edit)
                LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
        }
        #endregion


        #region rdbnInvoiceNo_CheckedChanged
        private void rdbnInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            //Added By Dhruba On 10/04/2015
            if (lblMode.Text == J_Mode.Add)
                LoadDetailGrid(0, lblMode.Text);
            else if (lblMode.Text == J_Mode.Edit)
                LoadDetailGrid(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString()), lblMode.Text);
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
            
            //BtnAdd.BackColor = Color.LightGray;
            //BtnEdit.BackColor = Color.LightGray;
            BtnSort.BackColor = Color.LightGray;
            BtnSearch.BackColor = Color.LightGray;
            BtnPrint.BackColor = Color.LightGray;

            //BtnDelete.Enabled = false;
            //BtnDelete.BackColor = Color.LightGray;

            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            //--------------------------------------
            //ADDED BY DHRUB ON 07/04/2015
            //--------------------------------------
            strTaggedCollectionDetailId = "";
            strTaggedInvoiveId = "";
            //--------------------------------------
            mskCollectionDate.Text = "";
            mskReconcileDate.Text = "";
            txtReferenceNo.Text = "";
            txtRemarks.Text = "";
            txtGrossAmount.Text = "0.00";
            txtGrossAmount.Text = "0.00";
            txtLessAmount.Text = "0.00";
            txtNetReceived.Text = "0.00";
            txtTaggedTotal.Text = "0.00";
            txtDueAmount.Text = "0.00";
            // 
            blnPaymentType = false; 
            //Populating Payment Type Combo
            strSQL = " SELECT PAYMENT_TYPE_ID," +
                     "        PAYMENT_TYPE_DESCRIPTION " +
                     " FROM   MST_PAYMENT_TYPE " +
                     " WHERE  INACTIVE_FLAG = 0 " +
                     " AND    HIDE_IN_COLLECTION_FLAG = 0 "+
                     " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType) == false) return;
            blnPaymentType = true;

            //Populating Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBank, 0) == false) return;
            grpSearchDetailPanel.Enabled = true;
            //--
            cmbCompany.Enabled = true;
            cmbCollectionMode.Enabled = true;

            grpSearchPanel.Enabled = true;
            //chkShowAll.Visible = false;
            chkShowAll.Checked = false;
            //--
            cmbPaymentType.Enabled = true;
            //--
            lblAutoInvoiceNo.Text = "";
            lblCollectionMode.Text = "";
            //--
            intBankInCollectionFlag = 0;
            intReferenceNoFlag = 0;
            //--
            rdbnCollectionAmt.Checked = true;
            //--
            mskReconcileDate.Enabled = true;
            blnReconDateIsEnabled = true;
            //--
            if (J_Var.J_pLastReconcileDate != "")
            {
                lblSetupReconcileDate.Text = "Last Reconcile date " + J_Var.J_pLastReconcileDate;
                lblSetupReconcileDate.Visible = true;
            }
            else
                lblSetupReconcileDate.Visible = false;


            //-------------------------------------------------
            //ADDED BY DHRUB ON 05/08/2015
            //TALLIED APPLICABLE ONLY FOR UNKNOWN ENTRIES 
            //-------------------------------------------------
            chkTallied.Visible = false;
            chkTallied.Checked = false;
            //-------------------------------------------------
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
            string[,] strMatrix1 =  {{"COLLECTION_HEADER_ID", "0", "", "Left", "", "", ""},
							         {"Coll.Dt.", "70", "", "Left", "", "", ""},
                                     {"Type", "100", "", "Left", "", "", ""},
                                     {"Bank", "40", "", "Left", "", "", ""},
                                     {"Ref. No.", "100", "", "Left", "", "", ""},
                                     {"Net Amount", "90", "0.00", "Right", "", "", ""},
                                     {"Due Amount", "90", "", "Right", "", "", ""},
                				     {"Inv.No.", "100", "", "Left", "", "", ""},
							         {"Party Name", "110", "", "Left", "", "", ""},
							         {"Recon.Dt.", "70", "", "Left", "", "", ""},
							         {"Remarks", "70", "", "Left", "", "", ""},
                                     {"Tallied", "50", "", "Left", "", "", ""},
							         {"", "50", "", "CENTER", "", "", ""}};
            //
            strMatrix = strMatrix1;
            //
            string[,] strAutoFlagMatrix = {{"=0", "N", "Manual", "T"},
                                           {"=1", "N", "", "T"}};

            string[,] strCollectionTalliedMatrix = {{"=0", "N", "", "T"},
                                                    {"=1", "N", "Tallied", "T"}};
            //
            strOrderBy = " TRN_COLLECTION_HEADER.COLLECTION_DATE DESC, TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID DESC ";
            //
            strQuery = @"SELECT TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID                                 AS COLLECTION_HEADER_ID,
                                CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103)                AS COLLECTION_DT,
                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                                  AS PAYMENT_TYPE_DESCRIPTION,
                                MST_BANK.BANK_NAME                                                         AS BANK_NAME,
                                TRN_COLLECTION_HEADER.REFERENCE_NO                                         AS REFERENCE_NO,
                                TRN_COLLECTION_HEADER.NET_AMT                                              AS NET_AMT,
                                CASE WHEN TRN_COLLECTION_HEADER.DUE_AMT = 0 
                                          THEN ''
                                          ELSE CAST(TRN_COLLECTION_HEADER.DUE_AMT AS VARCHAR)                                             
                                END                                                                        AS DUE_AMT,
                                ISNULL(INVREF.INVOICE_NO,'')                                               AS INVOICE_NO,
                                ISNULL(INVREF.PARTY_NAME,'')                                               AS PARTY_NAME,
                                ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') AS RECONCILIATION_DT,
                                TRN_COLLECTION_HEADER.COLLECTION_REMARKS                                   AS COLLECTION_REMARKS,
                                " + cmnService.J_SQLDBFormat("TRN_COLLECTION_HEADER.TALLIED", J_SQLColFormat.Case_End, strCollectionTalliedMatrix) + @" AS TALLIED,
                                " + cmnService.J_SQLDBFormat("TRN_COLLECTION_HEADER.AUTO_POST_FLAG", J_SQLColFormat.Case_End, strAutoFlagMatrix) + @" AS AUTO_POST_FLAG                                    
                         FROM   TRN_COLLECTION_HEADER
                                INNER JOIN MST_COMPANY          ON TRN_COLLECTION_HEADER.COMPANY_ID           = MST_COMPANY.COMPANY_ID
                                INNER JOIN MST_PAYMENT_TYPE     ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                LEFT  JOIN MST_BANK             ON TRN_COLLECTION_HEADER.BANK_ID              = MST_BANK.BANK_ID
                                LEFT  JOIN 
                                (
                                SELECT TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID,
                                       TRN_INVOICE_HEADER.INVOICE_NO,
                                       MST_PARTY.PARTY_NAME
                                FROM   TRN_COLLECTION_DETAIL,
                                       TRN_INVOICE_HEADER,
                                       MST_PARTY       
                                WHERE  TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                                AND    TRN_INVOICE_HEADER.PARTY_ID             = MST_PARTY.PARTY_ID
                                ---------------- added on 20-04-2015
                                AND    TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID IN (SELECT COLLECTION_HEADER_ID 
                                                                                      FROM   TRN_COLLECTION_DETAIL AS COLLDET
                                                                                      GROUP BY COLLECTION_HEADER_ID
                                                                                      HAVING COUNT(COLLECTION_HEADER_ID) = 1)
                                ) AS INVREF ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = INVREF.COLLECTION_HEADER_ID
                                ---------------- added on 20-04-2015
                         WHERE  TRN_COLLECTION_HEADER.COMPANY_ID = " + CompanyID + @" 
                         AND    TRN_COLLECTION_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + @" 
                         AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0 ";
            if (cmbCollectionMode.Text == BS_Collection_Mode.Unknown)
                strQuery = strQuery + @" AND TRN_COLLECTION_HEADER.NET_INVOICE_AMT  = 0 
                                         AND TRN_COLLECTION_HEADER.TALLIED = 0 ";
            else if (cmbCollectionMode.Text == BS_Collection_Mode.Mismatch)
                strQuery = strQuery + @" AND TRN_COLLECTION_HEADER.NET_INVOICE_AMT  <> 0
                                         AND TRN_COLLECTION_HEADER.GROSS_AMT  <> TRN_COLLECTION_HEADER.NET_INVOICE_AMT ";
            else if (cmbCollectionMode.Text == BS_Collection_Mode.Unreconcile)
                strQuery = strQuery + @" AND TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS NULL ";
            else if (cmbCollectionMode.Text == BS_Collection_Mode.Tallied)
                strQuery = strQuery + @" AND TRN_COLLECTION_HEADER.NET_INVOICE_AMT  = 0 
                                         AND TRN_COLLECTION_HEADER.TALLIED = 1 ";


            if (txtSearchAll.Text.Trim() != "")
                strQuery = strQuery + @" AND (TRN_COLLECTION_HEADER.COLLECTION_DATE LIKE '%"        + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR MST_BANK.BANK_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR TRN_COLLECTION_HEADER.REFERENCE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR TRN_COLLECTION_HEADER.NET_AMT LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR TRN_COLLECTION_HEADER.DUE_AMT LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR ISNULL(INVREF.INVOICE_NO,'')  LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR ISNULL(INVREF.PARTY_NAME,'')  LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR TRN_COLLECTION_HEADER.COLLECTION_REMARKS LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                                       "     OR ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
            else
                strSearch = "";

            //--
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            //--
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //--
        }

        #endregion

        #region LoadDetailGrid
        private void LoadDetailGrid(long CollectionHeaderID, string Mode)
        {
            ADODB._Recordset rsDetailGrid = null;
            long PaymentTypeID = 0;
            try
            {
                if(cmbPaymentType.SelectedIndex>0)
                    PaymentTypeID = cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex);

                //if (Mode == J_Mode.Add)
                #region Add Mode
                if (CollectionHeaderID == 0)
                {
                    //string[,] strMatrixViewDeductee = {{"INVOICE_HEADER_ID", "0", "", "R", "", "F", ""},
                    //                    {"Invoice Date", "100", "dd/MM/yyyy", "", "", "T", "fill"},
                    //                    {"Party Name", "150", "S", "", "", "T", ""},
                    //                    {"Type", "80", "S", "", "", "T", ""},
                    //                    {"Reference No.", "150", "S", "", "", "T", ""},
                    //                    {"Bill Amount", "75", "0.00", "R", "", "T", "fill"},
                    //                    {"Tagged Amount", "75", "0.00", "R", "", "T", "fill"}};
                    //-----------------------------------------------------------

                    //-----------------------------------------------------------
                    //Modified By dhrub On 10/04/2015[ SORT ORDER ]
                    //-----------------------------------------------------------
                    if (rdbnCollectionAmt.Checked == true)
                        strDetailOrderBy = "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";
                    if (rdbnInvoiceDate.Checked == true)
                        strDetailOrderBy = " CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112) ";
                    else if (rdbnInvoiceNo.Checked == true)
                        strDetailOrderBy = "TRN_INVOICE_HEADER.INVOICE_NO ";
                    else if (rdbnPartyName.Checked == true)
                        strDetailOrderBy = "MST_PARTY.PARTY_NAME ";
                    else if (rdbnReferenceNo.Checked == true)
                        strDetailOrderBy = "TRN_INVOICE_HEADER.REFERENCE_NO ";
                    //-----------------------------------------------------------
                    //--
                    strDetailQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID                                                   AS INVOICE_HEADER_ID,
                                              CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)                                  AS INVOICE_DATE,
                                              TRN_INVOICE_HEADER.INVOICE_NO                                                          AS INVOICE_NO,
                                              MST_PARTY.PARTY_NAME                                                                   AS PARTY_NAME,
                                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                                              AS PAYMENT_TYPE_DESCRIPTION,
                                              TRN_INVOICE_HEADER.REFERENCE_NO                                                        AS REFERENCE_NO,
                                              TRN_INVOICE_HEADER.NET_AMOUNT                                                          AS INVOICE_AMOUNT,
                                              TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT_HIDDEN,
                                              TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT, 
                                              '0.00'                                                                                 AS COLLECTION_AMOUNT,
                                              CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112)                                  AS INVOICE_DATE_YYYYMMDD
                                       FROM   TRN_INVOICE_HEADER INNER JOIN MST_PARTY 
                                              ON TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID
                                              INNER JOIN MST_PAYMENT_TYPE 
                                              ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID         = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                              LEFT JOIN TRN_COLLECTION_DETAIL 
                                              ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID       = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                       WHERE  TRN_INVOICE_HEADER.RECON_FLAG                 = 0
                                       AND   (TRN_INVOICE_HEADER.TRAN_TYPE                  = 'INV'
                                              OR TRN_INVOICE_HEADER.TRAN_TYPE               = 'OINV')
                                       AND    MST_PARTY.PARTY_CATEGORY_ID                   = 0 ";

                    //------------------------------------------------------------------------------------------------------------------------------------------------
                    //-- Add Mode : 
                    //-- IF PaymentType is CCAVENUE THEN IT WILL POPULATE ALL CcAvenue Invoices having Due Amount > 0
                    //-- IF PaymentType is Other than CCAVENUE THEN IT WILL POPULATE ALL Invoices having Due Amount > 0 && Other Than CcAvenue Payment Type
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    if (PaymentTypeID > 0 && PaymentTypeID == BS_PaymentTypeId.Cc_Avenue)
                    {
                        strDetailQuery = strDetailQuery + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    }
                    else if (PaymentTypeID > 0 && PaymentTypeID != BS_PaymentTypeId.Cc_Avenue && chkShowAll.Checked == false)
                    {
                        strDetailQuery = strDetailQuery + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    }
                    //--
                    strDetailGroupBy = @" GROUP BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),
                                                   MST_PARTY.PARTY_NAME,
                                                   MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                                   TRN_INVOICE_HEADER.REFERENCE_NO,
                                                   TRN_INVOICE_HEADER.NET_AMOUNT, 
                                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112)";

                    //if (chkShowAll.Checked == false)
                    //strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT <> (TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0))  ";
                    ////strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT <> ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) ";
                    //else

                    
                    strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT > ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) ";

                    //strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT > ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0)  ";

                    //-----------------------------------------------------------
                    if (chkShowAll.Checked == false)
                        strSQL = strDetailQuery + strDetailGroupBy + strDetailHaving + " ORDER BY " + strDetailOrderBy;
                    else
                        strSQL = strDetailQuery + strDetailGroupBy + " ORDER BY " + strDetailOrderBy;
                    //-----------------------------------------------------------
                    blnTagDeductee = false;
                    //--
                    rsDetailGrid = dmlService.J_ExecSqlReturnADODBRecordset(strSQL);
                    if (rsDetailGrid == null) return;
                    //
                    setDetailsGridRefresh(flxgrdDetails);
                    //
                    if (rsDetailGrid.RecordCount > 0)
                    {
                        //-- Clear the Flexgrid data
                        flxgrdDetails.Clear();
                        flxgrdDetails.DataSource = (msdatasrc.DataSource)rsDetailGrid;
                        //-- Formatting Amount as 0.00
                        NumberFormatDetail(flxgrdDetails);
                        //
                        setDetailsGridColumns(flxgrdDetails);
                        //rsDetailGrid.Close();
                    }
                    else
                        flxgrdDetails.FixedRows = 1;
                    //--
                    //setDetailsGridColumns(flxgrdDetails);
                    rsDetailGrid.Close();
                    //--
                    blnTagDeductee = true;
                    //
                }
                #endregion 
                //
                #region Edit Mode
                else // if (Mode == J_Mode.Edit)
                {
                    strDetailSearchQuery = "";
                    strDetailSearchQuery_UnionAll = "";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //ADDED BY DHRUB ON 07/04/2015
                    //--------------------------------------
                    //--------------------------------------
                    strSQL = @" SELECT --TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID   AS COLLECTION_HEADER_ID,
                                       SUBSTRING((SELECT ',''' + CAST(TRN_INVOICE_HEADER.INVOICE_HEADER_ID AS VARCHAR) + ''''
                                       FROM   TRN_COLLECTION_DETAIL
                                       INNER JOIN TRN_INVOICE_HEADER
                                       ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                                       WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID =  " + CollectionHeaderID + @"
                                       FOR XML PATH('')),2,10000) AS TAGGED_INVOICES 
                                FROM   TRN_COLLECTION_DETAIL
                                       INNER JOIN TRN_INVOICE_HEADER
                                       ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                                WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID =  " + CollectionHeaderID + @"
                                GROUP BY TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID ";

                    strTaggedInvoiveId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //---------------------------------------------------------------------------------------------------------------------
                    //--SQL PART 1: Retrieve Those Invoices which have Due Amount > 0 && Not Tagged with the Selected Collection_header_Id
                    //---------------------------------------------------------------------------------------------------------------------

                    //-----------------------------------------------------------
                    //Modified By dhrub On 10/04/2015
                    //-----------------------------------------------------------
                    if (rdbnCollectionAmt.Checked == true)
                        strDetailOrderBy = "TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT DESC, TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";
                    //--
                    else if (rdbnInvoiceNo.Checked == true)
                        strDetailOrderBy = "TRN_INVOICE_HEADER.INVOICE_NO ";
                    //--
                    else if (rdbnInvoiceDate.Checked == true)
                        strDetailOrderBy = " CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112) ";
                    //--
                    else if (rdbnPartyName.Checked == true)
                        strDetailOrderBy = "MST_PARTY.PARTY_NAME ";
                    //--
                    else if (rdbnReferenceNo.Checked == true)
                        strDetailOrderBy = "TRN_INVOICE_HEADER.REFERENCE_NO ";

                    
                    //--
                    strDetailQuery = @" SELECT  TRN_INVOICE_HEADER.INVOICE_HEADER_ID                  AS INVOICE_HEADER_ID,
                                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE,
                                                TRN_INVOICE_HEADER.INVOICE_NO                         AS INVOICE_NO,
                                                MST_PARTY.PARTY_NAME                                  AS PARTY_NAME,
                                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION             AS PAYMENT_TYPE_DESCRIPTION,
                                                TRN_INVOICE_HEADER.REFERENCE_NO                       AS REFERENCE_NO,
                                                TRN_INVOICE_HEADER.NET_AMOUNT                         AS INVOICE_AMOUNT,
                                                TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT_HIDDEN,
                                                TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT, 
                                                SUM(CASE WHEN TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = " + CollectionHeaderID + @" THEN TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE '0.00' END) AS COLLECTION_AMOUNT,
                                                CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112)                                  AS INVOICE_DATE_YYYYMMDD
                                         FROM   TRN_INVOICE_HEADER INNER JOIN MST_PARTY 
                                                ON TRN_INVOICE_HEADER.PARTY_ID             = MST_PARTY.PARTY_ID
                                                LEFT JOIN MST_PAYMENT_TYPE 
                                                ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                                LEFT JOIN TRN_COLLECTION_DETAIL 
                                                ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID    = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                        WHERE   TRN_INVOICE_HEADER.RECON_FLAG              = 0
                                        AND    (TRN_INVOICE_HEADER.TRAN_TYPE               = 'INV'
                                                OR TRN_INVOICE_HEADER.TRAN_TYPE            = 'OINV')
                                        AND     MST_PARTY.PARTY_CATEGORY_ID                = 0";
                    //--
                    //---------------------------------------------------------------------------
                    //BLOCKED BY DHRUB ON 09/04/2015 
                    //---------------------------------------------------------------------------
                    //if(PaymentTypeID > 0)
                    //    strDetailQuery = strDetailQuery + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + PaymentTypeID + @" ";
                    //------------------------------------------------------------------------------------------------------------------------------------------------
                    //-- Edit Mode : 
                    //-- IF PaymentType is CCAVENUE THEN IT WILL POPULATE ALL CcAvenue Invoices having Due Amount > 0
                    //-- IF PaymentType is Other than CCAVENUE THEN IT WILL POPULATE ALL Invoices having Due Amount > 0 && Other Than CcAvenue Payment Type
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    if (PaymentTypeID > 0 && PaymentTypeID == BS_PaymentTypeId.Cc_Avenue)
                    {
                        strDetailQuery = strDetailQuery + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    }
                    else if (PaymentTypeID > 0 && PaymentTypeID != BS_PaymentTypeId.Cc_Avenue && chkShowAll.Checked == false)
                    {
                        strDetailQuery = strDetailQuery + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    }
                    //--
                    strDetailGroupBy = @" GROUP BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),
                                                   MST_PARTY.PARTY_NAME,
                                                   MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                                   TRN_INVOICE_HEADER.REFERENCE_NO,
                                                   TRN_INVOICE_HEADER.NET_AMOUNT,
                                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112)";

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //UNION ALL 
                    //---------------------------------------------------------------------------------------------------
                    //--SQL PART 2: Retrieve Only Those Invoices which are Tagged with The Collection_Header_ID
                    //---------------------------------------------------------------------------------------------------

                    strDetailQuery_UnionAl = @" SELECT  TRN_INVOICE_HEADER.INVOICE_HEADER_ID                  AS INVOICE_HEADER_ID,
                                                        CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE,
                                                        TRN_INVOICE_HEADER.INVOICE_NO                         AS INVOICE_NO,
                                                        MST_PARTY.PARTY_NAME                                  AS PARTY_NAME,
                                                        MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION             AS PAYMENT_TYPE_DESCRIPTION,
                                                        TRN_INVOICE_HEADER.REFERENCE_NO                       AS REFERENCE_NO,
                                                        TRN_INVOICE_HEADER.NET_AMOUNT                         AS INVOICE_AMOUNT,
                                                        TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT_HIDDEN,
                                                        TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS BALANCE_AMOUNT, 
                                                        COLLECTION.COLLECTION_AMOUNT                          AS COLLECTION_AMOUNT,
                                                        CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112) AS INVOICE_DATE_YYYYMMDD " +
                                                        //CASE WHEN TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = " + CollectionHeaderID + @" THEN TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE '0.00' END AS COLLECTION_AMOUNT
                                             @" FROM    TRN_INVOICE_HEADER INNER JOIN MST_PARTY 
                                                        ON TRN_INVOICE_HEADER.PARTY_ID             = MST_PARTY.PARTY_ID
                                                        LEFT JOIN MST_PAYMENT_TYPE 
                                                        ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                                        LEFT JOIN TRN_COLLECTION_DETAIL 
                                                        ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID    = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                                        INNER JOIN 
                                                        (
                                                            SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                                   TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT 
                                                            FROM   TRN_COLLECTION_DETAIL
                                                            WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = "+ CollectionHeaderID +
                                                     @" ) AS COLLECTION
                                                        ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = COLLECTION.INVOICE_HEADER_ID
                                                WHERE   TRN_INVOICE_HEADER.RECON_FLAG              = 0
                                                AND    (TRN_INVOICE_HEADER.TRAN_TYPE               = 'INV'
                                                        OR TRN_INVOICE_HEADER.TRAN_TYPE            = 'OINV')
                                                AND     MST_PARTY.PARTY_CATEGORY_ID                = 0";

                    //--
                    //---------------------------------------------------------------------------
                    //BLOCKED BY DHRUB ON 09/04/2015 
                    //---------------------------------------------------------------------------
                    //if (PaymentTypeID > 0)
                    //    strDetailQuery_UnionAl = strDetailQuery_UnionAl + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + PaymentTypeID + @" ";
                    //------------------------------------------------------------------------------------------------------------------------------------------------
                    //-- Edit Mode : 
                    //-- IF PaymentType is CCAVENUE THEN IT WILL POPULATE ALL CcAvenue Invoices having Due Amount > 0
                    //-- IF PaymentType is Other than CCAVENUE THEN IT WILL POPULATE ALL Invoices having Due Amount > 0 && Other Than CcAvenue Payment Type
                    //-------------------------------------------------------------------------------------------------------------------------------------------------
                    //############################################################################################
                    //BLOCKED BY DHRUBA ON 24/04/2015 
                    //TO POPULATE CC_AVENUE OR OTHER THAN CC_AVENUE IN EDIT MODE
                    //IT WILL POPULATE ONLY TAGGED INVOICE 
                    //############################################################################################
                    //if (PaymentTypeID > 0 && PaymentTypeID == BS_PaymentTypeId.Cc_Avenue)
                    //{
                    //    strDetailQuery_UnionAl = strDetailQuery_UnionAl + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    //}
                    //else if (PaymentTypeID > 0 && PaymentTypeID != BS_PaymentTypeId.Cc_Avenue && chkShowAll.Checked == false)
                    //{
                    //    strDetailQuery_UnionAl = strDetailQuery_UnionAl + " AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> " + BS_PaymentTypeId.Cc_Avenue + @" ";
                    //}
                    //--
                    strDetailGroupBy_UnionAl = @" GROUP BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                                           CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),
                                                           MST_PARTY.PARTY_NAME,
                                                           MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                                           TRN_INVOICE_HEADER.INVOICE_NO,
                                                           TRN_INVOICE_HEADER.REFERENCE_NO,
                                                           TRN_INVOICE_HEADER.NET_AMOUNT,
                                                           COLLECTION.COLLECTION_AMOUNT,
                                                           CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,112)";
                                                           //TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,
                                                           //TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID ";
                    //--
                    if (strTaggedInvoiveId.Trim() != "")
                        strDetailSearchQuery = " AND TRN_INVOICE_HEADER.INVOICE_HEADER_ID NOT IN(" + strTaggedInvoiveId + ") ";

                    //--
                    if (strTaggedInvoiveId.Trim() != "")
                        strDetailSearchQuery_UnionAll = " AND TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID IN(" + strTaggedInvoiveId + ") "; 

                    //if (chkShowAll.Checked == false)
                    ////strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT <> (TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0))  ";
                    ////strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT <> ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) ";
                    //else

                    strDetailHaving = " HAVING  TRN_INVOICE_HEADER.NET_AMOUNT > ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) ";
                    //-----------------------------------------------------------
                    if (chkShowAll.Checked == false)
                        strSQL = strDetailQuery + strDetailSearchQuery + strDetailGroupBy + strDetailHaving;
                    else
                        strSQL = strDetailQuery + strDetailSearchQuery + strDetailGroupBy;

                    if (strTaggedInvoiveId != "")
                        strSQL = strSQL + " UNION ALL " + strDetailQuery_UnionAl + strDetailSearchQuery_UnionAll + strDetailGroupBy_UnionAl;

                    strSQL = strSQL + " ORDER BY " + strDetailOrderBy;
                    //-----------------------------------------------------------
                    blnTagDeductee = false;
                    //--
                    rsDetailGrid = dmlService.J_ExecSqlReturnADODBRecordset(strSQL);
                    if (rsDetailGrid == null) return;
                    //
                    setDetailsGridRefresh(flxgrdDetails);
                    //
                    if (rsDetailGrid.RecordCount > 0)
                    {
                        //-- Clear the Flexgrid data
                        flxgrdDetails.Clear();
                        flxgrdDetails.DataSource = (msdatasrc.DataSource)rsDetailGrid;
                        //-- Formatting Amount as 0.00
                        NumberFormatDetail(flxgrdDetails);
                        setDetailsGridColumns(flxgrdDetails);
                        //rsDetailGrid.Close();
                    }
                    else
                        flxgrdDetails.FixedRows = 1;
                    //--
                    //setDetailsGridColumns(flxgrdDetails);
                    rsDetailGrid.Close();
                    //--
                    blnTagDeductee = true;
                    //
                }
                #endregion
                //
            }
            catch (Exception err)
            {
                rsDetailGrid.Close();
                cmnService.J_UserMessage(err.Message);
            }
        }

        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            
            IDataReader drdShowRecord = null;
            try
            {
                // SQL Query
                strSQL = @"SELECT TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID                   AS COLLECTION_HEADER_ID,
                                   CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103) AS COLLECTION_DATE,
                                   MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                   AS PAYMENT_TYPE_DESCRIPTION,
                                   MST_PAYMENT_TYPE.AUTO_COLLECTION_POST_FLAG                  AS AUTO_COLLECTION_POST_FLAG,
                                   MST_PAYMENT_TYPE.SUNDRY_PARTY_FLAG                          AS SUNDRY_PARTY_FLAG, 
                                   MST_BANK.BANK_NAME                                          AS BANK_NAME,
                                   TRN_COLLECTION_HEADER.REFERENCE_NO                          AS REFERENCE_NO,
                                   TRN_COLLECTION_HEADER.GROSS_AMT                             AS GROSS_AMT,
                                   TRN_COLLECTION_HEADER.LESS_AMT                              AS LESS_AMT,
                                   TRN_COLLECTION_HEADER.NET_AMT                               AS NET_AMT,
                                   TRN_COLLECTION_HEADER.DUE_AMT                               AS DUE_AMT,
                                   TRN_COLLECTION_HEADER.COLLECTION_REMARKS                    AS COLLECTION_REMARKS,
                                   TRN_COLLECTION_HEADER.RECONCILIATION_DATE                   AS RECONCILIATION_DATE,
                                   TRN_COLLECTION_HEADER.TALLIED                               AS TALLIED,
                                   ISNULL(TRN_INVOICE_HEADER.INVOICE_NO,'')                    AS AUTO_INVOICE_NO      
                           FROM   TRN_COLLECTION_HEADER INNER JOIN MST_PAYMENT_TYPE 
                                  ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                  LEFT JOIN MST_BANK 
                                  ON TRN_COLLECTION_HEADER.BANK_ID = MST_BANK.BANK_ID
                                  LEFT JOIN TRN_INVOICE_HEADER
                                  ON TRN_COLLECTION_HEADER.ORIGINAL_INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                       WHERE  TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = " + Id;
                //--
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //--
                if (drdShowRecord == null) return false;
                //--
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    //--
                    mskCollectionDate.Text = drdShowRecord["COLLECTION_DATE"].ToString();
                    blnPaymentType = false;
                    //-----------------------------------------------------
                    BS.T_Selected_PaymentType = drdShowRecord["PAYMENT_TYPE_DESCRIPTION"].ToString();
                    lblSundryPartyFlag.Text = drdShowRecord["SUNDRY_PARTY_FLAG"].ToString();
                    lblAutoCollectionPostFlag.Text = drdShowRecord["AUTO_COLLECTION_POST_FLAG"].ToString();
                    blnPaymentType = true;
                    txtReferenceNo.Text = drdShowRecord["REFERENCE_NO"].ToString();
                    cmbBank.Text = drdShowRecord["BANK_NAME"].ToString();
                    txtGrossAmount.Text = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowRecord["GROSS_AMT"])));
                    txtLessAmount.Text = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowRecord["LESS_AMT"])));
                    txtNetReceived.Text = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowRecord["NET_AMT"])));
                    txtDueAmount.Text = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(drdShowRecord["DUE_AMT"])));
                    txtRemarks.Text = drdShowRecord["COLLECTION_REMARKS"].ToString();
                    mskReconcileDate.Text = drdShowRecord["RECONCILIATION_DATE"].ToString();


                    if (dtService.J_IsDateGreater(ref mskReconcileDate, J_Var.J_pLastReconcileDate, "", "", "", J_ShowMessage.NO) == false)
                    {
                        mskReconcileDate.Enabled = true;
                        blnReconDateIsEnabled = true;
                    }
                    else
                    {
                        mskReconcileDate.Enabled = false;
                        blnReconDateIsEnabled = false;
                    }

                    if (Convert.ToString(drdShowRecord["AUTO_INVOICE_NO"]) == "")
                    {
                        lblCollectionMode.Text = "Manual";
                        lblCollectionMode.Visible = true;

                        lblAutoInvoiceNo.Visible = false;
                    }
                    else 
                    {
                        lblCollectionMode.Text = "Auto";
                        lblCollectionMode.Visible = true;

                        lblAutoInvoiceNo.Text = Convert.ToString(drdShowRecord["AUTO_INVOICE_NO"]);
                        lblAutoInvoiceNo.Visible = true;
                    }


                    //-------------------------------------------------
                    //ADDED BY DHRUB ON 05/08/2015
                    //TALLIED APPLICABLE ONLY FOR UNKNOWN ENTRIES 
                    //-------------------------------------------------
                    if (Convert.ToString(drdShowRecord["TALLIED"]) == "1")
                        chkTallied.Checked = true;
                    else
                        chkTallied.Checked = false;

                    //--
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //--
                    blnPaymentType = false;
                    cmbPaymentType.Text = BS.T_Selected_PaymentType;
                    blnPaymentType = true;
                    LoadDetailGrid(Id, lblMode.Text);
                    //--
                    CalculateTaggedAmount();


                    //-------------------------------------------------
                    //ADDED BY DHRUB ON 05/08/2015
                    //TALLIED APPLICABLE ONLY FOR UNKNOWN ENTRIES 
                    //-------------------------------------------------
                    if (Convert.ToDouble(txtTaggedTotal.Text) == 0)
                        chkTallied.Visible = true;
                    else
                        chkTallied.Visible = false;

                   
                    //-------------------------------------------------
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
                long lngCollectionHeaderId = 0;
                long lngInvoiceHeaderId = 0;
                double dblCollectionAmount = 0;
                string strReconcileDate = "null";
                long lngBankId = 0;
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
                        if (!dtService.J_IsBlankDateCheck(ref mskReconcileDate, J_ShowMessage.NO))
                            strReconcileDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskReconcileDate.Text) + cmnService.J_DateOperator();
                        //
                        lngBankId = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //
                        strSQL = @"INSERT INTO TRN_COLLECTION_HEADER (
                                               COLLECTION_DATE, 
                                               COMPANY_ID, 
                                               FAYEAR_ID, 
                                               PAYMENT_TYPE_ID, 
                                               BANK_ID, 
                                               REFERENCE_NO, 
                                               GROSS_AMT, 
                                               LESS_AMT, 
                                               NET_AMT, 
                                               COLLECTION_REMARKS, 
                                               NET_INVOICE_AMT,
                                               RECONCILIATION_DATE, 
                                               USER_ID, 
                                               CREATE_DATETIME) 
                                   VALUES(
                                               " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskCollectionDate) + cmnService.J_DateOperator() + @", 
                                               " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + @", 
                                               " + J_Var.J_pFAYearId + @", 
                                               " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + @", 
                                               " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex) + @", 
                                              '" + cmnService.J_ReplaceQuote(txtReferenceNo.Text) + @"', 
                                               " + cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) + @", 
                                               " + cmnService.J_ReturnDoubleValue(txtLessAmount.Text) + @", 
                                               " + cmnService.J_ReturnDoubleValue(txtNetReceived.Text) + @",  
                                              '" + cmnService.J_ReplaceQuote(txtRemarks.Text) + @"', 
                                               " + cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) + @",
                                               " + strReconcileDate + @",
                                               " + J_Var.J_pUserId + @",
                                               " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            //Rollback Transaction
                            this.Cursor = Cursors.Default;
                            dmlService.J_Rollback();
                            mskCollectionDate.Select();
                            return;
                        }
                        // get max ledger header id
                        lngCollectionHeaderId = dmlService.J_ReturnMaxValue("TRN_COLLECTION_HEADER", "COLLECTION_HEADER_ID",
                                                                            "    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                                                                            "AND FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                                                                            "AND USER_ID    = " + J_Var.J_pUserId + "");
                        //-- TRN_COLLECTION_DETAIL
                        // Loop as per individual item
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            lngInvoiceHeaderId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.INVOICE_HEADER_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.INVOICE_HEADER_ID));

                            //Added By dhrub on 06/04/2015
                            if (flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.BILL_AMOUNT) != "")
                            {
                                // Amount is stored
                                dblCollectionAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT) == ""
                                                                       ? "0"
                                                                       : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT));
                            }
                            //--
                            if (dblCollectionAmount > 0)
                            {
                                strSQL = "INSERT INTO TRN_COLLECTION_DETAIL (" +
                                         "            COLLECTION_HEADER_ID," +
                                         "            INVOICE_HEADER_ID," +
                                         "            COLLECTION_AMOUNT) " +
                                         "    VALUES( " + lngCollectionHeaderId + "," +
                                         "            " + lngInvoiceHeaderId + "," +
                                         "            " + dblCollectionAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    this.Cursor = Cursors.Default;
                                    //Rollback Transaction
                                    dmlService.J_Rollback();
                                    BtnCancel.Select();
                                    return;
                                }
                                //-------------------------------------------
                                //FETCH THE ITEM WISE EMAIL TEXT
                                //-------------------------------------------
                                //FetchItemWiseEmailDetails(lngItemId);
                            }
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
                        
                        //BtnEdit.BackColor = Color.LightGray;
                        BtnSort.BackColor = Color.LightGray;
                        BtnSearch.BackColor = Color.LightGray;
                        BtnPrint.BackColor = Color.LightGray;

                        //BtnDelete.Enabled = false;
                        //BtnDelete.BackColor = Color.LightGray;
                        //cmbCompany.Enabled = true;
                        //-----------------------------------------------------------
                        ControlVisible(false);
                        //-----------------------------------------------------------
                        lngSearchId = dmlService.J_ReturnMaxValue("TRN_COLLECTION_HEADER", "COLLECTION_HEADER_ID");
                        //-------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
                        //-----------------------------------------------------------                        
                        
                        break;
                    #endregion

                    #region Edit
                    case J_Mode.Edit:
                        //*****  For Modify
                        int intTalliedInvoice = 0;
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;

                        if (cmnService.J_UserMessage("Proceed ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;


                        if (chkTallied.Checked == true)
                            intTalliedInvoice = 1;
                        else
                            intTalliedInvoice = 0;
                        //-----------------------------------------------------------
                        //--Assigning Cancellation Status to a variable
                        //-----------------------------------------------------------
                        //
                        if (!dtService.J_IsBlankDateCheck(ref mskReconcileDate, J_ShowMessage.NO))
                            strReconcileDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskReconcileDate.Text) + cmnService.J_DateOperator();
                        //
                        lngBankId = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                        //
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------                        
                        //int intRequestCD = 0; int intRequestSerialNo = 0; int intRequestInvoiceNo = 0;
                        //string strRequestDate = "null";
                        //string strDespatchDate = "null";
                        //
                        //if (chkRequestCD.Checked == true)
                        //    intRequestCD = 1;
                        //if (chkRequestSerialNo.Checked == true)
                        //    intRequestSerialNo = 1;
                        //if (chkRequestInvoice.Checked == true)
                        //    intRequestInvoiceNo = 1;
                        //
                        //if(intRequestCD == 1 || intRequestSerialNo == 1 || intRequestInvoiceNo == 1)
                        //    if (!dtService.J_IsBlankDateCheck(ref mskCollectionDate, J_ShowMessage.NO))
                        //        strRequestDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskCollectionDate.Text) + cmnService.J_DateOperator();

                        //if (!dtService.J_IsBlankDateCheck(ref mskDespatchDate, J_ShowMessage.NO))
                        //    strDespatchDate = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskDespatchDate.Text) + cmnService.J_DateOperator();
                        
                        //UPDATING COLLECTION HEADER RECORD
                        strSQL = "UPDATE TRN_COLLECTION_HEADER " +
                                 "SET    COLLECTION_DATE     = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskCollectionDate) + cmnService.J_DateOperator() + ", " +
                                 "       PAYMENT_TYPE_ID     = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + ", " +
                                 "       BANK_ID             = " + lngBankId + ", " +
                                 "       REFERENCE_NO        ='" + cmnService.J_ReplaceQuote(txtReferenceNo.Text) + "', " +
                                 "       GROSS_AMT           = " + cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) + ", " +
                                 "       LESS_AMT            = " + cmnService.J_ReturnDoubleValue(txtLessAmount.Text) + ", " +
                                 "       NET_AMT             = " + cmnService.J_ReturnDoubleValue(txtNetReceived.Text) + ", " +
                                 "       COLLECTION_REMARKS  ='" + cmnService.J_ReplaceQuote(txtRemarks.Text) + "', " +
                                 "       NET_INVOICE_AMT     = " + cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) + ", " +
                                 "       RECONCILIATION_DATE = " + strReconcileDate + ", " +
                                 "       TALLIED             = " + intTalliedInvoice +  " " + 
                                 "WHERE  COLLECTION_HEADER_ID   = " + lngSearchId;
                        //----------------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            //chkRequestCD.Select();
                            return;
                        }
                        // delete all records from TRN_INVOICE_DETAIL
                        if (dmlService.J_ExecSql("DELETE FROM TRN_COLLECTION_DETAIL WHERE COLLECTION_HEADER_ID = " + lngSearchId) == false)
                        {
                            mskCollectionDate.Select();
                            return;
                        }
                        //-- TRN_COLLECTION_DETAIL
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            lngInvoiceHeaderId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.INVOICE_HEADER_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.INVOICE_HEADER_ID));
                            // Amount is stored
                            dblCollectionAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT));
                            //--
                            if (dblCollectionAmount > 0)
                            {
                                strSQL = "INSERT INTO TRN_COLLECTION_DETAIL (" +
                                         "            COLLECTION_HEADER_ID," +
                                         "            INVOICE_HEADER_ID," +
                                         "            COLLECTION_AMOUNT) " +
                                         "    VALUES( " + lngSearchId + "," +
                                         "            " + lngInvoiceHeaderId + "," +
                                         "            " + dblCollectionAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    this.Cursor = Cursors.Default;
                                    //Rollback Transaction
                                    dmlService.J_Rollback();
                                    BtnCancel.Select();
                                    return;
                                }
                                //-------------------------------------------
                                //FETCH THE ITEM WISE EMAIL TEXT
                                //-------------------------------------------
                                //FetchItemWiseEmailDetails(lngItemId);
                            }
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.EditModeSave);
                        //-----------------------------------------------------------
//                        if (chkSendEmail.Checked == true)
//                        {
//                            string strFROM_EMAIL = ""; string strDISPLAY_NAME = ""; string strEMAIL_BCC = "";
//                            string strEMAIL_SUBJECT_DESPATCH = ""; string strEMAIL_BODY_DESPATCH = "";
//                            strSQL = @"SELECT MST_EMAIL_CATEGORY.FROM_EMAIL,
//                                              MST_EMAIL_CATEGORY.DISPLAY_NAME,
//                                              MST_EMAIL_CATEGORY.EMAIL_BCC,
//                                              MST_EMAIL_CATEGORY.EMAIL_SUBJECT_DESPATCH,
//                                              MST_EMAIL_CATEGORY.EMAIL_BODY_DESPATCH
//                                       FROM   TRN_INVOICE_HEADER, 
//                                              TRN_INVOICE_DETAIL, 
//                                              MST_EMAIL_CATEGORY, 
//                                              MST_ITEM 
//                                       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID 
//                                       AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID 
//                                       AND    MST_ITEM.EMAIL_TYPE_ID               = MST_EMAIL_CATEGORY.EMAIL_TYPE_ID 
//                                       AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + lngSearchId;
//                            //
//                            IDataReader drdShowRecord = null;
//                            drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
//                            //
//                            if (drdShowRecord == null) return;
//                            //
//                            while (drdShowRecord.Read())
//                            {
//                                strFROM_EMAIL = drdShowRecord["FROM_EMAIL"].ToString();
//                                strDISPLAY_NAME = drdShowRecord["DISPLAY_NAME"].ToString();
//                                strEMAIL_BCC = drdShowRecord["EMAIL_BCC"].ToString();
//                                strEMAIL_SUBJECT_DESPATCH = drdShowRecord["EMAIL_SUBJECT_DESPATCH"].ToString();
//                                strEMAIL_BODY_DESPATCH = drdShowRecord["EMAIL_BODY_DESPATCH"].ToString();
//                            } 
//                            drdShowRecord.Close();
//                            drdShowRecord.Dispose();
//                            //
//                        }
                        //-----------------------------------------------------------
                        strSQL = strQuery + " ORDER BY " + strOrderBy;
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
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "COLLECTION_HEADER_ID", lngSearchId);
                        break;
                    #endregion

                    case J_Mode.Delete:

                        //If Collection is greater than zero then cannot delete
                        if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(" SELECT GROSS_AMT FROM TRN_COLLECTION_HEADER WHERE COLLECTION_HEADER_ID = " + ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) > 0)
                        {
                            cmnService.J_UserMessage("You cannot delete this collection.");
                            return;
                        }

                        //IF THE COLLECTION TREATED AS UNKNOWN THEN ONLY CAN DELETE 
                        if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(" SELECT COUNT(COLLECTION_DETAIL_ID) FROM TRN_COLLECTION_DETAIL WHERE COLLECTION_HEADER_ID = " + ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) > 0)
                        {
                            cmnService.J_UserMessage("You cannot delete This Collection.\nThis collection already been tagged.");
                            return;
                        }
                        //-----------------------------------------------------------
                        if (cmnService.J_UserMessage("Are you sure.You want to delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                        //-----------------------------------------------------------
                        this.Cursor = Cursors.WaitCursor;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //--------------------------------------------------
                        //-----------------------------------------------------------
                        //DELETE FROM TRN_COLLECTION_HEADER
                        //-----------------------------------------------------------
                        strSQL = " DELETE FROM TRN_COLLECTION_HEADER " +
                                 " WHERE  COLLECTION_HEADER_ID  = " + lngSearchId;
                        //-----------------------------------------------------------
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //-----------------------------------------------------------
                        //Commit the transaction 
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        //-----------------------------------------------------------
                        cmnService.J_UserMessage("Data successfully deleted");

                        //Populate the View Grid 
                        //-------------------------------------
                        LoadGrid(cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex));
                        //-----------------------------------------------------------
                        this.Cursor = Cursors.Default;
                        //-----------------------------------------------------------
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
                    //
                    if (!dtService.J_IsBlankDateCheck(ref mskCollectionDate, J_ShowMessage.NO))
                    {
                        if (dtService.J_IsDateValid(mskCollectionDate.Text) == false)
                        {
                            cmnService.J_UserMessage("Enter the valid Collection date.");
                            mskCollectionDate.Select();
                            return false;
                        }
                        //-- 
                        if (dtService.J_IsDateGreater(ref mskCollectionDate, J_Var.J_pFAEndDate, "", "", "", J_ShowMessage.NO) == false)
                        {
                            cmnService.J_UserMessage("Collection date should be before the selected FA Year end date\n i.e. : " + J_Var.J_pFAEndDate);
                            mskCollectionDate.Select();
                            return false;
                        }
                    }
                    else
                    {
                        cmnService.J_UserMessage("Enter the Collection date.");
                        mskCollectionDate.Select();
                        return false;
                    }
                    //--
                    // Company
                    if (cmbCompany.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please select the company.");
                        cmbCompany.Select();
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
                            if (cmbPaymentType.Text == BS_PaymentType.Cc_Avenue  && Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT CC_AVENUE_FLAG FROM MST_BANK WHERE BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex))) == 0)
                            {
                                cmnService.J_UserMessage("Improper bank selected.");
                                cmbBank.Select();
                                return false;
                            }
                        }
                        
                        //
                        if (intReferenceNoFlag == 1 && txtReferenceNo.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Reference No : Mandatory for this Payment Type");
                            txtReferenceNo.Select();
                            return false;
                        }
                    }

                    //--
                    if (cmbPaymentType.Text.ToUpper() == "CC AVENUE")
                    {
                        if (cmbBank.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("For Payment Type " + cmbPaymentType.Text + ", Bank should be selected");
                            cmbBank.Select();
                            return false;
                        }
                    }
                    
                    //--------------------------------------------------------------------------------------------------------------------
                    // Collection Date within FA Year Date or not
                    //if (dtService.J_IsDateGreater(J_Var.J_pFABegDate, ref mskCollectionDate, "", "", "", J_ShowMessage.NO) == false
                    //    || dtService.J_IsDateGreater(ref mskCollectionDate, J_Var.J_pFAEndDate, "", "", "", J_ShowMessage.NO) == false)
                    //{
                    //    cmnService.J_UserMessage("Invoice date is outside FA Year date." +
                    //        "\n\nBegining Date : " + J_Var.J_pFABegDate +
                    //        "\nEnding Date    : " + J_Var.J_pFAEndDate);
                    //        mskCollectionDate.Select();
                    //        return false;
                    //}
                    //-- RECONCILE DATE
                    //if (cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) > 0)
                    //{
                    if (!dtService.J_IsBlankDateCheck(ref mskReconcileDate, J_ShowMessage.NO))
                    {
                        if (dtService.J_IsDateValid(mskReconcileDate.Text) == false)
                        {
                            cmnService.J_UserMessage("Enter the valid Reconcile date.");
                            mskReconcileDate.Select();
                            return false;
                        }

                        if (dtService.J_IsDateGreater(ref mskReconcileDate, J_Var.J_pLastReconcileDate, "", "", "", J_ShowMessage.NO) == true && blnReconDateIsEnabled == true)
                        {
                            cmnService.J_UserMessage("Reconcile date should after Last Reconcile date " + J_Var.J_pLastReconcileDate + ".");
                            mskReconcileDate.Select();
                            return false;
                        }
                    }
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
                    if (lblMode.Text == J_Mode.Add)
                    {
                        if (cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) == 0)
                        {
                            cmnService.J_UserMessage("Gross Amount should not be 0");
                            txtGrossAmount.Select();
                            return false;
                        }
                    }
                    //
                    //if (cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) == 0)
                    //{
                    //    cmnService.J_UserMessage("Total Tagged Amount should not be 0");
                    //    txtTaggedTotal.Select();
                    //    return false;
                    //}
                    //

                    //-------------------------------------------------
                    //ADDED BY DHRUB ON 05/08/2015
                    //TALLIED APPLICABLE ONLY FOR UNKNOWN ENTRIES 
                    //-------------------------------------------------
                    if (lblMode.Text == J_Mode.Edit)
                    {
                        if (!dtService.J_IsBlankDateCheck(ref mskReconcileDate, J_ShowMessage.NO))
                        {
                            if (Convert.ToDouble(txtTaggedTotal.Text) > 0 && chkTallied.Checked == true)
                            {
                                cmnService.J_UserMessage("Only unknown entries can be Tallied.");
                                chkTallied.Select();
                                return false;
                            }
                        }
                        else
                        {
                            if (chkTallied.Checked == true)
                            {
                                cmnService.J_UserMessage("Unknown entries cannot be Tallied while Reconciliation Date is Blank.");
                                mskReconcileDate.Select();
                                return false;
                            }
                        }
                    }
                    //-------------------------------------------------

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //Added By dhrub On 08/04/2015 
                    //------------------------------------------------------------------
                    //GROSS AMOUNT [ Error Message : If Gross Amount is equals to 0 and Tagged Amount is greater than 0.]
                    //------------------------------------------------------------------
                    if (cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) == 0 && cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) > 0)
                    {
                        cmnService.J_UserMessage("Error : Gross amount cannot be 0 in this case.",MessageBoxIcon.Error);
                        txtGrossAmount.Select();
                        return false;
                    }
                    //------------------------------------------------------------------
                    //TAGGED AMOUNT [ Warning Message : If Tagged Amount is equals to 0 ]
                    //------------------------------------------------------------------
                    if (cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text) == 0)
                    {
                        if (cmnService.J_UserMessage("Warning : No Invoice Amount Tagged. \nEntry Will be Treated as ''UNKNOWN''\nDo you want to Proceed....", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return false;
                    }
                    //------------------------------------------------------------------
                    //DUE AMOUNT [ Warning Message : If Due Amount is Greater than 0 ]
                    //------------------------------------------------------------------
                    if (cmnService.J_ReturnDoubleValue(txtDueAmount.Text) > 0)
                    {
                        if (cmnService.J_UserMessage("Warning : Lesser Invoice Amount Tagged. \nDo you want to Proceed....", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return false;
                    }
                    //------------------------------------------------------------------
                    //DUE AMOUNT [ Warning Message : If Due Amount is Less than 0 ]
                    //------------------------------------------------------------------
                    if (cmnService.J_ReturnDoubleValue(txtDueAmount.Text) < 0)
                    {
                        if (cmnService.J_UserMessage("Warning : Excess Invoice Amount Tagged. \nDo you want to Proceed....", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return false;
                    }

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    double dblCollectionAmt = 0; int Count = 0; int MessageLog = 0; int iRIndex = 0;
                    if (lblAutoCollectionPostFlag.Text == "1")
                    {
                        for (iRIndex = 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
                        {
                            //Added By dhrub on 06/04/2015
                            if (flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceCollection.BILL_AMOUNT) != "" )
                            {
                                dblCollectionAmt = Convert.ToDouble(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT) == ""
                                                    ? "0"
                                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceCollection.COLLECTION_AMOUNT));
                                if (dblCollectionAmt > 0)
                                    Count = Count + 1;
                                //
                                if (Count > 1) //-- ONLY 1 RECORDS FOR AUTO COLLECTION POST FLAG = 1
                                {
                                    MessageLog = 1;
                                    goto MessageDescription;
                                }
                            }

                        }

                    //
                    MessageDescription:
                        //--
                        //Blocked By Dhrub on 06/04/2015 --- [FOR  UNKNOWN PAYMENTS WHICH DOESEN'T HAVE ANY DETAILS ENTRY]
                        //--
                        //if (MessageLog == 0)
                        //{
                        //    cmnService.J_UserMessage("No record in grid.");
                        //    flxgrdDetails.Row = 1;
                        //    flxgrdDetails.Col = (int)enmInvoiceCollection.INVOICE_NO;
                        //    flxgrdDetails.Select();
                        //    return false;
                        //}
                        if (MessageLog == 1)
                        {
                            cmnService.J_UserMessage("Only 1 Invoice can be tagged to Payment Type : " + cmbPaymentType.Text);
                            flxgrdDetails.Row = iRIndex;
                            flxgrdDetails.Col = (int)enmInvoiceCollection.COLLECTION_AMOUNT;
                            flxgrdDetails.Select();
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

        #region setDetailsGridRefresh
        private void setDetailsGridRefresh(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, lngDetailGridColumns);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < lngDetailGridColumns; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;
        }
        #endregion

        #region setDetailsGridColumns
        private void setDetailsGridColumns(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            //
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // INVOICE_HEADER_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.INVOICE_HEADER_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.INVOICE_HEADER_ID, 0, 0);

            // Invoice Date
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.INVOICE_DATE;
            HFlexGrid.Text = "Invoice Date";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.INVOICE_DATE, 0, 1500);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.INVOICE_DATE, (short)J_Alignment.CenterCentre);

            // Invoice No
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.INVOICE_NO;
            HFlexGrid.Text = "Invoice No";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.INVOICE_NO, 0, 2000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.INVOICE_NO, (short)J_Alignment.CenterCentre);

            // Party Name
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.PARTY_NAME;
            HFlexGrid.Text = "Party Name";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.PARTY_NAME, 0, 3000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.PARTY_NAME, (short)J_Alignment.LeftCentre);

            // Type
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.TYPE;
            HFlexGrid.Text = "Type";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.TYPE, 0, 1500);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.TYPE, (short)J_Alignment.LeftCentre);

            // Reference
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.REFERENCE_NO;
            HFlexGrid.Text = "Reference No.";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.REFERENCE_NO, 0, 1850);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.REFERENCE_NO, (short)J_Alignment.RightCentre);

            // Bill Amount
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.BILL_AMOUNT;
            HFlexGrid.Text = "Bill Amount";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.BILL_AMOUNT, 0, 1200);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.BILL_AMOUNT, (short)J_Alignment.RightCentre);

            // Balance Amount
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN;
            HFlexGrid.Text = "BALANCE_AMOUNT_HIDDEN";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.BALANCE_AMOUNT_HIDDEN, 0, 0);
            //HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.BALANCE_AMOUNT, (short)J_Alignment.RightCentre);

            // Balance Amount
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.BALANCE_AMOUNT;
            HFlexGrid.Text = "Balance";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.BALANCE_AMOUNT, 0, 1200);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.BALANCE_AMOUNT, (short)J_Alignment.RightCentre);

            // Tagged Amount
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.COLLECTION_AMOUNT;
            HFlexGrid.Text = "Collection";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.COLLECTION_AMOUNT, 0, 1250);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.COLLECTION_AMOUNT, (short)J_Alignment.RightCentre);
            //--
            // Invoice Date YYYYMMDD
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceCollection.INVOICE_DATE_YYYYMMDD;
            HFlexGrid.Text = "Invoice Date YYYYMMDD";
            HFlexGrid.set_ColWidth((int)enmInvoiceCollection.INVOICE_DATE_YYYYMMDD, 0, 0);
            HFlexGrid.set_ColAlignment((int)enmInvoiceCollection.INVOICE_DATE_YYYYMMDD, (short)J_Alignment.CenterCentre);
            //--
            //if (lblSearchMode.Text == T_Mode.Confirm)
            //    HFlexGrid.BackColor = Color.PeachPuff;
            //else if (lblSearchMode.Text == T_Mode.Printing)
            //    HFlexGrid.BackColor = Color.Azure;
            //--
        }
        #endregion

        #region NumberFormatDetail
        private void NumberFormatDetail(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            for (int intCounter = 1; intCounter <= HFlexGrid.Rows - 1; intCounter++)
            {
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceCollection.BILL_AMOUNT,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.BILL_AMOUNT) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.BILL_AMOUNT))));
                //--
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceCollection.BALANCE_AMOUNT,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.BALANCE_AMOUNT) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.BALANCE_AMOUNT))));
                //--
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceCollection.COLLECTION_AMOUNT,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.COLLECTION_AMOUNT) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceCollection.COLLECTION_AMOUNT))));
            }
        }
        #endregion

        #region setTextBoxInGrid
        private void setTextBoxInGrid()
        {
            intTempGridPosition = flxgrdDetails.RowSel;

            if (flxgrdDetails.Rows > 1 && (//flxgrdDetails.Col == (int)enmInvoiceCollection.QUANTITY
                   flxgrdDetails.Col == (int)enmInvoiceCollection.COLLECTION_AMOUNT))
            {
                
                //-- For Text Box in Grid
                txtQty.Visible = true;

                txtQty.Left = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellLeft) + flxgrdDetails.Left - 1);
                txtQty.Top = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellTop) + flxgrdDetails.Top - 1);
                txtQty.Width = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellWidth));
                txtQty.Height = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellHeight) - 5);

                blnIsAmount = false;

                //dblCollectionAmt = cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceCollection.COLLECTION_AMOUNT)); //cmnService.J_ReturnDoubleValue(txtQty.Text);
                if (cmnService.J_ReturnDoubleValue(flxgrdDetails.get_TextMatrix(intTempGridPosition, flxgrdDetails.ColSel)) == 0)
                    txtQty.Text = "";

                txtQty.Text = flxgrdDetails.get_TextMatrix(intTempGridPosition, flxgrdDetails.ColSel);

                blnIsAmount = true;

                txtQty.TextAlign = HorizontalAlignment.Right;

                txtQty.Select();
                txtQty.Focus();
            }
        }
        #endregion

        #region CalculateNetAmount -- COMMENTED
        //private void CalculateNetAmount()
        //{
        //    int intQuantity = 0;
        //    double dblRate = 0;
        //    double dblAmount = 0;

        //    double dblTotalAmount = 0;

        //    double dblDiscountRate = cmnService.J_ReturnDoubleValue(txtDiscountRate.Text);
        //    double dblDiscountAmount = cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text);
        //    double dblAmountWithDiscount = 0;

        //    double dblTaxRate = 0;
        //    double dblTaxAmount = 0;
        //    double dblTotalTaxAmount = 0;
        //    double dblAmountWithTax = 0;

        //    double dblAdditionalCost = cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text);
        //    double dblAmountWithAdditional = 0;

        //    double dblRoundedOff = cmnService.J_ReturnDoubleValue(txtRoundedOff.Text);
        //    double dblNetAmount = 0;

        //    if (flxgrdDetails.Rows > 0)
        //    {
        //        for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
        //        {
        //            intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY) == ""
        //                ? "0"
        //                : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY));

        //            dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE) == ""
        //                ? "0"
        //                : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE));

        //            dblAmount = Convert.ToDouble(cmnService.J_FormatToString((intQuantity * dblRate), "0.00"));
        //            flxgrdDetails.set_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT, string.Format("{0:0.00}", dblAmount));

        //            dblTotalAmount = dblTotalAmount + dblAmount;
        //        }
        //    }

        //    // Discount Enable
        //    DiscountEnable();

        //    // calculate discount amount
        //    if (txtDiscountRate.ReadOnly == false)
        //    {
        //        if (dblDiscountRate == 0) dblDiscountAmount = 0;
        //        else if (dblDiscountRate > 0) dblDiscountAmount = Convert.ToDouble(cmnService.J_FormatToString((dblTotalAmount * dblDiscountRate / 100), "0.00"));
        //    }
        //    dblAmountWithDiscount = dblTotalAmount - dblDiscountAmount;

        //    // calculate tax
        //    if (flxgrdTax.Rows > 0)
        //    {
        //        for (int intCounter = 1; intCounter <= flxgrdTax.Rows - 1; intCounter++)
        //        {
        //            if (flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC).Trim() != "")
        //            {
        //                dblTaxRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE));
        //                dblTaxAmount = Convert.ToDouble(cmnService.J_FormatToString((dblAmountWithDiscount * dblTaxRate / 100), "0.00"));

        //                flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, string.Format("{0:0.00}", dblTaxAmount));

        //                dblTotalTaxAmount = dblTotalTaxAmount + dblTaxAmount;
        //            }
        //            else
        //            {
        //                flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_ID, "0");
        //                flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC, "");
        //                flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE, "");
        //                flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, "");
        //            }
        //        }
        //    }
        //    dblAmountWithTax = dblAmountWithDiscount + dblTotalTaxAmount;

        //    // calculate additional amount
        //    dblAmountWithAdditional = dblAmountWithTax + dblAdditionalCost;

        //    // calculate net amount
        //    dblNetAmount = dblAmountWithAdditional + dblRoundedOff;

        //    // Total Amount
        //    txtTotalAmount.Text = cmnService.J_FormatToString(dblTotalAmount, "0.00");

        //    // Discount
        //    if (txtDiscountRate.ReadOnly == false)
        //        txtDiscountAmount.Text = cmnService.J_FormatToString(dblDiscountAmount, "0.00");

        //    txtAmountWithDiscount.Text = cmnService.J_FormatToString(dblAmountWithDiscount, "0.00");

        //    // Tax
        //    txtAmountWithTax.Text = cmnService.J_FormatToString(dblAmountWithTax, "0.00");
        //    txtTaxAmount.Text = cmnService.J_FormatToString(dblTotalTaxAmount, "0.00");

        //    // Additional
        //    txtAmountWithAdditionalCost.Text = cmnService.J_FormatToString(dblAmountWithAdditional, "0.00");

        //    // Net Amount
        //    txtNetAmount.Text = cmnService.J_FormatToString(dblNetAmount, "0.00");
        //    txtNetAmountInwords.Text = cmnService.J_Inwords(dblNetAmount);

        //}
        #endregion 

        #region CalculateNetAmount
        private void CalculateNetAmount(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(txtLessAmount.Text) > cmnService.J_ReturnDoubleValue(txtGrossAmount.Text))
            {
                txtLessAmount.Text = "0.00";
                return;
            }
            txtNetReceived.Text = cmnService.J_FormatToString(cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) - cmnService.J_ReturnDoubleValue(txtLessAmount.Text), "0.00");
            txtDueAmount.Text = cmnService.J_FormatToString(cmnService.J_ReturnDoubleValue(txtGrossAmount.Text) - cmnService.J_ReturnDoubleValue(txtTaggedTotal.Text), "0.00");
        }
        #endregion

        #region CalculateTaggedAmount
        private void CalculateTaggedAmount()
        {
            try
            {
                double dblCollectionAmt = 0;
                if (flxgrdDetails.Rows > 0)
                {
                    for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
                    {
                        dblCollectionAmt = dblCollectionAmt + Convert.ToDouble((flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceCollection.COLLECTION_AMOUNT).Trim() == ""
                            ? "0"
                            : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceCollection.COLLECTION_AMOUNT)));
                    }
                }
                txtTaggedTotal.Text = cmnService.J_FormatToString(dblCollectionAmt, "0.00");
            }
            catch (Exception err)
            {
            }
        }
        #endregion




        #endregion

    }
}

