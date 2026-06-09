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

        public async Task<IActionResult> Dashboard(DateTime? weekStart)
        {
            weekStart ??= DateTime.Today;

            var monday = weekStart.Value;

            while (monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(-1);
            }

            var employees =
                await _httpClient.GetFromJsonAsync<List<Employee>>
                (
                    "https://localhost:7255/api/Employee"
                ) ?? new List<Employee>();

            var shifts =
                await _httpClient.GetFromJsonAsync<List<Shift>>
                (
                    "https://localhost:7255/api/Shift"
                ) ?? new List<Shift>();

            var totalHours =
            Math.Round(
            shifts.Sum(s => s.ShiftHours),
            1);

            var averageContractHours =
            employees.Any()
            ? Math.Round(
            employees.Average(e => e.WeeklyHours),
            1) : 0;

            var overHoursEmployees = employees.Where(employee =>
            shifts.Where(s => s.EmployeeID == employee.EmployeeID)
            .Sum(s => s.ShiftHours) > employee.WeeklyHours)
            .ToList();

            var noShiftEmployees = employees
                .Where(employee =>
                !shifts.Any(
                shift => shift.EmployeeID == employee.EmployeeID))
                .ToList();

            var weekEnd = monday.AddDays(7);
            var weekShifts = shifts
                .Where(s =>
                    s.StartTime >= monday &&
                    s.StartTime < weekEnd)
                .ToList();

            _logger.LogInformation(
                "Week Start: {WeekStart}",
                monday);

            var model = new DashboardViewModel
            {
                WeekStart = monday,

                EmployeeCount = employees.Count,
                ShiftCount = shifts.Count,

                TotalHours = totalHours,
                AverageContractHours = averageContractHours,

                UpcomingShifts = weekShifts,

                OverHoursEmployees = overHoursEmployees,
                NoShiftEmployees = noShiftEmployees,

                Employees = employees
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
