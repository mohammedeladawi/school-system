using SchoolProject.Application.Features.Base.ApplicationUser.Queries.ResponseDTOs;

namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQueryResponse : BaseGetUserByIdResponse
{
    public int Id { get; init; }
    public string? DepartmentName { get; init; }
}