using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.Responses;

namespace SchoolProject.Core.Features.Student.Queries.Models;

public record GetStudentByIdQuery(int Id) : IRequest<Response<GetStudentByIdQueryResponse>>;