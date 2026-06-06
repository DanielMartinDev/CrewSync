using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Data;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class EmployeeService
    {
        private readonly ShiftPlannerContext shiftPlannerContext;

        public EmployeeService(ShiftPlannerContext context)
        {
             shiftPlannerContext = context;
        }

        public List<Employee> GetEmployees()
        {
            return shiftPlannerContext.Employees.ToList();
        }

        public Employee? GetEmployee(int id)
        {
            return shiftPlannerContext.Employees.FirstOrDefault(
                e => e.EmployeeID == id);
        }

        public Employee CreateEmployee(Employee employee)
        {
            shiftPlannerContext.Employees.Add(employee);
            shiftPlannerContext.SaveChanges();

            return employee;
        }

        public Employee? GetEmployeeWithShifts(int id)
        {
            return shiftPlannerContext.Employees.Include(e => e.Shifts).FirstOrDefault(e => e.EmployeeID == id);
        }

        public bool UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = shiftPlannerContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            employee.Name = updatedEmployee.Name;
            employee.WeeklyHours = updatedEmployee.WeeklyHours;
            employee.StartDate = updatedEmployee.StartDate;
            employee.Role = updatedEmployee.Role;

            shiftPlannerContext.SaveChanges();
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = shiftPlannerContext.Employees.FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            shiftPlannerContext.Employees.Remove(employee);
            shiftPlannerContext.SaveChanges();
            return true;
        }
    }
}