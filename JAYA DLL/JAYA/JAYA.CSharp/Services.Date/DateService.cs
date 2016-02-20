using System;
using System.Threading;

namespace JAYA.CSharp.Services.Date
{
	public sealed class DateService
	{
		#region PRIVATE OBJECTS DECLERATION
		
		#endregion

		#region PRIVATE VARIABLES DECLERATION
		
		#endregion
		
		#region CONSTRUCTOR
		public DateService()
		{
		}
		#endregion

		#region USER DEFINES METHODS
		
		#region PRIVATE METHODS

		#endregion

		#region PUBLIC METHODS
		
		#region DATEADD
		public static DateTime DateAdd(object DateTimeObject, object TimeIntervalObject, J_DateElement IntervalUnit, params object[] FormatParamObject)
		{
			DateTime dtmDefaultDateTime = new DateTime();
			DateTime dtmDateTime = new DateTime();
			double dblNum = 0.0;
			string strFormatParamObject = null;
			
			if (((FormatParamObject != null) && (FormatParamObject.Length >= 1)) && ((FormatParamObject[0] != null) && (FormatParamObject[0] is string)))
				strFormatParamObject = (string) FormatParamObject[0];
			
			dtmDateTime = (DateTimeObject is string) 
				?(
				(strFormatParamObject == null) 
				? DateTime.Parse((string) DateTimeObject) 
				: DateTime.ParseExact((string) DateTimeObject, strFormatParamObject, Thread.CurrentThread.CurrentUICulture)
				)
				: ((DateTime) DateTimeObject);
			
			if (TimeIntervalObject == null)
				return dtmDateTime;
			
			dblNum = (TimeIntervalObject is string) ? double.Parse(((string) TimeIntervalObject).Trim()) : ((double) TimeIntervalObject);
			
			switch (IntervalUnit)
			{
				case J_DateElement.Year:
					return dtmDateTime.AddYears((int) dblNum);

				case J_DateElement.Month:
					return dtmDateTime.AddMonths((int) dblNum);

				case J_DateElement.Day:
					return dtmDateTime.AddDays(dblNum);

				case J_DateElement.Hour:
					return dtmDateTime.AddHours(dblNum);

				case J_DateElement.Minute:
					return dtmDateTime.AddMinutes(dblNum);

				case J_DateElement.Second:
					return dtmDateTime.AddSeconds(dblNum);
			}
			return dtmDefaultDateTime;
		}
		#endregion

		#region DATE COMPARE
		public static bool DateCompare(object DateToCompareObject, object DateToCompareWithObject, params object[] args)
		{
			DateTime dtmDefaultDateTime = new DateTime();
			DateTime dtmDateTime = new DateTime();
			string strFormat = null;
			string strFormat2 = null;

			J_RelationalOperator EqualTo = J_RelationalOperator.EqualTo;

			EqualTo = (((args == null) || (args.Length <= 0)) || (args[0] == null)) ? J_RelationalOperator.EqualTo : ((J_RelationalOperator) args[0]);
			
			if (((args != null) && (args.Length >= 3)) && (((args[1] != null) && (args[1] is string)) || ((args[2] != null) && (args[2] is string))))
			{
				if (args[1] != null)
					strFormat = (string) args[1];
				if (args[2] != null)
					strFormat2 = (string) args[2];
			}
			else if (((args != null) && (args.Length >= 2)) && ((args[1] != null) && (args[1] is string)))
				strFormat = (string) args[1];
			
			dtmDefaultDateTime = (DateToCompareObject is string) ? ((strFormat == null) ? DateTime.Parse((string) DateToCompareObject) : DateTime.ParseExact((string) DateToCompareObject, strFormat, Thread.CurrentThread.CurrentUICulture)) : ((DateTime) DateToCompareObject);
			dtmDateTime = (DateToCompareWithObject == null) ? DateTime.Today : ((DateToCompareWithObject is string) ? ((strFormat2 == null) ? DateTime.Parse((string) DateToCompareWithObject) : DateTime.ParseExact((string) DateToCompareWithObject, strFormat2, Thread.CurrentThread.CurrentUICulture)) : ((DateTime) DateToCompareWithObject));
			
			switch (EqualTo)
			{
				case J_RelationalOperator.EqualTo:
					return (dtmDefaultDateTime == dtmDateTime);

				case J_RelationalOperator.LessThan:
					return (dtmDefaultDateTime < dtmDateTime);

				case J_RelationalOperator.GreaterThan:
					return (dtmDefaultDateTime > dtmDateTime);

				case J_RelationalOperator.NotEqualTo:
					return (dtmDefaultDateTime != dtmDateTime);

				case J_RelationalOperator.LessThanEqualTo:
					return (dtmDefaultDateTime <= dtmDateTime);

				case J_RelationalOperator.GreaterThanEqualTo:
					return (dtmDefaultDateTime >= dtmDateTime);
			}
			return false;
		}
		#endregion

		#region FORMAT DATE
		public static string FormatDate(object DateTimeObject, string NewFormat, params object[] PresentFormat)
		{
			DateTime dtmDateTime = new DateTime();
			string strDateTime = "";
			string strFormat = null;

			if (((PresentFormat != null) && (PresentFormat.Length >= 1)) && ((PresentFormat[0] != null) && (PresentFormat[0] is string)))
				strFormat = (string) PresentFormat[0];
			
			dtmDateTime = (DateTimeObject is string) ? ((strFormat == null) ? DateTime.Parse((string) DateTimeObject) : DateTime.ParseExact((string) DateTimeObject, strFormat, Thread.CurrentThread.CurrentUICulture)) : ((DateTime) DateTimeObject);
			
			strDateTime = dtmDateTime.ToString(NewFormat);
			return strDateTime;
		}
		#endregion

		#region IS VADID DATE
		public static bool IsValidDate(params object[] args)
		{
			string DateString = null;
			bool Flag;
			int intNum1;
			
			if ((args != null) && (args.Length == 0))
				return true;
			
			for (int i = 0; i < args.Length; i++)
				intNum1 = (int) args[i];
			
			switch (args.Length)
			{
				case 2:
					DateString = ((int) args[0]) + ":" + ((int) args[1]);
					break;

				case 3:
					new DateTime((int) args[0], (int) args[1], (int) args[2]);
					break;

				case 5:
					new DateTime((int) args[0], (int) args[1], (int) args[2], (int) args[3], (int) args[4], 0);
					break;

				case 6:
					new DateTime((int) args[0], (int) args[1], (int) args[2], (int) args[3], (int) args[4], (int) args[5]);
					break;

				default:
					return false;
			}
			
			if (DateString == null)
				return true;
			
			Flag = IsValidDate(DateString, new object[0]);
		
			return Flag;
		}

		public static bool IsValidDate(string DateString, params object[] args)
		{
			if ((DateString == null) || (DateString.Length == 0))
				return true;
			
			if ((((args != null) && (args.Length >= 1)) && ((args[0] != null) && (args[0] is string))) && (((string) args[0]).Trim().Length > 0))
				DateTime.ParseExact(DateString, (string) args[0], Thread.CurrentThread.CurrentUICulture);
			else
				DateTime.Parse(DateString);
			
			return true;
		}
		#endregion

		#endregion

		#endregion


	}
}
