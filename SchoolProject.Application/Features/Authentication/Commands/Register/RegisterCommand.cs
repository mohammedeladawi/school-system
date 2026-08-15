using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public record RegisterCommand : IRequest<Response<string>>
    {
        public string Email { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string NameEn { get; init; } = null!;
        public string NameAr { get; init; } = null!;
        public string? Phone { get; init; }
        public string? Country { get; init; }
        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}