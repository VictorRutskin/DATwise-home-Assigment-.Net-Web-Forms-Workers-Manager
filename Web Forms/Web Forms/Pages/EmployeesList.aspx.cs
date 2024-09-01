using BL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private ServiceEmployees _employeeBL;

        protected async void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;
            _employeeBL = new ServiceEmployees(connectionString);

            if (!IsPostBack)
            {
                await LoadEmployeesAsync();
            }
        }


        private async Task LoadEmployeesAsync()
        {
            List<Employee> employees = await _employeeBL.GetEmployees();
            gvEmployees.DataSource = employees;
            gvEmployees.DataBind();
        }

        protected async void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            await LoadEmployeesAsync(); 
        }

        protected async void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
            string firstName = (row.FindControl("FirstName") as TextBox).Text;
            string lastName = (row.FindControl("LastName") as TextBox).Text;
            string email = (row.FindControl("Email") as TextBox).Text;
            string phone = (row.FindControl("Phone") as TextBox).Text;
            DateTime hireDate = DateTime.Parse((row.FindControl("HireDate") as TextBox).Text); 

            Employee employee = new Employee
            {
                EmployeeID = employeeId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                HireDate = hireDate 
            };

            await _employeeBL.UpdateEmployee(employee);

            gvEmployees.EditIndex = -1;
            await LoadEmployeesAsync(); 
        }

        protected async void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
            await _employeeBL.DeleteEmployee(employeeId);
            await LoadEmployeesAsync(); 
        }

        protected async void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            await LoadEmployeesAsync(); 
        }
    }
}
