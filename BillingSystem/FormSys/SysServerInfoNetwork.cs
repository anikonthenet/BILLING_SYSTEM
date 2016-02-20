
#region Namespace
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BillingSystem.FormCmn;
using BillingSystem.FormMst;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class SysServerInfoNetwork : Form
    {

        #region System Generate Code
        public SysServerInfoNetwork()
        {
            InitializeComponent();
        }
        #endregion

        #region Decleration Section

        //~~~~ create object of clsRoot Class ~~~~
        //clsRoot root = new clsRoot();

        DMLService dml = new DMLService();
        CommonService cmn = new CommonService();
        
        #endregion

        #region System Events

        #region SysServerInfoNetwork_Load
        private void SysServerInfoNetwork_Load(object sender, EventArgs e)
        {
            dml.J_GetSQLServers(ref cmbServers, ref cmbDatabase);
            ClearControls();
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
                
                nameValue.Add("SERVERNAME",  cmbServers.Text.Trim());
                nameValue.Add("DATABASENAME", cmbDatabase.Text.Trim());
                nameValue.Add("USERNAME", txtDatabaseUserName.Text.Trim());
                nameValue.Add("PASSWORD", txtDatabasePassword.Text.Trim());
                
                XMLService xml = new XMLService();
                xml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                
                // Close & Dispose the MstLogin Class
                this.Close();
                this.Dispose();

                if (dml.J_IsRecordExist("MST_FAYEAR") == true)
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
            }
            catch (Exception err_handler)
            {
                cmn.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region cmbServers_Enter
        private void cmbServers_Enter(object sender, EventArgs e)
        {
            cmbDatabase.Items.Clear();
        }
        #endregion

        #region cmbDatabase_Enter
        private void cmbDatabase_Enter(object sender, EventArgs e)
        {
            dml.J_GetSQLDatabases(ref cmbServers, ref txtDatabaseUserName, ref txtDatabasePassword, ref cmbDatabase);
        }
        #endregion

        #region PointerMove_KeyPress
        private void PointerMove_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (Convert.ToInt64(e.KeyChar) == 27) BtnCancel_Click(sender, e);
            if (Convert.ToInt32(e.KeyChar) == 12)
            {
                //----------------------------------------------------------
                this.Close();
                this.Dispose();
                //----------------------------------------------------------
                SysServerInfoLocal frm = new SysServerInfoLocal();
                frm.ShowDialog();
                frm.Dispose();
                //----------------------------------------------------------
            }
        }
        #endregion

        #endregion

        #region User Define Methods

        #region void ClearControls
        private void ClearControls()
        {
            cmbServers.SelectedIndex = 0;
            txtDatabaseUserName.Text = "";
            txtDatabasePassword.Text = "";
            cmbDatabase.Items.Clear();
        }
        #endregion

        #region bool ValidateFields
        private bool ValidateFields()
        {
            if (cmbServers.SelectedIndex <=0)
            {
                cmn.J_UserMessage("Please Select the SQL Server Machine Name !!");
                cmbServers.Focus();
                return false;
            }
            if (txtDatabaseUserName.Text.Trim() == "")
            {
                cmn.J_UserMessage("Please enter the Database User Name !!");
                txtDatabaseUserName.Focus();
                return false;
            }
            if (txtDatabasePassword.Text.Trim() == "")
            {
                cmn.J_UserMessage("Please enter the Database Password !!");
                txtDatabasePassword.Focus();
                return false;
            }
            if (cmbDatabase.SelectedIndex <= 0)
            {
                cmn.J_UserMessage("Please Select the SQL Database Name !!");
                cmbDatabase.Focus();
                return false;
            }

            if (dml.J_ValidateConnection(cmbServers.Text.Trim(), txtDatabaseUserName.Text.Trim(), txtDatabasePassword.Text.Trim(), cmbDatabase.Text.Trim()) == true)
                return true;
            else
            {
                cmn.J_UserMessage("Verify the Config Information and Try again.... !!");
                cmbServers.Focus();
                return false;
            }
        }
        #endregion

        

        #endregion

        
   




    }
}