using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IServiceEmployees
    {
        Task<bool> InsertEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<bool> UpdateEmployee(Employee employee);
        Task<Employee> GetEmployee(int id);
        Task<List<Employee>> GetEmployees();
    }
}
