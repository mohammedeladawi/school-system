using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Emails.Commands.Models;

public class SendEmailCommand : IRequest<Response<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Reason { get; set; }
}
