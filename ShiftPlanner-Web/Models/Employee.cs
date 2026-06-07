namespace Shift_Planner_Web.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; } = "";

        public DateTime StartDate { get; set; }

        public int WeeklyHours { get; set; }

        public EmployeeRole.Role Role { get; set; }

        public List<Shift> Shifts { get; set; } = new();
    }
}