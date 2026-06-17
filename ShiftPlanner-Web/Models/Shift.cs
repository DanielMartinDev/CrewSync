namespace Shift_Planner_Web.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }

        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int BreakDuration { get; set; }

        public double ShiftHours =>
            (EndTime - StartTime).TotalHours
            - (BreakDuration / 60.0);

        public double ShiftLength => (EndTime - StartTime).TotalHours;

        public string? Notes { get; set; }
    }
}