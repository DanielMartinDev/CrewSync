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

        [HttpGet("{id}/employee")]
        public ActionResult<Shift> GetEmployeeFromShift(int id)
        {
            var shift = _shiftService.GetShiftWithEmployee(id);

            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift)
        {
            try
            {
                var createdShift =
                    _shiftService.CreateShift(shift);

                return CreatedAtAction(
                    nameof(GetShift),
                    new { id = createdShift.ShiftID },
                    createdShift);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShift(int id, Shift updatedShift)
        {
            try
            {
                if (!_shiftService.UpdateShift(id, updatedShift))
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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