using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditUser;

public record EditUserCommand : IRequest<Response<string>>
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string NameEn { get; init; } = null!;
    public string NameAr { get; init; } = null!;
    public string? Phone { get; init; }
    public string? Country { get; init; }
}