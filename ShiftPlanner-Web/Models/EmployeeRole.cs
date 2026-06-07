using System.ComponentModel.DataAnnotations;

namespace Shift_Planner_Web.Models
{
    public class EmployeeRole
    {
        public enum Role
        {
            [Display(Name = "Customer Assistant")]
            Customer_Assistant,

            [Display(Name = "Team Lead")]
            Shift_Manager,

            [Display(Name = "Deputy Store Manager")]
            Deputy_Manager,

            [Display(Name = "Store Manager")]
            Store_Manager
        }

        public Role role;
    }
}
