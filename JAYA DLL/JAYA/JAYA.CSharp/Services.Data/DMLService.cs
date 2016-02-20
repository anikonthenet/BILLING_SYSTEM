
#region Refered Namespaces

using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;


#endregion

namespace JAYA.CSharp.Services.Data
{
	#region CLASS : DMLService
	public sealed class DMLService : IDisposable
	{
		#region PRIVATE OBJECTS DECLERATION
		
		private IDbConnection J_IdbConn;
		private IDbCommand J_IdbcmdCommand;
		private IDbTransaction J_IdbTran;
		private J_DatabaseType J_enmDatabaseType;
		private J_ApplicationType J_enmApplicationType;
		
		#endregion

		#region PRIVATE VARIABLES DECLERATION
		
		private string J_strConnectionString;
		private int J_intCommandTimeout;
		private int J_intRetryConnect;
		private int J_intRetryTime;
		private bool J_blnConnected;
		private bool J_blnDisposed;
		
		#endregion
		
		#region CONSTRUCTOR
		
		#region DMLService [1]
		public DMLService()
		{
			this.J_strConnectionString = string.Empty;
			this.J_enmDatabaseType = J_DatabaseType.Sql;
			this.J_enmApplicationType = J_ApplicationType.StandAlone;
			this.J_IdbConn = null;
			this.J_IdbTran = null;
			this.J_IdbcmdCommand = null;
			this.J_intCommandTimeout = 30;
			this.J_intRetryConnect = 3;
			this.J_blnDisposed = false;
			this.J_blnConnected = false;
		}
		#endregion
		
		#region DMLService [2]
		public DMLService(string strConnectionString)
		{
			this.J_strConnectionString = string.Empty;
			this.J_enmDatabaseType = J_DatabaseType.Sql;
			this.J_enmApplicationType = J_ApplicationType.StandAlone;
			this.J_IdbConn = null;
			this.J_IdbTran = null;
			this.J_IdbcmdCommand = null;
			this.J_intCommandTimeout = 30;
			this.J_intRetryConnect = 3;
			this.J_blnDisposed = false;
			this.J_blnConnected = false;
			this.J_ConfigureConnction(strConnectionString, J_DatabaseType.Sql);
		}
		#endregion
		
		#region DMLService [3]
		public DMLService(string strConnectionString, J_DatabaseType dbType)
		{
			this.J_strConnectionString = string.Empty;
			this.J_enmDatabaseType = dbType;
			this.J_enmApplicationType = J_ApplicationType.StandAlone;
			this.J_IdbConn = null;
			this.J_IdbTran = null;
			this.J_IdbcmdCommand = null;
			this.J_intCommandTimeout = 30;
			this.J_intRetryConnect = 3;
			this.J_blnDisposed = false;
			this.J_blnConnected = false;
			this.J_ConfigureConnction(strConnectionString, dbType);
		}
		#endregion
		
		#region DMLService [4]
		public DMLService(string strConnectionString, J_ApplicationType AppType)
		{
			this.J_strConnectionString = string.Empty;
			this.J_enmDatabaseType = J_DatabaseType.Sql;
			this.J_enmApplicationType = AppType;
			this.J_IdbConn = null;
			this.J_IdbTran = null;
			this.J_IdbcmdCommand = null;
			this.J_intCommandTimeout = 30;
			this.J_intRetryConnect = 3;
			this.J_blnDisposed = false;
			this.J_blnConnected = false;
			this.J_ConfigureConnction(strConnectionString, J_DatabaseType.Sql);
		}
		#endregion
		
		#region DMLService [5]
		public DMLService(string strConnectionString, J_DatabaseType dbType, J_ApplicationType AppType)
		{
			this.J_strConnectionString = string.Empty;
			this.J_enmDatabaseType = dbType;
			this.J_enmApplicationType = AppType;
			this.J_IdbConn = null;
			this.J_IdbTran = null;
			this.J_IdbcmdCommand = null;
			this.J_intCommandTimeout = 30;
			this.J_intRetryConnect = 3;
			this.J_blnDisposed = false;
			this.J_blnConnected = false;
			this.J_ConfigureConnction(strConnectionString, dbType);
		}
		#endregion
		
		#endregion
		
		#region USER DEFINES METHODS
		
		#region PRIVATE METHODS

