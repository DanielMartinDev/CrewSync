using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private List<Shift> shifts = new List<Shift>()
        {
            new Shift
            {
                ShiftID = 1,
                EmployeeID = 1,
                Day = ShiftDay.DayOfWeek.Monday,
                BreakDuration = TimeSpan.FromMinutes(30),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            }
        };

        [HttpGet]
        public ActionResult<List<Shift>> GetShifts()
        {
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public ActionResult<Shift> GetShift(int id)
        {
            var shift = shifts.FirstOrDefault(shift => shift.ShiftID == id);

            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift)
        {
            shifts.Add(shift);

            return CreatedAtAction(
                nameof(GetShift),
                new { id = shift.ShiftID },
                shift);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShift(int id)
        {
            var shift = shifts.FirstOrDefault(s => s.ShiftID == id);
            
            if (shift == null)
                return NotFound();

            shifts.Remove(shift);

            return NoContent();
        }
    }
}