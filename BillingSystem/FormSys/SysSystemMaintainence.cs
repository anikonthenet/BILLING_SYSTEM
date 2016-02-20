
#region Developer Information

/*_________________________________________________________________________________________________________

Developed By   : Ripan Paul
Module Name    : SysSystemMaintainence
Start Date     : 31/08/2010
End Date       : 
Main Table     : 
Other Tables   : 
Module Desc    : System Maintainence

//_________________________________________________________________________________________________________*/

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
using System.Data.SqlClient;
//---------------------------
//~~~~ This namespace are using for using VB6 component
using Microsoft.VisualBasic.Compatibility.VB6;

//~~~~ User Namespaces ~~~~
using BillingSystem.Classes;

#endregion

namespace BillingSystem.FormSys
{
    public partial class SysSystemMaintainence : Form
    {

        #region System Generated Code
        public SysSystemMaintainence()
        {
            InitializeComponent();

            // initialization of class objects
            dmlService = new DMLService();
            cmnService = new CommonService();

        }
        #endregion

        #region Objects & Variables decleration

        DMLService dmlService = null;
        CommonService cmnService = null;
        
        string strPath;
        string strSQL;
        string strLocalMachineName;
        
        #endregion

        #region User defined events

        #region SysSystemMaintainence_Load
        private void SysSystemMaintainence_Load(object sender, EventArgs e)
        {
            // Getting the Local Machine Name
            strLocalMachineName = Environment.MachineName;

            txtInformation1.Text = "Make sure that in all Client Machine's " + J_Var.J_pApplicationName + " Application should be closed";
            txtInfo.Visible = false;
        }
        #endregion

