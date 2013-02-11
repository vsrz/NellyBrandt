<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" Title="Nelly Pacis Brandt / Administrator"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Administrator Menu</h3>
    <table cellpadding="5" cellspacing="5" style="border: 1px solid black;" runat="server" id="htmlAdminTable">
        <thead style="background-color: #6666ff; color: #ffffff; font-weight: bold;">
            <tr>
                <td style="width: 25%; background-color: #6666ff; color: #ffffff; font-weight: bold;">Function</td>
                <td style="background-color: #6666ff; color: #ffffff; font-weight: bold;">Description</td>
            </tr>
        </thead>        
        <tr>
            <td style="background-color: #ffffcc;">
                <a href="Admin.aspx?p=1">View Audit Table</a>
            </td>
            <td style="background-color: #ffffcc;">
                This will show the data gathered in the audit table.  Whenever a user
                clicks on a control, the audit table grabs any submit data entered and
                posts it here.  For example, if the user decides to upload a photo,
                all data will be shown in this table whether the photo was uploaded
                successfully or not.
            </td>
        </tr>
        <tr>
            <td>
                <a href="Admin.aspx?p=2">Memorial registration</a>
            </td>
            <td>
                Show everyone who has signed up for the memorial.
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffcc;">
                <a href="http://lesuth.lunarpages.com/mylittleadmin4/" target="_blank">Table admin</a>
            </td>
            <td style="background-color: #ffffcc;">
                To edit any values in the database, you will need to supply further credentials
                on this page.
            </td>
        </tr>
        <tr>
            <td>
                <a href="Admin.aspx?p=4">Create an admin</a>
            </td>
            <td>
                Use this link to create a new admin account for the website.
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Visible="false" Width="90%">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:CreateUserWizard runat="server" Visible="False" ID="NewUser" 
    BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="1px" 
    Font-Names="Verdana" Font-Size="0.8em" >
        <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" 
            VerticalAlign="Top" />
        <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
        <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#284775" />
        <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#284775" />
        <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" 
            Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
        <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <StepStyle BorderWidth="0px" />
        <WizardSteps>
<asp:CreateUserWizardStep runat="server"></asp:CreateUserWizardStep>
<asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>
</WizardSteps>
</asp:CreateUserWizard>
    <br />
    <asp:HyperLink runat="server" ID="lnkMainMenu" Visible="false" Text="Return to main menu" NavigateUrl="Admin.aspx?p=0"></asp:HyperLink>
    <br />


</asp:Content>

