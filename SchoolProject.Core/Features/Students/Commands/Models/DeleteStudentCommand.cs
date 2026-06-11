using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public record DeleteStudentCommand(int Id) : IRequest<Response<string>>;
}