using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.RegisterUser
{
    public record RegisterUserCommand : CommonRegisterCommand, IRequest<Response<string>>;
}