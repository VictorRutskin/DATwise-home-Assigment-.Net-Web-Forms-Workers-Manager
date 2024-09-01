<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Web_Forms.UserControls.Navbar" %>

<link href="<%= ResolveUrl("~/Content/_navbar.css") %>" rel="stylesheet" type="text/css" />

<div class="menu-bar">
    <a href="/" class="menu-item">Home</a>
    <a href="EmployeesList" class="menu-item">Employee List</a>
    <a href="EmployeesForm" class="menu-item">Employee Form</a>
    <a href="About" class="menu-item">About</a>
    <a href="Contact" class="menu-item">Contact</a>
</div>
