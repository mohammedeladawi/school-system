using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

namespace SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

public record GetPaginatedStudentsQuery : IRequest<PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}