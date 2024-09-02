//using BL;
//using Common.ConfigurationHandler;
//using Common.ValidationHandler;
//using DAL.Models;
//using System;
//using System.Configuration;
//using System.Threading.Tasks;
//using System.Web.UI;

//namespace Web_Forms.Pages
//{
//    public partial class EmployeesForm : System.Web.UI.Page
//    {
//        private ServiceEmployees _serviceEmployees;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            _serviceEmployees = new ServiceEmployees(ConfigurationHandler.GetConnectionString());

//            if (!IsPostBack)
//            {
//                if (Request.QueryString["EmployeeID"] != null)
//                {
//                    int employeeId = int.Parse(Request.QueryString["EmployeeID"]);
//                    LoadEmployee(employeeId);
//                }
//            }
//        }

//        private async void LoadEmployee(int employeeId)
//        {
//            var employee = await _serviceEmployees.GetEmployee(employeeId);
//            if (employee != null)
//            {
//                txtFirstName.Text = employee.FirstName;
//                txtLastName.Text = employee.LastName;
//                txtEmail.Text = employee.Email;
//                txtPhone.Text = employee.Phone;
//                txtHireDate.Text = employee.HireDate.ToString("yyyy-MM-dd");
//            }
//        }

//        protected async void btnSave_Click(object sender, EventArgs e)
//        {
//            // Clear previous error messages
//            lblFirstNameError.Text = string.Empty;
//            lblLastNameError.Text = string.Empty;
//            lblEmailError.Text = string.Empty;
//            lblPhoneError.Text = string.Empty;
//            lblHireDateError.Text = string.Empty;

//            string errorMessage = string.Empty;

//            if (!ValidationHandler.ValidateFirstName(txtFirstName.Text))
//            {
//                lblFirstNameError.Text = "First Name is required.";
//                errorMessage += "First Name is required.\n";
//            }

//            if (!ValidationHandler.ValidateLastName(txtLastName.Text))
//            {
//                lblLastNameError.Text = "Last Name is required.";
//                errorMessage += "Last Name is required.\n";
//            }

//            if (!ValidationHandler.ValidateEmail(txtEmail.Text))
//            {
//                lblEmailError.Text = "Invalid email format.";
//                errorMessage += "Invalid email format.\n";
//            }

//            if (!ValidationHandler.ValidatePhone(txtPhone.Text))
//            {
//                lblPhoneError.Text = "Phone is required.";
//                errorMessage += "Phone is required.\n";
//            }

//            if (!ValidationHandler.ValidateHireDate(txtHireDate.Text))
//            {
//                lblHireDateError.Text = "Hire Date is required.";
//                errorMessage += "Hire Date is required.\n";
//            }

//            if (!string.IsNullOrEmpty(errorMessage))
//            {
//                // Show pop-up message with error details
//                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{errorMessage}');", true);
//                return;
//            }

//            var employee = new Employee
//            {
//                EmployeeID = Request.QueryString["EmployeeID"] != null ? int.Parse(Request.QueryString["EmployeeID"]) : 0,
//                FirstName = txtFirstName.Text,
//                LastName = txtLastName.Text,
//                Email = txtEmail.Text,
//                Phone = txtPhone.Text,
//                HireDate = DateTime.Parse(txtHireDate.Text)
//            };

//            if (employee.EmployeeID > 0)
//            {
//                await _serviceEmployees.UpdateEmployee(employee);
//            }
//            else
//            {
//                await _serviceEmployees.InsertEmployee(employee);
//            }

//            Response.Redirect("EmployeeList.aspx");
//        }

//    }
//}
