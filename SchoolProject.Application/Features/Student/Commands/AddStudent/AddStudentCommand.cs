using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Student.Commands.AddStudent;

public record AddStudentCommand :
    CommonStudentDto,
    IRequest<Response<string>>;