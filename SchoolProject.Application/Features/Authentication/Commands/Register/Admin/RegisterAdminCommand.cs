using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.Register.Admin
{
    public record RegisterAdminCommand : CommonRegisterCommand, IRequest<Response<string>>;
}