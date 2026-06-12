using Shift_Planner___API.Data;
using System.ComponentModel.DataAnnotations;

namespace Shift_Planner___API.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }
        public string? UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100,
            ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate {  get; set; }

        [Display(Name = "Contracted Hours")]
        [Range(1, 60)]
        public int WeeklyHours { get; set; }

        [Required]
        public EmployeeRole.Role Role { get; set; }
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();

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