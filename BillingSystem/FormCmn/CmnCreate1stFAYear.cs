
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Module Name		: Create 1st FA Year
Version			: 2.0
Start Date		: 13-11-2008
End Date		: 
Module Desc		: Create 1st FA Year
_________________________________________________________________________________________________________

*/

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

//~~~~ User Namespaces ~~~~
using BillingSystem.FormCmn;
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormCmn
{
    public partial class CmnCreate1stFAYear : Form
    {

        #region Objects & Variables decleration

        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();

        #endregion

        #region Constructor
        public CmnCreate1stFAYear()
        {
            InitializeComponent();
        }
        #endregion

        #region CmnCreate1stFAYear_Load
        private void CmnCreate1stFAYear_Load(object sender, EventArgs e)
        {
            mskBeginingDate.Text = "";
            mskEndingDate.Text = "";
            mskBeginingDate.Select();
        }
        #endregion

        #region BtnSubmit_Click
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Ending Date should be on & after Begining Date
            if (dtService.J_IsDateGreater(ref mskBeginingDate, ref mskEndingDate, "Begining Date", "Ending Date", "Ending Date should be on & after Begining Date !!", J_ShowMessage.YES) == false)
                return;
            
            // Transaction is begining
            dmlService.J_BeginTransaction();

            string strSQL = "INSERT INTO MST_FAYEAR (" +
                            "            FAYEAR_ID," +
                            "            FA_BEG_DATE," +
                            "            FA_END_DATE) " +
                            "     VALUES(" +
                            "            1," +
                            "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskBeginingDate.Text) + cmnService.J_DateOperator() + "," +
                            "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskEndingDate.Text) + cmnService.J_DateOperator() + ")";
            if (dmlService.J_ExecSql(strSQL) == false)
            {
                mskBeginingDate.Select();
                return;
            }
            
            // Transaction has been commited
            dmlService.J_Commit();
            
            // Close & Dispose the MstLogin Class
            this.dmlService.Dispose();
            this.Close();
            this.Dispose();
            
            // Call the login form
            CmnLogin frm = new CmnLogin();
            frm.ShowDialog();
            frm.Dispose();
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

        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion




    }
}