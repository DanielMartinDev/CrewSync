using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner_Web.Models;

namespace Shift_Planner_Web.Controllers
{
    [Authorize]
    public class HolidayRequestController : Controller
    {
        private readonly HttpClient _httpClient;

        public HolidayRequestController(
            IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var holidayRequests =
                await _httpClient.GetFromJsonAsync<List<HolidayRequest>>
                (
                    "https://localhost:7255/api/HolidayRequest"
                );

            return View(holidayRequests);
        }

        [HttpGet]
        public IActionResult Create(int employeeId)
        {
            var holidayRequest = new HolidayRequest
            {
                EmployeeID = employeeId
            };

            return View(holidayRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        HolidayRequest holidayRequest)
        {
            holidayRequest.Status =
                HolidayRequestStatus.Pending;

            var response =
                await _httpClient.PostAsJsonAsync(
                    "https://localhost:7255/api/HolidayRequest",
                    holidayRequest);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    await response.Content.ReadAsStringAsync();

                return View(holidayRequest);
            }

            if (User.IsInRole("Employee"))
            {
                return RedirectToAction(
                    "Index",
                    "EmployeePortal");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var holidayRequest =
                await _httpClient.GetFromJsonAsync<HolidayRequest>
                (
                    $"https://localhost:7255/api/HolidayRequest/{id}"
                );

            if (holidayRequest == null)
                return NotFound();

            return View(holidayRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            HolidayRequest holidayRequest)
        {
            var response =
                await _httpClient.PutAsJsonAsync(
                    $"https://localhost:7255/api/HolidayRequest/{holidayRequest.HolidayRequestID}",
                    holidayRequest);

            if (!response.IsSuccessStatusCode)
                return View(holidayRequest);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var holidayRequest =
                await _httpClient.GetFromJsonAsync<HolidayRequest>
                (
                    $"https://localhost:7255/api/HolidayRequest/{id}"
                );

            if (holidayRequest == null)
                return NotFound();

            holidayRequest.Status =
                HolidayRequestStatus.Approved;

            await _httpClient.PutAsJsonAsync(
                $"https://localhost:7255/api/HolidayRequest/{id}",
                holidayRequest);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var holidayRequest =
                await _httpClient.GetFromJsonAsync<HolidayRequest>
                (
                    $"https://localhost:7255/api/HolidayRequest/{id}"
                );

            if (holidayRequest == null)
                return NotFound();

            holidayRequest.Status =
                HolidayRequestStatus.Rejected;

            await _httpClient.PutAsJsonAsync(
                $"https://localhost:7255/api/HolidayRequest/{id}",
                holidayRequest);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var holidayRequest =
                await _httpClient.GetFromJsonAsync<HolidayRequest>
                (
                    $"https://localhost:7255/api/HolidayRequest/{id}"
                );

            if (holidayRequest == null)
                return NotFound();

            return View(holidayRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(
            HolidayRequest holidayRequest)
        {
            await _httpClient.DeleteAsync(
                $"https://localhost:7255/api/HolidayRequest/{holidayRequest.HolidayRequestID}");

            if (User.IsInRole("Employee"))
            {
                return RedirectToAction(
                    "Index",
                    "EmployeePortal");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}