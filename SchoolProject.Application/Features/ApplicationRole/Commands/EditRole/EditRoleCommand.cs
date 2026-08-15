using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationRole.Commands.EditRole;

public record EditRoleCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string NewName { get; set; } = string.Empty;
}