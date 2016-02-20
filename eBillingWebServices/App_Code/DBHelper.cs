
#region Imported Namespace

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.IO;
  
#endregion

namespace JayaSoftwares
{
    public class DBHelper
    {
        #region DECLARATIONS
        
        private DbProviderFactory  oFactory;
        private DbConnection       oConnection;
        private ConnectionState    oConnectionState;
        public  DbCommand          oCommand;
        public DbDataAdapter oAdapter;
        private DbParameter        oParameter;
        private DbTransaction      oTransaction;
        private bool               mblTransaction;
        private ConnectionType     DbConnectionType = ConnectionType.Synchronous;
        private DataBaseType       dbType ;

        private string S_CONNECTION = "" ;
        private string S_PROVIDER = "";
        private string S_DBFILENAME = "";
        

        #endregion

        #region ENUMERATORS

        #region Enum:: ConnectionFile
        public enum DataBaseType:uint
        {
            SqlServer = 1,
            OracleServer = 2,
            AccessDB = 3,
            ExcelSheet = 4,
            FireBird = 5,
            DB2Server =6
        }
        #endregion

        #region Enum:: ConnectionType
        public enum ConnectionType : uint
        {
            Synchronous = 1,
            Asynchronous = 2,
            MultipleActiveResultSet = 3
        }

        #endregion       
       
        #region Enum:: TransactionType
        public enum TransactionType : uint
        {
            Open = 1,
            Commit = 2,
            Rollback = 3
        }

        #endregion

        #region Enum:: SchemaType
        public enum SchemaType : uint
        {
            Tables = 1,
            Columns = 2,
            DataBases = 3
        }

        #endregion





        #endregion

        #region  SetConnectionType
        public ConnectionType SetConnectionType
        {
            get
            {
                return DbConnectionType;
            }
            set
            {
                DbConnectionType = value;
            }
        }

        #endregion

        #region DBFileName
        public string DBFileName
        {
            get
            {
                return S_DBFILENAME;
            }
            set
            {
                S_DBFILENAME = value;
            }
        }

        #endregion

        #region STRUCTURES

        /// <summary>
        ///Description	    :	This function is used to Execute the Command      
        ///Input			:	
        ///OutPut			:	
        ///Comments			:	
        /// </summary>
        public struct Parameters
        {
            private string strParamName;
            private object oParamValue;
            private ParameterDirection pParamDirection;
            private int IntSize;
            public DbType DType;
            public string strColumnName;

            public Parameters(string Name, object Value, ParameterDirection Direction)
            {
                strParamName = Name;
                oParamValue = Value;
                pParamDirection = Direction;
                IntSize = 0;
                strColumnName = "";
                DType = DbType.String;

            }

            public Parameters(string Name, object Value, ParameterDirection Direction,int iSize)
            {
                strParamName = Name;
                oParamValue = Value;
                pParamDirection = Direction;
                IntSize = iSize;
                strColumnName = "";
                DType = DbType.String;
            }

            public Parameters(string Name, object Value,int iSize)
            {
                strParamName = Name;
                oParamValue = Value;
                pParamDirection = ParameterDirection.Input;
                IntSize = iSize;
                strColumnName = "";
                DType = DbType.String;
            }
            public Parameters(string Name, object Value)
            {
                strParamName = Name;
                oParamValue = Value;
                pParamDirection = ParameterDirection.Input;
                IntSize = 0;
                strColumnName = "";
                DType = DbType.String;
            }
            public Parameters(string Name, DbType Type, int intSize, string ColumnName)
            {
                strParamName = Name;
                oParamValue = null;
                pParamDirection = ParameterDirection.Input;
                DType = Type;
                IntSize = intSize;
                strColumnName = ColumnName;

            }

            public string ParamName
            {
                get
                {
                    return strParamName;
                }
                set
                {
                    strParamName = value;
                }

            }
            public object ParamValue
            {
                get
                {
                    return oParamValue;
                }
                set
                {
                    oParamValue = value;
                }

            }

            public ParameterDirection ParamDirection
            {
                get
                {
                    return pParamDirection;
                }
                set
                {
                    pParamDirection = value;
                }

            }
            public int FieldSize
            {
                get
                {
                    return IntSize;
                }
                set
                {
                    IntSize = value;
                }

            }

            public DbType DBType
            {
                get
                {
                    return DType;
                }
                set
                {
                    DType = value;
                }

            }
            public string SourceColumn
            {
                get
                {
                    return strColumnName;
                }
                set
                {
                    strColumnName = value;
                }

            }


        }


        public struct ColumnMapping
        {
            public string SourceName;
            public string Destination;

            public ColumnMapping(string SourceColumn, string DestinationColumn)
            {
                SourceName = SourceColumn;
                Destination = DestinationColumn;

            }
        }

        #endregion

