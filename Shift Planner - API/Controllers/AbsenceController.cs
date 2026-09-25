using Microsoft.AspNetCore.Mvc;
using Shift_Planner_API.Models;
using Shift_Planner_API.Services;

namespace Shift_Planner_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsenceController : ControllerBase
    {
        private readonly AbsenceService absenceService;

        public AbsenceController(AbsenceService service)
        {
            absenceService = service;
        }

        [HttpGet]
        public ActionResult<List<Absence>> GetAbsences()
        {
            return absenceService.GetAbsences();
        }

        [HttpGet("{id}")]
        public ActionResult<Absence> GetAbsence(int id)
        {
            var absence = absenceService.GetAbsence(id);

            if (absence == null)
                return NotFound();

            return absence;
        }

        [HttpPost]
        public ActionResult<Absence> CreateAbsence(Absence absence)
        {
            try
            {
                var createdAbsence =
                    absenceService.CreateAbsence(absence);

                return CreatedAtAction(
                    nameof(GetAbsence),
                    new { id = createdAbsence.AbsenceID },
                    createdAbsence);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAbsence(
            int id,
            Absence absence)
        {
            try
            {
                var updated =
                    absenceService.UpdateAbsence(id, absence);

                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbsence(int id)
        {
            var deleted =
                absenceService.DeleteAbsence(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}