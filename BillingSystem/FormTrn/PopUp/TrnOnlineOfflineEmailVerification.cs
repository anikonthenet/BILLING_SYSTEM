
#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: TrnOnlineOfflineEmailVerification
Version			: 2.0
Start Date		: 
End Date		: 
Tables Used     : 
Module Desc		: 
____________________________________________________________________________________________________________________

*/

#endregion


#region Refered Namespaces & Classes

// System Namespaces
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

using JAYA.VB;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;


// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.FormCmn;
using BillingSystem.FormRpt;
using BillingSystem.Classes;
using BillingSystem.FormTrn.PopUp;
using BillingSystem.Reports.Listing;
using BillingSystem.Reports.Transaction.INVOICE;


#endregion


namespace BillingSystem.FormTrn.PopUp
{
    public partial class TrnOnlineOfflineEmailVerification : Form
    {

        #region OBJECT DECLARATION

        DMLService dmlService;
        CommonService cmnService;
        DateService dtService;
        JVBCommon mainVB;
        BS BillingSystem = new BS();  

        #endregion

        #region CONSTRUCTOR
        public TrnOnlineOfflineEmailVerification(long PartyId,string[] ArrayDetails)
        {
            InitializeComponent();

            arrPopupDetails = ArrayDetails;
            lngPartyId = PartyId;

            dmlService = new DMLService();
            cmnService = new CommonService();
            dtService  = new DateService();
            mainVB     = new JVBCommon();
            
        }
        #endregion

        #region PRIVATE VARIABLE DECLARATION

        private string strSQL = "";
        private float inttextSize = 0;
        private bool blnEmail = false;
        
        string[] arrPopupDetails = new string[14];
        long lngPartyId = 0;

        #endregion

        #region User Defined Events

        #region TrnOnlineOfflineEmailVerification_Load
        private void TrnOnlineOfflineEmailVerification_Load(object sender, EventArgs e)
        {
            // all controls are cleared
            ClearControls();
            //--------------------------------------
            mskInvoiceDate.Text = arrPopupDetails[0];
            txtInvAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(arrPopupDetails[1]));
            txtPartyName.Text = arrPopupDetails[2];
            txtAddress1.Text = arrPopupDetails[3];
            txtAddress2.Text = arrPopupDetails[4];
            txtAddress3.Text = arrPopupDetails[5];
            txtCity.Text = arrPopupDetails[6];
            txtPin.Text = arrPopupDetails[7];
            txtContactPersonName.Text = arrPopupDetails[8];
            txtMobileNo.Text = arrPopupDetails[9];
            txtEmailID.Text = arrPopupDetails[10];
            cmbPartyCategory.Text = arrPopupDetails[11];
            cmbPartyCategory.Enabled = false;
            //--
            if (arrPopupDetails[14] == J_Mode.Edit)
                grpSMSEmail.Visible = true;
            else
                grpSMSEmail.Visible = false;
            //--------------------------------------   
            BS.BS_SaveInvoiceStatus = false;
            //
            txtContactPersonName.Select();
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // All validation
                if (ValidateFields() == false) return;

