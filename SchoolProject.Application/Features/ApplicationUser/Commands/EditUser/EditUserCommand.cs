using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;
using SchoolProject.Application.Features.Base.Users.Commands.Validators;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;

public record EditUserCommand :
    BaseRegisterUpdateUserCommand,
    IRequest<Response<string>>
{
    public int Id { get; init; }
}