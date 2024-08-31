<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Default.aspx.cs" Inherits="Web_Forms.Pages._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/_default.css") %>" rel="stylesheet" type="text/css" />

    <div class="background-video">
        <video autoplay muted loop>
            <source src="/Media/office.mp4" type="video/mp4" />
            Your browser does not support the video tag.
        </video>
    </div>
</asp:Content>
