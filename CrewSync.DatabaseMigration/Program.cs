using Microsoft.EntityFrameworkCore;
using Shift_Planner___API.Data;

var sqliteOptions =
new DbContextOptionsBuilder<ShiftPlannerContext>()
.UseSqlite(
@"Data Source=C:\Users\danie\Documents\Development\C# Projects\ShiftPlanner-Web\Shift Planner - API\shiftplanner.db")
.Options;

var postgresOptions =
new DbContextOptionsBuilder<ShiftPlannerContext>()
.UseNpgsql(
"Host=localhost;Port=5432;Database=crewsync;Username=postgres;Password=Emma2019")
.Options;

using var sqlite =
new ShiftPlannerContext(sqliteOptions);

using var postgres =
new ShiftPlannerContext(postgresOptions);

Console.WriteLine("Starting migration...");
Console.WriteLine();

//
// USERS
//
var users = sqlite.Users.ToList();

postgres.Users.AddRange(users);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {users.Count} users");

//
// ROLES
//
var roles = sqlite.Roles.ToList();

postgres.Roles.AddRange(roles);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {roles.Count} roles");

//
// USER ROLES
//
var userRoles =
sqlite.UserRoles.ToList();

postgres.UserRoles.AddRange(
userRoles);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {userRoles.Count} user roles");

//
// EMPLOYEES
//
var employees =
sqlite.Employees.ToList();

postgres.Employees.AddRange(
employees);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {employees.Count} employees");

//
// AVAILABILITY
//
var availabilities =
sqlite.Availabilities.ToList();

postgres.Availabilities.AddRange(
availabilities);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {availabilities.Count} availability records");

//
// SHIFTS
//
var shifts =
sqlite.Shifts.ToList();

postgres.Shifts.AddRange(
shifts);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {shifts.Count} shifts");

//
// HOLIDAY REQUESTS
//
var holidayRequests =
sqlite.HolidayRequests.ToList();

postgres.HolidayRequests.AddRange(
holidayRequests);

await postgres.SaveChangesAsync();

Console.WriteLine(
$"Copied {holidayRequests.Count} holiday requests");

Console.WriteLine();
Console.WriteLine("Migration complete.");
Console.WriteLine();

Console.WriteLine(
$"Users: {postgres.Users.Count()}");

Console.WriteLine(
$"Employees: {postgres.Employees.Count()}");

Console.WriteLine(
$"Shifts: {postgres.Shifts.Count()}");

Console.WriteLine(
$"Availability: {postgres.Availabilities.Count()}");

Console.WriteLine(
$"Holiday Requests: {postgres.HolidayRequests.Count()}");
