using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.ViewModels
{
    public class DashboardViewModel
    {
        public int EmployeeCount { get; set; }
        public int ShiftCount { get; set; }

        public List<Shift> UpcomingShifts { get; set; } = new();
    }
}