

namespace SchoolProject.Core.Features.Department.Queries.Responses;

public record GetDepartmentByIdQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;

    public string? ManagerName { get; init; } = null!;

    public List<InstructorInDepartmentDto>? Instructors { get; init; } = new();
    public List<SubjectInDepartmentDto>? Subjects { get; init; } = new();
    public List<StudentInDepartmentDto>? Students { get; init; } = new();
}

public record InstructorInDepartmentDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public record SubjectInDepartmentDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public record StudentInDepartmentDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public record GetDepartmentStudentsCountQueryResponse
{
    public int DepartmentId { get; init; }
    public string DepartmentName { get; init; } = null!;
    public int StudentsCount { get; init; }
}