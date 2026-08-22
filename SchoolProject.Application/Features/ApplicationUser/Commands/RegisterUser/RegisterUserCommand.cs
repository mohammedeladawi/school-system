using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser
{
    public record RegisterUserCommand : BaseRegisterUpdateUserCommand, IRequest<Response<string>>
    {
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}