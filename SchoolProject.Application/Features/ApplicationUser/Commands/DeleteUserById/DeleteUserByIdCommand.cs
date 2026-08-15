using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.DeleteUserById;

public record DeleteUserByIdCommand(int Id) : IRequest<Response<string>>;