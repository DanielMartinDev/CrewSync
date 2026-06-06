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

        public bool UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            employee.Name = updatedEmployee.Name;
            employee.WeeklyHours = updatedEmployee.WeeklyHours;
            employee.StartDate = updatedEmployee.StartDate;
            employee.Role = updatedEmployee.Role;

            return true;
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