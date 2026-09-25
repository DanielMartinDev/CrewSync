using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Data;
using Shift_Planner___API.Models;
using Shift_Planner___API.DTOs;

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
            return shiftPlannerContext.Employees
                .Include(e => e.HolidayRequests)
                .ToList();
        }

        public EmployeeHolidayDto? GetEmployeeHoliday(int employeeId)
        {
            var employee = shiftPlannerContext.Employees
                .Include(e => e.HolidayRequests)
                .FirstOrDefault(e => e.EmployeeID == employeeId);

            if (employee == null)
                return null;

            var daysUsed = employee.HolidayRequests
                .Where(h => h.Status == HolidayRequestStatus.Approved)
                .Sum(h =>
                    (h.EndDate.Date - h.StartDate.Date).Days + 1);

            return new EmployeeHolidayDto
            {
                EmployeeID = employee.EmployeeID,
                HolidayAllowance = employee.HolidayAllowance,
                HolidayDaysUsed = daysUsed,
                HolidayDaysRemaining =
                    employee.HolidayAllowance - daysUsed
            };
        }

        public Employee? GetEmployee(int id)
        {
            return shiftPlannerContext.Employees
                .Include(e => e.HolidayRequests)
                .FirstOrDefault(e => e.EmployeeID == id);
        }

        public Employee CreateEmployee(Employee employee)
        {
            shiftPlannerContext.Employees.Add(employee);

            shiftPlannerContext.SaveChanges();

            foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
            {
                shiftPlannerContext.Availabilities.Add(
                    new Availability
                    {
                        EmployeeID = employee.EmployeeID,
                        DayOfWeek = day,
                        IsAvailable = false,
                        AvailableFrom = TimeSpan.Zero,
                        AvailableTo = TimeSpan.Zero
                    });
            }

            shiftPlannerContext.SaveChanges();

            return employee;
        }

        public Employee? GetEmployeeWithShifts(int id)
        {
            return shiftPlannerContext.Employees
                .Include(e => e.Shifts)
                .FirstOrDefault(e => e.EmployeeID == id);
        }

        public bool UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = shiftPlannerContext.Employees
                .FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            employee.Name = updatedEmployee.Name;
            employee.WeeklyHours = updatedEmployee.WeeklyHours;
            employee.StartDate = updatedEmployee.StartDate;
            employee.Role = updatedEmployee.Role;
            employee.UserId = updatedEmployee.UserId;
            employee.Email = updatedEmployee.Email;
            employee.HolidayAllowance = updatedEmployee.HolidayAllowance;

            shiftPlannerContext.SaveChanges();

            return true;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = shiftPlannerContext.Employees
                .FirstOrDefault(e => e.EmployeeID == id);

            if (employee == null)
                return false;

            shiftPlannerContext.Employees.Remove(employee);

            shiftPlannerContext.SaveChanges();

            return true;
        }
    }
}