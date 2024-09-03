using System;
using System.Threading.Tasks;
using System.Web.UI;
using Common.ConfigurationHandler;
using Common.CustomExceptions;
using Common.ValidationHandler;
using Common;
using Microsoft.EntityFrameworkCore;
using DAL.DbContext;
using BL.Interfaces;
using DAL.Models;
using BL.Services;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private IServiceLogger _serviceLogger;
        private IServiceEmployee _serviceEmployee;

        protected void Page_Init(object sender, EventArgs e)
        {
            _serviceEmployee = ((SiteMaster)Master).ServiceEmployee;
            _serviceLogger = ((SiteMaster)Master).ServiceLogger;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    if (int.TryParse(Request.QueryString["EmployeeID"], out int employeeId))
                    {
                        await LoadEmployee(employeeId);
                    }
                    else
                    {
                        PopupControl.Show(PopupType.Error, "Error", "Invalid Employee ID.");
                    }
                }
            }
        }

        private async Task LoadEmployee(int employeeId)
        {
            try
            {
                var employee = await _serviceEmployee.GetEmployeeByIdAsync(employeeId);
                if (employee != null)
                {
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtEmail.Text = employee.Email;
                    txtPhone.Text = employee.Phone;
                    txtHireDate.Text = employee.HireDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new EmployeeIDNotFoundInDbException("No employee data found for the given ID.");
                }
            }
            catch (EmployeeIDNotFoundInDbException ex)
            {
                PopupControl.Show(PopupType.Error, "Error", "No Employee with the id found.");
                WaitAndThenRedirectBack();
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while loading employee data, " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee data.");
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            // Clear previous error messages
            lblFirstNameError.Text = string.Empty;
            lblLastNameError.Text = string.Empty;
            lblEmailError.Text = string.Empty;
            lblPhoneError.Text = string.Empty;
            lblHireDateError.Text = string.Empty;

            string errorMessage = string.Empty;

            if (!ValidationHandler.ValidateFirstName(txtFirstName.Text))
            {
                lblFirstNameError.Text = "Valid First Name is required.";
                errorMessage += "Valid First Name is required.\n";
            }

            if (!ValidationHandler.ValidateLastName(txtLastName.Text))
            {
                lblLastNameError.Text = "Valid Last Name is required.";
                errorMessage += "Valid Last Name is required.\n";
            }

            if (!ValidationHandler.ValidateEmail(txtEmail.Text))
            {
                lblEmailError.Text = "Invalid email format.";
                errorMessage += "Invalid email format.\n";
            }

            if (!ValidationHandler.ValidatePhone(txtPhone.Text))
            {
                lblPhoneError.Text = "Valid Phone is required.";
                errorMessage += "Valid Phone is required.\n";
            }

            if (!DateTime.TryParse(txtHireDate.Text, out DateTime hireDate) || hireDate < new DateTime(1900, 1, 1) || hireDate > DateTime.Today)
            {
                lblHireDateError.Text = "Hire Date must be between January 1, 1900 and today.";
                errorMessage += "Hire Date must be between January 1, 1900 and today.\n";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                PopupControl.Show(PopupType.Error, "Validation Error", errorMessage);
                return;
            }

            try
            {
                var employee = new Employee
                {
                    EmployeeID = Request.QueryString["EmployeeID"] != null ? int.Parse(Request.QueryString["EmployeeID"]) : 0,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    HireDate = hireDate
                };

                await _serviceEmployee.UpdateEmployeeAsync(employee);

                PopupControl.Show(PopupType.Success, "Success", "Employee saved successfully.");

                // Disable the Save button
                Button1.Enabled = false;

                // Disable the fields after the save operation
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtEmail.Enabled = false;
                txtPhone.Enabled = false;
                txtHireDate.Enabled = false;

                // Redirect back after a delay
                WaitAndThenRedirectBack();
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Failed to save employee data, " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while saving the employee data.");
            }
        }

        private void WaitAndThenRedirectBack()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", JScriptHandler.Redirect_EmployeesList(3000), true);
        }
    }
}
