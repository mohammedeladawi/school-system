using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Responses;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record LoginCommand : IRequest<Response<AuthResponse>>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}
