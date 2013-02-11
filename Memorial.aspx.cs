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

public partial class Memorial : DefaultPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PopulatePartySizeList();
        PopulatePhotoView();
        viewLeftPanel.ActiveViewIndex = 1;
        if (DateTime.Now > new DateTime(2009, 10, 31))
        {
            MultiView1.ActiveViewIndex = 3;
        } 
        else if (IPRecentlyRegistered())
        {
            cookieName.Text = Request.Cookies["MemorialReservation"].Value.ToString();
            MultiView1.ActiveViewIndex = 2;
        }

    }

    private void PopulatePhotoView()
    {
        NbpDataContext db = new NbpDataContext();

        var query = from p in db.photos
                    //where p.photoIsMemorial == true
                    select p;

        ArrayList s = new ArrayList();

        foreach (var q in query)
        {
            s.Add(q);

        }
        
        dlistPhotos.DataSource = s;
        dlistPhotos.DataBind();
    }

    private void PopulatePartySizeList()
    {
        for (int i=0;i<=20;++i)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            reservationPartySize.Items.Add(li);         

        }
        reservationPartySize.Width = 128;
    }
    private bool EmailAlreadyUsed(string email)
    {
        NbpDataContext db = new NbpDataContext();

        var query = from p in db.reservations
                    where p.reservationEmail.ToString() == email
                    select p;

        return query.Count() > 0;

    }

    
    private bool IPRecentlyRegistered()
    {                
        // Going to try cookies instead
        if (Request.Cookies["MemorialReservation"] != null)
        {
            return Request.Cookies["MemorialReservation"].Value.ToString() != null;
        }
        
        return false;
        
        /* NbpDataContext db = new NbpDataContext();

        var query = from p in db.reservations
                    where p.reservationRemoteAddr.ToString() == Request.ServerVariables["REMOTE_ADDR"].ToString()
                    select p;

        return query.Count() > 0;
        */ 
        
    }

    private bool ReservationIsValid()
    {
        int val=0;

        if (reservationName.Text.ToString().Length < 3)
        {
            reservationError.Items.Add("The name you entered was too short.");
            val++;
        }
        if (EmailAlreadyUsed(reservationEmail.Text.ToString()))
        {            
            reservationError.Items.Add("That e-mail address has already registered");
            val++;
        }
        
        if(reservationEmail.Text.ToString().Length==0)
        {
            reservationError.Items.Add("Your e-mail address was not received");
            val++;
        }

        if (reservationPhoneNumber.Text.ToString().Length > 15)
        {
            reservationError.Items.Add("Please enter your phone number in XXX-XXX-XXXX format");
            val++;
        }

        if (IPRecentlyRegistered())
        {
            reservationError.Items.Add("Your IP Address has already registered, please e-mail me using the About link at the top to get information about your registration.");
            val++;
        }

        if (Convert.ToInt32(reservationPartySize.SelectedItem.Value) == 0)
        {
            reservationError.Items.Add("You must register at least 1 person.");
            val++;
        }


        if (val > 0) reservationError.Visible = true;
        return val==0;
    }


    protected void ReservationSubmit_Click(object s, EventArgs e)
    {
        NbpDataContext db = new NbpDataContext();


        if (ReservationIsValid())
        {
            reservation r = new reservation();
            r.reservationEmail = reservationEmail.Text.ToString();
            r.reservationId = Guid.NewGuid();
            r.reservationName = reservationName.Text.ToString();
            r.reservationPartySize = Convert.ToInt32(reservationPartySize.SelectedItem.Value);
            r.reservationRelationship = reservationRelationship.Text.ToString();
            r.reservationRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
            r.reservationTimestamp = DateTime.Now;
            r.reservationPhoneNumber = reservationPhoneNumber.Text.ToString();
            db.reservations.InsertOnSubmit(r);
            db.SubmitChanges();
            
            Session["nbpEmail"] = reservationEmail.Text.ToString();
            Session["nbpName"] = reservationName.Text.ToString();

            // send cookie
            HttpCookie c = new HttpCookie("MemorialReservation");
            c.Expires = DateTime.Now.AddMonths(1);
            c.Value = reservationName.Text.ToString();
            Response.Cookies.Add(c);            

            MultiView1.ActiveViewIndex = 1;
        }

        audit a = new audit();        
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "Memorial.aspx";
        a.auditSessionEmail = Session["nbpEmail"]!=null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"]!=null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = "Memorial signup";
        a.auditDescription = "Email: " + reservationEmail.Text.ToString() +
            " Name: " + reservationName.Text.ToString() +
            " PartySize: " + reservationPartySize.SelectedItem.Value.ToString() +
            " Relationship: " + reservationRelationship.Text.ToString() + 
            " Phone: " + reservationPhoneNumber.Text.ToString();
        a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();
    }
}
