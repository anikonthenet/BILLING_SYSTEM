
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: Global Enumeration
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Enumeration
_________________________________________________________________________________________________________

*/

#endregion

#region Namespaces

using System;
using BillingSystem.FormSys;

#endregion

namespace BillingSystem.Classes
{

    #region ENUMERATION

    #region J_OSType
    public enum J_OSType
    {
        _32Bit,
        _64Bit
    }
    #endregion

    #region J_ApplicationType
    public enum J_ApplicationType
    {
        StandAlone_SingleMachine,
        StandAlone_SingleMachineBrowser,
        StandAlone_Network,
        Web
    }
    #endregion

    #region J_DatabaseType
    public enum J_DatabaseType
    {
        SqlServer,
        Oracle,
        MsAccess,
        Excel_Xls,
        Excel_Xlsx,
        Others
    }
    #endregion

    #region J_ConnectionProviderType
    public enum J_ConnectionProviderType
    {
        Sql,
        Oracle,
        OleDb,
        Odbc,
        Others
    }
    #endregion


    #region J_SQLColFormat
    public enum J_SQLColFormat
    {
        DateFormatDDMMYYYY,
        DateFormatMMDDYYYY,
        DateFormatYYYYMMDD,
        NullCheck,
        UCase,
        LCase,
        Cast,
        Case_End,
        Format,
        Others
    }
    #endregion
    

    #region J_ComboBoxSelectedIndex
    public enum J_ComboBoxSelectedIndex
    {
        YES,
        NO
    }
    #endregion

    #region J_IndexType
    public enum J_IndexType
    {
        All,
        Unique,
        NonUnique
    }
    #endregion

    #region J_SearchType
    public enum J_SearchType
    {
        Incremental,
        Embedded
    }
    #endregion

    #region J_GridColumnSetting
    public enum J_GridColumnSetting
    {
        HeaderText,
        Width,
        Format,
        Alignment,
        NullToText,
        Visible,
        AutoSizeMode
    }
    #endregion

    #region J_ColumnType
    public enum J_ColumnType
    {
        None,
        String,
        Char,
        Integer,
        Long,
        Double,
        Date,
        Time,
        DateTime,
        Others
    }
    #endregion

    #region J_PrintType
    public enum J_PrintType
    {
        Direct,
        Preview,
        Export
    }
    #endregion

    #region J_DataType
    public enum J_DataType
    {
        Character,
        Numeric,
        BlankSpace,
        WildCard
    }
    #endregion

    #region J_Alignment
    public enum J_Alignment
    {
        LeftTop = 0,
        LeftCentre = 1,
        LeftBottom = 2,
        CenterTop = 3,
        CenterCentre = 4,
        CenterBottom = 5,
        RightTop = 6,
        RightCentre = 7,
        RightBottom = 8,
        General = 9
    }
    #endregion

    #region J_PanelIndex
    public enum J_PanelIndex
    {
        e00_DisplayText     = 0,
        e01_FAYear          = 1,
        e02_BranchName      = 2,
        e03_ServerName      = 3,
        e04_Database        = 4,
        e05_UserDisplayName = 5,
        e06_ProgressBar     = 6,
        e07_DateTime        = 7
    }
    #endregion

    #region J_QueryType
    public enum J_QueryType
    {
        Text,
        StoredProcedure,
        DirectTable,
        DirectQuery
    }
    #endregion

    #region J_SQLType
    public enum J_SQLType
    {
        DDL,
        DML
    }
    #endregion

    #region J_ShowMessage
    public enum J_ShowMessage
    {
        YES,
        NO
    }
    #endregion

    #region J_AllowSetFocus
    public enum J_AllowSetFocus
    {
        YES,
        NO
    }
    #endregion

    #region J_Colon
    public enum J_Colon
    {
        YES,
        NO
    }
    #endregion

    #region J_LoginScreen
    public enum J_LoginScreen
    {
        YES,
        NO
    }
    #endregion

