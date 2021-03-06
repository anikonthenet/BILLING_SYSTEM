
#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Dhrubo Mukherjee
Module Name		: TrnOnlineOffflineInvoiceEntry
Version			: 2.0
Start Date		: 18-02-2015
End Date		: 
Tables Used     : 
Module Desc		: Invoice Entry FOR Online/Offline mode
____________________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes


// System Namespaces
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

using JAYA.VB;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;


// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.FormCmn;
using BillingSystem.FormRpt;
using BillingSystem.Classes;
using BillingSystem.FormTrn.PopUp;
using BillingSystem.Reports.Listing;
using BillingSystem.Reports.Transaction.INVOICE;
#endregion

namespace BillingSystem.FormTrn.NormalEntries
{
    public partial class TrnOnlineOffflineInvoiceEntry : BillingSystem.FormGen.GenForm
    {

        #region System Generated Code
        public TrnOnlineOffflineInvoiceEntry()
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
        DataSet dsDataPrint = new DataSet();
        JVBCommon mainVB = new JVBCommon();
        ReportService rptService = null;
        RptDialog rptdialog;
        BS BillingSystem = new BS(); 

        string strSQL;			//For Storing the Local SQL Query
        
        int intTempGridPosition = 0;
        bool blnAmount;
        int intMaxColumnEntries = 1;
        long SearchId = 0;
        string strSelectedPartyCategoryInEditMode = "";


        string[] arrSendPopupDetails = new string[15];
        //-----------------------------------
        string strPartyName = "";
        string strPartyAddress1 = "";
        string strPartyAddress2 = "";
        string strPartyAddress3 = "";
        string strPartyCity = "";
        string strPartyPin = "";
        string strPartyContactPerson = "";
        string strPartyPhoneNumber = "";
        string strPartyFax = "";
        string strPartyMobileNumber = "";
        string strPartyEmailId = "";
        //-----------------------------------
        string strSMSText = "";           // FOR INITIALIZE THE EMAIL BODY AFTER RETRIEVAL FROM DATABASE
        string strWorkingKey = "";        // FOR INITIALIZE THE EMAIL BODY AFTER RETRIEVAL FROM DATABASE
        //-----------------------------------
        string strEmailBody = "";         // FOR INITIALIZE THE EMAIL BODY AFTER RETRIEVAL FROM DATABASE
        string strProductName = "";
        string strPDFFileName = "";
        string strDownloadLink1 = "";
        string strDownloadLink2 = "";
        string strEmailSubject = "";
        string strFromEmail = "";
        string strBCCEmail = "";
        
        //
        string strSelectedFileName = "";  // To initialize the pdf file path
        string strGenerateInvoiceFolderName = "GenerateInvoice";
        string strFileNotFoundImagePath = Path.Combine(Application.StartupPath + "\\GenerateInvoice", "not-found.jpg");
        string strEmail = "";
        string strWebsite = "";
        string strDisplayName = "";
        string strImageUrl = "";
        string strHTMLText = "";
        string strPdfPath = "";
        //
        string strDetails = "";
        string strOfflineDetails = "";

        bool blnPaymentType = true;

        // ADDED BY DHRUB ON 09/04/2015
        int intBankInInvoiceFlag = 0;

        //Added By dhrub on 21/05/2015
        bool blnInvoiceWiseReconciled = false; 
        #endregion

        #region ENUM decleration of Detail Grid Column
        //-- enum for setting detail grid column
        enum enmInvoiceEntry
        {

            INVOICE_DETAIL_ID = 1,
            SL_NO             = 2,
            ITEM_NAME         = 3,
            QUANTITY          = 4,
            UNIT              = 5,
            RATE              = 6,
            AMOUNT            = 7,
            REMARKS           = 8,
            ITEM_ID           = 9
        }

        enum enmInvoiceTax
        {
            TAX_ID     = 1,
            TAX_DESC   = 2,
            TAX_RATE   = 3,
            TAX_AMOUNT = 4,
        }

        #endregion

        #region User Defined Events

        #region TrnInvoiceEntry_Load
        private void TrnInvoiceEntry_Load(object sender, System.EventArgs e)
        {
            BtnAdd_Click(this, e);
            //
            if (lblMode.Text == J_Mode.Add)
            {
                strSQL = "SELECT COMPANY_ID," +
                         "       COMPANY_NAME " +
                         "FROM   MST_COMPANY " +
                         "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                         "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                         "ORDER BY COMPANY_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;
                
            }
            mskInvoiceDate.Text = DateTime.Now.ToString();
            cmbBillingMode.Select();
            lblTitle.Text = this.Text;
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            // Mode
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            // Button behavior
            BtnCancel.Enabled = false;
            BtnCancel.BackColor = Color.LightGray;
            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            // Control visibity
            ControlVisible(true);

            // Clear all the Controls
            ClearControls();
            
            cmbCompany.Select();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Mode
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            // Button behavior
            BtnCancel.Enabled = false;
            BtnCancel.BackColor = Color.LightGray;
            BtnDelete.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            // Clear all the Controls
            ClearControls();

            GetInvoiceNo();

            Enable(lblMode.Text);
            cmbCompany.Select();

        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
            GetInvoiceNo();
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Add)
            {
                cmnService.J_UserMessage("Please select the invoice to delete.");
                BtnEditInvoice.Select();

                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;

                return;
            }

            // Mode
            lblMode.Text = J_Mode.Delete;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            Insert_Update_Delete_Data();
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

