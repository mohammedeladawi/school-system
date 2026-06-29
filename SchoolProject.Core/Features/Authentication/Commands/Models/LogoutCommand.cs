using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record LogoutCommand (string RefreshToken) : IRequest<Response<string>>;