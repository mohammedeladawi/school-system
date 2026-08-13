using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.DeleteUserById;

public record DeleteUserByIdCommand(int Id) : IRequest<Response<string>>;