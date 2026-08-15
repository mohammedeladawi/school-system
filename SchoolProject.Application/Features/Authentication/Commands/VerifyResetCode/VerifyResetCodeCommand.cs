using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.VerifyResetCode;

public record VerifyResetCodeCommand(string Email, string Code) : IRequest<Response<ResetPasswordUrlResponse>>;
