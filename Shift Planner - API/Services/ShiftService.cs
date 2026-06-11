using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Data;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class ShiftService
    {
        private readonly ShiftPlannerContext shiftPlannerContext;

        public ShiftService(ShiftPlannerContext context)
        {
            shiftPlannerContext = context;
        }

        public List<Shift> GetShifts()
        {
            return shiftPlannerContext.Shifts.Include(s => s.Employee).ToList();
        }

        public Shift? GetShift(int id)
        {
            return shiftPlannerContext.Shifts
                .Include(s => s.Employee)
                .FirstOrDefault(s => s.ShiftID == id);
        }

        public Shift? GetShiftWithEmployee(int id)
        {
            return shiftPlannerContext.Shifts.Include(s => s.Employee).FirstOrDefault(s => s.ShiftID == id);
        }

        public Shift CreateShift(Shift shift)
        {
            var availability =
                shiftPlannerContext.Availabilities
                    .FirstOrDefault(a =>
                        a.EmployeeID == shift.EmployeeID &&
                        a.DayOfWeek == shift.StartTime.DayOfWeek);

            if (availability != null)
            {
                if (!availability.IsAvailable)
                {
                    throw new Exception(
                        "Employee is unavailable on this day.");
                }

                if (shift.StartTime.TimeOfDay < availability.AvailableFrom ||
                    shift.EndTime.TimeOfDay > availability.AvailableTo)
                {
                    throw new Exception(
                        $"Employee is only available from " +
                        $"{availability.AvailableFrom:hh\\:mm} to " +
                        $"{availability.AvailableTo:hh\\:mm} on " +
                        $"{availability.DayOfWeek}.");
                }
            }

            var holiday =
            shiftPlannerContext.HolidayRequests
                .FirstOrDefault(h =>
                h.EmployeeID == shift.EmployeeID &&
                h.Status == HolidayRequestStatus.Approved &&
                shift.StartTime.Date >= h.StartDate.Date &&
                shift.StartTime.Date <= h.EndDate.Date);

            if (holiday != null)
            {
                throw new Exception(
                    "Employee is on approved holiday.");
            }

            shiftPlannerContext.Shifts.Add(shift);

            shiftPlannerContext.SaveChanges();

            return shift;
        }

        public bool UpdateShift(
            int id,
            Shift updatedShift)
        {
            var shift =
                shiftPlannerContext.Shifts.FirstOrDefault(
                   s => s.ShiftID == id);

            if (shift == null)
                return false;

            var availability =
            shiftPlannerContext.Availabilities
                .FirstOrDefault(a =>
                    a.EmployeeID == updatedShift.EmployeeID &&
                    a.DayOfWeek == updatedShift.StartTime.DayOfWeek);

            if (availability != null)
            {
                if (!availability.IsAvailable)
                {
                    throw new Exception(
                        "Employee is unavailable on this day.");
                }

                if (updatedShift.StartTime.TimeOfDay < availability.AvailableFrom ||
                    updatedShift.EndTime.TimeOfDay > availability.AvailableTo)
                {
                    throw new Exception(
                        $"Employee is only available from " +
                        $"{availability.AvailableFrom:hh\\:mm} to " +
                        $"{availability.AvailableTo:hh\\:mm} on " +
                        $"{availability.DayOfWeek}.");
                }
            }

            var holiday =
            shiftPlannerContext.HolidayRequests
                .FirstOrDefault(h =>
                    h.EmployeeID == shift.EmployeeID &&
                    h.Status == HolidayRequestStatus.Approved &&
                    shift.StartTime.Date >= h.StartDate.Date &&
                    shift.StartTime.Date <= h.EndDate.Date);

            if (holiday != null)
            {
                throw new Exception(
                    "Employee is on approved holiday.");
            }

            shift.EmployeeID = updatedShift.EmployeeID;
            shift.StartTime = updatedShift.StartTime;
            shift.EndTime = updatedShift.EndTime;
            shift.BreakDuration = updatedShift.BreakDuration;

            shiftPlannerContext.SaveChanges();
            return true;
        }

        public bool DeleteShift(int id)
        {
            var shift = shiftPlannerContext.Shifts.FirstOrDefault(s => s.ShiftID == id);

            if (shift == null)
                return false;

            shiftPlannerContext.Shifts.Remove(shift);
            shiftPlannerContext.SaveChanges();
            return true;
        }
    }
}