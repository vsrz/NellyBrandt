<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GuestBook.aspx.cs" Inherits="GuestBook" Title="Nelly Pacis Brandt / Guestbook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <a href="images/ffam.jpg"><img src="images/ffam.jpg" alt="Friends and Family" width="655px" height="225px" /></a>    
    <asp:MultiView runat="server" ID="MultiView1" ActiveViewIndex="0">
        <asp:View runat="server" ID="defaultView">
            <h2><asp:Literal runat="server" ID="litGuestbookTitle" Text="<%$ Resources:litGuestbookTitle %>"></asp:Literal></h2>
            <p>
                <asp:Literal runat="server" ID="litGuestbookBody" Text="<%$ Resources:litGuestbookBody %>"></asp:Literal>
            </p>
            <p>
                <a href="GuestBook.aspx?p=1"><asp:Literal runat="server" ID="lnkSubmitEntry" Text="<%$ Resources:lnkSubmitEntry %>"></asp:Literal></a>
            </p>
            <asp:Repeater runat="server" ID="rpGuestbookViewer">
                <ItemTemplate>
                    <hr />
                    <div style="text-align: left; float: left; width: 50%; padding-left: 25px;">
                        <h4><%# Eval("guestbookAuthor") %></h4>
                    </div>
                    <div style="text-align: right; float: right; width: 42%">
                        <h4><%# ((DateTime)Eval("guestbookTimestamp")).ToLongDateString() %></h4>
                    </div>
                    <div style="clear:both;"></div>
                    <p>
                        <%# Eval("guestbookText") %>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </asp:View>
        <asp:View runat="server" ID="contributeView">
            <h3>Contribute</h3>
            <p>
                When contributing to the guestbook, please be sure to fill out all
                fields.  Your personal information is kept private and will not be 
                given to anyone.
            </p>
            <fieldset style="border: none">                      
                <table cellpadding="4" cellspacing="4" style="width: 670px; margin: 0 auto;">
                    <tr>
                        <td>Your name</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAuthor" Width="220"></asp:TextBox>                        
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="m" ControlToValidate="txtAuthor" ErrorMessage="Name is required" Display="Dynamic">Name is required</asp:RequiredFieldValidator>
                        </td>                  
                    </tr>
                    <tr>
                        <td>E-mail address</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail" Width="220"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="m" ControlToValidate="txtEmail" ErrorMessage="E-mail is required" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" ControlToValidate="txtEmail" runat="server" ErrorMessage="Invalid email address">
                                Invalid e-mail
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Relation to Nelly</td>
                        <td colspan="2">                        
                            <asp:TextBox runat="server" ID="txtRelationship" Width="220"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>Message</td>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtText" TextMode="MultiLine" Columns="60" Rows="12">
                            </asp:TextBox>                        
                        </td>              
                    </tr>
                    <tr>
                        <td colspan="3">                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <p>
                                <a href="GuestBook.aspx">Return to Guestbook</a>
                            </p>
                        </td>
                        <td align="right">
                            <asp:Button runat="server" ValidationGroup="m" ID="btnGuestbookSubmit" OnClick="btnGuestbookSubmit_Click" Text="Submit" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:View>
        <asp:View runat="server" ID="successfulPost">
            <h3>Contribution Successful</h3>
            <p>
                Your contribution to the guestbook was successful.  Please click the link below
                to view your entry.
            </p>
            <p>
                <a href="GuestBook.aspx">Guestbook</a>
            </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>