        #region cmbBillingMode_SelectedIndexChanged
        private void cmbBillingMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (blnPaymentType == true)
                {
                    txtReference.Text = "";
                    //cmbPaymentType.SelectedIndex = 0;
                    txtOnlineOrderNo.Text = "";
                }
                //
                #region CLEAR FETCH DATA
                BS.P_Billing_Salutation = "";
                BS.P_Billing_FName = "";
                BS.P_Billing_LName = "";
                BS.P_Billing_Company = "";
                BS.P_Billing_Address = "";
                BS.P_Billing_City = "";
                BS.P_Billing_State = "";
                BS.P_Billing_Pin = "";
                BS.P_Billing_Email = "";
                BS.P_Billing_Mobile = "";
                BS.P_Billing_Telephone = "";
                BS.P_CD_SerialID = "";
                BS.P_OrderNo = "";
                #endregion
                //
                grpOnlinePaymentDetails.Height = 61;
                //cmbBank.SelectedIndex = 0;
                //
                if (cmbBillingMode.SelectedIndex <= 0)
                {
                    grpOnlinePaymentDetails.Visible = false;
                    grpOfflinePaymentDetails.Visible = false;
                    return;
                }
                if (cmbBillingMode.Text == BS_BillMode.OnlineDelivery)
                {
                    grpOnlinePaymentDetails.Location = new Point(628, 39);
                    grpOnlinePaymentDetails.Visible = true;
                    grpOfflinePaymentDetails.Visible = false;

                    // Added By dhrub On 09/04/2015 
                    //-----------------------------------------------------
                    //Populating Bank Combo When Payment Type is CC_Avenue
                    //-----------------------------------------------------
                    strSQL = "SELECT BANK_ID," +
                             "       BANK_NAME " +
                             "FROM   MST_BANK " +
                             "WHERE  CC_AVENUE_FLAG = 1 " +
                             "ORDER BY BANK_NAME ";
                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbOnlineBank, 0) == false) return;
                }
                else if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                {
                    grpOnlinePaymentDetails.Visible = false;
                    grpOfflinePaymentDetails.Visible = true;

                    //ADDED BY DHRUB ON 09/04/2015
                    //----------------------------------------------------------
                    //Populating Payment Type Combo When Bill Mode is Offline 
                    //----------------------------------------------------------
                    strSQL = " SELECT PAYMENT_TYPE_ID," +
                             "        PAYMENT_TYPE_DESCRIPTION " +
                             " FROM   MST_PAYMENT_TYPE " +
                             " WHERE  INACTIVE_FLAG = 0 " +
                             " AND    HIDE_IN_ONLINE_OFFLINE_INVOICE_FLAG = 0 " +
                             " ORDER BY PAYMENT_TYPE_DESCRIPTION ";

                    if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType, 0) == false) return;
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }


        }
        #endregion


        #region cmbCompany_KeyUp
        private void cmbCompany_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCompany, e);
        }
        #endregion

        #region cmbCompany_Leave
        private void cmbCompany_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCompany);
            object objRowView = cmbCompany.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound + " : " + cmbCompany.Text);
                cmbCompany.Focus();
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0) cmnService.J_ClearComboBox(ref cmbInvoiceSeries);

            strSQL = "SELECT INVOICE_SERIES_ID," +
                     "       PREFIX " +
                     "FROM   MST_INVOICE_SERIES " +
                     "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                     "AND    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "AND    FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                     "ORDER BY PREFIX DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbInvoiceSeries, 1) == false) return;
        }
        #endregion 
        
        #region cmbCompany_KeyPress
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (cmbInvoiceSeries.Enabled == true) cmbInvoiceSeries.Select();
                else SendKeys.Send("{tab}");
            }
        }
        #endregion


        #region cmbInvoiceSeries_KeyUp
        private void cmbInvoiceSeries_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbInvoiceSeries, e);
        }
        #endregion

        #region cmbInvoiceSeries_Leave
        private void cmbInvoiceSeries_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbInvoiceSeries);
            object objRowView = cmbInvoiceSeries.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound + " : " + cmbInvoiceSeries.Text);
                cmbInvoiceSeries.Focus();
            }
        }
        #endregion

        #region cmbInvoiceSeries_SelectedIndexChanged
        private void cmbInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvoiceNo();
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
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbParty);
            object objRowView = cmbParty.SelectedItem;
            if (objRowView == null)
            {
                // Added by Ripan Paul on 23-07-2011
                BS.BS_PartyName = cmbParty.Text;
                //--
                if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                    BS.P_OrderNo = txtReference.Text;

                //--
                TrnPartySave frmPartySave = new TrnPartySave();
                frmPartySave.txtPartyName.Text = BS.BS_PartyName;
                frmPartySave.Text = "Party Master";
                frmPartySave.ShowDialog();

                strSQL = "SELECT PARTY_ID," +
                         "       PARTY_NAME " +
                         "FROM   MST_PARTY " +
                         "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                         "ORDER BY PARTY_NAME ";

                DMLService dml = new DMLService();
                if (dml.J_PopulateComboBox(strSQL, ref cmbParty) == false) return;
                cmbParty.Text = BS.BS_PartyName;
                dml.Dispose();

                cmbParty.Focus();
            }
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


        #region txtRemarks_KeyPress
        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                setTextBoxInGrid();
            }
        }
        #endregion


        #region flxgrdDetails_ClickEvent
        private void flxgrdDetails_ClickEvent(object sender, EventArgs e)
        {
            if (flxgrdDetails.Row == 1 
                && (//flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                   flxgrdDetails.Col == (int)enmInvoiceEntry.RATE
                || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS))
                flxgrdDetails.RowSel = 1;
            setTextBoxInGrid();
        }
        #endregion

        #region flxgrdDetails_KeyPressEvent
        private void flxgrdDetails_KeyPressEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent e)
        {
            if (e.keyAscii == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.SL_NO
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.AMOUNT
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.REMARKS;
                    setTextBoxInGrid();
                }
            }
        }
        #endregion

        #region flxgrdDetails_MouseMoveEvent
        private void flxgrdDetails_MouseMoveEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEvent e)
        {
            cmnService.J_GridToolTip(flxgrdDetails, e.x, e.y);
        }
        #endregion

        #region flxgrdDetails_KeyDownEvent
        private void flxgrdDetails_KeyDownEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEvent e)
        {
            if (e.keyCode == 112 && flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME) 
                PopulateItemMaster();
            if (e.keyCode == 46)
            {
                if (cmnService.J_UserMessage(J_Msg.WantToRemove,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    flxgrdDetails.Row = flxgrdDetails.RowSel;
                    flxgrdDetails.Col = flxgrdDetails.ColSel;
                    flxgrdDetails.Select();

                    return;
                }

                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_ID, "0");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_NAME, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.QUANTITY, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.UNIT, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.RATE, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.AMOUNT, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.REMARKS, "");

                CalculateNetAmount();
            }
        }
        #endregion

        #region flxgrdDetails_DblClick
        private void flxgrdDetails_DblClick(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit) return;
            //--
            if (flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME) 
                PopulateItemMaster();
        }
        #endregion

        #region txtDiscountRate_KeyPress
        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (txtDiscountAmount.ReadOnly == false) SendKeys.Send("{tab}");
                else
                {
                    flxgrdTax.Row = 1;
                    flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                    flxgrdTax.Select();
                }
            }
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,2", txtDiscountRate, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDiscountRate_TextChanged
        private void txtDiscountRate_TextChanged(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) == 0) txtDiscountAmount.Text = "0.00";
            CalculateNetAmount();
        }
        #endregion

        #region txtDiscountRate_Leave
        private void txtDiscountRate_Leave(object sender, EventArgs e)
        {
            txtDiscountRate.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtDiscountRate)), "0.00");
        }
        #endregion

        #region txtDiscountAmount_KeyPress
        private void txtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                flxgrdTax.Row = 1;
                flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                flxgrdTax.Select();
            }
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtDiscountAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDiscountAmount_TextChanged
        private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtDiscountAmount_Leave
        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            txtDiscountAmount.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtDiscountAmount)), "0.00");
        }
        #endregion


        #region flxgrdTax_KeyPressEvent
        private void flxgrdTax_KeyPressEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent e)
        {
            if (e.keyAscii == 13)
            {
                if (flxgrdTax.get_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_DESC) == "") return;

                if (flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC
                    || flxgrdTax.Col == (int)enmInvoiceTax.TAX_RATE
                    || flxgrdTax.Col == (int)enmInvoiceTax.TAX_AMOUNT)
                {
                    if (flxgrdTax.RowSel == flxgrdTax.Rows - 1)
                    {
                        flxgrdTax.Rows = flxgrdTax.Rows + 1;
                        flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                        flxgrdTax.Row = Convert.ToInt32(flxgrdTax.Row + 1);
                        flxgrdTax.Select();
                    }
                    else
                    {
                        flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                        flxgrdTax.Row = Convert.ToInt32(flxgrdTax.Row + 1);
                        flxgrdTax.Select();
                    }
                }
            }
        }
        #endregion

        #region flxgrdTax_KeyDownEvent
        private void flxgrdTax_KeyDownEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEvent e)
        {
            if (e.keyCode == 112 && flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC) PopulateTaxMaster();
            if (e.keyCode == 46)
            {
                if (cmnService.J_UserMessage(J_Msg.WantToRemove,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    flxgrdTax.Row = flxgrdTax.RowSel;
                    flxgrdTax.Col = flxgrdTax.ColSel;
                    flxgrdTax.Select();
                    
                    return;
                }

                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_ID, "0");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_DESC, "");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_RATE, "");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_AMOUNT, "");
                
                CalculateNetAmount();
            }
        }
        #endregion

        #region flxgrdTax_DblClick
        private void flxgrdTax_DblClick(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Edit) return;
            //--            
            if (flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC) PopulateTaxMaster();
        }
        #endregion

        #region flxgrdTax_MouseMoveEvent
        private void flxgrdTax_MouseMoveEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEvent e)
        {
            cmnService.J_GridToolTip(flxgrdTax, e.x, e.y);
        }
        #endregion


        #region txtAdditionalCost_KeyPress
        private void txtAdditionalCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtAdditionalCost, "Y") == false)
                e.Handled = true;
        }
        #endregion

        #region txtAdditionalCost_TextChanged
        private void txtAdditionalCost_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtAdditionalCost_Leave
        private void txtAdditionalCost_Leave(object sender, EventArgs e)
        {
            txtAdditionalCost.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtAdditionalCost)), "0.00");
        }
        #endregion


        #region txtRoundedOff_KeyPress
        private void txtRoundedOff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtRoundedOff, "Y") == false)
                e.Handled = true;
        }
        #endregion

        #region txtRoundedOff_TextChanged
        private void txtRoundedOff_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtRoundedOff_Leave
        private void txtRoundedOff_Leave(object sender, EventArgs e)
        {
            txtRoundedOff.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtRoundedOff)), "0.00");
        }
        #endregion


        #region txtQty_KeyPress
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                    flxgrdDetails.Select();
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.REMARKS;
                    flxgrdDetails.Select();
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    double dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.AMOUNT) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.AMOUNT));

                    if (flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.ITEM_NAME) == "" && dblAmount == 0) return;

                    if (intTempGridPosition < intMaxColumnEntries)
                    {
                        if (intTempGridPosition == flxgrdDetails.Rows - 1)
                        {
                            int intSLNO = Convert.ToInt32(flxgrdDetails.get_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.SL_NO));

                            flxgrdDetails.Rows = flxgrdDetails.Rows + 1;
                            flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                            flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row + 1);
                            flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.SL_NO, Convert.ToString(intSLNO + 1));
                            flxgrdDetails.Select();
                        }
                        else
                        {
                            flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                            flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row + 1);
                            flxgrdDetails.Select();
                        }
                    }
                    else
                    {
                        flxgrdDetails.Col = (int)enmInvoiceEntry.REMARKS;
                        flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row);
                        flxgrdDetails.Select();
                    }

                }
            }
            else
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    //if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "N,3,0", txtQty, "") == false) e.Handled = true;
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "N,10,2", txtQty, "") == false) e.Handled = true;
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "V,150,0", txtQty, "") == false) e.Handled = true;
                }
            }
        }
        #endregion

        #region txtQty_TextChanged
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (blnAmount == false)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    if (txtQty.Text == "" || txtQty.Text == "-" || txtQty.Text == "-." || txtQty.Text == ".")
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.QUANTITY, "0");
                    else
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.QUANTITY, (txtQty.Text == "" ? "0" : txtQty.Text));
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    if (txtQty.Text == "" || txtQty.Text == "-" || txtQty.Text == "-." || txtQty.Text == ".")
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.RATE, "0.00");
                    else
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.RATE, (txtQty.Text == "" ? "0.00" : txtQty.Text));
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.REMARKS, txtQty.Text);
                }

                NumberFormatDetail(flxgrdDetails);
                CalculateNetAmount();

            }
        }
        #endregion

        #region txtQty_Leave
        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Visible = false;
        }
        #endregion


        #region BtnEditInvoice_Click
        private void BtnEditInvoice_Click(object sender, EventArgs e)
        {
            // Company
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the company.");
                cmbCompany.Select();
                return;
            }

            // set the Help Grid Column Header Text & behavior
            // (0) Header Text
            // (1) Width
            // (2) Format
            // (3) Alignment
            string[,] strMatrix = {{"Invoice Header Id", "0", "", "Right"},
                                   {"Invoice No.", "120", "", ""},
                                   {"Invoice Date", "80", "dd/MM/yyyy", ""},
                                   {"Party", "230", "", ""},
                                   {"Net Amount.", "80", "0.00", "Right"},
                                   {"Party Category", "0", "", ""}};

            strSQL = "SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID," +
                     "       TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO," +
                     "       " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE," +
                     "       MST_PARTY.PARTY_NAME                           AS PARTY_NAME," +
                     "       TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT," +
                     "       MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION  AS PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   TRN_INVOICE_HEADER," +
                     "       MST_PARTY, " +
                     "       MST_PARTY_CATEGORY " +
                     "WHERE  TRN_INVOICE_HEADER.PARTY_ID   = MST_PARTY.PARTY_ID " +
                     "AND    MST_PARTY.PARTY_CATEGORY_ID   = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID "+ 
                     "AND    TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "AND    TRN_INVOICE_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                     "AND    TRN_INVOICE_HEADER.TRAN_TYPE  = 'INV' " +
                     "AND    TRN_INVOICE_HEADER.DELIVERY_MODE_ID > 0 " +
                     "ORDER BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

            CmnHelp objhelp = new CmnHelp("Invoice Details", true, 600);
            objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
            objhelp.J_pSearchColumnName = "Invoice No.";
            objhelp.J_pSearchColumnType = J_ColumnType.String;
            objhelp.ShowDialog();

            if (J_Var.J_pMatrix[0] != null)
            {
                // Mode
                lblMode.Text = J_Mode.Edit;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;

                // Button behavior
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                //BtnDelete.Enabled = true;
                //BtnDelete.BackColor = Color.Lavender;
                
                // Clear all the Controls
                ClearControls();

                // display respective data
                SearchId = Convert.ToInt64(Convert.ToString(J_Var.J_pMatrix[0]));
                strSelectedPartyCategoryInEditMode = Convert.ToString(Convert.ToString(J_Var.J_pMatrix[5]));

                //--------------------------------------------------------------------
                // Party Category
                //--------------------------------------------------------------------
                if (strSelectedPartyCategoryInEditMode != BS_PartyCategory.Sundry)
                {
                    cmnService.J_UserMessage("You Cannot Edit Invoice other than Party Category sundry.");
                    cmbBillingMode.Select();
                    return;
                }
                if(ShowRecord(SearchId) == false) return;

                // enable controls
                Enable(lblMode.Text);
                mskInvoiceDate.Select();
            }

            Set_SLNO();

            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

        }
        #endregion

        #region txtOnlineOrderNo_Leave
        private void txtOnlineOrderNo_Leave(object sender, EventArgs e)
        {
            try
            {
                strDetails = "";
                if (txtOnlineOrderNo.Text.Trim() != "")
                {
                    bgWorkerOnlineOrderNo.RunWorkerAsync();
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return;
            }
        }
        #endregion

        #region bgWorkerOnlineOrderNo_DoWork
        private void bgWorkerOnlineOrderNo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.ReturnDetails, txtOnlineOrderNo.Text.Trim(), "", "",out strDetails) == false)
                {
                    return;
                }
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        #region bgWorkerOnlineOrderNo_RunWorkerCompleted
        private void bgWorkerOnlineOrderNo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (strDetails != "")
                {
                    grpOnlinePaymentDetails.Height = 162;
                    //
                    string[] strARRDetails = strDetails.Split('^');
                    //
                    #region FETCH DATA
                    BS.P_Billing_Salutation = strARRDetails[0].Trim();
                    BS.P_Billing_FName = strARRDetails[1].Trim();
                    BS.P_Billing_LName = strARRDetails[2].Trim();
                    BS.P_Billing_Company = strARRDetails[3].Trim();
                    BS.P_Billing_Address = strARRDetails[4].Trim();
                    BS.P_Billing_City = strARRDetails[5].Trim();
                    BS.P_Billing_State = strARRDetails[6].Trim();
                    BS.P_Billing_Pin = strARRDetails[7].Trim();
                    BS.P_Billing_Email = strARRDetails[8].Trim();
                    BS.P_Billing_Mobile = strARRDetails[9].Trim();
                    BS.P_Billing_Telephone = strARRDetails[10].Trim();
                    BS.P_CD_SerialID = strARRDetails[11].Trim();
                    BS.P_OrderNo = strARRDetails[12].Trim();
                    #endregion
                    //
                    txtOnlineContactPerson.Text = BS.P_Billing_Salutation + " " + BS.P_Billing_FName + " " + BS.P_Billing_LName;
                    txtOnlineParty.Text = BS.P_Billing_Company;
                    txtOnlineEmail.Text = BS.P_Billing_Email;
                    txtOnlineMobile.Text = BS.P_Billing_Mobile;
                    if (cmnService.J_ReturnInt64Value(BS.P_CD_SerialID) > 0)
                    {
                        lblOnlineOthers.Text = "Online Serial Key Allotted";
                        lblOnlineOthers.ForeColor = Color.Blue;
                    }
                    else
                    {
                        lblOnlineOthers.Text = "Online Serial Not Key Allotted";
                        lblOnlineOthers.ForeColor= Color.Red;
                    }
                }
                else
                    grpOnlinePaymentDetails.Height = 61;
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        //

        #region txtReference_Leave
        private void txtReference_Leave(object sender, EventArgs e)
        {
            try
            {
                strDetails = "";
                if (txtReference.Text.Trim() != "")
                {
                    bgWorkerOfflineOrderNo.RunWorkerAsync();
                }
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return;
            }
        }
        #endregion

        #region bgWorkerOfflineOrderNo_DoWork
        private void bgWorkerOfflineOrderNo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.ReturnDetails, txtReference.Text.Trim(), "", "", out strOfflineDetails) == false)
                {
                    return;
                }
            }
            catch (Exception err)
            {
            }
        }
        #endregion

        #region bgWorkerOfflineOrderNo_RunWorkerCompleted
        private void bgWorkerOfflineOrderNo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (strOfflineDetails != "")
                {
                    string[] strOfflineARRDetails = strOfflineDetails.Split('^');
                    //
                    #region FETCH DATA
                    BS.P_Billing_Salutation = strOfflineARRDetails[0].Trim();
                    BS.P_Billing_FName = strOfflineARRDetails[1].Trim();
                    BS.P_Billing_LName = strOfflineARRDetails[2].Trim();
                    BS.P_Billing_Company = strOfflineARRDetails[3].Trim();
                    BS.P_Billing_Address = strOfflineARRDetails[4].Trim();
                    BS.P_Billing_City = strOfflineARRDetails[5].Trim();
                    BS.P_Billing_State = strOfflineARRDetails[6].Trim();
                    BS.P_Billing_Pin = strOfflineARRDetails[7].Trim();
                    BS.P_Billing_Email = strOfflineARRDetails[8].Trim();
                    BS.P_Billing_Mobile = strOfflineARRDetails[9].Trim();
                    BS.P_Billing_Telephone = strOfflineARRDetails[10].Trim();
                    BS.P_CD_SerialID = strOfflineARRDetails[11].Trim();
                    BS.P_OrderNo = strOfflineARRDetails[12].Trim();
                    #endregion
                    //
                    if (Convert.ToInt32(BS.P_CD_SerialID) > 0)
                    {
                        cmnService.J_UserMessage("Serial has been delivered Online for this Order No.", MessageBoxIcon.Error);
                        txtReference.Text = "";
                        return;
                    }
                }
            }
            catch
            {
            }
        }
        #endregion


        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbPaymentType_SelectedIndexChanged
        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnPaymentType == false) return;
            if (cmbPaymentType.SelectedIndex <= 0)
            {
                lblAutoCollectionPostFlag.Text = "0";
                lblSundryPartyFlag.Text = "0";
                return;
            }
            //
            lblSundryPartyFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUNDRY_PARTY_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            lblAutoCollectionPostFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT AUTO_COLLECTION_POST_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            //
        }
        #endregion

        #endregion

        #region User Define Functions

        #region PopulateItemMaster [OVERLOADED METHOD]

        #region PopulateItemMaster[1]
        private bool PopulateItemMaster()
        {
            return PopulateItemMaster(null,0);
        }
        #endregion 

        #region PopulateItemMaster[2]
        private bool PopulateItemMaster(IDbCommand command, int ProductId)
        {
            // declaration of variables
            IDataReader reader = null;
            try
            {
                // set the Help Grid Column Header Text & behavior
                // (0) Header Text
                // (1) Width
                // (2) Format
                // (3) Alignment
                string[,] strMatrix = {{"Item Id", "0", "", "Right"},
                                   {"Item Name", "300", "", ""},
                                   {"Rate", "100", "0.00", "Right"},
                                   {"Unit", "80", "0.00", ""}};

                strSQL = " SELECT ITEM_ID," +
                         "        ITEM_NAME," +
                         "        RATE," +
                         "        UNIT " +
                         " FROM   MST_ITEM " +
                         " WHERE  INACTIVE_FLAG = 0 " +
                         " AND    ONLINE_FLAG   = 1 ";

                if (cmbCompany.SelectedIndex > 0)
                    strSQL = strSQL + " AND  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " ";

                if (ProductId > 0)
                    strSQL = strSQL + " AND  MST_ITEM.ITEM_ID = " + ProductId + " ";

                strSQL = strSQL + " ORDER BY ITEM_NAME";



                if (ProductId > 0)
                {
                    reader = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (reader == null) return false;

                    while (reader.Read())
                    {
                        flxgrdDetails.set_TextMatrix(1, (int)enmInvoiceEntry.ITEM_ID, Convert.ToString(reader["ITEM_ID"]));
                        flxgrdDetails.set_TextMatrix(1, (int)enmInvoiceEntry.ITEM_NAME, Convert.ToString(reader["ITEM_NAME"]));
                        flxgrdDetails.set_TextMatrix(1, (int)enmInvoiceEntry.UNIT, Convert.ToString(reader["UNIT"]));
                        flxgrdDetails.set_TextMatrix(1, (int)enmInvoiceEntry.QUANTITY,"1");
                        flxgrdDetails.set_TextMatrix(1, (int)enmInvoiceEntry.RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(reader["RATE"])), "0.00"));
                    }

                    // data reader object is closed
                    reader.Dispose();
                }
                else
                {
                    CmnHelp objhelp = new CmnHelp("Item Details", true, 570);
                    objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
                    objhelp.J_pSearchColumnName = "Item Name";
                    objhelp.J_pSearchColumnType = J_ColumnType.String;
                    objhelp.ShowDialog();

                    if (J_Var.J_pMatrix[0] != null)
                    {
                        flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_ID, Convert.ToString(J_Var.J_pMatrix[0]));
                        flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_NAME, Convert.ToString(J_Var.J_pMatrix[1]));
                        flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.UNIT, Convert.ToString(J_Var.J_pMatrix[3]));
                        flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(J_Var.J_pMatrix[2])), "0.00"));
                    }
                }
                CalculateNetAmount();
                //
                return true;
            }
            catch (Exception err)
            {
                reader.Dispose();
                cmnService.J_UserMessage(err.Message);
                return false; 
            }
        }
        #endregion

        #endregion

        #region PopulateTaxMaster[OVERLOADED METHOD]

        #region PopulateTaxMaster[1]
        private bool PopulateTaxMaster()
        {
            return PopulateTaxMaster(null, 0);
        }
        #endregion 

        #region PopulateTaxMaster[2]
        private bool PopulateTaxMaster(IDbCommand Command,int TaxId)
        {
            // declaration of variables
            IDataReader reader = null;
            try
            {
                // set the Help Grid Column Header Text & behavior
                // (0) Header Text
                // (1) Width
                // (2) Format
                // (3) Alignment
                string[,] strMatrix = {{"Tax Id", "0", "", "Right"},
                                       {"Tax Description", "300", "", ""},
                                       {"Rate", "100", "0.00", "Right"}};

                strSQL = "SELECT TAX_ID," +
                         "       TAX_DESC," +
                         "       TAX_RATE " +
                         "FROM   MST_TAX " +
                         "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " ";

                if (cmbCompany.Items.Count > 0)
                    if (cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) == BS_CompanyId.PDSInfotech)
                        strSQL = strSQL + "AND    TAX_ID   NOT IN(2, 4, 5)";

                if (TaxId > 0)
                     strSQL = strSQL + " AND TAX_ID = " + TaxId + " ";

                strSQL = strSQL + "ORDER BY TAX_DESC";

                if (TaxId > 0)
                {
                    reader = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (reader == null) return false;

                    while (reader.Read())
                    {
                        flxgrdTax.set_TextMatrix(1, (int)enmInvoiceTax.TAX_ID, Convert.ToString(reader["TAX_ID"]));
                        flxgrdTax.set_TextMatrix(1, (int)enmInvoiceTax.TAX_DESC, Convert.ToString(reader["TAX_DESC"]));
                        flxgrdTax.set_TextMatrix(1, (int)enmInvoiceTax.TAX_RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(reader["TAX_RATE"])), "0.00"));
                    }
                    // data reader object is closed
                    reader.Dispose();
                }
                else
                {

                    CmnHelp objhelp = new CmnHelp("Tax Details", true, 490);
                    objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
                    objhelp.J_pSearchColumnName = "Tax Description";
                    objhelp.J_pSearchColumnType = J_ColumnType.String;
                    objhelp.ShowDialog();

                    if (J_Var.J_pMatrix[0] != null)
                    {
                        flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_ID, Convert.ToString(J_Var.J_pMatrix[0]));
                        flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_DESC, Convert.ToString(J_Var.J_pMatrix[1]));
                        flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(J_Var.J_pMatrix[2])), "0.00"));
                    }
                }
                CalculateNetAmount();
                return true;
            }
            catch (Exception err)
            {
                reader.Dispose();
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

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
            //CREATE A DIRECTORY TO GENERATE THE PDF FILES
            cmnService.J_CreateDirectory(Application.StartupPath + "\\" + strGenerateInvoiceFolderName);
            //--------------------------------------------------------------------------
            txtChallanRefNo.Text = "";
            txtOrderNo.Text = "";
            txtOnlineOrderNo.Text = "";
            txtOnlineOrderNo.Enabled = true;
            grpOnlinePaymentDetails.Height = 61;
            txtInvoiceNo.Visible = false; lblInvoiceNo.Visible = false;
            //----------------------------------------------------------
            strSQL = " SELECT DELIVERY_MODE_ID, " +
                     "        DELIVERY_MODE_DESC" +
                     " FROM   PAR_DELIVERY_MODE " +
                     " WHERE  ONLINE_FLAG <> 0  " +
                     " ORDER BY DELIVERY_MODE_ID ";

            cmbBillingMode.Enabled = true;
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBillingMode,0) == false) return;
            //----------------------------------------------------------
            strSQL = "SELECT PARTY_ID," +
                     "       PARTY_NAME " +
                     "FROM   MST_PARTY " +
                     "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                     "ORDER BY PARTY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbParty,0) == false) return;
            //----------------------------------------------------------
            if (cmbCompany.Items.Count > 0) cmbCompany.SelectedIndex = 1;
            //----------------------------------------------------------
            txtRemarks.Text = "";

            // Clear the detail grid
            this.setDetailsGridRefresh(flxgrdDetails);
            this.setDetailsGridColumns(flxgrdDetails);
            
            txtTotalAmount.Text = "0.00";

            txtDiscountText.Text = "";
            txtDiscountRate.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtAmountWithDiscount.Text = "0.00";

            // Clear the tax grid
            this.setDetailsGridRefresh_TAX(flxgrdTax);
            this.setDetailsGridColumns_TAX(flxgrdTax);
            
            txtTaxAmount.Text = "0.00";
            txtAmountWithTax.Text = "0.00";

            txtAdditionalCostText.Text = "";
            txtAdditionalCost.Text = "0.00";
            txtAmountWithAdditionalCost.Text = "0.00";

            txtRoundedOff.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtNetAmountInwords.Text = "Zero only.";

            Set_SLNO();

            grpSort.Visible   = false;
            grpSearch.Visible = false;

            //Added by Shrey Kejriwal on 13-04-2013

            //Populating Payment Type Combo
            strSQL = "SELECT PAYMENT_TYPE_ID," +
                     "       PAYMENT_TYPE_DESCRIPTION " +
                     "FROM   MST_PAYMENT_TYPE " +
                     "WHERE  INACTIVE_FLAG = 0 " +
                     "ORDER BY PAYMENT_TYPE_DESCRIPTION ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType,0) == false) return;

            //Populating Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBank, 0) == false) return;

            //Populating Online Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbOnlineBank, 0) == false) return;

            txtReference.Text = "";
            lblAllotedSerialNo.Visible = false;
            //Added by Shrey Kejriwal on 03/05/2013
            mskInvoiceDate.Text = DateTime.Now.ToString();
            //--------------------------------------------------------------------------
            PopulateItemMaster(null,cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT ITEM_ID FROM MST_ITEM WHERE DEFAULT_ITEM_ONLINE_OFFLINE_BILLING = 1"))));
            PopulateTaxMaster(null, BS_TaxId.VAT_ID_5);
            //--------------------------------------------------------------------------
            BS.P_Billing_Salutation = "";
            BS.P_Billing_FName = "";
            BS.P_Billing_LName = "";
            BS.P_Billing_Company = "";
            BS.P_Billing_Address = "";
            BS.P_Billing_City = "";
            BS.P_Billing_State = "";
            BS.P_Billing_Pin = "";
            BS.P_Billing_Email = "";
            BS.P_Billing_Mobile = "";
            BS.P_Billing_Telephone = "";
            BS.P_CD_SerialID = "";
            //
            mskPaymentDate.Text = "";
        }
        #endregion

        #region setDetailsGridRefresh
        private void setDetailsGridRefresh(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, 10);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < 10; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;

            HFlexGrid.set_TextMatrix(1, (int)enmInvoiceEntry.SL_NO, "1");
        }
        #endregion

        #region setDetailsGridColumns
        private void setDetailsGridColumns(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // INVOICE_DETAIL_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.INVOICE_DETAIL_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.INVOICE_DETAIL_ID, 0, 0);

            // SL_NO
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.SL_NO;
            HFlexGrid.Text = "SL No.";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.SL_NO, 0, 700);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.SL_NO, (short)J_Alignment.RightCentre);
            
            // ITEM_NAME
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.ITEM_NAME;
            HFlexGrid.Text = "Item Name";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.ITEM_NAME, 0, 4600);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.ITEM_NAME, (short)J_Alignment.LeftCentre);
            
            // QUANTITY
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.QUANTITY;
            HFlexGrid.Text = "Quantity";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.QUANTITY, 0, 1000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.QUANTITY, (short)J_Alignment.RightCentre);
            
            // UNIT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.UNIT;
            HFlexGrid.Text = "Unit";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.UNIT, 0, 800);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.UNIT, (short)J_Alignment.LeftCentre);

            // RATE
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.RATE;
            HFlexGrid.Text = "Rate";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.RATE, 0, 1000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.RATE, (short)J_Alignment.RightCentre);

            // AMOUNT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.AMOUNT;
            HFlexGrid.Text = "Amount";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.AMOUNT, 0, 1500);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.AMOUNT, (short)J_Alignment.RightCentre);

            // REMARKS
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.REMARKS;
            HFlexGrid.Text = "Remarks";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.REMARKS, 0, 4700);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.REMARKS, (short)J_Alignment.LeftCentre);
            
            // ITEM_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.ITEM_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.ITEM_ID, 0, 0);
            
        }
        #endregion

        #region NumberFormatDetail
        private void NumberFormatDetail(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            for (int intCounter = 1; intCounter <= HFlexGrid.Rows - 1; intCounter++)
            {
                // Formatting Rate
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE))));

                // Formatting Amount
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT))));
            }
        }
        #endregion

        #region setTextBoxInGrid
        private void setTextBoxInGrid()
        {
            intTempGridPosition = flxgrdDetails.RowSel;

            if (flxgrdDetails.Rows > 1 && (//flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                   flxgrdDetails.Col == (int)enmInvoiceEntry.RATE
                || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS))
            {
                //-- For Text Box in Grid
                txtQty.Visible = true;

                txtQty.Left = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellLeft) + flxgrdDetails.Left - 1);
                txtQty.Top = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellTop) + flxgrdDetails.Top - 1);
                txtQty.Width = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellWidth));
                txtQty.Height = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellHeight) - 5);

                blnAmount = true;

                txtQty.Text = "";
                txtQty.Text = flxgrdDetails.get_TextMatrix(intTempGridPosition, flxgrdDetails.ColSel);

                if (//flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                      flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                    txtQty.TextAlign = HorizontalAlignment.Right;
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                    txtQty.TextAlign = HorizontalAlignment.Left;

                blnAmount = false;
                txtQty.Select();
                txtQty.Focus();
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            //
            if (BillingSystem.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Internet Connection not found");
                return false;
            }
            // Bill Mode
            if (cmbBillingMode.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select a Bill Mode.");
                cmbBillingMode.Select();
                return false;
            }

            // Company
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the company.");
                cmbCompany.Select();
                return false;
            }

            // Invoice Series
            if (cmbInvoiceSeries.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the invoice series.");
                cmbInvoiceSeries.Select();
                return false;
            }

            // Invoice Date
            if (dtService.J_IsBlankDateCheck(ref mskInvoiceDate, "Please enter the invoice date.") == true)
                return false;

            // Invoice Date validation
            if (dtService.J_IsDateValid(mskInvoiceDate) == false)
            {
                cmnService.J_UserMessage("Please enter the valid invoice date.");
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
            
            // Collection Date should be within Current Date
            if (dtService.J_IsDateGreater(ref mskInvoiceDate, dmlService.J_ReturnServerDate(), "", "",
                "Invoice date should not greater than the server date." +
                "\n\nServer Date : " + dmlService.J_ReturnServerDate(), J_ShowMessage.YES) == false)
                return false;

            // Party
            if (cmbParty.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the party.");
                cmbParty.Select();
                return false;
            }

            // Bill Mode WHEN Offline
            if (cmbBillingMode.SelectedIndex > 0 && cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
            {
                // PaymentType
                if (cmbPaymentType.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select the Payment Type.");
                    cmbPaymentType.Select();
                    return false;
                }
                //else //-- 2015/03/31
                //{
                //    if (lblPartyCategory.Text.ToUpper() == "SUNDRY")
                //    {
                //        if (lblSundryPartyFlag.Text == "0")
                //        {
                //            cmnService.J_UserMessage("Payment Type : " + cmbPaymentType.Text + " is not allowed for 'Sundry Party'");
                //            cmbPaymentType.Select();
                //            return false;
                //        }
                //    }
                //}
                // Reference No.
                if (txtReference.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter Reference No.");
                    txtReference.Select();
                    return false;
                }
                else
                {
                    if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.ReturnDetails, txtReference.Text.Trim(), "", "", out strOfflineDetails) == false)
                    {
                        cmnService.J_UserMessage("Can't check the Web Database...\nPls try later...");
                        return false;
                    }
                    //--
                    if (strOfflineDetails != "")
                    {
                        string[] strOfflineARRDetails = strOfflineDetails.Split('^');
                        //
                        if (Convert.ToInt32(strOfflineARRDetails[11].Trim()) > 0)
                        {
                            cmnService.J_UserMessage("Serial has been delivered Online for this Order No.\nOffline Delivery Mode Invoice is not possible.", MessageBoxIcon.Error);
                            txtReference.Select();
                            return false;
                        }
                    }
                }
                //--
                if (cmbBank.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select a bank.");
                    cmbBank.Select();
                    return false;
                }
                else
                {
                    if (cmbPaymentType.Text == BS_PaymentType.Cc_Avenue && Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT CC_AVENUE_FLAG FROM MST_BANK WHERE BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex))) == 0)
                    {
                        cmnService.J_UserMessage("Improper bank selected.");
                        cmbBank.Select();
                        return false;
                    }
                }

            }
            // Bill Mode WHEN ONLINE
            else if (cmbBillingMode.SelectedIndex > 0 && cmbBillingMode.Text == BS_BillMode.OnlineDelivery)
            {
                // BANK_ID
                if (cmbOnlineBank.SelectedIndex <= 0)
                {
                    cmnService.J_UserMessage("Please select the Bank.");
                    cmbOnlineBank.Select();
                    return false;
                }
                //Online Order No
                if (txtOnlineOrderNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter Online Order No.");
                    txtOnlineOrderNo.Select();
                    return false;
                }
                //--
                //strSQL = "SELECT COUNT(*) FROM TRN_INVOICE_HEADER WHERE ONLINE_ORDER_NO = " + txtOnlineOrderNo.Text.Trim();
                if (lblMode.Text == J_Mode.Add)
                {
                    if (dmlService.J_IsRecordExist("TRN_INVOICE_HEADER", "ONLINE_ORDER_NO ='" + txtOnlineOrderNo.Text.Trim() + "'") == true)
                    {
                        cmnService.J_UserMessage("Online Order No. already exist");
                        txtOnlineOrderNo.Select();
                        return false;
                    }
                }
                else if (lblMode.Text == J_Mode.Edit)
                {
                    if (dmlService.J_IsRecordExist("TRN_INVOICE_HEADER", "ONLINE_ORDER_NO = '" + txtOnlineOrderNo.Text.Trim() + "' AND INVOICE_HEADER_ID <> " + SearchId) == true)
                    {
                        cmnService.J_UserMessage("Online Order No. already exist");
                        txtOnlineOrderNo.Select();
                        return false;
                    }
                }
                //--
                string strOut = "";
                if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.OnlineDeliverySerialNo, txtOnlineOrderNo.Text, "", "", out strOut) == false)
                {
                    cmnService.J_UserMessage("Invalid Online Order No.", MessageBoxIcon.Exclamation);
                    txtOnlineOrderNo.Select();
                    return false;
                }
            }
            //-- 2015/03/31
            if (lblAutoCollectionPostFlag.Text == "1" && lblPartyCategory.Text.ToUpper() == "SUNDRY" && J_Var.J_pCollectionPostFlag > 0)
            {
                // Collection Date
                if (dtService.J_IsBlankDateCheck(ref mskPaymentDate, "Please enter the Payment date.") == true)
                    return false;
                // Invoice Date validation
                if (dtService.J_IsDateValid(mskPaymentDate) == false)
                {
                    cmnService.J_UserMessage("Please enter the valid Payment date.");
                    mskPaymentDate.Select();
                    return false;
                }
                //
                if (dtService.J_IsDateGreater(ref mskPaymentDate, ref mskInvoiceDate, "", "",
                "Payment date should not be greater than the Invoice date.", J_ShowMessage.YES) == false)
                    return false;
            }
            //--
            if (Convert.ToDouble(txtTotalAmount.Text.Trim()) == 0)
            {
                cmnService.J_UserMessage("At least one item details required");
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }



            // Item Detail grid data validation
            int iItem = 0;
            int iRowIndex = 0;

            long lngItemId = 0;
            string strItem = "";
            int intQuantity = 0;
            double dblRate = 0;
            
            for (int iRIndex = 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
            {
                iRowIndex = iRIndex;

                lngItemId = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID));
                strItem = Convert.ToString(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_NAME));
                intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.QUANTITY));
                dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.RATE) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.RATE));

                // check item is empty or not
                if (string.IsNullOrEmpty(strItem) == false)
                {
                    if (intQuantity == 0)
                    {
                        iItem = 1;
                        goto Description;
                    }
                    if (dblRate == 0)
                    {
                        iItem = 2;
                        goto Description;
                    }
                    if (CheckDuplicateItem(lngItemId, iRIndex, out iRowIndex) == false)
                    {
                        iItem = 3;
                        goto Description;
                    }
                }
                else
                {
                    if (intQuantity > 0 || dblRate > 0)
                    {
                        iItem = 4;
                        goto Description;
                    }
                }
                iItem = 10;
            }

        Description:
            if (iItem == 0)
            {
                cmnService.J_UserMessage("No record in grid.");
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }
            if (iItem == 1)
            {
                cmnService.J_UserMessage("Please enter quantity");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.QUANTITY;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }
            if (iItem == 2)
            {
                cmnService.J_UserMessage("Please enter rate");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }
            if (iItem == 3)
            {
                cmnService.J_UserMessage("Duplicate item");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }
            if (iItem == 4)
            {
                cmnService.J_UserMessage("Please select the item");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }

            // Tax Detail grid data validation
            int iTax = 0;
            iRowIndex = 0;
            long lngTaxId = 0;
            
            for (int iRIndex = 1; iRIndex <= flxgrdTax.Rows - 1; iRIndex++)
            {
                iRowIndex = iRIndex;

                lngTaxId = Convert.ToInt32(flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                    ? "0"
                                    : flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID));
                
                // check tax is empty or not
                if (lngTaxId > 0)
                {
                    if (CheckDuplicateTax(lngTaxId, iRIndex, out iRowIndex) == false)
                    {
                        iTax = 1;
                        goto Description_Tax;
                    }
                }
            }

        Description_Tax:
            if (iTax == 1)
            {
                cmnService.J_UserMessage("Duplicate tax");
                flxgrdTax.Row = iRowIndex;
                flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                flxgrdTax.Select();
                return false;
            }

            // check additional cost text & amount together
            if (txtAdditionalCostText.Text == "" && Convert.ToDouble(txtAdditionalCost.Text.Trim()) != 0)
            {
                cmnService.J_UserMessage("Please enter additional cost text");
                txtAdditionalCostText.Select();
                return false;
            }
            if (txtAdditionalCostText.Text != "" && Convert.ToDouble(txtAdditionalCost.Text.Trim()) == 0)
            {
                cmnService.J_UserMessage("Please enter additional cost");
                txtAdditionalCost.Select();
                return false;
            }
            //-- DOWNLOAD THE PDF FILE
            //--
            for (int iRIndex = 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
            {
                strSQL = "SELECT PDF_DOC_NAME FROM MST_ITEM WHERE ITEM_ID = " + lngItemId;
                string PDFFileName = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                if (File.Exists(Path.Combine(Application.StartupPath, Path.GetFileName(PDFFileName))) == false)
                {
                    WebClient webclient = new WebClient();
                    //
                    webclient.DownloadFile(PDFFileName, Path.Combine(Application.StartupPath, Path.GetFileName(PDFFileName)));
                }
            }

            //--
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
            //ADDED BY DHRUB ON 24/05/2015
            //------------------------------------------------------------------------------------
            //WARNING MESSAGE FOR UNKNOWN PAYMENTS OTHER THAN CC_AVENUE 
            //IF ANY UNKNOWN PAYMENTS EXISTS ON THE INVOICE DATE 
            //IF INVOICE AMOUNT BECOME SAME THEN THE WARNING MESSAGE WILL BE GENERATED
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
//            if (lblMode.Text == J_Mode.Add && cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
//            {
//                strSQL = @" SELECT COUNT(*) 
//                            FROM   TRN_COLLECTION_HEADER 
//                            WHERE  TRN_COLLECTION_HEADER.NET_INVOICE_AMT = 0 
//                            AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0
//                            AND    TRN_COLLECTION_HEADER.NET_AMT         = " + txtNetAmount.Text;
//                if (dtService.J_IsBlankDateCheck(ref mskPaymentDate,J_ShowMessage.NO) == false) 
//                strSQL = strSQL  + @" AND    CONVERT(VARCHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) = " + dtService.J_ConvertToIntYYYYMMDD(mskPaymentDate);

//                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
//                {
//                    cmnService.J_UserMessage("Warning : There is an Unknown Collection.\nPlease make sure the collection entries.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }

            return true;
        }
        #endregion

        #region CheckDuplicateItem
        private bool CheckDuplicateItem(long ItemId, int RowIndex, out int DuplicateRowIndex)
        {
            long lngItemId_Dup = 0;
            DuplicateRowIndex = 0;

            for (int iRIndex = RowIndex + 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
            {
                lngItemId_Dup = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID));

                if (lngItemId_Dup == ItemId && lngItemId_Dup > 0)
                {
                    DuplicateRowIndex = iRIndex;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region CheckDuplicateTax
        private bool CheckDuplicateTax(long TaxId, int RowIndex, out int DuplicateRowIndex)
        {
            long lngTaxId_Dup = 0;
            DuplicateRowIndex = 0;
            
            for (int iRIndex = RowIndex + 1; iRIndex <= flxgrdTax.Rows - 1; iRIndex++)
            {
                lngTaxId_Dup = Convert.ToInt32(flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                    ? "0"
                                    : flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID));

                if (lngTaxId_Dup == TaxId && lngTaxId_Dup > 0)
                {
                    DuplicateRowIndex = iRIndex;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                //Initialize the Save status
                BS.BS_SaveInvoiceStatus = true;

                // declaration of local variables
                long lngInvoiceHeaderId = 0;
                long lngItemId          = 0;
                int intQuantity         = 0;
                double dblRate          = 0;
                double dblAmount        = 0;
                string strRemarks       = "";
                long lngBankID=0;
                long lngTaxId = 0;

                switch (lblMode.Text)
                {
                    #region ADD
                    case J_Mode.Add:
                        // All validation
                        if (ValidateFields() == false) return;                    
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref cmbInvoiceSeries) == true) return;
                        //-----------------------------------------------------------------
                        this.Cursor = Cursors.WaitCursor;
                        //--
                        FetchPartyDetails(cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex));
                        //-----------------------------------------------------------------
                        if (VerifyPartyDetails() == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //-----------------------------------------------------------------
                        //Check the Save Status based on the Verification form out put
                        //-----------------------------------------------------------------
                        if (BS.BS_SaveInvoiceStatus == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //--
                        if (cmbBillingMode.Text == BS_BillMode.OnlineDelivery)
                            cmbPaymentType.Text = BS_PaymentType.Cc_Avenue;
                        //-- BANK_ID
                        if (cmbBank.SelectedIndex > 0)
                            lngBankID = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                        else if (cmbOnlineBank.SelectedIndex > 0)
                            lngBankID = cmnService.J_GetComboBoxItemId(ref cmbOnlineBank, cmbOnlineBank.SelectedIndex);  
                        //--
                        // Transaction is started
                        dmlService.J_BeginTransaction();
                        //
                        this.Cursor = Cursors.WaitCursor;
                        // Getting last Invoice Number
                        GetInvoiceNo();
                        //
                        strSQL = "INSERT INTO TRN_INVOICE_HEADER (" +
                                 "            BRANCH_ID," +
                                 "            FAYEAR_ID," +
                                 "            COMPANY_ID," +
                                 "            PARTY_ID," +
                                 "            INVOICE_SERIES_ID," +
                                 "            INVOICE_NO," +
                                 "            INVOICE_DATE," +
                                 "            TRAN_TYPE," +
                                 "            SUB_TYPE," +
                                 "            CHALLAN_REF_NO," +
                                 "            ORDER_NO," +
                                 "            TOTAL_AMOUNT," +
                                 "            DISCOUNT_TEXT," +
                                 "            DISCOUNT_RATE," +
                                 "            DISCOUNT_AMOUNT," +
                                 "            AMOUNT_WITH_DISCOUNT," +
                                 "            TAX_TOTAL_AMOUNT," +
                                 "            AMOUNT_WITH_TAX," +
                                 "            ADDITIONAL_COST_TEXT," +
                                 "            ADDITIONAL_COST," +
                                 "            AMOUNT_WITH_ADDITIONAL_COST," +
                                 "            ROUNDED_OFF," +
                                 "            NET_AMOUNT," +
                                 "            NET_AMOUNT_INWORDS," +
                                 "            REMARKS," +
                                 "            PAYMENT_TYPE_ID," +
                                 "            BANK_ID," +
                                 "            REFERENCE_NO," +
                                 "            ONLINE_ORDER_NO," +
                                 "            DELIVERY_MODE_ID,"+
                                 "            CONTACT_PERSON," +
                                 "            MOBILE_NO," +
                                 "            EMAIL_ID," +
                                 "            USER_ID," +
                                 "            CREATE_DATE," +
                                 "            RECON_FLAG) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "            " + J_Var.J_pFAYearId + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex) + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "," +
                                 "           '" + txtInvoiceNo.Text + "'," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + "," +
                                 "           'INV'," +
                                 "           ''," +
                                 "           '" + cmnService.J_ReplaceQuote(txtChallanRefNo.Text) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtOrderNo.Text) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtTotalAmount.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtDiscountText.Text) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithDiscount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtTaxAmount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithTax.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtAdditionalCostText.Text.Trim()) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithAdditionalCost.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtRoundedOff.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtNetAmount.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtNetAmountInwords.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "'," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + "," +
                                 "            " + lngBankID + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtReference.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtOnlineOrderNo.Text.Trim()) + "'," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbBillingMode, cmbBillingMode.SelectedIndex) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(BS.BS_PartyContactPerson) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(BS.BS_PartyMobNo) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(BS.BS_PartyEmailId) + "'," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + "," +
                                 "            " + J_Var.J_pReconFlag + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            //Rollback Transaction
                            this.Cursor = Cursors.Default;
                            dmlService.J_Rollback();  
                            cmbCompany.Select();
                            return;
                        }
                        //
                        #region TRN_INVOICE_HEADER 'UPDATE' REFERENCE_NO
                        strSQL = @"UPDATE TRN_INVOICE_HEADER SET REFERENCE_NO = ONLINE_ORDER_NO 
                           WHERE  ONLINE_ORDER_NO <> ''
                           AND    REFERENCE_NO     = ''
                           AND    DELIVERY_MODE_ID = 1";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            //Rollback Transaction
                            this.Cursor = Cursors.Default;
                            dmlService.J_Rollback();
                            cmbCompany.Select();
                            return;
                        }
                        #endregion 
                        // get max ledger header id
                        lngInvoiceHeaderId = dmlService.J_ReturnMaxValue("TRN_INVOICE_HEADER", "INVOICE_HEADER_ID",
                                                                         "    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                                                                         "AND USER_ID    = " + J_Var.J_pUserId + "");
                        // Loop as per individual item
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngItemId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID));

                            // Quantity is stored
                            intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT));

                            // Remarks is stored
                            strRemarks = flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.REMARKS);


                            if (dblAmount != 0 && lngItemId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_DETAIL (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            ITEM_ID," +
                                         "            QUANTITY," +
                                         "            RATE," +
                                         "            AMOUNT," +
                                         "            REMARKS) " +
                                         "    VALUES( " + lngInvoiceHeaderId + "," +
                                         "            " + lngItemId + "," +
                                         "            " + intQuantity + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + "," +
                                         "           '" + cmnService.J_ReplaceQuote(strRemarks) + "')";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    this.Cursor = Cursors.Default;
                                    //Rollback Transaction
                                    dmlService.J_Rollback();  
                                    cmbCompany.Select();
                                    return;
                                }
                                //-------------------------------------------
                                //FETCH THE ITEM WISE EMAIL TEXT
                                //-------------------------------------------
                                //FetchItemWiseEmailDetails(lngItemId);
                            }
                        }
                        // Loop as per individual Tax
                        for (int intRIndex = 1; intRIndex <= flxgrdTax.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngTaxId = Convert.ToInt64(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT));

                            if (lngTaxId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_TAX (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            TAX_ID," +
                                         "            TAX_RATE," +
                                         "            TAX_AMOUNT) " +
                                         "    VALUES( " + lngInvoiceHeaderId + "," +
                                         "            " + lngTaxId + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    this.Cursor = Cursors.Default;
                                    //Rollback Transaction
                                    dmlService.J_Rollback();  
                                    cmbCompany.Select();
                                    return;
                                }
                            }
                        }
                        // update LAST_NO into MST_INVOICE_SERIES Table
                        strSQL = "UPDATE MST_INVOICE_SERIES " +
                                 "SET    LAST_NO = LAST_NO + 1 " +
                                 "WHERE  INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            this.Cursor = Cursors.Default;
                            //Rollback Transaction
                            dmlService.J_Rollback();
                            cmbCompany.Select();
                            return;
                        }
                        //Update Serial No
                        if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                        {
                            //-- CHECKING SERIAL NUMBER AVAILABILITY
                            strSQL = @"SELECT COUNT(MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID) AS BALANCE_IN_HAND
                                        FROM MST_OFFLINE_SERIAL
                                        LEFT JOIN TRN_INVOICE_HEADER 
                                             ON MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID
                                        WHERE TRN_INVOICE_HEADER.INVOICE_HEADER_ID IS NULL
                                        AND   MST_OFFLINE_SERIAL.INACTIVE_FLAG     = 0
                                        AND   MST_OFFLINE_SERIAL.ITEM_ID           = " + lngItemId;
                            //
                            if (Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)) == "0")
                            {
                                this.Cursor = Cursors.Default;
                                cmnService.J_UserMessage("Serial number not available for allotment", MessageBoxIcon.Exclamation);
                                dmlService.J_Rollback();
                                return;
                            }
                            // update LAST_NO into MST_INVOICE_SERIES Table
                            strSQL = @" UPDATE TRN_INVOICE_HEADER
                                        SET    OFFLINE_SERIAL_ID = SRL_DETAIL.OFFLINE_SERIAL_ID
                                        FROM   TRN_INVOICE_HEADER
                                               INNER JOIN     
    	                                       (SELECT TOP 1 MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID AS OFFLINE_SERIAL_ID,
	    	                                 '"+ Convert.ToString(lngInvoiceHeaderId) + @"'AS INVOICE_HEADER_ID
		                                       FROM   MST_OFFLINE_SERIAL
				                                      LEFT JOIN TRN_INVOICE_HEADER
				                                      ON MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID
		                                       WHERE  ITEM_ID = " + lngItemId +
                                            @" AND    MST_OFFLINE_SERIAL.INACTIVE_FLAG = 0
                                               AND    TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID IS NULL
                                               ORDER BY MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID) AS SRL_DETAIL
		                                       ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = SRL_DETAIL.INVOICE_HEADER_ID ";                                     
                            if (dmlService.J_ExecSql(strSQL) == false)
                            {
                                this.Cursor = Cursors.Default;
                                //Rollback Transaction
                                dmlService.J_Rollback();
                                cmbCompany.Select();
                                return;
                            }
                        }
                        // Transaction is commited
                        dmlService.J_Commit();
                        //--
                        //-- COLLECTION ENTRY //-- 31/03/2015
                        if (lblAutoCollectionPostFlag.Text == "1" && lblPartyCategory.Text.ToUpper() == "SUNDRY"  && J_Var.J_pCollectionPostFlag > 0)
                        {
                            long lngBankId = 0; string strReference = "";
                            // Invoice Series
                            if (cmbBillingMode.Text == BS_BillMode.OnlineDelivery)
                            {
                                lngBankId = cmnService.J_GetComboBoxItemId(ref cmbOnlineBank, cmbOnlineBank.SelectedIndex);
                                strReference = txtOnlineOrderNo.Text;
                            }
                            else
                            {
                                lngBankId = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                                strReference = txtReference.Text;
                            }
                            //
                            if (BillingSystem.InsertCollectionEntry(mskPaymentDate.Text,
                                                             cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex),
                                                             J_Var.J_pFAYearId,
                                                             cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex),
                                                             lngBankId,
                                                             cmnService.J_ReplaceQuote(strReference),
                                                             cmnService.J_ReturnDoubleValue(txtNetAmount.Text),
                                                             J_Var.J_pUserId,
                                                             lngInvoiceHeaderId) == false)
                            {
                                cmnService.J_UserMessage("Collection Entry could not be done");
                                return;
                            }
                        }
                        //--
                        //-------------------------------------------------------------
                        if(ProcessToSendEmailSms(lngInvoiceHeaderId, lngItemId)== true )
                        {
                            //cmnService.J_UserMessage("Email and SMS Successfully Send " +
                            //                            "\n Invoice No :" + txtInvoiceNo.Text +
                            //                            "\n Email Id :" + BS.BS_PartyEmailId +
                            //                            "\n Email :" + BS.BS_PartyMobNo); 
                        }
                        //-------------------------------------------------------------
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);

                        //-------------------------------------------------------------
                        // Initialize the Invoice Details into the variable
                        //-------------------------------------------------------------
                        BS.BS_SavedInvoiceDetails = txtInvoiceNo.Text + "^" +
                                                    BS.BS_OfflineSerialNo + "^" +
                                                    BS.BS_PartyEmailId + "^" +
                                                    BS.BS_PartyMobNo;

                        //-------------------------------------------------------------
                        // clear all controls
                        this.ClearControls();
                        //
                        cmnService.J_UserMessage("Data Saved");
                        //
                        this.Cursor = Cursors.Default;
                        //-------------------------------------------------------------
                        // Print the Invoice
                        //-------------------------------------------------------------
                        this.PrintInvoice(lngInvoiceHeaderId);                       
                        //
                        cmbCompany.Select();
                        //
                        break;
                    #endregion
                    //
                    #region EDIT
                    case J_Mode.Edit:


                        // All validation
                        if (ValidateFields() == false) return;

                        string strAcEntryDate = "";
                        string strBankDate = "";

                        strSQL = "SELECT ACCOUNT_ENTRY_DATE, BANK_STATEMENT_DATE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId;

                        IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);

                        while (reader.Read())
                        {
                            strAcEntryDate = reader["ACCOUNT_ENTRY_DATE"].ToString();
                            strBankDate = reader["BANK_STATEMENT_DATE"].ToString();
                        }

                        //---------------------------------------------
                        //Invoice cannot be modified if have any Reconciliation Date for any Collection Header 
                        //---------------------------------------------
                        if (blnInvoiceWiseReconciled == true)
                        {
                            cmnService.J_UserMessage("This invoice cannot be modified as it has already been reconciled into Collection.");
                            return;
                        }

                        reader.Close();
                        reader.Dispose();

                        if (strAcEntryDate != "" || strBankDate != "")
                        {
                            cmnService.J_UserMessage("This invoice cannot be modified as it has already been reconciled with the Bank/Accounts");
                            return;
                        }
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref mskInvoiceDate) == true) return;
                        //-----------------------------------------------------------------
                        FetchPartyDetails(cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex));
                        //-----------------------------------------------------------------
                        if (VerifyPartyDetails() == false) return;
                        //-----------------------------------------------------------------
                        if (BS.BS_SaveInvoiceStatus == false)
                            return;
                        //-- BANK_ID
                        if (cmbBank.SelectedIndex > 0)
                            lngBankID = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                        else if (cmbOnlineBank.SelectedIndex > 0)
                            lngBankID = cmnService.J_GetComboBoxItemId(ref cmbOnlineBank, cmbOnlineBank.SelectedIndex); 
                        // Transaction is started
                        dmlService.J_BeginTransaction();
                        //
                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    PARTY_ID                    =  " + cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex) + "," +
                                 "       INVOICE_DATE                =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + "," +
                                 "       CHALLAN_REF_NO              = '" + cmnService.J_ReplaceQuote(txtChallanRefNo.Text) + "'," +
                                 "       ORDER_NO                    = '" + cmnService.J_ReplaceQuote(txtOrderNo.Text) + "'," +
                                 "       TOTAL_AMOUNT                =  " + cmnService.J_ReturnDoubleValue(txtTotalAmount.Text) + "," +
                                 "       DISCOUNT_TEXT               = '" + cmnService.J_ReplaceQuote(txtDiscountText.Text) + "'," +
                                 "       DISCOUNT_RATE               =  " + cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) + "," +
                                 "       DISCOUNT_AMOUNT             =  " + cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) + "," +
                                 "       AMOUNT_WITH_DISCOUNT        =  " + cmnService.J_ReturnDoubleValue(txtAmountWithDiscount.Text) + "," +
                                 "       TAX_TOTAL_AMOUNT            =  " + cmnService.J_ReturnDoubleValue(txtTaxAmount.Text) + "," +
                                 "       AMOUNT_WITH_TAX             =  " + cmnService.J_ReturnDoubleValue(txtAmountWithTax.Text) + "," +
                                 "       ADDITIONAL_COST_TEXT        = '" + cmnService.J_ReplaceQuote(txtAdditionalCostText.Text.Trim()) + "'," +
                                 "       ADDITIONAL_COST             =  " + cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text) + "," +
                                 "       AMOUNT_WITH_ADDITIONAL_COST =  " + cmnService.J_ReturnDoubleValue(txtAmountWithAdditionalCost.Text) + "," +
                                 "       ROUNDED_OFF                 =  " + cmnService.J_ReturnDoubleValue(txtRoundedOff.Text) + "," +
                                 "       NET_AMOUNT                  =  " + cmnService.J_ReturnDoubleValue(txtNetAmount.Text) + "," +
                                 "       NET_AMOUNT_INWORDS          = '" + cmnService.J_ReplaceQuote(txtNetAmountInwords.Text.Trim()) + "'," +
                                 "       REMARKS                     = '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "', " +
                                 "       PAYMENT_TYPE_ID             =  " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + "," +
                                 "       BANK_ID                     =  " + lngBankID + "," +
                                 "       REFERENCE_NO                = '" + cmnService.J_ReplaceQuote(txtReference.Text.Trim()) + "', " +
                                 "       ONLINE_ORDER_NO             = '" + cmnService.J_ReplaceQuote(txtOnlineOrderNo.Text.Trim()) + "', " +
                                 "       DELIVERY_MODE_ID            =  " + cmnService.J_GetComboBoxItemId(ref cmbBillingMode, cmbBillingMode.SelectedIndex) + "," +
                                 "       CONTACT_PERSON              = '" + cmnService.J_ReplaceQuote(BS.BS_PartyContactPerson) + "', " +
                                 "       MOBILE_NO                   = '" + cmnService.J_ReplaceQuote(BS.BS_PartyMobNo) + "', " +
                                 "       EMAIL_ID                    = '" + cmnService.J_ReplaceQuote(BS.BS_PartyEmailId) + "' " +
                                 "WHERE  INVOICE_HEADER_ID           =  " + SearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // delete all records from TRN_INVOICE_DETAIL
                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_DETAIL WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // Loop as per individual item
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngItemId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID));

                            // Quantity is stored
                            intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT));

                            // Remarks is stored
                            strRemarks = flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.REMARKS);


                            if (dblAmount != 0 && lngItemId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_DETAIL (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            ITEM_ID," +
                                         "            QUANTITY," +
                                         "            RATE," +
                                         "            AMOUNT," +
                                         "            REMARKS) " +
                                         "    VALUES( " + SearchId + "," +
                                         "            " + lngItemId + "," +
                                         "            " + intQuantity + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + "," +
                                         "           '" + cmnService.J_ReplaceQuote(strRemarks) + "')";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    mskInvoiceDate.Select();
                                    return;
                                }
                                //-------------------------------------------
                                //FETCH THE ITEM WISE EMAIL TEXT
                                //-------------------------------------------
                                //FetchItemWiseEmailDetails(lngItemId);
                            }
                        }

                        // delete all records from TRN_INVOICE_TAX
                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_TAX WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // Loop as per individual Tax
                        for (int intRIndex = 1; intRIndex <= flxgrdTax.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngTaxId = Convert.ToInt64(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT));

                            if (lngTaxId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_TAX (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            TAX_ID," +
                                         "            TAX_RATE," +
                                         "            TAX_AMOUNT) " +
                                         "    VALUES( " + SearchId + "," +
                                         "            " + lngTaxId + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    mskInvoiceDate.Select();
                                    return;
                                }
                            }
                        }

                        // Transaction is commited
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.EditModeSave);

                        //-------------------------------------------------------------
                        if (ProcessToSendEmailSms(SearchId, lngItemId) == true)
                        {
                            //cmnService.J_UserMessage("Email and SMS Successfully Send " +
                            //                         "\n Invoice No :" + txtInvoiceNo.Text  +
                            //                         "\n Email Id :" + BS.BS_PartyEmailId +
                            //                         "\n Email :" + BS.BS_PartyMobNo); 
                        }
                        //-------------------------------------------------------------

                        // Mode
                        lblMode.Text = J_Mode.Add;
                        cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                        lblSearchMode.Text = J_Mode.General;

                        // Button behavior
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        BtnDelete.BackColor = Color.LightGray;
                        BtnExit.Enabled = true;
                        BtnExit.BackColor = Color.Lavender;

                        //-------------------------------------------------------------
                        // Initialize the Invoice Details into the variable
                        //-------------------------------------------------------------
                        BS.BS_SavedInvoiceDetails = txtInvoiceNo.Text + "^" +
                                                    BS.BS_OfflineSerialNo + "^" +
                                                    BS.BS_PartyEmailId + "^" +
                                                    BS.BS_PartyMobNo;
                        //-------------------------------------------------------------
                        this.ClearControls();

                        this.Cursor = Cursors.Default;
                        // enable controls
                        Enable(lblMode.Text);

                        // Print the Invoice
                        this.PrintInvoice(SearchId);

                        SearchId = 0;
                        cmbCompany.Select();
                        break;
                    #endregion
                    //
                    #region DELETE
                    case J_Mode.Delete:
                        //--
