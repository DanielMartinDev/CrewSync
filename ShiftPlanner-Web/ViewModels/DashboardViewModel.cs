using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.ViewModels
{
    public class DashboardViewModel
    {
        public int EmployeeCount { get; set; }
        public int ShiftCount { get; set; }

        public double TotalHours { get; set; }

        public List<Shift> UpcomingShifts { get; set; } = new();
        public List<Employee> OverHoursEmployees { get; set; } = new();
        public List<Employee> NoShiftEmployees { get; set; } = new();
    }
}