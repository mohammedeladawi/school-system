using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand : IRequest<Response<string>>
{
    public string NewPassword { get; init; } = null!;
    public string ConfirmNewPassword { get; init; } = null!;
    public string EncodedUserId { get; init; } = null!;
    public string EncodedCode { get; init; } = null!;
}
