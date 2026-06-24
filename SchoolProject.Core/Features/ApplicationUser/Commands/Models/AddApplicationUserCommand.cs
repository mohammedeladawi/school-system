using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models
{
    public record AddApplicationUserCommand :
        CommonApplicationUserCommand,
        IRequest<Response<string>>
    {
        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}