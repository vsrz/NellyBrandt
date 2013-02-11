using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PhotoView : DefaultPage
{
    private int _pageid;
    private Guid _gid;
    protected NbpDataContext db = new NbpDataContext();

    public int pageid
    {
        get { return _pageid; }
        set { _pageid = value; }
    }

    public Guid photoid
    {
        get { return _gid; }
        set { _gid = value; }
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

        if (Request.QueryString["gid"] != null)
        {
            try
            {
                photoid = new Guid(Request.QueryString["gid"]);
            }
            catch
            {
                photoid = GetFirstPhotoByGuid();
            }
        }

    }

    private Guid GetFirstPhotoByGuid()
    {        
        var query = from p in db.photos
                    where (p.photoSequenceNumber == 0)
                    select p;

        return query.First().photoId;
    }

    protected void SetPhotoNavigation()
    {
        /* find out if there is a next */
        var q2 = from p in db.photos
                where p.photoSequenceNumber == pageid
                select p;

        

        /* is this the first image */
        if (pageid == 0)
        {
            lnkPreviousImage.Visible = !lnkPreviousImage.Visible;
            lblFirstImage.Visible = !lblFirstImage.Visible;
        }

        /* are we at the last image? */
        var q = from p in db.photos
                where p.photoPageType == 0
                select p;

        if (pageid + 1 == q.Count())
        {
            lnkNextImage.Visible = !lnkNextImage.Visible;
            lblLastImage.Visible = !lblLastImage.Visible;
        }

        lnkPreviousImage.NavigateUrl = Request.Url.AbsolutePath + "?p=" + (pageid - 1);
        lnkNextImage.NavigateUrl = Request.Url.AbsolutePath + "?p=" + (pageid + 1);
        
        // set the random photo link
        lnkRandomImage.NavigateUrl = GetRandomPhotoLink();                
    }

    /// <summary>
    /// Returns hyperlink text to a random photo
    /// </summary>
    /// <returns></returns>
    protected string GetRandomPhotoLink()
    {
        var query = from p in db.photos
                    select p;
        int nextpid = pageid;
        Random r = new Random();

        // continue to generate a random page if the number that is generated
        // is equal to the current page
        while (nextpid == pageid)
            nextpid = r.Next(0, query.Count());
            
        
        return "PhotoView.aspx?p=" + nextpid;
        

    }

    protected void SetCurrentImage(int pageid)
    {
        decimal scaling_pct = 0;
        decimal imgWidth;
        decimal imgHeight;
        System.Drawing.Image img;
        
        var q = from p in db.photos
                where p.photoSequenceNumber == pageid
                select p;

        imgCurrent.ImageUrl = q.First().photoLocation;
        imgHyperlink.NavigateUrl = q.First().photoLocation;
        imgCurrentGuid.Value = q.First().photoId.ToString();
        imgCaption.Text = q.First().photoCaption.ToString();

        /* Determine the width and height of the file */
        img = System.Drawing.Image.FromFile(MapPath(imgCurrent.ImageUrl.ToString()));
        imgWidth = img.Width;
        imgHeight = img.Height;

        try
        {
            imgWidth = Convert.ToDecimal(img.Width);
            imgHeight = Convert.ToDecimal(img.Height);
        }
        catch 
        {
            imgWidth = 0;
            imgHeight = 0;
        }
                    
        if (imgWidth > 650)
        {
            // figure out the amount we need to reduce the image size by                        
            scaling_pct = 650 / imgWidth;
            imgCurrent.Width = (int)(scaling_pct * imgWidth);
            imgCurrent.Height = (int)(scaling_pct * imgHeight);
        }
            
    }

    protected void SetImageDate()
    {
        var q = from p in db.photos
                where p.photoSequenceNumber == pageid
                select p;

        lblImageDate.Text = "Taken: " + ((DateTime)(q.First().photoDate)).ToShortDateString();
        
    }

    protected void btnAddComment_Submit_Click(object sender, EventArgs e)
    {
        comment newComment = new comment();

        newComment.commentAuthor = txtNewComment_username.Text.ToString();
        newComment.commentIpSource = Request.ServerVariables["REMOTE_ADDR"];
        newComment.commentPhotoId = new Guid(imgCurrentGuid.Value.ToString());
        newComment.commentText = txtNewComment_Text.Text.ToString();
        newComment.commentId = Guid.NewGuid();
        newComment.commentTimestamp = DateTime.Now;

        db.comments.InsertOnSubmit(newComment);
        db.SubmitChanges();

        Session["nbpEmail"] = txtNewComment_email.Text.ToString();
        Session["nbpName"] = txtNewComment_username.Text.ToString();
        LoadCommentData();
        ToggleAddCommentDiv();

        audit a = new audit();
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "PhotoView.aspx";
        a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = "PhotoView Comment Posted";
        a.auditDescription = "Author: " + txtNewComment_username.Text.ToString() +
            " Text: " + txtNewComment_Text.Text.ToString();            
        a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();

    }

    protected void LoadCommentData()
    {
        Guid currentImageGuid = new Guid(imgCurrentGuid.Value.ToString());

        var q = from p in db.comments
                where p.commentPhotoId == currentImageGuid
                orderby p.commentTimestamp descending
                select p;

        rpCommentsView.DataSource = q;
        rpCommentsView.DataBind();
        

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPageId();        
        SetPhotoNavigation();
        SetCurrentImage(pageid);
        SetImageDate();
        LoadCommentData();
    }

    protected void ToggleAddCommentDiv()
    {
        /* toggle comment addition */
        btnUnhideAddComment.Visible = !btnUnhideAddComment.Visible;
        divAddComment.Visible = !divAddComment.Visible;
    }

    protected void btnUnhideAddComment_Click(object sender, EventArgs e)
    {
        ToggleAddCommentDiv();
    }
}
