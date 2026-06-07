using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _httpClient.GetFromJsonAsync<Employee>(
                            $"https://localhost:7255/api/Employee/{id}/schedule"
                );

            if (employee == null)
                return NotFound();

            return View(employee);
        }
    }
}