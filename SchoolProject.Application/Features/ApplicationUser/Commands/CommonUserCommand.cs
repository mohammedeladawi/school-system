using Microsoft.AspNetCore.Http;

namespace SchoolProject.Application.Features.ApplicationUser.Commands;

public record CommonUserCommand
{
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string NameEn { get; init; } = null!;
    public string NameAr { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public string? Address { get; init; }
    public IFormFile? Image { get; init; }
}