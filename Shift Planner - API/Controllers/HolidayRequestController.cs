using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Models;
using Shift_Planner___API.Services;

namespace Shift_Planner___API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayRequestController : ControllerBase
    {
        private readonly HolidayRequestService _holidayRequestService;

        public HolidayRequestController(
            HolidayRequestService holidayRequestService)
        {
            _holidayRequestService = holidayRequestService;
        }

        [HttpGet]
        public ActionResult<List<HolidayRequest>>
            GetHolidayRequests()
        {
            return Ok(
                _holidayRequestService
                    .GetHolidayRequests());
        }

        [HttpGet("{id}")]
        public ActionResult<HolidayRequest>
            GetHolidayRequest(int id)
        {
            var holidayRequest =
                _holidayRequestService
                    .GetHolidayRequest(id);

            if (holidayRequest == null)
                return NotFound();

            return Ok(holidayRequest);
        }

        [HttpPost]
        public ActionResult<HolidayRequest>
            CreateHolidayRequest(
                HolidayRequest holidayRequest)
        {
            try
            {
                var createdHolidayRequest =
                    _holidayRequestService
                        .CreateHolidayRequest(
                            holidayRequest);

                return CreatedAtAction(
                    nameof(GetHolidayRequest),
                    new
                    {
                        id = createdHolidayRequest.HolidayRequestID
                    },
                    createdHolidayRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHolidayRequest(
            int id,
            HolidayRequest updatedHolidayRequest)
        {
            try
            {
                if (!_holidayRequestService
                    .UpdateHolidayRequest(
                        id,
                        updatedHolidayRequest))
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHolidayRequest(
            int id)
        {
            if (!_holidayRequestService
                .DeleteHolidayRequest(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}