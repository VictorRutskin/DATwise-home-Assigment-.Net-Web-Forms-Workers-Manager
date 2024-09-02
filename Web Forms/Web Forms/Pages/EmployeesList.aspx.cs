using BL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_Forms.UserControls;

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

            // Register the event handler for the AdvancedSearch control
            AdvancedSearchControl.SearchClicked += OnSearchClicked;
        }

        private async Task LoadEmployeesAsync()
        {
            List<Employee> employees = await _employeeBL.GetEmployees();
            ViewState["Employees"] = employees; // Store the data
            BindEmployeesToGrid(employees);
        }

        private void BindEmployeesToGrid(List<Employee> employees)
        {
            gvEmployees.DataSource = employees;
            gvEmployees.DataBind();
        }

        protected void OnSearchClicked(object sender, Dictionary<string, string> searchTerms)
        {
            List<Employee> employees = ViewState["Employees"] as List<Employee>;

            if (employees != null)
            {
                var filteredEmployees = employees.Where(emp =>
                    (string.IsNullOrEmpty(searchTerms["FirstName"]) || emp.FirstName.ToLower().Contains(searchTerms["FirstName"].ToLower())) &&
                    (string.IsNullOrEmpty(searchTerms["LastName"]) || emp.LastName.ToLower().Contains(searchTerms["LastName"].ToLower())) &&
                    (string.IsNullOrEmpty(searchTerms["Email"]) || emp.Email.ToLower().Contains(searchTerms["Email"].ToLower())) &&
                    (string.IsNullOrEmpty(searchTerms["Phone"]) || emp.Phone.ToLower().Contains(searchTerms["Phone"].ToLower()))
                ).ToList();

                BindEmployeesToGrid(filteredEmployees);
            }
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