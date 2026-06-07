using System.ComponentModel.DataAnnotations;

namespace Shift_Planner___API.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [Required]
        [Range(0, 180)]
        public int BreakDuration { get; set; }
        public ShiftDay.DayOfWeek Day { get; set; }
    }
}