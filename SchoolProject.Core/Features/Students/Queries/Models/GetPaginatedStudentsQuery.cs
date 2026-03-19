using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Responses;
using StudentProject.Data.Enums;

namespace SchoolProject.Core.Features.Students.Queries.Models;

public class GetPaginatedStudentsQuery : IRequest<PaginatedResponse<PaginatedStudentsDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public StudentOrderingEnum? OrderBy { get; set; }

    public string? SearchTerm { get; set; }
}