using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public record ConfirmEmailCommand(int UserId, string Token) : IRequest<Response<string>>;