//                        strAcEntryDate = "";
//                        strBankDate = "";
//                        strSQL = "SELECT ACCOUNT_ENTRY_DATE, BANK_STATEMENT_DATE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId;
//                        reader = dmlService.J_ExecSqlReturnReader(strSQL);
//                        while (reader.Read())
//                        {
//                            strAcEntryDate = reader["ACCOUNT_ENTRY_DATE"].ToString();
//                            strBankDate = reader["BANK_STATEMENT_DATE"].ToString();
//                        }
//                        reader.Close();
//                        reader.Dispose();
//                        if (strAcEntryDate != "" || strBankDate != "")
//                        {
//                            cmnService.J_UserMessage("This invoice cannot be deleted as it has already been reconciled with the Bank/Accounts");
//                            return;
//                        }
//                        //-----------------------------------------------------------------------
//                        //-- TRN_DEDUCTEE_DETAILS
//                        //-----------------------------------------------------------------------
//                        strSQL = @"SELECT COUNT(*) 
//                                 FROM     TRN_COLLECTION_DETAIL 
//                                 WHERE    INVOICE_HEADER_ID = " + SearchId + " ";
//                        //--
//                        if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
//                        {
//                            cmnService.J_UserMessage("This invoice cannot be deleted as payment has already been received for this");
//                            return;
//                        }
//                        //--
//                        if (cmnService.J_UserMessage(J_Msg.AreYouSure2Delete,
//                            J_Var.J_pProjectName,
//                            MessageBoxButtons.YesNo,
//                            MessageBoxIcon.Question,
//                            MessageBoxDefaultButton.Button2) == DialogResult.No)
//                        {
//                            // Mode
//                            lblMode.Text = J_Mode.Edit;
//                            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
//                            lblSearchMode.Text = J_Mode.General;