        #region BtnSystemMaintainence_Click
        private void BtnSystemMaintainence_Click(object sender, EventArgs e)
        {
            try
		    {
                if(cmnService.J_UserMessage("Do you want to Maintainence your Database??",
                    J_Var.J_pProjectName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    BtnCancel.Select();
                    return;
                }

                cmnService.J_BusyMode();
                dmlService.J_ClearDatabaseLog();

                // Added by Ripan Paul on 15-06-2011
                //if (dmlService.J_IsDatabaseObjectExist("TRN_INVOICE_HEADER", "DISCOUNT_TEXT") == false)
                //{
                //    strSQL = "ALTER TABLE TRN_INVOICE_HEADER ADD " + cmnService.J_GetDataType("DISCOUNT_TEXT", J_ColumnType.String, 200) + "";
                //    dmlService.J_ExecSql(strSQL);

                //    strSQL = "UPDATE TRN_INVOICE_HEADER " +
                //             "SET    DISCOUNT_TEXT = ''";
                //    dmlService.J_ExecSql(strSQL);
                //}


                //-- VIEW [VW_INVOICE_HEADER] -- ADDED BY DHRUB ON 29/04/2015
                #region VW_INVOICE_HEADER
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME LIKE 'VW_INVOICE_HEADER'")) > 0)
                {
                    strSQL = @" DROP VIEW VW_INVOICE_HEADER ";

                    //---------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //---------------------------------------------
                }
                    
                strSQL = @" CREATE VIEW DBO.VW_INVOICE_HEADER
                            AS
                            (
                            SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                   TRN_INVOICE_HEADER.FAYEAR_ID,
                                   TRN_INVOICE_HEADER.COMPANY_ID,
                                   TRN_INVOICE_HEADER.TRAN_TYPE,
                                   MST_COMPANY.COMPANY_NAME,
                                   TRN_INVOICE_HEADER.PARTY_ID,
                                   MST_PARTY.PARTY_NAME,
                                   MST_PARTY_CATEGORY.PARTY_CATEGORY_ID,
                                   MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION,
                                   TRN_INVOICE_HEADER.INVOICE_SERIES_ID,
                                   TRN_INVOICE_HEADER.INVOICE_NO                         AS INVOICE_NO,
                                   TRN_INVOICE_HEADER.INVOICE_DATE                       AS INVOICE_DATE,
                                   CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE_DDMMYYYY,
                                   CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)  AS INVOICE_DATE_YYYYMMDD,
                                   TRN_INVOICE_HEADER.NET_AMOUNT                         AS NET_AMOUNT,
                                   TRN_INVOICE_HEADER.PAYMENT_TYPE_ID                    AS PAYMENT_TYPE_ID,
                                   MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION             AS PAYMENT_TYPE_DESCRIPTION,
                                   TRN_INVOICE_HEADER.BANK_ID                            AS BANK_ID,
                                   ISNULL(MST_BANK.BANK_NAME,'')                         AS BANK_NAME,
                                   TRN_INVOICE_HEADER.REFERENCE_NO                       AS REFERENCE_NO,
                                   TRN_INVOICE_HEADER.DELIVERY_MODE_ID                   AS DELIVERY_MODE_ID,
                                   PAR_DELIVERY_MODE.DELIVERY_MODE_DESC                  AS DELIVERY_MODE_DESC,
                                   TRN_INVOICE_HEADER.CONTACT_PERSON                     AS CONTACT_PERSON,
                                   TRN_INVOICE_HEADER.MOBILE_NO                          AS MOBILE_NO,
                                   TRN_INVOICE_HEADER.EMAIL_ID                           AS EMAIL_ID,
                                   TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID                  AS OFFLINE_SERIAL_ID,
                                   ISNULL(TRNCOLLECT.COLLECTION_AMOUNT,0)                AS COLLECTION_AMOUNT,
                                   ISNULL(TRNCOLLECT.ADJUSTED_AMOUNT,0)                  AS ADJUSTED_AMOUNT,
                                   TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRNCOLLECT.COLLECTION_AMOUNT,0) - ISNULL(TRNCOLLECT.ADJUSTED_AMOUNT,0) AS PENDING_AMOUNT,
                                   ------- ADDED BY DHRUB ON 03-06-2015
                                   ---------------------------------------------------------------------------------------------------------------------------------------
                                   TRN_INVOICE_HEADER.AMOUNT_WITH_DISCOUNT               AS TAXABLE_AMOUNT,
                                   TRN_INVOICE_HEADER.TAX_TOTAL_AMOUNT                   AS TAX_TOTAL_AMOUNT,
                                   ISNULL(MST_TAX.TAX_TYPE,'')                           AS TAX_TYPE,
                                   ---------------------------------------------------------------------------------------------------------------------------------------
                                   TRN_INVOICE_HEADER.ADDITIONAL_COST                    AS ADDITIONAL_COST,
                                   TRN_INVOICE_HEADER.ROUNDED_OFF                        AS ROUNDED_OFF
                                   ---------------------------------------------------------------------------------------------------------------------------------------
                            FROM   TRN_INVOICE_HEADER
                                   INNER JOIN MST_PARTY          
                                   ON TRN_INVOICE_HEADER.PARTY_ID         = MST_PARTY.PARTY_ID
                                   INNER JOIN MST_COMPANY        
                                   ON TRN_INVOICE_HEADER.COMPANY_ID       = MST_COMPANY.COMPANY_ID
                                   INNER JOIN PAR_DELIVERY_MODE  
                                   ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID = PAR_DELIVERY_MODE.DELIVERY_MODE_ID
                                   INNER JOIN MST_PAYMENT_TYPE   
                                   ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID  = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                   INNER JOIN MST_PARTY_CATEGORY 
                                   ON MST_PARTY.PARTY_CATEGORY_ID         = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
                                   LEFT  JOIN MST_BANK           
                                   ON TRN_INVOICE_HEADER.BANK_ID          = MST_BANK.BANK_ID
                                   ------- ADDED BY DHRUB  ON 03-06-2015
                                   -------------------------------------------------------------------------------
                                   LEFT JOIN TRN_INVOICE_TAX
                                   ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_INVOICE_TAX.INVOICE_HEADER_ID
                                   LEFT  JOIN MST_TAX 
                                   ON  TRN_INVOICE_TAX.TAX_ID = MST_TAX.TAX_ID
                                   -------------------------------------------------------------------------------
                                   LEFT  JOIN ( SELECT COLLADJ.INVOICE_HEADER_ID,
                                                       SUM(COLLADJ.COLLECTION_AMOUNT) AS COLLECTION_AMOUNT,
                                                       SUM(COLLADJ.ADJUSTED_AMOUNT)   AS ADJUSTED_AMOUNT
                                                FROM   (
                                                       ------------- INVOICE WISE TOTAL COLLECTION
                                                       SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                              SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS COLLECTION_AMOUNT,
                                                              0                                            AS ADJUSTED_AMOUNT
                                                       FROM   TRN_COLLECTION_DETAIL,
                                                              TRN_COLLECTION_HEADER
                                                       WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
                                                       AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0
                                                       GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                                       UNION
                                                       ------------- INVOICE WISE TOTAL ADJUSTMENT
                                                       SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                              0                                            AS COLLECTION_AMOUNT,
                                                              SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS ADJUSTED_AMOUNT
                                                       FROM   TRN_COLLECTION_DETAIL,
                                                              TRN_COLLECTION_HEADER
                                                       WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
                                                       AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 1
                                                       GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                                      ) AS COLLADJ
	                                            GROUP BY COLLADJ.INVOICE_HEADER_ID
	                                          ) AS TRNCOLLECT 
                                      ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRNCOLLECT.INVOICE_HEADER_ID
                            WHERE    TRN_INVOICE_HEADER.RECON_FLAG = 0
                            AND     (TRN_INVOICE_HEADER.TRAN_TYPE = 'INV' OR TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV')
                        )";
                //---------------------------------------------
                dmlService.J_ExecSql(strSQL);
                //---------------------------------------------
                
                #endregion

                //-- VIEW [VW_COLLECTION_HEADER] -- ADDED BY DHRUB ON 29/04/2015
                #region VW_COLLECTION_HEADER
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME LIKE 'VW_COLLECTION_HEADER'")) > 0)
                {
                    strSQL = @" DROP VIEW VW_COLLECTION_HEADER ";

                    //---------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //---------------------------------------------
                }
                strSQL = @" CREATE VIEW DBO.VW_COLLECTION_HEADER AS 
                            (
                               SELECT  TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,  
                                       TRN_COLLECTION_HEADER.COLLECTION_DATE,  
                                       CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103) AS COLLECTION_DATE_DDMMYYYY,  
                                       CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) AS COLLECTION_DATE_YYYYMMDD,
                                       TRN_COLLECTION_HEADER.COMPANY_ID,  
                                       MST_COMPANY.COMPANY_NAME,  
                                       TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID,  
                                       ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'') AS PAYMENT_TYPE_DESCRIPTION,  
                                       TRN_COLLECTION_HEADER.BANK_ID,  
                                       ISNULL(MST_BANK.BANK_NAME,'') AS BANK_NAME,  
                                       TRN_COLLECTION_HEADER.REFERENCE_NO,  
                                       TRN_COLLECTION_HEADER.GROSS_AMT,  
                                       TRN_COLLECTION_HEADER.LESS_AMT,  
                                       TRN_COLLECTION_HEADER.NET_AMT,  
                                       TRN_COLLECTION_HEADER.COLLECTION_REMARKS,  
                                       TRN_COLLECTION_HEADER.NET_INVOICE_AMT,  
                                       TRN_COLLECTION_HEADER.DUE_AMT,  
                                       TRN_COLLECTION_HEADER.RECONCILIATION_DATE,  
                                       ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') AS RECONCILIATION_DATE_DDMMYYYY,  
                                       ISNULL(CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112),'') AS RECONCILIATION_DATE_YYYYMMDD,  
                                       TRN_COLLECTION_HEADER.AUTO_POST_FLAG,  
                                       CASE WHEN TRN_COLLECTION_HEADER.AUTO_POST_FLAG = 0 THEN 'M' ELSE '' END AS POST_TYPE,  
                                       TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG,
                                       TRN_COLLECTION_HEADER.TALLIED  
                                FROM   TRN_COLLECTION_HEADER         
                                INNER JOIN MST_COMPANY      
                                ON TRN_COLLECTION_HEADER.COMPANY_ID      = MST_COMPANY.COMPANY_ID  
                                LEFT  JOIN MST_PAYMENT_TYPE 
                                ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID  
                                LEFT  JOIN MST_BANK         
                                ON TRN_COLLECTION_HEADER.BANK_ID         = MST_BANK.BANK_ID   
                            )";
                //---------------------------------------------
                dmlService.J_ExecSql(strSQL);
                //---------------------------------------------
                #endregion

                //-- VIEW [VW_COLLECTION_DETAIL] -- ADDED BY DHRUB ON 29/04/2015
                #region VW_COLLECTION_DETAIL
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME LIKE 'VW_COLLECTION_DETAIL'")) > 0)
                {
                    strSQL = @" DROP VIEW VW_COLLECTION_DETAIL ";

                    //---------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //---------------------------------------------
                }
                strSQL = @" CREATE VIEW DBO.VW_COLLECTION_DETAIL 
                            AS 
                            (
                               SELECT  TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID,  
                                       TRN_COLLECTION_HEADER.COLLECTION_DATE,  
                                       CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103) AS COLLECTION_DATE_DDMMYYYY,  
                                       CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112) AS COLLECTION_DATE_YYYYMMDD,  
                                       TRN_COLLECTION_HEADER.COMPANY_ID,  
                                       MST_COMPANY.COMPANY_NAME,  
                                       TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID,  
                                       ISNULL(MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION,'') AS PAYMENT_TYPE_DESCRIPTION,  
                                       TRN_COLLECTION_HEADER.BANK_ID,  
                                       ISNULL(MST_BANK.BANK_NAME,'') AS BANK_NAME,  
                                       TRN_COLLECTION_HEADER.REFERENCE_NO,  
                                       TRN_COLLECTION_HEADER.GROSS_AMT,  
                                       TRN_COLLECTION_HEADER.LESS_AMT,  
                                       TRN_COLLECTION_HEADER.NET_AMT,  
                                       TRN_COLLECTION_HEADER.COLLECTION_REMARKS,  
                                       TRN_COLLECTION_HEADER.NET_INVOICE_AMT,  
                                       TRN_COLLECTION_HEADER.DUE_AMT,  
                                       TRN_COLLECTION_HEADER.RECONCILIATION_DATE,  
                                       ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') AS RECONCILIATION_DATE_DDMMYYYY,  
                                       ISNULL(CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112),'')  AS RECONCILIATION_DATE_YYYYMMDD,  
                                       TRN_COLLECTION_HEADER.AUTO_POST_FLAG,  
                                       CASE WHEN TRN_COLLECTION_HEADER.AUTO_POST_FLAG = 0 THEN 'M' ELSE '' END AS POST_TYPE,  
                                       TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG,  
                                       ISNULL(TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,0) AS INVOICE_HEADER_ID,  
                                       ISNULL(TRN_INVOICE_HEADER.INVOICE_NO,'') AS INVOICE_NO,  
                                       ISNULL(TRN_INVOICE_HEADER.REFERENCE_NO,'') AS INVOICE_REFERENCE_NO,  
                                       ISNULL(TRN_INVOICE_HEADER.NET_AMOUNT,0) AS INVOICE_NET_AMOUNT,  
                                       TRN_INVOICE_HEADER.INVOICE_DATE,  
                                       ISNULL(CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103),'') AS INVOICE_DATE_DDMMYYYY,  
                                       ISNULL(CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112),'')  AS INVOICE_DATE_YYYYMMDD,
                                       TRN_COLLECTION_HEADER.TALLIED,  
                                       ISNULL(TRN_INVOICE_HEADER.PARTY_ID,0) AS PARTY_ID,  
                                       ISNULL(MST_PARTY.PARTY_NAME,'') AS PARTY_NAME,  
                                       ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,0) AS COLLECTION_AMOUNT  
                                FROM   TRN_COLLECTION_HEADER         
                                INNER JOIN MST_COMPANY           
                                ON TRN_COLLECTION_HEADER.COMPANY_ID           = MST_COMPANY.COMPANY_ID  
                                LEFT  JOIN MST_PAYMENT_TYPE      
                                ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID      = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID  
                                LEFT  JOIN MST_BANK              
                                ON TRN_COLLECTION_HEADER.BANK_ID              = MST_BANK.BANK_ID  
                                LEFT  JOIN TRN_COLLECTION_DETAIL 
                                ON TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID = TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID  
                                LEFT  JOIN TRN_INVOICE_HEADER    
                                ON TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID    = TRN_INVOICE_HEADER.INVOICE_HEADER_ID  
                                LEFT  JOIN MST_PARTY             
                                ON TRN_INVOICE_HEADER.PARTY_ID                = MST_PARTY.PARTY_ID   
                            )";
                //---------------------------------------------
                dmlService.J_ExecSql(strSQL);
                //---------------------------------------------
                
                #endregion

                //-- VIEW [VW_INVOICE_DETAIL] -- ADDED BY DHRUB ON 29/04/2015
                #region VW_INVOICE_DETAIL
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME LIKE 'VW_INVOICE_DETAIL'")) > 0)
                {
                    strSQL = @" DROP VIEW VW_INVOICE_DETAIL ";

                    //---------------------------------------------
                    dmlService.J_ExecSql(strSQL);
                    //---------------------------------------------
                }
                strSQL = @" CREATE VIEW DBO.VW_INVOICE_DETAIL 
                            AS 
                            (
                                SELECT TRN_INVOICE_HEADER.INVOICE_HEADER_ID,
                                       TRN_INVOICE_HEADER.FAYEAR_ID,
                                       TRN_INVOICE_HEADER.COMPANY_ID,
                                       TRN_INVOICE_HEADER.TRAN_TYPE,
                                       MST_COMPANY.COMPANY_NAME,
                                       TRN_INVOICE_HEADER.PARTY_ID,
                                       MST_PARTY.PARTY_NAME,
                                       MST_PARTY_CATEGORY.PARTY_CATEGORY_ID,
                                       MST_PARTY_CATEGORY.PARTY_CATEGORY_DESCRIPTION,
                                       TRN_INVOICE_HEADER.INVOICE_SERIES_ID,
                                       TRN_INVOICE_HEADER.INVOICE_NO                         AS INVOICE_NO,
                                       TRN_INVOICE_HEADER.INVOICE_DATE                       AS INVOICE_DATE,
                                       CONVERT(CHAR(10),TRN_INVOICE_HEADER.INVOICE_DATE,103) AS INVOICE_DATE_DDMMYYYY,
                                       CONVERT(CHAR(8),TRN_INVOICE_HEADER.INVOICE_DATE,112)  AS INVOICE_DATE_YYYYMMDD,
                                       TRN_INVOICE_HEADER.NET_AMOUNT                         AS NET_AMOUNT,
                                       TRN_INVOICE_HEADER.PAYMENT_TYPE_ID                    AS PAYMENT_TYPE_ID,
                                       MST_PAYMENT_TYPE.PAYMENT_TYPE_DESCRIPTION             AS PAYMENT_TYPE_DESCRIPTION,
                                       TRN_INVOICE_HEADER.BANK_ID                            AS BANK_ID,
                                       ISNULL(MST_BANK.BANK_NAME,'')                         AS BANK_NAME,
                                       TRN_INVOICE_HEADER.REFERENCE_NO                       AS REFERENCE_NO,
                                       TRN_INVOICE_HEADER.DELIVERY_MODE_ID                   AS DELIVERY_MODE_ID,
                                       PAR_DELIVERY_MODE.DELIVERY_MODE_DESC                  AS DELIVERY_MODE_DESC,
                                       TRN_INVOICE_HEADER.CONTACT_PERSON                     AS CONTACT_PERSON,
                                       TRN_INVOICE_HEADER.MOBILE_NO                          AS MOBILE_NO,
                                       TRN_INVOICE_HEADER.EMAIL_ID                           AS EMAIL_ID,
                                       TRN_INVOICE_HEADER.OFFLINE_SERIAL_ID                  AS OFFLINE_SERIAL_ID,
                                       ISNULL(TRNCOLLECT.COLLECTION_AMOUNT,0)                AS COLLECTION_AMOUNT,
                                       ISNULL(TRNCOLLECT.ADJUSTED_AMOUNT,0)                  AS ADJUSTED_AMOUNT,
                                       TRN_INVOICE_HEADER.NET_AMOUNT - ISNULL(TRNCOLLECT.COLLECTION_AMOUNT,0) - ISNULL(TRNCOLLECT.ADJUSTED_AMOUNT,0) AS PENDING_AMOUNT,
                                       TRN_COLLECTION_HEADER.COLLECTION_DATE,
                                       ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.COLLECTION_DATE,103),'') AS COLLECTION_DATE_DDMMYYYY,
                                       ISNULL(CONVERT(CHAR(8),TRN_COLLECTION_HEADER.COLLECTION_DATE,112),'')  AS COLLECTION_DATE_YYYYMMDD,
                                       ISNULL(TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG,0)                        AS ADJUSTMENT_FLAG,
                                       ISNULL(MST_PAYMENT_TYPE_COLLECTION.PAYMENT_TYPE_DESCRIPTION,'') AS PAYMENT_TYPE_DESCRIPTION_COLLECTION,
                                       ISNULL(MST_BANK_COLLECTION.BANK_NAME,'') AS BANK_NAME_COLLECTION,
                                       ISNULL(TRN_COLLECTION_HEADER.REFERENCE_NO,'') AS REFERENCE_NO_COLLECTION,
                                       ISNULL(TRN_COLLECTION_HEADER.COLLECTION_REMARKS,'') AS COLLECTION_REMARKS,
                                       ISNULL(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT,0) AS COLLECTION_ADJUSTED_AMOUNT,
                                       TRN_COLLECTION_HEADER.RECONCILIATION_DATE,
                                       ISNULL(CONVERT(CHAR(10),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,103),'') AS RECONCILIATION_DATE_DDMMYYYY,
                                       ISNULL(CONVERT(CHAR(8),TRN_COLLECTION_HEADER.RECONCILIATION_DATE,112),'')  AS RECONCILIATION_DATE_YYYYMMDD
                                FROM   TRN_INVOICE_HEADER
                                       INNER JOIN MST_PARTY          
                                       ON TRN_INVOICE_HEADER.PARTY_ID         = MST_PARTY.PARTY_ID
                                       INNER JOIN MST_COMPANY        
                                       ON TRN_INVOICE_HEADER.COMPANY_ID       = MST_COMPANY.COMPANY_ID
                                       INNER JOIN PAR_DELIVERY_MODE  
                                       ON TRN_INVOICE_HEADER.DELIVERY_MODE_ID = PAR_DELIVERY_MODE.DELIVERY_MODE_ID
                                       INNER JOIN MST_PAYMENT_TYPE   
                                       ON TRN_INVOICE_HEADER.PAYMENT_TYPE_ID  = MST_PAYMENT_TYPE.PAYMENT_TYPE_ID
                                       INNER JOIN MST_PARTY_CATEGORY 
                                       ON MST_PARTY.PARTY_CATEGORY_ID         = MST_PARTY_CATEGORY.PARTY_CATEGORY_ID
                                       LEFT  JOIN MST_BANK           
                                       ON TRN_INVOICE_HEADER.BANK_ID          = MST_BANK.BANK_ID
                                       LEFT  JOIN TRN_COLLECTION_DETAIL
                                       ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                       LEFT  JOIN TRN_COLLECTION_HEADER
                                       ON TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID
                                       LEFT JOIN MST_PAYMENT_TYPE AS MST_PAYMENT_TYPE_COLLECTION
                                       ON TRN_COLLECTION_HEADER.PAYMENT_TYPE_ID = MST_PAYMENT_TYPE_COLLECTION.PAYMENT_TYPE_ID
                                       LEFT JOIN MST_BANK AS MST_BANK_COLLECTION
                                       ON TRN_COLLECTION_HEADER.BANK_ID = MST_BANK_COLLECTION.BANK_ID
                                       LEFT  JOIN ( SELECT COLLADJ.INVOICE_HEADER_ID,
                                                           SUM(COLLADJ.COLLECTION_AMOUNT) AS COLLECTION_AMOUNT,
                                                           SUM(COLLADJ.ADJUSTED_AMOUNT)   AS ADJUSTED_AMOUNT
                                                    FROM   (
                                                           ------------- INVOICE WISE TOTAL COLLECTION
                                                           SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                                  SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS COLLECTION_AMOUNT,
                                                                  0                                            AS ADJUSTED_AMOUNT
                                                           FROM   TRN_COLLECTION_DETAIL,
                                                                  TRN_COLLECTION_HEADER
                                                           WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
                                                           AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 0
                                                           GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                                           UNION
                                                           ------------- INVOICE WISE TOTAL ADJUSTMENT
                                                           SELECT TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID,
                                                                  0                                            AS COLLECTION_AMOUNT,
                                                                  SUM(TRN_COLLECTION_DETAIL.COLLECTION_AMOUNT) AS ADJUSTED_AMOUNT
                                                           FROM   TRN_COLLECTION_DETAIL,
                                                                  TRN_COLLECTION_HEADER
                                                           WHERE  TRN_COLLECTION_DETAIL.COLLECTION_HEADER_ID = TRN_COLLECTION_HEADER.COLLECTION_HEADER_ID            
                                                           AND    TRN_COLLECTION_HEADER.ADJUSTMENT_FLAG = 1
                                                           GROUP BY TRN_COLLECTION_DETAIL.INVOICE_HEADER_ID
                                                          ) AS COLLADJ
				                                    GROUP BY COLLADJ.INVOICE_HEADER_ID
				                                  ) AS TRNCOLLECT 
                                          ON TRN_INVOICE_HEADER.INVOICE_HEADER_ID = TRNCOLLECT.INVOICE_HEADER_ID
                                WHERE    TRN_INVOICE_HEADER.RECON_FLAG = 0  
                                AND     (TRN_INVOICE_HEADER.TRAN_TYPE = 'INV' OR TRN_INVOICE_HEADER.TRAN_TYPE = 'OINV')
                            )";
                //---------------------------------------------
                dmlService.J_ExecSql(strSQL);
                //---------------------------------------------

                #endregion

                //--
                #region INDEXES

                #region MST_PARTY [IDX_PARTY_NAME]
                //---------- MST_PARTY
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_PARTY_NAME'")) > 0)
                {
                    strSQL = @" DROP INDEX MST_PARTY.IDX_PARTY_NAME";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_PARTY_NAME        ON MST_PARTY (PARTY_NAME)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region MST_PARTY [IDX_PARTY_CATEGORY_ID]
                //---------- MST_PARTY
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_PARTY_CATEGORY_ID'")) > 0)
                {
                    strSQL = @" DROP INDEX MST_PARTY.IDX_PARTY_CATEGORY_ID";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_PARTY_CATEGORY_ID ON MST_PARTY (PARTY_CATEGORY_ID)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_INVOICE_HEADER [IDX_INVOICE_NO]
                //---------- MST_PARTY
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_INVOICE_NO'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_INVOICE_HEADER.IDX_INVOICE_NO";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_INVOICE_NO   ON TRN_INVOICE_HEADER (INVOICE_NO)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_INVOICE_HEADER [IDX_INVOICE_DATE]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_INVOICE_DATE'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_INVOICE_HEADER.IDX_INVOICE_DATE";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_INVOICE_DATE ON TRN_INVOICE_HEADER (INVOICE_DATE)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_INVOICE_HEADER [IDX_PARTY_ID]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_PARTY_ID'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_INVOICE_HEADER.IDX_PARTY_ID";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_PARTY_ID     ON TRN_INVOICE_HEADER (PARTY_ID)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_INVOICE_HEADER [IDX_FAYEAR_ID]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_FAYEAR_ID'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_INVOICE_HEADER.IDX_FAYEAR_ID";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_FAYEAR_ID    ON TRN_INVOICE_HEADER (FAYEAR_ID)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_COLLECTION_HEADER [IDX_COLLECTION_DATE]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_COLLECTION_DATE'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_COLLECTION_HEADER.IDX_COLLECTION_DATE";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_COLLECTION_DATE     ON TRN_COLLECTION_HEADER (COLLECTION_DATE)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_COLLECTION_HEADER [IDX_RECONCILIATION_DATE]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_RECONCILIATION_DATE'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_COLLECTION_HEADER.IDX_RECONCILIATION_DATE";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_RECONCILIATION_DATE ON TRN_COLLECTION_HEADER (RECONCILIATION_DATE)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_COLLECTION_DETAIL [IDX_COLLECTION_HEADER_ID]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_COLLECTION_HEADER_ID'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_COLLECTION_DETAIL.IDX_COLLECTION_HEADER_ID";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_COLLECTION_HEADER_ID ON TRN_COLLECTION_DETAIL(COLLECTION_HEADER_ID)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #region TRN_COLLECTION_DETAIL [IDX_INVOICE_HEADER_ID]
                if (Convert.ToInt32(dmlService.J_ExecSqlReturnScalar("SELECT COUNT(*) FROM SYS.INDEXES WHERE NAME LIKE 'IDX_INVOICE_HEADER_ID'")) > 0)
                {
                    strSQL = @" DROP INDEX TRN_COLLECTION_DETAIL.IDX_INVOICE_HEADER_ID";
                    dmlService.J_ExecSql(strSQL);
                }
                strSQL = "CREATE INDEX IDX_INVOICE_HEADER_ID    ON TRN_COLLECTION_DETAIL(INVOICE_HEADER_ID)";
                dmlService.J_ExecSql(strSQL);

                #endregion 

                #endregion

                // cursor will be normal mode
                cmnService.J_NormalMode();
                
                txtInfo.Visible = true;
                txtInfo.Text = "Database Maintainence is completed.";
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }			
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion

        #endregion

    }
}