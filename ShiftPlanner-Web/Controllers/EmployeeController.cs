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
    }
}