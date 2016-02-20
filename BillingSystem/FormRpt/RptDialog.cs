
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: RptDialog
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented for reports
_________________________________________________________________________________________________________

*/

#endregion

#region Importing Namespace

// System Namespaces
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

using System.Threading;

// This namespace are using for Combo Box
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.Classes;
using BillingSystem.FormRpt;
using BillingSystem.Reports.Listing;
using BillingSystem.Reports.Transaction.INVOICE;

#endregion

#region set ENUM for Reports

// Declaration ENUM of enmReports
public enum J_Reports
{

    #region Listing Reports Enum
    
    Listing        = 100,
    FAYear         = 110,

    #endregion

    #region Master Reports Enum
    
    Master        = 1000,
    User          = 1010,
    Company       = 1020,
    Party         = 1030,
    Item          = 1040,
    InvoiceSeries = 1050,
    Tax           = 1060,

    #endregion

    #region Transaction Reports Enum
    
    Transaction = 10000,
    Invoice      = 100010,
    InvoiceRegister = 100020,

    //Added by Shrey Kejriwal on 25/08/2011
    ItemWiseInvoiceSummary = 100030,


    //Added by Shrey Kejriwal on 04/08/2012
    TaxRegister = 100040,

    //Added by Shrey Kejriwal on 30/04/2013
    PartyCategoryWiseRegister = 100050,

    // Added by Ripan Paul on 07-05-2013
    AccountsEntryDateWiseRegister = 100060,

    // Added by Ripan Paul on 08-05-2013
    ListOfUnknownDeposits = 100070,

    // Added by Ripan Paul on 08-05-2013
    BillWiseOutstanding = 100080,

    // Added by Ripan Paul on 09-05-2013
    PendingCCAvenueTransactions = 100090,

    // Added by Ripan Paul on 09-05-2013
    BankStDateWiseRegister = 100100,

    // Added by Ripan Paul on 09-05-2013
    DetailsCollectionType = 100110,

    //Added by Shrey Kejriwal on 21-01-2014
    SalesDistribution     = 100200,

    PartyCategoryWiseMonthlySalesSummary = 100300,

    PartyCategoryWiseMonthlySale =100400,

    AccountReconciliation  =100500,

    SundryPartySale =100600,

    ItemwiseSaleDetails = 100700,

    SundryPartySalesCumOutstanding =100800,

    SundryPartyReconcilation = 100900,

    PartyListSales = 101000,

    OutstandingPayments = 101100,

    UnknownCollectionEntry = 101200,

    KnownCollectionEntry = 101300,

    InvoicePaymentStatus = 101400,

    InvoiceStatusSummary = 101500,

    InvoiceStatusDetail = 101600,

    PaymentStatusDetail = 101700,

    PaymentStatusSummary = 101800,

    ReconciliationDetail = 101900,

    DailyCollectionSummary = 102000,

    UnreconciledCollectionList = 102100,

    UnknownPaymentList = 102200,

    AdjustmentRegister =102300,

    ReconciliationStatement = 102400,

    PeriodicCollectionSummary = 102500,

    SundryPartyOutstandingSummary = 102600,
 
    SundryPartyCollectionMiniStatement = 102700,

    OutstandingCumUnknown = 102800,

    AdvanceCollectionRegister = 102900,

    PaymentTypeWiseOutstandingSummary = 103000,

    CategoryWiseVATCSTSale= 103100,

    TallyReconciliation = 103200,

    DespatchStatusReport = 103300,

    EmailCheckList = 103400,
    #endregion

}

#endregion

namespace BillingSystem.FormRpt
{
    public partial class RptDialog : Form
    {

        #region CONSTRUCTOR & OBJECT CREATION

        public RptDialog()
        {
            InitializeComponent();


            dmlService = new DMLService();
            cmnService = new CommonService();
            dtservice = new DateService();
            rptService = new ReportService();
            
            this.enmSearchType = J_SearchType.Incremental;

        }

        #endregion

        #region VARIABLES DECLARATION SECTION

        // Declaration of objects
        DMLService dmlService = null;
        CommonService cmnService = null;
        DateService dtservice = null;
        ReportService rptService = null;

        // report file object
        ReportClass rptcls;

        // Storing the Query String as a text
        string strSQL = string.Empty;
        string strQueryString = string.Empty;

        // Set true false in radio buttion check change event & pass this value as a parameter in rptmain.SerchDatagridOnCriteria() Function
        private J_SearchType enmSearchType;

        // To implement the progress bar
        private Thread ThreadProgress;
        delegate void MyDelegate();

        private int intFaYearComboID = 0;
        private string strFaYearBegDate = "";
        private string strFaYearEndDate = "";

        #endregion

        #region EVENT HANDLER OF CONTROLS

