using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record ForgotPasswordCommand(string Email) : IRequest<Response<string>>;