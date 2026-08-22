using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;

public record EditInstructorCommand :
    BaseRegisterUpdateUserCommand,
    IRequest<Response<string>>
{
    public int Id { get; init; }
    public int? DepartmentId { get; init; }
    public int? SupervisorId { get; init; }
}