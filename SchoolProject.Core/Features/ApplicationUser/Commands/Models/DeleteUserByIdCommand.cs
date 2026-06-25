using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public record DeleteCommand(int Id) : IRequest<Response<string>>;