//                            // Button behavior
//                            //BtnDelete.Enabled = true;
//                            //BtnDelete.BackColor = Color.Lavender;
//                            BtnExit.Enabled = true;
//                            BtnExit.BackColor = Color.Lavender;

//                            BtnCancel.Select();
//                            return;
//                        }
//                        // Transaction is started
//                        dmlService.J_BeginTransaction();
//                        // delete all records from TRN_INVOICE_TAX
//                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_TAX WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
//                        {
//                            mskInvoiceDate.Select();
//                            return;
//                        }

//                        // delete all records from TRN_INVOICE_DETAIL
//                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_DETAIL WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
//                        {
//                            mskInvoiceDate.Select();
//                            return;
//                        }

//                        // delete all records from TRN_INVOICE_HEADER
//                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
//                        {
//                            mskInvoiceDate.Select();
//                            return;
//                        }

//                        // Transaction is commited
//                        dmlService.J_Commit();
//                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.DeleteMode);

//                        // Mode
//                        lblMode.Text = J_Mode.Add;
//                        cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
//                        lblSearchMode.Text = J_Mode.General;

//                        // Button behavior
//                        BtnCancel.Enabled = false;
//                        BtnCancel.BackColor = Color.LightGray;
//                        BtnDelete.Enabled = false;
//                        BtnDelete.BackColor = Color.LightGray;
//                        BtnExit.Enabled = true;
//                        BtnExit.BackColor = Color.Lavender;

