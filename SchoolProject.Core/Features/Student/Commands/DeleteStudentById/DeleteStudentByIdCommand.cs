using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.DeleteStudentById;

public record DeleteStudentByIdCommand(int Id) : IRequest<Response<string>>;