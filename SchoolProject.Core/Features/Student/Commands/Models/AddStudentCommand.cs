using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.Models
{
    public record AddStudentCommand :
        CommonStudentDto,
        IRequest<Response<string>>;
}