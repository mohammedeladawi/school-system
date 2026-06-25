using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public record LoginCommand : IRequest<Response<string>>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}
