using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.Logout;

public record LogoutCommand(string RefreshToken) : IRequest<Response<string>>;