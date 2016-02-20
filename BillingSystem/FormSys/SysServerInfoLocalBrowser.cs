
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
    public partial class SysServerInfoLocalBrowser : Form
    {
        #region System Generate Code
        public SysServerInfoLocalBrowser()
        {
            InitializeComponent();

            this.dmlService = new DMLService();
            this.cmnService = new CommonService();
        }
        #endregion

        #region Decleration Section

        DMLService dmlService = null;
        CommonService cmnService = null;
        
        string strSQL = string.Empty;

        #endregion

        #region System Events

        #region SysServerInfoLocalBrowser_Load
        private void SysServerInfoLocalBrowser_Load(object sender, EventArgs e)
        {
            txtBrowser.Text = "";
            BtnBrowser.Select();
        }
        #endregion

        #region BtnBrowser_Click
        private void BtnBrowser_Click(object sender, EventArgs e)
        {
            txtBrowser.Text = cmnService.J_OpenFileDialog("Microsoft Access | *.mdb","Microsoft Access | *.mdb", "Choose the database to connect");
            if (txtBrowser.Text == "")
                BtnBrowser.Select();
            else
                BtnSubmit.Select();
        }
        #endregion

        #region BtnSubmit_Click
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            
            try
            {
                // all validations
                if (ValidateFields() == false)
                    return;

                string strDatabasePath = cmnService.J_GetDirectoryName(txtBrowser.Text);

                // create the connection xml file
                Hashtable nameValue = new Hashtable();
                nameValue.Add("SERVERNAME", "");
                nameValue.Add("DATABASENAME", strDatabasePath);
                nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);
                
                XMLService objxml = new XMLService();
                objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);

                if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR") == true)
                {
                    // Close & Dispose the MstLogin Class
                    this.Close();
                    this.Dispose();
                    
                    // Close & Dispose the main Class
                    if (J_Var.J_pLoginStatus == 1)
                    {
                        J_Var.frmMain.Close();
                        J_Var.frmMain.Dispose();
                    }

                    if (dmlService.J_IsRecordExist("MST_FAYEAR") == true)
                    {
                        dmlService.Dispose();
                        
                        if (J_Var.J_pLoginScreen == J_LoginScreen.YES)
                        {
                            CmnLogin frm = new CmnLogin();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        else if (J_Var.J_pLoginScreen == J_LoginScreen.NO)
                        {
                            J_Var.frmMain = new mdiBillingSystem();
                            J_Var.frmMain.ShowDialog();
                        }
                    }
                    else
                    {
                        dmlService.Dispose();

                        CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                        frm.ShowDialog();
                        frm.Dispose();
                    }
                    return;
                }
                else
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database structure.\nPlease check the database");
                    BtnBrowser.Focus();
                    return;
                }
            }
            catch (Exception err_handler)
            {
                dmlService.Dispose();
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
        

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
        }
        #endregion

        #endregion

        #region User Define Methods

        #region bool ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (txtBrowser.Text == "")
                {
                    cmnService.J_UserMessage("Please choose the database");
                    BtnBrowser.Select();
                    return false;
                }

                if (cmnService.J_IsFileExist(txtBrowser.Text) == false)
                {
                    cmnService.J_UserMessage("Database doesn't exist in selected path");
                    BtnBrowser.Select();
                    return false;
                }

                if (cmnService.J_GetFileName(txtBrowser.Text).ToUpper() != J_Var.J_pMsAccessDatabaseName.ToUpper())
                {
                    cmnService.J_UserMessage("Invalid Database.");
                    BtnBrowser.Select();
                    return false;
                }

                if (dmlService.J_ValidateConnection(txtBrowser.Text, J_Var.J_pMsAccessDatabasePassword) == false)
                {
                    cmnService.J_UserMessage("Connection Failed....");
                    BtnBrowser.Focus();
                    return false;
                }

                return true;
            }
            catch 
            {
                cmnService.J_UserMessage("Verify the Config Information and Try again.... !!");
                BtnBrowser.Focus();
                return false;
            }
        }
        #endregion


        #endregion

    }
}