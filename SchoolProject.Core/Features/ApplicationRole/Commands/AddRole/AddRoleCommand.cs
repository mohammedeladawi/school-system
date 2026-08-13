using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.AddRole;

public class AddRoleCommand : IRequest<Response<string>>
{
    public string RoleName { get; set; } = string.Empty;
}