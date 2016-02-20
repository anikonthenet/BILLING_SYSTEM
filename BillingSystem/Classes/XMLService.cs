
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Ripan Paul
Class Name		: XMLService
Version			: 2.0
Start Date		: 
End Date		: 
Class Desc		: Implemented Class & Methods
_________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces

using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;

#endregion

namespace BillingSystem.Classes
{
    public sealed class XMLService
    {

        #region PRIVATE OBJECTS DECLERATION

        CommonService commonService;
        
        #endregion

        #region CONSTRUCTOR

        #region XMLService
        public XMLService()
        {
            this.commonService = new CommonService();
        }
        #endregion

        #endregion

        #region PUBLIC METHODS

        #region CREATE XML FILE [ OVERLOADED METHOD ]

        #region J_CreateXMLFile [1]
        public void J_CreateXMLFile(Hashtable ColumnNameValue, string PathWithFileName)
        {
            this.J_CreateXMLFile(ColumnNameValue, PathWithFileName, "SERVERINFO");
        }
        #endregion

        #region J_CreateXMLFile [2]
        public void J_CreateXMLFile(Hashtable ColumnNameValue, string PathWithFileName, string TagName)
        {
            this.J_DeleteXMLFile(PathWithFileName);

            DataSet dsXML = new DataSet();
            IDictionaryEnumerator enumerator = ColumnNameValue.GetEnumerator();

            if (File.Exists(PathWithFileName))
                dsXML.ReadXml(PathWithFileName);
            else
            {
                dsXML.Tables.Add(TagName);
                DataRow dr = dsXML.Tables[0].NewRow();
                dsXML.Tables[0].Rows.Add(dr);
            }
            while (enumerator.MoveNext())
            {
                dsXML.Tables[0].Columns.Add(commonService.J_Encode(enumerator.Key.ToString()));
                dsXML.Tables[0].Rows[0][commonService.J_Encode(enumerator.Key.ToString())] = commonService.J_Encode(enumerator.Value.ToString());
            }
            dsXML.WriteXml(PathWithFileName);
            dsXML.Dispose();
        }
        #endregion

        #endregion

        #region J_DeleteXMLFile
        public void J_DeleteXMLFile(string PathWithFileName)
        {
            if (File.Exists(PathWithFileName))
                File.Delete(PathWithFileName);
        }
        #endregion


        #endregion

    }
}