        #region RptDialog_Activated
        private void RptDialog_Activated(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region RptDialog_Load
        private void RptDialog_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region BtnOK_Click
        private void BtnOK_Click(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {

                #region Masters

                case (int)J_Reports.User:
                    PrintUserMaster();
                    break;
                case (int)J_Reports.FAYear:
                    PrintFAYearMaster();
                    break;
                case (int)J_Reports.Company:
                    PrintCompanyMaster();
                    break;
                case (int)J_Reports.Party:
                    PrintPartyMaster();
                    break;
                case (int)J_Reports.Item:
                    PrintItemMaster();
                    break;
                case (int)J_Reports.InvoiceSeries:
                    PrintInvoiceSeriesMaster();
                    break;

                #endregion

                #region Transactions

                case (int)J_Reports.Invoice:
                    if (this.ValidateFields(J_Reports.Invoice) == false) return;
                    PrintInvoice();
                    break;

                case (int)J_Reports.InvoiceRegister:
                    if (this.ValidateFields(J_Reports.InvoiceRegister) == false) return;
                    PrintInvoiceRegister();
                    break;

                //Added by Shrey Kejriwal on 25/08/2011
                case (int)J_Reports.ItemWiseInvoiceSummary:
                    if (this.ValidateFields(J_Reports.ItemWiseInvoiceSummary) == false) return;
                    PrintItemWiseInvoiceSummary();
                    break;

                //Added by Shrey Kejriwal on 04/08/2012
                case (int)J_Reports.TaxRegister:
                    if (this.ValidateFields(J_Reports.TaxRegister) == false) return;
                    PrintTaxRegister();
                    break;

                //Added by Shrey Kejriwal on 30/04/2013
                case (int)J_Reports.PartyCategoryWiseRegister:
                    if (this.ValidateFields(J_Reports.PartyCategoryWiseRegister) == false) return;
                    PrintPartyCategoryWiseRegister();
                    break;


                //Added by Ripan Paul on 07/05/2013
                case (int)J_Reports.AccountsEntryDateWiseRegister:
                    if (this.ValidateFields(J_Reports.AccountsEntryDateWiseRegister) == false) return;
                    PrintAccountsEntryDateWiseRegister();
                    break;

                //Added by Ripan Paul on 08/05/2013
                case (int)J_Reports.ListOfUnknownDeposits:
                    if (this.ValidateFields(J_Reports.ListOfUnknownDeposits) == false) return;
                    PrintListOfUnknownDeposits();
                    break;

                //Added by Ripan Paul on 08/05/2013
                case (int)J_Reports.BillWiseOutstanding:
                    if (this.ValidateFields(J_Reports.BillWiseOutstanding) == false) return;
                    PrintBillWiseOutstanding();
                    break;

                //Added by Ripan Paul on 09/05/2013
                case (int)J_Reports.PendingCCAvenueTransactions:
                    if (this.ValidateFields(J_Reports.PendingCCAvenueTransactions) == false) return;
                    PrintPendingCCAvenueTransactions();
                    break;

                //Added by Ripan Paul on 09/05/2013
                case (int)J_Reports.BankStDateWiseRegister:
                    if (this.ValidateFields(J_Reports.BankStDateWiseRegister) == false) return;
                    PrintBankStDateWiseRegister();
                    break;

                //Added by Ripan Paul on 09/05/2013
                case (int)J_Reports.DetailsCollectionType:
                    if (this.ValidateFields(J_Reports.DetailsCollectionType) == false) return;
                    PrintDetailsCollectionType();
                    break;

                //Added by Shrey Kejriwal on 21/01/2014
                case (int)J_Reports.SalesDistribution:
                    if (this.ValidateFields(J_Reports.SalesDistribution) == false) return;
                    PrintSalesDistribution();
                    break;
                //Added by Dhrub Mukherjee on 28/04/2014
                case (int)J_Reports.PartyCategoryWiseMonthlySalesSummary:
                    if (this.ValidateFields(J_Reports.PartyCategoryWiseMonthlySalesSummary) == false) return;
                    PartyCategoryWiseMonthlySaleDetails();
                    break;
                //Added by Dhrub Mukherjee on 28/04/2014
                case (int)J_Reports.PartyCategoryWiseMonthlySale:
                    if (this.ValidateFields(J_Reports.PartyCategoryWiseMonthlySale) == false) return;
                    PartyCategoryWiseMonthlySale();
                    break;

                case (int)J_Reports.AccountReconciliation:
                    if (this.ValidateFields(J_Reports.AccountReconciliation) == false) return;
                    PrintAccountReconciliation();
                    break;

                case (int)J_Reports.SundryPartySale:
                    if (this.ValidateFields(J_Reports.SundryPartySale) == false) return;
                    PrintSundryPartySale();
                    break;
                case (int)J_Reports.ItemwiseSaleDetails:
                    if (this.ValidateFields(J_Reports.ItemwiseSaleDetails) == false) return;
                    PrintItemwiseSaleDetails();
                    break;
                case (int)J_Reports.SundryPartySalesCumOutstanding:
                    if (this.ValidateFields(J_Reports.SundryPartySalesCumOutstanding) == false) return;
                    PrintSundryPartySalesCumOutstanding();
                    break;
                case (int)J_Reports.SundryPartyReconcilation:
                    if (this.ValidateFields(J_Reports.SundryPartyReconcilation) == false) return;
                    PrintSundryPartyReconcilation();
                    break;
                case (int)J_Reports.PartyListSales:
                    if (this.ValidateFields(J_Reports.PartyListSales) == false) return;
                    PrintPartyListSales();
                    break;
                case (int)J_Reports.OutstandingPayments:
                    if (this.ValidateFields(J_Reports.OutstandingPayments) == false) return;
                    PrintOutstandingPayments();
                    break;
                case (int)J_Reports.UnknownCollectionEntry:
                    if (this.ValidateFields(J_Reports.UnknownCollectionEntry) == false) return;
                    PrintUnknownCollectionEntry();
                    break;
                case (int)J_Reports.KnownCollectionEntry:
                    if (this.ValidateFields(J_Reports.KnownCollectionEntry) == false) return;
                    PrintKnownCollectionEntry();
                    break;
                case (int)J_Reports.InvoicePaymentStatus:
                    if (this.ValidateFields(J_Reports.InvoicePaymentStatus) == false) return;
                    PrintInvoicePaymentStatus();
                    break;
                case (int)J_Reports.InvoiceStatusSummary:
                    if (this.ValidateFields(J_Reports.InvoiceStatusSummary) == false) return;
                    PrintInvoiceStatusSummary();
                    break;
                case (int)J_Reports.InvoiceStatusDetail:
                    if (this.ValidateFields(J_Reports.InvoiceStatusDetail) == false) return;
                    PrintInvoiceStatusDetail();
                    break;
                case (int)J_Reports.PaymentStatusDetail:
                    if (this.ValidateFields(J_Reports.PaymentStatusDetail) == false) return;
                    PrintPaymentStatusDetail();
                    break;
                case (int)J_Reports.PaymentStatusSummary:
                    if (this.ValidateFields(J_Reports.PaymentStatusSummary) == false) return;
                    PrintPaymentStatusSummary();
                    break;
                case (int)J_Reports.ReconciliationDetail:
                    if (this.ValidateFields(J_Reports.ReconciliationDetail) == false) return;
                    PrintReconciliationDetail();
                    break;
                case (int)J_Reports.DailyCollectionSummary:
                    if (this.ValidateFields(J_Reports.DailyCollectionSummary) == false) return;
                    PrintDailyCollectionSummary();
                    break;
                case (int)J_Reports.UnreconciledCollectionList:
                    if (this.ValidateFields(J_Reports.UnreconciledCollectionList) == false) return;
                    PrintUnreconciledCollectionList();
                    break;
                case (int)J_Reports.UnknownPaymentList:
                    if (this.ValidateFields(J_Reports.UnknownPaymentList) == false) return;
                    PrintUnknownPaymentList();
                    break;
                case (int)J_Reports.AdjustmentRegister:
                    if (this.ValidateFields(J_Reports.AdjustmentRegister) == false) return;
                    PrintAdjustmentRegister();
                    break;

                case (int)J_Reports.ReconciliationStatement:
                    if (this.ValidateFields(J_Reports.ReconciliationStatement) == false) return;
                    PrintReconciliationStatement();
                    break;
                case (int)J_Reports.PeriodicCollectionSummary:
                    if (this.ValidateFields(J_Reports.PeriodicCollectionSummary) == false) return;
                    PrintPeriodicCollectionSummary();
                    break;
                case (int)J_Reports.SundryPartyOutstandingSummary:
                    if (this.ValidateFields(J_Reports.SundryPartyOutstandingSummary) == false) return;
                    PrintSundryPartyOutstandingSummary();
                    break;
                case (int)J_Reports.SundryPartyCollectionMiniStatement:
                    if (this.ValidateFields(J_Reports.SundryPartyCollectionMiniStatement) == false) return;
                    PrintSundryPartyCollectionMiniStatement();
                    break;
                case (int)J_Reports.OutstandingCumUnknown:
                    if (this.ValidateFields(J_Reports.OutstandingCumUnknown) == false) return;
                    PrintOutstandingCumUnknown();
                    break;
                case (int)J_Reports.AdvanceCollectionRegister:
                    if (this.ValidateFields(J_Reports.AdvanceCollectionRegister) == false) return;
                    PrintAdvanceCollectionRegister();
                    break;
                case (int)J_Reports.PaymentTypeWiseOutstandingSummary:
                    if (this.ValidateFields(J_Reports.PaymentTypeWiseOutstandingSummary) == false) return;
                    PrintPaymentTypeWiseOutstandingSummary();
                    break;
                case (int)J_Reports.CategoryWiseVATCSTSale:
                    if (this.ValidateFields(J_Reports.CategoryWiseVATCSTSale) == false) return;
                    PrintCategoryWiseVATCSTSale();
                    break;
                case (int)J_Reports.TallyReconciliation:
                    if (this.ValidateFields(J_Reports.TallyReconciliation) == false) return;
                    PrintTallyReconciliation();
                    break;
                case (int)J_Reports.DespatchStatusReport:
                    if (this.ValidateFields(J_Reports.DespatchStatusReport) == false) return;
                    PrintDespatchStatusReport();
                    break;

                case (int)J_Reports.EmailCheckList:
                    if (this.ValidateFields(J_Reports.EmailCheckList) == false) return;
                    PrintEmailCheckList();
                    break;
                
                #endregion

            }
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region mskFromDate_KeyPress
        private void mskFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskToDate_KeyPress
        private void mskToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskAsOnDate_KeyPress
        private void mskAsOnDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskFromMonth_KeyPress
        private void mskFromMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region mskToMonth_KeyPress
        private void mskToMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region grdvDescription_CellClick
        private void grdvDescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvDescription.Rows[e.RowIndex].Cells[0];
                    if (cell.Value == null || (bool)cell.Value == false)
                    {
                        grdvDescription.MultiSelect = true;
                        grdvDescription.Rows[e.RowIndex].Selected = true;
                        cell.Value = true;
                    }
                    else
                    {
                        grdvDescription.Rows[e.RowIndex].Selected = false;
                        cell.Value = false;
                    }
                }
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);

            }
        }
        #endregion

        #region grdvDescription_KeyDown
        private void grdvDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BtnOK.Focus();
        }
        #endregion

        #region chkSelectDeselect_CheckedChanged
        private void chkSelectDeselect_CheckedChanged(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow row in grdvDescription.Rows)
            {
                if (chkSelectDeselect.Checked == true)  //checked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        row.Cells[0].Value = true;
                }
                else                                    //Unchecked all checkbox
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        row.Cells[0].Value = false;
                }
            }
        }
        #endregion

        #region radio_Checked
        private void radio_Checked(object sender, EventArgs e)
        {
            RadioButton rbnWho = (RadioButton)sender;
            if (rbnWho.Name == "rbnIncrSearch")
                this.enmSearchType = J_SearchType.Incremental;
            else if (rbnWho.Name == "rbnEmbddSearch")
                    this.enmSearchType = J_SearchType.Embedded;
        }
        #endregion

        #region txtSearch_TextChanged
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectDeselect.Checked == true) chkSelectDeselect.Checked = false;
                
                // for single column datagrid
                if (rptService.J_pGridMultipleColumnStyle == false)
                {
                    if (rptService.J_pGridDataSet == null) return;
                    DataView dvwFilter = rptService.J_pGridDataSet.Tables[0].DefaultView;
                    dvwFilter.RowFilter = rptService.J_pGridDataSet.Tables[0].Columns[1].ColumnName + " like '" + cmnService.J_ReplaceQuote(txtSearch.Text) + "%'";
                    grdvDescription.DataSource = dvwFilter;
                }
                // for Multiple columns DatagridView
                else
                {
                    rptService.J_SearchOnDataGridView(ref grdvDescription, ref cmbSearch, txtSearch.Text, this.enmSearchType);
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region txtSearch_KeyPress
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) grdvDescription.Focus();
        }
        #endregion

        #region cmbSearch_SelectedIndexChanged
        private void cmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearch.Items.Count > 0)
            {
                txtSearch.Text = "";
                int intItemIndex = Support.GetItemData(cmbSearch, cmbSearch.SelectedIndex);
                
                if (intItemIndex == 1)
                    rptService.J_pSearchColumnType = J_ColumnType.String;
                else if (intItemIndex == 2)
                    rptService.J_pSearchColumnType = J_ColumnType.Integer;
                else if (intItemIndex == 3)
                    rptService.J_pSearchColumnType = J_ColumnType.Date;
                else if (intItemIndex == 4)
                    rptService.J_pSearchColumnType = J_ColumnType.Double;
            }
            try
            {
                for (int intCounter = 0; intCounter <= grdvDescription.ColumnCount - 1; intCounter++)
                    rptService.J_pSearchColumnName = cmbSearch.SelectedItem.ToString();
                
            }
            catch (Exception ex)
            {
                cmnService.J_UserMessage(ex.Message);
            }
        }
        #endregion

        #region cmbCombo1_SelectedIndexChanged
        private void cmbCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;

                case (int)J_Reports.Invoice:

                    // set the Help Grid Column Header Text & behavior
                    // (0) Header Text
                    // (1) Width
                    // (2) Format
                    // (3) Alignment
                    // (4) NullToText
                    // (5) Visible
                    // (6) AutoSizeMode
                    string[,] strInvoiceDetails = {{"Header Id", "0", "", "R", "", "F", ""},
							                       {"Invoice No", "150", "S", "", "", "T", ""},
                                                   {"Invoice Date", "100", "S", "", "", "T", "fill"},
                                                   {"Party Name", "400", "S", "", "", "T", "fill"}};

                    strSQL = "SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                             "       TRN_INVOICE_HEADER.INVOICE_NO        AS INVOICE_NO," +
                             "       " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS INVOICE_DATE," +                            
                             "       MST_PARTY.PARTY_NAME                 AS PARTY_NAME " +
                             "FROM   TRN_INVOICE_HEADER," +
                             "       MST_PARTY " +
                             "WHERE  TRN_INVOICE_HEADER.PARTY_ID   = MST_PARTY.PARTY_ID " +
                             "AND    TRN_INVOICE_HEADER.TRAN_TYPE  ='INV' " +
                             "AND    TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                             "AND    TRN_INVOICE_HEADER.BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                             "ORDER BY TRN_INVOICE_HEADER.INVOICE_NO DESC";

                    if (rptService.J_PopulateGridView(grdvDescription, strSQL, strInvoiceDetails, ref cmbSearch, true) == false) return;
                    chkSelectDeselect.Checked = false;
                    break;
                //Added by Shrey on 03/08/2011
                case (int)J_Reports.InvoiceRegister:

                    if (cmbCombo1.SelectedIndex > 0)
                    {
                        strSQL = "SELECT INVOICE_SERIES_ID," +
                                  "       PREFIX " +
                                  "FROM   MST_INVOICE_SERIES " +
                                  "WHERE  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                                  "ORDER BY INVOICE_SERIES_ID ";

                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                    }
                    else
                        cmbCombo2.Items.Clear();
                    break;

                //Added by Shrey Kejriwal on 25/08/2011
                case (int)J_Reports.ItemWiseInvoiceSummary:

                    if (cmbCombo1.SelectedIndex > 0)
                    {
                        strSQL = "SELECT INVOICE_SERIES_ID," +
                                  "       PREFIX " +
                                  "FROM   MST_INVOICE_SERIES " +
                                  "WHERE  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                                  "ORDER BY INVOICE_SERIES_ID ";

                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                    }
                    else
                        cmbCombo2.Items.Clear();
                    break;

                //Added by Shrey Kejriwal on 04/08/2012
                case (int)J_Reports.TaxRegister:

                    if (cmbCombo1.SelectedIndex > 0)
                    {
                        strSQL = "SELECT INVOICE_SERIES_ID," +
                                  "       PREFIX " +
                                  "FROM   MST_INVOICE_SERIES " +
                                  "WHERE  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                                  "ORDER BY INVOICE_SERIES_ID ";

                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                    }
                    else
                        cmbCombo2.Items.Clear();
                    break;

                case (int)J_Reports.AccountReconciliation:
                    if (cmbCombo1.SelectedIndex > 0)
                    {
                        intFaYearComboID = Support.GetItemData(cmbCombo1, cmbCombo1.SelectedIndex);

                        strSQL = @"SELECT CONVERT(VARCHAR(10),FA_BEG_DATE,103) 
                                   FROM   MST_FAYEAR          
                                   WHERE  FAYEAR_ID = " + intFaYearComboID;

                        strFaYearBegDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        strSQL = @"SELECT CONVERT(VARCHAR(10),FA_END_DATE,103) 
                                   FROM   MST_FAYEAR          
                                   WHERE  FAYEAR_ID = " + intFaYearComboID;

                        strFaYearEndDate = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                        //if (dtservice.J_IsBlankDateCheck(ref mskFromDate, J_ShowMessage.NO) == true)
                            mskFromDate.Text = strFaYearEndDate;
                    }
                    break;
                case (int)J_Reports.ItemwiseSaleDetails:
                    if (cmbCombo1.SelectedIndex > 0)
                    {
                        string[,] strItemDetails = {{"Item Id", "0", "", "R", "", "F", ""},
							                       {"Item Name", "500", "S", "", "", "T", "fill"},
                                                   {"Rate", "0", "", "R", "", "F", ""}};

                        strSQL = "SELECT ITEM_ID   AS ITEM_ID, " +
                                 "       ITEM_NAME AS ITEM_NAME, " +
                                 "       RATE      AS RATE " +
                                 "FROM   MST_ITEM " +
                                 "WHERE  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " ";

                        if (rptService.J_PopulateGridView(grdvDescription, strSQL, strItemDetails, ref cmbSearch, true) == false) return;
                        chkSelectDeselect.Checked = false;
                    }
                    break;

            }
        }
        #endregion

        #region cmbCombo1_KeyPress
        private void cmbCombo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCombo1_KeyUp
        private void cmbCombo1_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCombo1, e);
        }
        #endregion

        #region cmbCombo1_Leave
        private void cmbCombo1_Leave(object sender, EventArgs e)
        {
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCombo1);

            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            object objRowView = cmbCombo1.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.RecNotExist + " : " + cmbCombo1.Text);
                cmbCombo1.Focus();
            }
        }
        #endregion

        #region cmbCombo2_SelectedIndexChanged
        private void cmbCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;
            }
        }
        #endregion

        #region cmbCombo2_KeyPress
        private void cmbCombo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCombo2_KeyUp
        private void cmbCombo2_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCombo2, e);
        }
        #endregion

        #region  cmbCombo2_Leave
        private void cmbCombo2_Leave(object sender, EventArgs e)
        {
            
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCombo2);

            if (cmbCombo2.SelectedIndex < 0 ) return;
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            object objRowView = cmbCombo2.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.RecNotExist + " : " + cmbCombo2.Text);
                cmbCombo2.Focus();
            }
        }
        #endregion

        #region cmbCombo3_SelectedIndexChanged
        private void cmbCombo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;


            }
        }
        #endregion

        #region cmbCombo3_KeyPress
        private void cmbCombo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCombo3_KeyUp
        private void cmbCombo3_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCombo3, e);
        }
        #endregion

        #region cmbCombo3_Leave
        private void cmbCombo3_Leave(object sender, EventArgs e)
        {
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCombo3);

            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            object objRowView = cmbCombo3.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.RecNotExist + " : " + cmbCombo3.Text);
                cmbCombo3.Focus();
            }
        }
        #endregion

        #region cmbCombo4_SelectedIndexChanged
        private void cmbCombo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;


            }
        }
        #endregion

        #region cmbCombo4_KeyPress
        private void cmbCombo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbCombo4_KeyUp
        private void cmbCombo4_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCombo4, e);
        }
        #endregion

        #region cmbCombo4_Leave
        private void cmbCombo4_Leave(object sender, EventArgs e)
        {
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCombo4);

            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            object objRowView = cmbCombo4.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.RecNotExist + " : " + cmbCombo4.Text);
                cmbCombo4.Focus();
            }
        }
        #endregion


        #region txtTextBox1_KeyPress
        private void txtTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTextBox1_Leave
        private void txtTextBox1_Leave(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;

            }

        }
        #endregion

        #region txtTextBox2_KeyPress
        private void txtTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtTextBox2_Leave
        private void txtTextBox2_Leave(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;

            }
        }
        #endregion


        #region grpSort_CheckedChanged
        private void grpSort_CheckedChanged(object sender, EventArgs e)
        {
            switch (J_Var.J_pReportEnumIndex)
            {
                case (int)J_Reports.Transaction:
                    break;
            }
        }
        #endregion

        #endregion

        #region USER DEFINED FUNCTION

        #region SetRptDialogOptions
        public void SetRptDialogOptions(J_Reports enmReport, string strFormText)
        {
            try
            {
                // get server date
                mskAsOnDate.Text = dmlService.J_ReturnServerDate();
                mskFromDate.Text = dmlService.J_ReturnServerDate();
                mskToDate.Text = dmlService.J_ReturnServerDate();

                J_Var.J_pReportEnumIndex = (int)enmReport;
                switch (enmReport)
                {

                    #region Masters

                    case J_Reports.User:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;
                    case J_Reports.FAYear:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;
                    case J_Reports.Company:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;
                    case J_Reports.Party:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;
                    case J_Reports.Item:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;
                    case J_Reports.InvoiceSeries:
                        lblTitle.Text = strFormText;
                        BtnOK.Select();
                        break;


                    #endregion

                    #region Transactions

                    case J_Reports.Invoice:
                        lblTitle.Text = strFormText;
                        rbnEmbddSearch.Checked = true; 

                        grpCombo.Visible = true;
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        plnDetails.Visible = true;
                        lblGridViewTitle.Text = "Invoice Details";
                        plnComboSearchOn.Visible = true;

                        //Commented by Shrey Kejriwal on 05/08/2011

                        //grpSort.Visible = true;
                        //grpSort.Text = "Invoice Type";
                        //rbnSort1.Visible = true;
                        //rbnSort1.Text = "Full Set";
                        //rbnSort2.Visible = true;
                        //rbnSort2.Text = "Buyer's Copy";
                        //rbnSort3.Visible = true;
                        //rbnSort3.Text = "Duplicate Copy";
                        //rbnSort4.Visible = true;
                        //rbnSort4.Text = "Office Copy";
                        //rbnSort1.Checked = true;

                        grpSort1.Visible = true;
                        grpSort1.Text = "Print Type";
                        rbnSort1_1.Visible = true;
                        rbnSort1_1.Text = "None";
                        rbnSort1_2.Visible = true;
                        rbnSort1_2.Text = "Print with Letter Head";
                        rbnSort1_1.Checked = true;

                        grpSort2.Visible = true;
                        grpSort2.Text = "Print Signature";
                        rbnSort2_1.Visible = true;
                        rbnSort2_1.Text = "No";
                        rbnSort2_2.Visible = true;
                        rbnSort2_2.Text = "Yes";
                        rbnSort2_1.Checked = true;

                        //Added by Shrey Kejriwal on 05/08/2011
                        grpCheckBox.Visible = true;
                        grpCheckBox.Text = "Invoice Type";
                        
                        chkBox1.Visible = true;
                        chkBox1.Text = "Buyer's Copy";
                        
                        chkBox2.Visible = true;
                        chkBox2.Text = "Duplicate Copy";

                        chkBox3.Visible = true;
                        chkBox3.Text = "Office Copy";

                        cmbCombo1.Select();
                        break;

                    //ADDED BY SHREY ON 03/08/2011
                    case J_Reports.InvoiceRegister:
                        lblTitle.Text = strFormText;

                        //
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Invoice Series";
                        cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    //ADDED BY SHREY ON 25/08/2011
                    case J_Reports.ItemWiseInvoiceSummary:
                        lblTitle.Text = strFormText;

                        //
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        //lblComboTitle2.Visible = true;
                        //lblComboTitle2.Text = "Invoice Series";
                        //cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.TaxRegister:
                        lblTitle.Text = strFormText;

                        //
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        //lblComboTitle2.Visible = true;
                        //lblComboTitle2.Text = "Invoice Series";
                        //cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.PartyCategoryWiseRegister:
                        lblTitle.Text = strFormText;

                        //
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        //lblComboTitle2.Visible = true;
                        //lblComboTitle2.Text = "Invoice Series";
                        //cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;


                    case J_Reports.AccountsEntryDateWiseRegister:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.ListOfUnknownDeposits:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;

                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Entry Type";
                        cmbCombo2.Visible = true;
                        cmbCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmbCombo2.Items.Clear();
                        cmbCombo2.Items.Add(new ListBoxItem("VALID", 0));
                        cmbCombo2.Items.Add(new ListBoxItem("CANCELLED", 1));
                        cmbCombo2.SelectedIndex = 0;

                        mskFromDate.Select();
                        break;

                    case J_Reports.BillWiseOutstanding:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.PendingCCAvenueTransactions:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;

                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Entry Type";
                        cmbCombo2.Visible = true;
                        cmbCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmbCombo2.Items.Clear();
                        cmbCombo2.Items.Add(new ListBoxItem("Pending", 0));
                        cmbCombo2.Items.Add(new ListBoxItem("All", 1));
                        cmbCombo2.SelectedIndex = 0;

                        mskFromDate.Select();
                        break;

                    case J_Reports.BankStDateWiseRegister:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.DetailsCollectionType:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.SalesDistribution:
                        
                        //Added by Shrey Kejriwal on 21-01-2014
                        lblTitle.Text = strFormText;

                        //
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        //lblComboTitle2.Visible = true;
                        //lblComboTitle2.Text = "Invoice Series";
                        //cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.PartyCategoryWiseMonthlySalesSummary:
                        //Added by Dhrub Mukherjee on 28-04-2014
                        lblTitle.Text = strFormText;

                        grpMonth.Visible = true;
                        lblFromMonth.Text = "From";
                        mskFromMonth.Text = "";
                        lblToMonth.Text = "To";
                        mskToMonth.Text = "";
                        mskFromMonth.Select();

                        grpCombo.Visible = true;
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;


                        break;

                    case J_Reports.PartyCategoryWiseMonthlySale:
                        //Added by Dhrub Mukherjee on 29-04-2014
                        lblTitle.Text = strFormText;

                        grpMonth.Visible = true;
                        lblFromMonth.Text = "From";
                        mskFromMonth.Text = "";
                        lblToMonth.Text = "To";
                        mskToMonth.Text = "";
                        mskFromMonth.Select();

                        grpCombo.Visible = true;
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;


                        break;

                    case J_Reports.AccountReconciliation:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblToDate.Visible = false;
                        mskToDate.Visible = false;
                        label8.Visible = false;

                        lblFromDate.Text = "As On";
                        mskFromDate.Text = "";
                        
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "F.A Year";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT FAYEAR_ID," +
                                 "       'F.A.Year : ' + cast(YEAR(fa_beg_Date) as varchar) + '-' + cast(YEAR(fa_end_Date) as varchar) AS FA_DATE " +
                                 "FROM   MST_FAYEAR " +
                                 "ORDER BY FAYEAR_ID DESC";

                        dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1);

                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Company";
                        cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2, 1) == false) return;

                        mskFromDate.Select();
                        break;
                    case J_Reports.SundryPartySale:
                        lblTitle.Text = strFormText;

                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "F.A Year";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT FAYEAR_ID," +
                                 "       'F.A.Year : ' + cast(YEAR(fa_beg_Date) as varchar) + '-' + cast(YEAR(fa_end_Date) as varchar) AS FA_DATE " +
                                 "FROM   MST_FAYEAR " +
                                 "ORDER BY FAYEAR_ID DESC";

                        dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1);

                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Company";
                        cmbCombo2.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2, 1) == false) return;

                        cmbCombo1.Select();
                        break;
                    case J_Reports.ItemwiseSaleDetails:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        plnDetails.Visible = true;
                        lblGridViewTitle.Text = "Item Details";
                        plnComboSearchOn.Visible = true; 

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;

                        break;
                    case J_Reports.SundryPartySalesCumOutstanding:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;

                        grpSort.Visible = true;
                        grpSort.Text = "Outstanding Category";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "All";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "Pending";


                        mskFromDate.Select();
                        break;

                    case J_Reports.SundryPartyReconcilation:
                        lblTitle.Text = strFormText;
                        //---------------------------------------------
                        //---------------------------------------------

                        grpSort.Visible = true;
                        grpSort.Text = "Report Category";
                        grpSort.Location = new Point(290, 42);
                        grpSort.Width = 360;
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Sundry Party Reconcilation";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "Outstanding Amount for this period";
                        rbnSort3.Visible = true;
                        rbnSort3.Text = "Collection during this period (Previous)";
                        rbnSort4.Visible = true;
                        rbnSort4.Text = "Collection during this period";
                        rbnSort5.Visible = true;
                        rbnSort5.Text = "Sundry Party Sale for the Period";
                        rbnSort6.Visible = true;
                        rbnSort6.Text = "Current Bills against Advance Receipt";
                        rbnSort7.Visible = true;
                        rbnSort7.Text = "Collection during this period (Current)";
                        //---------------------------------------------
                        //---------------------------------------------
                        grpMonth.Visible = true;
                        lblFromMonth.Text = "For Month";
                        mskFromMonth.Text = "";
                        lblToMonth.Visible = false;
                        mskToMonth.Visible = false;
                        label10.Visible = false;
 

                        mskFromMonth.Select();
                        break;
                    case J_Reports.PartyListSales:
                        lblTitle.Text = strFormText;

                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(11, 42);
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Item";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT ITEM_ID," +
                                 "       ITEM_NAME " +
                                 "FROM   MST_ITEM " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY ITEM_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;

                        cmbCombo1.Select();
                        break;

                    case J_Reports.OutstandingPayments:
                        lblTitle.Text = strFormText;

                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;
                        break;
                    case J_Reports.UnknownCollectionEntry:
                        lblTitle.Text = strFormText;

                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;
                        break;
                    case J_Reports.KnownCollectionEntry:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;
                        //
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;

                        strSQL = "SELECT PAYMENT_TYPE_ID," +
                                 "       PAYMENT_TYPE_DESCRIPTION " +
                                 "FROM   MST_PAYMENT_TYPE ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;

                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;

                        mskFromDate.Select();
                        break;
                    case J_Reports.InvoicePaymentStatus:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1) == false) return;
                        //
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;

                        strSQL = "SELECT PAYMENT_TYPE_ID," +
                                 "       PAYMENT_TYPE_DESCRIPTION " +
                                 "FROM   MST_PAYMENT_TYPE ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;

                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;

                        mskFromDate.Select();
                        break;

                    case J_Reports.InvoiceStatusSummary:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        grpCombo.Height = 100;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 81;
                        grpSort.Width = 100;
                        grpSort.Visible = true;
                        grpSort.Text = "Party Type";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Sundry";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "All";
                        rbnSort1.Checked = true;
                        //--
                        grpSort1.Location = new Point(700, 42);
                        grpSort1.Width = 130;
                        grpSort.Height = 81;
                        grpSort1.Visible = true;
                        grpSort1.Text = "Collection";
                        rbnSort1_1.Visible = true;
                        rbnSort1_1.Text = "Pending Only";
                        rbnSort1_2.Visible = true;
                        rbnSort1_2.Text = "All";
                        rbnSort1_1.Checked = true;
                        //--
                        //--
                        grpSort2.Location = new Point(832, 42);
                        grpSort2.Width = 170;
                        grpSort2.Height = 81;
                        grpSort2.Visible = true;
                        grpSort2.Text = "Report Type";
                        rbnSort2_1.Visible = true;
                        rbnSort2_1.Text = "Ason Date";
                        rbnSort2_2.Visible = true;
                        rbnSort2_2.Text = "Periodic";
                        rbnSort2_1.Checked = true;
                        //
                        mskFromDate.Select();
                        break;

                    case J_Reports.InvoiceStatusDetail:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--

                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 81;
                        grpSort.Width = 100;
                        grpSort.Visible = true;
                        grpSort.Text = "Party Type";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Sundry";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "All";
                        rbnSort1.Checked = true;
                        //--
                        grpSort1.Location = new Point(700, 42);
                        grpSort1.Width = 130;
                        grpSort.Height = 81;
                        grpSort1.Visible = true;
                        grpSort1.Text = "Collection";
                        rbnSort1_1.Visible = true;
                        rbnSort1_1.Text = "Pending Only";
                        rbnSort1_2.Visible = true;
                        rbnSort1_2.Text = "All";
                        rbnSort1_2.Checked = true;
                        //--
                        //--
                        grpSort2.Location = new Point(832, 42);
                        grpSort2.Width = 170;
                        grpSort2.Height = 81;
                        grpSort2.Visible = true;
                        grpSort2.Text = "Report Type";
                        rbnSort2_1.Visible = true;
                        rbnSort2_1.Text = "Ason Date";
                        rbnSort2_2.Visible = true;
                        rbnSort2_2.Text = "Periodic";
                        rbnSort2_1.Checked = true;
                        //
                        mskFromDate.Select();
                        break;
                    case J_Reports.PaymentStatusDetail:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--

                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 81;
                        grpSort.Width = 150;
                        grpSort.Visible = true;
                        grpSort.Text = "Collection";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Pending Only";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "All";
                        rbnSort2.Checked = true;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.PaymentStatusSummary:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--

                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 81;
                        grpSort.Width = 150;
                        grpSort.Visible = true;
                        grpSort.Text = "Collection";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Pending Only";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "All";
                        rbnSort2.Checked = true;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.ReconciliationDetail:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.DailyCollectionSummary:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.UnreconciledCollectionList:
                        lblTitle.Text = strFormText;

                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1,1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--
                        mskAsOnDate.Select();
                        break;
                    case J_Reports.UnknownPaymentList:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.AdjustmentRegister:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.ReconciliationStatement:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;

                    case J_Reports.PeriodicCollectionSummary:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 120;
                        grpSort.Width = 200;
                        grpSort.Visible = true;
                        grpSort.Text = "Collection Type";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "All";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "Unknown Collection";
                        rbnSort3.Visible = true;
                        rbnSort3.Text = "Old Collection";
                        rbnSort4.Visible = true;
                        rbnSort4.Text = "Current Collection";
                        rbnSort5.Visible = true;
                        rbnSort5.Text = "Advance Collection";
                        rbnSort1.Checked = true;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.SundryPartyOutstandingSummary:
                        lblTitle.Text = strFormText;

                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--
                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 80;
                        grpSort.Width = 200;
                        grpSort.Visible = true;
                        grpSort.Text = "Payment Type";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Excluding CC AVENUE";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "ALL";
                        //--
                        mskAsOnDate.Select();
                        break;
                    case J_Reports.SundryPartyCollectionMiniStatement:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.OutstandingCumUnknown:
                        lblTitle.Text = strFormText;
                        //--
                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskAsOnDate.Select();
                        break;
                    case J_Reports.AdvanceCollectionRegister:
                        lblTitle.Text = strFormText;

                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);

                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;

                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        lblComboTitle2.Visible = true;
                        lblComboTitle2.Text = "Payment Type";
                        cmbCombo2.Visible = true;
                        //--PAYMENT TYPE
                        strSQL = " SELECT PAYMENT_TYPE_ID," +
                                 "        PAYMENT_TYPE_DESCRIPTION" +
                                 " FROM   MST_PAYMENT_TYPE " +
                                 " WHERE  INACTIVE_FLAG = 0 " +
                                 " ORDER BY PAYMENT_TYPE_DESCRIPTION ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo2) == false) return;
                        //--
                        lblComboTitle3.Visible = true;
                        lblComboTitle3.Text = "Bank";
                        cmbCombo3.Visible = true;
                        //--
                        strSQL = "SELECT BANK_ID," +
                                 "       BANK_NAME " +
                                 "FROM   MST_BANK " +
                                 "ORDER BY BANK_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo3) == false) return;
                        //--
                        lblComboTitle4.Visible = true;
                        lblComboTitle4.Text = "Collection Type";
                        cmbCombo4.Visible = true;
                        //--
                        cmbCombo4.Items.Add("Old");
                        cmbCombo4.Items.Add("Advance");
                        cmbCombo4.Items.Add("Current");
                        cmbCombo4.SelectedIndex = 0; 
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.PaymentTypeWiseOutstandingSummary:
                        lblTitle.Text = strFormText;
                        //--
                        grpAsOnDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskAsOnDate.Select();
                        break;

                    case J_Reports.CategoryWiseVATCSTSale:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    //--
                    case J_Reports.TallyReconciliation:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.DespatchStatusReport:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        grpSort.Location = new Point(600, 42);
                        grpSort.Height = 80;
                        grpSort.Width = 200;
                        grpSort.Visible = true;
                        grpSort.Text = "Report Type";
                        rbnSort1.Visible = true;
                        rbnSort1.Text = "Pending";
                        rbnSort2.Visible = true;
                        rbnSort2.Text = "All";
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--
                        mskFromDate.Select();
                        break;
                    case J_Reports.EmailCheckList:
                        lblTitle.Text = strFormText;
                        //--
                        grpFromToDate.Visible = true;
                        grpCombo.Visible = true;
                        grpCombo.Location = new Point(290, 42);
                        //--
                        lblComboTitle1.Visible = true;
                        lblComboTitle1.Text = "Company";
                        cmbCombo1.Visible = true;
                        //--
                        strSQL = "SELECT COMPANY_ID," +
                                 "       COMPANY_NAME " +
                                 "FROM   MST_COMPANY " +
                                 "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                                 "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                                 "ORDER BY COMPANY_NAME ";
                        if (dmlService.J_PopulateComboBox(strSQL, ref cmbCombo1, 1) == false) return;
                        //--

                        mskFromDate.Select();
                        break;
                    #endregion


                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ValidateFields
        public bool ValidateFields(J_Reports enmReport)
        {
            try
            {
                switch (enmReport)
                {
                    case J_Reports.Listing:
                        break;

                    
                    case J_Reports.Master:
                        break;

                    
                    case J_Reports.Transaction:

                        break;


                    case J_Reports.Invoice:
                        if (cmbCombo1.SelectedIndex <= 0)
                        {
                            cmnService.J_UserMessage("Please select Company.");
                            cmbCombo1.Select();
                            return false;
                        }
                        if (cmnService.J_GenerateDataGridViewSelectedId(grdvDescription) == "")
                        {
                            cmnService.J_UserMessage("Please select Invoice No .");
                            grdvDescription.Select();
                            return false;
                        }

                        break;
                    case J_Reports.InvoiceRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.ItemWiseInvoiceSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.TaxRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.PartyCategoryWiseRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.AccountsEntryDateWiseRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }
                        
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.ListOfUnknownDeposits:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.BillWiseOutstanding:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.PendingCCAvenueTransactions:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.BankStDateWiseRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.DetailsCollectionType:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;

                    case J_Reports.SalesDistribution:
                        //Added by Shrey Kejriwal on 21-01-2014
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskFromDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"From Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        else
                        {
                            if (dtservice.J_IsDateValid(mskToDate) == false)
                            {
                                cmnService.J_UserMessage("Please enter valid \"To Date\"");
                                mskFromDate.Select();
                                return false;
                            }
                        }

                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PartyCategoryWiseMonthlySalesSummary:
                        //Added  By Dhrub on 28/04/2014
                        if (dtservice.J_IsMonthYearValid(mskFromMonth) == false)
                        {
                            cmnService.J_UserMessage("Invalid From Month/Year.");
                            mskFromMonth.Select();
                            return false;
                        }
                        if (dtservice.J_IsMonthYearValid(mskToMonth) == false)
                        {
                            cmnService.J_UserMessage("Invalid To Month/Year.");
                            mskToMonth.Select();
                            return false;
                        }

                        if (dtservice.J_ConvertToIntYYYYMM(mskFromMonth) > dtservice.J_ConvertToIntYYYYMM(mskToMonth))
                        {
                            cmnService.J_UserMessage("\"From Date\" cannot be greater than \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }

                        break;
                    case J_Reports.PartyCategoryWiseMonthlySale:

                        //Added  By Dhrub on 29/04/2014
                        if (dtservice.J_IsMonthYearValid(mskFromMonth) == false)
                        {
                            cmnService.J_UserMessage("Invalid From Month/Year.");
                            mskFromMonth.Select();
                            return false;
                        }
                        if (dtservice.J_IsMonthYearValid(mskToMonth) == false)
                        {
                            cmnService.J_UserMessage("Invalid To Month/Year.");
                            mskToMonth.Select();
                            return false;
                        }

                        if (dtservice.J_ConvertToIntYYYYMM(mskFromMonth) > dtservice.J_ConvertToIntYYYYMM(mskToMonth))
                        {
                            cmnService.J_UserMessage("\"From Date\" cannot be greater than \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }

                        break;
                    case J_Reports.AccountReconciliation:

                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate,J_ShowMessage.NO) == true)
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskFromDate.Select();
                            return false;
                        }

                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskFromDate.Select();
                            return false;
                        }

                        if (dtservice.J_IsDateGreater(strFaYearBegDate, ref mskFromDate, "", "", "", J_ShowMessage.NO) == false
                          || dtservice.J_IsDateGreater(ref mskFromDate, strFaYearEndDate, "", "", "", J_ShowMessage.NO) == false)
                        {
                            cmnService.J_UserMessage("AsOn date should be within FA Year date." +
                                "\n\nBegining Date : " + strFaYearBegDate +
                                "\nEnding Date    : " + strFaYearEndDate);
                            mskFromDate.Select();
                            return false;
                        }

                        break;
                    case J_Reports.SundryPartySale:
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select FA Year.");
                            cmbCombo1.Select();
                            return false;
                        }
                        if (cmbCombo2.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name.");
                            cmbCombo2.Select();
                            return false;
                        }
                        break;
                    case J_Reports.SundryPartySalesCumOutstanding:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskFromDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        break;
                    case J_Reports.SundryPartyReconcilation:
                        //Added  By Dhrub on 28/04/2014
                        if (dtservice.J_IsMonthYearValid(mskFromMonth) == false)
                        {
                            cmnService.J_UserMessage("Invalid From Month/Year.");
                            mskFromMonth.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PartyListSales:
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Item.");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.OutstandingPayments:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.UnknownCollectionEntry:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.KnownCollectionEntry:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.InvoicePaymentStatus:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.InvoiceStatusSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PaymentStatusDetail:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PaymentStatusSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.ReconciliationDetail:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.DailyCollectionSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.UnreconciledCollectionList:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.UnknownPaymentList:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.AdjustmentRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.ReconciliationStatement:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PeriodicCollectionSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.SundryPartyOutstandingSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.SundryPartyCollectionMiniStatement:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.AdvanceCollectionRegister:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.PaymentTypeWiseOutstandingSummary:
                        if (dtservice.J_IsBlankDateCheck(ref mskAsOnDate))
                        {
                            cmnService.J_UserMessage("Please enter \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskAsOnDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"AsOn Date\"");
                            mskAsOnDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.CategoryWiseVATCSTSale:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;


                    case J_Reports.DespatchStatusReport:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                    case J_Reports.EmailCheckList:
                        if (dtservice.J_IsBlankDateCheck(ref mskFromDate))
                        {
                            cmnService.J_UserMessage("Please enter \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsBlankDateCheck(ref mskToDate))
                        {
                            cmnService.J_UserMessage("Please enter \"To Date\"");
                            mskToDate.Select();
                        }
                        if (dtservice.J_IsDateValid(mskFromDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"From Date\"");
                            mskFromDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateValid(mskToDate) == false)
                        {
                            cmnService.J_UserMessage("Please enter valid \"To Date\"");
                            mskToDate.Select();
                            return false;
                        }
                        if (dtservice.J_IsDateGreater(ref mskFromDate, ref mskToDate) == false)
                        {
                            mskToDate.Select();
                            return false;
                        }
                        if (cmbCombo1.Text == "")
                        {
                            cmnService.J_UserMessage("Please select Company name");
                            cmbCombo1.Select();
                            return false;
                        }
                        break;
                }
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion
        
        #region SetProgressBar
        private void SetProgressBar()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i != 90)
                    ((ToolStripProgressBar)J_Var.frmMain.stbMessage.Items[(int)J_PanelIndex.e06_ProgressBar]).Value = i;
                else
                    break;
                Thread.Sleep(100);
            }
        }
        #endregion

        #region ThreadTask
        private void ThreadTask()
        {
            Invoke(new MyDelegate(SetProgressBar));
        }
        #endregion


        #endregion

        #region USER DEFINED FUNCTION FOR REPORT GENERATION

        #region Masters

        #region PrintUserMaster
        private void PrintUserMaster()
        {
            crUserMaster rptUserMaster = new crUserMaster();
            rptcls = (ReportClass)rptUserMaster;
            
            // (0) Data Value in DataTable
            // (1) Data Type in DataTable
            // (2) Display Text in Front-end
            // (3) Display Text Type
            string[,] strCaseEndMatrix = {{"=0", "N", "", "T"},
							              {"=1", "N", "Inactive", "T"}};
            
            strQueryString = "SELECT USER_ID," +
                             "       USER_CODE," +
                             "       DISPLAY_NAME," +
                             "       LOGIN_ID," +
                             "       " + cmnService.J_SQLDBFormat("USER_CATEGORY", J_SQLColFormat.Case_End, strCaseEndMatrix) + " AS USER_CATEGORY " +
                             "FROM   MST_USER " +
                             "WHERE  " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " <> 'ADMIN' " +
                             "AND    BRANCH_ID = " + J_Var.J_pBranchId + " " +
                             "ORDER BY DISPLAY_NAME ";
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }
        #endregion

        #region PrintFAYearMaster
        private void PrintFAYearMaster()
        {
            crFAYearMaster rptFAYearMaster = new crFAYearMaster();
            rptcls = (ReportClass)rptFAYearMaster;

            strQueryString = "SELECT FAYEAR_ID," +
                             "       FA_BEG_DATE," +
                             "       FA_END_DATE," +
                             "       FA_LOCK_DATE " +
                             "FROM   MST_FAYEAR " +
                             "ORDER BY FA_BEG_DATE DESC ";
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }
        #endregion

        #region PrintCompanyMaster
        private void PrintCompanyMaster()
        {
            crCompanyMaster rptCompanyMaster = new crCompanyMaster();
            rptcls = (ReportClass)rptCompanyMaster;

            strQueryString = "SELECT COMPANY_ID," +
                             "       COMPANY_NAME," +
                             "       PAN," +
                             "       ADDRESS1," +
                             "       VAT_NO," +
                             "       CST_NO," +
                             "       SERVICE_TAX_NO " +
                             "FROM   MST_COMPANY " +
                             "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                             "ORDER BY COMPANY_NAME";
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }

        #endregion

        #region PrintPartyMaster
        private void PrintPartyMaster()
        {
            crPartyMaster rptPartyMaster = new crPartyMaster();
            rptcls = (ReportClass)rptPartyMaster;

            strQueryString = "SELECT PARTY_ID," +
                             "       PARTY_NAME," +
                             "       ADDRESS1," +
                             "       CITY," +
                             "       PIN," +
                             "       CONTACT_PERSON," +
                             "       MOBILE_NO," +
                             "       PHONE_NO," +
                             "       EMAIL_ID," +
                             "       FAX " +
                             "FROM   MST_PARTY " +
                             "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                             "ORDER BY PARTY_NAME";
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }

        #endregion

        #region PrintItemMaster
        private void PrintItemMaster()
        {
            crItemMaster rptItemMaster = new crItemMaster();
            rptcls = (ReportClass)rptItemMaster;

            strQueryString = "SELECT MST_ITEM.ITEM_ID         AS ITEM_ID," +
                             "       MST_COMPANY.COMPANY_ID   AS COMPANY_ID," +
                             "       MST_ITEM.ITEM_NAME       AS ITEM_NAME," +
                             "       MST_COMPANY.COMPANY_NAME AS COMPANY_NAME," +
                             "       MST_ITEM.RATE            AS RATE," +
                             "       MST_ITEM.UNIT            AS UNIT " +
                             "FROM   MST_ITEM," +
                             "       MST_COMPANY " +
                             "WHERE  MST_ITEM.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                             "AND    MST_ITEM.BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                             "ORDER BY MST_ITEM.ITEM_NAME";

            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }

        #endregion

        #region PrintInvoiceSeriesMaster
        private void PrintInvoiceSeriesMaster()
        {
            crInvoiceSeriesMaster rptInvoiceSeriesMaster = new crInvoiceSeriesMaster();
            rptcls = (ReportClass)rptInvoiceSeriesMaster;

            strQueryString = "SELECT MST_INVOICE_SERIES.INVOICE_SERIES_ID   AS INVOICE_SERIES_ID," +
                             "       MST_COMPANY.COMPANY_ID                 AS COMPANY_ID," +
                             "       MST_INVOICE_SERIES.PREFIX              AS PREFIX," +
                             "       MST_COMPANY.COMPANY_NAME               AS COMPANY_NAME," +
                             "       MST_INVOICE_SERIES.START_NO            AS START_NO," +
                             "       MST_INVOICE_SERIES.LAST_NO             AS LAST_NO," +
                             "       " + cmnService.J_SQLDBFormat("MST_INVOICE_SERIES.LAST_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + "  AS LAST_DATE," +
                             "       MST_INVOICE_SERIES.HEADER_DISPLAY_TEXT AS HEADER_DISPLAY_TEXT " +
                             "FROM   MST_INVOICE_SERIES," +
                             "       MST_COMPANY " +
                             "WHERE  MST_INVOICE_SERIES.COMPANY_ID = MST_COMPANY.COMPANY_ID " +
                             "AND    MST_INVOICE_SERIES.BRANCH_ID  = " + J_Var.J_pBranchId + " ";
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }

        #endregion


        #endregion

        #region Transactions

        #region PrintInvoice
        private void PrintInvoice()
        {
            crInvoice rptInvoice = new crInvoice();
            rptcls = (ReportClass)rptInvoice;

            // Temp Table Name
            string strTableName = "TMP_InvoiceType_" + J_Var.J_pUserId + "_" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date) + "_" + string.Format("{0:HHmmss}", System.DateTime.Now);

            // Create the report temp table
            strSQL = "CREATE TABLE " + strTableName + "(" +
                     "             " + cmnService.J_GetDataType("INVOICE_TYPE_ID", J_ColumnType.Long) + "," +
                     "             " + cmnService.J_GetDataType("INVOICE_TYPE", J_ColumnType.String, 50) + "," +
                     "             " + cmnService.J_GetDataType("PRINT_TYPE", J_ColumnType.Integer) + ")";
            if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false) return;

            // print type
            int intPrintType = 0;
            if (rbnSort1_2.Checked == true) intPrintType = 1;

            // invoice type
            //Added by Shrey Kejriwal on 05/08/2011
            if (chkBox1.Checked == true)
            {
                strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE                                                    , PRINT_TYPE) " +
                         "                          VALUES (1              , '" + cmnService.J_ReplaceQuote("**(Original - Buyer's Copy)**") + "', " + intPrintType + ")";
                if (dmlService.J_ExecSql(strSQL) == false) return;
            }

            if (chkBox2.Checked == true)
            {
                strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE      , PRINT_TYPE) " +
                         "                          VALUES (2              , '(Duplicate Copy)', " + intPrintType + ")";
                if (dmlService.J_ExecSql(strSQL) == false) return;
            }

            if (chkBox3.Checked == true)
            {
                strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE     , PRINT_TYPE) " +
                         "                          VALUES (3              , '(Office Copy)', " + intPrintType + ")";
                if (dmlService.J_ExecSql(strSQL) == false) return;
            }
            //--
            #region COMMENTED
            //Commented by Shrey Kejriwal on 05/08/2011

            //if (rbnSort1.Checked == true)
            //{
            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE                                                    , PRINT_TYPE) " +
            //             "                          VALUES (1              , '" + cmnService.J_ReplaceQuote("(Original - Buyer's Copy)") + "', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;

            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE      , PRINT_TYPE) " +
            //             "                          VALUES (2              , '(Duplicate Copy)', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;

            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE     , PRINT_TYPE) " +
            //             "                          VALUES (3              , '(Office Copy)', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;
            //}
            //else if (rbnSort2.Checked == true)
            //{
            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE                                                    , PRINT_TYPE) " +
            //             "                          VALUES (1              , '" + cmnService.J_ReplaceQuote("(Original - Buyer's Copy)") + "', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;
            //}
            //else if (rbnSort3.Checked == true)
            //{
            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE      , PRINT_TYPE) " +
            //             "                          VALUES (2              , '(Duplicate Copy)', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;
            //}
            //else if (rbnSort4.Checked == true)
            //{
            //    strSQL = "INSERT INTO " + strTableName + " (INVOICE_TYPE_ID, INVOICE_TYPE     , PRINT_TYPE) " +
            //             "                          VALUES (3              , '(Office Copy)', " + intPrintType + ")";
            //    if (dmlService.J_ExecSql(strSQL) == false) return;
            //}
            #endregion
            //--
            strQueryString = "SELECT " + strTableName + ".INVOICE_TYPE_ID           AS INVOICE_TYPE_ID," +
                             "       " + strTableName + ".INVOICE_TYPE              AS INVOICE_TYPE," +
                             "       " + strTableName + ".PRINT_TYPE                AS PRINT_TYPE," +
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
                             "       MST_COMPANY.PAN                                AS PAN," +
                             "       MST_COMPANY.CIN_NO                             AS CIN_NO," +
                             "       TRN_INVOICE_HEADER.DELIVERY_MARK               AS DELIVERY_MARK," +
                             "       MST_COMPANY.BANK_DETAIL1                       AS BANK_DETAIL1," +
                             "       MST_COMPANY.BANK_DETAIL2                       AS BANK_DETAIL2," +
                             "       MST_COMPANY.BANK_DETAIL3                       AS BANK_DETAIL3," +
                             "       MST_COMPANY.BANK_DETAIL4                       AS BANK_DETAIL4," +
                             "       MST_COMPANY.BANK_DETAIL5                       AS BANK_DETAIL5 " +
                             "FROM   TRN_INVOICE_HEADER," +
                             "       TRN_INVOICE_DETAIL," +
                             "       MST_INVOICE_SERIES," +
                             "       MST_COMPANY," +
                             "       MST_PARTY," +
                             "       MST_ITEM," +
                             "       " + strTableName + " " +
                             "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID  = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                             "AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID  = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                             "AND    TRN_INVOICE_HEADER.COMPANY_ID         = MST_COMPANY.COMPANY_ID " +
                             "AND    TRN_INVOICE_HEADER.PARTY_ID           = MST_PARTY.PARTY_ID " +
                             "AND    TRN_INVOICE_DETAIL.ITEM_ID            = MST_ITEM.ITEM_ID " +
                             "AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID IN(" + cmnService.J_GenerateDataGridViewSelectedId(grdvDescription) + ") " +
                             "AND    TRN_INVOICE_HEADER.BRANCH_ID          = " + J_Var.J_pBranchId + " " +
                             "ORDER BY " + strTableName + ".INVOICE_TYPE_ID," +
                             "       TRN_INVOICE_HEADER.INVOICE_NO";
            
            // SUB REPORTS
            // FOR SUMMARY OF TAX DETAILS
            string strSubRptTaxDetails ="SELECT MST_TAX.TAX_ID                    AS TAX_ID," +
                                        "       MST_TAX.TAX_DESC                  AS TAX_DESC," +      
                                        "       TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                                        "       TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE," +
                                        "       TRN_INVOICE_TAX.TAX_AMOUNT        AS TAX_AMOUNT " +
                                        "FROM   MST_TAX," +
                                        "       TRN_INVOICE_TAX " +        
                                        "WHERE  MST_TAX.TAX_ID    = TRN_INVOICE_TAX.TAX_ID " +
                                        "AND    MST_TAX.BRANCH_ID = " + J_Var.J_pBranchId + " " +
                                        "ORDER BY TRN_INVOICE_TAX.INVOICE_TAX_ID";
            // POPULATE & DISPLAY SUB REPORT
            rptcls.OpenSubreport("crSubRptTaxSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSubRptTaxDetails).Tables[0]);

            // report is executed
            DataSet ds = dmlService.J_ExecSqlReturnDataSet(strQueryString);
            if (ds == null) return;
            //
            PictureObject objBlobFieldObject;
            objBlobFieldObject = (PictureObject)rptcls.ReportDefinition.Sections[2].ReportObjects["imgSignature"];
            objBlobFieldObject.ObjectFormat.EnableSuppress = true;
            //
            if(rbnSort2_2.Checked == true)
                objBlobFieldObject.ObjectFormat.EnableSuppress = false;            
            //
            rptService.J_PreviewReport(ref rptcls, this, ds, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
            // Drop the report temp table
            if (dmlService.J_ExecSql("DROP TABLE " + strTableName) == false) return;


            strSQL = "";
        }

        #endregion

        #region PrintInvoiceRegister
        private void PrintInvoiceRegister()
        {

            // Added by Ripan Paul on 05-08-2011
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            
            //Commented by Shrey Kejriwal on 29-04-2013
            //string[,] strCaseEnd_VAT_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.VAT_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.VAT_ID, "F", "0", "N"}};

            //Added by Shrey Kejriwal on 29-04-2013
            string[,] strCaseEnd_VAT_AMOUNT = {{"MST_TAX.TAX_TYPE = 'V'", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'V'", "F", "0", "N"}};

            //COMMENTED BY SHREY KEJRIWAL ON 25/06/2012
            //string[,] strCaseEnd_CST_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.CST_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            //ADDED BY SHREY KEJRIWAL ON 25/06/2012
            //string[,] strCaseEnd_CST_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID IN (" + BS_TaxId.CST_ID + ", " + BS_TaxId.CST_ID_FORM_C + ")", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            //Added by Shrey Kejriwal on 29-04-2013
            string[,] strCaseEnd_CST_AMOUNT = {{"MST_TAX.TAX_TYPE = 'C'", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'C'", "F", "0", "N"}};


            string[,] strCaseEnd_VAT_SALE_AMOUNT = {{"INV.VAT_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.VAT_AMOUNT = 0", "F", "0", "N"}};

            string[,] strCaseEnd_CST_SALE_AMOUNT = {{"INV.CST_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.CST_AMOUNT = 0", "F", "0", "N"}};

            
            //Added by Shrey Kejriwal on 17/08/2011 
            //Report Query
            strQueryString = "SELECT INV.COMPANY_NAME                     AS COMPANY_NAME," +
                             "       INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID," +
                             "       INV.INVOICE_DATE                     AS INVOICE_DATE," +
                             "       INV.INVOICE_NO                       AS INVOICE_NO," +
                             "       INV.PARTY_ID                         AS PARTY_ID," +
                             "       INV.PARTY_NAME                       AS PARTY_NAME," +
                             "       INV.ITEM_ID                          AS ITEM_ID," +
                             "       INV.ITEM_NAME                        AS ITEM_NAME," +
                             "       INV.QUANTITY                         AS QUANTITY," +
                             "       INV.RATE                             AS RATE," +
                             "       INV.AMOUNT                           AS AMOUNT," +
                             "       INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT," +
                             "       INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT," +
                             "       INV.DISCOUNT_RATE                    AS DISCOUNT_RATE," +
                             "       INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT," +
                             "       INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT," +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS VAT_SALE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CST_SALE_AMOUNT," +
                             "       INV.VAT_AMOUNT                       AS VAT_AMOUNT," +
                             "       INV.CST_AMOUNT                       AS CST_AMOUNT," +
                             "       INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT," +
                             "       INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX," +
                             "       INV.ADDITIONAL_COST                  AS ADDITIONAL_COST," +
                             "       INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST," +
                             "       INV.ROUNDED_OFF                      AS ROUNDED_OFF," +
                             "       INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF," +
                             "       INV.NET_AMOUNT                       AS NET_AMOUNT," +
                             "       INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS," +
                             "       INV.REMARKS                          AS REMARKS " +
                             "FROM  (SELECT INV.COMPANY_NAME                     AS COMPANY_NAME, " +
                             "              INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID, " +
                             "              " + cmnService.J_SQLDBFormat("INV.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE,  " +
                             "              INV.INVOICE_NO                       AS INVOICE_NO,  " +
                             "              INV.PARTY_ID                         AS PARTY_ID, " +
                             "              INV.PARTY_NAME                       AS PARTY_NAME, " +
                             "              INV.ITEM_ID                          AS ITEM_ID,  " +
                             "              INV.ITEM_NAME                        AS ITEM_NAME,  " +
                             "              INV.QUANTITY                         AS QUANTITY,  " +
                             "              INV.RATE                             AS RATE,  " +
                             "              INV.AMOUNT                           AS AMOUNT, " +
                             "              INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT, " +
                             "              INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT,  " +
                             "              INV.DISCOUNT_RATE                    AS DISCOUNT_RATE,  " +
                             "              INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT,  " +
                             "              INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT,  " +
                             "              " + cmnService.J_SQLDBFormat("TAX.VAT_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS VAT_AMOUNT, " +
                             "              " + cmnService.J_SQLDBFormat("TAX.CST_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CST_AMOUNT, " +
                             "              INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT,  " +
                             "              INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX,  " +
                             "              INV.ADDITIONAL_COST                  AS ADDITIONAL_COST,  " +
                             "              INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                             "              INV.ROUNDED_OFF                      AS ROUNDED_OFF,  " +
                             "              INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF,  " +
                             "              INV.NET_AMOUNT                       AS NET_AMOUNT,  " +
                             "              INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS,  " +
                             "              INV.REMARKS                          AS REMARKS " +
                             "       FROM  (SELECT MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
                             "                     MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
                             "                     TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID, " +
                             "                     TRN_INVOICE_HEADER.BRANCH_ID                   AS BRANCH_ID,  " +
                             "                     TRN_INVOICE_HEADER.FAYEAR_ID                   AS FAYEAR_ID,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO,  " +
                             "                     MST_PARTY.PARTY_ID                             AS PARTY_ID, " +
                             "                     MST_PARTY.PARTY_NAME                           AS PARTY_NAME, " +
                             "                     MST_ITEM.ITEM_ID                               AS ITEM_ID,  " +
                             "                     MST_ITEM.ITEM_NAME                             AS ITEM_NAME,  " +
                             "                     TRN_INVOICE_DETAIL.QUANTITY                    AS QUANTITY,  " +
                             "                     TRN_INVOICE_DETAIL.RATE                        AS RATE,  " +
                             "                     TRN_INVOICE_DETAIL.AMOUNT                      AS AMOUNT, " +
                             "                     TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT,  " +
                             "                     TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX,  " +
                             "                     TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                             "                     TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF,  " +
                             "                     TRN_INVOICE_HEADER.ADDITIONAL_COST + TRN_INVOICE_HEADER.ROUNDED_OFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF ," +
                             "                     TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS,  " +
                             "                     TRN_INVOICE_HEADER.REMARKS                     AS REMARKS " +
                             "              FROM   TRN_INVOICE_HEADER, " +
                             "                     TRN_INVOICE_DETAIL, " +
                             "                     MST_COMPANY, " +
                             "                     MST_INVOICE_SERIES, " +
                             "                     MST_PARTY, " +
                             "                     MST_ITEM " +
                             "              WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID        = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                             "              AND    TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID " +
                             "              AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                             "              AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID " +
                             "              AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID) AS INV " +
                             "       LEFT JOIN " +
                             "             (SELECT TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID, " +
                             "                     TRN_INVOICE_TAX.TAX_ID            AS TAX_ID, " +
                             "                     MST_TAX.TAX_DESC                  AS TAX_DESC, " +
                             "                     TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE, " +
                             "                     " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_AMOUNT, J_SQLColFormat.Case_End) + " AS VAT_AMOUNT," +
                             "                     " + cmnService.J_SQLDBFormat(strCaseEnd_CST_AMOUNT, J_SQLColFormat.Case_End) + " AS CST_AMOUNT " +
                             "                     FROM   TRN_INVOICE_TAX, " +
                             "                     MST_TAX " +
                             "              WHERE  TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID ) AS TAX " +
                             "       ON     INV.INVOICE_HEADER_ID = TAX.INVOICE_HEADER_ID " +
                             "       WHERE  INV.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
                             "       AND    INV.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
                             "       AND    INV.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                             "       AND    INV.BRANCH_ID         = " + J_Var.J_pBranchId + " ";

                             //Commented BY DHRUB ON 01/04/2014 FOR Finding the data into previous FA year 
                             //"       AND    INV.FAYEAR_ID         = " + J_Var.J_pFAYearId + " ";

            if (cmbCombo2.Text != "")
                strQueryString = strQueryString + "AND INV.INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + " ) AS INV ";
            else
                strQueryString = strQueryString + " ) AS INV ";

            strQueryString = strQueryString + "ORDER BY INV.INVOICE_DATE, INV.INVOICE_NO ";

            crInvoiceRegister rptInvoiceRegister = new crInvoiceRegister();
            rptcls = (ReportClass)rptInvoiceRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            
            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintItemWiseInvoiceSummary
        private void PrintItemWiseInvoiceSummary()
        {
            //Added by Shrey Kejriwal on 25/08/2011
            // Create Temp Table Name
            string strTmpTableName = "TMP_ItemWiseInvoiceSummary_" + J_Var.J_pUserId + "_" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date) + "_" + string.Format("{0:HHmmss}", System.DateTime.Now);


            // Added by Ripan Paul on 05-08-2011
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            string[,] strCaseEnd_VAT_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.VAT_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.VAT_ID, "F", "0", "N"}};

            string[,] strCaseEnd_CST_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.CST_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            string[,] strCaseEnd_VAT_5_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.VAT_ID_5, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.VAT_ID, "F", "0", "N"}};

            string[,] strCaseEnd_CST_5_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.CST_ID_5, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            string[,] strCaseEnd_CST_FORMC_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.CST_ID_FORM_C, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID_FORM_C, "F", "0", "N"}};


            string[,] strCaseEnd_VAT_SALE_AMOUNT = {{"INV.VAT_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.VAT_AMOUNT = 0", "F", "0", "N"}};

            string[,] strCaseEnd_CST_SALE_AMOUNT = {{"INV.CST_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.CST_AMOUNT = 0", "F", "0", "N"}};
            
            string[,] strCaseEnd_CST_FORMC_SALE_AMOUNT = {{"INV.CST_FORMC_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                              {"INV.CST_FORMC_AMOUNT = 0", "F", "0", "N"}};

            string[,] strCaseEnd_VAT_5_SALE_AMOUNT = {{"INV.VAT_5_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                     {"INV.VAT_5_AMOUNT = 0", "F", "0", "N"}};

            string[,] strCaseEnd_CST_5_SALE_AMOUNT = {{"INV.CST_5_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                     {"INV.CST_5_AMOUNT = 0", "F", "0", "N"}};


            // begin the transaction
            dmlService.J_BeginTransaction();

            // Create the report temp table
            strSQL = "CREATE TABLE " + strTmpTableName + "(" +
                     "             " + cmnService.J_GetDataType("COMPANY_NAME", J_ColumnType.String, 50) + "," +
                     "             " + cmnService.J_GetDataType("INVOICE_HEADER_ID", J_ColumnType.Long) + "," +
                     "             " + cmnService.J_GetDataType("INVOICE_DATE", J_ColumnType.DateTime) + "," +
                     "             " + cmnService.J_GetDataType("INVOICE_NO", J_ColumnType.String, 50) + "," +
                     "             " + cmnService.J_GetDataType("PARTY_ID", J_ColumnType.Long) + "," +
                     "             " + cmnService.J_GetDataType("PARTY_NAME", J_ColumnType.String, 200) + "," +
                     "             " + cmnService.J_GetDataType("ITEM_ID", J_ColumnType.Long) + "," +
                     "             " + cmnService.J_GetDataType("ITEM_NAME", J_ColumnType.String, 50) + "," +
                     "             " + cmnService.J_GetDataType("QUANTITY", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("RATE", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOTAL_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("DISCOUNT_TEXT", J_ColumnType.String, 200) + "," +
                     "             " + cmnService.J_GetDataType("DISCOUNT_RATE", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("DISCOUNT_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("AMOUNT_WITH_DISCOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("VAT_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("VAT_5_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_5_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_FORMC_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("VAT_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("VAT_5_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_5_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("CST_FORMC_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TAX_TOTAL_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("AMOUNT_WITH_TAX", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("ADDITIONAL_COST", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("AMOUNT_WITH_ADDITIONAL_COST", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("ROUNDED_OFF", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("SUM_ADDITIONAL_DISCOUNT_ROUNDOFF", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("NET_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("NET_AMOUNT_INWORDS", J_ColumnType.String, 200) + "," +
                     "             " + cmnService.J_GetDataType("REMARKS", J_ColumnType.String, 200) + "," +
                     "             " + cmnService.J_GetDataType("TOT_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_DISCOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_VAT_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_VAT_5_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_5_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_FORMC_SALE_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_VAT_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_VAT_5_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_5_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_CST_FORMC_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_GROSS_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_ADDITIONAL_COST", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_ROUNDED_OFF", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_ADD_COST_ROUND_OFF_AMOUNT", J_ColumnType.Double) + "," +
                     "             " + cmnService.J_GetDataType("TOT_NET_AMOUNT", J_ColumnType.Double) + ")";

            if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false) return;

            #region commented
            //Added by Shrey Kejriwal on 17/08/2011 
            //Report Query
            //strSQL = "INSERT INTO " + strTmpTableName + " (" +
            //                 "       COMPANY_NAME," +
            //                 "       INVOICE_HEADER_ID," +
            //                 "       INVOICE_DATE," +
            //                 "       INVOICE_NO," +
            //                 "       PARTY_ID," +
            //                 "       PARTY_NAME," +
            //                 "       ITEM_ID," +
            //                 "       ITEM_NAME," +
            //                 "       QUANTITY," +
            //                 "       RATE," +
            //                 "       AMOUNT," +
            //                 "       TOTAL_AMOUNT," +
            //                 "       DISCOUNT_TEXT," +
            //                 "       DISCOUNT_RATE," +
            //                 "       DISCOUNT_AMOUNT," +
            //                 "       AMOUNT_WITH_DISCOUNT," +
            //                 "       VAT_AMOUNT," +
            //                 "       CST_AMOUNT," +
            //                 "       TAX_TOTAL_AMOUNT," +
            //                 "       AMOUNT_WITH_TAX," +
            //                 "       ADDITIONAL_COST," +
            //                 "       AMOUNT_WITH_ADDITIONAL_COST," +
            //                 "       ROUNDED_OFF," +
            //                 "       SUM_ADDITIONAL_DISCOUNT_ROUNDOFF," +
            //                 "       NET_AMOUNT," +
            //                 "       NET_AMOUNT_INWORDS," +
            //                 "       REMARKS)" +
            //                 "SELECT INV.COMPANY_NAME                     AS COMPANY_NAME, " +
            //                 "       INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID, " +
            //                 "       " + cmnService.J_SQLDBFormat("INV.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE,  " +
            //                 "       INV.INVOICE_NO                       AS INVOICE_NO,  " +
            //                 "       INV.PARTY_ID                         AS PARTY_ID, " +
            //                 "       INV.PARTY_NAME                       AS PARTY_NAME, " +
            //                 "       INV.ITEM_ID                          AS ITEM_ID,  " +
            //                 "       INV.ITEM_NAME                        AS ITEM_NAME,  " +
            //                 "       INV.QUANTITY                         AS QUANTITY,  " +
            //                 "       INV.RATE                             AS RATE,  " +
            //                 "       INV.AMOUNT                           AS AMOUNT, " +
            //                 "       INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT, " +
            //                 "       INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT,  " +
            //                 "       INV.DISCOUNT_RATE                    AS DISCOUNT_RATE,  " +
            //                 "       INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT,  " +
            //                 "       INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT,  " +
            //                 "       " + cmnService.J_SQLDBFormat("TAX.VAT_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS VAT_AMOUNT, " +
            //                 "       " + cmnService.J_SQLDBFormat("TAX.CST_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CST_AMOUNT, " +
            //                 "       INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT,  " +
            //                 "       INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX,  " +
            //                 "       INV.ADDITIONAL_COST                  AS ADDITIONAL_COST,  " +
            //                 "       INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST,  " +
            //                 "       INV.ROUNDED_OFF                      AS ROUNDED_OFF,  " +
            //                 "       INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF,  " +
            //                 "       INV.NET_AMOUNT                       AS NET_AMOUNT,  " +
            //                 "       INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS,  " +
            //                 "       INV.REMARKS                          AS REMARKS " +
            //                 "FROM  (SELECT MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
            //                 "              MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
            //                 "              TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID, " +
            //                 "              TRN_INVOICE_HEADER.BRANCH_ID                   AS BRANCH_ID,  " +
            //                 "              TRN_INVOICE_HEADER.FAYEAR_ID                   AS FAYEAR_ID,  " +
            //                 "              TRN_INVOICE_HEADER.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID,  " +
            //                 "              TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE,  " +
            //                 "              TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO,  " +
            //                 "              MST_PARTY.PARTY_ID                             AS PARTY_ID, " +
            //                 "              MST_PARTY.PARTY_NAME                           AS PARTY_NAME, " +
            //                 "              MST_ITEM.ITEM_ID                               AS ITEM_ID,  " +
            //                 "              MST_ITEM.ITEM_NAME                             AS ITEM_NAME,  " +
            //                 "              TRN_INVOICE_DETAIL.QUANTITY                    AS QUANTITY,  " +
            //                 "              TRN_INVOICE_DETAIL.RATE                        AS RATE,  " +
            //                 "              TRN_INVOICE_DETAIL.AMOUNT                      AS AMOUNT, " +
            //                 "              TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT,  " +
            //                 "              TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT,  " +
            //                 "              TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE,  " +
            //                 "              TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT,  " +
            //                 "              TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT,  " +
            //                 "              TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT,  " +
            //                 "              TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX,  " +
            //                 "              TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST,  " +
            //                 "              TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST,  " +
            //                 "              TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF,  " +
            //                 "              TRN_INVOICE_HEADER.ADDITIONAL_COST + TRN_INVOICE_HEADER.ROUNDED_OFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF ," +
            //                 "              TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT,  " +
            //                 "              TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS,  " +
            //                 "              TRN_INVOICE_HEADER.REMARKS                     AS REMARKS " +
            //                 "       FROM   TRN_INVOICE_HEADER, " +
            //                 "              TRN_INVOICE_DETAIL, " +
            //                 "              MST_COMPANY, " +
            //                 "              MST_INVOICE_SERIES, " +
            //                 "              MST_PARTY, " +
            //                 "              MST_ITEM " +
            //                 "       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID        = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
            //                 "       AND    TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID " +
            //                 "       AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
            //                 "       AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID " +
            //                 "       AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID) AS INV " +
            //                 "LEFT JOIN " +
            //                 "      (SELECT TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID, " +
            //                 "              TRN_INVOICE_TAX.TAX_ID            AS TAX_ID, " +
            //                 "              MST_TAX.TAX_DESC                  AS TAX_DESC, " +
            //                 "              TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE, " +
            //                 "              " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_AMOUNT, J_SQLColFormat.Case_End) + " AS VAT_AMOUNT," +
            //                 "              " + cmnService.J_SQLDBFormat(strCaseEnd_CST_AMOUNT, J_SQLColFormat.Case_End) + " AS CST_AMOUNT " +
            //                 "       FROM   TRN_INVOICE_TAX, " +
            //                 "              MST_TAX " +
            //                 "       WHERE  TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID " +
            //                 "       AND    TRN_INVOICE_TAX.TAX_ID IN(" + BS_TaxId.VAT_ID + "," + BS_TaxId.CST_ID + ")) AS TAX " +
            //                 "ON     INV.INVOICE_HEADER_ID = TAX.INVOICE_HEADER_ID " +
            //                 "WHERE  INV.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
            //                 "AND    INV.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
            //                 "AND    INV.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
            //                 "AND    INV.BRANCH_ID         = " + J_Var.J_pBranchId + " " +
            //                 "AND    INV.FAYEAR_ID         = " + J_Var.J_pFAYearId + " ";
            //if (cmbCombo2.Text != "")
            //    strSQL = strSQL + "AND INV.INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + " ";

            #endregion

            strSQL = "INSERT INTO " + strTmpTableName + " (" +
                 "       COMPANY_NAME," +
                 "       INVOICE_HEADER_ID," +
                 "       INVOICE_DATE," +
                 "       INVOICE_NO," +
                 "       PARTY_ID," +
                 "       PARTY_NAME," +
                 "       ITEM_ID," +
                 "       ITEM_NAME," +
                 "       QUANTITY," +
                 "       RATE," +
                 "       AMOUNT," +
                 "       TOTAL_AMOUNT," +
                 "       DISCOUNT_TEXT," +
                 "       DISCOUNT_RATE," +
                 "       DISCOUNT_AMOUNT," +
                 "       AMOUNT_WITH_DISCOUNT," +
                 "       VAT_SALE_AMOUNT," +
                 "       CST_SALE_AMOUNT," +
                 "       VAT_5_SALE_AMOUNT," +
                 "       CST_5_SALE_AMOUNT," +
                 "       CST_FORMC_SALE_AMOUNT," +
                 "       VAT_AMOUNT," +
                 "       CST_AMOUNT," +
                 "       VAT_5_AMOUNT," +
                 "       CST_5_AMOUNT," +
                 "       CST_FORMC_AMOUNT," +
                 "       TAX_TOTAL_AMOUNT," +
                 "       AMOUNT_WITH_TAX," +
                 "       ADDITIONAL_COST," +
                 "       AMOUNT_WITH_ADDITIONAL_COST," +
                 "       ROUNDED_OFF," +
                 "       SUM_ADDITIONAL_DISCOUNT_ROUNDOFF," +
                 "       NET_AMOUNT," +
                 "       NET_AMOUNT_INWORDS," +
                 "       REMARKS)" +
                 "SELECT INV.COMPANY_NAME                     AS COMPANY_NAME," +
                 "       INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID," +
                 "       INV.INVOICE_DATE                     AS INVOICE_DATE," +
                 "       INV.INVOICE_NO                       AS INVOICE_NO," +
                 "       INV.PARTY_ID                         AS PARTY_ID," +
                 "       INV.PARTY_NAME                       AS PARTY_NAME," +
                 "       INV.ITEM_ID                          AS ITEM_ID," +
                 "       INV.ITEM_NAME                        AS ITEM_NAME," +
                 "       INV.QUANTITY                         AS QUANTITY," +
                 "       INV.RATE                             AS RATE," +
                 "       INV.AMOUNT                           AS AMOUNT," +
                 "       INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT," +
                 "       INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT," +
                 "       INV.DISCOUNT_RATE                    AS DISCOUNT_RATE," +
                 "       INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT," +
                 "       INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT," +
                 "       " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + "       AS VAT_SALE_AMOUNT," +
                 "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + "       AS CST_SALE_AMOUNT," +
                 "       " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_5_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + "          AS VAT_5_SALE_AMOUNT," +
                 "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_5_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + "          AS CST_5_SALE_AMOUNT," +
                 "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_FORMC_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CST_FORMC_SALE_AMOUNT," +
                 "       INV.VAT_AMOUNT                       AS VAT_AMOUNT," +
                 "       INV.CST_AMOUNT                       AS CST_AMOUNT," +
                 "       INV.VAT_5_AMOUNT                     AS VAT_5_AMOUNT," +
                 "       INV.CST_5_AMOUNT                     AS CST_5_AMOUNT," +
                 "       INV.CST_FORMC_AMOUNT                 AS CST_FORMC_AMOUNT," +
                 "       INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT," +
                 "       INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX," +
                 "       INV.ADDITIONAL_COST                  AS ADDITIONAL_COST," +
                 "       INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST," +
                 "       INV.ROUNDED_OFF                      AS ROUNDED_OFF," +
                 "       INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF," +
                 "       INV.NET_AMOUNT                       AS NET_AMOUNT," +
                 "       INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS," +
                 "       INV.REMARKS                          AS REMARKS " +
                 "FROM  (SELECT INV.COMPANY_NAME                     AS COMPANY_NAME, " +
                 "              INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID, " +
                 "              INV.INVOICE_DATE                     AS INVOICE_DATE,  " +
                 "              INV.INVOICE_NO                       AS INVOICE_NO,  " +
                 "              INV.PARTY_ID                         AS PARTY_ID, " +
                 "              INV.PARTY_NAME                       AS PARTY_NAME, " +
                 "              INV.ITEM_ID                          AS ITEM_ID,  " +
                 "              INV.ITEM_NAME                        AS ITEM_NAME,  " +
                 "              INV.QUANTITY                         AS QUANTITY,  " +
                 "              INV.RATE                             AS RATE,  " +
                 "              INV.AMOUNT                           AS AMOUNT, " +
                 "              INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT, " +
                 "              INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT,  " +
                 "              INV.DISCOUNT_RATE                    AS DISCOUNT_RATE,  " +
                 "              INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT,  " +
                 "              INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT,  " +
                 "              " + cmnService.J_SQLDBFormat("TAX.VAT_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "       AS VAT_AMOUNT, " +
                 "              " + cmnService.J_SQLDBFormat("TAX.CST_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "       AS CST_AMOUNT, " +
                 "              " + cmnService.J_SQLDBFormat("TAX.VAT_5_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS VAT_5_AMOUNT, " +
                 "              " + cmnService.J_SQLDBFormat("TAX.CST_5_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS CST_5_AMOUNT, " +
                 "              " + cmnService.J_SQLDBFormat("TAX.CST_FORMC_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CST_FORMC_AMOUNT, " +
                 "              INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT,  " +
                 "              INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX,  " +
                 "              INV.ADDITIONAL_COST                  AS ADDITIONAL_COST,  " +
                 "              INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                 "              INV.ROUNDED_OFF                      AS ROUNDED_OFF,  " +
                 "              INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF,  " +
                 "              INV.NET_AMOUNT                       AS NET_AMOUNT,  " +
                 "              INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS,  " +
                 "              INV.REMARKS                          AS REMARKS " +
                 "       FROM  (SELECT MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
                 "                     MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
                 "                     TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID, " +
                 "                     TRN_INVOICE_HEADER.BRANCH_ID                   AS BRANCH_ID,  " +
                 "                     TRN_INVOICE_HEADER.FAYEAR_ID                   AS FAYEAR_ID,  " +
                 "                     TRN_INVOICE_HEADER.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID,  " +
                 "                     TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE,  " +
                 "                     TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO,  " +
                 "                     MST_PARTY.PARTY_ID                             AS PARTY_ID, " +
                 "                     MST_PARTY.PARTY_NAME                           AS PARTY_NAME, " +
                 "                     MST_ITEM.ITEM_ID                               AS ITEM_ID,  " +
                 "                     MST_ITEM.ITEM_NAME                             AS ITEM_NAME,  " +
                 "                     TRN_INVOICE_DETAIL.QUANTITY                    AS QUANTITY,  " +
                 "                     TRN_INVOICE_DETAIL.RATE                        AS RATE,  " +
                 "                     TRN_INVOICE_DETAIL.AMOUNT                      AS AMOUNT, " +
                 "                     TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT,  " +
                 "                     TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT,  " +
                 "                     TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE,  " +
                 "                     TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT,  " +
                 "                     TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT,  " +
                 "                     TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT,  " +
                 "                     TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX,  " +
                 "                     TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST,  " +
                 "                     TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                 "                     TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF,  " +
                 "                     TRN_INVOICE_HEADER.ADDITIONAL_COST + TRN_INVOICE_HEADER.ROUNDED_OFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF ," +
                 "                     TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT,  " +
                 "                     TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS,  " +
                 "                     TRN_INVOICE_HEADER.REMARKS                     AS REMARKS " +
                 "              FROM   TRN_INVOICE_HEADER, " +
                 "                     TRN_INVOICE_DETAIL, " +
                 "                     MST_COMPANY, " +
                 "                     MST_INVOICE_SERIES, " +
                 "                     MST_PARTY, " +
                 "                     MST_ITEM " +
                 "              WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID        = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                 "              AND    TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID " +
                 "              AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                 "              AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID " +
                 "              AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID) AS INV " +
                 "       LEFT JOIN " +
                 "             (SELECT TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID, " +
                 "                     TRN_INVOICE_TAX.TAX_ID            AS TAX_ID, " +
                 "                     MST_TAX.TAX_DESC                  AS TAX_DESC, " +
                 "                     TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE, " +
                 "                     " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_AMOUNT, J_SQLColFormat.Case_End) + "   AS VAT_AMOUNT," +
                 "                     " + cmnService.J_SQLDBFormat(strCaseEnd_CST_AMOUNT, J_SQLColFormat.Case_End) + "   AS CST_AMOUNT," +
                 "                     " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_5_AMOUNT, J_SQLColFormat.Case_End) + " AS VAT_5_AMOUNT," +
                 "                     " + cmnService.J_SQLDBFormat(strCaseEnd_CST_5_AMOUNT, J_SQLColFormat.Case_End) + " AS CST_5_AMOUNT," +
                 "                     " + cmnService.J_SQLDBFormat(strCaseEnd_CST_FORMC_AMOUNT, J_SQLColFormat.Case_End) + " AS CST_FORMC_AMOUNT " +
                 "                     FROM   TRN_INVOICE_TAX, " +
                 "                     MST_TAX " +
                 "              WHERE  TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID ) AS TAX " +
                 "       ON     INV.INVOICE_HEADER_ID = TAX.INVOICE_HEADER_ID " +
                 "       WHERE  INV.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
                 "       AND    INV.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
                 "       AND    INV.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                 "       AND    INV.BRANCH_ID         = " + J_Var.J_pBranchId + " ) AS INV ";
                 //"       AND    INV.FAYEAR_ID         = " + J_Var.J_pFAYearId + " ";

            //if (cmbCombo2.Text != "")
            //    strSQL = strSQL + "AND INV.INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + " ) AS INV ";
            //else
            //    strSQL = strSQL + " ) AS INV ";



            if (dmlService.J_ExecSql(strSQL) == false) return;



            // CALCULATING TOTAL VALUES

            strSQL = "SELECT " + cmnService.J_SQLDBFormat("SUM(INV.TOTAL_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "              AS TOT_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.DISCOUNT_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "           AS TOT_DISCOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "               AS TOT_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.VAT_SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "           AS TOT_VAT_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "           AS TOT_CST_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.VAT_5_SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "         AS TOT_VAT_5_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_5_SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "         AS TOT_CST_5_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_FORMC_SALE_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "     AS TOT_CST_FORMC_SALE_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.VAT_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "                AS TOT_VAT_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "                AS TOT_CST_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.VAT_5_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "              AS TOT_VAT_5_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_5_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "              AS TOT_CST_5_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.CST_FORMC_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "          AS TOT_CST_FORMC_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.GROSS_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "              AS TOT_GROSS_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.ADD_ADDITIONAL_COST)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "       AS TOT_ADD_ADDITIONAL_COST, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.ADD_ROUNDED_OFF)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "           AS TOT_ADD_ROUNDED_OFF, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.ADD_COST_ROUND_OFF_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS TOT_ADD_COST_ROUND_OFF_AMOUNT, " +
                     "       " + cmnService.J_SQLDBFormat("SUM(INV.NET_AMOUNT)", J_ColumnType.Double, J_SQLColFormat.NullCheck) + "                AS TOT_NET_AMOUNT " +
                     "FROM   (SELECT DISTINCT INVOICE_HEADER_ID        AS INVOICE_HEADER_ID1, " +
                     "                TOTAL_AMOUNT, " +
                     "                DISCOUNT_AMOUNT, " +
                     "                AMOUNT_WITH_DISCOUNT             AS SALE_AMOUNT, " +
                     "                VAT_SALE_AMOUNT, " +
                     "                CST_SALE_AMOUNT, " +
                     "                VAT_5_SALE_AMOUNT, " +
                     "                CST_5_SALE_AMOUNT, " +
                     "                CST_FORMC_SALE_AMOUNT, " +
                     "                VAT_AMOUNT, " +
                     "                CST_AMOUNT, " +
                     "                VAT_5_AMOUNT, " +
                     "                CST_5_AMOUNT, " +
                     "                CST_FORMC_AMOUNT, " +
                     "                AMOUNT_WITH_TAX                  AS GROSS_AMOUNT, " +
                     "                ADDITIONAL_COST                  AS ADD_ADDITIONAL_COST," +
                     "                ROUNDED_OFF                      AS ADD_ROUNDED_OFF," +
                     "                SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS ADD_COST_ROUND_OFF_AMOUNT," +
                     "                NET_AMOUNT " +
                     "          FROM   " + strTmpTableName + ")  AS INV ";

            DataSet dsInvoice = dmlService.J_ExecSqlReturnDataSet(strSQL);

            double tot_amount = 0;
            double tot_discount = 0;
            double tot_sale_amount = 0;
            double tot_vat_sale_amount = 0;
            double tot_cst_sale_amount = 0;
            double tot_vat_5_sale_amount = 0;
            double tot_cst_5_sale_amount = 0;
            double tot_cst_formc_sale_amount = 0;
            double tot_vat_amount = 0;
            double tot_cst_amount = 0;
            double tot_vat_5_amount = 0;
            double tot_cst_5_amount = 0;
            double tot_cst_formc_amount = 0;
            double tot_gross_amount = 0;
            double tot_additional_cost = 0;
            double tot_rounded_off = 0;
            double tot_add_cost_round_off_amount = 0;
            double tot_net_amount = 0;

            if (dsInvoice.Tables[0].Rows.Count > 0)
            {
                tot_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_AMOUNT"]);
                tot_discount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_DISCOUNT"]);
                tot_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_SALE_AMOUNT"]);
                tot_vat_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_VAT_SALE_AMOUNT"]);
                tot_cst_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_SALE_AMOUNT"]);
                tot_vat_5_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_VAT_5_SALE_AMOUNT"]);
                tot_cst_5_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_5_SALE_AMOUNT"]);
                tot_cst_formc_sale_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_FORMC_SALE_AMOUNT"]);
                tot_vat_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_VAT_AMOUNT"]);
                tot_cst_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_AMOUNT"]);
                tot_vat_5_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_VAT_5_AMOUNT"]);
                tot_cst_5_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_5_AMOUNT"]);
                tot_cst_formc_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_CST_FORMC_AMOUNT"]);
                tot_gross_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_GROSS_AMOUNT"]);
                tot_additional_cost = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_ADD_ADDITIONAL_COST"]);
                tot_rounded_off = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_ADD_ROUNDED_OFF"]);
                tot_add_cost_round_off_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_ADD_COST_ROUND_OFF_AMOUNT"]);
                tot_net_amount = Convert.ToDouble(dsInvoice.Tables[0].Rows[0]["TOT_NET_AMOUNT"]);
            }

            dsInvoice.Dispose();


            //UPDATING TOTAL VALUES

            strSQL = "UPDATE " + strTmpTableName + " " +
                     "SET TOT_AMOUNT                    = " + tot_amount + ", " +
                     "    TOT_DISCOUNT                  = " + tot_discount + ", " +
                     "    TOT_SALE_AMOUNT               = " + tot_sale_amount + ", " +
                     "    TOT_VAT_SALE_AMOUNT           = " + tot_vat_sale_amount + ", " +
                     "    TOT_CST_SALE_AMOUNT           = " + tot_cst_sale_amount + ", " +
                     "    TOT_VAT_5_SALE_AMOUNT         = " + tot_vat_5_sale_amount + ", " +
                     "    TOT_CST_5_SALE_AMOUNT         = " + tot_cst_5_sale_amount + ", " +
                     "    TOT_CST_FORMC_SALE_AMOUNT     = " + tot_cst_formc_sale_amount + ", " +
                     "    TOT_VAT_AMOUNT                = " + tot_vat_amount + ", " +
                     "    TOT_CST_AMOUNT                = " + tot_cst_amount + ", " +
                     "    TOT_VAT_5_AMOUNT              = " + tot_vat_5_amount + ", " +
                     "    TOT_CST_5_AMOUNT              = " + tot_cst_5_amount + ", " +
                     "    TOT_CST_FORMC_AMOUNT          = " + tot_cst_formc_amount + ", " +
                     "    TOT_GROSS_AMOUNT              = " + tot_gross_amount + ", " +
                     "    TOT_ADDITIONAL_COST           = " + tot_additional_cost + ", " +
                     "    TOT_ROUNDED_OFF               = " + tot_rounded_off + ", " +
                     "    TOT_ADD_COST_ROUND_OFF_AMOUNT = " + tot_add_cost_round_off_amount + ", " +
                     "    TOT_NET_AMOUNT                = " + tot_net_amount;
            if (dmlService.J_ExecSql(strSQL) == false) return;

            dmlService.J_Commit();

            //REPORT QUERY

            strQueryString = "SELECT COMPANY_NAME                  AS COMPANY_NAME1, " +
                             "       ITEM_ID                       AS ITEM_ID1, " +
                             "       ITEM_NAME                     AS ITEM_NAME1, " +
                             "       SUM(QUANTITY)                 AS TOT_QUANTITY," +
                             "       SUM(AMOUNT)                   AS TOT_AMOUNT," +
                             "       TOT_AMOUNT                    AS SUM_TOT_AMOUNT," +
                             "       TOT_DISCOUNT                  AS SUM_TOT_DISCOUNT," +
                             "       TOT_SALE_AMOUNT               AS SUM_TOT_SALE_AMOUNT," +
                             "       TOT_VAT_SALE_AMOUNT           AS SUM_TOT_VAT_SALE_AMOUNT," +
                             "       TOT_CST_SALE_AMOUNT           AS SUM_TOT_CST_SALE_AMOUNT," +
                             "       TOT_VAT_5_SALE_AMOUNT         AS SUM_TOT_VAT_5_SALE_AMOUNT," +
                             "       TOT_CST_5_SALE_AMOUNT         AS SUM_TOT_CST_5_SALE_AMOUNT," +
                             "       TOT_CST_FORMC_SALE_AMOUNT     AS SUM_TOT_CST_FORMC_SALE_AMOUNT," +
                             "       TOT_VAT_AMOUNT                AS SUM_TOT_VAT_AMOUNT," +
                             "       TOT_CST_AMOUNT                AS SUM_TOT_CST_AMOUNT," +
                             "       TOT_VAT_5_AMOUNT              AS SUM_TOT_VAT_5_AMOUNT," +
                             "       TOT_CST_5_AMOUNT              AS SUM_TOT_CST_5_AMOUNT," +
                             "       TOT_CST_FORMC_AMOUNT          AS SUM_TOT_CST_FORMC_AMOUNT," +
                             "       TOT_GROSS_AMOUNT              AS SUM_TOT_GROSS_AMOUNT," +
                             "       TOT_ADDITIONAL_COST           AS SUM_TOT_ADDITIONAL_COST," +
                             "       TOT_ROUNDED_OFF               AS SUM_TOT_ROUNDED_OFF," +
                             "       TOT_ADD_COST_ROUND_OFF_AMOUNT AS SUM_TOT_ADD_COST_ROUND_OFF_AMOUNT," +
                             "       TOT_NET_AMOUNT                AS SUM_TOT_NET_AMOUNT " +
                             "FROM   " + strTmpTableName + " " +
                             "GROUP BY COMPANY_NAME, " +
                             "         ITEM_ID, " +
                             "         ITEM_NAME, " +
                             "         TOT_AMOUNT, " +
                             "         TOT_DISCOUNT, " +
                             "         TOT_SALE_AMOUNT, " +
                             "         TOT_VAT_SALE_AMOUNT, " +
                             "         TOT_CST_SALE_AMOUNT, " +
                             "         TOT_VAT_5_SALE_AMOUNT, " +
                             "         TOT_CST_5_SALE_AMOUNT, " +
                             "         TOT_CST_FORMC_SALE_AMOUNT, " +
                             "         TOT_VAT_AMOUNT, " +
                             "         TOT_CST_AMOUNT, " +
                             "         TOT_VAT_5_AMOUNT, " +
                             "         TOT_CST_5_AMOUNT, " +
                             "         TOT_CST_FORMC_AMOUNT, " +
                             "         TOT_GROSS_AMOUNT, " +
                             "         TOT_ADDITIONAL_COST, " +
                             "         TOT_ROUNDED_OFF, " +
                             "         TOT_ADD_COST_ROUND_OFF_AMOUNT, " +
                             "         TOT_NET_AMOUNT ";

            // transaction is commited

            crItemWiseInvoiceSummary rptItemWiseInvoiceSummary = new crItemWiseInvoiceSummary();
            rptcls = (ReportClass)rptItemWiseInvoiceSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

            // Drop the report temp table
            if (dmlService.J_ExecSql("DROP TABLE " + strTmpTableName) == false) return;

        }
        #endregion

        #region PrintTaxRegister
        private void PrintTaxRegister()
        {

            // Added by Ripan Paul on 05-08-2011
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */

            //Commented by Shrey Kejriwal on 29-04-2013
            //string[,] strCaseEnd_VAT_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.VAT_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.VAT_ID, "F", "0", "N"}};

            //Added by Shrey Kejriwal on 29-04-2013
            string[,] strCaseEnd_VAT_AMOUNT = {{"MST_TAX.TAX_TYPE = 'V'", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'V'", "F", "0", "N"}};

            //COMMENTED BY SHREY KEJRIWAL ON 25/06/2012
            //string[,] strCaseEnd_CST_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID = " + BS_TaxId.CST_ID, "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            //ADDED BY SHREY KEJRIWAL ON 25/06/2012
            //string[,] strCaseEnd_CST_AMOUNT = {{"TRN_INVOICE_TAX.TAX_ID IN (" + BS_TaxId.CST_ID + ", " + BS_TaxId.CST_ID_FORM_C + ")", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
            //                                   {"TRN_INVOICE_TAX.TAX_ID <> " + BS_TaxId.CST_ID, "F", "0", "N"}};

            //Added by Shrey Kejriwal on 29-04-2013
            string[,] strCaseEnd_CST_AMOUNT = {{"MST_TAX.TAX_TYPE = 'C'", "F", "TRN_INVOICE_TAX.TAX_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'C'", "F", "0", "N"}};


            string[,] strCaseEnd_VAT_SALE_AMOUNT = {{"INV.VAT_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.VAT_AMOUNT = 0", "F", "0", "N"}};

            string[,] strCaseEnd_CST_SALE_AMOUNT = {{"INV.CST_AMOUNT > 0 ", "F", "INV.AMOUNT_WITH_DISCOUNT", "F"},
							                        {"INV.CST_AMOUNT = 0", "F", "0", "N"}};

            //Added by Shrey Kejriwal on 17/08/2011 
            //Report Query
            strQueryString = "SELECT INV.COMPANY_NAME                     AS COMPANY_NAME," +
                             "       INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID," +
                             "       INV.INVOICE_DATE                     AS INVOICE_DATE," +
                             "       INV.INVOICE_NO                       AS INVOICE_NO," +
                             "       INV.PARTY_ID                         AS PARTY_ID," +
                             "       INV.PARTY_NAME                       AS PARTY_NAME," +
                             "       INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT," +
                             "       INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT," +
                             "       INV.DISCOUNT_RATE                    AS DISCOUNT_RATE," +
                             "       INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT," +
                             "       INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT," +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS VAT_SALE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_SALE_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CST_SALE_AMOUNT," +
                             "       INV.VAT_AMOUNT                       AS VAT_AMOUNT," +
                             "       INV.CST_AMOUNT                       AS CST_AMOUNT," +
                             "       INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT," +
                             "       INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX," +
                             "       INV.ADDITIONAL_COST                  AS ADDITIONAL_COST," +
                             "       INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST," +
                             "       INV.ROUNDED_OFF                      AS ROUNDED_OFF," +
                             "       INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF," +
                             "       INV.NET_AMOUNT                       AS NET_AMOUNT," +
                             "       INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS," +
                             "       INV.REMARKS                          AS REMARKS " +
                             "FROM  (SELECT INV.COMPANY_NAME                     AS COMPANY_NAME, " +
                             "              INV.INVOICE_HEADER_ID                AS INVOICE_HEADER_ID, " +
                             "              INV.INVOICE_DATE                     AS INVOICE_DATE,  " +
                             "              INV.INVOICE_NO                       AS INVOICE_NO,  " +
                             "              INV.PARTY_ID                         AS PARTY_ID, " +
                             "              INV.PARTY_NAME                       AS PARTY_NAME, " +
                             "              INV.TOTAL_AMOUNT                     AS TOTAL_AMOUNT, " +
                             "              INV.DISCOUNT_TEXT                    AS DISCOUNT_TEXT,  " +
                             "              INV.DISCOUNT_RATE                    AS DISCOUNT_RATE,  " +
                             "              INV.DISCOUNT_AMOUNT                  AS DISCOUNT_AMOUNT,  " +
                             "              INV.AMOUNT_WITH_DISCOUNT             AS AMOUNT_WITH_DISCOUNT,  " +
                             "              " + cmnService.J_SQLDBFormat("TAX.VAT_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS VAT_AMOUNT, " +
                             "              " + cmnService.J_SQLDBFormat("TAX.CST_AMOUNT", J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS CST_AMOUNT, " +
                             "              INV.TAX_TOTAL_AMOUNT                 AS TAX_TOTAL_AMOUNT,  " +
                             "              INV.AMOUNT_WITH_TAX                  AS AMOUNT_WITH_TAX,  " +
                             "              INV.ADDITIONAL_COST                  AS ADDITIONAL_COST,  " +
                             "              INV.AMOUNT_WITH_ADDITIONAL_COST      AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                             "              INV.ROUNDED_OFF                      AS ROUNDED_OFF,  " +
                             "              INV.SUM_ADDITIONAL_DISCOUNT_ROUNDOFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF,  " +
                             "              INV.NET_AMOUNT                       AS NET_AMOUNT,  " +
                             "              INV.NET_AMOUNT_INWORDS               AS NET_AMOUNT_INWORDS,  " +
                             "              INV.REMARKS                          AS REMARKS " +
                             "       FROM  (SELECT MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
                             "                     MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
                             "                     TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID, " +
                             "                     TRN_INVOICE_HEADER.BRANCH_ID                   AS BRANCH_ID,  " +
                             "                     TRN_INVOICE_HEADER.FAYEAR_ID                   AS FAYEAR_ID,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE,  " +
                             "                     TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO,  " +
                             "                     MST_PARTY.PARTY_ID                             AS PARTY_ID, " +
                             "                     MST_PARTY.PARTY_NAME                           AS PARTY_NAME, " +
                             "                     TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE,  " +
                             "                     TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT,  " +
                             "                     TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX,  " +
                             "                     TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST,  " +
                             "                     TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST,  " +
                             "                     TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF,  " +
                             "                     TRN_INVOICE_HEADER.ADDITIONAL_COST + TRN_INVOICE_HEADER.ROUNDED_OFF AS SUM_ADDITIONAL_DISCOUNT_ROUNDOFF ," +
                             "                     TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT,  " +
                             "                     TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS,  " +
                             "                     TRN_INVOICE_HEADER.REMARKS                     AS REMARKS " +
                             "              FROM   TRN_INVOICE_HEADER, " +
                             "                     MST_COMPANY, " +
                             "                     MST_INVOICE_SERIES, " +
                             "                     MST_PARTY " +
                             "              WHERE  TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID " +
                             "              AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                             "              AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID) AS INV " +
                             "       LEFT JOIN " +
                             "             (SELECT TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID, " +
                             "                     TRN_INVOICE_TAX.TAX_ID            AS TAX_ID, " +
                             "                     MST_TAX.TAX_DESC                  AS TAX_DESC, " +
                             "                     TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE, " +
                             "                     " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_AMOUNT, J_SQLColFormat.Case_End) + " AS VAT_AMOUNT," +
                             "                     " + cmnService.J_SQLDBFormat(strCaseEnd_CST_AMOUNT, J_SQLColFormat.Case_End) + " AS CST_AMOUNT " +
                             "                     FROM   TRN_INVOICE_TAX, " +
                             "                     MST_TAX " +
                             "              WHERE  TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID ) AS TAX " +
                             "       ON     INV.INVOICE_HEADER_ID = TAX.INVOICE_HEADER_ID " +
                             "       WHERE  INV.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
                             "       AND    INV.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
                             "       AND    INV.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                             "       AND    INV.BRANCH_ID         = " + J_Var.J_pBranchId + " ";

                            //Commented by DHRUB ON 01/04/2014 FOR FA YEar wise papulate data 
                            //"       AND    INV.FAYEAR_ID         = " + J_Var.J_pFAYearId + " ";

            if (cmbCombo2.Text != "")
                strQueryString = strQueryString + "AND INV.INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + " ) AS INV ";
            else
                strQueryString = strQueryString + " ) AS INV ";

            strQueryString = strQueryString + "ORDER BY INV.INVOICE_DATE, INV.INVOICE_NO ";

            crTaxRegister rptTaxRegister = new crTaxRegister();
            rptcls = (ReportClass)rptTaxRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintPartyCategoryWiseRegister
        private void PrintPartyCategoryWiseRegister()
        {
            string[,] strCaseEnd_VAT_AMOUNT = {{"MST_TAX.TAX_TYPE = 'V'", "F", "TAX_TOTAL_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'V'", "F", "0", "N"}};

            string[,] strCaseEnd_CST_AMOUNT = {{"MST_TAX.TAX_TYPE = 'C'", "F", "TAX_TOTAL_AMOUNT", "F"},
							                   {"MST_TAX.TAX_TYPE <> 'C'", "F", "0", "N"}};

            //Report Query
            strQueryString = "SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID          AS INVOICE_HEADER_ID, " +
                             "       TRN_INVOICE_HEADER.INVOICE_DATE               AS INVOICE_DATE, " +
                             "       TRN_INVOICE_HEADER.INVOICE_NO                 AS INVOICE_NO, " +
                             "       MST_PARTY.PARTY_NAME                          AS PARTY_NAME, " +
                             "       MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION, " +
                             "       TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT       AS AMOUNT_WITH_DISCOUNT, " +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_VAT_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS VAT_SALE_AMOUNT," +
                             "       " + cmnService.J_SQLDBFormat(strCaseEnd_CST_AMOUNT, J_SQLColFormat.Case_End, J_ElsePart.YES) + " AS CST_SALE_AMOUNT," +
                             "       TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT           AS TAX_TOTAL_AMOUNT, " +
                             "       TRN_INVOICE_HEADER.ADDITIONAL_COST            AS ADDITIONAL_COST, " +
                             "       TRN_INVOICE_HEADER.ROUNDED_OFF                AS ROUNDED_OFF, " +
                             "       TRN_INVOICE_HEADER.NET_AMOUNT                 AS NET_AMOUNT " +
                             "FROM ((((TRN_INVOICE_HEADER " +
                             "                            INNER JOIN MST_PARTY  " +     
                             "                                 ON TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID) " +
                             "                            INNER JOIN MST_PARTY_CATEGORY " +
                             "                                 ON MST_PARTY.PARTY_CATEGORY_ID          = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID) " +
                             "                            LEFT JOIN TRN_INVOICE_TAX "+
                             "                                 ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_TAX.INVOICE_HEADER_ID )" +
                             "                            LEFT  JOIN MST_TAX " +
                             "                                 ON TRN_INVOICE_TAX.TAX_ID               = MST_TAX.TAX_ID) " +
                             "WHERE TRN_INVOICE_HEADER.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
                             "AND   TRN_INVOICE_HEADER.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
                             "AND   TRN_INVOICE_HEADER.COMPANY_ID        = " + Support.GetItemData(cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                             "AND   TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0 "+
                             //"AND   TRN_INVOICE_HEADER.BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                             //"AND   TRN_INVOICE_HEADER.FAYEAR_ID         = " + J_Var.J_pFAYearId + " " +
                             "ORDER BY MST_PARTY_CATEGORY.PARTY_CATEGORY_ID, " +
                             "         TRN_INVOICE_HEADER.INVOICE_DATE, " +
                             "         TRN_INVOICE_HEADER.INVOICE_NO";

            //COMMENTED BY DHRUB ON 27/03/2014
            //"FROM TRN_INVOICE_HEADER, " +
            //                 "     TRN_INVOICE_TAX, " +
            //                 "     MST_PARTY, " +
            //                 "     MST_PARTY_CATEGORY, " +
            //                 "     MST_TAX " +
            //                 "WHERE TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID " +
            //                 "AND TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_TAX.INVOICE_HEADER_ID " +
            //                 "AND TRN_INVOICE_TAX.TAX_ID               = MST_TAX.TAX_ID " +
            //                 "AND MST_PARTY.PARTY_CATEGORY_ID          = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID " +
            //                 "AND TRN_INVOICE_HEADER.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
            //                 "AND TRN_INVOICE_HEADER.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
            //                 "AND TRN_INVOICE_HEADER.COMPANY_ID        = " + Support.GetItemData(cmbCombo1, cmbCombo1.SelectedIndex) + " " +
            //                 "AND TRN_INVOICE_HEADER.BRANCH_ID         = " + J_Var.J_pBranchId + " " +
            //                 "AND TRN_INVOICE_HEADER.FAYEAR_ID         = " + J_Var.J_pFAYearId + " " +
            //                 "ORDER BY MST_PARTY_CATEGORY.PARTY_CATEGORY_ID, " +
            //                 "         TRN_INVOICE_HEADER.INVOICE_DATE, " +
            //                 "         TRN_INVOICE_HEADER.INVOICE_NO";


            crCategoryWiseRegister rptCategoryWiseRegister = new crCategoryWiseRegister();
            rptcls = (ReportClass)rptCategoryWiseRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion


        #region PrintAccountsEntryDateWiseRegister
        private void PrintAccountsEntryDateWiseRegister()
        {

            //Report Query
            strQueryString = @"SELECT CONVERT(VARCHAR, TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,112) AS ACCOUNT_ENTRY_DATE_SORT,
                                      TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  AS ACCOUNT_ENTRY_DATE,
                                      " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_NO", J_ColumnType.String, J_SQLColFormat.NullCheck) + @"             AS INVOICE_NO,
                                      TRN_INVOICE_HEADER.INVOICE_DATE        AS INVOICE_DATE,
                                      " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck, null, "**** UNKNOWN ****") + @"                      AS PARTY_NAME,
                                      " + cmnService.J_SQLDBFormat("MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PAYMENT_TYPE_DESCRIPTION,
                                      " + cmnService.J_SQLDBFormat("MST_BANK.BANK_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @"                        AS BANK_NAME,
                                      TRN_INVOICE_HEADER.BANK_STATEMENT_DATE AS BANK_STATEMENT_DATE,
                                      TRN_INVOICE_HEADER.NET_AMOUNT          AS NET_AMOUNT,
                                      TRN_INVOICE_HEADER.REFERENCE_NO        AS REFERENCE_NO
                               FROM   TRN_INVOICE_HEADER, 
                                      MST_PARTY,  
                                      MST_PAYMENT_TYPE, 
                                      MST_BANK
                               WHERE  TRN_INVOICE_HEADER.PAYMENT_TYPE_ID    *= MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                               AND    TRN_INVOICE_HEADER.BANK_ID            *= MST_BANK.BANK_ID
                               AND    TRN_INVOICE_HEADER.PARTY_ID           *= MST_PARTY.PARTY_ID
                               AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG   = 0
                               AND    TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                               AND    TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                               AND    TRN_INVOICE_HEADER.COMPANY_ID          = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                               ORDER BY CONVERT(VARCHAR, TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,112),
                                      TRN_INVOICE_HEADER.INVOICE_HEADER_ID";

            crAccountsEntryDateWiseRegister rptAccountsEntryDateWiseRegister = new crAccountsEntryDateWiseRegister();
            rptcls = (ReportClass)rptAccountsEntryDateWiseRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintListOfUnknownDeposits
        private void PrintListOfUnknownDeposits()
        {

            //Report Query
            strQueryString = @"SELECT MST_BANK.BANK_NAME                        AS BANK_NAME,
                                       CONVERT(VARCHAR, TRN_INVOICE_HEADER.BANK_STATEMENT_DATE, 112) AS BANK_STATEMENT_DATE_SORT,
                                       TRN_INVOICE_HEADER.BANK_STATEMENT_DATE    AS BANK_STATEMENT_DATE,
                                       " + cmnService.J_SQLDBFormat("MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PAYMENT_TYPE_DESCRIPTION,
                                       TRN_INVOICE_HEADER.REFERENCE_NO           AS REFERENCE_NO,
                                       TRN_INVOICE_HEADER.NET_AMOUNT             AS NET_AMOUNT,
                                       TRN_INVOICE_HEADER.CANCELLATION_FLAG      AS CANCELLATION_FLAG
                                FROM   TRN_INVOICE_HEADER,
                                       MST_PAYMENT_TYPE,
                                       MST_BANK
                                WHERE  TRN_INVOICE_HEADER.PAYMENT_TYPE_ID     *= MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                AND    TRN_INVOICE_HEADER.BANK_ID              = MST_BANK.BANK_ID
                                AND    TRN_INVOICE_HEADER.TRAN_TYPE            = 'UN'
                                AND    TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                                AND    TRN_INVOICE_HEADER.BANK_STATEMENT_DATE <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                                AND    TRN_INVOICE_HEADER.COMPANY_ID          = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + @"
                                ORDER BY MST_BANK.BANK_NAME,
                                       CONVERT(VARCHAR, TRN_INVOICE_HEADER.BANK_STATEMENT_DATE,112),
                                       MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                       TRN_INVOICE_HEADER.INVOICE_HEADER_ID";

            crListOfUnknownDeposits rptListOfUnknownDeposits = new crListOfUnknownDeposits();
            rptcls = (ReportClass)rptListOfUnknownDeposits;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintBillWiseOutstanding
        private void PrintBillWiseOutstanding()
        {

            //Report Query
            strQueryString = @"SELECT TRN_INVOICE_HEADER.INVOICE_DATE           AS INVOICE_DATE,
                                       TRN_INVOICE_HEADER.INVOICE_NO             AS INVOICE_NO,
                                       MST_PARTY.PARTY_NAME                      AS PARTY_NAME,
                                       CASE TRN_INVOICE_HEADER.BANK_ID WHEN 0 THEN ' ' ELSE MST_BANK.BANK_NAME END AS BANK_NAME,
                                       " + cmnService.J_SQLDBFormat("MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PAYMENT_TYPE_DESCRIPTION,
                                       TRN_INVOICE_HEADER.REFERENCE_NO                    AS REFERENCE_NO,
                                       TRN_INVOICE_HEADER.NET_AMOUNT                      AS NET_AMOUNT,
                                       UPPER(MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION)      AS PARTY_CATEGORY_DESCRIPTION
                                FROM   TRN_INVOICE_HEADER,
                                       MST_PARTY,
                                       MST_PAYMENT_TYPE,
                                       MST_BANK,
                                       MST_PARTY_CATEGORY
                                WHERE  TRN_INVOICE_HEADER.TRAN_TYPE           = 'INV'
                                AND    TRN_INVOICE_HEADER.PAYMENT_TYPE_ID    *= MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                AND    TRN_INVOICE_HEADER.PARTY_ID           *= MST_PARTY.PARTY_ID
                                AND    TRN_INVOICE_HEADER.BANK_ID            *= MST_BANK.BANK_ID
                                AND    MST_PARTY.PARTY_CATEGORY_ID            = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
                                AND    TRN_INVOICE_HEADER.INVOICE_DATE       >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                                AND    TRN_INVOICE_HEADER.INVOICE_DATE       <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                                AND    TRN_INVOICE_HEADER.COMPANY_ID          = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                AND    TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE IS NULL
                                AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG   = 0
                                ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                       TRN_INVOICE_HEADER.INVOICE_NO";

            crBillWiseOutstanding rptBillWiseOutstanding = new crBillWiseOutstanding();
            rptcls = (ReportClass)rptBillWiseOutstanding;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintPendingCCAvenueTransactions
        private void PrintPendingCCAvenueTransactions()
        {

            //Report Query
            strQueryString = @"SELECT TRN_INVOICE_HEADER.INVOICE_DATE        AS INVOICE_DATE,
                                       TRN_INVOICE_HEADER.INVOICE_NO          AS INVOICE_NO,
                                       MST_PARTY.PARTY_NAME                   AS PARTY_NAME,
                                       TRN_INVOICE_HEADER.REFERENCE_NO        AS REFERENCE_NO,
                                       TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  AS ACCOUNT_ENTRY_DATE,
                                       TRN_INVOICE_HEADER.BANK_STATEMENT_DATE AS BANK_STATEMENT_DATE,
                                       TRN_INVOICE_HEADER.NET_AMOUNT          AS NET_AMOUNT,
                                       UPPER(MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION)  AS PARTY_CATEGORY_DESCRIPTION 
                                FROM   TRN_INVOICE_HEADER,
                                       MST_PARTY,
                                       MST_PARTY_CATEGORY 
                                WHERE  TRN_INVOICE_HEADER.PARTY_ID            = MST_PARTY.PARTY_ID 
                                AND    MST_PARTY.PARTY_CATEGORY_ID            = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID 
                                AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG   = 0
                                AND    TRN_INVOICE_HEADER.PAYMENT_TYPE_ID     = 5
                                AND    TRN_INVOICE_HEADER.INVOICE_DATE       >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                                AND    TRN_INVOICE_HEADER.INVOICE_DATE       <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " ";
            if(cmbCombo2.SelectedIndex == 0) strQueryString += "AND TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE IS NULL ";
            strQueryString += @"ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                       TRN_INVOICE_HEADER.INVOICE_NO";

            crPendingCCAvenueTransactions rptPendingCCAvenueTransactions = new crPendingCCAvenueTransactions();
            rptcls = (ReportClass)rptPendingCCAvenueTransactions;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintBankStDateWiseRegister
        private void PrintBankStDateWiseRegister()
        {

            //Report Query
            strQueryString = @"SELECT CONVERT(VARCHAR, TRN_INVOICE_HEADER.BANK_STATEMENT_DATE, 112) AS BANK_STATEMENT_DATE_SORT,
                                      TRN_INVOICE_HEADER.BANK_STATEMENT_DATE  AS BANK_STATEMENT_DATE,
                                      " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_NO", J_ColumnType.String, J_SQLColFormat.NullCheck) + @"             AS INVOICE_NO,
                                      TRN_INVOICE_HEADER.INVOICE_DATE        AS INVOICE_DATE,
                                      " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck, null, "**** UNKNOWN ****") + @"                      AS PARTY_NAME,
                                      " + cmnService.J_SQLDBFormat("MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PAYMENT_TYPE_DESCRIPTION,
                                      " + cmnService.J_SQLDBFormat("MST_BANK.BANK_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @"                        AS BANK_NAME,
                                      TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE AS ACCOUNT_ENTRY_DATE,
                                      TRN_INVOICE_HEADER.NET_AMOUNT          AS NET_AMOUNT,
                                      TRN_INVOICE_HEADER.REFERENCE_NO        AS REFERENCE_NO
                               FROM   TRN_INVOICE_HEADER, 
                                      MST_PARTY,  
                                      MST_PAYMENT_TYPE, 
                                      MST_BANK
                               WHERE  TRN_INVOICE_HEADER.PAYMENT_TYPE_ID     *= MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                               AND    TRN_INVOICE_HEADER.BANK_ID             *= MST_BANK.BANK_ID
                               AND    TRN_INVOICE_HEADER.PARTY_ID            *= MST_PARTY.PARTY_ID
                               AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG    = 0
                               AND    TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                               AND    TRN_INVOICE_HEADER.BANK_STATEMENT_DATE <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                               AND    TRN_INVOICE_HEADER.COMPANY_ID           = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                               ORDER BY CONVERT(VARCHAR, TRN_INVOICE_HEADER.BANK_STATEMENT_DATE, 112),
                                      TRN_INVOICE_HEADER.INVOICE_HEADER_ID";

            crBankStDateWiseRegister rptBankStDateWiseRegister = new crBankStDateWiseRegister();
            rptcls = (ReportClass)rptBankStDateWiseRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintDetailsCollectionType
        private void PrintDetailsCollectionType()
        {

            //Report Query
            strQueryString = @"SELECT ASQL.INVOICE_DATE_SORT        AS INVOICE_DATE_SORT,
                                       ASQL.INVOICE_DATE_DISPLAY     AS INVOICE_DATE_DISPLAY,
                                       ASQL.INVOICE_DATE             AS INVOICE_DATE,
                                       ASQL.PAYMENT_TYPE_DESCRIPTION AS PAYMENT_TYPE_DESCRIPTION,
                                       SUM(ASQL.NET_AMOUNT)          AS NET_AMOUNT,
                                       SUM(ASQL.UNKNOWN_AMOUNT)      AS UNKNOWN_AMOUNT
                                FROM  (SELECT CONVERT(VARCHAR, TRN_INVOICE_HEADER.INVOICE_DATE, 112)                       AS INVOICE_DATE_SORT,
                                              CONVERT(VARCHAR, TRN_INVOICE_HEADER.INVOICE_DATE, 103)                       AS INVOICE_DATE_DISPLAY,
                                              TRN_INVOICE_HEADER.INVOICE_DATE                                              AS INVOICE_DATE,
                                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                                    AS PAYMENT_TYPE_DESCRIPTION,
                                              CASE TRN_INVOICE_HEADER.TRAN_TYPE WHEN 'INV' THEN SUM(NET_AMOUNT) ELSE 0 END AS NET_AMOUNT,
                                              CASE TRN_INVOICE_HEADER.TRAN_TYPE WHEN 'UN' THEN SUM(NET_AMOUNT) ELSE 0 END  AS UNKNOWN_AMOUNT
                                       FROM   TRN_INVOICE_HEADER,
                                              MST_PAYMENT_TYPE
                                       WHERE  TRN_INVOICE_HEADER.PAYMENT_TYPE_ID   = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                       AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0
                                       AND    TRN_INVOICE_HEADER.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                                       AND    TRN_INVOICE_HEADER.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                                       AND    TRN_INVOICE_HEADER.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                       GROUP BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                              TRN_INVOICE_HEADER.TRAN_TYPE) AS ASQL
                                GROUP BY ASQL.INVOICE_DATE_SORT,
                                       ASQL.INVOICE_DATE_DISPLAY,
                                       ASQL.INVOICE_DATE,
                                       ASQL.PAYMENT_TYPE_DESCRIPTION
                                ORDER BY ASQL.INVOICE_DATE,
                                       ASQL.PAYMENT_TYPE_DESCRIPTION";
            
            crDetailsCollectionType rptDetailsCollectionType = new crDetailsCollectionType();
            rptcls = (ReportClass)rptDetailsCollectionType;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);

        }
        #endregion

        #region PrintSalesDistribution
        private void PrintSalesDistribution()
        {
            //Report Query

            strQueryString = @"SELECT SQL1.COMPANY_NAME              AS COMPANY_NAME,
                                      SQL1.PARTY_CATEGORY            AS PARTY_CATEGORY,
                                      SQL1.ITEM_NAME                 AS ITEM_NAME,
                                      SQL1.RATE_2                    AS RATE_2,
                                      SQL1.NON_SALE_PERCENT          AS NON_SALE_PERCENT,
                                      SQL1.SALE_PERCENT              AS SALE_PERCENT,
                                      SUM(SQL1.TOTAL_SALE_QUANTITY)  AS TOTAL_SALE_QUANTITY,
                                      SUM(SQL1.TOTAL_DISCOUNT_VALUE) AS TOTAL_DISCOUNT_VALUE,
                                      SUM(SQL1.TOTAL_SALE_VALUE)     AS TOTAL_SALE_VALUE,
                                      ROUND(SUM(SQL1.TOTAL_SALE_VALUE  * SQL1.NON_SALE_PERCENT/100 ), 2) AS NS_VALUE,
                                      ROUND(SUM(SQL1.TOTAL_SALE_VALUE  * SQL1.SALE_PERCENT/100 ), 2)     AS S_VALUE,
                                      ROUND(SUM((SQL1.TOTAL_SALE_VALUE * SQL1.SALE_PERCENT/100) - SQL1.TOTAL_DISCOUNT_VALUE), 2) AS S_VALUE_NET
                               FROM (SELECT MST_COMPANY.COMPANY_NAME AS COMPANY_NAME,
                                            CASE WHEN MST_PARTY.PARTY_CATEGORY_ID = 1 
                                                 THEN 'DEALER SALES' ELSE 'OTHER SALES' END AS PARTY_CATEGORY,
                                            MST_PARTY.PARTY_ID,
                                            MST_PARTY.PARTY_NAME,
                                            MST_ITEM.ITEM_ID,
                                            MST_ITEM.ITEM_NAME,
                                            MST_ITEM.RATE_2,
                                            MST_ITEM.NON_SALE_PERCENT,
                                            MST_ITEM.SALE_PERCENT,
                                            SUM(TRN_INVOICE_DETAIL.QUANTITY) AS TOTAL_SALE_QUANTITY,
                                            ROUND(SUM(TRN_INVOICE_DETAIL.AMOUNT * ROUND(TRN_INVOICE_HEADER.DISCOUNT_AMOUNT / 
                                                     TRN_INVOICE_HEADER.TOTAL_AMOUNT * 100,2)/100),2) AS TOTAL_DISCOUNT_VALUE,
                                            SUM(TRN_INVOICE_DETAIL.AMOUNT)   AS TOTAL_SALE_VALUE
                                     FROM   TRN_INVOICE_HEADER,
                                            TRN_INVOICE_DETAIL,
                                            MST_PARTY,
                                            MST_ITEM,
                                            MST_COMPANY
                                     WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID
                                     AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID    
                                     AND    TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID    
                                     AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID
                                     AND    TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'       
                                     AND    (MST_ITEM.NON_SALE_PERCENT + MST_ITEM.SALE_PERCENT + MST_ITEM.RATE_2) > 0
                                     AND    TRN_INVOICE_HEADER.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                                     AND    TRN_INVOICE_HEADER.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                                     AND    TRN_INVOICE_HEADER.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                     GROUP BY MST_COMPANY.COMPANY_NAME,
                                              CASE WHEN MST_PARTY.PARTY_CATEGORY_ID = 1 THEN 'DEALER SALES' ELSE 'OTHER SALES' END,
                                              MST_PARTY.PARTY_ID,
                                              MST_PARTY.PARTY_NAME,       
                                              MST_ITEM.ITEM_ID,
                                              MST_ITEM.ITEM_NAME,  
                                              MST_ITEM.RATE_2,
                                              MST_ITEM.NON_SALE_PERCENT,
                                              MST_ITEM.SALE_PERCENT) AS SQL1
                               GROUP BY SQL1.COMPANY_NAME,
                                        SQL1.PARTY_CATEGORY,
                                        SQL1.ITEM_NAME,
                                        SQL1.RATE_2,
                                        SQL1.NON_SALE_PERCENT,
                                        SQL1.SALE_PERCENT 
                               ORDER BY SQL1.PARTY_CATEGORY,
                                        SQL1.ITEM_NAME";

            crSalesDistribution rptSalesDistribution = new crSalesDistribution();
            rptcls = (ReportClass)rptSalesDistribution;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, lblTitle.Text);
        }
        #endregion

        #region PartyCategoryWiseMonthlySaleDetails
        public void PartyCategoryWiseMonthlySaleDetails()
        {
            try
            {

                strSQL = @" SELECT MONTHSALE.YEARMONTH,
                               MONTHSALE.MMMYYYY,
                               SUM(MONTHSALE.SUNDRY_PARTY_SALE) AS SUNDRY_PARTY_SALE,
                               SUM(MONTHSALE.DEALER_SALE)       AS DEALER_SALE,
                               SUM(MONTHSALE.OTHER_SALE)        AS OTHER_SALE,
                               SUM(MONTHSALE.SUNDRY_PARTY_SALE + MONTHSALE.DEALER_SALE + MONTHSALE.OTHER_SALE) AS TOTAL_SALE
                        FROM
                        (" +
                    //-----------------------------------------------------------------
                    //--- SUNDRY PARTY SALE
                    //-----------------------------------------------------------------
                          @"SELECT CONVERT(CHAR(6),INVOICE_DATE,112) AS YEARMONTH,
                               LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4) AS MMMYYYY,
                               TRN_INVOICE_HEADER.COMPANY_ID	 AS COMPANY_ID,
                               SUM(NET_AMOUNT)                   AS SUNDRY_PARTY_SALE,
                               0                                 AS DEALER_SALE,
                               0                                 AS OTHER_SALE
                        FROM   TRN_INVOICE_HEADER,
                               MST_PARTY
                        WHERE  TRN_INVOICE_HEADER.PARTY_ID          =  MST_PARTY.PARTY_ID
                        AND    MST_PARTY.PARTY_CATEGORY_ID          = 0
                        AND    TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'
                        AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0
                        GROUP  BY  CONVERT(CHAR(6),INVOICE_DATE,112),
                                   LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4),
                                   TRN_INVOICE_HEADER.COMPANY_ID
                        UNION  " +
                    //-----------------------------------------------------------------
                    //--- DEALER SALE
                    //-----------------------------------------------------------------
                          @"SELECT CONVERT(CHAR(6),INVOICE_DATE,112) AS YEARMONTH,
                               LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4) AS MMMYYYY,
                               TRN_INVOICE_HEADER.COMPANY_ID	 AS COMPANY_ID,
                               0                                 AS SUNDRY_PARTY_SALE,
                               SUM(NET_AMOUNT)                   AS DEALER_SALE,
                               0                                 AS OTHER_SALE
                        FROM   TRN_INVOICE_HEADER,
                               MST_PARTY
                        WHERE  TRN_INVOICE_HEADER.PARTY_ID          =  MST_PARTY.PARTY_ID
                        AND    MST_PARTY.PARTY_CATEGORY_ID          = 1
                        AND    TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'
                        AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0
                        GROUP  BY  CONVERT(CHAR(6),INVOICE_DATE,112),  
                                   LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4),
                                   TRN_INVOICE_HEADER.COMPANY_ID
                        UNION  " +
                    //-----------------------------------------------------------------
                    //--- OTHER SALE
                    //-----------------------------------------------------------------
                          @"SELECT CONVERT(CHAR(6),INVOICE_DATE,112) AS YEARMONTH,
                               LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4) AS MMMYYYY,
                               TRN_INVOICE_HEADER.COMPANY_ID	 AS COMPANY_ID,
                               0                                 AS SUNDRY_PARTY_SALE,
                               0                                 AS DEALER_SALE,
                               SUM(NET_AMOUNT)                   AS OTHER_SALE 
                        FROM   TRN_INVOICE_HEADER,
                               MST_PARTY
                        WHERE  TRN_INVOICE_HEADER.PARTY_ID          =  MST_PARTY.PARTY_ID
                        AND    MST_PARTY.PARTY_CATEGORY_ID          = 2
                        AND    TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'
                        AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0
                        GROUP  BY  CONVERT(CHAR(6),INVOICE_DATE,112),  
                                   LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4),  
                                   TRN_INVOICE_HEADER.COMPANY_ID
                        )
                        AS MONTHSALE
                        WHERE MONTHSALE.YEARMONTH >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + "   " +
                       "AND   MONTHSALE.YEARMONTH <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskToMonth) + cmnService.J_DateOperator() + "   " +
                       "AND   MONTHSALE.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + "  " +
                      @"GROUP BY MONTHSALE.YEARMONTH,
                                  MONTHSALE.MMMYYYY
                        ORDER BY MONTHSALE.YEARMONTH  ";

                crPartyCategoryWiseMonthlySaleSummary rptMonthlySaleSummary = new crPartyCategoryWiseMonthlySaleSummary();
                rptcls = (ReportClass)rptMonthlySaleSummary;

                // set text to report
                rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromMonth.Text + " to " + mskToMonth.Text);

                // execute the report as per above sql
                rptService.J_PreviewReport(ref rptcls, this, strSQL);

            }
            catch (Exception err)
            {
                return;
            }

        }
        #endregion 


        #region PartyCategoryWiseMonthlySale
        public void PartyCategoryWiseMonthlySale()
        {
            strSQL = @"SELECT  MST_PARTY_CATEGORY.PARTY_CATEGORY_ID               AS PARTY_CATEGORY_ID,
                               MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION      AS PARTY_CATEGORY_DESCRIPTION,
                               CONVERT(CHAR(6),INVOICE_DATE,112)                  AS YEARMONTH, 
                               LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4) AS MMMYYYY,
                               SUM(TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT)       AS AMOUNT_WITH_DISCOUNT, 
                               SUM( CASE 
                                   WHEN MST_TAX.TAX_TYPE = 'V' THEN TAX_TOTAL_AMOUNT 
                                   ELSE 0 
                                 END )                                            AS VAT_SALE_AMOUNT, 
                               SUM( CASE 
                                   WHEN MST_TAX.TAX_TYPE = 'C' THEN TAX_TOTAL_AMOUNT 
                                   ELSE 0 
                                 END )                                            AS CST_SALE_AMOUNT, 
                               SUM(TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT)           AS TAX_TOTAL_AMOUNT, 
                               SUM(TRN_INVOICE_HEADER.ADDITIONAL_COST)            AS ADDITIONAL_COST, 
                               SUM(TRN_INVOICE_HEADER.ROUNDED_OFF)                AS ROUNDED_OFF, 
                               SUM(TRN_INVOICE_HEADER.NET_AMOUNT)                 AS NET_AMOUNT 
                        FROM   ((((TRN_INVOICE_HEADER 
                                   INNER JOIN MST_PARTY 
                                           ON TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID) 
                                  INNER JOIN MST_PARTY_CATEGORY 
                                          ON MST_PARTY.PARTY_CATEGORY_ID = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID) 
                                 LEFT JOIN TRN_INVOICE_TAX 
                                        ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_TAX.INVOICE_HEADER_ID ) 
                                LEFT JOIN MST_TAX 
                                       ON TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID)
                        WHERE CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112) >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + "   " +
                       "AND   CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112)<= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskToMonth) + cmnService.J_DateOperator() + "   " +
                       "AND   TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + "  " +
                      @"AND   TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0 
                        GROUP  BY MST_PARTY_CATEGORY.PARTY_CATEGORY_ID,
                                  MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION,
                                  CONVERT(CHAR(6),INVOICE_DATE,112),
                                  LEFT(CONVERT(CHAR(11),INVOICE_DATE,109),3) + ' ' + RIGHT(CONVERT(CHAR(11),INVOICE_DATE,109),4)
                        ORDER  BY MST_PARTY_CATEGORY.PARTY_CATEGORY_ID,
                                  CONVERT(CHAR(6),INVOICE_DATE,112)
                                  ";

            crPartyCategoryWiseSales rptMonthlySaleSummary = new crPartyCategoryWiseSales();
            rptcls = (ReportClass)rptMonthlySaleSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromMonth.Text + " to " + mskToMonth.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintAccountReconciliation
        public void PrintAccountReconciliation()
        {
            try
            {
                strSQL = @" SELECT CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                               TRN_INVOICE_HEADER.INVOICE_NO,
                               MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION,
                               MST_PARTY.PARTY_NAME,
                               TRN_INVOICE_HEADER.REFERENCE_NO,
                               TRN_INVOICE_HEADER.NET_AMOUNT,
                               CASE WHEN TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  IS NULL THEN '' ELSE CONVERT(CHAR(10),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,103)  END AS ACC_ENTRY_DATE,
                               CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL THEN '' ELSE CONVERT(CHAR(10),TRN_INVOICE_HEADER.BANK_STATEMENT_DATE,103) END AS BK_STATEMENT_DATE
                        FROM   TRN_INVOICE_HEADER,
                               MST_PARTY,
                               MST_PARTY_CATEGORY
                        WHERE  TRN_INVOICE_HEADER.PARTY_ID      = MST_PARTY.PARTY_ID
                        AND    MST_PARTY.PARTY_CATEGORY_ID      = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
                        AND    TRN_INVOICE_HEADER.TRAN_TYPE     = 'INV'
                        AND    TRN_INVOICE_HEADER.FAYEAR_ID     = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + "  " +
                          @"AND    TRN_INVOICE_HEADER.COMPANY_ID    = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + "  " +
                          @"AND    TRN_INVOICE_HEADER.INVOICE_DATE <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + " " +
                          @"AND   (TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE IS NULL OR TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE > " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + " ) "+
                          @"ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                 TRN_INVOICE_HEADER.INVOICE_NO";

                crAccountReconciliation rptAccountReconciliation = new crAccountReconciliation();
                rptcls = (ReportClass)rptAccountReconciliation;

                // set text to report
                rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "As On : " + mskFromDate.Text);

                rptService.J_SetTextToReport(ref rptcls, 2, "txtFaYear", cmbCombo1.Text);

                // execute the report as per above sql
                rptService.J_PreviewReport(ref rptcls, this, strSQL);
            }
            catch(Exception err)
            {
                return;
            }
        }
        #endregion 

        #region PrintSundryPartySale
        public void PrintSundryPartySale()
        {
            strSQL = @" SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                               TRN_INVOICE_HEADER.INVOICE_NO,
                               MST_PARTY.PARTY_NAME,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                               TRN_INVOICE_HEADER.REFERENCE_NO,
                               TRN_INVOICE_HEADER.NET_AMOUNT,
                               CASE WHEN BANK_ID = 0 THEN '' ELSE 'BANK' END AS TYPE,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS BILL_DATE,
                               CASE WHEN TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  IS NULL THEN '' ELSE CONVERT(CHAR(10),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,103) END AS ACCOUNTING_DATE,
                               CASE WHEN TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  IS NULL THEN 0 ELSE DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE) END AS ACC_DIFF,
                               CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL THEN '' ELSE CONVERT(CHAR(10),TRN_INVOICE_HEADER.BANK_STATEMENT_DATE,103) END AS BANK_TALLY_DATE,
                               CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL THEN 0 ELSE DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE) END AS BANK_DIFF,
                               CASE WHEN TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  IS NOT NULL AND TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NOT NULL THEN DATEDIFF(DAY,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE) ELSE 0 END AS NET_DIFF
                        FROM TRN_INVOICE_HEADER,
                             MST_PARTY,
                             MST_PAYMENT_TYPE,
                             MST_COMPANY          
                        WHERE TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID
                        AND   TRN_INVOICE_HEADER.PAYMENT_TYPE_ID   = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                        AND   TRN_INVOICE_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID 
                        AND   TRN_INVOICE_HEADER.TRAN_TYPE         = 'INV'
                        AND   TRN_INVOICE_HEADER.FAYEAR_ID         =  " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + "  " +
                      @"AND   TRN_INVOICE_HEADER.COMPANY_ID         =  " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex) + "  " +
                      @"AND   TRN_INVOICE_HEADER.CANCELLATION_FLAG =  0 
                        AND   MST_PARTY.PARTY_CATEGORY_ID          =  0 
                        AND   (ABS(CASE WHEN TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE  IS NULL THEN 0 ELSE DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE) END)  > MST_COMPANY.MAX_DAYS_PERMIT OR
                               ABS(CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL THEN 0 ELSE DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE) END) > MST_COMPANY.MAX_DAYS_PERMIT ) 

                        ORDER BY TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,
                                 TRN_INVOICE_HEADER.INVOICE_DATE,
                                 TRN_INVOICE_HEADER.INVOICE_HEADER_ID ";

            crSundryPartySale rptSundryPartySale = new crSundryPartySale();
            rptcls = (ReportClass)rptSundryPartySale;

            rptService.J_SetTextToReport(ref rptcls, 2, "txtFaYear", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintItemwiseSaleDetails
        //ADDED BY DHRUB ON 17/05/2014
        public void PrintItemwiseSaleDetails()
        {
            strSQL = " SELECT ROW_NUMBER() OVER(PARTITION BY MST_ITEM.ITEM_ID ORDER BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID ASC) AS SL_NO, "+
                      "        MST_ITEM.ITEM_NAME, " +
                      "        CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE, " +
                      "        TRN_INVOICE_HEADER.INVOICE_NO," +
                      "        MST_PARTY.PARTY_NAME, " +
                      "        MST_PARTY.CITY, " +
                      "        TRN_INVOICE_DETAIL.QUANTITY, " +
                      "        TRN_INVOICE_DETAIL.RATE, " +
                      "        TRN_INVOICE_DETAIL.AMOUNT, " +
                      "        TRN_INVOICE_DETAIL.REMARKS " +
                      " FROM   TRN_INVOICE_HEADER, " +
                      "        TRN_INVOICE_DETAIL, " +
                      "        MST_ITEM, " +
                      "        MST_PARTY " +
                      " WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                      " AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID " +
                      " AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID " +
                      " AND    TRN_INVOICE_HEADER.CANCELLATION_FLAG = 0  " +
                      " AND    TRN_INVOICE_HEADER.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + " " +
                      " AND    TRN_INVOICE_HEADER.INVOICE_DATE     >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + " " +
                      " AND    TRN_INVOICE_HEADER.INVOICE_DATE     <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + " " +
                      " AND    MST_ITEM.ITEM_ID IN(" + cmnService.J_GenerateDataGridViewSelectedId(grdvDescription) + ") ";

            strSQL  = strSQL + @" ORDER BY MST_ITEM.ITEM_NAME,
                                 TRN_INVOICE_HEADER.INVOICE_DATE,
                                 TRN_INVOICE_HEADER.INVOICE_NO  ";

            crItemWiseSaleDetaails rptItemWiseSaleDetaails = new crItemWiseSaleDetaails();
            rptcls = (ReportClass)rptItemWiseSaleDetaails;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", Convert.ToString(cmbCombo1.Text));

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintSundryPartySalesCumOutstanding
        public void PrintSundryPartySalesCumOutstanding()
        {

            strSQL = @"SELECT  STATEMENT.INV_DATE,
                               STATEMENT.INVOICE_NO,
                               STATEMENT.PARTY_NAME,
                               STATEMENT.PAYMENT_TYPE_DESCRIPTION,
                               STATEMENT.REFERENCE_NO,
                               STATEMENT.NET_AMOUNT,
                               STATEMENT.OPENING,
                               STATEMENT.OUTSTANDING,
                               STATEMENT.RECEIPT_AMOUNT, 
                               STATEMENT.BANK_DATE,
                               SUM(NET_AMOUNT)     OVER() AS SUM_NET_AMOUNT,
                               SUM(OPENING)        OVER() AS SUM_OPENING,
                               SUM(OUTSTANDING)    OVER() AS SUM_OUTSTANDING,
                               SUM(RECEIPT_AMOUNT) OVER() AS SUM_RECEIPT_AMOUNT
                        FROM (
                        -------- opening outstanding
                        SELECT 1 AS SECTION,
                               NULL AS INVOICE_DATE,
                               '' AS INV_DATE,
                               '' AS INVOICE_NO,
                               'Outstanding as on ' + CONVERT(CHAR(10)," + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @",103)  AS PARTY_NAME,
                               '' AS PAYMENT_TYPE_DESCRIPTION,
                               '' AS REFERENCE_NO,
                               0 AS NET_AMOUNT,
                               MAX(CASE WHEN TRN_INVOICE_HEADER.INVOICE_DATE >= MST_SETUP.SUNDRY_CUTOFF_DATE 
                                    THEN 0
                                    ELSE MST_SETUP.OPENING_SUNDRY_OUTSTANDING 
                                    END)+
                               ISNULL(SUM(CASE WHEN (TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR  TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @") 
                                                    AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> 4
                                                    AND   TRN_INVOICE_HEADER.INVOICE_DATE    >= MST_SETUP.SUNDRY_CUTOFF_DATE 
                                               THEN TRN_INVOICE_HEADER.NET_AMOUNT 
                                               ELSE 0
                                          END),0) AS OPENING,
                               0  AS OUTSTANDING,
                               0   AS RECEIPT_AMOUNT,
                               '' AS BANK_DATE
                        FROM TRN_INVOICE_HEADER,
                             MST_PARTY,
                             MST_SETUP
                        WHERE TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID
                        AND   TRN_INVOICE_HEADER.TRAN_TYPE       = 'INV'
                        AND   MST_PARTY.PARTY_CATEGORY_ID        = 0
                        AND   TRN_INVOICE_HEADER.INVOICE_DATE    < " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                        UNION
                        SELECT 2 AS SECTION,
                               TRN_INVOICE_HEADER.INVOICE_DATE,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                               TRN_INVOICE_HEADER.INVOICE_NO,
                               MST_PARTY.PARTY_NAME,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                               TRN_INVOICE_HEADER.REFERENCE_NO,
                               TRN_INVOICE_HEADER.NET_AMOUNT,
                               0  AS OPENING,
                               CASE WHEN (TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR  TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @") 
                                          AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> 4
                               THEN TRN_INVOICE_HEADER.NET_AMOUNT 
                               ELSE 0
                               END AS OUTSTANDING,
                               0   AS RECEIPT_AMOUNT,
                               CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                                                                                        OR TRN_INVOICE_HEADER.BANK_STATEMENT_DATE <  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                               THEN ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.BANK_STATEMENT_DATE,103),'') 
                               ELSE ''
                               END AS BANK_DATE
                        FROM   TRN_INVOICE_HEADER,
                               MST_PARTY,
                               MST_PAYMENT_TYPE
                        WHERE  TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID 
                        AND    TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                        AND    TRN_INVOICE_HEADER.TRAN_TYPE       = 'INV'
                        AND    MST_PARTY.PARTY_CATEGORY_ID        = 0
                        AND    TRN_INVOICE_HEADER.INVOICE_DATE   >=  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                        AND    TRN_INVOICE_HEADER.INVOICE_DATE   <=  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                        UNION
                        SELECT 3 AS SECTION,
                               NULL AS INVOICE_DATE,
                               '' AS INV_DATE,
                               '' AS INVOICE_NO,
                               'Less : Receipt of Previous Period ' AS PARTY_NAME,
                               '' AS PAYMENT_TYPE_DESCRIPTION,
                               '' AS REFERENCE_NO,
                               0  AS NET_AMOUNT,
                               0  AS OPENING,
                               0  AS OUTSTANDING,
                               ISNULL(SUM(TRN_INVOICE_HEADER.NET_AMOUNT),0) * -1  AS RECEIPT_AMOUNT,
                               '' AS BANK_DATE
                        FROM TRN_INVOICE_HEADER,
                             MST_PARTY,
                             MST_SETUP
                        WHERE TRN_INVOICE_HEADER.PARTY_ID            = MST_PARTY.PARTY_ID
                        AND   TRN_INVOICE_HEADER.TRAN_TYPE           = 'INV'
                        AND   MST_PARTY.PARTY_CATEGORY_ID            = 0
                        AND   TRN_INVOICE_HEADER.INVOICE_DATE        >= MST_SETUP.SUNDRY_CUTOFF_DATE
                        AND   TRN_INVOICE_HEADER.INVOICE_DATE        <  " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                        AND   TRN_INVOICE_HEADER.BANK_STATEMENT_DATE >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
                        AND  TRN_INVOICE_HEADER.BANK_STATEMENT_DATE  <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
                        ) AS STATEMENT
                        ORDER BY STATEMENT.SECTION,
                                 STATEMENT.INVOICE_DATE,
                                 STATEMENT.INVOICE_NO ";

//            strSQL = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
//                              CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
//                              TRN_INVOICE_HEADER.INVOICE_NO,
//                              MST_PARTY.PARTY_NAME,
//                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
//                              TRN_INVOICE_HEADER.REFERENCE_NO,
//                              TRN_INVOICE_HEADER.NET_AMOUNT,
//                              CASE WHEN (TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR  TRN_INVOICE_HEADER.BANK_STATEMENT_DATE > " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @" ) 
//                                         AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> 4
//                                   THEN TRN_INVOICE_HEADER.NET_AMOUNT 
//                                   ELSE 0
//                              END AS PENDING,
//                              CASE WHEN TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR TRN_INVOICE_HEADER.BANK_STATEMENT_DATE > " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @"
//                                   THEN ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.BANK_STATEMENT_DATE,103),'') 
//                                   ELSE ''
//                              END AS BANK_DATE
//                       FROM   TRN_INVOICE_HEADER,
//                              MST_PARTY,
//                              MST_PAYMENT_TYPE
//                       WHERE  TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID 
//                       AND    TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
//                       AND    TRN_INVOICE_HEADER.TRAN_TYPE       = 'INV'
//                       AND    MST_PARTY.PARTY_CATEGORY_ID        = 0
//                       AND    TRN_INVOICE_HEADER.INVOICE_DATE   >= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskFromDate) + cmnService.J_DateOperator() + @"
//                       AND    TRN_INVOICE_HEADER.INVOICE_DATE   <= " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() ;

//            if (rbnSort2.Checked == true)
//                strSQL = strSQL + @" AND CASE WHEN (TRN_INVOICE_HEADER.BANK_STATEMENT_DATE IS NULL OR  TRN_INVOICE_HEADER.BANK_STATEMENT_DATE > " + cmnService.J_DateOperator() + dtservice.J_ConvertMMddyyyy(mskToDate) + cmnService.J_DateOperator() + @" ) AND TRN_INVOICE_HEADER.PAYMENT_TYPE_ID <> 4
//                                              THEN TRN_INVOICE_HEADER.NET_AMOUNT 
//                                              ELSE 0
//                                         END > 0 ";


//            strSQL = strSQL + @" ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
//                                          TRN_INVOICE_HEADER.INVOICE_NO";


            crSundryPartySalesCumOutstanding rptSundryPartySalesCumOutstanding = new crSundryPartySalesCumOutstanding();
            rptcls = (ReportClass)rptSundryPartySalesCumOutstanding;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintSundryPartyReconcilation
        private void PrintSundryPartyReconcilation()
        {
            #region Sundry Party reconcilation
            if (rbnSort1.Checked == true)
            {
                strSQL = @" SELECT SUMMARY.GROUP_ID,
                                   SUMMARY.GROUP_CODE,
                                   SUMMARY.GROUP_DESC,
                                   SUM(ISNULL(SUMMARY.NET_AMOUNT,0)) AS NET_AMOUNT,
                                   SUM(ISNULL(SUMMARY.CURRENT_BILL,0)) AS CURRENT_BILL,
                                   SUM(ISNULL(SUMMARY.PREVIOUS_BILL,0)) AS PREVIOUS_BILL
                            FROM (
		                            --------- SUNDRY PARTY INVOICE GENERATED DURING THE PERIOD
		                            SELECT 1                                  AS GROUP_ID,
			                               'BILL'                             AS GROUP_CODE,
			                               'SUNDRY PARTY SALE FOR THE PERIOD' AS GROUP_DESC,
			                               SUM(NET_AMOUNT)                    AS NET_AMOUNT,
			                               0                                  AS CURRENT_BILL,
			                               0                                  AS PREVIOUS_BILL
		                            FROM TRN_INVOICE_HEADER 
		                            WHERE CONVERT(CHAR(6),INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
		                            AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
                            		--- OUTSTANDING BILLS DURING THIS PERIOD 
		                            SELECT 2 AS GROUP_ID,
                            			   'BILL' AS GROUP_CODE,
			                               'OUTSTANDING AMOUNT FOR THIS PERIOD' AS GROUP_DESC,
			                               SUM(NET_AMOUNT) AS NET_AMOUNT,
			                               0 AS CURRENT_BILL,
			                               0 AS PREVIOUS_BILL
		                            FROM TRN_INVOICE_HEADER 
                            		WHERE CONVERT(CHAR(6),INVOICE_DATE,112) >= '201410'
                                    AND   CONVERT(CHAR(6),INVOICE_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
                            		AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            AND  (ACCOUNT_ENTRY_DATE IS NULL OR CONVERT(CHAR(6),ACCOUNT_ENTRY_DATE,112) > " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @")
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
		                            --- RECEIVED DURING THIS PERIOD 
		                            SELECT 3 AS GROUP_ID,
			                               'COLLECTION' AS GROUP_CODE,
			                               'COLLECTION DURING THIS PERIOD' AS GROUP_DESC,
			                               SUM(NET_AMOUNT) AS NET_AMOUNT,
			                               0               AS CURRENT_BILL,
			                               0               AS PREVIOUS_BILL
		                            FROM TRN_INVOICE_HEADER 
		                            WHERE CONVERT(CHAR(6),ACCOUNT_ENTRY_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
		                            AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
		                            --- PREVIOUS BILL AMOUNTS RECEIVED DURING THIS PERIOD 
		                            SELECT 3 AS GROUP_ID,
			                               'COLLECTION' AS GROUP_CODE,
			                               'COLLECTION DURING THIS PERIOD' AS GROUP_DESC,
			                               0 AS NET_AMOUNT ,
			                               0 AS CURRENT_BILL,
			                               SUM(NET_AMOUNT) AS PREVIOUS_BILL
		                            FROM  TRN_INVOICE_HEADER 
		                            WHERE CONVERT(CHAR(6),INVOICE_DATE,112) < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
		                            AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            AND   CONVERT(CHAR(6),ACCOUNT_ENTRY_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @" 
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
		                            --- CURRENT BILL AMOUNTS RECEIVED DURING THIS PERIOD 
		                            SELECT 3 AS GROUP_ID,
			                               'COLLECTION'    AS GROUP_CODE,
			                               'COLLECTION DURING THIS PERIOD' AS GROUP_DESC,
			                               0               AS NET_AMOUNT,
			                               SUM(NET_AMOUNT) AS CURRENT_BILL,
			                               0               AS PREVIOUS_BILL
		                            FROM  TRN_INVOICE_HEADER 
		                            WHERE CONVERT(CHAR(6),INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
		                            AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            AND  CONVERT(CHAR(6),ACCOUNT_ENTRY_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
		                            -------- CURRENT BILLS AGAINST ADVANCE PAYMENT
		                            SELECT 6 AS GROUP_ID,
			                               'BILL' AS GROUP_CODE,
			                               'CURRENT BILLS AGAINST ADVANCE RECEIPT' AS GROUP_DESC,
			                               SUM(NET_AMOUNT) AS NET_AMOUNT,
			                               0 AS CURRENT_BILL,
			                               0 AS PREVIOUS_BILL
		                            FROM  TRN_INVOICE_HEADER 
		                            WHERE CONVERT(CHAR(6),INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND   TRAN_TYPE     = 'INV'
		                            AND   PARTY_ID IN (SELECT PARTY_ID FROM MST_PARTY WHERE PARTY_CATEGORY_ID = 0)
		                            AND   (ACCOUNT_ENTRY_DATE IS NOT NULL AND CONVERT(CHAR(6),ACCOUNT_ENTRY_DATE,112) < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @")
		                            -----------------------------------------------------------------
		                            UNION
		                            -----------------------------------------------------------------
		                            --- UNKNOWN ENTRIES FOR THE PERIOD
		                            SELECT 7 AS GROUP_ID,
			                               'COLLECTION' AS GROUP_CODE,
			                               'UNKNOWN COLLECTION FOR THE PERIOD' AS GROUP_DESC,
			                               SUM(NET_AMOUNT) AS NET_AMOUNT,
			                               0 AS CURRENT_BILL,
			                               0 AS PREVIOUS_BILL
		                            FROM  TRN_INVOICE_HEADER 
		                            WHERE TRAN_TYPE = 'UN'
		                            AND  CONVERT(CHAR(6),INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
		                            AND CANCELLATION_FLAG = 0

                            ) AS SUMMARY
                            GROUP BY SUMMARY.GROUP_ID,
                                                SUMMARY.GROUP_CODE,
                                                SUMMARY.GROUP_DESC
                            ORDER BY  SUMMARY.GROUP_CODE,
                                                SUMMARY.GROUP_ID ";

                crSundryPartyReconcilation rptSundryPartyReconcilation = new crSundryPartyReconcilation();
                rptcls = (ReportClass)rptSundryPartyReconcilation;
            }
            #endregion 
            //-----------------------------------------------------------------------------------------------
            #region Outstanding Amount for this period
            else if (rbnSort2.Checked == true)
            {
                strSQL = @" SELECT ROW_NUMBER() OVER (PARTITION BY 0  ORDER BY INVOICE_DATE,INVOICE_NO) AS ROW_NUM ,
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                   MST_PARTY.PARTY_NAME,
                                   TRN_INVOICE_HEADER.NET_AMOUNT
                            FROM   TRN_INVOICE_HEADER,
                                   MST_PARTY 
                            WHERE CONVERT(CHAR(6),INVOICE_DATE,112) >= '201410'
                            AND   CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND    TRN_INVOICE_HEADER.TRAN_TYPE     = 'INV'
                            AND    TRN_INVOICE_HEADER.PARTY_ID      = MST_PARTY.PARTY_ID
                            AND    MST_PARTY.PARTY_CATEGORY_ID      = 0
                            AND   (TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE IS NULL OR CONVERT(CHAR(6),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,112) > " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @")
                            ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                     TRN_INVOICE_HEADER.INVOICE_NO ";

                crOutstandingAmountForThePeriod rptOutstandingAmountForThePeriod = new crOutstandingAmountForThePeriod();
                rptcls = (ReportClass)rptOutstandingAmountForThePeriod;
            }
            #endregion
            //-----------------------------------------------------------------------------------------------
            #region Collection during this period (Previous)
            else if (rbnSort3.Checked == true)
            {
                strSQL = @" SELECT ROW_NUMBER() OVER (PARTITION BY 0  ORDER BY INVOICE_DATE,INVOICE_NO) AS ROW_NUM,               
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                   MST_PARTY.PARTY_NAME,
                                   TRN_INVOICE_HEADER.NET_AMOUNT,
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,103) AS ACCOUNT_ENTRY_DATE
                            FROM TRN_INVOICE_HEADER,
                                 MST_PARTY 
                            WHERE CONVERT(CHAR(6),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE ,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"  
                            AND   CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112)        <  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND   TRN_INVOICE_HEADER.TRAN_TYPE                                = 'INV'
                            AND   TRN_INVOICE_HEADER.PARTY_ID                                 = MST_PARTY.PARTY_ID
                            AND   MST_PARTY.PARTY_CATEGORY_ID                                 = 0
                            ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                     TRN_INVOICE_HEADER.INVOICE_NO";

                crCollectionDuringPreviousPeriod rptCollectionDuringPreviousPeriod = new crCollectionDuringPreviousPeriod();
                rptcls = (ReportClass)rptCollectionDuringPreviousPeriod;
            }
            #endregion 
            //-----------------------------------------------------------------------------------------------
            #region Collection during this period 
            else if (rbnSort4.Checked == true)
            {
                strSQL = @" SELECT MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                   SUM(TRN_INVOICE_HEADER.NET_AMOUNT) AS NET_AMOUNT
                            FROM   TRN_INVOICE_HEADER,
                                   MST_PAYMENT_TYPE,
                                   MST_PARTY
                            WHERE  CONVERT(CHAR(6),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND    TRN_INVOICE_HEADER.TRAN_TYPE     = 'INV'
                            AND    TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                            AND    TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID
                            AND    MST_PARTY.PARTY_CATEGORY_ID = 0
                            GROUP BY  MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION
                            ORDER BY  MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION";

                crCollectionDuringThisPeriod rptCollectionDuringThisPeriod = new crCollectionDuringThisPeriod();
                rptcls = (ReportClass)rptCollectionDuringThisPeriod;
            }
            #endregion 
            //-----------------------------------------------------------------------------------------------
            #region Sundry Party Sale for the Period
            else if (rbnSort5.Checked == true)
            {
                strSQL = @" SELECT MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                                   SUM(TRN_INVOICE_HEADER.NET_AMOUNT) AS NET_AMOUNT
                            FROM TRN_INVOICE_HEADER,
                                 MST_PAYMENT_TYPE,
                                 MST_PARTY
                            WHERE CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND   TRN_INVOICE_HEADER.TRAN_TYPE     = 'INV'
                            AND   TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                            AND   TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID
                            AND   MST_PARTY.PARTY_CATEGORY_ID = 0
                            GROUP BY  MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION
                            ORDER BY  MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION ";

                crSundryPartySaleForThePeriod rptSundryPartySaleForThePeriod = new crSundryPartySaleForThePeriod();
                rptcls = (ReportClass)rptSundryPartySaleForThePeriod;
            }
            #endregion 
            //-----------------------------------------------------------------------------------------------
            #region Current Bills against Advance Receipt
            else if (rbnSort6.Checked == true)
            {
                strSQL = @" SELECT ROW_NUMBER() OVER (PARTITION BY 0  ORDER BY INVOICE_DATE,INVOICE_NO) AS ROW_NUM,    
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                   MST_PARTY.PARTY_NAME,
                                   TRN_INVOICE_HEADER.NET_AMOUNT,
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,103) AS ACCOUNT_ENTRY_DATE
                            FROM TRN_INVOICE_HEADER,
                                 MST_PARTY 
                            WHERE CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND   CONVERT(CHAR(6),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,112) < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND   TRN_INVOICE_HEADER.TRAN_TYPE     = 'INV'
                            AND   TRN_INVOICE_HEADER.PARTY_ID      = MST_PARTY.PARTY_ID
                            AND   MST_PARTY.PARTY_CATEGORY_ID      = 0
                            ORDER BY INVOICE_DATE,
                                     INVOICE_NO";

                crCurrentBillsAgainstAdvanceReceipt rptCurrentBillsAgainstAdvanceReceipt = new crCurrentBillsAgainstAdvanceReceipt();
                rptcls = (ReportClass)rptCurrentBillsAgainstAdvanceReceipt;
            }
            #endregion 
            //-----------------------------------------------------------------------------------------------
            #region Collection during this period (CURRENT)
            else if (rbnSort7.Checked == true)
            {
                strSQL = @" SELECT ROW_NUMBER() OVER (PARTITION BY 0  ORDER BY INVOICE_DATE,INVOICE_NO) AS ROW_NUM,               
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INV_DATE,
                                   TRN_INVOICE_HEADER.INVOICE_NO,
                                   MST_PARTY.PARTY_NAME,
                                   TRN_INVOICE_HEADER.NET_AMOUNT,
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE,103) AS ACCOUNT_ENTRY_DATE
                            FROM TRN_INVOICE_HEADER,
                                 MST_PARTY 
                            WHERE CONVERT(CHAR(6),TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE ,112) = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"  
                            AND   CONVERT(CHAR(6),TRN_INVOICE_HEADER.INVOICE_DATE,112)        = " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMM(mskFromMonth) + cmnService.J_DateOperator() + @"
                            AND   TRN_INVOICE_HEADER.TRAN_TYPE                                = 'INV'
                            AND   TRN_INVOICE_HEADER.PARTY_ID                                 = MST_PARTY.PARTY_ID
                            AND   MST_PARTY.PARTY_CATEGORY_ID                                 = 0
                            ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                     TRN_INVOICE_HEADER.INVOICE_NO";

                crCollectionDuringCurrentPeriod rptCollectionDuringCurrentPeriod = new crCollectionDuringCurrentPeriod();
                rptcls = (ReportClass)rptCollectionDuringCurrentPeriod;
            }
            #endregion 

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "For Month :" + mskFromMonth.Text );

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion


        #region PrintPartyListSales
        public void PrintPartyListSales()
        {
            strSQL = @"SELECT ROW_NUMBER() OVER(ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,PARTY_NAME) AS SRL_NO,
                              TRN_INVOICE_HEADER.INVOICE_DATE                             AS INV_DATE,
                              CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)       AS INV_DATE_DDMMYYYY,
                              TRN_INVOICE_HEADER.PARTY_ID                                 AS PARTY_ID, 
                              MST_PARTY.PARTY_NAME                                        AS PARTY_NAME,
                              MST_PARTY.CONTACT_PERSON                                    AS CONTACT_PERSON,
                              MST_PARTY.MOBILE_NO                                         AS MOBILE_NO,
                              MST_PARTY.EMAIL_ID                                          AS EMAIL_ID
                       FROM   TRN_INVOICE_DETAIL 
                              INNER JOIN TRN_INVOICE_HEADER 
                              ON TRN_INVOICE_DETAIL.INVOICE_HEADER_ID =  TRN_INVOICE_HEADER.INVOICE_HEADER_ID 
                              INNER JOIN MST_PARTY
                              ON TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID 
                       WHERE  ITEM_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex);

            crPartyListSales rptPartyListSales = new crPartyListSales();
            rptcls = (ReportClass)rptPartyListSales;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtItemName", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintOutstandingPayments
        public void PrintOutstandingPayments()
        {
            strSQL = @" SELECT ROW_NUMBER() OVER(ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.INVOICE_NO) AS SRL_NO,
                               MST_COMPANY.COMPANY_NAME,
                               TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DT,
                               TRN_INVOICE_HEADER.INVOICE_NO,
                               MST_PARTY.PARTY_NAME,
                               TRN_INVOICE_HEADER.NET_AMOUNT,
                               ISNULL(COLLECTION_DESC.COLLECTION_AMT,0) AS COLLECTION_AMT,
                               TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(COLLECTION_DESC.COLLECTION_AMT,0) AS OUTSTANDING_AMT
                        FROM   TRN_INVOICE_HEADER
                               INNER JOIN MST_PARTY
                               ON TRN_INVOICE_HEADER.PARTY_ID   = MST_PARTY.PARTY_ID 
                               INNER JOIN MST_COMPANY 
                               ON TRN_INVOICE_HEADER.COMPANY_ID   = MST_COMPANY.COMPANY_ID
                               LEFT JOIN
                               (    SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
				                           ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS COLLECTION_AMT
			                        FROM   TRN_COLLECTION_DETAIL,
				                           TRN_COLLECTION_HEADER
			                        WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID                 = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
			                        AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)<= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"  
			                        GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                               )AS COLLECTION_DESC
                               ON  TRN_INVOICE_HEADER.INVOICE_HEADER_ID              = COLLECTION_DESC.INVOICE_HEADER_ID
                        WHERE  TRN_INVOICE_HEADER.RECON_FLAG                         = 0 
                        AND    MST_PARTY.PARTY_CATEGORY_ID                           = 0
                        AND    TRN_INVOICE_HEADER.COMPANY_ID                         = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                     @" AND    TRN_INVOICE_HEADER.TRAN_TYPE                          = 'INV'
                        AND    CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"  
                        AND    TRN_INVOICE_HEADER.NET_AMOUNT                         > ISNULL(COLLECTION_DESC.COLLECTION_AMT,0)
                        ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                 TRN_INVOICE_HEADER.INVOICE_NO ";



            crOutstandingPayment rptOutstandingPayment = new crOutstandingPayment();
            rptcls = (ReportClass)rptOutstandingPayment;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "Ason :" + mskAsOnDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintUnknownCollectionEntry
        public void PrintUnknownCollectionEntry()
        {
            strSQL = @"SELECT  ROW_NUMBER() OVER(ORDER BY COLLECTION_DATE,COLLECTION_HEADER_ID) AS SRL_NO,
                               TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                               CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103)      AS COLLECTION_DATE,
                               ISNULL(MST_COMPANY.COMPANY_NAME,'')                              AS COMPANY_NAME,
                               ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'')             AS PAYMENT_TYPE_DESCRIPTION,
                               ISNULL(MST_BANK.BANK_NAME,'')                                    AS BANK_NAME,
                               TRN_COLLECTION_HEADER.REFERENCE_NO,
                               TRN_COLLECTION_HEADER.NET_AMT,
                               TRN_COLLECTION_HEADER.COLLECTION_REMARKS
                        FROM   TRN_COLLECTION_HEADER
                               LEFT JOIN MST_COMPANY
                               ON TRN_COLLECTION_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID
                               LEFT JOIN MST_PAYMENT_TYPE
                               ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID 
                               LEFT JOIN MST_BANK 
                               ON TRN_COLLECTION_HEADER.BANK_ID = MST_BANK.BANK_ID 
                        WHERE  CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)<= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"  
                        AND    (RECONCILIATION_DATE IS NULL  OR CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112) > " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @")
                        ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                 TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID ";


            crUnknownCollectionEntry rptUnknownCollectionEntry = new crUnknownCollectionEntry();
            rptcls = (ReportClass)rptUnknownCollectionEntry;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "AsOn :" + mskAsOnDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintKnownCollectionEntry
        public void PrintKnownCollectionEntry()
        {
            strSQL = @"   SELECT  MST_COMPANY.COMPANY_NAME                                                       AS COMPANY_NAME,
                                  TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID                                     AS COLLECTION_HEADER_ID,
                                  CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103)                    AS COLLECTION_DT,
                                  ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'')                           AS PAYMENT_TYPE_DESCRIPTION,
                                  ISNULL(MST_BANK.BANK_NAME,'')                                                  AS BANK_NAME,
                                  TRN_COLLECTION_HEADER.REFERENCE_NO                                             AS COLLECTION_REF_NO,
                                  TRN_COLLECTION_HEADER.GROSS_AMT                                                AS COLLECTION_AMT,
                                  ISNULL(COLLECTION_HEADER_SUM.COLLECTION_AMT,0)                                 AS COLLECTION_BREAKUP_AMT,
                                  TRN_COLLECTION_HEADER.GROSS_AMT-ISNULL(COLLECTION_HEADER_SUM.COLLECTION_AMT,0) AS OUTSTANDING_AMT,
                                  TRN_INVOICE_HEADER.INVOICE_NO                                                  AS INVOICE_NO,  
                                  CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)                          AS INVOICE_DATE,
                                  TRN_INVOICE_HEADER.REFERENCE_NO                                                AS REFERENCE_NO,
                                  ISNULL(MST_PARTY.PARTY_NAME,'')                                                AS PARTY_NAME, 
                                  TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT                                        AS TAGGED_AMOUNT 
                          FROM    TRN_COLLECTION_HEADER 
                                  LEFT JOIN MST_COMPANY
                                   ON TRN_COLLECTION_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID  
                                  LEFT JOIN MST_FAYEAR
                                   ON TRN_COLLECTION_HEADER.FAYEAR_ID = MST_FAYEAR.FAYEAR_ID
                                  LEFT JOIN MST_BANK 
                                   ON TRN_COLLECTION_HEADER.BANK_ID = MST_BANK.BANK_ID 
                                  LEFT JOIN MST_PAYMENT_TYPE 
                                   ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                  INNER JOIN TRN_COLLECTION_DETAIL 
                                   ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID 
                                  INNER JOIN TRN_INVOICE_HEADER 
                                   ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                                  LEFT JOIN MST_PARTY
                                   ON TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID  
                                  INNER JOIN
                                  (    
                                    SELECT TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID,
                                           ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS COLLECTION_AMT
                                    FROM   TRN_COLLECTION_DETAIL,
                                           TRN_COLLECTION_HEADER
                                    WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID                 = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
                                    AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)>= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"  
                                    AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)<= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"  
                                    GROUP BY TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID
                                   )AS COLLECTION_HEADER_SUM
		                            ON  TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID       = COLLECTION_HEADER_SUM.COLLECTION_HEADER_ID
                          WHERE    TRN_COLLECTION_HEADER.COMPANY_ID                      = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                          AND      TRN_INVOICE_HEADER.RECON_FLAG                         = 0 
                          AND      MST_PARTY.PARTY_CATEGORY_ID                           = 0
                          AND      (TRN_COLLECTION_HEADER.RECONCILIATION_DATE  IS NOT NULL 
                                    AND CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE ,112)>= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"  
                                    AND CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" )";

            if (cmbCombo2.SelectedIndex > 0)
                strSQL = strSQL + @" AND  MST_PAYMENT_TYPE.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);

            if (cmbCombo3.SelectedIndex > 0)
                strSQL = strSQL + @" AND  MST_BANK.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);

            strSQL = strSQL + @" ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                          TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                                          TRN_INVOICE_HEADER.INVOICE_DATE,
                                          TRN_INVOICE_HEADER.INVOICE_NO ";

            crTaggedCollectionEntry rptTaggedCollectionEntry = new crTaggedCollectionEntry();
            rptcls = (ReportClass)rptTaggedCollectionEntry;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintInvoicePaymentStatus
        public void PrintInvoicePaymentStatus()
        {
            strSQL = @" SELECT MST_COMPANY.COMPANY_NAME                                                AS COMPANY_NAME,
                               TRN_INVOICE_HEADER.INVOICE_HEADER_ID                                    AS INVOICE_HEADER_ID,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)                   AS INVOICE_DATE,
                               TRN_INVOICE_HEADER.INVOICE_NO                                           AS INVOICE_NO,
                               TRN_INVOICE_HEADER.NET_AMOUNT                                           AS INV_AMOUNT,
                               TRN_INVOICE_HEADER.NET_AMOUNT - COLLECTION_HEADER_SUM.COLLECTION_AMT    AS OUTSTANDING_AMOUNT,
                               TRN_INVOICE_HEADER.REFERENCE_NO                                         AS INV_REFERENCE_NO,
                               MST_PARTY.PARTY_NAME                                                    AS PARTY_NAME,
                               ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID,0)                    AS COLLECTION_HEADER_ID,
                               ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,0)                       AS COLLECTION_AMOUNT,
                               ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103),'')  AS COLLECTION_DATE,
                               ISNULL(MST_BANK.BANK_NAME,'')                                           AS BANK_NAME,
                               ISNULL(TRN_COLLECTION_HEADER.REFERENCE_NO,'')                           AS COLLECTION_REFERENCE_NO,
                               ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'')                    AS COLLECTION_PAYMENT_TYPE
                        FROM   TRN_INVOICE_HEADER 
                               LEFT JOIN TRN_COLLECTION_DETAIL
                                ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                               LEFT JOIN TRN_COLLECTION_HEADER
                                ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
                               LEFT JOIN MST_PARTY
                                ON TRN_INVOICE_HEADER.PARTY_ID = MST_PARTY.PARTY_ID
                               LEFT JOIN MST_PAYMENT_TYPE 
                                ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE .PAYMENT_TYPE_ID
                               LEFT JOIN MST_BANK 
                                ON TRN_COLLECTION_HEADER.BANK_ID = MST_BANK.BANK_ID
                               LEFT JOIN MST_COMPANY
                                ON TRN_INVOICE_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID
                               LEFT JOIN
                                  (    
                                    SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                           ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0) AS COLLECTION_AMT
                                    FROM   TRN_COLLECTION_DETAIL
                                           INNER JOIN TRN_COLLECTION_HEADER
                                            ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
                                           INNER JOIN TRN_INVOICE_HEADER
                                            ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = TRN_INVOICE_HEADER.INVOICE_HEADER_ID 
                                    WHERE  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112) >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                                    AND    CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" 
                                    GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                   )AS COLLECTION_HEADER_SUM
                                ON  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = COLLECTION_HEADER_SUM.INVOICE_HEADER_ID
                        WHERE  TRN_INVOICE_HEADER.TRAN_TYPE      = 'INV'
                        AND    TRN_COLLECTION_HEADER.COMPANY_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                        AND    TRN_INVOICE_HEADER.RECON_FLAG     = 0 
                        AND    MST_PARTY.PARTY_CATEGORY_ID       = 0   
                        AND    (CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                                AND CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @")";

            if (cmbCombo2.SelectedIndex > 0)
                strSQL = strSQL + @" AND  TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);

            if (cmbCombo3.SelectedIndex > 0)
                strSQL = strSQL + @" AND  TRN_INVOICE_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);


            strSQL = strSQL + @" ORDER BY TRN_INVOICE_HEADER.INVOICE_DATE,
                                          TRN_INVOICE_HEADER.INVOICE_NO ";


            crInvoicePaymentStatus rptInvoicePaymentStatus = new crInvoicePaymentStatus();
            rptcls = (ReportClass)rptInvoicePaymentStatus;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintInvoiceStatusSummary
        public void PrintInvoiceStatusSummary()
        {
            string strPaymentType = "";
            string strReportTypeWiseDateText  = "";
            string strPartyType = "";
            string strCollectionType = "";
            
            #region COMMENTED CODE  [COMMENTED BY DHRUBA ON 14/04/2015]
//            //----------------------------------------------------------------------------------------
//            //-- INVOICE WISE PAYMENT STATUS 
//            //----------------------------------------------------------------------------------------
//            strSQL = @" SELECT SUMMARY.COMPANY_NAME,
//                               SUMMARY.INVOICE_HEADER_ID,
//                               SUMMARY.INVOICE_NO,
//                               SUMMARY.INVOICE_DT,
//                               SUMMARY.PARTY_NAME,
//                               SUMMARY.TYPE,
//                               SUMMARY.DELIVERY_MODE,
//                               SUMMARY.PAYMENT_TYPE,
//                               SUMMARY.REFERENCE_NO,
//                               SUMMARY.INVOICE_AMT,
//                               SUMMARY.COLLECTION_AMT,
//                               SUMMARY.INVOICE_AMT - SUMMARY.COLLECTION_AMT AS DUE_AMT
//                        FROM (
//	                            SELECT MST_COMPANY.COMPANY_NAME                                    AS COMPANY_NAME,
//                                       TRN_INVOICE_HEADER.INVOICE_HEADER_ID                        AS INVOICE_HEADER_ID,
//		                               TRN_INVOICE_HEADER.INVOICE_NO                               AS INVOICE_NO,
//		                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)       AS INVOICE_DT,
//		                               MST_PARTY.PARTY_NAME                                        AS PARTY_NAME,
//		                               CASE WHEN MST_PARTY.PARTY_CATEGORY_ID > 0 
//				                            THEN MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION 
//				                            ELSE '' 
//		                               END                                                         AS TYPE,
//		                               PAR_DELIVERY_MODE.DELIVERY_MODE_SHORT_DESC                  AS DELIVERY_MODE,
//		                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                   AS PAYMENT_TYPE,
//		                               TRN_INVOICE_HEADER.REFERENCE_NO                             AS REFERENCE_NO,
//		                               TRN_INVOICE_HEADER.NET_AMOUNT                               AS INVOICE_AMT,
//		                               ISNULL(SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT),0)      AS COLLECTION_AMT
//	                            FROM   TRN_INVOICE_HEADER
//	                                   INNER JOIN MST_PARTY             
//	                                    ON TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID
//	                                   INNER JOIN MST_PARTY_CATEGORY    
//	                                    ON MST_PARTY.PARTY_CATEGORY_ID          = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
//	                                   INNER JOIN PAR_DELIVERY_MODE     
//	                                    ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID  = PAR_DELIVERY_MODE.DELIVERY_MODE_ID
//	                                   LEFT  JOIN MST_PAYMENT_TYPE      
//	                                    ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID   = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
//	                                   LEFT  JOIN TRN_COLLECTION_DETAIL 
//	                                    ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
//	                                   INNER JOIN MST_COMPANY
//	                                    ON TRN_INVOICE_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID 
//	                            WHERE  TRN_INVOICE_HEADER.TRAN_TYPE  = 'INV' 
//	                            AND    TRN_INVOICE_HEADER.RECON_FLAG = 0   ";

//            strSQL = strSQL + @" AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
//                                 AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

//            if (cmbCombo1.SelectedIndex > 0)
//                strSQL = strSQL + @" AND TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex);

//            if (rbnSort1.Checked == true)
//                strSQL = strSQL + @" AND MST_PARTY_CATEGORY.PARTY_CATEGORY_ID = 0";

//            strSQL = strSQL + @" GROUP BY MST_COMPANY.COMPANY_NAME,
//                                          TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
//			                              TRN_INVOICE_HEADER.INVOICE_NO,
//			                              CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),
//			                              TRN_INVOICE_HEADER.NET_AMOUNT,
//			                              MST_PARTY.PARTY_NAME,
//			                              CASE WHEN MST_PARTY.PARTY_CATEGORY_ID > 0 
//				                               THEN MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION 
//				                               ELSE '' 
//			                              END,
//			                              PAR_DELIVERY_MODE.DELIVERY_MODE_SHORT_DESC,
//			                              MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION,
//			                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
//			                              TRN_INVOICE_HEADER.REFERENCE_NO
//                                ) AS SUMMARY ";

//            if (rbnSort1_1.Checked == true)
//                strSQL = strSQL + @" WHERE (SUMMARY.INVOICE_AMT - SUMMARY.COLLECTION_AMT) > 0";
//            else if (rbnSort1_2.Checked == true)
//                strSQL = strSQL + @" WHERE (SUMMARY.INVOICE_AMT - SUMMARY.COLLECTION_AMT) < 0";


//            strSQL = strSQL + @" ORDER BY SUMMARY.INVOICE_NO ";

            #endregion 


            //------------------------------------------------------------------------------------
            //--- INVOICE WISE COLLECION & ADJUSTMENT SUMMARY
            //------------------------------------------------------------------------------------
            strSQL = @" SELECT SUMMARY.INVOICE_HEADER_ID                          AS INVOICE_HEADER_ID,
                               SUMMARY.COMPANY_ID                                 AS COMPANY_ID,
                               SUMMARY.COMPANY_NAME                               AS COMPANY_NAME,
                               ROW_NUMBER() OVER (ORDER BY SUMMARY.INVOICE_NO)    AS SL_NO,
                               SUMMARY.INVOICE_NO                                 AS INVOICE_NO,
                               SUMMARY.INVOICE_DT                                 AS INVOICE_DT,
                               SUMMARY.PARTY_NAME                                 AS PARTY_NAME,
                               SUMMARY.TYPE                                       AS TYPE,
                               SUMMARY.DELIVERY_MODE                              AS DELIVERY_MODE,
                               SUMMARY.PAYMENT_TYPE                               AS PAYMENT_TYPE,
                               SUMMARY.REFERENCE_NO                               AS REFERENCE_NO,
                               SUMMARY.INVOICE_AMT                                AS INVOICE_AMT,
                               SUMMARY.COLLECTION_AMT                             AS COLLECTION_AMT, 
                               SUMMARY.ADJUSTMENT_AMT                             AS ADJUSTMENT_AMT, 
                               SUMMARY.DUE_AMT                                    AS DUE_AMT,
                               CASE WHEN SUMMARY.DUE_AMT < 0 THEN '*' ELSE '' END AS MARK
                        FROM (
			                        SELECT  TRN_INVOICE_HEADER.INVOICE_HEADER_ID                    AS INVOICE_HEADER_ID,
					                        TRN_INVOICE_HEADER.COMPANY_ID                           AS COMPANY_ID ,
					                        MST_COMPANY.COMPANY_NAME                                AS COMPANY_NAME,
					                        TRN_INVOICE_HEADER.INVOICE_NO                           AS INVOICE_NO,
					                        CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)   AS INVOICE_DT,
					                        MST_PARTY.PARTY_NAME                                    AS PARTY_NAME,
					                        CASE WHEN MST_PARTY.PARTY_CATEGORY_ID > 0 
						                         THEN MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION 
						                         ELSE '' 
					                        END                                                     AS TYPE,
					                        PAR_DELIVERY_MODE.DELIVERY_MODE_SHORT_DESC              AS DELIVERY_MODE,
					                        MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION               AS PAYMENT_TYPE,
					                        TRN_INVOICE_HEADER.REFERENCE_NO                         AS REFERENCE_NO,
					                        TRN_INVOICE_HEADER.NET_AMOUNT                           AS INVOICE_AMT,
					                        ISNULL(TRN_COLL_ADJ.NET_COLLECTION_AMOUNT,0)            AS COLLECTION_AMT, 
					                        ISNULL(TRN_COLL_ADJ.NET_ADJUSTMENT_AMOUNT,0)            AS ADJUSTMENT_AMT, 
					                       (TRN_INVOICE_HEADER.NET_AMOUNT - 
					                        ISNULL(TRN_COLL_ADJ.NET_COLLECTION_AMOUNT,0) - 
					                        ISNULL(TRN_COLL_ADJ.NET_ADJUSTMENT_AMOUNT,0))           AS DUE_AMT
			                        FROM  TRN_INVOICE_HEADER
				                          INNER JOIN MST_COMPANY           
				                          ON TRN_INVOICE_HEADER.COMPANY_ID         = MST_COMPANY.COMPANY_ID
				                          INNER JOIN MST_PARTY             
				                          ON TRN_INVOICE_HEADER.PARTY_ID           = MST_PARTY.PARTY_ID
				                          INNER JOIN MST_PARTY_CATEGORY    
				                          ON MST_PARTY.PARTY_CATEGORY_ID           = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
				                          INNER JOIN PAR_DELIVERY_MODE     
				                          ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID   = PAR_DELIVERY_MODE.DELIVERY_MODE_ID
				                          LEFT  JOIN MST_PAYMENT_TYPE      
				                          ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID    = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
				                          LEFT  JOIN 
				                          (
					                        SELECT COLLADJ.INVOICE_HEADER_ID,
						                           ISNULL(SUM(COLLADJ.NET_COLLECTION_AMOUNT),0) AS NET_COLLECTION_AMOUNT,
						                           ISNULL(SUM(COLLADJ.NET_ADJUSTMENT_AMOUNT),0) AS NET_ADJUSTMENT_AMOUNT
					                        FROM(
							                          ------------------------------------------------------------------------
							                          --------- NET COLLECTION AGAINST INVOICES
							                          ------------------------------------------------------------------------
							                          SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
									                         SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS NET_COLLECTION_AMOUNT,
									                         0                                            AS NET_ADJUSTMENT_AMOUNT
							                          FROM   TRN_COLLECTION_DETAIL,
									                         TRN_COLLECTION_HEADER
							                          WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
							                          AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG      = 0
							                          GROUP  BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
							                          ------------------------------------------------------------------------
							                          UNION
							                          ------------------------------------------------------------------------
							                          --------- NET ADJUSTMENT AGAINST INVOICES
							                          ------------------------------------------------------------------------
							                          SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
									                         0                                             AS NET_COLLECTION_AMOUNT,
									                         SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT)  AS NET_ADJUSTMENT_AMOUNT
							                          FROM   TRN_COLLECTION_DETAIL,
									                         TRN_COLLECTION_HEADER
							                          WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
							                          AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG      = 1
							                          GROUP  BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID    
						                        ) AS COLLADJ
					                        GROUP BY COLLADJ.INVOICE_HEADER_ID
				                         ) AS TRN_COLL_ADJ 
				                         ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID     = TRN_COLL_ADJ.INVOICE_HEADER_ID
		                        WHERE  (TRN_INVOICE_HEADER.TRAN_TYPE = 'INV' OR TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV')
		                        AND    TRN_INVOICE_HEADER.RECON_FLAG                          = 0 ";

            if(rbnSort2_1.Checked == true)
            {
                strSQL = strSQL + @" AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

                strReportTypeWiseDateText = "As On :" + mskToDate.Text;
            }
            else 
            {
                strSQL = strSQL + @" AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                                     AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

                strReportTypeWiseDateText = "From " + mskFromDate.Text + " to " + mskToDate.Text;
            }

            if (cmbCombo1.SelectedIndex > 0)
                strSQL = strSQL + @" AND TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex);

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND MST_PAYMENT_TYPE.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (rbnSort1.Checked == true)
            {
                strSQL = strSQL + @" AND MST_PARTY_CATEGORY.PARTY_CATEGORY_ID = 0";
                strPartyType = "Party Category : Sundry";
            }


            if (rbnSort1_1.Checked == true)
            {
                strSQL = strSQL + @" AND (TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRN_COLL_ADJ.NET_COLLECTION_AMOUNT,0) - ISNULL(TRN_COLL_ADJ.NET_ADJUSTMENT_AMOUNT,0)) <> 0";
                strCollectionType = "Collection Type : Pendings Only";
            }

            strSQL = strSQL + @" ) AS SUMMARY
                                ORDER BY SUMMARY.INVOICE_NO ";

            crInvoiceStatus rptInvoiceStatus = new crInvoiceStatus();
            rptcls = (ReportClass)rptInvoiceStatus;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", strReportTypeWiseDateText);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPartyType", strPartyType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCollectionType", strCollectionType);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintInvoiceStatusDetail
        public void PrintInvoiceStatusDetail()
        {
            string strReportTypeWiseDateText = "";
            string strPaymentType = "";
            string strPartyType = "";
            string strCollectionType = "";
            string strBankName = "";

            strSQL = @" SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID                                   AS INVOICE_HEADER_ID,
                               TRN_INVOICE_HEADER.COMPANY_ID                                          AS COMPANY_ID,
                               MST_COMPANY.COMPANY_NAME                                               AS COMPANY_NAME,
                               TRN_INVOICE_HEADER.INVOICE_NO                                          AS INVOICE_NO,
                               CASE WHEN MST_PARTY_CATEGORY.PARTY_CATEGORY_ID = 0  THEN ''
                                    WHEN MST_PARTY_CATEGORY.PARTY_CATEGORY_ID = 1  THEN 'D'  
                                    ELSE 'O'
                               END                                                                    AS PARTY_TYPE,
                               INV_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                              AS INV_PAYMENT_TYPE,
                               TRN_INVOICE_HEADER.REFERENCE_NO                                        AS INV_REFERENCE_NO,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103)                  AS INVOICE_DT,
                               MST_PARTY.PARTY_NAME                                                   AS PARTY_NAME,
                               TRN_INVOICE_HEADER.NET_AMOUNT                                          AS INVOICE_AMT,
                               TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRN_COLL_ADJ.COLL_ADJ_AMOUNT,0) AS DUE_AMT,
                               ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103),'') AS TRAN_DT,
                               ISNULL(TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID,0)                        AS PAYMENT_TYPE_ID,
                               ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'')                   AS PAYMENT_TYPE,
                               ISNULL(TRN_COLLECTION_HEADER.BANK_ID,0)                                AS BANK_ID,
                               ISNULL(MST_BANK.BANK_NAME,'')                                          AS BANK,
                               ISNULL(TRN_COLLECTION_HEADER.REFERENCE_NO,'')                          AS REFERENCE_NO,
                               ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,0)                      AS TRAN_AMT,
                               CASE WHEN (DENSE_RANK() OVER(PARTITION BY TRN_INVOICE_HEADER.INVOICE_NO ORDER BY TRN_INVOICE_HEADER.INVOICE_NO) = 
                                          ROW_NUMBER() OVER(PARTITION BY TRN_INVOICE_HEADER.INVOICE_NO ORDER BY TRN_INVOICE_HEADER.INVOICE_NO))
                                    THEN TRN_INVOICE_HEADER.NET_AMOUNT
                                    ELSE 0
                                    END                                                               AS DISTINCT_INV_AMT,
                               CASE WHEN (DENSE_RANK() OVER(PARTITION BY TRN_INVOICE_HEADER.INVOICE_NO ORDER BY TRN_INVOICE_HEADER.INVOICE_NO) = 
                                          ROW_NUMBER() OVER(PARTITION BY TRN_INVOICE_HEADER.INVOICE_NO ORDER BY TRN_INVOICE_HEADER.INVOICE_NO))
                                    THEN TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRN_COLL_ADJ.COLL_ADJ_AMOUNT,0)
                                    ELSE 0
                                    END                                                               AS DISTINCT_DUE_AMT
                        FROM   TRN_INVOICE_HEADER
                        INNER JOIN MST_PARTY                             ON TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID
                        INNER JOIN MST_PAYMENT_TYPE  AS INV_PAYMENT_TYPE ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID         = INV_PAYMENT_TYPE.PAYMENT_TYPE_ID 
                        INNER JOIN MST_PARTY_CATEGORY                    ON MST_PARTY.PARTY_CATEGORY_ID                = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
                        INNER JOIN MST_COMPANY                           ON TRN_INVOICE_HEADER.COMPANY_ID              = MST_COMPANY.COMPANY_ID
                        LEFT  JOIN TRN_COLLECTION_DETAIL                 ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID       = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                        LEFT  JOIN TRN_COLLECTION_HEADER                 ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
                        LEFT  JOIN MST_PAYMENT_TYPE                      ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID 
                        LEFT  JOIN MST_BANK                              ON TRN_COLLECTION_HEADER.BANK_ID              = MST_BANK.BANK_ID
                        LEFT  JOIN 
                        (
                              SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                     SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS COLL_ADJ_AMOUNT 
                              FROM   TRN_COLLECTION_DETAIL,
                                     TRN_COLLECTION_HEADER
                              WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
                              GROUP  BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                        ) AS TRN_COLL_ADJ ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID     = TRN_COLL_ADJ.INVOICE_HEADER_ID
                        WHERE  (TRN_INVOICE_HEADER.TRAN_TYPE = 'INV' OR TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV')
                        AND    TRN_INVOICE_HEADER.RECON_FLAG   = 0 
                        AND    TRN_INVOICE_HEADER.COMPANY_ID   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex);


            if (rbnSort2_1.Checked == true)
            {
                strSQL = strSQL + @" AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

                strReportTypeWiseDateText = "As On : " + mskToDate.Text;
            }
            else
            {
                strSQL = strSQL + @" AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                                     AND  CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

                strReportTypeWiseDateText = "From " + mskFromDate.Text + " to " + mskToDate.Text;
            }


            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            if (rbnSort1.Checked == true)
            {
                strSQL = strSQL + @" AND MST_PARTY_CATEGORY.PARTY_CATEGORY_ID = 0";
                strPartyType = "Party Category : Sundry";
            }


            if (rbnSort1_1.Checked == true)
            {
                strSQL = strSQL + @" AND TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRN_COLL_ADJ.COLL_ADJ_AMOUNT,0) <> 0";
                strCollectionType = "Collection Type : Pendings Only";
            }

            strSQL = strSQL + @" ORDER BY TRN_INVOICE_HEADER.INVOICE_NO,
                                          TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                          TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID ";



            crCollectionWiseTaggedInvoices rptCollectionWiseTaggedInvoices = new crCollectionWiseTaggedInvoices();
            rptcls = (ReportClass)rptCollectionWiseTaggedInvoices;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", strReportTypeWiseDateText);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPartyType", strPartyType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCollectionType", strCollectionType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion

        #region PrintPaymentStatusDetail
        public void PrintPaymentStatusDetail()
        {
            string strCollectionType = "";
            string strPaymentType = "";
            string strBankName = "";
            //---------------------------------------------------------------------------------------------------------------------
            strSQL = @" SELECT CASE WHEN ROW_NUMBER() OVER(PARTITION BY TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID 
                                                           ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,TRN_INVOICE_HEADER.INVOICE_NO ) <= 1 
                                    THEN TRN_COLLECTION_HEADER.NET_AMT
                                    ELSE 0
                               END                                                                        AS SUM_NET_AMT,
                               CASE WHEN ROW_NUMBER() OVER(PARTITION BY TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID 
                                                           ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,TRN_INVOICE_HEADER.INVOICE_NO ) <= 1 
                                    THEN TRN_COLLECTION_HEADER.DUE_AMT
                                    ELSE 0
                               END                                                                         AS SUM_DUE_AMT,
                               ROW_NUMBER() OVER(PARTITION BY TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID 
                                                 ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                                          TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                                                          TRN_INVOICE_HEADER.INVOICE_NO )                  AS SRL_NO,
                               TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID                                  AS COLLECTION_HEADER_ID,
                               MST_COMPANY.COMPANY_NAME                                                    AS COMPANY_NAME,
                               CASE WHEN TRN_COLLECTION_HEADER.AUTO_POST_FLAG = 0 THEN 'M' ELSE '' END     AS POST_TYPE,
                               CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103)                 AS COLLECTION_DT,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                                   AS PAYMENT_TYPE,
                               TRN_COLLECTION_HEADER.REFERENCE_NO                                          AS REF_NO,
                               ISNULL(MST_BANK.BANK_NAME,'')                                               AS BANK_NAME,
                               TRN_COLLECTION_HEADER.NET_AMT                                               AS COLLECTION_AMOUNT,
                               TRN_COLLECTION_HEADER.DUE_AMT                                               AS DUE_AMOUNT,
                               ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'')  AS RECONCILIATION_DT,
                               -------------------------------------------------------------------------------------------------
                               -------- DETAILS OF INVOICES (IF ANY) TAGGED TO A COLLECTION
                               -------------------------------------------------------------------------------------------------
                               ISNULL(TRN_INVOICE_HEADER.INVOICE_NO,'')                                    AS INVOICE_NO,
                               ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),'')            AS INV_DATE,
                               ISNULL(MST_PARTY.PARTY_NAME,'')                                             AS PARTY_NAME,
                               ---------- REFERENCE FOR CC AVENUE INVOICES
                               CASE WHEN TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = 5
                                    THEN  ISNULL(TRN_INVOICE_HEADER.REFERENCE_NO,'')
                                    ELSE ''
                               END                                                                         AS IREF_NO,     
                               ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,0)                           AS TAGGED_AMT
                        FROM   TRN_COLLECTION_HEADER
                               INNER JOIN MST_COMPANY          ON TRN_COLLECTION_HEADER.COMPANY_ID           = MST_COMPANY.COMPANY_ID
                               INNER JOIN MST_PAYMENT_TYPE     ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                               LEFT JOIN MST_BANK              ON TRN_COLLECTION_HEADER.BANK_ID              = MST_BANK.BANK_ID
                               LEFT JOIN TRN_COLLECTION_DETAIL ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID              
                               LEFT JOIN TRN_INVOICE_HEADER    ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID    = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                               LEFT JOIN MST_PARTY             ON TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID     
                        WHERE  TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0  " +
                     @" AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                        AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator();

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            if (rbnSort1.Checked == true)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.DUE_AMT <> 0";
                strCollectionType = "Collection Type : Pendings Only";
            }

            strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.TALLIED = 0
                                 ORDER BY  TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                          TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                                          TRN_INVOICE_HEADER.INVOICE_NO";


            crPaymentStatusDetail rptPaymentStatusDetail = new crPaymentStatusDetail();
            rptcls = (ReportClass)rptPaymentStatusDetail;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCollectionType", strCollectionType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintPaymentStatusSummary
        public void PrintPaymentStatusSummary()
        {
            string strCollectionType = "";
            string strPaymentType = "";
            string strBankName = "";
            //---------------------------------------------------------------------------------------------------------------------

            strSQL = @" SELECT TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID                                  AS COLLECTION_HEADER_ID,
                               MST_COMPANY.COMPANY_NAME                                                    AS COMPANY_NAME,
                               CASE WHEN TRN_COLLECTION_HEADER.AUTO_POST_FLAG = 0 THEN 'M' ELSE '' END     AS POST_TYPE,
                               CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103)                 AS COLLECTION_DT,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                                   AS PAYMENT_TYPE,
                               TRN_COLLECTION_HEADER.REFERENCE_NO                                          AS REF_NO,
                               ISNULL(MST_BANK.BANK_NAME,'')                                               AS BANK_NAME,
                               TRN_COLLECTION_HEADER.NET_AMT                                               AS COLLECTION_AMOUNT,
                               TRN_COLLECTION_HEADER.DUE_AMT                                               AS DUE_AMOUNT,
                               TRN_COLLECTION_HEADER.COLLECTION_REMARKS                                    AS COLLECTION_REMARKS,
                               ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'')  AS RECONCILIATION_DT 
                        FROM   TRN_COLLECTION_HEADER
                        INNER  JOIN MST_COMPANY          ON TRN_COLLECTION_HEADER.COMPANY_ID           = MST_COMPANY.COMPANY_ID
                        INNER  JOIN MST_PAYMENT_TYPE     ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                        LEFT   JOIN MST_BANK             ON TRN_COLLECTION_HEADER.BANK_ID              = MST_BANK.BANK_ID
                        WHERE TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0 "+
                     @" AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)>=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                        AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112)<=  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator(); 

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            if (rbnSort1.Checked == true)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.DUE_AMT <> 0";
                strCollectionType = "Collection Type : Pendings Only";
            }

            strSQL = strSQL + @" AND  TRN_COLLECTION_HEADER.TALLIED = 0
                                 ORDER BY  TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                           TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID ";


            crPaymentStatusSummary rptPaymentStatusSummary = new crPaymentStatusSummary();
            rptcls = (ReportClass)rptPaymentStatusSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCollectionType", strCollectionType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintReconciliationDetail
        public void PrintReconciliationDetail()
        {

            string strPaymentType = "";
            string strBankName = "";
            //---------------------------------------------------------------------------------------------------------------------

            strSQL = @" SELECT TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                               MST_COMPANY.COMPANY_NAME,
                               ROW_NUMBER() OVER(PARTITION BY TRN_COLLECTION_HEADER.RECONCILIATION_DATE ORDER BY TRN_COLLECTION_HEADER.RECONCILIATION_DATE,TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID) AS RCON_DT_SERIAL_NO,
                               CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103) AS RECON_DATE,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                               ISNULL(MST_BANK.BANK_NAME,'') AS BANK_NAME,
                               TRN_COLLECTION_HEADER.REFERENCE_NO,
                               --TRN_COLLECTION_HEADER.NET_AMT,
                               CASE WHEN TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = 5 
                                    THEN TRN_COLLECTION_HEADER.GROSS_AMT 
                                    ELSE TRN_COLLECTION_HEADER.NET_AMT 
                               END                          AS NET_AMT,
                               ISNULL(INVREF.INVOICE_NO,'') AS INVOICE_NO,
                               ISNULL(INVREF.INVOICE_DT,'') AS INVOICE_DT,
                               ISNULL(INVREF.PARTY_NAME,'') AS PARTY_NAME,
                               TRN_COLLECTION_HEADER.NET_INVOICE_AMT,
                               CASE WHEN TRN_COLLECTION_HEADER.DUE_AMT <> 0 THEN '*' ELSE '' END AS MARK,
                               SUM(CASE WHEN TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = 5 
                                        THEN TRN_COLLECTION_HEADER.GROSS_AMT 
                                        ELSE TRN_COLLECTION_HEADER.NET_AMT 
                                   END)  OVER(PARTITION BY TRN_COLLECTION_HEADER.RECONCILIATION_DATE)                                  AS SUM_NET_AMOUNT_RECON_DATE_WISE,
                               SUM(CASE WHEN TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = 5 
                                        THEN TRN_COLLECTION_HEADER.GROSS_AMT 
                                        ELSE TRN_COLLECTION_HEADER.NET_AMT 
                                   END)  OVER()                                                                                        AS SUM_NET_AMOUNT,
                               SUM(TRN_COLLECTION_HEADER.NET_INVOICE_AMT) OVER(PARTITION BY TRN_COLLECTION_HEADER.RECONCILIATION_DATE) AS  SUM_NET_INV_AMOUNT_RECON_DATE_WISE,
                               SUM(TRN_COLLECTION_HEADER.NET_INVOICE_AMT)  OVER() AS SUM_NET_INV_AMOUNT
                        FROM   TRN_COLLECTION_HEADER
                               INNER JOIN MST_PAYMENT_TYPE ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                               INNER JOIN MST_COMPANY      ON TRN_COLLECTION_HEADER.COMPANY_ID      = MST_COMPANY.COMPANY_ID
                               LEFT  JOIN MST_BANK         ON TRN_COLLECTION_HEADER.BANK_ID         = MST_BANK.BANK_ID
                               -------------- non - cc avenue --- tagging
                               LEFT  JOIN (SELECT TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID,
	       			                              TRN_INVOICE_HEADER.INVOICE_NO,
				                                  CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DT,
				                                  MST_PARTY.PARTY_NAME,
				                                  TRN_INVOICE_HEADER.NET_AMOUNT,
				                                  TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT
			                               FROM   TRN_COLLECTION_DETAIL,
                                                  TRN_COLLECTION_HEADER,
                                                  TRN_INVOICE_HEADER,
				                                  MST_PARTY
			                               WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID              
			                               AND    TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID    = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
			                               AND    TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID
			                               AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG      = 0
			                               AND    TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      <> " + BS_PaymentTypeId.Cc_Avenue + @" )AS INVREF 
                               ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = INVREF.COLLECTION_HEADER_ID
                        WHERE  TRN_COLLECTION_HEADER.COMPANY_ID                      = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + 
            @" AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG                          = 0
               AND    TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS NOT NULL  " +
            @" AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112)>= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + 
            @" AND    CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112)<= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate)   + 
            @" AND    TRN_COLLECTION_HEADER.TALLIED                                  = 0 ";

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            strSQL = strSQL + @" ORDER BY TRN_COLLECTION_HEADER.RECONCILIATION_DATE,
                                          TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID";


            crReconciliationDetail rptReconciliationDetail = new crReconciliationDetail();
            rptcls = (ReportClass)rptReconciliationDetail;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintDailyCollectionSummary
        public void PrintDailyCollectionSummary()
        {
            string strPaymentType = "";
            string strBankName = "";
            //---------------------------------------------------------------------------------------------------------------------
            strSQL = @" SELECT     RANK() OVER(PARTITION BY DAILY_COLLECTION.RECONCILIATION_DATE
                                               ORDER BY     DAILY_COLLECTION.RECONCILIATION_DATE,DAILY_COLLECTION.BANK_NAME,DAILY_COLLECTION.PAYMENT_TYPE_DESCRIPTION) AS SRL_NO,
                                   RANK() OVER(PARTITION BY DAILY_COLLECTION.RECONCILIATION_DATE,DAILY_COLLECTION.BANK_NAME
                                               ORDER BY     DAILY_COLLECTION.RECONCILIATION_DATE,DAILY_COLLECTION.BANK_NAME,DAILY_COLLECTION.PAYMENT_TYPE_DESCRIPTION) AS BANK_SRL_NO,
                                   DAILY_COLLECTION.COMPANY_NAME, 
		                           DAILY_COLLECTION.RECONCILIATION_DATE,
		                           DAILY_COLLECTION.RECONCILIATION_DATE_DDMMYYYY,
		                           DAILY_COLLECTION.BANK_NAME,
		                           DAILY_COLLECTION.PAYMENT_TYPE_DESCRIPTION,
		                           DAILY_COLLECTION.NET_AMT,
		                           SUM(DAILY_COLLECTION.NET_AMT) OVER(PARTITION BY DAILY_COLLECTION.RECONCILIATION_DATE,BANK_NAME) AS SUM_NET_AMOUNT_BANK_WISE,
		                           SUM(DAILY_COLLECTION.NET_AMT) OVER(PARTITION BY DAILY_COLLECTION.RECONCILIATION_DATE)           AS SUM_NET_AMOUNT_DATE_WISE
                        FROM       
                        (
	                        SELECT MST_COMPANY.COMPANY_NAME                                              AS COMPANY_NAME, 
		                           TRN_COLLECTION_HEADER.RECONCILIATION_DATE                             AS RECONCILIATION_DATE,
		                           CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103)       AS RECONCILIATION_DATE_DDMMYYYY,
		                           ISNULL(MST_BANK.BANK_NAME,'')                                         AS BANK_NAME,
		                           MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION                             AS PAYMENT_TYPE_DESCRIPTION,
		                           SUM(CASE WHEN TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = 5 
                                            THEN TRN_COLLECTION_HEADER.GROSS_AMT 
                                            ELSE TRN_COLLECTION_HEADER.NET_AMT 
                                       END)                                                              AS NET_AMT
	                        FROM   TRN_COLLECTION_HEADER
		                           INNER JOIN MST_PAYMENT_TYPE 
		                           ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID     
		                           LEFT  JOIN MST_BANK 
		                           ON TRN_COLLECTION_HEADER.BANK_ID         = MST_BANK.BANK_ID
		                           INNER JOIN MST_COMPANY
		                           ON TRN_COLLECTION_HEADER.COMPANY_ID      = MST_COMPANY.COMPANY_ID 
	                        WHERE  TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG    = 0
                            AND    TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS NOT NULL
                            AND    TRN_COLLECTION_HEADER.COMPANY_ID         = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                         @" AND    CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112) >=  " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) +
                         @" AND    CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112) <=  " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) +
                         @" AND    TRN_COLLECTION_HEADER.TALLIED                                    = 0 ";

                         
            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            strSQL = strSQL + @" GROUP BY MST_COMPANY.COMPANY_NAME,
			                              TRN_COLLECTION_HEADER.RECONCILIATION_DATE,
			                              CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),
			                              MST_BANK.BANK_NAME,
			                              MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION
                                 )AS DAILY_COLLECTION ";


            strSQL = strSQL + @" ORDER BY DAILY_COLLECTION.RECONCILIATION_DATE,
                                          DAILY_COLLECTION.BANK_NAME,
                                          DAILY_COLLECTION.PAYMENT_TYPE_DESCRIPTION";


            crDailyCollectionSummary rptDailyCollectionSummary = new crDailyCollectionSummary();
            rptcls = (ReportClass)rptDailyCollectionSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintUnreconciledCollectionList
        public void PrintUnreconciledCollectionList()
        {
            string strPaymentType = "";
            string strBankName = "";
            //---------------------------------------------------------------------------------------------------------------------

            strSQL = @" SELECT TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,
                               TRN_COLLECTION_HEADER.COMPANY_ID,
                               MST_COMPANY.COMPANY_NAME,
                               ROW_NUMBER() OVER (ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID) AS SL_NO,
                               CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103) AS COLLECTION_DATE_DDMMYYYY,
                               MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,
                               ISNULL(MST_BANK.BANK_NAME,'') AS BANK_NAME,
                               TRN_COLLECTION_HEADER.REFERENCE_NO,
                               TRN_COLLECTION_HEADER.NET_AMT,
                               ISNULL(INVREF.INVOICE_NO,'') AS INVOICE_NO,
                               ISNULL(INVREF.INVOICE_DT,'') AS INVOICE_DT,
                               ISNULL(INVREF.PARTY_NAME,'') AS PARTY_NAME
                        FROM   TRN_COLLECTION_HEADER
                        INNER JOIN MST_PAYMENT_TYPE ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                        INNER JOIN MST_COMPANY      ON TRN_COLLECTION_HEADER.COMPANY_ID      = MST_COMPANY.COMPANY_ID
                        LEFT  JOIN MST_BANK         ON TRN_COLLECTION_HEADER.BANK_ID         = MST_BANK.BANK_ID
                        LEFT  JOIN (SELECT TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID,
                                           TRN_INVOICE_HEADER.INVOICE_NO,
                                           CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DT,
                                           MST_PARTY.PARTY_NAME,
                                           TRN_INVOICE_HEADER.NET_AMOUNT,
                                           TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT
                                    FROM   TRN_COLLECTION_DETAIL,
                                           TRN_COLLECTION_HEADER,
                                           TRN_INVOICE_HEADER,
                                           MST_PARTY
                                    WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID              
                                    AND    TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID    = TRN_INVOICE_HEADER.INVOICE_HEADER_ID
                                    AND    TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID
                                    AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG      = 0
                                    AND    TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      <> " + BS_PaymentTypeId.Cc_Avenue + @") 
                                    AS INVREF 
                                    ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID     = INVREF.COLLECTION_HEADER_ID
                                    WHERE  TRN_COLLECTION_HEADER.COMPANY_ID           = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex);

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND TRN_COLLECTION_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            strSQL = strSQL  +    @" AND   TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG      = 0 
                                     AND   CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) <= " + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) +
                                  @" AND   TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS  NULL  
                                     AND   TRN_COLLECTION_HEADER.TALLIED              = 0 
                                     ORDER BY TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                              TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID";


            crUnReconciliedCollectionList rptUnReconciliedCollectionList = new crUnReconciliedCollectionList();
            rptcls = (ReportClass)rptUnReconciliedCollectionList;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "AsOn :" + mskAsOnDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);


            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintUnknownPaymentList
        public void PrintUnknownPaymentList()
        {
            strSQL = @" SELECT UNKNOWN_SET,
                               COMPANY_ID,
                               COMPANY_NAME,
                               RECONCILIATION_DATE_DDMMYYYY,
                               PAYMENT_TYPE_DESCRIPTION,
                               BANK_NAME,
                               REFERENCE_NO,
                               NET_AMT,
                               COLLECTION_REMARKS
                        FROM (                               
                        -----------------------------------------------
                        ------------ UNKNOWN COLLECTIONS 
                        -----------------------------------------------
                        SELECT 'Unknown Collections'  AS UNKNOWN_SET,
                               COLLECTION_HEADER_ID,
                               COMPANY_ID,
                               COMPANY_NAME,
                               RECONCILIATION_DATE,
                               RECONCILIATION_DATE_DDMMYYYY,
                               PAYMENT_TYPE_DESCRIPTION,
                               BANK_NAME,
                               REFERENCE_NO,
                               NET_AMT,
                               COLLECTION_REMARKS
                        FROM   VW_COLLECTION_HEADER
                        WHERE  COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                     @" AND    RECONCILIATION_DATE_YYYYMMDD <> ''
                        AND    RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) +
                     @" AND    RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) +
                     @" AND    NET_INVOICE_AMT = 0
                        AND    ADJUSTMENT_FLAG = 0
                        AND    VW_COLLECTION_HEADER.TALLIED  = 0 
                        UNION
                        -----------------------------------------------
                        ------------ MISMATCHED COLLECTIONS 
                        -----------------------------------------------
                        SELECT 'Mismatched Collections'  AS UNKNOWN_SET,
                               COLLECTION_HEADER_ID,
                               COMPANY_ID,
                               COMPANY_NAME,
                               RECONCILIATION_DATE,
                               RECONCILIATION_DATE_DDMMYYYY,
                               PAYMENT_TYPE_DESCRIPTION,
                               BANK_NAME,
                               REFERENCE_NO,
                               NET_AMT,
                               COLLECTION_REMARKS
                        FROM   VW_COLLECTION_HEADER
                        WHERE  COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                     @" AND    RECONCILIATION_DATE_YYYYMMDD <> ''
                        AND    RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) +
                     @" AND    RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) +
                     @" AND    GROSS_AMT                    <> NET_INVOICE_AMT 
                        AND    NET_INVOICE_AMT              > 0
                        AND    ADJUSTMENT_FLAG              = 0
                        AND    VW_COLLECTION_HEADER.TALLIED = 0
                        ) AS UNKN_MISSMATCH_DESC
                        ORDER BY UNKNOWN_SET,
                                 RECONCILIATION_DATE,
                                 COLLECTION_HEADER_ID ";

            crUnknownPaymentsList rptUnknownPaymentsList = new crUnknownPaymentsList();
            rptcls = (ReportClass)rptUnknownPaymentsList;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintAdjustmentRegister
        public void PrintAdjustmentRegister()
        {
            strSQL = @" SELECT VW_COLLECTION_DETAIL.COMPANY_ID,
                               VW_COLLECTION_DETAIL.COMPANY_NAME,
                               VW_COLLECTION_DETAIL.COLLECTION_HEADER_ID,
                               VW_COLLECTION_DETAIL.COLLECTION_DATE_DDMMYYYY,
                               VW_COLLECTION_DETAIL.PAYMENT_TYPE_ID,
                               VW_COLLECTION_DETAIL.PAYMENT_TYPE_DESCRIPTION,
                               VW_COLLECTION_DETAIL.REFERENCE_NO,
                               VW_COLLECTION_DETAIL.COLLECTION_REMARKS,
                               VW_COLLECTION_DETAIL.INVOICE_NO,
                               VW_COLLECTION_DETAIL.INVOICE_DATE_DDMMYYYY,
                               VW_COLLECTION_DETAIL.PARTY_ID,
                               VW_COLLECTION_DETAIL.PARTY_NAME,
                               VW_COLLECTION_DETAIL.COLLECTION_AMOUNT
                        FROM   VW_COLLECTION_DETAIL
                        WHERE  VW_COLLECTION_DETAIL.ADJUSTMENT_FLAG   =  1
                        AND    VW_COLLECTION_DETAIL.COMPANY_ID        = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex)  +
                     @" AND    CONVERT(CHAR(8),VW_COLLECTION_DETAIL.COLLECTION_DATE,112) >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate)  +
                     @" AND    CONVERT(CHAR(8),VW_COLLECTION_DETAIL.COLLECTION_DATE,112) <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate)  +
                     @" AND    VW_COLLECTION_DETAIL.TALLIED                               = 0  
                        ORDER BY   COLLECTION_DATE,
                                   COLLECTION_HEADER_ID ";

            crAdjustmentRegister rptAdjustmentRegister = new crAdjustmentRegister();
            rptcls = (ReportClass)rptAdjustmentRegister;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintReconciliationStatement
        public void PrintReconciliationStatement()
        {
            DateTime time = DateTime.Now;             // Use current time.
            string format = "dd_MM_yyyy_HH_mm_ss";   // Use this format.
            //Console.WriteLine(time.ToString(format)); // Write to console.

            string strTemptable_Name = "TEMP_RECON_STAT_" + time.ToString(format);
            try
            {
                if (dmlService.J_IsDatabaseObjectExist(strTemptable_Name) == true)
                {
                    strSQL = "DROP TABLE " + strTemptable_Name;
                    //--
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE TABLE " + strTemptable_Name
                                                       + @"(id               bigint       identity,
                                                            grp_id           bigint       not null default 0, 
                                                            grp_name         varchar(100) not null default '', 
                                                            grp_tag          varchar(100) not null default '', 
                                                            grp_desc         varchar(100) not null default '', 
                                                            grp_total        money        not null default 0,
                                                            calc_total       money        not null default 0,
                                                            inv_total        money        not null default 0,
                                                            pend_inv_total   money        not null default 0,
                                                            adj_total        money        not null default 0,
                                                            collection_total money        not null default 0,
                                                            current_total    money        not null default 0,
                                                            unknown_total    money        not null default 0,
                                                            advance_total    money        not null default 0,  
                                                            old_total        money        not null default 0,  
                                                            hide_flag        smallint     not null default 0
                                                            )";
                //--
                dmlService.J_ExecSql(strSQL);

                //---------------------------------------------
                strSQL = @" INSERT INTO " + strTemptable_Name + @"
                            SELECT 1                            AS GRP_ID,
                                   'OPENING'                    AS GRP_NAME,
                                   ''                           AS GRP_TAG,
                                   'Value of Previous Invoices' AS GRP_DESC,
                                   ISNULL(SUM(NET_AMOUNT),0)    AS GRP_TOTAL,
                                   ISNULL(SUM(NET_AMOUNT),0)    AS CALC_TOTAL,
                                   0                            AS INV_TOTAL,
                                   0                            AS PEND_INV_TOTAL,
                                   0                            AS ADJ_TOTAL,        
                                   0                            AS COLLECTION_TOTAL,
                                   0                            AS CURRENT_TOTAL,
                                   0                            AS UNKNOWN_TOTAL,
                                   0                            AS ADVANCE_TOTAL,
                                   0                            AS OLD_TOTAL,
                                   1                            AS HIDE_FLAG 
                            FROM  VW_INVOICE_HEADER 
                            WHERE (TRAN_TYPE = 'INV' OR TRAN_TYPE = 'OINV')
                            AND   COMPANY_ID             = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                            AND   PARTY_CATEGORY_ID      = 0
                            AND   INVOICE_DATE_YYYYMMDD  < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            -------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 2                                     AS GRP_ID,
                                   'OPENING'                             AS GRP_NAME,
                                   ''                                    AS GRP_TAG,
                                   'Collection against Opening Invoices' AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)      AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0) *-1  AS CALC_TOTAL,
                                   0                                     AS INV_TOTAL,
                                   0                                     AS PEND_INV_TOTAL,
                                   0                                     AS ADJ_TOTAL,     
                                   0                                     AS COLLECTION_TOTAL,
                                   0                                     AS CURRENT_TOTAL,
                                   0                                     AS UNKNOWN_TOTAL,
                                   0                                     AS ADVANCE_TOTAL,
                                   0                                     AS OLD_TOTAL,
                                   1                                     AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   ADJUSTMENT_FLAG              = 0
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0      
                            -------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 3                                     AS GRP_ID,
                                   'OPENING'                             AS GRP_NAME,
                                   ''                                    AS GRP_TAG,
                                   'Adjustment against Opening Invoices' AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)     AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0) *-1 AS CALC_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   1                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   ADJUSTMENT_FLAG              = 1
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            -------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 4                                    AS GRP_ID,
                                   'OPENING'                            AS GRP_NAME,
                                   'A'                                  AS GRP_TAG,
                                   'Pending Value of Previous Invoices' AS GRP_DESC,
                                   ISNULL(SUM(CALC_TOTAL),0)            AS GRP_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)            AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM   " + strTemptable_Name + @"     
                            WHERE  GRP_ID IN (1,2,3)
                            --------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 15                                                       AS GRP_ID,
                                   'OPENING'                                                AS GRP_NAME,
                                   'B'                                   AS GRP_TAG,
                                   'Less : Collection against Previous Invoices during the period' AS GRP_DESC,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0)                        AS GRP_TOTAL,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0) *-1                    AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)    AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <  " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   ADJUSTMENT_FLAG              = 0 
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 16                                                       AS GRP_ID,
                                   'OPENING'                                                AS GRP_NAME,
                                   'C'                                   AS GRP_TAG,
                                   'Less : Adjustment against Previous Invoices during the period' AS GRP_DESC,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0)                        AS GRP_TOTAL,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0) *-1                    AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                         AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <  " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   ADJUSTMENT_FLAG              = 1
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------
                            insert into " + strTemptable_Name + @"
                            SELECT 17                                       AS GRP_ID,
                                   'OPENING'                                AS GRP_NAME,
                                   'D = A-B-C'                              AS GRP_TAG,
                                   'Net Pending Value of Previous Invoices' AS GRP_DESC,
                                   ISNULL(SUM(CALC_TOTAL),0)                AS GRP_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)                AS CALC_TOTAL,
                                   0                                        AS INV_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)                AS PEND_INV_TOTAL,
                                   0                                        AS ADJ_TOTAL,     
                                   0                                        AS COLLECTION_TOTAL,
                                   0                                        AS CURRENT_TOTAL,
                                   0                                        AS UNKNOWN_TOTAL,
                                   0                                        AS ADVANCE_TOTAL,
                                   0                                        AS OLD_TOTAL,
                                   0                                        AS HIDE_FLAG  
                            FROM   " + strTemptable_Name + @"     
                            WHERE  GRP_ID IN (4,15,16)

                            ----------------------------------------------------------------------------
                            -- UNKNOWN COLLECTIONS BEFORE APRIL
                            ----------------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 30                                               AS GRP_ID,
                                   'OPENING'                                        AS GRP_NAME,
                                   'E'                                              AS GRP_TAG,
                                   'Unknown Collections received before the period' AS GRP_DESC,
                                   ISNULL(SUM(NET_AMT),0)                           AS GRP_TOTAL,
                                   0                                                AS CALC_TOTAL,
                                   0                                                AS INV_TOTAL,
                                   0                                                AS PEND_INV_TOTAL,
                                   0                                                AS ADJ_TOTAL,     
                                   0                                                AS COLLECTION_TOTAL,
                                   0                                                AS CURRENT_TOTAL,
                                   ISNULL(SUM(NET_AMT),0)                           AS UNKNOWN_TOTAL,
                                   0                                                AS ADVANCE_TOTAL,
                                   0                                                AS OLD_TOTAL,
                                   0                                                AS HIDE_FLAG  
                            FROM  VW_COLLECTION_HEADER
                            WHERE COMPANY_ID = 2
                            AND   RECONCILIATION_DATE IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD  < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   NET_INVOICE_AMT               = 0
                            AND   ADJUSTMENT_FLAG               = 0
                            AND   VW_COLLECTION_HEADER.TALLIED  = 0
                            --------------------------------------------------------
                            -- INVOICES MADE DURING APRIL
                            --------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 5                           AS GRP_ID,
                                   'CURRENT'                   AS GRP_NAME,
                                   'F'                         AS GRP_TAG,
                                   'Value of Current Invoices' AS GRP_DESC,
                                   ISNULL(SUM(NET_AMOUNT),0)   AS GRP_TOTAL,
                                   ISNULL(SUM(NET_AMOUNT),0)   AS CALC_TOTAL,
                                   ISNULL(SUM(NET_AMOUNT),0)   AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_INVOICE_HEADER 
                            WHERE (TRAN_TYPE = 'INV' OR TRAN_TYPE = 'OINV')
                            AND   COMPANY_ID             = 2
                            AND   PARTY_CATEGORY_ID      = 0
                            AND   INVOICE_DATE_YYYYMMDD  >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"       
                            AND   INVOICE_DATE_YYYYMMDD  <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            ---------------------------------------------------------------------
                            -- ADVANCE COLLECTIONS AGAINST INVOICES OF APRIL
                            ---------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 6                                                   AS GRP_ID,
                                   'CURRENT'                                           AS GRP_NAME,
                                   'G'                                                 AS GRP_TAG,
                                   'Less : Advance collected against Current Invoices' AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                    AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0) *-1                AS CALC_TOTAL,
                                   0                                                   AS INV_TOTAL,
                                   0                                                   AS PEND_INV_TOTAL,
                                   0                                                   AS ADJ_TOTAL,     
                                   0                                                   AS COLLECTION_TOTAL,
                                   0                                                   AS CURRENT_TOTAL,
                                   0                                                   AS UNKNOWN_TOTAL,
                                   0                                                   AS ADVANCE_TOTAL,
                                   0                                                   AS OLD_TOTAL,
                                   0                                                   AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 0
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------------------------
                            -- ADVANCE ADJUSTMENT AGAINST INVOICES OF APRIL
                            ---------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 7                                               AS GRP_ID,
                                   'CURRENT'                   AS GRP_NAME,
                                   'H'                                   AS GRP_TAG,
                                   'Less : Advance Adjustment against Current Invoices'   AS GRP_DESC,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0)               AS GRP_TOTAL,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0) *-1           AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD < " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 1
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------------------------
                            -- COLLECTIONS FOR APRIL AGAINST INVOICES OF APRIL
                            ---------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 12                                              AS GRP_ID,
                                   'CURRENT'                   AS GRP_NAME,
                                   'I'                                   AS GRP_TAG,
                                   'Less : Collection against Current Invoices during the period' AS GRP_DESC,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0)               AS GRP_TOTAL,
                                    ISNULL(SUM(COLLECTION_AMOUNT),0) *-1           AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   0                           AS ADJ_TOTAL,     
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                AS COLLECTION_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 0
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------------------------
                            -- ADJUSTMENTS FOR APRIL AGAINST INVOICES OF APRIL
                            ---------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 13                                             AS GRP_ID,
                                   'CURRENT'                   AS GRP_NAME,
                                   'J'                                   AS GRP_TAG,
                                   'Less : Adustment against Current Invoices during the period' AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)               AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0) *-1           AS CALC_TOTAL,
                                   0                           AS INV_TOTAL,
                                   0                           AS PEND_INV_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                           AS ADJ_TOTAL,     
                                   0                           AS COLLECTION_TOTAL,
                                   0                           AS CURRENT_TOTAL,
                                   0                           AS UNKNOWN_TOTAL,
                                   0                           AS ADVANCE_TOTAL,
                                   0                           AS OLD_TOTAL,
                                   0                           AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE          IS NOT NULL
                            AND   INVOICE_DATE                 IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 1
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ---------------------------------------------------------------------
                            -- PENDING VALUE OF INVOICES OF APRIL
                            ---------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 14                                   AS GRP_ID,
                                   'CURRENT'                            AS GRP_NAME,
                                   'K = F-G-H-I-J'                      AS GRP_TAG,
                                   'Pending Value of Current Invoices ' AS GRP_DESC,
                                   ISNULL(SUM(CALC_TOTAL),0)            AS GRP_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)            AS CALC_TOTAL,
                                   0                                    AS INV_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)            AS PEND_INV_TOTAL,
                                   0                                    AS ADJ_TOTAL,     
                                   0                                    AS COLLECTION_TOTAL,
                                   0                                    AS CURRENT_TOTAL,
                                   0                                    AS UNKNOWN_TOTAL,
                                   0                                    AS ADVANCE_TOTAL,
                                   0                                    AS OLD_TOTAL,
                                   0                                    AS HIDE_FLAG  
                            FROM   " + strTemptable_Name + @"     
                            WHERE  GRP_ID IN (5,6,7,12,13)
                            -------------------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 18                                        AS GRP_ID,
                                   'CURRENT'                                 AS GRP_NAME,
                                   'L = D+K'                                 AS GRP_TAG,
                                   'Net Pending Value of Invoices till date' AS GRP_DESC,
                                   ISNULL(SUM(CALC_TOTAL),0)                 AS GRP_TOTAL,
                                   ISNULL(SUM(CALC_TOTAL),0)                 AS CALC_TOTAL,
                                   0                                         AS INV_TOTAL,
                                   0                                         AS PEND_INV_TOTAL,
                                   0                                         AS ADJ_TOTAL,     
                                   0                                         AS COLLECTION_TOTAL,
                                   0                                         AS CURRENT_TOTAL,
                                   0                                         AS UNKNOWN_TOTAL,
                                   0                                         AS ADVANCE_TOTAL,
                                   0                                         AS OLD_TOTAL,
                                   0                                         AS HIDE_FLAG  
                            FROM   " + strTemptable_Name + @"     
                            WHERE  GRP_ID IN (14,17)
                            ----------------------------------------------------------------------------
                            -- UNKNOWN COLLECTIONS OF APRIL 
                            ----------------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 19                                               AS GRP_ID,
                                   'CURRENT'                                        AS GRP_NAME,
                                   'M'                                              AS GRP_TAG,
                                   'Unknown Collections received during the period' AS GRP_DESC,
                                   ISNULL(SUM(NET_AMT),0)                           AS GRP_TOTAL,
                                   ISNULL(SUM(NET_AMT),0)                           AS CALC_TOTAL,
                                   0                                                AS INV_TOTAL,
                                   0                                                AS PEND_INV_TOTAL,
                                   0                                                AS ADJ_TOTAL,     
                                   ISNULL(SUM(NET_AMT),0)                           AS COLLECTION_TOTAL,
                                   0                                                AS CURRENT_TOTAL,
                                   ISNULL(SUM(NET_AMT),0)                           AS UNKNOWN_TOTAL,
                                   0                                                AS ADVANCE_TOTAL,
                                   0                                                AS OLD_TOTAL,
                                   0                                                AS HIDE_FLAG  
                            FROM  VW_COLLECTION_HEADER
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   NET_INVOICE_AMT               = 0
                            AND   ADJUSTMENT_FLAG               = 0
                            AND   VW_COLLECTION_HEADER.TALLIED  = 0
                            ----------------------------------------------------------------------------
                            -- COLLECTIONS OF APRIL AGAINST INVOICES AFTER APRIL
                            ----------------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 20                                              AS GRP_ID,
                                   'CURRENT'                   AS GRP_NAME,
                                   'N'                                   AS GRP_TAG,
                                   'Advance Collection received during the period' AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                AS CALC_TOTAL,
                                   0                                               AS INV_TOTAL,
                                   0                                               AS PEND_INV_TOTAL,
                                   0                                               AS ADJ_TOTAL,     
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                AS COLLECTION_TOTAL,
                                   0                                               AS CURRENT_TOTAL,
                                   0                                               AS UNKNOWN_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)                AS ADVANCE_TOTAL,
                                   0                                               AS OLD_TOTAL,
                                   0                                               AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE IS NOT NULL
                            AND   INVOICE_DATE        IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >  " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 0
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0
                            ----------------------------------------------------------------------------
                            -- ADJUSTMENT OF APRIL AGAINST INVOICES AFTER APRIL
                            ----------------------------------------------------------------------------
                            INSERT INTO " + strTemptable_Name + @"
                            SELECT 21                                           AS GRP_ID,
                                   'CURRENT'                                    AS GRP_NAME,
                                   'O'                                          AS GRP_TAG,
                                   'Advance Adjustment during the period'       AS GRP_DESC,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)             AS GRP_TOTAL,
                                   ISNULL(SUM(COLLECTION_AMOUNT),0)             AS CALC_TOTAL,
                                   0                                            AS INV_TOTAL,
                                   0                                            AS PEND_INV_TOTAL,
                                   0                                            AS ADJ_TOTAL,     
                                   0                                            AS COLLECTION_TOTAL,
                                   0                                            AS CURRENT_TOTAL,
                                   0                                            AS UNKNOWN_TOTAL,
                                   0                                            AS ADVANCE_TOTAL,
                                   0                                            AS OLD_TOTAL,
                                   0                                            AS HIDE_FLAG  
                            FROM  VW_COLLECTION_DETAIL
                            WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                            AND   RECONCILIATION_DATE IS NOT NULL
                            AND   INVOICE_DATE        IS NOT NULL
                            AND   RECONCILIATION_DATE_YYYYMMDD >= " + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + @"
                            AND   RECONCILIATION_DATE_YYYYMMDD <= " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   INVOICE_DATE_YYYYMMDD        >  " + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + @"
                            AND   ADJUSTMENT_FLAG              = 1 
                            AND   VW_COLLECTION_DETAIL.TALLIED = 0";
                //---------------------------------------------
                dmlService.J_ExecSql(strSQL);
                //---------------------------------------------

                strSQL = @" SELECT GRP_NAME,
                                   GRP_TAG,
                                   GRP_DESC,
                                   GRP_TOTAL,
                                   INV_TOTAL,
                                   PEND_INV_TOTAL,
                                   ADJ_TOTAL,
                                   COLLECTION_TOTAL,
                                   CURRENT_TOTAL,
                                   UNKNOWN_TOTAL,
                                   OLD_TOTAL,
                                   ADVANCE_TOTAL
                            FROM " + strTemptable_Name +
                         @" WHERE HIDE_FLAG = 0 
                            ORDER BY ID ";

                //-------------------------------------------------------------------
                //-------------------------------------------------------------------
                strQueryString = @" SELECT 1         AS GRP_ID,
                                           'Opening' AS PARTICULARS,
                                           GRP_TOTAL AS AMOUNT,
                                           GRP_TOTAL AS CALCAMOUNT 
                                    FROM " + strTemptable_Name + @" 
                                    WHERE GRP_TAG = 'A'
                                    UNION
                                    ---------------------------------------------
                                    SELECT 2                   AS GRP_ID,
                                           'Add : New Invoice' AS PARTICULARS,
                                           GRP_TOTAL           AS AMOUNT,
                                           GRP_TOTAL           AS CALCAMOUNT
                                    FROM " + strTemptable_Name + @" 
                                    WHERE GRP_TAG = 'F'
                                    UNION
                                    ---------------------------------------------
                                    SELECT 3                    AS GRP_ID,
                                           'Less : Adjustments' AS PARTICULARS,
                                           SUM(ADJ_TOTAL)       AS AMOUNT,
                                           SUM(ADJ_TOTAL)*-1    AS CALCAMOUNT
                                    FROM " + strTemptable_Name + @"       
                                    UNION
                                    ---------------------------------------------
                                    SELECT 4                        AS GRP_ID,
                                           'Less : Collections'     AS PARTICULARS,
                                           SUM(COLLECTION_TOTAL)    AS AMOUNT,
                                           SUM(COLLECTION_TOTAL)*-1 AS CALCAMOUNT
                                    FROM " + strTemptable_Name + @"  ";

                //-----------------------------------------------------------------------
                crReconciliationStatement rptReconciliationStatement = new crReconciliationStatement();
                rptcls = (ReportClass)rptReconciliationStatement;
                //--
                //-------------------------------------------------
                TextObject objtxtValue;
                //-------------------------------------------------
                objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[2].ReportObjects["txtFromToDate"];
                objtxtValue.Text = "From " + mskFromDate.Text + " to " + mskToDate.Text;
                //--
                objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[2].ReportObjects["txtCompanyName"];
                objtxtValue.Text = cmbCombo1.Text;
                //--
                //------------------------------------------------- 
                // SUB REPORT
                rptcls.OpenSubreport("crSubReportReconSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strQueryString).Tables[0]);
                // MAIN REPORT
                rptService.J_PreviewReport(ref rptcls, this, strSQL);


                if (dmlService.J_IsDatabaseObjectExist(strTemptable_Name) == true)
                {
                    strSQL = "DROP TABLE " + strTemptable_Name;
                    //--
                    dmlService.J_ExecSql(strSQL);
                }

            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region PrintPeriodicCollectionSummary
        public void PrintPeriodicCollectionSummary()
        {
            string strCollectionType = "";

            strSQL = @" SELECT VW_COLLECTION_DETAIL.COMPANY_ID                              AS COMPANY_ID,
                               VW_COLLECTION_DETAIL.COMPANY_NAME                            AS COMPANY_NAME,
                               ROW_NUMBER() OVER (ORDER BY VW_COLLECTION_DETAIL.RECONCILIATION_DATE_YYYYMMDD,
                                                           VW_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                           VW_COLLECTION_DETAIL.INVOICE_NO) AS SL_NO,
                               VW_COLLECTION_DETAIL.RECONCILIATION_DATE_DDMMYYYY            AS RECONCILIATION_DATE_DDMMYYYY,
                               VW_COLLECTION_DETAIL.PAYMENT_TYPE_DESCRIPTION                AS PAYMENT_TYPE_DESCRIPTION,
                               VW_COLLECTION_DETAIL.BANK_NAME                               AS BANK_NAME,
                               VW_COLLECTION_DETAIL.REFERENCE_NO                            AS REFERENCE_NO,
                               CASE WHEN  VW_COLLECTION_DETAIL.INVOICE_NO = ''
                                    THEN  VW_COLLECTION_DETAIL.GROSS_AMT                       
                                    ELSE  VW_COLLECTION_DETAIL.COLLECTION_AMOUNT                       
                               END                                                          AS COLLECTION_AMOUNT,
                               VW_COLLECTION_DETAIL.INVOICE_NO                              AS INVOICE_NO,
                               VW_COLLECTION_DETAIL.INVOICE_DATE_DDMMYYYY                   AS INVOICE_DATE_DDMMYYYY,
                               VW_COLLECTION_DETAIL.PARTY_NAME                              AS PARTY_NAME,
                               CASE WHEN VW_COLLECTION_DETAIL.INVOICE_NO = '' 
                                    THEN VW_COLLECTION_DETAIL.GROSS_AMT         
                                    ELSE 0 
                               END                                                          AS UNKNOWN_COLLECTION, 
                               CASE WHEN VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  <  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator()   +  @"   THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END AS OLD_COLLECTION,
                               CASE WHEN (VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() +  @" 
                                     AND VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator()  +  @" ) THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END AS CURRENT_COLLECTION,
                               CASE WHEN VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  >  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"   THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END AS ADVANCE_COLLECTION
                        FROM   VW_COLLECTION_DETAIL
                        WHERE VW_COLLECTION_DETAIL.RECONCILIATION_DATE_YYYYMMDD      >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() +
                     @" AND   VW_COLLECTION_DETAIL.RECONCILIATION_DATE_YYYYMMDD      <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() +
                     @" AND   VW_COLLECTION_DETAIL.COMPANY_ID                         = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                     @" AND   VW_COLLECTION_DETAIL.ADJUSTMENT_FLAG                    = 0 " +
                     @" AND   VW_COLLECTION_DETAIL.TALLIED                            = 0 ";


            //UNKNOWN COLLECTION 
            if (rbnSort2.Checked == true)
            {
                strSQL = strSQL + @" AND CASE WHEN VW_COLLECTION_DETAIL.INVOICE_NO = '' THEN VW_COLLECTION_DETAIL.GROSS_AMT ELSE 0 END > 0 ";
                strCollectionType = rbnSort2.Text;   
            }
            //OLD COLLECTION 
            if (rbnSort3.Checked == true)
            {
                strSQL = strSQL + @" AND CASE WHEN VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  <  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"   THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END > 0";
                strCollectionType = rbnSort3.Text;   
            }
            //CURRENT COLLECTION
            if (rbnSort4.Checked == true)
            {
                strSQL = strSQL + @" AND CASE WHEN (VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
                                                    AND VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" ) THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END > 0 ";
                strCollectionType = rbnSort4.Text;   
            }
            //ADVANCE COLLECTION
            if (rbnSort5.Checked == true)
            {
                strSQL = strSQL + @" AND CASE WHEN VW_COLLECTION_DETAIL.INVOICE_DATE_YYYYMMDD  >  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"   THEN VW_COLLECTION_DETAIL.COLLECTION_AMOUNT ELSE 0 END > 0 ";
                strCollectionType = rbnSort5.Text;
            }

            strSQL = strSQL + @" ORDER BY VW_COLLECTION_DETAIL.RECONCILIATION_DATE_YYYYMMDD,
                                          VW_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                          VW_COLLECTION_DETAIL.INVOICE_NO  ";

            crPeriodicCollectionSummary rptPeriodicCollectionSummary = new crPeriodicCollectionSummary();
            rptcls = (ReportClass)rptPeriodicCollectionSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCollectionType", strCollectionType);
            

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintSundryPartyOutstandingSummary
        public void PrintSundryPartyOutstandingSummary()
        {
            string strPaymentType = "";
            string strBankName = "";
            string strPaymentTypeExcludingCcEvenue = "";

            //----------------------------------------------------
            strSQL = @" SELECT VW_INVOICE_HEADER.COMPANY_ID,
                               VW_INVOICE_HEADER.COMPANY_NAME,
                               ROW_NUMBER() OVER (ORDER BY VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD,
                                                           VW_INVOICE_HEADER.INVOICE_NO) AS SL_NO,
                               VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY,
                               VW_INVOICE_HEADER.INVOICE_NO,
                               VW_INVOICE_HEADER.PARTY_NAME,
                               VW_INVOICE_HEADER.PAYMENT_TYPE_DESCRIPTION,
                               VW_INVOICE_HEADER.BANK_NAME,
                               VW_INVOICE_HEADER.REFERENCE_NO,
                               VW_INVOICE_HEADER.NET_AMOUNT,
                               ISNULL(PAYMENT.COLLECTED_ADJUSTED,0) AS COLLECTED_ADJUSTED,
                               VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(PAYMENT.COLLECTED_ADJUSTED,0) AS DUE_AMT,
                               CASE WHEN VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(PAYMENT.COLLECTED_ADJUSTED,0) < 0 THEN '*' ELSE '' END AS DUE_MARK
                        FROM   VW_INVOICE_HEADER
                        LEFT JOIN (SELECT INVOICE_HEADER_ID,
                                          SUM(COLLECTION_ADJUSTED_AMOUNT) AS COLLECTED_ADJUSTED
                                   FROM   VW_INVOICE_DETAIL
                                   WHERE  (RECONCILIATION_DATE_YYYYMMDD <> '' AND RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @" )
                                   GROUP BY INVOICE_HEADER_ID) AS PAYMENT
                                   ON VW_INVOICE_HEADER.INVOICE_HEADER_ID = PAYMENT.INVOICE_HEADER_ID 
                        WHERE  VW_INVOICE_HEADER.PARTY_CATEGORY_ID                                    = 0
                        AND    VW_INVOICE_HEADER.COMPANY_ID                                           = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                        AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD                               <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"
                        AND    (VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(PAYMENT.COLLECTED_ADJUSTED,0)) <> 0";

            if (cmbCombo2.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND VW_INVOICE_HEADER.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
            }

            if (cmbCombo3.SelectedIndex > 0)
            {
                strSQL = strSQL + @" AND VW_INVOICE_HEADER.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
            }

            if (rbnSort1.Checked == true)
            {
                strSQL = strSQL + @" AND VW_INVOICE_HEADER.PAYMENT_TYPE_ID <> " + BS_PaymentTypeId.Cc_Avenue;
                strPaymentTypeExcludingCcEvenue = " Excluding CC AVENUE";
            }

            strSQL = strSQL + @" ORDER BY VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD,
                                 VW_INVOICE_HEADER.INVOICE_NO ";


            crSudryPartyOutstandingSummary rptSudryPartyOutstandingSummary = new crSudryPartyOutstandingSummary();
            rptcls = (ReportClass)rptSudryPartyOutstandingSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "Ason " + mskAsOnDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentTypeFilter", strPaymentTypeExcludingCcEvenue);
            

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintSundryPartyCollectionMiniStatement
        public void PrintSundryPartyCollectionMiniStatement()
        {
            strSQL = @" WITH SUNDRY_STATEMENT
                        AS
                        (       
		                        ---------------------------------------------------------------------------------------------------------
		                        SELECT 1                                                                                   AS ID,
			                           1                                                                                   AS GROUP_ID,
			                           0                                                                                   AS BANK_ID,
			                           'CASH'                                                                              AS BANK_NAME,
			                           ISNULL(SUM(CASE WHEN INVOICE_NO = '' THEN  GROSS_AMT ELSE COLLECTION_AMOUNT END),0) AS AMOUNT
		                        FROM   VW_COLLECTION_DETAIL
		                        WHERE  RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
		                        AND    RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" 
		                        AND    COMPANY_ID      = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1,cmbCombo1.SelectedIndex)  + @" 
		                        AND    ADJUSTMENT_FLAG = 0
		                        AND    PAYMENT_TYPE_ID = 4
                                AND    VW_COLLECTION_DETAIL.TALLIED = 0
		                        ---------------------------------------------------------------------------------------------------------
		                        UNION ALL   
		                        ---------------------------------------------------------------------------------------------------------
		                        SELECT (RANK() OVER(ORDER BY VW_COLLECTION_DETAIL.BANK_ID)) + 1                            AS ID,
			                           2                                                                                   AS GROUP_ID,
			                           VW_COLLECTION_DETAIL.BANK_ID                                                        AS BANK_ID,
			                           MST_BANK.BANK_NAME                                                                  AS BANK_NAME,
			                           ISNULL(SUM(CASE WHEN INVOICE_NO = '' THEN  GROSS_AMT ELSE COLLECTION_AMOUNT END),0) AS AMOUNT
		                        FROM   VW_COLLECTION_DETAIL
			                           INNER JOIN MST_BANK ON VW_COLLECTION_DETAIL.BANK_ID = MST_BANK.BANK_ID 
		                        WHERE  RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
		                        AND    RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" 
		                        AND    COMPANY_ID      = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
		                        AND    ADJUSTMENT_FLAG = 0
		                        AND    VW_COLLECTION_DETAIL.BANK_ID         > 0
		                        AND    VW_COLLECTION_DETAIL.TALLIED = 0
		                        GROUP BY VW_COLLECTION_DETAIL.BANK_ID,
				                         MST_BANK.BANK_NAME  
		                        ---------------------------------------------------------------------------------------------------------
		                        UNION ALL
		                        ---------------------------------------------------------------------------------------------------------
		                        SELECT 4                                                                                   AS ID,
			                           3                                                                                   AS GROUP_ID,
			                           0                                                                                   AS BANK_ID,
			                           'LESS : ADJUSTMENT'                                                                 AS BANK_NAME,
			                           ISNULL(SUM(CASE WHEN INVOICE_NO = '' THEN  GROSS_AMT ELSE COLLECTION_AMOUNT END),0) AS AMOUNT
		                        FROM   VW_COLLECTION_DETAIL
		                        WHERE  RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @" 
		                        AND    RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @" 
		                        AND    COMPANY_ID      = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
		                        AND    ADJUSTMENT_FLAG = 1
		                        AND    VW_COLLECTION_DETAIL.TALLIED = 0
		                        ---------------------------------------------------------------------------------------------------------
                        )
                        SELECT * 
                        FROM   SUNDRY_STATEMENT
                        ---------------------------------------------------------------------------------------------------------
                        UNION ALL
                        ---------------------------------------------------------------------------------------------------------
                        SELECT 5                                                                                   AS ID,
	                           4                                                                                   AS GROUP_ID,
	                           0                                                                                   AS BANK_ID,
	                           'NET SUNDRY PARTY COLLECTION'                                                       AS BANK_NAME,
	                           SUM(AMOUNT)                                                                         AS AMOUNT
                        FROM   SUNDRY_STATEMENT";

            crSundryPartyCollectionMiniStatement rptSundryPartyCollectionMiniStatement = new crSundryPartyCollectionMiniStatement();
            rptcls = (ReportClass)rptSundryPartyCollectionMiniStatement;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintOutstandingCumUnknown
        public void PrintOutstandingCumUnknown()
        {
            strQueryString = @" SELECT 1 AS GRP_ID,
                                       ISNULL(COUNT(SUMMARY.INVOICE_NO),0) AS NO_OF_INVOICES,
                                       ISNULL(SUM(SUMMARY.DUE_AMT),0)      AS DUE_AMT
                                FROM (         
                                SELECT VW_INVOICE_HEADER.COMPANY_ID,
                                       VW_INVOICE_HEADER.COMPANY_NAME,
                                       VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY,
                                       VW_INVOICE_HEADER.INVOICE_NO,
                                       VW_INVOICE_HEADER.PARTY_NAME,
                                       VW_INVOICE_HEADER.PAYMENT_TYPE_DESCRIPTION,
                                       VW_INVOICE_HEADER.BANK_NAME,
                                       VW_INVOICE_HEADER.REFERENCE_NO,
                                       VW_INVOICE_HEADER.NET_AMOUNT,
                                       ISNULL(PAYMENT.COLLECTED_ADJUSTED,0) AS COLLECTED_ADJUSTED,
                                       VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(PAYMENT.COLLECTED_ADJUSTED,0) AS DUE_AMT
                                FROM   VW_INVOICE_HEADER
                                LEFT JOIN (SELECT INVOICE_HEADER_ID,
                                                  SUM(COLLECTION_ADJUSTED_AMOUNT) AS COLLECTED_ADJUSTED
                                           FROM   VW_INVOICE_DETAIL
                                           WHERE  (RECONCILIATION_DATE_YYYYMMDD <> '' AND RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @" )
                                           GROUP BY INVOICE_HEADER_ID) AS PAYMENT
                                           ON VW_INVOICE_HEADER.INVOICE_HEADER_ID = PAYMENT.INVOICE_HEADER_ID 
                                WHERE  VW_INVOICE_HEADER.PARTY_CATEGORY_ID                                    = 0
                                AND    VW_INVOICE_HEADER.COMPANY_ID                                           = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"  
                                AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD                               <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"
                                AND    (VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(PAYMENT.COLLECTED_ADJUSTED,0)) <> 0
                                ) AS SUMMARY";


            strSQL = @" SELECT 2 AS GRP_ID,
                               DUE_AMT,
                               COUNT(DUE_AMT) AS NOS, 
                               DUE_AMT*COUNT(DUE_AMT) AS NET_VAL  
                        FROM   VW_COLLECTION_HEADER
                        WHERE  RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"
                        AND    ADJUSTMENT_FLAG = 0
                        AND    NET_INVOICE_AMT = 0
                        AND    VW_COLLECTION_HEADER.TALLIED  = 0
                        GROUP BY DUE_AMT
                        ORDER BY DUE_AMT";

            ////crOutstandingCumUnknown rptOutstandingCumUnknown = new crOutstandingCumUnknown();
            ////rptcls = (ReportClass)rptOutstandingCumUnknown;

            //// set text to report
            //rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            //rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", cmbCombo1.Text);

            //// execute the report as per above sql
            //rptService.J_PreviewReport(ref rptcls, this, strSQL);


            //-----------------------------------------------------------------------
            crOutstandingCumUnknown rptOutstandingCumUnknown = new crOutstandingCumUnknown();
            rptcls = (ReportClass)rptOutstandingCumUnknown;
            //--
            //-------------------------------------------------
            TextObject objtxtValue;
            //-------------------------------------------------
            objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[2].ReportObjects["txtFromToDate"];
            objtxtValue.Text = "Ason :" + mskAsOnDate.Text;
            //--
            objtxtValue = (TextObject)rptcls.ReportDefinition.Sections[2].ReportObjects["txtCompanyName"];
            objtxtValue.Text = cmbCombo1.Text;
            //--
            //------------------------------------------------- 
            // SUB REPORT
            rptcls.OpenSubreport("crTotalOutstanding").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strQueryString).Tables[0]);
            // MAIN REPORT
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion

        #region PrintAdvanceCollectionRegister
        public void PrintAdvanceCollectionRegister()
        {
            string strReportType = "";
            string strReportCaption = "";
            string strPaymentType = "";
            string strBankName = "";

            try
            {
                if (cmbCombo4.Text == "Old")
                    strReportType = "1";
                else if (cmbCombo4.Text == "Advance")
                    strReportType = "2";
                else if (cmbCombo4.Text == "Current")
                    strReportType = "3";

                strReportCaption = cmbCombo4.Text + " Collection Register ";
                
                //-------------------------------------------------------------

                strSQL = @" SELECT COMPANY_ID,
                                   COMPANY_NAME,
                                   ROW_NUMBER() OVER (ORDER BY RECONCILIATION_DATE,COLLECTION_HEADER_ID,INVOICE_NO) AS SL,
                                   RECONCILIATION_DATE_DDMMYYYY,
                                   PAYMENT_TYPE_DESCRIPTION,
                                   BANK_NAME,
                                   REFERENCE_NO,
                                   COLLECTION_AMOUNT,
                                   INVOICE_DATE_DDMMYYYY,
                                   INVOICE_NO,
                                   PARTY_NAME 
                            FROM  VW_COLLECTION_DETAIL
                            WHERE ADJUSTMENT_FLAG               = 0
                            AND   COLLECTION_AMOUNT             > 0
                            AND   COMPANY_ID                    = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) +
                         @" AND   RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() +
                         @" AND   RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() +
                         @" AND   ((" + strReportType + "  = 1 AND INVOICE_DATE_YYYYMMDD <  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + ") OR " +
                                @" (" + strReportType + "  = 2 AND INVOICE_DATE_YYYYMMDD >  " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator()   + ") OR " +
                                @" (" + strReportType + "  = 3 AND INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() +
                                                             " AND INVOICE_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator()   + ")) ";

                if (cmbCombo2.SelectedIndex > 0)
                {
                    strSQL = strSQL + @" AND VW_COLLECTION_DETAIL.PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo2, cmbCombo2.SelectedIndex);
                    strPaymentType = "Payment Type : " + Convert.ToString(cmbCombo2.Text.Trim());
                }

                if (cmbCombo3.SelectedIndex > 0)
                {
                    strSQL = strSQL + @" AND VW_COLLECTION_DETAIL.BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo3, cmbCombo3.SelectedIndex);
                    strBankName = "Bank Name : " + Convert.ToString(cmbCombo3.Text.Trim());
                }

                strSQL = strSQL + @" AND  VW_COLLECTION_DETAIL.TALLIED = 0
                                     ORDER BY RECONCILIATION_DATE,
                                              COLLECTION_HEADER_ID,
                                              INVOICE_NO";

                crAdvanceCollectionRegister rptAdvanceCollectionRegister = new crAdvanceCollectionRegister();
                rptcls = (ReportClass)rptAdvanceCollectionRegister;

                // set text to report
                rptService.J_SetTextToReport(ref rptcls, 2, "txtReportCaption", strReportCaption);
                rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
                rptService.J_SetTextToReport(ref rptcls, 2, "txtPaymentType", strPaymentType);
                rptService.J_SetTextToReport(ref rptcls, 2, "txtBankName", strBankName);
                
                // execute the report as per above sql
                rptService.J_PreviewReport(ref rptcls, this, strSQL);
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion 

        #region PrintPaymentTypeWiseOutstandingSummary
        public void PrintPaymentTypeWiseOutstandingSummary()
        {
            strSQL = @" SELECT RANK() OVER(ORDER BY SUMMARY.PAYMENT_TYPE_DESCRIPTION)AS SRL,
                               SUMMARY.PAYMENT_TYPE_DESCRIPTION        AS PAYMENT_TYPE_DESCRIPTION,
                               COUNT(SUMMARY.PAYMENT_TYPE_DESCRIPTION) AS NO_OF_INVOICE,
                               SUM(SUMMARY.NET_AMOUNT)                 AS INVOICE_AMOUNT,
                               SUM(SUMMARY.COLLECTED_ADJUSTED_AMOUNT)  AS COLLECTED_ADJUSTED_AMOUNT,
                               SUM(SUMMARY.DUE_AMOUNT)                 AS DUE_AMOUNT
                        FROM (
                                SELECT VW_INVOICE_HEADER.INVOICE_NO,
                                       VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY, 
                                       VW_INVOICE_HEADER.PAYMENT_TYPE_DESCRIPTION,
                                       VW_INVOICE_HEADER.NET_AMOUNT,
                                       ISNULL(SUM(COLLECTION.COLLECTED_ADJUSTED_AMOUNT),0) AS COLLECTED_ADJUSTED_AMOUNT,
                                       VW_INVOICE_HEADER.NET_AMOUNT - ISNULL(SUM(COLLECTION.COLLECTED_ADJUSTED_AMOUNT),0) AS DUE_AMOUNT
                                FROM   VW_INVOICE_HEADER
                                LEFT JOIN (SELECT INVOICE_HEADER_ID,
                                                  SUM(COLLECTION_AMOUNT) AS COLLECTED_ADJUSTED_AMOUNT
                                           FROM   VW_COLLECTION_DETAIL
                                           WHERE  RECONCILIATION_DATE_YYYYMMDD <> ''
                                           AND    RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"   
                                           AND    VW_COLLECTION_DETAIL.TALLIED  = 0
                                           GROUP  BY INVOICE_HEADER_ID
                                           ) AS COLLECTION 
                                           ON VW_INVOICE_HEADER.INVOICE_HEADER_ID = COLLECTION.INVOICE_HEADER_ID
                                WHERE VW_INVOICE_HEADER.COMPANY_ID                = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                AND   VW_INVOICE_HEADER.PARTY_CATEGORY_ID         = 0
                                AND   VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD    <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskAsOnDate) + cmnService.J_DateOperator() + @"
                                GROUP BY VW_INVOICE_HEADER.INVOICE_NO,
                                         VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY, 
                                         VW_INVOICE_HEADER.PAYMENT_TYPE_DESCRIPTION,
                                         VW_INVOICE_HEADER.NET_AMOUNT
                        ) AS SUMMARY
                        WHERE SUMMARY.DUE_AMOUNT > 0 
                        GROUP BY SUMMARY.PAYMENT_TYPE_DESCRIPTION
                        ORDER BY SUMMARY.PAYMENT_TYPE_DESCRIPTION";

            crPaymentTypeWiseOutstandingSummary rptPaymentTypeWiseOutstandingSummary = new crPaymentTypeWiseOutstandingSummary();
            rptcls = (ReportClass)rptPaymentTypeWiseOutstandingSummary;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "Ason :" + mskAsOnDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);

        }
        #endregion 

        #region PrintCategoryWiseVATCSTSale
        public void PrintCategoryWiseVATCSTSale()
        {
            strSQL = @" SELECT SUMMARY.COMPANY_NAME,
                               SUMMARY.PARTY_CATEGORY_DESCRIPTION,
                               -------------------------------------------------------------------
                               SUM(SUMMARY.VAT_BILL)                         AS VAT_BILL,
                               SUM(SUMMARY.VAT_GROSS)                        AS VAT_GROSS,
                               SUM(SUMMARY.VAT_AMOUNT)                       AS VAT_AMOUNT,
                               SUM(SUMMARY.VAT_ADDITIONAL)                   AS VAT_ADDITIONAL,
                               SUM(SUMMARY.VAT_ROUND)                        AS VAT_ROUND,
                               SUM(SUMMARY.VAT_SALE)                         AS VAT_SALE,
                               -------------------------------------------------------------------
                               SUM(SUMMARY.CST_BILL)                         AS CST_BILL,
                               SUM(SUMMARY.CST_GROSS)                        AS CST_GROSS,
                               SUM(SUMMARY.CST_AMOUNT)                       AS CST_AMOUNT,
                               SUM(SUMMARY.CST_ADDITIONAL)                   AS CST_ADDITIONAL,
                               SUM(SUMMARY.CST_ROUND)                        AS CST_ROUND,
                               SUM(SUMMARY.CST_SALE)                         AS CST_SALE,
                               -------------------------------------------------------------------
                               SUM(SUMMARY.VAT_SALE) + SUM(SUMMARY.CST_SALE) AS NET_SALE
                        FROM (
                                SELECT VW_INVOICE_HEADER.COMPANY_NAME,
                                       VW_INVOICE_HEADER.PARTY_CATEGORY_DESCRIPTION,
                                       VW_INVOICE_HEADER.INVOICE_NO,
                                       VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY,
                                       ---------------------------------------------------------------------------------------------------------------------
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN 1                                   ELSE 0 END AS VAT_BILL,
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN VW_INVOICE_HEADER.TAXABLE_AMOUNT    ELSE 0 END AS VAT_GROSS,
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN VW_INVOICE_HEADER.TAX_TOTAL_AMOUNT  ELSE 0 END AS VAT_AMOUNT,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN VW_INVOICE_HEADER.ADDITIONAL_COST   ELSE 0 END AS VAT_ADDITIONAL,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN VW_INVOICE_HEADER.ROUNDED_OFF       ELSE 0 END AS VAT_ROUND,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'V' THEN VW_INVOICE_HEADER.NET_AMOUNT        ELSE 0 END AS VAT_SALE,
                                       ---------------------------------------------------------------------------------------------------------------------
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN 1                                   ELSE 0 END AS CST_BILL,
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN VW_INVOICE_HEADER.TAXABLE_AMOUNT    ELSE 0 END AS CST_GROSS,
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN VW_INVOICE_HEADER.TAX_TOTAL_AMOUNT  ELSE 0 END AS CST_AMOUNT,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN VW_INVOICE_HEADER.ADDITIONAL_COST   ELSE 0 END AS CST_ADDITIONAL,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN VW_INVOICE_HEADER.ROUNDED_OFF       ELSE 0 END AS CST_ROUND,       
                                       CASE WHEN VW_INVOICE_HEADER.TAX_TYPE = 'C' THEN VW_INVOICE_HEADER.NET_AMOUNT        ELSE 0 END AS CST_SALE
                                FROM   VW_INVOICE_HEADER
                                WHERE  VW_INVOICE_HEADER.COMPANY_ID             = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @"
                                AND    VW_INVOICE_HEADER.TRAN_TYPE              = 'INV'
                                AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"
                        ) AS SUMMARY
                        GROUP BY SUMMARY.COMPANY_NAME,
                                 SUMMARY.PARTY_CATEGORY_DESCRIPTION ";

            
            crCategoryWiseVatCstSale rptCategoryWiseVatCstSale = new crCategoryWiseVatCstSale();
            rptcls = (ReportClass)rptCategoryWiseVatCstSale;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintTallyReconciliation
        public void PrintTallyReconciliation()
        {
            crTallyReconciliation rptInvoice = new crTallyReconciliation();
            rptcls = (ReportClass)rptInvoice;

            // Temp Table Name
            string strTableName = "TMP_TALLY_RECONCILIATION_" + J_Var.J_pUserId + "_" + string.Format("{0:ddMMyy}", System.DateTime.Now.Date) + "_" + string.Format("{0:HHmmss}", System.DateTime.Now);

            // Create the report temp table
            strSQL = "CREATE TABLE " + strTableName + "(" +
                     "             " + cmnService.J_GetDataType("ID",          J_ColumnType.Long)        + "," +
                     "             " + cmnService.J_GetDataType("DESCRIPTION", J_ColumnType.String, 100) + "," +
                     "             " + cmnService.J_GetDataType("DEBIT",       J_ColumnType.Double)      + "," +
                     "             " + cmnService.J_GetDataType("CREDIT",      J_ColumnType.Double)      + ")";
            if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false) return;



            strQueryString = @" INSERT INTO " + strTableName + @"
                                SELECT OPENING.ID,
                                       'Opening Balance' AS DESCRIPTION,
                                       CASE WHEN ISNULL(SUM(OPENING.DEBIT),0) >= ISNULL(SUM(OPENING.CREDIT),0) 
                                            THEN ISNULL(SUM(OPENING.DEBIT),0) - ISNULL(SUM(OPENING.CREDIT),0) 
                                            ELSE 0
                                       END               AS DEBIT,    
                                       CASE WHEN ISNULL(SUM(OPENING.CREDIT),0) >= ISNULL(SUM(OPENING.DEBIT),0) 
                                            THEN ISNULL(SUM(OPENING.CREDIT),0) - ISNULL(SUM(OPENING.DEBIT),0) 
                                            ELSE 0
                                       END               AS CREDIT
                                FROM (
                                      --------------------------------------------------
                                      ---- SUNDRY PARTY BILLING BEFORE THE PERIOD
                                      --------------------------------------------------
                                      SELECT 1                         AS ID,
                                             'BILLING BEFORE PERIOD'   AS DESCRIPTION,
                                             ISNULL(SUM(NET_AMOUNT),0) AS DEBIT,
                                             0                         AS CREDIT
                                      FROM  VW_INVOICE_HEADER 
                                      WHERE (TRAN_TYPE = 'INV' OR TRAN_TYPE = 'OINV')
                                      AND   COMPANY_ID            = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   PARTY_CATEGORY_ID     = 0
                                      AND   INVOICE_DATE_YYYYMMDD < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      UNION
                                      --------------------------------------------------
                                      ---- SUNDRY PARTY COLLECTION BEFORE THE PERIOD
                                      --------------------------------------------------
                                      SELECT 1                             AS ID,
                                            'COLLECTION BRFORE THE PERIOD' AS DESCRIPTION,
                                             0                             AS DEBIT,
                                             ISNULL(SUM(GROSS_AMT),0)      AS CREDIT
                                      FROM  VW_COLLECTION_HEADER
                                      WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   ADJUSTMENT_FLAG              = 0
                                      AND   RECONCILIATION_DATE          IS NOT NULL
                                      AND   RECONCILIATION_DATE_YYYYMMDD < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      AND   VW_COLLECTION_HEADER.TALLIED = 0
                                      UNION
                                      --------------------------------------------------
                                      ---- ADJUSTMENTS DURING THE PERIOD
                                      --------------------------------------------------
                                      SELECT 1                                                                             AS ID,
                                            'ADJUSTMENTS BEFORE THE PERIOD'                                                AS DESCRIPTION,
                                             SUM(CASE WHEN COLLECTION_AMOUNT  <  0 THEN ABS(COLLECTION_AMOUNT) ELSE 0 END) AS DEBIT,
                                             SUM(CASE WHEN COLLECTION_AMOUNT  >= 0 THEN ABS(COLLECTION_AMOUNT) ELSE 0 END) AS CREDIT 
                                      FROM  VW_COLLECTION_DETAIL
                                      WHERE COMPANY_ID                   = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   ADJUSTMENT_FLAG              = 1
                                      AND   RECONCILIATION_DATE          IS NOT NULL
                                      AND   RECONCILIATION_DATE_YYYYMMDD < " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      AND   VW_COLLECTION_DETAIL.TALLIED = 0
                                      ) AS OPENING
                                      GROUP BY OPENING.ID
                                      ---------------------------------------------------------
                                      -- CURRENT PERIOD
                                      ---------------------------------------------------------
                                      UNION
                                      --------------------------------------------------
                                      ---- SUNDRY PARTY BILLING DURING THE PERIOD
                                      --------------------------------------------------
                                      SELECT 2                          AS ID,
                                            'Billing during the period' AS DESCRIPTION,
                                            ISNULL(SUM(NET_AMOUNT),0)   AS DEBIT,
                                            0                           AS CREDIT
                                      FROM  VW_INVOICE_HEADER 
                                      WHERE TRAN_TYPE           = 'INV' 
                                      AND   COMPANY_ID          = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   PARTY_CATEGORY_ID   = 0
                                      AND   INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      AND   INVOICE_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"
                                      UNION
                                      --------------------------------------------------
                                      ---- SUNDRY PARTY COLLECTION DURING THE PERIOD
                                      --------------------------------------------------
                                      SELECT 3                             AS ID,
                                            'Collection during the period' AS DESCRIPTION,
                                             0                             AS DEBIT,
                                             ISNULL(SUM(GROSS_AMT),0)      AS CREDIT
                                      FROM  VW_COLLECTION_HEADER
                                      WHERE COMPANY_ID                    = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   ADJUSTMENT_FLAG               = 0
                                      AND   RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      AND   RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"
                                      AND   VW_COLLECTION_HEADER.TALLIED = 0
                                      UNION
                                      --------------------------------------------------
                                      ---- ADJUSTMENTS DURING THE PERIOD
                                      --------------------------------------------------
                                      SELECT 4                                                                                       AS ID,
                                            'Adjustments during the period'                                                          AS DESCRIPTION,
                                             ISNULL(SUM(CASE WHEN COLLECTION_AMOUNT  <  0 THEN ABS(COLLECTION_AMOUNT) ELSE 0 END),0) AS DEBIT,
                                             ISNULL(SUM(CASE WHEN COLLECTION_AMOUNT  >= 0 THEN ABS(COLLECTION_AMOUNT) ELSE 0 END),0) AS CREDIT 
                                      FROM  VW_COLLECTION_DETAIL
                                      WHERE COMPANY_ID                    = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                                      AND   ADJUSTMENT_FLAG               = 1
                                      AND   RECONCILIATION_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                                      AND   RECONCILIATION_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"
                                      AND   VW_COLLECTION_DETAIL.TALLIED = 0 ";
                 

            // insert data into the temp table 
            if (dmlService.J_ExecSql(strQueryString, J_SQLType.DML) == false) return;

            strQueryString = @"-----------------------------------------------------------------------
                               --- INSERTING CLOSING BALANCE
                               -----------------------------------------------------------------------
                               INSERT INTO " + strTableName + @"
                               SELECT 5                                                                                        AS ID,
                                     'Closing Balance'                                                                         AS DESCRIPTION,
                                      ISNULL(CASE WHEN SUM(CREDIT)-SUM(DEBIT) >= 0 THEN  SUM(CREDIT)-SUM(DEBIT)  ELSE 0 END,0) AS DEBIT, 
                                      ISNULL(CASE WHEN SUM(DEBIT)-SUM(CREDIT) >= 0 THEN  SUM(DEBIT) -SUM(CREDIT) ELSE 0 END,0) AS CREDIT
                               FROM " + strTableName + " ";

            // insert data into the temp table 
            if (dmlService.J_ExecSql(strQueryString, J_SQLType.DML) == false) return;


            strQueryString = @" SELECT ID,
                                       DESCRIPTION,
                                       DEBIT,
                                       CREDIT
                                FROM  " + strTableName + @" ORDER BY ID";

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            rptService.J_SetTextToReport(ref rptcls, 2, "txtCompanyName", cmbCombo1.Text);

            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strQueryString);


            // Drop the report temp table
            if (dmlService.J_ExecSql("DROP TABLE " + strTableName) == false) return;

        }
        #endregion

        #region PrintDespatchStatusReport
        public void PrintDespatchStatusReport()
        {
            strSQL = @" SELECT MST_COMPANY.COMPANY_NAME,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.REQUEST_DATE,103) AS REQUEST_DATE_DDMMYYYY,
                               TRN_INVOICE_HEADER.INVOICE_NO,
                               CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE,
                               MST_PARTY.PARTY_NAME,
                               ---------------------------------------------------------------
                               CASE WHEN SEND_CD      = 1 THEN 'CD' ELSE '' END + 
                               CASE WHEN SEND_SERIAL  = 1 THEN 
                                    CASE WHEN SEND_CD = 1 THEN ', Serial No.' ELSE 'Serial No.' END    
                                    ELSE '' 
                               END + 
                               CASE WHEN SEND_INVOICE = 1 THEN 
                                    CASE WHEN SEND_CD + SEND_SERIAL > 0 THEN ', Invoice' ELSE 'Invoice' END  
                                    ELSE '' 
                               END AS DESPATCH_DETAIL,
                               ---------------------------------------------------------------
                               ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.DESPATCH_DATE,103),'') AS DESPATCH_DATE_DDMMYYYY,
                               ISNULL(MST_COURIER.COURIER_DESC,'') AS COURIER_DESC,
                               TRN_INVOICE_HEADER.TRACKING_NO
                        FROM   TRN_INVOICE_HEADER 
                        INNER JOIN  MST_PARTY   ON TRN_INVOICE_HEADER.PARTY_ID   = MST_PARTY.PARTY_ID
                        INNER JOIN  MST_COMPANY ON TRN_INVOICE_HEADER.COMPANY_ID = MST_COMPANY.COMPANY_ID
                        LEFT  JOIN  MST_COURIER ON TRN_INVOICE_HEADER.COURIER_ID = MST_COURIER.COURIER_ID   
                        WHERE TRN_INVOICE_HEADER.TRAN_TYPE = 'INV'
                        AND   TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                        AND   TRN_INVOICE_HEADER.REQUEST_DATE IS NOT NULL
                        AND   CONVERT(CHAR(8),TRN_INVOICE_HEADER.REQUEST_DATE,112) >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                        AND   CONVERT(CHAR(8),TRN_INVOICE_HEADER.REQUEST_DATE,112) <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"";

            if(rbnSort1.Checked == true)
                strSQL = strSQL  + " AND TRN_INVOICE_HEADER.DESPATCH_DATE IS NULL ";

            strSQL = strSQL + @" ORDER BY TRN_INVOICE_HEADER.REQUEST_DATE,
                                          TRN_INVOICE_HEADER.INVOICE_NO ";


            crDespatchStatus rptDespatchStatus = new crDespatchStatus();
            rptcls = (ReportClass)rptDespatchStatus;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #region PrintEmailCheckList()
        public void PrintEmailCheckList()
        {
            strSQL = @" SELECT VW_INVOICE_HEADER.COMPANY_NAME,
                               VW_INVOICE_HEADER.INVOICE_DATE_DDMMYYYY,
                               CASE WHEN VW_INVOICE_HEADER.DELIVERY_MODE_ID = 1
                                    THEN VW_INVOICE_HEADER.REFERENCE_NO 
                                    ELSE ''
                               END AS REFERENCE_NO,   
                               VW_INVOICE_HEADER.EMAIL_ID,
                               VW_INVOICE_HEADER.CONTACT_PERSON,
                               VW_INVOICE_HEADER.PARTY_NAME,
                               VW_INVOICE_HEADER.INVOICE_NO
                        FROM   VW_INVOICE_HEADER 
                        WHERE  VW_INVOICE_HEADER.TRAN_TYPE              = 'INV'
                        AND    VW_INVOICE_HEADER.COMPANY_ID             = " + cmnService.J_GetComboBoxItemId(ref cmbCombo1, cmbCombo1.SelectedIndex) + @" 
                        AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD >= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskFromDate) + cmnService.J_DateOperator() + @"
                        AND    VW_INVOICE_HEADER.INVOICE_DATE_YYYYMMDD <= " + cmnService.J_DateOperator() + dtservice.J_ConvertToIntYYYYMMDD(mskToDate) + cmnService.J_DateOperator() + @"
                        AND    VW_INVOICE_HEADER.DELIVERY_MODE_ID <> 0
                        ORDER BY VW_INVOICE_HEADER.INVOICE_DATE DESC,
                                   VW_INVOICE_HEADER.EMAIL_ID ";

            crEmailCheckList rptEmailCheckList = new crEmailCheckList();
            rptcls = (ReportClass)rptEmailCheckList;

            // set text to report
            rptService.J_SetTextToReport(ref rptcls, 2, "txtFromToDate", "From " + mskFromDate.Text + " to " + mskToDate.Text);
            // execute the report as per above sql
            rptService.J_PreviewReport(ref rptcls, this, strSQL);
        }
        #endregion 

        #endregion

        #endregion
    }   
}
