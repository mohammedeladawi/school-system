using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;

namespace SchoolProject.Application.Features.Authentication.Commands.Register
{
    public record RegisterStudentCommand :
        CommonRegisterCommand,
        IRequest<Response<string>>
    {
        public int DepartmentId { get; init; }
    }
}
