namespace SchoolProject.Data.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Subject> Subjects { get; set; } = new();
    public List<Student> Students { get; set; } = new();
}