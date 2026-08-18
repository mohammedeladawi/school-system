using Microsoft.AspNetCore.Http;

namespace SchoolProject.Application.Features.Authentication.Commands.Register;

public record CommonRegisterCommand
{
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string NameEn { get; init; } = null!;
    public string NameAr { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public string? Address { get; init; }
    public string Password { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
    public IFormFile? Image { get; init; }

}