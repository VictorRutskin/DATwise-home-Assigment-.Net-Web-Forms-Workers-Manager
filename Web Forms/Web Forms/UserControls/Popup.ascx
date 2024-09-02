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


<script type="text/javascript">
    $(document).ready(function () {
        // Function to show and hide the popup
        function showPopup() {
            $('#messagePopup').modal('show');
            setTimeout(function () {
                $('#messagePopup').modal('hide');
            }, 3000); // Hide after 3 seconds
        }

        // Attach the function to the window object so it can be called from server-side code
        window.showPopup = showPopup;
    });
</script>