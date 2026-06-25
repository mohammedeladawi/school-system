using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.Models
{
    public record EditStudentCommand :
        CommonStudentDto,
        IRequest<Response<string>>
    {
        public int Id { get; init; }
    }
}