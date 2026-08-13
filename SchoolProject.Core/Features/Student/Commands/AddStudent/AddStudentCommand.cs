using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Student.Commands.AddStudent;

public record AddStudentCommand :
    CommonStudentDto,
    IRequest<Response<string>>;