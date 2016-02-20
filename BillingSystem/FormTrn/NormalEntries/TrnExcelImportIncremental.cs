
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnFVUImport
Version			: 1.0
Start Date		: 30-12-2010
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

    //~~~~ System Namespaces ~~~~
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data;
    using System.IO;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;

    //using Microsoft.Office.Interop.Access;

    //using System.Runtime.InteropServices;
    using System.Data.OleDb;
    //~~~~ User Namespaces ~~~~
    //using BillingSystem.FormTrn;
    using BillingSystem.FormRpt;
    using BillingSystem.FormTrn.NormalEntries;
    using BillingSystem.Classes;
    //--
    using Excel = Microsoft.Office.Interop.Excel;
    //~~~~ This namespace are using for using VB6 component
    using Microsoft.VisualBasic.Compatibility.VB6;

#endregion

namespace BillingSystem.FormTrn.NormalEntries
{
    #region STRUCTURE

    #region T_Error_Type
    public struct T_Error_Type
    {
        public const string BLANK_NULL_CHECK = "Blank/NULL value";
        public const string MANADATORY_CHECK = "Manadatory value";
        public const string NUMERIC_CHECK    = "should be Numeric";
        public const string DUPLICATE_CHECK  = "Duplicate Value";
        public const string LENGTH_CHECK     = "Length Check";
        public const string FORMAT_CHECK     = "Format Check";
        public const string MISMATCH_CHECK   = "Data not matched";
        public const string SEQUENCE_CHECK   = "Not in Sequence";
        public const string VALIDITY_CHECK   = "Invalid value";
        public const string ALREADY_EXISTS_CHECK = "Already Exists";
        //
        public const string MISC = "MISC";
    }
    #endregion

    #region T_Error_Type_Color
    public struct T_Error_Type_Color
    {
        public const string BLANK_NULL_CHECK = "Red";
        public const string MANADATORY_CHECK = "Salmon";
        public const string NUMERIC_CHECK = "SpringGreen";
        public const string DUPLICATE_CHECK = "SteelBlue";
        public const string LENGTH_CHECK = "Tan";
        public const string FORMAT_CHECK = "Tomato";
        public const string MISMATCH_CHECK = "Yellow";
        public const string SEQUENCE_CHECK = "Beige";
        public const string VALIDITY_CHECK = "Chocolate";
        public const string ALREADY_EXISTS_CHECK = "AntiqueWhite";
        //
        public const string MISC = "Fuchsia";
    }
    #endregion

    #region T_Sheet_Name
    public struct T_Sheet_Name
    {
        public const string OFFLINE_SERIAL_DETAILS = "Offline Serial Code";
        public const string VALIDATION_ERROR_DETAILS = "Validation Error Details";
    }
    #endregion

    #region T_SaveTag
    public struct T_SaveTag
    {
        public const string IMPORT = "IMPORT";
        public const string VALIDATE = "VALIDATE";
    }
    #endregion

    #endregion      

    public partial class TrnExcelImportIncremental : BillingSystem.FormGen.GenForm
    {

        #region System Generated Code
        public TrnExcelImportIncremental()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        ExcelService ExcelService = new ExcelService();
        BillingSystem.Classes.BS BillingSystem = new BillingSystem.Classes.BS();
        //-----------------------------------------------------------------------
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        //string strCheckFields;				//For Sotring the Where Values
        //-----------------------------------------------------------------------
        DataSet dsetGridClone = new DataSet();
        //-----------------------------------------------------------------------
        //string strTempMode;
        //-----------------------------------------------------------------------
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        //Microsoft.Office.Interop.Access.Application Access = new Microsoft.Office.Interop.Access.Application();
        //-----------------------------------------------------------------------
        int intCaratPosition = 0;
        string strSerialExcelPath = "";
        long lngBasicInfoID = 0;
        string strErrorWorksheetName = "Validation Error";
        //-----------------------------------------------------------------------
        string strNewDeducteeWorkSheetName = "New Offline Serial Found";
        long lngRowCount;
        long lngNewDeducteesCreated;
        //
        string strErrorMessage = "";

        string strTemporaryfileOfflineSerialPath = "";
        string strTemporaryValidateExcelfilePath = "";

        OleDbDataAdapter myCommand;
        string strConnectionString = "";
        OleDbConnection con;
        //
        bool blnOpenTabPage = false;

        #region Variable Declaration for Create New Excel Sheet 

        string strExcelFolder = "";
        string ExcelFileName = "";

        string strSourcePath = Path.Combine(Application.StartupPath, "BLANK EXCEL FILE FORMAT");

        string strSourceFile = "";
        string strDestFile = "";
        //--
        string strOfflineSerial = "Offline Serial Code";

        bool blnUpdateTextfileName = true;    

        #endregion 

        #endregion

        #region set ENUM

        #region T_OFFLINE_SERIAL_COLUMN

        public enum T_OFFLINE_SERIAL_COLUMN
        {
            OFFLINE_SERIAL_NO = 0,
            OFFLINE_SERIAL_CODE = 1,
            OFFLINE_CODE = 2,
            none
        }

        #endregion

        #endregion

        #region User Defined Events

        #region TrnExcelImport_Load

