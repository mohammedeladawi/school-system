using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public record DeleteApplicationUserByIdCommand(int Id) : IRequest<Response<string>>;
