using CrewSync.Website.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrewSync.Website.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Waitlist()
    {

        var entries =
            await _context.WaitlistEntries
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

        return View(entries);
    }
}