using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Features.Authentication.Commands.Register;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent
{
    public record RegisterStudentCommand :
        CommonRegisterCommand,
        IRequest<Response<string>>
    {
        public int DepartmentId { get; init; }
    }
}
