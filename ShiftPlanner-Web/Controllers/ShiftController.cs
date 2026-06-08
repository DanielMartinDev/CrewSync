using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.Controllers
{
    public class ShiftController : Controller
    {
        private readonly HttpClient _httpClient;

        public ShiftController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var shifts = await _httpClient.GetFromJsonAsync<List<Shift>>
            (
                "https://localhost:7255/api/Shift"
            );

            return View(shifts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadEmployees();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shift shift)
        {
            var response = 
                await _httpClient.PostAsJsonAsync("https://localhost:7255/api/Shift", shift);

            if (!response.IsSuccessStatusCode)
                return View(shift);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var shift = await _httpClient.GetFromJsonAsync<Shift>
                (
                    $"https://localhost:7255/api/Shift/{id}"
                );

            await LoadEmployees();

            if (shift == null)
                return NotFound();

            return View(shift);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shift shift)
        {
            var response =
            await _httpClient.PutAsJsonAsync(
            $"https://localhost:7255/api/Shift/{shift.ShiftID}",
            shift);

            await LoadEmployees();

            if (!response.IsSuccessStatusCode)
                return View(shift);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var shift = await _httpClient.GetFromJsonAsync<Shift>
                (
                       $"https://localhost:7255/api/Shift/{id}"
                );

            if (shift == null)
                return NotFound();

            return View(shift);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var shift = await _httpClient.GetFromJsonAsync<Shift>
            (
                     $"https://localhost:7255/api/Shift/{id}"
            );

            return View(shift);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Shift shift)
        {
            await _httpClient.DeleteAsync(
                 $"https://localhost:7255/api/Shift/{shift.ShiftID}");

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadEmployees()
        {

            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>
                (
                    "https://localhost:7255/api/Employee"
                );

            ViewBag.Employees =
                new SelectList(
                    employees,
                    "EmployeeID",
                    "Name");
        }
    }
}