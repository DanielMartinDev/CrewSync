namespace CrewSync.Website.Models;

public class WaitlistEntry
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public string Industry { get; set; } = string.Empty;

    public int EmployeeCount { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    = DateTime.UtcNow;

    public bool PrivacyConsent { get; set; }
}