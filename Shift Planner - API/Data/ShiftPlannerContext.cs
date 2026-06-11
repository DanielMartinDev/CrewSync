using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Data
{
    public class ShiftPlannerContext : IdentityDbContext<ApplicationUser>
    {
        public ShiftPlannerContext(DbContextOptions<ShiftPlannerContext> options) 
            : base(options) { }

        public DbSet<Employee> Employees {  get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<HolidayRequest> HolidayRequests { get; set; }
    }
}