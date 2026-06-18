namespace SchoolProject.Data.Entities;

public class InstructorSubject
{
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}