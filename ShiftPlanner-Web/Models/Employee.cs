using Shift_Planner___API.Models;

namespace ShiftPlanner_Web.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; } = "";

        public DateTime StartDate { get; set; }

        public int WeeklyHours { get; set; }

        public EmployeeRole.Role EmployeeRole { get; set; }

        public List<Shift> Shifts { get; set; } = new();
    }
}