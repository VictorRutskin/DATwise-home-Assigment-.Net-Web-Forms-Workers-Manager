<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Popup.ascx.cs" Inherits="Web_Forms.UserControls.Popup" %>

<div id="messagePopup" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog" role="document">
        <div id="popupContent" runat="server" class="modal-content">
            <div class="modal-header">
                <h5 id="popupTitle" runat="server" class="modal-title">Message</h5>
            </div>
            <div class="modal-body">
                <p id="popupMessage" runat="server" class="message-content">Operation completed successfully!</p>
            </div>
        </div>
    </div>
</div>

<%-- script to show and hide the popup, dissapears after 3 seconds --%>
<script type="text/javascript">
    $(document).ready(function () {
        function showPopup() {
            $('#messagePopup').modal('show');
            setTimeout(function () {
                $('#messagePopup').modal('hide');
            }, 3000); 
        }

        window.showPopup = showPopup;
    });
</script>