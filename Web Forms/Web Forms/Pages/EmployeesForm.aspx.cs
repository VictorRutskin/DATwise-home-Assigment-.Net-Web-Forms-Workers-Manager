using Common.ValidationHandler;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{errorMessage}');", true);
                return;
            }

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
            }
            else
            {
                SqlDataSource1.Insert();
            }

            Response.Redirect("EmployeeList.aspx");
        }
    }
}
