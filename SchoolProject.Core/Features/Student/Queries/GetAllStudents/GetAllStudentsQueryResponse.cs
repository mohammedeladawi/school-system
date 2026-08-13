namespace SchoolProject.Core.Features.Student.Queries.GetAllStudents;

public record GetAllStudentsQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string? DepartmentName { get; init; } = null!;
}