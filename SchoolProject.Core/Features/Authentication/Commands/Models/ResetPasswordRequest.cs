namespace SchoolProject.Core.Features.Authentication.Commands.Models;

public class ResetPasswordRequest
{
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}
