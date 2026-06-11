using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;
using Shift_Planner___API.Services;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly AvailabilityService _availabilityService;

        public AvailabilityController(
            AvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(
                _availabilityService.GetAvailabilities());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var availability =
                _availabilityService.GetAvailability(id);

            if (availability == null)
                return NotFound();

            return Ok(availability);
        }

        [HttpGet("employee/{employeeId}")]
        public IActionResult GetByEmployee(int employeeId)
        {
            var availability =
                _availabilityService
                    .GetAvailabilityByEmployee(employeeId);

            return Ok(availability);
        }

        [HttpPost]
        public IActionResult Create(
            Availability availability)
        {
            var createdAvailability =
                _availabilityService
                    .CreateAvailability(availability);

            return Ok(createdAvailability);
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            Availability availability)
        {
            var updated =
                _availabilityService
                    .UpdateAvailability(
                        id,
                        availability);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted =
                _availabilityService
                    .DeleteAvailability(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}