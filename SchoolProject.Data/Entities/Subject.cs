namespace SchoolProject.Data.Entities;

public class Subject
{
    public int Id { get; set; }
   
    public string Name { get; set; } = null!;
    public DateTime Period { get; set; }

    public List<Student> Students { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
}