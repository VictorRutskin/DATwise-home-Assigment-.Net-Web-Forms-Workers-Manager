using BL.Interfaces;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL
{
    public class ServiceEmployees : IServiceEmployees
    {
        private readonly EmployeeManager _employeeManager;

        public ServiceEmployees(string connectionString)
        {
            _employeeManager = new EmployeeManager(connectionString);
        }

        public async Task<bool> InsertEmployee(Employee employee)
        {
            return await _employeeManager.InsertEmployeeAsync(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeManager.DeleteEmployeeAsync(id);
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeeManager.UpdateEmployeeAsync(employee);
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeManager.GetEmployeeByIdAsync(id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeManager.GetAllEmployeesAsync();
        }
    }
}
