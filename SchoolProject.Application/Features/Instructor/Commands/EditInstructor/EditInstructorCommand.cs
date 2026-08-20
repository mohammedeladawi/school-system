using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.ApplicationUser.Commands.EditInstructor;

public record EditInstructorCommand :
    CommonUserCommand,
    IRequest<Response<string>>
{
    public int Id { get; init; }
    public int? DepartmentId { get; init; }
    public int? SupervisorId { get; init; }
}