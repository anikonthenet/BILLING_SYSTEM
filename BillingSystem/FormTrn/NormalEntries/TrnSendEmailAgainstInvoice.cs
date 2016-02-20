#region Programmer Information

/*
____________________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: TrnInvoiceEntry
Version			: 2.0
Start Date		: 09-06-2011
End Date		: 
Tables Used     : 
Module Desc		: Invoice Entry
____________________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

// System Namespaces
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

using JAYA.VB;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;


// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.FormCmn;
using BillingSystem.FormRpt;
using BillingSystem.Classes;
using BillingSystem.FormTrn.PopUp;
using BillingSystem.Reports.Listing;
using BillingSystem.Reports.Transaction.INVOICE;


#endregion

namespace BillingSystem.FormTrn.NormalEntries
{
    public partial class TrnSendEmailAgainstInvoice : BillingSystem.FormGen.GenForm
    {
        #region Default Constructor
        public TrnSendEmailAgainstInvoice()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration

        // Variables and objects declaration
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        DataSet dsetGridClone = new DataSet();
        DataSet dsDataPrint = new DataSet();  
        JVBCommon mainVB = new JVBCommon();
        ReportService rptService = null;
        RptDialog rptdialog;
        BS BillingSystem = new BS(); 
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string strCheckFields;				//For Sotring the Where Values
        string[,] strMatrix;
        string strEmailBody = "";           // FOR INITIALIZE THE EMAIL BODY AFTER RETRIEVAL FROM DATABASE
        string strEmailSubject = "";
        string strSelectedFileName = "";    // To initialize the pdf file path
        string strGenerateInvoiceFolderName = "GenerateInvoice";
        string strFileNotFoundImagePath = Path.Combine(Application.StartupPath + "\\GenerateInvoice"  , "not-found.jpg");
        string strEmail = "";
        string strWebsite = "";
        string strDisplayName = "";
        string strImageUrl = "";
        string strHTMLText = "";
        //-----------------------------------------------------------------------
        long lngSearchId = 0;
        long lngPartyId = 0;
        //-----------------------------------------------------------------------
        #endregion

        #region User Defined Events

        #region TrnSendEmailAgainstInvoice_Load
        private void TrnSendEmailAgainstInvoice_Load(object sender, EventArgs e)
        {
            //ADDED BY DHRUB ON 07/05/2014
            ViewGrid.Size = new Size(1000, 506);
            //---------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //-----------------------------------------
            ControlVisible(true);
            ClearControls();
            //-----------------------------------------
            lblTitle.Text = this.Text;

            strSQL = "SELECT COMPANY_ID," +
                     "       COMPANY_NAME " +
                     "FROM   MST_COMPANY " +
                     "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId  + " " +                     
                     "ORDER BY COMPANY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany,0) == false) return;
            //-----------------------------------------

            cmnService.J_CreateDirectory(Application.StartupPath + "\\" + strGenerateInvoiceFolderName);

            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Path.Combine(Application.StartupPath ,strGenerateInvoiceFolderName));
            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                file.Delete();
            }
            //-----------------------------------------
        }
        #endregion

        #region BtnEdit_Click
        private void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex >= 0)
                {
                    ClearControls();
                    //-------------------------------
                    this.Cursor = Cursors.WaitCursor;
                    //-------------------------------
                    // A particular id wise retriving the data from database
                    if (ShowRecord(Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0].ToString())) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                    }
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    lblSearchMode.Text = J_Mode.General;
                    ControlVisible(true);
                    //-------------------------------
                    this.Cursor = Cursors.Default;
                    //-------------------------------
                    //-------------------------------
                    strCheckFields = "";

                }
                else
                {
                    cmnService.J_UserMessage("No record selected");
                    if (dsetGridClone == null) return;
                    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnCancel_Click

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------

                ControlVisible(false);
                ClearControls();					//Clear all the Controls

                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-------------------------------------------
                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId) == false)
                    BtnAdd.Select();
                //-------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
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

                // Storing the Criteria Fiels & Values 
                strCheckFields = "";

                // Party name search
                if (txtPartyNameSearch.Text.Trim() != "")
                    strCheckFields += "AND MST_PARTY.PARTY_NAME LIKE '%" + cmnService.J_ReplaceQuote(txtPartyNameSearch.Text.Trim().ToUpper()) + "%' ";

                // Invoice Date Search
                if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDateSearch) + cmnService.J_DateOperator() + " ";

                // Reference Search
                if (txtInvoiceNoSearch.Text.Trim() != "")
                    strCheckFields += "AND TRN_INVOICE_HEADER.INVOICE_NO LIKE '%" + cmnService.J_ReplaceQuote(txtInvoiceNoSearch.Text.Trim().ToUpper()) + "%' ";
                
                strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                if (dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId) == false)
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
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


                ClearControls();
                ControlVisible(true);
                strCheckFields = "";
                strSQL = strQuery + "ORDER BY " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSortCancel_Click
        private void BtnSortCancel_Click(object sender, EventArgs e)
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

                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSortOK_Click
        private void BtnSortOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Invoice Header ID
                if (rdbtnAsEntered.Checked == true)
                    strOrderBy = "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

                // STAR Marked 
                else if (rdbtnMaxDaysPermit.Checked == true)
                    strOrderBy = @"CASE WHEN 
                                       (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                    OR (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                  THEN '*' ELSE '' END DESC";

                strCheckFields = "";
                if (strCheckFields == "")
                    strSQL = strQuery + "ORDER BY " + strOrderBy;
                else
                    strSQL = strQuery + strCheckFields + "ORDER BY " + strOrderBy;

                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;

                lblSearchMode.Text = J_Mode.General;
                grpSort.Visible = false;
                dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion 

        #region BtnSort_Click
        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchMode.Text = J_Mode.Sorting;
                if (ValidateFields() == false) return;

                grpSort.Visible = true;
                grpSearch.Visible = false;

                rdbtnAsEntered.Checked = false;
                rdbtnMaxDaysPermit.Checked = false;

                if (strOrderBy == "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC")
                    rdbtnAsEntered.Select();
                else if (strOrderBy == @"CASE WHEN 
                                       (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                    OR (CASE WHEN ABS(DATEDIFF(DAY,TRN_INVOICE_HEADER.INVOICE_DATE,TRN_INVOICE_HEADER.BANK_STATEMENT_DATE))> 30 THEN '*' ELSE '' END) = '*'
                                  THEN '*' ELSE '' END DESC")
                    rdbtnMaxDaysPermit.Select();

            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Insert_Update_Delete_Data();
        }
        #endregion

        #region btnPreviewBill_Click
        private void btnPreviewBill_Click(object sender, EventArgs e)
        {
            if (txtPDFPath.Text.Trim() != "")
                Process.Start(txtPDFPath.Text);
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1. HeaderText,
            //2. Width,
            //3. Format,
            //4. Alignment,
            //5. NullToText,
            //6. Visible,
            //7. AutoSizeMode
            string[,] strMatrix1 =  {{"Invoice Header Id", "0", "", "Right", "", "false", ""},
							         {"Invoice No.", "110", "", "", "", "", ""},
							         {"Invoice Date", "70", "dd/MM/yyyy", "", "", "", ""},
							         {"Party", "180", "", "", "", "", "fill"},
							         {"Category", "50", "", "", "", "", ""},
							         {"Net Amount.", "70", "0.00", "Right", "", "", ""},
                                     {"Payment Type.", "100", "", "", "", "", ""},
                                     {"Bank", "40", "", "", "", "", ""},
                                     {"Reference No", "70", "", "", "", "", ""},
                                     {"A/c Entry Date", "80", "dd/MM/yyyy", "", "", "", ""},
                                     {"Bank Date", "60", "dd/MM/yyyy", "", "", "", ""},
                                     {"EMAIL", "40","","CENTER","","",""},
                                     {"Send Date", "70", "dd/MM/yyyy", "", "", "", ""}};

            strMatrix = strMatrix1;

            strQuery = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID          AS INVOICE_HEADER_ID,              
                                TRN_INVOICE_HEADER.INVOICE_NO                 AS INVOICE_NO,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS INVOICE_DATE, 
                                " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck, null, "********* UNKNOWN DEPOSIT *********") + @" AS PARTY_NAME,
                                MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION AS PARTY_CATEGORY_DESCRIPTION,
                                TRN_INVOICE_HEADER.NET_AMOUNT                 AS NET_AMOUNT,
                                MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION     AS PAYMENT_TYPE_DESCRIPTION,
                                MST_BANK.BANK_NAME                            AS BANK_NAME,
                                TRN_INVOICE_HEADER.REFERENCE_NO               AS REFERENCE_NO,
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.ACCOUNT_ENTRY_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS ACCOUNT_ENTRY_DATE, 
                                " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.BANK_STATEMENT_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @" AS BANK_STATEMENT_DATE,
                                CASE WHEN TRN_INVOICE_HEADER.EMAIL_CONFIRMATION_COUNTER = 0 
                                     THEN '' 
                                     ELSE CAST(TRN_INVOICE_HEADER.EMAIL_CONFIRMATION_COUNTER AS CHAR)
                                END                                           AS SEND_STATUS,
                                TRN_INVOICE_HEADER.EMAIL_SEND_DATE            AS EMAIL_SEND_DATE
                       FROM   (((((TRN_INVOICE_HEADER
                       LEFT JOIN (SELECT INVOICE_HEADER_ID 
                                  FROM TRN_INVOICE_HEADER 
                                  WHERE PAYMENT_TYPE_ID = 4
                                  AND   ACCOUNT_ENTRY_DATE IS NOT NULL) AS CASH_RECONCILED
                               ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = CASH_RECONCILED.INVOICE_HEADER_ID)
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID)
                       LEFT JOIN MST_PARTY_CATEGORY 
                               ON MST_PARTY.PARTY_CATEGORY_ID        = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID)
                       LEFT JOIN MST_PAYMENT_TYPE
                               ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID)
                       LEFT JOIN MST_BANK
                               ON TRN_INVOICE_HEADER.BANK_ID         = MST_BANK.BANK_ID)";
            
            if (cmbCompany.SelectedIndex <= 0)
            {
                strQuery += "WHERE 1 = 2 ";
            }
            else
            {
                strQuery += "WHERE  TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                            "AND    TRN_INVOICE_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                            "AND    TRN_INVOICE_HEADER.TRAN_TYPE  = 'INV' ";
            }


            if (rbnLoadPending.Checked == true)
            {
                strQuery += @" AND TRN_INVOICE_HEADER.EMAIL_CONFIRMATION_COUNTER = 0";
            }

            strOrderBy = "TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid

        }
        #endregion

        #region rbnLoadAll_CheckedChanged
        private void rbnLoadAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0)
                return;

            cmbCompany_SelectedIndexChanged(sender, e);

        }
        #endregion 


        #region rbnExistingEmailCategory_CheckedChanged
        private void rbnExistingEmailCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnExistingEmailCategory.Checked == true)
            {
                lblExistingEmailFormatDesc.Visible = true;
                cmbEmailCategoryDesc.Visible = true;
                //--------------------------------------------
                lblNewEmailFormatDesc.Visible = false;
                txtEmailCategoryDesc.Visible = false;
            }
            else if (rbnNewEmailFormat.Checked == true)
            {
                lblExistingEmailFormatDesc.Visible = false;
                cmbEmailCategoryDesc.Visible = false;
                //--------------------------------------------
                lblNewEmailFormatDesc.Visible = true;
                txtEmailCategoryDesc.Visible = true;
 
            }
        }
        #endregion

        #region cmbEmailCategoryDesc_SelectedIndexChanged
        private void cmbEmailCategoryDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDataReader reader = null;
            try
            {
                if (cmbEmailCategoryDesc.SelectedIndex <= 0)
                    return;

                strSQL = @" SELECT  EMAIL_BODY,
                                    EMAIL_SUBJECT
                            FROM    MST_EMAIL_CATEGORY
                            WHERE   EMAIL_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbEmailCategoryDesc, cmbEmailCategoryDesc.SelectedIndex);

                // strEmailBody = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                if (reader == null) return;

                while (reader.Read())
                {
                    strEmailBody = Convert.ToString(reader["EMAIL_BODY"]);
                    rtxtEmailBody.Text = Convert_HtmlToText(Convert.ToString(reader["EMAIL_BODY"]));
                    strEmailSubject = Convert.ToString(reader["EMAIL_SUBJECT"]);

                    reader.Close();
                    reader.Dispose();
                    return;
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                reader.Close();
                reader.Dispose();
                return;
            }
        }
        #endregion

        #region BtnBrowse_Click
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            GC.Collect();
            //---------------------------------------
            // Configure open file dialog box
            //---------------------------------------
            System.Windows.Forms.OpenFileDialog openFile;
            openFile = new System.Windows.Forms.OpenFileDialog();
            //openFile.Filter = "Images (.jpg)|*.jpg|Pdf Files|*.pdf"; // Filter files by extension
            openFile.Filter = "Pdf Files|*.pdf"; // Filter files by extension
            openFile.Title = "Select a file";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            strSelectedFileName = openFile.FileName;

            //---------------------------------------
            this.Cursor = Cursors.WaitCursor;
            //---------------------------------------
            //InitializeAdobe(strSelectedFileName);
            //---------------------------------------
            InitializeWebBrowser(strSelectedFileName);
            //---------------------------------------
            this.Cursor = Cursors.Default;
            //---------------------------------------
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

                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);

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
            //cmnService.J_GridToolTip(ViewGrid, e.X, e.Y);
        }
        #endregion

        #region ViewGrid_MouseUp
        private void ViewGrid_MouseUp(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
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

        #endregion

        #region User Defined Functions

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            if (lblMode.Text == J_Mode.Edit)
            {
                cmbCompany.Enabled = false;
                grpPopulateData.Enabled = false;
            }
            else
            {
                cmbCompany.Enabled = true;
                grpPopulateData.Enabled = true;
            }


            BtnAdd.Enabled = false;
            BtnAdd.BackColor = Color.LightGray;
            //--
            BtnDelete.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            //
            BtnPrint.Enabled = false;
            BtnPrint.BackColor = Color.LightGray;
            //
            BtnSort.Enabled = false;
            BtnSort.BackColor = Color.LightGray;

            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            GC.Collect();
            //----------------------------------------------------------
            wbsViewPdf.Navigate(strFileNotFoundImagePath);
            wbsViewPdf.Navigate(new Uri(strFileNotFoundImagePath));
            wbsViewPdf.Url = new System.Uri(strFileNotFoundImagePath);
            this.wbsViewPdf.Url = new System.Uri(strFileNotFoundImagePath, System.UriKind.Absolute);
            //----------------------------------------------------------
            lblInvoiceNo.Text = "";
            lblInvoiceDate.Text = "";
            lblPartyName.Text = "";
            lblAmount.Text = "";
            txtEmailID.Text = ""; 
            txtEmailIDBcc.Text = ""; 
            //----------------------------------------------------------
            rbnExistingEmailCategory.Checked = true;
            rbnLoadAll.Checked = true;
            //----------------------------------------------------------
            strSQL = @" SELECT EMAIL_TYPE_ID,
                               EMAIL_TYPE_DESC
                        FROM   MST_EMAIL_CATEGORY
                        ORDER BY EMAIL_TYPE_DESC ";
            //----------------------------------------------------------
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbEmailCategoryDesc) == false) return;
            if (cmbEmailCategoryDesc.Items.Count > 0)cmbEmailCategoryDesc.SelectedIndex = 0;
            //----------------------------------------------------------
            rtxtEmailBody.Text = ""; 
            txtPartyNameSearch.Text = "";
            txtInvoiceNoSearch.Text = "";
            mskInvoiceDateSearch.Text = "";
            //----------------------------------------------------------
            strEmailSubject = "";
            strEmailBody = "";
            strEmail = "";
            strHTMLText = "";
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            IDataReader reader = null;

            try
            {
                // SQL Query
                strSQL = @"SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID      AS INVOICE_HEADER_ID,              
                              TRN_INVOICE_HEADER.INVOICE_NO                 AS INVOICE_NO,
                              " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + @"  AS INVOICE_DATE, 
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_ID", J_ColumnType.Long, J_SQLColFormat.NullCheck) + @"     AS PARTY_ID,
                              " + cmnService.J_SQLDBFormat("MST_PARTY.PARTY_NAME", J_ColumnType.String, J_SQLColFormat.NullCheck) + @" AS PARTY_NAME,
                              MST_PARTY.EMAIL_ID                            AS PARTY_EMAIL_ID,
                              TRN_INVOICE_HEADER.NET_AMOUNT                 AS NET_AMOUNT,
                              TRN_INVOICE_HEADER.EMAIL_CONFIRMATION_COUNTER AS SEND_STATUS,
                              TRN_INVOICE_HEADER.EMAIL_SEND_DATE            AS EMAIL_SEND_DATE
                       FROM   TRN_INVOICE_HEADER
                       LEFT JOIN MST_PARTY 
                               ON TRN_INVOICE_HEADER.PARTY_ID        = MST_PARTY.PARTY_ID
                       WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + Id;

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null) return false;

                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    lngPartyId = Convert.ToInt64(drdShowRecord["PARTY_ID"].ToString());
                    lblInvoiceNo.Text = drdShowRecord["INVOICE_NO"].ToString();
                    lblInvoiceDate.Text = drdShowRecord["INVOICE_DATE"].ToString();
                    lblAmount.Text = string.Format("{0:0.00}", Convert.ToDouble(drdShowRecord["NET_AMOUNT"].ToString()));
                    lblPartyName.Text = drdShowRecord["PARTY_NAME"].ToString();
                    txtEmailID.Text = Convert.ToString(drdShowRecord["PARTY_EMAIL_ID"]);
                    if (Convert.ToInt32(drdShowRecord["SEND_STATUS"]) > 0)
                    {
                        lblEmailSendCounter.Text = Convert.ToString(drdShowRecord["SEND_STATUS"]);
                        lblLastSendDate.Text = drdShowRecord["EMAIL_SEND_DATE"].ToString();

                        lblEmailSendCounter.Visible = true;
                        lblLastSendDate.Visible = true;
                    }
                    else
                    {
                        lblEmailSendCounter.Visible = false;
                        lblLastSendDate.Visible = false;
                    }
                    
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();

                    //---------------------------------------------------------------
                    // GENETARING THE INVOICE PDF 
                    // [IF FALSE THEN WILL SHOW THE DEFAULT IMAGE  ELSE THE PDF]
                    //---------------------------------------------------------------
                    if (GenerateInvoicePDF() == true)
                    {
                        InitializeWebBrowser(Path.Combine(Application.StartupPath + "\\" + strGenerateInvoiceFolderName, Convert.ToString(lblInvoiceNo.Text.Replace("/", "-").Trim())) + ".pdf");
                        if (FetchItemWiseEmailDetails(lngSearchId) == true) 
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                drdShowRecord.Close();
                drdShowRecord.Dispose();


                cmnService.J_UserMessage(J_Msg.RecNotExist);

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

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        break;
                    case J_Mode.Edit:
                        //-----------------------------------------------------------
                        if (ValidateFields() == false) return;
                        //-----------------------------------------------------------
                        this.Cursor = Cursors.WaitCursor;
                        //-----------------------------------------------------------
                        dmlService.J_BeginTransaction();
                        //-----------------------------------------------------------
                        
                        //-----------------------------------------------------------
                        if (SendEMail(txtEmailID.Text.Trim(), txtEmailIDBcc.Text.Trim(), strEmail, strWebsite, strDisplayName, strImageUrl, "") == true)
                        {
                            //----------------------------------------------------
                            // UPDATE THE PARTY MASTER TABLE WITH PARTY EMAIL ID
                            //----------------------------------------------------
                            strSQL = @" UPDATE MST_PARTY
                                        SET    EMAIL_ID  = '" + txtEmailID.Text.Trim() + @"' 
                                        WHERE  PARTY_ID = " + lngPartyId;

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                txtEmailID.Select();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            //----------------------------------------------------
                            //----------------------------------------------------
                            strSQL = @" UPDATE TRN_INVOICE_HEADER
                                        SET    EMAIL_CONFIRMATION_COUNTER = EMAIL_CONFIRMATION_COUNTER + 1, 
                                               EMAIL_SEND_DATE = " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(Convert.ToString(System.DateTime.Now)) + cmnService.J_DateOperator() +
                                     @" WHERE  INVOICE_HEADER_ID = " + lngSearchId;
                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                txtEmailID.Select();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            //-----------------------------------------------------------
                            // UPDATE THE LOG HISTORY TABLE 
                            //-----------------------------------------------------------
                            strSQL = @" INSERT INTO  TRN_EMAIL_ACTIVITY_LOG( INVOICE_HEADER_ID, 
                                                                             INVOICE_NO,
                                                                             SEND_EMAIL_TO,
                                                                             SEND_EMAIL_BCC,
                                                                             USER_ID,
                                                                             ACTIVITY_DATE)
                                                                    VALUES ( " + lngSearchId + @",
                                                                            '" + lblInvoiceNo.Text + @"',
                                                                            '" + cmnService.J_ReplaceQuote(txtEmailID.Text.Trim()) + @"',
                                                                            '" + cmnService.J_ReplaceQuote(txtEmailIDBcc.Text.Trim()) + @"',
                                                                             " + J_Var.J_pUserId + @",
                                                                             " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(Convert.ToString(System.DateTime.Now)) + cmnService.J_DateOperator() + @")";

                            if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                            {
                                dmlService.J_Rollback();
                                txtEmailID.Select();
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                        else
                        { 
                            dmlService.J_Rollback();
                            txtEmailID.Select();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        //-----------------------------------------------------------
                        cmnService.J_UserMessage("Email Successfully Send TO \n[ "+ txtEmailID.Text + " ].");
                        //-----------------------------------------------------------
                        ClearControls();
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref ViewGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                        cmnService.J_StatusButton(this, lblMode.Text);
                        ControlVisible(true); 
                        //-----------------------------------------------------------
                        dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                        //-----------------------------------------------------------
                        this.Cursor = Cursors.Default;
                        //-----------------------------------------------------------
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

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64((ViewGrid.CurrentRowIndex).ToString()) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "PARTY_ID", lngSearchId);
                    //    return false;
                    //}
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
                            dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "INVOICE_HEADER_ID", lngSearchId);
                            return false;
                        }
                    }
                    else if (grpSearch.Visible == true)
                    {
                        if (txtPartyNameSearch.Text.Trim() == "" &&
                            dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == true &&
                            txtInvoiceNoSearch.Text.Trim() == "" )
                        {
                            cmnService.J_UserMessage(J_Msg.SearchingValues);
                            txtPartyNameSearch.Select();
                            return false;
                        }
                        if (dtService.J_IsBlankDateCheck(ref mskInvoiceDateSearch, J_ShowMessage.NO) == false)
                        {
                            if (dtService.J_IsDateValid(mskInvoiceDateSearch) == false)
                            {
                                cmnService.J_UserMessage("Enter valid Invoice Date to search.");
                                mskInvoiceDateSearch.Select();
                                return false;
                            }
                        }
                    }

                    return true;
                }
                else
                {

                    strSQL = @" SELECT MST_ITEM.EMAIL_DETAIL_ID 
                                FROM   TRN_INVOICE_HEADER 
                                       INNER JOIN TRN_INVOICE_DETAIL
                                       ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID 
                                       INNER JOIN MST_ITEM
                                       ON TRN_INVOICE_DETAIL.ITEM_ID = MST_ITEM.ITEM_ID 
                                       INNER JOIN MST_EMAIL_DETAIL
                                       ON MST_ITEM.EMAIL_DETAIL_ID = MST_EMAIL_DETAIL.EMAIL_DETAIL_ID 
                                WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + lngSearchId + " ";

                    if (Convert.ToUInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) == 0)
                    {
                        cmnService.J_UserMessage("Please specify a email details against this Invoice.");
                        txtEmailID.Select();
                        return false;
                    }

                    //---------------------------------------------
                    //VALIDATION FOR EMAIL ID TO 
                    //---------------------------------------------
                    if (txtEmailID.Text.Trim() != "")
                    {
                        if (txtEmailID.Text.Trim().Contains(";") == true)
                        {
                            cmnService.J_UserMessage("Please seperate multiple Email ID with ','");
                            txtEmailID.Select();
                            return false;
                        }
                        //----------------------------------------------------------
                        if(txtEmailID.Text.Trim().Contains(",") == true)
                        {
                            string[] strEmail_ID = txtEmailID.Text.Trim().Split(',');

                            for (int i = 0; i < strEmail_ID.Length; i++)
                            {
                                if (IsValidEmailAddress(strEmail_ID[i]) == false)
                                {
                                    cmnService.J_UserMessage("Please enter a valid Email ID : " + strEmail_ID[i]);
                                    txtEmailID.Select();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if(IsValidEmailAddress(txtEmailID.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("Please enter a valid Email ID.");
                                txtEmailID.Select();
                                return false;
                            }
                        }
                    }



                    //---------------------------------------------
                    // VALIDATION FOR EMAIL ID TO BCC
                    //---------------------------------------------
                    if (txtEmailIDBcc.Text.Trim() != "")
                    {
                        if (txtEmailID.Text.Trim().Contains(";") == true)
                        {
                            cmnService.J_UserMessage("Please seperate multiple Email ID with ','");
                            txtEmailID.Select();
                            return false;
                        }
                        //-----------------------------------------------------
                        if (txtEmailIDBcc.Text.Trim().Contains(",") == true)
                        {
                            string[] strEmail_ID = txtEmailIDBcc.Text.Trim().Split(',');

                            for (int i = 0; i < strEmail_ID.Length; i++)
                            {
                                if (IsValidEmailAddress(strEmail_ID[i]) == false)
                                {
                                    cmnService.J_UserMessage("Please enter a valid Email ID : " + strEmail_ID[i]);
                                    txtEmailIDBcc.Select();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (IsValidEmailAddress(txtEmailIDBcc.Text.Trim()) == false)
                            {
                                cmnService.J_UserMessage("Please enter a valid Email ID.");
                                txtEmailIDBcc.Select();
                                return false;
                            }
                        }
                    }

                    //---------------------------------------------
                    // VALIDATION FOR EMAIL FORMAT
                    //---------------------------------------------
                    if (cmbEmailCategoryDesc.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Please enter a valid Email Format.");
                        cmbEmailCategoryDesc.Select();
                        return false;
                    }
                    //---------------------------------------------

                    if (strEmailSubject.Trim() == "")
                    {
                        if (cmnService.J_UserMessage("Do you want to send Email Without Subject.(y/n)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            cmbEmailCategoryDesc.Select();
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

        #region GetDateDifference(string Date1, string Date2)
        public string GetDateDifference(string Date1, string Date2)
        {
            string strDaysWithText = "";
            try
            {
                int intDaysDifference = 0;

                Date1 = Convert.ToString(dtService.J_ConvertToIntYYYYMMDD(Date1));
                Date1 = Convert.ToString(Date1.Substring(0, 4) + "-" + Date1.Substring(4, 2) + "-" + Date1.Substring(6,2));


                Date2 = Convert.ToString(dtService.J_ConvertToIntYYYYMMDD(Date2));
                Date2 = Convert.ToString(Date2.Substring(0, 4) + "-" + Date2.Substring(4, 2) + "-" + Date2.Substring(6, 2));

                strSQL = "SELECT DATEDIFF(DAY,'" + Date1 + "','" + Date2 + "') AS DiffDate";

                intDaysDifference = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

                if (Convert.ToInt32(intDaysDifference) >= 0)
                {
                    strDaysWithText = Convert.ToString(Math.Abs(intDaysDifference) + " Days Later.");
                    return strDaysWithText;
                }
                else if (Convert.ToInt32(intDaysDifference) < 0)
                {
                    strDaysWithText = Convert.ToString(Math.Abs(intDaysDifference) + " Days Earlier.");
                    return strDaysWithText;
                }
                return strDaysWithText;
            }
            catch (Exception err)
            {
                return strDaysWithText;
            }
        }
        #endregion 

        #region Convert_TextToHtml
        public string Convert_TextToHtml(string Source,bool allow)
        {
            //Create a StringBuilder object from the string input
            //parameter
            StringBuilder sb = new StringBuilder(Source);
            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace("  ", " &nbsp;");
            //Check if HTML tags are not allowed
            if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            }
            //Create a StringReader from the processed string of
            //the StringBuilder
            StringReader sr = new StringReader(sb.ToString());
            StringWriter sw = new StringWriter();
            //Loop while next character exists
            while (sr.Peek() > -1)
            {
                //Read a line from the string and store it to a temp
                //variable
                string temp = sr.ReadLine();
                //write the string with the HTML break tag
                //Note here write method writes to a Internal StringBuilder
                //object created automatically
                sw.Write(temp + "<br>");
            }
            //Return the final processed text
            return sw.GetStringBuilder().ToString();
        }
        #endregion 

        #region Convert_HtmlToText
        private string Convert_HtmlToText(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                MessageBox.Show("Error");
                return source;
            }
        }
        #endregion 

        #region InitializeWebBrowser
        private void InitializeWebBrowser(string PdfPath)
        {
            GC.Collect();
            //WebBrowser wbsTenant = new WebBrowser();
            //wbsTenant.AllowNavigation = true;
            //wbsTenant.Location = new System.Drawing.Point(610, 110);
            //wbsTenant.Name = "wbsTenant";
            //wbsTenant.Size = new System.Drawing.Size(393, 206);
            //wbsTenant.TabIndex = 0;
            //Controls.Add(wbsTenant);
            //wbsTenant.Navigate(new Uri(PdfPath));
            //wbsTenant.BringToFront();
            //wbsTenant.Show();

            wbsViewPdf.Refresh(WebBrowserRefreshOption.Completely);
            wbsViewPdf.Navigate(PdfPath);
            wbsViewPdf.Navigate(new Uri(PdfPath));
            wbsViewPdf.Url = new System.Uri(PdfPath);
            this.wbsViewPdf.Url = new System.Uri(PdfPath, System.UriKind.Absolute);

            txtPDFPath.Text = PdfPath;
        }
        #endregion

        #region GenerateInvoicePDF
        public bool GenerateInvoicePDF()
        {
            try
            {
                // report file object
                ReportClass rptcls;
                //---------------------------------------------
                crInvoice rptInvoice = new crInvoice();
                rptcls = (ReportClass)rptInvoice;
                //---------------------------------------------
                strSQL = "SELECT 1                                              AS INVOICE_TYPE_ID," +
                         "       '(Original - Buyer''s Copy)'                   AS INVOICE_TYPE," +
                         "       0                                              AS PRINT_TYPE," +
                         "       TRN_INVOICE_HEADER.INVOICE_HEADER_ID           AS INVOICE_HEADER_ID," +
                         "       MST_INVOICE_SERIES.INVOICE_SERIES_ID           AS INVOICE_SERIES_ID," +
                         "       MST_INVOICE_SERIES.HEADER_DISPLAY_TEXT         AS HEADER_DISPLAY_TEXT," +
                         "       MST_PARTY.PARTY_NAME                           AS PARTY_NAME," +
                         "       MST_PARTY.ADDRESS1                             AS ADDRESS1," +
                         "       MST_PARTY.ADDRESS2                             AS ADDRESS2," +
                         "       MST_PARTY.ADDRESS3                             AS ADDRESS3," +
                         "       MST_PARTY.CITY                                 AS CITY," +
                         "       MST_PARTY.PIN                                  AS PIN," +
                         "       MST_PARTY.MOBILE_NO                            AS MOBILE_NO," +
                         "       MST_PARTY.PHONE_NO                             AS PHONE_NO," +
                         "       MST_PARTY.FAX                                  AS FAX," +
                         "       MST_PARTY.CONTACT_PERSON                       AS CONTACT_PERSON," +
                         "       TRN_INVOICE_HEADER.INVOICE_NO                  AS INVOICE_NO," +
                         "       TRN_INVOICE_HEADER.INVOICE_DATE                AS INVOICE_DATE," +
                         "       TRN_INVOICE_HEADER.CHALLAN_REF_NO              AS CHALLAN_REF_NO," +
                         "       TRN_INVOICE_HEADER.ORDER_NO                    AS ORDER_NO," +
                         "       TRN_INVOICE_HEADER.TOTAL_AMOUNT                AS TOTAL_AMOUNT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_TEXT               AS DISCOUNT_TEXT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_RATE               AS DISCOUNT_RATE," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_AMOUNT             AS DISCOUNT_AMOUNT," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT        AS AMOUNT_WITH_DISCOUNT," +
                         "       TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT            AS TAX_TOTAL_AMOUNT," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_TAX             AS AMOUNT_WITH_TAX," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST_TEXT        AS ADDITIONAL_COST_TEXT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST             AS ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.AMOUNT_WITH_ADDITIONAL_COST AS AMOUNT_WITH_ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.ROUNDED_OFF                 AS ROUNDED_OFF," +
                         "       TRN_INVOICE_HEADER.NET_AMOUNT                  AS NET_AMOUNT," +
                         "       TRN_INVOICE_HEADER.NET_AMOUNT_INWORDS          AS NET_AMOUNT_INWORDS," +
                         "       MST_ITEM.ITEM_ID                               AS ITEM_ID," +
                         "       MST_ITEM.ITEM_NAME                             AS ITEM_NAME," +
                         "       TRN_INVOICE_DETAIL.QUANTITY                    AS QUANTITY," +
                         "       MST_ITEM.UNIT                                  AS UNIT," +
                         "       TRN_INVOICE_DETAIL.RATE                        AS RATE," +
                         "       TRN_INVOICE_DETAIL.AMOUNT                      AS AMOUNT," +
                         "       TRN_INVOICE_DETAIL.REMARKS                     AS REMARKS," +
                         "       MST_COMPANY.COMPANY_ID                         AS COMPANY_ID," +
                         "       MST_COMPANY.COMPANY_NAME                       AS COMPANY_NAME," +
                         "       MST_COMPANY.ADDRESS1                           AS COMPANY_ADDRESS1," +
                         "       MST_COMPANY.ADDRESS2                           AS COMPANY_ADDRESS2," +
                         "       MST_COMPANY.ADDRESS3                           AS COMPANY_ADDRESS3," +
                         "       MST_COMPANY.PIN                                AS COMPANY_PIN," +
                         "       MST_COMPANY.CONTACT_NO                         AS COMPANY_CONTACT_NO," +
                         "       MST_COMPANY.FAX                                AS COMPANY_FAX," +
                         "       MST_COMPANY.EMAIL_ID                           AS COMPANY_EMAIL_ID," +
                         "       MST_COMPANY.WEB_SITE                           AS COMPANY_WEB_SITE," +
                         "       MST_COMPANY.VAT_NO                             AS VAT_NO," +
                         "       MST_COMPANY.CST_NO                             AS CST_NO," +
                         "       MST_COMPANY.SERVICE_TAX_NO                     AS SERVICE_TAX_NO," +
                         "       MST_COMPANY.PAN                                AS PAN, " +
                         "       MST_COMPANY.CIN_NO                             AS CIN_NO " +
                         "FROM   TRN_INVOICE_HEADER," +
                         "       TRN_INVOICE_DETAIL," +
                         "       MST_INVOICE_SERIES," +
                         "       MST_COMPANY," +
                         "       MST_PARTY," +
                         "       MST_ITEM " +
                         "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID  = TRN_INVOICE_DETAIL.INVOICE_HEADER_ID " +
                         "AND    TRN_INVOICE_HEADER.INVOICE_SERIES_ID  = MST_INVOICE_SERIES.INVOICE_SERIES_ID " +
                         "AND    TRN_INVOICE_HEADER.COMPANY_ID         = MST_COMPANY.COMPANY_ID " +
                         "AND    TRN_INVOICE_HEADER.PARTY_ID           = MST_PARTY.PARTY_ID " +
                         "AND    TRN_INVOICE_DETAIL.ITEM_ID            = MST_ITEM.ITEM_ID " +
                         "AND    TRN_INVOICE_HEADER.INVOICE_HEADER_ID IN(" + lngSearchId + ") " +
                         "AND    TRN_INVOICE_HEADER.BRANCH_ID          = " + J_Var.J_pBranchId + " " +
                         "ORDER BY INVOICE_TYPE_ID," +
                         "         TRN_INVOICE_HEADER.INVOICE_NO";

                // SUB REPORTS
                // FOR SUMMARY OF TAX DETAILS
                string strSubRptTaxDetails = " SELECT MST_TAX.TAX_ID                    AS TAX_ID," +
                                             "        MST_TAX.TAX_DESC                  AS TAX_DESC," +
                                             "        TRN_INVOICE_TAX.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                                             "        TRN_INVOICE_TAX.TAX_RATE          AS TAX_RATE," +
                                             "        TRN_INVOICE_TAX.TAX_AMOUNT        AS TAX_AMOUNT " +
                                             " FROM   MST_TAX," +
                                             "        TRN_INVOICE_TAX " +
                                             " WHERE  MST_TAX.TAX_ID    = TRN_INVOICE_TAX.TAX_ID " +
                                             " AND    MST_TAX.BRANCH_ID = " + J_Var.J_pBranchId + " " +
                                             " ORDER BY TRN_INVOICE_TAX.INVOICE_TAX_ID";
                // POPULATE & DISPLAY SUB REPORT
                rptcls.OpenSubreport("crSubRptTaxSummary").SetDataSource(dmlService.J_ExecSqlReturnDataSet(strSubRptTaxDetails).Tables[0]);

                // report is executed
                DataSet ds = dmlService.J_ExecSqlReturnDataSet(strSQL);
                if (ds == null) return false;

                PictureObject objBlobFieldObject;
                objBlobFieldObject = (PictureObject)rptcls.ReportDefinition.Sections[2].ReportObjects["imgSignature"];
                objBlobFieldObject.ObjectFormat.EnableSuppress = true;

                objBlobFieldObject.ObjectFormat.EnableSuppress = false;

                //GENERATE PDF FILE 
                if (J_ExportReport(ref rptcls, strSQL,"","","",BS_ExportReportFormat.PortableDocFormat, Convert.ToString(lblInvoiceNo.Text.Replace("/","-").Trim()), Application.StartupPath + "\\" + strGenerateInvoiceFolderName) == false)
                    return false;

                
                return true;
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }

        }
        #endregion

        #region EXPORT TO PDF [OVERLOADED METHOD]

        #region J_ExportReport [1]
        public bool J_ExportReport(ref ReportClass reportClass, string SqlText,string CompanyName,string CompanyAddress,string ReportTitle,string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            this.dsDataPrint = new DataSet();
            this.dsDataPrint = dmlService.J_ExecSqlReturnDataSet(SqlText);
            return J_ExportReport(ref reportClass, dsDataPrint,CompanyName,CompanyAddress,ReportTitle, ExportReportFormat, ExportFileName, ExportFilePath);
        }
        #endregion

        #region J_ExportReport [2]
        public bool J_ExportReport(ref ReportClass reportClass, DataSet dataset,string CompanyName,string CompanyAddress,string ReportTitle, string ExportReportFormat, string ExportFileName, string ExportFilePath)
        {
            string strFilePath = ExportFilePath + "\\";
            string strFileWithPath = "";
            if (dataset == null) return false;


            if (dataset.Tables[0].Rows.Count > 0)
            {
                // set the data source
                reportClass.SetDataSource(dataset.Tables[0]);

                RptPreview frm = new RptPreview();
                frm.CRViewer.ReportSource = reportClass;

                // check company name exist
                if (CompanyName == "" || CompanyName == null)
                    CompanyName = "";
                reportClass.SetParameterValue("txtCompanyName", CompanyName);

                // check company address exist
                if (CompanyAddress == "" || CompanyAddress == null)
                    CompanyAddress = "";
                reportClass.SetParameterValue("txtBranch", CompanyAddress);

                // check report title exist
                if (ReportTitle == "" || ReportTitle == null)
                    ReportTitle = "";
                reportClass.SetParameterValue("txtReportTitle", ReportTitle);

                frm.Text = "Report : " + ReportTitle;

                strFileWithPath = Path.Combine(strFilePath ,ExportFileName) + "." + BS_ExportReportFormat.PortableDocFormat;
                reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                switch (ExportReportFormat)
                {
                    case BS_ExportReportFormat.PortableDocFormat:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.PortableDocFormat;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strFileWithPath);

                        break;
                    case BS_ExportReportFormat.Excel:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Excel;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strFileWithPath);
                        break;
                    case BS_ExportReportFormat.Word:
                        strFileWithPath = strFilePath + ExportFileName + "." + BS_ExportReportFormat.Word;
                        reportClass.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strFileWithPath);
                        break;
                }

                //if (enmPrintType == J_PrintType.Direct)
                //    frm.CRViewer.PrintReport();
                //else if (enmPrintType == J_PrintType.Preview)
                //{
                //    frm.CRViewer.Refresh();
                //    frm.MdiParent = ReportDialog.MdiParent;
                //    frm.Show();
                //}
                return true;
            }
            else
            {
                //commonService.J_UserMessage("Record not found.\nPreview not available");
                cmnService.J_UserMessage("Please make sure you entered a valid file path", MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion 

        #endregion

        #region ConvertTextTOHtml(string)
        public string ConvertTextTOHtml(string Source,bool allow)
        {

            //Create a StringBuilder object from the string intput
            //parameter
            StringBuilder sb = new StringBuilder(Source);

            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace(" ", " &nbsp;");


            //Check if HTML tags are not allowed
            if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            } 


            //Create a StringReader from the processed string of 
            //the StringBuilder
            StringReader sr = new StringReader(sb.ToString());
            StringWriter sw = new StringWriter();

            //Loop while next character exists
            while(sr.Peek()>-1)
            {
                //Read a line from the string and store it to a temp
                //variable
                string temp = sr.ReadLine();
                //write the string with the HTML break tag
                //Note here write method writes to a Internal StringBuilder
                //object created automatically
                sw.Write(temp+"<br>") ;
            } 
            //Return the final processed text
            return sw.GetStringBuilder().ToString();

        }
        #endregion

        #region SendEMail
        private bool SendEMail(string EmailIdTo, string EmaiIdBcc, string ProductWiseEmailFrom, string ProductWebsite, string ProductDisplayName, string ProductImageUrl, string PartyContactPerson)
        {
            if (BillingSystem.T_CheckInternetConnectivty() == false) return false;
            //
            //----------------------------------
            //ENCODING THE TEXT TO HTML FORMAT
            //----------------------------------
            strHTMLText = ConvertTextTOHtml(rtxtEmailBody.Text, true);

            string strToEmail = ""; 
            string strWebSite = ""; 
            string strEmail = "";
            string strDisplayName = ""; 
            string strImageURL = ""; 
            string strContactPerson = "";
            //------------------------------------------------------------------
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential
                     (Convert.ToString(J_Var.J_pNetworkCredential_Username), Convert.ToString(J_Var.J_pNetworkCredential_Password));
            SmtpServer.Port = Convert.ToInt32(J_Var.J_pNetworkCredential_Port);
            SmtpServer.Host = J_Var.J_pNetworkCredential_Host;

            //SmtpServer.Credentials = new System.Net.NetworkCredential("mail@classroomseries.com", "tdsman@#123");
            //SmtpServer.Port = 25;
            //SmtpServer.Host = "mail.classroomseries.com";
            //------------------------------------------------------------------
            MailMessage mail = new MailMessage();
            //--
            try
            {

                //--------------------------------------------------------------------
                if (EmailIdTo.EndsWith(",") == true)
                    EmailIdTo = EmailIdTo.Substring(0, EmailIdTo.Length - 1);
                //--------------------------------------------------------------------
                if (EmaiIdBcc.EndsWith(",") == true)
                    EmaiIdBcc = EmaiIdBcc.Substring(0, EmaiIdBcc.Length - 1);
                //--------------------------------------------------------------------
                if (EmailIdTo.Trim() == "") return false;
                //--------------------------------------------------------------------
                if (cmnService.J_UserMessage("Do you want to send email to [" + EmailIdTo + "] ??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return false;
                //
                this.Cursor = Cursors.WaitCursor;
                //

                strEmail = ProductWiseEmailFrom;
                strWebSite = ProductWebsite;
                strDisplayName = ProductDisplayName;
                strImageURL = ProductImageUrl;
                //
                if (PartyContactPerson.Trim() == "")
                    strContactPerson = "Sir";
                else
                    strContactPerson = BillingSystem.ToTitleCase(PartyContactPerson.Trim());
                //
                mail.From = new MailAddress(strEmail, strDisplayName, System.Text.Encoding.UTF8);
                mail.Priority = MailPriority.High;
                mail.ReplyTo = new MailAddress(strEmail);
                //
                if (EmaiIdBcc.Trim() != "")
                {
                    //mail.Bcc.Add("mukherjee.dhrub@gmail.com");
                    mail.Bcc.Add(EmaiIdBcc);
                }
                //
                mail.To.Add(EmailIdTo);

                FileStream fs = new FileStream(txtPDFPath.Text , FileMode.Open, FileAccess.Read);
                Attachment a = new Attachment(fs,lblInvoiceNo.Text.Replace("/","-").Trim() + ".Pdf", MediaTypeNames.Application.Octet);
                mail.Attachments.Add(a);
                
                mail.Subject = strEmailSubject;
                StringBuilder htmlString = new StringBuilder();
                //
                #region EMAIL BODY
                htmlString.Append(strHTMLText);
                #endregion
                //
                mail.Body = htmlString.ToString();
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpServer.Send(mail);
                //
                return true;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.ToString());
                this.Cursor = Cursors.Default;
                return false;
            }
        }
        #endregion

        #region FetchItemWiseEmailDetails
        public bool FetchItemWiseEmailDetails(long SearchId)
        {
            IDataReader reader = null;
            try
            {
                strSQL = @" SELECT MST_EMAIL_DETAIL.EMAIL         AS EMAIL,
                                   MST_EMAIL_DETAIL.WEBSITE       AS WEBSITE,
                                   MST_EMAIL_DETAIL.DISPLAYNAME   AS DISPLAYNAME,
                                   MST_EMAIL_DETAIL.IMAGEURL      AS IMAGEURL,
                                   MST_EMAIL_DETAIL.EMAIL_TO_BCC  AS EMAIL_TO_BCC
                            FROM   TRN_INVOICE_DETAIL
                                   LEFT JOIN MST_ITEM
                                   ON TRN_INVOICE_DETAIL.ITEM_ID = MST_ITEM.ITEM_ID
                                   LEFT JOIN MST_EMAIL_DETAIL
                                   ON MST_ITEM.EMAIL_DETAIL_ID = MST_EMAIL_DETAIL.EMAIL_DETAIL_ID
                            WHERE  TRN_INVOICE_DETAIL.INVOICE_HEADER_ID = " + SearchId + " ";

                    reader = dmlService.J_ExecSqlReturnReader(strSQL);
                    if (reader == null) return false;

                    while (reader.Read())
                    {

                        strEmail = Convert.ToString(reader["EMAIL"]);
                        strWebsite = Convert.ToString(reader["WEBSITE"]);
                        strDisplayName = Convert.ToString(reader["DISPLAYNAME"]);
                        strImageUrl = Convert.ToString(reader["IMAGEURL"]);
                        txtEmailIDBcc.Text = Convert.ToString(reader["EMAIL_TO_BCC"]);

                        reader.Close();
                        reader.Dispose();
                        return true;
                    }
                    reader.Close();
                    reader.Dispose();
                    return false;
               
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                reader.Close();
                reader.Dispose();
                return false;
            }
        }
        #endregion 

        #region IsValidEmailAddress
        public bool IsValidEmailAddress(string EmailIdToCheck)
        {
            try
            {
                MailAddress m = new MailAddress(EmailIdToCheck);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion




        #endregion

    }
}