    #region J_NewLine
    public enum J_NewLine
    {
        YES,
        NO
    }
    #endregion

    #region J_ExportImport
    public enum J_ExportImport
    {
        YES,
        NO
    }
    #endregion

    #region J_ContentHeader
    public enum J_ContentHeader
    {
        YES,
        NO
    }
    #endregion

    #region J_SubstringType
    public enum J_SubstringType
    {
        LEFT,
        MID,
        RIGHT
    }
    #endregion

    #region J_ComboBoxDefaultText
    public enum J_ComboBoxDefaultText
    {
        YES,
        NO
    }
    #endregion

    #region J_ElsePart
    public enum J_ElsePart
    {
        YES,
        NO
    }
    #endregion

    #region J_Identity
    public enum J_Identity
    {
        YES,
        NO
    }
    #endregion

    #region J_Branch
    public enum J_Branch
    {
        YES,
        NO
    }
    #endregion

    #region J_DefaultValue
    public enum J_DefaultValue
    {
        YES,
        NO
    }
    #endregion

    #region J_CENTRAL_BRANCH
    public enum J_CENTRAL_BRANCH
    {
        CENTRAL,
        BRANCH
    }
    #endregion

    #region J_ExportFileType
    public enum J_ExportFileType : uint
    {
        TextFile,
        CSVFile,
        ExcelFile,
        MsAcessFile
    }
    #endregion

    #region J_RestoreFileType
    public enum J_RestoreFileType : uint
    {
        TextFile,
        CSVFile,
        ExcelFile,
        MsAcessFile,
        MsSQLServerFile
    }
    #endregion

    #endregion

    #region STRUCTURE

    #region J_Msg
    public struct J_Msg
    {
        public const string AddModeSave        = "  Data Saved";
        public const string EditModeSave       = "  Existing data updated";
        public const string DeleteMode         = "  Data deleted";
        public const string RecNotExist        = "Data does not exist";
        public const string SelRec             = "Please select the record from the List";
        public const string AreYouSure2Delete  = "Are you sure to delete this Record ??";
        public const string DuplicateRec       = "Duplicate does not allowed";
        public const string ChildRecExist      = "Can not be cancelled";
        public const string InvalidUser        = "Invalid user";
        public const string InvalidPassword    = "Invalid password";
        public const string ConnectionFailed   = "Connection failed";
        public const string DataNotFound       = "Record not found";
        public const string SearchingValues    = "Please enter the searching value";
        public const string ReportPreview      = "Record not found.\n No preview available";
        public const string InvalidDate        = "Invalid date entered";
        public const string BlankDate          = "Please enter date";
        public const string InsufficientRights = "Insufficient Rights.\nPlease contact administrator";
        public const string WantToProceed      = "Proceed......(Y/N)";
        public const string WantToCalcelled    = "Want to Cancel";
        public const string WantToPrint        = "Print......(Y/N)";
        public const string DuplicateCode      = "Duplicate Code";
        public const string DuplicateName      = "Duplicate Name";
        public const string Cancelled          = "  Successfully cancelled";
        public const string CannotDelete       = "Can not be Deleted";
        public const string CannotCancel       = "Can not be Cancelled";
        public const string WantToRemove       = "Want to Remove...(Y/N)";

    }
    #endregion

    #region J_Mode
    public struct J_Mode
    {
        public const string Add         = "Add Mode";
        public const string Edit        = "Edit Mode";
        public const string View        = "View Mode";
        public const string ViewListing = "View Listing Mode";
        public const string Delete      = "Delete Mode";
        public const string General     = "General Mode";
        public const string Sorting     = "Sorting Mode";
        public const string Searching   = "Searching Mode";
    }
    #endregion

