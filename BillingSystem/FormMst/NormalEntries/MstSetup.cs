
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: Setup Information
Version			: 2.0
Start Date		: 
End Date		: 
Tables Used     : MST_SETUP ( PRIMARY TABLE )
Module Desc		: Setup Information Retrieval
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

//~~~~ System Namespaces ~~~~
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//~~~~ User Namespaces ~~~~
using BillingSystem.Classes;
using BillingSystem.FormMst;

#endregion

namespace BillingSystem.FormMst.NormalEntries
{
    public partial class MstSetup : BillingSystem.FormGen.GenForm
    {
        #region Constructor
        public MstSetup()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        DateService dtService = new DateService();
        
        string strSQL;						//For Storing the Local SQL Query
        
        #endregion

        #region User Defined Events

        #region MstSetup_Load
        private void MstSetup_Load(object sender, EventArgs e)
        {
            BtnAdd_Click(this, e);
            if (ShowRecord() == false)
            {
                ControlVisible(false);
                ClearControls();
            }
            lblTitle.Text = this.Text;
            txtAddress.Select(0,0);
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //-----------------------------------------------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                //-----------------------------------------------------------------------------------
                BtnCancel.BackColor = Color.LightGray;
                BtnCancel.Enabled = false;
                BtnExit.BackColor = Color.Lavender;
                BtnExit.Enabled = true;
                //-----------------------------------------------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                //-----------------------------------------------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data();
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

        #region txt_KeyPress
        public void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion


        #region txtSundryOpening_KeyPress
        private void txtSundryOpening_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");

            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,14,2", txtSundryOpening, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtSundryOpening_Leave
        private void txtSundryOpening_Leave(object sender, EventArgs e)
        {
            txtSundryOpening.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtSundryOpening)), "0.00");
        }
        #endregion


        #endregion

        #region User Defined Functions
        
        #region ShowRecord
        private bool ShowRecord()
        {
            IDataReader drdShowRecord = null;
            
            try
            {
                strSQL = "SELECT " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_ID", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "                      AS BRANCH_ID," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_CODE", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                     AS BRANCH_CODE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.BRANCH_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                     AS BRANCH_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.COMPANY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                    AS COMPANY_NAME," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.ADDRESS", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                         AS ADDRESS," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.CITY", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                            AS CITY," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.PIN", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                             AS PIN," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.CONTACT_NO", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                      AS CONTACT_NO," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.EMAIL_ID", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                        AS EMAIL_ID," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.WEB_SITE", J_ColumnType.String, J_SQLColFormat.NullCheck) + "                        AS WEB_SITE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.START_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + "               AS START_DATE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.OPENING_SUNDRY_OUTSTANDING", J_ColumnType.String, J_SQLColFormat.NullCheck) + "      AS OPENING_SUNDRY_OUTSTANDING," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SUNDRY_CUTOFF_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + "       AS SUNDRY_CUTOFF_DATE," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.SAVE_CONFIRMATION_MSG", J_ColumnType.Integer, J_SQLColFormat.NullCheck) + "          AS SAVE_CONFIRMATION_MSG," +
                         "       " + cmnService.J_SQLDBFormat("MST_SETUP.LAST_RECONCILIATION_DATE", J_ColumnType.Date, J_SQLColFormat.DateFormatDDMMYYYY) + " AS LAST_RECONCILIATION_DATE," +
                         "       NETWORK_CREDENTIAL_USERNAME   AS USERNAME," +
                         "       NETWORK_CREDENTIAL_PASSWORD   AS PASSWORD," +
                         "       NETWORK_CREDENTIAL_PORT       AS PORT," +
                         "       NETWORK_CREDENTIAL_HOST       AS HOST, " +
                         "       SMS_WORKING_KEY               AS SMS_WORKING_KEY, " +
                         "       SMS_OFFLINE_INVOICE_MESSAGE   AS SMS_OFFLINE_INVOICE_MESSAGE, " +
                         "       SMS_ONLINE_INVOICE_MESSAGE    AS SMS_ONLINE_INVOICE_MESSAGE, " +
                         "       SMS_SENDER_NAME               AS SMS_SENDER_NAME " +
                         "FROM   MST_SETUP " +
                         "WHERE  MST_SETUP.BRANCH_ID = " + J_Var.J_pBranchId + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                // variable declaration & initialization
                int intTentativeDisbursementDays = 0;
                double dblPreClosurePenaltyPer = 0;

                while (drdShowRecord.Read())
                {
                    txtBranchCode.Text                  = Convert.ToString(drdShowRecord["BRANCH_CODE"]);
                    txtBranchName.Text                  = Convert.ToString(drdShowRecord["BRANCH_NAME"]);
                    txtCompanyName.Text                 = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    txtAddress.Text                     = Convert.ToString(drdShowRecord["ADDRESS"]);
                    txtCity.Text                        = Convert.ToString(drdShowRecord["CITY"]);
                    txtPin.Text                         = Convert.ToString(drdShowRecord["PIN"]);
                    txtContactNo.Text                   = Convert.ToString(drdShowRecord["CONTACT_NO"]);
                    txtEmailId.Text                     = Convert.ToString(drdShowRecord["EMAIL_ID"]);
                    txtWebsite.Text                     = Convert.ToString(drdShowRecord["WEB_SITE"]);
                    mskStartDate.Text                   = Convert.ToString(drdShowRecord["START_DATE"]);
                    txtSundryOpening.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["OPENING_SUNDRY_OUTSTANDING"]));
                    mskSundryCutoffDate.Text = Convert.ToString(drdShowRecord["SUNDRY_CUTOFF_DATE"]);
                    //
                    txtNetUsername.Text = Convert.ToString(drdShowRecord["USERNAME"]);
                    txtNetPassword.Text = Convert.ToString(drdShowRecord["PASSWORD"]);
                    txtNetPort.Text = Convert.ToString(drdShowRecord["PORT"]);
                    txtNetHost.Text = Convert.ToString(drdShowRecord["HOST"]);
                    //
                    txtSmsWorkingKey.Text = Convert.ToString(drdShowRecord["SMS_WORKING_KEY"]);
                    txtOfflineSmsMessage.Text = Convert.ToString(drdShowRecord["SMS_OFFLINE_INVOICE_MESSAGE"]);
                    txtOnlineSmsMessage.Text = Convert.ToString(drdShowRecord["SMS_ONLINE_INVOICE_MESSAGE"]);
                    txtSmsSenderName.Text = Convert.ToString(drdShowRecord["SMS_SENDER_NAME"]);

                    if (Convert.ToInt32(Convert.ToString(drdShowRecord["SAVE_CONFIRMATION_MSG"])) == 0)
                        chkSaveConfirmationMessage.Checked = false;
                    else if (Convert.ToInt32(Convert.ToString(drdShowRecord["SAVE_CONFIRMATION_MSG"])) == 1)
                        chkSaveConfirmationMessage.Checked = true;

                    mskLastReconcileDate.Text = Convert.ToString(drdShowRecord["LAST_RECONCILIATION_DATE"]);


                    J_Var.J_pBranchId                   = Convert.ToInt32(Convert.ToString(drdShowRecord["BRANCH_ID"]));
                    J_Var.J_pBranchCode                 = Convert.ToString(drdShowRecord["BRANCH_CODE"]);
                    J_Var.J_pBranchName                 = Convert.ToString(drdShowRecord["BRANCH_NAME"]);
                    J_Var.J_pCompanyName                = Convert.ToString(drdShowRecord["COMPANY_NAME"]);
                    J_Var.J_pBranchAddress              = Convert.ToString(drdShowRecord["ADDRESS"]);
                    J_Var.J_pSoftwareStartDate          = Convert.ToString(drdShowRecord["START_DATE"]);
                    J_Var.J_pSaveConfirmMsg             = Convert.ToInt32(Convert.ToString(drdShowRecord["SAVE_CONFIRMATION_MSG"]));
                    //
                    J_Var.J_pNetworkCredential_Username = Convert.ToString(drdShowRecord["USERNAME"]);
                    J_Var.J_pNetworkCredential_Password = Convert.ToString(drdShowRecord["PASSWORD"]);
                    J_Var.J_pNetworkCredential_Port = Convert.ToString(drdShowRecord["PORT"]);
                    J_Var.J_pNetworkCredential_Host = Convert.ToString(drdShowRecord["HOST"]);
                    //--
                    J_Var.J_pSMS_WorkingKey = Convert.ToString(drdShowRecord["SMS_WORKING_KEY"]);
                    J_Var.J_pOfflineInvoiceSMS_Text = Convert.ToString(drdShowRecord["SMS_OFFLINE_INVOICE_MESSAGE"]);
                    J_Var.J_pOnlineInvoiceSMS_Text = Convert.ToString(drdShowRecord["SMS_ONLINE_INVOICE_MESSAGE"]);
                    J_Var.J_pSMS_SenderName = Convert.ToString(drdShowRecord["SMS_SENDER_NAME"]);
                    //--
                    J_Var.J_pLastReconcileDate = Convert.ToString(drdShowRecord["LAST_RECONCILIATION_DATE"]);
                    
                    // reader object is closed & also disposed
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    return true;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();

                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtBranchCode.Text  = "";
            txtBranchName.Text  = "";
            txtCompanyName.Text = "";
            txtAddress.Text     = "";
            txtCity.Text        = "";
            txtPin.Text         = "";
            txtContactNo.Text   = "";
            txtEmailId.Text     = "";
            txtWebsite.Text     = "";
            txtSundryOpening.Text = "0.00";
            mskSundryCutoffDate.Text = "";
            txtNetUsername.Text = "";
            txtNetPassword.Text = "";
            txtNetPort.Text = "";
            txtNetHost.Text = "";
            mskLastReconcileDate.Text = ""; 
            chkSaveConfirmationMessage.Checked = false;
            txtSmsWorkingKey.Text = "";
            txtOfflineSmsMessage.Text = "";
            txtSmsSenderName.Text = "";  
 
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                int intSaveConfirmationMessage = 0;
                string strLast_Reconciliation_Date = "";

                // Save Confirmation Message
                if (chkSaveConfirmationMessage.Checked == true)
                    intSaveConfirmationMessage = 1;
                else if (chkSaveConfirmationMessage.Checked == false)
                    intSaveConfirmationMessage = 0;


                if (dtService.J_IsBlankDateCheck(ref mskLastReconcileDate, J_ShowMessage.NO) == false)
                    strLast_Reconciliation_Date = cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskLastReconcileDate) + cmnService.J_DateOperator();
                else 
                    strLast_Reconciliation_Date = "null";
                    
                switch (lblMode.Text)
                {
                    case J_Mode.Add:

                        if (ValidateFields() == false) return;
                        // save confirmation message
                        if (cmnService.J_SaveConfirmationMessage(ref txtAddress) == true) return;

                        

                        // begin transaction
                        dmlService.J_BeginTransaction();
                        
                        strSQL = "UPDATE MST_SETUP " +
                            "     SET    ADDRESS                      = '" + cmnService.J_ReplaceQuote(txtAddress.Text.Trim()) + "'," +
                            "            CITY                         = '" + cmnService.J_ReplaceQuote(txtCity.Text.Trim()) + "'," +
                            "            PIN                          = '" + cmnService.J_ReplaceQuote(txtPin.Text.Trim()) + "'," +
                            "            CONTACT_NO                   = '" + cmnService.J_ReplaceQuote(txtContactNo.Text.Trim()) + "'," +
                            "            EMAIL_ID                     = '" + cmnService.J_ReplaceQuote(txtEmailId.Text.Trim()) + "'," +
                            "            WEB_SITE                     = '" + cmnService.J_ReplaceQuote(txtWebsite.Text.Trim()) + "'," +
                            "            OPENING_SUNDRY_OUTSTANDING   =  " + cmnService.J_ReplaceQuote(txtSundryOpening.Text.Trim()) + "," +
                            "            SUNDRY_CUTOFF_DATE           =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskSundryCutoffDate) + cmnService.J_DateOperator() + "," +
                            "            SAVE_CONFIRMATION_MSG        =  " + intSaveConfirmationMessage + "," +
                            "            NETWORK_CREDENTIAL_USERNAME  = '" + cmnService.J_ReplaceQuote(txtNetUsername.Text.Trim()) + "'," +
                            "            NETWORK_CREDENTIAL_PASSWORD  = '" + cmnService.J_ReplaceQuote(txtNetPassword.Text.Trim()) + "'," +
                            "            NETWORK_CREDENTIAL_PORT      = '" + cmnService.J_ReplaceQuote(txtNetPort.Text.Trim()) + "'," +
                            "            NETWORK_CREDENTIAL_HOST      = '" + cmnService.J_ReplaceQuote(txtNetHost.Text.Trim()) + "', " +
                            "            SMS_WORKING_KEY              = '" + cmnService.J_ReplaceQuote(txtSmsWorkingKey.Text.Trim()) + "', " +
                            "            SMS_OFFLINE_INVOICE_MESSAGE  = '" + cmnService.J_ReplaceQuote(txtOfflineSmsMessage.Text.Trim()) + "', " +
                            "            SMS_ONLINE_INVOICE_MESSAGE   = '" + cmnService.J_ReplaceQuote(txtOnlineSmsMessage.Text.Trim()) + "', " +
                            "            SMS_SENDER_NAME              = '" + cmnService.J_ReplaceQuote(txtSmsSenderName.Text.Trim()) + "', " +
                            "            LAST_RECONCILIATION_DATE     =  " + strLast_Reconciliation_Date + "" +
                            "     WHERE  BRANCH_ID                    =  " + J_Var.J_pBranchId + " ";
                        if (dmlService.J_ExecSql(strSQL) == false) return;
                        
                        J_Var.J_pBranchAddress  = txtAddress.Text.Trim();
                        J_Var.J_pSaveConfirmMsg = intSaveConfirmationMessage;

                        // Transaction is commited
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, "Setup Updated");
                        
                        this.dmlService.Dispose();
                        this.Close();
                        this.Dispose();

                        break;
                    case J_Mode.Edit:
                        break;
                    case J_Mode.Delete:
                        break;
                }
            }
            catch (Exception err_handler)
            {
                dmlService.J_Rollback();
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion
        
        #region ValidateFields
        private bool ValidateFields()
        {
            // Sundry Cut off Date
            if (dtService.J_IsDateValid(mskSundryCutoffDate) == false)
            {
                cmnService.J_UserMessage("Please enter the valid Sundry cutoff date.");
                mskSundryCutoffDate.Select();
                return false;
            }

            // Last Reconcile Date 
            if (dtService.J_IsBlankDateCheck(ref mskLastReconcileDate,J_ShowMessage.NO) == false)
            {
                if (dtService.J_IsDateValid(mskLastReconcileDate) == false)
                {
                    cmnService.J_UserMessage("Please enter the valid Last Reconciliation date.");
                    mskLastReconcileDate.Select();
                    return false;
                }
            }
            return true;
        }
        #endregion
       
        #endregion

    }
}

