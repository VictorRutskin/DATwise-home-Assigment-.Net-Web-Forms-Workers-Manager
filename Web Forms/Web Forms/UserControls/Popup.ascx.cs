using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DAL.Models;

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

            // Set Design after getting type
            popupContent.Attributes["class"] = $"modal-content {GetPopupClass(type)}";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", JScriptHandler.Activate_ShowPopup(), true);

        }


        private string GetPopupClass(PopupType type)
        {
            // Return CSS design class based on popup type 
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