    #region J_TextSeparator
    public struct J_TextSeparator
    {
        public const string Comma      = ",";
        public const string Pipe       = "|";
        public const string FrontSlash = "/";
        public const string Dash       = "-";
        public const string Dot        = ".";
        public const string Star       = "*";
        public const string AtTheRate  = "@";
        public const string Caret      = "^";
        public const string Quote      = "'";

    }
    #endregion

    #region J_TextQualifier
    public struct J_TextQualifier
    {
        public const string None = "None";
        public const string SingleQuete = "'";
        public const string DoubleQuete = "";
        public const string Caret = "^";

    }
    #endregion

    #region J_ColumnMapping
    public struct J_ColumnMapping
    {
        public string SourceName;
        public string Destination;

        public J_ColumnMapping(string SourceColumn, string DestinationColumn)
        {
            SourceName  = SourceColumn;
            Destination = DestinationColumn;
        }
    }
    #endregion

    #region J_DataTableDataType
    public struct J_DataTableDataType
    {
        public const string Integer   = "Int32";
        public const string Long      = "Int64";
        public const string String    = "String";
        public const string Character = "String";
        public const string DateTime  = "DateTime";
        public const string Decimal   = "Decimal";
        public const string Byte      = "Byte";
        public const string Boolean   = "Boolean";
        public const string Currency  = "Currency";
        public const string Binary    = "Binary";
    }
    #endregion


    #endregion

    #region J_Var CLASS
    public sealed class J_Var
    {

        #region PUBLIC OBJECTS DECLERATION

        //~~~~ Declare global object of MDI main ~~~~
        public static mdiBillingSystem frmMain;
        
        #endregion

        #region PRIVATE VARIABLES DECLERATION

        private static string ApplicationName     = "No Title";
        private static string ProjectName         = "No Title";
        private static string SWStartDate         = "";
        private static string FABegDate           = "";
        private static string FAEndDate = "";
        private static int CollectionPostFlag = 0;
        private static int ReconFlag = 1;
        private static string CompanyCode         = "";
        private static string CompanyName         = "";
        private static string CompanyAddress      = "";
        private static string BranchCode          = "";
        private static string BranchName          = "";
        private static string BranchAddress       = "";
        private static string LoginId             = "";
        private static string UserDisplayName     = "";
        private static string ServerName          = "";
        private static string DatabaseName        = "";
        private static string CentralDatabaseName = "";
        private static string PoolingDatabaseName = "";

        private static int LoginStatus       = 0;
        private static int FAYearId          = 0;
        private static int CompanyId         = 0;
        private static int BranchId          = 0;
        private static long UserId           = 0;
        private static int UserCategory      = 0;
        private static int AdminUser         = 0;
        private static int ReportEnumIndex   = 0;
        private static int SaveConfirmMsg    = 0;
        
        private static int ConnectionTimeout = 30;
        private static int CommandTimeout    = 30;

        private static string XmlBranchInfo                               = "xmlBranchInfo.xml";
        private static string XmlConnectionFileName                       = "xmlConnection.xml";
        private static J_ConnectionProviderType enmConnectionProviderType = J_ConnectionProviderType.Sql;
        private static J_DatabaseType enmDatabaseType                     = J_DatabaseType.SqlServer;
        private static J_ApplicationType enmApplicationType               = J_ApplicationType.StandAlone_Network;
        private static string MsAccessDatabaseName                        = "eMFBranch.mdb";
        private static string MsAccessDatabasePassword                    = "mother";
        private static J_LoginScreen enmLoginScreen                       = J_LoginScreen.YES;
        private static string ZipFilePassword                             = "jcs";
        private static J_Branch enmBranch                                 = J_Branch.YES;
        private static J_OSType enmOSType                                 = J_OSType._32Bit;
        
        private static string[] Matrix;

        private static string NetworkCredential_Username = "";    //SET TO SEND THE EMAIL
        private static string NetworkCredential_Password = "";    //SET TO SEND THE EMAIL
        private static string NetworkCredential_Port     = "";    //SET TO SEND THE EMAIL
        private static string NetworkCredential_Host     = "";    //SET TO SEND THE EMAIL

