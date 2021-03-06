
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

// This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

// User Namespaces
using BillingSystem.FormCmn;
using BillingSystem.Classes;
using BillingSystem.FormTrn.PopUp;

using JAYA.VB;

#endregion

namespace BillingSystem.FormTrn.NormalEntries
{
    public partial class TrnInvoiceEntry : BillingSystem.FormGen.GenForm
    {

        #region System Generated Code
        public TrnInvoiceEntry()
        {
            InitializeComponent();
        }
        #endregion

        #region Objects & Variables decleration
         
        // Variables and objects declaration
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();       

        JVBCommon mainVB = new JVBCommon();
        
        string strSQL;			//For Storing the Local SQL Query

        int intTempGridPosition = 0;
        bool blnAmount;
        bool blnPaymentType = true;

        long SearchId = 0;
        int intEnabledBankBasedOnPaymentTypes = 0;
        int intBankInInvoiceFlag = 0;
        int intReferenceNoFlag = 0;

        //Added By dhrub On 21/04/2015
        bool blnInvoiceWiseReconciled = false; 
        #endregion

        #region ENUM decleration of Detail Grid Column
        //-- enum for setting detail grid column
        enum enmInvoiceEntry
        {

            INVOICE_DETAIL_ID = 1,
            SL_NO             = 2,
            ITEM_NAME         = 3,
            QUANTITY          = 4,
            UNIT              = 5,
            RATE              = 6,
            AMOUNT            = 7,
            REMARKS           = 8,
            ITEM_ID           = 9
        }

        enum enmInvoiceTax
        {
            TAX_ID     = 1,
            TAX_DESC   = 2,
            TAX_RATE   = 3,
            TAX_AMOUNT = 4,
        }
        #endregion

        #region User Defined Events

        #region TrnInvoiceEntry_Load
        private void TrnInvoiceEntry_Load(object sender, System.EventArgs e)
        {
            BtnAdd_Click(this, e);

            if (lblMode.Text == J_Mode.Add)
            {
                strSQL = "SELECT COMPANY_ID," +
                         "       COMPANY_NAME " +
                         "FROM   MST_COMPANY " +
                         "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                         "AND    COMPANY_ID = " + BS_CompanyId.PDSInfotech + " " +
                         "ORDER BY COMPANY_NAME ";
                if (dmlService.J_PopulateComboBox(strSQL, ref cmbCompany, 1) == false) return;
                
            }

            mskInvoiceDate.Text = DateTime.Now.ToString();

            cmbParty.Select();
            
            lblTitle.Text = this.Text;
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            // Mode
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            // Button behavior
            BtnCancel.Enabled = false;
            BtnCancel.BackColor = Color.LightGray;
            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            // Control visibity
            ControlVisible(true);

            // Clear all the Controls
            ClearControls();
            
            cmbCompany.Select();
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Mode
            lblMode.Text = J_Mode.Add;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            // Button behavior
            BtnCancel.Enabled = false;
            BtnCancel.BackColor = Color.LightGray;
            BtnDelete.Enabled = false;
            BtnDelete.BackColor = Color.LightGray;
            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

            // Clear all the Controls
            ClearControls();

            GetInvoiceNo();

            Enable(lblMode.Text);
            cmbCompany.Select();

        }
        #endregion

        #region BtnSave_Click
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Insert_Update_Delete_Data();
            GetInvoiceNo();
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lblMode.Text == J_Mode.Add)
            {
                cmnService.J_UserMessage("Please select the invoice to delete.");
                BtnEditInvoice.Select();

                BtnExit.Enabled = true;
                BtnExit.BackColor = Color.Lavender;

                return;
            }

            // Mode
            lblMode.Text = J_Mode.Delete;
            cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
            lblSearchMode.Text = J_Mode.General;

            Insert_Update_Delete_Data();
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


