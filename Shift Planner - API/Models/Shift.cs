using System.ComponentModel.DataAnnotations;

namespace Shift_Planner___API.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now.AddHours(9);
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(17);

        [Required]
        [Range(0, 180)]
        public int BreakDuration { get; set; }
        public double ShiftHours =>
            (EndTime - StartTime).TotalHours
            - (BreakDuration / 60.0);

        public double ShiftLength => (EndTime - StartTime).TotalHours;
        public string? Notes { get; set; }
    }
}