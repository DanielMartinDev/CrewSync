using Microsoft.AspNetCore.Mvc;
using Shift_Planner_Web.Models;
using ShiftPlanner_Web.Models;

namespace ShiftPlanner_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var employees =
            await _httpClient.GetFromJsonAsync<List<Employee>>
            (
                "https://localhost:7255/api/Employee"
            );

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var response =
                await _httpClient.PostAsJsonAsync("https://localhost:7255/api/Employee", employee);

            if (!response.IsSuccessStatusCode)
                return View(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>
            (
                 $"https://localhost:7255/api/Employee/{id}"
            );

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            var response =
        await _httpClient.PutAsJsonAsync(
            $"https://localhost:7255/api/Employee/{employee.EmployeeID}",
            employee);

            if (!response.IsSuccessStatusCode)
                return View(employee);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>(
                            $"https://localhost:7255/api/Employee/{id}/schedule"
                );

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>
            (
                     $"https://localhost:7255/api/Employee/{id}"
            );

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee employee)
        {
            await _httpClient.DeleteAsync(
                 $"https://localhost:7255/api/Employee/{employee.EmployeeID}");

            return RedirectToAction(nameof(Index));
        }
    }
}