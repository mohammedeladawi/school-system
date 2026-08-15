using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationRole.Commands.AddRole;

public class AddRoleCommand : IRequest<Response<string>>
{
    public string RoleName { get; set; } = string.Empty;
}