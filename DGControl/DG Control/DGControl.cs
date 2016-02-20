
namespace DGControl
{

	#region Refered Namespaces & Classes

	using System;
	using System.Windows.Forms;
	using System.ComponentModel;
	using System.Drawing;

	#endregion

	/// <summary>
	/// This Class inherits from datagrid class.This Class Extends all exiting functionality & add row selection
	/// on KeyUP & KeyDown Event 
	/// </summary>	
	[ToolboxBitmap(typeof(DataGrid))]
	public class DGControl:System.Windows.Forms.DataGrid
	{	
		
		#region PocessCmdKey
		
		/// <summary>
		/// This  Function overrides from control class to support Key UP & Key Down key will be handles
		/// in datagrid keydown Event
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message  msg, System.Windows.Forms.Keys keyData)
		{
			if (keyData == Keys.Down ||keyData == Keys.Up)
			{			
				DGControl_KeyDown(null,new KeyEventArgs(keyData));
				return true;					
			}
			else
			{
				return false;			
			}		
		}

		#endregion

		#region DG_KyDown

		/// <summary>
		/// This fuction is a event handler for datagrid keydown event.
		///  this function can't available in design time 
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="e"></param>
		private void DGControl_KeyDown(object Sender, KeyEventArgs e)
		{
			int intCurrentRowIndex=this.CurrentRowIndex;
			
			if (e.KeyCode == Keys.Down)
			{
				int intNumberOfRow;
				intNumberOfRow = this.BindingContext[this.DataSource,this.DataMember].Count;
				
				if ( intCurrentRowIndex != intNumberOfRow - 1  )  
				{
					this.UnSelect(intCurrentRowIndex);
					intCurrentRowIndex = intCurrentRowIndex + 1;
					this.Select(intCurrentRowIndex);
					this.CurrentRowIndex = intCurrentRowIndex;	
				}
							
			}
			if(e.KeyCode == Keys.Up)
			{
				if (intCurrentRowIndex != 0 )
				{
					this.UnSelect(intCurrentRowIndex);
					intCurrentRowIndex = intCurrentRowIndex - 1;
					this.Select(intCurrentRowIndex);
					this.CurrentRowIndex = intCurrentRowIndex;
				}
			}		
		}

		#endregion

	}
}
