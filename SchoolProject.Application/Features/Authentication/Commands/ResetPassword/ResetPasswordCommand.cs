using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand : IRequest<Response<string>>
{
    public string NewPassword { get; init; } = null!;
    public string ConfirmNewPassword { get; init; } = null!;
    public string EncodedUserId { get; init; } = null!;
    public string EncodedCode { get; init; } = null!;
}
