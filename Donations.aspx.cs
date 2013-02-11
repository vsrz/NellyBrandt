using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Donations : System.Web.UI.Page
{
    protected void ForwardToPaypal(object s, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        NbpDataContext db = new NbpDataContext();

        string postUrl = "https://www.paypal.com/cgi-bin/webscr";
        string cmd = "_donations";
        string business = "DD3QBBHPA59RW";
        string lc = "US";
        string item_name = "Jeremy Villegas c/o Nelly Brandt";
        string currency_code = "USD";
        string bn = "PP-DonationsBF:btn_donateCC_LG.gif:NonHosted";
        

        sb.Append(postUrl + "?cmd=" + cmd);
        sb.Append("&business=" + business);
        sb.Append("&lc=" + lc);
        sb.Append("&item_name=" + item_name);
        sb.Append("&currency_code=" + currency_code);
        sb.Append("&bn=" + bn);

        // audit stuff
        audit a = new audit();
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "Donations.aspx";
        a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = "Donation Link Clicked";
        a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();

        // now send redirect
        Response.Redirect(sb.ToString());
        
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
