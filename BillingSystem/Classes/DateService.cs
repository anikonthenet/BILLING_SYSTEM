
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: DateService
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Class & Methods
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

// Used for Imported kernel32.dll
using System.Runtime.InteropServices;

#endregion

namespace BillingSystem.Classes
{
    public class DateService
    {

        #region API

        [DllImport("kernel32.dll")]
        private static extern uint GetUserDefaultLCID();
        [DllImport("kernel32.dll")]
        private static extern bool SetLocaleInfo(uint Locale, uint LCType, string lpLCData);

        #endregion

        #region PRIVATE OBJECTS DECLERATION

        private CommonService commonService;

        #endregion

        #region PRIVATE VARIABLES DECLERATION

        private string strSQL;
        private int StartYear;
        private int EndYear;

        // Used for API [kernel32.dll]
        private const int LOCALE_SSHORTDATE = 0x1F;
        private const int LOCALE_SDATE = 0x1D;

        #endregion

        #region CONSTRUCTOR

        #region DateService [1]
        public DateService()
        {
            this.commonService = new CommonService();
            
            this.strSQL = string.Empty;
            this.StartYear = 1753;
            this.EndYear = 9998;
        }
        #endregion

        #endregion

        #region DESTRUCTOR
        ~DateService()
        {
            this.commonService = null;
        }
        #endregion


        #region USER DEFINES METHODS

        #region PRIVATE METHODS



        #endregion

        #region PUBLIC METHODS

        #region SYSTEM DATE FORMAT CHECK dd_MM_yyyy  [ OVERLOADED METHOD ]

        #region J_SystemDateFormatCheck_dd_MM_yyyy [1]
        public bool J_SystemDateFormatCheck_dd_MM_yyyy()
        {
            return this.J_SystemDateFormatCheck_dd_MM_yyyy(J_Var.J_pProjectName, "dd/MM/yyyy");
        }
        #endregion

        #region J_SystemDateFormatCheck_dd_MM_yyyy [2]
        public bool J_SystemDateFormatCheck_dd_MM_yyyy(string SystemDateFormat)
        {
            return this.J_SystemDateFormatCheck_dd_MM_yyyy(J_Var.J_pProjectName, SystemDateFormat);
        }
        #endregion

