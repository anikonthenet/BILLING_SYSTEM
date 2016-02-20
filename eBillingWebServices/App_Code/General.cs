
#region Imported Namespace

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;

#endregion

namespace JayaSoftwares
{
    #region Enum Declaration
    
    public enum SqlType : uint
    {
        TableOnly = 1,
        QueryOnly = 2
    }

    public enum Campaign : int
    {
        PDSInfotech = 1,
        SimpleTaxIndia = 2,
        eTaxIndia = 3,
        RKSolutions = 4
    }

    public enum Product_Brand : int
    {
        TDSMAN = 1
    }
    

    public enum Software_Product : int
    {
        TDSMAN = 1,
        TDSMAN_CORRECTION = 2,
        CHEQUEMAN = 4
    }


    public enum Software_Product_FY : int
    {
        FY_2011_12 = 12
    }

    public enum Install_Status : int
    {
        Installed = 1,
        Uninstalled = 0
    }

    #endregion

    #region gUserMessage

    public struct gUserMessage
    {
        public const string AddRecordSuccess        = "Record Save Successfully !!";
        public const string AddRecordFailure        = "Record Save failed !!";
        public const string AddRecordRestriction    = "Record can not be save !!";
        public const string EditRecordSuccess       = "Record Modified Successfully !!";
        public const string EditRecordFailure       = "Record Modified Failied !!";
        public const string EditRecordRestriction   = "Record can not be Modified !!";
        public const string DeleteRecordSuccess     = "Record Deleted Successfully !!";
        public const string DeleteRecordFailure     = "Record Deleted Failed !!";
        public const string DeleteRecordRestriction = "Record Can not be Deleted!!";
        public const string TransactionSuccess      = "Transaction Successfull !!";
        public const string TransactionFailure      = "Transaction Failure !!";
        public const string RecordReference         = "This Record has reference !!";
        public const string DBError                 = "Database error ocurs !!";
        public const string NoRecords               = "Records not found !!";
        public const string UploadSuccess           = "File Upload Success !!";
        public const string UploadFailure           = "File Upload Failed !!";
        public const string FileCopySuccess         = "File copy Success !!";
        public const string FileCopyFailure         = "File copy Failed !!";
    }

    #endregion
    
    #region General

    public class General
    {      


        #region Variables Declares

        DBHelper objHelper = new DBHelper();

        int intNoOfRecords = 0;
        string strSqlQuery = string.Empty;

        #endregion

