using Shift_Planner___API.Data;

namespace Shift_Planner_Web.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? UserId { get; set; }
        public string Email { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }
        public string Name { get; set; } = "";

        public DateTime StartDate { get; set; }

        public int WeeklyHours { get; set; }

        public EmployeeRole.Role Role { get; set; }

        public List<Shift> Shifts { get; set; } = new();

        public double ScheduledHours =>
        Shifts.Sum(s => s.ShiftHours);

        public double RemainingHours =>
            WeeklyHours - ScheduledHours;

        public double Utilisation =>
            WeeklyHours == 0
                ? 0
                : (ScheduledHours / WeeklyHours) * 100;
    }
}