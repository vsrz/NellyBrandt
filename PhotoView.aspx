<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoView.aspx.cs" Inherits="PhotoView" Title="Nelly Pacis Brandt / Photo Library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server" ID="PhotoViewScriptManager"></asp:ScriptManager>
    <div style="width: 670px;" id="jump">
        <table cellpadding="0" cellspacing="0" style="width: 670px;">
            <tr>
                <td align="center">
                    <asp:HiddenField runat="server" ID="imgCurrentGuid" Value="" />                    
                    <p style="margin: 0 auto;">
                        <asp:Label runat="server" ID="lblFirstImage" Text="This is the first photo" Visible="false"></asp:Label>
                        <asp:HyperLink runat="server" ID="lnkPreviousImage" Text="<< Previous Photo"></asp:HyperLink>
                        &nbsp;|&nbsp;
                        <asp:HyperLink runat="server" ID="lnkRandomImage" Text="Random Photo"></asp:HyperLink>
                        &nbsp;|&nbsp;
                        <asp:HyperLink runat="server" ID="lnkNextImage" Text="Next Photo >>"></asp:HyperLink>
                        <asp:Label runat="server" ID="lblLastImage" Text="This is the last photo" Visible="false"></asp:Label>
                    </p>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:HyperLink runat="server" ID="imgHyperlink">
                        <asp:Image runat="server" ID="imgCurrent" ImageUrl="" />
                    </asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="center">
                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <asp:Label runat="server" ID="lblImageDate"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="imgCaption" Text=""></asp:Label>                    
                </td>
            </tr>            
        </table>
        <br />
        <asp:UpdatePanel runat="server" ID="udpNewPost">
            <ContentTemplate>
                <asp:Button runat="server" ID="btnUnhideAddComment" Text="Add Comment" OnClick="btnUnhideAddComment_Click" />
                <div id="divAddComment" runat="server" visible="false">
                    <table cellpadding="4" cellspacing="4">
                        <tr>
                            <th colspan="2">
                                Add your comment
                            </th>
                        </tr>
                        <tr>
                            <td>Your name</td>
                            <td><asp:TextBox runat="server" ID="txtNewComment_username" Columns="40"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>E-Mail</td>
                            <td><asp:TextBox runat="server" ID="txtNewComment_email" Columns="40"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Comment</td>
                            <td><asp:TextBox ID="txtNewComment_Text" runat="server" Columns="60" Rows="12" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button runat="server" ID="btnAddComment_Submit" Text="Submit" OnClick="btnAddComment_Submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUnhideAddComment" />
                <asp:PostBackTrigger ControlID="btnAddComment_Submit" />
            
            </Triggers>
        </asp:UpdatePanel>
        <br /><br />
        <asp:UpdatePanel runat="server" ID="udpCommentsView">
            <ContentTemplate>
                <div style="width: 80%; margin: 0 auto;">
                    <asp:Repeater runat="server" ID="rpCommentsView">
                        <ItemTemplate>
                            <hr style="color: #bb0000;" />
                            <div style="float:left; width: 50%; text-align: left; padding-left: 24px;">
                                <%# "<b>Posted By " + Eval("commentAuthor") + "</b>" %>
                            </div>
                            <div style="float:right; width: 39%; text-align: right;">
                                <%# "<b>On " + Eval("commentTimestamp") + "</b>" %>
                            </div>
                            <div style="clear: both;"></div>
                            <br />
                            <div style="width: 90%; margin: 0 auto;">
                                <%# Eval("commentText") %>
                            </div>
                        </ItemTemplate>                        
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddComment_Submit" />
            </Triggers>
        </asp:UpdatePanel>
        <br /><br />
    </div>
</asp:Content>
