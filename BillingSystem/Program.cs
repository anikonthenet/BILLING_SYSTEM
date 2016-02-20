
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: Program
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Program
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using BillingSystem.FormCmn;
using BillingSystem.FormSys;
using BillingSystem.Classes;

#endregion

namespace BillingSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // set the application name
            J_Var.J_pApplicationName = "BS v1.0";

            // set the project name
            J_Var.J_pProjectName = "Billing System";
            
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // start block
            // set some parameters
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            // set the Zip password
            J_Var.J_pZipFilePassword = "jcs";

            // set the Login Screen come or not
            //J_Var.J_pLoginScreen = J_LoginScreen.NO;

            #region FOR MOCROSOFT ACCESS DATABASE

            // =========================================
            // FOR MOCROSOFT ACCESS DATABASE
            // =========================================

            J_Var.J_pMsAccessDatabaseName     = "BillingSystem.mdb";
            J_Var.J_pMsAccessDatabasePassword = "";

            J_Var.J_pApplicationType        = J_ApplicationType.StandAlone_Network;
            J_Var.J_pDatabaseType           = J_DatabaseType.SqlServer;
            J_Var.J_pConnectionProviderType = J_ConnectionProviderType.Sql;
            
            // =========================================
            // =========================================

            #endregion

            #region FOR MOCROSOFT SQL SERVER DATABASE

            // =========================================
            // FOR MOCROSOFT SQL SERVER DATABASE
            // =========================================

            //J_Var.J_pApplicationType = J_ApplicationType.StandAlone_Network;
            //J_Var.J_pDatabaseType = J_DatabaseType.SqlServer;
            //J_Var.J_pConnectionProviderType = J_ConnectionProviderType.Sql;

            // =========================================
            // =========================================

            #endregion

            // set the xml file name as connection to database
            J_Var.J_pXmlConnectionFileName = "_JS_BS.xml";
            
            // set the xml file name as Branch Information
            J_Var.J_pXmlBranchInfoFileName = "_JS_BS_B.xml";

            // set the command time-out
            J_Var.J_pCommandTimeout = 99999;

            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            // end block
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            
            // declare & initialization objects
            DMLService dmlService    = new DMLService();
            CommonService cmnService = new CommonService();
            DateService dtService    = new DateService();
            
            // set the Operating Syatem type [32 bit | 64 bit]
            J_Var.J_pOSType = cmnService.J_GetOSType();
            
            // declare & initialization variable
            string strSQL = string.Empty;

            // To Check the DateTime Format
            if (dtService.J_SystemDateFormatCheck_dd_MM_yyyy() == false)
                return;

            // to check the application type as StandAlone & SingleMachine
            if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachine)
            {
                // to check the microsoft database file is exist or not
                if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pMsAccessDatabaseName) == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Database file does not exist.\nPlease check the database file");
                    return;
                }
                
                // to check the connection is possible or not
                if (dmlService.J_ValidateConnection() == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database.\nPlease check the database");
                    return;
                }

                // to check the MST_FAYEAR table is exist or not
                if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR") == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database structure.\nPlease check the database");
                    return;
                }
                
                // declare & initialize the hashtable object to create the xml file
                Hashtable nameValue = new Hashtable();

                // store values to hashtable
                nameValue.Add("SERVERNAME", "");
                nameValue.Add("DATABASENAME", Application.StartupPath);
                nameValue.Add("USERNAME", J_Var.J_pMsAccessDatabaseName);
                nameValue.Add("PASSWORD", J_Var.J_pMsAccessDatabasePassword);

                // declare & initialize the object of XMLService
                XMLService objxml = new XMLService();
                
                // create the xml file to connect to the database
                objxml.J_CreateXMLFile(nameValue, Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                
                if (dmlService.J_IsRecordExist("MST_FAYEAR") == true)
                {
                    dmlService.Dispose();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

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

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                    frm.ShowDialog();
                    frm.Dispose();
                }
                return;
            }

            // to check the application type as StandAlone, SingleMachine & Browser to Config window
            if (J_Var.J_pApplicationType == J_ApplicationType.StandAlone_SingleMachineBrowser)
            {
                // to check the microsoft xml file is exist or not
                if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName) == false)
                {
                    dmlService.Dispose();
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    SysServerInfoLocalBrowser frmLocalBrowser = new SysServerInfoLocalBrowser();
                    frmLocalBrowser.ShowDialog();
                    frmLocalBrowser.Dispose();
                    return;
                }

                DataSet dsUserInfo = dmlService.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
                if (dsUserInfo == null) return;
                J_Var.J_pDatabaseName = cmnService.J_Decode(dsUserInfo.Tables[0].Rows[0][cmnService.J_Encode("DATABASENAME")].ToString());

                // to check the microsoft database file is exist or not
                if (cmnService.J_IsFileExist(J_Var.J_pDatabaseName + "/" + J_Var.J_pMsAccessDatabaseName) == false)
                {
                    dmlService.Dispose();
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    SysServerInfoLocalBrowser frmLocalBrowser = new SysServerInfoLocalBrowser();
                    frmLocalBrowser.ShowDialog();
                    frmLocalBrowser.Dispose();
                    return;
                }

                // to check the connection is possible or not
                if (dmlService.J_ValidateConnection() == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database.\nPlease check the database");
                    return;
                }

                // to check the MST_FAYEAR table is exist or not
                if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR") == false)
                {
                    dmlService.Dispose();
                    cmnService.J_UserMessage("Invalid database structure.\nPlease check the database");
                    return;
                }

                // check atleast one fayear data exist or not.
                if (dmlService.J_IsRecordExist("MST_FAYEAR") == true)
                {
                    dmlService.Dispose();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

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

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                    frm.ShowDialog();
                    frm.Dispose();
                }
                return;
            }

            // to check the microsoft xml file is exist or not
            if (cmnService.J_IsFileExist(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName) == true)
            {
                if (dmlService.J_ValidateConnection() == true)
                {
                    // check the MST_FAYEAR table exist or not
                    if (dmlService.J_IsDatabaseObjectExist("MST_FAYEAR") == true)
                    {
                        if (dmlService.J_IsRecordExist("MST_FAYEAR") == true)
                        {
                            dmlService.Dispose();
                            
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            CmnLogin frm = new CmnLogin();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        else
                        {
                            dmlService.Dispose();
                            
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            CmnCreate1stFAYear frm = new CmnCreate1stFAYear();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        return;
                    }
                }
            }
            
            dmlService.Dispose();

            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SysServerInfoLocal frmLocal = new SysServerInfoLocal();
                frmLocal.ShowDialog();
                frmLocal.Dispose();
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SysServerInfoNetwork frmNetwork = new SysServerInfoNetwork();
                frmNetwork.ShowDialog();
                frmNetwork.Dispose();
            }
            return;

        }
    }
}