<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Web_Forms.UserControls.Navbar" %>

<link href="<%= ResolveUrl("~/Content/navbar.css") %>" rel="stylesheet" type="text/css" />

<div class="navbar">
    <ul>
        <li><a href="/"> Home </a></li>
        <li><a href="EmployeesList"> Employee List </a></li>
        <li><a href="EmployeesForm"> Employee Form </a></li>
        <li><a href="About"> About </a></li>
        <li><a href="Contact"> Contact </a></li>
    </ul>
</div>