		#region APPEND PARAMETERS
		private void J_AppendParameters(ref IDbCommand Command, params object[] Parameters)
		{
			IDbDataParameter Parameter = null;
			object obj2;
			
			foreach (object[] objArray in Parameters)
			{
				switch (objArray[4].GetType().ToString())
				{
					case "System.String":
						if (((string) objArray[4]).Length == 0)
							obj2 = DBNull.Value;
						else
							obj2 = objArray[4];
						break;

					case "System.Boolean":
						obj2 = objArray[4];
						break;

					default:
						if (objArray[4] is int)
							if (objArray[4] == null)
								obj2 = DBNull.Value;
							else
								obj2 = objArray[4];
						else
							obj2 = objArray[4];
						break;
				}
				
				switch (this.J_enmDatabaseType)
				{
					case J_DatabaseType.Sql:
						Parameter = ((SqlCommand) Command).Parameters.Add((string) objArray[0], (SqlDbType) objArray[1], (int) objArray[3]);
						break;

					case J_DatabaseType.OleDb:
						Parameter = ((OleDbCommand) Command).Parameters.Add((string) objArray[0], (OleDbType) objArray[1], (int) objArray[3]);
						break;

					case J_DatabaseType.Odbc:
						Parameter = ((OleDbCommand) Command).Parameters.Add((string) objArray[0], (OleDbType) objArray[1], (int) objArray[3]);
						break;

					case J_DatabaseType.Oracle:
						Parameter = ((OracleCommand) Command).Parameters.Add((string) objArray[0], (OracleType) objArray[1], (int) objArray[3]);
						break;

					default:
						Parameter = ((SqlCommand) Command).Parameters.Add((string) objArray[0], (SqlDbType) objArray[1], (int) objArray[3]);
						break;
				}
				
				Parameter.Direction = (ParameterDirection) objArray[2];
				if ((Parameter.Direction == ParameterDirection.Input) || (Parameter.Direction == ParameterDirection.InputOutput))
					Parameter.Value = obj2;
			}
		}
		#endregion

		#region CONFIGURE CONNECTION
		private void J_ConfigureConnction(string strConnectionString, J_DatabaseType dbType)
		{
			switch (dbType)
			{
				case J_DatabaseType.Sql:
					this.J_IdbConn = new SqlConnection();
					break;

				case J_DatabaseType.Oracle:
					this.J_IdbConn = new OracleConnection();
					break;
				
				case J_DatabaseType.OleDb:
					this.J_IdbConn = new OleDbConnection();
					break;

				case J_DatabaseType.Odbc:
					this.J_IdbConn = new OleDbConnection();
					break;

				default:
					this.J_IdbConn = new SqlConnection();
					break;
			}
			this.J_strConnectionString = strConnectionString;
		}
		#endregion

		#region OPEN CONNECTION
		private bool J_OpenConnection()
		{
			if ((this.J_strConnectionString == null) || (this.J_strConnectionString.Trim().Length == 0))
				this.J_strConnectionString = string.Empty;
			
			this.J_CloseConnection();
			this.J_IdbConn.ConnectionString = this.J_strConnectionString;
			
			for (int i = 1; i <= this.J_intRetryConnect; i++)
			{
				try
				{
					this.J_IdbConn.Open();
					if (this.J_IdbConn.State.Equals(ConnectionState.Open))
					{
						this.J_blnConnected = true;
						break;
					}
				}
				catch (Exception exception)
				{
					Thread.Sleep((int) (this.J_intRetryTime * 0x3e8));
				}
			}
			
			this.J_IdbcmdCommand = this.J_IdbConn.CreateCommand();
			this.J_IdbcmdCommand.CommandTimeout = this.J_intCommandTimeout;
			return this.J_blnConnected;
		}
		#endregion

