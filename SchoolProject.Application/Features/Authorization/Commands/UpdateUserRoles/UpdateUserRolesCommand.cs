using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authorization.Commands.UpdateUserRoles;

public record UpdateUserRolesCommand : IRequest<Response<string>>
{
    public int UserId { get; init; }
    public List<string> RoleNames { get; init; } = new List<string>();
}