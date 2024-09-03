using DAL.Models;
using DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Managers;
using BL.Interfaces;

namespace BL.Services
{
    public class ServiceEmployee : IServiceEmployee
    {
        private readonly EmployeeManager _employeeManager;
        private List<Employee> _allEmployeesList;

        public ServiceEmployee(myDbContext dbContext)
        {
            _employeeManager = new EmployeeManager(dbContext);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeManager.GetEmployeeByIdAsync(employeeId);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
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
            // Save all employees in memory for future filtering instead of accessing db every time
            _allEmployeesList = await _employeeManager.GetAllEmployees();
            return _allEmployeesList;
        }

        public async Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(Dictionary<string, string> searchTerms)
        {
            // Ensure _allEmployeesList is not null; if null, get employees
            if (_allEmployeesList == null)
            {
                await GetAllEmployees();
            }

            var filteredEmployees = _allEmployeesList.AsQueryable();

            foreach (var term in searchTerms)
            {
                string key = term.Key.ToLower();
                string value = term.Value.ToLower();

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
                    case "startdate":
                        if (DateTime.TryParse(value, out DateTime startDate))
                        {
                            filteredEmployees = filteredEmployees.Where(employee => employee.HireDate.Date >= startDate.Date);
                        }
                        break;
                    case "enddate":
                        if (DateTime.TryParse(value, out DateTime endDate))
                        {
                            filteredEmployees = filteredEmployees.Where(employee => employee.HireDate.Date <= endDate.Date);
                        }
                        break;
                }
            }
            return filteredEmployees.ToList();
        }
    }
}
