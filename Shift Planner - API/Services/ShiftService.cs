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

            shift.EmployeeID = updatedShift.EmployeeID;
            shift.StartTime = updatedShift.StartTime;
            shift.EndTime = updatedShift.EndTime;
            shift.BreakDuration = updatedShift.BreakDuration;
            shift.Day = updatedShift.Day;

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