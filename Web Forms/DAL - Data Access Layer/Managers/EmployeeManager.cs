using DAL.Interfaces;
using DAL.Models;
using DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly myDbContext _context;

        public EmployeeManager(myDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

    }
}