        #region cmbCompany_KeyUp
        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (cmbInvoiceSeries.Enabled == true) cmbInvoiceSeries.Select();
                else SendKeys.Send("{tab}");
            }
        }
        #endregion

        #region cmbCompany_KeyUp
        private void cmbCompany_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbCompany, e);
        }
        #endregion

        #region cmbCompany_Leave
        private void cmbCompany_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbCompany);
            object objRowView = cmbCompany.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound + " : " + cmbCompany.Text);
                cmbCompany.Focus();
            }
        }
        #endregion

        #region cmbCompany_SelectedIndexChanged
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex <= 0) cmnService.J_ClearComboBox(ref cmbInvoiceSeries);

            strSQL = "SELECT INVOICE_SERIES_ID," +
                     "       PREFIX " +
                     "FROM   MST_INVOICE_SERIES " +
                     "WHERE  BRANCH_ID  = " + J_Var.J_pBranchId + " " +
                     "AND    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "AND    FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                     "ORDER BY PREFIX DESC";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbInvoiceSeries, 1) == false) return;
        }
        #endregion 
        

        #region cmbInvoiceSeries_KeyUp
        private void cmbInvoiceSeries_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbInvoiceSeries, e);
        }
        #endregion

        #region cmbInvoiceSeries_Leave
        private void cmbInvoiceSeries_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbInvoiceSeries);
            object objRowView = cmbInvoiceSeries.SelectedItem;
            if (objRowView == null)
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound + " : " + cmbInvoiceSeries.Text);
                cmbInvoiceSeries.Focus();
            }
        }
        #endregion

        #region cmbInvoiceSeries_SelectedIndexChanged
        private void cmbInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvoiceNo();
        }
        #endregion 


        #region cmbParty_KeyUp
        private void cmbParty_KeyUp(object sender, KeyEventArgs e)
        {
            cmnService.J_AutoCompleteCombo_KeyUp(ref cmbParty, e);
        }
        #endregion

        #region cmbParty_Leave
        private void cmbParty_Leave(object sender, EventArgs e)
        {
            // Get the Selected Record from my Data Bound Combo (Return Type is Object)
            cmnService.J_AutoCompleteCombo_Leave(ref cmbParty);
            object objRowView = cmbParty.SelectedItem;
            if (objRowView == null)
            {
                // Added by Ripan Paul on 23-07-2011
                BS.BS_PartyName = cmbParty.Text;

                TrnPartySave frmPartySave = new TrnPartySave();
                frmPartySave.txtPartyName.Text = BS.BS_PartyName;
                frmPartySave.Text = "Party Master";
                frmPartySave.ShowDialog();

                strSQL = "SELECT PARTY_ID," +
                         "       PARTY_NAME " +
                         "FROM   MST_PARTY " +
                         "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                         "ORDER BY PARTY_NAME ";

                DMLService dml = new DMLService();
                if (dml.J_PopulateComboBox(strSQL, ref cmbParty) == false) return;
                cmbParty.Text = BS.BS_PartyName;
                dml.Dispose();

                cmbParty.Focus();
            }
        }
        #endregion

        #region cmbParty_SelectedIndexChanged
        private void cmbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParty.SelectedIndex <= 0)
            {
                lblPartyCategory.Text = "";
                return;
            }

            //Checking the Party category of the party selected
            strSQL = "SELECT PARTY_CATEGORY_DESCRIPTION " +
                     "FROM   MST_PARTY_CATEGORY, " +
                     "       MST_PARTY " +
                     "WHERE MST_PARTY_CATEGORY.PARTY_CATEGORY_ID  = MST_PARTY.PARTY_CATEGORY_ID " +
                     "AND   MST_PARTY.PARTY_ID = " + Support.GetItemData(cmbParty, cmbParty.SelectedIndex);

            lblPartyCategory.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));

        }
        #endregion


        #region txtRemarks_KeyPress
        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                setTextBoxInGrid();
            }
        }
        #endregion


        #region flxgrdDetails_ClickEvent
        private void flxgrdDetails_ClickEvent(object sender, EventArgs e)
        {
            if (flxgrdDetails.Row == 1 
                && (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                || flxgrdDetails.Col == (int)enmInvoiceEntry.RATE
                || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS))
                flxgrdDetails.RowSel = 1;
            setTextBoxInGrid();
        }
        #endregion

        #region flxgrdDetails_KeyPressEvent
        private void flxgrdDetails_KeyPressEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent e)
        {
            if (e.keyAscii == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.SL_NO
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.QUANTITY;
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.AMOUNT
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.REMARKS;
                    setTextBoxInGrid();
                }
            }
        }
        #endregion

        #region flxgrdDetails_MouseMoveEvent
        private void flxgrdDetails_MouseMoveEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEvent e)
        {
            cmnService.J_GridToolTip(flxgrdDetails, e.x, e.y);
        }
        #endregion

        #region flxgrdDetails_KeyDownEvent
        private void flxgrdDetails_KeyDownEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEvent e)
        {
            if (e.keyCode == 112 && flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME) PopulateItemMaster();
            if (e.keyCode == 46)
            {
                if (cmnService.J_UserMessage(J_Msg.WantToRemove,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    flxgrdDetails.Row = flxgrdDetails.RowSel;
                    flxgrdDetails.Col = flxgrdDetails.ColSel;
                    flxgrdDetails.Select();

                    return;
                }

                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_ID, "0");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_NAME, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.QUANTITY, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.UNIT, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.RATE, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.AMOUNT, "");
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.REMARKS, "");

                CalculateNetAmount();
            }
        }
        #endregion

        #region flxgrdDetails_DblClick
        private void flxgrdDetails_DblClick(object sender, EventArgs e)
        {
            if (flxgrdDetails.Col == (int)enmInvoiceEntry.ITEM_NAME) PopulateItemMaster();
        }
        #endregion


        #region txtDiscountRate_KeyPress
        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                if (txtDiscountAmount.ReadOnly == false) SendKeys.Send("{tab}");
                else
                {
                    flxgrdTax.Row = 1;
                    flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                    flxgrdTax.Select();
                }
            }
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,5,2", txtDiscountRate, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDiscountRate_TextChanged
        private void txtDiscountRate_TextChanged(object sender, EventArgs e)
        {
            if (cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) == 0) txtDiscountAmount.Text = "0.00";
            CalculateNetAmount();
        }
        #endregion

        #region txtDiscountRate_Leave
        private void txtDiscountRate_Leave(object sender, EventArgs e)
        {
            txtDiscountRate.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtDiscountRate)), "0.00");
        }
        #endregion

        #region txtDiscountAmount_KeyPress
        private void txtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
            {
                flxgrdTax.Row = 1;
                flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                flxgrdTax.Select();
            }
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtDiscountAmount, "") == false)
                e.Handled = true;
        }
        #endregion

        #region txtDiscountAmount_TextChanged
        private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtDiscountAmount_Leave
        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            txtDiscountAmount.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtDiscountAmount)), "0.00");
        }
        #endregion


        #region flxgrdTax_KeyPressEvent
        private void flxgrdTax_KeyPressEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent e)
        {
            if (e.keyAscii == 13)
            {
                if (flxgrdTax.get_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_DESC) == "") return;

                if (flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC
                    || flxgrdTax.Col == (int)enmInvoiceTax.TAX_RATE
                    || flxgrdTax.Col == (int)enmInvoiceTax.TAX_AMOUNT)
                {
                    if (flxgrdTax.RowSel == flxgrdTax.Rows - 1)
                    {
                        flxgrdTax.Rows = flxgrdTax.Rows + 1;
                        flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                        flxgrdTax.Row = Convert.ToInt32(flxgrdTax.Row + 1);
                        flxgrdTax.Select();
                    }
                    else
                    {
                        flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                        flxgrdTax.Row = Convert.ToInt32(flxgrdTax.Row + 1);
                        flxgrdTax.Select();
                    }
                }
            }
        }
        #endregion

        #region flxgrdTax_KeyDownEvent
        private void flxgrdTax_KeyDownEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyDownEvent e)
        {
            if (e.keyCode == 112 && flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC) PopulateTaxMaster();
            if (e.keyCode == 46)
            {
                if (cmnService.J_UserMessage(J_Msg.WantToRemove,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    flxgrdTax.Row = flxgrdTax.RowSel;
                    flxgrdTax.Col = flxgrdTax.ColSel;
                    flxgrdTax.Select();
                    
                    return;
                }

                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_ID, "0");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_DESC, "");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_RATE, "");
                flxgrdTax.set_TextMatrix(flxgrdTax.RowSel, (int)enmInvoiceTax.TAX_AMOUNT, "");
                
                CalculateNetAmount();
            }
        }
        #endregion

        #region flxgrdTax_DblClick
        private void flxgrdTax_DblClick(object sender, EventArgs e)
        {
            if (flxgrdTax.Col == (int)enmInvoiceTax.TAX_DESC) PopulateTaxMaster();
        }
        #endregion

        #region flxgrdTax_MouseMoveEvent
        private void flxgrdTax_MouseMoveEvent(object sender, AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseMoveEvent e)
        {
            cmnService.J_GridToolTip(flxgrdTax, e.x, e.y);
        }
        #endregion


        #region txtAdditionalCost_KeyPress
        private void txtAdditionalCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtAdditionalCost, "Y") == false)
                e.Handled = true;
        }
        #endregion

        #region txtAdditionalCost_TextChanged
        private void txtAdditionalCost_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtAdditionalCost_Leave
        private void txtAdditionalCost_Leave(object sender, EventArgs e)
        {
            txtAdditionalCost.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtAdditionalCost)), "0.00");
        }
        #endregion


        #region txtRoundedOff_KeyPress
        private void txtRoundedOff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
            if (mainVB.gTextBoxValidation(Convert.ToInt16(e.KeyChar), "N,10,2", txtRoundedOff, "Y") == false)
                e.Handled = true;
        }
        #endregion

        #region txtRoundedOff_TextChanged
        private void txtRoundedOff_TextChanged(object sender, EventArgs e)
        {
            CalculateNetAmount();
        }
        #endregion

        #region txtRoundedOff_Leave
        private void txtRoundedOff_Leave(object sender, EventArgs e)
        {
            txtRoundedOff.Text = cmnService.J_FormatToString(Convert.ToDouble(cmnService.J_NumericData(txtRoundedOff)), "0.00");
        }
        #endregion


        #region txtQty_KeyPress
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                    flxgrdDetails.Select();
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    flxgrdDetails.Col = (int)enmInvoiceEntry.REMARKS;
                    flxgrdDetails.Select();
                    setTextBoxInGrid();
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    double dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.AMOUNT) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.AMOUNT));

                    if (flxgrdDetails.get_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.ITEM_NAME) == "" && dblAmount == 0) return;

                    if (intTempGridPosition == flxgrdDetails.Rows - 1)
                    {
                        int intSLNO = Convert.ToInt32(flxgrdDetails.get_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.SL_NO));
                        
                        flxgrdDetails.Rows = flxgrdDetails.Rows + 1;
                        flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                        flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row + 1);
                        flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.SL_NO, Convert.ToString(intSLNO + 1));
                        flxgrdDetails.Select();
                    }
                    else
                    {
                        flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                        flxgrdDetails.Row = Convert.ToInt32(flxgrdDetails.Row + 1);
                        flxgrdDetails.Select();
                    }
                }
            }
            else
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "N,3,0", txtQty, "") == false) e.Handled = true;
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "N,10,2", txtQty, "") == false) e.Handled = true;
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    if (mainVB.gTextBoxValidation(Convert.ToInt32(e.KeyChar), "V,150,0", txtQty, "") == false) e.Handled = true;
                }
            }
        }
        #endregion

        #region txtQty_TextChanged
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (blnAmount == false)
            {
                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY)
                {
                    if (txtQty.Text == "" || txtQty.Text == "-" || txtQty.Text == "-." || txtQty.Text == ".")
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.QUANTITY, "0");
                    else
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.QUANTITY, (txtQty.Text == "" ? "0" : txtQty.Text));
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                {
                    if (txtQty.Text == "" || txtQty.Text == "-" || txtQty.Text == "-." || txtQty.Text == ".")
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.RATE, "0.00");
                    else
                        flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.RATE, (txtQty.Text == "" ? "0.00" : txtQty.Text));
                }
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                {
                    flxgrdDetails.set_TextMatrix(intTempGridPosition, (int)enmInvoiceEntry.REMARKS, txtQty.Text);
                }

                NumberFormatDetail(flxgrdDetails);
                CalculateNetAmount();

            }
        }
        #endregion

        #region txtQty_Leave
        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Visible = false;
        }
        #endregion


        #region BtnEditInvoice_Click
        private void BtnEditInvoice_Click(object sender, EventArgs e)
        {
            // Company
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the company.");
                cmbCompany.Select();
                return;
            }

            // set the Help Grid Column Header Text & behavior
            // (0) Header Text
            // (1) Width
            // (2) Format
            // (3) Alignment
            string[,] strMatrix = {{"Invoice Header Id", "0", "", "Right"},
                                   {"Invoice No.", "120", "", ""},
                                   {"Invoice Date", "80", "dd/MM/yyyy", ""},
                                   {"Party", "230", "", ""},
                                   {"Net Amount.", "80", "0.00", "Right"}};

            strSQL = "SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID AS INVOICE_HEADER_ID," +
                     "       TRN_INVOICE_HEADER.INVOICE_NO        AS INVOICE_NO," +
                     "       " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE," +
                     "       MST_PARTY.PARTY_NAME                 AS PARTY_NAME," +
                     "       TRN_INVOICE_HEADER.NET_AMOUNT        AS NET_AMOUNT " +
                     "FROM   TRN_INVOICE_HEADER," +
                     "       MST_PARTY " +
                     "WHERE  TRN_INVOICE_HEADER.PARTY_ID   = MST_PARTY.PARTY_ID " +
                     "AND    TRN_INVOICE_HEADER.COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "AND    TRN_INVOICE_HEADER.FAYEAR_ID  = " + J_Var.J_pFAYearId + " " +
                     "AND    TRN_INVOICE_HEADER.TRAN_TYPE  = 'INV' " +
                     "AND    TRN_INVOICE_HEADER.DELIVERY_MODE_ID = 0 " +
                     "ORDER BY TRN_INVOICE_HEADER.INVOICE_HEADER_ID DESC";

            CmnHelp objhelp = new CmnHelp("Invoice Details", true, 600);
            objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
            objhelp.J_pSearchColumnName = "Invoice No.";
            objhelp.J_pSearchColumnType = J_ColumnType.String;
            objhelp.ShowDialog();

            if (J_Var.J_pMatrix[0] != null)
            {
                // Mode
                lblMode.Text = J_Mode.Edit;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;

                // Button behavior
                BtnCancel.Enabled = true;
                BtnCancel.BackColor = Color.Lavender;
                //BtnDelete.Enabled = true;
                //BtnDelete.BackColor = Color.Lavender;
                
                // Clear all the Controls
                ClearControls();

                // display respective data
                SearchId = Convert.ToInt64(Convert.ToString(J_Var.J_pMatrix[0]));
                if(ShowRecord(SearchId) == false) return;

                // enable controls
                Enable(lblMode.Text);
                mskInvoiceDate.Select();
            }

            Set_SLNO();

            BtnExit.Enabled = true;
            BtnExit.BackColor = Color.Lavender;

        }
        #endregion


        #region Control_KeyPress
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13) SendKeys.Send("{tab}");
        }
        #endregion

        #region cmbPaymentType_SelectedIndexChanged
        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnPaymentType == false) return;
            if (cmbPaymentType.SelectedIndex <= 0)
            {
                lblAutoCollectionPostFlag.Text = "0";
                lblSundryPartyFlag.Text = "0";
                return;
            }
            //
            lblSundryPartyFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT SUNDRY_PARTY_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            lblAutoCollectionPostFlag.Text = Convert.ToString(dmlService.J_ExecSqlReturnScalar("SELECT AUTO_COLLECTION_POST_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            intBankInInvoiceFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT BANK_IN_INVOICE_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            intReferenceNoFlag = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT REFERENCE_NO_FLAG FROM MST_PAYMENT_TYPE WHERE PAYMENT_TYPE_ID = " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex)));
            //

        }
        #endregion

        
        #endregion

        #region User Define Functions

        #region PopulateItemMaster
        private void PopulateItemMaster()
        {
            if (cmbCompany.SelectedIndex <= 0) return;

            // set the Help Grid Column Header Text & behavior
            // (0) Header Text
            // (1) Width
            // (2) Format
            // (3) Alignment
            string[,] strMatrix = {{"Item Id", "0", "", "Right"},
                                   {"Item Name", "300", "", ""},
                                   {"Rate", "100", "0.00", "Right"},
                                   {"Unit", "80", "0.00", ""}};

            strSQL = "SELECT ITEM_ID," +
                     "       ITEM_NAME," +
                     "       RATE," +
                     "       UNIT " +
                     "FROM   MST_ITEM " +
                     "WHERE  COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                     "AND    INACTIVE_FLAG = 0 "+
                     "ORDER BY ITEM_NAME";

            CmnHelp objhelp = new CmnHelp("Item Details", true, 570);
            objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
            objhelp.J_pSearchColumnName = "Item Name";
            objhelp.J_pSearchColumnType = J_ColumnType.String;
            objhelp.ShowDialog();
            
            if (J_Var.J_pMatrix[0] != null)
            {
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_ID, Convert.ToString(J_Var.J_pMatrix[0]));
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.ITEM_NAME, Convert.ToString(J_Var.J_pMatrix[1]));
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.UNIT, Convert.ToString(J_Var.J_pMatrix[3]));
                flxgrdDetails.set_TextMatrix(flxgrdDetails.Row, (int)enmInvoiceEntry.RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(J_Var.J_pMatrix[2])), "0.00"));
            }

            CalculateNetAmount();

        }
        #endregion

        #region PopulateTaxMaster
        private void PopulateTaxMaster()
        {
            // set the Help Grid Column Header Text & behavior
            // (0) Header Text
            // (1) Width
            // (2) Format
            // (3) Alignment
            string[,] strMatrix = {{"Tax Id", "0", "", "Right"},
                                   {"Tax Description", "300", "", ""},
                                   {"Rate", "100", "0.00", "Right"}};

            strSQL = "SELECT TAX_ID," +
                     "       TAX_DESC," +
                     "       TAX_RATE " +
                     "FROM   MST_TAX " +
                     "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                     "ORDER BY TAX_DESC";
            if (cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) == 2)
            {
                strSQL = "SELECT TAX_ID," +
                         "       TAX_DESC," +
                         "       TAX_RATE " +
                         "FROM   MST_TAX " +
                         "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                         "AND    TAX_ID   NOT IN(2, 4, 5)" +
                         "ORDER BY TAX_DESC";
            }

            CmnHelp objhelp = new CmnHelp("Tax Details", true, 490);
            objhelp.J_ShowDataInHelpGrid(strSQL, strMatrix);
            objhelp.J_pSearchColumnName = "Tax Description";
            objhelp.J_pSearchColumnType = J_ColumnType.String;
            objhelp.ShowDialog();

            if (J_Var.J_pMatrix[0] != null)
            {
                flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_ID, Convert.ToString(J_Var.J_pMatrix[0]));
                flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_DESC, Convert.ToString(J_Var.J_pMatrix[1]));
                flxgrdTax.set_TextMatrix(flxgrdTax.Row, (int)enmInvoiceTax.TAX_RATE, cmnService.J_FormatToString(Convert.ToDouble(Convert.ToString(J_Var.J_pMatrix[2])), "0.00"));
            }

            CalculateNetAmount();
        }
        #endregion


        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ClearControls
        private void ClearControls()
        {
            txtChallanRefNo.Text = "";
            txtOrderNo.Text = "";

            strSQL = "SELECT PARTY_ID," +
                     "       PARTY_NAME " +
                     "FROM   MST_PARTY " +
                     "WHERE  BRANCH_ID = " + J_Var.J_pBranchId + " " +
                     "ORDER BY PARTY_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbParty) == false) return;
            
            txtRemarks.Text = "";

            // Clear the detail grid
            this.setDetailsGridRefresh(flxgrdDetails);
            this.setDetailsGridColumns(flxgrdDetails);
            
            txtTotalAmount.Text = "0.00";

            txtDiscountText.Text = "";
            txtDiscountRate.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtAmountWithDiscount.Text = "0.00";

            // Clear the tax grid
            this.setDetailsGridRefresh_TAX(flxgrdTax);
            this.setDetailsGridColumns_TAX(flxgrdTax);
            
            txtTaxAmount.Text = "0.00";
            txtAmountWithTax.Text = "0.00";

            txtAdditionalCostText.Text = "";
            txtAdditionalCost.Text = "0.00";
            txtAmountWithAdditionalCost.Text = "0.00";

            txtRoundedOff.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtNetAmountInwords.Text = "Zero only.";

            Set_SLNO();

            grpSort.Visible   = false;
            grpSearch.Visible = false;

            //Added by Shrey Kejriwal on 13-04-2013

            //Populating Payment Type Combo
            strSQL = "SELECT PAYMENT_TYPE_ID," +
                     "       PAYMENT_TYPE_DESCRIPTION " +
                     "FROM   MST_PAYMENT_TYPE " +
                     "WHERE  INACTIVE_FLAG = 0 " +
                     "AND    HIDE_IN_INVOICE_FLAG = 0 "+
                     "ORDER BY PAYMENT_TYPE_DESCRIPTION ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbPaymentType) == false) return;

            //Populating Bank Combo
            strSQL = "SELECT BANK_ID," +
                     "       BANK_NAME " +
                     "FROM   MST_BANK " +
                     "ORDER BY BANK_NAME ";
            if (dmlService.J_PopulateComboBox(strSQL, ref cmbBank) == false) return;

            txtReference.Text = "";

            //Added by Shrey Kejriwal on 03/05/2013
            mskInvoiceDate.Text = DateTime.Now.ToString();
                        
            mskCollectionDate.Text= "";

            lblAutoCollectionPostFlag.Text = "0";
            lblSundryPartyFlag.Text = "0";
            //--
            intBankInInvoiceFlag = 0;
            intReferenceNoFlag = 0;
            //--
            blnInvoiceWiseReconciled = false;
        }
        #endregion

        #region setDetailsGridRefresh
        private void setDetailsGridRefresh(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, 10);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < 10; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;

            HFlexGrid.set_TextMatrix(1, (int)enmInvoiceEntry.SL_NO, "1");
        }
        #endregion

        #region setDetailsGridColumns
        private void setDetailsGridColumns(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // INVOICE_DETAIL_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.INVOICE_DETAIL_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.INVOICE_DETAIL_ID, 0, 0);

            // SL_NO
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.SL_NO;
            HFlexGrid.Text = "SL No.";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.SL_NO, 0, 700);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.SL_NO, (short)J_Alignment.RightCentre);
            
            // ITEM_NAME
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.ITEM_NAME;
            HFlexGrid.Text = "Item Name";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.ITEM_NAME, 0, 4600);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.ITEM_NAME, (short)J_Alignment.LeftCentre);
            
            // QUANTITY
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.QUANTITY;
            HFlexGrid.Text = "Quantity";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.QUANTITY, 0, 1000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.QUANTITY, (short)J_Alignment.RightCentre);
            
            // UNIT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.UNIT;
            HFlexGrid.Text = "Unit";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.UNIT, 0, 800);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.UNIT, (short)J_Alignment.LeftCentre);

            // RATE
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.RATE;
            HFlexGrid.Text = "Rate";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.RATE, 0, 1000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.RATE, (short)J_Alignment.RightCentre);

            // AMOUNT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.AMOUNT;
            HFlexGrid.Text = "Amount";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.AMOUNT, 0, 1500);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.AMOUNT, (short)J_Alignment.RightCentre);

            // REMARKS
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.REMARKS;
            HFlexGrid.Text = "Remarks";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.REMARKS, 0, 4700);
            HFlexGrid.set_ColAlignment((int)enmInvoiceEntry.REMARKS, (short)J_Alignment.LeftCentre);
            
            // ITEM_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceEntry.ITEM_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceEntry.ITEM_ID, 0, 0);
            
        }
        #endregion

        #region NumberFormatDetail
        private void NumberFormatDetail(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            for (int intCounter = 1; intCounter <= HFlexGrid.Rows - 1; intCounter++)
            {
                // Formatting Rate
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE))));

                // Formatting Amount
                HFlexGrid.set_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT,
                    string.Format("{0:0.00}", Convert.ToDouble(HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT) == ""
                    ? "0"
                    : HFlexGrid.get_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT))));
            }
        }
        #endregion

        #region setTextBoxInGrid
        private void setTextBoxInGrid()
        {
            intTempGridPosition = flxgrdDetails.RowSel;

            if (flxgrdDetails.Rows > 1 && (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                || flxgrdDetails.Col == (int)enmInvoiceEntry.RATE
                || flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS))
            {
                //-- For Text Box in Grid
                txtQty.Visible = true;

                txtQty.Left = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellLeft) + flxgrdDetails.Left - 1);
                txtQty.Top = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellTop) + flxgrdDetails.Top - 1);
                txtQty.Width = Convert.ToInt32(Support.TwipsToPixelsX(flxgrdDetails.CellWidth));
                txtQty.Height = Convert.ToInt32(Support.TwipsToPixelsY(flxgrdDetails.CellHeight) - 5);

                blnAmount = true;

                txtQty.Text = "";
                txtQty.Text = flxgrdDetails.get_TextMatrix(intTempGridPosition, flxgrdDetails.ColSel);

                if (flxgrdDetails.Col == (int)enmInvoiceEntry.QUANTITY
                    || flxgrdDetails.Col == (int)enmInvoiceEntry.RATE)
                    txtQty.TextAlign = HorizontalAlignment.Right;
                else if (flxgrdDetails.Col == (int)enmInvoiceEntry.REMARKS)
                    txtQty.TextAlign = HorizontalAlignment.Left;

                blnAmount = false;
                txtQty.Select();
                txtQty.Focus();
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            // Company
            if (cmbCompany.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the company.");
                cmbCompany.Select();
                return false;
            }

            // Invoice Series
            if (cmbInvoiceSeries.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the invoice series.");
                cmbInvoiceSeries.Select();
                return false;
            }

            // Invoice Date
            if (dtService.J_IsBlankDateCheck(ref mskInvoiceDate, "Please enter the invoice date.") == true)
                return false;

            // Invoice Date validation
            if (dtService.J_IsDateValid(mskInvoiceDate) == false)
            {
                cmnService.J_UserMessage("Please enter the valid invoice date.");
                mskInvoiceDate.Select();
                return false;
            }

            // Invoice Date within FA Year Date or not
            if (dtService.J_IsDateGreater(J_Var.J_pFABegDate, ref mskInvoiceDate, "", "", "", J_ShowMessage.NO) == false
                || dtService.J_IsDateGreater(ref mskInvoiceDate, J_Var.J_pFAEndDate, "", "", "", J_ShowMessage.NO) == false)
            {
                cmnService.J_UserMessage("Invoice date should be within FA Year date." +
                    "\n\nBegining Date : " + J_Var.J_pFABegDate +
                    "\nEnding Date    : " + J_Var.J_pFAEndDate);
                mskInvoiceDate.Select();
                return false;
            }
            
            // Invoice Date should be within Current Date
            if (dtService.J_IsDateGreater(ref mskInvoiceDate, dmlService.J_ReturnServerDate(), "", "",
                "Invoice date should not greater than the server date." +
                "\n\nServer Date : " + dmlService.J_ReturnServerDate(), J_ShowMessage.YES) == false)
                return false;

            // Party
            if (cmbParty.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the party.");
                cmbParty.Select();
                return false;
            }
            // Payment Type //-- 2015/03/30
            if (cmbPaymentType.SelectedIndex <= 0)
            {
                cmnService.J_UserMessage("Please select the Payment Type");
                cmbPaymentType.Select();
                return false;
            }
            else
            {
                //------------------------------------------------------------------
                //-- for SUndry Parties , Certain Payment Types are restricted
                //------------------------------------------------------------------
                if (lblPartyCategory.Text.ToUpper() == "SUNDRY")
                {
                    if(lblSundryPartyFlag.Text == "0")
                    {
                        cmnService.J_UserMessage("Payment Type : " + cmbPaymentType.Text + " is not allowed for 'Sundry Party'");
                        cmbPaymentType.Select();
                        return false;                    
                    }
                }

                //------------------------------------------------------------------
                //-- Reference Validation based on Payment Type
                //------------------------------------------------------------------
                if (intReferenceNoFlag == 1 && txtReference.Text.Trim() == "")
                {
                    cmnService.J_UserMessage("Reference No : Mandatory for this Payment Type");
                    txtReference.Select();
                    return false;
                }
                //
                //Added By dhrub on 09/04/2015
                //------------------------------------------------------------------
                //-- If Bank is Compulsory for a Payment Type, User has to Select a Bank
                //--- If Bank is NOT Compulsory for a Payment Type, User cannot Select a Bank
                //------------------------------------------------------------------
                if (intBankInInvoiceFlag == 0)
                {
                    if (cmbBank.SelectedIndex > 0)
                    {
                        cmnService.J_UserMessage("Bank not allowed for this Payment Type",MessageBoxIcon.Error);
                        cmbBank.Select();
                        return false;
                    }
                }
                else 
                {
                    //--
                    if (cmbBank.SelectedIndex <= 0)
                    {
                        cmnService.J_UserMessage("Bank Mandatory for this Payment Type", MessageBoxIcon.Error);
                        cmbBank.Select();
                        return false;
                    }
                }
                if (cmbPaymentType.Text == BS_PaymentType.Cc_Avenue && Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT CC_AVENUE_FLAG FROM MST_BANK WHERE BANK_ID = " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex))) == 0)
                {
                    cmnService.J_UserMessage("Improper bank selected.");
                    cmbBank.Select();
                    return false;
                }
            }
            //--
            if (lblAutoCollectionPostFlag.Text == "1" && lblPartyCategory.Text.ToUpper() == "SUNDRY" && J_Var.J_pCollectionPostFlag > 0)
            {
                // Collection Date
                if (dtService.J_IsBlankDateCheck(ref mskCollectionDate, "Please enter the Payment date.") == true)
                    return false;
                // Invoice Date validation
                if (dtService.J_IsDateValid(mskCollectionDate) == false)
                {
                    cmnService.J_UserMessage("Please enter the valid Payment date.");
                    mskCollectionDate.Select();
                    return false;
                }
                //
                if (dtService.J_IsDateGreater(ref mskCollectionDate, ref mskInvoiceDate, "", "",
                "Payment date should not be greater than the Invoice date.", J_ShowMessage.YES) == false)
                    return false;
            }
            //--
            if (Convert.ToDouble(txtTotalAmount.Text.Trim()) == 0)
            {
                cmnService.J_UserMessage("At least one item details required");
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }

            // Item Detail grid data validation
            int iItem = 0;
            int iRowIndex = 0;

            long lngItemId = 0;
            string strItem = "";
            int intQuantity = 0;
            double dblRate = 0;
            
            for (int iRIndex = 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
            {
                iRowIndex = iRIndex;

                lngItemId = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID));
                strItem = Convert.ToString(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_NAME));
                intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.QUANTITY));
                dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.RATE) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.RATE));

                // check item is empty or not
                if (string.IsNullOrEmpty(strItem) == false)
                {
                    if (intQuantity == 0)
                    {
                        iItem = 1;
                        goto Description;
                    }
                    if (dblRate == 0)
                    {
                        iItem = 2;
                        goto Description;
                    }
                    if (CheckDuplicateItem(lngItemId, iRIndex, out iRowIndex) == false)
                    {
                        iItem = 3;
                        goto Description;
                    }
                }
                else
                {
                    if (intQuantity > 0 || dblRate > 0)
                    {
                        iItem = 4;
                        goto Description;
                    }
                }
                iItem = 10;
            }

        Description:
            if (iItem == 0)
            {
                cmnService.J_UserMessage("No record in grid.");
                flxgrdDetails.Row = 1;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }
            if (iItem == 1)
            {
                cmnService.J_UserMessage("Please enter quantity");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.QUANTITY;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }
            if (iItem == 2)
            {
                cmnService.J_UserMessage("Please enter rate");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.RATE;
                flxgrdDetails.Select();
                setTextBoxInGrid();
                return false;
            }
            if (iItem == 3)
            {
                cmnService.J_UserMessage("Duplicate item");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }
            if (iItem == 4)
            {
                cmnService.J_UserMessage("Please select the item");
                flxgrdDetails.Row = iRowIndex;
                flxgrdDetails.Col = (int)enmInvoiceEntry.ITEM_NAME;
                flxgrdDetails.Select();
                return false;
            }

            // Tax Detail grid data validation
            int iTax = 0;
            iRowIndex = 0;
            long lngTaxId = 0;
            
            for (int iRIndex = 1; iRIndex <= flxgrdTax.Rows - 1; iRIndex++)
            {
                iRowIndex = iRIndex;

                lngTaxId = Convert.ToInt32(flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                    ? "0"
                                    : flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID));
                
                // check tax is empty or not
                if (lngTaxId > 0)
                {
                    if (CheckDuplicateTax(lngTaxId, iRIndex, out iRowIndex) == false)
                    {
                        iTax = 1;
                        goto Description_Tax;
                    }
                }
            }

        Description_Tax:
            if (iTax == 1)
            {
                cmnService.J_UserMessage("Duplicate tax");
                flxgrdTax.Row = iRowIndex;
                flxgrdTax.Col = (int)enmInvoiceTax.TAX_DESC;
                flxgrdTax.Select();
                return false;
            }

            // check additional cost text & amount together
            if (txtAdditionalCostText.Text == "" && Convert.ToDouble(txtAdditionalCost.Text.Trim()) != 0)
            {
                cmnService.J_UserMessage("Please enter additional cost text");
                txtAdditionalCostText.Select();
                return false;
            }
            if (txtAdditionalCostText.Text != "" && Convert.ToDouble(txtAdditionalCost.Text.Trim()) == 0)
            {
                cmnService.J_UserMessage("Please enter additional cost");
                txtAdditionalCost.Select();
                return false;
            }


            //ADDED BY DHRUB ON 24/05/2015
            //------------------------------------------------------------------------------------
            //WARNING MESSAGE FOR UNKNOWN PAYMENTS OTHER THAN CC_AVENUE 
            //IF ANY UNKNOWN PAYMENTS EXISTS ON THE INVOICE DATE 
            //IF INVOICE AMOUNT BECOME SAME THEN THE WARNING MESSAGE WILL BE GENERATED
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
//            if (lblMode.Text == J_Mode.Add)
//            {
//                strSQL = @" SELECT COUNT(*) 
//                            FROM   TRN_COLLECTION_HEADER 
//                            WHERE  TRN_COLLECTION_HEADER.NET_INVOICE_AMT = 0 
//                            AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0
//                            AND    TRN_COLLECTION_HEADER.NET_AMT         = " + txtNetAmount.Text +
//                         @" AND    CONVERT(VARCHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) = " + dtService.J_ConvertToIntYYYYMMDD(mskCollectionDate);

