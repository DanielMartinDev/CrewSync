using Microsoft.AspNetCore.Identity;

namespace Shift_Planner___API.Data
{
    public class SeedRoles
    {
        public static async Task Seed(IServiceProvider services)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles =
            {
                "Admin",
                "Manager",
                "Employee"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            var adminUser =
    await userManager.FindByEmailAsync(
        "admin@shiftplanner.com");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@shiftplanner.com",
                    Email = "admin@shiftplanner.com"
                };

                await userManager.CreateAsync(
                    adminUser,
                    "Admin123!");

                await userManager.AddToRoleAsync(
                    adminUser,
                    "Admin");
            }

            var managerUser =
                await userManager.FindByEmailAsync(
                    "manager@shiftplanner.com");

            if (managerUser == null)
            {
                managerUser = new ApplicationUser
                {
                    UserName = "manager@shiftplanner.com",
                    Email = "manager@shiftplanner.com"
                };

                await userManager.CreateAsync(
                    managerUser,
                    "Manager123!");

                await userManager.AddToRoleAsync(
                    managerUser,
                    "Manager");
            }

            var employeeUser =
                await userManager.FindByEmailAsync(
                    "employee@shiftplanner.com");

            if (employeeUser == null)
            {
                employeeUser = new ApplicationUser
                {
                    UserName = "employee@shiftplanner.com",
                    Email = "employee@shiftplanner.com"
                };

                await userManager.CreateAsync(
                    employeeUser,
                    "Employee123!");

                await userManager.AddToRoleAsync(
                    employeeUser,
                    "Employee");
            }

            var context =
            services.GetRequiredService<ShiftPlannerContext>();

            var employee =
                context.Employees
                    .FirstOrDefault(e => e.Name == "Daniel");

            if (employee != null &&
                string.IsNullOrEmpty(employee.UserId))
            {
                employee.UserId = employeeUser!.Id;

                context.SaveChanges();
            }
        }
    }
}