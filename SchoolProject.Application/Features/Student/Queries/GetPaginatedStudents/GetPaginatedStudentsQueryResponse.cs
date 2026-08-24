using SchoolProject.Application.Features.Base.Users.Queries.ResponseDTOs;

namespace SchoolProject.Application.Features.Student.Queries.GetPaginatedStudents;

public record GetPaginatedStudentsQueryResponse : BaseGetPaginatedUsersResponse
{
    public string? DepartmentName { get; set; }
}