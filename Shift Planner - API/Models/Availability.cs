using Shift_Planner___API.Models;

namespace Shift_Planner___API.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public int EmployeeID { get; set; }
        public bool IsAvailable { get; set; }
        public Employee? Employee { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan AvailableFrom { get; set; }
        public TimeSpan AvailableTo { get; set; }
    }
}