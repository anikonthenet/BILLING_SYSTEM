
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: CmnLogin
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: User to Login
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

// System Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// User Namespaces
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// This namespace are using for global functionality
using BillingSystem.Classes;
using BillingSystem.FormSys;

#endregion

namespace BillingSystem.FormCmn
{
    public partial class CmnLogin : Form
    {

        #region Object Decleration Section

        // Object Decleration
        private DMLService dmlService;
        private CommonService cmnService;
        private IDataReader reader;

        #endregion

        #region Variable Decleration Section

        // Used to store SQL
        string strSQL;

        #endregion

        #region CONSTRUCTOR
        public CmnLogin()
        {
            InitializeComponent();

            // Object Initialization
            this.dmlService = new DMLService();
            this.cmnService = new CommonService();
            this.reader = null;

            // Variable Initialization
            strSQL = "";

        }
        #endregion

        #region DESTRUCTOR
        ~CmnLogin()
        {
            this.dmlService.Dispose();
            this.Dispose(true);
        }
        #endregion

        #region User Define Methods

        #region void ClearControls
        private void ClearControls()
        {
            strSQL = "SELECT BRANCH_ID," +
                     "       BRANCH_NAME " +
                     "FROM   MST_SETUP " +
                     "ORDER BY BRANCH_NAME ";
            dmlService.J_PopulateComboBox(strSQL, ref cmbBranch, J_ComboBoxDefaultText.NO);
            
            // 1. Table Name
            // 2. Column Name
            // 3. Column Data Type
            // 4. No. of space
            string[,] strArray = {{"MST_FAYEAR", "FA_BEG_DATE", "D", "3"},
                                  {"MST_FAYEAR", "FA_END_DATE", "D", "3"}};

            strSQL = "SELECT FAYEAR_ID," +
                     "       " + dmlService.J_ConcateColumns(strArray) + " AS FA_DATE " +
                     "FROM   MST_FAYEAR " +
                     "WHERE INACTIVE_FLAG = 0 " +
                     "ORDER BY FA_BEG_DATE DESC";
            dmlService.J_PopulateComboBox(strSQL, ref cmbFAYear, J_ComboBoxDefaultText.NO);
            
            txtPassword.Text = "";
        }
        #endregion

