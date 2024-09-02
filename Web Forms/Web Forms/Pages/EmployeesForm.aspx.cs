using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.CustomExceptions;
using Common.ConfigurationHandler; // Make sure this namespace is included
using Common.ValidationHandler;
using DAL_Data_Access_Layer.Models;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private ILoggerService _loggerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _loggerService = new LoggerService(Server.MapPath(ConfigurationHandler.GetLogFilePath()));

            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    int employeeId;
                    if (int.TryParse(Request.QueryString["EmployeeID"], out employeeId))
                    {
                        try
                        {
                            LoadEmployee(employeeId);
                        }
                        catch (Exception ex)
                        {
                            _loggerService.LogError(new NotFoundInDbException("Failed to load employee.", _loggerService));
                            PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee data.");
                        }
                    }
                    else
                    {
                        PopupControl.Show(PopupType.Error, "Error", "Invalid Employee ID.");
                    }
                }
            }
        }

        private void LoadEmployee(int employeeId)
        {
            try
            {
                SqlDataSource1.SelectParameters["EmployeeID"].DefaultValue = employeeId.ToString();
                var employee = SqlDataSource1.Select(DataSourceSelectArguments.Empty) as System.Data.DataView;
                if (employee != null && employee.Count > 0)
                {
                    var row = employee[0];
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtPhone.Text = row["Phone"].ToString();
                    txtHireDate.Text = Convert.ToDateTime(row["HireDate"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new NotFoundInDbException("No employee data found for the given ID.", _loggerService);
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new NotFoundInDbException("Failed to load employee data.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee data.");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
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
                // Show pop-up message with error details
                PopupControl.Show(PopupType.Error, "Validation Error", errorMessage);
                return;
            }

            try
            {
                // Insert or Update Employee
                SqlDataSource1.InsertParameters["FirstName"].DefaultValue = txtFirstName.Text;
                SqlDataSource1.InsertParameters["LastName"].DefaultValue = txtLastName.Text;
                SqlDataSource1.InsertParameters["Email"].DefaultValue = txtEmail.Text;
                SqlDataSource1.InsertParameters["Phone"].DefaultValue = txtPhone.Text;
                SqlDataSource1.InsertParameters["HireDate"].DefaultValue = txtHireDate.Text;

                if (Request.QueryString["EmployeeID"] != null)
                {
                    SqlDataSource1.UpdateParameters["EmployeeID"].DefaultValue = Request.QueryString["EmployeeID"];
                    SqlDataSource1.UpdateParameters["FirstName"].DefaultValue = txtFirstName.Text;
                    SqlDataSource1.UpdateParameters["LastName"].DefaultValue = txtLastName.Text;
                    SqlDataSource1.UpdateParameters["Email"].DefaultValue = txtEmail.Text;
                    SqlDataSource1.UpdateParameters["Phone"].DefaultValue = txtPhone.Text;
                    SqlDataSource1.UpdateParameters["HireDate"].DefaultValue = txtHireDate.Text;

                    SqlDataSource1.Update();
                    PopupControl.Show(PopupType.Success, "Success", "Employee updated successfully.");
                }
                else
                {
                    SqlDataSource1.Insert();
                    PopupControl.Show(PopupType.Success, "Success", "Employee added successfully.");
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new UnauthorizedUserException("Failed to save employee data.", _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while saving the employee data.");
            }
        }
    }
}
