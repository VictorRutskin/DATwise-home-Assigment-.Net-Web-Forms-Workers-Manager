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
using BL.Services;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private IServiceLogger _serviceLogger;
        private IServiceEmployee _serviceEmployee;

        protected void Page_Init(object sender, EventArgs e)
        {
            ((SiteMaster)Master).InitializeServices(ref _serviceEmployee, ref _serviceLogger);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // If loaded for the first time
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
                var employees =  await _serviceEmployee.GetFilteredEmployeesAsync(searchTerms);

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
                await _serviceEmployee.DeleteEmployeeAsync(employeeId);

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
                GridViewRow row = gvEmployees.Rows[e.RowIndex];
                Employee employee = GetEmployeeFromRow(row);

                await _serviceEmployee.UpdateEmployeeAsync(employee);
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


        // Private methods
        private async void BindEmployeeGrid()
        {
            try
            {
                // Bind to the GridView
                var employees = await _serviceEmployee.GetAllEmployees();
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

        private Employee GetEmployeeFromRow(GridViewRow row)
        {
            return new Employee
            {
                FirstName = ((TextBox)row.FindControl("txtFirstName")).Text,
                LastName = ((TextBox)row.FindControl("txtLastName")).Text,
                Email = ((TextBox)row.FindControl("txtEmail")).Text,
                Phone = ((TextBox)row.FindControl("txtPhone")).Text,
                HireDate = DateTime.Parse(((TextBox)row.FindControl("txtHireDate")).Text)
            };
        }

    }
}
