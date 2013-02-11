using System;
using System.Collections;
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

public partial class GuestBook : DefaultPage
{
    private int _pageid;

    public int pageid
    {
        get { return _pageid; }
        set { _pageid = value; }
    }

    protected void PopulateRepeater()
    {
        NbpDataContext db = new NbpDataContext();
        var q = from p in db.guestbooks
                orderby p.guestbookTimestamp descending
                select p;

        rpGuestbookViewer.DataSource = q;
        rpGuestbookViewer.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != null)
        {
            try
            {
                pageid = Convert.ToInt32(Request.QueryString["p"]);
            }
            catch
            {
                pageid = 0;
            }            
        }

        if (pageid == 0)
        {
            PopulateRepeater();
        }


        MultiView1.ActiveViewIndex = pageid;
    }

    protected void btnGuestbookSubmit_Click(object s, EventArgs e)
    {
        if (Page.IsValid)
        {
            NbpDataContext db = new NbpDataContext();
            guestbook g = new guestbook();

            g.guestbookId = Guid.NewGuid();
            g.guestbookEmail = txtEmail.Text.ToString();
            g.guestbookRelationship = txtRelationship.Text.ToString();
            g.guestbookSourceIp = Request.ServerVariables["REMOTE_ADDR"];
            g.guestbookText = txtText.Text.ToString();
            g.guestbookTimestamp = DateTime.Now;
            g.guestbookAuthor = txtAuthor.Text.ToString();

            db.guestbooks.InsertOnSubmit(g);
            db.SubmitChanges();

            Session["nbpEmail"] = txtEmail.Text.ToString();
            Session["nbpName"] = txtAuthor.Text.ToString();

            // audit stuff
            audit a = new audit();
            a.auditId = Guid.NewGuid();
            a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
            a.auditRequestedUrl = "Guestbook.aspx";
            a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
            a.auditSessionId = Session.SessionID.ToString();
            a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
            a.auditTimestamp = DateTime.Now;
            a.auditType = "Guestbook Signed";
            a.auditDescription = "E-Mail: " + txtEmail.Text.ToString() +
                " Relationship: " + txtRelationship.Text.ToString() +
                " Text: " + txtText.Text.ToString();
            a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
            a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
            db.audits.InsertOnSubmit(a);
            db.SubmitChanges();


            MultiView1.ActiveViewIndex = 2;
            MultiView1.DataBind();
        }
        else
        {
            foreach (BaseValidator valControl in Page.Validators)
            {
                WebControl aControl = (WebControl)Page.FindControl(valControl.ControlToValidate);
                if (!valControl.IsValid)
                    aControl.BackColor = System.Drawing.Color.Yellow;
                else
                    aControl.BackColor = System.Drawing.Color.White;
            }
            

        }

    }

}
