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
            // Validate shift times first
            var employeeExists =
            shiftPlannerContext.Employees
            .Any(e => e.EmployeeID == shift.EmployeeID);

            if (!employeeExists)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            if (shift.EndTime <= shift.StartTime)
            {
                throw new Exception(
                    "Shift end time must be after start time.");
            }

            // Validate break duration

            if (shift.BreakDuration >=
                (shift.EndTime - shift.StartTime).TotalMinutes)
            {
                throw new Exception(
                    "Break duration cannot be equal to or longer than the shift.");
            }


            // Check employee availability

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

                if (shift.StartTime.TimeOfDay <
                        availability.AvailableFrom ||
                    shift.EndTime.TimeOfDay >
                        availability.AvailableTo)
                {
                    throw new Exception(
                        $"Employee is only available from " +
                        $"{availability.AvailableFrom:hh\\:mm} to " +
                        $"{availability.AvailableTo:hh\\:mm} on " +
                        $"{availability.DayOfWeek}.");
                }
            }


            // Check approved holidays

            var holiday =
            shiftPlannerContext.HolidayRequests
            .FirstOrDefault(h =>
             h.EmployeeID == shift.EmployeeID &&
             h.Status == HolidayRequestStatus.Approved &&
             shift.StartTime.Date <= h.EndDate.Date &&
             shift.EndTime.Date >= h.StartDate.Date);

            if (holiday != null)
            {
                throw new Exception(
                    "Employee is on approved holiday.");
            }


            // Check for overlapping shifts

            var overlappingShift =
                shiftPlannerContext.Shifts.Any(s =>
                    s.EmployeeID == shift.EmployeeID &&
                    shift.StartTime < s.EndTime &&
                    shift.EndTime > s.StartTime);

            if (overlappingShift)
            {
                throw new Exception(
                    "Employee already has a shift during this time.");
            }


            // Save shift

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

            var employeeExists =
            shiftPlannerContext.Employees
            .Any(e => e.EmployeeID == updatedShift.EmployeeID);

            if (!employeeExists)
            {
                throw new Exception(
                    "Employee could not be found.");
            }

            // Validate shift times

            if (updatedShift.EndTime <= updatedShift.StartTime)
            {
                throw new Exception(
                    "Shift end time must be after start time.");
            }

            // Validate break duration

            if (updatedShift.BreakDuration >=
                (updatedShift.EndTime - updatedShift.StartTime).TotalMinutes)
            {
                throw new Exception(
                    "Break duration cannot be equal to or longer than the shift.");
            }


            // Check employee availability

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

                if (updatedShift.StartTime.TimeOfDay <
                        availability.AvailableFrom ||
                    updatedShift.EndTime.TimeOfDay >
                        availability.AvailableTo)
                {
                    throw new Exception(
                        $"Employee is only available from " +
                        $"{availability.AvailableFrom:hh\\:mm} to " +
                        $"{availability.AvailableTo:hh\\:mm} on " +
                        $"{availability.DayOfWeek}.");
                }
            }


            // Check approved holidays

            var holiday =
             shiftPlannerContext.HolidayRequests
             .FirstOrDefault(h =>
             h.EmployeeID == updatedShift.EmployeeID &&
             h.Status == HolidayRequestStatus.Approved &&
             updatedShift.StartTime.Date <= h.EndDate.Date &&
             updatedShift.EndTime.Date >= h.StartDate.Date);

            if (holiday != null)
            {
                throw new Exception(
                    "Employee is on approved holiday.");
            }


            // Check for overlapping shifts

            var overlappingShift =
            shiftPlannerContext.Shifts
            .FirstOrDefault(s =>
            s.EmployeeID == updatedShift.EmployeeID &&
            s.ShiftID != id &&
            updatedShift.StartTime < s.EndTime &&
            updatedShift.EndTime > s.StartTime);

            if (overlappingShift != null)
            {
                throw new Exception(
                    $"Employee already has another shift " +
                    $"from {overlappingShift.StartTime:dd/MM/yyyy HH:mm} " +
                    $"to {overlappingShift.EndTime:dd/MM/yyyy HH:mm}. " +
                    $"You are trying to schedule this shift from " +
                    $"{updatedShift.StartTime:dd/MM/yyyy HH:mm} " +
                    $"to {updatedShift.EndTime:dd/MM/yyyy HH:mm}.");
            }


            // Apply changes

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