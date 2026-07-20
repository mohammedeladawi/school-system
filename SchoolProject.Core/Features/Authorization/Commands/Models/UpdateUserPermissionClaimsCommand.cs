using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Commands.Models;

public record UpdateUserPermissionClaimsCommand : IRequest<Response<string>>
{
    public int UserId { get; init; }
    public List<string> PermissionClaims { get; init; } = new List<string>();
}