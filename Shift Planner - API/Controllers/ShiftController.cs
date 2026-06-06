using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;
using Shift_Planner___API.Services;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftService _shiftService;

        public ShiftController(ShiftService shiftService) 
        { 
            _shiftService = shiftService;
        }

        [HttpGet]
        public ActionResult<List<Shift>> GetShifts()
        {
            return Ok(_shiftService.GetShifts());
        }

        [HttpGet("{id}")]
        public ActionResult<Shift> GetShift(int id)
        {
            var shift = _shiftService.GetShift(id);

            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift)
        {
            var createdShift = _shiftService.CreateShift(shift);

            return CreatedAtAction(
                nameof(GetShift),
                new { id = createdShift.ShiftID },
                createdShift);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShift(int id)
        {
            if (!_shiftService.DeleteShift(id))
                return NotFound();

            return NoContent();
        }
    }
}