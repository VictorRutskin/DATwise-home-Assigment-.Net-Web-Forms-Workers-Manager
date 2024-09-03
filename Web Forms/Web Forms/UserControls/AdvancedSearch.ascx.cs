using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Web_Forms.UserControls
{
    public partial class AdvancedSearch : UserControl
    {
        public event EventHandler<Dictionary<string, string>> SearchClicked;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Cutting search terms for filter
            Dictionary<string, string> searchTerms = new Dictionary<string, string>
            {
                { "FirstName", txtFirstName.Text.Trim() },
                { "LastName", txtLastName.Text.Trim() },
                { "Email", txtEmail.Text.Trim() },
                { "Phone", txtPhone.Text.Trim() },
                { "StartDate", txtStartDate.Text.Trim() },
                { "EndDate", txtEndDate.Text.Trim() }
            };

            SearchClicked?.Invoke(this, searchTerms);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all search fields
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;

            SearchClicked?.Invoke(this, new Dictionary<string, string>());
        }

    }
}
