
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Dipak Himmatramka
Module Name		: FAYear Master
Version			: 2.0
Start Date		: 05-08-2010
End Date		: 05-08-2010
Tables Used     : MST_FAYEAR
Module Desc		: VIEW OF THE FA YEAR
________________________________________________________________________________________________________

*/

#endregion

#region Reffered Namespaces

using System;
    using System.Collections.Generic;   
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Data.SqlClient;
    using System.Globalization;
    
    //~~~~ User Namespaces ~~~~
    using BillingSystem.FormRpt;    
    using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormPar
{
    public partial class MstFAYear : BillingSystem.FormGen.GenForm
    {
        #region Constructor
        public MstFAYear()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
        
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        JAYA.VB.JVBCommon mainVB = new JAYA.VB.JVBCommon();
        
        long lngSearchId;					//For Storing the Id
        string strSQL;						//For Storing the Local SQL Query
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        
        DataSet dsetGridClone = new DataSet();
        string[,] strMatrix = null;

        #endregion

        #region Event Handlers

        #region MstFAYear_Load
        private void MstFAYear_Load(object sender, EventArgs e)
        {
            lblMode.Text = J_Mode.ViewListing;
            cmnService.J_StatusButton(this, lblMode.Text);
            
            BtnEdit.BackColor = Color.LightGray;
            BtnEdit.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            BtnDelete.Enabled = false;
            
            ControlVisible(false);
            
            // set the Help Grid Column Header Text & behavior
            // (0) Header Text
            // (1) Width
            // (2) Format
            // (3) Alignment
            // (4) NullToText
            // (5) Visible
            // (6) AutoSizeMode
            string[,] strMatrix1 = {{"FA Id", "0", "", "Right", "", "", ""},
							        {"Begin Date", "315", "dd/MM/yyyy", "", "", "", ""},
            				        {"End Date", "315", "dd/MM/yyyy", "", "", "", ""},
            				        {"Lock Date", "315", "dd/MM/yyyy", "", "", "", ""}};
            strMatrix = strMatrix1;
            
            strOrderBy = "FAYEAR_ID";
            strQuery = "SELECT FAYEAR_ID," +
                "              FA_BEG_DATE," +
                "              FA_END_DATE," +
                "              FA_LOCK_DATE " +
                "       FROM   MST_FAYEAR ";
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref this.ViewGrid, strSQL, strMatrix);

            lblTitle.Text = this.Text;
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region BtnRefresh_Click
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            lblMode.Text = J_Mode.ViewListing;
            cmnService.J_StatusButton(this, lblMode.Text);

            BtnEdit.Enabled = false;
            BtnEdit.BackColor = Color.LightGray;
            BtnDelete.BackColor = Color.LightGray;
            BtnDelete.Enabled = false;
            
            lblSearchMode.Text = J_Mode.General;

            strSQL = strQuery + "order by " + strOrderBy;
            //---------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref this.ViewGrid, strSQL, strMatrix);
            if (dsetGridClone == null) return;
            //---------------------------------------------------------
            dmlService.J_setGridPosition(ref this.ViewGrid, dsetGridClone, "FAYEAR_ID", lngSearchId);
        }
        #endregion

        #region BtnPrint_Click
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            cmnService.J_ShowChildReportForm(J_Var.frmMain, J_Reports.FAYear, this.Text);
        }
        #endregion

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, System.EventArgs e)
        {
            if (Convert.ToInt64(ViewGrid.CurrentRowIndex) < 0) return;
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0]);

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }
        #endregion

        #region ViewGrid_CurrentCellChanged

        private void ViewGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            lngSearchId = Convert.ToInt64(ViewGrid[ViewGrid.CurrentRowIndex, 0]);
        }

        #endregion

        #endregion

        #region User Defined Functions
        
        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #endregion

    }
}

