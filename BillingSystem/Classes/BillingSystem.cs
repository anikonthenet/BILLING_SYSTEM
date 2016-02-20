    #region Refered Namespaces
    //~~ System Namespaces
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Data;
    using System.Collections;
    using System.Data.Sql;
    using System.Data.SqlClient;
    using System.Data.OracleClient;
    using System.Data.OleDb;
    using System.Data.Odbc;
    using System.Windows.Forms;
    using System.Reflection;
    using System.Diagnostics;
    using CrystalDecisions.CrystalReports.Engine;
    using Microsoft.VisualBasic.Compatibility.VB6;


    // User Namespaces
    using BillingSystem.Classes;
    using BillingSystem.FormRpt;
    using BillingSystem.FormSys;
    using BillingSystem.FormTrn;
    using BillingSystem.Reports.Listing;
    using BillingSystem.Reports.Transaction;



    #endregion

namespace BillingSystem.Classes
{
    #region STRUCTURE 

    #region BS_TaxId
    //Added by Shrey Kejriwal on 19/08/2011
    public struct BS_TaxId
    {
        public static int VAT_ID = 1;
        public static int CST_ID = 3;
        public static int CST_ID_FORM_C = 6;
        public static int VAT_ID_5 = 7;
        public static int CST_ID_5 = 8;
    }
    #endregion

    #region BS_ExportReportFormat
    public struct BS_ExportReportFormat
    {
        public const string PortableDocFormat = "pdf";
        public const string Excel = "xls";
        public const string Word = "doc";
    }
    #endregion

    #region BS_ProductWiseId
    public struct BS_ProductWiseId
    {
        public static int TDSMAN_Standard_Edition_2015_16 = 43;
        public static int TDSMAN_Professional_Edition_2015_16 = 44;
    }
    #endregion 

    #region BS_BillMode
    public struct BS_BillMode
    {
        public const string PhysicalDelivery = "Physical Delivery";
        public const string OnlineDelivery   = "Online Delivery";
        public const string OfflineDelivery  = "Offline Delivery";
    }
    #endregion

    #region BS_CompanyId
    public struct BS_CompanyId
    {
        public const int JayaSoftwares = 1;
        public const int PDSInfotech = 2;
    }
    #endregion 

    #region BS_PaymentTypeId
    public struct BS_PaymentTypeId
    {
        public static int Untagged = 0;
        public static int Neft_ChqDirect = 1;
        public static int Cash_Deposit_Direct = 2;
        public static int Chq_Office = 3;
        public static int Cash_Received = 4;
        public static int Cc_Avenue = 5;
        public static int Adv_Adj = 6;
        public static int Credit = 7;
    }
    #endregion 

    #region BS_PaymentType
    public struct BS_PaymentType
    {
        public static string Neft_ChqDirect = "NEFT/CHQ-DIRECT";
        public static string Cash_Deposit_Direct = "CASH DEPOSIT-DIRECT";
        public static string Chq_Office = "CHQ-OFFICE";
        public static string Cash_Received = "CASH RECEIVED";
        public static string Cc_Avenue = "CC AVENUE";
        public static string Adv_Adj = "ADV/ADJ";
        public static string Untagged = "UNTAGGED";
        public static string Credit = "CREDIT";
    }
    #endregion 

    #region BS_PartyCategory
    public struct BS_PartyCategory
    {
        public static string Sundry = "Sundry";
        public static string Dealer = "Dealer";
        public static string Others = "Others";
    }
    #endregion 

    #region BS_VerifyWebDB
    public struct BS_VerifyWebDB
    {
        public static string OnlineDeliverySerialNo = "OnlineDeliverySerialNo";
        public static string OfflineSerialNo = "OfflineSerialNo";
        public static string VerifyEmail = "VerifyEmail";
        public static string VerifySMS = "VerifySMS";
        public static string ReturnDetails = "ReturnDetails";
        public static string Others = "Others";
    }
    #endregion 

    #region BS_Collection_Mode
    public struct BS_Collection_Mode
    {
        public static string All = "All";
        public static string Unknown = "Unknown";
        public static string Mismatch = "Mismatch";
        public static string Unreconcile = "Unreconcile";
        public static string Tallied = "Tallied";
    }
    #endregion 

    #endregion

    class BS
    {

        #region Private Variable declaration

        private static string PartyName = "";
        private static string PartyMobNo = "";
        private static bool PartySendSMS = false;
        private static string PartyEmailId = "";
        private static bool PartySendEmail = false;
        private static string EmailID = "";
        private static string PartyContactPerson = "";
        private static string SavedInvoiceDetails = "";
        private static string OfflineSerialNo = "";

        private static bool SaveInvoiceStatus = true;
        private static bool SendEmailStatus = false;
        private static bool SendSMSStatus = false;



        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();


        private DataSet dsDataGrid;
        private DataSet dsDataPrint;

        string strSQL;
        //--

        private static string Billing_Salutation = "";
        private static string Billing_FName = "";
        private static string Billing_LName = "";
        private static string Billing_Company = "";
        private static string Billing_Address = "";
        private static string Billing_City = "";
        private static string Billing_State = "";
        private static string Billing_Pin = "";
        private static string Billing_Email = "";
        private static string Billing_Mobile = "";
        private static string Billing_Telephone = "";
        private static string CD_SerialID = "";
        private static string OrderNo = "";


        private static long LockInterval = 1;
        private static string tblTEMP_ERR_VALIDATION = "";
        private static string tblTEMP_OFFLINE_SERIAL_DETAILS = "";
        //ADDED BY DHRUB ON 08/04/2015
        private static string  Selected_PaymentType = "";
        private static string Selected_BillType = "";
        private static string Selected_Bank = "";
        private static string Selected_Party = "";
        #endregion       

        #region T_SystemMaintenance
        public bool T_SystemMaintenance()
        {
            try
            {
                //if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "ONLINE_FLAG") == true) return true;
                //if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "DEFAULT_ITEM_ONLINE_OFFLINE_BILLING") == true)
                //if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_BODY_OFFLINE") == true)
                //    return true;
                //--
                #region MST_BANK
                if (dmlService.J_IsDatabaseObjectExist("MST_BANK") == false)
                {
                    strSQL = "CREATE TABLE MST_BANK " +
                             "             (BANK_ID INTEGER DEFAULT 0, " +
                             "              BANK_NAME TEXT(10) DEFAULT \"\")";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_PAYMENT_TYPE
                if (dmlService.J_IsDatabaseObjectExist("MST_PAYMENT_TYPE") == false)
                {
                    strSQL = "CREATE TABLE MST_PAYMENT_TYPE " +
                             "             (PAYMENT_TYPE_ID INTEGER DEFAULT 0, " +
                             "              PAYMENT_TYPE_DESCRIPTION TEXT(30) DEFAULT \"\", " +
                             "              PAYMENT_TYPE_GROUP INTEGER DEFAULT 0)";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - PAYMENT_TYPE_ID
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "PAYMENT_TYPE_ID") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD PAYMENT_TYPE_ID INTEGER DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET PAYMENT_TYPE_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - BANK_ID
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "BANK_ID") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD BANK_ID INTEGER DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET BANK_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - REFERENCE_NO
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "REFERENCE_NO") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD REFERENCE_NO TEXT(20) DEFAULT \"\"";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET REFERENCE_NO = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - ACCOUNT_ENTRY_DATE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "ACCOUNT_ENTRY_DATE") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD ACCOUNT_ENTRY_DATE DATETIME";

                    dmlService.J_ExecSql(strSQL);

                }
                #endregion

                #region TRN_INVOICE_HEADER - BANK_STATEMENT_DATE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "BANK_STATEMENT_DATE") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD BANK_STATEMENT_DATE DATETIME";

