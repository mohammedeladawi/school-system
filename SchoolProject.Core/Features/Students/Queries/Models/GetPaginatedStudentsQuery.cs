using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Responses;

namespace SchoolProject.Core.Features.Students.Queries.Models;

public class GetPaginatedStudentsQuery : IRequest<PaginatedResponse<PaginatedStudentsDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string[]? OrderBy { get; set; }

    public string? SearchTerm { get; set; }
}