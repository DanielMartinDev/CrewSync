using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Data;
using ShiftPlanner_Web.ViewModels;

namespace ShiftPlanner_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser>
        _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(
                    "Dashboard",
                    "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            LoginViewModel model)
        {
            var result =
                await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    false,
                    false);

            if (!result.Succeeded)
            {
                ViewBag.Error =
                    "Invalid email or password";

                return View(model);
            }

            var user =
                await _userManager.FindByEmailAsync(
                    model.Email);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(
                    user,
                    "Employee"))
                {
                    return RedirectToAction(
                        "Index",
                        "EmployeePortal");
                }

                if (await _userManager.IsInRoleAsync(
                    user,
                    "Manager"))
                {
                    return RedirectToAction(
                        "Dashboard",
                        "Home");
                }

                if (await _userManager.IsInRoleAsync(
                    user,
                    "Admin"))
                {
                    return RedirectToAction(
                        "Dashboard",
                        "Home");
                }
            }

            return RedirectToAction(
                "Dashboard",
                "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                nameof(Login));
        }
    }
}
