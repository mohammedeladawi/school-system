using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;
using SchoolProject.Application.Features.Base.Users.Commands.Handlers;

namespace SchoolProject.Application.Features.Student.Commands.RegisterStudent
{
    public record RegisterStudentCommand :
        BaseRegisterUpdateUserCommand,
        IRequest<Response<string>>
    {
        public int DepartmentId { get; init; }
        public string Password { get; init; } = null!;
        public string ConfirmPassword { get; init; } = null!;
    }
}
