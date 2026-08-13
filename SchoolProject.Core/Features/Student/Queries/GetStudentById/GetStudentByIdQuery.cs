using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.GetStudentById;

namespace SchoolProject.Core.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<Response<GetStudentByIdQueryResponse>>;