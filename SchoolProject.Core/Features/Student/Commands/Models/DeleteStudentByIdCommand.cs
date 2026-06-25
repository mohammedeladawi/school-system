using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.Models
{
    public record DeleteStudentByIdCommand(int Id) : IRequest<Response<string>>;
}