
#region Developers Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: CmnChangePassword
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Change the User Password
_________________________________________________________________________________________________________

*/

#endregion

#region Reffered Namespaces

//~~~~ System Namespaces ~~~~
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using BillingSystem.FormMst;
using BillingSystem.FormRpt;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormCmn
{
    public partial class CmnChangePassword : Form
    {
        #region Constructor
        public CmnChangePassword()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables Required

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();

        string strSQL;						//For Storing the Local SQL Query
        
        #endregion

        #region Event Handlers

        #region CmnChangePassword_Load
        private void CmnChangePassword_Load(object sender, EventArgs e)
        {
            string strLoginId = string.Empty;
            strSQL = "SELECT LOGIN_ID " +
                     "FROM   MST_USER " +
                     "WHERE  USER_ID   = " + J_Var.J_pUserId + " ";

            if (J_Var.J_pLoginId.Trim().ToUpper() != "ADMIN")
                strSQL = strSQL + "AND BRANCH_ID = " + J_Var.J_pBranchId + " ";
            
            strLoginId = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (strLoginId == null) return;
            txtLoginId.Text = strLoginId;
            
            lblTitle.Text = "Change User Password";
            txtOldPassword.Select();
        }
        #endregion

        #region BtnOK_Click
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false) return;

            if (MessageBox.Show(this,
                    "Are you sure to change your Password ?",
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dmlService.J_BeginTransaction();
                strSQL = "UPDATE MST_USER " +
                         "SET    USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtNewPassword.Text) + "'" +
                         "WHERE  BRANCH_ID     =  " + J_Var.J_pBranchId + " " +
                         "AND    USER_ID       =  " + J_Var.J_pUserId + "";
                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    dmlService.J_Rollback();
                    return;
                }
                dmlService.J_Commit();
                cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, "Password Updated to :: ******* ");

                dmlService.Dispose();
                Application.Restart();
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
        
        #region txt_KeyPress
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        
        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            string strOldPassword = string.Empty;
            
            // To check for entry of the existing Password
            if (txtOldPassword.Text == "")
            {
                cmnService.J_UserMessage("Please Enter Old Password");
                txtOldPassword.Select();
                return false;
            }
            
            // To check for  entry of the new Password
            if (txtNewPassword.Text == "")
            {
                cmnService.J_UserMessage("Please Enter New Password");
                txtNewPassword.Select();
                return false;
            }

            // To check for  entry of the new Confirm Password
            if (txtConfirmPassword.Text == "")
            {
                cmnService.J_UserMessage("Please Enter Confirm Password");
                txtConfirmPassword.Select();
                return false;
            }
            
            // To check for correct entry of the existing Password
            strSQL = "SELECT USER_PASSWORD " +
                     "FROM   MST_USER " +
                     "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                     "AND    USER_ID   = " + J_Var.J_pUserId + "";
            strOldPassword = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
            if (strOldPassword == null) return false;
            if (txtOldPassword.Text != strOldPassword)
            {
                cmnService.J_UserMessage("Incorrect Old Password Entered");
                txtOldPassword.Select();
                return false;
            }
            
            // To check for matching of new password and the new Confirm Password
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                cmnService.J_UserMessage("New and Confirm Password does not match!!");
                txtNewPassword.Select();
                return false;
            }

            return true;
        }
        #endregion
        

        #endregion

    }
}