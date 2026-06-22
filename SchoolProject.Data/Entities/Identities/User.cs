using Microsoft.AspNetCore.Identity;
namespace SchoolProject.Data.Entities.Identities;

public class User : IdentityUser<int>
{
    public string? Address { get; set; } = null;
    public string? Country { get; set; } = null;
}