using Microsoft.AspNetCore.Identity;
using SchoolProject.Shared.Helpers;
namespace SchoolProject.Data.Entities.Identities;

public class ApplicationUser : IdentityUser<int>
{
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;

    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);

    public string? Phone { get; set; }
    public string? Country { get; set; }
}