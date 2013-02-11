<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" Title="Nelly Pacis Brandt / About the Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Literal runat="server" ID="litAboutHeading" Text="<%$ Resources:litAboutHeading %>"></asp:Literal></h2>
    <p>
        <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:litAboutBody %>"></asp:Literal>
    </p>
    <p>
        <a href="mailto:admin@nellybrandt.com"><asp:Literal runat="server" ID="Literal2" Text="<%$ Resources:litContactMe %>"></asp:Literal></a>
    </p>
</asp:Content>

