
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: JCSharpCommon
Version			: 1.1
Start Date		: 
End Date		: 
Class Desc		: Implemented Global methods
_________________________________________________________________________________________________________

*/

#endregion

namespace JAYA.CSharp.Services.Data
{	
	#region Refered Namespaces

	//~~~ Windows Namespace ~~~
	using System;
	using System.Text.RegularExpressions;
	using System.Data;
	using System.Data.SqlClient;
	using System.Windows.Forms;
	using Microsoft.VisualBasic.Compatibility.VB6;

	//~~~ Web Namespace ~~~
	using System.Web.UI.WebControls;
	
	#endregion

	public class JCSharpCommon
	{

		#region Private Variable Decleration

		//~~~~ Use to store the Provider within this class ~~~~
		private string strProvider;

		//~~~~ Use to store the SQL Query within this class ~~~~
		private string strSQL;
		
		//~~~~ Use to store the Boolean Value within this class ~~~~
		private bool blnReturnValue = false;

		#endregion

		#region [ Overloaded Method ] CONSTRUCTOR 

		public JCSharpCommon()
		{
			strProvider = "SQLServer".ToUpper();
		}

		public JCSharpCommon(string sProvider_Access_SQLServer_Oracle)
		{
			strProvider = sProvider_Access_SQLServer_Oracle.ToUpper();
		}

		#endregion

		#region [ Overloaded Method ] gIsRecordExist 