        private static string SMS_WorkingKey = "";                //TO SET SMS WORKING KEY
        private static string Offline_Invoice_SMS_Text = "";      //TO SET SMS WORKING KEY
        private static string Online_Invoice_SMS_Text = "";       //TO SET SMS WORKING KEY
        private static string SMS_SenderName = "";                //TO SET SMS WORKING KEY

        private static string LastReconcileDate = "";             //SET TO CHECK THE LAST RECONCILIATION DATE

        #endregion

        #region PUBLIC VARIABLES DECLERATION

        
        #endregion

        #region USER DEFINE PROPERTIES

        #region OPERATING SYSTEM TYPE [32 bit | 64 bit]
        public static J_OSType J_pOSType
        {
            get
            {
                return enmOSType;
            }
            set
            {
                enmOSType = (J_OSType)value;
            }
        }
        #endregion

        #region J_pApplicationName
        public static string J_pApplicationName
        {
            get
            {
                return ApplicationName;
            }
            set
            {
                ApplicationName = value;
            }
        }
        #endregion

        #region J_pProjectName
        public static string J_pProjectName
        {
            get
            {
                return ProjectName;
            }
            set
            {
                ProjectName = value;
            }
        }
        #endregion

        #region J_pReportEnumIndex
        public static int J_pReportEnumIndex
        {
            get
            {
                return ReportEnumIndex;
            }
            set
            {
                ReportEnumIndex = value;
            }
        }
        #endregion

        #region J_pSaveConfirmMsg
        public static int J_pSaveConfirmMsg
        {
            get
            {
                return SaveConfirmMsg;
            }
            set
            {
                SaveConfirmMsg = value;
            }
        }
        #endregion

        #region J_pSoftwareStartDate
        public static string J_pSoftwareStartDate
        {
            get
            {
                return SWStartDate;
            }
            set
            {
                SWStartDate = value;
            }
        }
        #endregion

        #region J_pLoginStatus
        public static int J_pLoginStatus
        {
            get
            {
                return LoginStatus;
            }
            set
            {
                LoginStatus = value;
            }
        }
        #endregion

        #region J_pFAYearId
        public static int J_pFAYearId
        {
            get
            {
                return FAYearId;
            }
            set
            {
                FAYearId = value;
            }
        }
        #endregion

        #region J_pFABegDate
        public static string J_pFABegDate
        {
            get
            {
                return FABegDate;
            }
            set
            {
                FABegDate = value;
            }
        }
        #endregion

        #region J_pFAEndDate
        public static string J_pFAEndDate
        {
            get
            {
                return FAEndDate;
            }
            set
            {
                FAEndDate = value;
            }
        }
        #endregion

        #region J_pCollectionPostFlag
        public static int J_pCollectionPostFlag
        {
            get
            {
                return CollectionPostFlag;
            }
            set
            {
                CollectionPostFlag = value;
            }
        }
        #endregion

        #region J_pReconFlag
        public static int J_pReconFlag
        {
            get
            {
                return ReconFlag;
            }
            set
            {
                ReconFlag = value;
            }
        }
        #endregion
        
        #region J_pCompanyId
        public static int J_pCompanyId
        {
            get
            {
                return CompanyId;
            }
            set
            {
                CompanyId = value;
            }
        }
        #endregion

        #region J_pCompanyCode
        public static string J_pCompanyCode
        {
            get
            {
                return CompanyCode;
            }
            set
            {
                CompanyCode = value;
            }
        }
        #endregion

        #region J_pCompanyName
        public static string J_pCompanyName
        {
            get
            {
                return CompanyName;
            }
            set
            {
                CompanyName = value;
            }
        }
        #endregion

        #region J_pCompanyAddress
        public static string J_pCompanyAddress
        {
            get
            {
                return CompanyAddress;
            }
            set
            {
                CompanyAddress = value;
            }
        }
        #endregion