        /* -------------------------------------------------------------------------------------------
        * Function - To Populating the Drop Down List.
        * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gPopulateDropDownList

        #region gPopulateDropDownList [1]
        /// <summary>
        ///     This Function Populates the Drop Down List By the Sql Query, Here "-----Select-----" Will be the First Value
        /// </summary>
        /// <param name="DDLObject">DROPDOWN LIST's ID</param>
        /// <param name="strSQLQuery">SQL Query</param>
        /// <param name="strColumnField_DDLText">Data Text Column Name</param>
        /// <param name="strColumnField_DDLValue">Data Value Column Name</param>        
        public void gPopulateDropDownList(DropDownList DDLObject, string strSQLQuery, string strColumnField_DDLText, string strColumnField_DDLValue)
        {
            try
            {

                if (strSQLQuery != null || strSQLQuery != "")
                {                    
                    objHelper.gOpenConnection();

                    DDLObject.Items.Clear();

                    DDLObject.DataSource =objHelper.gReturnDataSet(CommandType.Text,strSQLQuery);

                    DDLObject.DataTextField = strColumnField_DDLText;
                    DDLObject.DataValueField = strColumnField_DDLValue;

                    DDLObject.DataBind();
                }

                DDLObject.Items.Insert(0, "--------------- Select ---------------");

                DDLObject.SelectedIndex = 0;
            }
            finally
            {
                objHelper.gCloseConnection();
            }
        }
        #endregion

        #region gPopulateDropDownList [2]
        /// <summary>
        ///     This Function Populates the Drop Down List By the Sql Query, Here the First Value will be as per User provided Into the Param.
        /// </summary>
        /// <param name="DDLObject">DROPDOWN LIST's ID</param>
        /// <param name="strSQLQuery">SQL Query</param>
        /// <param name="strColumnField_DDLText">Data Text Column Name</param>
        /// <param name="strColumnField_DDLValue">Data Value Column Name</param>        
        /// <param name="strSelectedString">Selected String For DDL</param>
        public void gPopulateDropDownList(DropDownList DDLObject, string strSQLQuery, string strColumnField_DDLText, string strColumnField_DDLValue, string strSelectedString)
        {

            if (strSQLQuery != null || strSQLQuery != "")
            {
                DDLObject.Items.Clear();

                DDLObject.DataSource = objHelper.gReturnDataSet(CommandType.Text, strSQLQuery);

                DDLObject.DataTextField = strColumnField_DDLText;
                DDLObject.DataValueField = strColumnField_DDLValue;

                DDLObject.DataBind();
            }

            DDLObject.Items.Insert(0, strSelectedString);
            DDLObject.SelectedIndex = 0;
        }

        #endregion

        #region gPopulateDropDownList [3]
        /// <summary>
        ///  This Function Populated the DROPDOWNLIST by The SQL Query
        /// </summary>
        /// <param name="DDLObject">DROP DOWN LIST</param>
        /// <param name="strSQLQuery">SQL Query</param>
        /// <param name="strSelectedString">String Value for Selected Into the Drop Down List</param>

        public void gPopulateDropDownList(DropDownList DDLObject, string strSQLQuery, string strSelectedString)
        {
            try
            {
                if (strSQLQuery != null || strSQLQuery != "")
                {
                    DDLObject.Items.Add(new ListItem(strSelectedString, "0"));

                    objHelper.gOpenConnection();

                    IDataReader dataReader = objHelper.gExecuteReader(CommandType.Text, strSQLQuery);

                    while (dataReader.Read())
                    {
                        DDLObject.Items.Add(new ListItem(dataReader[1].ToString(), dataReader[0].ToString()));
                    }

                    dataReader.Close();
                }
            }
            finally
            {
                objHelper.gCloseConnection();
            }
        }
        #endregion
             
        
        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Checking the Existence of the Record from the Database	
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gIsRecordExist
        
        /// <summary>
        ///     This Function Accepts the Table Name of SQL Query and The Type of Query Type and Checking the Existance Of Records
        ///     into the Table.params object[] objFieldNameValue is a optional parameter.
        /// </summary>
        /// <param name="strTableName_strSQL">Table Name or SQL Query</param>
        /// <param name="enmQueryType">QUERY TYPE - Text of TableName</param>
        /// <param name="objFieldNameValue">send column name & its value separated by comma </param> 
        /// <returns>it Return bolean values</returns>
        public bool gIsRecordExist(string strTableName_strSQL, SqlType enmQueryType, params object[] objFieldNameValue)
        {
            bool bnlStatus = false;
            string strFieldName = string.Empty;            
            bool bnlMultipleField = false;
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
                    strSqlQuery = strSqlQuery +" WHERE  ";

                    for (int i = 0; i < objFieldNameValue.Length;i++ )
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
                            strFieldName =  objFieldNameValue[i].ToString();

                        j++;
                    }
                }

                // IF THERE ARE  1 VALUES PASSED
                if (objFieldNameValue.Length == 1)                
                    strSqlQuery = strSqlQuery + " WHERE  " + objFieldNameValue[0].ToString();
                
                //NOW GET NO OF RECORDS FROM DATABASE 
                if (gExecuteRecordExistenceQuery(strSqlQuery) == true)
                    bnlStatus = true;
            }
            catch
            {
                bnlStatus = false;
            }

            return bnlStatus;
        }     


        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Checking the Expression is Numeric or Not
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gIsNumeric

        public bool gIsNumeric(string Expression)
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

        /* -------------------------------------------------------------------------------------------
		 * Function - For Execute the Record Existance Checking Query
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gExecuteRecordExistenceQuery
        /// <summary>
        ///     This Function Checking the Existanc of the Record into the Table Return by the SQL Query
        /// </summary>
        /// <param name="strSqlQuery">SQL (SELECT) Query</param>
        /// <returns></returns>
        public bool gExecuteRecordExistenceQuery(string strSqlQuery)
        {
            bool bnlStatus = false;
            intNoOfRecords = 0;

            try
            {
                objHelper.gOpenConnection();

                IDataReader reader = objHelper.gExecuteReader(CommandType.Text, strSqlQuery);
                              
                    while (reader.Read())                    
                        intNoOfRecords = Convert.ToInt16(reader[0]);                                    

                if (intNoOfRecords > 0)
                    bnlStatus = true;
            }
            catch
            {
                bnlStatus = false;
            }
            finally
            {
                objHelper.gCloseConnection();
            }

            return bnlStatus;
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Replace Character - Convert the ' into '' while saving data into the database
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gReplaceCharacter

        public string gReplaceQuotes(string strValue)
        {
            return (strValue.Replace("'", "''"));
        }


        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Getting the Next Id of the Column of a Table
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gGetNextMaxId
        /// <summary>
        ///     This Function Fetching the Maximum Id of a Column of a table and By Adding 1 with The Maximum ID 
        ///     for Getting the Next Maximum ID. 
        /// </summary>
        /// <param name="strTblName">Table Name</param>
        /// <param name="strIdFieldName">Column Name</param>
        /// <returns></returns>
        public long gGetNextMaxId(string strTableName, string strIdFieldName)
        {
            long lngMaxId = 0;

            try
            {
                strSqlQuery = "SELECT MAX(" + strIdFieldName + ") FROM " + strTableName;

                objHelper.gOpenConnection();

               IDataReader reader = objHelper.gExecuteReader(CommandType.Text,strSqlQuery);
                              
                    while (reader.Read())
                    {
                        if (string.IsNullOrEmpty(reader[0].ToString()) == false)
                            lngMaxId = Convert.ToInt64(reader[0]);
                    }                

                reader.Close();

                objHelper.gCloseConnection();

                if (lngMaxId >= 0)
                    lngMaxId = lngMaxId + 1;

            }
            catch
            {
                lngMaxId = 0;
            }

            return lngMaxId;

        }

        #endregion
        
        /* -------------------------------------------------------------------------------------------
		 * Function - For Getting the Maximum ID of Column of a Table
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gMaxId
        /// <summary>
        ///     This Function Calculates the Maximum ID of a Column of a Table.
        /// </summary>
        /// <param name="strTblName">Table Name</param>
        /// <param name="strIdFieldName">Column Name of that Table</param>
        /// <returns></returns>
        public long gMaxId(string strTblName, string strIdFieldName)
        {
            long lngMaxId = 0;

            try
            {
                strSqlQuery = "SELECT MAX(" + strIdFieldName + ") FROM " + strTblName;

                objHelper.gOpenConnection();

                IDataReader reader = objHelper.gExecuteReader(CommandType.Text, strSqlQuery);
                              
                    while (reader.Read())
                    {
                        lngMaxId = Convert.ToInt64(reader[0]);
                    }                

                reader.Close();
                objHelper.gCloseConnection();
            }
            catch
            {
                lngMaxId = 0;
            }

            return lngMaxId;
        }
        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Convert Null Value into Text
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gConvertNullToText

        public object gConvertNullToText(object obj)
        {
            if (Convert.IsDBNull(obj))
            {
                return "";
            }
            else
            {
                return obj;
            }

        }


        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Convert Null Value into Zero
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gConvertNullTOZero

        public int gConvertNullTOZero(Object Obj)
        {
            if (System.Convert.IsDBNull(Obj))
                return 0;
            else
                return Convert.ToInt32(Obj);
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
         * Function - Encoding the Values
         * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gEncode
        /// <summary>
        ///     This Function Encoding the String into Byte Format 
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public string gEncode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - Decoding the values
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gDecode
        /// <summary>
        /// This Function Decoding the Encoding Format
        /// </summary>
        /// <param name="str">string</param>
        /// <returns></returns>
        public string gDecode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Sending Mail to the respective Mail Ids.
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gSendMail
        /// <summary>
        /// This function will send email to multiple users
        /// </summary>
        /// <param name="strSubject">Subject of the email </param>
        /// <param name="strFrom">email address of the sender </param>
        /// <param name="listTo">generic string type variables for multiple recipient's email id </param>
        /// <param name="strMsgBody"> Body of the email may be simple message or html formatted </param>
        /// <param name="bnlMsgBodyType">Set true if your message body contain Html tag  </param>
        /// <returns></returns>
        public bool gSendMail(string strSubject, string strFrom, List<string> listTo, string strMsgBody, bool bnlMsgBodyType)
        {
            bool strReturnMsg = true;

            // Creating MailMessage Object
            MailMessage mailMsg = new MailMessage();

            // Setting Sender email id
            mailMsg.From = new MailAddress("<" + strFrom + ">");

            // Setting recipients email id
            //Iterate through list<string> generics
            foreach (string strTo in listTo)
            {
                mailMsg.To.Add(strTo);
            }

            // Assign Mail Subject
            mailMsg.Subject = strSubject;

            //Assign Message Body 
            mailMsg.Body = strMsgBody;

            //Assign Mail Body Type
            mailMsg.IsBodyHtml = bnlMsgBodyType;

            SmtpClient smtpClient = new SmtpClient();
            try
            {
                // message sending goes here
                smtpClient.Send(mailMsg);
            }
            //exception handling
            catch
            {
                strReturnMsg = false;
            }

            return strReturnMsg;
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Sending SMS to the Respective Phone Numbers.
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gSendSMS

        #region gSendSMS [1]
        /// <summary>
        ///     This Function will send the SMS to the Users         
        /// </summary>
        /// <param name="strSenderNameOrNo">Name or Number of the Sender</param>
        /// <param name="liStrToNos">Mobile Number to Whom the SMS will Send</param>
        /// <param name="strMessage">Message for the SMS</param>
        /// <returns></returns>
        public bool gSendSMS(string strSenderNameOrNo, List<string> liStrToNos, string strMessage)
        {
            // used as return value after executing
            bool bnlReturnStatus = true;
            string strTo = string.Empty;

            //Create object of type WebClient located in System.Net
            WebClient client = new WebClient();

            // Creating  NameValueCollection for parameter passing to url.this class located in System.Collection.Specialized
            NameValueCollection sendNameValueCollection = new NameValueCollection();

            // Url To send request to the server
            string uriString = ConfigurationManager.AppSettings["SmsProviderUrl"].ToString();

            // add userid query string from web.config file
            sendNameValueCollection.Add("userid", ConfigurationManager.AppSettings["UserId"].ToString());

            // add password query string from web.config file
            sendNameValueCollection.Add("password", ConfigurationManager.AppSettings["UserPassword"].ToString());

            // add submit_name query string 
            sendNameValueCollection.Add("submit_name", strSenderNameOrNo);

            //Extract all recipient's no & remove first coma separator
            foreach (string strToNo in liStrToNos)
                strTo = strTo + "," + strToNo;

            strTo = strTo.Remove(0, 1);

            //add recipient's no query string 
            sendNameValueCollection.Add("send_to", strTo);

            //add message_type to Zero to query string 
            sendNameValueCollection.Add("message_type", "0");

            //add message_text to Zero to query string 
            sendNameValueCollection.Add("message_text", strMessage);
            try
            {
                // Send Message to recipients & get the response from server 
                byte[] responseArray = client.UploadValues(uriString, "POST", sendNameValueCollection);
                string strGetRes = Encoding.ASCII.GetString(responseArray);
                sendNameValueCollection.Clear();//Clear Values from NameValueCollection object

            }
            catch
            {
                bnlReturnStatus = false;
            }

            return bnlReturnStatus;
        }

        #endregion

        #region gSendSMS [2]
        /// <summary>
        ///     This Function will send the SMS to the Users         
        /// </summary>
        /// <param name="strSenderNameOrNo">Name or Number of the Sender</param>
        /// <param name="liStrToNos">Mobile Number to Whom the SMS will Send</param>
        /// <param name="strMessage">Message for the SMS</param>
        /// <param name="strReturnMessage">Message will Return to the Sender's Mobile</param>
        /// <returns></returns>
        public bool gSendSMS(string strSenderNameOrNo, List<string> liStrToNos, string strMessage, out string strReturnMessage)
        {
            bool bnlReturnStatus = true;
            strReturnMessage = string.Empty;
            string strTo = string.Empty;

            string strStatus = "";

            string strScucess = "SUCCESS";
            string strErr = "ERROR";
            string strNotAllSuccess = "NOT_ALL_SUCCESS";
            string strNotOkUrl = "NOT_OK_URL";


            //Create object of type WebClient located in System.Net
            WebClient client = new WebClient();

            // Creating  NameValueCollection for parameter passing to url.this class located in System.Collection.Specialized
            NameValueCollection sendNameValueCollection = new NameValueCollection();

            // Url To send request to the server
            string uriString = ConfigurationManager.AppSettings["SmsProviderUrl"].ToString();

            // add userid query string from web.config file
            sendNameValueCollection.Add("userid", ConfigurationManager.AppSettings["UserId"].ToString());

            // add password query string from web.config file
            sendNameValueCollection.Add("password", ConfigurationManager.AppSettings["UserPassword"].ToString());

            // add submit_name query string 
            sendNameValueCollection.Add("submit_name", strSenderNameOrNo);

            //Extract all recipient's no & remove first coma separator
            foreach (string strToNo in liStrToNos)
                strTo = strTo + "," + strToNo;
            strTo = strTo.Remove(0, 1);

            //add recipient's no query string 
            sendNameValueCollection.Add("send_to", strTo);

            //add message_type to Zero to query string 
            sendNameValueCollection.Add("message_type", "0");

            //add message_text to Zero to query string 
            sendNameValueCollection.Add("message_text", strMessage);

            try
            {
                // Send Message to recipients & get the response from server 
                byte[] responseArray = client.UploadValues(uriString, "POST", sendNameValueCollection);
                string strGetRes = Encoding.ASCII.GetString(responseArray);
                sendNameValueCollection.Clear();//Clear Values from NameValueCollection object


                Match mStausString = Regex.Match(strGetRes, strScucess);

                // match server response stream with desire message  
                if (mStausString.Value.ToString() != "")
                {
                    strStatus = mStausString.Value.ToString();
                }
                else
                {
                    mStausString = Regex.Match(strGetRes, strErr);
                    if (mStausString.Value.ToString() != "")
                    {
                        strStatus = mStausString.Value.ToString();
                    }
                    else
                    {
                        mStausString = Regex.Match(strGetRes, strNotAllSuccess);
                        if (mStausString.Value.ToString() != "")
                        {
                            strStatus = mStausString.Value.ToString();
                        }
                        else
                        {
                            mStausString = Regex.Match(strGetRes, strNotOkUrl);
                            if (mStausString.Value.ToString() != "")
                            {
                                strStatus = mStausString.Value.ToString();
                            }
                        }
                    }
                }

                //retrun string matching message 
                if (strStatus == "SUCCESS")
                    strReturnMessage = "Message Successfully Sent";
                else if (strStatus == "NOT_ALL_SUCCESS")
                    strReturnMessage = "Not all bulk messages sent";
                else if (strStatus == "NOT_OK_URL")
                    strReturnMessage = "Url is not posted properly";
                else if (strStatus == "ERROR")
                    strReturnMessage = "This response indicate that an error had occurred. No message accepted for delivery";

            }
            catch
            {
                bnlReturnStatus = false;
            }

            return bnlReturnStatus;

        }

        #endregion

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Uploading Files into the Provided Path.
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gUploadFiles

        #region UploadFiles [1]
        /// <summary>
        ///     This Function Uploads the File into the Defined Path
        /// </summary>
        /// <param name="strPath">Path Name</param>
        /// <param name="ctlUpload">File Upload Control</param>
        /// <param name="page">From Which Page its been Uploaded</param>
        /// <returns>bool</returns>
        public bool gUploadFiles(string strPath, FileUpload ctlUpload, Page page)
        {
            bool blnReturn = true;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(page.Server.MapPath(strPath));

                if (dir.Exists == false)
                    dir.Create();

                if (ctlUpload.HasFile)
                    ctlUpload.SaveAs(page.Server.MapPath(strPath) + "\\" + ctlUpload.FileName);
            }
            catch
            {
                blnReturn = false;
            }

            return blnReturn;
        }
        #endregion

        #region UploadFiles [2]
        /// <summary>
        ///     This Function Uploads the File into the Defined Path
        /// </summary>
        /// <param name="strPath">Path Name</param>
        /// <param name="ctlUpload">File Upload Control</param>
        /// <param name="page">From Which Page its been Uploaded</param>
        /// <param name="strErrMsg">Display Error Message While Uploading</param>
        /// <returns></returns>
        public bool gUploadFiles(string strPath, FileUpload ctlUpload, Page page, out string strErrMsg)
        {
            bool blnReturn = true;
            strErrMsg = "";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(page.Server.MapPath(strPath));

                if (dir.Exists == false)
                    dir.Create();

                if (ctlUpload.HasFile)
                    ctlUpload.SaveAs(page.Server.MapPath(strPath) + "\\" + ctlUpload.FileName);
            }
            catch (Exception ErrMsg)
            {
                strErrMsg = ErrMsg.Message + " :Uploads Failed";
                blnReturn = false;
            }

            return blnReturn;
        }

        #endregion

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Resctrict Users to Tamper with the URL (Tamper Proof URL)
		 * -------------------------------------------------------------------------------------------*/
        #region FOR Digest into the URL

