
#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: TrnPartySave
Version			: 2.0
Start Date		: 
End Date		: 
Tables Used     : 
Module Desc		: 
____________________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using JAYA.VB;

using BillingSystem.Classes;
using Microsoft.VisualBasic.Compatibility.VB6;


#endregion

namespace BillingSystem.FormTrn.PopUp
{
    public partial class TrnPartySave : Form
    {

        #region OBJECT DECLARATION

        DMLService dmlService;
        CommonService cmnService;
        DateService dtService;
        JVBCommon mainVB;
        

        #endregion

        #region CONSTRACTOR
        public TrnPartySave()
        {
            InitializeComponent();

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
        ToolTip tltp = new ToolTip();

        BS BillingSystem = new BS(); 
        #endregion

        

        #region EVENTS

        #region TrnPartySave_Load
        private void TrnPartySave_Load(object sender, EventArgs e)
        {
            // all controls are cleared
            ClearControls();
            //
            txtPartyName.Select();
            //
            //if (BS.P_OrderNo != "")
                txtOnlineOrderNo.Text = BS.P_OrderNo;
            //else
            //    txtOnlineOrderNo.Text = "";

            //if (BS.P_Billing_FName != "")
            //{
            //    btnFetchWebData.Visible = true;
            //    txtWebAddress.Visible = true;
            //}
            //else
            //{
            //    btnFetchWebData.Visible = false;
            //    txtWebAddress.Visible = false;
            //}
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Added by Ripan Paul on 23-07-2011
            if(dmlService.J_IsRecordExist("MST_PARTY",
                                          "    BRANCH_ID  =  " + J_Var.J_pBranchId + " " +
                                          "AND PARTY_NAME = '" + cmnService.J_ReplaceQuote(txtPartyName.Text.Trim().ToUpper()) + "'") == true)
            {
                cmnService.J_UserMessage("Duplicate Party found ...");
                txtPartyName.Select();
                return;
            }

            // Save Confirmation Message
            if (cmnService.J_SaveConfirmationMessage(ref txtAddress1) == true) return;

            // Transaction is started
            dmlService.J_BeginTransaction();

            // insert query & execution
            strSQL = "INSERT INTO MST_PARTY (" +
                     "            BRANCH_ID," +
                     "            PARTY_CATEGORY_ID," +
                     "            PARTY_NAME," +
                     "            ADDRESS1," +
                     "            ADDRESS2," +
                     "            ADDRESS3," +
                     "            CITY," +
                     "            PIN," +
                     "            CONTACT_PERSON," +
                     "            MOBILE_NO," +
                     "            PHONE_NO," +
                     "            FAX," +
                     "            EMAIL_ID," +
                     "            USER_ID," +
                     "            CREATE_DATE) " +
                     "    VALUES( " + J_Var.J_pBranchId + "," +
                     "            " + Support.GetItemData(cmbPartyCategory, cmbPartyCategory.SelectedIndex) + ", " +
                     "           '" + cmnService.J_ReplaceQuote(txtPartyName.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtCity.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtPin.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtMobileNo.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtFaxNo.Text.Trim()) + "'," +
                     "           '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + "'," +
                     "            " + J_Var.J_pUserId + "," +
                     "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ")";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                txtPartyName.Select();
                return;
            }
            
            // Transaction is commited
            dmlService.J_Commit();

            // Added by Ripan Paul on 23-07-2011
            BS.BS_PartyName = txtPartyName.Text.Trim();
            
            // after data is inserted, the message is displayed
            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);

            // exit from this form
            BtnExit_Click(sender, e);

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