        private void TrnExcelImport_Load(object sender, EventArgs e)
        {
            GC.Collect();
            //
            //Added by Indrajit on 23-02-2013
            tmrLoginRefresh.Interval = (int)BS.T_pLockInterval * 60000;
            tmrLoginRefresh.Start();
            //-----------
            lblTitle.Text = "Excel Validate";
            //-----------
            cmbItem.Select();
            //-----------
            strSQL = " SELECT ITEM_ID," +
                "             ITEM_NAME " +
                "      FROM   MST_ITEM " +
                "      WHERE  ONLINE_FLAG = 1 " +
                "      ORDER BY ITEM_NAME";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbItem) == false) return;
            //-----------
            rbnIncremental.Checked = true;
            //--
            BtnSave.Tag = T_SaveTag.VALIDATE;
            //--
        }

        #endregion

        #region btnSelectExcelPath_Click
        private void btnSelectExcelPath_Click(object sender, EventArgs e)
        {
            strSerialExcelPath = cmnService.J_OpenFileDialog("Excel File | *.xls; *.xlsx", "Excel File | *.xls; *.xlsx", "Choose the Excel File to import");
            if (strSerialExcelPath != "")
                txtExcelPath.Text = strSerialExcelPath;            
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            GC.Collect();
            //
            if (BtnExit.Text == "Back")
            {
                dgcViewOfflineSerial.Visible = false;
                dgcViewOfflineSerial.Visible = true;
                //--
                //LoadChallanGrid();
                BtnExit.Text = "Exit";
            }
            else
            {
                dmlService.Dispose();
                if (Directory.Exists(strTemporaryfileOfflineSerialPath) == true)
                    Directory.Delete(strTemporaryfileOfflineSerialPath);

                this.Close();
                this.Dispose();
            }
        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            ////@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            if (Convert.ToString(BtnSave.Tag) == T_SaveTag.VALIDATE)
            {
                #region VALIDATE
                //--
                if (ValidateFields() == false) return;
                //--
                //-- %%%%%%%%%%%%%%%%%%%%%%%
                if (cmnService.J_UserMessage("Proceed Excel Import??", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                //-- %%%%%%%%%%%%%%%%%%%%%%%
                //--
                this.Cursor = Cursors.WaitCursor;

                string strConnectionString = "";

                //MAKING CONNECTION TO THE EXCEL FILE
                if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xls")
                    strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                else if (Path.GetExtension(txtExcelPath.Text.Trim().ToLower()) == ".xlsx")
                    strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtExcelPath.Text + ";Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1\"";

                con = new OleDbConnection(strConnectionString);
                //                
                con.Open();

                //--
                if (CheckExcelStructure(txtExcelPath.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    cmnService.J_UserMessage("Selected Excel file is invalid.\n" +
                                             "Please get the latest Excel file from : Import from Excel > Create Blank Excel sheet", MessageBoxIcon.Exclamation);
                    //--
                    prgBar.Value = 0;
                    btnSelectExcelPath.Select();
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Process Started";
                //--
                //if (dmlService.J_IsDatabaseObjectExist("TEMP_ERR_VALIDATION") == true)
                //{
                //
                //lblProgressDisplayMessage.Visible = true;
                //lblProgressDisplayMessage.Text = "EXCEL file initialized";
                //
                //if (INITIALIZE_COLOR_ERROR_CELLS(txtExcelPath.Text, cmbFormNo.Text ) == false)
                //{
                //    cmnService.J_UserMessage("Initialize Coloring Error Cells failed");
                //    this.Cursor = Cursors.Default;
                //    prgBar.Value = 0;
                //    return;
                //}
                //}
                //
                //prgBar.Value = prgBar.Value + 5;
                //this.Refresh();
                // CREATE TEMP TABLES        
                if (CREATE_TEMP_TABLES() == false)
                {
                    cmnService.J_UserMessage("Temporary Tables Not created", MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                // GET DATA FROM EXCEL ACCESS       
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Transferring Data";
                //
                //start do work
                if (GET_DATA_FROM_EXCEL_SQLSVR() == false)
                {
                    cmnService.J_UserMessage(strErrorMessage);
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    con.Close();
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                // VALIDATE DATA     
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Validation Started";
                //
                if (VALIDATE_DATA() == false)
                {
                    cmnService.J_UserMessage("Data Validation failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    con.Close();
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //--
                //if (DELETE_WORKSHEET(txtExcelPath.Text, strErrorWorksheetName) == false)
                //{
                //    cmnService.J_UserMessage("Error Sheet Deletion failed", MessageBoxIcon.Error);
                //    this.Cursor = Cursors.Default;
                //    prgBar.Value = 0;
                //    con.Close();
                //    return;
                //}
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "New Worksheet created";
                //
                //--
                //if (CREATE_NEW_WORKSHEET(txtExcelPath.Text) == false)
                //{
                //    cmnService.J_UserMessage("Error Sheet Creation failed", MessageBoxIcon.Error);
                //    this.Cursor = Cursors.Default;
                //    prgBar.Value = 0;
                //    return;
                //}
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //--
                //
                //lblProgressDisplayMessage.Visible = true;
                //lblProgressDisplayMessage.Text = "Error sheet writing started";
                //
                if (WRITE_ERROR_WORKSHEET(txtExcelPath.Text) == false)
                {
                    cmnService.J_UserMessage("Writing Error Sheet failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    con.Close();
                    return;
                }
                prgBar.Value = prgBar.Value + 5;
                this.Refresh();
                //--
                if (chkColorCodingExcelsheet.Checked == true)
                {
                    //
                    lblProgressDisplayMessage.Visible = true;
                    lblProgressDisplayMessage.Text = "EXCEL file coloring";
                    //
                    if (COLOR_ERROR_CELLS(strTemporaryValidateExcelfilePath) == false)
                    {
                        cmnService.J_UserMessage("Coloring Error Cells failed", MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        prgBar.Value = 0;
                        con.Close();
                        return;
                    }
                }
                //--------

                //--------------------------
                //-- DELETE ALL .tmp FILES
                //--------------------------
                //foreach (string sFile in System.IO.Directory.GetFiles(Path.GetDirectoryName(txtExcelPath.Text)))
                //{
                //    if(cmnService.J_IsProcessOpen(Path.Combine(Path.GetDirectoryName(txtExcelPath.Text) , Convert.ToString(sFile))) == false)
                //        if (sFile.ToUpper().EndsWith(".TMP"))
                //            System.IO.File.Delete(sFile);
                //}
                Delete_TMP_Files(txtExcelPath.Text);
                lblProgressDisplayMessage.Visible = true;
                lblProgressDisplayMessage.Text = "Temporary files deleted";
                //
                //--
                lblProgressDisplayMessage.Visible = false;
                //
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TEMP_ERR_VALIDATION"))) > 0)
                {
                    for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                    {
                        prgBar.PerformStep();
                    }
                    this.Cursor = Cursors.Default;

                    con.Close();

                    //cmnService.J_UserMessage("Excel File Validation failed \n Check the <Validation Error> Sheet of the Excel file ", MessageBoxIcon.Exclamation);
                    if (cmnService.J_UserMessage("Excel File Validation failed \n Do you want to open the Excel file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        if (File.Exists(txtExcelPath.Text) == true)
                        {
                            System.Diagnostics.Process.Start(txtExcelPath.Text);
                            System.Diagnostics.Process.Start(strTemporaryValidateExcelfilePath);
                            this.Cursor = Cursors.Default;
                        }
                    }
                    prgBar.Value = 0;
                    con.Close();
                    return;
                }
                //--
                for (int i = prgBar.Minimum; i <= prgBar.Maximum; i++)
                {
                    prgBar.PerformStep();
                }
                this.Cursor = Cursors.Default;

                con.Close();
                cmnService.J_UserMessage("Excel File Validation is completed \n     Proceed to Import ", MessageBoxIcon.Information);
                //--
                if (LOAD_IMPORT_INTERFACE() == false)
                {
                    cmnService.J_UserMessage("Loading Import Interface failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    prgBar.Value = 0;
                    return;
                }
                this.Cursor = Cursors.Default;
                //LoadChallanGrid();
                //--
                BtnSave.Text = "&Import Data";
                BtnSave.Tag = T_SaveTag.IMPORT;
                lblTitle.Text = "Excel Import";
                //--
                #endregion
            }
            else if (Convert.ToString(BtnSave.Tag) == T_SaveTag.IMPORT)
            {
                //#region IMPORT
                //--
                string strMessage = "";
                if (rbnIncremental.Checked == true)
                    strMessage = "Incremental import";
                else if (rbnNewImport.Checked == true)
                    strMessage = "All existing Offline Serial Code data will be deleted";
                //--
                if (cmnService.J_UserMessage(strMessage + "\nbased on " +
                    "ITEM \t: " + cmbItem.Text + " \n" +
                    "Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                //--
                prgImportBar.Value = 0;
                //--
                this.Cursor = Cursors.WaitCursor;
                //--                
                prgImportBar.Value = prgImportBar.Value + 5;
                this.Refresh();
                //--
                if (rbnNewImport.Checked == true)
                {
                    if (BillingSystem.T_DeleteOfflineSerialDetails(cmnService.J_GetComboBoxItemId(ref cmbItem,cmbItem.SelectedIndex)) == false)
                    {
                        cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //
                    prgImportBar.Value = prgImportBar.Value + 10;
                    this.Refresh();
                }
                //--

                //--
                prgImportBar.Value = prgImportBar.Value + 5;
                this.Refresh();
                     
                if (INSERT_MASTER_DATA() == false)
                {
                    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
                this.Refresh();
                //--
                if (DROP_TEMP_TABLES() == false)
                {
                    cmnService.J_UserMessage("Import failed", MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
                this.Refresh();

                for (int i = prgImportBar.Minimum; i <= prgImportBar.Maximum; i++)
                {
                    prgImportBar.PerformStep();
                }
                //
                BtnSave.Enabled = false;
                //
                this.Cursor = Cursors.Default;
                //--
                BtnExit.Text = "E&xit";

                cmnService.J_UserMessage("Import from Excel File completed", MessageBoxIcon.Information);
                prgImportBar.Value = 0;
                prgBar.Value = 0;
                //--
                //BS.T_pTabFormCaption = lblFormNo.Text;
                //
                this.Close();
                this.Dispose();
                //--
                ////Do you want to check the data imported?
                //if (cmnService.J_UserMessage("Do you want to go to Form " + TDSMAN.Classes.TDSMAN.T_pTabFormCaption + " to view the data imported??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    //TDSMAN.Classes.TDSMAN.T_pTabFormCaption = lblFormNo.Text;
                //    //
                //    TdsMan.CloseChildForm(new Mst(), J_Var.frmMain);
                //    //
                //    cmnService.J_ShowChildForm(new TrnRegularReturn(), J_Var.frmMain, "Form " + lblFormNo.Text);
                //}
                ////--
                //
                //#endregion
            }
            //------------------------
        }
        #endregion

        #region dgcViewOfflineSerial_DoubleClick

        private void dgcViewOfflineSerial_DoubleClick(object sender, EventArgs e)
        {
            //if (dgcViewChallan.CurrentRowIndex >= 0)
            //{
            //    dgcViewChallan.Visible = false;
            //    BtnExit.Text = "Back";
            //    //--
            //    dgcViewDeductee.Visible = true;
            //    //--------------------------------------------------
            //    //A particular ID wise retriving the data from database
            //    if (LoadDeducteeDetailsGrid(Convert.ToInt64(Convert.ToString(dgcViewChallan[dgcViewChallan.CurrentRowIndex, 0]))) == false)
            //    {
            //        return;
            //    }
            //}
            //lblToolTip.Visible = false;
        }

        #endregion

        #region dgcViewOfflineSerial_KeyDown

        private void dgcViewOfflineSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) dgcViewOfflineSerial_DoubleClick(sender, e);
        }

        #endregion

        #region dgcViewOfflineSerial_MouseClick

        private void dgcViewOfflineSerial_MouseClick(object sender, MouseEventArgs e)
        {
            if(dgcViewOfflineSerial.Visible == false) return;
            //
            dgcViewOfflineSerial.Select(dgcViewOfflineSerial.CurrentRowIndex);
            dgcViewOfflineSerial.Select();
            dgcViewOfflineSerial.Focus();
        }

        #endregion

        #region ddgcViewOfflineSerial_MouseMove
        private void dgcViewOfflineSerial_MouseMove(object sender, MouseEventArgs e)
        {
            
            //var cell = dataGridView1.CurrentCell;
            //var cellDisplayRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            //toolTip1.Show(string.Format("this is cell {0},{1}", e.ColumnIndex, e.RowIndex),
            //              dataGridView1,
            //              cellDisplayRect.X + cell.Size.Width / 2,
            //              cellDisplayRect.Y + cell.Size.Height / 2,
            //              2000);
            //dataGridView1.ShowCellToolTips = false;
        }
        #endregion        
        
        #region tmrLoginRefresh_Tick
        private void tmrLoginRefresh_Tick(object sender, EventArgs e)
        {
            //if (TDSMAN.Classes.TDSMAN.T_pBasicInfoId > 0)
            //{
            //    strSQL = "UPDATE TEMP_STACK_BASIC_INFO " +
            //             "SET LAST_UPDATED_TIME   = " + TdsMan.GetServerDateTime() + " " +
            //             "WHERE BASIC_INFO_ID     = " + TDSMAN.Classes.TDSMAN.T_pBasicInfoId + " " +
            //             "AND   PRINT_USER_SERIAL = '" + TDSMAN.Classes.TDSMAN.T_pProductSerial + "'";
            //    dmlService.J_ExecSql(dmlService.J_pCommand, strSQL);
            //}
        }
        #endregion

        #region tbcExcelImport_Selecting
        private void tbcExcelImport_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.Action == TabControlAction.Selecting)
            {
                if (blnOpenTabPage == false)
                {
                    e.Cancel = true;
                }
                else
                    blnOpenTabPage = false;
            }
        }
        #endregion

        #region CreateBlankExcelSheet 
        
        #region lblCreateBlankExcelSheet_Click
        private void lblCreateBlankExcelSheet_Click(object sender, EventArgs e)
        {
            try
            {
                //--
                if (cmnService.J_UserMessage("Get Excel file structure ??", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                //--
                string strExcelFolder = cmnService.J_OpenFolderDialog("Select Destination Folder");

                //---FILE NAME FOR EXCEL
                //
                string strExcelFileName = "Offline Serial Code" + DateTime.Now.ToString("HH_mm_ss") + ".XLS";
                //
                //ExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text + "." + cmbFileType.Text;

                ////--SOURCE FILE 
                //strSourceFile = Path.Combine(strSourcePath, ExcelFileName);

                //--DESTINATION FILE
                string strDestFile = Path.Combine(strExcelFolder, strExcelFileName);

                //if (cmnService.J_IsFileExist(strDestFile) == true)
                //{
                //    if (cmnService.J_UserMessage("File exists with same name.\nDo you want to replace the old file ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //        return;
                //}
               
               
                #region CREATING NEW EXCEL FILE

                int intColumn;
                int intRow;
                //--
                //if (cmnService.J_UserMessage("Proceed??", MessageBoxButtons.YesNo) == DialogResult.No)
                //    return;
                //--
                this.Cursor = Cursors.WaitCursor;
                //--
                
                //
                if (CREATE_EXCEL_FILE(strDestFile) == false) return;
                //--
                //--------------------------------------------------------------------------------------------------
                if (CREATE_NEW_WORKSHEET(strDestFile, "Offline Serial Code") == false) return;
                //
                intColumn = 64;
                intRow = 1;
                //
                if (WRITE_WORKSHEET(strDestFile,
                                          T_Sheet_Name.OFFLINE_SERIAL_DETAILS,
                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                          "Offline Serial No", true) == false) return;
                //
                if (WRITE_WORKSHEET(strDestFile,
                                          T_Sheet_Name.OFFLINE_SERIAL_DETAILS,
                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                          "Offline Serial Code", true) == false) return;
                //
                if (WRITE_WORKSHEET(strDestFile,
                                          T_Sheet_Name.OFFLINE_SERIAL_DETAILS,
                                          (Convert.ToChar(intColumn += 1) + Convert.ToString(intRow)),
                                          "Offline Code", true) == false) return;
                //--
                if (DELETE_WORKSHEET(strDestFile, "Sheet1") == false) return;
                if (DELETE_WORKSHEET(strDestFile, "Sheet2") == false) return;
                if (DELETE_WORKSHEET(strDestFile, "Sheet3") == false) return;
                //------------------------------------
                if (KILL_EXCEL() == false)
                    return;
                //--
                this.Cursor = Cursors.Default;
                //cmnService.J_UserMessage("Excel file created");

                #endregion
                //
                this.Refresh();
                //CHECK IF FILE IS CREATED OR NOT
                if (cmnService.J_IsFileExist(strDestFile) == true)
                {
                    //cmnService.J_UserMessage("Excel File Structure Created");
                    cmnService.J_UserMessage("The Excel Template has been created");
                    System.Diagnostics.Process.Start(strDestFile);
                }
                else
                    cmnService.J_UserMessage("The creation of Excel Template has failed");
                //
                return;
            }
            catch (Exception err)
            {

                cmnService.J_UserMessage("Excel file creation failed");
                //--
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #endregion 

        #endregion

        #region User Defined Functions

        #region ValidateFields
        private bool ValidateFields()
        {
            try
            {
                if (lblSearchMode.Text == J_Mode.Sorting)
                {
                    //if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //{
                    //    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //    if (dsetGridClone == null) return false;
                    //    dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //    return false;
                    //}
                    return true;
                }
                else if (lblSearchMode.Text == J_Mode.Searching)
                {
                    //if (grpSearch.Visible == false)
                    //{
                    //    if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.DataNotFound);
                    //        if (dsetGridClone == null) return false;
                    //        dmlService.J_setGridPosition(ref ViewGrid, dsetGridClone, "EMPLOYEE_ID", lngSearchId);
                    //        return false;
                    //    }
                    //}
                    //else if (grpSearch.Visible == true)
                    //{
                    //    if (txtPANSearch.Text.Trim() == "" &&
                    //        txtEmployeeNameSearch.Text.Trim() == "" &&
                    //        txtCompanyNameSearch.Text.Trim() == "")
                    //    {
                    //        cmnService.J_UserMessage(J_Msg.SearchingValues);
                    //        txtPANSearch.Select();
                    //        return false;
                    //    }
                    //}
                    //return true;
                }
                else
                {
                    //-----------------------------------------------------------------------
                    //-- ITEM
                    //-----------------------------------------------------------------------
                    if (cmbItem.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Item - Cannot be Blank");
                        cmbItem.Select();
                        return false;
                    }

                    // FILE SHOULD BE FVU
                    if (Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLS" && Path.GetExtension(txtExcelPath.Text).ToUpper() != ".XLSX")
                    {
                        cmnService.J_UserMessage("Selected file should be a Excel file");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    // FILE EXIST
                    if (cmnService.J_IsFileExist(txtExcelPath.Text) == false)
                    {
                        cmnService.J_UserMessage("Selected Excel file not found");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    // FILE OPEN
                    //-- ANIK 2011-09-09
                    string strPath = txtExcelPath.Text.ToString();
                    //if (cmnService.J_IsProcessOpen(txtExcelPath.Text) == true)
                    if (BillingSystem.T_isFileOpenOrReadOnly(ref strPath) == true)
                    {
                        cmnService.J_UserMessage("Selected Excel file is open");
                        btnSelectExcelPath.Select();
                        return false;
                    }
                    return true;
                }
                return true;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_TABLES
        private bool CREATE_TEMP_TABLES()
        {
            try
            {
                //Creating the Offline Serial Temp Table.
                if (dmlService.J_IsDatabaseObjectExist("TEMP_OFFLINE_SERIAL_DETAILS") == true)
                {
                    strSQL = "DROP TABLE TEMP_OFFLINE_SERIAL_DETAILS";
                    dmlService.J_ExecSql(strSQL);
                }

                if (dmlService.J_IsDatabaseObjectExist("TEMP_OFFLINE_SERIAL_DETAILS") == false)
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    strSQL = "CREATE TABLE TEMP_OFFLINE_SERIAL_DETAILS (" +
                             "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_ID", J_Identity.YES) + "," +
                             "                                " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.Integer, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("RUNNING_SERIAL_NO_CELL", J_ColumnType.String,10, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_CODE", J_ColumnType.String, 255, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_CODE_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("OFFLINE_CODE", J_ColumnType.String, 255, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("OFFLINE_CODE_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + ")";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    dmlService.J_ExecSql(strSQL);
                }

                BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS = "TEMP_OFFLINE_SERIAL_DETAILS";

                ////--
                ////Creating Temp Error master table.
                //if (dmlService.J_IsDatabaseObjectExist("TEMP_ERR_VALIDATION") == true)
                //{
                //    strSQL = "DROP TABLE TEMP_ERR_VALIDATION";
                //    dmlService.J_ExecSql(strSQL);
                //}

                //if (dmlService.J_IsDatabaseObjectExist("TEMP_ERR_VALIDATION") == false)
                //{
                //    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //    strSQL = "CREATE TABLE TEMP_ERR_VALIDATION (" +
                //             "                                " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 50, J_DefaultValue.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                //             "                                " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255, J_DefaultValue.YES) + ")";
                //    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //    dmlService.J_ExecSql(strSQL);
                //}
                ////
                
                

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CREATE_TEMP_ERR_TABLES
        private bool CREATE_TEMP_ERR_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("TEMP_ERR_VALIDATION") == true)
                {
                    strSQL = "DROP TABLE TEMP_ERR_VALIDATION";
                    dmlService.J_ExecSql(strSQL);
                }
                //
                if (dmlService.J_IsDatabaseObjectExist("TEMP_ERR_VALIDATION") == false)
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //MODIFIED BY DHRUB ON 15/05/2014 FOR COMMON SQL 
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //strSQL = "CREATE TABLE TEMP_ERR_VALIDATION (" +
                    //     "                  ERR_VALIDATION_ID COUNTER," +
                    //     "                  ERR_TYPE          TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_CELL          TEXT(10) DEFAULT \"\"," +
                    //     "                  ERR_COLUMN        TEXT(50) DEFAULT \"\"," +
                    //     "                  ERR_SHEET         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_COLOR         TEXT(25) DEFAULT \"\"," +
                    //     "                  ERR_DESC          TEXT(255) DEFAULT \"\"," +
                    //     "                  ERR_FORM_NO       TEXT(25) DEFAULT \"\")";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    strSQL = "CREATE TABLE TEMP_ERR_VALIDATION (" +
                             "                                " + cmnService.J_GetDataType("ERR_VALIDATION_ID", J_Identity.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_TYPE", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_COLUMN", J_ColumnType.String, 50, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_SHEET", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_COLOR", J_ColumnType.String, 25, J_DefaultValue.YES) + "," +
                             "                                " + cmnService.J_GetDataType("ERR_DESC", J_ColumnType.String, 255, J_DefaultValue.YES) + ")";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    dmlService.J_ExecSql(strSQL);
                    //-------------------------------------------------------
                    BS.T_tblTEMP_ERR_VALIDATION = "TEMP_ERR_VALIDATION";
                }
                //
                strSQL = "DELETE FROM TEMP_ERR_VALIDATION";
                dmlService.J_ExecSql(strSQL);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DROP_TEMP_TABLES
        private bool DROP_TEMP_TABLES()
        {
            try
            {
                //
                if (dmlService.J_IsDatabaseObjectExist("TEMP_OFFLINE_SERIAL_DETAILS") == true)
                {
                    strSQL = "DROP TABLE TEMP_OFFLINE_SERIAL_DETAILS";
                    dmlService.J_ExecSql(strSQL);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region GET_DATA_FROM_EXCEL_SQLSVR
        private bool GET_DATA_FROM_EXCEL_SQLSVR()
        {
            try
            {

                #region VARIABLE_DECLARATION
                // CHALLAN DETAILS
                int intLineNumberOfflineSerial = 0;
                string strRunningSerialNo = "";
                string strOfflineSerialCode = "";
                string strOfflineCode = "";
                //--
                // CREATE THE SUBFOLDER.
                string strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //

                strTemporaryfileOfflineSerialPath = Path.Combine(strStartupPath, "OfflineSerial.txt");

                string tableName = "";
                string textfileName = "";

                #endregion
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
                //// READ EXCEL FILE
                DataSet myDataSet;
                OleDbDataAdapter myCommand;

                //--
                #region INSERT OFFLINE SERIAL DETAILS 


                #region TRANSFERRING DATA FROM EXCEL TO TEXT FILE

                // INSERT CHALLAN DETAILS

                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                myDataSet = new DataSet();
                //
                myCommand = new OleDbDataAdapter("SELECT * FROM [Offline Serial Code$]", con);
                myCommand.Fill(myDataSet);
                StreamWriter StreamWriter = cmnService.J_ReturnStreamWriter(strTemporaryfileOfflineSerialPath);
                //Travers through each row in the dataset
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    lblProgressDisplayMessage.Visible = true;
                    //
                    //Stores info in Datarow into an array
                    Object[] cells = myDataRow.ItemArray;
                    //
                    intLineNumberOfflineSerial = intLineNumberOfflineSerial + 1;
                    int intColumnValue = 64;
                    //
                    int intRowIndex = 0;

                    strRunningSerialNo = Convert.ToString(cells[0]).ToUpper();
                    strOfflineSerialCode = Convert.ToString(cells[1]).ToUpper();
                    strOfflineCode = Convert.ToString(cells[2]).ToUpper();

                    // CHECK BLANK ROW TO EXIT
                    if (strRunningSerialNo == "" &&
                         strOfflineSerialCode == "" &&
                           strOfflineCode == "" )
                        break;

                    cmnService.J_WriteLine(ref StreamWriter, BillingSystem.T_WriteField(intLineNumberOfflineSerial.ToString()) +
                                                             BillingSystem.T_WriteField(strRunningSerialNo) + BillingSystem.T_WriteField((Convert.ToString((char)(intColumnValue + 1)) + (Convert.ToString(intLineNumberOfflineSerial + 1)))) +
                                                             BillingSystem.T_WriteField(strOfflineSerialCode) + BillingSystem.T_WriteField((Convert.ToString((char)(intColumnValue + 2)) + (Convert.ToString(intLineNumberOfflineSerial + 1)))) +
                                                             BillingSystem.T_WriteField(strOfflineCode) + BillingSystem.T_WriteField((Convert.ToString((char)(intColumnValue + 3)) + (Convert.ToString(intLineNumberOfflineSerial + 1))))
                                           );
                }
                myDataSet.Dispose();
                myCommand.Dispose();

                StreamWriter.Flush();
                StreamWriter.Close();

                #endregion


                #region TRANSFERING DATA FROM TEXT TO ACCESS
                //Added by INDRAJIT on 14-03-2012

                //TABLE NAME TO BE CREATED
                //tableName = "" + TDSMAN.Classes.TDSMAN.T_tblTEMP_CHALLAN_DETAILS + "";
                tableName = BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS;

                

                //TEXT FILE NAME
                textfileName = "OfflineSerial";

                BillingSystem.T_ReplaceDoubleQuotesinFile(strTemporaryfileOfflineSerialPath, true);


                //IMPORTING THE DATA FROM THE TEXT FILE DIRECTLY TO THE 
                ImportTextToTables(tableName, textfileName, false);

                // DELETE THE TXT FILE
                if (File.Exists(strTemporaryfileOfflineSerialPath) == true)
                    File.Delete(strTemporaryfileOfflineSerialPath);

                #endregion

                #endregion 

                return true;
            }
            catch(Exception e)
            {
                strErrorMessage = "Invalid data format in Excel file, please check.";
                con.Close();
                con.Dispose();
                return false;
            }
        }
        #endregion

        #region ImportTextToTables
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //MODIFIED BY DHRUB FOR DATA IMPORT COMMON METHODOLOGY 
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //------------------------------------------------------- 
        private void ImportTextToTables(string tbl, string txtfile, bool hdr)
        {
            //Check 'n Create SCHEMA file for Temp Tables
            string strFolderPath = cmnService.J_GetDirectoryName(strTemporaryfileOfflineSerialPath);

            if (File.Exists(strFolderPath + "\\schema.ini") == true)
            {
                File.Delete(strFolderPath + "\\schema.ini");
            }
            StreamWriter StreamWriter = new StreamWriter(strFolderPath + "\\schema.ini");

            StreamWriter.WriteLine("[" + txtfile + ".txt]");
            StreamWriter.WriteLine("ColNameHeader=" + (hdr == true ? "True" : "False") + "");
            StreamWriter.WriteLine("Format=Delimited(^)");
            StreamWriter.WriteLine("MaxScanRows=0");
            StreamWriter.WriteLine("CharacterSet=ANSI");


            // TO COPY EXCEL FILE FROM SOURCE LOCATION TO DESTINATION LOCATION
            //System.IO.File.Copy(strTemporaryfileOfflineSerialPath, @"\\ANIK-PC\Users\Public\Documents", true);
            
            ////Check 'n Create Temp Tables
            //if (dmlService.J_IsDatabaseObjectExist(tbl) == true)
            //{
            //    strSQL = "DROP TABLE [" + tbl + "]";

            //    dmlService.J_ExecSql(strSQL);
            //}

            //strSQL = "CREATE TABLE " + tbl + "(" +
            //         "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_ID", J_ColumnType.Integer,J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("RUNNING_SERIAL_NO", J_ColumnType.Integer,J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("RUNNING_SERIAL_NO_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_CODE", J_ColumnType.String, 255, J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("OFFLINE_SERIAL_CODE_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("OFFLINE_CODE", J_ColumnType.String, 255, J_DefaultValue.YES) + "," +
            //         "                                " + cmnService.J_GetDataType("OFFLINE_CODE_CELL", J_ColumnType.String, 10, J_DefaultValue.YES) + ")";

            //if (dmlService.J_ExecSql(strSQL, J_SQLType.DDL) == false) return;

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //BULK INSERT 
            //database_name .table_name
            //FROM 'data_file' 
            //WITH 
            // ( 
            //[ [ , ] DATAFILETYPE =  'char'|'native'| 'widechar' | 'widenative' ]
            //[ [ , ] FIELDTERMINATOR = 'field_terminator' ] 
            //[ [ , ] ROWTERMINATOR = 'row_terminator' ] 
            // )] 
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            strSQL = @" BULK
                            INSERT " + tbl + @"
                            FROM '" + Path.Combine(strFolderPath, txtfile + ".txt") + "' " + @"
                            WITH
                            (
                            DATAFILETYPE = 'char',
                            FIELDTERMINATOR = '^',
                            ROWTERMINATOR = '^\n'
                            )";

            dmlService.J_ExecSql(strSQL, J_SQLType.DML);
        }

        #endregion

        #region VALIDATE_DATA
        private bool VALIDATE_DATA()
        {
            string strSheetName = "";
            try
            {
                if (CREATE_TEMP_ERR_TABLES() == false)
                    return false;
                //---------------------------------------------------------

                strSheetName = T_Sheet_Name.OFFLINE_SERIAL_DETAILS;
                //---
                #region  RUNNING_SERIAL_NO
                ///*
                //     *  ------------LIST OF CHECKS FOR SERIAL NO
                //     *  1. Duplicate Check 
                //     *  2. Blank/Zero Check
                //     *  3. Numeric Check
                //     *  4. Negative Check
                //     *  5. Fraction Check
                //     *  6. Sequential Check
                //     *  
                //    */


                ////UPDATE ALL NULL RECORDS TO ''
                //strSQL = "UPDATE " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " SET RUNNING_SERIAL_NO = '' WHERE RUNNING_SERIAL_NO IS NULL";
                //dmlService.J_ExecSql(strSQL);

                ////------------------------------------
                ////duplicate check
                ////------------------------------------
                //strSQL = "SELECT COUNT(RUNNING_SERIAL_NO)" +
                //         "FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " " +
                //         "GROUP BY RUNNING_SERIAL_NO " +
                //         "HAVING COUNT(RUNNING_SERIAL_NO) > 1";
                ////------------------------------------
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                ////------------------------------------
                //if (lngRowCount > 0)
                //{
                //    // Modified by Ripan Paul on 27-06-2013
                //    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //          "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                //          "             TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //          "             'RUNNING_SERIAL_NO_CELL'," +
                //          "             '" + strSheetName + "'," +
                //          "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                //          "      FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " INNER JOIN " +
                //          "            (SELECT RUNNING_SERIAL_NO " +
                //          "             FROM  " +  BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS +
                //          "             GROUP BY RUNNING_SERIAL_NO " +
                //          "             HAVING COUNT(RUNNING_SERIAL_NO) > 1) AS RUN " +
                //          "      ON     TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO = RUN.RUNNING_SERIAL_NO ";

                //    //                
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}


                ////BLANK OR 0 CHECK
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   " +  BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS +
                //    "     WHERE  (RUNNING_SERIAL_NO = ''" +
                //    "     OR     RUNNING_SERIAL_NO = '0')" +
                //    "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.BLANK_NULL_CHECK + "'," +
                //        "             TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //        "             'RUNNING_SERIAL_NO_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.BLANK_NULL_CHECK + "'" +
                //        "      FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                //        "            (SELECT ERR_CELL " +
                //        "             FROM   TEMP_ERR_VALIDATION " +
                //        "             WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                //        "      ON     TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                //        "     WHERE   ERR_V.ERR_CELL               IS NULL " +
                //        "     AND    (TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO = ''" +
                //        "     OR      TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO  = '0') ";


                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                ////is numeric check
                //strSQL = "SELECT COUNT(*)" +
                //  "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                //  "     WHERE  ISNUMERIC(RUNNING_SERIAL_NO) = 0" +
                //  "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //  "                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    //strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                //    //    "      SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                //    //    "             RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //    //    "             'RUNNING_SERIAL_NO_CELL'," +
                //    //    "             '" + strSheetName + "'," +
                //    //    "             '" + T_Error_Type_Color.NUMERIC_CHECK + "'," +
                //    //    "             '" + cmbFormNo.Text + "'" +
                //    //    "      FROM   TEMP_CHALLAN_DETAILS" +
                //    //    "     WHERE  ISNUMERIC(RUNNING_SERIAL_NO) = 0" +
                //    //    "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //    // Modified by Ripan Paul on 24-06-2013
                //    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.NUMERIC_CHECK + "'," +
                //        "             TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //        "             'RUNNING_SERIAL_NO_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.NUMERIC_CHECK + "'" +
                //        "      FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                //        "           (SELECT ERR_CELL " +
                //        "            FROM   TEMP_ERR_VALIDATION " +
                //        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                //        "      ON     TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                //        "     WHERE   ERR_V.ERR_CELL               IS NULL " +
                //        "     AND     ISNUMERIC(TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO) = 0 ";


                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                ////Negative Check
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                //    "     WHERE  CAST(RUNNING_SERIAL_NO AS MONEY) < 0" +
                //    "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    //strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                //    //    "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                //    //    "             RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //    //    "             'RUNNING_SERIAL_NO_CELL'," +
                //    //    "             '" + strSheetName + "'," +
                //    //    "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'," +
                //    //    "             '" + cmbFormNo.Text + "'" +
                //    //    "      FROM   TEMP_CHALLAN_DETAILS" +
                //    //    "      WHERE  CDBL(VAL(RUNNING_SERIAL_NO)) < 0" +
                //    //    "      AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //    // Modified by Ripan Paul on 24-06-2013
                //    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                //        "             TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //        "             'RUNNING_SERIAL_NO_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                //        "      FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                //        "           (SELECT ERR_CELL " +
                //        "            FROM   TEMP_ERR_VALIDATION " +
                //        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                //        "      ON     TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                //        "      WHERE  ERR_V.ERR_CELL               IS NULL " +
                //        "      AND    CAST(RUNNING_SERIAL_NO AS MONEY)  < 0 ";


                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}

                ////Fraction Check
                //strSQL = "SELECT COUNT(*)" +
                //    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                //    "     WHERE  CAST(RUNNING_SERIAL_NO AS MONEY) - CAST(CAST(CAST(RUNNING_SERIAL_NO AS MONEY)AS INT)AS MONEY) > 0" +
                //    "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                ////
                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount > 0)
                //{
                //    //strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR, ERR_FORM_NO)" +
                //    //    "      SELECT '" + T_Error_Type.FORMAT_CHECK + "'," +
                //    //    "             RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //    //    "             'RUNNING_SERIAL_NO_CELL'," +
                //    //    "             '" + strSheetName + "'," +
                //    //    "             '" + T_Error_Type_Color.FORMAT_CHECK + "'," +
                //    //    "             '" + cmbFormNo.Text + "'" +
                //    //    "     FROM   TEMP_CHALLAN_DETAILS" +
                //    //    "     WHERE  CDBL(VAL(RUNNING_SERIAL_NO)) - CDBL(INT(CDBL(VAL(RUNNING_SERIAL_NO)))) > 0" +
                //    //    "     AND    RUNNING_SERIAL_NO_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                //    //    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";

                //    // Modified by Ripan Paul on 24-06-2013
                //    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //        "      SELECT '" + T_Error_Type.FORMAT_CHECK + "'," +
                //        "             TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //        "             'RUNNING_SERIAL_NO_CELL'," +
                //        "             '" + strSheetName + "'," +
                //        "             '" + T_Error_Type_Color.FORMAT_CHECK + "'" +
                //        "     FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                //        "           (SELECT ERR_CELL " +
                //        "            FROM   TEMP_ERR_VALIDATION " +
                //        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                //        "     ON     TEMP_OFFLINE_SERIAL_DETAILS.RUNNING_SERIAL_NO_CELL = ERR_V.ERR_CELL " +
                //        "     WHERE  ERR_V.ERR_CELL               IS NULL " +
                //        "     AND    CAST(RUNNING_SERIAL_NO AS MONEY) - CAST(CAST(CAST(RUNNING_SERIAL_NO AS MONEY)AS INT)AS MONEY) > 0 ";


                //    //                
                //    if (dmlService.J_ExecSql(strSQL) == false)
                //        return false;
                //}


                ////NOW CHECKING IF THERE IS NO ERROR FOUND IN SERIAL NO FIELD
                //strSQL = "SELECT COUNT(*) " +
                //         "FROM   TEMP_ERR_VALIDATION " +
                //         "WHERE  ERR_COLUMN = 'RUNNING_SERIAL_NO_CELL'";

                //lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //if (lngRowCount == 0)
                //{
                //    //NOW CHECKING THE SERIAL NO SEQUENCING

                //    strSQL = "SELECT COUNT(*) FROM TEMP_OFFLINE_SERIAL_DETAILS";
                //    long lngOfflineSerialCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //    //strSQL = "SELECT MAX(CINT(RUNNING_SERIAL_NO)) FROM TEMP_CHALLAN_DETAILS";
                //    strSQL = "SELECT MAX(CAST(RUNNING_SERIAL_NO AS BIGINT)) FROM TEMP_OFFLINE_SERIAL_DETAILS";
                //    long lngMaxSerialNo = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                //    if (lngOfflineSerialCount != lngMaxSerialNo)
                //    {
                //        //SEQUENCING PROBLEM || SOME SERIAL NO IS MISSING IN BETWEEN

                //        //NOW INSERTING ALL SERIAL NOS AS ERROR 
                //        strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                //          "      SELECT '" + T_Error_Type.SEQUENCE_CHECK + "'," +
                //          "             RUNNING_SERIAL_NO_CELL AS ERROR_CELL," +
                //          "             'RUNNING_SERIAL_NO_CELL'," +
                //          "             '" + strSheetName + "'," +
                //          "             '" + T_Error_Type_Color.SEQUENCE_CHECK + "'" +
                //          "      FROM   TEMP_OFFLINE_SERIAL_DETAILS";
                //        //                
                //        if (dmlService.J_ExecSql(strSQL) == false)
                //            return false;
                //    }
                //}
                #endregion
                //---
                #region OFFLINE_SERIAL_CODE

                /*
                 *  ------------LIST OF CHECKS FOR Offline Serial Code Field
                 *  1. Blank Checking
                 *  2. Duplicate Check
                 *  5. Length Check (>20)
                 *  
                */

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE TEMP_OFFLINE_SERIAL_DETAILS SET OFFLINE_SERIAL_CODE = '' WHERE OFFLINE_SERIAL_CODE IS NULL";
                dmlService.J_ExecSql(strSQL);



                //BLANK CHECK FOR OFFLINE_SERIAL_CODE 
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                    "     WHERE  OFFLINE_SERIAL_CODE  = ''   " +
                    "     AND    OFFLINE_SERIAL_CODE_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL AS ERROR_CELL," +
                        "             'OFFLINE_SERIAL_CODE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   TEMP_ERR_VALIDATION " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL      IS NULL " +
                        "     AND    TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE = ''  ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //------------------------------------
                //Duplicate Check
                //------------------------------------
                strSQL = "SELECT COUNT(OFFLINE_SERIAL_CODE)" +
                         "FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " " +
                         "GROUP BY OFFLINE_SERIAL_CODE " +
                         "HAVING COUNT(OFFLINE_SERIAL_CODE) > 1";
                //------------------------------------
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //------------------------------------
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                          "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                          "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL AS ERROR_CELL," +
                          "             'OFFLINE_SERIAL_CODE_CELL'," +
                          "             '" + strSheetName + "'," +
                          "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                          "      FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " INNER JOIN " +
                          "            (SELECT OFFLINE_SERIAL_CODE " +
                          "             FROM  " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS +
                          "             GROUP BY OFFLINE_SERIAL_CODE " +
                          "             HAVING COUNT(OFFLINE_SERIAL_CODE) > 1) AS RUN " +
                          "      ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE = RUN.OFFLINE_SERIAL_CODE ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //CHECKING IF EXISTS INTO THE DATABASE
                strSQL = @" SELECT COUNT(*)
                            FROM   TEMP_OFFLINE_SERIAL_DETAILS
                                   INNER JOIN MST_OFFLINE_SERIAL 
                                   ON TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE =  MST_OFFLINE_SERIAL.OFFLINE_SERIAL_CODE 
                            WHERE  ITEM_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbItem, cmbItem.SelectedIndex);

                //------------------------------------
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //------------------------------------
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                          "      SELECT '" + T_Error_Type.ALREADY_EXISTS_CHECK + "'," +
                          "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL AS ERROR_CELL," +
                          "             'OFFLINE_SERIAL_CODE_CELL'," +
                          "             '" + strSheetName + "'," +
                          "             '" + T_Error_Type_Color.ALREADY_EXISTS_CHECK + "'" +
                          "      FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " " +
                          "             INNER JOIN MST_OFFLINE_SERIAL  " +
                          "             ON TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE =  MST_OFFLINE_SERIAL.OFFLINE_SERIAL_CODE "+
                          "      WHERE  ITEM_ID = " + cmnService.J_GetComboBoxItemId(ref cmbItem, cmbItem.SelectedIndex);

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }

                //LENGTH CHECK FOR BSR CODE
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                    "     WHERE  LEN(OFFLINE_SERIAL_CODE)   > 20" +
                    "     AND    OFFLINE_SERIAL_CODE <> ''" +
                    "     AND    OFFLINE_SERIAL_CODE_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                    "                                             WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL AS ERROR_CELL," +
                        "             'OFFLINE_SERIAL_CODE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   TEMP_ERR_VALIDATION " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE_CELL  = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    LEN(TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE) > 20 " +
                        "     AND    TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE      <> ''  ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                #endregion
                //---
                #region OFFLINE_CODE

                /*
                 *  ------------LIST OF CHECKS FOR Offline Serial Code Field
                 *  1. Blank Checking
                 *  2. Duplicate Check
                 *  5. Length Check (>20)
                 *  
                */

                //UPDATE ALL NULL RECORDS TO ''
                strSQL = "UPDATE TEMP_OFFLINE_SERIAL_DETAILS SET OFFLINE_CODE = '' WHERE OFFLINE_CODE IS NULL";
                dmlService.J_ExecSql(strSQL);



                //BLANK CHECK FOR OFFLINE_CODE 
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                    "     WHERE  OFFLINE_CODE  = ''   " +
                    "     AND    OFFLINE_CODE_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                    "                                           WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.VALIDITY_CHECK + "'," +
                        "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL AS ERROR_CELL," +
                        "             'OFFLINE_CODE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.VALIDITY_CHECK + "'" +
                        "     FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   TEMP_ERR_VALIDATION " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL      IS NULL " +
                        "     AND    TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE = ''  ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }



                //------------------------------------
                //Duplicate Check
                //------------------------------------
                strSQL = "SELECT COUNT(OFFLINE_CODE)" +
                         "FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " " +
                         "GROUP BY OFFLINE_CODE " +
                         "HAVING COUNT(OFFLINE_CODE) > 1";
                //------------------------------------
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //------------------------------------
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                          "      SELECT '" + T_Error_Type.DUPLICATE_CHECK + "'," +
                          "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL AS ERROR_CELL," +
                          "             'OFFLINE_CODE_CELL'," +
                          "             '" + strSheetName + "'," +
                          "             '" + T_Error_Type_Color.DUPLICATE_CHECK + "'" +
                          "      FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " INNER JOIN " +
                          "            (SELECT OFFLINE_CODE " +
                          "             FROM  " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS +
                          "             GROUP BY OFFLINE_CODE " +
                          "             HAVING COUNT(OFFLINE_CODE) > 1) AS RUN " +
                          "      ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE = RUN.OFFLINE_CODE ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                //CHECKING IF EXISTS INTO THE DATABASE
                strSQL = @" SELECT COUNT(*)
                            FROM   TEMP_OFFLINE_SERIAL_DETAILS
                                   INNER JOIN MST_OFFLINE_SERIAL 
                                   ON TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE =  MST_OFFLINE_SERIAL.OFFLINE_CODE 
                            WHERE  ITEM_ID  = " + cmnService.J_GetComboBoxItemId(ref cmbItem, cmbItem.SelectedIndex);

                //------------------------------------
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
                //------------------------------------
                if (lngRowCount > 0)
                {
                    // Modified by Ripan Paul on 27-06-2013
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                          "      SELECT '" + T_Error_Type.ALREADY_EXISTS_CHECK + "'," +
                          "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL AS ERROR_CELL," +
                          "             'OFFLINE_CODE_CELL'," +
                          "             '" + strSheetName + "'," +
                          "             '" + T_Error_Type_Color.ALREADY_EXISTS_CHECK + "'" +
                          "      FROM   " + BS.T_tblTEMP_OFFLINE_SERIAL_DETAILS + " " +
                          "             INNER JOIN MST_OFFLINE_SERIAL  " +
                          "             ON TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE =  MST_OFFLINE_SERIAL.OFFLINE_CODE " +
                          "      WHERE  ITEM_ID = " + cmnService.J_GetComboBoxItemId(ref cmbItem, cmbItem.SelectedIndex);

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                //LENGTH CHECK FOR BSR CODE
                strSQL = "SELECT COUNT(*)" +
                    "     FROM   TEMP_OFFLINE_SERIAL_DETAILS" +
                    "     WHERE  LEN(OFFLINE_CODE)   > 20" +
                    "     AND    OFFLINE_CODE <> ''" +
                    "     AND    OFFLINE_CODE_CELL NOT IN (SELECT ERR_CELL FROM TEMP_ERR_VALIDATION " +
                    "                                             WHERE ERR_SHEET = '" + strSheetName + "')";
                //
                lngRowCount = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));

                if (lngRowCount > 0)
                {
                    strSQL = " INSERT INTO TEMP_ERR_VALIDATION(ERR_TYPE, ERR_CELL, ERR_COLUMN, ERR_SHEET, ERR_COLOR)" +
                        "      SELECT '" + T_Error_Type.LENGTH_CHECK + "'," +
                        "             TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL AS ERROR_CELL," +
                        "             'OFFLINE_CODE_CELL'," +
                        "             '" + strSheetName + "'," +
                        "             '" + T_Error_Type_Color.LENGTH_CHECK + "'" +
                        "     FROM   TEMP_OFFLINE_SERIAL_DETAILS LEFT JOIN " +
                        "           (SELECT ERR_CELL " +
                        "            FROM   TEMP_ERR_VALIDATION " +
                        "            WHERE  ERR_SHEET = '" + strSheetName + "') AS ERR_V " +
                        "     ON     TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE_CELL  = ERR_V.ERR_CELL " +
                        "     WHERE  ERR_V.ERR_CELL       IS NULL " +
                        "     AND    LEN(TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE) > 20 " +
                        "     AND    TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE      <> ''  ";

                    //                
                    if (dmlService.J_ExecSql(strSQL) == false)
                        return false;
                }


                #endregion
                //---

                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                return false;
            }
        }
        #endregion

        #region SAVE_ERR
        private bool SAVE_ERR(string ErrType, string ErrCell, string ErrColumn, string ErrColor, string ErrSheet, string ErrFormNo)
        {
            try
            {
                //
                strSQL = "INSERT INTO TEMP_ERR_VALIDATION (ERR_TYPE," +
                    "                                      ERR_CELL," +
                    "                                      ERR_COLUMN," +
                    "                                      ERR_COLOR," +
                    "                                      ERR_SHEET," +
                    "                                      ERR_FORM_NO) " +
                    "     VALUES                          ('" + ErrType + "'," +
                    "                                      '" + ErrCell + "'," +
                    "                                      '" + ErrColumn + "'," +
                    "                                      '" + ErrColor + "'," +
                    "                                      '" + ErrSheet + "'," +
                    "                                      '" + ErrFormNo + "')";
                //
                dmlService.J_ExecSql(strSQL);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region KILL EXCEL
        private bool KILL_EXCEL()
        {
            try
            {
                //--
                //foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                //{
                //    if (process.MainModule.ModuleName.ToUpper().Equals("EXCEL.EXE"))
                //    {
                //        process.Kill();
                //        //process.Close();
                //        //process.Dispose();
                //        break;
                //    }
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DELETE WORKSHEET
        private bool DELETE_WORKSHEET(string ExcelFilePath, string ExcelSheet)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    if (ws.Name.ToString().Trim() == ExcelSheet)
                    {
                        ws.Delete();
                        break;
                    }
                } 

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch(Exception err)
            {
                cmnService.J_UserMessage(err.Message);
                return false;
            }
        }
        #endregion

        #region CREATE NEW EXCEL_FILE
        public bool CREATE_NEW_EXCEL_FILE(string strFilePath,string FileName)
        {
            strTemporaryValidateExcelfilePath = Path.Combine(strFilePath, FileName) + DateTime.Now.ToString("HH_mm_ss")+".xls";
            try
            {
                //if (File.Exists(Path.Combine(strFilePath, FileName) + ".xls") == true)
                //    File.Delete(Path.Combine(strFilePath, FileName) + ".xls");

                //if (BillingSystem.T_isFileOpenOrReadOnly(ref strPath) == true)
                //{
                //    cmnService.J_UserMessage("Selected Excel file is open");
                //    btnSelectExcelPath.Select();
                //    return false;
                //}

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return false;
                }


                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "Sheet 1 content";

                xlWorkBook.SaveAs(strTemporaryValidateExcelfilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                return true;
            }
            catch (Exception Err)
            {
                return false;
            }
        }
        #endregion 


        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);


                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = strErrorWorksheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();
               




                //------------------------------------------------

                //Microsoft.Office.Interop.Excel.Application xlApp = null;
                //Microsoft.Office.Interop.Excel.Workbook xlWorkbook = null;
                //Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
                //Microsoft.Office.Interop.Excel.Worksheet xlNewSheet = null;

                //try
                //{
                //    xlApp = new Microsoft.Office.Interop.Excel.Application();

                //    if (xlApp == null)
                //        return;

                //    // Uncomment the line below if you want to see what's happening in Excel
                //    // xlApp.Visible = true;

                //    xlWorkbook = xlApp.Workbooks.Open(filename, 0, false, 5, "", "",
                //            false, XlPlatform.xlWindows, "",
                //            true, false, 0, true, false, false);

                //    xlSheets = xlWorkbook.Sheets as Sheets;

                //    // The first argument below inserts the new worksheet as the first one
                //    xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                //    xlNewSheet.Name = worksheetName;

                //    xlWorkbook.Save();
                //    xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                //    xlApp.Quit();
                //}
                //finally
                //{
                //    Marshal.ReleaseComObject(xlNewSheet);
                //    Marshal.ReleaseComObject(xlSheets);
                //    Marshal.ReleaseComObject(xlWorkbook);
                //    Marshal.ReleaseComObject(xlApp);
                //    xlApp = null;
                //}
                return true;
            }
            catch(Exception e)
            {

                return false;
            }
        }
        #endregion

        #region WRITE ERROR WORKSHEET
        private bool WRITE_ERROR_WORKSHEET(string ExcelFilePath)
        {
            GC.Collect();
            #region Variable Declaration
            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            string strMatchSheetName = T_Sheet_Name.OFFLINE_SERIAL_DETAILS;
            int intSkipIF = 0;

            #endregion 

            try
            {

                string strStartupPath = Path.Combine(Application.StartupPath, "TMP FOLDER");
                if (Directory.Exists(strStartupPath) == false)
                    // DELETE IF THE FILE EXISTS.
                    Directory.CreateDirectory(strStartupPath);
                //

                if (CREATE_NEW_EXCEL_FILE(strStartupPath, "Validation Error") == false)
                    return false;

                #region WRITE ERRORS INTO THE EXCEL WORKSHEET

                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + strTemporaryValidateExcelfilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;



                //This is the offending line:
                //Microsoft.Office.Interop.Excel.Worksheet wsnew =  sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                //wsnew.Name = strErrorWorksheetName;
                wsnew.Rows.Delete(100);

                wb.Save();

                //@@@@@@@@@@@@@@@
                //long lngTotalRecordsErrorSheet = cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TEMP_ERR_VALIDATION")));
                // 
                wsnew.get_Range("B:B", m).ColumnWidth = 150;
                wsnew.get_Range("B2", m).Value2 = "Error Validations";
                wsnew.get_Range("B2", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                wsnew.get_Range("B2", m).Font.Size = 15;
                //
                wsnew.get_Range("B4", m).Value2 = T_Sheet_Name.OFFLINE_SERIAL_DETAILS;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   " + BS.T_tblTEMP_ERR_VALIDATION + " " +
                    "     ORDER BY ERR_SHEET," +
                    "            ERR_VALIDATION_ID";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {
                    wsnew.get_Range("B" + lngErrorSheetRow, m).Value2 = "Cell : " + drdGetErrorSheetRecord["ERR_CELL"].ToString() + " - [" + drdGetErrorSheetRecord["ERR_COLUMN"].ToString().Replace("_", " ").Replace("CELL", "") + "] " + drdGetErrorSheetRecord["ERR_TYPE"].ToString();
                    //wsnew.get_Range("B" + lngErrorSheetRow, m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;

                #endregion
                //--
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region LOAD IMPORT INTERFACE
        private bool LOAD_IMPORT_INTERFACE()
        {
            try
            {
                if (cmnService.J_ReturnInt32Value(Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM TEMP_ERR_VALIDATION"))) == 0)
                {
                    //------------------------------------------------------
                    //tbcExcelImport.TabPages.Remove(tbpValidateExcelFile);
                    //------------------------------------------------------
                    blnOpenTabPage = true;
                    tbcExcelImport.SelectTab(tbpImportExcelFile);
                    //------------------------------------------------------
                    lblItemName.Text = cmbItem.Text.Trim(); 
                    txtTotalRecords.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(OFFLINE_SERIAL_ID) AS COUNT_OFFLINE_SERIAL_ID FROM TEMP_OFFLINE_SERIAL_DETAILS"));
                    LoadOfflineSerialGrid();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion



        #region LoadOfflineSerialGrid
        private void LoadOfflineSerialGrid()
        {
            try
            {
                //--
                #region ALTER TABLE TEMP_CHALLAN_DETAILS(ITEM_ID)
                if (dmlService.J_IsDatabaseObjectExist("TEMP_OFFLINE_SERIAL_DETAILS", "ITEM_ID") == false)
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //MODIFIED BY DHRUB ON 20/05/2014 FOR COMMON SQL 
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //strSQL = "ALTER TABLE TEMP_CHALLAN_DETAILS ADD COLUMN TOT_TAX NUMBER DEFAULT 0";
                    strSQL = "ALTER TABLE TEMP_OFFLINE_SERIAL_DETAILS ADD " + cmnService.J_GetDataType("ITEM_ID", J_ColumnType.Integer, J_DefaultValue.YES) + " ";
                    dmlService.J_ExecSql(strSQL);
                }
                //--
                strSQL = "UPDATE TEMP_OFFLINE_SERIAL_DETAILS SET ITEM_ID = " + cmnService.J_GetComboBoxItemId(ref cmbItem, cmbItem.SelectedIndex);
                dmlService.J_ExecSql(strSQL);
                //--
                #endregion

                //-----------------------------------------------------------
                string[,] strMatrixOfflineSerialDetails = {{"Offline Serial ID", "0", "", "Right", "", "", ""},
                                                           {"Sl No.", "50", "S", "", "", "", ""},
                                                           {"Offline Serial Code.", "300", "S", "", "", "", ""},
                                                           {"Offline Code", "300", "S", "", "", "", ""}};
                //-----------------------------------------------------------
                //strMatrix = strMatrix1;
                //-----------------------------------------------------------
                /* (1) Column Value
                 * (2) Column Data Type
                 * (3) Replace String
                 * (4) Replace String Data Type */
                //-----------------------------------------------------------

                strOrderBy = " TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_ID ";

                strQuery = " SELECT TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_ID   AS SERIAL_ID," +
                           "        TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_ID   AS SL_NO," +
                           "        TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_SERIAL_CODE AS OFFLINE_SERIAL_CODE," +
                           "        TEMP_OFFLINE_SERIAL_DETAILS.OFFLINE_CODE        AS OFFLINE_CODE " +
                           " FROM   TEMP_OFFLINE_SERIAL_DETAILS ";

                //-----------------------------------------------------------
                strSQL = strQuery + "ORDER BY " + strOrderBy;
                //-----------------------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                //dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewChallan, strSQLGridViewTabPages, strMatrixChallanDetails);       //Show Data into the Grid                
                dsetGridClone = dmlService.J_ShowDataInGrid(ref  dgcViewOfflineSerial, strSQL, strMatrixOfflineSerialDetails);       //Show Data into the Grid
                //---------------------------------------
                //FETCHING NO OF SERIAL AVAILABLE 
                //---------------------------------------
                strSQL = @" SELECT COUNT(*)
                            FROM   MST_OFFLINE_SERIAL
                                   LEFT JOIN TRN_INVOICE_HEADER
                                   ON MST_OFFLINE_SERIAL.OFFLINE_SERIAL_ID = TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID 
                            WHERE  ITEM_ID = " + cmnService.J_GetComboBoxItemId(ref cmbItem,cmbItem.SelectedIndex)+ 
                         "  AND    INACTIVE_FLAG = 0 " + 
                         "  AND    TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID IS NULL";

                lblNoofAvailableSerial.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)); 
            }
            catch (Exception err)
            {
                cmnService.J_UserMessage(err.Message);
            }
        }
        #endregion

        
        #region INSERT_MASTER_DATA
        public bool INSERT_MASTER_DATA()
        {
            try
            {

                #region INSERTING THE OFFLINE SERIAL CODE INTO MST_OFFLINE_SERIAL
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //INSERT TEMPORARY MASTER DATA FOR EXCEL INTO MAIN MASTER
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                strSQL = "INSERT INTO MST_OFFLINE_SERIAL" +
                   "                 (ITEM_ID," +
                   "                  OFFLINE_SERIAL_CODE," +
                   "                  OFFLINE_CODE)" +
                   "      SELECT      ITEM_ID," +
                   "                  OFFLINE_SERIAL_CODE," +
                   "                  OFFLINE_CODE " +
                   "      FROM        TEMP_OFFLINE_SERIAL_DETAILS ";

                if (dmlService.J_ExecSql(strSQL) == false)
                {
                    return false;
                }

                return true;
                #endregion 
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CheckExcelStructure
        private bool CheckExcelStructure(string ExcelFilePath)
        {
            try
            {
                //
                if (BillingSystem.T_IsExcelDatabaseObjectExist("Offline Serial Code", null, con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (BillingSystem.T_IsExcelDatabaseObjectExist("Offline Serial Code", "Offline Serial No", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (BillingSystem.T_IsExcelDatabaseObjectExist("Offline Serial Code", "Offline Serial Code", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                if (BillingSystem.T_IsExcelDatabaseObjectExist("Offline Serial Code", "Offline Code", con) == false)
                {
                    con.Close();
                    con.Dispose();
                    return false;
                }
                //
                return true;
            }
            catch(Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                con.Close();
                return false;
            }
        }
        #endregion

        #region Delete_TMP_Files
        private void Delete_TMP_Files(string FilePath)
        {
            try
            {
                foreach (string sFile in System.IO.Directory.GetFiles(Path.GetDirectoryName(FilePath)))
                {
                    if (cmnService.J_IsProcessOpen(Path.Combine(Path.GetDirectoryName(FilePath), Convert.ToString(sFile))) == false)
                        if (sFile.ToUpper().EndsWith(".TMP"))
                            System.IO.File.Delete(sFile);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region GenerateSerial
        public void GenerateSerial()
        {
//            //Added by Shrey Kejriwal on 21/05/2012
//            try
//            {

//                //Checking if duplicate serial exists

////                strSQL = @"SELECT count(DEDUCTEE_DETAILS_ID)
////                        FROM TEMP_DEDUCTEE_DETAILS
////                        GROUP BY TEMP_DEDUCTEE_DETAILS.CHALLAN_SERIAL_NO, TEMP_DEDUCTEE_DETAILS.DEDUCTEE_SERIAL_NO
////                        HAVING (((Count(TEMP_DEDUCTEE_DETAILS.[DEDUCTEE_SERIAL_NO]))>1))";

////                int iCount = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));

////                if (iCount > 0)
////                {
////                    lblProgressDisplayMessage.Text = "Rectifying deductee Serial Nos";

////                    // ---- DUPLICATE SERIAL NO EXISTS -----

////                    // -- RECTIFYING THE SERIAL NOS OF THOSE CHALLAN IN WHICH DUPLICATE RECORD EXISTS

////                    IDataReader reader;

////                    DMLService innerSQLDML = new DMLService();

////                    strSQL = "SELECT DEDUCTEE_DETAILS_ID, " +
////                             "       CHALLAN_SERIAL_NO " +
////                             "FROM   TEMP_DEDUCTEE_DETAILS " +
////                             "INNER JOIN (SELECT CHALLAN_SERIAL_NO " +
////                             "            FROM TEMP_DEDUCTEE_DETAILS " +
////                             "            GROUP BY CHALLAN_SERIAL_NO, DEDUCTEE_SERIAL_NO " +
////                             "            HAVING COUNT(DEDUCTEE_SERIAL_NO) > 1) AS TEMP " +
////                             "        ON TEMP_DEDUCTEE_DETAILS.CHALLAN_SERIAL_NO =  TEMP_DEDUCTEE_DETAILS.CHALLAN_SERIAL_NO " +  
////                             "ORDER BY CHALLAN_SERIAL_NO";

////                    reader = dmlService.J_ExecSqlReturnReader(strSQL);

////                    string strPrevChallanNo = "";

////                    int intSerial = 0;

////                    while (reader.Read())
////                    {
////                        string strChallanNo = Convert.ToString(reader["CHALLAN_SERIAL_NO"]);
////                        long lngRecordID = Convert.ToInt64(reader["DEDUCTEE_DETAILS_ID"]);

////                        if (strPrevChallanNo == strChallanNo)
////                            intSerial++;
////                        else
////                            intSerial = 1;

////                        strSQL = "UPDATE TEMP_DEDUCTEE_DETAILS " +
////                                 "SET DEDUCTEE_SERIAL_NO = '" + intSerial + "' " +
////                                 "WHERE DEDUCTEE_DETAILS_ID = " + lngRecordID;

////                        innerSQLDML.J_ExecSql(strSQL);

////                        strPrevChallanNo = Convert.ToString(reader["CHALLAN_SERIAL_NO"]);
////                    }

////                    reader.Dispose();
////                    reader.Close();

////                }


//                dmlService.J_BeginTransaction();

//                //CREATING TEMP TABLE TO STORE THE SERIAL NOS FOR THE IDS

//                if (dmlService.J_IsDatabaseObjectExist("TEMP_UPDATE_SERIAL") == true)
//                {
//                    strSQL = "DROP TABLE TEMP_UPDATE_SERIAL";
//                    dmlService.J_ExecSql(strSQL);
//                }

//                strSQL = "CREATE TABLE TEMP_UPDATE_SERIAL(" +
//                         "              DEDUCTEE_DETAILS_ID NUMBER DEFAULT 0, " +
//                         "              DEDUCTEE_SERIAL_NO  NUMBER DEFAULT 0)";

//                dmlService.J_ExecSql(strSQL, J_SQLType.DDL);

//                //INSERTING THE UPDATED SERIAL NOS FOR THE DEDUCTEE IDS 

//                strSQL = "INSERT INTO TEMP_UPDATE_SERIAL(DEDUCTEE_DETAILS_ID, DEDUCTEE_SERIAL_NO) " +
//                         "SELECT DEDUCTEE_DETAILS_ID, " +
//                         "       (SELECT COUNT(*) " +
//                         "        FROM   TEMP_DEDUCTEE_DETAILS AS TEMP " +
//                         "        WHERE  TEMP_DEDUCTEE_DETAILS.CHALLAN_SERIAL_NO = TEMP.CHALLAN_SERIAL_NO " +
//                         "        AND TEMP_DEDUCTEE_DETAILS.DEDUCTEE_DETAILS_ID > " +
//                         "                TEMP.DEDUCTEE_DETAILS_ID) + 1 AS DEDUCTEE_SERIAL_NO " +
//                         "FROM   TEMP_DEDUCTEE_DETAILS";

//                dmlService.J_ExecSql(strSQL);


//                //---------------------------------------------------------
//                //COMMENTED BY DHRUB FOR COMMON UPDATE SQL FORMAT ON 08/05/2014
//                //---------------------------------------------------------
//                ////UPDATING THE SERIAL NOS IN THE DEDUCTEE DETAILS IMPORT TABLE
//                //strSQL = "UPDATE TEMP_DEDUCTEE_DETAILS " +
//                //         "INNER JOIN TEMP_UPDATE_SERIAL " +
//                //         "       ON  TEMP_DEDUCTEE_DETAILS.DEDUCTEE_DETAILS_ID = TEMP_UPDATE_SERIAL.DEDUCTEE_DETAILS_ID " +
//                //         "SET TEMP_DEDUCTEE_DETAILS.DEDUCTEE_SERIAL_NO = TEMP_UPDATE_SERIAL.DEDUCTEE_SERIAL_NO";

//                //---------------------------------------------------------------
//                //ADDED BY DHRUB FOR COMMON UPDATE SQL FORMAT ON 08/05/2014
//                //---------------------------------------------------------------
//                strActualTableName = "TEMP_DEDUCTEE_DETAILS";
//                strJoinStatement = "INNER JOIN TEMP_UPDATE_SERIAL " +
//                                   "       ON  TEMP_DEDUCTEE_DETAILS.DEDUCTEE_DETAILS_ID = TEMP_UPDATE_SERIAL.DEDUCTEE_DETAILS_ID ";
//                strSetStatement = "SET TEMP_DEDUCTEE_DETAILS.DEDUCTEE_SERIAL_NO = TEMP_UPDATE_SERIAL.DEDUCTEE_SERIAL_NO";

//                strSQL = cmnService.J_ConvertUpdateSqlDBFormat(strActualTableName, strJoinStatement, strSetStatement, strWhereStatement);
//                //---------------------------------------------------------------
//                //---------------------------------------------------------------
//                //---------------------------------------------------------------
//                dmlService.J_ExecSql(strSQL);

//                strSQL = "DROP TABLE TEMP_UPDATE_SERIAL";
//                dmlService.J_ExecSql(strSQL);

//                dmlService.J_Commit();

//            }
//            catch (Exception err)
//            {
//                dmlService.J_Rollback();
//                return;
//            }
        }
        #endregion

        #region COLOR ERROR CELLS
        private bool COLOR_ERROR_CELLS(string ExcelFilePath)
        {

            IDataReader drdGetErrorSheetRecord = null;
            //--
            long lngErrorSheetRow = 5;
            //--
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;
                //
                strSQL = "SELECT ERR_TYPE," +
                    "            ERR_CELL," +
                    "            ERR_COLUMN," +
                    "            ERR_COLOR," +
                    "            ERR_SHEET," +
                    "            ERR_DESC " +
                    "     FROM   TEMP_ERR_VALIDATION " +
                    "     ORDER BY ERR_SHEET";
                //
                drdGetErrorSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetErrorSheetRecord == null)
                {
                    drdGetErrorSheetRecord.Close();
                    drdGetErrorSheetRecord.Dispose();
                    return false;
                }
                while (drdGetErrorSheetRecord.Read())
                {

                    Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[drdGetErrorSheetRecord["ERR_SHEET"].ToString()];
                    //
                    wsnew.get_Range(drdGetErrorSheetRecord["ERR_CELL"].ToString(), m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName(drdGetErrorSheetRecord["ERR_COLOR"].ToString()));
                    //
                    wb.Save();
                    //
                    lngErrorSheetRow = lngErrorSheetRow + 1;
                }
                drdGetErrorSheetRecord.Close();
                drdGetErrorSheetRecord.Dispose();
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Functions for Create Blank Excel Sheet

        #region ReleaseObject
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region CREATE EXCEL FILE
        // SOURCE PATH : http://csharp.net-informations.com/excel/csharp-create-excel.htm
        private bool CREATE_EXCEL_FILE(string ExcelFilePath)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                //Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                //--
                //string strExcelFileName = cmbFileType.Text.ToUpper() + "_BLANK" + cmbFormNo.Text.ToUpper() + "." + cmbFileType.Text;
                //--

                xlApp = new Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
                //--
                //------------------------------------
                if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLS")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                else if (Path.GetExtension(ExcelFilePath).ToUpper() == ".XLSX")
                    xlWorkBook.SaveAs(ExcelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //------------------------------------
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                //ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region CREATE NEW WORKSHEET
        private bool CREATE_NEW_WORKSHEET(string ExcelFilePath, string WorksheetName)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //
                //Microsoft.Office.Interop.Excel.Worksheet WrkSheet;
                //WrkSheet =   (Microsoft.Office.Interop.Excel.Worksheet)Globals.ThisWorkbook.Worksheets.Add(missing, missing, missing, missing);
                //
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //string myPath = @"" + ExcelFilePath;
                //excelApp.Workbooks.Open(myPath);
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                excelapp.DisplayAlerts = false;

                //if (excelapp == null) throw new Exception("Can't start Excel");
                if (excelapp == null) return false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                //if I create a new file and then add a worksheet,
                //it will exit normally (i.e. if you uncomment the next two lines
                //and comment out the .Open() line below):
                //Excel.Workbook wb = wbs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //wb.SaveAs(filename, m, m, m, m, m, 
                //          Excel.XlSaveAsAccessMode.xlExclusive,
                //          m, m, m, m, m);

                //but if I open an existing file and add a worksheet,
                //it won't exit (leaves zombie excel processes)
                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                //Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                //                             m, m, m, m, m, m,
                //                             Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                //                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:
                Microsoft.Office.Interop.Excel.Worksheet wsnew = sheets.Add(m, m, m, m) as Microsoft.Office.Interop.Excel.Worksheet;

                wsnew.Name = WorksheetName;

                //N.B. it doesn't help if I try specifying the parameters in Add() above

                wb.Save();
                wb.Close(m, m, m);

                //overkill to do GC so many times, but shows that doesn't fix it
                //GC();
                //cleanup COM references
                //changing these all to FinalReleaseComObject doesn't help either
                //while (Marshal.ReleaseComObject(wsnew) > 0) { }
                wsnew = null;
                //while (Marshal.ReleaseComObject(sheets) > 0) { }
                sheets = null;
                //while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                //while (Marshal.ReleaseComObject(wbs) > 0) { }
                wbs = null;
                //GC();
                excelapp.Quit();
                //while (Marshal.ReleaseComObject(excelapp) > 0) { }
                excelapp = null;
                //GC();

                return true;
            }
            catch (Exception e)
            {
                cmnService.J_UserMessage(e.Message);
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE WORKSHEET

        #region WRITE WORKSHEET
        private bool WRITE_WORKSHEET(string ExcelFilePath, string SheetName, string Cell, string CellValue, bool Mandatory)
        {
            try
            {
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range(Cell, m).Value2 = CellValue;
                wsnew.get_Range(Cell, m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range(Cell, m).Borders.Value = true;
                wsnew.get_Range(Cell, m).RowHeight = 48.75;
                wsnew.get_Range(Cell, m).ColumnWidth = 12;
                wsnew.get_Range(Cell, m).WrapText = true;
                wsnew.get_Range(Cell, m).Font.Name = "Arial";
                wsnew.get_Range(Cell, m).Font.Bold = true;
                wsnew.get_Range(Cell, m).Font.Size = 9;
                //@@@@@@@@@@@@@@@
                if (Mandatory == true)
                    //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow);
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                else if (Mandatory == false)
                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //                    wsnew.get_Range(Cell, m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                //@@@@@@@@@@@@@@@

                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch (Exception ERR)
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }
        #endregion

        #region WRITE_REMARKS_WORKSHEET
        private bool WRITE_REMARKS_WORKSHEET(string ExcelFilePath, string SheetName, string FormNo)
        {
            try
            {
                IDataReader drdGetSheetRecord = null;
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("A1", m).Value2 = "Reason";
                wsnew.get_Range("A1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("A1", m).Borders.Value = true;
                wsnew.get_Range("A1", m).ColumnWidth = 12;
                wsnew.get_Range("A1", m).WrapText = true;
                wsnew.get_Range("A1", m).Font.Name = "Arial";
                wsnew.get_Range("A1", m).Font.Bold = true;
                wsnew.get_Range("A1", m).Font.Size = 10;
                wsnew.get_Range("A1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("A1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                //
                wsnew.get_Range("B1", m).Value2 = "Reason Description";
                wsnew.get_Range("B1", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B1", m).Borders.Value = true;
                wsnew.get_Range("B1", m).ColumnWidth = 65;
                wsnew.get_Range("B1", m).WrapText = true;
                wsnew.get_Range("B1", m).Font.Name = "Arial";
                wsnew.get_Range("B1", m).Font.Bold = true;
                wsnew.get_Range("B1", m).Font.Size = 10;
                wsnew.get_Range("B1", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                wsnew.get_Range("B1", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
                ////@@@@@@@@@@@@@@@
                strSQL = "SELECT REASON_ID," +
                    "            REASON," +
                    "            DESCRIPTION " +
                    "     FROM   MST_REASON " +
                    "     WHERE  FORM_NO ='" + FormNo + "' ";
                strSQL = strSQL + "ORDER BY REASON";
                //
                drdGetSheetRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                //-------------------------------------------------------
                if (drdGetSheetRecord == null)
                {
                    return false;
                }
                long lngSheetRow = 2;
                while (drdGetSheetRecord.Read())
                {
                    //
                    wsnew.get_Range("A" + lngSheetRow, m).Value2 = drdGetSheetRecord["REASON"].ToString();
                    wsnew.get_Range("A" + lngSheetRow, m).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    wsnew.get_Range("A" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("A" + lngSheetRow, m).Font.Size = 10;
                    //
                    wsnew.get_Range("B" + lngSheetRow, m).Value2 = drdGetSheetRecord["DESCRIPTION"].ToString();
                    wsnew.get_Range("B" + lngSheetRow, m).Borders.Value = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Name = "Arial";
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Bold = true;
                    wsnew.get_Range("B" + lngSheetRow, m).Font.Size = 10;
                    //
                    lngSheetRow = lngSheetRow + 1;
                }
                drdGetSheetRecord.Close();
                drdGetSheetRecord.Dispose();
                //
                //excelapp.ActiveWindow.FreezePanes = false;
                //excelapp.get_Range("A1", "M18").Select();
                //excelapp.ActiveWindow.FreezePanes = true;
                //
                //wsnew.get_Range("A1", "M18").Locked = false;
                //wsnew.get_Range("A1", "M18").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #region WRITE READ ME WORKSHEET
        private bool WRITE_READ_ME_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                if (KILL_EXCEL() == false)
                    return false;
                //--
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //
                wsnew.get_Range("B4", m).Borders.Value = true;
                wsnew.get_Range("B4", m).ColumnWidth = 25;
                wsnew.get_Range("B4", m).RowHeight = 15;
                wsnew.get_Range("B4", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 153));
                //
                wsnew.get_Range("B5", m).Borders.Value = true;
                wsnew.get_Range("B5", m).ColumnWidth = 25;
                wsnew.get_Range("B5", m).RowHeight = 15;
                wsnew.get_Range("B5", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(204, 255, 204));
                //
                wsnew.get_Range("C4", m).Value2 = "Light yellow Color in header, should be Mandatory";
                wsnew.get_Range("C4", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C4", m).Borders.Value = true;
                wsnew.get_Range("C4", m).ColumnWidth = 55;
                wsnew.get_Range("C4", m).WrapText = true;
                wsnew.get_Range("C4", m).Font.Name = "Arial";
                wsnew.get_Range("C4", m).Font.Bold = true;
                wsnew.get_Range("C4", m).Font.Size = 10;
                //
                wsnew.get_Range("C5", m).Value2 = "Light Green Color in header, Optional";
                wsnew.get_Range("C5", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C5", m).Borders.Value = true;
                wsnew.get_Range("C5", m).ColumnWidth = 55;
                wsnew.get_Range("C5", m).WrapText = true;
                wsnew.get_Range("C5", m).Font.Name = "Arial";
                wsnew.get_Range("C5", m).Font.Bold = true;
                wsnew.get_Range("C5", m).Font.Size = 10;
                //
                //wsnew.get_Range("B3", "C6").BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black);
                wsnew.get_Range("B3", "C6").Borders.Value = true;
                //
                wsnew.get_Range("C8", m).Value2 = "ERROR Color in Excel format";
                wsnew.get_Range("C8", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C8", m).Borders.Value = true;
                wsnew.get_Range("C8", m).ColumnWidth = 55;
                wsnew.get_Range("C8", m).WrapText = true;
                wsnew.get_Range("C8", m).Font.Name = "Arial";
                wsnew.get_Range("C8", m).Font.Bold = true;
                wsnew.get_Range("C8", m).Font.Size = 12;
                wsnew.get_Range("C8", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C8", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                //
                wsnew.get_Range("B10", m).Value2 = "Color";
                wsnew.get_Range("B10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("B10", m).Borders.Value = true;
                wsnew.get_Range("B10", m).ColumnWidth = 55;
                wsnew.get_Range("B10", m).WrapText = true;
                wsnew.get_Range("B10", m).Font.Name = "Arial";
                wsnew.get_Range("B10", m).Font.Bold = true;
                wsnew.get_Range("B10", m).Font.Size = 10;
                wsnew.get_Range("B10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("B10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("C10", m).Value2 = "Message";
                wsnew.get_Range("C10", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C10", m).Borders.Value = true;
                wsnew.get_Range("C10", m).ColumnWidth = 55;
                wsnew.get_Range("C10", m).WrapText = true;
                wsnew.get_Range("C10", m).Font.Name = "Arial";
                wsnew.get_Range("C10", m).Font.Bold = true;
                wsnew.get_Range("C10", m).Font.Size = 10;
                wsnew.get_Range("C10", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                wsnew.get_Range("C10", m).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                //
                wsnew.get_Range("B12", m).Borders.Value = true;
                wsnew.get_Range("B12", m).ColumnWidth = 55;
                wsnew.get_Range("B12", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SteelBlue);
                //
                wsnew.get_Range("C12", m).Value2 = "Duplication (It should be unique)";
                wsnew.get_Range("C12", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C12", m).Borders.Value = true;
                wsnew.get_Range("C12", m).ColumnWidth = 55;
                wsnew.get_Range("C12", m).WrapText = true;
                wsnew.get_Range("C12", m).Font.Name = "Arial";
                wsnew.get_Range("C12", m).Font.Bold = true;
                wsnew.get_Range("C12", m).Font.Size = 10;
                //
                wsnew.get_Range("B14", m).Borders.Value = true;
                wsnew.get_Range("B14", m).ColumnWidth = 55;
                wsnew.get_Range("B14", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tan);
                //
                wsnew.get_Range("C14", m).Value2 = "Error in data length";
                wsnew.get_Range("C14", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C14", m).Borders.Value = true;
                wsnew.get_Range("C14", m).ColumnWidth = 55;
                wsnew.get_Range("C14", m).WrapText = true;
                wsnew.get_Range("C14", m).Font.Name = "Arial";
                wsnew.get_Range("C14", m).Font.Bold = true;
                wsnew.get_Range("C14", m).Font.Size = 10;
                //
                wsnew.get_Range("B16", m).Borders.Value = true;
                wsnew.get_Range("B16", m).ColumnWidth = 55;
                wsnew.get_Range("B16", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SpringGreen);
                //
                wsnew.get_Range("C16", m).Value2 = "Only numeric data allowed";
                wsnew.get_Range("C16", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C16", m).Borders.Value = true;
                wsnew.get_Range("C16", m).ColumnWidth = 55;
                wsnew.get_Range("C16", m).WrapText = true;
                wsnew.get_Range("C16", m).Font.Name = "Arial";
                wsnew.get_Range("C16", m).Font.Bold = true;
                wsnew.get_Range("C16", m).Font.Size = 10;
                //
                wsnew.get_Range("B18", m).Borders.Value = true;
                wsnew.get_Range("B18", m).ColumnWidth = 55;
                wsnew.get_Range("B18", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Chocolate);
                //
                wsnew.get_Range("C18", m).Value2 = "Invalid Data";
                wsnew.get_Range("C18", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C18", m).Borders.Value = true;
                wsnew.get_Range("C18", m).ColumnWidth = 55;
                wsnew.get_Range("C18", m).WrapText = true;
                wsnew.get_Range("C18", m).Font.Name = "Arial";
                wsnew.get_Range("C18", m).Font.Bold = true;
                wsnew.get_Range("C18", m).Font.Size = 10;
                //
                wsnew.get_Range("B20", m).Borders.Value = true;
                wsnew.get_Range("B20", m).ColumnWidth = 55;
                wsnew.get_Range("B20", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                //
                wsnew.get_Range("C20", m).Value2 = "Missing Data";
                wsnew.get_Range("C20", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C20", m).Borders.Value = true;
                wsnew.get_Range("C20", m).ColumnWidth = 55;
                wsnew.get_Range("C20", m).WrapText = true;
                wsnew.get_Range("C20", m).Font.Name = "Arial";
                wsnew.get_Range("C20", m).Font.Bold = true;
                wsnew.get_Range("C20", m).Font.Size = 10;
                //
                wsnew.get_Range("B22", m).Borders.Value = true;
                wsnew.get_Range("B22", m).ColumnWidth = 55;
                wsnew.get_Range("B22", m).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato);
                //
                wsnew.get_Range("C22", m).Value2 = "Invalid Format";
                wsnew.get_Range("C22", m).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                wsnew.get_Range("C22", m).Borders.Value = true;
                wsnew.get_Range("C22", m).ColumnWidth = 55;
                wsnew.get_Range("C22", m).WrapText = true;
                wsnew.get_Range("C22", m).Font.Name = "Arial";
                wsnew.get_Range("C22", m).Font.Bold = true;
                wsnew.get_Range("C22", m).Font.Size = 10;
                //
                wsnew.get_Range("B8", "C27").Borders.Value = true;
                //
                //wsnew.get_Range("A1", "F61").Locked = false;
                //wsnew.get_Range("A1", "F61").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //                        
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

        #endregion

        #region PROTECT WORKSHEET
        private bool PROTECT_WORKSHEET(string ExcelFilePath, string SheetName)
        {
            try
            {
                //--
                string filename = @"" + ExcelFilePath;
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();

                if (excelapp == null) return false;

                excelapp.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Workbooks wbs = excelapp.Workbooks;

                Microsoft.Office.Interop.Excel.Workbook wb = wbs.Open(filename,
                                             m, m, m, m, m, m,
                                             Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                             m, m, m, m, m, m, m);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //This is the offending line:

                Microsoft.Office.Interop.Excel.Worksheet wsnew = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
                wsnew.Name = SheetName;
                //

                //wsnew.get_Range("A1", "M100").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = false;
                //wsnew.get_Range("A1", "M1").Locked = true;
                //wsnew.Protect("JAYA", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                //
                wb.Save();
                wb.Close(m, m, m);

                wb = null;
                wbs = null;

                excelapp.Quit();
                excelapp = null;
                //--
                return true;
            }
            catch
            {
                this.Cursor = Cursors.Default;
                //
                cmnService.J_UserMessage("Excel file creation failed");
                //
                return false;
            }
        }

        #endregion

       
        #endregion 

        #endregion


    }
}