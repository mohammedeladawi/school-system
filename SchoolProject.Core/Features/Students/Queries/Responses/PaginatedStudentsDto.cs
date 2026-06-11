namespace SchoolProject.Core.Responses;

public record PaginatedStudentsDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string? DepartmentName { get; init; } = null!;
}