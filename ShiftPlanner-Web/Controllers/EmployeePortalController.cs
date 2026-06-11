using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Data;
using Shift_Planner_Web.Models;

namespace ShiftPlanner_Web.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeePortalController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeePortalController(
            IHttpClientFactory factory,
            UserManager<ApplicationUser> userManager)
        {
            _httpClient = factory.CreateClient();
            _userManager = userManager;
        }

        private async Task<Employee?> GetCurrentEmployee()
        {
            var user =
                await _userManager.GetUserAsync(User);

            var employees =
                await _httpClient.GetFromJsonAsync<List<Employee>>
                (
                    "https://localhost:7255/api/Employee"
                );

            return employees?
                .FirstOrDefault(
                    e => e.UserId == user!.Id);
        }

        public async Task<IActionResult> Index()
        {
            var employee =
                await GetCurrentEmployee();

            if (employee == null)
                return NotFound();

            var shifts =
                await _httpClient.GetFromJsonAsync<List<Shift>>
                (
                    "https://localhost:7255/api/Shift"
                ) ?? new();

            var holidays =
                await _httpClient.GetFromJsonAsync<List<HolidayRequest>>
                (
                    "https://localhost:7255/api/HolidayRequest"
                ) ?? new();

            ViewBag.Employee = employee;

            ViewBag.Holidays =
                holidays.Where(
                    h => h.EmployeeID ==
                         employee.EmployeeID)
                .OrderByDescending(
                    h => h.StartDate)
                .ToList();

            return View(
                shifts.Where(
                    s => s.EmployeeID ==
                         employee.EmployeeID)
                .OrderBy(
                    s => s.StartTime)
                .ToList());
        }
    }
}