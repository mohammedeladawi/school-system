using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

public record BaseChangePasswordCommand : IRequest<Response<string>>
{
    public int Id { get; init; }
    public string CurrentPassword { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
    public string ConfirmNewPassword { get; init; } = null!;
}