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
    public partial class TrnSerialNoStatus : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnSerialNoStatus()
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
        bool blnExit = false;

        long lngSearchId = 0;

        string[,] strMatrix;
        //
        string SearchAllText = "Press Alt + F";
        //
        #endregion

        #region User Defined Events

        #region BankEntry_Load
        private void BankEntry_Load(object sender, EventArgs e)
        {
            //Added By dhrub on 03/05/2014 for Set the Grid Size
            //ViewGrid.Size = new Size(1000, 510);
            //
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //
            BtnAdd.Enabled = false;
            BtnEdit.Enabled = false;
            BtnDelete.Enabled = false;
            BtnRefresh.Enabled = false;
            BtnSearch.Enabled = false;
            BtnSort.Enabled = false;
            BtnSave.Enabled = false;
            BtnCancel.Enabled = false;
            BtnPrint.Enabled = false;
            BtnAdd.BackColor = Color.LightGray;
            BtnEdit.BackColor = Color.LightGray;
            BtnSave.BackColor = Color.LightGray;
            BtnCancel.BackColor = Color.LightGray;
            BtnSort.BackColor = Color.LightGray;
            BtnSearch.BackColor = Color.LightGray;
            BtnDelete.BackColor = Color.LightGray;
            BtnRefresh.BackColor = Color.LightGray;
            BtnPrint.BackColor = Color.LightGray;
            
            lblTitle.Text = this.Text;

            string strSQL1 = "SELECT ITEM_ID," +
                     "       ITEM_NAME " +
                     "FROM   MST_ITEM " +
                     "WHERE  ONLINE_FLAG = 1 " +
                     "ORDER BY DEFAULT_ITEM_ONLINE_OFFLINE_BILLING DESC ";
            blnExit = false;
            if (dmlService.J_PopulateComboBox(strSQL1, ref cmbItemName) == false) return;
            blnExit = true;

            cmbItemName.Enabled = true;
            BtnSort.BackColor = Color.LightGray;
            BtnSort.Enabled = false;
            BtnPrint.BackColor = Color.LightGray;
            BtnPrint.Enabled = false;

            chkCancelledEntry.Checked = false;

            cmbItemName.Select();
        }
        #endregion

        #region BtnAdd_Click
        public void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbItemName.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select Comapany");
                    cmbItemName.Select();
                    return;
                }
                
                cmbItemName.Enabled = false;                
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
                    //if (ShowRecord(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) == false)
                    //{
                    //    ControlVisible(false);
                    //    if (dsetGridClone == null) return;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);

                    //    cmbItemName.Enabled = true;
                    //}

                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;

                    cmbItemName.Enabled = false;

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

                cmbItemName.Enabled = true;
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
            //Insert_Update_Delete_Data();
        }
        #endregion

        #region cmbItemName_SelectedIndexChanged
        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnExit == false)
                return;

            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode
            //txtSearchAll.Text = "";
            string[,] strMatrix1 =  {{"Offline Code", "80", "", "Left", "", "", ""},
                                     {"Status", "50", "", "Left", "", "", ""},
                                     {"Invoice No.", "150", "", "Left", "", "", ""},
							         {"Invoice Date", "80", "", "Left", "", "", ""},
                                     {"Party Name", "150", "", "Left", "", "", ""},
                                     {"Contact Person", "150", "", "Left", "", "", ""},
                                     {"Email", "160", "", "Left", "", "", ""},
                                     {"Mobile", "110", "", "Right", "", "", ""}};
            //
            //string[,] strCaseEndMatrix = {{"=0", "N", "", "T"},
            //                              {"=1", "N", "Cancelled", "T"}};
            //
            strMatrix = strMatrix1;
            //
            strOrderBy = "MST_OFFLINE_SERIAL.OFFLINE_CODE";
            //
            strQuery = @"SELECT MST_OFFLINE_SERIAL.OFFLINE_CODE,
                               CASE WHEN MST_OFFLINE_SERIAL.INACTIVE_FLAG = 1 
                                    THEN 'Cancelled'
                                    ELSE ''
                                END AS STATUS,   
                               ISNULL(TRN_INVOICE_HEADER.INVOICE_NO,'') AS INVOICE_NO,
                               ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),'') AS INVOICE_DATE,
                               ISNULL(MST_PARTY.PARTY_NAME,'') AS PARTY_NAME,
                               ISNULL(MST_PARTY.CONTACT_PERSON,'') AS CONTACT_PERSON,
                               ISNULL(MST_PARTY.EMAIL_ID,'') AS EMAIL_ID,
                               ISNULL(MST_PARTY.MOBILE_NO,'') AS MOBILE_NO
                        FROM ((MST_OFFLINE_SERIAL
                        LEFT JOIN TRN_INVOICE_HEADER 
                        ON     MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID)
                        LEFT JOIN MST_PARTY 
                        ON     TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID)
                        WHERE  MST_OFFLINE_SERIAL.ITEM_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbItemName, cmbItemName.SelectedIndex);

            
            //
            strSQL = strQuery + " ORDER BY " + strOrderBy;
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //
        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            //if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
            //{
            //    BtnAdd.Focus();
            //    return;
            //}
            //lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }
        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            //BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                //lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
                //if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);

                //if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

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
            //lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
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

                // clear controls
                ClearControls();

                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                cmbItemName.Enabled = true;
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
                string strSearch = "";
                //--
                if (txtSearchAll.Text.Length > 0)
                    txtSearchAll.BackColor = Color.White;
                else
                    txtSearchAll.BackColor = Color.AliceBlue;
                //--
                strSearch = "AND (MST_OFFLINE_SERIAL.OFFLINE_CODE LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.INVOICE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR TRN_INVOICE_HEADER.INVOICE_DATE LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.PARTY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.CONTACT_PERSON LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.EMAIL_ID LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%' " +
                            "     OR MST_PARTY.MOBILE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtSearchAll.Text.Trim().ToUpper()) + "%') ";
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


        #endregion

    }
}