                    dmlService.J_ExecSql(strSQL);

                }
                #endregion

                #region TRN_INVOICE_HEADER - CANCELLATION_FLAG
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "CANCELLATION_FLAG") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD CANCELLATION_FLAG INTEGER DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET CANCELLATION_FLAG = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_PARTY_CATEGORY
                if (dmlService.J_IsDatabaseObjectExist("MST_PARTY_CATEGORY") == false)
                {
                    strSQL = "CREATE TABLE MST_PARTY_CATEGORY " +
                             "             (PARTY_CATEGORY_ID INTEGER DEFAULT 0, " +
                             "              PARTY_CATEGORY_DESCRIPTION VARCHAR(30) DEFAULT '')";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_PARTY - PARTY_CATEGORY_ID
                if (dmlService.J_IsDatabaseObjectExist("MST_PARTY", "PARTY_CATEGORY_ID") == false)
                {
                    strSQL = "ALTER TABLE MST_PARTY ADD PARTY_CATEGORY_ID INTEGER DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_PARTY SET PARTY_CATEGORY_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_TAX - TAX_TYPE
                if (dmlService.J_IsDatabaseObjectExist("MST_TAX", "TAX_TYPE") == false)
                {
                    strSQL = "ALTER TABLE MST_TAX ADD TAX_TYPE VARCHAR(1) DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_TAX SET TAX_TYPE = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                // -----------------------------------------------------------
                // -- Added by Shrey Kejriwal on 21/01/2014
                // -----------------------------------------------------------

                #region MST_ITEM - RATE_2
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "RATE_2") == false)
                {
                    strSQL = "ALTER TABLE MST_ITEM ADD RATE_2 MONEY DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET RATE_2 = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM - NON_SALE_PERCENT
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "NON_SALE_PERCENT") == false)
                {
                    strSQL = "ALTER TABLE MST_ITEM ADD NON_SALE_PERCENT MONEY DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET NON_SALE_PERCENT = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion


                #region MST_ITEM - SALE_PERCENT
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "SALE_PERCENT") == false)
                {
                    strSQL = "ALTER TABLE MST_ITEM ADD SALE_PERCENT MONEY DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET SALE_PERCENT = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_COMPANY - MAX_DAYS_PERMIT
                if (dmlService.J_IsDatabaseObjectExist("MST_COMPANY", "MAX_DAYS_PERMIT") == false)
                {
                    strSQL = "ALTER TABLE MST_COMPANY ADD MAX_DAYS_PERMIT BIGINT DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_COMPANY SET MAX_DAYS_PERMIT = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_COMPANY - CIN_NO
                if (dmlService.J_IsDatabaseObjectExist("MST_COMPANY", "CIN_NO") == false)
                {
                    strSQL = "ALTER TABLE MST_COMPANY ADD CIN_NO NVARCHAR(50) DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_COMPANY SET CIN_NO = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM - INACTIVE_FLAG
                // -----------------------------------------------------------
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "INACTIVE_FLAG") == false)
                {
                    strSQL = "ALTER TABLE MST_ITEM ADD INACTIVE_FLAG SMALLINT DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET INACTIVE_FLAG = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                // -----------------------------------------------------------
                #region MST_SETUP - NETWORK_CREDENTIAL_USERNAME
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "NETWORK_CREDENTIAL_USERNAME") == false)
                {
                    strSQL = "ALTER TABLE MST_SETUP ADD NETWORK_CREDENTIAL_USERNAME VARCHAR(200) NOT NULL DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_SETUP SET NETWORK_CREDENTIAL_USERNAME = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_SETUP - NETWORK_CREDENTIAL_PASSWORD
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "NETWORK_CREDENTIAL_PASSWORD") == false)
                {
                    strSQL = "ALTER TABLE MST_SETUP ADD NETWORK_CREDENTIAL_PASSWORD VARCHAR(100) NOT NULL DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_SETUP SET NETWORK_CREDENTIAL_PASSWORD = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_SETUP - NETWORK_CREDENTIAL_PORT
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "NETWORK_CREDENTIAL_PORT") == false)
                {
                    strSQL = "ALTER TABLE MST_SETUP ADD NETWORK_CREDENTIAL_PORT VARCHAR(100) NOT NULL DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_SETUP SET NETWORK_CREDENTIAL_PORT = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_SETUP - NETWORK_CREDENTIAL_HOST
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "NETWORK_CREDENTIAL_HOST") == false)
                {
                    strSQL = "ALTER TABLE MST_SETUP ADD NETWORK_CREDENTIAL_HOST VARCHAR(200) NOT NULL DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_SETUP SET NETWORK_CREDENTIAL_HOST = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_SETUP - ENABLE_EMAIL_SENDING_SYSTEM
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "ENABLE_EMAIL_SENDING_SYSTEM") == false)
                {
                    strSQL = "ALTER TABLE MST_SETUP ADD ENABLE_EMAIL_SENDING_SYSTEM  SMALLINT NOT NULL DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_SETUP SET ENABLE_EMAIL_SENDING_SYSTEM = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - EMAIL_CONFIRMATION_COUNTER
                //---------------------------------------------------------------------------------------------------------
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "EMAIL_CONFIRMATION_COUNTER") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD EMAIL_CONFIRMATION_COUNTER  INT NOT NULL DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET EMAIL_CONFIRMATION_COUNTER = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - EMAIL_SEND_DATE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "EMAIL_SEND_DATE") == false)
                {
                    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD EMAIL_SEND_DATE DATETIME ";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET EMAIL_SEND_DATE = NULL";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM - EMAIL_TYPE_ID
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "EMAIL_TYPE_ID") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD EMAIL_TYPE_ID  BIGINT NOT NULL DEFAULT 0 ";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = " UPDATE MST_ITEM SET EMAIL_TYPE_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion
                //----------------------------------------------------------------------------------------------------------
                #region MST_EMAIL_DETAIL
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_DETAIL") == false)
                {
                    strSQL = "CREATE TABLE MST_EMAIL_DETAIL " +
                             "             (EMAIL_DETAIL_ID  BIGINT IDENTITY(1,1) NOT NULL, "+
                             "              EMAIL            VARCHAR(100) NOT NULL DEFAULT '', " +
                             "              WEBSITE          VARCHAR(100) NOT NULL DEFAULT '', " +
                             "              DISPLAYNAME      VARCHAR(100) NOT NULL DEFAULT '', " +
                             "              IMAGEURL         VARCHAR(100) NOT NULL DEFAULT '')";

                    dmlService.J_ExecSql(strSQL);
                    //--
                    strSQL = @" INSERT INTO MST_EMAIL_DETAIL(EMAIL,WEBSITE,DISPLAYNAME,IMAGEURL) 
                                VALUES( 'info@tdsman.com','www.tdsman.com','TDSMAN','http://www.tdsman.com/images/sign-logo.jpg')";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = @" INSERT INTO MST_EMAIL_DETAIL(EMAIL,WEBSITE,DISPLAYNAME,IMAGEURL) 
                                VALUES( 'info@chequeman.com','www.chequeman.com','CHEQUEMAN','http://www.chequeman.com/images/cheque-man-logo.png')";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM - EMAIL_DETAIL_ID
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "EMAIL_DETAIL_ID") == false)
                {
                    strSQL = "ALTER TABLE MST_ITEM ADD  EMAIL_DETAIL_ID BIGINT NOT NULL DEFAULT 0";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET EMAIL_DETAIL_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL") == false)
                {
                    strSQL = "CREATE TABLE MST_EMAIL " +
                             "             (EMAIL_ID       BIGINT IDENTITY(1,1), " +
                             "              EMAIL_DESC     VARCHAR(100) NOT NULL DEFAULT '')";

                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'gmail.com')";
                    dmlService.J_ExecSql(strSQL);


                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'yahoo.com')";
                    dmlService.J_ExecSql(strSQL);


                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'rediffmail.com')";
                    dmlService.J_ExecSql(strSQL);


                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'live.com')";
                    dmlService.J_ExecSql(strSQL);

                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'aol.com')";
                    dmlService.J_ExecSql(strSQL);

                    strSQL = @" INSERT INTO MST_EMAIL(EMAIL_DESC) 
                                VALUES( 'hotmail.com')";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY") == false)
                {
                    strSQL = @" CREATE TABLE MST_EMAIL_CATEGORY(EMAIL_TYPE_ID    BIGINT  IDENTITY(1,1),
                                                                EMAIL_TYPE_DESC  VARCHAR(500) NOT NULL DEFAULT '',
                                                                EMAIL_BODY       TEXT         NOT NULL DEFAULT '',
                                                                CREATE_USER_ID   BIGINT       NOT NULL DEFAULT 0,
                                                                INACTIVE_FLAG    SMALLINT     NOT NULL DEFAULT 0,
                                                                CREATE_DATE      DATETIME     DEFAULT SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30'))";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion


                #region TRN_EMAIL_ACTIVITY_LOG
                if (dmlService.J_IsDatabaseObjectExist("TRN_EMAIL_ACTIVITY_LOG") == false)
                {
                    strSQL = @" CREATE TABLE TRN_EMAIL_ACTIVITY_LOG(LOG_HEADER_ID	   BIGINT IDENTITY(1,1),
									                                INVOICE_HEADER_ID  BIGINT       NOT NULL DEFAULT 0,
									                                INVOICE_NO         VARCHAR(100) NOT NULL DEFAULT '',
									                                SEND_EMAIL_TO      VARCHAR(500) NOT NULL DEFAULT '',
									                                SEND_EMAIL_BCC     VARCHAR(500) NOT NULL DEFAULT '',
									                                USER_ID	           BIGINT       NOT NULL DEFAULT 0,
									                                ACTIVITY_DATE	   DATETIME)";
                     dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_DETAIL - EMAIL_TO_BCC
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_DETAIL", "EMAIL_TO_BCC") == false)
                {
                    strSQL = " ALTER TABLE MST_EMAIL_DETAIL ADD EMAIL_TO_BCC VARCHAR(100) NOT NULL DEFAULT '' ";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_EMAIL_DETAIL SET EMAIL_TO_BCC = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - EMAIL_SUBJECT
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_SUBJECT") == false)
                {
                    strSQL = " ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_SUBJECT VARCHAR(100) NOT NULL DEFAULT ''";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_EMAIL_CATEGORY SET EMAIL_SUBJECT = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region PAR_DELIVERY_MODE
                if (dmlService.J_IsDatabaseObjectExist("PAR_DELIVERY_MODE") == false)
                {
                    strSQL = "CREATE TABLE PAR_DELIVERY_MODE(" +
                             "                               DELIVERY_MODE_ID     BIGINT      DEFAULT 0  NOT NULL," +
                             "                               DELIVERY_MODE_DESC   VARCHAR(20) DEFAULT '' NOT NULL," +
                             "                               ONLINE_FLAG          TINYINT     DEFAULT 0  NOT NULL)";

                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsRecordExist("PAR_DELIVERY_MODE") == false)
                {
                    strSQL = @" INSERT INTO PAR_DELIVERY_MODE(DELIVERY_MODE_ID,DELIVERY_MODE_DESC,ONLINE_FLAG) VALUES (0, 'Physical Delivery', 0)";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @" INSERT INTO PAR_DELIVERY_MODE(DELIVERY_MODE_ID,DELIVERY_MODE_DESC,ONLINE_FLAG) VALUES (1, 'Online Delivery', 1)";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @" INSERT INTO PAR_DELIVERY_MODE(DELIVERY_MODE_ID,DELIVERY_MODE_DESC,ONLINE_FLAG) VALUES (2, 'Offline Delivery', 1)";
                    dmlService.J_ExecSql(strSQL);
                }

                #endregion 

                #region TRN_INVOICE_HEADER [DELIVERY_MODE_ID]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "DELIVERY_MODE_ID") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD DELIVERY_MODE_ID BIGINT DEFAULT 0 NOT NULL";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET DELIVERY_MODE_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER [ONLINE_ORDER_NO]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "ONLINE_ORDER_NO") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD ONLINE_ORDER_NO VARCHAR(20) DEFAULT '' NOT NULL";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET ONLINE_ORDER_NO = ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER [CONTACT_PERSON]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "CONTACT_PERSON") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD CONTACT_PERSON VARCHAR(50) DEFAULT '' NOT NULL";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER [MOBILE_NO]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "MOBILE_NO") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD MOBILE_NO VARCHAR(50) DEFAULT '' NOT NULL";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER [EMAIL_ID]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "EMAIL_ID") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD EMAIL_ID VARCHAR(100) DEFAULT '' NOT NULL";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER [OFFLINE_SERIAL_ID]
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "OFFLINE_SERIAL_ID") == false)
                {
                    strSQL = " ALTER TABLE TRN_INVOICE_HEADER ADD OFFLINE_SERIAL_ID BIGINT DEFAULT 0 NOT NULL";

                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE TRN_INVOICE_HEADER SET OFFLINE_SERIAL_ID = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_ITEM [ONLINE_FLAG]
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "ONLINE_FLAG") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD ONLINE_FLAG TINYINT DEFAULT 0 NOT NULL";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = "UPDATE MST_ITEM SET ONLINE_FLAG = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_OFFLINE_SERIAL
                if (dmlService.J_IsDatabaseObjectExist("TRN_OFFLINE_SERIAL") == false)
                {
                    strSQL = @" CREATE TABLE TRN_OFFLINE_SERIAL(
								                                OFFLINE_SERIAL_ID   BIGINT IDENTITY(1,1) NOT NULL,
								                                OFFLINE_SERIAL_DESC VARCHAR(20)          NOT NULL DEFAULT '',
								                                INACTIVE_FLAG       TINYINT              NOT NULL DEFAULT 0)";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_SMS_DETAIL
                if (dmlService.J_IsDatabaseObjectExist("MST_SMS_DETAIL") == false)
                {
                    strSQL = @" CREATE TABLE MST_SMS_DETAIL( SMS_DETAIL_ID	BIGINT IDENTITY(1,1),
                                                             WORKING_KEY	VARCHAR(200) NOT NULL DEFAULT '',
                                                             SMS_TEXT       VARCHAR(200) NOT NULL DEFAULT '',
                                                             INACTIVE_FLAG  TINYINT NOT NULL DEFAULT 0)";
                     dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM[REORDER_LEVEL]
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "REORDER_LEVEL") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD REORDER_LEVEL INT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);

                    strSQL = "UPDATE MST_ITEM SET REORDER_LEVEL = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                //-- ANIK @ 25/02/2015
                #region MST_ITEM DOWNLOAD_LINK1
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "DOWNLOAD_LINK1") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD DOWNLOAD_LINK1 VARCHAR(200) DEFAULT '' NOT NULL";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 
                //-- ANIK @ 25/02/2015
                #region MST_ITEM DOWNLOAD_LINK2
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "DOWNLOAD_LINK2") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD DOWNLOAD_LINK2 VARCHAR(200) DEFAULT '' NOT NULL";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 
                //-- ANIK @ 25/02/2015
                #region MST_ITEM DEFAULT_ITEM_ONLINE_OFFLINE_BILLING
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "DEFAULT_ITEM_ONLINE_OFFLINE_BILLING") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD DEFAULT_ITEM_ONLINE_OFFLINE_BILLING INT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY EMAIL_BODY_ONLINE
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_BODY_ONLINE") == false)
                {
                    strSQL = " ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_BODY_ONLINE TEXT NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY EMAIL_BODY_OFFLINE
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_BODY_OFFLINE") == false)
                {
                    strSQL = " ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_BODY_OFFLINE TEXT NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_OFFLINE_SERIAL
                if (dmlService.J_IsDatabaseObjectExist("MST_OFFLINE_SERIAL") == false)
                {
                    strSQL = "CREATE TABLE MST_OFFLINE_SERIAL " +
                             "             (OFFLINE_SERIAL_ID   BIGINT IDENTITY(1,1), " +
                             "              ITEM_ID             BIGINT NOT NULL DEFAULT 0, " +
                             "              OFFLINE_SERIAL_CODE VARCHAR(20) NOT NULL DEFAULT '', " +
                             "              INACTIVE_FLAG       TINYINT NOT NULL DEFAULT 0)";

                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - EMAIL_SUBJECT_ONLINE
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_SUBJECT_ONLINE") == false)
                {
                    strSQL = "ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_SUBJECT_ONLINE VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - EMAIL_SUBJECT_OFFLINE
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_SUBJECT_OFFLINE") == false)
                {
                    strSQL = "ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_SUBJECT_OFFLINE VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - FROM_EMAIL
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "FROM_EMAIL") == false)
                {
                    strSQL = "ALTER TABLE MST_EMAIL_CATEGORY ADD FROM_EMAIL VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - DISPLAY_NAME
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "DISPLAY_NAME") == false)
                {
                    strSQL = "ALTER TABLE MST_EMAIL_CATEGORY ADD DISPLAY_NAME VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_EMAIL_CATEGORY - EMAIL_BCC
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_BCC") == false)
                {
                    strSQL = "ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_BCC VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_INVOICE_HEADER - DELIVERY_MARK
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "DELIVERY_MARK") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD DELIVERY_MARK AS 
                            CASE WHEN DELIVERY_MODE_ID = 0 
                                 THEN '*'
                                 WHEN DELIVERY_MODE_ID = 1 
                                      THEN ''
                                 WHEN DELIVERY_MODE_ID = 2 
                                      THEN '#'
                                 ELSE ''
                            END";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_ITEM PDF_DOC_NAME
                if (dmlService.J_IsDatabaseObjectExist("MST_ITEM", "PDF_DOC_NAME") == false)
                {
                    strSQL = " ALTER TABLE MST_ITEM ADD PDF_DOC_NAME VARCHAR(100) DEFAULT '' NOT NULL";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_OFFLINE_SERIAL OFFLINE_CODE
                if (dmlService.J_IsDatabaseObjectExist("MST_OFFLINE_SERIAL", "OFFLINE_CODE") == false)
                {
                    strSQL = "ALTER TABLE MST_OFFLINE_SERIAL ADD OFFLINE_CODE VARCHAR(20) DEFAULT '' NOT NULL";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 
                
                #region TRN_INVOICE_HEADER REFERENCE_NO
                strSQL = @"UPDATE TRN_INVOICE_HEADER SET REFERENCE_NO = ONLINE_ORDER_NO 
                           WHERE  ONLINE_ORDER_NO <> ''
                           AND    REFERENCE_NO     = ''
                           AND    DELIVERY_MODE_ID = 1";
                dmlService.J_ExecSql(strSQL);
                #endregion 
 
                #region TRN_INVOICE_HEADER SEND_CD
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "SEND_CD") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD SEND_CD SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER SEND_SERIAL
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "SEND_SERIAL") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD SEND_SERIAL  SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER SEND_INVOICE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "SEND_INVOICE") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD SEND_INVOICE SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER REQUEST_DATE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "REQUEST_DATE") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD REQUEST_DATE DATETIME";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER REQUEST_USER_ID
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "REQUEST_USER_ID") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD REQUEST_USER_ID BIGINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER DESPATCH_DATE
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "DESPATCH_DATE") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD DESPATCH_DATE DATETIME";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER DESPATCH_USER_ID
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "DESPATCH_USER_ID") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD DESPATCH_USER_ID BIGINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_EMAIL_CATEGORY EMAIL_SUBJECT_DESPATCH
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_SUBJECT_DESPATCH") == false)
                {
                    strSQL = @"ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_SUBJECT_DESPATCH VARCHAR(100) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_EMAIL_CATEGORY EMAIL_BODY_DESPATCH
                if (dmlService.J_IsDatabaseObjectExist("MST_EMAIL_CATEGORY", "EMAIL_BODY_DESPATCH") == false)
                {
                    strSQL = @"ALTER TABLE MST_EMAIL_CATEGORY ADD EMAIL_BODY_DESPATCH TEXT NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region MST_COURIER
                if (dmlService.J_IsDatabaseObjectExist("MST_COURIER") == false)
                {
                    //--
                    strSQL = "CREATE TABLE MST_COURIER ( " +
                            "        COURIER_ID         INT IDENTITY(1,1) NOT NULL," +
                            "        COURIER_DESC       VARCHAR(255) NOT NULL DEFAULT ''," +
                            "        COURIER_TRACK_SITE VARCHAR(255) NOT NULL DEFAULT ''," +
                            "        COURIER_CONTACT_NO VARCHAR(255) NOT NULL DEFAULT '')";
                    dmlService.J_ExecSql(strSQL);
                    //--
                    #region INSERT DATA
                    //--
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC, COURIER_TRACK_SITE) VALUES('OVERNITE EXPRESS', 'http://www.overnitenet.com')"; dmlService.J_ExecSql(strSQL);
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC, COURIER_TRACK_SITE) VALUES('DTDC', 'http://dtdc.com')"; dmlService.J_ExecSql(strSQL);
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC, COURIER_TRACK_SITE) VALUES('SPEED POST', 'http://www.indiapost.gov.in')"; dmlService.J_ExecSql(strSQL);
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC, COURIER_TRACK_SITE) VALUES('FLY KING', 'http://www.flykingonline.com')"; dmlService.J_ExecSql(strSQL);
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC, COURIER_TRACK_SITE) VALUES('PROFESSIONAL COURIERS', 'http://www.tpcindia.com')"; dmlService.J_ExecSql(strSQL);
                    strSQL = @"INSERT INTO MST_COURIER(COURIER_DESC) VALUES('UNKNOWN')"; dmlService.J_ExecSql(strSQL);
                    //--
                    #endregion
                }
                #endregion

                #region TRN_INVOICE_HEADER COURIER_ID
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "COURIER_ID") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD COURIER_ID BIGINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_INVOICE_HEADER TRACKING_NO
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "TRACKING_NO") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD TRACKING_NO VARCHAR(25) NOT NULL DEFAULT ''";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 
                
                #region TRN_COLLECTION_HEADER
                if (dmlService.J_IsDatabaseObjectExist("TRN_COLLECTION_HEADER") == false)
                {
                    strSQL = @"CREATE TABLE TRN_COLLECTION_HEADER (
                                    COLLECTION_HEADER_ID       BIGINT       IDENTITY,
                                    ORIGINAL_INVOICE_HEADER_ID BIGINT       NOT NULL DEFAULT 0,
                                    COLLECTION_DATE            DATETIME,
                                    COMPANY_ID                 BIGINT       NOT NULL DEFAULT 0,
                                    FAYEAR_ID                  BIGINT       NOT NULL DEFAULT 0,
                                    PAYMENT_TYPE_ID            BIGINT       NOT NULL DEFAULT 0,
                                    BANK_ID                    BIGINT       NOT NULL DEFAULT 0,
                                    REFERENCE_NO               VARCHAR(20)  NOT NULL DEFAULT '',
                                    GROSS_AMT                  MONEY        NOT NULL DEFAULT 0,
                                    LESS_AMT                   MONEY        NOT NULL DEFAULT 0,
                                    NET_AMT                    MONEY        NOT NULL DEFAULT 0,
                                    COLLECTION_REMARKS         VARCHAR(100) NOT NULL DEFAULT '',
                                    NET_INVOICE_AMT            MONEY        NOT NULL DEFAULT 0,
                                    DUE_AMT                    AS GROSS_AMT - NET_INVOICE_AMT,
                                    RECONCILIATION_DATE        DATETIME,
                                    CREATE_DATETIME            DATETIME     DEFAULT GETDATE(),
                                    USER_ID                    BIGINT       NOT NULL DEFAULT 0,
                                    AUTO_POST_FLAG             TINYINT      NOT NULL DEFAULT 0
                                    )";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion 

                #region TRN_COLLECTION_DETAIL
                if (dmlService.J_IsDatabaseObjectExist("TRN_COLLECTION_DETAIL") == false)
                {
                    strSQL = @"CREATE TABLE TRN_COLLECTION_DETAIL (
                                    COLLECTION_DETAIL_ID BIGINT IDENTITY,
                                    COLLECTION_HEADER_ID BIGINT NOT NULL DEFAULT 0,
                                    INVOICE_HEADER_ID    BIGINT NOT NULL DEFAULT 0,
                                    COLLECTION_AMOUNT    MONEY  NOT NULL DEFAULT 0)";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion                 

                #region TRN_INVOICE_HEADER RECON_FLAG
                if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "RECON_FLAG") == false)
                {
                    strSQL = @"ALTER TABLE TRN_INVOICE_HEADER ADD RECON_FLAG TINYINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_PAYMENT_TYPE SUNDRY_PARTY_FLAG
                if (dmlService.J_IsDatabaseObjectExist("MST_PAYMENT_TYPE", "SUNDRY_PARTY_FLAG") == false)
                {
                    strSQL = @"ALTER TABLE MST_PAYMENT_TYPE ADD SUNDRY_PARTY_FLAG SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE MST_PAYMENT_TYPE SET SUNDRY_PARTY_FLAG = 1 WHERE PAYMENT_TYPE_ID IN(1,2,3,4,5,7)";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_PAYMENT_TYPE AUTO_COLLECTION_POST_FLAG
                if (dmlService.J_IsDatabaseObjectExist("MST_PAYMENT_TYPE", "AUTO_COLLECTION_POST_FLAG") == false)
                {
                    strSQL = @"ALTER TABLE MST_PAYMENT_TYPE ADD AUTO_COLLECTION_POST_FLAG SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    strSQL = @"UPDATE MST_PAYMENT_TYPE SET AUTO_COLLECTION_POST_FLAG = 1 WHERE PAYMENT_TYPE_ID IN(1,2,3,4)";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_INVOICE_SERIES FAYEAR_ID
                if (dmlService.J_IsDatabaseObjectExist("MST_INVOICE_SERIES", "FAYEAR_ID") == false)
                {
                    strSQL = @"ALTER TABLE MST_INVOICE_SERIES ADD FAYEAR_ID BIGINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_FAYEAR INACTIVE_FLAG
                if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR", "INACTIVE_FLAG") == false)
                {
                    strSQL = @"ALTER TABLE MST_FAYEAR ADD INACTIVE_FLAG SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                }
                #endregion

                #region MST_PAYMENT_TYPE INACTIVE_FLAG
                if (dmlService.J_IsDatabaseObjectExist("MST_PAYMENT_TYPE", "INACTIVE_FLAG") == false)
                {
                    strSQL = @"ALTER TABLE MST_PAYMENT_TYPE ADD INACTIVE_FLAG SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);
                    //
                    //
                    strSQL = @"UPDATE MST_PAYMENT_TYPE SET INACTIVE_FLAG = 1 WHERE PAYMENT_TYPE_ID IN(6,0)";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region MST_SETUP LAST_RECONCILIATION_DATE
                if (dmlService.J_IsDatabaseObjectExist("MST_SETUP", "LAST_RECONCILIATION_DATE") == false)
                {
                    strSQL = @"ALTER TABLE MST_SETUP ADD LAST_RECONCILIATION_DATE DATETIME ";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                #region TRN_COLLECTION_HEADER TALLIED
                if (dmlService.J_IsDatabaseObjectExist("TRN_COLLECTION_HEADER", "TALLIED") == false)
                {
                    strSQL = @"ALTER TABLE TRN_COLLECTION_HEADER ADD TALLIED SMALLINT NOT NULL DEFAULT 0";
                    dmlService.J_ExecSql(strSQL);

                    strSQL = @"UPDATE TRN_COLLECTION_HEADER SET TALLIED = 0";
                    dmlService.J_ExecSql(strSQL);
                }
                #endregion

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region T_CheckInternetConnectivty
        public bool T_CheckInternetConnectivty()
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch (Exception err)
            {
                return false; // host not reachable.
            }
        }
        #endregion

        #region ToTitleCase
        public string ToTitleCase(string mText)
        {
            string rText = "";
            try
            {
                mText = mText.ToLower();
                System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo TextInfo = cultureInfo.TextInfo;
                rText = TextInfo.ToTitleCase(mText);
            }
            catch
            {
                rText = mText;
            }
            return rText;
        }
        #endregion

        #region VerifyWebDatabase
        public bool VerifyWebDatabase(string Type, string OrderNo, string Email, string SMS, out string EmailMobile)
        {
            //SqlConnection SqlConn = new SqlConnection("Data Source=64.31.60.73,1232; Initial Catalog=CENTER; User ID=center; Pwd=Center@#$123; Connect Timeout=525600; Persist Security Info=True; MultipleActiveResultSets=False; Packet Size=4096; Application Name=&quot;Microsoft SQL Server Management Studio&quot;");
            SqlConnection SqlConn = new SqlConnection(@"Data Source=64.31.60.73,1232; 
                                                        Initial Catalog=TDSMAN; 
                                                        User ID=tdsman; 
                                                        Password=ran789jan@#$; 
                                                        Connect Timeout=525600; 
                                                        Persist Security Info=True; 
                                                        MultipleActiveResultSets=False; 
                                                        Packet Size=4096; 
                                                        Application Name='Microsoft SQL Server Management Studio'");
            SqlCommand SqlComm = new SqlCommand();
            string strSQL="";
            int intCount = 0;
            EmailMobile = "";
            try
            {
                SqlConn.Open();
                SqlComm = null;
                //
                if (Type == BS_VerifyWebDB.OnlineDeliverySerialNo)
                {
                    strSQL = @"SELECT COUNT(*)
                               FROM   TDSMAN_TRN_ORDER_DETAIL,
                                      TDSMAN_ONLINE_BUYERS_SERIAL
                               WHERE  TDSMAN_TRN_ORDER_DETAIL.ORDER_DETAIL_ID = TDSMAN_ONLINE_BUYERS_SERIAL.ORDER_DETAIL_ID
                               AND    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO          ='" + OrderNo + @"'";
                    SqlComm = new SqlCommand(strSQL, SqlConn);
                    intCount = cmnService.J_ReturnInt32Value(Convert.ToString(SqlComm.ExecuteScalar()));
                    //
                    if (intCount == 0)
                        return false;
                }
                else if (Type == BS_VerifyWebDB.VerifyEmail)
                {
                    strSQL = @"SELECT COUNT(*)
                               FROM   TDSMAN_TRN_ORDER_HEADER,
                                      TDSMAN_TRN_ORDER_DETAIL
                               WHERE  TDSMAN_TRN_ORDER_HEADER.ORDER_HEADER_ID = TDSMAN_TRN_ORDER_DETAIL.ORDER_HEADER_ID
                               AND    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO    ='" + OrderNo + @"'
                               AND    TDSMAN_TRN_ORDER_HEADER.BILLING_EMAIL   ='" + Email + @"'";
                    SqlComm = new SqlCommand(strSQL, SqlConn);
                    intCount = cmnService.J_ReturnInt32Value(Convert.ToString(SqlComm.ExecuteScalar()));
                    //
                    if (intCount == 0)
                    {
                        strSQL = @"SELECT BILLING_EMAIL
                                   FROM   TDSMAN_TRN_ORDER_HEADER,
                                          TDSMAN_TRN_ORDER_DETAIL
                                   WHERE  TDSMAN_TRN_ORDER_HEADER.ORDER_HEADER_ID = TDSMAN_TRN_ORDER_DETAIL.ORDER_HEADER_ID
                                   AND    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO    ='" + OrderNo + @"'";
                        SqlComm = new SqlCommand(strSQL, SqlConn);
                        EmailMobile = Convert.ToString((SqlComm.ExecuteScalar()));
                        return false;
                    }
                }
                else if (Type == BS_VerifyWebDB.VerifySMS)
                {
                    strSQL = @"SELECT COUNT(*)
                               FROM   TDSMAN_TRN_ORDER_HEADER,
                                      TDSMAN_TRN_ORDER_DETAIL
                               WHERE  TDSMAN_TRN_ORDER_HEADER.ORDER_HEADER_ID = TDSMAN_TRN_ORDER_DETAIL.ORDER_HEADER_ID
                               AND    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO    ='" + OrderNo + @"'
                               AND    TDSMAN_TRN_ORDER_HEADER.BILLING_MOBILE  ='" + SMS + @"'";
                    SqlComm = new SqlCommand(strSQL, SqlConn);
                    intCount = cmnService.J_ReturnInt32Value(Convert.ToString(SqlComm.ExecuteScalar()));
                    //
                    if (intCount == 0)
                    {
                        strSQL = @"SELECT BILLING_MOBILE
                                   FROM   TDSMAN_TRN_ORDER_HEADER,
                                          TDSMAN_TRN_ORDER_DETAIL
                                   WHERE  TDSMAN_TRN_ORDER_HEADER.ORDER_HEADER_ID = TDSMAN_TRN_ORDER_DETAIL.ORDER_HEADER_ID
                                   AND    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO    ='" + OrderNo + @"'";
                        SqlComm = new SqlCommand(strSQL, SqlConn);
                        EmailMobile = Convert.ToString((SqlComm.ExecuteScalar()));
                        return false;
                    }
                }
                else if (Type == BS_VerifyWebDB.ReturnDetails)
                {
                    //Billing_Salutation -- 0
                    //Billing_FName      -- 1
                    //Billing_LName      -- 2
                    //Billing_Company    -- 3
                    //Billing_Address    -- 4
                    //Billing_City       -- 5
                    //Billing_State      -- 6
                    //Billing_Pin        -- 7
                    //Billing_Email      -- 8
                    //Billing_Mobile     -- 9
                    //Billing_Telephone  -- 10
                    //LICENSED_CD_SERIAL_ID -- 11
                    //OrderNo -- 12
                    strSQL = @"SELECT   TDSMAN_TRN_ORDER_HEADER.Billing_Salutation  + '^' + 
                                        TDSMAN_TRN_ORDER_HEADER.Billing_FName       + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_LName       + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Company     + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Address     + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_City        + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_State       + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Pin         + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Email       + '^' +
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Mobile      + '^' + 
                                        TDSMAN_TRN_ORDER_HEADER.Billing_Telephone   + '^' + 
                                        CONVERT(varchar(10),ISNULL(TDSMAN_ONLINE_BUYERS_SERIAL.LICENSED_CD_SERIAL_ID,0)) + '^' + 
                                        '" + OrderNo + @"'
                               FROM     TDSMAN_TRN_ORDER_HEADER INNER JOIN TDSMAN_TRN_ORDER_DETAIL
                                        ON TDSMAN_TRN_ORDER_HEADER.ORDER_HEADER_ID = TDSMAN_TRN_ORDER_DETAIL.ORDER_HEADER_ID
                                        LEFT JOIN TDSMAN_ONLINE_BUYERS_SERIAL
                                        ON TDSMAN_TRN_ORDER_DETAIL.ORDER_DETAIL_ID = TDSMAN_ONLINE_BUYERS_SERIAL.ORDER_DETAIL_ID
                               WHERE    TDSMAN_TRN_ORDER_DETAIL.ORDER_REF_NO    = '" + OrderNo + @"'";
                    SqlComm = new SqlCommand(strSQL, SqlConn);
                    EmailMobile = Convert.ToString((SqlComm.ExecuteScalar()));
                    return true;
                }
                //
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
            finally
            {
                SqlConn.Close();                
            }

        }
        #endregion

        #region PopulateGridView

        #region PopulateGridView
        public void PopulateGridView(DataGridView grdDesc, string sQueryString, string[,] arrColumn)
        {
            DataSet dsetShowDataInGrid = new DataSet();
            dsetShowDataInGrid = dmlService.J_ExecSqlReturnDataSet(sQueryString);

            //-- Column index of Dgv
            int intColumnIndex = 1;

            //-- Add Checkbox in  Column no 1
            DataGridViewCheckBoxColumn colCbx = new DataGridViewCheckBoxColumn();
            colCbx.ThreeState = false;

            grdDesc.Columns.Clear();
            grdDesc.Columns.Add(colCbx);
            grdDesc.DataSource = dsetShowDataInGrid.Tables[0];
            grdDesc.Columns[0].Width = 30;

            for (int intCounter = 0; intCounter <= arrColumn.GetUpperBound(0); intCounter++)
            {
                //----------------------------------------------------------
                //-- set the Header text & Width of respective column
                //----------------------------------------------------------					
                grdDesc.Columns[intColumnIndex].DataPropertyName = dsetShowDataInGrid.Tables[0].Columns[intCounter].ColumnName;
                grdDesc.Columns[intColumnIndex].HeaderText = arrColumn[intCounter, (int)J_GridColumnSetting.HeaderText];
                grdDesc.Columns[intColumnIndex].Width = int.Parse(arrColumn[intCounter, (int)J_GridColumnSetting.Width]);
                grdDesc.Columns[intColumnIndex].ReadOnly = true;
                //----------------------------------------------------------
                //-- set the Data Format of respective column
                //----------------------------------------------------------
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Format = arrColumn[intCounter, (int)J_GridColumnSetting.Format];
                //----------------------------------------------------------
                //-- set the Alignment of respective column
                //----------------------------------------------------------					
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                {
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    grdDesc.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    grdDesc.Columns[intColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    grdDesc.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                //----------------------------------------------------------
                //-- set the Visibility of respective column
                //----------------------------------------------------------					
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Visible].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.Visible].Trim().ToUpper() == "T")
                    grdDesc.Columns[intColumnIndex].Visible = true;
                else
                    grdDesc.Columns[intColumnIndex].Visible = false;
                //----------------------------------------------------------
                //-- set the AutoSize Mode  of respective column Default None
                //----------------------------------------------------------	
                if (arrColumn[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim().ToUpper() == "NONE")
                    grdDesc.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                else
                    grdDesc.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //----------------------------------------------------------	
                intColumnIndex += 1;
                //----------------------------------------------------------	
            }
        }
        #endregion

        #region PopulateGridView
        public void PopulateGridView(DataGridView grdDesc, IDbCommand command, string sQueryString, string[,] arrColumn)
        {
            DataSet dsetShowDataInGrid = new DataSet();
            dsetShowDataInGrid = dmlService.J_ExecSqlReturnDataSet(command, sQueryString);

            //-- Column index of Dgv
            int intColumnIndex = 1;

            //-- Add Checkbox in  Column no 1
            DataGridViewCheckBoxColumn colCbx = new DataGridViewCheckBoxColumn();
            colCbx.ThreeState = false;

            grdDesc.Columns.Clear();
            grdDesc.Columns.Add(colCbx);
            grdDesc.DataSource = dsetShowDataInGrid.Tables[0];
            grdDesc.Columns[0].Width = 30;

            for (int intCounter = 0; intCounter <= arrColumn.GetUpperBound(0); intCounter++)
            {
                //----------------------------------------------------------
                //-- set the Header text & Width of respective column
                //----------------------------------------------------------					
                grdDesc.Columns[intColumnIndex].DataPropertyName = dsetShowDataInGrid.Tables[0].Columns[intCounter].ColumnName;
                grdDesc.Columns[intColumnIndex].HeaderText = arrColumn[intCounter, (int)J_GridColumnSetting.HeaderText];
                grdDesc.Columns[intColumnIndex].Width = int.Parse(arrColumn[intCounter, (int)J_GridColumnSetting.Width]);
                grdDesc.Columns[intColumnIndex].ReadOnly = true;
                //----------------------------------------------------------
                //-- set the Data Format of respective column
                //----------------------------------------------------------
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Format = arrColumn[intCounter, (int)J_GridColumnSetting.Format];
                //----------------------------------------------------------
                //-- set the Alignment of respective column
                //----------------------------------------------------------					
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                {
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    grdDesc.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    grdDesc.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    grdDesc.Columns[intColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    grdDesc.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                //----------------------------------------------------------
                //-- set the Visibility of respective column
                //----------------------------------------------------------					
                if (arrColumn[intCounter, (int)J_GridColumnSetting.Visible].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.Visible].Trim().ToUpper() == "T")
                    grdDesc.Columns[intColumnIndex].Visible = true;
                else
                    grdDesc.Columns[intColumnIndex].Visible = false;
                //----------------------------------------------------------
                //-- set the AutoSize Mode  of respective column Default None
                //----------------------------------------------------------	
                if (arrColumn[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim() == "" | arrColumn[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim().ToUpper() == "NONE")
                    grdDesc.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                else
                    grdDesc.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //----------------------------------------------------------	
                intColumnIndex += 1;
                //----------------------------------------------------------	
            }
        }
        #endregion

        #endregion

        #region InsertCollectionEntry
        public bool InsertCollectionEntry(string CollectionDate, long CompanyID, long FAYearID, long PaymentTypeID, long BankID, string ReferenceNo,
                                           double NetAmount, long UserID, long InvoiceHeaderID)
        {
            try
            {
                string strSQL = "";
                //                
                dmlService.J_BeginTransaction();
                //
                strSQL = @"INSERT INTO TRN_COLLECTION_HEADER (
                                               ORIGINAL_INVOICE_HEADER_ID,
                                               COLLECTION_DATE, 
                                               COMPANY_ID, 
                                               FAYEAR_ID, 
                                               PAYMENT_TYPE_ID, 
                                               BANK_ID, 
                                               REFERENCE_NO, 
                                               GROSS_AMT,
                                               NET_AMT, 
                                               NET_INVOICE_AMT,
                                               USER_ID, 
                                               CREATE_DATETIME,
                                               AUTO_POST_FLAG) 
                                   VALUES(
                                               " + InvoiceHeaderID + @", 
                                               " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(CollectionDate) + cmnService.J_DateOperator() + @", 
                                               " + CompanyID + @", 
                                               " + FAYearID + @", 
                                               " + PaymentTypeID + @", 
                                               " + BankID + @", 
                                              '" + ReferenceNo + @"', 
                                               " + NetAmount + @",  
                                               " + NetAmount + @",    
                                               " + NetAmount + @",  
                                               " + UserID + @",
                                               " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + @", 
                                               1)";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    //Rollback Transaction
                    dmlService.J_Rollback();
                    return false;
                }
                // get max ledger header id
                long CollectionHeaderId = dmlService.J_ReturnMaxValue("TRN_COLLECTION_HEADER", "COLLECTION_HEADER_ID",
                                                                    "    COMPANY_ID = " + CompanyID + " " +
                                                                    "AND FAYEAR_ID  = " + FAYearID + " " +
                                                                    "AND USER_ID    = " + UserID + "");
                //
                strSQL = "INSERT INTO TRN_COLLECTION_DETAIL (" +
                                         "            COLLECTION_HEADER_ID," +
                                         "            INVOICE_HEADER_ID," +
                                         "            COLLECTION_AMOUNT) " +
                                         "    VALUES( " + CollectionHeaderId + "," +
                                         "            " + InvoiceHeaderID + "," +
                                         "            " + NetAmount + ")";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    //Rollback Transaction
                    dmlService.J_Rollback();
                    return false;
                }
                //
                dmlService.J_Commit();
                //
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

        #region T_DeleteOfflineSerialDetails
        public bool T_DeleteOfflineSerialDetails(long ItemID)
        {
            strSQL = "DELETE FROM MST_OFFLINE_SERIAL WHERE ITEM_ID = " + ItemID;
            if (dmlService.J_ExecSql(strSQL) == false)
                return false;
            else
                return true;
        }
        #endregion    
        
        #region T_isFileOpenOrReadOnly
        public bool T_isFileOpenOrReadOnly(ref string file)
        {
            try
            {
                //first make sure it's not a read only file
                if ((File.GetAttributes(file) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    //first we open the file with a FileStream
                    using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        try
                        {
                            stream.ReadByte();
                            return false;
                        }
                        catch (IOException)
                        {
                            return true;
                        }
                        finally
                        {
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                }
                else
                    return true;
            }
            catch (IOException)
            {
                return true;
            }
        }
        #endregion

        #region T_WriteField [ OVERLOADED METHOD ]

        #region T_WriteField [1]
        public string T_WriteField(string str)
        {
            return str.Trim() + "^";
        }
        #endregion

        #region T_WriteField [2]
        public string T_WriteField(string str, string strFormat)
        {
            return string.Format("{0:" + strFormat + "}", Convert.ToDouble(str.Trim() == "" ? "0" : str.Trim())) + "^";
        }
        #endregion

        #endregion

        #region EXCEL DATABASE OBJECT EXIST [ OVERLOADED METHOD ]

        #region T_IsExcelDatabaseObjectExist [1]
        public bool T_IsExcelDatabaseObjectExist(string TableName, string ColumnName)
        {
            return T_IsExcelDatabaseObjectExist(TableName, ColumnName, null);
        }
        #endregion

        #region T_IsExcelDatabaseObjectExist [2]
        public bool T_IsExcelDatabaseObjectExist(string TableName, string ColumnName, OleDbConnection cn)
        {
            DataTable dt;
            //
            if (TableName != null && ColumnName == null)
            {
                dt = cn.GetSchema("tables");
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if ("'" + TableName.Trim().ToUpper() + "$'" == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper())
                    {
                        dt.Dispose();
                        //cn.Dispose();
                        return true;
                    }
                }
            }
            else if (TableName != null && ColumnName != null)
            {
                //dt = cn.GetSchema("Columns",);

                //ADDED BY DHRUB ON 04/11/2013
                dt = cn.GetSchema("Columns", new string[] { null, null, "'" + TableName.Trim().ToUpper() + "$'", null });

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Debug.Print(Convert.ToString(dt.Rows[i]["COLUMN_NAME"]).Trim().ToUpper());
                    if ("'" + TableName.Trim().ToUpper() + "$'" == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper()
                    && ColumnName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["COLUMN_NAME"]).Trim().ToUpper())
                    {
                        dt.Dispose();
                        //cn.Dispose();
                        return true;
                    }
                }
            }
            //
            //COMMENTED FOR 24Q SALARY DETAIL EXTRA FIVE FIELDS CHECKING
            //cn.Dispose();
            return false;
        }
        #endregion


        #endregion

        #region T_ReplaceDoubleQuotesinFile
        public void T_ReplaceDoubleQuotesinFile(string FilePath, bool ExcelImport)
        {
            try
            {
                //ADDED BY SHREY KEJRIWAL ON 24/05/2012
                //REPLACING DOUBLE QUOTES

                //Reading the data from the text file
                StreamReader READER = new StreamReader(FilePath);

                //Replacing the double quotes 
                string strStream = READER.ReadToEnd();

                strStream = strStream.Replace("\"", "\"\"");

                //Added by Shrey Kejriwal on 13/12/2012
                //To make sure the last line is also imported
                if (ExcelImport == false) // Skipping extra line creation for excel imports
                    strStream = strStream + "\n";

                READER.Close();
                READER.Dispose();

                //Deleting the text file
                File.Delete(FilePath);

                //Checking if any field value starts with double quotes 
                //i.e checking the value ' ^" ' in the file 
                if (strStream.Contains("^\"\"") == false)
                {
                    // if the value ' ^" ' not exists then creating file from the whole stream
                    StreamWriter Writer = cmnService.J_ReturnStreamWriter(FilePath);

                    //Writing in the same text file
                    Writer.Write(strStream);

                    Writer.Flush();
                    Writer.Close();
                    Writer.Dispose();
                }
                else
                {
                    //if the value ' ^" ' exists 
                    StreamWriter Writer = cmnService.J_ReturnStreamWriter(FilePath);

                    //reading the stream line by line
                    StringReader StringReader = new StringReader(strStream);

                    //checking EOF
                    while (StringReader.Peek() != -1)
                    {
                        //Reading Line
                        string strLine = StringReader.ReadLine();

                        //Checking if the value '^"' exists in this line
                        if (strLine.Contains("^\"\"") == false)
                            Writer.WriteLine(strLine);
                        else
                        {
                            //Splitting the line into array 
                            string[] strarr = strLine.Split('^');

                            for (int i = 0; i < strarr.Length; i++)
                            {
                                //now checking each field of the line
                                if (strarr[i] != "")
                                {
                                    if (strarr[i].Substring(0, 1) == "\"")
                                    {
                                        //if any field has a problem

                                        //Then rectifying the field
                                        strarr[i] = "\"" + strarr[i] + "\"";
                                    }
                                }
                            }

                            //Joining the rectified array
                            strLine = string.Join("^", strarr);

                            //Writing the rectified/original line
                            Writer.WriteLine(strLine);
                        }
                    }

                    StringReader.Close();
                    StringReader.Dispose();

                    Writer.Flush();
                    Writer.Close();
                    Writer.Dispose();
                }
            }
            catch (Exception err)
            {
                return;
            }
        }

        #endregion


        #region T_GetSetupDetails()
        public bool T_GetSetupDetails()
        {
            IDataReader reader = null;
            try
            {

                // Make the query string for Company Information
                strSQL = "SELECT " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "                  AS BRANCH_ID," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_CODE", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                 AS BRANCH_CODE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                 AS BRANCH_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.COMPANY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                AS COMPANY_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.ADDRESS", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                     AS ADDRESS," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.START_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + "           AS START_DATE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SAVE_CONFIRMATION_MSG", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "       AS SAVE_CONFIRMATION_MSG," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_USERNAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS NETWORK_CREDENTIAL_USERNAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_PASSWORD", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS NETWORK_CREDENTIAL_PASSWORD," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_PORT", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS NETWORK_CREDENTIAL_PORT," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_HOST", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS NETWORK_CREDENTIAL_HOST, " +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.LAST_RECONCILIATION_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + " AS LAST_RECONCILIATION_DATE " +
                         "FROM   MST_SETUP " +
                         "WHERE  MST_SETUP.BRANCH_ID = " + J_Var.J_pBranchId + "";

                // return the data reader as per above query string
                reader = dmlService.J_ExecSqlReturnReader(strSQL);

                // check the given reader is null
                if (reader == null) return false;

                // fetch the data from reader
                while (reader.Read())
                {
                    J_Var.J_pBranchId = Convert.ToInt32(Convert.ToString(reader["BRANCH_ID"]));
                    J_Var.J_pBranchCode = Convert.ToString(reader["BRANCH_CODE"]);
                    J_Var.J_pBranchName = Convert.ToString(reader["BRANCH_NAME"]);
                    J_Var.J_pCompanyName = Convert.ToString(reader["COMPANY_NAME"]);
                    J_Var.J_pBranchAddress = Convert.ToString(reader["ADDRESS"]);
                    J_Var.J_pSoftwareStartDate = Convert.ToString(reader["START_DATE"]);
                    J_Var.J_pSaveConfirmMsg = Convert.ToInt32(Convert.ToString(reader["SAVE_CONFIRMATION_MSG"]));

                    //ADDED BY DHRUB ON 31/01/2015 FOR SEND EMAIL AGAINST IVOICE
                    J_Var.J_pNetworkCredential_Username = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_USERNAME"]));
                    J_Var.J_pNetworkCredential_Password = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_PASSWORD"]));
                    J_Var.J_pNetworkCredential_Port = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_PORT"]));
                    J_Var.J_pNetworkCredential_Host = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_HOST"]));
                    J_Var.J_pLastReconcileDate = Convert.ToString(reader["LAST_RECONCILIATION_DATE"]);


                }
                // reader is closed & disposed
                reader.Close();
                reader.Dispose();
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion    

        #region USER DEFINE PROPERTIES

        #region P_Billing_Salutation
        public static string P_Billing_Salutation
        {
            get
            {
                return Billing_Salutation;
            }
            set
            {
                Billing_Salutation = value;
            }
        }
        #endregion

        #region P_Billing_FName
        public static string P_Billing_FName
        {
            get
            {
                return Billing_FName;
            }
            set
            {
                Billing_FName = value;
            }
        }
        #endregion

        #region P_Billing_LName
        public static string P_Billing_LName
        {
            get
            {
                return Billing_LName;
            }
            set
            {
                Billing_LName = value;
            }
        }
        #endregion

        #region P_Billing_Company
        public static string P_Billing_Company
        {
            get
            {
                return Billing_Company;
            }
            set
            {
                Billing_Company = value;
            }
        }
        #endregion

        #region P_Billing_Address
        public static string P_Billing_Address
        {
            get
            {
                return Billing_Address;
            }
            set
            {
                Billing_Address = value;
            }
        }
        #endregion

        #region P_Billing_City
        public static string P_Billing_City
        {
            get
            {
                return Billing_City;
            }
            set
            {
                Billing_City = value;
            }
        }
        #endregion

        #region P_Billing_State
        public static string P_Billing_State
        {
            get
            {
                return Billing_State;
            }
            set
            {
                Billing_State = value;
            }
        }
        #endregion

        #region P_Billing_Pin
        public static string P_Billing_Pin
        {
            get
            {
                return Billing_Pin;
            }
            set
            {
                Billing_Pin = value;
            }
        }
        #endregion

        #region P_Billing_Email
        public static string P_Billing_Email
        {
            get
            {
                return Billing_Email;
            }
            set
            {
                Billing_Email = value;
            }
        }
        #endregion

        #region P_Billing_Mobile
        public static string P_Billing_Mobile
        {
            get
            {
                return Billing_Mobile;
            }
            set
            {
                Billing_Mobile = value;
            }
        }
        #endregion

        #region P_Billing_Telephone
        public static string P_Billing_Telephone
        {
            get
            {
                return Billing_Telephone;
            }
            set
            {
                Billing_Telephone = value;
            }
        }
        #endregion

        #region P_CD_SerialID
        public static string P_CD_SerialID
        {
            get
            {
                return CD_SerialID;
            }
            set
            {
                CD_SerialID = value;
            }
        }
        #endregion

        #region P_OrderNo
        public static string P_OrderNo
        {
            get
            {
                return OrderNo;
            }
            set
            {
                OrderNo = value;
            }
        }
        #endregion

        #region BS_PartyName
        public static string BS_PartyName
        {
            get { return PartyName; }
            set { PartyName = value; }
        }
         #endregion

        #region BS_PartyContactPerson
        public static string BS_PartyContactPerson
        {
            get { return PartyContactPerson; }
            set { PartyContactPerson = value; }
        }
        #endregion

        #region BS_PartyMobNo
        public static string BS_PartyMobNo
        {
            get { return PartyMobNo; }
            set { PartyMobNo = value; }
        }
        #endregion

        #region BS_PartySendSMS
        public static bool BS_PartySendSMS
        {
            get { return PartySendSMS; }
            set { PartySendSMS = value; }
        }
        #endregion

        #region BS_PartyEmailId
        public static string BS_PartyEmailId
        {
            get { return PartyEmailId; }
            set { PartyEmailId = value; }
        }
        #endregion

        #region BS_PartySendEmail
        public static bool BS_PartySendEmail
        {
            get { return PartySendEmail; }
            set { PartySendEmail = value; }
        }
        #endregion

        #region BS_EmailId
        public static string BS_EmailID
        {
            get { return EmailID; }
            set { EmailID = value; }
        }
        #endregion

        #region BS_SaveInvoiceStatus
        public static bool BS_SaveInvoiceStatus
        {
            get { return SaveInvoiceStatus; }
            set { SaveInvoiceStatus = value; }
        }
        #endregion

        #region BS_SendEmailStatus
        public static bool BS_SendEmailStatus
        {
            get { return SendEmailStatus; }
            set { SendEmailStatus = value; }
        }
        #endregion

        #region BS_SendSMSStatus
        public static bool BS_SendSMSStatus
        {
            get { return SendSMSStatus; }
            set { SendSMSStatus = value; }
        }
        #endregion

        #region BS_SavedInvoiceDetails
        public static string BS_SavedInvoiceDetails
        {
            get { return SavedInvoiceDetails; }
            set { SavedInvoiceDetails = value; }
        }
        #endregion

        #region BS_OfflineSerialNo
        public static string BS_OfflineSerialNo
        {
            get { return OfflineSerialNo; }
            set { OfflineSerialNo = value; }
        }
        #endregion 
        //--
        #region T_pLockInterval
        public static long T_pLockInterval
        {
            get
            {
                return LockInterval;
            }
            set
            {
                LockInterval = value;
            }
        }
        #endregion

        #region T_tblTEMP_OFFLINE_SERIAL_DETAILS
        public static string T_tblTEMP_OFFLINE_SERIAL_DETAILS
        {
            get
            {
                return tblTEMP_OFFLINE_SERIAL_DETAILS;
            }
            set
            {
                tblTEMP_OFFLINE_SERIAL_DETAILS = value;
            }
        }
        #endregion

        #region T_tblTEMP_ERR_VALIDATION
        public static string T_tblTEMP_ERR_VALIDATION
        {
            get
            {
                return tblTEMP_ERR_VALIDATION;
            }
            set
            {
                tblTEMP_ERR_VALIDATION = value;
            }
        }
        #endregion


        #region T_Selected_PaymentType
        public static string T_Selected_PaymentType
        {
            get
            {
                return Selected_PaymentType;
            }
            set
            {
                Selected_PaymentType = value;
            }
        }
        #endregion

        #region T_Selected_BillType
        public static string T_Selected_BillType
        {
            get
            {
                return Selected_BillType;
            }
            set
            {
                Selected_BillType = value;
            }
        }
        #endregion

        #region T_Selected_Bank
        public static string T_Selected_Bank
        {
            get
            {
                return Selected_Bank;
            }
            set
            {
                Selected_Bank = value;
            }
        }
        #endregion

        #region T_Selected_Party
        public static string T_Selected_Party
        {
            get
            {
                return Selected_Party;
            }
            set
            {
                Selected_Party = value;
            }
        }
        #endregion

        #endregion


    }
}
