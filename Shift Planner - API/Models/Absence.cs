using Shift_Planner___API.Models;
using Shift_Planner_API.Data;

namespace Shift_Planner_API.Models
{
    public class Absence
    {
        public int AbsenceID { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public AbsenceType Type { get; set; } = AbsenceType.Sickness;
        public string Notes { get; set; } = string.Empty;
    }
}