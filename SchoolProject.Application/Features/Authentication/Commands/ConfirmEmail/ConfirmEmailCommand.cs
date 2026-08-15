using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(int UserId, string EncodedToken) : IRequest<Response<string>>;