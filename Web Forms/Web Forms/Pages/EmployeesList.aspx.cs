using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_Forms.UserControls;
using BL;
using Common.CustomExceptions;
using Common.ConfigurationHandler;
using DAL_Data_Access_Layer.Models;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private ILoggerService _loggerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize LoggerService with the path from ConfigurationHandler
            _loggerService = new LoggerService(Server.MapPath(ConfigurationHandler.GetLogFilePath()));

            if (!IsPostBack)
            {
                // Bind the GridView data initially
                BindEmployeeGrid();
            }

            // Register the event handler for the AdvancedSearch control
            AdvancedSearchControl.SearchClicked += OnSearchClicked;
        }

        private void BindEmployeeGrid()
        {
            try
            {
                // The GridView will be bound automatically by the SqlDataSource
                gvEmployees.DataBind();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new ModelStateException("Error while binding employee grid.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee list.");
            }
        }

        protected void OnSearchClicked(object sender, Dictionary<string, string> searchTerms)
        {
            try
            {
                // Apply filtering on the SqlDataSource if needed
                string filterExpression = BuildFilterExpression(searchTerms);
                SqlDataSource1.FilterExpression = filterExpression;
                gvEmployees.DataBind();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new ModelStateException("Error while applying search filter.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while applying the search filter.");
            }
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

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvEmployees.EditIndex = e.NewEditIndex;
                BindEmployeeGrid(); // Refresh the GridView
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new UnauthorizedUserException("Error while entering edit mode.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while entering edit mode.");
            }
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
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

                    SqlDataSource1.UpdateParameters["FirstName"].DefaultValue = firstName;
                    SqlDataSource1.UpdateParameters["LastName"].DefaultValue = lastName;
                    SqlDataSource1.UpdateParameters["Email"].DefaultValue = email;
                    SqlDataSource1.UpdateParameters["Phone"].DefaultValue = phone;
                    SqlDataSource1.UpdateParameters["HireDate"].DefaultValue = hireDate.ToString();
                    SqlDataSource1.UpdateParameters["EmployeeID"].DefaultValue = employeeId.ToString();

                    SqlDataSource1.Update();
                    gvEmployees.EditIndex = -1;
                    BindEmployeeGrid(); // Refresh the GridView

                    PopupControl.Show(PopupType.Success, "Success", "Employee updated successfully.");
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new UnauthorizedUserException("Error while updating employee data.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while updating the employee data.");
            }
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);

                SqlDataSource1.DeleteParameters["EmployeeID"].DefaultValue = employeeId.ToString();
                SqlDataSource1.Delete();

                PopupControl.Show(PopupType.Success, "Success", "Employee deleted successfully.");
                BindEmployeeGrid(); // Refresh the GridView
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new UnauthorizedUserException("Error while deleting employee data.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while deleting the employee data.");
            }
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvEmployees.EditIndex = -1;
                BindEmployeeGrid(); // Refresh the GridView
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new UnauthorizedUserException("Error while canceling edit.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while canceling edit mode.");
            }
        }
    }
}
