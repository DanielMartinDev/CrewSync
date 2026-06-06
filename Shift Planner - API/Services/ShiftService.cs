using Shift_Planner___API.Models;

namespace Shift_Planner___API.Services
{
    public class ShiftService
    {
        private readonly List<Shift> shifts;

        public ShiftService()
        {
            shifts = new List<Shift>();
        }

        public List<Shift> GetShifts()
        {
            return shifts;
        }

        public Shift? GetShift(int id)
        {
            return shifts.FirstOrDefault(
                s => s.ShiftID == id);
        }

        public Shift CreateShift(Shift shift)
        {
            shifts.Add(shift);

            return shift;
        }

        public bool DeleteShift(int id)
        {
            var shift = shifts.FirstOrDefault(s => s.ShiftID == id);

            if (shift == null)
                return false;

            shifts.Remove(shift);
            return true;
        }
    }
}
