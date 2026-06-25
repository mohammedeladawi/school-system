using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Responses;
using StudentProject.Data.Enums;

namespace SchoolProject.Core.Features.Student.Queries.Models;

public record GetPaginatedStudentsQuery : IRequest<PaginatedResponse<GetPaginatedStudentsQueryResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public StudentOrderingEnum? OrderBy { get; init; }

    public string? SearchTerm { get; init; }
}