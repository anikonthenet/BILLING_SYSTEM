
#region Developer Information

/*_______________________________________________________________________________________________________

Developed By   : Ripan Paul
Module Name    : SysUserRights
Start Date     : 31/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : User Rights
_________________________________________________________________________________________________________*/

#endregion

#region Refered Namespaces & Classes

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

    using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class SysUserRightsGrigView : Form
    {
        #region System Generated Code
        public SysUserRightsGrigView()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        // create object of clsRoot Class
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        ReportService rptService = new ReportService();
        
        string strSQL;
        string strQuery;			        //For Storing the general SQL Query
        string[,] strMatrix = null;
        
        #endregion

        #region ENUM decleration of Detail Grid Column
        //-- enum for setting detail grid column
        enum enmSysUserRights
        {
            MENU_ID = 1,
            GRANT_ID= 2,
            MENU_GROUP = 3,
            MENU_DESC = 4,
            MENU_SUB_DESC_1 = 5
        }
        #endregion

        #region User Define Events

        #region SysUserRightsGrigView_Activated
        private void SysUserRightsGrigView_Activated(object sender, EventArgs e)
        {
            //-------------------------------------------------------
            //-- set the Help Grid Column Header Text & behavior
            //-- (0) Header Text
            //-- (1) Width
            //-- (2) Format
            //-- (3) Alignment
            //-- (4) NullToText
            //-- (5) Visible
            //-- (6) AutoSizeMode
            //-------------------------------------------------------
            string[,] strMatrix1 = {{"Menu Id", "0", "", "", "", "F", ""},
							        {"First Level", "100", "S", "", "", "", ""},
							        {"Second Level", "300", "S", "", "", "", ""},
							        {"Third Level", "300", "S", "", "", "", "T"}};
            strMatrix = strMatrix1;
            //-----------------------------------------------------------
            strQuery = "SELECT MENU_ID         AS MENU_ID," +
                       "       MENU_GROUP_NAME AS MENU_GROUP_NAME," +
                       "       MENU_DESC       AS MENU_DESC," +
                       "       MENU_SUB_DESC_1 AS MENU_SUB_DESC_1 " +
                       "FROM   MST_MENU " +
                       "WHERE  MENU_VISIBILITY <> 1 " +
                       "ORDER BY MENU_GROUP_CODE," +
                       "       MENU_SLNO ";
            //-----------------------------------------------------------
            ClearControls();
            cmbUserId.Select();
            lblTitle.Text = "User Rights";
        }
        #endregion

        #region cmbUserId_KeyPress
        private void cmbUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbUserId_SelectedIndexChanged
        private void cmbUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDetailGrid();
            if (cmbUserId.SelectedIndex <= 0) return;
            
            foreach (DataGridViewRow row in grdvDescription.Rows)
            {
                if (dmlService.J_IsRecordExist("MST_USER_RIGHTS",
                    "    USER_ID = " + Convert.ToInt64(Support.GetItemData(cmbUserId, cmbUserId.SelectedIndex)) + " " +
                    "AND MENU_ID = " + Convert.ToInt64(Convert.ToString(row.Cells[1].Value)) + "") == true)
                {
                    row.Cells[0].Value = true;
                }
            }
        }
        #endregion

        #region BtnGrant_Click
        private void BtnGrant_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data();
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


        #region chkselDeselect_CheckedChanged
        private void chkselDeselect_CheckedChanged(object sender, EventArgs e)
        {
            // Select or deselect in Grid as per check box value
            cmnService.J_GridCheckBoxSelectDeselect(grdvDescription, chkselDeselect.Checked);
        }
        #endregion

        #region grdvDescription_CellClick
        private void grdvDescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grdvDescription.Rows[e.RowIndex].Cells[0];
                    if (cell.Value == null || (bool)cell.Value == false)
                    {
                        grdvDescription.MultiSelect = true;
                        grdvDescription.Rows[e.RowIndex].Selected = true;
                        cell.Value = true;
                    }
                    else
                    {
                        cell.Value = false;
                        grdvDescription.Rows[e.RowIndex].Selected = false;
                    }
                }
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }
        #endregion

        #region grdvDescription_KeyDown
        private void grdvDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BtnGrant.Select();
        }
        #endregion

        #region chkselDeselect_MouseMove
        private void chkselDeselect_MouseMove(object sender, MouseEventArgs e)
        {
            chkselDeselect.ForeColor = Color.Blue;
        }
        #endregion

        #region plnDetails_MouseMove
        private void plnDetails_MouseMove(object sender, MouseEventArgs e)
        {
            chkselDeselect.ForeColor = Color.Black;
        }
        #endregion

        #endregion

        #region User Define Functions

        #region ClearControls
        private void ClearControls()
        {
            strSQL = "SELECT USER_ID," +
                "            LOGIN_ID " +
                "     FROM   MST_USER " +
                "     WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                "     AND    " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " <> 'ADMIN' " +
                "     AND    USER_ID  <> " + J_Var.J_pUserId + " " +
                "     ORDER BY LOGIN_ID ";
            dmlService.J_PopulateComboBox(strSQL, ref cmbUserId);
            PopulateDetailGrid();
        }
        #endregion

        #region PopulateDetailGrid
        private void PopulateDetailGrid()
        {
            grdvDescription.DataSource = null;
            rptService.J_PopulateGridView(grdvDescription, strQuery, strMatrix);
            chkselDeselect.Checked = false;
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            //-- User Id selection for the form is must
            if (cmbUserId.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please Select the User Id.");
                cmbUserId.Select();
                return false;
            }

            return true;
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validations
                        if (ValidateFields() == false) return;
                        
                        // save confirmation message
                        if (cmnService.J_SaveConfirmationMessage(ref cmbUserId) == true) return;
                        
                        // begin the transaction
                        dmlService.J_BeginTransaction();

                        // delete query & execution
                        strSQL = "DELETE FROM MST_USER_RIGHTS " +
                            "     WHERE  USER_ID = " + Convert.ToInt64(Support.GetItemData(cmbUserId, cmbUserId.SelectedIndex)) + " ";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbUserId.Select();
                            return;
                        }
                        
                        // selected menu is inserted
                        foreach (DataGridViewRow row in grdvDescription.Rows)
                        {
                            if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                            {
                                strSQL = "INSERT INTO MST_USER_RIGHTS(" +
                                         "            USER_ID," +
                                         "            MENU_ID)" +
                                         "VALUES(" +
                                         "            " + Convert.ToInt64(Support.GetItemData(cmbUserId, cmbUserId.SelectedIndex)) + "," +
                                         "            " + Convert.ToInt64(Convert.ToString(row.Cells[1].Value)) + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    cmbUserId.Select();
                                    return;
                                }
                            }
                        }
                        
                        // transaction is commited
                        dmlService.J_Commit();

                        // display the message in status bur
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, "Menu rights are successfully completed");
                        
                        // all controls are cleared
                        ClearControls();
                        cmbUserId.Select();
                        
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

        
        
        #endregion

        

        
    }
}