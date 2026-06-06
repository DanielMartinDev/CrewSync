using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private List<Employee> employees = new List<Employee>()
            {
                new Employee
                {
                    EmployeeID = 1,
                    Role = EmployeeRole.Role.Store_Manager,
                    Name = "Daniel",
                    WeeklyHours = 40,
                    StartDate = DateTime.Now
                },
                new Employee
                {
                    EmployeeID = 2,
                    Role = EmployeeRole.Role.Customer_Assistant,
                    Name = "Bob",
                    WeeklyHours = 35,
                    StartDate = DateTime.Now
                }
            };

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = employees.FirstOrDefault(employee => employee.EmployeeID == id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }       

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            employees.Add(employee);

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.EmployeeID },
                employee);
        }
    }
}