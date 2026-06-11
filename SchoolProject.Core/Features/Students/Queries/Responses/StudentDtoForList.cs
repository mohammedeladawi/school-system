namespace SchoolProject.Core.Features.Students.Queries.Responses;

public record StudentDtoForList
{
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public string? DepartmentName { get; init; } = null!;
}