		#region gIsRecordExist [ 1 To 2 ]
		
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn, string sTableName)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) FROM " + sTableName;
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 3 ]
		
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Other Criteria
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sOtherCriteria)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) FROM " + sTableName + " ";
			
			//~~~~ any other criteria ~~~~
			if( sOtherCriteria != "" )
				strSQL = strSQL + "WHERE " + sOtherCriteria + " ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 4 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			string sFieldValue)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " + 
				"FROM   " + sTableName + " " +
				"WHERE  UPPER(" + sFieldName + ") = '" + gReplaceQuote(sFieldValue.Trim().ToUpper()) + "' ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 4 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			long lFieldValue)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " +
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " ";
				
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 5 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Other Criteria
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			string sFieldValue,
			string sOtherCriteria)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " + 
				"FROM   " + sTableName + " " +
				"WHERE  UPPER(" + sFieldName + ") = '" + gReplaceQuote(sFieldValue.Trim().ToUpper()) + "' ";
			
			//~~~~ any other criteria ~~~~
			if( sOtherCriteria != "" )
				strSQL = strSQL + "AND " + sOtherCriteria + " ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 5 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Other Criteria
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			long lFieldValue,
			string sOtherCriteria)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " +
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " ";
				
			//~~~~ any other criteria ~~~~
			if( sOtherCriteria != "" )
				strSQL = strSQL + "AND " + sOtherCriteria + " ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 6 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Key Field Id for Modification purpose (Optional)
		//~~~~ 6.Value of the Key Field Id (Optional)
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			string sFieldValue,
			string sFieldId,
			long lFieldIdValue)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " + 
				"FROM   " + sTableName + " " +
				"WHERE  UPPER(" + sFieldName + ") = '" + gReplaceQuote(sFieldValue.Trim().ToUpper()) + "' ";
				
			//~~~~ Id Field & Id Value is exist ~~~~
			if( sFieldId != "" && Convert.ToInt64(lFieldIdValue) != 0 )
				strSQL = strSQL + "AND " + sFieldId + " != " + Convert.ToInt64(lFieldIdValue) + " ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion
		
		#region gIsRecordExist [ 1 To 6 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Key Field Id for Modification purpose (Optional)
		//~~~~ 6.Value of the Key Field Id (Optional)
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			long lFieldValue,
			string sFieldId,
			long lFieldIdValue)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " +
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " ";
				
			//~~~~ Id Field & Id Value is exist ~~~~
			if( sFieldId != "" && Convert.ToInt64(lFieldIdValue) != 0 )
				strSQL = strSQL + "AND " + sFieldId + " != " + Convert.ToInt64(lFieldIdValue) + " ";
			
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 7 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Key Field Id for Modification purpose (Optional)
		//~~~~ 6.Value of the Key Field Id (Optional)
		//~~~~ 7.Any other Criteria for 'AND' clause (Optional)
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			string sFieldValue,
			string sFieldId,
			long lFieldIdValue,
			string sOtherCriteria)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " + 
				"FROM   " + sTableName + " " +
				"WHERE  UPPER(" + sFieldName + ") = '" + gReplaceQuote(sFieldValue.Trim().ToUpper()) + "' ";
				
			//~~~~ Id Field & Id Value is exist ~~~~
			if( sFieldId != "" && Convert.ToInt64(lFieldIdValue) != 0 )
				strSQL = strSQL + "AND " + sFieldId + " != " + Convert.ToInt64(lFieldIdValue) + " ";
			
			//~~~~ any other criteria ~~~~
			if( sOtherCriteria != "" )
				strSQL = strSQL + "AND " + sOtherCriteria + " ";
		
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion

		#region gIsRecordExist [ 1 To 7 ]

		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ This function checking the data is exist or not taking some criteria ~~~~
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		//~~~~ 1.Connection Object
		//~~~~ 2.Lookup Table
		//~~~~ 3.Lookup Field
		//~~~~ 4.Input Value
		//~~~~ 5.Key Field Id for Modification purpose (Optional)
		//~~~~ 6.Value of the Key Field Id (Optional)
		//~~~~ 7.Any other Criteria for 'AND' clause (Optional)
		//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		public bool gIsRecordExist(ref SqlConnection cn,
			string sTableName,
			string sFieldName,
			long lFieldValue,
			string sFieldId,
			long lFieldIdValue,
			string sOtherCriteria)
		{
			//~~~~ create the SQL ~~~~
			strSQL = "SELECT COUNT(*) " +
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " ";
				
			//~~~~ Id Field & Id Value is exist ~~~~
			if( sFieldId != "" && Convert.ToInt64(lFieldIdValue) != 0 )
				strSQL = strSQL + "AND " + sFieldId + " != " + Convert.ToInt64(lFieldIdValue) + " ";
			
			//~~~~ any other criteria ~~~~
			if( sOtherCriteria != "" )
				strSQL = strSQL + "AND " + sOtherCriteria + " ";
		
			//~~~~ create & initialise the data table object ~~~~
			DataTable dtblIsRecordExist = gReturnDataTable(strSQL,ref cn);
			if( dtblIsRecordExist.Rows.Count > 0 )
			{
				//~~~~ data table object check the data is exist or not ~~~~
				if( Convert.ToInt64(dtblIsRecordExist.Rows[0][0]) > 0 )
				{
					blnReturnValue = true;
				}
			}
			return blnReturnValue;
		}

		#endregion
				
		#endregion

		#region [ Overloaded Method ] gGetMaxId 
		
		//-------------------------------------------------------------------------
		//-- This function returns the Maximum Id value 
		//-- for a table and if other criteria is available
		//-- so including other criteria
		//-------------------------------------------------------------------------
		public long gGetMaxId(ref SqlConnection cn,
			string sTableName,
			string sIdField)
		{
			//-- make the SQL
			strSQL = "SELECT ISNULL(Max(" + sIdField + "),0) AS MAX_ID " +
				"FROM   " + sTableName + " ";
		
			//-- create & initialise the data table object
			SqlCommand cmdGetMaxId = new SqlCommand(strSQL,cn);
			long lngGetMaxId = (long)cmdGetMaxId.ExecuteScalar();
			cmdGetMaxId.Dispose();
			return lngGetMaxId;
		}

		//-------------------------------------------------------------------------
		//-- This function returns the Maximum Id value 
		//-- for a table and if other criteria is available
		//-- so including other criteria
		//-------------------------------------------------------------------------
		public long gGetMaxId(ref SqlConnection cn,
			string sTableName,
			string sIdField,
			string sOtherCriteria)
		{
			//-- make the SQL
			strSQL = "SELECT ISNULL(Max(" + sIdField + "),0) AS MAX_ID " +
				"FROM   " + sTableName + " ";
		
			//-- Any other criteria
			if( sOtherCriteria != "" )
				strSQL = strSQL + "WHERE " + sOtherCriteria;
		
			//-- create & initialise the data table object
			SqlCommand cmdGetMaxId = new SqlCommand(strSQL,cn);
			long lngGetMaxId = (long)cmdGetMaxId.ExecuteScalar();
			cmdGetMaxId.Dispose();
			return lngGetMaxId;
		}

		#endregion

		#region [ Overloaded Method ] gGetId 
		
		#region gGetId [ If value String Type ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,string sFieldValue)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE UPPER(" + sFieldName + ") = '" + sFieldValue.Trim().ToUpper() + "' ";

			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#region gGetId [ If Value string type & some other criteria ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,string sFieldValue,string sOtherCriteria)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE  UPPER(" + sFieldName + ") = '" + sFieldValue.Trim().ToUpper() + "' " +
				"AND    " + sOtherCriteria + "";
			
			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#region gGetId [ If Value long type ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,long lFieldValue)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " ";
			
			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#region gGetId [ If Value long type & some other criteria ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,long lFieldValue,string sOtherCriteria)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = " + Convert.ToInt64(lFieldValue) + " " +
				"AND    " + sOtherCriteria + "";
			
			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#region gGetId [ If Value Date type ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,DateTime dFieldValue)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = '" + dFieldValue.ToShortDateString() + "' ";
			
			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#region gGetId [ If Value Date type & some other criteria ]
		
		public long gGetId(ref SqlConnection cn,string sTableName,string sIdField,string sFieldName,DateTime dFieldValue,string sOtherCriteria)
		{
			long lngGetId = 0;
			
			//-- make the SQL
			strSQL = "SELECT " + sIdField + " " + 
				"FROM   " + sTableName + " " +
				"WHERE  " + sFieldName + " = '" + dFieldValue.ToShortDateString() + "' " +
				"AND    " + sOtherCriteria + "";
			
			//-- create & initialise the data table object
			DataTable dtblGetId = gReturnDataTable(strSQL,ref cn);
			
			if( dtblGetId.Rows.Count > 0 )
				if((long)dtblGetId.Rows[0][0] > 0)
					lngGetId = (long)dtblGetId.Rows[0][0];
			return lngGetId;
		}

		#endregion

		#endregion

		#region [ Overloaded Method ] IsDate 

		public bool IsDate(string sString)
		{
			if (sString == null)
				sString = "";
		    
			if (sString.Length > 0)
			{
				//DateTime dummyDate = null;
				try
				{
					//dummydate = DateTime.Parse(sString);
				}
				catch
				{
					return false;
				}
				return true;
			}
			else
				return false;
		}

		public bool IsDate(string sString, out DateTime ResultDate)
		{
			bool blnIsDate = true;
			
			if (sString == null)
				sString = "";
			    
			try
			{
				ResultDate = DateTime.Parse(sString);
			}
			catch 
			{
				ResultDate = DateTime.MinValue;
				blnIsDate = false;
			}
			return blnIsDate;
		}
		
		#endregion

		#region gReturnDataTable
		
		//~~~~ This function return the data table ~~~~
		public DataTable gReturnDataTable(string sQueryString,ref SqlConnection cn)
		{
			//~~~~ create the data table object ~~~~
			DataTable dtblReturnDataTable = new DataTable();
			
			if( sQueryString.Trim() != "" )
			{
				//~~~~ create & initialise the data adapter object ~~~~
				SqlDataAdapter dadpReturnDataTable = new SqlDataAdapter(sQueryString, cn);
	
				//~~~~ fill data table object through data adapter object ~~~~
				dadpReturnDataTable.Fill(dtblReturnDataTable);
			}
			return dtblReturnDataTable;
		}

		#endregion

		#region gReturnDataSet

		//~~~~ This function return the data set ~~~~
		public DataSet gReturnDataSet(string sQueryString,ref SqlConnection cn)
		{
			//~~~~ create the data set object ~~~~
			DataSet dsetReturnDataSet = new DataSet();
			
			if( sQueryString.Trim() != "" )
			{

				//~~~~ create & initialise the data adapter object ~~~~
				SqlDataAdapter dadpReturnDataSet = new SqlDataAdapter(sQueryString, cn);
	
				//~~~~ fill data set object through data adapter object ~~~~
				dadpReturnDataSet.Fill(dsetReturnDataSet);
			}
			return dsetReturnDataSet;
		}

		#endregion

		#region gReturnDataReader

		//~~~~ This function return the data set ~~~~
		public SqlDataReader gReturnDataReader(string sQueryString,ref SqlConnection cn)
		{
			//~~~~ create the data reader object ~~~~
			SqlDataReader drdReturnDataReader = null;
			
			if( sQueryString.Trim() != "" )
			{
				//~~~~ create & initialise the data command object ~~~~
				SqlCommand cmdReturnDataReader = new SqlCommand(sQueryString, cn);
	
				//cmdDataReader.Connection = cn;
				//cmdDataReader.CommandType = CommandType.Text;
				//cmdDataReader.CommandText = sQueryString;

				//~~~~ fill data reader object through data command object ~~~~
				drdReturnDataReader = cmdReturnDataReader.ExecuteReader();	
			}
			return drdReturnDataReader;
		}

		#endregion

		#region gReplaceQuote
		
		public string gReplaceQuote(string sString)
		{
			return Regex.Replace(sString, "'", "''");
		}

		#endregion

		#region gNullToText
				
		public string gNullToText(object oObject)
		{
			if(oObject == null )
				return "";
			else
				return oObject.ToString();
		}

		#endregion

		#region gNullToZero
		
		public double gNullToZero(object oObject)
		{
			if(oObject == null )
				return 0;
			else
                return Convert.ToDouble(oObject);
		}
		
		#endregion

		#region gServerDate
		
		//~~~~ This function return the server date ~~~~
		public string gServerDate(ref SqlConnection cn)
		{
			string strDateTime;

			if( strProvider.ToUpper() == "ACCESS" )
				strSQL = "select date()";
			else if( strProvider == "SQLSERVER" )
				strSQL = "select convert(char(20),getdate(),109)";
			else if( strProvider == "ORACLE" )
				strSQL = "select TO_CHAR(SYSDATE,'dd/MM/yyyy') FROM DUAL";
			
			//~~~~ create & initialise the command object ~~~~
			SqlCommand cmdServerDate = new SqlCommand(strSQL,cn);
			//cmdTransaction.CommandText = "select convert(char(20),getdate(),109)";
			//cmdTransaction.Connection = cn;
			strDateTime = (string)cmdServerDate.ExecuteScalar();
			cmdServerDate.Cancel();
			cmdServerDate.Dispose();

			//~~~~ return the Server Date ~~~~
			return strDateTime;
		}
			
		#endregion

		#region gPopulateComboBox
		
		//~~~~ This function populate data into the combo box ~~~~
		public void gPopulateComboBox(ref SqlConnection cn,
									ref ComboBox cComboBox,
									string sQueryString)
		{
			//------------------------------------------------------------------------------
			//~~~~ declare & initialise the command object ~~~~
			//~~~~ and executed the command object which is stored into the ~~~~
			//~~~~ data reader object ~~~~
			//------------------------------------------------------------------------------
			SqlCommand cmdPopulateComboBox = new SqlCommand(sQueryString, cn);
			SqlDataReader drdPopulateComboBox = null;
			drdPopulateComboBox = cmdPopulateComboBox.ExecuteReader();
	
			//~~~~ clear the combo box ~~~~
			cComboBox.Items.Clear();
			cComboBox.Items.Add("");
			
			//~~~~ data reader object read the data and stored into combo box ~~~~
			if( drdPopulateComboBox.HasRows )
			{
				while(drdPopulateComboBox.Read())
				{
					cComboBox.Items.Add(new ListBoxItem(drdPopulateComboBox.GetString(1).ToString(), (int)drdPopulateComboBox.GetValue(0)));
				}
			}
			cComboBox.SelectedIndex = 0;
			
			//~~~~ data reader object is closed ~~~~
			drdPopulateComboBox.Close();
			cmdPopulateComboBox.Cancel();
			cmdPopulateComboBox.Dispose();
		}
		
		#endregion

		#region gPopulateDropDownList

		//~~~~ This function populate data into the Drop Down List ~~~~
		public void gPopulateDropDownList(ref SqlConnection cn,
										ref DropDownList dDropDownList,
										string sQueryString)
		{
		
			//------------------------------------------------------------------------------
			//~~~~ declare & initialise the command object ~~~~
			//~~~~ and executed the command object which is stored into the ~~~~
			//~~~~ data reader object ~~~~
			//------------------------------------------------------------------------------
			SqlCommand cmdPopulateDropDownList = new SqlCommand(sQueryString, cn);
			SqlDataReader drdPopulateDropDownList = null;
			drdPopulateDropDownList = cmdPopulateDropDownList.ExecuteReader();
	
			//~~~~ clear the combo box ~~~~
			dDropDownList.Items.Clear();
			dDropDownList.Items.Add("");
			
			//~~~~ data reader object read the data and stored into combo box ~~~~
			if( drdPopulateDropDownList.HasRows )
				while(drdPopulateDropDownList.Read())
					dDropDownList.Items.Add(new ListItem(drdPopulateDropDownList.GetString(1).ToString(),
						drdPopulateDropDownList.GetString(0).ToString()));
		
			dDropDownList.SelectedIndex = 0;
			
			//~~~~ data reader object is closed ~~~~
			drdPopulateDropDownList.Close();
			cmdPopulateDropDownList.Cancel();
			cmdPopulateDropDownList.Dispose();
		}

		#endregion

		#region AutoCompleteCombo_KeyUp
		
		//~~~~ This function searching substring wise ~~~~
		public void AutoCompleteCombo_KeyUp(ComboBox cComboBox,KeyEventArgs e)
		{
			string strTypedText;
			int intFoundIndex;
			object objFoundItem;
			string strFoundText;
			string strAppendText;
			
			//~~~~ Allow select keys without Autocompleting ~~~~
			switch(e.KeyCode) 
			{
				case Keys.Back : return;
				case Keys.Left : return;
				case Keys.Right : return;
				case Keys.Up : return;
				case Keys.Delete : return;
				case Keys.Down : return;
			}
			
			//~~~~ Get the Typed Text and Find it in the list ~~~~
			strTypedText = cComboBox.Text;
			intFoundIndex = cComboBox.FindString(strTypedText);

			//~~~~ If we found the Typed Text in the list then Autocomplete ~~~~
			if( intFoundIndex >= 0 )
			{
				//-------------------------------------------------------------
				//~~~~ Get the Item from the list (Return Type depends if Datasource was bound ~~~~
				//~~~~ or List Created) ~~~~
				//-------------------------------------------------------------
				objFoundItem = cComboBox.Items[intFoundIndex];
				
				//-------------------------------------------------------------
				//~~~~ Use the ListControl.GetItemText to resolve the Name in case the Combo  ~~~~
				//~~~~ was Data bound ~~~~
				//-------------------------------------------------------------
				strFoundText = cComboBox.GetItemText(objFoundItem);
				
				//~~~~ Append then found text to the typed text to preserve case ~~~~
				strAppendText = strFoundText.Substring(strTypedText.Length);
				cComboBox.Text = strTypedText + strAppendText;
				
				//~~~~ Select the Appended Text ~~~~
				cComboBox.SelectionStart = strTypedText.Length;
				cComboBox.SelectionLength = strAppendText.Length;
			}
		}

		#endregion

		#region AutoCompleteCombo_Leave
		
		//~~~~ This function select the listindex substring wise ~~~~
		public void AutoCompleteCombo_Leave(ComboBox cComboBox)
		{
			//-------------------------------------------------------------
			//-- iFoundIndex = IIf(cbo.FindStringExact(cbo.Text.Trim) <= 0, 0, cbo.FindStringExact(cbo.Text.Trim))
			//-------------------------------------------------------------
			int intFoundIndex = cComboBox.FindStringExact(cComboBox.Text.Trim());
			cComboBox.SelectedIndex = intFoundIndex;
		}

		#endregion

		#region Left
		
		public string Left(string sString, int length)
		{
			//we start at 0 since we want to get the characters starting from the
			//left and with the specified lenght and assign it to a variable
			string result = sString.Substring(0, length);
			//return the result of the operation
			return result;
		}

		#endregion

		#region Right
		
		public string Right(string sString, int length)
		{
			//start at the index based on the lenght of the sting minus
			//the specified lenght and assign it a variable
			string result = sString.Substring(sString.Length - length, length);
			//return the result of the operation
			return result;
		}

		#endregion

		#region Mid
		
		public string Mid(string sString,int startIndex, int length)
		{
			//start at the specified index in the string ang get N number of
			//characters depending on the lenght and assign it to a variable
			string result = sString.Substring(startIndex, length);
			//return the result of the operation
			return result;
		}

		public string Mid(string sString,int startIndex)
		{
			//start at the specified index and return all characters after it
			//and assign it to a variable
			string result = sString.Substring(startIndex);
			//return the result of the operation
			return result;
		}

		#endregion

		#region IsNumeric

		public bool IsNumeric(string sString)
		{
			if (sString == null)
				sString = "";
			
			if (sString.Length > 0)
			{
				double dummyOut = new double();
				System.Globalization.CultureInfo cultureInfo = 
					new System.Globalization.CultureInfo("en-US", true);
				return Double.TryParse(sString, System.Globalization.NumberStyles.Any,
					cultureInfo.NumberFormat, out dummyOut);    
			}    
			else
				return false;    
		}

		#endregion
		
	}
}
