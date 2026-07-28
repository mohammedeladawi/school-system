using Microsoft.AspNetCore.Mvc;
using SchoolProject.Shared.AppMetaData;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Api.Controllers;

public class AuthenticationController : AppControllerBase
{
    [HttpPost(Router.Authentication.Register)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.Login)]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.RefreshToken)]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.Logout)]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet(Router.Authentication.ConfirmEmail)]
    public async Task<IActionResult> ConfirmEmail(int userId, string token)
    {
        var result = await Mediator.Send(new ConfirmEmailCommand(userId, token));
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.ForgotPassword)]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.VerifyResetCode)]
    public async Task<IActionResult> VerifyResetCode(VerifyResetCodeCommand command)
    {
        var result = await Mediator.Send(command);
        return NewResult(result);
    }

    [HttpPost(Router.Authentication.ResetPassword)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, [FromQuery] string encodedUserId, [FromQuery] string encodedCode)
    {
        var command = new ResetPasswordCommand
        {
            NewPassword = request.NewPassword,
            ConfirmNewPassword = request.ConfirmNewPassword,
            EncodedUserId = encodedUserId,
            EncodedCode = encodedCode
        };

        var result = await Mediator.Send(command);
        return NewResult(result);
    }
}
