using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoggedInPage
/// </summary>
public class LoggedInPage : DefaultPage
{
	public LoggedInPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!User.Identity.IsAuthenticated)
        {
            Server.Transfer(Page.ResolveClientUrl("Login.aspx"));

        }
        

    }
}
