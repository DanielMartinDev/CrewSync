namespace Shift_Planner___API.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan BreakDuration { get; set; }
        public ShiftDay.DayOfWeek Day { get; set; }
    }
}