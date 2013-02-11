using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string GetResource(string resourceName)
    {
        string vPath = this.Page.Request.Path;
        try
        {
            return (string)this.GetLocalResourceObject(resourceName.Replace(" ", ""));

        }
        catch
        {
            return "";
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //navigation.InnerHtml += "<li><a href=\"Default.aspx\">" + "home" + "</a></li>";
        //navigation.InnerHtml += "<li><a href=\"Memorial.aspx\">" + GetResource("litMenuMemorial") + "</a></li>";
        /*
        
        <li><a href="GuestBook.aspx">guestbook</a></li>                        
        <li><a href="PhotoView.aspx">photos</a></li>
        <li><a href="PhotoUpload.aspx">submit a photo</a></li>                        
        <li><a href="About.aspx">about</a></li>
        */
    }


    public bool LogAudit()
    {
        return true;

    }
}
