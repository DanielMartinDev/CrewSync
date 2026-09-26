using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Data;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class HolidayRequestService
    {
        private readonly ShiftPlannerContext shiftPlannerContext;

        public HolidayRequestService(
            ShiftPlannerContext context)
        {
            shiftPlannerContext = context;
        }

        public List<HolidayRequest>
            GetHolidayRequests()
        {
            return shiftPlannerContext
                .HolidayRequests
                .Include(h => h.Employee)
                .ToList();
        }

        public HolidayRequest?
            GetHolidayRequest(int id)
        {
            return shiftPlannerContext
                .HolidayRequests
                .Include(h => h.Employee)
                .FirstOrDefault(
                    h => h.HolidayRequestID == id);
        }

        public List<HolidayRequest>
            GetHolidayRequestsByEmployee(
                int employeeId)
        {
            return shiftPlannerContext
                .HolidayRequests
                .Where(
                    h => h.EmployeeID == employeeId)
                .OrderBy(
                    h => h.StartDate)
                .ToList();
        }

        public HolidayRequest CreateHolidayRequest(
    HolidayRequest holidayRequest)
        {
            if (holidayRequest.EndDate <
                holidayRequest.StartDate)
            {
                throw new Exception(
                    "End date must be after start date.");
            }

            var existingRequest =
                shiftPlannerContext.HolidayRequests
                    .Any(h =>
                        h.EmployeeID == holidayRequest.EmployeeID &&
                        holidayRequest.StartDate <= h.EndDate &&
                        holidayRequest.EndDate >= h.StartDate);

            if (existingRequest)
            {
                throw new Exception(
                    "You already have a holiday request for these dates.");
            }

            var employee =
                shiftPlannerContext.Employees
                    .Include(e => e.HolidayRequests)
                    .FirstOrDefault(
                        e => e.EmployeeID == holidayRequest.EmployeeID);

            if (employee == null)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            var requestedDays =
                (holidayRequest.EndDate.Date -
                 holidayRequest.StartDate.Date).Days + 1;

            var usedDays = employee.HolidayRequests
                .Where(h =>
                    h.Status == HolidayRequestStatus.Approved)
                .Sum(h =>
                    (h.EndDate.Date -
                     h.StartDate.Date).Days + 1);

            var remainingDays =
                employee.HolidayAllowance - usedDays;

            if (requestedDays > remainingDays)
            {
                throw new Exception(
                    $"This holiday request uses {requestedDays} days, " +
                    $"but the employee only has {remainingDays} days remaining.");
            }

            shiftPlannerContext.HolidayRequests.Add(
                holidayRequest);

            shiftPlannerContext.SaveChanges();

            return holidayRequest;
        }

        public bool UpdateHolidayRequest(
         int id,
         HolidayRequest updatedHolidayRequest)
        {
            var holidayRequest =
                shiftPlannerContext
                    .HolidayRequests
                    .FirstOrDefault(
                        h => h.HolidayRequestID == id);

            if (holidayRequest == null)
                return false;

            if (updatedHolidayRequest.EndDate <
                updatedHolidayRequest.StartDate)
            {
                throw new Exception(
                    "Holiday end date cannot be before the start date.");
            }

            var overlappingRequest =
                shiftPlannerContext
                    .HolidayRequests
                    .Any(h =>
                        h.HolidayRequestID != id &&
                        h.EmployeeID ==
                            updatedHolidayRequest.EmployeeID &&
                        updatedHolidayRequest.StartDate <= h.EndDate &&
                        updatedHolidayRequest.EndDate >= h.StartDate);

            if (overlappingRequest)
            {
                throw new Exception(
                    "This employee already has a holiday request covering some or all of these dates.");
            }

            var employee =
                shiftPlannerContext
                    .Employees
                    .Include(e => e.HolidayRequests)
                    .FirstOrDefault(
                        e => e.EmployeeID ==
                            updatedHolidayRequest.EmployeeID);

            if (employee == null)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            var usedDays =
                employee.HolidayRequests
                    .Where(h =>
                        h.HolidayRequestID != id &&
                        h.Status == HolidayRequestStatus.Approved)
                    .Sum(h =>
                        (h.EndDate.Date -
                         h.StartDate.Date).Days + 1);

            var updatedRequestDays =
                (updatedHolidayRequest.EndDate.Date -
                 updatedHolidayRequest.StartDate.Date).Days + 1;

            var remainingDays =
                employee.HolidayAllowance - usedDays;

            if (updatedHolidayRequest.Status ==
                    HolidayRequestStatus.Approved &&
                updatedRequestDays > remainingDays)
            {
                throw new Exception(
                    $"This holiday request uses {updatedRequestDays} days, " +
                    $"but the employee only has {remainingDays} days remaining.");
            }

            holidayRequest.EmployeeID =
                updatedHolidayRequest.EmployeeID;

            holidayRequest.StartDate =
                updatedHolidayRequest.StartDate;

            holidayRequest.EndDate =
                updatedHolidayRequest.EndDate;

            holidayRequest.Status =
                updatedHolidayRequest.Status;

            holidayRequest.ManagerNotes =
                updatedHolidayRequest.ManagerNotes;

            shiftPlannerContext.SaveChanges();

            return true;
        }

        public bool DeleteHolidayRequest(
            int id)
        {
            var holidayRequest =
                shiftPlannerContext
                    .HolidayRequests
                    .FirstOrDefault(
                        h => h.HolidayRequestID == id);

            if (holidayRequest == null)
                return false;

            shiftPlannerContext
                .HolidayRequests
                .Remove(holidayRequest);

            shiftPlannerContext
                .SaveChanges();

            return true;
        }
    }
}