using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Student.Queries.GetAllStudents;

namespace SchoolProject.Application.Features.Student.Queries.GetAllStudents;

public record GetAllStudentsQuery : IRequest<Response<List<GetAllStudentsQueryResponse>>>;