        #region bool ValidateFields
        private bool ValidateFields()
        {
            // Check the selected branch is blank or not
            if (cmbBranch.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please select the Branch !!");
                cmbBranch.Focus();
                return false;
            }

            // Check the selected Login Id is blank or not
            if (cmbLoginId.Text.Trim() == "")
            {
                cmnService.J_UserMessage("Please select the Login Id !!");
                cmbLoginId.Focus();
                return false;
            }

            // Check the Password is blank or not
            if (txtPassword.Text == "")
            {
                cmnService.J_UserMessage("Please enter the password !!");
                txtPassword.Focus();
                return false;
            }

            // Check the Password is invalid or not
            if (cmbLoginId.Text.ToUpper() == "ADMIN")
            {
                if (dmlService.J_IsRecordExist("MST_USER",
                        "    " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " = '" + cmnService.J_ReplaceQuote(cmbLoginId.Text.ToUpper()) + "' " +
                        "AND USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "'") == false)
                {
                    cmnService.J_UserMessage(J_Msg.InvalidPassword);
                    txtPassword.Focus();
                    return false;
                }
            }
            else if (cmbLoginId.Text.ToUpper() != "ADMIN")
            {
                if (dmlService.J_IsRecordExist("MST_USER",
                    "    BRANCH_ID     =  " + J_Var.J_pBranchId + " " +
                    "AND " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " = '" + cmnService.J_ReplaceQuote(cmbLoginId.Text.ToUpper()) + "' " +
                    "AND USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "'") == false)
                {
                    cmnService.J_UserMessage(J_Msg.InvalidPassword);
                    txtPassword.Focus();
                    return false;
                }
            }

            // Check the selected F.A. Year is blank or not
            if (cmbFAYear.SelectedIndex < 0)
            {
                cmnService.J_UserMessage("Please select the FA Year");
                cmbFAYear.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region System Events

        #region CmnLogin_Load
        private void CmnLogin_Load(object sender, EventArgs e)
        {
            // all controls are cleared
            ClearControls();
            this.Text = ":: " + J_Var.J_pProjectName + " - Login ::";
            cmbLoginId.Select();
            //-- COMMENT
            //cmbLoginId.Text = "admin";
            //txtPassword.Text = "admin";
            //BtnOK.Select();
        }
        #endregion

        #region BtnOK_Click
        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Check the Validation
                if (ValidateFields() == false)
                    return;
                
                // Make the query string for Company Information
                strSQL = "SELECT " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "                  AS BRANCH_ID," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_CODE", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                 AS BRANCH_CODE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                 AS BRANCH_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.COMPANY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                AS COMPANY_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.ADDRESS", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                     AS ADDRESS," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.START_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + "           AS START_DATE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SAVE_CONFIRMATION_MSG", J_ColumnType.Integer, J_SQLColFormat.NullCheck)+ "       AS SAVE_CONFIRMATION_MSG," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_USERNAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS NETWORK_CREDENTIAL_USERNAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_PASSWORD", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS NETWORK_CREDENTIAL_PASSWORD," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_PORT", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS NETWORK_CREDENTIAL_PORT," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.NETWORK_CREDENTIAL_HOST", J_ColumnType.String, J_SQLColFormat.NullCheck) + "     AS NETWORK_CREDENTIAL_HOST, " +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SMS_WORKING_KEY", J_ColumnType.String, J_SQLColFormat.NullCheck)         + "     AS SMS_WORKING_KEY, " +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SMS_OFFLINE_INVOICE_MESSAGE", J_ColumnType.String, J_SQLColFormat.NullCheck) + " AS SMS_OFFLINE_INVOICE_MESSAGE, " +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SMS_ONLINE_INVOICE_MESSAGE", J_ColumnType.String, J_SQLColFormat.NullCheck)  + " AS SMS_ONLINE_INVOICE_MESSAGE, " +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SMS_SENDER_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "             AS SMS_SENDER_NAME " +
                         "FROM   MST_SETUP " +
                         "WHERE  MST_SETUP.BRANCH_ID = " + J_Var.J_pBranchId + "";
                
                // return the data reader as per above query string
                this.reader = dmlService.J_ExecSqlReturnReader(strSQL);

                // check the given reader is null
                if (reader == null)
                {
                    cmbLoginId.Select();
                    return;
                }

                // fetch the data from reader
                while (reader.Read())
                {
                    J_Var.J_pBranchId                             = Convert.ToInt32(Convert.ToString(reader["BRANCH_ID"]));
                    J_Var.J_pBranchCode                           = Convert.ToString(reader["BRANCH_CODE"]);
                    J_Var.J_pBranchName                           = Convert.ToString(reader["BRANCH_NAME"]);
                    J_Var.J_pCompanyName                          = Convert.ToString(reader["COMPANY_NAME"]);
                    J_Var.J_pBranchAddress                        = Convert.ToString(reader["ADDRESS"]);
                    J_Var.J_pSoftwareStartDate                    = Convert.ToString(reader["START_DATE"]);
                    J_Var.J_pSaveConfirmMsg                       = Convert.ToInt32(Convert.ToString(reader["SAVE_CONFIRMATION_MSG"]));

                    //ADDED BY DHRUB ON 31/01/2015 FOR SEND EMAIL AGAINST IVOICE
                    J_Var.J_pNetworkCredential_Username           = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_USERNAME"]));
                    J_Var.J_pNetworkCredential_Password           = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_PASSWORD"]));
                    J_Var.J_pNetworkCredential_Port               = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_PORT"]));
                    J_Var.J_pNetworkCredential_Host               = Convert.ToString(Convert.ToString(reader["NETWORK_CREDENTIAL_HOST"]));

                    //ADDED BY DHRUB ON 31/01/2015 FOR SEND EMAIL AGAINST IVOICE
                    J_Var.J_pSMS_WorkingKey = Convert.ToString(Convert.ToString(reader["SMS_WORKING_KEY"]));
                    J_Var.J_pOfflineInvoiceSMS_Text = Convert.ToString(Convert.ToString(reader["SMS_OFFLINE_INVOICE_MESSAGE"]));
                    J_Var.J_pOnlineInvoiceSMS_Text = Convert.ToString(Convert.ToString(reader["SMS_ONLINE_INVOICE_MESSAGE"]));
                    J_Var.J_pSMS_SenderName = Convert.ToString(Convert.ToString(reader["SMS_SENDER_NAME"]));
                }

                // reader is closed & disposed
                this.reader.Close();
                this.reader.Dispose();

                // Make the query string
                strSQL = "SELECT USER_ID," +
                         "       DISPLAY_NAME," +
                         "       USER_CATEGORY " +
                         "FROM   MST_USER " +
                         "WHERE  " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " = '" + cmnService.J_ReplaceQuote(cmbLoginId.Text.ToUpper().Trim()) + "' " +
                         "AND    USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";

                if (cmbLoginId.Text.ToUpper() != "ADMIN")
                    strSQL = strSQL + "AND BRANCH_ID     =  " + J_Var.J_pBranchId + " ";

                // return the dataset as per above query string
                DataSet ds = dmlService.J_ExecSqlReturnDataSet(strSQL);
                
                // check the given dataset is null
                if (ds == null)
                {
                    cmbLoginId.Select();
                    return;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    J_Var.J_pUserId          = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0][0]));
                    J_Var.J_pLoginId         = cmnService.J_ReplaceQuote(cmbLoginId.Text.ToUpper().Trim());
                    J_Var.J_pUserDisplayName = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    J_Var.J_pUserCategory    = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0][2]));
                    
