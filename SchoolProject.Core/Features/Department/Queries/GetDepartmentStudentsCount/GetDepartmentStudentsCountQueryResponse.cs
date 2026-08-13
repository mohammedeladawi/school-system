namespace SchoolProject.Core.Features.Department.Queries.GetDepartmentStudentsCount;

public record GetDepartmentStudentsCountQueryResponse
{
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = null!;
    public int StudentsCount { get; init; }
}