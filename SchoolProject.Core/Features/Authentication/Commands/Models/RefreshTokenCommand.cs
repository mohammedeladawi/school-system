using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Responses;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Response<AuthResponse>>;