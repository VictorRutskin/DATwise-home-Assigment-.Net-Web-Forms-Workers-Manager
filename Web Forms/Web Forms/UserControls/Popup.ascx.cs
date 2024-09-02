using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL_Data_Access_Layer.Models;

namespace Web_Forms.UserControls
{
    public partial class Popup : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Show(PopupType type, string title, string message)
        {
            // Set the title and message
            popupTitle.InnerText = title;
            popupMessage.InnerText = message;

            // Optional: Apply different styles based on the type
            popupContent.Attributes["class"] = $"modal-content {GetPopupClass(type)}";

            // Use JavaScript to show the popup and then hide it after a delay
            string script = @"
                $(document).ready(function() {
                    $('#messagePopup').modal('show');
                    setTimeout(function() {
                        $('#messagePopup').modal('hide');
                    }, 3000); // Hide after 3 seconds
                });
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
        }

        private string GetPopupClass(PopupType type)
        {
            // Return CSS class based on popup type if needed
            switch (type)
            {
                case PopupType.Error:
                    return "bg-danger";
                case PopupType.Warning:
                    return "bg-warning";
                case PopupType.Success:
                    return "bg-success";
                case PopupType.Information:
                    return "bg-info";
                default:
                    return "";
            }
        }
    }



}
