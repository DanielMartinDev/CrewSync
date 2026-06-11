using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner_Web.Models;
using ShiftPlanner_Web.Models;
using ShiftPlanner_Web.ViewModels;

[Authorize(Roles = "Admin, Manager")]
public class AvailabilityController : Controller
{
    private readonly HttpClient _httpClient;

    public AvailabilityController(
        IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    public async Task<IActionResult> Index(int employeeId)
    {
        var availability =
            await _httpClient
                .GetFromJsonAsync<List<Availability>>
                (
                    $"https://localhost:7255/api/Availability/employee/{employeeId}"
                );

        ViewBag.EmployeeId = employeeId;

        return View(availability);
    }

    [HttpPost]
    public async Task<IActionResult> Index(
    List<Availability> availability)
    {
        foreach (var day in availability)
        {
            await _httpClient.PutAsJsonAsync(
                $"https://localhost:7255/api/Availability/{day.AvailabilityID}",
                day);
        }

        return RedirectToAction(
        "Details",
        "Employee",
        new
        {
            id = availability.First().EmployeeID
        });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int employeeId)
    {
        var availability =
            await _httpClient.GetFromJsonAsync<List<Availability>>
            (
                $"https://localhost:7255/api/Availability/employee/{employeeId}"
            );

        var employee =
            await _httpClient.GetFromJsonAsync<Employee>
            (
                $"https://localhost:7255/api/Employee/{employeeId}"
            );

        var model =
            new AvailabilityEditViewModel
            {
                EmployeeID = employeeId,
                EmployeeName = employee.Name,
                Availability = availability
            };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var request =
            await _httpClient.GetFromJsonAsync<HolidayRequest>
            (
                $"https://localhost:7255/api/HolidayRequest/{id}"
            );

        if (request == null)
            return NotFound();

        request.Status = HolidayRequestStatus.Approved;

        await _httpClient.PutAsJsonAsync(
            $"https://localhost:7255/api/HolidayRequest/{id}",
            request);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var request =
            await _httpClient.GetFromJsonAsync<HolidayRequest>
            (
                $"https://localhost:7255/api/HolidayRequest/{id}"
            );

        if (request == null)
            return NotFound();

        request.Status = HolidayRequestStatus.Rejected;

        await _httpClient.PutAsJsonAsync(
            $"https://localhost:7255/api/HolidayRequest/{id}",
            request);

        return RedirectToAction(nameof(Index));
    }
}