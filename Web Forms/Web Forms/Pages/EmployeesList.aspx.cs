using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_Forms.UserControls;
using BL;
using Common.CustomExceptions;
using Common.ConfigurationHandler;
using DAL_Data_Access_Layer.Models;
using static Common.CustomExceptions.LoggerService;

namespace Web_Forms.Pages
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        private ILoggerService _loggerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _loggerService = new LoggerService(Server.MapPath(ConfigurationHandler.GetLogFilePath()));

            if (!IsPostBack)
            {
                BindEmployeeGrid();
            }

            AdvancedSearchControl.SearchClicked += OnSearchClicked;
        }

        protected void OnSearchClicked(object sender, Dictionary<string, string> searchTerms)
        {
            try
            {
                string filterExpression = BuildFilterExpression(searchTerms);
                SqlDataSource1.FilterExpression = filterExpression;
                gvEmployees.DataBind();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new DatabaseAccessException("Error while applying search filter: " + ex.Message, _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while applying the search filter.");
            }
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Values[0]);

                SqlDataSource1.DeleteParameters["EmployeeID"].DefaultValue = employeeId.ToString();
                SqlDataSource1.Delete();

                PopupControl.Show(PopupType.Success, "Success", "Employee deleted successfully.");
                BindEmployeeGrid();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new DatabaseAccessException("Error while deleting employee data: " + ex.Message, _loggerService));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while deleting the employee data.");
            }
        }

        private void BindEmployeeGrid()
        {
            try
            {
                gvEmployees.DataBind();

                if (gvEmployees.Rows.Count == 0)
                {
                    PopupControl.Show(PopupType.Warning, "No Data", "No employees found.");
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(new DatabaseAccessException("Error while binding employee grid: " + ex.Message, _loggerService));
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
