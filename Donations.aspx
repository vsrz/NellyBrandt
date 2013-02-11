<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Donations.aspx.cs" Inherits="Donations" Title="Nelly Pacis Brandt / Donations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Nelly's Memorial Fund</h2>
    <p>
        Each donation will go directly to Nelly's family whom have paid their time
        and their own money towards the the memorial and the costs of running this
        website.    
    </p>
    <p>
        Donations can be made via credit card or directly from your bank account.
        They are handled through Paypal.com, a safe and secure way to pay.  If you
        would like to make a donation, please click the donate link below.  The link
        will open in a new window.
    </p>
    <p>
        Thank you for your consideration.
    </p>
    <center>
        <br /><br />
        <asp:ImageButton OnClick="ForwardToPaypal" OnClientClick="aspnetForm.target = '_blank';" runat="server" ID="paypalimage" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_donateCC_LG.gif" AlternateText="PayPal - The safer, easier way to pay online!">
        </asp:ImageButton>        

    </center>
</asp:Content>

