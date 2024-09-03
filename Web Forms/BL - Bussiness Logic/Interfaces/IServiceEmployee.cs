using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IServiceEmployee
    {
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<List<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(Dictionary<string, string> searchTerms);

    }
}