//                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
//                {
//                    cmnService.J_UserMessage("Warning : There is an Unknown Collection on " + mskCollectionDate.Text + ".\nPlease make sure the collection entries.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }

            return true;
        }
        #endregion

        #region CheckDuplicateItem
        private bool CheckDuplicateItem(long ItemId, int RowIndex, out int DuplicateRowIndex)
        {
            long lngItemId_Dup = 0;
            DuplicateRowIndex = 0;

            for (int iRIndex = RowIndex + 1; iRIndex <= flxgrdDetails.Rows - 1; iRIndex++)
            {
                lngItemId_Dup = Convert.ToInt32(flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                    ? "0"
                                    : flxgrdDetails.get_TextMatrix(iRIndex, (int)enmInvoiceEntry.ITEM_ID));

                if (lngItemId_Dup == ItemId && lngItemId_Dup > 0)
                {
                    DuplicateRowIndex = iRIndex;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region CheckDuplicateTax
        private bool CheckDuplicateTax(long TaxId, int RowIndex, out int DuplicateRowIndex)
        {
            long lngTaxId_Dup = 0;
            DuplicateRowIndex = 0;
            
            for (int iRIndex = RowIndex + 1; iRIndex <= flxgrdTax.Rows - 1; iRIndex++)
            {
                lngTaxId_Dup = Convert.ToInt32(flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                    ? "0"
                                    : flxgrdTax.get_TextMatrix(iRIndex, (int)enmInvoiceTax.TAX_ID));

                if (lngTaxId_Dup == TaxId && lngTaxId_Dup > 0)
                {
                    DuplicateRowIndex = iRIndex;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Insert_Update_Delete_Data
        private void Insert_Update_Delete_Data()
        {
            try
            {
                // declaration of local variables
                long lngInvoiceHeaderId = 0;
                long lngItemId          = 0;
                int intQuantity         = 0;
                double dblRate          = 0;
                double dblAmount        = 0;
                string strRemarks       = "";

                long lngTaxId = 0;
                
                switch (lblMode.Text)
                {
                    case J_Mode.Add:
                        // All validation
                        if (ValidateFields() == false) return;

                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref cmbInvoiceSeries) == true) return;
                        
                        // Transaction is started
                        dmlService.J_BeginTransaction();

                        // Getting last Invoice Number
                        GetInvoiceNo();

                        strSQL = "INSERT INTO TRN_INVOICE_HEADER (" +
                                 "            BRANCH_ID," +
                                 "            FAYEAR_ID," +
                                 "            COMPANY_ID," +
                                 "            PARTY_ID," +
                                 "            INVOICE_SERIES_ID," +
                                 "            INVOICE_NO," +
                                 "            INVOICE_DATE," +
                                 "            TRAN_TYPE," +
                                 "            SUB_TYPE," +
                                 "            CHALLAN_REF_NO," +
                                 "            ORDER_NO," +
                                 "            TOTAL_AMOUNT," +
                                 "            DISCOUNT_TEXT," +
                                 "            DISCOUNT_RATE," +
                                 "            DISCOUNT_AMOUNT," +
                                 "            AMOUNT_WITH_DISCOUNT," +
                                 "            TAX_TOTAL_AMOUNT," +
                                 "            AMOUNT_WITH_TAX," +
                                 "            ADDITIONAL_COST_TEXT," +
                                 "            ADDITIONAL_COST," +
                                 "            AMOUNT_WITH_ADDITIONAL_COST," +
                                 "            ROUNDED_OFF," +
                                 "            NET_AMOUNT," +
                                 "            NET_AMOUNT_INWORDS," +
                                 "            REMARKS," +
                                 "            PAYMENT_TYPE_ID," +
                                 "            BANK_ID," +
                                 "            REFERENCE_NO," +
                                 "            USER_ID," +
                                 "            CREATE_DATE," + 
                                 "            RECON_FLAG) " +
                                 "    VALUES( " + J_Var.J_pBranchId + "," +
                                 "            " + J_Var.J_pFAYearId + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex) + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "," +
                                 "           '" + txtInvoiceNo.Text + "'," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + "," +
                                 "           'INV'," +
                                 "           ''," +
                                 "           '" + cmnService.J_ReplaceQuote(txtChallanRefNo.Text) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtOrderNo.Text) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtTotalAmount.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtDiscountText.Text) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithDiscount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtTaxAmount.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithTax.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtAdditionalCostText.Text.Trim()) + "'," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtAmountWithAdditionalCost.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtRoundedOff.Text) + "," +
                                 "            " + cmnService.J_ReturnDoubleValue(txtNetAmount.Text) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtNetAmountInwords.Text.Trim()) + "'," +
                                 "           '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "'," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + "," +
                                 "            " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex) + "," +
                                 "           '" + cmnService.J_ReplaceQuote(txtReference.Text.Trim()) + "'," +
                                 "            " + J_Var.J_pUserId + "," +
                                 "            " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(dmlService.J_ReturnServerDate()) + cmnService.J_DateOperator() + "," +
                                 "            " + J_Var.J_pReconFlag + ")";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbCompany.Select();
                            return;
                        }

                        // get max ledger header id
                        lngInvoiceHeaderId = dmlService.J_ReturnMaxValue("TRN_INVOICE_HEADER", "INVOICE_HEADER_ID",
                                                                         "    COMPANY_ID = " + cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex) + " " +
                                                                         "AND USER_ID    = " + J_Var.J_pUserId + "");

                        // Loop as per individual item
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngItemId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID));

                            // Quantity is stored
                            intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE));
                            
                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT));
                            
                            // Remarks is stored
                            strRemarks = flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.REMARKS);
                            

                            if (dblAmount != 0 && lngItemId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_DETAIL (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            ITEM_ID," +
                                         "            QUANTITY," +
                                         "            RATE," +
                                         "            AMOUNT," +
                                         "            REMARKS) " +
                                         "    VALUES( " + lngInvoiceHeaderId + "," +
                                         "            " + lngItemId + "," +
                                         "            " + intQuantity + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + "," +
                                         "           '" + cmnService.J_ReplaceQuote(strRemarks) + "')";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    cmbCompany.Select();
                                    return;
                                }
                            }
                        }

                        // Loop as per individual Tax
                        for (int intRIndex = 1; intRIndex <= flxgrdTax.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngTaxId = Convert.ToInt64(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT));

                            if (lngTaxId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_TAX (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            TAX_ID," +
                                         "            TAX_RATE," +
                                         "            TAX_AMOUNT) " +
                                         "    VALUES( " + lngInvoiceHeaderId + "," +
                                         "            " + lngTaxId + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    cmbCompany.Select();
                                    return;
                                }
                            }
                        }

                        // update LAST_NO into MST_INVOICE_SERIES Table
                        strSQL = "UPDATE MST_INVOICE_SERIES " +
                                 "SET    LAST_NO = LAST_NO + 1 " +
                                 "WHERE  INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            cmbCompany.Select();
                            return;
                        }
                        // Transaction is commited
                        dmlService.J_Commit();
                        //-- COLLECTION ENTRY //-- 30/03/2015
                        if (lblAutoCollectionPostFlag.Text == "1" && lblPartyCategory.Text.ToUpper() == "SUNDRY" && J_Var.J_pCollectionPostFlag > 0)
                        {
                            //
                            long lngBankId = 0;
                            // Invoice Series
                            if (cmbBank.SelectedIndex > 0)
                                lngBankId = cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex);
                            //
                            BillingSystem.Classes.BS BillingSys = new BillingSystem.Classes.BS();
                            if (BillingSys.InsertCollectionEntry(mskCollectionDate.Text,
                                                             cmnService.J_GetComboBoxItemId(ref cmbCompany, cmbCompany.SelectedIndex),
                                                             J_Var.J_pFAYearId,
                                                             cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex),
                                                             lngBankId,
                                                             cmnService.J_ReplaceQuote(txtReference.Text),
                                                             cmnService.J_ReturnDoubleValue(txtNetAmount.Text),
                                                             J_Var.J_pUserId,
                                                             lngInvoiceHeaderId) == false)
                            {
                                cmnService.J_UserMessage("Collection Entry could not be done");
                                return;
                            }
                        }
                        //
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);
                        
                        // clear all controls
                        this.ClearControls();

                        // Print the Invoice
                        this.PrintInvoice(lngInvoiceHeaderId);

                        cmbCompany.Select();
                        break;
                    case J_Mode.Edit:

                        //---------------------------------------------
                        // All validation
                        if (ValidateFields() == false) return;

                        string strAcEntryDate = "";
                        string strBankDate = "";

                        strSQL = "SELECT ACCOUNT_ENTRY_DATE, BANK_STATEMENT_DATE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId;

                        IDataReader reader = dmlService.J_ExecSqlReturnReader(strSQL);

                        while (reader.Read())
                        {
                            strAcEntryDate = reader["ACCOUNT_ENTRY_DATE"].ToString();
                            strBankDate = reader["BANK_STATEMENT_DATE"].ToString();
                        }

                        reader.Close();
                        reader.Dispose();

                        if (strAcEntryDate != "" || strBankDate != "")
                        {
                            cmnService.J_UserMessage("This invoice cannot be modified as it has already been reconciled with the Bank/Accounts");
                            return;
                        }

                        //---------------------------------------------
                        //Invoice cannot be modified if have any Reconciliation Date for any Collection Header 
                        //---------------------------------------------
                        if (blnInvoiceWiseReconciled == true)
                        {
                            cmnService.J_UserMessage("This invoice cannot be modified as it has already been reconciled into Collection.");
                            return;
                        }

                        // Save Confirmation Message
                        if (cmnService.J_SaveConfirmationMessage(ref mskInvoiceDate) == true) return;

                        
                        // Transaction is started
                        dmlService.J_BeginTransaction();
                        
                        strSQL = "UPDATE TRN_INVOICE_HEADER " +
                                 "SET    PARTY_ID                    =  " + cmnService.J_GetComboBoxItemId(ref cmbParty, cmbParty.SelectedIndex) + "," +
                                 "       INVOICE_DATE                =  " + cmnService.J_DateOperator() + dtService.J_ConvertMMddyyyy(mskInvoiceDate) + cmnService.J_DateOperator() + "," +
                                 "       CHALLAN_REF_NO              = '" + cmnService.J_ReplaceQuote(txtChallanRefNo.Text) + "'," +
                                 "       ORDER_NO                    = '" + cmnService.J_ReplaceQuote(txtOrderNo.Text) + "'," +
                                 "       TOTAL_AMOUNT                =  " + cmnService.J_ReturnDoubleValue(txtTotalAmount.Text) + "," +
                                 "       DISCOUNT_TEXT               = '" + cmnService.J_ReplaceQuote(txtDiscountText.Text) + "'," +
                                 "       DISCOUNT_RATE               =  " + cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) + "," +
                                 "       DISCOUNT_AMOUNT             =  " + cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) + "," +
                                 "       AMOUNT_WITH_DISCOUNT        =  " + cmnService.J_ReturnDoubleValue(txtAmountWithDiscount.Text) + "," +
                                 "       TAX_TOTAL_AMOUNT            =  " + cmnService.J_ReturnDoubleValue(txtTaxAmount.Text) + "," +
                                 "       AMOUNT_WITH_TAX             =  " + cmnService.J_ReturnDoubleValue(txtAmountWithTax.Text) + "," +
                                 "       ADDITIONAL_COST_TEXT        = '" + cmnService.J_ReplaceQuote(txtAdditionalCostText.Text.Trim()) + "'," +
                                 "       ADDITIONAL_COST             =  " + cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text) + "," +
                                 "       AMOUNT_WITH_ADDITIONAL_COST =  " + cmnService.J_ReturnDoubleValue(txtAmountWithAdditionalCost.Text) + "," +
                                 "       ROUNDED_OFF                 =  " + cmnService.J_ReturnDoubleValue(txtRoundedOff.Text) + "," +
                                 "       NET_AMOUNT                  =  " + cmnService.J_ReturnDoubleValue(txtNetAmount.Text) + "," +
                                 "       NET_AMOUNT_INWORDS          = '" + cmnService.J_ReplaceQuote(txtNetAmountInwords.Text.Trim()) + "'," +
                                 "       REMARKS                     = '" + cmnService.J_ReplaceQuote(txtRemarks.Text.Trim()) + "', " +
                                 "       PAYMENT_TYPE_ID             =  " + cmnService.J_GetComboBoxItemId(ref cmbPaymentType, cmbPaymentType.SelectedIndex) + "," +
                                 "       BANK_ID                     =  " + cmnService.J_GetComboBoxItemId(ref cmbBank, cmbBank.SelectedIndex) + "," +
                                 "       REFERENCE_NO                = '" + cmnService.J_ReplaceQuote(txtReference.Text.Trim()) + "' " +
                                 "WHERE  INVOICE_HEADER_ID           =  " + SearchId + "";
                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // delete all records from TRN_INVOICE_DETAIL
                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_DETAIL WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // Loop as per individual item
                        for (int intRIndex = 1; intRIndex <= flxgrdDetails.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngItemId = Convert.ToInt64(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.ITEM_ID));

                            // Quantity is stored
                            intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.QUANTITY));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT) == ""
                                ? "0"
                                : flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.AMOUNT));

                            // Remarks is stored
                            strRemarks = flxgrdDetails.get_TextMatrix(intRIndex, (int)enmInvoiceEntry.REMARKS);


                            if (dblAmount != 0 && lngItemId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_DETAIL (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            ITEM_ID," +
                                         "            QUANTITY," +
                                         "            RATE," +
                                         "            AMOUNT," +
                                         "            REMARKS) " +
                                         "    VALUES( " + SearchId + "," +
                                         "            " + lngItemId + "," +
                                         "            " + intQuantity + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + "," +
                                         "           '" + cmnService.J_ReplaceQuote(strRemarks) + "')";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    mskInvoiceDate.Select();
                                    return;
                                }
                            }
                        }

                        // delete all records from TRN_INVOICE_TAX
                        if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_TAX WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        {
                            mskInvoiceDate.Select();
                            return;
                        }

                        // Loop as per individual Tax
                        for (int intRIndex = 1; intRIndex <= flxgrdTax.Rows - 1; intRIndex++)
                        {
                            // Item Id is stored
                            lngTaxId = Convert.ToInt64(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_ID));

                            // Rate is stored
                            dblRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_RATE));

                            // Amount is stored
                            dblAmount = Convert.ToDouble(flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT) == ""
                                ? "0"
                                : flxgrdTax.get_TextMatrix(intRIndex, (int)enmInvoiceTax.TAX_AMOUNT));

                            if (lngTaxId > 0)
                            {
                                strSQL = "INSERT INTO TRN_INVOICE_TAX (" +
                                         "            INVOICE_HEADER_ID," +
                                         "            TAX_ID," +
                                         "            TAX_RATE," +
                                         "            TAX_AMOUNT) " +
                                         "    VALUES( " + SearchId + "," +
                                         "            " + lngTaxId + "," +
                                         "            " + dblRate + "," +
                                         "            " + dblAmount + ")";
                                if (dmlService.J_ExecSql(strSQL) == false)
                                {
                                    mskInvoiceDate.Select();
                                    return;
                                }
                            }
                        }

                        // Transaction is commited
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.EditModeSave);

                        // Mode
                        lblMode.Text = J_Mode.Add;
                        cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                        lblSearchMode.Text = J_Mode.General;

                        // Button behavior
                        BtnCancel.Enabled = false;
                        BtnCancel.BackColor = Color.LightGray;
                        BtnDelete.Enabled = false;
                        BtnDelete.BackColor = Color.LightGray;
                        BtnExit.Enabled = true;
                        BtnExit.BackColor = Color.Lavender;

                        // clear all controls
                        this.ClearControls();

                        
                        // enable controls
                        Enable(lblMode.Text);

                        // Print the Invoice
                        this.PrintInvoice(SearchId);

                        SearchId = 0;
                        cmbCompany.Select();
                        break;
                    case J_Mode.Delete:
                        //if (cmnService.J_UserMessage(J_Msg.AreYouSure2Delete,
                        //    J_Var.J_pProjectName,
                        //    MessageBoxButtons.YesNo,
                        //    MessageBoxIcon.Question,
                        //    MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    // Mode
                        //    lblMode.Text = J_Mode.Edit;
                        //    cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                        //    lblSearchMode.Text = J_Mode.General;

                        //    // Button behavior
                        //    //BtnDelete.Enabled = true;
                        //    //BtnDelete.BackColor = Color.Lavender;
                        //    BtnExit.Enabled = true;
                        //    BtnExit.BackColor = Color.Lavender;

                        //    BtnCancel.Select();
                        //    return;
                        //}

                        //strAcEntryDate = "";
                        //strBankDate = "";

                        //strSQL = "SELECT ACCOUNT_ENTRY_DATE, BANK_STATEMENT_DATE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId;

                        //reader = dmlService.J_ExecSqlReturnReader(strSQL);

                        //while (reader.Read())
                        //{
                        //    strAcEntryDate = reader["ACCOUNT_ENTRY_DATE"].ToString();
                        //    strBankDate = reader["BANK_STATEMENT_DATE"].ToString();
                        //}

                        //reader.Close();
                        //reader.Dispose();

                        //if (strAcEntryDate != "" || strBankDate != "")
                        //{
                        //    cmnService.J_UserMessage("This invoice cannot be modified as it has already been reconciled with the Bank/Accounts");
                        //    return;
                        //}


                        //// Transaction is started
                        //dmlService.J_BeginTransaction();

                        //// delete all records from TRN_INVOICE_TAX
                        //if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_TAX WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        //{
                        //    mskInvoiceDate.Select();
                        //    return;
                        //}

                        //// delete all records from TRN_INVOICE_DETAIL
                        //if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_DETAIL WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        //{
                        //    mskInvoiceDate.Select();
                        //    return;
                        //}

                        //// delete all records from TRN_INVOICE_HEADER
                        //if (dmlService.J_ExecSql("DELETE FROM TRN_INVOICE_HEADER WHERE INVOICE_HEADER_ID = " + SearchId + "") == false)
                        //{
                        //    mskInvoiceDate.Select();
                        //    return;
                        //}

                        //// Transaction is commited
                        //dmlService.J_Commit();
                        //cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.DeleteMode);

                        //// Mode
                        //lblMode.Text = J_Mode.Add;
                        //cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                        //lblSearchMode.Text = J_Mode.General;

                        //// Button behavior
                        //BtnCancel.Enabled = false;
                        //BtnCancel.BackColor = Color.LightGray;
                        //BtnDelete.Enabled = false;
                        //BtnDelete.BackColor = Color.LightGray;
                        //BtnExit.Enabled = true;
                        //BtnExit.BackColor = Color.Lavender;

                        //// clear all controls
                        //this.ClearControls();
                        //SearchId = 0;

                        //// enable controls
                        //Enable(lblMode.Text);

                        //cmbCompany.Select();
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

        #region CalculateNetAmount
        private void CalculateNetAmount()
        {
            int intQuantity                = 0;
            double dblRate                 = 0;
            double dblAmount               = 0;

            double dblTotalAmount          = 0;

            double dblDiscountRate         = cmnService.J_ReturnDoubleValue(txtDiscountRate.Text);
            double dblDiscountAmount       = cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text);
            double dblAmountWithDiscount   = 0;

            double dblTaxRate              = 0;
            double dblTaxAmount            = 0;
            double dblTotalTaxAmount       = 0;
            double dblAmountWithTax        = 0;

            double dblAdditionalCost       = cmnService.J_ReturnDoubleValue(txtAdditionalCost.Text);
            double dblAmountWithAdditional = 0;

            double dblRoundedOff           = cmnService.J_ReturnDoubleValue(txtRoundedOff.Text);
            double dblNetAmount            = 0;
            
            if (flxgrdDetails.Rows > 0)
            {
                for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
                {
                    intQuantity = Convert.ToInt32(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.QUANTITY));

                    dblRate = Convert.ToDouble(flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE) == ""
                        ? "0"
                        : flxgrdDetails.get_TextMatrix(intCounter, (int)enmInvoiceEntry.RATE));

                    dblAmount = Convert.ToDouble(cmnService.J_FormatToString((intQuantity * dblRate), "0.00"));
                    flxgrdDetails.set_TextMatrix(intCounter, (int)enmInvoiceEntry.AMOUNT, string.Format("{0:0.00}", dblAmount));
                    
                    dblTotalAmount = dblTotalAmount + dblAmount;
                }
            }
            
            // Discount Enable
            DiscountEnable();

            // calculate discount amount
            if (txtDiscountRate.ReadOnly == false)
            {
                if (dblDiscountRate == 0) dblDiscountAmount = 0;
                else if (dblDiscountRate > 0) dblDiscountAmount = Convert.ToDouble(cmnService.J_FormatToString((dblTotalAmount * dblDiscountRate / 100), "0.00"));
            }
            dblAmountWithDiscount = dblTotalAmount - dblDiscountAmount;

            // calculate tax
            if (flxgrdTax.Rows > 0)
            {
                for (int intCounter = 1; intCounter <= flxgrdTax.Rows - 1; intCounter++)
                {
                    if (flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC).Trim() != "")
                    {
                        dblTaxRate = Convert.ToDouble(flxgrdTax.get_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE));
                        dblTaxAmount = Convert.ToDouble(cmnService.J_FormatToString((dblAmountWithDiscount * dblTaxRate / 100), "0.00"));

                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, string.Format("{0:0.00}", dblTaxAmount));

                        dblTotalTaxAmount = dblTotalTaxAmount + dblTaxAmount;
                    }
                    else
                    {
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_ID, "0");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_DESC, "");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_RATE, "");
                        flxgrdTax.set_TextMatrix(intCounter, (int)enmInvoiceTax.TAX_AMOUNT, "");
                    }
                }
            }
            dblAmountWithTax = dblAmountWithDiscount + dblTotalTaxAmount;

            // calculate additional amount
            dblAmountWithAdditional = dblAmountWithTax + dblAdditionalCost;

            // calculate net amount
            dblNetAmount = dblAmountWithAdditional + dblRoundedOff;

            // Total Amount
            txtTotalAmount.Text = cmnService.J_FormatToString(dblTotalAmount, "0.00");
            
            // Discount
            if (txtDiscountRate.ReadOnly == false) 
                txtDiscountAmount.Text = cmnService.J_FormatToString(dblDiscountAmount, "0.00");
            
            txtAmountWithDiscount.Text = cmnService.J_FormatToString(dblAmountWithDiscount, "0.00");

            // Tax
            txtAmountWithTax.Text = cmnService.J_FormatToString(dblAmountWithTax, "0.00");
            txtTaxAmount.Text = cmnService.J_FormatToString(dblTotalTaxAmount, "0.00");
            
            // Additional
            txtAmountWithAdditionalCost.Text = cmnService.J_FormatToString(dblAmountWithAdditional, "0.00");

            // Net Amount
            txtNetAmount.Text = cmnService.J_FormatToString(dblNetAmount, "0.00");
            txtNetAmountInwords.Text = cmnService.J_Inwords(dblNetAmount);

        }
        #endregion 
        
        #region setDetailsGridRefresh_TAX
        private void setDetailsGridRefresh_TAX(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.set_Cols(0, 5);

            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
                for (int intCols = 0; intCols < 5; intCols++)
                    HFlexGrid.set_TextMatrix(intRows, intCols, "");

            HFlexGrid.Rows = 2;
        }
        #endregion

        #region setDetailsGridColumns_TAX
        private void setDetailsGridColumns_TAX(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid HFlexGrid)
        {
            HFlexGrid.Row = 0;
            HFlexGrid.Col = 0;
            HFlexGrid.set_ColWidth(0, 0, 250);

            // TAX_ID
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_ID;
            HFlexGrid.Text = "";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_ID, 0, 0);

            // TAX_DESC
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_DESC;
            HFlexGrid.Text = "Tax Description";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_DESC, 0, 3000);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_DESC, (short)J_Alignment.LeftCentre);

            // RATE
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_RATE;
            HFlexGrid.Text = "Rate";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_RATE, 0, 900);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_RATE, (short)J_Alignment.RightCentre);

            // AMOUNT
            HFlexGrid.Row = 0;
            HFlexGrid.Col = (int)enmInvoiceTax.TAX_AMOUNT;
            HFlexGrid.Text = "Amount";
            HFlexGrid.set_ColWidth((int)enmInvoiceTax.TAX_AMOUNT, 0, 1200);
            HFlexGrid.set_ColAlignment((int)enmInvoiceTax.TAX_AMOUNT, (short)J_Alignment.RightCentre);

        }
        #endregion

        #region Enable
        private void Enable(string Mode)
        {
            if (Mode == J_Mode.Add)
            {
                cmbCompany.Enabled = true;
                cmbCompany.BackColor = Color.White;
                cmbCompany.ForeColor = Color.Black;

                cmbInvoiceSeries.Enabled = true;
                cmbInvoiceSeries.BackColor = Color.White;
                cmbInvoiceSeries.ForeColor = Color.Black;

                //mskCollectionDate.Enabled = true;
                //cmbPaymentType.Enabled = true;
            }
            else if (Mode == J_Mode.Edit)
            {
                cmbCompany.Enabled = false;
                cmbCompany.BackColor = Color.LightYellow;
                cmbCompany.ForeColor = Color.Blue;

                cmbInvoiceSeries.Enabled = false;
                cmbInvoiceSeries.BackColor = Color.LightYellow;
                cmbInvoiceSeries.ForeColor = Color.Blue;

                //mskCollectionDate.Enabled = false;
                //cmbPaymentType.Enabled = false;
            }
        }
        #endregion

        #region DiscountEnable
        private void DiscountEnable()
        {
            txtDiscountRate.ReadOnly = false;
            txtDiscountRate.BackColor = Color.White;
            txtDiscountRate.ForeColor = Color.Black;

            txtDiscountAmount.ReadOnly = false;
            txtDiscountAmount.BackColor = Color.White;
            txtDiscountAmount.ForeColor = Color.Black;
            
            if(cmnService.J_ReturnDoubleValue(txtDiscountRate.Text) > 0)
            {
                txtDiscountAmount.ReadOnly = true;
                txtDiscountAmount.BackColor = Color.LightYellow;
                txtDiscountAmount.ForeColor = Color.Black;
            }
            else if(cmnService.J_ReturnDoubleValue(txtDiscountAmount.Text) > 0)
            {
                txtDiscountRate.ReadOnly = true;
                txtDiscountRate.BackColor = Color.LightYellow;
                txtDiscountRate.ForeColor = Color.Black;
            }
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long InvoiceHeaderId)
        {
            // declaration of variables
            IDataReader reader = null;

            string strInvoiceSeries  = "";
            string strInvoiceNo      = "";
            double dblDiscountRate   = 0;
            double dblDiscountAmount = 0;
            double dblAdditionalCost = 0;
            double dblRoundedOff     = 0;

            string strParty = "";

            try
            {
                strSQL = "SELECT MST_INVOICE_SERIES.PREFIX                  AS PREFIX," +
                         "       TRN_INVOICE_HEADER.INVOICE_NO              AS INVOICE_NO," +
                         "       " + cmnService.J_SQLDBFormat("TRN_INVOICE_HEADER.INVOICE_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS INVOICE_DATE," +
                         "       TRN_INVOICE_HEADER.CHALLAN_REF_NO          AS CHALLAN_REF_NO," +
                         "       TRN_INVOICE_HEADER.ORDER_NO                AS ORDER_NO," +
                         "       MST_PARTY.PARTY_NAME                       AS PARTY_NAME," +
                         "       TRN_INVOICE_HEADER.REMARKS                 AS REMARKS," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_TEXT           AS DISCOUNT_TEXT," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_RATE           AS DISCOUNT_RATE," +
                         "       TRN_INVOICE_HEADER.DISCOUNT_AMOUNT         AS DISCOUNT_AMOUNT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST_TEXT    AS ADDITIONAL_COST_TEXT," +
                         "       TRN_INVOICE_HEADER.ADDITIONAL_COST         AS ADDITIONAL_COST," +
                         "       TRN_INVOICE_HEADER.ROUNDED_OFF             AS ROUNDED_OFF, " +
                         "       TRN_INVOICE_HEADER.PAYMENT_TYPE_ID         AS PAYMENT_TYPE_ID, " +
                         "       MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION  AS PAYMENT_TYPE_DESCRIPTION, " +
                         "       MST_PAYMENT_TYPE.SUNDRY_PARTY_FLAG         AS SUNDRY_PARTY_FLAG, " +
                         "       MST_PAYMENT_TYPE.AUTO_COLLECTION_POST_FLAG AS AUTO_COLLECTION_POST_FLAG, " +
                         "       TRN_INVOICE_HEADER.BANK_ID                 AS BANK_ID, " +
                         "       MST_BANK.BANK_NAME                         AS BANK_NAME, " +
                         "       TRN_INVOICE_HEADER.REFERENCE_NO            AS REFERENCE_NO, " +
                         "       " + cmnService.J_SQLDBFormat("TRN_COLLECTION_HEADER.COLLECTION_DATE", J_SQLColFormat.DateFormatDDMMYYYY) + " AS COLLECTION_DATE " +
                         "FROM   ((((((TRN_INVOICE_HEADER " +
                         "INNER JOIN MST_INVOICE_SERIES " +
                         "        ON TRN_INVOICE_HEADER.INVOICE_SERIES_ID = MST_INVOICE_SERIES.INVOICE_SERIES_ID) " +
                         "INNER JOIN MST_PARTY " +
                         "        ON TRN_INVOICE_HEADER.PARTY_ID          = MST_PARTY.PARTY_ID) " +
                         "LEFT  JOIN MST_PAYMENT_TYPE " +
                         "        ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID   = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID) " +
                         "LEFT  JOIN MST_BANK " +
                         "        ON TRN_INVOICE_HEADER.BANK_ID           = MST_BANK.BANK_ID) " +
                         "LEFT  JOIN TRN_COLLECTION_DETAIL " +
                         "        ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID) " +
                         "LEFT  JOIN TRN_COLLECTION_HEADER " +
                         "        ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID) " +
                         "WHERE  TRN_INVOICE_HEADER.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";
                reader = dmlService.J_ExecSqlReturnReader(strSQL);
                if (reader == null) return false;

                while (reader.Read())
                {
                    strInvoiceSeries           = Convert.ToString(reader["PREFIX"]);
                    strInvoiceNo               = Convert.ToString(reader["INVOICE_NO"]);

                    mskInvoiceDate.Text        = Convert.ToString(reader["INVOICE_DATE"]);
                    txtChallanRefNo.Text       = Convert.ToString(reader["CHALLAN_REF_NO"]);
                    txtOrderNo.Text            = Convert.ToString(reader["ORDER_NO"]);
                    strParty                   = Convert.ToString(reader["PARTY_NAME"]);
                    txtRemarks.Text            = Convert.ToString(reader["REMARKS"]);

                    txtDiscountText.Text       = Convert.ToString(reader["DISCOUNT_TEXT"]);
                    dblDiscountRate            = Convert.ToDouble(Convert.ToString(reader["DISCOUNT_RATE"]));
                    dblDiscountAmount          = Convert.ToDouble(Convert.ToString(reader["DISCOUNT_AMOUNT"]));
                    txtAdditionalCostText.Text = Convert.ToString(reader["ADDITIONAL_COST_TEXT"]);
                    dblAdditionalCost          = Convert.ToDouble(Convert.ToString(reader["ADDITIONAL_COST"]));
                    dblRoundedOff              = Convert.ToDouble(Convert.ToString(reader["ROUNDED_OFF"]));

                    txtReference.Text = Convert.ToString(reader["REFERENCE_NO"]);

                    blnPaymentType = false;
                    //BLOCKED BY DHRUB ON 09/04/2015
                    //cmbPaymentType.Text = Convert.ToString(reader["PAYMENT_TYPE_DESCRIPTION"]);
                    BS.T_Selected_PaymentType = Convert.ToString(reader["PAYMENT_TYPE_DESCRIPTION"]);
                    //--
                    cmbBank.Text        = Convert.ToString(reader["BANK_NAME"]);
                    lblSundryPartyFlag.Text = Convert.ToString(reader["SUNDRY_PARTY_FLAG"]);
                    lblAutoCollectionPostFlag.Text = Convert.ToString(reader["AUTO_COLLECTION_POST_FLAG"]);
                    blnPaymentType = true;

                    mskCollectionDate.Text = Convert.ToString(reader["COLLECTION_DATE"]);                    
                }

                // data reader object is closed
                reader.Dispose();

                //Added By dhrub on 09/04/2015
                cmbPaymentType.Text = BS.T_Selected_PaymentType;

                cmbParty.Text          = strParty;
                cmbInvoiceSeries.Text  = strInvoiceSeries;
                txtInvoiceNo.Text      = strInvoiceNo;
                txtDiscountRate.Text   = string.Format("{0:0.00}", dblDiscountRate);
                txtDiscountAmount.Text = string.Format("{0:0.00}", dblDiscountAmount);
                txtAdditionalCost.Text = string.Format("{0:0.00}", dblAdditionalCost);
                txtRoundedOff.Text     = string.Format("{0:0.00}", dblRoundedOff);

                // DISPLAY ITEM DETAIL GRID
                strSQL = "SELECT TRN_INVOICE_DETAIL.INVOICE_DETAIL_ID AS INVOICE_DETAIL_ID," +
                         "       ''                                   AS SL_NO," +
                         "       MST_ITEM.ITEM_NAME                   AS ITEM_NAME," +
                         "       TRN_INVOICE_DETAIL.QUANTITY          AS QUANTITY," +
                         "       MST_ITEM.UNIT                        AS UNIT," +
                         "       TRN_INVOICE_DETAIL.RATE              AS RATE," +
                         "       0                                    AS AMOUNT," +
                         "       TRN_INVOICE_DETAIL.REMARKS           AS REMARKS," +
                         "       TRN_INVOICE_DETAIL.ITEM_ID           AS ITEM_ID " +
                         "FROM   TRN_INVOICE_DETAIL," +
                         "       MST_ITEM " +
                         "WHERE  TRN_INVOICE_DETAIL.ITEM_ID           = MST_ITEM.ITEM_ID " +
                         "AND    TRN_INVOICE_DETAIL.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";

                // declare & initialize the dml object
                DMLService dml = new DMLService();

                ADODB._Recordset rs = dml.J_ExecSqlReturnADODBRecordset(strSQL);
                if (rs == null) return false;

                this.setDetailsGridRefresh(flxgrdDetails);
                if (rs.RecordCount > 0)
                {
                    // Clear the Flexgrid data
                    flxgrdDetails.Clear();
                    flxgrdDetails.DataSource = (msdatasrc.DataSource)rs;
                }
                setDetailsGridColumns(flxgrdDetails);
                rs.Close();

                // DISPLAY TAX DETAIL GRID
                strSQL = "SELECT MST_TAX.TAX_ID             AS TAX_ID," +
                         "       MST_TAX.TAX_DESC           AS TAX_DESC," +
                         "       TRN_INVOICE_TAX.TAX_RATE   AS TAX_RATE," +
                         "       TRN_INVOICE_TAX.TAX_AMOUNT AS TAX_AMOUNT " +
                         "FROM   TRN_INVOICE_TAX," +
                         "       MST_TAX " +
                         "WHERE  TRN_INVOICE_TAX.TAX_ID            = MST_TAX.TAX_ID " +
                         "AND    TRN_INVOICE_TAX.INVOICE_HEADER_ID = " + InvoiceHeaderId + "";

                ADODB._Recordset rsTax = dml.J_ExecSqlReturnADODBRecordset(strSQL);
                if (rsTax == null) return false;

                this.setDetailsGridRefresh_TAX(flxgrdTax);
                if (rsTax.RecordCount > 0)
                {
                    // Clear the Flexgrid data
                    flxgrdTax.Clear();
                    flxgrdTax.DataSource = (msdatasrc.DataSource)rsTax;
                }
                setDetailsGridColumns_TAX(flxgrdTax);
                rsTax.Close();

                // dml object is disposed
                dml.Dispose();

                // calculate amount
                CalculateNetAmount();

                //--------------------------------------------------------------------------
                //ADDED BY DHRUB On 21/05/2015 TO CHECK THE INVOICE IS EDITABLE OR NOT 
                //--------------------------------------------------------------------------
                strSQL = @" SELECT COUNT(TRN_COLLECTION_HEADER.RECONCILIATION_DATE) AS NOOF_RECONCILE
                            FROM   TRN_COLLECTION_DETAIL
                                   INNER JOIN TRN_COLLECTION_HEADER
                                   ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID    
                            WHERE  TRN_COLLECTION_HEADER.RECONCILIATION_DATE IS NOT NULL
                            AND    TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID = " + InvoiceHeaderId;

                if (Convert.ToDouble(dmlService.J_ExecSqlReturnScalar(strSQL)) > 0)
                    blnInvoiceWiseReconciled = true;
                else
                    blnInvoiceWiseReconciled = false;

                return true;
            }
            catch
            {
                reader.Dispose();
                return false;
            }
        }
        #endregion

        #region Set_SLNO
        private void Set_SLNO()
        {
            int intSLNO = 0;
            for (int intCounter = 1; intCounter <= flxgrdDetails.Rows - 1; intCounter++)
                flxgrdDetails.set_TextMatrix(intCounter, (int)enmInvoiceEntry.SL_NO, Convert.ToString(++intSLNO));
        }
        #endregion

        #region PrintInvoice
        private bool PrintInvoice(long lngInvoiceHeaderId)
        {
            if (cmnService.J_UserMessage(J_Msg.WantToPrint,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return false;
            }

            TrnInvoicePrint frmInvoicePrint = new TrnInvoicePrint(lngInvoiceHeaderId);
            frmInvoicePrint.Text = "Invoice Print";
            frmInvoicePrint.ShowDialog();

            return true;
        }
        #endregion

        #region GetInvoiceNo
        private void GetInvoiceNo()
        {
            txtInvoiceNo.Text = "";
            if (cmbInvoiceSeries.SelectedIndex <= 0) return;

            strSQL = "SELECT LAST_NO " +
                     "FROM   MST_INVOICE_SERIES " +
                     "WHERE  BRANCH_ID         = " + J_Var.J_pBranchId + " " +
                     "AND    INVOICE_SERIES_ID = " + cmnService.J_GetComboBoxItemId(ref cmbInvoiceSeries, cmbInvoiceSeries.SelectedIndex) + "";
            long lngLastNo = Convert.ToInt64(dmlService.J_ExecSqlReturnScalar(strSQL));
            txtInvoiceNo.Text = cmbInvoiceSeries.Text + "/" + cmnService.J_FormatToString(lngLastNo, "00000");
        }
        #endregion


        #endregion
    }
}

