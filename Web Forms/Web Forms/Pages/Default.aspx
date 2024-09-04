<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="~/Pages/Default.aspx.cs" Inherits="Web_Forms.Pages._Default" %>
<%@ Register Src="~/UserControls/PaperButton.ascx" TagPrefix="uc" TagName="PaperButton" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Home Page</title>
    <link href="<%= ResolveUrl("~/Content/_default.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Content/_Site.css") %>" rel="stylesheet" type="text/css" />

</head>
<body>

    <form id="form1" runat="server">

        <img src="<%= ResolveUrl("~/Media/logo_full_no_bg.png") %>" alt="Logo" class="logo" />

    <div class="button-position">
            <input type="checkbox" name="send" id="send" class="hide-checkbox"/>
            <label for="send" class="send btn btn-edit large-btn">
                        START NOW!
            </label>
    </div>


        <div class="background-video">
            <video autoplay muted loop>
                <source src="/Media/home_background.mp4" type="video/mp4" />
                Your browser does not support the video tag.
            </video>
        </div>
    </form>
</body>
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        var checkbox = document.getElementById('send');

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                window.location.href = '/EmployeesList';
            }
        });
    });
</script>
</html>
