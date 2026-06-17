namespace Shift_Planner___API.Models
{
    public class HolidayRequest
    {
        public int HolidayRequestID { get; set; }
        public int EmployeeID { get; set; }

        public Employee? Employee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public HolidayRequestStatus Status { get; set; }
        public string? ManagerNotes { get; set; }
    }
}