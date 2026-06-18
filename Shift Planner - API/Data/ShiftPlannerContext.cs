using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Models;

namespace Shift_Planner___API.Data
{
    public class ShiftPlannerContext : IdentityDbContext<ApplicationUser>
    {
        public ShiftPlannerContext(DbContextOptions<ShiftPlannerContext> options) 
            : base(options) { }
        protected override void OnModelCreating(
        ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>()
                .Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone");

            builder.Entity<Employee>()
                 .Property(e => e.StartDate)
                 .HasColumnType("timestamp without time zone");

            builder.Entity<Shift>()
                .Property(s => s.StartTime)
                .HasColumnType("timestamp without time zone");

            builder.Entity<Shift>()
                .Property(s => s.EndTime)
                .HasColumnType("timestamp without time zone");

            builder.Entity<HolidayRequest>()
                .Property(h => h.StartDate)
                .HasColumnType("timestamp without time zone");

            builder.Entity<HolidayRequest>()
                .Property(h => h.EndDate)
                .HasColumnType("timestamp without time zone");
        }

        public DbSet<Employee> Employees {  get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<HolidayRequest> HolidayRequests { get; set; }
    }
}