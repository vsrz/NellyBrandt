using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;

/// <summary>
/// Summary description for DefaultPage
/// </summary>
public class DefaultPage : System.Web.UI.Page
{
	public DefaultPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    /// <summary>
    /// Adds the Tagalog Culture to the default CultureInfo class
    /// </summary>
    public class TagalogCultureInfo : CultureInfo
    {
        private string _cultureName;
        private string _cultureDescription;
        
        public TagalogCultureInfo(string extendedCulture) : base("en-US")            
        {
            if (extendedCulture != "tl-PH")
            {
                throw new NotSupportedException("The culture " + extendedCulture + " is not supported");
            }

            _cultureName = extendedCulture;

            switch (extendedCulture)
            {
                case "tl-PH":
                    _cultureDescription = "Filipino (Philippines)";
                    break;
            }
        }

        public override string DisplayName
        {
            get
            {
                return _cultureDescription;
            }
        }
        
        public override string Name
        {
            get
            {
                return _cultureName;
            }
        }

        public override string EnglishName
        {
            get
            {
                return _cultureDescription;
            }
        }

        public override string NativeName
        {
            get
            {
                return _cultureDescription;
            }
        }
    }

    protected override void InitializeCulture()
    {
        base.InitializeCulture();

        if (Session["Culture"] == null)
        {
            // Set the CurrentCulture property to the culture associated with the Web
            // browser's current language setting.
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
        }
        else
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Culture"].ToString());
            Page.Culture = Session["Culture"].ToString();
            Page.UICulture = Session["Culture"].ToString();
        }
    }

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

    public void AuditTransaction(string type, string desc)
    {
        NbpDataContext db = new NbpDataContext();

        // audit stuff
        audit a = new audit();
        a.auditId = Guid.NewGuid();
        a.auditRemoteAddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
        a.auditRequestedUrl = "";
        a.auditSessionEmail = Session["nbpEmail"] != null ? Session["nbpEmail"].ToString() : "";
        a.auditSessionId = Session.SessionID.ToString();
        a.auditSessionName = Session["nbpName"] != null ? Session["nbpName"].ToString() : "";
        a.auditTimestamp = DateTime.Now;
        a.auditType = type;
        a.auditDescription = desc;
        a.auditReferrer = "";
        a.auditLogonUser = Request.ServerVariables["LOGON_USER"];
        db.audits.InsertOnSubmit(a);
        db.SubmitChanges();
    }

}
