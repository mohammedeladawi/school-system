using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(int UserId, string EncodedToken) : IRequest<Response<string>>;