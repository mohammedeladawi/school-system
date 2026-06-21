using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public record DeleteStudentByIdCommand(int Id) : IRequest<Response<string>>;
}