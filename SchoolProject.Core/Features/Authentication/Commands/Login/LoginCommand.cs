using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Login;

public record LoginCommand : IRequest<Response<AuthResponse>>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}
