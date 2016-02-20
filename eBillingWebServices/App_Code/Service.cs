#region Using NameSpaces

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;

using System.Globalization;

using JayaSoftwares;

#endregion

namespace JayaSoftwares
{
    [WebService(Namespace = "http://tdsman.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]

    public class Service : System.Web.Services.WebService
    {
        public Service()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        #region Class and Private Objects Declaration

        /// <summary>
        /// 
        /// </summary>

        DBHelper objDBHelper = new DBHelper();
        General objGeneral = new General();
        string strSQLQuery = string.Empty;

       
        #endregion

        #region ReturnUsersData
        [WebMethod]
        public DataSet ReturnUsersData(int SoftwareId, string FinancialYear, int CountryId)
        {
            DataSet DS = null;

            try
            {

                strSQLQuery = "SELECT LICENSED_CD_SERIAL_CODE, " +
                              "       LICENSED_CD_SERIAL_NO, " +
                              "       INSTALL_DETAILS.REGISTRATION_DATE, " +
                              "       LICENSEE_COMPANY_NAME, " +
                              "       LICENSEE_NAME, " +
                              "       LICENSEE_EMAIL, " +
                              "       LICENSEE_MOBILE, " +
                              "       LICENSEE_PHONE, " +
                              "       LICENSEE_FAX, " +
                              "       LICENSEE_ADD, " +
                              "       LICENSEE_CITY, " +
                              "       LICENSEE_STATE, " +
                              "       LICENSEE_PIN " +
                              "FROM TDSMAN_LICENSED_CD_SERIAL, " +
                              "     TDSMAN_LICENSED_USER_DETAIL, " +
                              "     (SELECT LICENSED_USER_DETAIL_ID, " +
                              "             MIN(REGISTRATION_DATE) AS REGISTRATION_DATE " +
                              "      FROM TDSMAN_LICENSED_INSTALL_DETAILS " +
                              "      GROUP BY LICENSED_USER_DETAIL_ID) AS INSTALL_DETAILS " +
                              "WHERE TDSMAN_LICENSED_CD_SERIAL.LICENSED_CD_SERIAL_ID = TDSMAN_LICENSED_USER_DETAIL.LICENSED_CD_SERIAL_ID " +
                              "AND   TDSMAN_LICENSED_USER_DETAIL.LICENSED_USER_DETAIL_ID = INSTALL_DETAILS.LICENSED_USER_DETAIL_ID " +
                              "AND   LICENSED_CD_SERIAL_CODE <> '' " +
                              "AND   SOFTWARE_PRODUCT_ID = " + SoftwareId + " " +
                              "AND   LICENSED_CD_FY = '" + FinancialYear + "' " +
                              "AND   COUNTRY_ID = " + CountryId + " " +
                              "ORDER BY LICENSED_CD_SERIAL_CODE";

                DS = objDBHelper.gReturnDataSet(CommandType.Text, strSQLQuery);
                return DS;
            }
            catch (Exception ERR)
            {
                return null;
            }
        }
#endregion

        #region ReturntdsmanNewUsersData
        [WebMethod]
        public DataSet ReturntdsmanNewUsersData()
        {
            DataSet DS = null;

            try
            {

                strSQLQuery = @"SELECT NEW_USERS.LICENSED_USER_DETAIL_ID,
                                       NEW_USERS.LICENSEE_NAME,
                                       NEW_USERS.LICENSEE_COMPANY_NAME,
                                       NEW_USERS.LICENSEE_CITY,
                                       NEW_USERS.LICENSEE_PHONE,
                                       NEW_USERS.LICENSEE_MOBILE, 
                                       NEW_USERS.LICENSEE_MOBILE,
                                       CONVERT(VARCHAR(10), INSTALL_DETAILS.REGISTRATION_DATE, 103) AS REG_DATE,
                                       (CASE WHEN OLD_USERS_EMAIL.LICENSEE_EMAIL   IS NULL
                                			 AND   OLD_USERS_MOBILE.LICENSEE_MOBILE IS NULL THEN 1
                                			 ELSE 0 END) AS NEW
                                FROM (SELECT LICENSED_USER_DETAIL_ID,
                                             LICENSEE_NAME,
                                             LICENSEE_COMPANY_NAME,
                                             LICENSEE_CITY,
                                             LICENSEE_PHONE,
                                             LICENSEE_MOBILE, 
                                             LICENSEE_EMAIL 
                                      FROM TDSMAN_LICENSED_CD_SERIAL,
                                           TDSMAN_LICENSED_USER_DETAIL
                                       WHERE TDSMAN_LICENSED_CD_SERIAL.LICENSED_CD_SERIAL_ID = 
                                                TDSMAN_LICENSED_USER_DETAIL.LICENSED_CD_SERIAL_ID
                                       AND SOFTWARE_PRODUCT_ID = 1
                                       AND LICENSED_CD_FY      = '14'
                                       AND COUNTRY_ID          = 0 ) AS NEW_USERS
                                INNER JOIN (SELECT LICENSED_USER_DETAIL_ID, 
                                                   MIN(REGISTRATION_DATE) AS REGISTRATION_DATE 
                                            FROM TDSMAN_LICENSED_INSTALL_DETAILS 
                                            GROUP BY LICENSED_USER_DETAIL_ID) AS INSTALL_DETAILS
                                ON NEW_USERS.LICENSED_USER_DETAIL_ID = INSTALL_DETAILS.LICENSED_USER_DETAIL_ID
                                LEFT JOIN (SELECT DISTINCT LICENSEE_EMAIL 
                                           FROM TDSMAN_LICENSED_CD_SERIAL,
                                                TDSMAN_LICENSED_USER_DETAIL
                                           WHERE TDSMAN_LICENSED_CD_SERIAL.LICENSED_CD_SERIAL_ID = 
                                                      TDSMAN_LICENSED_USER_DETAIL.LICENSED_CD_SERIAL_ID
                                           AND SOFTWARE_PRODUCT_ID = 1
                                           AND LICENSED_CD_FY      = '13'
                                           AND COUNTRY_ID          = 0 ) AS OLD_USERS_EMAIL
                                ON NEW_USERS.LICENSEE_EMAIL = OLD_USERS_EMAIL.LICENSEE_EMAIL
                                LEFT JOIN (SELECT DISTINCT LICENSEE_MOBILE 
                                           FROM TDSMAN_LICENSED_CD_SERIAL,
                                                TDSMAN_LICENSED_USER_DETAIL
                                           WHERE TDSMAN_LICENSED_CD_SERIAL.LICENSED_CD_SERIAL_ID = 
                                                      TDSMAN_LICENSED_USER_DETAIL.LICENSED_CD_SERIAL_ID
                                           AND SOFTWARE_PRODUCT_ID = 1
                                           AND LICENSED_CD_FY      = '13'
                                           AND COUNTRY_ID          = 0 ) AS OLD_USERS_MOBILE
                                ON NEW_USERS.LICENSEE_MOBILE = OLD_USERS_MOBILE.LICENSEE_MOBILE
                                ORDER BY INSTALL_DETAILS.REGISTRATION_DATE DESC";

                DS = objDBHelper.gReturnDataSet(CommandType.Text, strSQLQuery);
                return DS;
            }
            catch (Exception ERR)
            {
                return null;
            }
        }
        #endregion

        
    }
}
