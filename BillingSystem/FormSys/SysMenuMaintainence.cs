
#region Developer Information

/*_________________________________________________________________________________________________________

Developed By   : Ripan Paul
Module Name    : SysMenuMaintainence
Start Date     : 31/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : Menu Maintainence

//_________________________________________________________________________________________________________*/

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

//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class SysMenuMaintainence : Form
    {

        #region System Generated Code
        public SysMenuMaintainence()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        
        string strPath;
        string strSQL;
        string strLocalMachineName;
        
        #endregion

        #region User defined events

        #region SysMenuMaintainence_Load
        private void SysMenuMaintainence_Load(object sender, EventArgs e)
        {
            // Getting the Local Machine Name
            strLocalMachineName = Environment.MachineName;

            txtInformation1.Text = "Make sure that in all Client Machine's " + J_Var.J_pApplicationName + " Application should be closed";
            txtInfo.Visible = false;
        }
        #endregion

        #region BtnMenuMaintainence_Click
        private void BtnMenuMaintainence_Click(object sender, EventArgs e)
        {
            try
		    {
                if (cmnService.J_UserMessage("Do you want to Maintainence your Database??",
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    BtnCancel.Select();
                    return;
                }
                
                cmnService.J_BusyMode();
                dmlService.J_BeginTransaction();
                



                
                
                
                
                dmlService.J_Commit();
                
                dmlService.J_ClearDatabaseLog();
                cmnService.J_NormalMode();
                
                txtInfo.Visible = true;
                txtInfo.Text = "Database Maintainence is completed.";
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
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion



        #endregion

        #region User defined methods

        #region InsertMenuData
        private bool InsertMenuData(long   MenuId, 
                                    string GroupCode, 
                                    string MenuGroup, 
                                    string SLNo,  
                                    string MenuName, 
                                    string MenuDesc, 
                                    string MenuSubDesc_1, 
                                    bool   MenuVisibility)
        {
            if (dmlService.J_IsRecordExist("MST_MENU", "MENU_ID = " + MenuId + "") == true)
                return true;
            
            int intMenuVisibility = 0;
            if (MenuVisibility == false) intMenuVisibility = 1;

            strSQL = "INSERT INTO MST_MENU (" +
                     "            MENU_ID," +
                     "            MENU_GROUP_CODE," +
                     "            MENU_GROUP_NAME," +
                     "            MENU_SLNO," +
                     "            MENU_NAME," +
                     "            MENU_DESC," +
                     "            MENU_SUB_DESC_1," +
                     "            MENU_VISIBILITY) " +
                     "VALUES    ( " + MenuId + "," +
                     "           '" + GroupCode + "'," +
                     "           '" + MenuGroup + "'," +
                     "           '" + SLNo + "'," +
                     "           '" + MenuName + "'," +
                     "           '" + MenuDesc + "'," +
                     "           '" + MenuSubDesc_1 + "'," +
                     "            " + intMenuVisibility + ")";
            if (dmlService.J_ExecSql(strSQL) == false) return false;
            return true;
        }
        #endregion

        


        #endregion


    }
}