        #region J_pBranchId
        public static int J_pBranchId
        {
            get
            {
                return BranchId;
            }
            set
            {
                BranchId = value;
            }
        }
        #endregion

        #region J_pBranchCode
        public static string J_pBranchCode
        {
            get
            {
                return BranchCode;
            }
            set
            {
                BranchCode = value;
            }
        }
        #endregion

        #region J_pBranchName
        public static string J_pBranchName
        {
            get
            {
                return BranchName;
            }
            set
            {
                BranchName = value;
            }
        }
        #endregion

        #region J_pBranchAddress
        public static string J_pBranchAddress
        {
            get
            {
                return BranchAddress;
            }
            set
            {
                BranchAddress = value;
            }
        }
        #endregion


        #region J_pNetworkCredential_Username
        public static string J_pNetworkCredential_Username
        {
            get
            {
                return NetworkCredential_Username;
            }
            set
            {
                NetworkCredential_Username = value;
            }
        }
        #endregion

        #region J_pNetworkCredential_Password
        public static string J_pNetworkCredential_Password
        {
            get
            {
                return NetworkCredential_Password;
            }
            set
            {
                NetworkCredential_Password = value;
            }
        }
        #endregion

        #region J_pNetworkCredential_Port
        public static string J_pNetworkCredential_Port
        {
            get
            {
                return NetworkCredential_Port;
            }
            set
            {
                NetworkCredential_Port = value;
            }
        }
        #endregion

        #region J_pNetworkCredential_Host
        public static string J_pNetworkCredential_Host
        {
            get
            {
                return NetworkCredential_Host;
            }
            set
            {
                NetworkCredential_Host = value;
            }
        }
        #endregion

        #region J_pLastReconcileDate
        public static string J_pLastReconcileDate
        {
            get
            {
                return LastReconcileDate;
            }
            set
            {
                LastReconcileDate = value;
            }
        }
        #endregion


        #region J_pSMS_WorkingKey
        public static string J_pSMS_WorkingKey
        {
            get
            {
                return SMS_WorkingKey;
            }
            set
            {
                SMS_WorkingKey = value;
            }
        }
        #endregion

        #region J_pOfflineInvoiceSMS_Text
        public static string J_pOfflineInvoiceSMS_Text
        {
            get
            {
                return Offline_Invoice_SMS_Text;
            }
            set
            {
                Offline_Invoice_SMS_Text = value;
            }
        }
        #endregion

        #region J_pOnlineInvoiceSMS_Text
        public static string J_pOnlineInvoiceSMS_Text
        {
            get
            {
                return Online_Invoice_SMS_Text;
            }
            set
            {
                Online_Invoice_SMS_Text = value;
            }
        }
        #endregion

        #region J_pSMS_SenderName
        public static string J_pSMS_SenderName
        {
            get
            {
                return SMS_SenderName;
            }
            set
            {
                SMS_SenderName = value;
            }
        }
        #endregion
        


        #region J_pUserId
        public static long J_pUserId
        {
            get
            {
                return UserId;
            }
            set
            {
                UserId = value;
            }
        }
        #endregion

        #region J_pAdminUser
        public static int J_pAdminUser
        {
            get
            {
                return AdminUser;
            }
            set
            {
                AdminUser = value;
            }
        }
        #endregion

        #region J_pLoginId
        public static string J_pLoginId
        {
            get
            {
                return LoginId;
            }
            set
            {
                LoginId = value;
            }
        }
        #endregion

        #region J_pUserDisplayName
        public static string J_pUserDisplayName
        {
            get
            {
                return UserDisplayName;
            }
            set
            {
                UserDisplayName = value;
            }
        }
        #endregion

        #region J_pUserCategory
        public static int J_pUserCategory
        {
            get
            {
                return UserCategory;
            }
            set
            {
                UserCategory = value;
            }
        }
        #endregion


