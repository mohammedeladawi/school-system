using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.Models
{
    public record DeleteRoleCommand(int Id) : IRequest<Response<string>>;
}
