using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shift_Planner___API.Data;
using ShiftPlanner_Web.ViewModels;

namespace Shift_Planner_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser>
            _signInManager;

        public AccountController(
            SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
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

            if (result.Succeeded)
            {
                return RedirectToAction(
                    "Dashboard",
                    "Home");
            }

            ViewBag.Error =
                "Invalid email or password";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}