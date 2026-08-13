namespace SchoolProject.Core.Features.Student.Commands;

public record CommonStudentDto
{
    public string NameEn { get; init; } = null!;
    public string NameAr { get; init; } = null!;
    public string Address { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public int DepartmentId { get; init; }
}
