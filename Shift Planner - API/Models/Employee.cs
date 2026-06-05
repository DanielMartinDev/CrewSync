namespace Shift_Planner___API.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate {  get; set; }
        public int WeeklyHours { get; set; }
        public EmployeeRole.Role Role { get; set; }
    }
}