using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_LPPA
{
    public partial class manageUsersForm : System.Web.UI.Page
    {
        string x = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script type='text/javascript'>function PageLoadLogin() {if (localStorage.getItem('timestamp') > Date.now()){}else{location.replace('Index.aspx')}}PageLoadLogin();</script>");
            if (Request.QueryString["userID"] == "")
            {

            }
            else
            {
                if (x == "")
                {
                    x = Request.QueryString["userID"];

                    if (x == Request.QueryString["userID"])
                    {
                        x = Request.QueryString["userID"];
                    }
                }
                id.Value = x;
            }

            
        }
    }
}