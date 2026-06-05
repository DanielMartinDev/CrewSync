namespace Shift_Planner___API.Models
{
    public class EmployeeRole
    {
        public enum Role
        {
            Customer_Assistant,
            Shift_Manager,
            Deputy_Manager,
            Store_Manager
        }

        public Role employeeRole;
    }
}
