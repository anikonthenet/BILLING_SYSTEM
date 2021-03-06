
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: CommonService
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Class & Methods
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;

using Microsoft.Win32;

using BillingSystem.FormRpt;
using BillingSystem.FormGen;

using Microsoft.VisualBasic.Compatibility.VB6;
using ICSharpCode.SharpZipLib.Zip;
using AxMSHierarchicalFlexGridLib;

using System.Runtime.InteropServices;
using System.Reflection;

#endregion

namespace BillingSystem.Classes
{
    public class CommonService : IDisposable
    {

        #region PRIVATE VARIABLES DECLERATION

        private ToolTip toolTip = new ToolTip();
        private string strText = string.Empty;
        
        #endregion

        #region PRIVATE METHODS

        #region DISPOSE
        private void Dispose(bool Disposing)
        {
            this.toolTip.Dispose();
        }
        #endregion

        #region Words
        private string Words(int iPaise)
        {
            string strWPaise;
            int int1;
            int int2;
            string[] strDigit = new string[9];
            string[] strDecade = new string[9];
            string[] strTens = new string[9];

            if (iPaise == 0)
                return "";

            strDigit[0] = "One";
            strDigit[1] = "Two";
            strDigit[2] = "Three";
            strDigit[3] = "Four";
            strDigit[4] = "Five";
            strDigit[5] = "Six";
            strDigit[6] = "Seven";
            strDigit[7] = "Eight";
            strDigit[8] = "Nine";

            strDecade[0] = "Eleven";
            strDecade[1] = "Twelve";
            strDecade[2] = "Thirteen";
            strDecade[3] = "Fourteen";
            strDecade[4] = "Fifteen";
            strDecade[5] = "Sixteen";
            strDecade[6] = "Seventeen";
            strDecade[7] = "Eighteen";
            strDecade[8] = "Nineteen";

            strTens[0] = "Ten";
            strTens[1] = "Twenty";
            strTens[2] = "Thirty";
            strTens[3] = "Forty";
            strTens[4] = "Fifty";
            strTens[5] = "Sixty";
            strTens[6] = "Seventy";
            strTens[7] = "Eighty";
            strTens[8] = "Ninety";

            if (iPaise > 10 && iPaise < 20)
                strWPaise = strDecade[iPaise - 11];
            else
            {
                if (iPaise < 10)
                    strWPaise = strDigit[iPaise - 1];
                else
                {
                    int1 = Convert.ToInt32(iPaise / 10);
                    int2 = iPaise % 10;
                    if (int2 == 0)
                        strWPaise = strTens[int1 - 1];
                    else
                        strWPaise = strTens[int1 - 1] + " " + strDigit[int2 - 1];
                }
            }
            return strWPaise;
        }
        #endregion