        #region RetreiveConnectionString
        private string RetreiveConnectionString()
        {
            switch (dbType)
            {
                case DataBaseType.SqlServer:

                    if (DbConnectionType == ConnectionType.Synchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnSqlServer"].ConnectionString;

                    else if (DbConnectionType == ConnectionType.Asynchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnSqlServer"].ConnectionString + ";Asynchronous Processing=true";

                    else if (DbConnectionType == ConnectionType.MultipleActiveResultSet)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnSqlServer"].ConnectionString + ";MultipleActiveResultSets = True";

                    S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnSqlServer"].ProviderName;

                    break;
                case DataBaseType.OracleServer:
                    if (DbConnectionType == ConnectionType.Synchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnOracle"].ConnectionString;

                    else if (DbConnectionType == ConnectionType.Asynchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnOracle"].ConnectionString + ";Asynchronous Processing=true";

                    else if (DbConnectionType == ConnectionType.MultipleActiveResultSet)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnOracle"].ConnectionString + ";MultipleActiveResultSets = True";

                    S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnOracle"].ProviderName;

                    break;

                case DataBaseType.AccessDB: //connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DBMS"
                    if (DbConnectionType == ConnectionType.Synchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnAccess"].ConnectionString;

                    else if (DbConnectionType == ConnectionType.Asynchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnAccess"].ConnectionString + ";Asynchronous Processing=true";

                    else if (DbConnectionType == ConnectionType.MultipleActiveResultSet)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnAccess"].ConnectionString + ";MultipleActiveResultSets = True";

                    S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnAccess"].ProviderName;

                    if (S_PROVIDER == "System.Data.OleDb")
                    {
                        if (string.IsNullOrEmpty(S_DBFILENAME) == false)
                            S_CONNECTION = S_CONNECTION.Replace("DBMS", HttpContext.Current.Server.MapPath(S_DBFILENAME));
                        else
                            S_CONNECTION = S_CONNECTION.Replace("DBMS", HttpContext.Current.Server.MapPath("App_Data/") + "photo-match-011109.mdb");

                    }


                    break;

                case DataBaseType.ExcelSheet:
                    //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties="Excel 8.0;HDR=Yes;IMEX=1";
                    //Provider=Microsoft.ACE.OLEDB.12.0;;Data Source=c:\myFolder\myExcel2007file.xlsx;Extended Properties="Excel 12.0 Xml;HDR=YES";";      
                    if (DbConnectionType == ConnectionType.Synchronous)
                    {
                        if (Path.GetExtension(HttpContext.Current.Server.MapPath(S_DBFILENAME)) == ".xls")
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxls"].ConnectionString;
                        else
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxlsx"].ConnectionString;

                    }
                    else if (DbConnectionType == ConnectionType.Asynchronous)
                    {
                        if (Path.GetExtension(HttpContext.Current.Server.MapPath(S_DBFILENAME)) == ".xls")
                        {
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxls"].ConnectionString + ";Asynchronous Processing=true";

                        }
                        else
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxlsx"].ConnectionString + ";Asynchronous Processing=true";
                    }
                    else if (DbConnectionType == ConnectionType.MultipleActiveResultSet)
                        if (Path.GetExtension(HttpContext.Current.Server.MapPath(S_DBFILENAME)) == ".xls")
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxls"].ConnectionString + ";MultipleActiveResultSets = True";
                        else
                            S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnExcelxlsx"].ConnectionString + ";MultipleActiveResultSets = True";

                    if (Path.GetExtension(HttpContext.Current.Server.MapPath(S_DBFILENAME)) == ".xls")
                        S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnExcelxls"].ProviderName;
                    else
                        S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnExcelxlsx"].ProviderName;

                    if (S_PROVIDER == "System.Data.OleDb")
                    {
                        if (string.IsNullOrEmpty(S_DBFILENAME) == true)
                            S_CONNECTION = S_CONNECTION.Replace("DBMS", HttpContext.Current.Server.MapPath("App_Data/") + "photo.mdb");
                        else
                            S_CONNECTION = S_CONNECTION.Replace("DBMS", HttpContext.Current.Server.MapPath(S_DBFILENAME));
                    }

                    break;

                default:
                    if (DbConnectionType == ConnectionType.Synchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    else if (DbConnectionType == ConnectionType.Asynchronous)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString + ";Asynchronous Processing=true";

                    else if (DbConnectionType == ConnectionType.MultipleActiveResultSet)
                        S_CONNECTION = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString + ";MultipleActiveResultSets = True";

                    S_PROVIDER = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;

                    break;
            }

            return S_PROVIDER;

        }

        #endregion

        #region CONSTRUCTOR

        public DBHelper()
        {
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }

        public DBHelper(ConnectionType conncetionType )
        {
            DbConnectionType = conncetionType;
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }

        public DBHelper(DataBaseType DBMSType)
        {
            dbType = DBMSType;
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }
        public DBHelper(DataBaseType DBMSType,string strDbFileNamePath)
        {
            dbType = DBMSType;
            S_DBFILENAME = strDbFileNamePath;
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }

        public DBHelper(ConnectionType conncetionType, DataBaseType DBMSType)
        {
            DbConnectionType = conncetionType;
            dbType = DBMSType;
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }
        public DBHelper(ConnectionType conncetionType, DataBaseType DBMSType,string strDBFileNamePath)
        {
            DbConnectionType = conncetionType;
            dbType = DBMSType;
            S_DBFILENAME = strDBFileNamePath;
            oFactory = DbProviderFactories.GetFactory(RetreiveConnectionString());
        }



        #endregion

        #region DESTRUCTOR

        ~DBHelper()
        {
            oFactory = null;
        }

        #endregion

        #region CONNECTIONS

        /// <summary>
        ///Description	    :	This function is used to Open Database Connection 
        /// </summary>
        public void gOpenConnection()
        {
            /*
            // This check is not required as it will throw "Invalid Provider Exception" on the contructor itself.
            if (0 == DbProviderFactories.GetFactoryClasses().Select("InvariantName='" + S_PROVIDER + "'").Length)
                throw new Exception("Invalid Provider");
            */
            try
            {                
                oConnection = oFactory.CreateConnection();

                if (oConnection.State == ConnectionState.Closed)
                {
                    oConnection.ConnectionString = S_CONNECTION;
                    oConnection.Open();
                    oConnectionState = ConnectionState.Open;
                                       
                }
            }
            catch (DbException oDbErr)
            {
                //catch any SQL server data provider generated error messag
                throw new Exception(oDbErr.Message);
            }
            catch (System.NullReferenceException oNullErr)
            {
                throw new Exception(oNullErr.Message);
            }
        }

        /// <summary>
        ///Description : This function is used to Close Database Connection      
        /// </summary>
        public void gCloseConnection()
        {
            //check for an open connection            
            try
            {
                if (oConnection.State == ConnectionState.Open)
                {

                    
                    //System.Data.SqlClient.SqlConnection.ClearAllPools();
                    oConnection.Close();
                    oConnectionState = ConnectionState.Closed;                    
                }
            }
            catch (DbException oDbErr)
            {
                //catch any SQL server data provider generated error messag
                throw new Exception(oDbErr.Message);
            }
            catch (System.NullReferenceException oNullErr)
            {
                throw new Exception(oNullErr.Message);
            }
            finally
            {
                if (null != oConnection)
                    oConnection.Dispose();
            }
        }

        #endregion

        #region TRANSACTION

       /// <summary>
        /// This function is used to Handle Transaction Events 
       /// </summary>
       /// <param name="veTransactionType"></param>
        public void gTransactionHandler(TransactionType veTransactionType)
        {
            switch (veTransactionType)
            {
                case TransactionType.Open:  //open a transaction
                    try
                    {
                        gOpenConnection();
                        oTransaction = oConnection.BeginTransaction();
                        mblTransaction = true;
                    }
                    catch (InvalidOperationException oErr)
                    {                        
                        throw new Exception("@TransactionHandler - " + oErr.Message);
                    }                   
                    break;

                case TransactionType.Commit:  //commit the transaction
                    if (null != oTransaction.Connection)
                    {
                        try
                        {
                            oTransaction.Commit();
                            mblTransaction = false;
                           
                        }
                        catch (InvalidOperationException oErr)
                        {
                            throw new Exception("@TransactionHandler - " + oErr.Message);
                        }
                        finally
                        {
                            gCloseConnection();
                        }
                    }
                    break;

                case TransactionType.Rollback:  //rollback the transaction
                    try
                    {
                        if (mblTransaction)
                        {
                            oTransaction.Rollback();
                        }
                        mblTransaction = false;
                    }
                    catch (InvalidOperationException oErr)
                    {
                        throw new Exception("@TransactionHandler - " + oErr.Message);
                    }
                    finally
                    {
                        gCloseConnection();
                    }
                    break;
            }

        }

        #endregion

        #region COMMANDS

        #region PARAMETERLESS METHODS
                
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText)
        {

            if (oConnection.State != ConnectionState.Open)           
            this.gOpenConnection();

            if (null == oCommand)
                oCommand = oFactory.CreateCommand();

            oCommand.Connection = oConnection;
            oCommand.CommandText = cmdText;
            oCommand.CommandType = cmdType;
                     


            if (blTransaction)
                oCommand.Transaction = oTransaction;
        }


        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText,int intCMDTimeout)
        {

            if (oConnection.State != ConnectionState.Open)
                this.gOpenConnection();

            if (null == oCommand)
                oCommand = oFactory.CreateCommand();

            oCommand.Connection = oConnection;
            oCommand.CommandText = cmdText;
            oCommand.CommandType = cmdType;
            oCommand.CommandTimeout = intCMDTimeout;



            if (blTransaction)
                oCommand.Transaction = oTransaction;
        }



        #endregion

        #region OBJECT BASED PARAMETER ARRAY
               
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {

            if (oConnection.State != ConnectionState.Open)
                this.gOpenConnection();

            if (null == oCommand)
                oCommand = oFactory.CreateCommand();

            oCommand.Connection = oConnection;
            oCommand.CommandText = cmdText;
            oCommand.CommandType = cmdType;

            if (blTransaction)
                oCommand.Transaction = oTransaction;

            if (null != cmdParms)
                CreateDBParameters(cmdParms);
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY
        
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {

            if (oConnection.State != ConnectionState.Open)
                this.gOpenConnection();


            oCommand = oFactory.CreateCommand();
            oCommand.Connection = oConnection;
            oCommand.CommandText = cmdText;
            oCommand.CommandType = cmdType;

            if (blTransaction)
                oCommand.Transaction = oTransaction;

            if (null != cmdParms)
                CreateDBParameters(cmdParms);
        }

        #endregion

        #endregion

        #region PARAMETER METHODS

        #region Object Based

       /// <summary>
        /// This function is used to Create Parameters for the Command For Execution
       /// </summary>
       /// <param name="colParameters"></param>
        private void CreateDBParameters(object[,] colParameters)
        {
            for (int i = 0; i < colParameters.Length / 2; i++)
            {
                oParameter = oCommand.CreateParameter();
                oParameter.ParameterName = colParameters[i, 0].ToString();
                oParameter.Value = colParameters[i, 1];
                oCommand.Parameters.Add(oParameter);
            }
        }

        #endregion

        #region Structure Based

       
        private void CreateDBParameters(Parameters[] colParameters)
        {
            for (int i = 0; i < colParameters.Length; i++)
            {
                Parameters oParam = (Parameters)colParameters[i];

                oParameter = oCommand.CreateParameter();
                oParameter.ParameterName = oParam.ParamName;
                oParameter.Value = oParam.ParamValue;
                oParameter.Direction = oParam.ParamDirection;
                
                if(oParam.FieldSize >0)
                oParameter.Size = oParam.FieldSize;
                
                oCommand.Parameters.Add(oParameter);

            }
        }

        #endregion

        #endregion

        #region EXCEUTE METHODS

        #region Parameterless Methods
        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText);
                return oCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText,int intCommandTimeout)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, intCommandTimeout);
                return oCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }


        /// <summary>
        /// This function is used to Execute the Command 
        /// </summary>
        /// <param name="blTransaction"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(blTransaction, cmdType, cmdText);
                int val = oCommand.ExecuteNonQuery();

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
            }
        }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="blTransaction"></param>
     /// <param name="cmdType"></param>
     /// <param name="cmdText"></param>
     /// <param name="intCommandTimeout"></param>
     /// <returns></returns>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, int intCommandTimeout)
        {
            try
            {
                PrepareCommand(blTransaction, cmdType, cmdText, intCommandTimeout);
                int val = oCommand.ExecuteNonQuery();

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
            }
        }



