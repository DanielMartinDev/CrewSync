namespace Shift_Planner_Web.Models
{
    public class ShiftDay
    {
        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        public DayOfWeek weekday;
    }
}