        #region J_SystemDateFormatCheck_dd_MM_yyyy [3]
        public bool J_SystemDateFormatCheck_dd_MM_yyyy(string ProjectName, string SystemDateFormat)
        {
            if (this.J_GetSystemDateFormat() != SystemDateFormat)
            {
                if (commonService.J_UserMessage("Current Date Format : " + this.J_GetSystemDateFormat() +
                                                "\nDate Format is required : " + SystemDateFormat +
                                                "\n\n Proceed.....", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    this.J_SetSystemDateFormat(SystemDateFormat);
                    return true;
                }
            }
            return true;

            //if (dtService.J_GetSystemDateFormat() != "dd/MM/yyyy")
            //{
            //    commonService.J_UserMessage("Read Carefully............" +
            //            "\n\nPlease Change your System Date Format at dd/MM/yyyy" +
            //            "\nthrough 'Regional and Language Options' from 'Control Panel'", ProjectName);
            //    if (File.Exists(Environment.SystemDirectory + "/Control.exe") == true)
            //        Process.Start(Environment.SystemDirectory + "/Control.exe");
            //    return false;
            //}
            //else
            //    return true;
        }
        #endregion

        #endregion

        #region CHECK VALID DATE [ OVERLOADED METHOD ]

        #region J_IsDateValid [1]
        public bool J_IsDateValid(MaskedTextBox dateMaskedTextBox)
        {
            return this.J_IsDateValid(dateMaskedTextBox.Text);
        }
        #endregion

        #region J_IsDateValid [2]
        public bool J_IsDateValid(string date)
        {
            if (date.Trim().Length < 10)
                return false;
            
            if (date.Substring(0, 2).Trim().Length != 2)
                return false;
            if (date.Substring(3, 2).Trim().Length != 2)
                return false;
            if (date.Substring(6, 4).Trim().Length != 4)
                return false;

            if (commonService.J_IsNumeric(date.Substring(0, 2).ToString()) == false)
                return false;
            int intDD = Convert.ToInt32(date.Substring(0, 2).ToString());

            if (commonService.J_IsNumeric(date.Substring(3, 2).ToString()) == false)
                return false;
            int intMM = Convert.ToInt32(date.Substring(3, 2).ToString());

            if (commonService.J_IsNumeric(date.Substring(6, 4).ToString()) == false)
                return false;
            int intYYYY = Convert.ToInt32(date.Substring(6, 4).ToString());
            
            if (intYYYY == 0 || intYYYY < this.StartYear || intYYYY > this.EndYear)
                return false;
            else
            {
                if (intYYYY % 4 == 0)
                {
                    if (intMM == 1 || intMM == 3 || intMM == 5 || intMM == 7 || intMM == 8 || intMM == 10 || intMM == 12)
                    {
                        if (intDD < 1 || intDD > 31)
                            return false;
                    }
                    else if (intMM == 4 || intMM == 6 || intMM == 9 || intMM == 11)
                    {
                        if (intDD < 1 || intDD > 30)
                            return false;
                    }
                    else if (intMM == 2)
                    {
                        if (intDD < 1 || intDD > 29)
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    if (intMM == 1 || intMM == 3 || intMM == 5 || intMM == 7 || intMM == 8 || intMM == 10 || intMM == 12)
                    {
                        if (intDD < 1 || intDD > 31)
                            return false;
                    }
                    else if (intMM == 4 || intMM == 6 || intMM == 9 || intMM == 11)
                    {
                        if (intDD < 1 || intDD > 30)
                            return false;
                    }
                    else if (intMM == 2)
                    {
                        if (intDD < 1 || intDD > 28)
                            return false;
                    }
                    else
                        return false;
                }
            }
            return true;
        }
        #endregion


        #endregion

        #region CHECK VALID MONTH YEAR [ OVERLOADED METHOD ]

        #region J_IsMonthYearValid [1]
        public bool J_IsMonthYearValid(MaskedTextBox MonthYearMaskedTextBox)
        {
            return this.J_IsMonthYearValid(MonthYearMaskedTextBox.Text);
        }
        #endregion

        #region J_IsMonthYearValid [2]
        public bool J_IsMonthYearValid(string MonthYear)
        {
            if (MonthYear.Trim().Length < 7)
                return false;

            if (MonthYear.Substring(0, 2).Trim().Length != 2)
                return false;
            if (MonthYear.Substring(3, 4).Trim().Length != 4)
                return false;

            if (commonService.J_IsNumeric(MonthYear.Substring(0, 2).ToString()) == false)
                return false;
            int intMM = Convert.ToInt32(MonthYear.Substring(0, 2).ToString());

            if (commonService.J_IsNumeric(MonthYear.Substring(3, 4).ToString()) == false)
                return false;
            int intYYYY = Convert.ToInt32(MonthYear.Substring(3, 4).ToString());
            
            if (intYYYY == 0 || intYYYY < this.StartYear || intYYYY > this.EndYear)
                return false;
            if (intMM == 0 || intMM > 12)
                return false;
            
            return true;
        }

        #endregion

        #endregion

        #region CONVERT TO DATETIME AS ddMMyyyy [ OVERLOADED METHOD ]

        #region J_ConvertddMMyyyy [1]
        public DateTime J_ConvertddMMyyyy(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertddMMyyyy(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertddMMyyyy [2]
        public DateTime J_ConvertddMMyyyy(string date)
        {
            IFormatProvider format = new CultureInfo("en-GB", true);

            if (string.IsNullOrEmpty(date) == true)
                return DateTime.Parse(DateTime.Today.ToString(format), format);
            else
                return DateTime.Parse(date, format);
        }
        #endregion

        #endregion

        #region CONVERT TO STRING AS MMddyyyy [ OVERLOADED METHOD ]

        #region J_ConvertMMddyyyy [1]
        public string J_ConvertMMddyyyy(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertMMddyyyy(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertMMddyyyy [2]
        public string J_ConvertMMddyyyy(string date)
        {
            if (date == "" || date == null) return "";
            return date.Substring(3, 2) + '/' + date.Substring(0, 2) + '/' + date.Substring(6, 4);
        }
        #endregion

        #endregion

        #region CONVERT TO STRING AS yyyyMMdd [ OVERLOADED METHOD ]

        #region J_ConvertMMddyyyy [1]
        public string J_ConvertyyyyMMdd(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertyyyyMMdd(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertyyyyMMdd [2]
        public string J_ConvertyyyyMMdd(string date)
        {
            if (date == "" || date == null) return "";
            return date.Substring(6, 4) + '/' + date.Substring(3, 2) + '/' + date.Substring(0, 2);
        }
        #endregion

        #endregion

        #region CONVERT TO STRING AS yyyyddMM [ OVERLOADED METHOD ]

        #region J_ConvertyyyyddMM [1]
        public string J_ConvertyyyyddMM(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertyyyyddMM(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertyyyyddMM [2]
        public string J_ConvertyyyyddMM(string date)
        {
            if (date == "" || date == null) return "";
            return date.Substring(6, 4) + '/' + date.Substring(0, 2) + '/' + date.Substring(3, 2);
        }
        #endregion

        #endregion

        #region CONVERT TO INTEGER AS YYYYMMDD [ OVERLOADED METHOD ]

        #region J_ConvertToIntYYYYMMDD [1]
        public int J_ConvertToIntYYYYMMDD(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertToIntYYYYMMDD(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertToIntYYYYMMDD [2]
        public int J_ConvertToIntYYYYMMDD(string date)
        {
            if (date == "" || date == null) return 0;
            return Convert.ToInt32(date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2));
        }
        #endregion

        #endregion

        #region CONVERT TO INTEGER AS YYYYMM [ OVERLOADED METHOD ]

        #region J_ConvertToIntYYYYMM [1]
        public int J_ConvertToIntYYYYMM(MaskedTextBox MonthYearMaskedTextBox)
        {
            return this.J_ConvertToIntYYYYMM(MonthYearMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertToIntYYYYMM [2]
        public int J_ConvertToIntYYYYMM(string MonthYear)
        {
            if (MonthYear == "" || MonthYear == null) return 0;
            return Convert.ToInt32(MonthYear.Substring(3, 4) + MonthYear.Substring(0, 2));
        }
        #endregion
        
        #endregion

        #region CONVERT TO INTEGER AS MMDDYYYY [ OVERLOADED METHOD ]

        #region J_ConvertToIntMMDDYYYY [1]
        public int J_ConvertToIntMMDDYYYY(MaskedTextBox DateMaskedTextBox)
        {
            return this.J_ConvertToIntMMDDYYYY(DateMaskedTextBox.Text);
        }
        #endregion

        #region J_ConvertToIntMMDDYYYY [2]
        public int J_ConvertToIntMMDDYYYY(string date)
        {
            if (date == "" || date == null) return 0;
            return Convert.ToInt32(date.Substring(3, 2) + date.Substring(0, 2) + date.Substring(6, 4));
        }
        #endregion

        #endregion

        #region BLANK DATE CHECK [ OVERLOADED METHOD ]

        #region J_IsBlankDateCheck [1]
        public bool J_IsBlankDateCheck(ref MaskedTextBox dateMaskedTextBox)
        {
            return this.J_IsBlankDateCheck(ref dateMaskedTextBox, "Please enter date !!", J_ShowMessage.YES);
        }
        #endregion

        #region J_IsBlankDateCheck [2]
        public bool J_IsBlankDateCheck(ref MaskedTextBox dateMaskedTextBox, J_ShowMessage enmShowMessage)
        {
            return this.J_IsBlankDateCheck(ref dateMaskedTextBox, "Please enter date !!", enmShowMessage);
        }
        #endregion

        #region J_IsBlankDateCheck [3]
        public bool J_IsBlankDateCheck(ref MaskedTextBox dateMaskedTextBox, string DisplayMessage)
        {
            return this.J_IsBlankDateCheck(ref dateMaskedTextBox, DisplayMessage, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsBlankDateCheck [4]
        public bool J_IsBlankDateCheck(ref MaskedTextBox dateMaskedTextBox, string DisplayMessage, J_ShowMessage enmShowMessage)
        {
            if (dateMaskedTextBox.Text == "  /  /")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayMessage);
                    dateMaskedTextBox.Select();
                }
                return true;
            }
            else if (dateMaskedTextBox.Text == "  /  /    ")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayMessage);
                    dateMaskedTextBox.Select();
                }
                return true;
            }
            else if (dateMaskedTextBox.Text == "/  /")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayMessage);
                    dateMaskedTextBox.Select();
                }
                return true;
            }
            else
                return false;
        }
        #endregion

        #region J_IsBlankDateCheck [5]
        public bool J_IsBlankDateCheck(string date, string DisplayMessage, J_ShowMessage enmShowMessage)
        {
            if (date == "  /  /")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                    commonService.J_UserMessage(DisplayMessage);
                return true;
            }
            else if (date == "  /  /    ")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                    commonService.J_UserMessage(DisplayMessage);
                return true;
            }
            else if (date == "/  /")
            {
                if (enmShowMessage == J_ShowMessage.YES)
                    commonService.J_UserMessage(DisplayMessage);
                return true;
            }
            else
                return false;
        }
        #endregion
        
        #endregion

        #region CHECK DATE GREATER [ OVERLOADED METHOD ]

        #region J_IsDateGreater [1]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, "From Date", "To Date", "To Date should be greater than or equalto From Date !!", J_ShowMessage.YES);
        }
        #endregion

        #region J_IsDateGreater [2]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, J_ShowMessage enmShowMessage)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, "From Date", "To Date", "To Date should be greater than or equalto From Date !!", enmShowMessage);
        }
        #endregion

        #region J_IsDateGreater [3]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string FromDateCaption, string ToDateCaption)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, FromDateCaption, ToDateCaption, "To Date should be greater than or equalto From Date !!", J_ShowMessage.YES);
        }
        #endregion

        #region J_IsDateGreater [4]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string FromDateCaption, string ToDateCaption, J_ShowMessage enmShowMessage)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, FromDateCaption, ToDateCaption, "To Date should be greater than or equalto From Date !!", enmShowMessage);
        }
        #endregion
        
