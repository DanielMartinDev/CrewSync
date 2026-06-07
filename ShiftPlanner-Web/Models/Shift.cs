using Shift_Planner___API.Models;

namespace ShiftPlanner_Web.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }

        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int BreakDuration { get; set; }

        public int Day { get; set; }
    }
}