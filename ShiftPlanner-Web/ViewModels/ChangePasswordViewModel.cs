using System.ComponentModel.DataAnnotations;

namespace ShiftPlanner_Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; } = "";

        [Required]
        public string NewPassword { get; set; } = "";

        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = "";
    }
}