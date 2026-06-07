namespace ShiftPlanner_Web.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; } = "";

        public DateTime StartDate { get; set; }

        public int WeeklyHours { get; set; }

        public int Role { get; set; }
    }
}