        #region For Create Table Script
        private string m_Create_Table_Script(string ColumnName, J_ColumnType ColumnType, J_Identity Identity, int ColumnSize, J_DefaultValue enmDefaultValue, object DefaultValue)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                switch (ColumnType)
                {
                    case J_ColumnType.String:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = "[" + ColumnName + "] [VARCHAR] (" + ColumnSize + ")";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = "[" + ColumnName + "] [VARCHAR] (" + ColumnSize + ") DEFAULT('')";
                            else
                                strText = "[" + ColumnName + "] [VARCHAR] (" + ColumnSize + ") DEFAULT('" + this.J_ReplaceQuote(Convert.ToString(DefaultValue)) + "')";
                        }
                        break;
                    case J_ColumnType.Char:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = "[" + ColumnName + "] [CHAR] (" + ColumnSize + ")";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = "[" + ColumnName + "] [CHAR] (" + ColumnSize + ") DEFAULT('')";
                            else
                                strText = "[" + ColumnName + "] [CHAR] (" + ColumnSize + ") DEFAULT('" + this.J_ReplaceQuote(Convert.ToString(DefaultValue)) + "')";
                        }
                        break;
                    case J_ColumnType.Integer:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = "[" + ColumnName + "] [INT]";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = "[" + ColumnName + "] [INT] DEFAULT(0)";
                            else
                                strText = "[" + ColumnName + "] [INT] DEFAULT(" + Convert.ToInt32(DefaultValue) + ")";
                        }
                        break;
                    case J_ColumnType.Long:
                        strText = "[" + ColumnName + "] [BIGINT] ";
                        if (Identity == J_Identity.YES)
                            strText = strText + "IDENTITY(1,1) NOT NULL";
                        else if (Identity == J_Identity.NO)
                        {
                            if (enmDefaultValue == J_DefaultValue.YES)
                            {
                                if (DefaultValue == null)
                                    strText = strText + "DEFAULT(0)";
                                else
                                    strText = strText + "DEFAULT(" + Convert.ToInt64(DefaultValue) + ")";
                            }
                        }
                        break;
                    case J_ColumnType.Double:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = "[" + ColumnName + "] [MONEY]";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = "[" + ColumnName + "] [MONEY] DEFAULT(0)";
                            else
                                strText = "[" + ColumnName + "] [MONEY] DEFAULT(" + Convert.ToInt64(DefaultValue) + ")";
                        }
                        break;
                    case J_ColumnType.DateTime:
                        strText = "[" + ColumnName + "] [DATETIME]";
                        break;
                    default:
                        break;
                }
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                switch (ColumnType)
                {
                    case J_ColumnType.String:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = ColumnName + " TEXT(" + ColumnSize + ")";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = ColumnName + " TEXT(" + ColumnSize + ") DEFAULT \"\"";
                            else
                                strText = ColumnName + " TEXT(" + ColumnSize + ")";
                        }
                        break;
                    case J_ColumnType.Char:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = ColumnName + " TEXT(" + ColumnSize + ")";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = ColumnName + " TEXT(" + ColumnSize + ") DEFAULT \"\"";
                            else
                                strText = ColumnName + " TEXT(" + ColumnSize + ")";
                        }
                        break;
                    case J_ColumnType.Integer:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = ColumnName + " LONG";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = ColumnName + " LONG DEFAULT 0";
                            else
                                strText = ColumnName + " LONG DEFAULT " + Convert.ToInt32(DefaultValue) + "";
                        }
                        break;
                    case J_ColumnType.Long:
                        if (Identity == J_Identity.YES)
                            strText = ColumnName + " COUNTER";
                        else if (Identity == J_Identity.NO)
                        {
                            if (enmDefaultValue == J_DefaultValue.YES)
                            {
                                if (DefaultValue == null)
                                    strText = ColumnName + " LONG DEFAULT 0";
                                else
                                    strText = ColumnName + " LONG DEFAULT " + Convert.ToInt64(DefaultValue) + "";
                            }
                        }
                        break;
                    case J_ColumnType.Double:
                        if (enmDefaultValue == J_DefaultValue.NO)
                            strText = ColumnName + " CURRENCY";
                        else if (enmDefaultValue == J_DefaultValue.YES)
                        {
                            if (DefaultValue == null)
                                strText = ColumnName + " CURRENCY DEFAULT 0";
                            else
                                strText = ColumnName + " CURRENCY DEFAULT " + Convert.ToInt64(DefaultValue) + "";
                        }
                        break;
                    case J_ColumnType.DateTime:
                        strText = ColumnName + " DATETIME";
                        break;
                    default:
                        break;
                }
            }
            return strText;

        }
        #endregion



        #endregion

        #region PUBLIC METHODS

        #region DISPOSE
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion

        #region J_Encode
        public string J_Encode(string stringText)
        {
            try
            {
                byte[] encbuff = Encoding.UTF8.GetBytes(stringText);
                return Convert.ToBase64String(encbuff);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region J_Decode
        public string J_Decode(string stringText)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(stringText);
                return Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region REPLACE QUOTE
        public string J_ReplaceQuote(string StrText)
        {
            return StrText.Replace("'", "''");
        }
        #endregion

        #region USER MESSAGE FOR STAND ALONE APPLICATION [ OVERLOADED METHOD ]

        #region J_UserMessage [1]
        public DialogResult J_UserMessage(string DisplayText)
        {
            return this.J_UserMessage(DisplayText, J_Var.J_pProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [2]
        public DialogResult J_UserMessage(string DisplayText, MessageBoxButtons eMessageBoxButtons)
        {
            return this.J_UserMessage(DisplayText, J_Var.J_pProjectName, eMessageBoxButtons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [3]
        public DialogResult J_UserMessage(string DisplayText, MessageBoxIcon eMessageBoxIcon)
        {
            return this.J_UserMessage(DisplayText, J_Var.J_pProjectName, MessageBoxButtons.OK, eMessageBoxIcon, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [4]
        public DialogResult J_UserMessage(string DisplayText, MessageBoxButtons eMessageBoxButtons, MessageBoxIcon eMessageBoxIcon)
        {
            return this.J_UserMessage(DisplayText, J_Var.J_pProjectName, eMessageBoxButtons, eMessageBoxIcon, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [5]
        public DialogResult J_UserMessage(string DisplayText, string ProjectName)
        {
            return this.J_UserMessage(DisplayText, ProjectName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [6]
        public DialogResult J_UserMessage(string DisplayText, string ProjectName, MessageBoxButtons eMessageBoxButtons)
        {
            return this.J_UserMessage(DisplayText, ProjectName, eMessageBoxButtons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [7]
        public DialogResult J_UserMessage(string DisplayText, string ProjectName, MessageBoxIcon eMessageBoxIcon)
        {
            return this.J_UserMessage(DisplayText, ProjectName, MessageBoxButtons.OK, eMessageBoxIcon, MessageBoxDefaultButton.Button1);
        }
        #endregion

        #region J_UserMessage [8]
        public DialogResult J_UserMessage(string DisplayText, MessageBoxButtons eMessageBoxButtons, MessageBoxIcon eMessageBoxIcon, MessageBoxDefaultButton eMessageBoxDefaultButton)
        {
            if (DisplayText == "" || DisplayText == null)
                return DialogResult.None;
            return MessageBox.Show(DisplayText, J_Var.J_pProjectName, eMessageBoxButtons, eMessageBoxIcon, eMessageBoxDefaultButton);
        }
        #endregion

        #region J_UserMessage [8]
        public DialogResult J_UserMessage(string DisplayText, string ProjectName, MessageBoxButtons eMessageBoxButtons, MessageBoxIcon eMessageBoxIcon, MessageBoxDefaultButton eMessageBoxDefaultButton)
        {
            if (DisplayText == "" || DisplayText == null)
                return DialogResult.None;
            return MessageBox.Show(DisplayText, ProjectName, eMessageBoxButtons, eMessageBoxIcon, eMessageBoxDefaultButton);
        }
        #endregion


        #endregion

        #region PANEL MESSAGE [ OVERLOADED METHOD ]

        #region J_PanelMessage [1]
        public void J_PanelMessage(J_PanelIndex PanelIndex)
        {
            this.J_PanelMessage(PanelIndex, "NONE", Color.Empty, Color.Empty);
        }
        #endregion
        
        #region J_PanelMessage [2]
        public void J_PanelMessage(J_PanelIndex PanelIndex, string sMessage)
        {
            this.J_PanelMessage(PanelIndex, sMessage, Color.Empty, Color.Empty);
        }
        #endregion

        #region J_PanelMessage [3]
        public void J_PanelMessage(J_PanelIndex PanelIndex, string sMessage, Color foreColor)
        {
            this.J_PanelMessage(PanelIndex, sMessage, foreColor, Color.Empty);
        }
        #endregion

        #region J_PanelMessage [4]
        public void J_PanelMessage(J_PanelIndex PanelIndex, string sMessage, Color foreColor, Color backColor)
        {
            if (foreColor == Color.Empty && backColor == Color.Empty)
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].Text = sMessage;
            else if (foreColor != Color.Empty && backColor == Color.Empty)
            {
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].Text = sMessage;
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].ForeColor = foreColor;
            }
            else if (foreColor == Color.Empty && backColor != Color.Empty)
            {
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].Text = sMessage;
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].BackColor = backColor;
            }
            else if (foreColor != Color.Empty && backColor != Color.Empty)
            {
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].Text = sMessage;
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].ForeColor = foreColor;
                J_Var.frmMain.stbMessage.Items[(int)PanelIndex].BackColor = backColor;
            }
        }
        #endregion

        #endregion
        

        #region SHOW CHILD FORM

        #region J_ShowChildForm
        public void J_ShowChildForm(Form Child, Form Parent, string FormTitle)
        {
            foreach (Form frmChild in Parent.MdiChildren)
            {
                if (string.Compare(Child.Name, frmChild.Name, true) == 0)
                {
                    frmChild.Activate();
                    return;
                }
            }
            Child.WindowState = FormWindowState.Maximized;
            Child.Text = FormTitle;
            Child.MdiParent = Parent;
            Child.Show();
        }
        #endregion

        #endregion

        #region SHOW CHILD REPORT FORM

        #region J_ShowChildReportForm
        public void J_ShowChildReportForm(Form Parent, J_Reports enmReport, string ReportTitle)
        {
            RptDialog childForm = new RptDialog();
            foreach (Form frmChild in Parent.MdiChildren)
                if (string.Compare(childForm.Name, frmChild.Name, true) == 0)
                    frmChild.Dispose();
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Text = ReportTitle;
            childForm.MdiParent = Parent;
            childForm.SetRptDialogOptions(enmReport, ReportTitle);
            childForm.Show();
        }
        #endregion

        #endregion

        #region GRID TOOL TIP [ OVERLOADED METHOD ]

        #region J_GridToolTip [1]
        public void J_GridToolTip(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid MSHFlexGridName, int x, int y)
        {
            this.J_GridToolTip(MSHFlexGridName, x, y, null);
        }
        #endregion

        #region J_GridToolTip [2]
        public void J_GridToolTip(AxMSHierarchicalFlexGridLib.AxMSHFlexGrid MSHFlexGridName, int x, int y, string Message)
        {
            if (Message == "" || Message == null)
            {
                int intRowIndexValue, intColumnIndexValue;
                int intH1_RowIndexValue, intH2_RowIndexValue;
                int intW1_ColumnIndexValue, intW2_ColumnIndexValue;

                for (intRowIndexValue = 0; intRowIndexValue <= MSHFlexGridName.Rows - 1; intRowIndexValue++)
                {
                    intH1_RowIndexValue = (int)Support.TwipsToPixelsY((double)MSHFlexGridName.get_RowPos(intRowIndexValue));
                    intH2_RowIndexValue = (int)Support.TwipsToPixelsY((double)MSHFlexGridName.get_RowHeight(intRowIndexValue));

                    if (y >= intH1_RowIndexValue && y <= (intH1_RowIndexValue + intH2_RowIndexValue))
                        break;
                }

                for (intColumnIndexValue = 0; intColumnIndexValue <= MSHFlexGridName.get_Cols(0) - 1; intColumnIndexValue++)
                {
                    intW1_ColumnIndexValue = (int)Support.TwipsToPixelsX(MSHFlexGridName.get_ColPos(intColumnIndexValue));
                    intW2_ColumnIndexValue = (int)Support.TwipsToPixelsX(MSHFlexGridName.get_ColWidth(intColumnIndexValue, 0));

                    if (x >= intW1_ColumnIndexValue && x <= (intW1_ColumnIndexValue + intW2_ColumnIndexValue))
                        break;
                }

                if (intRowIndexValue > 0
                    && intRowIndexValue <= MSHFlexGridName.Rows - 1
                    && intColumnIndexValue > 0
                    && intColumnIndexValue <= MSHFlexGridName.get_Cols(0) - 1)
                    J_Var.frmMain.ToolTipCustom.SetToolTip(MSHFlexGridName, MSHFlexGridName.get_TextMatrix(intRowIndexValue, intColumnIndexValue));
                else
                    J_Var.frmMain.ToolTipCustom.SetToolTip(MSHFlexGridName, "");
            }
            else
                J_Var.frmMain.ToolTipCustom.SetToolTip(MSHFlexGridName, Message);

        }
        #endregion

        #region J_GridToolTip [3]
        public void J_GridToolTip(DataGrid datagrid, int X, int Y)
        {
            this.J_GridToolTip(datagrid, X, Y, null);
        }
        #endregion

        #region J_GridToolTip [4]
        public void J_GridToolTip(DataGrid datagrid, int X, int Y, string Message)
        {
            if (Message == "" || Message == null)
            {
                DataGrid.HitTestInfo hitTestInfo = datagrid.HitTest(X, Y);
                if (hitTestInfo.Type == DataGrid.HitTestType.Cell)
                    toolTip.SetToolTip(datagrid, Convert.ToString(datagrid[hitTestInfo.Row, hitTestInfo.Column]));
                else
                    toolTip.SetToolTip(datagrid, "");
            }
            else
                toolTip.SetToolTip(datagrid, Message);
        }
        #endregion

        #endregion

        #region BUSY MODE

        #region J_BusyMode
        public void J_BusyMode()
        {
            Cursor.Current = Cursors.WaitCursor;
        }
        #endregion

        #endregion

        #region NORMAL MODE

        #region J_NormalMode
        public void J_NormalMode()
        {
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #endregion


        #region J_AutoCompleteCombo_KeyUp
        public void J_AutoCompleteCombo_KeyUp(ref ComboBox combobox, KeyEventArgs e)
        {
            string strTypedText;
            int intFoundIndex;
            object objFoundItem;
            string strFoundText;
            string strAppendText;

            switch (e.KeyCode)
            {
                case Keys.Back: return;
                case Keys.Left: return;
                case Keys.Right: return;
                case Keys.Up: return;
                case Keys.Delete: return;
                case Keys.Down: return;
            }

            strTypedText = combobox.Text;
            intFoundIndex = combobox.FindString(strTypedText);

            if (intFoundIndex >= 0)
            {
                objFoundItem = combobox.Items[intFoundIndex];
                strFoundText = combobox.GetItemText(objFoundItem);
                strAppendText = strFoundText.Substring(strTypedText.Length);
                combobox.Text = strTypedText + strAppendText;
                combobox.SelectionStart = strTypedText.Length;
                combobox.SelectionLength = strAppendText.Length;
            }
        }
        #endregion

        #region J_AutoCompleteCombo_Leave
        public void J_AutoCompleteCombo_Leave(ref ComboBox combobox)
        {
            int intFoundIndex = combobox.FindStringExact(combobox.Text.Trim());
            combobox.SelectedIndex = intFoundIndex;
        }
        #endregion


        #region J_NullToText
        public string J_NullToText(object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        #endregion

        #region J_NullToZero
        public long J_NullToZero(object obj)
        {
            try
            {
                if (obj == null)
                    return 0;
                else
                    return this.J_ReturnInt64Value(obj);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region J_Left
        public string J_Left(string String, int length)
        {
            return String.Substring(0, length);
        }
        #endregion

        #region J_Right
        public string J_Right(string String, int length)
        {
            return String.Substring(String.Length - length, length);
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

        #region J_IsNumeric
        public bool J_IsNumeric(string strText)
        {
            if (strText == "" || strText == null) return false;
            if (strText.Length > 0)
            {
                double dblOut;
                CultureInfo cultureInfo = new CultureInfo("en-US", true);
                return double.TryParse(strText, NumberStyles.Any, cultureInfo.NumberFormat, out dblOut);
            }
            else
                return false;
        }

        #endregion

        #region RETURN INT16 VALUE [ OVERLOADED METHOD ]

        #region J_ReturnInt16Value [1]
        public Int16 J_ReturnInt16Value(char text)
        {
            return Convert.ToInt16(text);
        }
        #endregion

        #region J_ReturnInt16Value [2]
        public Int16 J_ReturnInt16Value(string strText)
        {
            if (strText == "" || strText == null) return 0;
            if (strText == "-") return 0;
            return Convert.ToInt16(strText.Trim() == "" ? "0" : strText.Trim());
        }
        #endregion

        #endregion

        #region RETURN INT32 VALUE [ OVERLOADED METHOD ]
        
        #region J_ReturnInt32Value [1]
        public Int32 J_ReturnInt32Value(char text)
        {
            return Convert.ToInt32(text);
        }
        #endregion

        #region J_ReturnInt32Value [2]
        public Int32 J_ReturnInt32Value(string strText)
        {
            if (strText == "" || strText == null) return 0;
            if (strText == "-") return 0;
            return Convert.ToInt32(strText.Trim() == "" ? "0" : strText.Trim());
        }
        #endregion

        #endregion

        #region RETURN INT64 VALUE [ OVERLOADED METHOD ]

        #region J_ReturnInt64Value [1]
        public Int64 J_ReturnInt64Value(char text)
        {
            return Convert.ToInt64(text);
        }
        #endregion

        #region J_ReturnInt64Value [2]
        public Int64 J_ReturnInt64Value(string strText)
        {
            if (strText == "" || strText == null) return 0;
            if (strText == "-") return 0;
            return Convert.ToInt64(strText.Trim() == "" ? "0" : strText.Trim());
        }
        #endregion

        #region J_ReturnInt64Value [3]
        public Int64 J_ReturnInt64Value(object obj)
        {
            try
            {
                if (obj == null) return 0;
                return Convert.ToInt64(obj == null ? "0" : obj);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region RETURN DOUBLE VALUE [ OVERLOADED METHOD ]

        #region J_ReturnDoubleValue [1]
        public double J_ReturnDoubleValue(string strText)
        {
            if (strText == "" || strText == null) return 0;
            if (strText == "." || strText == "-" || strText == "-.") return 0;
            return Convert.ToDouble(strText.Trim() == "" ? "0" : strText.Trim());
        }
        #endregion

        #region J_ReturnDoubleValue [2]
        public double J_ReturnDoubleValue(object obj)
        {
            if (obj == null) return 0;
            return Convert.ToDouble(obj == null ? "0" : obj);
        }
        #endregion

        #endregion

        #region GENERATE DATA GRID VIEW SELECTED ID [ OVERLOADED METHOD ]

        #region J_GenerateDataGridViewSelectedId [1]
        public string J_GenerateDataGridViewSelectedId(DataGridView dataGridView)
        {
            string strItem = "";
            if (dataGridView.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                        strItem = strItem + "," + row.Cells[1].Value.ToString();

                if (strItem.Length > 0)
                    strItem = this.J_Mid(strItem, 1, strItem.Length - 1);
            }
            return strItem;
        }
        #endregion

        #region J_GenerateDataGridViewSelectedId [2]
        public string J_GenerateDataGridViewSelectedId(DataGridView dataGridView, out long SelectedItemCount)
        {
            SelectedItemCount = 0;
            string strItem = "";
            if (dataGridView.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && (bool)row.Cells[0].Value == true)
                    {
                        strItem = strItem + "," + row.Cells[1].Value.ToString();
                        SelectedItemCount = SelectedItemCount + 1;
                    }
                }

                if (strItem.Length > 0)
                    strItem = this.J_Mid(strItem, 1, strItem.Length - 1);
            }
            return strItem;
        }
        #endregion

        #endregion

        #region RETURN ASCII CODE [ OVERLOADED METHOD ]

        #region J_ReturnAsciiCode [1]
        public long J_ReturnAsciiCode(string strText)
        {
            return this.J_ReturnAsciiCode(strText, J_ExportImport.NO);
        }
        #endregion

        #region J_ReturnAsciiCode [2]
        public long J_ReturnAsciiCode(string strText, J_ExportImport ExportImport)
        {
            string strAscii = "";
            long lngAscii = 0;

            Encoding ascii = Encoding.ASCII;
            Byte[] encodedBytes = ascii.GetBytes(strText);
            foreach (Byte bytes in encodedBytes)
            {
                if(ExportImport == J_ExportImport.NO)
                    strAscii = strAscii + bytes.ToString();
                else
                    lngAscii = lngAscii + Convert.ToInt64(bytes.ToString());
            }
            if (ExportImport == J_ExportImport.NO)
                return Convert.ToInt64(strAscii);
            else
                return lngAscii;
        }
        #endregion

        #endregion

        #region J_CheckAlphabetsNumeric
        public bool J_CheckAlphabetsNumeric(string strText, J_DataType DataType)
        {
            string strRegex = string.Empty;
            
            if (DataType == J_DataType.Character)
                strRegex = "[^a-zA-Z,]";
            else if (DataType == J_DataType.Numeric)
                strRegex = "[^0-9,]";
            else if (DataType == J_DataType.BlankSpace)
                strRegex = "^[\\s]*$";

            Regex rgx = new Regex(strRegex);
            return !rgx.IsMatch(strText);
        }
        #endregion

        #region J_Inwords
        public string J_Inwords(double Amount)
        {
            int iPaise;
            int iPos;
            int iNum;
            int iVlu;
            string[] Tabs = new string[6];
            string strAmt;
            string strResult;

            Tabs[0] = " Milions ";
            Tabs[1] = " Crores ";
            Tabs[2] = " Lakhs ";
            Tabs[3] = " Thousand ";
            Tabs[4] = " Hundred ";
            Tabs[5] = " ";

            strAmt = string.Format("{0:00000000000.00}", Amount);

            strResult = "";
            iPos = 0;

            for (int i = 0; i <= 5; i++)
            {
                iNum = (i == 4 ? 1 : 2);
                iVlu = this.J_ReturnInt32Value(this.J_Mid(strAmt, iPos, iNum));
                iPos = iPos + iNum;
                if (iVlu > 0)
                    strResult = strResult + Words(iVlu) + Tabs[i];
            }

            iPaise = this.J_ReturnInt32Value(this.J_Right(strAmt, 2));
            if (iPaise > 0)
            {
                if (strResult == "")
                    strResult = Words(this.J_ReturnInt32Value(iPaise.ToString())) + " paise";
                else
                    strResult = strResult + "and " + Words(this.J_ReturnInt32Value(iPaise.ToString())) + " paise";
            }

            if (strResult.Trim() == "")
                strResult = "Zero only.";
            else
                strResult = strResult + " only.";

            return strResult;
        }
        #endregion

        #region J_StatusButton
        public void J_StatusButton(GenForm FormName, string Mode)
        {
            FormName.BtnAdd.BackColor = Color.LightGray;
            FormName.BtnEdit.BackColor = Color.LightGray;
            FormName.BtnSave.BackColor = Color.LightGray;
            FormName.BtnCancel.BackColor = Color.LightGray;
            FormName.BtnSort.BackColor = Color.LightGray;
            FormName.BtnSearch.BackColor = Color.LightGray;
            FormName.BtnDelete.BackColor = Color.LightGray;
            FormName.BtnRefresh.BackColor = Color.LightGray;
            FormName.BtnPrint.BackColor = Color.LightGray;
            FormName.BtnExit.BackColor = Color.LightGray;

            switch (Mode)
            {
                case J_Mode.ViewListing:
                    FormName.ViewGrid.Visible = true;
                    FormName.grpSort.Visible = false;
                    FormName.grpSearch.Visible = false;
                    FormName.BtnAdd.Enabled = false;
                    FormName.BtnEdit.Enabled = true;
                    FormName.BtnEdit.BackColor = Color.Lavender;
                    FormName.BtnSave.Enabled = false;
                    FormName.BtnCancel.Enabled = false;
                    FormName.BtnSort.Enabled = false;
                    FormName.BtnSearch.Enabled = false;
                    FormName.BtnDelete.Enabled = false;
                    FormName.BtnRefresh.Enabled = true;
                    FormName.BtnRefresh.BackColor = Color.Lavender;
                    FormName.BtnPrint.Enabled = true;
                    FormName.BtnPrint.BackColor = Color.Lavender;
                    FormName.BtnExit.Enabled = true;
                    FormName.BtnExit.BackColor = Color.Lavender;
                    break;
                case J_Mode.View:
                    FormName.ViewGrid.Visible = true;
                    FormName.grpSort.Visible = false;
                    FormName.grpSearch.Visible = false;
                    FormName.BtnAdd.Enabled = true;
                    FormName.BtnAdd.BackColor = Color.Lavender;
                    FormName.BtnEdit.Enabled = true;
                    FormName.BtnEdit.BackColor = Color.Lavender;
                    FormName.BtnSave.Enabled = false;
                    FormName.BtnCancel.Enabled = false;
                    FormName.BtnSort.Enabled = true;
                    FormName.BtnSort.BackColor = Color.Lavender;
                    FormName.BtnSearch.Enabled = true;
                    FormName.BtnSearch.BackColor = Color.Lavender;
                    FormName.BtnDelete.Enabled = true;
                    FormName.BtnDelete.BackColor = Color.Lavender;
                    FormName.BtnRefresh.Enabled = true;
                    FormName.BtnRefresh.BackColor = Color.Lavender;
                    FormName.BtnPrint.Enabled = true;
                    FormName.BtnPrint.BackColor = Color.Lavender;
                    FormName.BtnExit.Enabled = true;
                    FormName.BtnExit.BackColor = Color.Lavender;
                    break;
                case J_Mode.Add:
                    FormName.ViewGrid.Visible = false;
                    FormName.grpSort.Visible = false;
                    FormName.grpSearch.Visible = false;
                    FormName.BtnAdd.Enabled = false;
                    FormName.BtnEdit.Enabled = false;
                    FormName.BtnSave.Enabled = true;
                    FormName.BtnSave.BackColor = Color.Lavender;
                    FormName.BtnCancel.Enabled = true;
                    FormName.BtnCancel.BackColor = Color.Lavender;
                    FormName.BtnSort.Enabled = false;
                    FormName.BtnSearch.Enabled = false;
                    FormName.BtnDelete.Enabled = false;
                    FormName.BtnRefresh.Enabled = false;
                    FormName.BtnPrint.Enabled = false;
                    FormName.BtnExit.Enabled = false;
                    break;
                case J_Mode.Edit:
                    FormName.ViewGrid.Visible = false;
                    FormName.grpSort.Visible = false;
                    FormName.grpSearch.Visible = false;
                    FormName.BtnAdd.Enabled = false;
                    FormName.BtnEdit.Enabled = false;
                    FormName.BtnSave.Enabled = true;
                    FormName.BtnSave.BackColor = Color.Lavender;
                    FormName.BtnCancel.Enabled = true;
                    FormName.BtnCancel.BackColor = Color.Lavender;
                    FormName.BtnSort.Enabled = false;
                    FormName.BtnSearch.Enabled = false;
                    FormName.BtnDelete.Enabled = false;
                    FormName.BtnRefresh.Enabled = false;
                    FormName.BtnPrint.Enabled = false;
                    FormName.BtnExit.Enabled = false;
                    break;
            }
        }
        #endregion

        #region J_ReturnAsciiCode
        public long J_ReturnAsciiCode1(string strText)
        {
            string strAscii = "";

            Encoding ascii = Encoding.ASCII;
            Byte[] encodedBytes = ascii.GetBytes(strText);
            foreach (Byte bytes in encodedBytes)
                strAscii = strAscii + bytes.ToString();

            return Convert.ToInt64(strAscii);
        }
        #endregion

        #region SAVE CONFIRMATION MESSAGE [ OVERLOADED METHOD ]

        #region J_SaveConfirmationMessage [1]
        public bool J_SaveConfirmationMessage()
        {
            return this.J_SaveConfirmationMessage(null, J_AllowSetFocus.NO);
        }
        #endregion

        #region J_SaveConfirmationMessage [2]
        public bool J_SaveConfirmationMessage(ref TextBox control)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, J_AllowSetFocus.YES);
        }
        #endregion

        #region J_SaveConfirmationMessage [3]
        public bool J_SaveConfirmationMessage(ref TextBox control, J_AllowSetFocus AllowSetFocus)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, AllowSetFocus);
        }
        #endregion

        #region J_SaveConfirmationMessage [4]
        public bool J_SaveConfirmationMessage(ref Button control)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, J_AllowSetFocus.YES);
        }
        #endregion

        #region J_SaveConfirmationMessage [5]
        public bool J_SaveConfirmationMessage(ref Button control, J_AllowSetFocus AllowSetFocus)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, AllowSetFocus);
        }
        #endregion

        #region J_SaveConfirmationMessage [6]
        public bool J_SaveConfirmationMessage(ref ComboBox control)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, J_AllowSetFocus.YES);
        }
        #endregion

        #region J_SaveConfirmationMessage [7]
        public bool J_SaveConfirmationMessage(ref ComboBox control, J_AllowSetFocus AllowSetFocus)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, AllowSetFocus);
        }
        #endregion

        #region J_SaveConfirmationMessage [8]
        public bool J_SaveConfirmationMessage(ref MaskedTextBox control)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, J_AllowSetFocus.YES);
        }
        #endregion

        #region J_SaveConfirmationMessage [9]
        public bool J_SaveConfirmationMessage(ref MaskedTextBox control, J_AllowSetFocus AllowSetFocus)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, AllowSetFocus);
        }
        #endregion
        
        #region J_SaveConfirmationMessage [10]
        public bool J_SaveConfirmationMessage(ref RadioButton control)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, J_AllowSetFocus.YES);
        }
        #endregion
        
        #region J_SaveConfirmationMessage [11]
        public bool J_SaveConfirmationMessage(ref RadioButton control, J_AllowSetFocus AllowSetFocus)
        {
            Control ctrl = control;
            return this.J_SaveConfirmationMessage(ctrl, AllowSetFocus);
        }
        #endregion

        #region J_SaveConfirmationMessage [12]
        public bool J_SaveConfirmationMessage(Control control, J_AllowSetFocus AllowSetFocus)
        {
            if (J_Var.J_pSaveConfirmMsg == 1)
            {
                if (this.J_UserMessage(J_Msg.WantToProceed,
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    if(AllowSetFocus == J_AllowSetFocus.YES)
                        if(control != null)
                            control.Select();
                    return true;
                }
                return false;
            }
            else
                return false;
        }
        #endregion

        #endregion


        #region SQL DB FORMAT [ OVERLOADED METHOD ]

        #region J_SQLDBFormat [1]
        public string J_SQLDBFormat(string ColumnName, J_SQLColFormat SQLColFormat)
        {
            return this.J_SQLDBFormat(ColumnName, J_ColumnType.None, SQLColFormat);
        }
        #endregion

        #region J_SQLDBFormat [2]
        public string J_SQLDBFormat(string ColumnName, J_ColumnType ColumnType, J_SQLColFormat SQLColFormat)
        {
            return this.J_SQLDBFormat(ColumnName, ColumnType, SQLColFormat, null, null);
        }
        #endregion

        #region J_SQLDBFormat [3]
        public string J_SQLDBFormat(string ColumnName, J_SQLColFormat SQLColFormat, string[,] strArray)
        {
            return this.J_SQLDBFormat(ColumnName, J_ColumnType.None, SQLColFormat, strArray, null);
        }
        #endregion

        #region J_SQLDBFormat [4]
        public string J_SQLDBFormat(string ColumnName, J_ColumnType ColumnType, J_SQLColFormat SQLColFormat, string[,] strArray, string NullText)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                if (SQLColFormat == J_SQLColFormat.DateFormatDDMMYYYY)
                {
                    return "CONVERT(CHAR(10), " + ColumnName + ", 103)";
                }
                else if (SQLColFormat == J_SQLColFormat.DateFormatMMDDYYYY)
                {
                    return "CONVERT(CHAR(10), " + ColumnName + ", 101)";
                }
                else if (SQLColFormat == J_SQLColFormat.DateFormatYYYYMMDD)
                {
                    return "CONVERT(CHAR(10), " + ColumnName + ", 112)";
                }
                else if (SQLColFormat == J_SQLColFormat.NullCheck)
                {
                    if (ColumnType == J_ColumnType.Double || ColumnType == J_ColumnType.Integer || ColumnType == J_ColumnType.Long)
                        return "ISNULL(" + ColumnName + ", 0)";
                    else if (ColumnType == J_ColumnType.String)
                    {
                        if(NullText == null || NullText == "")
                            return "ISNULL(" + ColumnName + ", '')";
                        else
                            return "ISNULL(" + ColumnName + ", '" + NullText + "')";
                    }
                }
                else if (SQLColFormat == J_SQLColFormat.UCase)
                {
                    return "UPPER(" + ColumnName + ")";
                }
                else if (SQLColFormat == J_SQLColFormat.LCase)
                {
                    return "LOWER(" + ColumnName + ")";
                }
                else if (SQLColFormat == J_SQLColFormat.Case_End)
                {
                    if (strArray == null) return "";

                    strText = "(CASE ";
                    for (int i = 0; i <= strArray.GetUpperBound(0); i++)
                    {
                        if (strArray[i, 3] == "N")
                            strText = strText + "WHEN " + ColumnName + strArray[i, 0] + " THEN " + strArray[i, 2] + " ";
                        else if (strArray[i, 3] == "T")
                            strText = strText + "WHEN " + ColumnName + strArray[i, 0] + " THEN '" + strArray[i, 2] + "' ";
                    }
                    strText = strText + "END) ";
                    return strText;
                }
                else if (SQLColFormat == J_SQLColFormat.Cast)
                {
                    if (ColumnType == J_ColumnType.Integer || ColumnType == J_ColumnType.Long || ColumnType == J_ColumnType.Double)
                        strText = " CAST(ISNULL(" + ColumnName + ", 0) AS VARCHAR)";
                    else if (ColumnType == J_ColumnType.Date)
                        strText = " CAST(ISNULL(" + ColumnName + ", '') AS VARCHAR)";
                }
                return strText;
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (SQLColFormat == J_SQLColFormat.DateFormatDDMMYYYY)
                {
                    return "FORMAT(" + ColumnName + ", 'dd/MM/yyyy')";
                }
                else if (SQLColFormat == J_SQLColFormat.DateFormatMMDDYYYY)
                {
                    return "FORMAT(" + ColumnName + ", 'MM/dd/yyyy')";
                }
                else if (SQLColFormat == J_SQLColFormat.DateFormatYYYYMMDD)
                {
                    return "FORMAT(" + ColumnName + ", 'yyyy/MM/dd')";
                }
                else if (SQLColFormat == J_SQLColFormat.NullCheck)
                {
                    if (ColumnType == J_ColumnType.Double || ColumnType == J_ColumnType.Integer || ColumnType == J_ColumnType.Long)
                        return "IIF(ISNULL(" + ColumnName + ") = True, 0, " + ColumnName + ")";
                    else if (ColumnType == J_ColumnType.String)
                        return "IIF(ISNULL(" + ColumnName + ") = True, '', " + ColumnName + ")";
                }
                else if (SQLColFormat == J_SQLColFormat.UCase)
                {
                    return "UCASE(" + ColumnName + ")";
                }
                else if (SQLColFormat == J_SQLColFormat.LCase)
                {
                    return "LCASE(" + ColumnName + ")";
                }
                else if (SQLColFormat == J_SQLColFormat.Case_End)
                {
                    if (strArray == null) return "";
                    int iCount = strArray.GetUpperBound(0);

                    strText = "";
                    for (int i = 0; i <= iCount; i++)
                    {
                        if (i != iCount)
                        {
                            if (strArray[i, 3] == "N")
                                strText = strText + "IIF(" + ColumnName + strArray[i, 0] + ", " + strArray[i, 2] + ", ";
                            else if (strArray[i, 3] == "T")
                                strText = strText + "IIF(" + ColumnName + strArray[i, 0] + ", '" + strArray[i, 2] + "', ";
                        }
                        else if (i == iCount)
                        {
                            if (strArray[i, 3] == "N")
                                strText = strText + "" + strArray[i, 2] + "";
                            else if (strArray[i, 3] == "T")
                                strText = strText + "'" + strArray[i, 2] + "'";

                            for (int j = 0; j <= iCount - 1; j++)
                                strText = strText + ")";
                        }
                    }
                    return strText;
                }
                else if (SQLColFormat == J_SQLColFormat.Cast)
                {
                    if(ColumnType == J_ColumnType.Integer || ColumnType == J_ColumnType.Long)
                        strText = " CSTR(IIF(ISNULL(" + ColumnName + ") = True, 0, " + ColumnName + "))";
                    else if (ColumnType == J_ColumnType.Double)
                        strText = " CSTR(FORMAT(IIF(ISNULL(" + ColumnName + ") = True, 0, " + ColumnName + "), '0.00'))";
                    else if (ColumnType == J_ColumnType.Date)
                        strText = " CSTR(IIF(ISNULL(" + ColumnName + ") = True, '', " + ColumnName + "))";
                }
                return strText;
            }
            return "";
        }
        #endregion


        #region J_SQLDBFormat [5]
        public string J_SQLDBFormat(string ColumnName, J_SQLColFormat SQLColFormat, string Format)
        {
            return this.J_SQLDBFormat(ColumnName, SQLColFormat, Format, 0);
        }
        #endregion

        #region J_SQLDBFormat [6]
        public string J_SQLDBFormat(string ColumnName, J_SQLColFormat SQLColFormat, string Format, int Size)
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                if (SQLColFormat == J_SQLColFormat.Format)
                {
                    strText = " RIGHT('" + Format + "' + CONVERT(VARCHAR(" + Size + "), " + ColumnName + "), " + Size + ")";
                }
                return strText;
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (SQLColFormat == J_SQLColFormat.Format)
                {
                    strText = " FORMAT(" + ColumnName + ",'" + Format + "')";
                }
                return strText;
            }
            return "";
        }
        #endregion


        #region J_SQLDBFormat [7]
        public string J_SQLDBFormat(string[,] strArray, J_SQLColFormat SQLColFormat)
        {
            return this.J_SQLDBFormat(strArray, SQLColFormat, J_ElsePart.NO);
        }
        #endregion

        #region J_SQLDBFormat [8]
        public string J_SQLDBFormat(string[,] strArray, J_SQLColFormat SQLColFormat, J_ElsePart ElsePart)
        {
            strText = "";
            if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
            {
                if (SQLColFormat == J_SQLColFormat.Case_End)
                {
                    if (strArray == null) return "";

                    int iCounter = 0;
                    strText = "(CASE ";
                    if (ElsePart == J_ElsePart.NO)
                    {
                        for (iCounter = 0; iCounter <= strArray.GetUpperBound(0); iCounter++)
                        {
                            if (strArray[iCounter, 3] == "N")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN " + strArray[iCounter, 2] + " ";
                            else if (strArray[iCounter, 3] == "T")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN '" + strArray[iCounter, 2] + "' ";
                            else if (strArray[iCounter, 3] == "F")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN " + strArray[iCounter, 2] + " ";
                        }
                    }
                    else if (ElsePart == J_ElsePart.YES)
                    {
                        for (iCounter = 0; iCounter <= strArray.GetUpperBound(0) - 1; iCounter++)
                        {
                            if (strArray[iCounter, 3] == "N")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN " + strArray[iCounter, 2] + " ";
                            else if (strArray[iCounter, 3] == "T")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN '" + strArray[iCounter, 2] + "' ";
                            else if (strArray[iCounter, 3] == "F")
                                strText = strText + "WHEN " + strArray[iCounter, 0] + " THEN " + strArray[iCounter, 2] + " ";
                        }

                        if (strArray[iCounter, 3] == "N")
                            strText = strText + "ELSE " + strArray[iCounter, 2] + " ";
                        else if (strArray[iCounter, 3] == "T")
                            strText = strText + "ELSE '" + strArray[iCounter, 2] + "' ";
                        else if (strArray[iCounter, 3] == "F")
                            strText = strText + "ELSE " + strArray[iCounter, 2] + " ";
                    }
                    strText = strText + "END) ";
                    return strText;
                }
                return strText;
            }
            else if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
            {
                if (SQLColFormat == J_SQLColFormat.Case_End)
                {
                    if (strArray == null) return "";
                    int iCount = strArray.GetUpperBound(0);

                    strText = "";
                    for (int i = 0; i <= iCount; i++)
                    {
                        if (i != iCount)
                        {
                            if (strArray[i, 3] == "N")
                                strText = strText + "IIF(" + strArray[i, 0] + ", " + strArray[i, 2] + ", ";
                            else if (strArray[i, 3] == "T")
                                strText = strText + "IIF(" + strArray[i, 0] + ", '" + strArray[i, 2] + "', ";
                            else if (strArray[i, 3] == "F")
                                strText = strText + "IIF(" + strArray[i, 0] + ", " + strArray[i, 2] + ", ";
                        }
                        else if (i == iCount)
                        {
                            if (strArray[i, 3] == "N")
                                strText = strText + "" + strArray[i, 2] + "";
                            else if (strArray[i, 3] == "T")
                                strText = strText + "'" + strArray[i, 2] + "'";
                            else if (strArray[i, 3] == "F")
                                strText = strText + "" + strArray[i, 2] + "";

                            for (int j = 0; j <= iCount - 1; j++)
                                strText = strText + ")";
                        }
                    }
                    return strText;
                }
                return strText;
            }
            return "";
        }
        #endregion


        #endregion


        #region J_ConcatOperator
        public string J_ConcatOperator()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                return "&";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                return "+";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.Oracle)
                return "+";
            else
                return "";
        }
        #endregion

        #region J_OuterJoinOperator
        public string J_OuterJoinOperator()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                return "+";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                return "*";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.Oracle)
                return "*";
            else
                return "";
        }
        #endregion

        #region J_DateOperator
        public string J_DateOperator()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                return "#";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                return "'";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.Oracle)
                return "'";
            else
                return "";
        }
        #endregion

        #region J_SubstringOperator
        public string J_SubstringOperator()
        {
            if (J_Var.J_pDatabaseType == J_DatabaseType.MsAccess)
                return "MID";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.SqlServer)
                return "SUBSTRING";
            else if (J_Var.J_pDatabaseType == J_DatabaseType.Oracle)
                return "SUBSTRING";
            else
                return "";
        }
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

        #region NUMERIC DATA [ OVERLOADED METHOD ]
        
        #region J_NumericData [1]
        public object J_NumericData(TextBox textbox)
        {
            Control control = textbox;
            return this.J_NumericData(control);
        }
        #endregion

        #region J_NumericData [2]
        public object J_NumericData(ComboBox combobox)
        {
            Control control = combobox;
            return this.J_NumericData(control);
        }
        #endregion

        #region J_NumericData [3]
        public object J_NumericData(object objValue)
        {
            Control control = (Control)objValue;
            return this.J_NumericData(control);
        }
        #endregion

        #region J_NumericData [4]
        public object J_NumericData(Control control)
        {
            object NumericData = 0;
            if (control.Text == null)
                return NumericData;
            else if (control.Text.Trim() == "")
                return NumericData;
            else if (control.Text.Trim() == ".")
                return NumericData;
            else
            {
                NumericData = control.Text.Trim();
                return NumericData;
            }
        }
        #endregion

        #endregion

        #region J_Reverse
        public string J_Reverse(string strText)
        {
            char[] arr = strText.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
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

        #region J_GetFileName
        public string J_GetFileName(string FilePath)
        {
            if (FilePath == "" | FilePath == null) return "";
            FileInfo fileinfo = new FileInfo(FilePath);
            return Convert.ToString(fileinfo.Name);
        }
        #endregion

        #region GET FILES [ OVERLOADED METHOD ]

        #region J_GetFiles [1]
        public string[] J_GetFiles(string FolderPath)
        {
            return this.J_GetFiles(FolderPath, "*.txt");
        }
        #endregion

        #region J_GetFiles [2]
        public string[] J_GetFiles(string FolderPath, string Extension)
        {
            return Directory.GetFiles(FolderPath, Extension);
        }
        #endregion

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
            openFilePath.FileName   = "";
            openFilePath.Title      = DialogBoxTitle;
            openFilePath.Filter     = FilterText;

            if(DefaultExtension != "" && DefaultExtension != null)
                openFilePath.DefaultExt = DefaultExtension;
            
            openFilePath.ShowDialog();
            return Convert.ToString(openFilePath.FileName);
        }
        #endregion

        #endregion


        #region CREATE FILE [ OVERLOADED METHOD ]

        #region J_CreateFile [1]
        public void J_CreateFile(string FileNameWithPath)
        {
            this.J_CreateFile(FileNameWithPath, J_DatabaseType.Others);
        }
        #endregion

        #region J_CreateFile [2]
        public void J_CreateFile(string FileNameWithPath, J_DatabaseType DatabaseType)
        {
            this.J_CreateFile(FileNameWithPath, "jcs", DatabaseType);
        }
        #endregion

        #region J_CreateFile [3]
        public void J_CreateFile(string FileNameWithPath, string FilePassword, J_DatabaseType DatabaseType)
        {
            if (this.J_IsFileExist(FileNameWithPath) == true)
                this.J_DeleteFile(FileNameWithPath);

            if (DatabaseType == J_DatabaseType.MsAccess)
            {
                ADOX.CatalogClass catalog = new ADOX.CatalogClass();

                string strConnString = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " + FileNameWithPath + ";" +
                                        "Jet OLEDB:Engine Type = 5;Jet OLEDB:Database Password = '" + FilePassword + "'";

                catalog.Create(strConnString);

                ((ADODB.Connection)catalog.ActiveConnection).Close();
                Marshal.ReleaseComObject(catalog);

            }
            else if (DatabaseType == J_DatabaseType.Others)
            {
                File.Create(FileNameWithPath);
            }
        }
        #endregion

        #endregion

        #region J_DeleteFile
        public void J_DeleteFile(string FileNameWithPath)
        {
            if (this.J_IsFileExist(FileNameWithPath) == true)
                File.Delete(FileNameWithPath);
        }
        #endregion


        #region J_CreateDirectory
        public void J_CreateDirectory(string Path)
        {
            if(Directory.Exists(Path) == false)
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

        #region UNZIP FILE AND RETURN BOOLEAN BALUE [ OVERLOADED METHOD ]

        #region J_UnZipBool [1]
        public bool J_UnZipBool(string FilePath)
        {
            return this.J_UnZipBool(FilePath, "");
        }
        #endregion

        #region J_UnZipBool [2]
        public bool J_UnZipBool(string FilePath, string password)
        {
            try
            {
                string strFolderPath = this.J_GetDirectoryName(FilePath);
                string strFileName = this.J_GetFileName(FilePath);

                this.J_DeleteDirectory(strFolderPath + "\\" + this.J_Left(strFileName, strFileName.IndexOf(".")));

                ZipEntry theEntry;
                string tmpEntry = String.Empty;
                ZipInputStream s = new ZipInputStream(File.OpenRead(FilePath));

                if (password != null && password != String.Empty)
                    s.Password = password;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory 
                    if (strFolderPath != "")
                        Directory.CreateDirectory(strFolderPath);

                    if (fileName != String.Empty)
                    {
                        //if (theEntry.Name.IndexOf(".ini") < 0)
                        //{
                            string fullPath = strFolderPath + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);

                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                            FileStream streamWriter = File.Create(fullPath);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        //}
                    }
                }
                s.Close();
                return true;
            }
            catch(Exception exception)
            {
                this.J_UserMessage(exception.Message);
                return false;
            }
        }
        #endregion

        #endregion

        #region UNZIP FILE AND RETURN STRING BALUE [ OVERLOADED METHOD ]

        #region J_UnZipString [1]
        public string J_UnZipString(string FilePath)
        {
            return this.J_UnZipString(FilePath, "");
        }
        #endregion

        #region J_UnZipString [2]
        public string J_UnZipString(string FilePath, string password)
        {
            int iCounter = 0;
            string strFolder = string.Empty;
            
            try
            {
                string strFolderPath = this.J_GetDirectoryName(FilePath);
                string strFileName = this.J_GetFileName(FilePath);

                this.J_DeleteDirectory(strFolderPath + "\\" + this.J_Left(strFileName, strFileName.IndexOf(".")));

                ZipEntry theEntry;
                string tmpEntry = String.Empty;
                ZipInputStream s = new ZipInputStream(File.OpenRead(FilePath));

                if (password != null && password != String.Empty)
                    s.Password = password;
                
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory 
                    if (strFolderPath != "")
                        Directory.CreateDirectory(strFolderPath);
                    
                    if (fileName != String.Empty)
                    {
                        //if (theEntry.Name.IndexOf(".ini") < 0)
                        //{
                            string fullPath = strFolderPath + "\\" + theEntry.Name;
                            fullPath = fullPath.Replace("\\ ", "\\");
                            string fullDirPath = Path.GetDirectoryName(fullPath);

                            if (iCounter == 0)
                            {
                                strFolder = fullDirPath;
                                iCounter = 1;
                            }

                            if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                            FileStream streamWriter = File.Create(fullPath);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        //}
                    }
                }
                s.Close();
                return strFolder;
            }
            catch (Exception exception)
            {
                this.J_UserMessage(exception.Message);
                return strFolder;
            }
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

        #region RETURN FILE INFO [ OVERLOADED METHOD ]

        #region J_ReturnFileInfo [1]
        public FileInfo J_ReturnFileInfo(string FilePath)
        {
            return this.J_ReturnFileInfo(FilePath, null);
        }
        #endregion

        #region J_ReturnFileInfo [2]
        public FileInfo J_ReturnFileInfo(string FolderPath, string FileName)
        {
            FileInfo fileInfo = null;
            if(FileName == "" || FileName == null)
                fileInfo = new FileInfo(FolderPath);
            else
                fileInfo = new FileInfo(FolderPath + "\\" + FileName);
            return fileInfo;
        }
        #endregion

        #endregion

        #region RETURN STREAM WRITER [ OVERLOADED METHOD ]

        #region J_ReturnStreamWriter [1]
        public StreamWriter J_ReturnStreamWriter(string FilePath)
        {
            return this.J_ReturnStreamWriter(FilePath, null);
        }
        #endregion

        #region J_ReturnStreamWriter [2]
        public StreamWriter J_ReturnStreamWriter(string FolderPath, string FileName)
        {
            StreamWriter streamWriter;
            if (FileName == "" || FileName == null)
            {
                if (!File.Exists(FolderPath))
                    streamWriter = new StreamWriter(FolderPath);
                else
                    streamWriter = File.AppendText(FolderPath);
            }
            else
            {
                if (!File.Exists(FolderPath + "\\" + FileName))
                    streamWriter = new StreamWriter(FolderPath + "\\" + FileName);
                else
                    streamWriter = File.AppendText(FolderPath + "\\" + FileName);
            }
            return streamWriter;
        }
        #endregion

        #endregion

        #region WRITE INTO TEXT FILE THROUGH WRITE LINE [ OVERLOADED METHOD ]

        #region J_WriteLine [1]
        public void J_WriteLine(ref StreamWriter streamWriter, string strText)
        {
            this.J_WriteLine(ref streamWriter, strText, J_NewLine.NO, 0);
        }
        #endregion

        #region J_WriteLine [2]
        public void J_WriteLine(ref StreamWriter streamWriter, string strText, J_NewLine NewLine)
        {
            this.J_WriteLine(ref streamWriter, strText, NewLine, 1);
        }
        #endregion

        #region J_WriteLine [3]
        public void J_WriteLine(ref StreamWriter streamWriter, string strText, J_NewLine NewLine, int NoOfNewLine)
        {
            streamWriter.WriteLine(strText);
            if (NewLine == J_NewLine.YES)
            {
                for(int Line = 1; Line <= NoOfNewLine; Line++)
                    streamWriter.Write(streamWriter.NewLine);
            }
        }
        #endregion

        #endregion

        #region WRITE INTO TEXT FILE THROUGH WRITE [ OVERLOADED METHOD ]

        #region J_Write [1]
        public void J_Write(ref StreamWriter streamWriter, string strText)
        {
            this.J_Write(ref streamWriter, strText, J_NewLine.NO, 0);
        }
        #endregion

        #region J_Write [2]
        public void J_Write(ref StreamWriter streamWriter, string strText, J_NewLine NewLine)
        {
            this.J_Write(ref streamWriter, strText, NewLine, 1);
        }
        #endregion

        #region J_Write [3]
        public void J_Write(ref StreamWriter streamWriter, string strText, J_NewLine NewLine, int NoOfNewLine)
        {
            streamWriter.Write(strText);
            if (NewLine == J_NewLine.YES)
            {
                for (int Line = 1; Line <= NoOfNewLine; Line++)
                    streamWriter.Write(streamWriter.NewLine);
            }
        }
        #endregion

        #endregion

        #region GET INTERNAL NAME [ OVERLOADED METHOD ]

        #region J_GetInternalName [1]
        public string J_GetInternalName(string FilePath)
        {
            return this.J_GetInternalName(FilePath, null);
        }
        #endregion

        #region J_GetInternalName [2]
        public string J_GetInternalName(string FolderPath, string FileName)
        {
            FileVersionInfo fileVersionInfo = null;
            if(FileName == "" || FileName == null)
                fileVersionInfo = FileVersionInfo.GetVersionInfo(FolderPath);
            else
                fileVersionInfo = FileVersionInfo.GetVersionInfo(FolderPath + "\\" + FileName);
            return fileVersionInfo.InternalName.ToString();
        }
        #endregion

        #endregion

        #region GET FILE CREATION DATE TIME [ OVERLOADED METHOD ]

        #region J_GetFileCreationDateTime [1]
        public DateTime J_GetFileCreationDateTime(string FilePath)
        {
            return this.J_GetFileCreationDateTime(FilePath, null);
        }
        #endregion

        #region J_GetFileCreationDateTime [2]
        public DateTime J_GetFileCreationDateTime(string FolderPath, string FileName)
        {
            FileInfo fileInfo = null;
            if (FileName == "" || FileName == null)
                fileInfo = this.J_ReturnFileInfo(FolderPath);
            else
                fileInfo = this.J_ReturnFileInfo(FolderPath + "\\" + FileName);
            return fileInfo.CreationTime;
        }
        #endregion

        #endregion


        #region J_ReadFile
        public StreamReader J_ReadFile(string FilePath)
        {
            return File.OpenText(FilePath);
        }
        #endregion

        #region IS VALID TEXT FILE [ OVERLOADED METHOD ]

        #region J_IsValidTextFile [1]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default arguments are J_TextSeparator.Pipe | J_ContentHeader.NO | J_ShowMessage.YES
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, J_TextSeparator.Pipe, J_ContentHeader.NO, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsValidTextFile [2]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default arguments are J_TextSeparator.Pipe | J_ShowMessage.YES
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="ContentHeader">Content Header</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, J_ContentHeader ContentHeader)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, J_TextSeparator.Pipe, ContentHeader, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsValidTextFile [3]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default arguments are J_ContentHeader.NO | J_ShowMessage.YES
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="TextSeparator">Text Separator [Pipe OR Comma]</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, string TextSeparator)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, TextSeparator, J_ContentHeader.NO, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsValidTextFile [4]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default argument is J_ShowMessage.YES
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="TextSeparator">Text Separator [Pipe OR Comma]</param>
        /// <param name="ContentHeader">Content Header</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, string TextSeparator, J_ContentHeader ContentHeader)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, TextSeparator, ContentHeader, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsValidTextFile [5]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default arguments are J_TextSeparator.Pipe | J_ContentHeader.NO
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="ShowMessage">Show Message</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, J_ShowMessage ShowMessage)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, J_TextSeparator.Pipe, J_ContentHeader.NO, ShowMessage);
        }
        #endregion

        #region J_IsValidTextFile [6]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default argument is J_TextSeparator.Pipe
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="ContentHeader">Content Header</param>
        /// <param name="ShowMessage">Show Message</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, J_ContentHeader ContentHeader, J_ShowMessage ShowMessage)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, J_TextSeparator.Pipe, ContentHeader, ShowMessage);
        }
        #endregion
        
        #region J_IsValidTextFile [7]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// Default argument is J_ContentHeader.NO
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="TextSeparator">Text Separator</param>
        /// <param name="ShowMessage">Show Message</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, string TextSeparator, J_ShowMessage ShowMessage)
        {
            return this.J_IsValidTextFile(FilePath, NoOfColumns, TextSeparator, J_ContentHeader.NO, ShowMessage);
        }
        #endregion

        #region J_IsValidTextFile [8]
        /// <summary>
        /// Returns the boolean value [True OR False].
        /// No default argument
        /// </summary>
        /// <param name="FilePath">File Path</param>
        /// <param name="NoOfColumns">No. of Columns</param>
        /// <param name="TextSeparator">Text Separator</param>
        /// <param name="ContentHeader">Content Header</param>
        /// <param name="ShowMessage">Show Message</param>
        /// <returns>Returns the boolean value [True OR False]</returns>
        public bool J_IsValidTextFile(string FilePath, int NoOfColumns, string TextSeparator, J_ContentHeader ContentHeader, J_ShowMessage ShowMessage)
        {
            if (TextSeparator != J_TextSeparator.Comma && TextSeparator != J_TextSeparator.Pipe)
                return false;
            
            string strText = "";
            string strSubText1 = "";
            string strSubText2 = "";

            long IndAsciiValue = 0;
            long TotAsciiValue = 0;

            int iCounter = 0;

            string strFolderPath = this.J_GetDirectoryName(FilePath);
            StreamReader streamReader = this.J_ReadFile(FilePath);
            
            if(ContentHeader == J_ContentHeader.YES)
                strText = streamReader.ReadLine();

            while (strText != null)
            {
                strText = streamReader.ReadLine();
                strSubText2 = strText;

                IndAsciiValue = 0;
                for (iCounter = 1; iCounter <= (NoOfColumns - 1); iCounter++)
                {
                    strSubText1 = this.J_Mid(strSubText2, 0, strSubText2.IndexOf(TextSeparator, 1));
                    strSubText2 = this.J_Mid(strSubText2, strSubText2.IndexOf(TextSeparator, 1) + 1, strSubText2.Length - (strSubText2.IndexOf(TextSeparator, 1) + 1));

                    IndAsciiValue = IndAsciiValue + this.J_ReturnAsciiCode(strSubText1, J_ExportImport.YES);
                }
                TotAsciiValue = TotAsciiValue + IndAsciiValue;

                if (IndAsciiValue != Convert.ToInt64(this.J_ReturnInt64Value(strSubText2)))
                {
                    strText = streamReader.ReadLine();
                
                    if (TotAsciiValue != Convert.ToInt64(this.J_ReturnInt64Value(strSubText2)))
                    {
                        if (ShowMessage == J_ShowMessage.YES)
                            this.J_UserMessage("File has been tampered");

                        streamReader.Close();
                        streamReader.Dispose();
                    
                        if(this.J_Right(FilePath, 3).ToUpper() != "TXT")
                            this.J_DeleteDirectory(strFolderPath);
                        
                        return false;
                    }
                }
            }
            streamReader.Close();
            streamReader.Dispose();
            return true;
        }
        #endregion

        #endregion

        #region J_IsFileExist
        public bool J_IsFileExist(string FilePath)
        {
            return File.Exists(FilePath);
        }
        #endregion

        #region J_IsFolderExist
        public bool J_IsFolderExist(string FolderPath)
        {
            return Directory.Exists(FolderPath);
        }
        #endregion


        #region CREATE REGISTRY KEY [OVERLOADED METHODS]

        #region J_CreateRegistryKey [1]
        public void J_CreateRegistryKey(string RootKey)
        {
            this.J_CreateRegistryKey(RootKey, null);
        }
        #endregion

        #region J_CreateRegistryKey [2]
        public void J_CreateRegistryKey(string OpenKey, string SubKey)
        {
            this.J_CreateRegistryKey(OpenKey, SubKey, null, null);
        }
        #endregion

        #region J_CreateRegistryKey [3]
        public void J_CreateRegistryKey(string OpenKey, string SubKey, string SubKeyItemName, string KeyItemValue)
        {
            RegistryKey registryKey;
            if ((SubKey == "" || SubKey == null) && (SubKeyItemName == "" || SubKeyItemName == null) && (KeyItemValue == "" || KeyItemValue == null))
                registryKey = Registry.CurrentUser.CreateSubKey(OpenKey);
            else
            {
                registryKey = Registry.CurrentUser.OpenSubKey(OpenKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                registryKey.CreateSubKey(SubKey);

                if ((SubKeyItemName != "" && SubKeyItemName != null) && (KeyItemValue != "" && KeyItemValue != null))
                {
                    registryKey.Close();
                    registryKey = Registry.CurrentUser.OpenSubKey(OpenKey + "\\" + SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    registryKey.SetValue(SubKeyItemName, KeyItemValue);
                }
            }
            registryKey.Close();

        }
        #endregion

        #endregion

        #region J_SetRegistryKeyValue
        public void J_SetRegistryKeyValue(string OpenKey, string KeyItemName, string KeyItemValue)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(OpenKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            registryKey.SetValue(KeyItemName, KeyItemValue);
            registryKey.Close();
        }
        #endregion

        #region J_GetRegistryKeyValue
        public string J_GetRegistryKeyValue(string OpenKey, string KeyItemName)
        {
            string RegistryKeyValue = string.Empty;
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(OpenKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (registryKey == null) return "";
            RegistryKeyValue = Convert.ToString(registryKey.GetValue(KeyItemName));
            registryKey.Close();
            return RegistryKeyValue;
        }
        #endregion

        #region DELETE REGISTRY KEY [OVERLOADED METHODS]

        #region J_DeleteRegistryKey [1]
        public void J_DeleteRegistryKey(string RootKey)
        {
            this.J_DeleteRegistryKey(RootKey, null);
        }
        #endregion

        #region J_DeleteRegistryKey [2]
        public void J_DeleteRegistryKey(string OpenKey, string SubKey)
        {
            if (SubKey == "" || SubKey == null)
                Registry.CurrentUser.DeleteSubKey(OpenKey, true);
            else
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(OpenKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                registryKey.DeleteSubKeyTree(SubKey);
                registryKey.Close();
            }
        }
        #endregion

        #endregion


        #region J_GetLogicalDrives
        public String[] J_GetLogicalDrives()
        {
            return Environment.GetLogicalDrives();
        }
        #endregion

        #region J_ReplaceTextToFile
        public bool J_ReplaceTextToFile(string FilePath, string TextSeparatorFind, string TextSeparatorReplace)
        {
            string strFileContent = string.Empty;
            string searchText = string.Empty;
            string newText = string.Empty;

            StreamReader streamReader = new StreamReader(FilePath);
            strFileContent = streamReader.ReadToEnd();
            streamReader.Dispose();
            
            StreamWriter streamWriter = new StreamWriter(FilePath);
            searchText = Regex.Escape(TextSeparatorFind);
            
            if (Regex.IsMatch(strFileContent, searchText, RegexOptions.IgnoreCase))
            {
                newText = Regex.Replace(strFileContent, searchText, TextSeparatorReplace, RegexOptions.IgnoreCase);
                streamWriter.Write(newText);
                streamWriter.Dispose();
                return true;
            }
            streamWriter.Dispose();
            return false;
        }
        #endregion


        #region CLEAR COMBO BOX [OVERLOADED METHODS]

        #region J_ClearComboBox [1]
        public void J_ClearComboBox(ref ComboBox comboBox)
        {
            this.J_ClearComboBox(ref comboBox, "", J_ComboBoxDefaultText.YES, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region J_ClearComboBox [2]
        public void J_ClearComboBox(ref ComboBox comboBox, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            this.J_ClearComboBox(ref comboBox, "", ComboBoxDefaultText, J_ComboBoxSelectedIndex.YES);
        }
        #endregion
        
        #region J_ClearComboBox [3]
        public void J_ClearComboBox(ref ComboBox comboBox, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_ClearComboBox(ref comboBox, "", J_ComboBoxDefaultText.YES, ComboBoxSelectedIndex);
        }
        #endregion

        #region J_ClearComboBox [4]
        public void J_ClearComboBox(ref ComboBox comboBox, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_ClearComboBox(ref comboBox, "", ComboBoxDefaultText, ComboBoxSelectedIndex);
        }
        #endregion

        #region J_ClearComboBox [5]
        public void J_ClearComboBox(ref ComboBox comboBox, string DefaultText)
        {
            this.J_ClearComboBox(ref comboBox, DefaultText, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region J_ClearComboBox [6]
        public void J_ClearComboBox(ref ComboBox comboBox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText)
        {
            this.J_ClearComboBox(ref comboBox, DefaultText, ComboBoxDefaultText, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region J_ClearComboBox [7]
        public void J_ClearComboBox(ref ComboBox comboBox, string DefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            this.J_ClearComboBox(ref comboBox, DefaultText, J_ComboBoxDefaultText.YES, J_ComboBoxSelectedIndex.YES);
        }
        #endregion

        #region J_ClearComboBox [8]
        public void J_ClearComboBox(ref ComboBox comboBox, string DefaultText, J_ComboBoxDefaultText ComboBoxDefaultText, J_ComboBoxSelectedIndex ComboBoxSelectedIndex)
        {
            comboBox.Items.Clear();

            if (ComboBoxDefaultText == J_ComboBoxDefaultText.YES)
            {
                if(DefaultText == null || DefaultText == "")
                    comboBox.Items.Add("");
                else
                    comboBox.Items.Add(DefaultText);

                if (ComboBoxSelectedIndex == J_ComboBoxSelectedIndex.YES)
                    comboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion


        #region IS NULL OR EMPTY [OVERLOADED METHODS]

        #region J_IsNullOrEmpty [1]
        public bool J_IsNullOrEmpty(string strText)
        {
            string outStrText;
            return this.J_IsNullOrEmpty(strText, out outStrText);
        }
        #endregion

        #region J_IsNullOrEmpty [2]
        public bool J_IsNullOrEmpty(string strText, out string outStrText)
        {
            outStrText = strText;
            return string.IsNullOrEmpty(strText);
        }
        #endregion

        #endregion

        #region J_GetComboBoxItemId
        public long J_GetComboBoxItemId(ref ComboBox comboBox, int SelectedIndex)
        {
            return Convert.ToInt64(Support.GetItemData(comboBox, SelectedIndex));
        }
        #endregion


        #region GRID CHECK BOX SELECT DESELECT [ OVERLOADED METHOD ]

        #region J_GridCheckBoxSelectDeselect [1]
        public void J_GridCheckBoxSelectDeselect(DataGridView dataGridView, bool Checked)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                //checked all checkbox
                if (Checked == true)
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == false)
                        row.Cells[0].Value = true;
                }
                //Unchecked all checkbox
                else if (Checked == false)
                {
                    if (row.Cells[0].Value == null || (bool)row.Cells[0].Value == true)
                        row.Cells[0].Value = false;
                }
            }
        }
        #endregion

        #region J_GridCheckBoxSelectDeselect [2]
        public void J_GridCheckBoxSelectDeselect(AxMSHFlexGrid HFlexGrid, int ColumnIndex, bool Checked)
        {
            this.J_GridCheckBoxSelectDeselect(HFlexGrid, ColumnIndex, "√", "", Checked);
        }
        #endregion

        #region J_GridCheckBoxSelectDeselect [3]
        public void J_GridCheckBoxSelectDeselect(AxMSHFlexGrid HFlexGrid, int ColumnIndex, string TrueDisplayValue, bool Checked)
        {
            this.J_GridCheckBoxSelectDeselect(HFlexGrid, ColumnIndex, TrueDisplayValue, "", Checked);
        }
        #endregion

        #region J_GridCheckBoxSelectDeselect [4]
        public void J_GridCheckBoxSelectDeselect(AxMSHFlexGrid HFlexGrid, int ColumnIndex, string TrueDisplayValue, string falseDisplayValue, bool Checked)
        {
            for (int intRows = 1; intRows <= HFlexGrid.Rows - 1; intRows++)
            {
                if (Checked == true)
                    HFlexGrid.set_TextMatrix(intRows, ColumnIndex, TrueDisplayValue);
                else if (Checked == false)
                    HFlexGrid.set_TextMatrix(intRows, ColumnIndex, falseDisplayValue);
            }
        }
        #endregion

        #endregion

        #region FOR CREATE TABLE SCRIPT [ OVERLOADED METHOD ]

        #region J_GetDataType [1]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, 100, J_DefaultValue.YES, null);
        }
        #endregion

        #region J_GetDataType [2]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType, J_DefaultValue DefaultValue)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, 100, DefaultValue, null);
        }
        #endregion

        #region J_GetDataType [3]
        public string J_GetDataType(string ColumnName, J_Identity Identity)
        {
            return this.m_Create_Table_Script(ColumnName, J_ColumnType.Long, Identity, 0, J_DefaultValue.YES, null);
        }
        #endregion

        #region J_GetDataType [4]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType, int ColumnSize)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, ColumnSize, J_DefaultValue.YES, null);
        }
        #endregion

        #region J_GetDataType [5]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType, int ColumnSize, J_DefaultValue DefaultValue)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, ColumnSize, DefaultValue, null);
        }
        #endregion

        #region J_GetDataType [6]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType, object DefaultValue)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, 100, J_DefaultValue.YES, DefaultValue);
        }
        #endregion

        #region J_GetDataType [7]
        public string J_GetDataType(string ColumnName, J_ColumnType ColumnType, int ColumnSize, object DefaultValue)
        {
            return this.m_Create_Table_Script(ColumnName, ColumnType, J_Identity.NO, ColumnSize, J_DefaultValue.YES, DefaultValue);
        }
        #endregion

        #endregion

        #region FORMAT TO STRING [ OVERLOADED METHOD ]

        #region J_FormatToString [1]
        public string J_FormatToString(string strText, string Format)
        {
            return this.J_FormatToString((object)strText, Format);
        }
        #endregion

        #region J_FormatToString [2]
        public string J_FormatToString(Int16 Value, string Format)
        {
            return this.J_FormatToString((object)Value, Format);
        }
        #endregion

        #region J_FormatToString [3]
        public string J_FormatToString(Int32 Value, string Format)
        {
            return this.J_FormatToString((object)Value, Format);
        }
        #endregion

        #region J_FormatToString [4]
        public string J_FormatToString(Int64 Value, string Format)
        {
            return this.J_FormatToString((object)Value, Format);
        }
        #endregion

        #region J_FormatToString [5]
        public string J_FormatToString(double Value, string Format)
        {
            return this.J_FormatToString((object)Value, Format);
        }
        #endregion

        #region J_FormatToString [6]
        public string J_FormatToString(object Value, string Format)
        {
            return string.Format("{0:" + Format + "}", Value);
        }
        #endregion

        #endregion
        
        #region J_GetColumnDataType
        public string J_GetColumnDataType(string DataType)
        {
            if (DataType == "Int32")
                return "NUMBER";
            if (DataType == "Int64")
                return "NUMBER";
            if (DataType == "String")
                return "TEXT";
            if (DataType == "Decimal")
                return "CURRENCY";
            if (DataType == "DateTime")
                return "DATETIME";
            if (DataType == "Currency")
                return "CURRENCY";
            if (DataType == "Byte")
                return "BYTE";
            if (DataType == "Binary")
                return "OLE";

            return null;
        }
        #endregion

        #region J_SelectItemInListBox
        public void J_SelectItemInListBox(ref ListBox listBox, string FindString)
        {
            int index = listBox.FindString(FindString);
            if (index != -1) listBox.SetSelected(index, true);
        }
        #endregion


        #region J_GetOSType
        // Ref : http://www.dreamincode.net/code/snippet4273.htm
        public J_OSType J_GetOSType()
        {
            // declaration & initialize the enum variable as 32 bit
            J_OSType enmOSType = J_OSType._32Bit;

            //check if the 32 bit program files folder exists
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ProgramFiles(x86)")) == false)
            {
                //not found so return true
                enmOSType = J_OSType._64Bit;
            }

            //use Reflection to see if running in 32-bit mode
            if (enmOSType == J_OSType._32Bit)
            {
                //loop through all the loaded assemblies looking for 64-bit framework
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //look for the 64-bit framework
                    if (assembly.Location.ToLower().Contains("framework64"))
                    {
                        //found it so must be 64-bit OS
                        enmOSType = J_OSType._64Bit;
                        break;
                    }

                }
            }
            //made it this far so it's a 32-bit OS
            return enmOSType;
        }
        #endregion

        #region DATA GRID VIEW ROW SELECT[ OVERLOADED METHOD ]

        #region J_DataGridViewRowSelect [1]
        public void J_DataGridViewRowSelect(DataGridViewRow row)
        {
            this.J_DataGridViewRowSelect(row, null);
        }
        #endregion

        #region J_DataGridViewRowSelect [2]
        public void J_DataGridViewRowSelect(DataGridViewRow row, string Message)
        {
            if(Message != null && Message != "")
                this.J_UserMessage(Message);
            row.Selected = true;
        }
        #endregion

        #endregion




        #endregion

    }
    
}
