using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        
        JayaSoftwares.Service objWebService = new JayaSoftwares.Service();

        ds = objWebService.ReturnUsersData(1, "14", 0);
        ds = objWebService.ReturnUsersData(4, "", 1);

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();


    }
}
