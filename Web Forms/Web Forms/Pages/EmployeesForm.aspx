<%@ Page Title="EmployeesForm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesForm.aspx.cs" Inherits="Web_Forms.Pages.EmployeesForm" Async="true" %>

<%@ Register TagPrefix="uc" TagName="Popup" Src="~/UserControls/Popup.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- popup --%>
    <uc:Popup ID="PopupControl" runat="server" />

    <div class="container">

        <%-- title --%>
        <h1>Employee Form</h1>

        <%-- all inputs --%>
        <div class="form-group">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName" />
            <asp:TextBox ID="txtFirstName" runat="server" Placeholder="* Only English letters are allowed." />
            <asp:Label ID="lblFirstNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName" />
            <asp:TextBox ID="txtLastName" runat="server" Placeholder="* English letters are allowed." />
            <asp:Label ID="lblLastNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="* example@domain.com" />
            <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone" />
            <asp:TextBox ID="txtPhone" runat="server" Placeholder="* 05XXXXXXXX (Israeli phone number)" />
            <asp:Label ID="lblPhoneError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
        </div>
       <div class="form-group">
            <asp:Label ID="lblHireDate" runat="server" Text="Hire Date:" AssociatedControlID="txtHireDate" />
            <asp:TextBox ID="txtHireDate" runat="server" TextMode="Date" />
            <span class="input-hint">* The date must be between January 1, 1900, and today.</span>
            <asp:Label ID="lblHireDateError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
        </div>

        <%-- button --%>
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" />

    </div>
</asp:Content>
