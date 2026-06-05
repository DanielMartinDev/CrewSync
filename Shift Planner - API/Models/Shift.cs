namespace Shift_Planner___API.Models
{
    public class Shift
    {
        public int shiftID { get; set; }
        public int employeeID { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public TimeSpan breakDuration { get; set; }
        public ShiftDay day { get; set; }
    }
}