using Microsoft.AspNetCore.Mvc;
using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly HttpClient _httpClient;

        public AbsenceController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var absences =
                await _httpClient.GetFromJsonAsync<List<Absence>>(
                    "https://localhost:7255/api/Absence")
                ?? new List<Absence>();

            return View(absences);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees =
                await _httpClient.GetFromJsonAsync<List<Employee>>(
                    "https://localhost:7255/api/Employee")
                ?? new List<Employee>();

            ViewBag.Employees = employees;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Absence absence)
        {
            if (absence.EndDate < absence.StartDate)
            {
                ModelState.AddModelError(
                    nameof(absence.EndDate),
                    "End date cannot be before the start date.");
            }

            if (!ModelState.IsValid)
            {
                var employees =
                    await _httpClient.GetFromJsonAsync<List<Employee>>(
                        "https://localhost:7255/api/Employee")
                    ?? new List<Employee>();

                ViewBag.Employees = employees;

                return View(absence);
            }

            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7255/api/Absence",
                absence);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    $"Unable to record the absence: {error}");

                var employees =
                    await _httpClient.GetFromJsonAsync<List<Employee>>(
                        "https://localhost:7255/api/Employee")
                    ?? new List<Employee>();

                ViewBag.Employees = employees;

                return View(absence);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var absence =
                await _httpClient.GetFromJsonAsync<Absence>(
                    $"https://localhost:7255/api/Absence/{id}");

            if (absence == null)
                return NotFound();

            var employees =
                await _httpClient.GetFromJsonAsync<List<Employee>>(
                    "https://localhost:7255/api/Employee")
                ?? new List<Employee>();

            ViewBag.Employees = employees;

            return View(absence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    Absence absence)
        {
            if (absence.EndDate < absence.StartDate)
            {
                ModelState.AddModelError(
                    nameof(absence.EndDate),
                    "End date cannot be before the start date.");
            }

            if (!ModelState.IsValid)
            {
                var employees =
                    await _httpClient.GetFromJsonAsync<List<Employee>>(
                        "https://localhost:7255/api/Employee")
                    ?? new List<Employee>();

                ViewBag.Employees = employees;

                return View(absence);
            }

            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7255/api/Absence/{id}",
                absence);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ModelState.AddModelError(
                    string.Empty,
                    $"Unable to update the absence: {error}");

                var employees =
                    await _httpClient.GetFromJsonAsync<List<Employee>>(
                        "https://localhost:7255/api/Employee")
                    ?? new List<Employee>();

                ViewBag.Employees = employees;

                return View(absence);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var absence =
                await _httpClient.GetFromJsonAsync<Absence>(
                    $"https://localhost:7255/api/Absence/{id}");

            if (absence == null)
                return NotFound();

            return View(absence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"https://localhost:7255/api/Absence/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to delete the absence.");

                var absence =
                    await _httpClient.GetFromJsonAsync<Absence>(
                        $"https://localhost:7255/api/Absence/{id}");

                return View(absence);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}