using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Student.Queries.GetStudentById;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<Response<GetStudentByIdQueryResponse>>;