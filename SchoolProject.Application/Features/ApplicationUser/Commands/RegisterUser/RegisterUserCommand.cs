using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser
{
    public record RegisterUserCommand : CommonUserCommand, IRequest<Response<string>>
    {
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}