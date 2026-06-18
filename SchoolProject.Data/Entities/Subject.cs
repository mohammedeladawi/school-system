using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities;

public class Subject
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);

    public DateTime Period { get; set; }

    public List<Student> Students { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
    public List<Instructor> Instructors { get; set; } = new();
}