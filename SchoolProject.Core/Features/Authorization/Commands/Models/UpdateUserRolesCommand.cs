using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Commands.Models;

public record UpdateUserRolesCommand : IRequest<Response<string>>
{
    public int UserId { get; init; }
    public List<string> RoleNames { get; init; } = new List<string>();
}