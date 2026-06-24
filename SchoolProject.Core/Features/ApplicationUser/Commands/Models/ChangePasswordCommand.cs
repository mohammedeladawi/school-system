using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public record ChangePasswordCommand : IRequest<Response<string>>
{
    public int Id { get; init; }
    public string CurrentPassword { get; init; }  = null!;
    public string NewPassword { get; init; } = null!;
    public string ConfirmNewPassword { get; init; } = null!;
}
