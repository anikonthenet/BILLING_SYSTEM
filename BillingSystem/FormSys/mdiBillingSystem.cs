
#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

//~~~~ User Namespaces ~~~~
using BillingSystem.FormCmn;
using BillingSystem.FormPar;
using BillingSystem.FormMst.NormalEntries;
using BillingSystem.FormTrn.NormalEntries;
using BillingSystem.FormUtl;
using BillingSystem.FormRpt;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class mdiBillingSystem : Form
    {

        #region System Generated Code
        public mdiBillingSystem()
        {
            InitializeComponent();
        }
        #endregion

        #region Decleration Section

        DMLService dmlService = new DMLService();
        DMLService dmlIsConnected = new DMLService();
        CommonService cmnService = new CommonService();

        string strSQL = string.Empty;
        string strDatabaseDisplayTextInStatusBar = string.Empty;

        BS billingService = new BS();

        #endregion

        #region EVENTS

        #region GENERAL

        #region mdiBillingSystem_Load
        private void mdiBillingSystem_Load(object sender, EventArgs e)
        {
            this.Text = J_Var.J_pProjectName;
            //--

            //this.lblVerDate.Text = "Ver. Date  :  31st Mar 2015(β)";
            //this.lblVerDate.Text = "Ver. Date  :  1st Apr 2015(β)";
            //this.lblVerDate.Text = "Ver. Date  :  2nd Apr 2015";
            //this.lblVerDate.Text = "Ver. Date  :  20th Apr 2015";
            //this.lblVerDate.Text = "Ver. Date  :  27th Apr 2015";
            //this.lblVerDate.Text = "Ver. Date  :  04th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  07th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  15th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  15th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  16th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  18th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  23th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  30th May 2015";
            //this.lblVerDate.Text = "Ver. Date  :  30rd JUN 2015";
            //this.lblVerDate.Text = "Ver. Date  :  10th JUN 2015";
            //this.lblVerDate.Text = "Ver. Date  :  22th JUN 2015";
            //this.lblVerDate.Text = "Ver. Date  :  26th JUN 2015*";
            //this.lblVerDate.Text = "Ver. Date  :  30th JUN 2015";
            //this.lblVerDate.Text = "Ver. Date  :  9th JULY 2015";
            //this.lblVerDate.Text = "Ver. Date  :  10th JULY 2015";
            this.lblVerDate.Text = "Ver. Date  :  18th FEB 2016";

            //--
            if (billingService.T_SystemMaintenance() == false)
            {
                return;
            }

            DataSet dsUserInfo = dmlService.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
            if (dsUserInfo == null) return;
            
            // Server & Database Name
            J_Var.J_pServerName = cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("SERVERNAME")].ToString());
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
                    J_Var.J_pDatabaseName = cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
                    J_Var.J_pDatabaseName = cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network)
                    J_Var.J_pDatabaseName = cmnService.J_ConvertMsAccessDatabasePath(cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString()), J_Colon.YES);
            }
            else
                J_Var.J_pDatabaseName = cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());
            
            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, "");

            if (J_Var.J_pLoginScreen == J_LoginScreen.YES)
            {
                cmnService.J_PanelMessage(J_PanelIndex.e01_FAYear, J_Var.J_pFABegDate + " - " + J_Var.J_pFAEndDate);
                cmnService.J_PanelMessage(J_PanelIndex.e02_BranchName, J_Var.J_pBranchCode + "  " + J_Var.J_pBranchName);
            }
            else if (J_Var.J_pLoginScreen == J_LoginScreen.NO)
            {
                cmnService.J_PanelMessage(J_PanelIndex.e01_FAYear, "");
                cmnService.J_PanelMessage(J_PanelIndex.e02_BranchName, "");

                USep20.Visible = false;
                mnuSwitchUser.Visible = false;
                USep60.Visible = false;
                mnuMenuRights.Visible = false;
            }

            cmnService.J_PanelMessage(J_PanelIndex.e03_ServerName, "");

            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
                    cmnService.J_PanelMessage(J_PanelIndex.e04_Database, cmnService.J_Left(J_Var.J_pDatabaseName, 3) + "...\\" + J_Var.J_pMsAccessDatabaseName);
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
                    cmnService.J_PanelMessage(J_PanelIndex.e04_Database, cmnService.J_Left(J_Var.J_pDatabaseName, 3) + "...\\" + J_Var.J_pMsAccessDatabaseName);
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network)
                    cmnService.J_PanelMessage(J_PanelIndex.e04_Database, J_Var.J_pServerName + "\\\\" + cmnService.J_Left(J_Var.J_pDatabaseName, 3) + "...\\" + J_Var.J_pMsAccessDatabaseName);
            }
            else
            {
                cmnService.J_PanelMessage(J_PanelIndex.e03_ServerName, J_Var.J_pServerName);
                cmnService.J_PanelMessage(J_PanelIndex.e04_Database, J_Var.J_pDatabaseName);
            }
            
            cmnService.J_PanelMessage(J_PanelIndex.e05_UserDisplayName, J_Var.J_pUserDisplayName);
            
            // Dataset object is disposed
            dsUserInfo.Dispose();
            
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
                {
                    mnuChangeConfigInfo.Visible = false;
                }
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network)
                {
                    if (Environment.MachineName.ToUpper() != J_Var.J_pServerName.ToUpper())
                    {
                        mnuBackup.Visible = false;
                        UTSep10.Visible = false;
                    }
                }
                UTSep20.Visible = false;
                mnuBuildIndex.Visible = false;
            }
            
            //this.MenuVisibility(true);

        }
        #endregion

        #region mdiBillingSystem_FormClosing
        private void mdiBillingSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            dmlService.Dispose();
            cmnService.Dispose();
        }
        #endregion

        #region stbMessage_MouseMove
        private void stbMessage_MouseMove(object sender, MouseEventArgs e)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
                {
                    ToolTipCustom.SetToolTip(stbMessage, J_Var.J_pDatabaseName + "\\" + J_Var.J_pMsAccessDatabaseName);
                }
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
                {
                    ToolTipCustom.SetToolTip(stbMessage, J_Var.J_pDatabaseName + "\\" + J_Var.J_pMsAccessDatabaseName);
                }
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network)
                {
                    if (Environment.MachineName.Trim().ToUpper() == J_Var.J_pServerName.Trim().ToUpper())
                        ToolTipCustom.SetToolTip(stbMessage, J_Var.J_pServerName + "\\\\" + J_Var.J_pDatabaseName + "\\" + J_Var.J_pMsAccessDatabaseName);
                }
            }
        }
        #endregion

        #region tmrWait_Tick
        private void tmrWait_Tick(object sender, EventArgs e)
        {
            if (stbMessage.Items[(int)J_PanelIndex.e00_DisplayText].Text != "")
            {
                tmrWait.Interval = 10000;
                cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, "");
            }
            cmnService.J_PanelMessage(J_PanelIndex.e07_DateTime, dmlService.J_ReturnServerDate() + " " + dmlService.J_ReturnServerTime());

            if(dmlService.J_IsDatabaseObjectExist("MST_SETUP") == true) 
                cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, " Connected",Color.Honeydew);
            else
                cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, " Disconnected ......", Color.Red);

        }
        #endregion


        #endregion

        #region MASTERS

        #region PARAMETRIC

        #region mnuFAYear_Click
        private void mnuFAYear_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuFAYear.Name) == false) return;
            cmnService.J_ShowChildForm(new MstFAYear(), this, "FA Year Master");
        }
        #endregion
        
        #endregion

        #region NORMAL ENTRIES

        #region mnuMUser_Click
        private void mnuMUser_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMUser.Name) == false) return;
            cmnService.J_ShowChildForm(new MstUser(), this, "User Master");
        }
        #endregion

        #region mnuMCompany_Click
        private void mnuMCompany_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMCompany.Name) == false) return;
            cmnService.J_ShowChildForm(new MstCompany(), this, "Company Master");
        }
        #endregion

        #region mnuMParty_Click
        private void mnuMParty_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMParty.Name) == false) return;
            cmnService.J_ShowChildForm(new MstParty(), this, "Party Master");
        }
        #endregion

        #region mnuMItem_Click
        private void mnuMItem_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMItem.Name) == false) return;
            cmnService.J_ShowChildForm(new MstItem(), this, "Item Master");
        }
        #endregion

        #region mnuMInvoiceSeries_Click
        private void mnuMInvoiceSeries_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMInvoiceSeries.Name) == false) return;
            cmnService.J_ShowChildForm(new MstInvoiceSeries(), this, "Invoice Series Master");
        }
        #endregion

        #region mnuMTax_Click
        private void mnuMTax_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMTax.Name) == false) return;
            cmnService.J_ShowChildForm(new MstTax(), this, "Tax Master");
        }
        #endregion

        #region mnuEmailCategory_Click
        private void mnuEmailCategory_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new MstEmailCategory(), this, "Email Category");
        }
        #endregion




        #endregion


        #endregion

        #region TRANSACTIONS

        #region NORMAL ENTRIES

        #region mnuTInvoiceEntry_Click
        private void mnuTInvoiceEntry_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuMUser.Name) == false) return;
            cmnService.J_ShowChildForm(new TrnInvoiceEntry(), this, "Invoice Entry (Physical Delivery)");
        }
        #endregion

        #region mnuBankEntry_Click
        private void mnuBankEntry_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnBankEntry(), this, "Bank Reconciliation");
        }
        #endregion

        #region mnuUnknownPaymentEntry_Click
        private void mnuUnknownPaymentEntry_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnUnknownPaymentEntry(), this, "Unknown Payment Entry");
        }
        #endregion

        #region mnuInvoiceConfirmationViaEmail_Click
        private void mnuInvoiceConfirmationViaEmail_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnSendEmailAgainstInvoice(), this, "Send Email Confirmation Via Email");
        }
        #endregion

        #region mnuOnlineOfflineCodeDeliveryBilling_Click
        private void mnuOnlineOfflineCodeDeliveryBilling_Click(object sender, EventArgs e)
        {
            strSQL = "SELECT COUNT(*) FROM MST_ITEM WHERE ONLINE_FLAG = 1 ";
            if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) == 0)
            {
                cmnService.J_UserMessage("No Item tagged for billing");
                return;
            }
            //--
            strSQL = "SELECT COUNT(*) FROM MST_ITEM WHERE DEFAULT_ITEM_ONLINE_OFFLINE_BILLING = 1 ";
            if (dmlService.J_ReturnNoOfRows(strSQL, J_QueryType.DirectQuery) == 0)
            {
                cmnService.J_UserMessage("No default Item tagged for billing");
                return;
            }
            //--
            cmnService.J_ShowChildForm(new TrnOnlineOffflineInvoiceEntry(), this, "Online/Offline Code Delivery Billing");
        }
        #endregion

        #region mnuSerialNoStatus_Click
        private void mnuSerialNoStatus_Click(object sender, EventArgs e)
        {
            //--
            cmnService.J_ShowChildForm(new TrnSerialNoStatus(), this, "Serial No. Status");
            //--
        }
        #endregion

        #region mnuRequestCDInvoiceSerialNo_Click
        private void mnuRequestCDInvoiceSerialNo_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnRequestCD(), this, "Request CD/Invoice/Serial No.");
        }
        #endregion

        #region mnuDespatchCDInvoiceSerialNo_Click
        private void mnuDespatchCDInvoiceSerialNo_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnDepstchCD(), this, "Dispatch CD/Invoice/Serial No.");
        }
        #endregion

        #region mnuCollection_Click
        private void mnuCollection_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnCollection(), this, "Collection");
        }
        #endregion

        #region mnuAdjustment_Click
        private void mnuAdjustment_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnAdjustment(), this, "Adjustment");
        }
        #endregion

        #region mnuOpeningInvoiceEntry_Click
        private void mnuOpeningInvoiceEntry_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new TrnOpeningInvoiceEntry(), this, "Opening Invoice Entry");
        }
        #endregion


        #endregion



        #endregion

        #region UTILITIES

        #region mnuCalculator_Click
        private void mnuCalculator_Click(object sender, EventArgs e)
        {
            //if (File.Exists(Environment.SystemDirectory + "/calc.exe") == true)
            //    Process.Start(Environment.SystemDirectory + "/calc.exe");

            if (File.Exists(Application.StartupPath + "/calc.exe") == true)
                Process.Start(Application.StartupPath + "/calc.exe");
        }
        #endregion

        #region mnuChangePassword_Click
        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new CmnChangePassword(), this, "Change Password");
        }
        #endregion

        #region mnuSwitchUser_Click
        private void mnuSwitchUser_Click(object sender, EventArgs e)
        {
            J_Var.J_pLoginStatus = 1;

            CmnLogin frm = new CmnLogin();
            frm.ShowDialog();
            frm.Dispose();
        }
        #endregion

        #region mnuChangeConfigInfo_Click
        private void mnuChangeConfigInfo_Click(object sender, EventArgs e)
        {
            J_Var.J_pLoginStatus = 1;

            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
                {
                    SysServerInfoLocal frm = new SysServerInfoLocal();
                    frm.ShowDialog();
                    frm.Dispose();
                }
                else if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
                {
                    SysServerInfoLocalBrowser frm = new SysServerInfoLocalBrowser();
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            else
            {
                SysServerInfoNetwork frm = new SysServerInfoNetwork();
                frm.ShowDialog();
                frm.Dispose();
            }
        }
        #endregion

        #region mnuBackup_Click
        private void mnuBackup_Click(object sender, EventArgs e)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (File.Exists(Application.StartupPath + "/eBackup.exe") == true)
                {
                    dmlService.Dispose();
                    this.Close();
                    this.Dispose();

                    Process.Start(Application.StartupPath + "/eBackup.exe");
                }
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                SysBackup frm = new SysBackup();
                frm.ShowDialog();
                frm.Dispose();
            }
            //cmnService.J_ShowChildForm(new SysBackup(), this, "Backup");
        }
        #endregion
        
        #region mnuSystemMaintainence_Click
        private void mnuSystemMaintainence_Click(object sender, EventArgs e)
        {
            SysSystemMaintainence frm = new SysSystemMaintainence();
            frm.ShowDialog();
            frm.Dispose();

            //cmnService.J_ShowChildForm(new SysSystemMaintainence(), this, "System Maintainence");
        }
        #endregion

        #region mnuMenuMaintenance_Click
        private void mnuMenuMaintenance_Click(object sender, EventArgs e)
        {
            SysMenuMaintainence frm = new SysMenuMaintainence();
            frm.ShowDialog();
            frm.Dispose();

            //cmnService.J_ShowChildForm(new SysMenuMaintainence(), this, "Menu Maintainence");
        }
        #endregion

        #region mnuBuildIndex_Click
        private void mnuBuildIndex_Click(object sender, EventArgs e)
        {
            SysBuildIndex frm = new SysBuildIndex();
            frm.ShowDialog();
            frm.Dispose();

            //cmnService.J_ShowChildForm(new SysBuildIndex(), this, "Build Index");
        }
        #endregion

        #region mnuSettings_Click
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildForm(new MstSetup(), this, "Settings information");
        }
        #endregion

        #region mnuMenuRights_Click
        private void mnuMenuRights_Click(object sender, EventArgs e)
        {
            //cmnService.J_ShowChildForm(new SysUserRights(), this, "User Rights");
            cmnService.J_ShowChildForm(new SysUserRightsGrigView(), this, "User Rights");
        }
        #endregion

        #endregion

        #region REPORTS

        #region mnuRInvoice_Click
        private void mnuRInvoice_Click(object sender, EventArgs e)
        {
            //if (this.CheckRights(mnuRInvoice.Name) == false) return;
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.Invoice, "INVOICE");
        }
        #endregion

        #region mnuInvoiceRegister_Click
        private void mnuInvoiceRegister_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.InvoiceRegister, "Invoice Register");
        }
        #endregion

        #region mnuItemWiseInvoiceSummary_Click
        //Added by Shrey Kejriwal on 25/08/2011
        private void mnuItemWiseInvoiceSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.ItemWiseInvoiceSummary, "Item wise Invoice Summary");
        }
        #endregion

        #region mnuTaxRegister_Click
        private void mnuTaxRegister_Click(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 04/08/2012
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.TaxRegister, "Tax Register");
        }
        #endregion

        #region mnuPartyCategoryWiseRegister_Click
        private void mnuPartyCategoryWiseRegister_Click(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 04/08/2012
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PartyCategoryWiseRegister, "Party Category wise Register");
        }
        #endregion

        #region mnuAccountsEntryDateWiseRegister_Click
        // Added by Ripan Paul on 07-05-2013
        private void mnuAccountsEntryDateWiseRegister_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.AccountsEntryDateWiseRegister, "Day wise detail - A/c entry date");
        }
        #endregion

        #region mnuListOfUnknownDeposits_Click
        // Added by Ripan Paul on 08-05-2013
        private void mnuListOfUnknownDeposits_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.ListOfUnknownDeposits, "List of Unknown Deposits");
        }
        #endregion

        #region mnuListOfUnknownDeposits_Click
        // Added by Ripan Paul on 08-05-2013
        private void mnuBillWiseOutstanding_Click(object sender, EventArgs e)
        {

            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.BillWiseOutstanding, "Bill wise Outstanding");
        }
        #endregion

        #region mnuPendingCCAvenueTransactions_Click
        // Added by Ripan Paul on 09-05-2013
        private void mnuPendingCCAvenueTransactions_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PendingCCAvenueTransactions, "Pending CC Avenue Transactions");
        }
        #endregion

        #region mnuBankStDateWiseRegister_Click
        // Added by Ripan Paul on 09-05-2013
        private void mnuBankStDateWiseRegister_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.BankStDateWiseRegister, "Day wise detail - Bank st. date");
        }
        #endregion

        #region mnuDetailsCollectionType_Click
        // Added by Ripan Paul on 09-05-2013
        private void mnuDetailsCollectionType_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.DetailsCollectionType, "Details of Collection Type");
        }
        #endregion

        #region mnuRSalesDistribution_Click
        private void mnuRSalesDistribution_Click(object sender, EventArgs e)
        {
            //Added by Shrey Kejriwal on 27/01/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SalesDistribution, "Sales Distribution");
        }
        #endregion

        #region mnuPartyCategoryWiseMonthlySaleDetails_Click
        private void mnuPartyCategoryWiseMonthlySaleDetails_Click(object sender, EventArgs e)
        {
            //Added by Dhrub Mukherjee  on 28/04/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PartyCategoryWiseMonthlySalesSummary, "Monthly Sale Summary");
        }
        #endregion 

        #region mnuPartyCategoryWiseMonthlySale_Click
        private void mnuPartyCategoryWiseMonthlySale_Click(object sender, EventArgs e)
        {
            //Added by Dhrub Mukherjee  on 29/04/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PartyCategoryWiseMonthlySale , "Monthly Sale Details");

        }
        #endregion 

        #region mnuAccountReconciliation_Click
        private void mnuAccountReconciliation_Click(object sender, EventArgs e)
        {
            //Added by Dhrub Mukherjee  on 29/04/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.AccountReconciliation, "Account Reconciliation");

        }
        #endregion 

        #region mnuSundryPartySale_Click
        private void mnuSundryPartySale_Click(object sender, EventArgs e)
        {
            //Added by Dhrub Mukherjee  on 29/04/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SundryPartySale, "Sundry Party Sale");
        }
        #endregion 

        #region mnuItemWiseSaleDetails_Click
        private void mnuItemWiseSaleDetails_Click(object sender, EventArgs e)
        {
            //Added by Dhrub Mukherjee  on 17/05/2014
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.ItemwiseSaleDetails, "Item Wise Sale Details");
        }
        #endregion 

        #region mnuSundryPartySalesCumOutstanding_Click
        private void mnuSundryPartySalesCumOutstanding_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SundryPartySalesCumOutstanding, "Sundry Party Sales Cum Outstanding Statement");
        }
        #endregion 

        #region mnuSundryPartyReconcilation_Click
        private void mnuSundryPartyReconcilation_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SundryPartyReconcilation, "Sundry Party Reconcilation");
        }
        #endregion

        #region mnuPartyListSales_Click
        private void mnuPartyListSales_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PartyListSales, "Party List (Sales)");
        }
        #endregion

        #region mnuOutstandingPayments_Click
        private void mnuOutstandingPayments_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.OutstandingPayments, "Outstanding Payments");
        }
        #endregion

        #region mnuUnknownPayments_Click
        private void mnuUnknownPayments_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.UnknownCollectionEntry, "Unknown Payments");
        }
        #endregion

        #region mnuMappedCollectionEntries_Click
        private void mnuMappedCollectionEntries_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.KnownCollectionEntry, "Tagged Collection Entries");
        }
        #endregion

        #region mnuInvoicePaymentStatus_Click
        private void mnuInvoicePaymentStatus_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.InvoicePaymentStatus, "Invoice Payment Status");
        }
        #endregion

        #region mnuInvoiceStatus_Click
        private void mnuInvoiceStatus_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.InvoiceStatusSummary, "Invoice Status Summary");
        }
        #endregion

        #region mnuInvoiceStatusDetails_Click
        private void mnuInvoiceStatusDetails_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.InvoiceStatusDetail, "Invoice Status Detail");
        }
        #endregion

        #region mnuPaymentStatusDetails_Click
        private void mnuPaymentStatusDetails_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PaymentStatusDetail, "Payment Status Detail");
        }
        #endregion

        #region mnuPaymentStatusSummary_Click
        private void mnuPaymentStatusSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PaymentStatusSummary, "Payment Status Summary");
        }
        #endregion

        #region mnuReconciliationDetail_Click
        private void mnuReconciliationDetail_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.ReconciliationDetail, "Reconciliation Detail");
        }
        #endregion

        #region mnuDailyCollectionSummary_Click
        private void mnuDailyCollectionSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.DailyCollectionSummary, "Daily Collection Summary");
        }
        #endregion

        #region mnuUnreconciledCollectionList_Click
        private void mnuUnreconciledCollectionList_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.UnreconciledCollectionList, "Unreconciled Collection List");
        }
        #endregion

        #region mnuUnknownPaymentList_Click
        private void mnuUnknownPaymentList_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.UnknownPaymentList, "Unknown Collections List");
        }
        #endregion

        #region mnuAdjustmentRegister_Click
        private void mnuAdjustmentRegister_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.AdjustmentRegister, "Adjustment Register");
        }
        #endregion

        #region mnuReconciliationStatement_Click
        private void mnuReconciliationStatement_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.ReconciliationStatement, "Reconciliation Statement");
        }
        #endregion

        #region mnuPeriodicCollectionSummary_Click
        private void mnuPeriodicCollectionSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PeriodicCollectionSummary, "Periodic Collection Summary");
        }
        #endregion

        #region mnuSundryPartyOutstandingSummary_Click
        private void mnuSundryPartyOutstandingSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SundryPartyOutstandingSummary, "Sundry Party Outstanding Summary");
        }
        #endregion

        #region mnuSundryPartyCollectionMiniStatement_Click
        private void mnuSundryPartyCollectionMiniStatement_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.SundryPartyCollectionMiniStatement, "Sundry Party Collection Mini Statement");
        }
        #endregion

        #region mnuOutstandingCumUnknown_Click
        private void mnuOutstandingCumUnknown_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.OutstandingCumUnknown, "Outstanding Cum Unknown");
        }
        #endregion

        #region mnuAdvanceCollectionRegister_Click
        private void mnuAdvanceCollectionRegister_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.AdvanceCollectionRegister, "Advance/Old/Current Collection Register");
        }
        #endregion


        #region mnuPaymentTypeWiseOutstandingSummary_Click
        private void mnuPaymentTypeWiseOutstandingSummary_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.PaymentTypeWiseOutstandingSummary, "Payment Type Wise Outstanding Summary");
        }
        #endregion

        #region mnuCategoryWiseVATCSTSale_Click
        private void mnuCategoryWiseVATCSTSale_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.CategoryWiseVATCSTSale, "Category Wise VAT CST Sale");
        }
        #endregion

        #region mnuTallyReconciliation_Click
        private void mnuTallyReconciliation_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.TallyReconciliation, "Tally Reconciliation");
        }
        #endregion

        #region mnuDespatchStatus_Click
        private void mnuDespatchStatus_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.DespatchStatusReport, "Tally Reconciliation");
        }
        #endregion

        #region mnuEmailCheckList_Click
        private void mnuEmailCheckList_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.EmailCheckList, "Email Check List");
        }
        #endregion
        
        #endregion

        #region OTHERS

        #region mnuHelpAbout_Click
        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            UtlAbout frm = new UtlAbout();
            frm.ShowDialog();
        }
        #endregion

        #region mnuUpdateNow_Click
        private void mnuUpdateNow_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/eUpdateApplication.exe") == true)
            {
                this.Close();
                this.Dispose();
                Process.Start(Application.StartupPath + "/eUpdateApplication.exe");
            }
        }
        #endregion

        #region mnuCloseAll_Click
        private void mnuCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
                childForm.Close();
        }
        #endregion

        #region mnuExit_Click
        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (cmnService.J_UserMessage("Are you sure to exit from application ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dmlService.Dispose();
                this.Close();
                this.Dispose();
            }
        }
        #endregion


        #endregion

        #endregion

        #region USER DEFINE METHODS

        #region MenuVisibility
        private void MenuVisibility(bool State)
        {
            string strMenuName = string.Empty;

            strSQL = "SELECT MENU_NAME " +
                     "FROM   MST_MENU " +
                     "WHERE  MENU_VISIBILITY = 0";
            DataSet dsMenuVisibility = dmlService.J_ExecSqlReturnDataSet(strSQL);
            if (dsMenuVisibility == null) return;

            if (dsMenuVisibility.Tables[0].Rows.Count > 0)
            {
                for (int intCount = 0; intCount < dsMenuVisibility.Tables[0].Rows.Count; intCount++)
                {
                    strMenuName = dsMenuVisibility.Tables[0].Rows[intCount][0].ToString();
                    
                    // Normal Masters Menu
                    if (strMenuName == mnuMUser.Name)
                        mnuMUser.Visible = State;
                    

                }
            }
            dsMenuVisibility.Dispose();
        }
        #endregion

        #region CheckRights
        public bool CheckRights(string MenuName)
        {
            if (J_Var.J_pLoginId.ToUpper() != "ADMIN")
            {
                if (dmlService.J_IsRecordExist("MST_MENU, MST_USER_RIGHTS",
                    "    MST_MENU.MENU_ID         = MST_USER_RIGHTS.MENU_ID " +
                    "AND MST_MENU.MENU_VISIBILITY = 0 " +
                    "AND MST_USER_RIGHTS.USER_ID  = " + J_Var.J_pUserId + " " +
                    "AND " + cmnService.J_SQLDBFormat("MST_MENU.MENU_NAME", J_SQLColFormat.UCase) + " = '" + MenuName.ToUpper() + "'") == false)
                {
                    cmnService.J_UserMessage(J_Msg.InsufficientRights);
                    return false;
                }
            }
            return true;
        }
        #endregion


        

        

        

        

        

        




        #endregion

    }
}