                // Transaction is started
                dmlService.J_BeginTransaction();
                //---------------------------------------------
                strSQL = @" UPDATE MST_PARTY
                            SET    MOBILE_NO = '" + cmnService.J_ReplaceQuote(txtMobileNo.Text.Trim()) + @"',
                                   EMAIL_ID  = '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + @"',
                                   CONTACT_PERSON  = '" + cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim()) + @"'
                            WHERE  PARTY_ID  = " + lngPartyId;

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    //Rollback Transaction
                    dmlService.J_Rollback();
                    return;
                }
                //---------------------------------------------
                //Commit Transaction 
                //---------------------------------------------
                dmlService.J_Commit();
                //---------------------------------------------
                BS.BS_PartyContactPerson = cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim());
                BS.BS_PartyMobNo = cmnService.J_ReplaceQuote(txtMobileNo.Text.Trim());
                BS.BS_PartyEmailId = cmnService.J_ReplaceQuote(txtEmailID.Text.Trim());
                //
                if (chkSMS.Checked == true)
                    BS.BS_PartySendSMS = true;
                else
                    BS.BS_PartySendSMS = false;
                //
                if (chkEmail.Checked == true)
                    BS.BS_PartySendEmail = true;
                else
                    BS.BS_PartySendEmail = false;
                //---------------------------------------------
                BS.BS_SaveInvoiceStatus = true;

                dmlService.Dispose();
                this.Close();
                this.Dispose();
            }
            catch (Exception err)
            {
                //Rollback Transaction
                dmlService.J_Rollback();

                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            BS.BS_SaveInvoiceStatus = false;
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion
        

        #region txtEmailID_KeyDown
        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
        #endregion

        #region txtEmailID_KeyPress
        private void txtEmailID_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }
        #endregion

        #region btnPreviewBill_Click
        private void btnPreviewBill_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        #region USER DEFINE METHODS

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                //CONTACT PERSON
                if (txtContactPersonName.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please Enter a Contact Person Name.");
                    txtContactPersonName.Select();
                    return false;
                }

                //MOBILE NO
                if (txtMobileNo.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please Enter a MobileNo.");
                    txtMobileNo.Select();
                    return false;
                }

                if (txtMobileNo.Text.Trim() != "")
                {
                    //
                    if (txtMobileNo.Text.Trim().Length < 10 || txtMobileNo.Text.Trim().Length > 10)
                    {
                        cmnService.J_UserMessage("Please Enter a valid MobileNo.");
                        txtMobileNo.Select();
                        return false;
                    }
                    //--
                    if (txtMobileNo.Text.Trim().Substring(0, 1) != "9")
                    {
                        if (txtMobileNo.Text.Trim().Substring(0, 1) != "8")
                        {
                            if (txtMobileNo.Text.Trim().Substring(0, 1) != "7")
                            {
                                cmnService.J_UserMessage("Please Enter a valid MobileNo.");
                                txtMobileNo.Select();
                                return false;
                            }
                        }
                    }
                }
                //--
                string strOut = "";
                if (arrPopupDetails[13] == BS_BillMode.OnlineDelivery)
                {
                    if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.VerifySMS, arrPopupDetails[12], "", txtMobileNo.Text, out strOut) == false)
                    {
                        cmnService.J_UserMessage("SMS does not match with the one given while Billing Online [" + strOut + "]", MessageBoxIcon.Exclamation);
                        txtMobileNo.Select();
                        return false;
                    }
                }
                //--    

                //Email ID
                if (txtEmailID.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please Enter a Email.");
                    txtEmailID.Select();
                    return false;
                }

                //---------------------------------------------
                //VALIDATION FOR EMAIL ID TO 
                //---------------------------------------------
                if (txtEmailID.Text.Trim() != "")
                {
                    //if (txtEmailID.Text.Trim().Contains(";") == true)
                    //{
                    //    cmnService.J_UserMessage("Please seperate multiple Email ID with ','");
                    //    txtEmailID.Select();
                    //    return false;
                    //}
                    //----------------------------------------------------------
                    if (txtEmailID.Text.Trim().Contains(",") == true)
                    {
                        string[] strEmail_ID = txtEmailID.Text.Trim().Split(',');

                        for (int i = 0; i < strEmail_ID.Length; i++)
                        {
                            if (IsValidEmailAddress(strEmail_ID[i]) == false)
                            {
                                cmnService.J_UserMessage("Please enter a valid Email ID : " + strEmail_ID[i]);
                                txtEmailID.Select();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (IsValidEmailAddress(txtEmailID.Text.Trim()) == false)
                        {
                            cmnService.J_UserMessage("Please enter a valid Email ID.");
                            txtEmailID.Select();
                            return false;
                        }
                    }
                }
                //--
                if (arrPopupDetails[13] == BS_BillMode.OnlineDelivery)
                {
                    if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.VerifyEmail, arrPopupDetails[12], txtEmailID.Text, "", out strOut) == false)
                    {
                        cmnService.J_UserMessage("Email does not match with the one given while Billing Online [" + strOut + "]", MessageBoxIcon.Exclamation);
                        txtEmailID.Select();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnExit_Click(sender, e);
        }
        #endregion       

        #region ClearControls
        private void ClearControls()
        {
            txtInvAmount.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtPin.Text = "";
            txtContactPersonName.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";

            strSQL = @" SELECT PARTY_CATEGORY_ID,
                               PARTY_CATEGORY_DESCRIPTION
                        FROM   MST_PARTY_CATEGORY 
                        WHERE  INACTIVE_FLAG = 0 
                        ORDER BY PARTY_CATEGORY_ID ";

            dmlService.J_PopulateComboBox(strSQL, ref cmbPartyCategory, J_ComboBoxDefaultText.NO);
        }
        #endregion

        #region IsValidEmailAddress
        public bool IsValidEmailAddress(string EmailIdToCheck)
        {
            try
            {
                MailAddress m = new MailAddress(EmailIdToCheck);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion

        

        
        #endregion

    }
}