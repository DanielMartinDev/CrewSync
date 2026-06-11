using ShiftPlanner_Web.Models;

namespace ShiftPlanner_Web.ViewModels
{
    public class AvailabilityEditViewModel
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; } = "";

        public List<Availability> Availability { get; set; } = new();
    }
}
