
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
    public partial class SysBackup : Form
    {
        #region System Generated Code
        public SysBackup()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~ create object of clsRoot Class ~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        DMLService dml = new DMLService();
        CommonService cmn = new CommonService();
        //----------------------------------------------------------------------------
        string strPath;
        string strLocalMachineName;
        //----------------------------------------------------------------------------
        #endregion

        #region User defined events

        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            //-- Getting the Local Machine Name
            strLocalMachineName = Environment.MachineName;
            txtPath.Visible = false;
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            try
		    {
                txtPath.Visible = false;
                
                if (strLocalMachineName.Trim().ToUpper() != J_Var.J_pServerName.Trim().ToUpper())
                {
                    cmn.J_UserMessage("Please take the BACKUP from server itself !!");
                    BtnCancel.Select();
                    return;                        
                }
                
                string strFolderPath = cmn.J_OpenFolderDialog();
                if (strFolderPath == "") return;

                cmn.J_BusyMode();
                dml.J_ClearDatabaseLog(J_Var.J_pDatabaseName);
                
                string strSQL = "EXEC SP_MSFOREACHTABLE " +
                                "@COMMAND1 = \"DROP TABLE ?\"," +
                                "@WHEREAND = \"AND ID IN (SELECT ID FROM SYSOBJECTS WHERE XTYPE = 'U' AND (NAME LIKE 'TMP%' OR NAME LIKE 'TEMP%'))\"";
                dml.J_ExecSql(strSQL);

                strPath = strFolderPath + "/" + J_Var.J_pBranchCode + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".BAK";
                strSQL = "BACKUP DATABASE [" + J_Var.J_pDatabaseName + "] " +
                         "       TO DISK = '" + strPath + "'";
                dml.J_ExecSql(strSQL);
                
                txtPath.Visible = true;
                txtPath.Text = "Collect the backup from : " + strPath + "";
                
                cmn.J_NormalMode();
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

        #endregion
    }
}