<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Web_Forms.UserControls.Navbar" %>

<link href="<%= ResolveUrl("~/Content/_navbar.css") %>" rel="stylesheet" type="text/css" />

<div class="menu-bar">
    <a href="/" class="logo-link">
        <img src="<%= ResolveUrl("~/Media/logo_no_bg.png") %>" alt="Logo" class="logo" />
    </a>
    <%-- can add if needed --%>
    <%--<a href="/" class="menu-item <%= IsActivePage("Default") %>">Home</a>--%>
    <a href="EmployeesList" class="menu-item <%= IsActivePage("EmployeesList") %>">Employee List</a>
    <a href="EmployeesForm" class="menu-item <%= IsActivePage("EmployeesForm") %>">Add Employee Form</a>
</div>
