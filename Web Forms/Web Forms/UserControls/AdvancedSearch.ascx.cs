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
            var searchTerms = new Dictionary<string, string>
            {
                { "FirstName", txtFirstName.Text.Trim() },
                { "LastName", txtLastName.Text.Trim() },
                { "Email", txtEmail.Text.Trim() },
                { "Phone", txtPhone.Text.Trim() }
            };

            SearchClicked?.Invoke(this, searchTerms);
        }
    }
}
