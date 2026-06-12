using Microsoft.AspNetCore.Identity;

namespace Shift_Planner___API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool MustChangePassword { get; set; }
    }
}