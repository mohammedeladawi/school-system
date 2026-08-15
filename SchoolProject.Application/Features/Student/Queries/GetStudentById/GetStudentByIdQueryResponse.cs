namespace SchoolProject.Application.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string? DepartmentName { get; init; }
}