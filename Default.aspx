<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Nelly Pacis Brandt / Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left; float: left; width: 40%">
        <h2><asp:Literal runat="server" ID="litPageHeading" Text="<%$ Resources:litPageHeading %>"></asp:Literal></h2>    
    </div>
    <div style="text-align: right; float:right; width: 50%; margin: 12px 42px 0 0;">        
        <asp:Literal runat="server" ID="litLanguageTag" Text="Language / Linguahe"></asp:Literal>
        <br />
        <asp:DropDownList runat="server" ID="ddlLanguages" OnSelectedIndexChanged="ddlLanguages_Changed" AutoPostBack="true"></asp:DropDownList>        
    </div>
    <div style="clear: both;"></div>
    <div style="float: left; width: 65%;">
        <p>
            <asp:Literal runat="server" ID="litPageBody" Text="<%$ Resources:litPageBody %>"></asp:Literal>
        </p>
        <h3><asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:litMemorialHeading %>"></asp:Literal></h3>
        <p>
            <asp:Literal runat="server" ID="Literal2" Text="<%$ Resources:litMemorialBody %>"></asp:Literal>
        </p>
        <asp:MultiView runat="server" ID="mvOldView">
            <asp:View runat="server" ID="new">            

            </asp:View>
            <asp:View runat="server" ID="oldView">
                <h3><asp:Literal runat="server" ID="litReceptionHeading" Text="<%$ Resources:litReceptionHeading %>"></asp:Literal></h3>                
                <p>
                    <asp:Literal runat="server" ID="litReceptionBody" Text="<%$ Resources:litReceptionBody %>"></asp:Literal>
                </p>
                <h3><asp:Literal runat="server" ID="litMemorialHeading" Text="<%$ Resources:litMemorialHeading %>"></asp:Literal></h3>
                <p>
                    <asp:Literal runat="server" ID="litMemorialBody" Text="<%$ Resources:litMemorialBody %>"></asp:Literal>
                </p>
            </asp:View>        
        </asp:MultiView>
        <br /><br />
    </div>
    <div style="float:right; width: 34%; text-align: right;">                
        <a href="images/image001.jpg">
            <img src="images/image001.jpg" width="184px" height="310px" alt="Nelly Brandt" style="margin-right: 25px;"/>
        </a>
    </div>
    <div style="clear: both;"></div>
</asp:Content>

