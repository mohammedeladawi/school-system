namespace SchoolProject.Core.Features.Students.Queries.Responses;

public class SingleStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? DepartmentName { get; set; }
}