//                        // clear all controls
//                        this.ClearControls();
//                        SearchId = 0;

//                        // enable controls
//                        Enable(lblMode.Text);

//                        cmbCompany.Select();
                        break;
                    #endregion
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion


        #region CalculateNetAmount
        private void CalculateNetAmount()
        {
            int intQuantity                = 0;
            double dblRate                 = 0;
            double dblAmount               = 0;

            double dblTotalAmount          = 0;

            double dblDiscountRate         = cmnService.J_ReturnDoubleValue(txtDiscountRate.Text);
            double dblDiscountAmount       = cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text);
            double dblAmountWithDiscount   = 0;

            double dblTaxRate              = 0;
            double dblTaxAmount            = 0;
            double dblTotalTaxAmount       = 0;
            double dblAmountWithTax        = 0;

            double dblAdditionalCost       = cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text);
            double dblAmountWithAdditional = 0;

            double dblRoundedOff           = cmnService.J_ReturnDoubleValue(txtRoundedOff.Text);
            double dblNetAmount            = 0;
            
            if (flxgrdDetails.Rows > 0)
            {
                for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
                {
                    intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY));

                    dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE));

                    dblAmount = Convert.ToDouble(cmnService.J_FormatToString((intQuantity * dblRate), "0.00"));
                    flxgrdDetails.set_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT, string.Format("{0:0.00}", dblAmount));
                    
                    dblTotalAmount = dblTotalAmount + dblAmount;
                }
            }
            
            // Discount Enable
            DiscountEnable();

            // calculate discount amount
            if (txtDiscountRate.ReadOnly == false)
            {
                if (dblDiscountRate == 0) dblDiscountAmount = 0;
                else if (dblDiscountRate > 0) dblDiscountAmount = Convert.ToDouble(cmnService.J_FormatToString((dblTotalAmount * dblDiscountRate / 100), "0.00"));
            }
            dblAmountWithDiscount = dblTotalAmount - dblDiscountAmount;

            // calculate tax
            if (flxgrdTax.Rows > 0)
            {
                for (int intCounter = 1; intCounter <= flxgrdTax.Rows - 1; intCounter++)
                {
                    if (flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC).Trim() != "")
                    {
                        dblTaxRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE));
                        dblTaxAmount = Convert.ToDouble(cmnService.J_FormatToString((dblAmountWithDiscount * dblTaxRate / 100), "0.00"));

                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, string.Format("{0:0.00}", dblTaxAmount));

                        dblTotalTaxAmount = dblTotalTaxAmount + dblTaxAmount;
                    }
                    else
                    {
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_ID, "0");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC, "");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE, "");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, "");
                    }
                }
            }
            dblAmountWithTax = dblAmountWithDiscount + dblTotalTaxAmount;

            // calculate additional amount
            dblAmountWithAdditional = dblAmountWithTax + dblAdditionalCost;

            // calculate net amount
            dblNetAmount = dblAmountWithAdditional + dblRoundedOff;

            // Total Amount
            txtTotalAmount.Text = cmnService.J_FormatToString(dblTotalAmount, "0.00");
            
            // Discount
            if (txtDiscountRate.ReadOnly == false) 
                txtDiscountAmount.Text = cmnService.J_FormatToString(dblDiscountAmount, "0.00");
            
            txtAmountWithDiscount.Text = cmnService.J_FormatToString(dblAmountWithDiscount, "0.00");

            // Tax
            txtAmountWithTax.Text = cmnService.J_FormatToString(dblAmountWithTax, "0.00");
            txtTaxAmount.Text = cmnService.J_FormatToString(dblTotalTaxAmount, "0.00");
            
            // Additional
            txtAmountWithAdditionalCost.Text = cmnService.J_FormatToString(dblAmountWithAdditional, "0.00");

            // Net Amount
            txtNetAmount.Text = cmnService.J_FormatToString(dblNetAmount, "0.00");
            txtNetAmountInwords.Text = cmnService.J_Inwords(dblNetAmount);

        }
        #endregion 

        
        #region setDetailsGridRefresh_TAX
        private void setDetailsGridRefresh_TAX(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, 5);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < 5; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;
        }
        #endregion

        #region setDetailsGridColumns_TAX
        private void setDetailsGridColumns_TAX(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // TAX_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_ID, 0, 0);

            // TAX_DESC
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_DESC;
            HFlexGrid.Text = "Tax Description";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_DESC, 0, 3000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_DESC, (short)J_Alignment.LeftCentre);

            // RATE
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_RATE;
            HFlexGrid.Text = "Rate";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_RATE, 0, 900);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_RATE, (short)J_Alignment.RightCentre);

            // AMOUNT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_AMOUNT;
            HFlexGrid.Text = "Amount";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_AMOUNT, 0, 1200);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_AMOUNT, (short)J_Alignment.RightCentre);

        }
        #endregion

        #region Enable
        private void Enable(string Mode)
        {
            if (Mode == J_Mode.Add)
            {
                cmbCompany.Enabled = true;
                cmbCompany.BackColor = Color.White;
                cmbCompany.ForeColor = Color.Black;

                cmbInvoiceSeries.Enabled = true;
                cmbInvoiceSeries.BackColor = Color.White;
                cmbInvoiceSeries.ForeColor = Color.Black;

                //mskPaymentDate.Enabled = true;
                //cmbPaymentType.Enabled = true;
            }
            else if (Mode == J_Mode.Edit)
            {
                cmbCompany.Enabled = false;
                cmbCompany.BackColor = Color.LightYellow;
                cmbCompany.ForeColor = Color.Blue;

                cmbInvoiceSeries.Enabled = false;
                cmbInvoiceSeries.BackColor = Color.LightYellow;
                cmbInvoiceSeries.ForeColor = Color.Blue;

                //mskPaymentDate.Enabled= false;
                //cmbPaymentType.Enabled= false;
            }
        }
        #endregion

        #region DiscountEnable
        private void DiscountEnable()
        {
            txtDiscountRate.ReadOnly = false;
            txtDiscountRate.BackColor = Color.White;
            txtDiscountRate.ForeColor = Color.Black;

            txtDiscountAmount.ReadOnly = false;
            txtDiscountAmount.BackColor = Color.White;
            txtDiscountAmount.ForeColor = Color.Black;
            
            if(cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) > 0)
            {
                txtDiscountAmount.ReadOnly = true;
                txtDiscountAmount.BackColor = Color.LightYellow;
                txtDiscountAmount.ForeColor = Color.Black;
            }
            else if(cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) > 0)
            {
                txtDiscountRate.ReadOnly = true;
                txtDiscountRate.BackColor = Color.LightYellow;
                txtDiscountRate.ForeColor = Color.Black;
            }
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long InvoiceHeaderId)
        {
            // declaration of variables
            IDataReader reader = null;

            string strInvoiceSeries  = "";
            string strInvoiceNo      = "";
            double dblDiscountRate   = 0;
            double dblDiscountAmount = 0;
            double dblAdditionalCost = 0;
            double dblRoundedOff     = 0;

            string strParty = "";

            try
            {
                strSQL = "SELECT MST_INVOICE_SERIES.PREFIX                  AS PREFIX," +
                         "       TRN_INVOICE_HEADER.INVOICE_NO              AS INVOICE_NO," +
                         "       " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE," +
                         "       TRN_INVOICE_HEADER.CHALLAN_REF_NO          AS CHALLAN_REF_NO," +
                         "       TRN_INVOICE_HEADER.ORDER_NO                AS ORDER_NO," +
                         "       MST_PARTY.PARTY_NAME                       AS PARTY_NAME," +
                         "       TRN_INVOICE_HEADER.REMARKS                 AS REMARKS," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_TEXT           AS DISCOUNT_TEXT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_RATE           AS DISCOUNT_RATE," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_AMOUNT         AS DISCOUNT_AMOUNT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST_TEXT    AS ADDITIONAL_COST_TEXT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST         AS ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.ROUNDED_OFF             AS ROUNDED_OFF, " +
                         "       TRN_INVOICE_HEADER.PAYMENT_TYPE_ID         AS PAYMENT_TYPE_ID, " +
                         "       MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION  AS PAYMENT_TYPE_DESCRIPTION, " +
                         "       MST_PAYMENT_TYPE.SUNDRY_PARTY_FLAG         AS SUNDRY_PARTY_FLAG, " +
                         "       MST_PAYMENT_TYPE.AUTO_COLLECTION_POST_FLAG AS AUTO_COLLECTION_POST_FLAG, " +
                         "       TRN_INVOICE_HEADER.BANK_ID                 AS BANK_ID, " +
                         "       MST_BANK.BANK_NAME                         AS BANK_NAME, " +
                         "       TRN_INVOICE_HEADER.REFERENCE_NO            AS REFERENCE_NO, " +
                         "       TRN_INVOICE_HEADER.ONLINE_ORDER_NO         AS ONLINE_ORDER_NO, " +
                         "       PAR_DELIVERY_MODE.DELIVERY_MODE_DESC       AS DELIVERY_MODE_DESC, " +
                         "       MST_OFFLINE_SERIAL.OFFLINE_SERIAL_CODE     AS OFFLINE_SERIAL_CODE, " +
                         "       MST_OFFLINE_SERIAL.OFFLINE_CODE            AS OFFLINE_CODE, " +
                         "       " + cmnService.J_SQLDBFormat("TRN_COLLECTION_HEADER.COLLECTION_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS COLLECTION_DATE " +
                         "FROM   ((((((((TRN_INVOICE_HEADER " +
                         "INNER JOIN MST_INVOICE_SERIES " +
                         "        ON TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID) " +
                         "INNER JOIN MST_PARTY " +
                         "        ON TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID) " +
                         "LEFT  JOIN MST_PAYMENT_TYPE " +
                         "        ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID   = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID) " +
                         "LEFT  JOIN MST_BANK " +
                         "        ON TRN_INVOICE_HEADER.BANK_ID           = MST_BANK.BANK_ID) " +
                         "LEFT  JOIN MST_OFFLINE_SERIAL " +
                         "        ON TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID = MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID) " +
                         "INNER JOIN PAR_DELIVERY_MODE"+
                         "        ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID  = PAR_DELIVERY_MODE.DELIVERY_MODE_ID) " +
                         "LEFT  JOIN TRN_COLLECTION_DETAIL " +
                         "        ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID) " +
                         "LEFT  JOIN TRN_COLLECTION_HEADER " +
                         "        ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID) " +
                         "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";

                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                if (reader == null) return false;

                while (reader.Read())
                {
                    //--------------------------------------------------------------------
                    strInvoiceSeries           = Convert.ToString(reader["PREFIX"]);
                    strInvoiceNo               = Convert.ToString(reader["INVOICE_NO"]);

                    mskInvoiceDate.Text        = Convert.ToString(reader["INVOICE_DATE"]);
                    txtChallanRefNo.Text       = Convert.ToString(reader["CHALLAN_REF_NO"]);
                    txtOrderNo.Text            = Convert.ToString(reader["ORDER_NO"]);
                    strParty                   = Convert.ToString(reader["PARTY_NAME"]);
                    txtRemarks.Text            = Convert.ToString(reader["REMARKS"]);

                    txtDiscountText.Text       = Convert.ToString(reader["DISCOUNT_TEXT"]);
                    dblDiscountRate            = Convert.ToDouble(Convert.ToString(reader["DISCOUNT_RATE"]));
                    dblDiscountAmount          = Convert.ToDouble(Convert.ToString(reader["DISCOUNT_AMOUNT"]));
                    txtAdditionalCostText.Text = Convert.ToString(reader["ADDITIONAL_COST_TEXT"]);
                    dblAdditionalCost          = Convert.ToDouble(Convert.ToString(reader["ADDITIONAL_COST"]));
                    dblRoundedOff              = Convert.ToDouble(Convert.ToString(reader["ROUNDED_OFF"]));

                    blnPaymentType = false;
                    BS.T_Selected_PaymentType = Convert.ToString(reader["PAYMENT_TYPE_DESCRIPTION"]);
                    //cmbPaymentType.Text = Convert.ToString(reader["PAYMENT_TYPE_DESCRIPTION"]);
                    

                    lblSundryPartyFlag.Text = Convert.ToString(reader["SUNDRY_PARTY_FLAG"]);
                    lblAutoCollectionPostFlag.Text = Convert.ToString(reader["AUTO_COLLECTION_POST_FLAG"]);
                    blnPaymentType = true;

                    
                    cmbBank.Text = Convert.ToString(reader["BANK_NAME"]);

                    BS.T_Selected_Bank = Convert.ToString(reader["BANK_NAME"]);
                    //cmbOnlineBank.Text = Convert.ToString(reader["BANK_NAME"]);
                    //--
                    //txtReference.Text          = Convert.ToString(reader["REFERENCE_NO"]);

                    blnPaymentType = false;

                    BS.T_Selected_BillType = Convert.ToString(reader["DELIVERY_MODE_DESC"]);
                    //cmbBillingMode.Text = Convert.ToString(reader["DELIVERY_MODE_DESC"]);

                    
                    cmbBillingMode.Enabled = false;
                    txtOnlineOrderNo.Text = Convert.ToString(reader["ONLINE_ORDER_NO"]);
                    txtOnlineOrderNo.Enabled = false;
                    blnPaymentType = true;
                    //
                    if (Convert.ToString(reader["OFFLINE_CODE"]) != "")
                    {
                        lblAllotedSerialNo.Visible = true;
                        lblAllotedSerialNo.Text = "Alloted Code : " + Convert.ToString(reader["OFFLINE_CODE"]);
                    }
                    else
                        lblAllotedSerialNo.Visible = false;
                    //
                    txtReference.Text = Convert.ToString(reader["REFERENCE_NO"]);
                    //
                    mskPaymentDate.Text = Convert.ToString(reader["COLLECTION_DATE"]);
   
                }

                // data reader object is closed
                reader.Dispose();

                blnPaymentType = false;
                cmbBillingMode.Text = BS.T_Selected_BillType;
                cmbPaymentType.Text = BS.T_Selected_PaymentType;
                blnPaymentType = true;
                cmbOnlineBank.Text = BS.T_Selected_Bank;


                cmbParty.Text          = strParty;
                cmbInvoiceSeries.Text  = strInvoiceSeries;
                txtInvoiceNo.Text      = strInvoiceNo;
                txtInvoiceNo.Visible = true; lblInvoiceNo.Visible = true;
                txtDiscountRate.Text   = string.Format("{0:0.00}", dblDiscountRate);
                txtDiscountAmount.Text = string.Format("{0:0.00}", dblDiscountAmount);
                txtAdditionalCost.Text = string.Format("{0:0.00}", dblAdditionalCost);
                txtRoundedOff.Text     = string.Format("{0:0.00}", dblRoundedOff);

                // DISPLAY ITEM DETAIL GRID
                strSQL = "SELECT TRN_INVOICE_DETAIL.INVOICE_DETAIL_ID AS INVOICE_DETAIL_ID," +
                         "       ''                                   AS SL_NO," +
                         "       MST_ITEM.ITEM_NAME                   AS ITEM_NAME," +
                         "       TRN_INVOICE_DETAIL.QUANTITY          AS QUANTITY," +
                         "       MST_ITEM.UNIT                        AS UNIT," +
                         "       TRN_INVOICE_DETAIL.RATE              AS RATE," +
                         "       0                                    AS AMOUNT," +
                         "       TRN_INVOICE_DETAIL.REMARKS           AS REMARKS," +
                         "       TRN_INVOICE_DETAIL.ITEM_ID           AS ITEM_ID " +
                         "FROM   TRN_INVOICE_DETAIL," +
                         "       MST_ITEM " +
                         "WHERE  TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID " +
                         "AND    TRN_INVOICE_DETAIL.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";

                // declare & initialize the dml object
                DMLService dml = new DMLService();

                ADODB._Recordset rs = dml.J_ExecSqlReturnADODBRecordset(strSQL);
                if (rs == null) return false;

                this.setDetailsGridRefresh(flxgrdDetails);
                if (rs.RecordCount > 0)
                {
                    // Clear the Flexgrid data
                    flxgrdDetails.Clear();
                    flxgrdDetails.DataSource = (msdatasrc.DataSource)rs;
                }
                setDetailsGridColumns(flxgrdDetails);
                rs.Close();

                // DISPLAY TAX DETAIL GRID
                strSQL = "SELECT MST_TAX.TAX_ID             AS TAX_ID," +
                         "       MST_TAX.TAX_DESC           AS TAX_DESC," +
                         "       TRN_INVOICE_TAX.TAX_RATE   AS TAX_RATE," +
                         "       TRN_INVOICE_TAX.TAX_AMOUNT AS TAX_AMOUNT " +
                         "FROM   TRN_INVOICE_TAX," +
                         "       MST_TAX " +
                         "WHERE  TRN_INVOICE_TAX.TAX_ID            = MST_TAX.TAX_ID " +
                         "AND    TRN_INVOICE_TAX.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";

                ADODB._Recordset rsTax = dml.J_ExecSqlReturnADODBRecordset(strSQL);
                if (rsTax == null) return false;

                this.setDetailsGridRefresh_TAX(flxgrdTax);
                if (rsTax.RecordCount > 0)
                {
                    // Clear the Flexgrid data
                    flxgrdTax.Clear();
                    flxgrdTax.DataSource = (msdatasrc.DataSource)rsTax;
                }
                setDetailsGridColumns_TAX(flxgrdTax);
                rsTax.Close();

                // dml object is disposed
                dml.Dispose();

                // calculate amount
                CalculateNetAmount();

                //--------------------------------------------------------------------------
                //ADDED BY DHRUB On 21/05/2015 TO CHECK THE INVOICE IS EDITABLE OR NOT 
                //--------------------------------------------------------------------------
                strSQL = @" SELECT COUNT(TRN_COLLECTION_HEADER.RECONCILIATION_DATE) AS NOOF_RECONCILE
                            FROM   TRN_COLLECTION_DETAIL
                                   INNER JOIN TRN_COLLECTION_HEADER
                                   ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID    
                            WHERE  TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS NOT NULL
                            AND    TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = " + InvoiceHeaderId;

                if (Convert.ToDouble(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    blnInvoiceWiseReconciled = true;
                else
                    blnInvoiceWiseReconciled = false;


                return true;
            }
            catch
            {
                reader.Dispose();
                return false;
            }
        }
        #endregion

        #region Set_SLNO
        private void Set_SLNO()
        {
            int intSLNO = 0;
            for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
                flxgrdDetails.set_TextMatrix(intCounter, (int)enmInvoiceEntry.SL_NO, Convert.ToString(++intSLNO));
        }
        #endregion

        #region PrintInvoice
        private bool PrintInvoice(long lngInvoiceHeaderId)
        {
            //if (cmnService.J_UserMessage(J_Msg.WantToPrint,
            //        J_Var.J_pProjectName,
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button2) == DialogResult.No)
            //{
            //    return false;
            //}

            TrnInvoicePrint frmInvoicePrint = new TrnInvoicePrint(lngInvoiceHeaderId, BS.BS_SavedInvoiceDetails);
            frmInvoicePrint.Text = "Invoice Print";
            frmInvoicePrint.ShowDialog();

            return true;
        }
        #endregion

        #region GetInvoiceNo
        private void GetInvoiceNo()
        {
            txtInvoiceNo.Text = "";
            if (cmbInvoiceSeries.SelectedIndex <= 0) return;

            strSQL = "SELECT LAST_NO " +
                     "FROM   MST_INVOICE_SERIES " +
                     "WHERE  BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                     "AND    INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "";
            long lngLastNo = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL));
            txtInvoiceNo.Text = cmbInvoiceSeries.Text + "/" + cmnService.J_FormatToString(lngLastNo, "00000");
        }
        #endregion


        #region GenerateInvoicePDF
        public bool GenerateInvoicePDF(long InvoiceHeaderId)
        {
            try
            {
                // report file object
                ReportClass rptcls;
                //---------------------------------------------
                crInvoice rptInvoice = new crInvoice();
                rptcls = (ReportClass)rptInvoice;
                //---------------------------------------------
                strSQL = "SELECT 1                                              AS INVOICE_TYPE_ID," +
                         "       '(Original - Buyer''s Copy)'                   AS INVOICE_TYPE," +
                         "       0                                              AS PRINT_TYPE," +
                         "       TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID," +
                         "       MST_INVOICE_SERIES.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID," +
                         "       MST_INVOICE_SERIES.HEADER_DISPLAY_TEXT         AS HEADER_DISPLAY_TEXT," +
                         "       MST_PARTY.PARTY_NAME                           AS PARTY_NAME," +
                         "       MST_PARTY.ADDRESS1                             AS ADDRESS1," +
                         "       MST_PARTY.ADDRESS2                             AS ADDRESS2," +
                         "       MST_PARTY.ADDRESS3                             AS ADDRESS3," +
                         "       MST_PARTY.CITY                                 AS CITY," +
                         "       MST_PARTY.PIN                                  AS PIN," +
                         "       MST_PARTY.MOBILE_NO                            AS MOBILE_NO," +
                         "       MST_PARTY.PHONE_NO                             AS PHONE_NO," +
                         "       MST_PARTY.FAX                                  AS FAX," +
                         "       MST_PARTY.CONTACT_PERSON                       AS CONTACT_PERSON," +
                         "       TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO," +
                         "       TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE," +
                         "       TRN_INVOICE_HEADER.CHALLAN_REF_NO              AS CHALLAN_REF_NO," +
                         "       TRN_INVOICE_HEADER.ORDER_NO                    AS ORDER_NO," +
                         "       TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT," +
                         "       TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST_TEXT        AS ADDITIONAL_COST_TEXT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF," +
                         "       TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT," +
                         "       TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS," +
                         "       MST_ITEM.ITEM_ID                               AS ITEM_ID," +
                         "       MST_ITEM.ITEM_NAME                             AS ITEM_NAME," +
                         "       TRN_INVOICE_DETAIL.QUANTITY                    AS QUANTITY," +
                         "       MST_ITEM.UNIT                                  AS UNIT," +
                         "       TRN_INVOICE_DETAIL.RATE                        AS RATE," +
                         "       TRN_INVOICE_DETAIL.AMOUNT                      AS AMOUNT," +
                         "       TRN_INVOICE_DETAIL.REMARKS                     AS REMARKS," +
                         "       MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
                         "       MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
                         "       MST_COMPANY.ADDRESS1                           AS COMPANY_ADDRESS1," +
                         "       MST_COMPANY.ADDRESS2                           AS COMPANY_ADDRESS2," +
                         "       MST_COMPANY.ADDRESS3                           AS COMPANY_ADDRESS3," +
                         "       MST_COMPANY.PIN                                AS COMPANY_PIN," +
                         "       MST_COMPANY.CONTACT_NO                         AS COMPANY_CONTACT_NO," +
                         "       MST_COMPANY.FAX                                AS COMPANY_FAX," +
                         "       MST_COMPANY.EMAIL_ID                           AS COMPANY_EMAIL_ID," +
                         "       MST_COMPANY.WEB_SITE                           AS COMPANY_WEB_SITE," +
                         "       MST_COMPANY.VAT_NO                             AS VAT_NO," +
                         "       MST_COMPANY.CST_NO                             AS CST_NO," +
                         "       MST_COMPANY.SERVICE_TAX_NO                     AS SERVICE_TAX_NO," +
                         "       MST_COMPANY.PAN                                AS PAN, " +
                         "       MST_COMPANY.CIN_NO                             AS CIN_NO," +
                         "       TRN_INVOICE_HEADER.DELIVERY_MARK               AS DELIVERY_MARK," +
                         "       MST_COMPANY.BANK_DETAIL1                       AS BANK_DETAIL1," + //-- 2016/02/18 ANIK
                         "       MST_COMPANY.BANK_DETAIL2                       AS BANK_DETAIL2," +
                         "       MST_COMPANY.BANK_DETAIL3                       AS BANK_DETAIL3," +
                         "       MST_COMPANY.BANK_DETAIL4                       AS BANK_DETAIL4," +
                         "       MST_COMPANY.BANK_DETAIL5                       AS BANK_DETAIL5 " +
                         "FROM   TRN_INVOICE_HEADER," +
                         "       TRN_INVOICE_DETAIL," +
                         "       MST_INVOICE_SERIES," +
                         "       MST_COMPANY," +
                         "       MST_PARTY," +
                         "       MST_ITEM " +
                         "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID  = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                         "AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID  = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                         "AND    TRN_INVOICE_HEADER.COMPANY_ID         = MST_COMPANY.COMPANY_ID " +
                         "AND    TRN_INVOICE_HEADER.PARTY_ID           = MST_PARTY.PARTY_ID " +
                         "AND    TRN_INVOICE_DETAIL.ITEM_ID            = MST_ITEM.ITEM_ID " +
                         "AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID IN(" + InvoiceHeaderId + ") " +
                         "AND    TRN_INVOICE_HEADER.BRANCH_ID          = " + J_Var.J_pBranchId + " " +
                         "ORDER BY INVOICE_TYPE_ID," +
                         "         TRN_INVOICE_HEADER.INVOICE_NO";

                // SUB REPORTS
                // FOR SUMMARY OF TAX DETAILS
                string strSubRptTaxDetails = " SELECT MST_TAX.TAX_ID                    AS TAX_ID," +
                                             "        MST_TAX.TAX_DESC                  AS TAX_DESC," +
                                             "        TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                                             "        TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE," +
                                             "        TRN_INVOICE_TAX.TAX_AMOUNT        AS TAX_AMOUNT " +
                                             " FROM   MST_TAX," +
                                             "        TRN_INVOICE_TAX " +
                                             " WHERE  MST_TAX.TAX_ID    = TRN_INVOICE_TAX.TAX_ID " +
                                             " AND    MST_TAX.BRANCH_ID = " + J_Var.J_pBranchId + " " +
                                             " ORDER BY TRN_INVOICE_TAX.INVOICE_TAX_ID";
                // POPULATE & DISPLAY SUB REPORT
                rptcls.OpenSubreport("crSubRptTaxSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSubRptTaxDetails).Tables[0]);

                // report is executed
                DataSet ds = dmlService.J_ExecSqlReturnDataSet(strSQL);
                if (ds == null) return false;

                PictureObject objBlobFieldObject;
                objBlobFieldObject = (PictureObject)rptcls.ReportDefinition.Sections[2].ReportObjects["imgSignature"];
                objBlobFieldObject.ObjectFormat.EnableSuppress = true;

                objBlobFieldObject.ObjectFormat.EnableSuppress = false;

                //GENERATE PDF FILE 
                if (J_ExportReport(ref rptcls, strSQL, "", "", "", BS_ExportReportFormat.PortableDocFormat, Convert.ToString(txtInvoiceNo.Text.Replace("/", "-").Trim()), Application.StartupPath + "\\" + strGenerateInvoiceFolderName) == false)
                    return false;

                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }

        }
        #endregion

        #region FetchPartyDetails
        public void FetchPartyDetails(long lngPartyId)
        {
            IDataReader reader = null;
            try
            {
               
                strSQL = @" SELECT PARTY_ID,
                                   PARTY_NAME,
                                   ADDRESS1,
                                   ADDRESS2,
                                   ADDRESS3,
                                   CITY,
                                   PIN,
                                   CONTACT_PERSON,
                                   MOBILE_NO,
                                   PHONE_NO,
                                   FAX,
                                   EMAIL_ID,
                                   PARTY_CATEGORY_ID
                             FROM  MST_PARTY
                             WHERE PARTY_ID = " + lngPartyId;
                //---------------------------------------------
                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                //---------------------------------------------
                if (reader == null) return;

                while (reader.Read())
                {
                    strPartyName = Convert.ToString(reader["PARTY_NAME"]);
                    strPartyAddress1 = Convert.ToString(reader["ADDRESS1"]);
                    strPartyAddress2 = Convert.ToString(reader["ADDRESS2"]);
                    strPartyAddress3 = Convert.ToString(reader["ADDRESS3"]);
                    strPartyCity = Convert.ToString(reader["CITY"]);
                    strPartyPin = Convert.ToString(reader["PIN"]);
                    strPartyContactPerson = Convert.ToString(reader["CONTACT_PERSON"]);
                    strPartyMobileNumber = Convert.ToString(reader["MOBILE_NO"]);
                    strPartyPhoneNumber = Convert.ToString(reader["PHONE_NO"]);
                    strPartyFax = Convert.ToString(reader["FAX"]);
                    strPartyEmailId = Convert.ToString(reader["EMAIL_ID"]);
                    // data reader object is closed
                    reader.Dispose();
                    return;
                }

                // data reader object is closed
                reader.Dispose();
            }
            catch(Exception Err)
            {
                // data reader object is closed
                reader.Dispose();
                this.Cursor = Cursors.Default;                        
                cmnService.J_UserMessage(Err.Message);
                return;
            }
        }
        #endregion 

        #region EXPORT TO PDF [OVERLOADED METHOD]

        #region J_ExportReport [1]
        public bool J_ExportReport(ref ReportClass reportClass, string SqlText, string CompanyName, string CompanyAddress, string ReportTitle, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return J_ExportReport(ref reportClass, dsDataPrint, CompanyName, CompanyAddress, ReportTitle, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [2]
        public bool J_ExportReport(ref ReportClass reportClass, DataSet dataset, string CompanyName, string CompanyAddress, string ReportTitle, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            string strFilePath = ExportFilePath + "\\";
            string strFileWithPath = "";
            if (dataset == null) return false;


            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);

                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                // check company name exist
                if (CompanyName == "" || CompanyName == null)
                    CompanyName = "";
                reportClass.SetParameterValue("txtCompanyName", CompanyName);

                // check company address exist
                if (CompanyAddress == "" || CompanyAddress == null)
                    CompanyAddress = "";
                reportClass.SetParameterValue("txtBranch", CompanyAddress);

                // check report title exist
                if (ReportTitle == "" || ReportTitle == null)
                    ReportTitle = "";
                reportClass.SetParameterValue("txtReportTitle", ReportTitle);

                frm.Text = "Report : " + ReportTitle;

                strFileWithPath = Path.Combine(strFilePath, ExportFileName) + "." + BS_ExportReportFormat.PortableDocFormat;
                reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                switch (ExportReportFormat)
                {
                    case BS_ExportReportFormat.PortableDocFormat:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.PortableDocFormat;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                        break;
                    case BS_ExportReportFormat.Excel:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Excel;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strFileWithPath);
                        break;
                    case BS_ExportReportFormat.Word:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Word;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strFileWithPath);
                        break;
                }

                //if (enmPrintType == J_PrintType.Direct)
                //    frm.CRViewer.PrintReport();
                //else if (enmPrintType == J_PrintType.Preview)
                //{
                //    frm.CRViewer.Refresh();
                //    frm.MdiParent = ReportDialog.MdiParent;
                //    frm.Show();
                //}
                return true;
            }
            else
            {
                //commonService.J_UserMessage("Record not found.\nPreview not available");
                cmnService.J_UserMessage("Please make sure you entered a valid file path", MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #endregion

        #region ConvertTextTOHtml(string)
        public string ConvertTextTOHtml(string Source, bool allow)
        {

            //Create a StringBuilder object from the string intput
            //parameter
            StringBuilder sb = new StringBuilder(Source);

            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace(" ", " &nbsp;");


            //Check if HTML tags are not allowed
            if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            }


            //Create a StringReader from the processed string of 
            //the StringBuilder
            StringReader sr = new StringReader(sb.ToString());
            StringWriter sw = new StringWriter();

            //Loop while next character exists
            while (sr.Peek() > -1)
            {
                //Read a line from the string and store it to a temp
                //variable
                string temp = sr.ReadLine();
                //write the string with the HTML break tag
                //Note here write method writes to a Internal StringBuilder
                //object created automatically
                sw.Write(temp + "<br>");
            }
            //Return the final processed text
            return sw.GetStringBuilder().ToString();

        }
        #endregion

        #region Convert_HtmlToText
        private string Convert_HtmlToText(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                MessageBox.Show("Error");
                return source;
            }
        }
        #endregion 

        #region SendEMail
        private bool SendEMail(string EmailIdTo, 
                               string EmaiIdBcc, 
                               string EmailFrom,
                               string DisplayName,
                               string DownloadLink1,
                               string DownloadLink2,
                               string ContactPerson,
                               string InvoiceNo,
                               string OrderNO,
                               string ProductName,
                               string PDFFileName,
                               string SerialNo)
        {
            if (BillingSystem.T_CheckInternetConnectivty() == false)
            {
                cmnService.J_UserMessage("Please Check the Internet Connectivity"); 
                return false;
            }
            //
            //----------------------------------
            //ENCODING THE TEXT TO HTML FORMAT
            //----------------------------------
            //strHTMLText = ConvertTextTOHtml("", true);
            strHTMLText = strEmailBody;
            //
            string strToEmail = "";
            string strWebSite = "";
            string strEmail = "";
            string strDisplayName = "";
            string strImageURL = "";
            string strContactPerson = "";
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
                if (EmaiIdBcc.EndsWith(",") == true)
                    EmaiIdBcc = EmaiIdBcc.Substring(0, EmaiIdBcc.Length - 1);
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
                strEmail = EmailFrom;
                //strWebSite = ProductWebsite;
                strDisplayName = DisplayName;
                //strImageURL = ProductImageUrl;
                //
                if (ContactPerson.Trim() == "")
                    strContactPerson = "Sir";
                else
                    strContactPerson = BillingSystem.ToTitleCase(ContactPerson.Trim());
                //
                strHTMLText = strHTMLText.Replace("CUST_NAME", strContactPerson);
                strHTMLText = strHTMLText.Replace("ORDER_NO", OrderNO);
                //
                strHTMLText = strHTMLText.Replace("PRODUCT_NAME", ProductName);
                strHTMLText = strHTMLText.Replace("DWNLD_LINK", DownloadLink1);
                //
                if (cmbBillingMode.Text.ToUpper() == "OFFLINE DELIVERY")
                {
//                    strSQL = @"SELECT MST_OFFLINE_SERIAL.OFFLINE_SERIAL_CODE 
//                               FROM   MST_OFFLINE_SERIAL, 
//                                      TRN_INVOICE_HEADER 
//                               WHERE  MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID
//                               AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + InvoiceHeaderId;
//                    string SerialNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    strHTMLText = strHTMLText.Replace("SERIAL_NO", SerialNo);
                }
                else
                    strEmailSubject = strEmailSubject + " " + OrderNO;
                //
                mail.From = new MailAddress(strEmail, strDisplayName, System.Text.Encoding.UTF8);
                mail.Priority = MailPriority.High;
                mail.ReplyTo = new MailAddress(strEmail);
                //
                if (EmaiIdBcc.Trim() != "")
                {
                    //mail.Bcc.Add("mukherjee.dhrub@gmail.com");
                    mail.Bcc.Add(EmaiIdBcc);
                }
                //
                mail.To.Add(EmailIdTo);
                //-- BILL ATTACHMENT
                FileStream fs = new FileStream(strPdfPath, FileMode.Open, FileAccess.Read);
                Attachment a = new Attachment(fs, InvoiceNo.Replace("/", "-").Trim() + ".PDF", MediaTypeNames.Application.Octet);
                mail.Attachments.Add(a);
                //--
                if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                {
                    //-- PDF ATTACHMENT
                    PDFFileName = Path.Combine(Application.StartupPath, PDFFileName);
                    if (File.Exists(PDFFileName) == true)
                    {
                        FileStream fs1 = new FileStream(PDFFileName, FileMode.Open, FileAccess.Read);
                        Attachment a1 = new Attachment(fs1, Path.GetFileName(PDFFileName), MediaTypeNames.Application.Octet);
                        mail.Attachments.Add(a1);
                    }
                }
                //
                mail.Subject = strEmailSubject;
                StringBuilder htmlString = new StringBuilder();
                // 
                #region EMAIL BODY
                //
                htmlString.Append(strHTMLText);
                //
                #endregion
                //
                mail.Body = htmlString.ToString();
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Timeout = 300000;//(5min) 
                SmtpServer.Send(mail);
                //
                return true;
            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Failed to send the Email due to Error Occured :" + err.Message);
                //this.Cursor = Cursors.Default;
                return false;
            }
        }
        #endregion

        #region SendSMS
        private bool SendSMS(string strMobileNo, string strEmailID, string InvoiceNo, string SerialNo)
        {
            string strMessage = "";
            try
            {
                if (BillingSystem.T_CheckInternetConnectivty() == false)
                {
                    cmnService.J_UserMessage("Please Check the Internet Connectivity");
                    return false;
                }
                //
                if (strMobileNo.Length < 10)
                    return false;
                //--
                if (strMobileNo.Substring(0, 1) != "9")
                {
                    if (strMobileNo.Substring(0, 1) != "8")
                    {
                        if (strMobileNo.Substring(0, 1) != "7")
                        {
                            return false;
                        }
                    }
                }
                //--   
                
                if (cmbBillingMode.Text.ToUpper() == "OFFLINE DELIVERY")
                {
//                    strSQL = @"SELECT MST_OFFLINE_SERIAL.OFFLINE_SERIAL_CODE 
//                               FROM   MST_OFFLINE_SERIAL, 
//                                      TRN_INVOICE_HEADER 
//                               WHERE  MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID
//                               AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + InvoiceHeaderId;
//                    string SerialNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                    strHTMLText = strHTMLText.Replace("SERIAL_NO", SerialNo);
                    //
                    //strMessage = "Use Serial Number " + SerialNo + " to register TDSMAN software.Invoice and download link has been emailed to you. Help: (033)22623535 or email:info@tdsman.com";
                    //--2015-03-03
                    //------------------------------------------------------------
                    //------------------------------------------------------------
                    //BLOCKED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                    //------------------------------------------------------------
                    //------------------------------------------------------------
                    //strMessage = "Use Serial Number " + SerialNo + " to register TDSMAN. Invoice and download link has been emailed to " + strEmailID + ". Helpline: (033)22623535 or email:info@tdsman.com";
                    //ADDED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                    strMessage = J_Var.J_pOfflineInvoiceSMS_Text.Replace("SERIAL_NO", SerialNo).Replace("EMAIL_ID", strEmailID);  
                }
                else
                    //------------------------------------------------------------
                    //------------------------------------------------------------
                    //BLOCKED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                    //------------------------------------------------------------
                    //------------------------------------------------------------
                    //strMessage = "Invoice against order No. " + InvoiceNo + " has been emailed to " + strEmailID + ". We indeed value your business. Helpline: (033)22623535 or email:info@tdsman.com";
                    //ADDED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                    strMessage = J_Var.J_pOnlineInvoiceSMS_Text.Replace("INVOICE_NO", SerialNo).Replace("EMAIL_ID", strEmailID);
                //--
                //--
                //------------------------------------------------------------
                //------------------------------------------------------------
                //BLOCKED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                //------------------------------------------------------------
                //------------------------------------------------------------
                //string strUrl = "http://dndopen.loyalsmsindia.co.in/api/web2sms.php?workingkey=8782u0m235vlxh8rvk4" +
                //                "&to=" + strMobileNo +
                //                "&sender=TDSMAN" +
                //                "&message=" + strMessage;
                //------------------------------------------------------------
                //------------------------------------------------------------
                //ADDED BY DHRUB ON 10/06/2015 TO MAKE IT DATABASE DRIVEN
                //------------------------------------------------------------
                string strUrl = J_Var.J_pSMS_WorkingKey +
                                "&to=" + strMobileNo +
                                "&sender=" + J_Var.J_pSMS_SenderName + 
                                "&message=" + strMessage;

                //
                //string strUrl = strWorkingKey +
                //                "&to=" + strMobileNo +
                //                "&sender=" + strDisplayName +
                //                strSMSText;
                // request to server with url
                HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(strUrl);
                //get response from server & assign to stream variables to fetch server response
                HttpWebResponse res = (HttpWebResponse)Req.GetResponse();
                //--
                Stream sr = res.GetResponseStream();
                StreamReader readStream = new StreamReader(sr);
                string strGetRes = readStream.ReadToEnd();
                readStream.Close();
                res.Close();
                sr.Close();
                //--
                string[] strArr = strGetRes.Split(' ');
                string strMessageID = "";
                foreach (string strID in strArr)
                {
                    if (strID.Contains("ID"))
                        strMessageID = strID;
                }
                //--
                if (string.IsNullOrEmpty(strMessageID))
                {
                    cmnService.J_UserMessage("Message Send Failed");
                    return false;
                }
                return true;
                //--
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage("Failed to send the Email due to Error Occured " + err.Message);
                return false;
            }
        }
        #endregion

        #region FetchItemWiseEmailDetails
        public bool FetchItemWiseEmailDetails(long SearchId)
        {
            IDataReader reader = null;
            try
            {
                strSQL = @" SELECT MST_EMAIL_DETAIL.EMAIL         AS EMAIL,
                                   MST_EMAIL_DETAIL.WEBSITE       AS WEBSITE,
                                   MST_EMAIL_DETAIL.DISPLAYNAME   AS DISPLAYNAME,
                                   MST_EMAIL_DETAIL.IMAGEURL      AS IMAGEURL,
                                   MST_EMAIL_DETAIL.EMAIL_TO_BCC  AS EMAIL_TO_BCC
                            FROM   MST_ITEM
                                   LEFT JOIN MST_EMAIL_DETAIL
                                   ON MST_ITEM.EMAIL_DETAIL_ID = MST_EMAIL_DETAIL.EMAIL_DETAIL_ID
                            WHERE  MST_ITEM.ITEM_ID = " + SearchId + " ";

                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                if (reader == null) return false;

                while (reader.Read())
                {
                    //
                    strEmail = Convert.ToString(reader["EMAIL"]);
                    strWebsite = Convert.ToString(reader["WEBSITE"]);
                    strDisplayName = Convert.ToString(reader["DISPLAYNAME"]);
                    strImageUrl = Convert.ToString(reader["IMAGEURL"]);
                    BS.BS_EmailID = Convert.ToString(reader["EMAIL_TO_BCC"]);
                    //
                    reader.Close();
                    reader.Dispose();
                    return true;
                }
                reader.Close();
                reader.Dispose();
                return false;

            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                reader.Close();
                reader.Dispose();
                cmnService.J_UserMessage(err.Message);
                //
                return false;
            }
        }
        #endregion 

        #region ProcessToSendEmailSms
        public bool ProcessToSendEmailSms(long InvoiceHeaderId,long ItemId)
        {
            try
            {
                if (cmbBillingMode.Text == BS_BillMode.OfflineDelivery)
                {
                    strSQL = @" SELECT OFFLINE_SERIAL_CODE 
                                FROM   TRN_INVOICE_HEADER 
                                       INNER JOIN MST_OFFLINE_SERIAL
                                       ON TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID = MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID 
                                WHERE  INVOICE_HEADER_ID = " + InvoiceHeaderId;
                    //
                    BS.BS_OfflineSerialNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                }
                else
                    BS.BS_OfflineSerialNo = "";
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //ADDED BY DHRUB On 20/02/2015
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (GenerateInvoicePDF(InvoiceHeaderId) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Export to PDF file Failed!"); 
                    return false;
                }
                //Initialize the Pdf Path 
                strPdfPath = Path.Combine(Application.StartupPath + "\\" + strGenerateInvoiceFolderName, Convert.ToString(txtInvoiceNo.Text.Replace("/", "-").Trim())) + ".pdf";
                //---------------------------------
                if (BS.BS_PartySendEmail == true)
                {
                    //Get the Email Text 
                    //---------------------------------
                    GetEmailDetails(ItemId);
                    //---------------------------------
                    if (SendEMail(BS.BS_PartyEmailId,
                        strBCCEmail,
                        strFromEmail,
                        strDisplayName,
                        strDownloadLink1,
                        strDownloadLink2,
                        BS.BS_PartyContactPerson,
                        txtInvoiceNo.Text,
                        txtOnlineOrderNo.Text,
                        strProductName,
                        Path.GetFileName(strPDFFileName),
                        BS.BS_OfflineSerialNo) == false)
                    {
                        this.Cursor = Cursors.Default;
                        cmnService.J_UserMessage("Failed to send the Email.");
                        BS.BS_SendEmailStatus = false;
                        return false;
                    }
                    else
                    {
                        strSQL = @"UPDATE TRN_INVOICE_HEADER SET 
                                      EMAIL_CONFIRMATION_COUNTER = EMAIL_CONFIRMATION_COUNTER + 1,
                                      EMAIL_SEND_DATE            = GETDATE() 
                               WHERE  INVOICE_HEADER_ID          = " + InvoiceHeaderId;
                        dmlService.J_ExecSql(strSQL);
                    }
                }
                //---------------------------------
                if (BS.BS_PartySendSMS == true)
                {
                    //---------------------------------
                    if (SendSMS(BS.BS_PartyMobNo,
                        BS.BS_PartyEmailId,
                        txtOnlineOrderNo.Text,
                        BS.BS_OfflineSerialNo) == false)
                    {
                        cmnService.J_UserMessage("Failed to send the SMS.");
                        BS.BS_SendSMSStatus = false;
                        return false;
                    }
                }
                //------------------
                BS.BS_SendSMSStatus = true;
                BS.BS_SendEmailStatus = true;
                return true;

            }
            catch (Exception err)
            {
                this.Cursor = Cursors.Default;
                cmnService.J_UserMessage("Failed to send the SMS due to Error Occured :" + err.Message);
                return false;
            }
        
        }
        #endregion 

        #region GetEmailDetails
        public void GetEmailDetails(long ItemId)
        { 
            IDataReader reader = null;
            try
            {
                string strFieldName = "";
                string strSubjectName = "";
                //
                if (cmbBillingMode.Text.ToUpper() == "ONLINE DELIVERY")
                {
                    strFieldName = "EMAIL_BODY_ONLINE";
                    strSubjectName = "EMAIL_SUBJECT_ONLINE";
                }
                else
                {
                    strFieldName = "EMAIL_BODY_OFFLINE";
                    strSubjectName = "EMAIL_SUBJECT_OFFLINE";
                }
                //
                strSQL = @" SELECT  MST_EMAIL_CATEGORY." + strFieldName + @" AS EMAIL_BODY,
                                    MST_EMAIL_CATEGORY." + strSubjectName + @" AS EMAIL_SUBJECT,
                                    MST_EMAIL_CATEGORY.FROM_EMAIL            AS FROM_EMAIL,
                                    MST_EMAIL_CATEGORY.DISPLAY_NAME          AS DISPLAY_NAME,
                                    MST_EMAIL_CATEGORY.EMAIL_BCC             AS EMAIL_BCC,
                                    MST_ITEM.DOWNLOAD_LINK1                  AS DOWNLOAD_LINK1,
                                    MST_ITEM.DOWNLOAD_LINK2                  AS DOWNLOAD_LINK2,
                                    MST_ITEM.ITEM_NAME                       AS ITEM_NAME,
                                    MST_ITEM.PDF_DOC_NAME                    AS PDF_DOC_NAME
                            FROM    MST_ITEM
                                    INNER JOIN MST_EMAIL_CATEGORY
                                    ON MST_ITEM.EMAIL_TYPE_ID = MST_EMAIL_CATEGORY.EMAIL_TYPE_ID
                            WHERE   MST_ITEM.ITEM_ID  = " + ItemId;
                // strEmailBody = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                if (reader == null) return;

                while (reader.Read())
                {
                    strEmailBody = Convert.ToString(reader["EMAIL_BODY"]);
                    strEmailSubject = Convert.ToString(reader["EMAIL_SUBJECT"]);
                    strFromEmail = Convert.ToString(reader["FROM_EMAIL"]);
                    strDisplayName = Convert.ToString(reader["DISPLAY_NAME"]);
                    strBCCEmail = Convert.ToString(reader["EMAIL_BCC"]);
                    strDownloadLink1 = Convert.ToString(reader["DOWNLOAD_LINK1"]);
                    strDownloadLink2 = Convert.ToString(reader["DOWNLOAD_LINK2"]);
                    strProductName = Convert.ToString(reader["ITEM_NAME"]);
                    strPDFFileName = Convert.ToString(reader["PDF_DOC_NAME"]);
                    //
                    reader.Close();
                    reader.Dispose();
                    return;
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                reader.Close();
                reader.Dispose();
                return;
            }

        }
        #endregion 
        

        #region VerifyPartyDetails
        public bool VerifyPartyDetails()
        {
            try
            {
                arrSendPopupDetails[0] = mskInvoiceDate.Text;
                arrSendPopupDetails[1] = string.Format("{0:0.00}", Convert.ToDouble(txtNetAmount.Text));
                arrSendPopupDetails[2] = strPartyName;
                arrSendPopupDetails[3] = strPartyAddress1;
                arrSendPopupDetails[4] = strPartyAddress2;
                arrSendPopupDetails[5] = strPartyAddress3;
                arrSendPopupDetails[6] = strPartyCity;
                arrSendPopupDetails[7] = strPartyPin;
                arrSendPopupDetails[8] = strPartyContactPerson;
                arrSendPopupDetails[9] = strPartyMobileNumber;
                arrSendPopupDetails[10] = strPartyEmailId;
                arrSendPopupDetails[11] = lblPartyCategory.Text;
                arrSendPopupDetails[12] = txtOnlineOrderNo.Text;
                arrSendPopupDetails[13] = cmbBillingMode.Text;
                arrSendPopupDetails[14] = lblMode.Text;
                //-----------------------------------------------------------------
                TrnOnlineOfflineEmailVerification frmOnlineOfflineEmailVerification = new TrnOnlineOfflineEmailVerification(cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex), arrSendPopupDetails);
                //-----------------------------------------------------------------
                this.Cursor = Cursors.Default;
                //--        
                frmOnlineOfflineEmailVerification.ShowDialog();
                //-----------------------------------------------------------------
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }        
        }
        #endregion 

        
        
        

        #endregion

        

        
    }
}

