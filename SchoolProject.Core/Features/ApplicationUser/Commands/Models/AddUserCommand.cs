using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models
{
    public record AddUserCommand :
        CommonUserDto,
        IRequest<Response<string>>
    {
        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}