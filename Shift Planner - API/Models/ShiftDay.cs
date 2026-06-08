namespace Shift_Planner___API.Models
{
    public class ShiftDay
    {
        public enum DayOfWeek
        {
            Monday = 1,
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
