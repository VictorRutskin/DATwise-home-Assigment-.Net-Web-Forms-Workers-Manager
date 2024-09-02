<%@ Page Title="EmployeesForm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesForm.aspx.cs" Inherits="Web_Forms.Pages.EmployeesForm" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Employee Form</h1>

      <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName" />
<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
<asp:Label ID="lblFirstNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
<br />

<asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName" />
<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
<asp:Label ID="lblLastNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
<br />

<asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
<asp:Label ID="lblEmailError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
<br />

<asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone" />
<asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
<asp:Label ID="lblPhoneError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
<br />

<asp:Label ID="lblHireDate" runat="server" Text="Hire Date:" AssociatedControlID="txtHireDate" />
<asp:TextBox ID="txtHireDate" runat="server"></asp:TextBox>
<asp:Label ID="lblHireDateError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
<br />

<asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />



</asp:Content>
