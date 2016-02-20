
#region Programmer Information

/*_______________________________________________________________________________________________________

Author			: Ripan Paul
Module Name		: MstUser
Version			: 2.0
Start Date		: 04-08-2010
End Date		: 
Module Desc		: User Master
_________________________________________________________________________________________________________*/

#endregion

#region Refered Namespaces & Classes

// System Namespaces 
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

// User Namespaces 
using BillingSystem;
using BillingSystem.FormMst;
using BillingSystem.FormRpt;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormMst.NormalEntries
{
    public partial class MstUser : BillingSystem.FormGen.GenForm
    {
        #region System Generated Code

        public MstUser()
        {
            InitializeComponent();
        }

        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        
        long lngSearchId;					//For Storing the Id
        
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        
        DataSet dsetGridClone = new DataSet();
        
        string strTempMode;
        
        string[,] strMatrix = null;

        #endregion

        #region User Defined Events

        #region MstUser_Load
        private void MstUser_Load(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ControlVisible(false);
                ClearControls();
                
                // set the Help Grid Column Header Text & behavior
                // (0) Header Text
                // (1) Width
                // (2) Format
                // (3) Alignment
                // (4) NullToText
                // (5) Visible
                // (6) AutoSizeMode
                string[,] strMatrix1 = {{"UserId", "0", "", "Right", "", "", ""},
							            {"Display Name", "525", "S", "", "", "", ""},
							            {"Login Id", "250", "S", "", "", "", ""},
							            {"Status", "150", "0", "", "", "", ""}};
                strMatrix = strMatrix1;
                
                // (0) Data Value in DataTable
                // (1) Data Type in DataTable
                // (2) Display Text in Front-end
                // (3) Display Text Type
                string[,] strCaseEndMatrix = {{"=0", "N", "ACTIVE", "T"},
							                  {"=1", "N", "INACTIVE", "T"}};
                
                strOrderBy = "DISPLAY_NAME";
                strQuery = "SELECT USER_ID                           AS USER_ID," +
                    "              DISPLAY_NAME                      AS DISPLAY_NAME," +
                    "              LOGIN_ID                          AS LOGIN_ID," +
                    "              " + cmnService.J_SQLDBFormat("USER_CATEGORY", J_SQLColFormat.Case_End, strCaseEndMatrix) + " AS USER_CATEGORY " +
                    "       FROM   MST_USER " +
                    "       WHERE  " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " <> 'ADMIN' " +
                    "       AND    BRANCH_ID = " + J_Var.J_pBranchId + " ";
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                
                txtUserName.Enabled = false;
                lblTitle.Text = this.Text;
                ViewGrid_Click(sender, e);
            }
            catch (Exception err_Handler)
            {
                cmnService.J_UserMessage(err_Handler.Message);
            }
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
         
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                
                txtUserName.Enabled = true;
                txtLoginId.ReadOnly = false;
                txtLoginId.BackColor = Color.White;
                
                strCheckFields = "";
                txtUserName.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnEdit_Click
        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    ControlVisible(true);
                    ClearControls();
                    grpPassword.Visible = false;
                    
                    txtLoginId.ReadOnly = true;
                    txtLoginId.BackColor = Color.LightYellow;
                    
                    chkResetPassword.Visible = true;
                    chkResetPassword.Checked = false;
                    chkResetPassword.Location = new Point(428, 235);
                    
                    // A particular id wise retriving the data from database
                    if (ShowRecord(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
                    }
                    
                    txtUserName.Enabled = true;
                    
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    
                    strCheckFields = "";
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                
                strSQL = strQuery + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId) == false)
                    BtnAdd.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSort_Click
        private void BtnSort_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Sorting;
                if (ValidateFields() == false) return;
                
                grpSort.Visible = true;
                grpSearch.Visible = false;
                
                rbnSortUserName.Checked = false;
                rbnSortAsEntered.Checked = false;
                
                if (strOrderBy == "USER_ID")
                    rbnSortAsEntered.Select();
                else if (strOrderBy == "DISPLAY_NAME")
                    rbnSortUserName.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 


        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Display Name
                if (rbnSortUserName.Checked == true)
                    strOrderBy = "DISPLAY_NAME";

                // As Entered
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "USER_ID";

                strCheckFields = "";
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortOK_KeyPress
        private void BtnSortOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortCancel_KeyPress
        private void BtnSortCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearch_Click
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Searching;
                
                if (ValidateFields() == false) return;
                
                grpSort.Visible = false;
                grpSearch.Visible = true;
                
                txtUserNameSearch.Select();
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSearchOK_Click
        private void BtnSearchOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Validate Fields
                if (ValidateFields() == false) return;
                
                // Storing the Criteria Fiels & Values 
                strCheckFields = "";
                
                // User name search
                if (txtUserNameSearch.Text.Trim() != "")
                    strCheckFields = "AND " + cmnService.J_SQLDBFormat("DISPLAY_NAME", J_SQLColFormat.UCase) + " like '%" + cmnService.J_ReplaceQuote(txtUserNameSearch.Text.Trim().ToUpper()) + "%' ";
                
                strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId) == false)
                {
                    txtUserNameSearch.Select();
                    return;
                }
                
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchOK_KeyPress
        private void BtnSearchOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnSearchCancel_Click
        private void BtnSearchCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.General;
                grpSearch.Visible = false;
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSearchCancel_KeyPress
        private void BtnSearchCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (ViewGrid.CurrentRowIndex >= 0)
            //    {
            //        lblMode.Text = J_Mode.Delete;

            //        //-- To Validate the User deletion
            //        if (Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 1]) == "ADMINSTRATOR")
            //        {
            //            cmnService.J_UserMessage("You cannot delete the ADMINSTRATOR !!");
            //            lblMode.Text = J_Mode.View;
            //            if (strCheckFields == "")
            //                strSQL = strQuery + "order by " + strOrderBy;
            //            else
            //                strSQL = strQuery + strCheckFields + "order by " + strOrderBy;

            //            if (dsetGridClone != null) dsetGridClone.Clear();
            //            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //            if (dsetGridClone == null) return;
            //            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);

            //            return;
            //        }
            //        else if (Convert.ToInt64(lngSearchId) == Convert.ToInt64(J_Var.J_pUserId))
            //        {
            //            cmnService.J_UserMessage("You cannot DELETE the Logged on User !!");

            //            lblMode.Text = J_Mode.View;
            //            if (strCheckFields == "")
            //                strSQL = strQuery + "order by " + strOrderBy;
            //            else
            //                strSQL = strQuery + strCheckFields + "order by " + strOrderBy;

            //            if (dsetGridClone != null) dsetGridClone.Clear();
            //            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
            //            if (dsetGridClone == null) return;
            //            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);

            //            return;
            //        }
            //        else
            //            Insert_Update_Delete_Data();

            //        lblSearchMode.Text = J_Mode.General;
            //        grpSort.Visible = false;
            //        grpSearch.Visible = false;
            //    }
            //    else
            //    {
            //        cmnService.J_UserMessage(J_Msg.DataNotFound);
            //        if (dsetGridClone == null) return;
            //        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
            //    }
            //}
            //catch (Exception err_handler)
            //{
            //    cmnService.J_UserMessage(err_handler.Message);
            //}
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);
                
                lblSearchMode.Text = J_Mode.General;
                
                BtnDelete.BackColor = Color.LightGray;
                BtnDelete.Enabled = false;
                
                ClearControls();
                strCheckFields = "";
                strSQL = strQuery + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.User, this.Text);
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion


        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }
        #endregion

        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, System.EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);

                strTempMode = lblMode.Text;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString());
        }
        #endregion

        #region ViewGrid_MouseMove
        private void ViewGrid_MouseMove(object sender, MouseEventArgs e)
        {
            cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion


        #region chkResetPassword_CheckedChanged
        private void chkResetPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResetPassword.Checked == true)
            {
                grpPassword.Visible = true;
                grpPassword.Location = new Point(288,256);
            }
            else if (chkResetPassword.Checked == false)
            {
                grpPassword.Visible = false;
            }
        }
        #endregion


        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region txtUserNameSearch_KeyPress
        private void txtUserNameSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region rbnSort_KeyPress
        private void rbnSort_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSortOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSortCancel_Click(sender, e);
        }
        #endregion 

        #endregion

        #region User Define Functions

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtUserName.Text = "";
            txtLoginId.Text = "";
            
            txtPassword.Text = "";
            txtPassword2.Text = "";
            
            grpSort.Visible = false;
            grpSearch.Visible = false;
            
            txtUserNameSearch.Text = "";
            
            chkActiveInactive.Checked = true;
            chkResetPassword.Visible = false;
            
            grpPassword.Visible = true;
            grpPassword.Location = new Point(288, 230);
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;

            try
            {
                // SQL Query
                strSQL = "SELECT DISPLAY_NAME," +
                         "       LOGIN_ID," +
                         "       USER_CATEGORY " +
                         "FROM   MST_USER " +
                         "WHERE  USER_ID = " + Id + "";
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    txtUserName.Text = drdShowRecord["DISPLAY_NAME"].ToString();
                    txtLoginId.Text  = drdShowRecord["LOGIN_ID"].ToString();
                    
                    if (Convert.ToInt32(drdShowRecord["USER_CATEGORY"].ToString()) == 0)
                        chkActiveInactive.Checked = true;
                    else
                        chkActiveInactive.Checked = false;

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    txtUserName.Select();
                    return true;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                
                lngSearchId = 0;
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    if (Convert.ToInt64((ViewGrid.CurrentRowIndex).ToString()) < 0)
                    {
                        cmnService.J_UserMessage(J_Msg.DataNotFound);
                        if (dsetGridClone == null) return false;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
                        return false;
                    }
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(ViewGrid.CurrentRowIndex.ToString()) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtUserNameSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage("Please enter the user name to search.");
                            txtUserNameSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (txtUserName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the user name.");
                        txtUserName.Select();
                        return false;
                    }

                    if (txtLoginId.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the login id.");
                        txtLoginId.Select();
                        return false;
                    }

                    if ((lblMode.Text == J_Mode.Add) || (lblMode.Text == J_Mode.Edit && chkResetPassword.Checked == true))
                    {
                        if (txtPassword.Text == "")
                        {
                            cmnService.J_UserMessage("Please enter the password.");
                            txtPassword.Select();
                            return false;
                        }

                        if (txtPassword2.Text == "")
                        {
                            cmnService.J_UserMessage("Please enter the password.");
                            txtPassword2.Select();
                            return false;
                        }

                        if (txtPassword.Text != txtPassword2.Text)
                        {
                            cmnService.J_UserMessage("Entered passwords do not match.");
                            txtPassword.Select();
                            return false;
                        }
                    }
                    
                    // Duplicacy check with respect to the mode of other validations
                    if (lblMode.Text == J_Mode.Add)
                    {
                        // Duplicate check for LOGIN ID ONLY
                        if (dmlService.J_IsRecordExist("MST_USER",
                            "    BRANCH_ID = " + J_Var.J_pBranchId + " " +
                            "AND " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " = '" + txtLoginId.Text.ToUpper() + "'") == true)
                        {
                            cmnService.J_UserMessage("Login id already exist.");
                            txtLoginId.Select();
                            return false;
                        }
                        
                        // Check active/inactive
                        if (chkActiveInactive.Checked == false)
                        {
                            cmnService.J_UserMessage("New user should be active.");
                            chkActiveInactive.Select();
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                string strLoginId = string.Empty;
                int intCategory = 0;
                
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validations
                        if (ValidateFields() == false) return;

                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtUserName) == true) return;
                        
                        intCategory = Convert.ToInt32(chkActiveInactive.Checked == true ? "0" : "1");
                        
                        // Begin Transaction
                        dmlService.J_BeginTransaction();

                        strSQL = "INSERT INTO MST_USER (" +
                                 "            BRANCH_ID," +
                                 "            USER_CODE," +
                                 "            DISPLAY_NAME," +
                                 "            LOGIN_ID," +
                                 "            USER_PASSWORD," +
                                 "            USER_CATEGORY) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "           ''," +
                                 "           '" + cmnService.J_ReplaceQuote(txtUserName.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtLoginId.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtPassword.Text.Trim()) + "'," +
                                 "            " + intCategory + ")";
                        
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtUserName.Select();
                            return;
                        }
                        
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_USER", "USER_ID", 
                            "    BRANCH_ID = " + J_Var.J_pBranchId + " " +
                            "AND " + cmnService.J_SQLDBFormat("LOGIN_ID", J_SQLColFormat.UCase) + " = '" + cmnService.J_ReplaceQuote(txtLoginId.Text.Trim().ToUpper()) + "'");
                        if (lngSearchId == 0) return;
                        
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        ClearControls();
                        txtUserName.Select();
                        
                        break;
                    case J_Mode.Edit:
                        // For Modify
                        if (ValidateFields() == false) return;
                        if (cmnService.J_SaveConfirmationMessage(ref txtUserName) == true) return;
                       
                        intCategory = Convert.ToInt32(chkActiveInactive.Checked == true ? "0" : "1");

                        // Begin Transaction
                        dmlService.J_BeginTransaction();
                        
                        strSQL = "UPDATE MST_USER " +
                                 "SET    DISPLAY_NAME  = '" + cmnService.J_ReplaceQuote(txtUserName.Text.Trim()) + "'," +
                                 "       USER_CATEGORY =  " + intCategory + " ";
                        
                        // If reset password is checked then update password too
                        
                        if (chkResetPassword.Checked == true)
                            strSQL = strSQL + ", USER_PASSWORD = '" + cmnService.J_ReplaceQuote(txtPassword.Text) + "' ";
                        strSQL = strSQL + "WHERE USER_ID = " + lngSearchId + "";
                        
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtUserName.Select();
                            return;
                        }
                        
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.EditModeSave);
                        
                        ClearControls();
                        
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        
                        BtnDelete.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        
                        ControlVisible(false);
                        
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "USER_ID", lngSearchId);
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

