namespace Shift_Planner___API.Models
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
