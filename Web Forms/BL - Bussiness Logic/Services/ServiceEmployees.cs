using DAL.Models;
using DAL.myDbContext;
using DAL_Data_Access_Layer.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

public class EmployeeService
{
    private readonly EmployeeManager _employeeManager;
    private List<Employee> _allEmployeesList;

    public EmployeeService(myDbContext dbContext)
    {
        _employeeManager = new EmployeeManager(dbContext);
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _employeeManager.GetEmployeeByIdAsync(employeeId);
    }

    public async Task SaveEmployeeAsync(Employee employee)
    {
        if (employee.EmployeeID == 0)
        {
            await _employeeManager.AddEmployeeAsync(employee);
        }
        else
        {
            await _employeeManager.UpdateEmployeeAsync(employee);
        }
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        await _employeeManager.DeleteEmployeeAsync(employeeId);
    }

    public async Task<List<Employee>> GetAllEmployees()
    {
        _allEmployeesList = await _employeeManager.GetAllEmployees();
        return  _allEmployeesList;
    }

    public async Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(Dictionary<string, string> searchTerms)
    {
        // Ensure _allEmployeesList is not null
        if (_allEmployeesList == null)
        {
            await GetAllEmployees();
        }

        // Apply filters based on search terms
        var filteredEmployees = _allEmployeesList.AsQueryable();

        // Loop through each search term and build the query
        foreach (var term in searchTerms)
        {
            string key = term.Key.ToLower();
            string value = term.Value.ToLower();

            // Apply filters based on key-value pairs
            switch (key)
            {
                case "firstname":
                    filteredEmployees = filteredEmployees.Where(employee => employee.FirstName.ToLower().Contains(value));
                    break;
                case "lastname":
                    filteredEmployees = filteredEmployees.Where(employee => employee.LastName.ToLower().Contains(value));
                    break;
                case "email":
                    filteredEmployees = filteredEmployees.Where(employee => employee.Email.ToLower().Contains(value));
                    break;
                case "phone":
                    filteredEmployees = filteredEmployees.Where(employee => employee.Phone.ToLower().Contains(value));
                    break;
                default:
                    // Optionally handle unsupported keys
                    throw new ArgumentException($"Unknown search term key: {key}");
            }
        }

        return filteredEmployees.ToList();
    }




    // This is a helper method to build the filter expression
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
