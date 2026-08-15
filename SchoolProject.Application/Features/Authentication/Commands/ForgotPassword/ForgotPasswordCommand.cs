using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<Response<string>>;