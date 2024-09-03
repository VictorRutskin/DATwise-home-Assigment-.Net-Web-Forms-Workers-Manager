using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmployeeManager
    {
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<List<Employee>> GetAllEmployees();
    }
}
