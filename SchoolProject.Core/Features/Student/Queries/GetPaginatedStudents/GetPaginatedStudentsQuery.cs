using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.GetPaginatedStudents;

namespace SchoolProject.Core.Features.Student.Queries.GetPaginatedStudents;

public record GetPaginatedStudentsQuery : IRequest<PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}