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
using System.Drawing;

public partial class PhotoUpload : DefaultPage
{
    NbpDataContext db = new NbpDataContext();

    bool CheckFileType(string f)
    {
        string ext = System.IO.Path.GetExtension(f);
        switch (ext.ToLower())
        {
            case ".gif":
            case ".png":
            case ".jpg":
            case ".jpeg":
                return true;
            default:
                return false;
        }
    }

    protected System.Drawing.Image PhotoReformat(System.IO.Stream filestream)
    {
        System.Drawing.Image img;
        System.Drawing.Image resizedImage;

        img = System.Drawing.Image.FromStream(filestream);

        resizedImage = ResizeImage(img);

        filestream.Close();

        return resizedImage;

    }

    /// <summary>
    /// Returns new image size based on value provided in maxWidth.
    /// </summary>
    /// <param name="old">Image size</param>
    /// <param name="maxWidth">Maximum image width</param>
    /// <returns></returns>
    private static Size CalculateNewImageSize(Size old, int maxWidth)
    {
        if (old.Width > maxWidth)
        {
            decimal scaling_pct = (decimal)maxWidth / (decimal)old.Width;
            return new Size((int)(scaling_pct * (decimal)old.Width), (int)(scaling_pct * (decimal)old.Height));
        }
        
        return old;
    }

    /// <summary>
    /// Returns new image size based on default value of 650 pixels wide.
    /// </summary>
    /// <param name="old">Size of the image</param>
    /// <returns></returns>
    private static Size CalculateNewImageSize(Size old)
    {
        return CalculateNewImageSize(old, 650);
    }

    protected static System.Drawing.Image ResizeImageFile(System.Drawing.Image img)
    {        
        using (img)
        {
            Size newSize = CalculateNewImageSize(img.Size);
            using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555))
            {
                using (Graphics canvas = Graphics.FromImage(newImage))
                {
                    canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    canvas.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    canvas.DrawImage(img, new Rectangle(new Point(0,0), newSize));

                    System.IO.MemoryStream m = new System.IO.MemoryStream();
                    return (System.Drawing.Image.FromHbitmap(newImage.GetHbitmap()));

                }

            }

        }        
    }

    protected System.Drawing.Image ResizeImage(System.Drawing.Image img)
    {
        System.Drawing.Image.GetThumbnailImageAbort dummyCallback;
        System.Drawing.Image fullSizeImg;        
        decimal imgWidth, imgHeight, scaling_pct = 0;
        int newImgWidth, newImgHeight;


        dummyCallback = new System.Drawing.Image.GetThumbnailImageAbort(ref ImageCallback);

        fullSizeImg = img;

        try
        {
            imgWidth = img.Width;
            imgHeight = img.Height;
        }
        catch
        {
            return null;

        }

        if (imgWidth > 650)
        {
            // figure out the amount we need to reduce the image size by                        
            scaling_pct = 650 / imgWidth;
            newImgWidth = (int)(scaling_pct * imgWidth);
            newImgHeight = (int)(scaling_pct * imgHeight);
        }
        else
        {
            newImgHeight = (int)imgHeight;
            newImgWidth = (int)imgWidth;
        }        

        //resizedImage = fullSizeImg.GetThumbnailImage(newImgWidth, newImgHeight, dummyCallback, IntPtr.Zero);

        return ResizeImageFile(fullSizeImg);

    }

    protected bool ImageCallback()
    {
        return false;
    }


    protected void PhotoUpload_Submit_Click(object sender, EventArgs e)
    {
        if (upload_File.FileContent.Length > 0 && CheckFileType(upload_File.FileName))
        {
            setting s;
            int photoSeqNumber = 0;
            string filename;
            System.Drawing.Image img;

            /* Validate and resize image */
            img = PhotoReformat(upload_File.FileContent);

            /* Get filename and increment setting */
            var q = from p in db.settings
                    where p.settingName == "NextImageFilename"
                    select p;
            s = q.First();

            filename = "images/image" + q.First().settingInt.ToString().PadLeft(3, '0');
            filename += System.IO.Path.GetExtension(upload_File.FileName);

            s.settingInt++;
            db.SubmitChanges();
            
            /* Copy file into image store */
            img.Save(MapPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);

            //upload_File.SaveAs(MapPath(filename));

            /* Build new photo in database */
            photo uploadedPhoto = new photo();

            uploadedPhoto.photoId = Guid.NewGuid();
            uploadedPhoto.photoDate = Convert.ToDateTime(upload_PhotoDate.Text.ToString());
            uploadedPhoto.photoCaption = upload_Caption.Text.ToString();
            uploadedPhoto.photoLocation = filename;
            uploadedPhoto.photoPageType = 0;
            uploadedPhoto.photoUploadedTimestamp = DateTime.Now;
            uploadedPhoto.photoUploader = upload_Username.Text.ToString();
            uploadedPhoto.photoIsMemorial = rbMemorial.Checked;

            /* get next sequence number */
            var sq = from p in db.photos
                    where p.photoSequenceNumber != null
                    orderby p.photoSequenceNumber descending
                    select p.photoSequenceNumber;

            if (sq.Count() != 0)
            {
                photoSeqNumber = sq.First().Value + 1;
            }


            uploadedPhoto.photoSequenceNumber = photoSeqNumber;

            db.photos.InsertOnSubmit(uploadedPhoto);
            db.SubmitChanges();

            lnkImageUploadLink.NavigateUrl = "PhotoView.aspx?p=" + photoSeqNumber;            
            MultiView1.ActiveViewIndex = 1;

            Session["nbpName"] = upload_Username.Text.ToString();
            Session["nbpEmail"] = upload_Email.Text.ToString();

        }

        // audit stuff
        audit a = new audit();
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "PhotoUpload.aspx";
        a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = "PhotoUpload attempt";
        a.auditDescription = "PhotoDate: " + upload_PhotoDate.Text.ToString() +
            " Caption: " + upload_Caption.Text.ToString() +
            " Filename: " + upload_File.FileName.ToString() +
            " Name: " + upload_Username.Text.ToString();
        a.auditReferrer = Request.UrlReferrer.AbsolutePath.ToString();
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();


    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
