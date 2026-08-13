using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.VerifyResetCode;

public record VerifyResetCodeCommand(string Email, string Code) : IRequest<Response<ResetPasswordUrlResponse>>;
