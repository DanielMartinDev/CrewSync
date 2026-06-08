using Microsoft.AspNetCore.Mvc;
using Shift_Planner_Web.Models;
using ShiftPlanner_Web.Models;
using ShiftPlanner_Web.ViewModels;
using System.Diagnostics;

namespace ShiftPlanner_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _httpClient = factory.CreateClient();
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Dashboard));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var employees =
            await _httpClient.GetFromJsonAsync<List<Employee>>
            (
                "https://localhost:7255/api/Employee"
            );

            var shifts =
            await _httpClient.GetFromJsonAsync<List<Shift>>
            (
                "https://localhost:7255/api/Shift"
            );

            var model = new DashboardViewModel
            {
                EmployeeCount = employees.Count,
                ShiftCount = shifts.Count,

                UpcomingShifts = shifts
                .OrderBy(s => s.Day)
                .ThenBy(s => s.StartTime)
                .Take(10)
                .ToList()
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
