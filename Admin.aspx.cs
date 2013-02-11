using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : LoggedInPage
{
    private int _pageid;
    protected NbpDataContext db = new NbpDataContext();

    public int pageid
    {
        get { return _pageid; }
        set { _pageid = value; }
    }

    /// <summary>
    /// Get the page ID from QueryString "GET" Variables 
    /// </summary>
    private void GetPageId()
    {
        if (Request.QueryString["p"] != null)
        {

            try
            {
                pageid = Convert.ToInt32(Request.QueryString["p"], 10);

            }
            catch
            {
                pageid = 0;
            }

        }
        else
        {
            pageid = 0;
        }
    }

    protected void ShowGridView()
    {
        htmlAdminTable.Visible = false;
        GridView1.Visible = true;
        lnkMainMenu.Visible = true;

    }
        
    protected void Page_Load(object sender, EventArgs e)
    {        
        GetPageId();

        switch (pageid)
        {
            case 0:
                lnkMainMenu.Visible = false;
                htmlAdminTable.Visible = true;
                break;
            case 1:
                // show audit table
                var q = from p in db.audits
                        orderby p.auditTimestamp descending
                        select new { p.auditTimestamp, p.auditRemoteAddr, p.auditRequestedUrl, p.auditDescription };
                
                
                GridView1.DataSource = q;
                GridView1.DataBind();
                ShowGridView();                
                break;
            case 2:
                // show memorial table
                var m = from p in db.reservations
                        orderby p.reservationTimestamp descending
                        select new { p.reservationName, p.reservationPartySize, p.reservationPhoneNumber, p.reservationEmail };

                GridView1.DataSource = m;
                GridView1.DataBind();
                ShowGridView();

                break;
            case 3:
                // redirect            
                break;
            case 4:
                // new user signup
                NewUser.Visible = true;
                htmlAdminTable.Visible = false;
                lnkMainMenu.Visible = true;
                break;

        }
    }


}
