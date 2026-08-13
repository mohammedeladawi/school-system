using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.EditRole;

public record EditRoleCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string NewName { get; set; } = string.Empty;
}