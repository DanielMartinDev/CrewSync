using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Data;
using Shift_Planner_Web.Models;
using ShiftPlanner_Web.Models;

namespace ShiftPlanner_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(IHttpClientFactory factory, UserManager<ApplicationUser> userManager)
        {
            _httpClient = factory.CreateClient();
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var employees =
                await _httpClient.GetFromJsonAsync<List<Employee>>
                (
                    "https://localhost:7255/api/Employee"
                ) ?? new();

            var shifts =
                await _httpClient.GetFromJsonAsync<List<Shift>>
                (
                    "https://localhost:7255/api/Shift"
                ) ?? new();

            foreach (var employee in employees)
            {
                employee.Shifts = shifts
                    .Where(s => s.EmployeeID == employee.EmployeeID)
                    .ToList();
            }

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
                await _httpClient.PostAsJsonAsync(
                    "https://localhost:7255/api/Employee",
                    employee);

            if (!response.IsSuccessStatusCode)
                return View(employee);

            var createdEmployee =
                await response.Content
                    .ReadFromJsonAsync<Employee>();

            var tempPassword = "Welcome123!";

            var user = new ApplicationUser
            {
                UserName = employee.Email,
                Email = employee.Email,
                MustChangePassword = true
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    tempPassword);

            if (!result.Succeeded)
            {
                ViewBag.Error =
                    string.Join(
                        ", ",
                        result.Errors.Select(
                            e => e.Description));

                return View(employee);
            }

            if (result.Succeeded)
            {
                string identityRole;

                switch (employee.Role)
                {
                    case EmployeeRole.Role.Customer_Assistant:
                    case EmployeeRole.Role.Shift_Manager:
                        identityRole = "Employee";
                        break;

                    case EmployeeRole.Role.Deputy_Manager:
                        identityRole = "Manager";
                        break;

                    case EmployeeRole.Role.Store_Manager:
                        identityRole = "Admin";
                        break;

                    default:
                        identityRole = "Employee";
                        break;
                }

                await _userManager.AddToRoleAsync(
                    user,
                    identityRole);

                createdEmployee!.UserId = user.Id;

                await _httpClient.PutAsJsonAsync(
                    $"https://localhost:7255/api/Employee/{createdEmployee.EmployeeID}",
                    createdEmployee);
            }

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
            var employee = await _httpClient.GetFromJsonAsync<Employee>
                (
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
            if (!string.IsNullOrEmpty(employee.UserId))
            {
                var user =
                    await _userManager.FindByIdAsync(
                        employee.UserId);

                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            await _httpClient.DeleteAsync(
                $"https://localhost:7255/api/Employee/{employee.EmployeeID}");

            return RedirectToAction(nameof(Index));
        }
    }
}