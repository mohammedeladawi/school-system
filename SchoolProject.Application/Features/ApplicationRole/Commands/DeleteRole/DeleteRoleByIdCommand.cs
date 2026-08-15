using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationRole.Commands.DeleteRole;

public record DeleteRoleByIdCommand(int Id) : IRequest<Response<string>>;