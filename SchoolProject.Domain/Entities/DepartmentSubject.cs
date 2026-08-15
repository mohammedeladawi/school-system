namespace SchoolProject.Domain.Entities;

public class DepartmentSubject
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}