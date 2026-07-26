using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record ConfirmEmailCommand(int UserId, string Token) : IRequest<Response<string>>;