        #region J_IsDateGreater [5]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string DisplayText)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, "From Date", "To Date", DisplayText, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsDateGreater [6]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string DisplayText, J_ShowMessage enmShowMessage)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, "From Date", "To Date", DisplayText, enmShowMessage);
        }
        #endregion

        #region J_IsDateGreater [7]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string FromDateCaption, string ToDateCaption, string DisplayText)
        {
            return this.J_IsDateGreater(ref FromDateMaskedTextBox, ref ToDateMaskedTextBox, FromDateCaption, ToDateCaption, DisplayText, J_ShowMessage.YES);
        }
        #endregion

        #region J_IsDateGreater [8]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, ref MaskedTextBox ToDateMaskedTextBox, string FromDateCaption, string ToDateCaption, string DisplayText, J_ShowMessage enmShowMessage)
        {
            if (this.J_IsBlankDateCheck(ref FromDateMaskedTextBox, "Please enter " + FromDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(FromDateMaskedTextBox) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + FromDateCaption + " !!");
                    FromDateMaskedTextBox.Select();
                }
                return false;
            }
            
            if (this.J_IsBlankDateCheck(ref ToDateMaskedTextBox, "Please enter " + ToDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(ToDateMaskedTextBox) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + ToDateCaption + " !!");
                    ToDateMaskedTextBox.Select();
                }
                return false;
            }

            if (this.J_ConvertToIntYYYYMMDD(ToDateMaskedTextBox) < this.J_ConvertToIntYYYYMMDD(FromDateMaskedTextBox))
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayText);
                    FromDateMaskedTextBox.Select();
                }
                return false;
            }
            return true;
        }
        #endregion

        #region J_IsDateGreater [9]
        public bool J_IsDateGreater(ref MaskedTextBox FromDateMaskedTextBox, string ToDate, string FromDateCaption, string ToDateCaption, string DisplayText, J_ShowMessage enmShowMessage)
        {
            if (this.J_IsBlankDateCheck(ref FromDateMaskedTextBox, "Please enter " + FromDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(FromDateMaskedTextBox) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + FromDateCaption + " !!");
                    FromDateMaskedTextBox.Select();
                }
                return false;
            }
            
            if (this.J_IsBlankDateCheck(ToDate, "Please enter " + ToDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(ToDate) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + ToDateCaption + " !!");
                    FromDateMaskedTextBox.Select();
                }
                return false;
            }

            if (this.J_ConvertToIntYYYYMMDD(ToDate) < this.J_ConvertToIntYYYYMMDD(FromDateMaskedTextBox))
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayText);
                    FromDateMaskedTextBox.Select();
                }
                return false;
            }
            return true;
        }
        #endregion

        #region J_IsDateGreater [10]
        public bool J_IsDateGreater(string FromDate, ref MaskedTextBox ToDateMaskedTextBox, string FromDateCaption, string ToDateCaption, string DisplayText, J_ShowMessage enmShowMessage)
        {
            if (this.J_IsBlankDateCheck(FromDate, "Please enter " + FromDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(FromDate) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + FromDateCaption + " !!");
                    ToDateMaskedTextBox.Select();
                }
                return false;
            }

            if (this.J_IsBlankDateCheck(ref ToDateMaskedTextBox, "Please enter " + ToDateCaption + " !!", enmShowMessage) == true)
                return false;
            else if (this.J_IsDateValid(ToDateMaskedTextBox) == false)
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage("Please enter valid " + ToDateCaption + " !!");
                    ToDateMaskedTextBox.Select();
                }
                return false;
            }

            if (this.J_ConvertToIntYYYYMMDD(ToDateMaskedTextBox) < this.J_ConvertToIntYYYYMMDD(FromDate))
            {
                if (enmShowMessage == J_ShowMessage.YES)
                {
                    commonService.J_UserMessage(DisplayText);
                    ToDateMaskedTextBox.Select();
                }
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region CHECK MONTH GREATER [ OVERLOADED METHOD ]

        #region J_IsMonthGreater [1]
        public bool J_IsMonthGreater(ref MaskedTextBox FromMonthMaskedTextBox, ref MaskedTextBox ToMonthMaskedTextBox)
        {
            return this.J_IsMonthGreater(ref FromMonthMaskedTextBox, ref ToMonthMaskedTextBox, "To Month/Year should be greater then From Month/Year !!");
        }
        #endregion

        #region J_IsMonthGreater [2]
        public bool J_IsMonthGreater(ref MaskedTextBox FromMonthMaskedTextBox, ref MaskedTextBox ToMonthMaskedTextBox, string DisplayText)
        {
            if (this.J_ConvertToIntYYYYMM(ToMonthMaskedTextBox.Text) < this.J_ConvertToIntYYYYMM(FromMonthMaskedTextBox.Text))
            {
                commonService.J_UserMessage(DisplayText);
                FromMonthMaskedTextBox.Select();
                return false;
            }
            return true;
        }
        #endregion
        
        #endregion

        #region J_GetSystemDateFormat
        public string J_GetSystemDateFormat()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            return cultureInfo.DateTimeFormat.ShortDatePattern;
        }
        #endregion

        #region SET SYSTEM DATE FORMAT [ OVERLOADED METHOD ]

        #region J_SetSystemDateFormat [1]
        public void J_SetSystemDateFormat(string ShortDateFormat)
        {
            this.J_SetSystemDateFormat(ShortDateFormat, J_TextSeparator.FrontSlash);
        }
        #endregion

        #region J_SetSystemDateFormat [2]
        public void J_SetSystemDateFormat(string ShortDateFormat, string Separator)
        {
            uint lngLCID = GetUserDefaultLCID();
            SetLocaleInfo(lngLCID, LOCALE_SSHORTDATE, ShortDateFormat);
            SetLocaleInfo(lngLCID, LOCALE_SDATE, Separator);
        }
        #endregion

        #endregion

        #region ADD WEEKS [ OVERLOADED METHOD ]

        #region J_AddWeeks [1]
        public object J_AddWeeks(string date, int NoOfWeeks)
        {
            string[] outCalander = null;
            return this.J_AddWeeks(date, NoOfWeeks, out outCalander);
        }
        #endregion

        #region J_AddWeeks [2]
        public object J_AddWeeks(string date, int NoOfWeeks, out string[] outCalander)
        {
            outCalander = new string[NoOfWeeks];
            DateTime StartDate = this.J_ConvertddMMyyyy(date);
            for (int i = 1; i <= NoOfWeeks; i++)
            {
                StartDate = this.J_ConvertddMMyyyy(StartDate.AddDays(7).ToString("dd/MM/yyyy"));
                outCalander[i - 1] = StartDate.ToString("dd/MM/yyyy");
            }
            return (object)StartDate;
        }
        #endregion

        #endregion

        #region ADD DAYS [ OVERLOADED METHOD ]

        #region J_AddDays [1]
        public object J_AddDays(ref MaskedTextBox dateMaskedTextBox, int NoOfDays)
        {
            string[] outCalander = null;
            return this.J_AddDays(dateMaskedTextBox.Text, NoOfDays, out outCalander);
        }
        #endregion        

        #region J_AddDays [2]
        public object J_AddDays(string date, int NoOfDays)
        {
            string[] outCalander = null;
            return this.J_AddDays(date, NoOfDays, out outCalander);
        }
        #endregion

        #region J_AddDays [3]
        public object J_AddDays(ref MaskedTextBox dateMaskedTextBox, int NoOfDays, out string[] outCalander)
        {
            return this.J_AddDays(dateMaskedTextBox.Text, NoOfDays, out outCalander);
        }
        #endregion

        #region J_AddDays [4]
        public object J_AddDays(string date, int NoOfDays, out string[] outCalander)
        {
            if(NoOfDays <= 0) outCalander = new string[0];
            else outCalander = new string[NoOfDays];

            string strDate = this.J_ConvertddMMyyyy(date).AddDays(NoOfDays).ToString("dd/MM/yyyy");
            DateTime dtmStartDate = this.J_ConvertddMMyyyy(date);

            for (int i = 1; i <= NoOfDays; i++)
                outCalander[i - 1] = dtmStartDate.ToString("dd/MM/yyyy");
            
            return (object)strDate;
        }
        #endregion


        #endregion



        #endregion



        #endregion


    }
}
