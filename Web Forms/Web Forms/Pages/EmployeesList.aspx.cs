using BL___Bussiness_Logic;
using DAL___Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private EmployeeBL _employeeBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;
            _employeeBL = new EmployeeBL(connectionString);

            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }

        private void LoadEmployees()
        {
            List<Employee> employees = _employeeBL.GetAllEmployees();
            gvEmployees.DataSource = employees;
            gvEmployees.DataBind();
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            LoadEmployees();
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
            string firstName = (row.FindControl("FirstName") as TextBox).Text;
            string lastName = (row.FindControl("LastName") as TextBox).Text;
            string email = (row.FindControl("Email") as TextBox).Text;
            string phone = (row.FindControl("Phone") as TextBox).Text;

            Employee employee = new Employee
            {
                EmployeeID = employeeId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };

            _employeeBL.UpdateEmployee(employee);

            gvEmployees.EditIndex = -1;
            LoadEmployees();
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
            _employeeBL.DeleteEmployee(employeeId);
            LoadEmployees();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            LoadEmployees();
        }
    }
}