        #endregion

        #region Object Based Parameter Array

        /// <summary>
        ///Description	    :	This function is used to Execute the Command       
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array, Clear Parameters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command       
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	Overloaded method. 
        /// </summary>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return gExecuteNonQuery(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command      
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array, Clear Paramaeters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {
                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command      
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	Overloaded function. 
        /// </summary>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return gExecuteNonQuery(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #region Structure Based Parameter Array

        /// <summary>
        /// This function is used to Execute the Command 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <param name="blDisposeCommand"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        /// <summary>
        /// This function is used to Execute the Command     
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return gExecuteNonQuery(true,cmdType, cmdText, cmdParms, true);
        }


        /// <summary>
        /// This function is used to Execute the Command     
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return gExecuteNonQuery(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        /// This function is used to Execute the Command
        /// </summary>
        /// <param name="blTransaction"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <param name="blDisposeCommand"></param>
        /// <returns></returns>
        public int gExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
            }
        }

        

        #endregion

        #endregion

        #region READER METHODS

        #region Parameterless Methods

       /// <summary>
        /// This function is used to fetch data using Data Reader
       /// </summary>
       /// <param name="cmdType"></param>
       /// <param name="cmdText"></param>
       /// <returns></returns>
        public DbDataReader gExecuteReader(CommandType cmdType, string cmdText)
        {                       
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText);
                DbDataReader dr = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oCommand.Parameters.Clear();
                return dr;
            }
            catch (Exception ex)
            {
                gCloseConnection();
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
            }
        }


        /// <summary>
        /// This function is used to fetch data using Data Reader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public DbDataReader gExecuteReader(bool bnlTransaction, CommandType cmdType, string cmdText)
        {
            try
            {    
               
                PrepareCommand(bnlTransaction, cmdType, cmdText);
                return oCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                gCloseConnection();
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();               
            }
        }






        #endregion

        #region Object Based Parameter Array

        /// <summary>
        /// This function is used to fetch data using Data Reader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public DbDataReader gExecuteReader(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                DbDataReader dr = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oCommand.Parameters.Clear();
                return dr;
            }
            catch (Exception ex)
            {
                gCloseConnection();
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
            }
        }

        #endregion

        #region Structure Based Parameter Array
       /// <summary>
        /// This function is used to fetch data using Data Reader
       /// </summary>
       /// <param name="cmdType"></param>
       /// <param name="cmdText"></param>
       /// <param name="cmdParms"></param>
       /// <returns></returns>
        public DbDataReader gExecuteReader(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                gCloseConnection();
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
            }
        }

        #endregion

        #endregion

        #region RETURN DATASET METHODS

        #region Parameter Less Method

       /// <summary>
       ///	This function is used to fetch data using Data Adapter	      
       /// </summary>
       /// <param name="cmdType">Command Type</param>
       /// <param name="cmdText">Sql Query</param>
       /// <returns>It returns Dataset</returns>
        public DataSet gReturnDataSet(CommandType cmdType, string cmdText)
        {            
            DbDataAdapter dda = null;
            try
            {
                gOpenConnection();
                dda = oFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText);

                dda.SelectCommand = oCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        #endregion

        #region Object Based Parameter Method

       /// <summary>
        /// This function is used to fetch data using Data Adapter	       
       /// </summary>
        /// <param name="cmdType"> Command Type</param>
       /// <param name="cmdText">Sql Query</param>
        /// <param name="cmdParms"> 2-Dimensional Parameter Array of object Parameter</param>
       /// <returns> It Returns Dataset </returns>
        public DataSet gReturnDataSet(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            DbDataAdapter dda = null;
            try
            {
                gOpenConnection();
                dda = oFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText, cmdParms);

                dda.SelectCommand = oCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        #endregion

        #region Structure Based Parameter Method

        /// <summary>
        /// This function is used to fetch data using Data Adapter	   
        /// </summary>
        /// <param name="cmdType"> Command Type</param>
        /// <param name="cmdText"> Sql Query</param>
        /// <param name="cmdParms">2-Dimensional Parameter Array</param>
        /// <returns>Its return DatSet</returns>
       
        public DataSet gReturnDataSet(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
           
            DbDataAdapter dda = null;
            try
            {
                gOpenConnection();
                dda = oFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText, cmdParms);

                dda.SelectCommand = oCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        #endregion

        #endregion

        #region RETRUN SCALAR METHODS

        #region Parameterless Methods

       /// <summary>
        /// This function is used to invoke Execute Scalar Method
       /// </summary>
        /// <param name="cmdType">CommandType</param>
       /// <param name="cmdText">Sql Query</param>
        /// <returns>it return first column of query in object form</returns>
        public object gExecuteScalar(CommandType cmdType, string cmdText)
        {
            try
            {
                gOpenConnection();

                PrepareCommand(false, cmdType, cmdText);

                object val = oCommand.ExecuteScalar();

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        #endregion

        #region Object Based Parameter Array

       /// <summary>
        /// This function is used to invoke Execute Scalar Method   
       /// </summary>
       /// <param name="cmdType"> Command Type</param>
       /// <param name="cmdText">Sql Query </param>
       /// <param name="cmdParms"> 2-Dimentional Parameter array</param>
        /// <param name="blDisposeCommand">is command will dispose </param>
        /// <returns>it return first column of query in object form</returns>
        public object gExecuteScalar(CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="cmdText">Sql Query </param>
        /// <param name="cmdParms"> 2 - Dimentional parameter array</param>
        /// <returns>it return first column of query in object form</returns>
        public object gExecuteScalar(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return gExecuteScalar(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        /// This function is used to invoke Execute Scalar Method
        /// </summary>
        /// <param name="blTransaction">Transaction enables </param>
        /// <param name="cmdType"> Command Type </param>
        /// <param name="cmdText"> Sql Query</param>
        /// <param name="cmdParms"> 2- Dimentional paarameter array</param>
        /// <param name="blDisposeCommand">command will dispose </param>
        /// <returns> it return first column of query in object form </returns>
        public object gExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
            }
        }

       /// <summary>
        /// This function is used to invoke Execute Scalar Method     
       /// </summary>
       /// <param name="blTransaction">Transaction Enable</param>
       /// <param name="cmdType"> Command Type</param>
       /// <param name="cmdText"> Sql Query</param>
       /// <param name="cmdParms"> 2 Dimentional Parameter array</param>
       /// <returns>It returns object data type</returns>
        public object gExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return gExecuteScalar(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #region Structure Based Parameter Array

        /// <summary>
        /// This function is used to invoke Execute Scalar Method	
        /// </summary>
        /// <param name="cmdType">Command Type</param>
        /// <param name="cmdText">Sql Query</param>
        /// <param name="cmdParms">2 Dimentional Parameter array</param>
        /// <param name="blDisposeCommand">is command object will dispose</param>
        /// <returns>It returns object data type</returns>
        public object gExecuteScalar(CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {
                gOpenConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
                gCloseConnection();
            }
        }

        /// <summary>
        /// This function is used to invoke Execute Scalar Method  on Structure Based Parameter 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public object gExecuteScalar(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return gExecuteScalar(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        /// This function is used to invoke Execute Scalar Method  on Structure Based Parameter 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public object gExecuteScalar(bool blTransaction,CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(blTransaction, cmdType, cmdText);
                return oCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }



       /// <summary>
        /// This function is used to invoke Execute Scalar Method  on Structure Based Parameter 
       /// </summary>
       /// <param name="blTransaction"></param>
       /// <param name="cmdType"></param>
       /// <param name="cmdText"></param>
       /// <param name="cmdParms"></param>
       /// <param name="blDisposeCommand"></param>
       /// <returns></returns>
        public object gExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return oCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != oCommand)
                    oCommand.Dispose();
            }
        }

       /// <summary>
        /// This function is used to invoke Execute Scalar Method  on Structure Based Parameter 
       /// </summary>
       /// <param name="blTransaction"></param>
       /// <param name="cmdType"></param>
       /// <param name="cmdText"></param>
       /// <param name="cmdParms"></param>
       /// <returns></returns>
        public object gExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return gExecuteScalar(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #endregion

        #region gGetDBSchema

        public DataTable gGetDBSchema( SchemaType enmSchemaType )
        {
            DataTable dtTabl=null;
            string strVal = "";

            switch (enmSchemaType)
            {
                case SchemaType.Columns:
                    strVal="Columns";
                    break;

                case SchemaType.DataBases:
                    strVal="DataBases";
                    break;
                case SchemaType.Tables:
                    strVal="Tables";
                    break;
            }

            try
            {                
               gOpenConnection();
               dtTabl = oConnection.GetSchema(strVal);
            }catch (Exception err)
            {
                throw err;
            }
            finally
            {
                dtTabl.Dispose();
                gCloseConnection();
            }

            return dtTabl;
        }

        #endregion

        #region FUNCTION: gRecordExists
        /// <summary>
        ///     This Function Checking the Existanc of the Record into the Table Return by the SQL Query
        /// </summary>
        /// <param name="strSqlQuery">SQL (SELECT) Query</param>
        /// <returns></returns>
        private bool gRecordExists(bool bnlInsideTransaction, string strSqlQuery)
        {
            bool bnlStatus = false;
            int intNoOfRecords = 0;
            IDataReader reader = null;
            try
            {
                //gOpenConnection();

                reader = gExecuteReader(bnlInsideTransaction, CommandType.Text, strSqlQuery);

                while (reader.Read())
                    intNoOfRecords = Convert.ToInt16(reader[0]);

                if (intNoOfRecords > 0)
                    bnlStatus = true;
            }
            catch
            {
                reader.Close();
                bnlStatus = false;
            }
            finally
            {
                reader.Close();
            }
           

            return bnlStatus;
        }

        #endregion

        #region FUNCTION: gIsRecordExist

        /// <summary>
        ///     This Function Accepts the Table Name of SQL Query and The Type of Query Type and Checking the Existance Of Records
        ///     into the Table.params object[] objFieldNameValue is a optional parameter.
        /// </summary>
        /// <param name="strTableName_strSQL">Table Name or SQL Query</param>
        /// <param name="enmQueryType">QUERY TYPE - Text of TableName</param>
        /// <param name="objFieldNameValue">send column name & its value separated by comma </param> 
        /// <returns>it Return bolean values</returns>
        public bool gIsRecordExist(bool bnlInsideTransaction ,string strTableName_strSQL, SqlType enmQueryType, params object[] objFieldNameValue)
        {
            bool bnlStatus = false;
            string strFieldName = string.Empty;
            bool bnlMultipleField = false;
            string strSqlQuery = "";
            int j = 1;

            try
            {
                switch (enmQueryType)
                {
                    case SqlType.QueryOnly:
                        strSqlQuery = strTableName_strSQL;
                        break;

                    case SqlType.TableOnly:
                        strSqlQuery = "SELECT COUNT(*) AS NO_OF_RECORDS FROM " + strTableName_strSQL + "";
                        break;
                }
                // IF THERE ARE MORE THAN 2 VALUES PASSED
                if (objFieldNameValue.Length >= 2)
                {
                    strSqlQuery = strSqlQuery + " WHERE  ";

                    for (int i = 0; i < objFieldNameValue.Length; i++)
                    {
                        if (j % 2 == 0)
                        {
                            if (gIsNumeric(objFieldNameValue[i].ToString()) == false)
                                if (bnlMultipleField == false)
                                {
                                    strSqlQuery = strSqlQuery + " UPPER(" + strFieldName + ") = '" + gReplaceQuotes(objFieldNameValue[i].ToString().Trim().ToUpper()) + "' ";
                                    bnlMultipleField = true;
                                }
                                else
                                    strSqlQuery = strSqlQuery + " AND UPPER(" + strFieldName + ") = '" + gReplaceQuotes(objFieldNameValue[i].ToString().Trim().ToUpper()) + "' ";

                            else
                            {
                                if (bnlMultipleField == false)
                                {
                                    strSqlQuery = strSqlQuery + strFieldName + " =" + objFieldNameValue[i].ToString();
                                    bnlMultipleField = true;
                                }
                                else
                                    strSqlQuery = strSqlQuery + " AND  " + strFieldName + " =" + objFieldNameValue[i].ToString();
                            }
                        }
                        else
                            strFieldName = objFieldNameValue[i].ToString();

                        j++;
                    }
                }

                // IF THERE ARE  1 VALUES PASSED
                if (objFieldNameValue.Length == 1)
                    strSqlQuery = strSqlQuery + " WHERE  " + objFieldNameValue[0].ToString();

                //NOW GET NO OF RECORDS FROM DATABASE 
                if (gRecordExists(bnlInsideTransaction,strSqlQuery) == true)
                    bnlStatus = true;
            }
            catch
            {
                bnlStatus = false;
            }

            return bnlStatus;
        }


        #endregion

        #region FUNCTION: gReplaceCharacter

        private string gReplaceQuotes(string strValue)
        {
            return (strValue.Replace("'", "''"));
        }


        #endregion        

        #region FUNCTION: gIsNumeric

        private bool gIsNumeric(string Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool bnlStatus;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            bnlStatus = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return bnlStatus;
        }

        #endregion

        #region gReturnDataAdapter
        public DbDataAdapter gReturnDataAdapter(string strSelectCommand, string strInsertCommand, Parameters[] InsertParameters)
        {
            try
            {
                gPrepareAdapter(false, strSelectCommand, strInsertCommand, InsertParameters, "", null, "", null);

                return oAdapter;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region gReturnDataAdapter
        public DbDataAdapter gReturnDataAdapter(bool bnlIsTransaction, string strSelectCommand, string strInsertCommand, Parameters[] InsertParameters, string strUpdateCommand, Parameters[] UpdateParameters)
        {
            try
            {
                gPrepareAdapter(bnlIsTransaction, strSelectCommand, strInsertCommand, InsertParameters, strUpdateCommand, UpdateParameters, "", null);

                return oAdapter;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region gReturnDataAdapter
        public DbDataAdapter gReturnDataAdapter(string strSelectCommand, string strInsertCommand, Parameters[] InsertParameters, string strUpdateCommand, Parameters[] UpdateParameters, string strDeleteCommand, Parameters[] DeleteParameters)
        {
            try
            {
                gPrepareAdapter(false, strSelectCommand, strInsertCommand, InsertParameters, strUpdateCommand, UpdateParameters, strDeleteCommand, DeleteParameters);

                return oAdapter;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region gReturnDataAdapter
        public DbDataAdapter gReturnDataAdapter(bool bnlIsTransaction, string strSelectCommand, string strInsertCommand, Parameters[] InsertParameters)
        {
            try
            {
                gPrepareAdapter(bnlIsTransaction, strSelectCommand, strInsertCommand, InsertParameters, "", null, "", null);

                return oAdapter;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region gPrepareAdapter
        private void gPrepareAdapter(bool blTransaction, string strSelectCommand, string strInsertCommand, Parameters[] InsertParameter, string strUpdateCommand, Parameters[] UpdateParam, string strDeleteCommand, Parameters[] DeleteParam)
        {
            try
            {

                if (oConnection.State != ConnectionState.Open)
                    this.gOpenConnection();

                if (null == oAdapter)
                    oAdapter = oFactory.CreateDataAdapter();

                oCommand = oFactory.CreateCommand();

                oCommand.Connection = oConnection;
                oCommand.CommandText = strSelectCommand;
                oCommand.CommandType = CommandType.Text;

                if (blTransaction)
                    oCommand.Transaction = oTransaction;

                oAdapter.SelectCommand = oCommand;

                // ------ SETTING INSERT COMMAND -------

                oCommand = oFactory.CreateCommand();
                oCommand.Connection = oConnection;
                oCommand.CommandText = strInsertCommand;
                oCommand.CommandType = CommandType.Text;

                if (blTransaction)
                    oCommand.Transaction = oTransaction;

                // ------ SETTING INSERT COMMAND PARAMETER -------
                for (int i = 0; i < InsertParameter.Length; i++)
                {
                    Parameters oParam = (Parameters)InsertParameter[i];

                    oParameter = oCommand.CreateParameter();

                    oParameter.ParameterName = oParam.ParamName;
                    oParameter.DbType = oParam.DBType;
                    oParameter.Size = oParam.FieldSize;
                    oParameter.SourceColumn = oParam.SourceColumn;

                    oCommand.Parameters.Add(oParameter);

                }
                // ------ SETTING INSERT COMMAND TO ADAPTER -------
                oAdapter.InsertCommand = oCommand;

                // ------ SETTING UPDATE COMMAND -------
                if (!string.IsNullOrEmpty(strUpdateCommand))
                {
                    oCommand = oFactory.CreateCommand();
                    oCommand.Connection = oConnection;
                    oCommand.CommandText = strUpdateCommand;
                    oCommand.CommandType = CommandType.Text;

                    if (blTransaction)
                        oCommand.Transaction = oTransaction;

                    // ------ SETTING INSERT COMMAND PARAMETER -------
                    for (int i = 0; i < UpdateParam.Length; i++)
                    {
                        Parameters oParam = (Parameters)UpdateParam[i];

                        oParameter = oCommand.CreateParameter();

                        oParameter.ParameterName = oParam.ParamName;
                        oParameter.DbType = oParam.DBType;
                        oParameter.Size = oParam.FieldSize;
                        oParameter.SourceColumn = oParam.SourceColumn;

                        oCommand.Parameters.Add(oParameter);

                    }
                    // ------ SETTING UPDATE COMMAND TO ADAPTER -------
                    oAdapter.UpdateCommand = oCommand;

                }
                // ------ SETTING DELETE COMMAND -------
                if (!string.IsNullOrEmpty(strDeleteCommand))
                {
                    oCommand = oFactory.CreateCommand();
                    oCommand.Connection = oConnection;
                    oCommand.CommandText = strDeleteCommand;
                    oCommand.CommandType = CommandType.Text;

                    // ------ SETTING INSERT COMMAND PARAMETER -------
                    for (int i = 0; i < DeleteParam.Length; i++)
                    {
                        Parameters oParam = (Parameters)DeleteParam[i];

                        oParameter = oCommand.CreateParameter();

                        oParameter.ParameterName = oParam.ParamName;
                        oParameter.DbType = oParam.DBType;
                        oParameter.Size = oParam.FieldSize;
                        oParameter.SourceColumn = oParam.SourceColumn;

                        oCommand.Parameters.Add(oParameter);

                    }
                    // ------ SETTING UPDATE COMMAND TO ADAPTER -------
                    oAdapter.UpdateCommand = oCommand;

                }

                if (blTransaction)
                    oCommand.Transaction = oTransaction;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


        #region BulkCopyToSqlDataBase
        public bool BulkCopyToSqlDataBase(DataTable table, string DestinationTableName, bool UseSourceIndentity)
        {
            return BulkCopyToSqlDataBase(table, null, DestinationTableName, UseSourceIndentity, false);
        }

        #endregion

        #region BulkCopyToSqlDataBase
        public bool BulkCopyToSqlDataBase(DataTable table, ColumnMapping[] columnParameter, string DestinationTableName, bool UseSourceIndentity)
        {
            return BulkCopyToSqlDataBase(table, columnParameter, DestinationTableName, UseSourceIndentity, false);
        }

        #endregion

        #region BulkCopyToSqlDataBase
        public bool BulkCopyToSqlDataBase(DataTable table, ColumnMapping[] columnParameter, string DestinationTableName)
        {
            return BulkCopyToSqlDataBase(table, columnParameter, DestinationTableName, false, false);
        }

        #endregion

        #region BulkCopyToSqlDataBase
        private bool BulkCopyToSqlDataBase(DataTable table, ColumnMapping[] columnParameter, string DestinationTableName, bool bnlUseSourceIdentity, bool bnlExternalTransaction)
        {
            bool bnlStatus = true;
            try
            {
                /* Set up the bulk copy object.Note that when specifying the UseInternalTransaction option, you cannot also specify an external transaction.
                 Therefore, you must use the SqlBulkCopy construct that requires a string for the connection, rather than an existing SqlConnection object. */

                System.Data.SqlClient.SqlBulkCopy bulkCopy;

                if (bnlExternalTransaction)
                {
                    if (bnlUseSourceIdentity)
                        bulkCopy = new System.Data.SqlClient.SqlBulkCopy((System.Data.SqlClient.SqlConnection)this.oConnection, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity, (System.Data.SqlClient.SqlTransaction)this.oTransaction);
                    else
                        bulkCopy = new System.Data.SqlClient.SqlBulkCopy((System.Data.SqlClient.SqlConnection)this.oConnection, System.Data.SqlClient.SqlBulkCopyOptions.Default, (System.Data.SqlClient.SqlTransaction)this.oTransaction);
                }
                else
                {
                    if (bnlUseSourceIdentity)
                        bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this.S_CONNECTION, System.Data.SqlClient.SqlBulkCopyOptions.KeepIdentity | System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction);
                    else
                        bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this.S_CONNECTION, System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction);

                }

                if (columnParameter != null)
                    for (int i = 0; i < columnParameter.Length; i++)
                    {
                        ColumnMapping oColumn = (ColumnMapping)columnParameter[i];

                        System.Data.SqlClient.SqlBulkCopyColumnMapping Colmapping =
                            new System.Data.SqlClient.SqlBulkCopyColumnMapping(oColumn.SourceName, oColumn.Destination);

                        bulkCopy.ColumnMappings.Add(Colmapping);
                    }

                bulkCopy.BatchSize = 1000;
                bulkCopy.BulkCopyTimeout = 10;
                bulkCopy.DestinationTableName = DestinationTableName;

                bulkCopy.WriteToServer(table);

                //if (bnlExternalTransaction)
                //{

                //}
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
    }
}
