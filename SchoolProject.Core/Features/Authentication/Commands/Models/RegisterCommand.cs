using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public record RegisterCommand :
        CommonUserDto,
        IRequest<Response<string>>
    {
        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}