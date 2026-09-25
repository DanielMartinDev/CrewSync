namespace Shift_Planner_Web.Models
{
    public class HolidayRequest
    {
        public int HolidayRequestID { get; set; }
        public int EmployeeID { get; set; }

        public Employee? Employee { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public HolidayRequestStatus Status { get; set; }

        public string? ManagerNotes { get; set; }
    }
}