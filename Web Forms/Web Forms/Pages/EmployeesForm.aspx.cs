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

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private myDbContext _dbContext;
        private readonly ServiceEmployee _serviceEmployee;
        private IServiceLogger _serviceLogger;

        public EmployeesForm()
        {
            // Setup DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<myDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationHandler.GetConnectionString());
            _dbContext = new myDbContext(optionsBuilder.Options);

            _serviceEmployee = new ServiceEmployee(_dbContext);
            _serviceLogger = new ServiceLogger(_dbContext,Server.MapPath(ConfigurationHandler.GetLogFilePath()));
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
                lblFirstNameError.Text = "First Name is required.";
                errorMessage += "First Name is required.\n";
            }

            if (!ValidationHandler.ValidateLastName(txtLastName.Text))
            {
                lblLastNameError.Text = "Last Name is required.";
                errorMessage += "Last Name is required.\n";
            }

            if (!ValidationHandler.ValidateEmail(txtEmail.Text))
            {
                lblEmailError.Text = "Invalid email format.";
                errorMessage += "Invalid email format.\n";
            }

            if (!ValidationHandler.ValidatePhone(txtPhone.Text))
            {
                lblPhoneError.Text = "Phone is required.";
                errorMessage += "Phone is required.\n";
            }

            if (!ValidationHandler.ValidateHireDate(txtHireDate.Text))
            {
                lblHireDateError.Text = "Hire Date is required.";
                errorMessage += "Hire Date is required.\n";
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
                    HireDate = DateTime.Parse(txtHireDate.Text)
                };

                await _serviceEmployee.SaveEmployeeAsync(employee);

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
