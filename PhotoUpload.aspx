<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoUpload.aspx.cs" Inherits="PhotoUpload" Title="Nelly Pacis Brandt / Photo Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView runat="server" ID="MultiView1" ActiveViewIndex="0">
        <asp:View runat="server" ID="viewUpload">
            <h2><asp:Literal runat="server" ID="litUploadTitle" Text="<%$ Resources:litUploadTitle %>"></asp:Literal></h2>
            <p>
                <asp:Literal runat="server" ID="litUploadBody" Text="<%$ Resources:litUploadBody %>"></asp:Literal>
            </p>
            <table cellpadding="4" cellspacing="4" style="margin: 0 auto;">
                <tr>
                    <td><asp:Literal runat="server" ID="litYourName" Text="<%$ Resources:litYourName %>"></asp:Literal></td>
                    <td><asp:TextBox runat="server" ID="upload_Username" Width="260"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="m" ControlToValidate="upload_Username" ErrorMessage="Name is required" Display="Dynamic">
                            * Required
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" ID="litYourEmail" Text="<%$ Resources:litYourEmail %>"></asp:Literal></td>
                    <td><asp:TextBox runat="server" ID="upload_Email" Width="260"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="m" ControlToValidate="upload_Email" ErrorMessage="* Required" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" ControlToValidate="upload_Email" runat="server" ErrorMessage="Invalid email address">
                            * Required
                        </asp:RegularExpressionValidator>                    
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" ID="litPhotoDate" Text="<%$ Resources:litPhotoDate %>"></asp:Literal></td>
                    <td><asp:TextBox runat="server" ID="upload_PhotoDate" Width="260"></asp:TextBox></td>
                    <td>
                        Format: mm/dd/yyyy                    
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" ID="litCaption" Text="<%$ Resources:litCaption %>"></asp:Literal></td>
                    <td><asp:TextBox runat="server" ID="upload_Caption" Width="260"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" ID="litPhotoUpload" Text="<%$ Resources:litPhotoUpload %>"></asp:Literal></td>
                    <td><asp:FileUpload runat="server" ID="upload_File" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <fieldset style="border: solid 2px #888888">
                            <legend style="font-weight: bold; font-size: small;">Photo Type</legend>
                            <div style="width: 80%; margin: 0 auto;">
                                <asp:RadioButton runat="server" Enabled="true" ID="rbMemorial" Text="Memorial" Checked="True" GroupName="phototype" />
                                <br />
                                <asp:RadioButton runat="server" Text="Non-memorial photo" GroupName="phototype" />
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <hr /><br /><asp:Button runat="server" ID="upload_Submit" 
                            OnClick="PhotoUpload_Submit_Click" Text="Submit" ValidationGroup="m" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="viewSuccessfulUpload">
            <h2>Upload Successful</h2>
            <p>
                Your upload was successful.  Please use the link below to view your image.
            </p>
            <p>
                <asp:HyperLink runat="server" ID="lnkImageUploadLink" Text="Your image"></asp:HyperLink>
            </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
