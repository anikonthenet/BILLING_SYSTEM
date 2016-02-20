
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: DMLService
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Class & Methods
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Reflection;

using System.IO;

using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace BillingSystem.Classes
{
    public class DMLService : IDisposable
    {

        #region PRIVATE OBJECTS DECLERATION

        private IDbConnection J_IdbConn;
        private ADODB.Connection J_ADODBConn;
        
        private IDbCommand J_IdbCommand;
        private IDbTransaction J_IdbTran;
        private DataSet J_DataSet;
        private ADODB.Recordset J_ADODBRecordset;

        private CommonService commonservice;
        private DateService dtservice;

        #endregion

        #region PRIVATE VARIABLES DECLERATION

        private J_ApplicationType enmApplicationType;
        private J_DatabaseType enmDatabaseType;
        private J_ConnectionProviderType enmConnectionProviderType;
        private J_OSType enmOSType;

        private string J_strServerName;
        private string J_strDataBase;
        private string J_strUserName;
        private string J_strPassword;
        
        private string J_strConnectionString;
        private string J_strADODBConnectionString;
        
        private bool J_blnDisposed;
        private bool J_blnConnected;

        private string strSQL;

        #endregion
        
        #region CONSTRUCTOR

        #region DMLService [1]
        public DMLService()
        {
            this.J_IdbConn = null;
            this.J_ADODBConn = null;
            
            this.J_IdbCommand = null;
            this.J_IdbTran = null;
            this.J_DataSet = null;
            this.J_ADODBRecordset = null;

            this.commonservice = new CommonService();
            this.dtservice = new DateService();

            this.enmApplicationType        = J_Var.J_pApplicationType;
            this.enmDatabaseType           = J_Var.J_pDatabaseType;
            this.enmConnectionProviderType = J_Var.J_pConnectionProviderType;
            this.enmOSType                 = J_Var.J_pOSType;

            this.J_strServerName = string.Empty;
            this.J_strDataBase = string.Empty;
            this.J_strUserName = string.Empty;
            this.J_strPassword = string.Empty;
            
            this.J_strConnectionString = string.Empty;
            this.J_strADODBConnectionString = string.Empty;

            this.J_blnDisposed = false;
            this.J_blnConnected = false;

            this.strSQL = string.Empty;

            this.J_ConfigureConnction(this.enmConnectionProviderType);
        }
        #endregion

        #region DMLService [2]
        public DMLService(J_DatabaseType DatabaseType)
        {
            this.J_IdbConn = null;
            this.J_ADODBConn = null;
            
            this.J_IdbCommand = null;
            this.J_IdbTran = null;
            this.J_DataSet = null;
            this.J_ADODBRecordset = null;

            this.commonservice = new CommonService();
            this.dtservice = new DateService();

            this.enmApplicationType        = J_Var.J_pApplicationType;
            this.enmDatabaseType           = DatabaseType;
            this.enmConnectionProviderType = J_Var.J_pConnectionProviderType;
            this.enmOSType                 = J_Var.J_pOSType;

            this.J_strServerName = string.Empty;
            this.J_strDataBase = string.Empty;
            this.J_strUserName = string.Empty;
            this.J_strPassword = string.Empty;

            this.J_strConnectionString = string.Empty;
            this.J_strADODBConnectionString = string.Empty;

            this.J_blnDisposed = false;
            this.J_blnConnected = false;

            this.strSQL = string.Empty;

            this.J_ConfigureConnction(this.enmConnectionProviderType);
        }
        #endregion

        #region DMLService [3]
        public DMLService(J_ConnectionProviderType ConnectionProviderType)
        {
            this.J_IdbConn = null;
            this.J_ADODBConn = null;

            this.J_IdbCommand = null;
            this.J_IdbTran = null;
            this.J_DataSet = null;
            this.J_ADODBRecordset = null;

            this.commonservice = new CommonService();
            this.dtservice = new DateService();

            this.enmApplicationType        = J_Var.J_pApplicationType;
            this.enmDatabaseType           = J_Var.J_pDatabaseType;
            this.enmConnectionProviderType = ConnectionProviderType;
            this.enmOSType                 = J_Var.J_pOSType;

            this.J_strServerName = string.Empty;
            this.J_strDataBase = string.Empty;
            this.J_strUserName = string.Empty;
            this.J_strPassword = string.Empty;

            this.J_strConnectionString = string.Empty;
            this.J_strADODBConnectionString = string.Empty;

            this.J_blnDisposed = false;
            this.J_blnConnected = false;

            this.strSQL = string.Empty;

            this.J_ConfigureConnction(this.enmConnectionProviderType);
        }
        #endregion

        #region DMLService [4]
        public DMLService(J_DatabaseType DatabaseType, J_ConnectionProviderType ConnectionProviderType)
        {
            this.J_IdbConn = null;
            this.J_ADODBConn = null;

            this.J_IdbCommand = null;
            this.J_IdbTran = null;
            this.J_DataSet = null;
            this.J_ADODBRecordset = null;

            this.commonservice = new CommonService();
            this.dtservice = new DateService();

            this.enmApplicationType        = J_Var.J_pApplicationType;
            this.enmDatabaseType           = DatabaseType;
            this.enmConnectionProviderType = ConnectionProviderType;
            this.enmOSType                 = J_Var.J_pOSType;

            this.J_strServerName = string.Empty;
            this.J_strDataBase = string.Empty;
            this.J_strUserName = string.Empty;
            this.J_strPassword = string.Empty;

            this.J_strConnectionString = string.Empty;
            this.J_strADODBConnectionString = string.Empty;

            this.J_blnDisposed = false;
            this.J_blnConnected = false;

            this.strSQL = string.Empty;

            this.J_ConfigureConnction(this.enmConnectionProviderType);
        }
        #endregion

        #endregion
        
        #region USER DEFINES METHODS

        #region PRIVATE METHODS

        #region CONFIGURE CONNECTION
        private void J_ConfigureConnction(J_ConnectionProviderType dbType)
        {
            switch (dbType)
            {
                case J_ConnectionProviderType.Sql:
                    this.J_IdbConn = new SqlConnection();
                    break;

                case J_ConnectionProviderType.Oracle:
                    this.J_IdbConn = new OracleConnection();
                    break;

                case J_ConnectionProviderType.OleDb:
                    this.J_IdbConn = new OleDbConnection();
                    break;

                case J_ConnectionProviderType.Odbc:
                    this.J_IdbConn = new OleDbConnection();
                    break;

                default:
                    this.J_IdbConn = new SqlConnection();
                    break;
            }
            this.J_ADODBConn = new ADODB.Connection();
        }
        #endregion

        #region OPEN CONNECTION [ OVERLOADED METHOD ]

        #region J_OpenConnection [1]
        private bool J_OpenConnection()
        {
            if (this.enmApplicationType == J_ApplicationType.StandAlone_Network)
            {
                if ((this.J_strConnectionString == null) || (this.J_strConnectionString.Trim().Length == 0))
                    this.J_strConnectionString = string.Empty;

                this.J_strConnectionString = J_GetConnectionString(this.enmApplicationType);
                return this.J_OpenConnection(this.J_strServerName, this.J_strUserName, this.J_strPassword, this.J_strDataBase);
            }
            else if (this.enmApplicationType == J_ApplicationType.StandAlone_SingleMachine)
            {
                return this.J_OpenConnection(this.enmApplicationType);
            }
            else if (this.enmApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
            {
                return this.J_OpenConnection(this.enmApplicationType);
            }
            return false;
        }
        #endregion

        #region J_OpenConnection [2]
        private bool J_OpenConnection(J_ApplicationType ApplicationType)
        {
            try
            {
                if (ApplicationType == J_ApplicationType.StandAlone_SingleMachine && this.enmDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                    {
                        this.J_strConnectionString = "Provider=" + this.J_pProvider + ";Data Source = " + Application.StartupPath + "\\" + J_Var.J_pMsAccessDatabaseName + ";" +
                                "Persist Security Info=False;Jet OLEDB:Database Password=" + J_Var.J_pMsAccessDatabasePassword + "";

                        //this.J_strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = \\Pds1\tdsmanfy2012-13\tdsman.mdb;Persist Security Info=False;Jet OLEDB:Database Password=";
                    }
                }
                else if (ApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser && this.enmDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                    {
                        if (this.J_DataSet != null) this.J_DataSet.Clear();
                        this.J_DataSet = this.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                        if (this.J_DataSet == null)
                        {
                            this.J_CloseConnection();
                            this.J_blnConnected = false;
                            return false;
                        }
                        this.J_strDataBase = commonservice.J_Decode(this.J_DataSet.Tables[0].Rows[0][commonservice.J_Encode("DATABASENAME")].ToString());
                        this.J_strConnectionString = "Provider=" + this.J_pProvider + ";Data Source = " + this.J_strDataBase + "\\" + J_Var.J_pMsAccessDatabaseName + ";" +
                            "Persist Security Info=False;Jet OLEDB:Database Password=" + J_Var.J_pMsAccessDatabasePassword + "";
                    }
                }

                this.J_CloseConnection();
                this.J_IdbConn.ConnectionString = this.J_strConnectionString;

                this.J_IdbConn.Open();
                this.J_ADODBConn.Open(this.J_strConnectionString, null, null, 0);

                this.J_IdbCommand = this.J_IdbConn.CreateCommand();
                this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;

                this.J_ADODBRecordset = new ADODB.Recordset();

                this.J_blnConnected = true;

            }
            catch
            {
                this.J_blnConnected = false;
            }
            return this.J_blnConnected;
        }
        #endregion

        #region J_OpenConnection [3]
        private bool J_OpenConnection(string ServerName, string UserName, string Password)
        {
            return this.J_OpenConnection(this.J_strServerName, this.J_strUserName, this.J_strPassword, null);
        }
        #endregion

        #region J_OpenConnection [4]
        private bool J_OpenConnection(string ServerName, string UserName, string Password, string DatabaseName)
        {
            try
            {
                this.J_strServerName = ServerName;
                this.J_strUserName = UserName;
                this.J_strPassword = Password;
                this.J_strDataBase = DatabaseName;

                if (this.enmDatabaseType == J_DatabaseType.SqlServer)
                {
                    if ((this.J_strConnectionString != null) && (this.J_strConnectionString.Trim().Length != 0))
                        this.J_strConnectionString = string.Empty;

                    if ((this.J_strDataBase == null) || (this.J_strDataBase.Trim().Length == 0))
                        this.J_strConnectionString = "SERVER   = " + this.J_strServerName + ";" +
                                                     "UID      = " + this.J_strUserName + ";" +
                                                     "PWD      = " + this.J_strPassword + ";";
                    else
                    {
                        this.J_strConnectionString = "SERVER   = " + this.J_strServerName + ";" +
                                                     "DATABASE = " + this.J_strDataBase + ";" +
                                                     "UID      = " + this.J_strUserName + ";" +
                                                     "PWD      = " + this.J_strPassword + ";" +
                                                     "Pooling  = false";
                    }

                    if (this.enmConnectionProviderType == J_ConnectionProviderType.Sql)
                        this.J_strADODBConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;Initial Catalog=" + this.J_strDataBase + ";Data Source=" + this.J_strServerName + ";Pooling=false";
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                        this.J_strADODBConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;Initial Catalog=" + this.J_strDataBase + ";Data Source=" + this.J_strServerName + ";Pooling=false";
                }
                else if (this.enmDatabaseType == J_DatabaseType.Oracle)
                {
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.Oracle)
                        this.J_strADODBConnectionString = "Provider=msdaora;Data Source=" + this.J_strDataBase + ";User Id=" + this.J_strUserName + ";Password=" + this.J_strPassword + "";
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                        this.J_strADODBConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;Initial Catalog=" + this.J_strDataBase + ";Data Source=" + this.J_strServerName + "";
                }
                else if (this.enmDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                    {
                        this.J_strConnectionString = "Provider=" + this.J_pProvider + ";Data Source = \\\\" + this.J_strServerName + "\\" + this.J_strDataBase + "\\" + J_Var.J_pMsAccessDatabaseName + ";" +
                            "Persist Security Info=False;Jet OLEDB:Database Password=" + this.J_strPassword + "";

                        //this.J_strConnectionString = @"Provider=Microsoft.Jet.OLEDB.12.0;Data Source = D:\Desktop\billing.accdb;Persist Security Info=False;Jet OLEDB:Database Password=";
                    }
                }

                this.J_CloseConnection();
                this.J_IdbConn.ConnectionString = this.J_strConnectionString;

                this.J_IdbConn.Open();

                if (this.enmDatabaseType == J_DatabaseType.MsAccess)
                    this.J_ADODBConn.Open(this.J_strConnectionString, null, null, 0);
                else
                {
                    if ((this.J_strDataBase != null) && (this.J_strDataBase.Trim().Length > 0))
                        this.J_ADODBConn.Open(this.J_strADODBConnectionString, this.J_strUserName, this.J_strPassword, 0);
                }

                this.J_IdbCommand = this.J_IdbConn.CreateCommand();
                this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;

                this.J_ADODBRecordset = new ADODB.Recordset();
                
                this.J_blnConnected = true;

            }
            catch(Exception err)
            {
                this.J_blnConnected = false;
            }
            return this.J_blnConnected;
        }
        #endregion

        #region J_OpenConnection [5]
        private bool J_OpenConnection(string MSAccessDatabaseNameWithPath)
        {
            return this.J_OpenConnection(MSAccessDatabaseNameWithPath, null);
        }
        #endregion

        #region J_OpenConnection [6]
        private bool J_OpenConnection(string MSAccessDatabaseNameWithPath, string MSAccessDatabasePassword)
        {
            try
            {
                if ((MSAccessDatabaseNameWithPath == null) || (MSAccessDatabaseNameWithPath.Trim().Length == 0))
                    MSAccessDatabaseNameWithPath = ""; ;
                if ((MSAccessDatabasePassword == null) || (MSAccessDatabasePassword.Trim().Length == 0))
                    MSAccessDatabasePassword = "";

                this.J_strConnectionString = "Provider=" + this.J_pProvider + ";Data Source = " + MSAccessDatabaseNameWithPath + ";" +
                    "Persist Security Info=False;Jet OLEDB:DataBase Password=" + MSAccessDatabasePassword + "";

                this.J_CloseConnection();
                this.J_IdbConn.ConnectionString = this.J_strConnectionString;

                this.J_IdbConn.Open();
                this.J_ADODBConn.Open(this.J_strConnectionString, null, null, 0);

                this.J_IdbCommand = this.J_IdbConn.CreateCommand();
                this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;

                this.J_ADODBRecordset = new ADODB.Recordset();

                this.J_blnConnected = true;

            }
            catch
            {
                this.J_blnConnected = false;
            }
            return this.J_blnConnected;
        }

        #endregion

        #endregion

        #region CLOSE CONNECTION
        private void J_CloseConnection()
        {
            if (this.J_blnConnected)
            {
                if (this.J_IdbTran != null)
                    this.J_Rollback();

                if (this.J_IdbCommand != null)
                {
                    this.J_IdbCommand.Dispose();
                    this.J_IdbCommand = null;
                }

                if (this.J_IdbConn != null)
                {
                    this.J_IdbConn.Close();
                    this.J_IdbConn.Dispose();
                }

                if (this.J_ADODBRecordset != null)
                {
                    this.J_ADODBRecordset = null;
                }

                if (this.J_ADODBConn.State == (int)ConnectionState.Open)
                {
                    this.J_ADODBConn.Close();
                }

                if (this.commonservice != null)
                {
                    this.commonservice.Dispose();
                }
            }
            this.J_blnConnected = false;
        }
        #endregion

        #region DISPOSE
        private void Dispose(bool Disposing)
        {
            if (!this.J_blnDisposed && Disposing)
                this.J_CloseConnection();
            this.J_blnDisposed = true;
        }
        #endregion


        #region EXPORT FILE ENGINE

        #region ExportFileEngine [1]
        private bool ExportFileEngine(string ExportFolderPath, J_ExportFileType FileType, string SourceTableName, string Delimiter, string TextQualifier)
        {
            int intNoOfRow = 0;
            return this.ExportFileEngine(ExportFolderPath, FileType, SourceTableName, "*", null, Delimiter, TextQualifier, false, out intNoOfRow);
        }
        #endregion

        #region ExportFileEngine [2]
        private bool ExportFileEngine(string ExportFolderPath, J_ExportFileType FileType, string SourceTableName, string ExportColumns, string Delimiter, string TextQualifier)
        {
            int intNoOfRow = 0;
            return this.ExportFileEngine(ExportFolderPath, FileType, SourceTableName, ExportColumns, null, Delimiter, TextQualifier, false, out intNoOfRow);
        }
        #endregion

        #region ExportFileEngine [3]
        private bool ExportFileEngine(string ExportFolderPath, J_ExportFileType FileType, string SourceTableName, string ExportColumns, string Criteria, string Delimiter, string TextQualifier)
        {
            int intNoOfRow = 0;
            return this.ExportFileEngine(ExportFolderPath, FileType, SourceTableName, ExportColumns, Criteria, Delimiter, TextQualifier, false, out intNoOfRow);
        }
        #endregion

        #region ExportFileEngine [4]
        private bool ExportFileEngine(string ExportFolderPath, J_ExportFileType FileType, string SourceTableName, string ExportColumns, string Criteria, string Delimiter, string TextQualifier, bool Header)
        {
            int intNoOfRow = 0;
            return this.ExportFileEngine(ExportFolderPath, FileType, SourceTableName, ExportColumns, Criteria, Delimiter, TextQualifier, Header, out intNoOfRow);
        }
        #endregion

        #region ExportFileEngine [5]
        private bool ExportFileEngine(string ExportFolderPath, J_ExportFileType ExportFileType, string SourceTableName, string ExportColumns, string Criteria, string Delimiter, string TextQualifier, bool Header, out int NoOfRow)
        {
            NoOfRow = 0;
            string strFileName   = "";
            
            try
            {
                switch (this.J_pDatabaseType)
                {
                    case J_DatabaseType.MsAccess:
                        switch (ExportFileType)
                        {
                            case J_ExportFileType.CSVFile:
                                strFileName = SourceTableName + ".csv";
                                break;

                            case J_ExportFileType.TextFile:
                                strFileName = SourceTableName + ".txt";
                                this.J_CreateSchemaFile(ExportFolderPath, SourceTableName, ExportColumns, Delimiter, TextQualifier, "MM/dd/yyyy", Header);

                                strSQL = "SELECT " + ExportColumns + " INTO [Text;";
                                if (Header == true)
                                {
                                    strSQL += "HDR=Yes;FMT=Delimited" + Delimiter + ";";
                                }
                                else
                                {
                                    strSQL += "HDR=No;FMT=Delimited" + Delimiter + ";";
                                }

                                if (Criteria == null)
                                {
                                    strSQL += "DATABASE=" + ExportFolderPath + "].[" + strFileName + "] FROM " + SourceTableName;
                                }
                                else if (Criteria != null)
                                {
                                    strSQL += "DATABASE=" + ExportFolderPath + "].[" + strFileName + "] FROM " + SourceTableName + " WHERE " + Criteria;
                                }

                                break;

                            case J_ExportFileType.ExcelFile:
                                strFileName = SourceTableName + ".xls";
                                strSQL = "SELECT " + ExportColumns + " INTO [Excel 8.0;";
                                if (Header == true)
                                {
                                    strSQL += "HDR=Yes;";
                                }
                                else
                                {
                                    strSQL += "HDR=No;";
                                }

                                if (Criteria == null)
                                {
                                    strSQL += "DATABASE=" + ExportFolderPath + "].[" + strFileName + "] FROM " + SourceTableName;
                                }
                                else if (Criteria != null)
                                {
                                    strSQL += "DATABASE=" + ExportFolderPath + "].[" + strFileName + "] FROM " + SourceTableName + " WHERE " + Criteria;
                                }

                                break;
                        }
                        break;

                    case J_DatabaseType.SqlServer:
                        switch (ExportFileType)
                        {
                            case J_ExportFileType.TextFile:
                                strFileName = SourceTableName + ".txt";
                                this.J_CreateSchemaFile(ExportFolderPath, SourceTableName, ExportColumns, Delimiter, TextQualifier, "MM/dd/yyyy", Header);

                                if (ExportColumns == "" || ExportColumns == null || ExportColumns == "*")
                                    ExportColumns = this.J_GetExportSourceTableColumns(SourceTableName);
                                
                                string strcon = this.J_GetConnectionString(J_ApplicationType.StandAlone_Network);
                                if (Criteria == null)
                                    strSQL = "exec master..xp_cmdshell 'bcp  \" SELECT " + ExportColumns + " FROM  " + this.J_pDatabaseName + ".dbo." + SourceTableName + "\" queryout  " + "\"" + ExportFolderPath + "\\" + strFileName + "\" -c -t" + "\"" + Delimiter + "\" -U " + this.J_strUserName + " -P " + this.J_strPassword + "'";
                                else
                                    strSQL = "exec master..xp_cmdshell 'bcp  \" SELECT " + ExportColumns + " FROM  " + this.J_pDatabaseName + ".dbo." + SourceTableName + " WHERE " + Criteria + "\" queryout  " + "\"" + ExportFolderPath + "\\" + strFileName + "\" -c -t" + "\"" + Delimiter + "\" -U " + this.J_strUserName + " -P " + this.J_strPassword + "'";
                                
                                break;
                        }
                        break;

                }

                if (this.J_ExecSql(strSQL, out NoOfRow) == false) return false;
                return true;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region RESTORE FILE ENGINE

        #region RestoreFileEngine [1]
        private bool RestoreFileEngine(string SourceFilePath, string TableName)
        {
            int intNoOfRow = 0;
            return this.RestoreFileEngine(SourceFilePath, J_RestoreFileType.TextFile, TableName, null, null, null, J_TextSeparator.Pipe, out intNoOfRow);
        }
        #endregion

        #region RestoreFileEngine [2]
        private bool RestoreFileEngine(string SourceFilePath, string TableName, string SourceTableColumns, string DestinationTableColumns)
        {
            int intNoOfRow = 0;
            return this.RestoreFileEngine(SourceFilePath, J_RestoreFileType.TextFile, TableName, SourceTableColumns, DestinationTableColumns, null, J_TextSeparator.Pipe, out intNoOfRow);
        }
        #endregion

        #region RestoreFileEngine [3]
        private bool RestoreFileEngine(string SourceFilePath, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria)
        {
            int intNoOfRow = 0;
            return this.RestoreFileEngine(SourceFilePath, J_RestoreFileType.TextFile, TableName, SourceTableColumns, DestinationTableColumns, Criteria, J_TextSeparator.Pipe, out intNoOfRow);
        }
        #endregion

        #region RestoreFileEngine [4]
        private bool RestoreFileEngine(string SourceFilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria)
        {
            int intNoOfRow = 0;
            return this.RestoreFileEngine(SourceFilePath, FileType, TableName, SourceTableColumns, DestinationTableColumns, Criteria, J_TextSeparator.Pipe, out intNoOfRow);
        }
        #endregion

        #region RestoreFileEngine [5]
        private bool RestoreFileEngine(string SourceFilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter)
        {
            int intNoOfRow = 0;
            return this.RestoreFileEngine(SourceFilePath, FileType, TableName, SourceTableColumns, DestinationTableColumns, Criteria, Delimiter, out intNoOfRow);
        }
        #endregion

        #region RestoreFileEngine [6]
        private bool RestoreFileEngine(string SourceFilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter, out int NoOfRow)
        {
            NoOfRow = 0;
            string strFileName                = commonservice.J_GetFileName(SourceFilePath);
            string strFileNameWithoutExtesion = commonservice.J_Left(strFileName, strFileName.IndexOf("."));
            string strFolderPath              = commonservice.J_GetDirectoryName(SourceFilePath);

            try
            {
                switch (J_pDatabaseType)
                {
                    case J_DatabaseType.MsAccess:

                        strSQL = "SELECT * INTO " + TableName + " FROM " +
                                @"[Text; HDR=NO;FMT=Delimited(" + Delimiter + ") DATABASE=" + strFolderPath + "].[" + strFileName + "]";
                        
                        break;

                    case J_DatabaseType.SqlServer:
                        
                        if (DestinationTableColumns == null || DestinationTableColumns == "*")
                            strSQL = "INSERT INTO " + TableName + " ";
                        else
                            strSQL = "INSERT INTO " + TableName + "(" + DestinationTableColumns + ") ";
                        
                        if (SourceTableColumns == null || SourceTableColumns == "*")
                            strSQL += "SELECT * FROM ";
                        else
                            strSQL += "SELECT " + SourceTableColumns + " FROM ";
                        
                        if(FileType == J_RestoreFileType.TextFile)
                            strSQL += "OPENROWSET('Microsoft.Jet.OLEDB.4.0','text;HDR=NO;Database=" + strFolderPath + "', " + strFileNameWithoutExtesion + "#txt) ";

                        if(Criteria != null)
                            strSQL += "WHERE " + Criteria;

                        break;

                    case J_DatabaseType.Excel_Xls:
                        break;
                    case J_DatabaseType.Excel_Xlsx:
                        break;
                }

                if (this.J_ExecSql(strSQL, out NoOfRow) == false) return false;
                return true;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion



        #endregion

        #region PUBLIC METHODS

        #region GET CONNECTION STRING
        public string J_GetConnectionString(J_ApplicationType ApplicationType)
        {
            string setting = string.Empty;
            this.J_DataSet = new DataSet();

            switch (ApplicationType)
            {
                case J_ApplicationType.StandAlone_Network:
                    this.J_DataSet = this.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);

                    this.J_strServerName = this.commonservice.J_Decode(this.J_DataSet.Tables[0].Rows[0][this.commonservice.J_Encode("SERVERNAME")].ToString());
                    this.J_strDataBase   = this.commonservice.J_Decode(this.J_DataSet.Tables[0].Rows[0][this.commonservice.J_Encode("DATABASENAME")].ToString());
                    this.J_strUserName   = this.commonservice.J_Decode(this.J_DataSet.Tables[0].Rows[0][this.commonservice.J_Encode("USERNAME")].ToString());
                    this.J_strPassword   = this.commonservice.J_Decode(this.J_DataSet.Tables[0].Rows[0][this.commonservice.J_Encode("PASSWORD")].ToString());

                    if (this.enmConnectionProviderType == J_ConnectionProviderType.Sql)
                        setting = "SERVER   = " + this.J_strServerName + ";" +
                                  "DATABASE = " + this.J_strDataBase + ";" +
                                  "UID      = " + this.J_strUserName + ";" +
                                  "PWD      = " + this.J_strPassword + ";" +
                                  "Pooling  = false;";

                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                        setting = "PROVIDER = SQLOLEDB;" +
                                  "SERVER   = " + this.J_strServerName + ";" +
                                  "DATABASE = " + this.J_strDataBase + ";" +
                                  "USER ID  = " + this.J_strUserName + ";" +
                                  "PASSWORD = " + this.J_strPassword + ";" +
                                  "Pooling  = false;";

                    if (this.enmConnectionProviderType == J_ConnectionProviderType.Oracle)
                        setting = "PROVIDER    = ORAOLEDB.ORACLE;" +
                                  "SERVER      = " + this.J_strServerName + ";" +
                                  "DATA SOURCE = " + this.J_strDataBase + ";" +
                                  "USER ID     = " + this.J_strUserName + ";" +
                                  "PASSWORD    = " + this.J_strPassword + "";
                    break;
                case J_ApplicationType.Web:
                    setting = "Web";
                    break;
            }
            this.J_DataSet = null;
            return setting;
        }
        #endregion

        #region BEGIN TRANSACTION [OVERLOADED METHODS]

        #region J_BeginTransaction [1]
        public void J_BeginTransaction()
        {
            this.J_ValidateConnection();
            this.J_IdbTran = this.J_IdbConn.BeginTransaction();
            this.J_IdbCommand.Transaction = this.J_IdbTran;
        }
        #endregion

        #region J_BeginTransaction [2]
        public void J_BeginTransaction(ref IDbConnection connection, ref IDbCommand command, ref IDbTransaction transaction)
        {
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
        }
        #endregion

        #endregion

        #region ROLLBACK [OVERLOADED METHODS]

        #region J_Rollback [1]
        public void J_Rollback()
        {
            if (this.J_IdbTran != null)
            {
                this.J_IdbTran.Rollback();
                this.J_IdbTran = null;
            }
        }
        #endregion

        #region J_Rollback [2]
        public void J_Rollback(IDbTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
        }
        #endregion

        #endregion

        #region COMMIT [OVERLOADED METHODS]

        #region J_Commit [1]
        public void J_Commit()
        {
            this.J_IdbTran.Commit();
            this.J_IdbTran = null;
        }
        #endregion

        #region J_Commit [2]
        public void J_Commit(ref IDbTransaction transaction)
        {
            transaction.Commit();
            transaction = null;
        }
        #endregion

        #endregion

        #region VALIDATE CONNECTION [ OVERLOADED METHOD ]

        #region J_ValidateConnection [1]
        public bool J_ValidateConnection()
        {
            return (this.J_blnConnected || this.J_OpenConnection());
        }
        #endregion

        #region J_ValidateConnection [2]
        public bool J_ValidateConnection(string ServerName, string UserName, string Password)
        {
            return this.J_ValidateConnection(ServerName, UserName, Password, null);
        }
        #endregion

        #region J_ValidateConnection [3]
        public bool J_ValidateConnection(string ServerName, string UserName, string Password, string DatabaseName)
        {
            return (this.J_blnConnected || this.J_OpenConnection(ServerName, UserName, Password, DatabaseName));
        }
        #endregion

        #region J_ValidateConnection [4]
        public bool J_ValidateConnection(string MSAccessDatabaseNameWithPath)
        {
            return (this.J_blnConnected || this.J_OpenConnection(MSAccessDatabaseNameWithPath));
        }
        #endregion

        #region J_ValidateConnection [5]
        public bool J_ValidateConnection(string MSAccessDatabaseNameWithPath, string MSAccessDatabasePassword)
        {
            return (this.J_blnConnected || this.J_OpenConnection(MSAccessDatabaseNameWithPath, MSAccessDatabasePassword));
        }
        #endregion
        
        #endregion

        #region EXEC SQL [ OVERLOADED METHOD ]

        #region EXEC SQL [1]
        public bool J_ExecSql(string SqlText)
        {
            int RowsAffected;
            return this.J_ExecSql(null, SqlText, out RowsAffected, J_SQLType.DML);
        }
        #endregion

        #region EXEC SQL [2]
        public bool J_ExecSql(IDbCommand command, string SqlText)
        {
            int RowsAffected;
            return this.J_ExecSql(command, SqlText, out RowsAffected, J_SQLType.DML);
        }
        #endregion

        #region EXEC SQL [3]
        public bool J_ExecSql(string SqlText, out int RowsAffected)
        {
            return this.J_ExecSql(null, SqlText, out RowsAffected, J_SQLType.DML);
        }
        #endregion

        #region EXEC SQL [4]
        public bool J_ExecSql(IDbCommand command, string SqlText, out int RowsAffected)
        {
            return this.J_ExecSql(command, SqlText, out RowsAffected, J_SQLType.DML);
        }
        #endregion

        #region EXEC SQL [5]
        public bool J_ExecSql(string SqlText, J_SQLType SQLType)
        {
            int RowsAffected;
            return this.J_ExecSql(null, SqlText, out RowsAffected, SQLType);
        }
        #endregion

        #region EXEC SQL [6]
        public bool J_ExecSql(IDbCommand command, string SqlText, J_SQLType SQLType)
        {
            int RowsAffected;
            return this.J_ExecSql(command, SqlText, out RowsAffected, SQLType);
        }
        #endregion

        #region EXEC SQL [7]
        public bool J_ExecSql(string SqlText, out int RowsAffected, J_SQLType SQLType)
        {
            return this.J_ExecSql(null, SqlText, out RowsAffected, SQLType);
        }
        #endregion
        
        #region EXEC SQL [8]
        public bool J_ExecSql(IDbCommand command, string SqlText, out int RowsAffected, J_SQLType SQLType)
        {
            RowsAffected = 0;
            try
            {
                if (command == null)
                {
                    if (this.J_ValidateConnection() == false)
                        return false;

                    this.J_IdbCommand.CommandText = SqlText;
                    this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;

                    if (SQLType == J_SQLType.DDL)
                    {
                        this.J_IdbCommand.ExecuteNonQuery();
                        RowsAffected = 1;
                    }
                    else if (SQLType == J_SQLType.DML)
                        RowsAffected = this.J_IdbCommand.ExecuteNonQuery();

                    if (RowsAffected >= 0)
                        return true;
                    else
                        return false;
                }
                else if (command != null)
                {
                    command.CommandText = SqlText;
                    command.CommandTimeout = J_Var.J_pCommandTimeout;

                    if (SQLType == J_SQLType.DDL)
                    {
                        command.ExecuteNonQuery();
                        RowsAffected = 1;
                    }
                    else if (SQLType == J_SQLType.DML)
                        RowsAffected = command.ExecuteNonQuery();

                    if (RowsAffected >= 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch(Exception err)
            {
                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return false;
            }
        }
        #endregion


        #endregion

        #region EXEC SQL RETURN ADAPTER [ OVERLOADED METHOD ]

        #region J_ExecSqlReturnAdapter [1]
        public IDataAdapter J_ExecSqlReturnAdapter(string SqlText)
        {
            return this.J_ExecSqlReturnAdapter(null, SqlText, this.enmConnectionProviderType);
        }
        #endregion

        #region J_ExecSqlReturnAdapter [2]
        public IDataAdapter J_ExecSqlReturnAdapter(IDbCommand command, string SqlText)
        {
            return this.J_ExecSqlReturnAdapter(command, SqlText, this.enmConnectionProviderType);
        }
        #endregion

        #region J_ExecSqlReturnAdapter [3]
        public IDataAdapter J_ExecSqlReturnAdapter(string SqlText, J_ConnectionProviderType ConnectionProviderType)
        {
            return this.J_ExecSqlReturnAdapter(null, SqlText, this.enmConnectionProviderType);
        }
        #endregion

        #region J_ExecSqlReturnAdapter [4]
        public IDataAdapter J_ExecSqlReturnAdapter(IDbCommand command, string SqlText, J_ConnectionProviderType ConnectionProviderType)
        {
            if (command == null)
            {
                this.J_IdbCommand.CommandText = SqlText;
                this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;
                return this.J_ExecSqlReturnAdapter(this.J_IdbCommand, ConnectionProviderType);
            }
            else if (command != null)
            {
                command.CommandText = SqlText;
                command.CommandTimeout = J_Var.J_pCommandTimeout;
                return this.J_ExecSqlReturnAdapter(command, ConnectionProviderType);
            }
            return null;
        }
        #endregion

        #region J_ExecSqlReturnAdapter [5]
        public IDataAdapter J_ExecSqlReturnAdapter(IDbCommand command)
        {
            return this.J_ExecSqlReturnAdapter(command, this.enmConnectionProviderType);
        }
        #endregion

        #region J_ExecSqlReturnAdapter [6]
        public IDataAdapter J_ExecSqlReturnAdapter(IDbCommand command, J_ConnectionProviderType ConnectionProviderType)
        {
            try
            {
                if (this.J_ValidateConnection() == false)
                    return null;

                switch (ConnectionProviderType)
                {
                    case J_ConnectionProviderType.Sql:
                        return new SqlDataAdapter((SqlCommand)command);
                    case J_ConnectionProviderType.Oracle:
                        return new OracleDataAdapter((OracleCommand)command);
                    case J_ConnectionProviderType.OleDb:
                        return new OleDbDataAdapter((OleDbCommand)command);
                    case J_ConnectionProviderType.Odbc:
                        return new OleDbDataAdapter((OleDbCommand)command);
                }
                return new SqlDataAdapter((SqlCommand)command);
            }
            catch
            {
                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return null;
            }
        }
        #endregion
        
        #endregion

        #region EXEC SQL RETURN DATASET [ OVERLOADED METHOD ]

        #region EXEC SQL RETURN DATASET [1]
        public DataSet J_ExecSqlReturnDataSet(string SqlText)
        {
            return this.J_ExecSqlReturnDataSet(null, SqlText, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATASET [2]
        public DataSet J_ExecSqlReturnDataSet(IDbCommand command, string SqlText)
        {
            return this.J_ExecSqlReturnDataSet(command, SqlText, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATASET [3]
        public DataSet J_ExecSqlReturnDataSet(string SqlText, string srcTableName)
        {
            return this.J_ExecSqlReturnDataSet(null, SqlText, srcTableName);
        }
        #endregion

        #region EXEC SQL RETURN DATASET [4]
        public DataSet J_ExecSqlReturnDataSet(IDbCommand command, string SqlText, string srcTableName)
        {
            try
            {
                IDataAdapter adapter = null;

                if (command == null)
                {
                    if (this.J_ValidateConnection() == false)
                        return null;

                    adapter = this.J_ExecSqlReturnAdapter(SqlText);
                }
                else if (command != null)
                {
                    adapter = this.J_ExecSqlReturnAdapter(command, SqlText);
                }

                if (adapter == null)
                    return null;
                
                this.J_DataSet = new DataSet();

                switch (this.enmConnectionProviderType)
                {
                    case J_ConnectionProviderType.Sql:
                        ((SqlDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                        return this.J_DataSet;

                    case J_ConnectionProviderType.OleDb:
                        ((OleDbDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                        return this.J_DataSet;

                    case J_ConnectionProviderType.Odbc:
                        ((OleDbDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                        return this.J_DataSet;

                    case J_ConnectionProviderType.Oracle:
                        ((OracleDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                        return this.J_DataSet;

                    default:
                        ((SqlDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                        return this.J_DataSet;
                }
                //((SqlDataAdapter)adapter).Fill(this.J_DataSet, srcTableName);
                //return this.J_DataSet;
            }
            catch(Exception ERR)
            {
                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return null;
            }
        }
        #endregion

        #endregion
        
        #region EXEC SQL RETURN DATATABLE [ OVERLOADED METHOD ]

        #region EXEC SQL RETURN DATATABLE [1]
        public DataTable J_ExecSqlReturnDataTable(string SqlText)
        {
            return this.J_ExecSqlReturnDataTable(SqlText, 0, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATATABLE [2]
        public DataTable J_ExecSqlReturnDataTable(IDbCommand command, string SqlText)
        {
            return this.J_ExecSqlReturnDataTable(command, SqlText, 0, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATATABLE [3]
        public DataTable J_ExecSqlReturnDataTable(string SqlText, int Index)
        {
            return this.J_ExecSqlReturnDataTable(SqlText, Index, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATATABLE [4]
        public DataTable J_ExecSqlReturnDataTable(IDbCommand command, string SqlText, int Index)
        {
            return this.J_ExecSqlReturnDataTable(command, SqlText, Index, "Table");
        }
        #endregion

        #region EXEC SQL RETURN DATATABLE [5]
        public DataTable J_ExecSqlReturnDataTable(string SqlText, int Index, string srcTableName)
        {
            return this.J_ExecSqlReturnDataSet(SqlText, srcTableName).Tables[Index];
        }
        #endregion

        #region EXEC SQL RETURN DATATABLE [6]
        public DataTable J_ExecSqlReturnDataTable(IDbCommand command, string SqlText, int Index, string srcTableName)
        {
            return this.J_ExecSqlReturnDataSet(command, SqlText, srcTableName).Tables[Index];
        }
        #endregion

        #endregion

        #region EXEC SQL RETURN READER [ OVERLOADED METHOD ]

        #region EXEC SQL RETURN READER [1]
        public IDataReader J_ExecSqlReturnReader(string SqlText)
        {
            return this.J_ExecSqlReturnReader(null, SqlText, CommandBehavior.Default, CommandType.Text);
        }
        #endregion

        #region EXEC SQL RETURN READER [2]
        public IDataReader J_ExecSqlReturnReader(IDbCommand command, string SqlText)
        {
            return this.J_ExecSqlReturnReader(command, SqlText, CommandBehavior.Default, CommandType.Text);
        }
        #endregion

        #region EXEC SQL RETURN READER [3]
        public IDataReader J_ExecSqlReturnReader(string SqlText, CommandType Type)
        {
            return this.J_ExecSqlReturnReader(null, SqlText, CommandBehavior.Default, Type);
        }
        #endregion

        #region EXEC SQL RETURN READER [4]
        public IDataReader J_ExecSqlReturnReader(IDbCommand command, string SqlText, CommandType Type)
        {
            return this.J_ExecSqlReturnReader(command, SqlText, CommandBehavior.Default, Type);
        }
        #endregion

        #region EXEC SQL RETURN READER [5]
        public IDataReader J_ExecSqlReturnReader(string SqlText, CommandBehavior commandBehavior)
        {
            return this.J_ExecSqlReturnReader(null, SqlText, commandBehavior, CommandType.Text);
        }
        #endregion

        #region EXEC SQL RETURN READER [6]
        public IDataReader J_ExecSqlReturnReader(IDbCommand command, string SqlText, CommandBehavior commandBehavior)
        {
            return this.J_ExecSqlReturnReader(command, SqlText, commandBehavior, CommandType.Text);
        }
        #endregion

        #region EXEC SQL RETURN READER [7]
        public IDataReader J_ExecSqlReturnReader(string SqlText, CommandBehavior commandBehavior, CommandType Type)
        {
            return this.J_ExecSqlReturnReader(null, SqlText, commandBehavior, Type);
        }
        #endregion

        #region EXEC SQL RETURN READER [8]
        public IDataReader J_ExecSqlReturnReader(IDbCommand command, string SqlText, CommandBehavior commandBehavior, CommandType Type)
        {
            try
            {
                if (command == null)
                {
                    if (this.J_ValidateConnection() == false)
                        return null;

                    this.J_IdbCommand.CommandText = SqlText;
                    this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;
                    this.J_IdbCommand.CommandType = Type;
                    return this.J_IdbCommand.ExecuteReader(commandBehavior);
                }
                else if (command != null)
                {
                    command.CommandText = SqlText;
                    command.CommandTimeout = J_Var.J_pCommandTimeout;
                    command.CommandType = Type;
                    return command.ExecuteReader(commandBehavior);
                }
                return null;
            }
            catch(Exception ERR)
            {
                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return null;
            }
        }
        #endregion

        #endregion

        #region EXEC SQL RETURN ADODB RECORD SET [ OVERLOADED METHOD ]

        #region EXEC SQL RETURN ADODBRecordset [1]
        public ADODB.Recordset J_ExecSqlReturnADODBRecordset(string SqlText)
        {
            return this.J_ExecSqlReturnADODBRecordset(SqlText, ADODB.CursorLocationEnum.adUseClient, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly);
        }
        #endregion

        #region EXEC SQL RETURN ADODBRecordset [2]
        public ADODB.Recordset J_ExecSqlReturnADODBRecordset(string SqlText, ADODB.CursorLocationEnum ADODBCursorLocation)
        {
            return this.J_ExecSqlReturnADODBRecordset(SqlText, ADODBCursorLocation, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly);
        }
        #endregion

        #region EXEC SQL RETURN ADODBRecordset [3]
        public ADODB.Recordset J_ExecSqlReturnADODBRecordset(string SqlText, ADODB.CursorLocationEnum ADODBCursorLocation, ADODB.CursorTypeEnum ADODBCursorType)
        {
            return this.J_ExecSqlReturnADODBRecordset(SqlText, ADODBCursorLocation, ADODBCursorType, ADODB.LockTypeEnum.adLockReadOnly);
        }
        #endregion

        #region EXEC SQL RETURN ADODBRecordset [4]
        public ADODB.Recordset J_ExecSqlReturnADODBRecordset(string SqlText, ADODB.CursorLocationEnum ADODBCursorLocation, ADODB.CursorTypeEnum ADODBCursorType, ADODB.LockTypeEnum ADODBLockType)
        {
            try
            {
                if (this.J_ValidateConnection() == false)
                    return null;

                this.J_ADODBRecordset.CursorLocation = ADODBCursorLocation;
                this.J_ADODBRecordset.Open(SqlText, this.J_ADODBConn, ADODBCursorType, ADODBLockType, 0);

                return this.J_ADODBRecordset;
            }
            catch(Exception err)
            {
                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return null;
            }
        }
        #endregion

        #endregion

        #region EXEC SQL RETURN SCALAR [ OVERLOADED METHOD ]

        #region J_ExecSqlReturnScalar [1]
        public object J_ExecSqlReturnScalar(string SqlText)
        {
            return this.J_ExecSqlReturnScalar(null, SqlText);
        }
        #endregion
                
        #region J_ExecSqlReturnScalar [2]
        public object J_ExecSqlReturnScalar(IDbCommand command, string SqlText)
        {
            try
            {
                if (command == null)
                {
                    if (this.J_ValidateConnection() == false)
                        return null;

                    this.J_IdbCommand.CommandText = SqlText;
                    this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;
                    return this.J_IdbCommand.ExecuteScalar();
                }
                else if (command != null)
                {
                    command.CommandText = SqlText;
                    command.CommandTimeout = J_Var.J_pCommandTimeout;
                    return command.ExecuteScalar();
                }
                return null;
            }
            catch
            {
                this.J_CloseConnection();
                return null;
            }
        }
        #endregion

        #endregion


        #region POPULATE COMBO BOX  [ OVERLOADED METHOD ]

        #region POPULATE COMBO BOX [1]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [2]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [3]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, int SelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [4]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [5]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [6]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [7]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [8]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [9]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [10]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [11]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, int SelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [12]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [13]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [14]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [15]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [16]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [17]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [18]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [19]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [20]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [21]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [22]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [23]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [24]
        public bool J_PopulateComboBox(string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(null, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [25]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [26]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [27]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [28]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [29]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [30]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, DefaultText, ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [31]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            return this.J_PopulateComboBox(command, SqlText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [32]
        public bool J_PopulateComboBox(IDbCommand command, string SqlText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            IDataReader reader = null;

            try
            {
                if (command == null)
                    reader = this.J_ExecSqlReturnReader(SqlText);
                else if (command != null)
                    reader = this.J_ExecSqlReturnReader(command, SqlText);

                if (reader == null) return false;
                
                commonservice.J_ClearComboBox(ref combobox, DefaultText, ComboBoxDefaultText, J_ComboBoxSelectedIndex.NO);
                while (reader.Read())
                    combobox.Items.Add(new ListBoxItem(reader.GetString(1).ToString(), Convert.ToInt32(reader.GetValue(0))));
                
                // reader object is closed & disposed
                reader.Close();
                reader.Dispose();

                if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.YES)
                    combobox.SelectedIndex = SelectedIndex;
                else if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.NO)
                {
                    if (DefaultText == null || DefaultText == "")
                    {
                        combobox.Text = "";
                        combobox.SelectedText = "";
                    }
                    else
                    {
                        combobox.Text = DefaultText;
                        combobox.SelectedText = DefaultText;
                    }
                }
                return true;
            }
            catch
            {
                reader.Close();
                reader.Dispose();

                this.J_CloseConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return false;
            }
        }
        #endregion


        #region POPULATE COMBO BOX [33]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [34]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [35]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [36]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, ComboBoxDefaultText, 0, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [37]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, int SelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [38]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [39]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, int SelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [40]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, ComboBoxDefaultText, SelectedIndex, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region POPULATE COMBO BOX [41]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [42]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [43]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, J_ComboBoxDefaultText.YES, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [44]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, DefaultText, ComboBoxDefaultText, 0, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [45]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [46]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", ComboBoxDefaultText, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [47]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_PopulateComboBox(ArrayText, ref combobox, "", J_ComboBoxDefaultText.YES, SelectedIndex, ComboBoxSelectedIndex);
        }
        #endregion

        #region POPULATE COMBO BOX [48]
        public void J_PopulateComboBox(string[] ArrayText, ref ComboBox combobox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, int SelectedIndex, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            commonservice.J_ClearComboBox(ref combobox, DefaultText, ComboBoxDefaultText, ComboBoxSelectedIndex);

            for (int iCounter = 0; iCounter <= ArrayText.Length - 1; iCounter++)
                combobox.Items.Add(ArrayText[iCounter]);

            if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.YES)
                combobox.SelectedIndex = SelectedIndex;
        }
        #endregion

        #endregion

        #region POPULATE LISTBOX

        #region J_PopulateListBox [1]
        public bool J_PopulateListBox(string SqlText, ref ListBox listBox, string displayMember)
        {
            return this.J_PopulateListBox(null, SqlText, ref listBox, displayMember, null, J_Alignment.LeftCentre);
        }
        #endregion

        #region J_PopulateListBox [2]
        public bool J_PopulateListBox(IDbCommand command, string SqlText, ref ListBox listBox, string displayMember)
        {
            return this.J_PopulateListBox(command, SqlText, ref listBox, displayMember, null, J_Alignment.LeftCentre);
        }
        #endregion

        #region J_PopulateListBox [3]
        public bool J_PopulateListBox(string SqlText, ref ListBox listBox, string displayMember, string formatString)
        {
            return this.J_PopulateListBox(null, SqlText, ref listBox, displayMember, formatString, J_Alignment.LeftCentre);
        }
        #endregion

        #region J_PopulateListBox [4]
        public bool J_PopulateListBox(IDbCommand command, string SqlText, ref ListBox listBox, string displayMember, string formatString)
        {
            return this.J_PopulateListBox(command, SqlText, ref listBox, displayMember, formatString, J_Alignment.LeftCentre);
        }
        #endregion

        #region J_PopulateListBox [5]
        public bool J_PopulateListBox(string SqlText, ref ListBox listBox, string displayMember, string formatString, J_Alignment alignment)
        {
            return this.J_PopulateListBox(null, SqlText, ref listBox, displayMember, formatString, alignment);
        }
        #endregion

        #region J_PopulateListBox [6]
        public bool J_PopulateListBox(IDbCommand command, string SqlText, ref ListBox listBox, string displayMember, string formatString, J_Alignment alignment)
        {
            listBox.DataSource = null;
            listBox.Items.Clear();

            if (command == null)
                listBox.DataSource = this.J_ExecSqlReturnDataTable(SqlText);
            else if (command != null)
                listBox.DataSource = this.J_ExecSqlReturnDataTable(command, SqlText);

            if (listBox.DataSource == null) return false;

            if (displayMember != null && displayMember != "") listBox.DisplayMember = displayMember;
            if (formatString != null && formatString != "") listBox.FormatString = "0.00";

            if (alignment == J_Alignment.LeftCentre)
                listBox.RightToLeft = RightToLeft.No;
            else if (alignment == J_Alignment.RightCentre)
                listBox.RightToLeft = RightToLeft.Yes;

            return true;
        }
        #endregion

        #endregion


        #region CONVERT XML TO DATA SET [ OVERLOADED METHOD ]

        #region CONVERT XML TO DATA SET [1]
        public DataSet J_ConvertXmlToDataSet(string xmlPhysicalFilePath)
        {
            return J_ConvertXmlToDataSet(xmlPhysicalFilePath, XmlReadMode.Auto);
        }
        #endregion

        #region CONVERT XML TO DATA SET [2]
        public DataSet J_ConvertXmlToDataSet(string xmlPhysicalFilePath, XmlReadMode readMode)
        {
            try
            {
                this.J_DataSet = new DataSet();
                this.J_DataSet.ReadXml(xmlPhysicalFilePath, readMode);
                return this.J_DataSet;
            }
            catch
            {
                this.J_CloseConnection();
                return null;
            }
        }
        #endregion

        #endregion

        #region DISPOSE
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion

        #region RECORD EXIST [ OVERLOADED METHOD ]

        #region J_IsRecordExist [1]
        public bool J_IsRecordExist(string TableName)
        {
            return this.J_IsRecordExist(TableName, null);
        }
        #endregion

        #region J_IsRecordExist [2]
        public bool J_IsRecordExist(IDbCommand command, string TableName)
        {
            return this.J_IsRecordExist(command, TableName, null);
        }
        #endregion

        #region J_IsRecordExist [3]
        public bool J_IsRecordExist(string TableName, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) FROM " + TableName + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria + " ";

            if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                return true;

            return false;
        }
        #endregion

        #region J_IsRecordExist [4]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) FROM " + TableName + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria + " ";

            if (command == null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                    return true;
            }
            else if (command != null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL)) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region J_IsRecordExist [5]
        public bool J_IsRecordExist(string TableName, string FieldName, string FieldValue)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, null);
        }
        #endregion

        #region J_IsRecordExist [6]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, string FieldValue)
        {
            return this.J_IsRecordExist(command, TableName, FieldName, FieldValue, null);
        }
        #endregion

        #region J_IsRecordExist [7]
        public bool J_IsRecordExist(string TableName, string FieldName, long FieldValue)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, null);
        }
        #endregion

        #region J_IsRecordExist [8]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, long FieldValue)
        {
            return this.J_IsRecordExist(command, TableName, FieldName, FieldValue, null);
        }
        #endregion

        #region J_IsRecordExist [9]
        public bool J_IsRecordExist(string TableName, string FieldName, string FieldValue, string OtherCriteria)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, OtherCriteria);
        }
        #endregion

        #region J_IsRecordExist [10]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, string FieldValue, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) AS NoOfRows " +
                     "FROM   " + TableName + " " +
                     "WHERE  UPPER(" + FieldName + ") = '" + this.commonservice.J_ReplaceQuote(FieldValue.Trim().ToUpper()) + "' ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "AND " + OtherCriteria + " ";

            if (command == null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                    return true;
            }
            else if (command != null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL)) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region J_IsRecordExist [11]
        public bool J_IsRecordExist(string TableName, string FieldName, long FieldValue, string OtherCriteria)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, OtherCriteria);
        }
        #endregion

        #region J_IsRecordExist [12]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, long FieldValue, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) AS NoOfRows " +
                     "FROM   " + TableName + " " +
                     "WHERE  " + FieldName + " = " + Convert.ToInt64(FieldValue) + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "AND " + OtherCriteria + " ";

            if (command == null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                    return true;
            }
            else if (command != null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL)) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region J_IsRecordExist [13]
        public bool J_IsRecordExist(string TableName, string FieldName, string FieldValue, string FieldId, long FieldIdValue)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, FieldId, FieldIdValue, null);
        }
        #endregion

        #region J_IsRecordExist [14]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, string FieldValue, string FieldId, long FieldIdValue)
        {
            return this.J_IsRecordExist(command, TableName, FieldName, FieldValue, FieldId, FieldIdValue, null);
        }
        #endregion

        #region J_IsRecordExist [15]
        public bool J_IsRecordExist(string TableName, string FieldName, long FieldValue, string FieldId, long FieldIdValue)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, FieldId, FieldIdValue, null);
        }
        #endregion

        #region J_IsRecordExist [16]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, long FieldValue, string FieldId, long FieldIdValue)
        {
            return this.J_IsRecordExist(command, TableName, FieldName, FieldValue, FieldId, FieldIdValue, null);
        }
        #endregion

        #region J_IsRecordExist [17]
        public bool J_IsRecordExist(string TableName, string FieldName, string FieldValue, string FieldId, long FieldIdValue, string OtherCriteria)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, FieldId, FieldIdValue, OtherCriteria);
        }
        #endregion

        #region J_IsRecordExist [18]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, string FieldValue, string FieldId, long FieldIdValue, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) AS NoOfRows " +
                     "FROM   " + TableName + " " +
                     "WHERE  UPPER(" + FieldName + ") = '" + this.commonservice.J_ReplaceQuote(FieldValue.Trim().ToUpper()) + "' " +
                     "AND    " + FieldId + "         <> " + Convert.ToInt64(FieldIdValue) + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "AND " + OtherCriteria + " ";

            if (command == null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                    return true;
            }
            else if (command != null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL)) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region J_IsRecordExist [19]
        public bool J_IsRecordExist(string TableName, string FieldName, long FieldValue, string FieldId, long FieldIdValue, string OtherCriteria)
        {
            return this.J_IsRecordExist(null, TableName, FieldName, FieldValue, FieldId, FieldIdValue, OtherCriteria);
        }
        #endregion

        #region J_IsRecordExist [20]
        public bool J_IsRecordExist(IDbCommand command, string TableName, string FieldName, long FieldValue, string FieldId, long FieldIdValue, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) AS NoORows " +
                     "FROM   " + TableName + " " +
                     "WHERE  " + FieldName + " = " + Convert.ToInt64(FieldValue) + " " +
                     "AND    " + FieldId + "  <> " + Convert.ToInt64(FieldIdValue) + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "AND " + OtherCriteria + " ";

            if (command == null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)) > 0)
                    return true;
            }
            else if (command != null)
            {
                if (commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL)) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #endregion

        #region RETURN MAXIMUM VALUE [ OVERLOADED METHOD ]

        #region J_ReturnMaxValue [1]
        public long J_ReturnMaxValue(string TableName, string FieldName)
        {
            return this.J_ReturnMaxValue(null, TableName, FieldName, null);
        }
        #endregion

        #region J_ReturnMaxValue [2]
        public long J_ReturnMaxValue(IDbCommand command, string TableName, string FieldName)
        {
            return this.J_ReturnMaxValue(command, TableName, FieldName, null);
        }
        #endregion

        #region J_ReturnMaxValue [3]
        public long J_ReturnMaxValue(string TableName, string FieldName, string OtherCriteria)
        {
            return this.J_ReturnMaxValue(null, TableName, FieldName, OtherCriteria);
        }
        #endregion

        #region J_ReturnMaxValue [4]
        public long J_ReturnMaxValue(IDbCommand command, string TableName, string FieldName, string OtherCriteria)
        {
            strSQL = "SELECT " + commonservice.J_SQLDBFormat("MAX(" + this.commonservice.J_ReplaceQuote(FieldName) + ")", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS MAX_VALUE " +
                     "FROM   " + TableName + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria;

            if(command == null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL));
            else if (command != null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL));
            
            return 0;
        }
        #endregion

        #endregion

        #region RETURN MINIMUM VALUE [ OVERLOADED METHOD ]

        #region J_ReturnMinValue [1]
        public long J_ReturnMinValue(string TableName, string FieldName)
        {
            return this.J_ReturnMinValue(null, TableName, FieldName, null);
        }
        #endregion

        #region J_ReturnMinValue [2]
        public long J_ReturnMinValue(IDbCommand command, string TableName, string FieldName)
        {
            return this.J_ReturnMinValue(command, TableName, FieldName, null);
        }
        #endregion

        #region J_ReturnMinValue [3]
        public long J_ReturnMinValue(string TableName, string FieldName, string OtherCriteria)
        {
            return this.J_ReturnMinValue(null, TableName, FieldName, OtherCriteria);
        }
        #endregion

        #region J_ReturnMinValue [4]
        public long J_ReturnMinValue(IDbCommand command, string TableName, string FieldName, string OtherCriteria)
        {
            strSQL = "SELECT " + commonservice.J_SQLDBFormat("MIN(" + this.commonservice.J_ReplaceQuote(FieldName) + ")", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS MIN_VALUE " +
                     "FROM   " + TableName + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria;

            if (command == null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL));
            else if (command != null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL));

            return 0;
        }
        #endregion

        #endregion

        #region RETURN ID [ OVERLOADED METHOD ]

        #region J_ReturnId [1]
        public long J_ReturnId(string SqlText)
        {
            return this.J_ReturnId(null, SqlText);
        }
        #endregion

        #region J_ReturnId [2]
        public long J_ReturnId(IDbCommand command, string SqlText)
        {
            if(command == null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(SqlText));
            else if (command != null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, SqlText));
            
            return 0;
        }
        #endregion

        #endregion

        #region RETURN SERVER DATE [ OVERLOADED METHOD ]

        #region J_ReturnServerDate [1]
        public string J_ReturnServerDate()
        {
            return this.J_ReturnServerDate(null, this.enmDatabaseType);
        }
        #endregion

        #region J_ReturnServerDate [2]
        public string J_ReturnServerDate(IDbCommand command)
        {
            return this.J_ReturnServerDate(command, this.enmDatabaseType);
        }
        #endregion

        #region J_ReturnServerDate [3]
        public string J_ReturnServerDate(J_DatabaseType dbType)
        {
            return this.J_ReturnServerDate(null, dbType);
        }
        #endregion

        #region J_ReturnServerDate [4]
        public string J_ReturnServerDate(IDbCommand command, J_DatabaseType dbType)
        {
            switch (dbType)
            {
                case J_DatabaseType.SqlServer:
                    this.strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
                    break;

                case J_DatabaseType.Oracle:
                    break;

                case J_DatabaseType.MsAccess:
                    this.strSQL = "SELECT FORMAT(DATE(),'dd/MM/yyyy')";
                    break;

                default:
                    this.strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),103)";
                    break;
            }
            
            if(command == null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(this.strSQL));
            else if (command != null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(command, this.strSQL));
            
            return "";
        }
        #endregion

        #endregion

        #region RETURN SERVER TIME [ OVERLOADED METHOD ]

        #region J_ReturnServerTime [1]
        public string J_ReturnServerTime()
        {
            return this.J_ReturnServerTime(null, this.enmDatabaseType);
        }
        #endregion

        #region J_ReturnServerTime [2]
        public string J_ReturnServerTime(IDbCommand command)
        {
            return this.J_ReturnServerTime(command, this.enmDatabaseType);
        }
        #endregion

        #region J_ReturnServerTime [3]
        public string J_ReturnServerTime(J_DatabaseType dbType)
        {
            return this.J_ReturnServerTime(null, dbType);
        }
        #endregion

        #region J_ReturnServerTime [4]
        public string J_ReturnServerTime(IDbCommand command, J_DatabaseType dbType)
        {
            switch (dbType)
            {
                case J_DatabaseType.SqlServer:
                    this.strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),108)";
                    break;

                case J_DatabaseType.Oracle:
                    break;

                case J_DatabaseType.MsAccess:
                    this.strSQL = "SELECT FORMAT(TIME(),'hh:mm:ss ampm')";
                    break;

                default:
                    this.strSQL = "SELECT CONVERT(CHAR(10),GETDATE(),108)";
                    break;
            }

            if (command == null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(this.strSQL));
            else if (command != null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(command, this.strSQL));

            return "";
        }
        #endregion

        #endregion


        #region DATABASE OBJECT EXIST [ OVERLOADED METHOD ]

        #region J_IsDatabaseObjectExist [1]
        public bool J_IsDatabaseObjectExist(string TableName)
        {
            return J_IsDatabaseObjectExist(null, null, TableName, null, null, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [2]
        public bool J_IsDatabaseObjectExist(IDbConnection connection, IDbCommand command, string TableName)
        {
            return J_IsDatabaseObjectExist(connection, command, TableName, null, null, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [3]
        public bool J_IsDatabaseObjectExist(string TableName, string ColumnName)
        {
            return J_IsDatabaseObjectExist(null, null, TableName, ColumnName, null, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [4]
        public bool J_IsDatabaseObjectExist(IDbConnection connection, IDbCommand command, string TableName, string ColumnName)
        {
            return J_IsDatabaseObjectExist(connection, command, TableName, ColumnName, null, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [5]
        public bool J_IsDatabaseObjectExist(string TableName, string ColumnName, string DataType)
        {
            return J_IsDatabaseObjectExist(null, null, TableName, ColumnName, DataType, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [6]
        public bool J_IsDatabaseObjectExist(IDbConnection connection, IDbCommand command, string TableName, string ColumnName, string DataType)
        {
            return J_IsDatabaseObjectExist(connection, command, TableName, ColumnName, DataType, 0);
        }
        #endregion

        #region J_IsDatabaseObjectExist [7]
        public bool J_IsDatabaseObjectExist(string TableName, string ColumnName, int ColumnSize)
        {
            return this.J_IsDatabaseObjectExist(null, null, TableName, ColumnName, null, ColumnSize);
        }
        #endregion

        #region J_IsDatabaseObjectExist [8]
        public bool J_IsDatabaseObjectExist(IDbConnection connection, IDbCommand command, string TableName, string ColumnName, int ColumnSize)
        {
            return this.J_IsDatabaseObjectExist(connection, command, TableName, ColumnName, null, ColumnSize);
        }
        #endregion

        #region J_IsDatabaseObjectExist [9]
        public bool J_IsDatabaseObjectExist(string TableName, string ColumnName, string DataType, int ColumnSize)
        {
            return this.J_IsDatabaseObjectExist(null, null, TableName, ColumnName, null, ColumnSize);
        }
        #endregion

        #region J_IsDatabaseObjectExist [10]
        public bool J_IsDatabaseObjectExist(IDbConnection connection, IDbCommand command, string TableName, string ColumnName, string DataType, int ColumnSize)
        {
            OleDbConnection cn = null;
            DataTable dt;

            try
            {
                if (this.enmDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (this.enmConnectionProviderType == J_ConnectionProviderType.OleDb)
                    {
                        if (connection == null)
                        {
                            if (this.J_ValidateConnection() == false) return false;
                            cn = new OleDbConnection(this.J_pConnectionString);
                        }
                        else if (connection != null)
                        {
                            cn = (OleDbConnection)connection;
                        }

                        // open another connection
                        cn.Open();

                        if (TableName != null && ColumnName == null)
                        {
                            dt = cn.GetSchema("tables");
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if (TableName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper())
                                {
                                    dt.Dispose();
                                    cn.Dispose();
                                    return true;
                                }
                            }
                        }
                        else if (TableName != null && ColumnName != null && ColumnSize == 0)
                        {
                            dt = cn.GetSchema("Columns");
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if (TableName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper()
                                && ColumnName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["COLUMN_NAME"]).Trim().ToUpper())
                                {
                                    dt.Dispose();
                                    cn.Dispose();
                                    return true;
                                }
                            }
                        }
                        else if (TableName != null && ColumnName != null && ColumnSize != 0)
                        {
                            dt = cn.GetSchema("Columns");
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if (TableName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["TABLE_NAME"]).Trim().ToUpper()
                                && ColumnName.Trim().ToUpper() == Convert.ToString(dt.Rows[i]["COLUMN_NAME"]).Trim().ToUpper()
                                && ColumnSize == commonservice.J_ReturnInt32Value(Convert.ToString(dt.Rows[i]["CHARACTER_MAXIMUM_LENGTH"])))
                                {
                                    dt.Dispose();
                                    cn.Dispose();
                                    return true;
                                }
                            }
                        }
                        cn.Dispose();
                    }
                }
                else if (this.enmDatabaseType == J_DatabaseType.SqlServer)
                {
                    strSQL = "SELECT COUNT(*) AS NO_OF_TABLES " +
                             "FROM   INFORMATION_SCHEMA.COLUMNS " +
                             "WHERE  UPPER(TABLE_NAME) = '" + TableName.ToUpper() + "' ";

                    if (ColumnName != "" && ColumnName != null)
                        strSQL = strSQL + "AND UPPER(COLUMN_NAME) = '" + ColumnName.ToUpper() + "' ";

                    if (DataType != "" && DataType != null)
                        strSQL = strSQL + "AND UPPER(DATA_TYPE) = '" + DataType.ToUpper() + "' ";

                    if (ColumnSize != 0)
                        strSQL = strSQL + "AND CHARACTER_MAXIMUM_LENGTH = " + ColumnSize + " ";

                    if (command == null)
                    {
                        if (Convert.ToInt64(commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL))) > 0) return true;
                    }
                    else if (command != null)
                    {
                        if (Convert.ToInt64(commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL))) > 0) return true;
                    }
                }
                return false;
            }
            catch
            {
                if(cn != null) cn.Dispose();
                return false;
            }
        }
        #endregion

        #endregion

        #region INDEX EXIST [ OVERLOADED METHOD ]

        #region J_IsIndexExist [1]
        public bool J_IsIndexExist(string TableName)
        {
            return this.J_IsIndexExist(TableName, null);
        }
        #endregion

        #region J_IsIndexExist [2]
        public bool J_IsIndexExist(string TableName, string IndexName)
        {
            strSQL = "SELECT COUNT(*) NO_OF_INDEXES " +
                     "FROM   SYS.INDEXES " +
                     "WHERE  OBJECT_ID = OBJECT_ID('" + TableName + "') ";

            if (IndexName != "" && IndexName != null)
                strSQL = strSQL + "AND UPPER(NAME) = '" + IndexName.ToUpper() + "'";

            if (Convert.ToInt64(commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL))) > 0)
                return true;
            return false;
        }
        #endregion

        #endregion

        #region DROP INDEX [ OVERLOADED METHOD ]

        #region J_DropIndex [1]
        public bool J_DropIndex(string TableName)
        {
            return this.J_DropIndex(TableName, null, J_IndexType.All);
        }
        #endregion

        #region J_DropIndex [2]
        public bool J_DropIndex(string TableName, string IndexName)
        {
            return this.J_DropIndex(TableName, IndexName, J_IndexType.All);
        }
        #endregion

        #region J_DropIndex [3]
        public bool J_DropIndex(string TableName, string IndexName, J_IndexType IndexType)
        {
            int intRowCount = 0;
            string[,] strArray;
            int intCounter = 0;

            strSQL = "SELECT COUNT(*) AS NO_OF_ROWS " +
                     "FROM   SYS.INDEXES " +
                     "WHERE  OBJECT_ID = OBJECT_ID('" + this.commonservice.J_ReplaceQuote(TableName) + "') " +
                     "AND    NAME IS NOT NULL ";
            if (IndexName != "" && IndexName != null)
                strSQL = strSQL + "AND NAME = '" + this.commonservice.J_ReplaceQuote(IndexName) + "' ";
            
            if (IndexType == J_IndexType.Unique)
                strSQL = strSQL + "AND IS_UNIQUE = 1 ";
            else if (IndexType == J_IndexType.NonUnique)
                strSQL = strSQL + "AND IS_UNIQUE = 0 ";

            intRowCount = Convert.ToInt32(commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL)));
            if (intRowCount == 0) return false;

            strArray = new string[intRowCount, 1];

            strSQL = "SELECT NAME " +
                     "FROM   SYS.INDEXES " +
                     "WHERE  OBJECT_ID = OBJECT_ID('" + this.commonservice.J_ReplaceQuote(TableName) + "') " +
                     "AND    NAME IS NOT NULL ";
            if (IndexName != "" && IndexName != null)
                strSQL = strSQL + "AND NAME = '" + this.commonservice.J_ReplaceQuote(IndexName) + "' ";
            
            if (IndexType == J_IndexType.Unique)
                strSQL = strSQL + "AND IS_UNIQUE = 1 ";
            else if (IndexType == J_IndexType.NonUnique)
                strSQL = strSQL + "AND IS_UNIQUE = 0 ";

            SqlDataReader reader = (SqlDataReader)this.J_ExecSqlReturnReader(strSQL);
            if (reader == null) return false;
            
            if (reader.HasRows == true)
            {
                intCounter = 0;
                while (reader.Read())
                {
                    strArray[intCounter, 0] = reader["NAME"].ToString();
                    intCounter++;
                }
            }
            reader.Close();
            reader.Dispose();

            for (intCounter = 0; intCounter <= strArray.GetUpperBound(0); intCounter++)
            {
                strSQL = "DROP INDEX " + this.commonservice.J_ReplaceQuote(strArray[intCounter, 0]) + " ON " + this.commonservice.J_ReplaceQuote(TableName) + "";
                if (this.J_ExecSql(strSQL) == false) return false;
            }
            return true;
        }
        #endregion

        #endregion


        #region GET SQL SERVERS [ OVERLOADED METHOD ]

        #region J_GetSQLServers [1]
        public void J_GetSQLServers(ref ComboBox ServerName, ref ComboBox DatabaseName)
        {
            this.J_GetSQLServers(ref ServerName, ref DatabaseName, "----------Select Server---------");
        }
        #endregion

        #region J_GetSQLServers [2]
        public void J_GetSQLServers(ref ComboBox ServerName, ref ComboBox DatabaseName, string DisplayText)
        {
            try
            {
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                DataTable dataTable = instance.GetDataSources();
                
                if (dataTable.Rows.Count > 0)
                {
                    ServerName.Items.Clear();
                    ServerName.Items.Add(DisplayText);
                    foreach (DataRow rowServer in dataTable.Rows)
                        ServerName.Items.Add(rowServer[0]);
                    ServerName.SelectedIndex = 0;
                }
                dataTable.Dispose();
            }
            catch
            {
                DatabaseName.Text = "";
                DatabaseName.Items.Clear();
            }
        }
        #endregion

        #endregion

        #region GET SQL DATABASES [ OVERLOADED METHOD ]

        #region J_GetSQLDatabases [1]
        public void J_GetSQLDatabases(ref ComboBox ServerName, ref TextBox UserName, ref TextBox Password, ref ComboBox DatabaseName)
        {
            this.J_GetSQLDatabases(ref ServerName, ref UserName, ref Password, ref DatabaseName, "---------Select Database---------");
        }
        #endregion

        #region J_GetSQLDatabases [2]
        public void J_GetSQLDatabases(ref ComboBox ServerName, ref TextBox UserName, ref TextBox Password, ref ComboBox DatabaseName, string DisplayText)
        {
            if (ServerName.Text == "")
            {
                DatabaseName.Items.Clear();
                return;
            }
            if (UserName.Text == "")
            {
                DatabaseName.Items.Clear();
                return;
            }
            if (Password.Text == "")
            {
                DatabaseName.Items.Clear();
                return;
            }
            
            if (this.J_ValidateConnection(ServerName.Text, UserName.Text, Password.Text) == true)
            {
                SqlDataReader reader = (SqlDataReader)this.J_ExecSqlReturnReader("sp_databases", CommandType.StoredProcedure);
                
                DatabaseName.Items.Clear();
                DatabaseName.Items.Add(DisplayText);

                while (reader.Read())
                    DatabaseName.Items.Add(reader["DATABASE_NAME"]);
                reader.Dispose();

                DatabaseName.SelectedIndex = 0;
            }
            else
                DatabaseName.Items.Clear();

            this.J_CloseConnection();
        }
        #endregion

        #endregion

        #region CLEAR DATABASE LOG [ OVERLOADED METHOD ]

        #region J_ClearDatabaseLog [1]
        public void J_ClearDatabaseLog()
        {
            this.J_ClearDatabaseLog(this.J_strDataBase);
        }
        #endregion

        #region J_ClearDatabaseLog [2]
        public void J_ClearDatabaseLog(string DatabaseName)
        {
            if (this.enmDatabaseType == J_DatabaseType.SqlServer)
                this.J_ExecSql("DBCC SHRINKDATABASE ('" + DatabaseName + "', 0)");
        }
        #endregion
        
        #endregion

        #region SHOW DATA IN GRID [ OVERLOADED METHOD ]

        #region J_ShowDataInGrid [1]
        public DataSet J_ShowDataInGrid(ref DGControl.DGControl dgControl, string SqlText)
        {
            return this.J_ShowDataInGrid(ref dgControl, SqlText, null);
        }
        #endregion

        #region J_ShowDataInGrid [2]
        public DataSet J_ShowDataInGrid(IDbCommand command, ref DGControl.DGControl dgControl, string SqlText)
        {
            return this.J_ShowDataInGrid(command, ref dgControl, SqlText, null);
        }
        #endregion

        #region J_ShowDataInGrid [3]
        public DataSet J_ShowDataInGrid(ref DGControl.DGControl dgControl, string SqlText, string[,] GridColumns)
        {
            return this.J_ShowDataInGrid(null, ref dgControl, SqlText, GridColumns);
        }
        #endregion

        #region J_ShowDataInGrid [4]
        public DataSet J_ShowDataInGrid(IDbCommand command, ref DGControl.DGControl dgControl, string SqlText, string[,] GridColumns)
        {
            if (this.J_DataSet != null) this.J_DataSet.Clear();

            if(command == null)
                this.J_DataSet = this.J_ExecSqlReturnDataSet(SqlText);
            else if (command != null)
                this.J_DataSet = this.J_ExecSqlReturnDataSet(command, SqlText);
            
            if (this.J_DataSet == null) return null;
            dgControl.DataSource = this.J_DataSet.Tables[0];
            if (GridColumns != null) this.J_setCustomGridColumn(ref dgControl, this.J_DataSet, GridColumns);
            return this.J_DataSet;
        }
        #endregion

        #region J_ShowDataInGrid [5]
        public DataSet J_ShowDataInGrid(ref DGVControl.DGVControl dgvControl, string SqlText, string[,] arrColumns)
        {
            return this.J_ShowDataInGrid(null, ref dgvControl, SqlText, arrColumns);
        }
        #endregion

        #region J_ShowDataInGrid [6]
        public DataSet J_ShowDataInGrid(IDbCommand command, ref DGVControl.DGVControl dgvControl, string SqlText, string[,] arrColumns)
        {
            try
            {
                DataSet dsDataGrid = null;

                if (command == null) dsDataGrid = this.J_ExecSqlReturnDataSet(SqlText);
                else if (command != null) dsDataGrid = this.J_ExecSqlReturnDataSet(command, SqlText);

                dgvControl.DataSource = dsDataGrid.Tables[0];

                int intColumnIndex = 0;

                if (arrColumns == null) return dsDataGrid;

                for (int intCounter = 0; intCounter <= arrColumns.GetUpperBound(0); intCounter++)
                {
                    // set the Header text & Width of respective column
                    dgvControl.Columns[intColumnIndex].DataPropertyName = dsDataGrid.Tables[0].Columns[intCounter].ColumnName;
                    dgvControl.Columns[intColumnIndex].HeaderText = arrColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                    dgvControl.Columns[intColumnIndex].Width = int.Parse(arrColumns[intCounter, (int)J_GridColumnSetting.Width]);
                    dgvControl.Columns[intColumnIndex].ReadOnly = true;

                    // set the Data Format of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                        dgvControl.Columns[intColumnIndex].DefaultCellStyle.Format = arrColumns[intCounter, (int)J_GridColumnSetting.Format];

                    // set the Alignment of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "" || arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                    {
                        dgvControl.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgvControl.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    else if (arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "R" || arrColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "RIGHT")
                    {
                        dgvControl.Columns[intColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvControl.Columns[intColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvControl.Columns[intColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    // set the Visibility of respective column
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim() == "" || arrColumns[intCounter, (int)J_GridColumnSetting.Visible].Trim().ToUpper() == "T")
                        dgvControl.Columns[intColumnIndex].Visible = true;
                    else
                        dgvControl.Columns[intColumnIndex].Visible = false;

                    // set the AutoSize Mode  of respective column Default None
                    if (arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim() == "" || arrColumns[intCounter, (int)J_GridColumnSetting.AutoSizeMode].Trim().ToUpper() == "NONE")
                        dgvControl.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    else
                        dgvControl.Columns[intColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    intColumnIndex += 1;
                }
                return dsDataGrid;
            }
            catch (Exception exception)
            {
                commonservice.J_UserMessage(exception.Message);
                return null;
            }
        }
        #endregion

        #endregion

        #region SET CUSTOM GRID COLUMN [ OVERLOADED METHOD ]

        #region J_setCustomGridColumn [1]
        public void J_setCustomGridColumn(ref DGControl.DGControl dgControl, string SqlText, string[,] GridColumns)
        {
            if (this.J_DataSet != null) this.J_DataSet.Clear();
            this.J_DataSet = this.J_ExecSqlReturnDataSet(SqlText);
            this.J_setCustomGridColumn(ref dgControl, this.J_DataSet, GridColumns);
        }
        #endregion

        #region J_setCustomGridColumn [2]
        public void J_setCustomGridColumn(ref DGControl.DGControl dgControl, DataSet dataset, string[,] GridColumns)
        {
            DataGridTableStyle dgtsTableStyle = new DataGridTableStyle();
            DataGridTextBoxColumn[] dgTextBoxColumn = new DataGridTextBoxColumn[GridColumns.GetUpperBound(0) + 1];
            dgtsTableStyle.MappingName = dataset.Tables[0].TableName;
            
            for (int intCounter = 0; intCounter <= GridColumns.GetUpperBound(0); intCounter++)
            {
                dgTextBoxColumn[intCounter] = new DataGridTextBoxColumn();
                dgTextBoxColumn[intCounter].MappingName = dataset.Tables[0].Columns[intCounter].ColumnName;
                dgTextBoxColumn[intCounter].HeaderText = GridColumns[intCounter, (int)J_GridColumnSetting.HeaderText];
                dgTextBoxColumn[intCounter].Width = int.Parse(GridColumns[intCounter, (int)J_GridColumnSetting.Width]);
                
                if (GridColumns[intCounter, (int)J_GridColumnSetting.Format].Trim() != "")
                    dgTextBoxColumn[intCounter].Format = GridColumns[intCounter, (int)J_GridColumnSetting.Format];

                if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim() == "")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Left;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "L")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Left;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "LEFT")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Left;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "C")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Center;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "CENTER")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Center;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "R")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Right;
                else if (GridColumns[intCounter, (int)J_GridColumnSetting.Alignment].Trim().ToUpper() == "RIGHT")
                    dgTextBoxColumn[intCounter].Alignment = HorizontalAlignment.Right;
                
                if (GridColumns[intCounter, (int)J_GridColumnSetting.NullToText].Trim() == "")
                    dgTextBoxColumn[intCounter].NullText = "";
                
                dgtsTableStyle.GridColumnStyles.Add(dgTextBoxColumn[intCounter]);
            }
            dgControl.TableStyles.Clear();
            dgControl.TableStyles.Add(dgtsTableStyle);
        }
        #endregion

        #endregion

        #region SET DG GRID POSITION [ OVERLOADED METHOD ]

        #region J_setGridPosition [1]
        public bool J_setGridPosition(ref DGControl.DGControl dgControl, DataSet dataset, string IdFieldName, long IdValue)
        {
            long SearchValue = 0;
            return this.J_setGridPosition(ref dgControl, dataset, IdFieldName, IdValue, out SearchValue);
        }
        #endregion

        #region J_setGridPosition [2]
        public bool J_setGridPosition(ref DGControl.DGControl dgControl, DataSet dataset, string IdFieldName, long IdValue, out long SearchValue)
        {
            SearchValue = 0;
            int intCounter = 0;

            for (int iCounter = 0; iCounter <= dataset.Tables[0].Rows.Count - 1; iCounter++)
            {
                if (commonservice.J_ReturnInt64Value(dataset.Tables[0].Rows[iCounter][IdFieldName].ToString()) == IdValue)
                {
                    intCounter = iCounter;
                    break;
                }
            }
            if (commonservice.J_ReturnInt64Value(dgControl.CurrentRowIndex.ToString()) < 0) return false;
            
            if (dgControl.IsSelected(dgControl.CurrentRowIndex) == true)
            {
                dgControl.UnSelect(dgControl.CurrentRowIndex);
                dgControl.CurrentRowIndex = intCounter;
                dgControl.Select(dgControl.CurrentRowIndex);
            }
            else
            {
                dgControl.CurrentRowIndex = intCounter;
                dgControl.Select(dgControl.CurrentRowIndex);
            }

            dgControl.Refresh();
            dgControl.Select();
            dgControl.Focus();

            SearchValue = commonservice.J_ReturnInt64Value(dgControl[dgControl.CurrentRowIndex, 0].ToString());
            return true;
        }
        #endregion

        #endregion

        #region MAXIMUM DATA LENGTH [ OVERLOADED METHOD ]

        #region J_ReturnMaxDataLength [1]
        public int J_ReturnMaxDataLength(string TableName, string ColumnName)
        {
            return this.J_ReturnMaxDataLength(null, TableName, ColumnName, null);
        }
        #endregion

        #region J_ReturnMaxDataLength [2]
        public int J_ReturnMaxDataLength(IDbCommand command, string TableName, string ColumnName)
        {
            return this.J_ReturnMaxDataLength(command, TableName, ColumnName, null);
        }
        #endregion

        #region J_ReturnMaxDataLength [3]
        public int J_ReturnMaxDataLength(string TableName, string ColumnName, string OtherCriteria)
        {
            return this.J_ReturnMaxDataLength(null, TableName, ColumnName, OtherCriteria);
        }
        #endregion

        #region J_ReturnMaxDataLength [4]
        public int J_ReturnMaxDataLength(IDbCommand command, string TableName, string ColumnName, string OtherCriteria)
        {
            strSQL = "SELECT MAX(LEN(" + ColumnName + ")) AS MAX_VALUE " +
                     "FROM   " + TableName + "";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria;

            if (command == null)
                return (int)commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL));
            else if (command != null)
                return (int)commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL));

            return 0;
        }
        #endregion

        #endregion

        #region MINIMUM DATA LENGTH [ OVERLOADED METHOD ]

        #region J_ReturnMinDataLength [1]
        public int J_ReturnMinDataLength(string TableName, string ColumnName)
        {
            return this.J_ReturnMinDataLength(null, TableName, ColumnName, null);
        }
        #endregion

        #region J_ReturnMinDataLength [2]
        public int J_ReturnMinDataLength(IDbCommand command, string TableName, string ColumnName)
        {
            return this.J_ReturnMinDataLength(command, TableName, ColumnName, null);
        }
        #endregion

        #region J_ReturnMinDataLength [3]
        public int J_ReturnMinDataLength(string TableName, string ColumnName, string OtherCriteria)
        {
            return this.J_ReturnMinDataLength(null, TableName, ColumnName, OtherCriteria);
        }
        #endregion

        #region J_ReturnMinDataLength [4]
        public int J_ReturnMinDataLength(IDbCommand command, string TableName, string ColumnName, string OtherCriteria)
        {
            strSQL = "SELECT MIN(LEN(" + ColumnName + ")) AS MIN_VALUE " +
                     "FROM   " + TableName + "";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria;

            if (command == null)
                return (int)commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL));
            else if (command != null)
                return (int)commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL));

            return 0;
        }
        #endregion

        #endregion

        #region IMPORT DATA [ OVERLOADED METHOD ]

        #region J_ImportData [1]
        public bool J_ImportData(string TableName, string FilePath)
        {
            return this.J_ImportData(TableName, FilePath, J_TextSeparator.Pipe, "\n");
        }
        #endregion

        #region J_ImportData [2]
        public bool J_ImportData(string TableName, string FilePath, string TextSeparator)
        {
            return this.J_ImportData(TableName, FilePath, TextSeparator, "\n");
        }
        #endregion

        #region J_ImportData [3]
        public bool J_ImportData(string TableName, string FilePath, string TextSeparator, string NewLine)
        {
            if (this.J_IsDatabaseObjectExist(TableName) == false)
            {
                commonservice.J_UserMessage("Table Name does not exist.\n\nTable Name : " + TableName);
                return false;
            }

            if (FilePath == "" || FilePath == null)
            {
                commonservice.J_UserMessage("File Path should not be blank");
                return false;
            }

            if (this.enmDatabaseType == J_DatabaseType.MsAccess)
                strSQL = "INSERT INTO " + TableName + " SELECT * FROM [TEXT;DATABASE=" + commonservice.J_GetDirectoryName(FilePath) + ";].[" + commonservice.J_GetFileName(FilePath) + "];";
            else if (this.enmDatabaseType == J_DatabaseType.SqlServer)
                strSQL = "BULK INSERT " + TableName + " FROM '" + FilePath + "' " +
                         "WITH (FIELDTERMINATOR = '" + TextSeparator + "', ROWTERMINATOR   = '" + NewLine + "')";

            if (this.J_ExecSql(strSQL) == false) return false;
            return true;
        }
        #endregion

        #endregion
        
        #region J_ConcateColumns
        /// <summary>
        /// Concate of columns
        /// [
        /// string Table Name | 
        /// string Column Name | 
        /// string Column Data Type (T => Text; N => Integer or Long; C => Currency; D => Date) |
        /// int Space Between Columns
        /// ]
        /// </summary>
        /// <param name="Columns"> Pass parameters as array [Table Name, Column Name, Column Data Type, Space Between Columns] </param>
        /// <returns> It returns string value </returns>
        public string J_ConcateColumns(string[,] Columns)
        {
            if (Columns == null) return "";

            string strConcateColumn = "";
            int intMaxLength = 0;

            for (int iCounter = 0; iCounter <= Columns.GetUpperBound(0); iCounter++)
            {
                if (iCounter >= 1)
                    strConcateColumn += commonservice.J_ConcatOperator() +
                        " SPACE(" + intMaxLength + " - LEN(" + Convert.ToString(Columns[iCounter - 1, 1]) + ") + " + Convert.ToString(Columns[iCounter, 3]) + ") ";

                // FOR VARCHAR/TEXT TYPE DATA
                if (Convert.ToString(Columns[iCounter, 2]).ToUpper() == "T")
                {
                    if (iCounter == 0)
                        strConcateColumn += commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.String, J_SQLColFormat.NullCheck) + " ";
                    else if (iCounter >= 1)
                        strConcateColumn += commonservice.J_ConcatOperator() + " " + commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.String, J_SQLColFormat.NullCheck) + " ";
                }

                // FOR NUMERIC TYPE DATA
                if (Convert.ToString(Columns[iCounter, 2]).ToUpper() == "N")
                {
                    if (iCounter == 0)
                        strConcateColumn += commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.Integer, J_SQLColFormat.Cast) + " ";
                    else if (iCounter >= 1)
                        strConcateColumn += commonservice.J_ConcatOperator() + " " + commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.Integer, J_SQLColFormat.Cast) + " ";
                }

                // FOR CURRENCY / DECIMAL TYPE DATA
                if (Convert.ToString(Columns[iCounter, 2]).ToUpper() == "C")
                {
                    if (iCounter == 0)
                        strConcateColumn += commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.Double, J_SQLColFormat.Cast) + " ";
                    else if (iCounter >= 1)
                        strConcateColumn += commonservice.J_ConcatOperator() + " " + commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_ColumnType.Double, J_SQLColFormat.Cast) + " ";
                }

                // FOR DATE TYPE DATA
                if (Convert.ToString(Columns[iCounter, 2]).ToUpper() == "D")
                {
                    if (iCounter == 0)
                        strConcateColumn += commonservice.J_SQLDBFormat(commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_SQLColFormat.DateFormatDDMMYYYY), J_ColumnType.Date, J_SQLColFormat.Cast) + " ";
                    else if (iCounter >= 1)
                        strConcateColumn += commonservice.J_ConcatOperator() + " " + commonservice.J_SQLDBFormat(commonservice.J_SQLDBFormat(Convert.ToString(Columns[iCounter, 1]), J_SQLColFormat.DateFormatDDMMYYYY), J_ColumnType.Date, J_SQLColFormat.Cast) + " ";
                }

                intMaxLength = this.J_ReturnMaxDataLength(Convert.ToString(Columns[iCounter, 0]), Convert.ToString(Columns[iCounter, 1]));
            }
            return strConcateColumn;
        }
        #endregion

        #region RETURN NO OF ROWS [ OVERLOADED METHOD ]

        #region J_ReturnNoOfRows [1]
        public long J_ReturnNoOfRows(string TableName)
        {
            return this.J_ReturnNoOfRows(null, TableName, null);
        }
        #endregion

        #region J_ReturnNoOfRows [2]
        public long J_ReturnNoOfRows(IDbCommand command, string TableName)
        {
            return this.J_ReturnNoOfRows(command, TableName, null);
        }
        #endregion

        #region J_ReturnNoOfRows [3]
        public long J_ReturnNoOfRows(string TableName, string OtherCriteria)
        {
            return this.J_ReturnNoOfRows(null, TableName, OtherCriteria);
        }
        #endregion

        #region J_ReturnNoOfRows [4]
        public long J_ReturnNoOfRows(IDbCommand command, string TableName, string OtherCriteria)
        {
            strSQL = "SELECT COUNT(*) " +
                     "FROM   " + TableName + " ";
            if (OtherCriteria != "" && OtherCriteria != null)
                strSQL = strSQL + "WHERE " + OtherCriteria + " ";

            if (command == null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(strSQL));
            else if (command != null)
                return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, strSQL));
            return 0;
        }
        #endregion

        #region J_ReturnNoOfRows [5]
        public long J_ReturnNoOfRows(string sqlText, J_QueryType QueryType)
        {
            return this.J_ReturnNoOfRows(null, sqlText, QueryType);
        }
        #endregion

        #region J_ReturnNoOfRows [6]
        public long J_ReturnNoOfRows(IDbCommand command, string sqlText, J_QueryType QueryType)
        {
            if (QueryType == J_QueryType.DirectQuery)
            {
                if(command == null)
                    return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(sqlText));
                else if (command != null)
                    return commonservice.J_NullToZero(this.J_ExecSqlReturnScalar(command, sqlText));
                return 0;
            }
            return 0;
        }
        #endregion

        #endregion

        #region GET DAY NAME [ OVERLOADED METHOD ]

        #region J_GetDayName [1]
        public string J_GetDayName(string date)
        {
            return this.J_GetDayName(null, date, this.enmDatabaseType);
        }
        #endregion

        #region J_GetDayName [2]
        public string J_GetDayName(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_GetDayName(null, DateMaskedTextBox.Text, this.enmDatabaseType);
        }
        #endregion

        #region J_GetDayName [3]
        public string J_GetDayName(string date, J_DatabaseType dbType)
        {
            return this.J_GetDayName(null, date, dbType);
        }
        #endregion

        #region J_GetDayName [4]
        public string J_GetDayName(MaskedTextBox DateMaskedTextBox, J_DatabaseType dbType)
        {
            return this.J_GetDayName(null, DateMaskedTextBox.Text, dbType);
        }
        #endregion

        #region J_GetDayName [5]
        public string J_GetDayName(IDbCommand command, string date)
        {
            return this.J_GetDayName(command, date, this.enmDatabaseType);
        }
        #endregion

        #region J_GetDayName [6]
        public string J_GetDayName(IDbCommand command, MaskedTextBox DateMaskedTextBox)
        {
            return this.J_GetDayName(command, DateMaskedTextBox.Text, this.enmDatabaseType);
        }
        #endregion

        #region J_GetDayName [7]
        public string J_GetDayName(IDbCommand command, MaskedTextBox DateMaskedTextBox, J_DatabaseType dbType)
        {
            return this.J_GetDayName(command, DateMaskedTextBox.Text, dbType);
        }
        #endregion

        #region J_GetDayName [8]
        public string J_GetDayName(IDbCommand command, string date, J_DatabaseType dbType)
        {
            switch (dbType)
            {
                case J_DatabaseType.SqlServer:
                    this.strSQL = "SELECT DATENAME(dw, '" + dtservice.J_ConvertyyyyMMdd(date) + "')";
                    break;
                case J_DatabaseType.Oracle:
                    this.strSQL = "SELECT DATENAME(dw, '" + dtservice.J_ConvertyyyyMMdd(date) + "')";
                    break;
                case J_DatabaseType.MsAccess:
                    this.strSQL = "SELECT Format('" + dtservice.J_ConvertyyyyMMdd(date) + "','dddd')";
                    break;
            }

            if (command == null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(this.strSQL));
            else if (command != null)
                return commonservice.J_NullToText(this.J_ExecSqlReturnScalar(command, this.strSQL));

            return "";
        }
        #endregion

        #endregion


        #region J_BulkCopyToSqlDatabase [OVERLOADED METHOD]

        #region J_BulkCopyToSqlDatabase [1]
        public bool J_BulkCopyToSqlDatabase(DataTable SourceDataTable, string DestinationTableName, bool UseSourceIndentity)
        {
            return J_BulkCopyToSqlDatabase(SourceDataTable, null, DestinationTableName, UseSourceIndentity, false);
        }
        #endregion

        #region J_BulkCopyToSqlDatabase [2]
        public bool J_BulkCopyToSqlDatabase(DataTable SourceDataTable, J_ColumnMapping[] columnParameter, string DestinationTableName, bool UseSourceIndentity)
        {
            return J_BulkCopyToSqlDatabase(SourceDataTable, columnParameter, DestinationTableName, UseSourceIndentity, false);
        }
        #endregion

        #region J_BulkCopyToSqlDatabase [3]
        public bool J_BulkCopyToSqlDatabase(DataTable SourceDataTable, J_ColumnMapping[] columnParameter, string DestinationTableName)
        {
            return J_BulkCopyToSqlDatabase(SourceDataTable, columnParameter, DestinationTableName, false, false);
        }
        #endregion

        #region J_BulkCopyToSqlDatabase [4]
        private bool J_BulkCopyToSqlDatabase(DataTable SourceDataTable, J_ColumnMapping[] columnParameter, string DestinationTableName, bool bnlUseSourceIdentity, bool bnlExternalTransaction)
        {
            bool bnlStatus = true;
            try
            {
                /* Set up the bulk copy object.Note that when specifying the UseInternalTransaction option, you cannot also specify an external transaction.
                 Therefore, you must use the SqlBulkCopy construct that requires a string for the connection, rather than an existing SqlConnection object. */

                SqlBulkCopy bulkCopy;

                if (bnlExternalTransaction)
                {
                    if (bnlUseSourceIdentity)
                        bulkCopy = new SqlBulkCopy((SqlConnection)this.J_IdbConn, SqlBulkCopyOptions.KeepIdentity, (SqlTransaction)this.J_IdbTran);
                    else
                        bulkCopy = new SqlBulkCopy((SqlConnection)this.J_IdbConn, SqlBulkCopyOptions.Default, (SqlTransaction)this.J_IdbTran);
                }
                else
                {
                    if (bnlUseSourceIdentity)
                        bulkCopy = new SqlBulkCopy(this.J_pConnectionString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction);
                    else
                        bulkCopy = new SqlBulkCopy(this.J_pConnectionString, SqlBulkCopyOptions.Default | SqlBulkCopyOptions.UseInternalTransaction);

                }

                if (columnParameter != null)
                {
                    for (int iCounter = 0; iCounter < columnParameter.Length; iCounter++)
                    {
                        J_ColumnMapping oColumn = (J_ColumnMapping)columnParameter[iCounter];
                        SqlBulkCopyColumnMapping Colmapping = new SqlBulkCopyColumnMapping(oColumn.SourceName, oColumn.Destination);
                        bulkCopy.ColumnMappings.Add(Colmapping);
                    }
                }

                bulkCopy.BatchSize = 1000;
                bulkCopy.BulkCopyTimeout = 30;
                bulkCopy.DestinationTableName = DestinationTableName;

                bulkCopy.WriteToServer(SourceDataTable);
                
            }
            catch (Exception err)
            {

                //if (bnlExternalTransaction)
                //{

                //}
                bnlStatus = false;
                throw err;
            }
            return bnlStatus;
        }
        #endregion

        #endregion


        #region CREATE TABLE [OVERLOADED METHOD]

        #region J_CreateTable [1]
        public bool J_CreateTable(string QueryString, string TableName)
        {
            return this.J_CreateTable(null, this.J_ExecSqlReturnDataTable(QueryString), TableName);
        }
        #endregion

        #region J_CreateTable [2]
        public bool J_CreateTable(DataTable datatable, string TableName)
        {
            if (datatable == null) return false;
            return this.J_CreateTable(null, datatable, TableName);
        }
        #endregion

        #region J_CreateTable [3]
        public bool J_CreateTable(IDbCommand command, string QueryString, string TableName)
        {
            if (command == null)
                return this.J_CreateTable(this.J_ExecSqlReturnDataTable(QueryString), TableName);
            else if (command != null)
                return this.J_CreateTable(command, this.J_ExecSqlReturnDataTable(command, QueryString), TableName);
            return false;
        }
        #endregion

        #region J_CreateTable [4]
        public bool J_CreateTable(IDbCommand command, DataTable datatable, string TableName)
        {
            if (datatable == null) return false;
            string strColumnName = "";
            try
            {
                strSQL = "CREATE TABLE " + TableName + "(";
                foreach (DataColumn column in datatable.Columns)
                {
                    if (column.AutoIncrement)
                        strColumnName += commonservice.J_GetDataType(column.ColumnName, J_Identity.YES) + ",";
                    else
                    {
                        switch (column.DataType.Name)
                        {
                            case J_DataTableDataType.Integer:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.Integer) + ",";
                                break;
                            case J_DataTableDataType.Long:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.Long) + ",";
                                break;
                            case J_DataTableDataType.Decimal:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.Double) + ",";
                                break;
                            case J_DataTableDataType.String:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.String) + ",";
                                break;
                            case J_DataTableDataType.DateTime:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.DateTime) + ",";
                                break;
                            case J_DataTableDataType.Byte:
                                strColumnName += column.ColumnName + " " + commonservice.J_GetColumnDataType(column.DataType.Name.ToString()) + ",";
                                break;
                            case J_DataTableDataType.Boolean:
                                strColumnName += column.ColumnName + " " + commonservice.J_GetColumnDataType(column.DataType.Name.ToString()) + ",";
                                break;
                            case J_DataTableDataType.Currency:
                                strColumnName += commonservice.J_GetDataType(column.ColumnName, J_ColumnType.Double) + ",";
                                break;
                            case J_DataTableDataType.Binary:
                                strColumnName += column.ColumnName + " " + commonservice.J_GetColumnDataType(column.DataType.Name.ToString()) + ",";
                                break;
                        }
                    }
                }

                strColumnName = strColumnName.Substring(0, strColumnName.Length - 1);
                strColumnName += ")";
                strSQL += strColumnName;

                if (command == null)
                {
                    if (this.J_ExecSql(strSQL, J_SQLType.DDL) == true) return true;
                }
                else if (command != null)
                {
                    if (this.J_ExecSql(command, strSQL, J_SQLType.DDL) == true) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region EXPORT FROM SQL SERVER TO MS ACCESS DATABASE [OVERLOADED METHOD]

        #region J_ExportSQLServerToMSAccess [1]
        public void J_ExportSQLServerToMSAccess(string MSAccessDatabaseFileWithPath, string SourceQuery, string DestinationQuery)
        {
            this.J_ExportSQLServerToMSAccess(null, MSAccessDatabaseFileWithPath, "", SourceQuery, DestinationQuery);
        }
        #endregion

        #region J_ExportSQLServerToMSAccess [2]
        public void J_ExportSQLServerToMSAccess(string MSAccessDatabaseFileWithPath, string Password, string SourceQuery, string DestinationQuery)
        {
            this.J_ExportSQLServerToMSAccess(null, MSAccessDatabaseFileWithPath, Password, SourceQuery, DestinationQuery);
        }
        #endregion

        #region J_ExportSQLServerToMSAccess [3]
        public void J_ExportSQLServerToMSAccess(IDbCommand command, string MSAccessDatabaseFileWithPath, string SourceQuery, string DestinationQuery)
        {
            this.J_ExportSQLServerToMSAccess(command, MSAccessDatabaseFileWithPath, "", SourceQuery, DestinationQuery);
        }
        #endregion

        #region J_ExportSQLServerToMSAccess [4]
        public void J_ExportSQLServerToMSAccess(IDbCommand command, string MSAccessDatabaseFileWithPath, string Password, string SourceQuery, string DestinationQuery)
        {
            if (enmDatabaseType == J_DatabaseType.SqlServer)
            {
                if (command == null)
                {
                    this.J_ExecSql("EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'Ad Hoc Distributed Queries', 1; RECONFIGURE");
                    strSQL = @"INSERT INTO OPENROWSET('MICROSOFT.JET.OLEDB.4.0',';DATABASE=" + MSAccessDatabaseFileWithPath + ";PWD=" + Password + "'," +
                              "'" + DestinationQuery + "') " + SourceQuery;
                    this.J_ExecSql(strSQL);
                }
                else if (command != null)
                {
                    this.J_ExecSql(command, "EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'Ad Hoc Distributed Queries', 1; RECONFIGURE");
                    strSQL = @"INSERT INTO OPENROWSET('MICROSOFT.JET.OLEDB.4.0',';DATABASE=" + MSAccessDatabaseFileWithPath + ";PWD=" + Password + "'," +
                              "'" + DestinationQuery + "') " + SourceQuery;
                    this.J_ExecSql(command, strSQL);
                }
            }
        }
        #endregion

        #endregion

        #region CREATE SCHEMA FILE [OVERLOADED METHOD]

        #region J_CreateSchemaFile [1]
        public bool J_CreateSchemaFile(string SchemaFolderPath, string TableName, string TableColumns)
        {
            return this.J_CreateSchemaFile(SchemaFolderPath, TableName, TableColumns, J_TextSeparator.Pipe, J_TextQualifier.None, "MM/dd/yyyy", false);
        }
        #endregion

        #region J_CreateSchemaFile [2]
        public bool J_CreateSchemaFile(string SchemaFolderPath, string TableName, string TableColumns, string Delimiter)
        {
            return this.J_CreateSchemaFile(SchemaFolderPath, TableName, TableColumns, Delimiter, J_TextQualifier.None, "MM/dd/yyyy", false);
        }
        #endregion

        #region J_CreateSchemaFile [3]
        public bool J_CreateSchemaFile(string SchemaFolderPath, string TableName, string TableColumns, string Delimiter, string Qualifier)
        {
            return this.J_CreateSchemaFile(SchemaFolderPath, TableName, TableColumns, Delimiter, Qualifier, "MM/dd/yyyy", false);
        }
        #endregion

        #region J_CreateSchemaFile [4]
        public bool J_CreateSchemaFile(string SchemaFolderPath, string TableName, string TableColumns, string Delimiter, string Qualifier, string DateTimeFormat)
        {
            return this.J_CreateSchemaFile(SchemaFolderPath, TableName, TableColumns, Delimiter, Qualifier, DateTimeFormat, false);
        }
        #endregion

        #region J_CreateSchemaFile [5]
        public bool J_CreateSchemaFile(string SchemaFolderPath, string TableName, string TableColumns, string Delimiter, string Qualifier, string DateTimeFormat, bool Header)
        {
            try
            {
                if (string.IsNullOrEmpty(DateTimeFormat))
                    DateTimeFormat = "MM/dd/yyyy";

                DataTable datatable = this.J_ExecSqlReturnDataTable("SELECT TOP 1 " + TableColumns + " FROM " + TableName);
                StreamWriter streamWriter = commonservice.J_ReturnStreamWriter(SchemaFolderPath + "\\schema.ini");

                commonservice.J_WriteLine(ref streamWriter, "", J_NewLine.YES, 2);
                commonservice.J_WriteLine(ref streamWriter, "[" + TableName + ".txt]", J_NewLine.YES, 2);

                if (Header)
                    commonservice.J_WriteLine(ref streamWriter, "ColNameHeader=True", J_NewLine.NO);
                else
                    commonservice.J_WriteLine(ref streamWriter, "ColNameHeader=false", J_NewLine.NO);

                commonservice.J_WriteLine(ref streamWriter, "CharacterSet=1252", J_NewLine.NO);
                commonservice.J_WriteLine(ref streamWriter, "Format=Delimited(" + Delimiter + ")", J_NewLine.NO);
                commonservice.J_WriteLine(ref streamWriter, "TextDelimiter=" + Qualifier, J_NewLine.NO);
                commonservice.J_WriteLine(ref streamWriter, "DateTimeFormat=" + DateTimeFormat, J_NewLine.YES, 2);

                //----WRITE COLUMN DETAILS 
                int intCounter = 1;
                string strDesc = "";

                foreach (DataColumn column in datatable.Columns)
                {
                    switch (column.DataType.Name)
                    {
                        case J_DataTableDataType.Integer:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Long:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.Decimal:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Integer";
                            break;
                        case J_DataTableDataType.String:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Char Width 1000";
                            break;
                        case J_DataTableDataType.DateTime:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Date";
                            break;
                        case J_DataTableDataType.Byte:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Byte";
                            break;
                        case J_DataTableDataType.Boolean:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Bit";
                            break;
                        case J_DataTableDataType.Currency:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " Currency";
                            break;
                        case J_DataTableDataType.Binary:
                            strDesc = "Col" + intCounter + "=" + column.ColumnName + " OLE";
                            break;
                    }
                    commonservice.J_WriteLine(ref streamWriter, strDesc, J_NewLine.NO);
                    intCounter++;
                }

                streamWriter.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region EXPORT [OVERLOADED METHOD]

        #region J_Export [1]
        public bool J_Export(string ExportFolderPath, string SourceTableName)
        {
            return this.ExportFileEngine(ExportFolderPath, J_ExportFileType.TextFile, SourceTableName, J_TextSeparator.Pipe, J_TextQualifier.None);
        }
        #endregion

        #region J_Export [2]
        public bool J_Export(string ExportFolderPath, string SourceTableName, string ExportColumns)
        {
            return this.ExportFileEngine(ExportFolderPath, J_ExportFileType.TextFile, SourceTableName, ExportColumns, J_TextSeparator.Pipe, J_TextQualifier.None);
        }
        #endregion

        #region J_Export [3]
        public bool J_Export(string ExportFolderPath, string SourceTableName, string ExportColumns, string Criteria)
        {
            return this.ExportFileEngine(ExportFolderPath, J_ExportFileType.TextFile, SourceTableName, ExportColumns, Criteria, J_TextSeparator.Pipe, J_TextQualifier.None);
        }
        #endregion

        #endregion

        #region RESTORE [OVERLOADED METHOD]

        #region J_Restore [1]
        public bool J_Restore(string FilePath, string TableName)
        {
            return this.RestoreFileEngine(FilePath, TableName);
        }
        #endregion

        #region J_Restore [2]
        public bool J_Restore(string FilePath, string TableName, string SourceTableColumns, string DestinationTableColumns)
        {
            return this.RestoreFileEngine(FilePath, TableName, SourceTableColumns, DestinationTableColumns);
        }
        #endregion

        #region J_Restore [3]
        public bool J_Restore(string FilePath, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria)
        {
            return this.RestoreFileEngine(FilePath, TableName, SourceTableColumns, DestinationTableColumns, Criteria);
        }
        #endregion

        #region J_Restore [4]
        public bool J_Restore(string FilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria)
        {
            return this.RestoreFileEngine(FilePath, FileType, TableName, SourceTableColumns, DestinationTableColumns, Criteria);
        }
        #endregion

        #region J_Restore [5]
        public bool J_Restore(string FilePath, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter)
        {
            return this.RestoreFileEngine(FilePath, J_RestoreFileType.TextFile, TableName, SourceTableColumns, DestinationTableColumns, Criteria, Delimiter);
        }
        #endregion

        #region J_Restore [6]
        public bool J_Restore(string FilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter)
        {
            return this.RestoreFileEngine(FilePath, FileType, TableName, SourceTableColumns, DestinationTableColumns, Criteria, Delimiter);
        }
        #endregion

        #region J_Restore [7]
        public bool J_Restore(string FilePath, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter, out int intNoOfRows)
        {
            return this.RestoreFileEngine(FilePath, J_RestoreFileType.TextFile, TableName, SourceTableColumns, DestinationTableColumns, Criteria, Delimiter, out intNoOfRows);
        }
        #endregion

        #region J_Restore [8]
        public bool J_Restore(string FilePath, J_RestoreFileType FileType, string TableName, string SourceTableColumns, string DestinationTableColumns, string Criteria, string Delimiter, out int intNoOfRows)
        {
            return this.RestoreFileEngine(FilePath, FileType, TableName, SourceTableColumns, DestinationTableColumns, Criteria, Delimiter, out intNoOfRows);
        }
        #endregion

        #endregion


        #region GET IMPORT DESTINATION TABLE COLUMNS

        #region J_GetImportDestinationTableColumns [1]
        public string J_GetImportDestinationTableColumns(string TableName)
        {
            return this.J_GetImportDestinationTableColumns(TableName, "*");
        }
        #endregion

        #region J_GetImportDestinationTableColumns [2]
        public string J_GetImportDestinationTableColumns(string TableName, string TableColumns)
        {
            DataTable datatable = this.J_ExecSqlReturnDataTable("SELECT TOP 1 " + TableColumns + " FROM " + TableName);
            strSQL = "";
            foreach (DataColumn column in datatable.Columns)
                strSQL += column.ColumnName + ",";
            return commonservice.J_Left(strSQL, strSQL.Length - 1);
        }
        #endregion

        #endregion

        #region GET IMPORT SOURCE TABLE COLUMNS

        #region J_GetImportSourceTableColumns [1]
        public string J_GetImportSourceTableColumns(string QueryString)
        {
            return this.J_GetImportSourceTableColumns(QueryString, false);
        }
        #endregion

        #region J_GetImportSourceTableColumns [2]
        public string J_GetImportSourceTableColumns(string QueryString, bool IncludeNullCode)
        {
            DataTable datatable = this.J_ExecSqlReturnDataTable(QueryString);
            strSQL = "";
            if (IncludeNullCode == false)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    strSQL += column.ColumnName + ",";
                }
            }
            else if (IncludeNullCode == true)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    switch (column.DataType.Name)
                    {
                        case J_DataTableDataType.Integer:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Long:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Decimal:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.String:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.DateTime:
                            strSQL += column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Byte:
                            strSQL += column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Boolean:
                            strSQL += column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Currency:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Binary:
                            strSQL += column.ColumnName + ",";
                            break;
                    }
                }
            }
            return commonservice.J_Left(strSQL, strSQL.Length - 1);
        }
        #endregion

        #endregion

        #region GET EXPORT SOURCE TABLE COLUMNS

        #region J_GetExportSourceTableColumns [1]
        public string J_GetExportSourceTableColumns(string TableName)
        {
            return this.J_GetExportSourceTableColumns(TableName, "*", false);
        }
        #endregion

        #region J_GetExportSourceTableColumns [2]
        public string J_GetExportSourceTableColumns(string TableName, bool IncludeNullCode)
        {
            return this.J_GetExportSourceTableColumns(TableName, "*", IncludeNullCode);
        }
        #endregion

        #region J_GetExportSourceTableColumns [3]
        public string J_GetExportSourceTableColumns(string TableName, string TableColumns)
        {
            return this.J_GetExportSourceTableColumns(TableName, TableColumns, false);
        }
        #endregion

        #region J_GetExportSourceTableColumns [4]
        public string J_GetExportSourceTableColumns(string TableName, string TableColumns, bool IncludeNullCode)
        {
            DataTable datatable = this.J_ExecSqlReturnDataTable("SELECT TOP 1 " + TableColumns + " FROM " + TableName);
            strSQL = "";
            if (IncludeNullCode == false)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    if(column.DataType.Name == J_DataTableDataType.DateTime)
                        strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_SQLColFormat.DateFormatMMDDYYYY) + "AS " + column.ColumnName + ",";
                    else if(column.DataType.Name != J_DataTableDataType.DateTime)
                        strSQL += column.ColumnName + ",";
                }
            }
            else if (IncludeNullCode == true)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    switch (column.DataType.Name)
                    {
                        case J_DataTableDataType.Integer:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Integer, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Long:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Long, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Decimal:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.String:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.DateTime:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_SQLColFormat.DateFormatMMDDYYYY) + "AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Byte:
                            strSQL += column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Boolean:
                            strSQL += column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Currency:
                            strSQL += commonservice.J_SQLDBFormat(column.ColumnName, J_ColumnType.Double, J_SQLColFormat.NullCheck) + " AS " + column.ColumnName + ",";
                            break;
                        case J_DataTableDataType.Binary:
                            strSQL += column.ColumnName + ",";
                            break;
                    }
                }
            }
            return commonservice.J_Left(strSQL, strSQL.Length - 1);
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region USER DEFINE PROPERTIES

        #region APPLICATION TYPE
        public J_ApplicationType J_pApplicationType
        {
            get
            {
                return this.enmApplicationType;
            }
            set
            {
                this.enmApplicationType = value;
            }
        }
        #endregion

        #region DATABASE TYPE
        public J_DatabaseType J_pDatabaseType
        {
            get
            {
                return this.enmDatabaseType;
            }
            set
            {
                this.enmDatabaseType = value;
            }
        }
        #endregion

        #region CONNECTION PROVIDER TYPE
        public J_ConnectionProviderType J_pConnectionProviderType
        {
            get
            {
                return this.enmConnectionProviderType;
            }
            set
            {
                this.enmConnectionProviderType = value;
            }
        }
        #endregion

        #region CONNECTED
        public bool J_pConnected
        {
            get
            {
                return this.J_blnConnected;
            }
            set
            {
                this.J_blnConnected = value;
            }
        }
        #endregion

        #region CONNECTION STRING
        public string J_pConnectionString
        {
            get
            {
                return this.J_strConnectionString;
            }
            set
            {
                this.J_strConnectionString = value;
            }
        }
        #endregion

        #region DATABASE NAME
        public string J_pDatabaseName
        {
            get
            {
                return this.J_strDataBase;
            }
            set
            {
                this.J_strDataBase = value;
            }
        }
        #endregion



        #region CONNECTION
        public IDbConnection J_pConnection
        {
            get
            {
                return this.J_IdbConn;
            }
        }
        #endregion

        #region ADODB CONNECTION
        public ADODB.Connection J_pADODBConnection
        {
            get
            {
                return this.J_ADODBConn;
            }
        }
        #endregion

        #region COMMAND
        public IDbCommand J_pCommand
        {
            get
            {
                return this.J_IdbCommand;
            }
        }
        #endregion

        #region ADODB RECORDSET
        public ADODB.Recordset J_pADODBRecordset
        {
            get
            {
                return this.J_ADODBRecordset;
            }
        }
        #endregion

        #region PROVIDER
        public string J_pProvider
        {
            get
            {
                switch (this.enmDatabaseType)
                {
                    case J_DatabaseType.MsAccess:
                        switch (this.enmOSType)
                        {
                            case J_OSType._32Bit:
                                return "Microsoft.Jet.OLEDB.4.0";
                            case J_OSType._64Bit:
                                return "Microsoft.Ace.OLEDB.12.0";
                        }
                        break;
                    case J_DatabaseType.SqlServer:
                        break;
                    case J_DatabaseType.Oracle:
                        break;
                    
                }
                return "";
            }
        }
        #endregion


        
        #endregion


    }
}