        #region J_pMatrix
        public static string[] J_pMatrix
        {
            get
            {
                return Matrix;
            }
            set
            {
                Matrix = value;
            }
        }
        #endregion

        #region J_pServerName
        public static string J_pServerName
        {
            get
            {
                return ServerName;
            }
            set
            {
                ServerName = value;
            }
        }
        #endregion

        #region J_pDatabaseName
        public static string J_pDatabaseName
        {
            get
            {
                return DatabaseName;
            }
            set
            {
                DatabaseName = value;
            }
        }
        #endregion


        #region CONNECTION TIMEOUT
        public static int J_pConnectionTimeout
        {
            get
            {
                return ConnectionTimeout;
            }
            set
            {
                ConnectionTimeout = (int)value;
            }
        }
        #endregion

        #region COMMAND TIMEOUT
        public static int J_pCommandTimeout
        {
            get
            {
                return CommandTimeout;
            }
            set
            {
                CommandTimeout = (int)value;
            }
        }
        #endregion


        #region XML BRANCH INFO FILE NAME
        public static string J_pXmlBranchInfoFileName
        {
            get
            {
                return XmlBranchInfo;
            }
            set
            {
                XmlBranchInfo = value;
            }
        }
        #endregion

        #region XML CONNECTION FILE NAME
        public static string J_pXmlConnectionFileName
        {
            get
            {
                return XmlConnectionFileName;
            }
            set
            {
                XmlConnectionFileName = value;
            }
        }
        #endregion


        #region CONNECTION PROVIDER TYPE
        public static J_ConnectionProviderType J_pConnectionProviderType
        {
            get
            {
                return enmConnectionProviderType;
            }
            set
            {
                enmConnectionProviderType = (J_ConnectionProviderType)value;
            }
        }
        #endregion

        #region DATABASE TYPE
        public static J_DatabaseType J_pDatabaseType
        {
            get
            {
                return enmDatabaseType;
            }
            set
            {
                enmDatabaseType = (J_DatabaseType)value;
            }
        }
        #endregion

        #region APPLICATION TYPE
        public static J_ApplicationType J_pApplicationType
        {
            get
            {
                return enmApplicationType;
            }
            set
            {
                enmApplicationType = value;
            }
        }
        #endregion

        #region MS ACCESS DATABASE NAME
        public static string J_pMsAccessDatabaseName
        {
            get
            {
                return MsAccessDatabaseName;
            }
            set
            {
                MsAccessDatabaseName = value;
            }
        }
        #endregion

        #region MS ACCESS DATABASE PASSWORD
        public static string J_pMsAccessDatabasePassword
        {
            get
            {
                return MsAccessDatabasePassword;
            }
            set
            {
                MsAccessDatabasePassword = value;
            }
        }
        #endregion

        #region LOGIN SCREEN
        public static J_LoginScreen J_pLoginScreen
        {
            get
            {
                return enmLoginScreen;
            }
            set
            {
                enmLoginScreen = value;
            }
        }
        #endregion

        #region ZIP FILE PASSWORD
        public static string J_pZipFilePassword
        {
            get
            {
                return ZipFilePassword;
            }
            set
            {
                ZipFilePassword = value;
            }
        }
        #endregion

        #region BRANCH
        public static J_Branch J_pBranch
        {
            get
            {
                return enmBranch;
            }
            set
            {
                enmBranch = value;
            }
        }
        #endregion

        #region CENTRAL DATABASE NAME
        public static string J_pCentralDatabaseName
        {
            get
            {
                return CentralDatabaseName;
            }
            set
            {
                CentralDatabaseName = value;
            }
        }
        #endregion

        #region POOLING DATABASE NAME
        public static string J_pPoolingDatabaseName
        {
            get
            {
                return PoolingDatabaseName;
            }
            set
            {
                PoolingDatabaseName = value;
            }
        }
        #endregion


        #endregion

    }
    #endregion

    
}
