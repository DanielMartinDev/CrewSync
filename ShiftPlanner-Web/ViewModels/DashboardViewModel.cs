using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.ViewModels
{
    public class DashboardViewModel
    {
        public int EmployeeCount { get; set; }
        public int ShiftCount { get; set; }

        public double TotalHours { get; set; }
        public DateTime WeekStart { get; set; }

        public double AverageContractHours { get; set; }
        public List<Shift> UpcomingShifts { get; set; } = new();
        public List<Employee> OverHoursEmployees { get; set; } = new();
        public List<Employee> NoShiftEmployees { get; set; } = new();
        public List<HolidayRequest> HolidayRequests { get; set; } = new();
        public List<HolidayRequest> PendingHolidayRequests
        {
            get;
            set;
        } = new();
        public List<Employee> Employees { get; set; } = new();
    }
}