namespace ShiftPlanner_Web.Models
{
    public class EmployeeHolidayDto
    {
        public int EmployeeID { get; set; }

        public int HolidayAllowance { get; set; }

        public int HolidayDaysUsed { get; set; }

        public int HolidayDaysRemaining { get; set; }
    }
}