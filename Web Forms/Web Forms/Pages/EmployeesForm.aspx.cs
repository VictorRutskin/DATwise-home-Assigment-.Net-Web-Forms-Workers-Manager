﻿using System;
using System.Threading.Tasks;
using System.Web.UI;
using Common.ConfigurationHandler;
using Common.CustomExceptions;
using Common.ValidationHandler;
using Common;
using Microsoft.EntityFrameworkCore;
using DAL.DbContext;
using BL.Interfaces;
using DAL.Models;
using BL.Services;
using System.Text;

namespace Web_Forms.Pages
{
    public partial class EmployeesForm : System.Web.UI.Page
    {
        private IServiceLogger _serviceLogger;
        private IServiceEmployee _serviceEmployee;

        protected void Page_Init(object sender, EventArgs e)
        {
            ((SiteMaster)Master).InitializeServices(ref _serviceEmployee,ref _serviceLogger);
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            // If loaded for the first time
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    if (int.TryParse(Request.QueryString["EmployeeID"], out int employeeId))
                    {
                        formTitle.InnerText = "Edit Employee Form";
                        Button1.Text = "Edit Employee";
                        await LoadEmployee(employeeId);
                    }
                    else
                    {
                        PopupControl.Show(PopupType.Error, "Error", "Invalid Employee ID.");
                    }
                }
            }
        }



        private async Task LoadEmployee(int employeeId)
        {
            try
            {
                var employee = await _serviceEmployee.GetEmployeeByIdAsync(employeeId);
                if (employee != null)
                {
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtEmail.Text = employee.Email;
                    txtPhone.Text = employee.Phone;
                    txtHireDate.Text = employee.HireDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new EmployeeIDNotFoundInDbException("No employee data found for the given ID.");
                }
            }
            catch (EmployeeIDNotFoundInDbException ex)
            {
                PopupControl.Show(PopupType.Error, "Error", "No Employee with the id found.");
                WaitAndThenRedirectBack(3000);
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Error while loading employee data, " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while loading the employee data.");
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            ClearPreviousErrors();

            StringBuilder errorMessages = ValidateInputs();

            if (errorMessages.Length > 0)
            {
                PopupControl.Show(PopupType.Error, "Validation Error", errorMessages.ToString());
                return;
            }

            try
            {
                // Create populated employee from fields
                Employee employee = CreateEmployee();

                await _serviceEmployee.UpdateEmployeeAsync(employee);

                PopupControl.Show(PopupType.Success, "Success", "Employee saved successfully.");

                // Disable all controls after updating
                DisableControls();

                // Redirect back after a delay
                WaitAndThenRedirectBack(3000);
            }
            catch (Exception ex)
            {
                await _serviceLogger.LogErrorAsync(new DatabaseAccessException("Failed to save employee data, " + ex.Message, ex));
                PopupControl.Show(PopupType.Error, "Error", "An error occurred while saving the employee data.");
            }
        }


        // Private methods
        private void WaitAndThenRedirectBack(int miliseconds)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", JScriptHandler.Redirect_EmployeesList(miliseconds), true);
        }

        private void DisableControls()
        {
            // Disable the Save button
            Button1.Enabled = false;

            // Disable the fields 
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtHireDate.Enabled = false;
        }

        private void ClearPreviousErrors()
        {
            lblFirstNameError.Text = string.Empty;
            lblLastNameError.Text = string.Empty;
            lblEmailError.Text = string.Empty;
            lblPhoneError.Text = string.Empty;
            lblHireDateError.Text = string.Empty;
        }

        private Employee CreateEmployee()
        {
            return new Employee
            {
                EmployeeID = Request.QueryString["EmployeeID"] != null ? int.Parse(Request.QueryString["EmployeeID"]) : 0,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                HireDate = DateTime.Parse(txtHireDate.Text)
            };
        }

        private StringBuilder ValidateInputs()
        {
            StringBuilder errorMessages = new StringBuilder();

            if (!ValidationHandler.ValidateName(txtFirstName.Text))
            {
                string error_text = "Valid First Name is required.";
                lblFirstNameError.Text = error_text;
                errorMessages.AppendLine(error_text);
            }

            if (!ValidationHandler.ValidateName(txtLastName.Text))
            {
                string error_text = "Valid Last Name is required.";
                lblLastNameError.Text = error_text;
                errorMessages.AppendLine(error_text);
            }

            if (!ValidationHandler.ValidateEmail(txtEmail.Text))
            {
                string error_text = "Valid email format is required.";
                lblEmailError.Text = error_text;
                errorMessages.AppendLine(error_text);
            }

            if (!ValidationHandler.ValidatePhone(txtPhone.Text))
            {
                string error_text = "Valid Phone is required.";
                lblPhoneError.Text = error_text;
                errorMessages.AppendLine(error_text);
            }

            if (!ValidationHandler.ValidateHireDate(txtHireDate.Text))
            {
                string error_text = "Hire Date must be between January 1, 1900 and today.";
                lblHireDateError.Text = error_text;
                errorMessages.AppendLine(error_text);
            }

            return errorMessages;
        }
   
    }
}
