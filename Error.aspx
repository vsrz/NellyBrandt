<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        Web Error
    </h3>
    <p>
        There was a web error reaching the website.  Please try again later.  Use the button
        below to return to the previous page.
    </p>
    <input style="margin-left: 5%; padding-left: 15px; padding-right: 15px;" type="button" onClick="history.go(-1)" value="Back" />
</asp:Content>

