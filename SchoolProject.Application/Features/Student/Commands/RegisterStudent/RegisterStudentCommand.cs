using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.ApplicationUser.Commands;
using SchoolProject.Application.Features.Authentication.Commands.RegisterOrUpdate;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent
{
    public record RegisterStudentCommand :
        CommonUserCommand,
        IRequest<Response<string>>
    {
        public int DepartmentId { get; init; }

        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}
