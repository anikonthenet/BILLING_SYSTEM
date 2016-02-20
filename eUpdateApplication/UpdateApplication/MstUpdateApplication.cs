
#region Developer Information

/*_________________________________________________________________________________________________________
Developed By   : Ripan Paul
Module Name    : MstUpdateApplication
Start Date     : 23/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : Updating the Application
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

using System.IO;
using System.Diagnostics;

#endregion

namespace eUpdateApplication.UpdateApplication
{
    public partial class MstUpdateApplication : Form
    {

        #region DECLERATION VARIABLES


        #endregion

        #region MstUpdateApplication
        public MstUpdateApplication()
        {
            InitializeComponent();

            J_Var.J_pProjectName = "Update MFMS v1.0 Application";
            J_Var.J_pExeFileName = "MFMSBranch";

        }
        #endregion

        #region MstUpdateApplication_Load
        private void MstUpdateApplication_Load(object sender, EventArgs e)
        {
            this.Text = J_Var.J_pProjectName;
            txtBrowser.Text = "";
            BtnBrowser.Select();
        }
        #endregion

        #region BtnBrowser_Click
        private void BtnBrowser_Click(object sender, EventArgs e)
        {
            txtBrowser.Text = this.J_OpenFileDialog("Application File | *.exe", "Application File | *.exe", "Choose the exe file to update");
            if (txtBrowser.Text == "")
                BtnBrowser.Select();
            else
                BtnUpdate.Select();
        }
        #endregion

        #region BtnUpdate_Click
        private void BtnUpdate_Click(object sender, EventArgs e)
        {

            //+++++++++++++++++++++++++++++++++++++++++++++++++
            // R&D to get exe file version
            //FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "/" + J_Var.J_pExeFileName + ".exe");
            //MessageBox.Show(versionInfo.FileVersion);
            //+++++++++++++++++++++++++++++++++++++++++++++++++
            
            
            if (this.J_IsProcessOpen(J_Var.J_pExeFileName) == true)
            {
                MessageBox.Show("Application is running.\nPlease close the application.", J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string strSourceFilePath = txtBrowser.Text;
            if (strSourceFilePath == "" || strSourceFilePath == null) return;
            string strDestinationFolderPath = Application.StartupPath;

            if (File.Exists(strSourceFilePath) == false)
            {
                MessageBox.Show("Selected file does not exist.", J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnBrowser.Select();
                return;
            }

            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(strSourceFilePath);
            if (J_Var.J_pExeFileName + ".exe" != Convert.ToString(fileVersionInfo.InternalName))
            {
                MessageBox.Show("Invalid application file.\nPlease select valid application file.", J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.J_GetDirectoryName(strSourceFilePath).Trim().ToUpper() == strDestinationFolderPath.Trim().ToUpper())
            {
                MessageBox.Show("Selected file is destination path.\nPlease select another source path.", J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnBrowser.Select();
                return;
            }

            if (File.Exists(strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe") == true)
            {
                File.Copy(strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe", strDestinationFolderPath + "/" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + string.Format("{0:HHmmss}", System.DateTime.Now) + "-" + J_Var.J_pExeFileName + ".exe", true);
                File.Delete(strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe");
            }
            File.Copy(strSourceFilePath, strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe");

            if (MessageBox.Show("Want to proceed....", J_Var.J_pProjectName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;
            
            MessageBox.Show("Application successfully updated", J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (File.Exists(strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe") == true)
            {
                this.Close();
                this.Dispose();

                Process.Start(strDestinationFolderPath + "/" + J_Var.J_pExeFileName + ".exe");
            }
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

        #region OPEN FILE DIALOG [ OVERLOADED METHOD ]

        #region J_OpenFileDialog [1]
        public string J_OpenFileDialog()
        {
            return this.J_OpenFileDialog("All Files|*.*", null, "Source File");
        }
        #endregion

        #region J_OpenFileDialog [2]
        public string J_OpenFileDialog(string FilterText)
        {
            return this.J_OpenFileDialog(FilterText, null, "Source File");
        }
        #endregion

        #region J_OpenFileDialog [2]
        public string J_OpenFileDialog(string FilterText, string DefaultExtension)
        {
            return this.J_OpenFileDialog(FilterText, DefaultExtension, "Source File");
        }
        #endregion

        #region J_OpenFileDialog [4]
        public string J_OpenFileDialog(string FilterText, string DefaultExtension, string DialogBoxTitle)
        {
            OpenFileDialog openFilePath = new OpenFileDialog();
            openFilePath.FileName = "";
            openFilePath.Title = DialogBoxTitle;
            openFilePath.Filter = FilterText;

            if (DefaultExtension != "" && DefaultExtension != null)
                openFilePath.DefaultExt = DefaultExtension;

            openFilePath.ShowDialog();
            return Convert.ToString(openFilePath.FileName);
        }
        #endregion

        #endregion

        #region J_IsProcessOpen
        public bool J_IsProcessOpen(string ProcessName)
        {
            foreach (Process process in Process.GetProcesses())
                if (process.ProcessName.Contains(ProcessName))
                    return true;
            return false;
        }
        #endregion

        #region J_GetDirectoryName
        public string J_GetDirectoryName(string FilePath)
        {
            if (FilePath == "" | FilePath == null) return "";
            FileInfo fileinfo = new FileInfo(FilePath);
            return Convert.ToString(fileinfo.DirectoryName);
        }
        #endregion
        

    }

    #region Sealed Class
    public sealed class J_Var
    {

        #region Private Variable Declaration

        private static string ProjectName = "Update Application";
        private static string ExeFileName = "";

        #endregion

        #region USER DEFINE PROPERTIES

        #region PROJECT NAME
        public static string J_pProjectName
        {
            get
            {
                return ProjectName;
            }
            set
            {
                ProjectName = value;
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