using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_Forms.UserControls;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // The GridView will be bound automatically by the SqlDataSource.
            }

            // Register the event handler for the AdvancedSearch control
            AdvancedSearchControl.SearchClicked += OnSearchClicked;
        }

        protected void OnSearchClicked(object sender, Dictionary<string, string> searchTerms)
        {
            // Apply filtering on the SqlDataSource if needed
            string filterExpression = BuildFilterExpression(searchTerms);
            SqlDataSource1.FilterExpression = filterExpression;
        }

        private string BuildFilterExpression(Dictionary<string, string> searchTerms)
        {
            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(searchTerms["FirstName"]))
            {
                conditions.Add($"FirstName LIKE '%{searchTerms["FirstName"]}%'");
            }
            if (!string.IsNullOrEmpty(searchTerms["LastName"]))
            {
                conditions.Add($"LastName LIKE '%{searchTerms["LastName"]}%'");
            }
            if (!string.IsNullOrEmpty(searchTerms["Email"]))
            {
                conditions.Add($"Email LIKE '%{searchTerms["Email"]}%'");
            }
            if (!string.IsNullOrEmpty(searchTerms["Phone"]))
            {
                conditions.Add($"Phone LIKE '%{searchTerms["Phone"]}%'");
            }

            return string.Join(" AND ", conditions);
        }

        // Event handlers for GridView
        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            gvEmployees.DataBind(); // Refresh the GridView
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);

            TextBox txtFirstName = row.FindControl("txtFirstName") as TextBox;
            TextBox txtLastName = row.FindControl("txtLastName") as TextBox;
            TextBox txtEmail = row.FindControl("txtEmail") as TextBox;
            TextBox txtPhone = row.FindControl("txtPhone") as TextBox;
            TextBox txtHireDate = row.FindControl("txtHireDate") as TextBox;

            if (txtFirstName != null && txtLastName != null && txtEmail != null && txtPhone != null && txtHireDate != null)
            {
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                DateTime hireDate = DateTime.Parse(txtHireDate.Text);

                // Update employee logic using SqlDataSource
                SqlDataSource1.UpdateParameters["FirstName"].DefaultValue = firstName;
                SqlDataSource1.UpdateParameters["LastName"].DefaultValue = lastName;
                SqlDataSource1.UpdateParameters["Email"].DefaultValue = email;
                SqlDataSource1.UpdateParameters["Phone"].DefaultValue = phone;
                SqlDataSource1.UpdateParameters["HireDate"].DefaultValue = hireDate.ToString();
                SqlDataSource1.UpdateParameters["EmployeeID"].DefaultValue = employeeId.ToString();

                SqlDataSource1.Update();
                gvEmployees.EditIndex = -1;
                gvEmployees.DataBind(); // Refresh the GridView
            }
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);

            // Delete employee logic using SqlDataSource
            SqlDataSource1.DeleteParameters["EmployeeID"].DefaultValue = employeeId.ToString();
            SqlDataSource1.Delete();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            gvEmployees.DataBind(); // Refresh the GridView
        }
    }
}
