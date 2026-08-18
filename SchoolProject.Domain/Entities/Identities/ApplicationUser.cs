using Microsoft.AspNetCore.Identity;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.Helpers;
namespace SchoolProject.Domain.Entities.Identities;

public class ApplicationUser : IdentityUser<int>
{
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;
    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);
    public string Address { get; set; } = null!;
    public string? ImagePath { get; set; }
    public UserType UserType { get; set; } // Denormalization
}