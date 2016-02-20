
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Arup 
Module Name		: MstParty
Version			: 2.0
Start Date		: 09-06-2010
End Date		: 09-06-2010
Last Modified   : 
Tables Used     : MST_PARTY, 
Module Desc		: ENTRY OF THE COMPANY MASTER
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

// System Namespaces
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

// User Namespaces
using JAYA.VB;
using BillingSystem.FormRpt;
using BillingSystem.Classes;

// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace BillingSystem.FormMst.NormalEntries
{
    public partial class MstParty : BillingSystem.FormGen.GenForm
    {
        #region System Generated Code
        public MstParty()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        JVBCommon mainVB = new JVBCommon();

        DataSet dsetGridClone = new DataSet();
        
        // For Storing the Id
        long lngSearchId;

        // For Storing the Local SQL Query
        string strSQL;

        // For Storing the general SQL Query
        string strQuery;

        // For Sotring the Order By Values
        string strOrderBy;

        // For Sotring the Where Values
        string strCheckFields;
        
        string strTempMode;
        string[,] strMatrix = null;
        //---------------------------------
        bool blnEmail;
        float inttextSize = 0;

        #endregion

        #region User Defined Events

        #region MstParty_Load
        private void MstParty_Load(object sender, System.EventArgs e)
        {
            try
            {
                //ADDED BY DHRUB ON 03/05/2014 FOR Default Grid Size
                ViewGrid.Size = new Size(1004, 554);
 
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
                string[,] strMatrix1 = {{"PartyID", "0", "", "Right", "", "", ""},
							            {"Party Name", "180", "S", "", "", "", ""},
							            {"Category", "180", "S", "", "", "", ""},
                                        {"Address", "180", "", "", "", "", ""},
							            {"City", "80", "S", "", "", "", ""},
                                        {"Pin", "70", "", "", "", "", ""},
							            {"Contact Person", "130", "S", "", "", "", ""},
                                        {"Mobile No", "100", "", "", "", "", ""},
							            {"Phone", "80", "S", "", "", "", ""},
                                        {"Email Id", "120", "S", "", "", "", ""},
                                        {"Vat No", "80", "S", "", "", "", ""}};
                strMatrix = strMatrix1;
                
                strOrderBy = "PARTY_NAME";
                strQuery = "SELECT MST_PARTY.PARTY_ID                   AS PARTY_ID," +
                    "              MST_PARTY.PARTY_NAME                 AS PARTY_NAME," +
                    "              MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION, " +
                    "              MST_PARTY.ADDRESS1                   AS ADDRESS1," +
                    "              MST_PARTY.CITY                       AS CITY," +
                    "              MST_PARTY.PIN                        AS PIN," +
                    "              MST_PARTY.CONTACT_PERSON             AS CONTACT_PERSON," +
                    "              MST_PARTY.MOBILE_NO                  AS MOBILE_NO," +
                    "              MST_PARTY.PHONE_NO                   AS PHONE_NO," +
                    "              MST_PARTY.EMAIL_ID                   AS EMAIL_ID," +
                    "              MST_PARTY.VAT_NO                     AS VAT_NO " +
                    "       FROM   MST_PARTY, " +
                    "              MST_PARTY_CATEGORY " +
                    "       WHERE  MST_PARTY.PARTY_CATEGORY_ID = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID " +
                    "       and    MST_PARTY.BRANCH_ID         = " + J_Var.J_pBranchId + " ";
                
                strSQL = strQuery + "ORDER BY " + strOrderBy;                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                
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
        public void BtnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                
                strCheckFields = "";
                txtPartyName.Select();
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
                    
                    // A particular id wise retrieving the data from database
                    if (ShowRecord(Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]))) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
                    }
                    
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    
                    strCheckFields = "";
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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
                ClearControls();     //Clear all the Controls
                
                strSQL = strQuery + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId) == false)
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
                
                rbnSortPartyName.Checked = false;              
                rbnSortAsEntered.Checked = false;
                
                if (strOrderBy == "PARTY_NAME")
                    rbnSortPartyName.Select();              
                else if (strOrderBy == "PARTY_ID")
                    rbnSortAsEntered.Select();
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
                if (rbnSortPartyName.Checked == true)
                    strOrderBy = "PARTY_NAME";             
                else if (rbnSortAsEntered.Checked == true)
                    strOrderBy = "PARTY_ID";
                
                if (strCheckFields == "")
                    strSQL = strQuery + "order by " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "order by " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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
                
                txtPartyNameSearch.Select();
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
                
                strCheckFields = "";
                
                // Area name
                if (txtPartyNameSearch.Text.Trim() != "")
                    strCheckFields = "AND MST_PARTY.PARTY_NAME like '%" + cmnService.J_ReplaceQuote(txtPartyNameSearch.Text.Trim().ToUpper()) + "%' ";
                     
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;
                
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId) == false)
                {
                    txtPartyNameSearch.Select();
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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
                
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.Party, this.Text);
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


        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

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
                lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
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
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
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
                              
                
        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region Searching_KeyPress
        private void Searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) BtnSearchOK_Click(sender, e);
            if (Convert.ToInt64(e.KeyChar) == 27) BtnSearchCancel_Click(sender, e);
        }
        #endregion

        #region Sorting_KeyPress
        private void Sorting_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
            txtPartyName.Text = "";
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
            txtVatNo.Text = "";
            
            grpSort.Visible   = false;
            grpSearch.Visible = false;
            
            txtPartyNameSearch.Text = "";

            strSQL = "SELECT PARTY_CATEGORY_ID, " +
                     "       PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   MST_PARTY_CATEGORY " +
                     "WHERE  INACTIVE_FLAG = 0 " +
                     "ORDER BY PARTY_CATEGORY_ID ";

            dmlService.J_PopulateComboBox(strSQL, ref cmbPartyCategory, J_ComboBoxDefaultText.NO);
            
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            
            string strStateName    = string.Empty;
            string strDistrictName = string.Empty;
            
            try
            {
                strSQL = "SELECT MST_PARTY.PARTY_ID                            AS PARTY_ID," +
                    "            MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION," +
                    "            MST_PARTY.PARTY_NAME                          AS PARTY_NAME," +
                    "            MST_PARTY.ADDRESS1                            AS ADDRESS1," +
                    "            MST_PARTY.ADDRESS2                            AS ADDRESS2," +
                    "            MST_PARTY.ADDRESS3                            AS ADDRESS3," +
                    "            MST_PARTY.CITY                                AS CITY," +
                    "            MST_PARTY.PIN                                 AS PIN," +
                    "            MST_PARTY.CONTACT_PERSON                      AS CONTACT_PERSON," +
                    "            MST_PARTY.MOBILE_NO                           AS MOBILE_NO," +
                    "            MST_PARTY.PHONE_NO                            AS PHONE_NO," +
                    "            MST_PARTY.FAX                                 AS FAX," +
                    "            MST_PARTY.EMAIL_ID                            AS EMAIL_ID, " +
                    "            MST_PARTY.VAT_NO                              AS VAT_NO " +                   
                    "     FROM   MST_PARTY, " +
                    "            MST_PARTY_CATEGORY " +
                    "     WHERE  MST_PARTY.PARTY_CATEGORY_ID = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID " +
                    "     AND    MST_PARTY.BRANCH_ID   = " + J_Var.J_pBranchId + " " +
                    "     AND    MST_PARTY.PARTY_ID  = " + Id + " ";
                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;
                
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;
                    txtPartyName.Text         = Convert.ToString(drdShowRecord["PARTY_NAME"]);
                    txtAddress1.Text          = Convert.ToString(drdShowRecord["ADDRESS1"]);
                    txtAddress2.Text          = Convert.ToString(drdShowRecord["ADDRESS2"]);
                    txtAddress3.Text          = Convert.ToString(drdShowRecord["ADDRESS3"]);
                    txtCity.Text              = Convert.ToString(drdShowRecord["CITY"]);
                    txtPin.Text               = Convert.ToString(drdShowRecord["PIN"]);
                    txtContactPersonName.Text = Convert.ToString(drdShowRecord["CONTACT_PERSON"]);
                    txtMobileNo.Text          = Convert.ToString(drdShowRecord["MOBILE_NO"]);
                    txtPhone.Text             = Convert.ToString(drdShowRecord["PHONE_NO"]);
                    txtFaxNo.Text             = Convert.ToString(drdShowRecord["FAX"]);
                    txtEmailID.Text           = Convert.ToString(drdShowRecord["EMAIL_ID"]);
                    txtVatNo.Text             = Convert.ToString(drdShowRecord["VAT_NO"]);

                    cmbPartyCategory.Text = Convert.ToString(drdShowRecord["PARTY_CATEGORY_DESCRIPTION"]);
                  

                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    
                    
                    txtPartyName.Select();
                    return true;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();
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
                    if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    {
                        cmnService.J_UserMessage(J_Msg.DataNotFound);
                        if (dsetGridClone == null) return false;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
                        return false;
                    }
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    if (grpSearch.Visible == false)
                    {
                        if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                        {
                            cmnService.J_UserMessage(J_Msg.DataNotFound);
                            if (dsetGridClone == null) return false;
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtPartyNameSearch.Text.Trim() == "")
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtPartyNameSearch.Select();
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    // Company Name
                    if (txtPartyName.Text.Trim() == "")
                    {
                        cmnService.J_UserMessage("Please enter the Party Name.");
                        txtPartyName.Select();
                        return false;
                    }
                    
                    

                    // Duplicacy check with repect to the mode
                    if (lblMode.Text == J_Mode.Add)
                    {
                        // Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_PARTY",
                            "    PARTY_NAME          = '" + cmnService.J_ReplaceQuote(txtPartyName.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId ) == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtPartyName.Select();
                            return false;
                        }
                        
                    }
                    else if (lblMode.Text == J_Mode.Edit)
                    {
                        //  Duplicacy check with respect to Branch id
                        if (dmlService.J_IsRecordExist("MST_PARTY",
                            "    PARTY_NAME          = '" + cmnService.J_ReplaceQuote(txtPartyName.Text) + "' " +
                            "AND BRANCH_ID          =  " + J_Var.J_pBranchId + " " +                           
                            "AND PARTY_ID           <>  " + lngSearchId + "") == true)
                        {
                            cmnService.J_UserMessage(J_Msg.DuplicateCode);
                            txtPartyName.Select();
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
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtPartyName) == true) return;
                        
                        // set the transaction as begin
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
                                 "            VAT_NO," +
                                 "            USER_ID," +
                                 "            CREATE_DATE) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "            " + Support.GetItemData(cmbPartyCategory, cmbPartyCategory.SelectedIndex) + "," +
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
                                 "           '" + cmnService.J_ReplaceQuote(txtVatNo.Text.Trim()) + "'," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtPartyName.Select();
                            return;
                        }
                        
                        // get last inserted area id as per branch & user
                        lngSearchId = dmlService.J_ReturnMaxValue("MST_PARTY", "PARTY_ID",
                            "    BRANCH_ID         = " + J_Var.J_pBranchId + " " +                           
                            "AND USER_ID           = " + J_Var.J_pUserId + "");
                        if (lngSearchId == 0) return;
                        
                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        // all controls are cleared
                        ClearControls();
                        txtPartyName.Select();
                        
                        break;
                    case J_Mode.Edit:
                        
                        // all validation
                        if (ValidateFields() == false) return;
                        
                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref txtPartyName) == true) return;

                        // set the transaction as begin
                        dmlService.J_BeginTransaction();

                        // update query & execution
                        strSQL = "UPDATE MST_PARTY " +
                                 "SET    PARTY_CATEGORY_ID =  " + Support.GetItemData(cmbPartyCategory, cmbPartyCategory.SelectedIndex) + ", " +
                                 "       PARTY_NAME        = '" + cmnService.J_ReplaceQuote(txtPartyName.Text.Trim()) + "'," +
                                 "       ADDRESS1          = '" + cmnService.J_ReplaceQuote(txtAddress1.Text.Trim()) + "'," +
                                 "       ADDRESS2          = '" + cmnService.J_ReplaceQuote(txtAddress2.Text.Trim()) + "'," +
                                 "       ADDRESS3          = '" + cmnService.J_ReplaceQuote(txtAddress3.Text.Trim()) + "'," +
                                 "       CITY              = '" + cmnService.J_ReplaceQuote(txtCity.Text.Trim()) + "'," +
                                 "       PIN               = '" + cmnService.J_ReplaceQuote(txtPin.Text.Trim()) + "'," +
                                 "       CONTACT_PERSON    = '" + cmnService.J_ReplaceQuote(txtContactPersonName.Text.Trim()) + "'," +
                                 "       MOBILE_NO         = '" + cmnService.J_ReplaceQuote(txtMobileNo.Text.Trim()) + "'," +
                                 "       PHONE_NO          = '" + cmnService.J_ReplaceQuote(txtPhone.Text.Trim()) + "'," +
                                 "       FAX               = '" + cmnService.J_ReplaceQuote(txtFaxNo.Text.Trim()) + "'," +
                                 "       EMAIL_ID          = '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + "'," +
                                 "       VAT_NO            = '" + cmnService.J_ReplaceQuote(txtVatNo.Text.Trim()) + "'" +
                                 "WHERE  PARTY_ID          =  " + lngSearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            txtPartyName.Select();
                            return;
                        }

                        // Transaction is commited
                        dmlService.J_Commit();

                        // after insert data, the message is displayed
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.EditModeSave);

                        // all controls are cleared
                        ClearControls();
                        
                        // Refresh the view grid
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        
                        // change the buttons status
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        
                        BtnDelete.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        
                        ControlVisible(false);
                        
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
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