        #region GetDigest
        /// <summary>
        ///     GetDigest encrypt url's query string parameter
        /// </summary>
        /// <param name="tamperProofParams"></param>
        /// <returns></returns>
        private string GetDigest(string tamperProofParams)
        {
            string Digest = String.Empty;

            try
            {
                string input = String.Concat("Secret", tamperProofParams, "Secret");

                //		'The array of bytes that will contain the encrypted value of input
                byte[] hashedDataBytes;
                //		'The encoder class used to convert strPlainText to an array of bytes
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                //		'Create an instance of the MD5CryptoServiceProvider class
                System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
                //		'Call ComputeHash, passing in the plain-text string as an array of bytes
                //		'The return value is the encrypted value, as an array of bytes
                hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));
                //		'Base-64 Encode the results and strip off ending '==', if it exists
                Digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());
            }
            catch
            {
                Digest = String.Empty;
            }

            return Digest;
        }

        #endregion

        #region IsURLTampered
        /// <summary>
        ///     This function will encrypt url's Query parameter   
        /// </summary>
        /// <param name="strTamperProofParams"></param>
        /// <param name="strReceivedDigest"></param>
        /// <returns></returns>
        public bool IsURLTampered(string strTamperProofParams, string strReceivedDigest)
        {
            //		'Determine what the digest SHOULD be
            string expectedDigest = this.GetDigest(strTamperProofParams);
            //         'Any + in the digest passed through the querystring would be convereted into 'spaces, so 'uncovert' them


            if (strReceivedDigest != null && strReceivedDigest.Trim() != "")

                strReceivedDigest = strReceivedDigest.Replace(" ", "+");
            else
                return true;

            //		'Now, see if the received and expected digests match up
            if (string.Compare(expectedDigest, strReceivedDigest) != 0)
                return true;
            else
                return false;
            //		 'Don't match up, egad

        }
        #endregion

        #region CreateTamperProofURL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="strTamperProofParams"></param>
        /// <returns></returns>
        public string CreateTamperProofURL(string strUrl, string strTamperProofParams)
        {
            if (strTamperProofParams.Length > 0)
            {
                strUrl += "?" + strTamperProofParams;
                strUrl += String.Concat("&Digest=", this.GetDigest(strTamperProofParams));
            }
            return strUrl;
        }
        #endregion

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Set the Focus(Cursor Position) On the Specific Control
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: SetFocus
        /// <summary>
        ///     Setting the Focus on the Control
        /// </summary>
        /// <param name="strControlId">Control ID</param>
        /// <param name="objPage">Page Object</param>
        public void gSetFocus(string strControlId, Page objPage)
        {
            string strScript = "<Script Language=JavaScript>";
            strScript += "document.getElementById('" + strControlId + "').focus();";
            strScript += "</Script>";
            if (!objPage.ClientScript.IsClientScriptBlockRegistered("focusscript"))
            {
                objPage.ClientScript.RegisterStartupScript(this.GetType(), "focusscript", strScript);
            }
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Display a Message to the user
         * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: DisplayMessage
        /// <summary>
        ///     Display the Message on the Web Page
        /// </summary>
        /// <param name="strMesg">Message To Be Displayed</param>
        /// <param name="objPage">Page Object</param>
        public void gDisplayMessage(string strMesg, Page objPage)
        {
            if (strMesg != null || strMesg != "")
            {
                string strScript = "<Script Language=JavaScript>";
                strScript += "alert('" + strMesg.Replace("'","\\'") + "');";
                strScript += "</Script>";
                if (!objPage.ClientScript.IsClientScriptBlockRegistered("Alert"))
                {
                    objPage.ClientScript.RegisterStartupScript(this.GetType(), "Alert", strScript);
                }
            }
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Checking the Valid Date.
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gValidDateChecking

        #region gValidDateChecking [1]
        /// <summary>
        ///     This Function Checking the Date and Input Format is [DD/MM/YYYY]
        /// </summary>
        /// <param name="checkDate">Input Format is [DD/MM/YYYY]</param>
        /// <returns></returns>
        public string gValidDateChecking(string checkDate)
        {
            string strMessage = "";

            try
            {
                //----------------------------------------------------
                if (checkDate.Trim().Length < 10)
                {
                    strMessage = "Enter Proper Date in [DD/MM/YYYY] Format";
                    return strMessage;
                }
                //----------------------------------------------------

                string strDay = "";
                string strMonth = "";
                string strYear = "";
                //----------------------------------------------------
                strDay = checkDate.Substring(0, 2);
                strMonth = checkDate.Substring(3, 2);
                strYear = checkDate.Substring(6, 4);
                //----------------------------------------------------
                if (strDay.Trim().Length != 2)
                {
                    strMessage = "Enter Proper Day in [DD] Format";
                    return strMessage;
                }
                if (strMonth.Trim().Length != 2)
                {
                    strMessage = "Enter Proper Month in [MM] Format";
                    return strMessage;
                }
                if (strYear.Trim().Length != 4)
                {
                    strMessage = "Enter Proper Year in [YYYY] Format";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether The Date Is Blank Or Not
                //-------------------------------------------------------------------
                if (strDay == "")
                {
                    strMessage = "Date Cannot Be Blank";
                    return strMessage;
                }
                else
                {
                    if (strMonth == "")
                    {
                        strMessage = "Month Cannot Be Blank";
                        return strMessage;
                    }
                    else
                    {
                        if (strYear == "")
                        {
                            strMessage = "Year Cannot Be Blank";
                            return strMessage;
                        }
                    }
                }

                //-------------------------------------------------------------------
                //Check For Numeric Value
                //-------------------------------------------------------------------
                if (gIsNumeric(strDay) == true)
                {
                    if (gIsNumeric(strMonth) == true)
                    {
                        if (gIsNumeric(strYear) != true)
                        {
                            strMessage = "Enter Numeric Value";
                            return strMessage;
                        }
                    }
                    else
                    {
                        strMessage = "Enter Numeric Value";
                        return strMessage;
                    }
                }
                else
                {
                    strMessage = "Enter Numeric Value";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether The Entered Date Is Zero Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strDay) < 1)
                {
                    strMessage = "Date Cannot Be Zero";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether The Entered Month Is Zero Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strMonth) < 1)
                {
                    strMessage = "Month Cannot Zero.";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check whether the Entered Month Is Greater Then 12 Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strMonth) > 12)
                {
                    strMessage = "Month Cannot Be Greater Than 12";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Day Check
                //-------------------------------------------------------------------			
                int entr_year = Convert.ToInt16(strYear);
                int entr_month = Convert.ToInt16(strMonth);
                int num_days;
                num_days = DateTime.DaysInMonth(entr_year, entr_month);

                if (Convert.ToInt16(strDay) > num_days)
                {
                    strMessage = "Enter A Valid Date";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether Year Entered Is Not Out Of Range
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strYear) < 1753)
                {
                    strMessage = "Enter A Proper Year";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                strMessage = "ValidDate";

            }
            catch
            {
                strMessage = "";
            }

            return strMessage;
        }

        #endregion

        #region gValidDateChecking [2]
        /// <summary>
        ///     This function Checking the Valid Date
        /// </summary>
        /// <param name="strDay">Day in DD Format</param>
        /// <param name="strMonth">Month in MM Format</param>
        /// <param name="strYear">Year in YYYY Format</param>
        /// <returns></returns>
        public string gValidDateChecking(string strDay, string strMonth, string strYear)
        {
            string strMessage = "";

            try
            {

                //-------------------------------------------------------------------
                //Check For Numeric Value
                //-------------------------------------------------------------------
                if (gIsNumeric(strDay) == true)
                {
                    if (gIsNumeric(strMonth) == true)
                    {
                        if (gIsNumeric(strYear) != true)
                        {
                            strMessage = "Enter Numeric Value";
                            return strMessage;
                        }
                    }
                    else
                    {
                        strMessage = "Enter Numeric Value";
                        return strMessage;
                    }
                }
                else
                {
                    strMessage = "Enter Numeric Value";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether The Entered Date Is Zero Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strDay) < 1)
                {
                    strMessage = "Date Cannot Be Zero";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether The Entered Month Is Zero Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strMonth) < 1)
                {
                    strMessage = "Month Cannot Zero.";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check whether the Entered Month Is Greater Then 12 Or Not
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strMonth) > 12)
                {
                    strMessage = "Month Cannot Be Greater Than 12";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Day Check
                //-------------------------------------------------------------------			
                int entr_year = Convert.ToInt16(strYear);
                int entr_month = Convert.ToInt16(strMonth);
                int num_days;
                num_days = DateTime.DaysInMonth(entr_year, entr_month);

                if (Convert.ToInt16(strDay) > num_days)
                {
                    strMessage = "Enter A Valid Date";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                //Check Whether Year Entered Is Not Out Of Range
                //-------------------------------------------------------------------
                if (Convert.ToInt16(strYear) < 1753)
                {
                    strMessage = "Enter A Proper Year";
                    return strMessage;
                }

                //-------------------------------------------------------------------
                strMessage = "ValidDate";
            }
            catch
            {
                strMessage = "";
            }

            return strMessage;
        }
        #endregion

        #region getDateInDDMMYYYY
        public object getDateInDDMMYYYY(string strVal)
        {
            object objDate = "";

            // System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;

            string strDateFormat = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;

            if (!string.IsNullOrEmpty(strVal))
            {
                if (strDateFormat == "dd/MM/yyyy")
                {
                    objDate = strVal.Substring(6, 4) + "/" + strVal.Substring(3, 2) + "/" + strVal.Substring(0, 2);
                }
                else
                {
                    string strMonth = "";
                    string strDay = "";
                    string strYear = "";
                    int intIndex = 0;

                    intIndex = strVal.IndexOf("/");

                    if (intIndex == -1)
                        intIndex = strVal.IndexOf("-");

                    strMonth = strVal.Substring(0, intIndex);

                    strVal = strVal.Substring(intIndex + 1);

                    intIndex = strVal.IndexOf("/");

                    if (intIndex == -1)
                        intIndex = strVal.IndexOf("-");


                    strDay = strVal.Substring(0, intIndex);

                    intIndex = strVal.IndexOf("/");
                    //-----------------------------------------------
                    if (intIndex == -1)
                        intIndex = strVal.IndexOf("-");
                    //-----------------------------------------------              
                    strVal = strVal.Substring(intIndex);
                    //-----------------------------------------------
                    intIndex = strVal.IndexOf(" ");
                    //-----------------------------------------------
                    if (intIndex == -1)
                        strYear = strVal.Substring(1);
                    else
                        strYear = strVal.Substring(1, strVal.IndexOf(" "));
                    //-----------------------------------------------
                    if (strDay.Length == 1)
                        strDay = "0" + strDay;
                    //-----------------------------------------------
                    if (strMonth.Length == 1)
                        strMonth = "0" + strMonth;
                    //-----------------------------------------------
                    if (strYear.Length == 3)
                        strYear = strYear + "0";
                    //-----------------------------------------------
                    objDate = strYear + "/" + strMonth + "/" + strDay;
                    //-----------------------------------------------

                }
            }

            return objDate;

        }

        #endregion

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Get the Web Server's Current Date. 
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gGetWebServers_CurrentDate
        /// <summary>
        ///     This Function Returns the Web Portal Servers Current Date in MM/DD/YYYY Format  
        /// </summary>
        /// <returns></returns>
        public string gGetWebServers_CurrentDate()
        {
            string strCurrentDate;

            try
            {
                DateTime dtDate = DateTime.Today;
                strCurrentDate = dtDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                strCurrentDate = "";
            }

            return strCurrentDate;

        }
        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - To Get the Main Server's Current Date.
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gGetDatabaseServers_CurrentDate
        /// <summary>
        ///     This Function Returns the Current Date of the Database Server
        /// </summary>
        /// <returns></returns>
        public string gGetDatabaseServers_CurrentDate()
        {
            string strServerDate = "";

            try
            {               

                objHelper.gOpenConnection();

                string strQuery = "SELECT CONVERT(CHAR(10),GETDATE(),103)";                             

                strServerDate = (string)objHelper.gExecuteScalar(CommandType.Text, strQuery);
            }
            catch
            {
                strServerDate = "";
            }
            finally
            {
                objHelper.gCloseConnection();
            }

            return strServerDate;
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Getting the Value from the Right of the Complete String
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: RIGHT

        public string gRight(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Getting the Value from the Middle of the Complete String
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gMID

        #region gMID [1]

        public string gMid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        #endregion

        #region gMID [2]

        public string gMid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }
        #endregion

        #endregion

        /* -------------------------------------------------------------------------------------------
		 * Function - For Reversing the String 
		 * -------------------------------------------------------------------------------------------*/
        #region FUNCTION: gReverse

        public string gReverse(string str)
        {
            int len = str.Length;
            char[] arr = new char[len];

            for (int i = 0; i < len; i++)
            {
                arr[i] = str[len - 1 - i];
            }

            return new string(arr);
        }
        #endregion


        #region FUNCTION: POPUpWindows

        #region Open Window with adjustable Width/Height
        /// <summary>Open Browser Window, set Width/Height</summary>
        /// <param name="URL">string</param>
        /// <param name="Name">string</param>
        /// <param name="Width">string</param>
        /// <param name="Height">string</param>
        /// <param name="MenuBar">bool</param>
        /// <param name="Toolbar">bool</param>
        /// <param name="Location">bool</param>
        /// <param name="StatusBar">bool</param>
        /// <param name="Copyhistory">bool</param>
        /// <param name="ScrollBar">bool</param>
        /// <returns>string</returns>
        public string gOpenPopUPWindow
           (string URL,
            string Name,
            string Width,
            string Height,
            bool MenuBar,
            bool Toolbar,
            bool Location,
            bool StatusBar,
            bool Copyhistory,
            bool ScrollBar)
        {
            string ret = "javascript:void window.open ('";
            ret = ret + URL + "', '" + Name + "', '";

            ret = ret + "resizable=1,";
            ret = ret + "width=" + Width + ",";
            ret = ret + "height=" + Height + ",";

            ret = ret + "menuBar=" + ((MenuBar) ? "1" : "0") + ",";
            ret = ret + "toolbar=" + ((Toolbar) ? "1" : "0") + ",";
            ret = ret + "location=" + ((Location) ? "1" : "0") + ",";
            ret = ret + "statusBar=" + ((StatusBar) ? "1" : "0") + ",";
            ret = ret + "copyhistory=" + ((Copyhistory) ? "1" : "0") + ",";
            ret = ret + "scrollBar=" + ((ScrollBar) ? "1" : "0") + ",";
            ret = ret + "')";
            return ret;
        }
        #endregion

        #region Open Full Screen Window
        /// <summary>Open Browser Window: Full Screen</summary>
        /// <param name="URL">string</param>
        /// <param name="Name">string</param>
        /// <param name="FullScreen">bool</param>
        /// <param name="MenuBar">bool</param>
        /// <param name="Resizable">bool</param>
        /// <param name="Toolbar">bool</param>
        /// <param name="Location">bool</param>
        /// <param name="StatusBar">bool</param>
        /// <param name="Copyhistory">bool</param>
        /// <param name="ScrollBalr">bool</param>
        /// <returns>string</returns>
       public string gOpenPopUPWindow
           (string URL,
            string Name,
            bool FullScreen,
            bool MenuBar,
            bool Resizable,
            bool Toolbar,
            bool Location,
            bool StatusBar,
            bool Copyhistory,
            bool ScrollBar)
        {
            string ret = "javascript:void window.open ('";
            ret = ret + URL + "', '" + Name + "', '";
            ret = ret + "fullScreen=" + ((FullScreen) ? "1" : "0") + ",";
            ret = ret + "resizable=" + ((Resizable) ? "1" : "0") + ",";
            ret = ret + "menuBar=" + ((MenuBar) ? "1" : "0") + ",";
            ret = ret + "toolbar=" + ((Toolbar) ? "1" : "0") + ",";
            ret = ret + "location=" + ((Location) ? "1" : "0") + ",";
            ret = ret + "statusBar=" + ((StatusBar) ? "1" : "0") + ",";
            ret = ret + "copyhistory=" + ((Copyhistory) ? "1" : "0") + ",";
            ret = ret + "scrollBar=" + ((ScrollBar) ? "1" : "0") + ",";
            ret = ret + "')";
            return ret;
        }
        #endregion

        #endregion


        // To Display the Header In the Page Header for Every Page
        #region FUNCTION: ShowHeader

        public string gShowHeader(string strMemberName)
        {
            string strHtmlString;

            strHtmlString = "<table border= 0  cellpadding= 0  cellspacing= 0  style= border-collapse: collapse  bordercolor= #111111  width= 780px  id= AutoNumber1 height= 112>";
            strHtmlString += "<tr>";
            strHtmlString += "<td valign= top  align= right > ";
            strHtmlString += "<b>";
            strHtmlString += "<a href= '' style= text-decoration: none >";
            strHtmlString += "<font color= white  name= Trebuchet MS  size= 2  face= Trebuchet MS ></font></a></b>";
            strHtmlString += "&nbsp;&nbsp;";
            strHtmlString += "<b> ";
            strHtmlString += "<a href= '' style= text-decoration: none >";
            strHtmlString += "<font color= white  name= Trebuchet MS  size= 2  face= Trebuchet MS ></font></a></b></b></td>";
            strHtmlString += "</tr>";
            strHtmlString += "<tr>";
            strHtmlString += "<td>&nbsp;</td>";
            strHtmlString += "</tr>";
            strHtmlString += "<tr>";
            strHtmlString += "<td>&nbsp;</td>";
            strHtmlString += "</tr>";
            strHtmlString += "<tr>";
            strHtmlString += "<td><b><font face= Trebuchet MS  size= 2  color= black >";
            strHtmlString += "</font></b>&nbsp;</td>";
            strHtmlString += "</tr>";
            strHtmlString += "<tr valign= bottom >";
            strHtmlString += "<td align= right valign= bottom><font color= white ><b><font face= Trebuchet MS  size= 2>Welcome:" + strMemberName + "&nbsp;&nbsp;</font></b></font></td>";
            strHtmlString += "</tr>";
            strHtmlString += "</table>";

            return strHtmlString;

        }

        #endregion

        // Validate From date with To date
        #region gIsToDateGreater
        /// <summary>
        /// checking From Date is greater than To Date
        /// </summary>
        /// <param name="strFromDate">Accept From Date string as dd/MM/yyyy format </param>
        /// <param name="strToDate">Accept To Date string as dd/MM/yyyy format </param>
        /// <returns></returns>
        public bool gIsFromDateGreater(string strFromDate, string strToDate)
        {
            bool bnlReturnVal = false;
            string strFdate = strFromDate.Substring(6, 4) + strFromDate.Substring(3, 2) + strFromDate.Substring(0, 2);
            string strTdate = strToDate.Substring(6, 4) + strToDate.Substring(3, 2) + strToDate.Substring(0, 2);

            int intFromDate = Convert.ToInt32(strFdate);
            int intToDate = Convert.ToInt32(strTdate);

            if (intFromDate > intToDate)
                bnlReturnVal = true;

            return bnlReturnVal;
        }

        #endregion
        
        //
        #region RemoveDirectory

        public void RemoveDirectory(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                if (dirInfo.Exists == true)
                    RemoveDirFiles(dirInfo);
            }
            catch (System.IO.IOException exIO)
            {
                throw new Exception(exIO.Message);

            }
            catch (System.Security.SecurityException ser_Ex)
            {
                throw new Exception(ser_Ex.Message);

            }
            catch (System.Security.Authentication.AuthenticationException Auth_Excep)
            {
                throw new Exception(Auth_Excep.Message);

            }
            catch (Exception err_Handler)
            {
                throw new Exception(err_Handler.Message);

            }

        }

        public void RemoveDirFiles(DirectoryInfo dirInfo)
        {
            foreach (FileInfo file in dirInfo.GetFiles("*.*"))
                file.Delete();
        }

        #endregion

        #region gResizeImage
        public void gResizeImage(string fileName, string outputFileName, int width, int height,string strImagePath)
        {

            //Open the original image

            System.Drawing.Image original = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(strImagePath + "/" + fileName));

            //Create a bitmap of the correct size.

            Bitmap temp = new Bitmap(width, height, original.PixelFormat);

            //Get a Graphics object from the bitmap.

            Graphics newImage = Graphics.FromImage(temp);

            //Set the quality of the output image.

            newImage.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            newImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;

            //Draw the image with the new width/height

            newImage.DrawImage(original, 0, 0, width, height);

            //Save the bitmap

            string strSavePath = HttpContext.Current.Server.MapPath(strImagePath + "/" + outputFileName);

            temp.Save(strSavePath);

            //Dispose of our objects.

            original.Dispose();

            temp.Dispose();

            newImage.Dispose();

        }

        #endregion


        #region gResizeImageWithAspect
        
        public void gResizeImageWithAspect(string strSourceFileName,string strDetinationFileName,string strFolderPath,int newWidth)
        {

            System.Drawing.Image original = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(strFolderPath + "/" + strSourceFileName));

            //Find the aspect ratio between the height and width.

            float aspect = (float)original.Height / (float)original.Width;

            //Calculate the new height using the aspect ratio

            // and the desired new width.

            int newHeight = (int)(newWidth * aspect);

            //Create a bitmap of the correct size.

            Bitmap temp = new Bitmap(newWidth, newHeight, original.PixelFormat);

            //Get a Graphics object from the bitmap.

            Graphics newImage = Graphics.FromImage(temp);

            newImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //Draw the image with the new width/height

            newImage.DrawImage(original, 0, 0, newWidth, newHeight);
            
            //Save the bitmap

            string strSavePath = HttpContext.Current.Server.MapPath(strFolderPath + "/" + strDetinationFileName);

            temp.Save(strSavePath);

            //Dispose of our objects.

            original.Dispose();

            temp.Dispose();

            newImage.Dispose();

        }

        #endregion



        #region gResizeImageWithAspectSize

        public void gResizeImageWithAspectSize(string strSourceFileName, string strDetinationFileName, string strFolderPath, int newWidth)
        {

            System.Drawing.Image original = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(strFolderPath + "/" + strSourceFileName));

            //Find the aspect ratio between the height and width.

            float aspect = (float)original.Height / (float)original.Width;

            //Calculate the new height using the aspect ratio

            // and the desired new width.

            int newHeight = (int)(newWidth * aspect);

            //Create a bitmap of the correct size.
            //================================================

            int sourceWidth = original.Width;
            int sourceHeight = original.Height;

            if (sourceWidth > newWidth)
            {

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)newWidth / (float)sourceWidth);
                nPercentH = ((float)newHeight / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                //==========================================================

                Bitmap temp = new Bitmap(destWidth, destHeight);

                //Get a Graphics object from the bitmap.

                Graphics newImage = Graphics.FromImage(temp);

                newImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //Draw the image with the new width/height

                newImage.DrawImage(original, 0, 0, newWidth, newHeight);

                //Save the bitmap

                string strSavePath = HttpContext.Current.Server.MapPath(strFolderPath + "/" + strDetinationFileName);



                //  NOW IT GOIING TO BE SAVED AT DESIRE LOCATION

                // Encoder parameter for image quality
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);

                // Jpeg image codec
                ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

                if (jpegCodec == null)
                    return;


                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                
                temp.Save(strSavePath, jpegCodec, encoderParams);


                //temp.Save(strSavePath);

                //Dispose of our objects.

                original.Dispose();

                temp.Dispose();

                newImage.Dispose();
            }
            else
            {
                Bitmap temp = new Bitmap(sourceWidth, sourceHeight);

                //Get a Graphics object from the bitmap.

                Graphics newImage = Graphics.FromImage(temp);

                newImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //Draw the image with the new width/height

                newImage.DrawImage(original, 0, 0, sourceWidth, sourceHeight);

                //Save the bitmap

                string strSavePath = HttpContext.Current.Server.MapPath(strFolderPath + "/" + strDetinationFileName);

                //  NOW IT GOIING TO BE SAVED AT DESIRE LOCATION

                // Encoder parameter for image quality
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 85L);

                // Jpeg image codec
                ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

                if (jpegCodec == null)
                    return;


                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;


                temp.Save(strSavePath, jpegCodec, encoderParams);

                //Dispose of our objects.

                original.Dispose();

                temp.Dispose();

                newImage.Dispose();

            }

        }

        #endregion

        #region getEncoderInfo
        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        #endregion



    }
    #endregion

 }
