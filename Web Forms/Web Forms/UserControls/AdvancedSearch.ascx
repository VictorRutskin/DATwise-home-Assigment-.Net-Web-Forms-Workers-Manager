<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedSearch.ascx.cs" Inherits="Web_Forms.UserControls.AdvancedSearch" %>

<link href="<%= ResolveUrl("~/Content/_advanced-search.css") %>" rel="stylesheet" type="text/css" />

<asp:Panel ID="pnlAdvancedSearch" runat="server" CssClass="advanced-search-panel">
    <div class="search-container">
        <div class="search-field">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="form-label" />
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="search-field">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="form-label" />
            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="search-field">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="form-label" />
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="search-field">
            <asp:Label ID="lblPhone" runat="server" Text="Phone:" CssClass="form-label" />
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="search-field">
            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="form-label" />
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
        <div class="search-field">
            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="form-label" />
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>
        <div class="buttons-container">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary search-button" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn btn-secondary search-button" OnClick="btnClear_Click" />
        </div>
    </div>
    <asp:Label ID="lblSearchError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
</asp:Panel>
