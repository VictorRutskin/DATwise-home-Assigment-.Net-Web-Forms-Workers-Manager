using BL___Bussiness_Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private EmployeeBL _employeeBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;
            _employeeBL = new EmployeeBL(connectionString);

            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    int employeeId = int.Parse(Request.QueryString["EmployeeID"]);
                    LoadEmployee(employeeId);
                }
            }
        }

        private void LoadEmployee(int employeeId)
        {
            _employeeBL.LoadEmployee(employeeId, out string firstName, out string lastName, out string email, out string phone, out DateTime hireDate);

            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtHireDate.Text = hireDate.ToString("yyyy-MM-dd");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EmployeeID"] != null)
            {
                int employeeId = int.Parse(Request.QueryString["EmployeeID"]);
                _employeeBL.UpdateEmployee(employeeId, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text, DateTime.Parse(txtHireDate.Text));
            }
            else
            {
                _employeeBL.InsertEmployee(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text, DateTime.Parse(txtHireDate.Text));
            }

            Response.Redirect("EmployeeList.aspx");
        }

    }
}