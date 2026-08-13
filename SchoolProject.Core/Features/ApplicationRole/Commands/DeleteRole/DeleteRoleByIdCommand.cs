using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationRole.Commands.DeleteRole;

public record DeleteRoleByIdCommand(int Id) : IRequest<Response<string>>;