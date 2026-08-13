using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Logout;

public record LogoutCommand(string RefreshToken) : IRequest<Response<string>>;