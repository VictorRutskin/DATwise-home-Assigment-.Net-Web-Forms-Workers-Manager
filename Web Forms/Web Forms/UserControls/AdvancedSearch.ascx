<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedSearch.ascx.cs" Inherits="Web_Forms.UserControls.AdvancedSearch" %>

<link href="<%= ResolveUrl("~/Content/_advanced-search.css") %>" rel="stylesheet" type="text/css" />

<asp:Panel ID="pnlAdvancedSearch" runat="server" CssClass="advanced-search-panel">
    <table>
        <tr>
            <td><asp:Label ID="lblFirstName" runat="server" Text="First Name:" /></td>
            <td><asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox></td>
            <td><asp:Label ID="lblLastName" runat="server" Text="Last Name:" /></td>
            <td><asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox></td>
            <td><asp:Label ID="lblEmail" runat="server" Text="Email:" /></td>
            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox></td>
            <td><asp:Label ID="lblPhone" runat="server" Text="Phone:" /></td>
            <td><asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox></td>
            <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
        </tr>
    </table>
    <asp:Label ID="lblSearchError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
</asp:Panel>
