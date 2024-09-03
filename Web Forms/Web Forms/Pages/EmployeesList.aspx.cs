using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_Forms.UserControls;
using BL;
using Common.CustomExceptions;
using Common.ConfigurationHandler;
using System.Threading.Tasks;
using DAL.Models;
using DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BL.Interfaces;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private IServiceLogger _serviceLogger;
        private ServiceEmployee _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<myDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationHandler.GetConnectionString());
            var dbContext = new myDbContext(optionsBuilder.Options);
            _employeeService = new ServiceEmployee(dbContext);
            _serviceLogger = new ServiceLogger(dbContext,Server.MapPath(ConfigurationHandler.GetLogFilePath()));


            if (!IsPostBack)
            {
                BindEmployeeGrid();
            }

            AdvancedSearchControl.SearchClicked += OnSearchClicked;
        }

        protected async void OnSearchClicked(object sender, Dictionary<string, string> searchTerms)
        {
            try
            {
                // Get filtered employees from the service
                var employees =  await _employeeService.GetFilteredEmployeesAsync(searchTerms);

                // Bind to the GridView
                gvEmployees.DataSource = employees;
                gvEmployees.DataBind();
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while applying search filter: " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while applying the search filter.");
            }
        }



        protected async void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
                await _employeeService.DeleteEmployeeAsync(employeeId);

                PopupControl.Show(PopupType.Success, "Success", "Employee deleted successfully.");
                BindEmployeeGrid();
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while deleting employee data: " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while deleting the employee data.");
            }
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            BindEmployeeGrid();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            BindEmployeeGrid();
        }

        protected async void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);
                var row = gvEmployees.Rows[e.RowIndex];

                var employee = new Employee
                {
                    EmployeeID = employeeId,
                    FirstName = ((TextBox)row.FindControl("txtFirstName")).Text,
                    LastName = ((TextBox)row.FindControl("txtLastName")).Text,
                    Email = ((TextBox)row.FindControl("txtEmail")).Text,
                    Phone = ((TextBox)row.FindControl("txtPhone")).Text,
                    HireDate = DateTime.Parse(((TextBox)row.FindControl("txtHireDate")).Text)
                };

                await _employeeService.SaveEmployeeAsync(employee);
                gvEmployees.EditIndex = -1;
                BindEmployeeGrid();

                PopupControl.Show(PopupType.Success, "Success", "Employee updated successfully.");
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while updating employee data: " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while updating the employee data.");
            }
        }


        private async void BindEmployeeGrid()
        {
            try
            {
                // Bind to the GridView
                var employees = await _employeeService.GetAllEmployees();
                gvEmployees.DataSource = employees;
                gvEmployees.DataBind();

                if (gvEmployees.Rows.Count == 0)
                {
                    PopupControl.Show(PopupType.Warning, "No Data", "No employees found.");
                }
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while binding employee grid: " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee list.");
            }
        }

        private string BuildFilterExpression(Dictionary<string, string> searchTerms)
        {
            var conditions = new List<string>();

            foreach (var term in searchTerms)
            {
                if (!string.IsNullOrEmpty(term.Value))
                {
                    conditions.Add($"{term.Key} LIKE '%{term.Value}%'");
                }
            }

            return string.Join(" AND ", conditions);
        }
    }
}