        #region txtEmailID_TextChanged
        private void txtEmailID_TextChanged(object sender, EventArgs e)
        {
            //IDataReader drdShowEmailHelp = null;
            //try
            //{
            //    //if (blnEmail == false)
            //    //    return;
            //    //--
            //    if (txtEmailID.Text.Length > 1)
            //    {
            //        if (txtEmailID.Text.Contains("@") == true)
            //        {
            //            strSQL = "SELECT EMAIL_ID," +
            //                     "       EMAIL_DESC " +
            //                     "FROM   MST_EMAIL " +
            //                     "WHERE  EMAIL_DESC LIKE '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Substring(txtEmailID.Text.IndexOf('@') + 1)) + "%' " +
            //                     "ORDER BY EMAIL_DESC";
            //            drdShowEmailHelp = dmlService.J_ExecSqlReturnReader(strSQL);
            //            //--
            //            if (drdShowEmailHelp == null)
            //            {
            //                lstEmailHelp.Visible = false;
            //                return;
            //            }
            //            else
            //            {
            //                lstEmailHelp.Items.Clear();
            //                lstEmailHelp.Height = 19;
            //                lstEmailHelp.Visible = true;
            //                //--
            //                if (inttextSize == 0)
            //                {
            //                    Graphics graphics = this.CreateGraphics();
            //                    System.Drawing.FontFamily FontFamily = new FontFamily(txtEmailID.Font.Name.ToString());
            //                    System.Drawing.Font FontName = new System.Drawing.Font(FontFamily, cmnService.J_ReturnInt64Value(txtEmailID.Font.Size));
            //                    SizeF textSize = graphics.MeasureString(txtEmailID.Text, FontName);
            //                    inttextSize = textSize.Width;
            //                    //
            //                    lstEmailHelp.Location = new Point(txtEmailID.Left + Convert.ToInt32(inttextSize), txtEmailID.Top + 20);
            //                    //                        
            //                }
            //                //
            //                while (drdShowEmailHelp.Read())
            //                {
            //                    lstEmailHelp.Items.Add(new ListBoxItem(drdShowEmailHelp["EMAIL_DESC"].ToString()));
            //                    //--
            //                    if (lstEmailHelp.Height <= 300)
            //                        lstEmailHelp.Height = lstEmailHelp.Height + 19;
            //                    //--
            //                    //lstEmailHelp.Width = txtEmailID.Width - Convert.ToInt32(inttextSize);
            //                    lstEmailHelp.Width = 200;
            //                    //--             
            //                }
            //                //--
            //                if (lstEmailHelp.Items.Count <= 0)
            //                    lstEmailHelp.Visible = false;
            //            }
            //            //
            //            drdShowEmailHelp.Close();
            //            drdShowEmailHelp.Dispose();
            //            //
            //        }
            //        else
            //        {
            //            lstEmailHelp.Visible = false;
            //            inttextSize = 0;
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    cmnService.J_UserMessage(err.Message);
            //}
        }
        #endregion

        #region txtEmailID_KeyDown
        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstEmailHelp.Visible == true)
                {
                    lstEmailHelp.Focus();
                    lstEmailHelp.SelectedIndex = 0;
                }
            }
            //
            if (e.KeyCode == Keys.Escape) lstEmailHelp.Visible = false;
        }
        #endregion

        #region txtEmailID_KeyPress
        private void txtEmailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{tab}");
                lstEmailHelp.Visible = false;
            }
        }
        #endregion

        #region lstEmailHelp_KeyPress
        private void lstEmailHelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                txtEmailID.Text = txtEmailID.Text.Substring(0, txtEmailID.Text.IndexOf('@') + 1) + lstEmailHelp.Text;
                lstEmailHelp.Visible = false;
                txtEmailID.Focus();
            }
        }
        #endregion

        #region btnFetchWebData_MouseMove
        private void btnFetchWebData_MouseMove(object sender, MouseEventArgs e)
        {
            tltp.Show("Get data from the web", btnFetchWebData);
        }
        #endregion

        #region btnFetchWebData_Click
        private void btnFetchWebData_Click(object sender, EventArgs e)
        {
            try
            {
                string strDetails = "";
                //
                if (BillingSystem.VerifyWebDatabase(BS_VerifyWebDB.ReturnDetails, txtOnlineOrderNo.Text.Trim(), "", "", out strDetails) == false)
                {
                    return;
                }
                //
                string[] strARRDetails = strDetails.Split('^');
                //
                #region FETCH DATA
                txtContactPersonName.Text = strARRDetails[0].Trim() + " " + strARRDetails[1].Trim() + " " + strARRDetails[2].Trim();
                //BS.P_Billing_Salutation = strARRDetails[0].Trim();
                //BS.P_Billing_FName = strARRDetails[1].Trim();
                //BS.P_Billing_LName = strARRDetails[2].Trim();
                BS.P_Billing_Company = strARRDetails[3].Trim();
                txtWebAddress.Text = strARRDetails[4].Trim();
                txtCity.Text = strARRDetails[5].Trim();
                //BS.P_Billing_State = strARRDetails[6].Trim();
                txtPin.Text = strARRDetails[7].Trim();
                txtEmailID.Text = strARRDetails[8].Trim();
                txtMobileNo.Text = strARRDetails[9].Trim();
                txtPhone.Text = strARRDetails[10].Trim();
                //BS.P_CD_SerialID = strARRDetails[11].Trim();
                #endregion
                //
            }
            catch(Exception err)
            {
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

        #endregion

        #region USER DEFINE METHODS

        #region ClearControls
        private void ClearControls()
        {
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtPin.Text = "";
            txtContactPersonName.Text = "";
            txtMobileNo.Text = "";
            txtPhone.Text = "";
            txtFaxNo.Text = "";
            txtEmailID.Text = "";

            strSQL = "SELECT PARTY_CATEGORY_ID, " +
                     "       PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   MST_PARTY_CATEGORY " +
                     "WHERE  INACTIVE_FLAG = 0 "+
                     "ORDER BY PARTY_CATEGORY_ID ";

            dmlService.J_PopulateComboBox(strSQL, ref cmbPartyCategory, J_ComboBoxDefaultText.NO);
            
        }
        #endregion

        
        

        

        #endregion

        


    }
}