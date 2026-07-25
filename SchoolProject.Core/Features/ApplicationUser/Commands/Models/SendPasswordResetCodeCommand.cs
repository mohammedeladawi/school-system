using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public class SendPasswordResetCodeCommand : IRequest<Response<string>>
{
    public string Email { get; set; } = null!;
}