using DAL___Data_Access_Layer;
using DAL___Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL___Bussiness_Logic
{
    public class EmployeeBL
    {
        private EmployeeManager _employeeManager;

        public EmployeeBL(string connectionString)
        {
            _employeeManager = new EmployeeManager(connectionString);
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeManager.GetAllEmployees();
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeManager.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            _employeeManager.DeleteEmployee(employeeId);
        }

        public void LoadEmployee(int employeeId, out string firstName, out string lastName, out string email, out string phone, out DateTime hireDate)
        {
            using (var reader = _employeeManager.GetEmployeeById(employeeId))
            {
                if (reader.Read())
                {
                    firstName = reader["FirstName"].ToString();
                    lastName = reader["LastName"].ToString();
                    email = reader["Email"].ToString();
                    phone = reader["Phone"].ToString();
                    hireDate = DateTime.Parse(reader["HireDate"].ToString());
                }
                else
                {
                    firstName = lastName = email = phone = string.Empty;
                    hireDate = DateTime.MinValue;
                }
            }
        }

        public void UpdateEmployee(int employeeId, string firstName, string lastName, string email, string phone, DateTime hireDate)
        {
            _employeeManager.UpdateEmployee(employeeId, firstName, lastName, email, phone, hireDate);
        }

        public void InsertEmployee(string firstName, string lastName, string email, string phone, DateTime hireDate)
        {
            _employeeManager.InsertEmployee(firstName, lastName, email, phone, hireDate);
        }
    }
}
