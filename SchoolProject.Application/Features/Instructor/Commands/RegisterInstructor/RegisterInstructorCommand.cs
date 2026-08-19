using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Features.Instructor.Commands.RegisterInstructor
{
    public record RegisterInstructorCommand :
        CommonRegisterCommand,
        IRequest<Response<string>>
    {
        public int? DepartmentId { get; init; }
        public int? SupervisorId { get; init; }
    }
}
