
#region Developer Information
//_________________________________________________________________________________________________________
//Developed By   : Anik Ghosh
//Module Name    : SysBackup
//Start Date     : 26/12/2008
//End Date       : 
//Main Table     : 
//Other Tables   : 
//Module Desc    : Taking Backup
//_________________________________________________________________________________________________________
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
using System.Data.SqlClient;
//---------------------------
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using BillingSystem;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class SysBuildIndex : Form
    {
        #region System Generated Code
        public SysBuildIndex()
        {
            InitializeComponent();

            dmlService = new DMLService();
            mainVB = new JAYA.VB.JVBCommon();
        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = null;
        JAYA.VB.JVBCommon mainVB = null;
        
        string strPath;
        string strSQL;
        string strLocalMachineName;
        
        #endregion

        #region User defined events

        #region SysBuildIndex_Load
        private void SysBuildIndex_Load(object sender, EventArgs e)
        {
            // Getting the Local Machine Name
            strLocalMachineName = Environment.MachineName;

            txtInformation1.Text = "Make sure that in all Client Machine's " + J_Var.J_pApplicationName + " Application should be closed";
            txtInfo.Visible = false;
        }
        #endregion

        #region BtnBuildIndex_Click
        private void BtnBuildIndex_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,
                                "Do you want to build index in your Database??",
                                J_Var.J_pProjectName,
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            txtInfo.Visible = true;
            txtInfo.Text = "Build Index process is completed in your database.";
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #endregion
    }
}