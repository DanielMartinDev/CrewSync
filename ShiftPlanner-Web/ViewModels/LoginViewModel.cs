using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner_Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}