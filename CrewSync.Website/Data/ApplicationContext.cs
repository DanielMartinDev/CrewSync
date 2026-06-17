using CrewSync.Website.Models;
using Microsoft.EntityFrameworkCore;

namespace CrewSync.Website.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WaitlistEntry> WaitlistEntries
        => Set<WaitlistEntry>();
}