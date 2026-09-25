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

        public async Task<List<Shift>> GetShifts()
        {
            return shiftPlannerContext.Shifts
                .AsNoTracking()
                .Include(s => s.Employee)
                .ToList();
        }

        public async Task<Shift?> GetShift(int id)
        {
            return await shiftPlannerContext.Shifts
                .AsNoTracking()
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.ShiftID == id);
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

                if (shift.EndTime <= shift.StartTime)
                {
                    throw new Exception("Shift end time must be after start time.");
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

            var overlappingShift = shiftPlannerContext.Shifts.Any(s =>
            s.EmployeeID == shift.EmployeeID &&
            s.ShiftID != shift.ShiftID &&
            shift.StartTime < s.EndTime &&
            shift.EndTime > s.StartTime);

            if (overlappingShift)
            {
                throw new Exception(
                    "Employee already has a shift during this time.");
            }

            if (shift.BreakDuration >=
               (shift.EndTime - shift.StartTime).TotalMinutes)
            {
                throw new Exception(
                    "Break duration cannot be equal to or longer than the shift.");
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
                    updatedShift.StartTime.Date >= h.StartDate.Date &&
                    updatedShift.StartTime.Date <= h.EndDate.Date);

            if (holiday != null)
            {
                throw new Exception(
                    "Employee is on approved holiday.");
            }

            var overlappingShift = shiftPlannerContext.Shifts.Any(s =>
            s.EmployeeID == updatedShift.EmployeeID &&
            s.ShiftID != id &&
            updatedShift.StartTime < s.EndTime &&
            updatedShift.EndTime > s.StartTime);

            if (overlappingShift)
            {
                throw new Exception(
                    "Employee already has a shift during this time.");
            }

            if (shift.EndTime <= shift.StartTime)
            {
                throw new Exception("Shift end time must be after start time.");
            }

            shift.EmployeeID = updatedShift.EmployeeID;
            shift.StartTime = updatedShift.StartTime;
            shift.EndTime = updatedShift.EndTime;
            shift.BreakDuration = updatedShift.BreakDuration;
            shift.Notes = updatedShift.Notes;

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