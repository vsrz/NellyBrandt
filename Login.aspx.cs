using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : DefaultPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.ServerVariables["REMOTE_ADDR"].ToString() != "144.37.205.10")
            {
                AuditTransaction("Login page loaded", "Login page loaded");
            }
        }
        
    }

    protected void Login_Successful(object s, EventArgs e)
    {
        Response.Redirect("Admin.aspx");

    }

    protected void Login_Error(object s, EventArgs e)
    {        
        if (Request.ServerVariables["REMOTE_ADDR"].ToString() != "144.37.205.10")
        {
            AuditTransaction("Login failed", "Username: " + Login1.UserName + " Password: " + Login1.Password);
        }
        

    }
}
