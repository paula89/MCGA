using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_LPPA
{
    public partial class CRUD_LPPA : System.Web.UI.MasterPage
    {
        string x = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script type='text/javascript'>function PageLoadLogin() {if (localStorage.getItem('timestamp') > Date.now()){}else{location.replace('Index.aspx')}}PageLoadLogin();</script>");
            
            if (Request.QueryString["user"] == "")
            {
              
            }
            else {
                if (x == "")
                {
                    x = Request.QueryString["user"];
                
                    if (x == Request.QueryString["user"])
                    {
                        x = Request.QueryString["user"];
                    }
                }
                userTitle.InnerText = x;
            }
            

            //Intento de enmascarar
            //string url = Request.Url.ToString();  
            //Response.Write(a);
            //string[] strValues = url.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            
            
            User.Visible = true;
            Admin.Visible = true;
            SuperAdmin.Visible = true;
            
        }


        
    }
}