                    // Close & Dispose the MstLogin Class
                    dmlService.Dispose();
                    this.Close();
                    this.Dispose();
                    
                    if (J_Var.J_pLoginStatus == 1)
                    {
                        J_Var.frmMain.Close();
                        J_Var.frmMain.Dispose();
                    }

                    Hashtable nameValue = new Hashtable();
                    nameValue.Add("BRANCHINFO", J_Var.J_pBranchCode);
                    
                    XMLService objxml = new XMLService();
                    objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlBranchInfoFileName, "BRANCHINFO");
                    
                    // Create object of MDI Class 
                    J_Var.frmMain = new mdiBillingSystem();
                    J_Var.frmMain.ShowDialog();
                }
                else
                    cmbLoginId.Select();
            }
            catch (Exception err_handler)
            {
                this.reader.Close();
                this.reader.Dispose(); 

                cmnService.J_UserMessage(err_handler.Message);
                cmbLoginId.Select();
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion


        #region cmbLoginId_KeyPress
        private void cmbLoginId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion

        #region txtPassword_KeyPress
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion


        #region cmbBranch_SelectedIndexChanged
        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex < 0)
                cmnService.J_ClearComboBox(ref cmbLoginId);
            else
            {
                J_Var.J_pBranchId = Support.GetItemData(cmbBranch, cmbBranch.SelectedIndex);
                strSQL = "SELECT ALL_USERS.USER_ID  AS USER_ID," +
                         "       ALL_USERS.LOGIN_ID AS LOGIN_ID " +
                         "FROM  (SELECT USER_ID," +
                         "              LOGIN_ID " +
                         "       FROM   MST_USER " +
                         "       WHERE  USER_CATEGORY = 0 " +
                         "       AND    BRANCH_ID     = 0 " +
                         "       UNION ALL " +
                         "       SELECT USER_ID," +
                         "              LOGIN_ID " +
                         "       FROM   MST_USER " +
                         "       WHERE  USER_CATEGORY = 0 " +
                         "       AND    BRANCH_ID     = " + J_Var.J_pBranchId + ") AS ALL_USERS " +
                         "ORDER BY ALL_USERS.LOGIN_ID";
                dmlService.J_PopulateComboBox(strSQL, ref cmbLoginId);
            }
        }
        #endregion

        #region cmbBranch_KeyPress
        private void cmbBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion


        #region cmbFAYear_SelectedIndexChanged
        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            J_Var.J_pFAYearId = Support.GetItemData(cmbFAYear, cmbFAYear.SelectedIndex);
            J_Var.J_pFABegDate = cmnService.J_Mid(cmbFAYear.Text, 0, 10);
            J_Var.J_pFAEndDate = cmnService.J_Mid(cmbFAYear.Text, 13, 10);
            J_Var.J_pCollectionPostFlag = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COLLECTION_POST_FLAG FROM MST_FAYEAR WHERE FAYEAR_ID = " + J_Var.J_pFAYearId)));
            if (J_Var.J_pCollectionPostFlag > 0)
                J_Var.J_pReconFlag = 0;
            else
                J_Var.J_pReconFlag = 1;
        }
        #endregion

        #region cmbFAYear_KeyPress
        private void cmbFAYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion


        #endregion


    }
}