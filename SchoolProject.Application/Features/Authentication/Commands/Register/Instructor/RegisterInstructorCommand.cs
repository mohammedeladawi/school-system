using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public record RegisterInstructorCommand :
        CommonRegisterCommand,
        IRequest<Response<string>>
    {
        public int? DepartmentId { get; init; }
        public int? SupervisorId { get; init; }
    }
}
