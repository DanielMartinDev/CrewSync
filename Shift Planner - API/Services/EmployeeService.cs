using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> employees;

        public EmployeeService()
        {
            employees = new List<Employee>();
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee? GetEmployee(int id)
        {
            return employees.FirstOrDefault(
                e => e.EmployeeID == id);
        }

        public Employee CreateEmployee(Employee employee)
        {
            employees.Add(employee);

            return employee;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            employees.Remove(employee);
            return true;
        }
    }
}