using System;

namespace JAYA.CSharp
{
	#region ENUM DECLERATION

	#region J_ApplicationType
	public enum J_ApplicationType
	{
		StandAlone,
		Web
	}
	#endregion

	#region J_DatabaseType
	public enum J_DatabaseType
	{
		Sql,
		Oracle,
		OleDb,
		Odbc
	}
	#endregion

	#region J_DataType
	public enum J_DataType
	{
		Integer,
		Double,
		Currency,
		Date,
		String
	}
	#endregion

	#region J_DateElement
	public enum J_DateElement
	{
		Year,
		Month,
		Day,
		Hour,
		Minute,
		Second,
		DayOfWeek,
		DayOfYear
	}
	#endregion

	#region J_MessageType
	public enum J_MessageType
	{
		Error,
		Warning,
		Debug,
		Information
	}
	#endregion

	#region J_RegistryRoot
	public enum J_RegistryRoot
	{
		HKEY_CLASSES_ROOT,
		HKEY_CURRENT_CONFIG,
		HKEY_CURRENT_USER,
		HKEY_DYNDATA,
		HKEY_LOCALE_MACHINE,
		HKEY_PERFORMANCE_DATA,
		HKEY_USERS
	}
	#endregion

	#region J_RelationalOperator
	public enum J_RelationalOperator
	{
		EqualTo,
		LessThan,
		GreaterThan,
		NotEqualTo,
		LessThanEqualTo,
		GreaterThanEqualTo
	}
	#endregion

	#region J_ValidationType
	public enum J_ValidationType
	{
		IsRequired,
		IsMatching,
		IsNumeric,
		IsChar,
		IsBlank,
		IsEmail,
		IsWhitespace,
		IsUSAZip,
		IsUSATel,
		IsSimilar,
		IsMinLen,
		IsMaxLen,
		IsPositive,
		IsNegative,
		IsValueInRange,
		IsMultipleChecked,
		IsAtleastOneChecked,
		IsSelected,
		IsValidDate,
		CompareValue,
		Custom
	}
	#endregion

	#endregion

	
}
