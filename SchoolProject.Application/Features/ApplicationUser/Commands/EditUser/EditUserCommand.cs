using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;

public record EditUserCommand :
    CommonUserCommand,
    IRequest<Response<string>>
{
    public int Id { get; init; }
}