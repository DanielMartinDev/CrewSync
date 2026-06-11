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