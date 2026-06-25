using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;

public record EditUserCommand :
    CommonUserDto,
    IRequest<Response<string>>
{
    public int Id { get; init; }
}
