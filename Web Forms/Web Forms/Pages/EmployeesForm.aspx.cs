using BL;
using Common;
using DAL.Models;
using System;
using System.Configuration;
using System.Web.UI;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private ServiceEmployees _serviceEmployees;

        protected void Page_Load(object sender, EventArgs e)
        {
            _serviceEmployees = new ServiceEmployees(MyConfigurationManager.GetConnectionString());

            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    int employeeId = int.Parse(Request.QueryString["EmployeeID"]);
                    LoadEmployee(employeeId);
                }
            }
        }

        private async void LoadEmployee(int employeeId)
        {
            var employee = await _serviceEmployees.GetEmployee(employeeId);
            if (employee != null)
            {
                txtFirstName.Text = employee.FirstName;
                txtLastName.Text = employee.LastName;
                txtEmail.Text = employee.Email;
                txtPhone.Text = employee.Phone;
                txtHireDate.Text = employee.HireDate.ToString("yyyy-MM-dd");
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
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

            if (employee.EmployeeID > 0)
            {
                await _serviceEmployees.UpdateEmployee(employee);
            }
            else
            {
                await _serviceEmployees.InsertEmployee(employee);
            }

            Response.Redirect("EmployeeList.aspx");
        }
    }
}
