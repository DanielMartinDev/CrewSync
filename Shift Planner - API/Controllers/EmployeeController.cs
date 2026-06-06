using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;
using Shift_Planner___API.Services;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            var createdEmployee = _employeeService.CreateEmployee(employee);

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.EmployeeID },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (!_employeeService.UpdateEmployee(id, updatedEmployee))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!_employeeService.DeleteEmployee(id))
                return NotFound();

            return NoContent();
        }
    }
}