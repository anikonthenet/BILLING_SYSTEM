
#region Developer Information

/*_________________________________________________________________________________________________________
Developed By   : Ripan Paul
Module Name    : MstBackup
Start Date     : 23/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : Taking Backup
_________________________________________________________________________________________________________*/

#endregion

#region Refered Namespaces & Classes

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace eBackup.Backup
{
    public partial class MstBackup : Form
    {
        
        #region J_Colon
        public enum J_Colon
        {
            YES,
            NO
        }
        #endregion

        #region MstBackup
        public MstBackup()
        {
            InitializeComponent();


            J_Var.J_pMsAccessDatabaseName  = "eMFBranch.mdb";
            J_Var.J_pXmlBranchInfoFileName = "_JS_eMF_B.xml";
            J_Var.J_pXmlConnectionFileName = "_JS_eMF.xml";
            J_Var.J_pExeFileName           = "MFMSBranch";

            //-- StandAlone_Network
            if (File.Exists(Application.StartupPath + "/" + J_Var.J_pXmlBranchInfoFileName) == true)
            {
                DataSet dsBranchInfo = this.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlBranchInfoFileName);
                if (dsBranchInfo == null) return;
                J_Var.J_pBranchCode = this.J_Decode(dsBranchInfo.Tables[0].Rows[0][this.J_Encode("BRANCHINFO")].ToString());
            }

            lblInfo.Text = "Make sure that " + J_Var.J_pExeFileName + " v1.0 Application should be closed in all Client Machines.";
            lblMessage.Visible = false;

            
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            
            if (File.Exists(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName) == false)
            {
                MessageBox.Show("Connection file does not exist in source path.", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnCancel.Select();
                return;
            }
            
            DataSet dsBackup = this.J_ConvertXmlToDataSet(Application.StartupPath + "/" + J_Var.J_pXmlConnectionFileName);
            if (dsBackup == null) return;

            // StandAlone_SingleMachine
            // StandAlone_SingleMachineBrowser
            J_Var.J_pDatabasePath = this.J_Decode(dsBackup.Tables[0].Rows[0][this.J_Encode("DATABASENAME")].ToString());
            
            // StandAlone_Network
            //J_Var.J_pDatabasePath = this.J_ConvertMsAccessDatabasePath(this.J_Decode(dsBackup.Tables[0].Rows[0][this.J_Encode("DATABASENAME")].ToString()), J_Colon.YES);

            if (File.Exists(J_Var.J_pDatabasePath + "/" + J_Var.J_pMsAccessDatabaseName) == false)
            {
                MessageBox.Show("Database does not exist in source path.", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnCancel.Select();
                return;
            }

            string strFolderPath = this.J_OpenFolderDialog();
            if (strFolderPath == "")
            {
                MessageBox.Show("Select Folder to Save Backup File", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnBackup.Select();
                return;
            }


            //string strDestinationPath = strFolderPath + "/" + J_Var.J_pBranchCode + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".mdb";
            //File.Copy(J_Var.J_pDatabasePath + "/" + J_Var.J_pMsAccessDatabaseName, strDestinationPath, true);

            
            //================================================================================

            string strFolderName = J_Var.J_pBranchCode + "-BK-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now);
            this.J_CreateDirectory(strFolderPath + "\\" + strFolderName);

            string strDestinationPath = strFolderPath + "\\" + strFolderName + "/" + J_Var.J_pBranchCode + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now) + ".mdb";
            File.Copy(J_Var.J_pDatabasePath + "/" + J_Var.J_pMsAccessDatabaseName, strDestinationPath, true);


            this.J_Zip(strFolderPath, strFolderName, "\\" + strFolderName + ".zip");
            this.J_DeleteDirectory(strFolderPath + "\\" + strFolderName);

            lblMessage.Visible = true;
            lblMessage.Text = "Backup completed.";

            //================================================================================

            //MessageBox.Show("Backup completed.......", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);


            
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

            if (File.Exists(Application.StartupPath + "/" + J_Var.J_pExeFileName + ".exe") == true)
                Process.Start(Application.StartupPath + "/" + J_Var.J_pExeFileName + ".exe");
        }
        #endregion


        #region J_Left
        public string J_Left(string String, int length)
        {
            return String.Substring(0, length);
        }
        #endregion

        #region MID [ OVERLOADED METHOD ]

        #region J_Mid
        public string J_Mid(string String, int startIndex)
        {
            return this.J_Mid(String, startIndex, 0);
        }
        #endregion

        #region J_Mid
        public string J_Mid(string String, int startIndex, int length)
        {
            return String.Substring(startIndex, length);
        }
        #endregion

        #endregion

        #region J_Encode
        public string J_Encode(string stringText)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(stringText);
            return Convert.ToBase64String(encbuff);
        }
        #endregion

        #region J_Decode
        public string J_Decode(string stringText)
        {
            byte[] decbuff = Convert.FromBase64String(stringText);
            return Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        #region OPEN FOLDER DIALOG [ OVERLOADED METHOD ]

        #region J_OpenFolderDialog [1]
        public string J_OpenFolderDialog()
        {
            return this.J_OpenFolderDialog("Select Folder to Save Backup File");
        }
        #endregion

        #region J_OpenFolderDialog [2]
        public string J_OpenFolderDialog(string Description)
        {
            FolderBrowserDialog openFolderPath = new FolderBrowserDialog();
            openFolderPath.Description = Description;
            openFolderPath.ShowDialog();
            return Convert.ToString(openFolderPath.SelectedPath);
        }
        #endregion

        #endregion

        #region CONVERT XML TO DATA SET [ OVERLOADED METHOD ]

        #region CONVERT XML TO DATA SET [1]
        public DataSet J_ConvertXmlToDataSet(string xmlPhysicalFilePath)
        {
            return J_ConvertXmlToDataSet(xmlPhysicalFilePath, XmlReadMode.Auto);
        }
        #endregion

        #region CONVERT XML TO DATA SET [2]
        public DataSet J_ConvertXmlToDataSet(string xmlPhysicalFilePath, XmlReadMode readMode)
        {
            DataSet J_DataSet = new DataSet();
            J_DataSet.ReadXml(xmlPhysicalFilePath, readMode);
            return J_DataSet;
        }
        #endregion

        #endregion

        #region J_ConvertMsAccessDatabasePath
        public string J_ConvertMsAccessDatabasePath(string MsAccessDatabasePath, J_Colon colon)
        {
            if (colon == J_Colon.YES)
                return this.J_Left(MsAccessDatabasePath, 1) + ":" + this.J_Mid(MsAccessDatabasePath, 1, MsAccessDatabasePath.Length - 1);
            else if (colon == J_Colon.NO)
                return this.J_Left(MsAccessDatabasePath, 1) + this.J_Mid(MsAccessDatabasePath, 2, MsAccessDatabasePath.Length - 2);
            else
                return "";
        }
        #endregion

        #region CREATE ZIP FILE [ OVERLOADED METHOD ]

        #region J_Zip [1]
        public void J_Zip(string Path, string FolderName, string outputPathAndFile)
        {
            this.J_Zip(Path, FolderName, outputPathAndFile, "");
        }
        #endregion

        #region J_Zip [2]
        public void J_Zip(string Path, string FolderName, string outputPathAndFile, string password)
        {
            FileStream ostream;
            byte[] obuffer;
            ZipEntry oZipEntry;
            ArrayList arrList;
            ZipOutputStream oZipStream;

            int TrimLength = 0;
            string outPath = "";

            // generate file list
            arrList = J_GenerateFileList(Path + "\\" + FolderName);
            TrimLength = (Directory.GetParent(Path + "\\" + FolderName)).ToString().Length + 1;
            outPath = Path + @"\" + outputPathAndFile;

            // create zip stream
            oZipStream = new ZipOutputStream(File.Create(outPath));
            if (password != "" && password != null && password != String.Empty)
                oZipStream.Password = password;

            // maximum compression
            oZipStream.SetLevel(9);

            // for each file, generate a zipentry
            foreach (string Fil in arrList)
            {
                oZipEntry = new ZipEntry(Fil.Remove(0, TrimLength));
                oZipStream.PutNextEntry(oZipEntry);

                // if a file ends with '/' its a directory
                if (!Fil.EndsWith(@"/"))
                {
                    ostream = File.OpenRead(Fil);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);

                    ostream.Close();
                }
            }

            oZipStream.Finish();
            oZipStream.Close();

        }
        #endregion

        #endregion

        #region J_GenerateFileList
        private ArrayList J_GenerateFileList(string Path)
        {
            ArrayList fils = new ArrayList();
            bool Empty = true;

            // add each file in directory
            foreach (string file in Directory.GetFiles(Path))
            {
                fils.Add(file);
                Empty = false;
            }

            if (Empty)
            {
                // if directory is completely empty, add it
                if (Directory.GetDirectories(Path).Length == 0)
                    fils.Add(Path + @"/");
            }

            foreach (string dirs in Directory.GetDirectories(Path)) // recursive
            {
                foreach (object obj in J_GenerateFileList(dirs))
                    fils.Add(obj);
            }
            return fils;
        }
        #endregion

        #region J_CreateDirectory
        public void J_CreateDirectory(string Path)
        {
            if (Directory.Exists(Path) == false)
                Directory.CreateDirectory(Path);
        }
        #endregion

        #region J_DeleteDirectory
        public void J_DeleteDirectory(string Path)
        {
            if (Directory.Exists(Path) == true)
                Directory.Delete(Path, true);
        }
        #endregion


    }


    #region Sealed Class
    public sealed class J_Var
    {
        #region Private Variable Declaration

        private static string XmlConnectionFileName = "_JS_eMF.xml";
        private static string XmlBranchInfoFileName = "_JS_eMF_B.xml";
        private static string DatabasePath = "";
        private static string MsAccessDatabaseName = "eMFBranch.mdb";
        private static string BranchCode = "JS";
        private static string ExeFileName = "MFMSBranch";

        #endregion

        #region USER DEFINE PROPERTIES

        #region XML CONNECTION FILE NAME
        public static string J_pXmlConnectionFileName
        {
            get
            {
                return XmlConnectionFileName;
            }
            set
            {
                XmlConnectionFileName = value;
            }
        }
        #endregion

        #region XML BRANCH INFO FILE NAME
        public static string J_pXmlBranchInfoFileName
        {
            get
            {
                return XmlBranchInfoFileName;
            }
            set
            {
                XmlBranchInfoFileName = value;
            }
        }
        #endregion

        #region DATABASE PATH
        public static string J_pDatabasePath
        {
            get
            {
                return DatabasePath;
            }
            set
            {
                DatabasePath = value;
            }
        }
        #endregion

        #region MS ACCESS DATABASE NAME
        public static string J_pMsAccessDatabaseName
        {
            get
            {
                return MsAccessDatabaseName;
            }
            set
            {
                MsAccessDatabaseName = value;
            }
        }
        #endregion

        #region BRANCH CODE
        public static string J_pBranchCode
        {
            get
            {
                return BranchCode;
            }
            set
            {
                BranchCode = value;
            }
        }
        #endregion

        #region APPLICATION FILE NAME
        public static string J_pExeFileName
        {
            get
            {
                return ExeFileName;
            }
            set
            {
                ExeFileName = value;
            }
        }
        #endregion

        #endregion
    }
    #endregion


}