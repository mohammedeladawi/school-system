using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.GetAllStudents;

namespace SchoolProject.Core.Features.Student.Queries.GetAllStudents;

public record GetAllStudentsQuery : IRequest<Response<List<GetAllStudentsQueryResponse>>>;