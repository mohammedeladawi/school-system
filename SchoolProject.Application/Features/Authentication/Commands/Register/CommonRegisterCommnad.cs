using Microsoft.AspNetCore.Http;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.Authentication.Commands.Register;

public record CommonRegisterCommand : CommonUserCommand
{
    public string Password { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
}