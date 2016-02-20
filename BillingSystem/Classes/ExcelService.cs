#region Refered Namespaces & Classes

    using System;
    using System.Threading;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    using System.Data;
    using System.Data.SqlClient;

    using System.IO;
    using System.Diagnostics;

    using System.Runtime.InteropServices;

    using System.Windows.Forms;

    using BillingSystem.Classes;

#endregion


namespace BillingSystem.Classes
{
    public sealed class ExcelService
    {
        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        string strSQL = string.Empty;

        #endregion

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

        private J_DatabaseType enmDatabaseType;
        private J_ConnectionProviderType enmConnectionProviderType;

        private string J_strConnectionString;
        private string J_strADODBConnectionString;

        private bool J_blnDisposed;
        private bool J_blnConnected;

        #endregion

        #region USER DEFINE PUBLIC METHODS


        #region OPEN CONNECTION      
        
        #region J_OpenExcelConnection  [1]
        private bool J_OpenExcelConnection(string MSExcelDatabaseNameWithPath)
        {
            try
            {
                if ((MSExcelDatabaseNameWithPath == null) || (MSExcelDatabaseNameWithPath.Trim().Length == 0))
                    MSExcelDatabaseNameWithPath = "";

                if (Path.GetExtension(MSExcelDatabaseNameWithPath) == ".xls")
                    this.J_strConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source =" + MSExcelDatabaseNameWithPath + ";Extended Properties=Excel 8.0";
                    //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=Excel 8.0");
                else if (Path.GetExtension(MSExcelDatabaseNameWithPath) == ".xlsx")
                    this.J_strConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + MSExcelDatabaseNameWithPath + ";" +
                        " Extended Properties=Excel 12.0 Xml;";

                this.J_CloseExcelConnection();
                //this.J_IdbConn.ConnectionString = this.J_strConnectionString;

                //this.J_IdbConn.Open();
                this.J_ADODBConn.Open(this.J_strConnectionString, null, null, 0);

                this.J_IdbCommand = this.J_IdbConn.CreateCommand();
                this.J_IdbCommand.CommandTimeout = J_Var.J_pCommandTimeout;

                this.J_ADODBRecordset = new ADODB.Recordset();

                this.J_blnConnected = true;

            }
            catch(Exception E)
            {
                cmnService.J_UserMessage(E.Message);
                this.J_blnConnected = false;
            }
            return this.J_blnConnected;
        }

        #endregion

        #endregion
        
        #region CLOSE CONNECTION
        private void J_CloseExcelConnection()
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
            }
            this.J_blnConnected = false;
        }
        #endregion

        #region DISPOSE
        private void Dispose(bool Disposing)
        {
            if (!this.J_blnDisposed && Disposing)
                this.J_CloseExcelConnection();
            this.J_blnDisposed = true;
        }
        #endregion


        #region EXEC SQL RETURN READER [1]
        public IDataReader J_ExecSqlReturnReader(string SqlText, string MSExcelDatabaseNameWithPath)
        {
            return this.J_ExecSqlReturnReader(null, SqlText, CommandBehavior.Default, CommandType.Text, MSExcelDatabaseNameWithPath);
        }
        #endregion


        #region EXEC SQL RETURN READER [8]
        public IDataReader J_ExecSqlReturnReader(IDbCommand command, string SqlText, CommandBehavior commandBehavior, CommandType Type, string MSExcelDatabaseNameWithPath)
        {
            try
            {
                if (command == null)
                {
                    this.J_OpenExcelConnection(MSExcelDatabaseNameWithPath);

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
            catch
            {
                this.J_CloseExcelConnection();
                commonservice.J_UserMessage("Connection Failed", MessageBoxIcon.Stop);
                return null;
            }
        }
        #endregion

        #region PUBLIC METHODS

        #region ROLLBACK
        public void J_Rollback()
        {
            if (this.J_IdbTran != null)
            {
                this.J_IdbTran.Rollback();
                this.J_IdbTran = null;
            }
        }
        #endregion

        #region COMMIT
        public void J_Commit()
        {
            this.J_IdbTran.Commit();
            this.J_IdbTran = null;
        }
        #endregion

        #region VALIDATE CONNECTION [ OVERLOADED METHOD ]

        #region J_ValidateExcelConnection [1]
        public bool J_ValidateExcelConnection(string MSExcelFileNameWithPath)
        {
            return (this.J_blnConnected || this.J_OpenExcelConnection(MSExcelFileNameWithPath));
        }
        #endregion


        #endregion

        #endregion


        #endregion

    }
}
