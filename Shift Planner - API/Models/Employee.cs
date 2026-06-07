using System.ComponentModel.DataAnnotations;

namespace Shift_Planner___API.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100,
            ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate {  get; set; }

        [Range(1, 60)]
        public int WeeklyHours { get; set; }

        [Required]
        public EmployeeRole.Role Role { get; set; }
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}