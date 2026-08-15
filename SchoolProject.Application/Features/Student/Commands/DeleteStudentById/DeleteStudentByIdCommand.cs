using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Student.Commands.DeleteStudentById;

public record DeleteStudentByIdCommand(int Id) : IRequest<Response<string>>;