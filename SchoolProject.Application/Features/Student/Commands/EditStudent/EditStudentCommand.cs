using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Base.Users.Commands.RequestDTOs;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public record EditStudentCommand :
    BaseRegisterUpdateUserCommand,
    IRequest<Response<string>>
{
    public int Id { get; init; }
    public int DepartmentId { get; init; }
}