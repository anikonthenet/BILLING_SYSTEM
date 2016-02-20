using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Windows.Forms;

namespace UninstallBS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Procedure for Uninstall Application
            Process uninstallProcess = new Process();
            uninstallProcess.StartInfo.FileName = "MsiExec.exe";
            uninstallProcess.StartInfo.Arguments = "/I{E8C4358B-A22A-415B-8275-D4307F8AA471}";
            uninstallProcess.StartInfo.UseShellExecute = false;
            uninstallProcess.Start();

        }
    }
}
