
#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: TrnInvoicePrint
Version			: 2.0
Start Date		: 14-06-2011
End Date		: 
Tables Used     : 
Module Desc		: Print the Invoice
____________________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using BillingSystem.Classes;
using BillingSystem.Reports.Transaction.INVOICE;

#endregion

namespace BillingSystem.FormTrn.PopUp
{
    public partial class TrnInvoicePrint : Form
    {

        #region OBJECT DECLARATION

        DMLService dmlService;
        CommonService cmnService;
        ReportService rptService;        

        #endregion

        #region CONSTRACTOR [OVERLOADED METHOD]

        #region CONSTRACTOR[1]
        public TrnInvoicePrint(long InvoiceHeaderId)
        {
            InitializeComponent();

            dmlService = new DMLService();
            cmnService = new CommonService();
            rptService = new ReportService();
            
            lngInvoiceHeaderId = InvoiceHeaderId;
            
        }
        #endregion

        #region CONSTRACTOR[2]
        public TrnInvoicePrint(long InvoiceHeaderId,string InvoiceDetails)
        {
            InitializeComponent();

            dmlService = new DMLService();
            cmnService = new CommonService();
            rptService = new ReportService();

            lngInvoiceHeaderId = InvoiceHeaderId;

            string[] strInvoiceDetails = InvoiceDetails.Split('^');

            InvoiceNO = strInvoiceDetails[0].Trim();
            //SerialNo  = strInvoiceDetails[1].Trim();
            EmailID   = strInvoiceDetails[2].Trim();
            MobileNo  = strInvoiceDetails[3].Trim();
            //--
            if (strInvoiceDetails[1].Trim() == "")
            {
                lblOfflineSerialNumber.Visible = false;
                label6.Visible = false;
            }
            else
            {
                lblOfflineSerialNumber.Visible = true;
                label6.Visible = true;
                SerialNo = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT OFFLINE_CODE FROM MST_OFFLINE_SERIAL WHERE OFFLINE_SERIAL_CODE ='" + strInvoiceDetails[1].Trim() + "'"));
            }
        }
        #endregion

        #endregion

        #region PRIVATE VARIABLE DECLARATION

        private long lngInvoiceHeaderId = 0;
        private string strSQL = "";

        private string InvoiceNO = "";
        private string SerialNo = "";
        private string EmailID = "";
        private string MobileNo = "";


        #endregion

        #region EVENTS

        #region TrnInvoicePrint_Load
        private void TrnInvoicePrint_Load(object sender, EventArgs e)
        {
            grpSort.Text = "Invoice Type";
            //Commented by Shrey on 05/08/2011
            //rbnSort1.Text = "Full Set";
            //rbnSort2.Text = "Buyer's Copry";
            //rbnSort3.Text = "Duplicate Copy";
            //rbnSort4.Text = "Office Copy";
            //rbnSort1.Checked = true;

            //Added by Shrey Kejriwal on 05/08/2011
            chkBox1.Text = "Buyer's Copy";
            chkBox2.Text = "Duplicate Copy";
            chkBox3.Text = "Office Copy";

            grpSort1.Text = "Print Type";
            rbnSort1_1.Text = "None";
            rbnSort1_2.Text = "Print with Letter Head";
            rbnSort1_1.Checked = true;

            //-----------------------------------------------------------------------------------------------------
            if (InvoiceNO.Trim() != "" || SerialNo.Trim() != "" || MobileNo.Trim() != "" || EmailID.Trim() != "")
                grpInvoiceDetails.Visible = true;
            else
                grpInvoiceDetails.Visible = false;
            //-----------------------------------------------------------------------------------------------------
            if (InvoiceNO.Trim() != "")
                lblInvoiceNo.Text = InvoiceNO;
            else
                lblInvoiceNo.Text = "";
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            if (SerialNo.Trim() != "")
                lblOfflineSerialNumber.Text = SerialNo;
            else
                lblOfflineSerialNumber.Text = "";
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            if (MobileNo.Trim() != "")
                lblMobileNo.Text = MobileNo;
            else
                lblMobileNo.Text = "";
            //-----------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------
            if (EmailID.Trim() != "")
                lblEmailId.Text = EmailID;
            else
                lblEmailId.Text = "";
            //-----------------------------------------------------------------------------------------------------


            BtnSave.Select();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Transaction is started
            dmlService.J_BeginTransaction();

            // report file object
            ReportClass rptcls; 
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
                         "                          VALUES (1              , '" + cmnService.J_ReplaceQuote("(Original - Buyer's Copy)") + "', " + intPrintType + ")";
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
            //
            strSQL = "SELECT " + strTableName + ".INVOICE_TYPE_ID           AS INVOICE_TYPE_ID," +
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
                     "       MST_COMPANY.PAN                                AS PAN, " +
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
                     "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                     "AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                     "AND    TRN_INVOICE_HEADER.COMPANY_ID        = MST_COMPANY.COMPANY_ID " +
                     "AND    TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID " +
                     "AND    TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID " +
                     "AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + lngInvoiceHeaderId + " " +
                     "AND    TRN_INVOICE_HEADER.BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                     "ORDER BY " + strTableName + ".INVOICE_TYPE_ID," +
                     "       TRN_INVOICE_HEADER.INVOICE_NO";

            // SUB REPORTS
            // FOR SUMMARY OF TAX DETAILS
            string strSubRptTaxDetails = "SELECT MST_TAX.TAX_ID                    AS TAX_ID," +
                                         "       MST_TAX.TAX_DESC                  AS TAX_DESC," +
                                         "       TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                                         "       TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE," +
                                         "       TRN_INVOICE_TAX.TAX_AMOUNT        AS TAX_AMOUNT " +
                                         "FROM   MST_TAX," +
                                         "       TRN_INVOICE_TAX " +
                                         "WHERE  MST_TAX.TAX_ID                    = TRN_INVOICE_TAX.TAX_ID " +
                                         "AND    TRN_INVOICE_TAX.INVOICE_HEADER_ID = " + lngInvoiceHeaderId + " " +
                                         "AND    MST_TAX.BRANCH_ID                 = " + J_Var.J_pBranchId + " " +
                                         "ORDER BY TRN_INVOICE_TAX.INVOICE_TAX_ID";

            // Transaction is commited
            dmlService.J_Commit();

            // POPULATE & DISPLAY SUB REPORT
            rptcls.OpenSubreport("crSubRptTaxSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSubRptTaxDetails).Tables[0]);

            PictureObject objBlobFieldObject;
            objBlobFieldObject = (PictureObject)rptcls.ReportDefinition.Sections[2].ReportObjects["imgSignature"];
            objBlobFieldObject.ObjectFormat.EnableSuppress = true;

            // report is executed
            DataSet ds = dmlService.J_ExecSqlReturnDataSet(strSQL);
            if (ds == null) return;
            rptService.J_PreviewReport(ref rptcls, this, ds, J_Var.J_pCompanyName, J_Var.J_pBranchAddress, "INVOICE");

            // Drop the report temp table
            if (dmlService.J_ExecSql("DROP TABLE " + strTableName) == false) return;           
            //--
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


        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnExit_Click(sender, e);
        }
        #endregion       

        #endregion

    }
}