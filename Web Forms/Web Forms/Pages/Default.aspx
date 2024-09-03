<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="~/Pages/Default.aspx.cs" Inherits="Web_Forms.Pages._Default" %>
<%@ Register Src="~/UserControls/PaperButton.ascx" TagPrefix="uc" TagName="PaperButton" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Home Page</title>
    <link href="<%= ResolveUrl("~/Content/_default.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Content/_paper-button.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <%-- Paper Button --%>
    <uc:PaperButton ID="PaperButton" runat="server" />

    <div class="background-video">
        <video autoplay muted loop>
            <source src="/Media/home_background.mp4" type="video/mp4" />
            Your browser does not support the video tag.
        </video>
    </div>
</body>
</html>
