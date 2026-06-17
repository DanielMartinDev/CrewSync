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

        public HolidayRequest
            CreateHolidayRequest(
                HolidayRequest holidayRequest)
        {
            shiftPlannerContext
                .HolidayRequests
                .Add(holidayRequest);

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

            if (holidayRequest.EndDate <
                holidayRequest.StartDate)
            {
                throw new Exception(
                    "End date must be after start date.");
            }

            shiftPlannerContext
                .SaveChanges();

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

            shiftPlannerContext
                .SaveChanges();

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