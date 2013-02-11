<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Memorial.aspx.cs" Inherits="Memorial" Title="Nelly Pacis Brandt / Memorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 60%; float: left;">
    <asp:MultiView runat="server" ID="viewLeftPanel" Visible="false">   
        <asp:View runat="server" ID="View1">        
            <h2><asp:Literal runat="server" ID="litInformationHeader" Text="<%$ Resources:litInformationHeader %>"></asp:Literal></h2>        
            <p>
                <asp:Literal runat="server" ID="litInformationBody" Text="<%$ Resources:litInformationBody %>"></asp:Literal>
            </p>
            <h3><asp:Literal runat="server" ID="litChurchDirectionsHeading" Text="<%$ Resources:litChurchDirectionsHeading %>"></asp:Literal></h3>
            <ul>
                <li>Take <b>I-5 North</b> past Los Angeles</li>
                <li>Exit <b>CA-134 W/Ventura Fwy</b> toward Ventura</li>
                <li>Take <b>exit 1D</b> for <b>Cahuenga Blvd.</b></li>
                <li>Turn <b>right</b> onto <b>Cahuenga St.</b></li>
                <li>Turn <b>left</b> at <b>Erwin St.</b></li>
                <li>Destination will be on the <b>left</b></li>            
            </ul>
            <p><a href="http://maps.google.com/maps?rlz=1C1GGLS_enUS304US304&sourceid=chrome&um=1&ie=UTF-8&cid=0,0,454534903219084714&fb=1&hq=st+patricks+church&hnear=north+hollywood&gl=us&daddr=6160+Cartwright+Ave,+North+Hollywood,+CA+91606-5005&geocode=3788813476956277314,34.182821,-118.363247&ei=_D7BSpmRFYPqtAO_0ZmIBQ&sa=X&oi=local_result&ct=directions-to&resnum=1" target="_blank"><asp:Literal runat="server" ID="litMapOfChurch" Text="<%$ Resources:litMapOfChurch %>"></asp:Literal></a></p>
            <p>
                <a href="http://saintpatrickcatholicchurch.com/" target="_blank">Saint Patrick's Catholic Church</a><br />
                6160 Cartwright Ave<br />
                North Hollywood, CA 91606-5005
            </p>
            <br />
            <h3><asp:Literal runat="server" ID="litReceptionHeading" Text="<%$ Resources:litReceptionHeading %>"></asp:Literal></h3>
            <p>
                <asp:Literal runat="server" ID="litReceptionBody" Text="<%$ Resources:litReceptionBody %>"></asp:Literal>
            </p>
            <h3><asp:Literal runat="server" ID="litReceptionDirectionsHeading" Text="<%$ Resources:litReceptionDirectionsHeading %>"></asp:Literal></h3>
            <ul>
                <li>Head south on <b>Cartwright Ave</b> toward <b>Delano St.</b></li>
                <li>Turn <b>left</b> at <b>Oxnard St.</b></li>
                <li>Turn <b>right</b> at <b>Cahuenga Blvd</b></li>
                <li>Turn <b>left</b> to merge onto the <b>CA-134 East</b> toward Pasadena</li>
                <li>Take exit <b>7B</b> toward <b>Brand Blvd/Central Ave</b></li>
                <li>Turn <b>left</b> at <b>N Central Ave</b></li>
                <li>The destination will be on the right</li>
            </ul>
            <p>
                <a href="http://maps.google.com/maps?f=q&source=s_q&hl=en&geocode=&q=900+N.+Central+Ave.,+Glendale,+CA+91203&sll=34.158722,-118.257968&sspn=0.072302,0.104628&ie=UTF8&hq=&hnear=900+N+Central+Ave,+Glendale,+California+91203&ll=34.157086,-118.259797&spn=0.009038,0.013078&z=17">QQ Buffet & Grill</a><br />
                900 N. Central Ave.<br />
                Glendale, CA 91203
            </p>
        </asp:View>
        <asp:View runat="server" ID="view2">
            <h2><asp:Literal runat="server" ID="litInfoHeaderNew" Text="<%$ Resources:litInformationHeader %>"></asp:Literal></h2>        
            <p>
                <asp:Literal runat="server" ID="litMemorialThankYou" Text="<%$ Resources:litMemorialThankYou %>"></asp:Literal>
            </p>
        </asp:View>
    </asp:MultiView>
    </div>    
    <div style="width:39%; float:right; border-left: 1px solid #AAD3DD; margin-top: 8px;">       
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" Visible="false">   
            <asp:View runat="server" ID="registrationView1">
                <h3>Registration</h3>
                <hr />
                <p>
                    Use this form to state your intention to come to the family event.  This will help us gain an
                    accurate head count for the venue.
                </p>
                <p>
                    Only one reservation per family is required.  Please do not use this form multiple 
                    times.  If you need to change your reservation please e-mail me using the 
                    contact form on the <a href="About.aspx">About</a> page.
                </p>                                
                <asp:BulletedList runat="server" ID="reservationError" Visible="false" ForeColor="Red" CssClass="reservationError"></asp:BulletedList>
                <table style="padding-left: 25px;">
                    <tr>
                        <td>Name<asp:RequiredFieldValidator ValidationGroup="m" runat="server" ControlToValidate="reservationName" ID="rfvName">&nbsp;* Required</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox runat="server" ID="reservationName"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            E-mail
                            <asp:RequiredFieldValidator ValidationGroup="m" ControlToValidate="reservationEmail" 
                                runat="server" ID="rfvEmail">&nbsp;* Required
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="reservationEmail"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone#
                            <asp:RequiredFieldValidator ValidationGroup="m" runat="server" 
                                ControlToValidate="reservationPhoneNumber" ID="rfvPhone">&nbsp;* Required
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:TextBox runat="server" ID="reservationPhoneNumber"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Number of people (including yourself)</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList runat="server" ID="reservationPartySize"></asp:DropDownList>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CompareValidator runat="server" ID="validateCount" ControlToValidate="reservationPartySize"
                                Type="Integer" Operator="GreaterThan" Text="Must be greater than 0" ValueToCompare="0" ValidationGroup="m">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Your relationship to Nelly</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox runat="server" ID="reservationRelationship"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right"><br /><asp:Button runat="server" ID="reservationSubmit" Text="Submit" ValidationGroup="m" OnClick="ReservationSubmit_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            <asp:View runat="server" ID="registrationView2">
                <h3>Thank you</h3>
                <p>Your registration request was received successfully.</p>
                <p>
                    Please send me a message if you have any questions about your registration.
                    You may reach me by clicking the contact link in the <a href="About.aspx">About</a> page.
                </p>
            </asp:View>
            <asp:View runat="server" ID="registrationView3">
                <h3>Thank you</h3>
                <p>You have already registered, <asp:Label runat="server" ID="cookieName"></asp:Label>.</p>
                <p>
                    If you believe you have received this message as an error or would like to modify
                    your reservation arrangements, you may reach me by clicking the contact link 
                    in the <a href="About.aspx">About</a> page.
                </p>
            
            </asp:View>
            <asp:View runat="server" ID="registrationView4">
                <h3>Thank you</h3>
                <p>Registration is now closed.  Please feel free to send us a message if you have
                    any questions.  You may do so by visiting the <a href="About.aspx">About</a> page. 
                    Thank you for all who attended our celebration.</p>
            </asp:View>
        </asp:MultiView>
    </div>    
    <h2>Memorial Video</h2>
    <p>
        This is the video taken at Nelly's memorial service shot by Ernie Cheng.  Thank you to everyone who contributed and 
        made this site possible. If you have any questions or comments, please send me a message by e-mailing 
        me in the <a href="About.aspx">About</a> page.
    </p>
    <h3>Problems viewing the video?</h3>
    <p>    
        If you are having problems viewing the video, you may not have <b>Adobe Flash</b> installed (which you can <a href="http://www.adobe.com/go/getflashplayer">get for free right here</a>).
    </p>
    <br />
    <table style="width: 100%; margin: 0 auto">
        <tr><td align="center"><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="545" height="411" id="viddler_e7145d6f"><param name="movie" value="http://www.viddler.com/player/e7145d6f/" /><param name="allowScriptAccess" value="always" /><param name="allowFullScreen" value="true" /><embed src="http://www.viddler.com/player/e7145d6f/" width="545" height="411" type="application/x-shockwave-flash" allowScriptAccess="always" allowFullScreen="true" name="viddler_e7145d6f"></embed></object></td></tr>
        <tr>
            <td align="center">        
                <script type="text/javascript">
                    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
                    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
                </script>
                <script type="text/javascript">
                    try {
                        var pageTracker = _gat._getTracker("UA-10860292-1");
                        pageTracker._trackPageview();
                    } catch (err) { }
                </script>
            </td>
        </tr>
    </table>    

    <div style="width: 100%; margin: 0 auto;">
        <asp:MultiView runat="server" Visible="false">
            <asp:View runat="server" ID="View12">
                <h3>
                    <asp:Literal runat="server" ID="litPhotoHeading" Text="<%$ Resources:litPhotos %>"></asp:Literal>
                </h3>
                <asp:DataList ID="dlistPhotos" runat="server" BorderWidth="1" BorderColor="#000000" 
                        GridLines="Both" RepeatColumns="2" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <a href="PhotoView.aspx?m=<%# Eval("photoId") %>">
                            
                        </a>
                    </ItemTemplate>
                </asp:DataList>        
            </asp:View>
        </asp:MultiView>            
    </div>
        
</asp:Content>

