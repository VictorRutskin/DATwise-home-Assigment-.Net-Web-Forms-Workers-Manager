<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaperButton.ascx.cs" Inherits="Web_Forms.UserControls.PaperButton" %>

<link href="<%= ResolveUrl("~/Content/_paper-button.css") %>" rel="stylesheet" type="text/css" />

<input type="checkbox" name="send" id="send" />
<label for="send" class="send">
    <div class="rotate">
        <div class="move">
            <div class="part left"></div>
            <div class="part right"></div>
        </div>
    </div>
</label>

<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        var checkbox = document.getElementById('send');

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                setTimeout(function () {
                    window.location.href = '/EmployeesList';
                }, 0); // Wait for 2 seconds (2000 milliseconds)
            }
        });
    });
</script>
