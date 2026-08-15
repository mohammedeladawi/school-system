using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authorization.Commands.UpdateUserPermissionClaims;

public record UpdateUserPermissionClaimsCommand : IRequest<Response<string>>
{
    public int UserId { get; init; }
    public List<string> PermissionClaims { get; init; } = new List<string>();
}