		#region CLOSE CONNECTION
		private void J_CloseConnection()
		{
			if (this.J_blnConnected)
			{
				if (this.J_IdbTran != null)
					this.J_Rollback();
				
				if (this.J_IdbcmdCommand != null)
				{
					this.J_IdbcmdCommand.Dispose();
					this.J_IdbcmdCommand = null;
				}
				
				if (this.J_IdbConn != null)
				{
					this.J_IdbConn.Close();
					this.J_IdbConn.Dispose();
					this.J_IdbConn = null;
				}
				this.J_blnConnected = false;
			}
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

		#endregion

		#region PUBLIC METHODS

		#region GET CONNECTION STRING
		public static string J_GetConnectionString(object connectionName)
		{
			string setting = null;
			DataSet dset = new DataSet();

			if(J_pTypeOfApplication == J_ApplicationType.StandAlone)
			{
				string strServerName,strDataBase,strUserName,strPassword;
				dset.ReadXml(Application.StartupPath + "/xmlConnection.xml");
				strServerName = dset.Tables[0].Rows[0][0].ToString();
				strDataBase = dset.Tables[0].Rows[0][1].ToString();
				strUserName = dset.Tables[0].Rows[0][2].ToString();
				strPassword = dset.Tables[0].Rows[0][3].ToString();
				setting = "SERVER = " + strServerName + ";DATABASE = " + strDataBase + ";UID = " + strUserName + ";PWD = " + strPassword + ";";
			}
			else if(J_pTypeOfApplication == J_ApplicationType.Web)
			{
				setting = "Web";
			}
			return setting;
		}
		#endregion

		#region BEGIN TRANSACTION
		public void J_BeginTransaction()
		{
			this.J_ValidateConnection();
			this.J_IdbTran = this.J_IdbConn.BeginTransaction();
			this.J_IdbcmdCommand.Transaction = this.J_IdbTran;
		}
		#endregion

		#region ROLLBACK
		public void J_Rollback()
		{
			this.J_IdbTran.Rollback();
		}
		#endregion
		
		#region COMMIT
		public void J_Commit()
		{
			this.J_IdbTran.Commit();
		}
		#endregion

		#region VALIDATE CONNECTION
		public bool J_ValidateConnection()
		{
			return (this.J_blnConnected || this.J_OpenConnection());
		}
		#endregion
		
		#region DISPOSE
		public void Dispose()
		{
			this.Dispose(true);
		}
		#endregion

		#region CLOSE
		public void J_Close()
		{
			this.Dispose(true);
		}
		#endregion

		#region SQL SAFE [ STATIC FUNCTION ]
		public static string J_SqlSafe(string sqlText)
		{
			return sqlText.Replace("'", "''");
		}
		#endregion

		#region EXEC SQL [ OVERLOADED METHOD ]
		
		#region EXEC SQL [1]
		public void J_ExecSql(string SqlText, out int RowsAffected, params object[] Parameters)
		{
			RowsAffected = -1;
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			if ((Parameters != null) && (Parameters.Length != 0))
				this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			RowsAffected = this.J_IdbcmdCommand.ExecuteNonQuery();
		}
		#endregion
		
		#region EXEC SQL [2]
		public void J_ExecSql(string SqlText, out int RowsAffected, out Hashtable OutParameters, params object[] Parameters)
		{
			RowsAffected = -1;
			OutParameters = new Hashtable();
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			if ((Parameters != null) && (Parameters.Length != 0))
				this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			
			RowsAffected = this.J_IdbcmdCommand.ExecuteNonQuery();
			foreach (IDbDataParameter Parameter in this.J_IdbcmdCommand.Parameters)
			{
				if (((Parameter.Direction != ParameterDirection.InputOutput) && (Parameter.Direction != ParameterDirection.Output)) && (Parameter.Direction != ParameterDirection.ReturnValue))
					continue;
				OutParameters.Add(Parameter.ParameterName, Parameter.Value);
			}
		}
		#endregion

		#endregion

		#region EXEC SQL RETURN ADAPTER
		public IDataAdapter J_ExecSqlReturnAdapter(string SqlText)
		{
			IDataAdapter adapter = null;
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			switch (this.J_enmDatabaseType)
			{
				case J_DatabaseType.Sql:
					return new SqlDataAdapter((SqlCommand) this.J_IdbcmdCommand);

				case J_DatabaseType.OleDb:
					return new OleDbDataAdapter((OleDbCommand) this.J_IdbcmdCommand);

				case J_DatabaseType.Odbc:
					return new OleDbDataAdapter((OleDbCommand) this.J_IdbcmdCommand);

				case J_DatabaseType.Oracle:
					return new OracleDataAdapter((OracleCommand) this.J_IdbcmdCommand);
			}
			adapter = new SqlDataAdapter((SqlCommand) this.J_IdbcmdCommand);
			return adapter;
		}
		#endregion

		#region EXEC SQL RETURN DATASET [ OVERLOADED METHOD ]
		
		#region EXEC SQL RETURN DATASET [1]
		public DataSet J_ExecSqlReturnDataSet(string SqlText)
		{
			return this.J_ExecSqlReturnDataSet(SqlText, "Table");
		}
		#endregion

		#region EXEC SQL RETURN DATASET [2]
		public DataSet J_ExecSqlReturnDataSet(string SqlText, string srcTableName)
		{
			IDataAdapter adapter = null;
			DataSet dstDataSet = null;
			this.J_ValidateConnection();
			
			dstDataSet = new DataSet();
			adapter = this.J_ExecSqlReturnAdapter(SqlText);
			
			switch (this.J_enmDatabaseType)
			{
				case J_DatabaseType.Sql:
					((SqlDataAdapter) adapter).Fill(dstDataSet, srcTableName);
					return dstDataSet;
				
				case J_DatabaseType.OleDb:
					((OleDbDataAdapter) adapter).Fill(dstDataSet, srcTableName);
					return dstDataSet;

				case J_DatabaseType.Odbc:
					((OleDbDataAdapter) adapter).Fill(dstDataSet, srcTableName);
					return dstDataSet;

				case J_DatabaseType.Oracle:
					((OracleDataAdapter) adapter).Fill(dstDataSet, srcTableName);
					return dstDataSet;

				default:
					((SqlDataAdapter) adapter).Fill(dstDataSet, srcTableName);
					return dstDataSet;
			}
			((SqlDataAdapter) adapter).Fill(dstDataSet, srcTableName);
			return dstDataSet;
		}
		#endregion
		
		#endregion

		#region EXEC SQL RETURN READER [ OVERLOADED METHOD ]
		
		#region EXEC SQL RETURN READER [1]
		public IDataReader J_ExecSqlReturnReader(string SqlText)
		{
			return this.J_ExecSqlReturnReader(SqlText, CommandBehavior.CloseConnection, CommandType.Text, null);
		}
		#endregion

		#region EXEC SQL RETURN READER [2]
		public IDataReader J_ExecSqlReturnReader(string SqlText, CommandBehavior commandBehavior)
		{
			return this.J_ExecSqlReturnReader(SqlText, commandBehavior, CommandType.Text, null);
		}
		#endregion

		#region EXEC SQL RETURN READER [3]
		public IDataReader J_ExecSqlReturnReader(string SqlText, CommandBehavior commandBehavior, CommandType Type, params object[] Parameters)
		{
			IDataReader reader = null;
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			this.J_IdbcmdCommand.CommandType = Type;
			
			if ((Parameters != null) && (Parameters.Length != 0))
				this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			
			reader = this.J_IdbcmdCommand.ExecuteReader(commandBehavior);
			return reader;
		}
		#endregion
		
		#endregion

		#region EXEC SQL RETURN SCALAR
		public object J_ExecSqlReturnScalar(string SqlText)
		{
			object objScalar = null;
			this.J_ValidateConnection();
			this.J_IdbcmdCommand.CommandText = SqlText;
			objScalar = this.J_IdbcmdCommand.ExecuteScalar();
			return objScalar;
		}
		#endregion

		#region EXEC STORE PROCEDURE CONTAINS OUT PARAMETERS
		public void J_ExecStoreProcedureContainsOutParameters(string SqlText, out Hashtable outParameters, params object[] Parameters)
		{
			outParameters = new Hashtable();
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandType = CommandType.StoredProcedure;
			this.J_IdbcmdCommand.CommandText = SqlText;
			this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			this.J_IdbcmdCommand.ExecuteNonQuery();
			
			foreach (IDbDataParameter Parameter in this.J_IdbcmdCommand.Parameters)
			{
				if (((Parameter.Direction != ParameterDirection.InputOutput) && (Parameter.Direction != ParameterDirection.Output)) && (Parameter.Direction != ParameterDirection.ReturnValue))
					continue;
				outParameters.Add(Parameter.ParameterName, Parameter.Value);
			}
		}
		#endregion

		#region EXEC STORE PROCEDURE CONTAINS OUT PARAMETERS RETURN DATA READER
		public IDataReader J_ExecStoreProcedureContainsOutParametersReturnDataReader(string SqlText, out Hashtable outParameters, params object[] Parameters)
		{
			IDataReader reader = null;
			outParameters = new Hashtable();
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			this.J_IdbcmdCommand.CommandType = CommandType.StoredProcedure;
			this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			this.J_IdbcmdCommand.ExecuteReader().Close();
			
			foreach (IDbDataParameter Parameter in this.J_IdbcmdCommand.Parameters)
			{
				if (((Parameter.Direction != ParameterDirection.InputOutput) && (Parameter.Direction != ParameterDirection.Output)) && (Parameter.Direction != ParameterDirection.ReturnValue))
					continue;
				outParameters.Add(Parameter.ParameterName, Parameter.Value);
			}
			reader = this.J_IdbcmdCommand.ExecuteReader();
			return reader;
		}
		#endregion

		#region EXEC STORE PROCEDURE CONTAINS OUT PARAMETERS RETURN DATA SET [ OVERLOADED METHOD ]
		
		#region EXEC STORE PROCEDURE CONTAINS OUT PARAMETERS RETURN DATA SET [1]
		public DataSet J_ExecStoreProcedureContainsOutParametersReturnDataSet(string SqlText, out Hashtable outParameters, params object[] Parameters)
		{
			return this.J_ExecStoreProcedureContainsOutParametersReturnDataSet(SqlText, out outParameters, "Table", Parameters);
		}
		#endregion

		#region EXEC STORE PROCEDURE CONTAINS OUT PARAMETERS RETURN DATA SET [2]
		public DataSet J_ExecStoreProcedureContainsOutParametersReturnDataSet(string SqlText, out Hashtable outParameters, string srcTableName, params object[] Parameters)
		{
			IDataAdapter adapter = null;
			DataSet dataSet = null;
			outParameters = new Hashtable();
			this.J_ValidateConnection();
			
			this.J_IdbcmdCommand.CommandText = SqlText;
			this.J_IdbcmdCommand.CommandType = CommandType.StoredProcedure;
			this.J_AppendParameters(ref this.J_IdbcmdCommand, Parameters);
			dataSet = new DataSet();
			
			switch (this.J_enmDatabaseType)
			{
				case J_DatabaseType.Sql:
					adapter = new SqlDataAdapter((SqlCommand) this.J_IdbcmdCommand);
					((SqlDataAdapter) adapter).Fill(dataSet, srcTableName);
					break;

				case J_DatabaseType.OleDb:
					adapter = new OleDbDataAdapter((OleDbCommand) this.J_IdbcmdCommand);
					((OleDbDataAdapter) adapter).Fill(dataSet, srcTableName);
					break;

				case J_DatabaseType.Odbc:
					adapter = new OleDbDataAdapter((OleDbCommand) this.J_IdbcmdCommand);
					((OleDbDataAdapter) adapter).Fill(dataSet, srcTableName);
					break;

				case J_DatabaseType.Oracle:
					adapter = new OracleDataAdapter((OracleCommand) this.J_IdbcmdCommand);
					((OracleDataAdapter) adapter).Fill(dataSet, srcTableName);
					break;

				default:
					adapter = new SqlDataAdapter((SqlCommand) this.J_IdbcmdCommand);
					((SqlDataAdapter) adapter).Fill(dataSet, srcTableName);
					break;
			}
		
			foreach (IDbDataParameter Parameter in this.J_IdbcmdCommand.Parameters)
			{
				if (((Parameter.Direction != ParameterDirection.InputOutput) && (Parameter.Direction != ParameterDirection.Output)) && (Parameter.Direction != ParameterDirection.ReturnValue))
					continue;
				outParameters.Add(Parameter.ParameterName, Parameter.Value);
			}
			return dataSet;
		}
		#endregion
		
		#endregion

		#endregion

		#endregion

		#region USER DEFINE PROPERTIES
		
		#region APPLICATION TYPE
		public J_ApplicationType J_pTypeOfApplication
		{
			get
			{
				return this.J_enmApplicationType;
			}
			set
			{
				this.J_enmApplicationType = value;
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

		#region DATABASE TYPE
		public J_DatabaseType J_pDatabaseType
		{
			get
			{
				return this.J_enmDatabaseType;
			}
			set
			{
				this.J_enmDatabaseType = (J_DatabaseType) value;
			}
		}
		#endregion

		#endregion

	}
	#endregion
}
