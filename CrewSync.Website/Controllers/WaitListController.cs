using CrewSync.Website.Data;
using CrewSync.Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrewSync.Website.Controllers;

public class WaitlistController : Controller
{
    private readonly ApplicationDbContext _context;

    public WaitlistController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Join(
    WaitlistEntry entry)
    {
        entry.Notes ??= string.Empty;

        entry.CreatedAt =
            DateTime.UtcNow;

        _context.WaitlistEntries.Add(entry);

        await _context.SaveChangesAsync();

        return RedirectToAction(
            "Index",
            "Home");
    }
}