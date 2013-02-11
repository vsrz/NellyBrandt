using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using System.Globalization;
using System.Reflection;

public partial class Default : DefaultPage
{

        



    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        /*
        if (Session["Culture"] == null)
        {
            if (Request.Cookies.Get("Culture") != null)
            {
                HttpCookie c = Request.Cookies.Get("Culture");
                Session.Remove("Culture");
                switch (c.Value.ToString())
                {
                    case "fil-PH":
                        Session.Add("Culture", "fil-PH");
                        break;
                    case "en-US":
                        Session.Add("Culture", "en-US");
                        break;
                    default:
                        Session.Add("Culture", "en-US");
                        break;
                }
            }
        }
        */
        if (!Page.IsPostBack)
        {
            ddlLanguages.Items.Add(new ListItem(CultureInfo.GetCultureInfo("en-US").DisplayName.ToString(), "en-US"));
            ddlLanguages.Items.Add(new ListItem(CultureInfo.GetCultureInfo("fil-PH").DisplayName.ToString(), "fil-PH"));

            // first set session if cookie is found
            /*if (Request.Cookies["Culture"].ToString() != null)
            {
                ddlLanguages.Items.FindByValue(Request.Cookies["Culture"].ToString()).Selected = true;
                if (Session["Culture"].ToString() != Request.Cookies["Culture"].ToString())
                {
                    Session.Add("Culture", Request.Cookies["Culture"].ToString());
                }
            } */
            if (Session["Culture"] != null)
            {
                ddlLanguages.Items.FindByValue(Session["Culture"].ToString()).Selected = true;
            }
            /*
            if (Page.Culture != ddlLanguages.SelectedValue.ToString())
            {
                Page.Culture = ddlLanguages.SelectedValue.ToString();
                Server.Transfer("Default.aspx", false);
            }
            */
            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void ddlLanguages_Changed(object sender, EventArgs e)
    {
        NbpDataContext db = new NbpDataContext();
        string selectedLanguage = ddlLanguages.SelectedValue.ToString();
        HttpCookie c = new HttpCookie("Culture");
        c.Expires = DateTime.Now.AddYears(5);
        c.Value = selectedLanguage;
        Response.Cookies.Add(c);

        Page.Culture = selectedLanguage;
        Session.Remove("Culture");
        Session.Add("Culture", selectedLanguage);

        // track from db
        audit a = new audit();
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "Default.aspx";
        a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = "Language changed";
        a.auditDescription = ddlLanguages.SelectedValue.ToString();
        a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();

        Server.Transfer("Default.aspx", false);
        
        
        
    }
}
