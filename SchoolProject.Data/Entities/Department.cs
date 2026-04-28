using SchoolProject.Data.Helpers;

namespace SchoolProject.Data.Entities;

public class Department
{
    public int Id { get; set; }
    public string NameEn { get; set; } = null!;
    public string NameAr { get; set; } = null!;

    // This will be ignored by the database, but it can be used in the application to display the name based on the current culture
    public string Name => GeneralLocalizableEntity.LocalizeText(NameEn, NameAr);

    public List<Subject> Subjects { get; set; } = new();
    public List<Student> Students { get; set; } = new();
}