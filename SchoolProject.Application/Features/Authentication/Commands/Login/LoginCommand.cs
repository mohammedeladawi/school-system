using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.Login;

public record LoginCommand : IRequest<Response<AuthResponse>>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}
