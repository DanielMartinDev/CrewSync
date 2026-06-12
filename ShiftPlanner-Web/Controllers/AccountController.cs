using Microsoft.AspNetCore.Authorization;
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
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Manager") || User.IsInRole("Admin"))
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

            if (user != null &&
                user.MustChangePassword)
            {
                return RedirectToAction(
                    nameof(ChangePassword));
            }

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

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(
    ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user =
                await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login");

            var result =
                await _userManager.ChangePasswordAsync(
                    user,
                    model.CurrentPassword,
                    model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        "",
                        error.Description);
                }

                return View(model);
            }

            user.MustChangePassword = false;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);

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
