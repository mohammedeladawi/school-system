using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Student.Commands.EditStudent;

public record EditStudentCommand :
    CommonStudentDto,
    IRequest<Response<string>>
{
    public int Id { get; init; }
}