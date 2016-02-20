
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BillingSystem.FormCmn;
using BillingSystem.FormMst;
using BillingSystem.Classes;

namespace BillingSystem.FormSys
{
    public partial class SysServerInfoLocal : Form
    {
        #region System Generate Code
        public SysServerInfoLocal()
        {
            InitializeComponent();

            this.dmlService = new DMLService();
            this.cmnService = new CommonService();
        }
        #endregion

        #region Decleration Section

        DMLService dmlService    = null;
        CommonService cmnService = null;
        
        string strSQL = string.Empty;

        string strServerName = string.Empty;
        string strUserName = string.Empty;
        string strPassword = string.Empty;
        string strDatabaseName = string.Empty;

        #endregion

        #region System Events

        #region SysServerInfoLocal_Load
        private void SysServerInfoLocal_Load(object sender, EventArgs e)
        {
            ClearControls();

            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_Network)
                    lblDatabaseName.Text = "Database Path";
            }

            txtServerName.Select();
        }
        #endregion

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);

            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                if (Convert.ToInt32(e.KeyChar) == 14)
                {
                    this.Close();
                    this.Dispose();

                    SysServerInfoNetwork frm = new SysServerInfoNetwork();
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
        }
        #endregion

        #region BtnSubmit_Click
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (ValidateFields() == false)
                    return;
                
                Hashtable nameValue = new Hashtable();
                
                nameValue.Add("SERVERNAME", this.strServerName);
                nameValue.Add("DATABASENAME", this.strDatabaseName);
                nameValue.Add("USERNAME", this.strUserName);
                nameValue.Add("PASSWORD", this.strPassword);
                
                XMLService objxml = new XMLService();
                objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);

                if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR") == true)
                {
                    // Close & Dispose the MstLogin Class
                    this.Close();
                    this.Dispose();

                    if (dmlService.J_IsRecordExist("MST_FAYEAR") == true)
                    {
                        CmnLogin frm = new CmnLogin();
                        frm.ShowDialog();
                        frm.Dispose();
                    }
                    else
                    {
                        CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                        frm.ShowDialog();
                        frm.Dispose();
                    }
                    return;
                }
                else
                {
                    cmnService.J_UserMessage("Invalid database structure.\nPlease check the database");
                    txtServerName.Focus();
                    return;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #endregion

        #region User Define Methods

        #region ClearControls
        private void ClearControls()
        {
            txtServerName.Text       = "";
            txtSQLDatabaseName.Text  = "";
            txtDatabaseUserName.Text = "";
            txtDatabasePassword.Text = "";
        }
        #endregion

        #region bool ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (txtServerName.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter the SQL Server Machine Name !!");
                    txtServerName.Select();
                    return false;
                }
                if (J_Var.J_pDatabaseType != J_DatabaseType.MsAccess)
                {
                    if (txtDatabaseUserName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Database User Name !!");
                        txtDatabaseUserName.Select();
                        return false;
                    }
                    if (txtDatabasePassword.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Database Password !!");
                        txtDatabasePassword.Select();
                        return false;
                    }
                }
                if (txtSQLDatabaseName.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Please enter the SQL Database Name !!");
                    txtSQLDatabaseName.Select();
                    return false;
                }
                
                this.strServerName   = txtServerName.Text.Trim();
                this.strUserName = txtDatabaseUserName.Text.Trim();
                this.strPassword = txtDatabasePassword.Text.Trim();
                this.strDatabaseName = txtSQLDatabaseName.Text.Trim();

                if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                {
                    if (txtDatabaseUserName.Text.Trim() == "")
                        this.strUserName = "Admin";
                    else if (txtDatabaseUserName.Text.Trim() != "")
                        this.strUserName = txtDatabaseUserName.Text.Trim();
                
                    //this.strDatabaseName = cmnService.J_ConvertMsAccessDatabasePath(txtSQLDatabaseName.Text.Trim(), J_Colon.NO);
                }

                if (dmlService.J_ValidateConnection(this.strServerName, this.strUserName, this.strPassword, this.strDatabaseName) == false)
                {
                    cmnService.J_UserMessage("Connection Failed....");
                    txtServerName.Focus();
                    return false;
                }

                return true;
            }
            catch 
            {
                cmnService.J_UserMessage("Verify the Config Information and Try again.... !!");
                txtServerName.Focus();
                return false;
            }
        }
        #endregion

        
        

        #endregion

    }
}