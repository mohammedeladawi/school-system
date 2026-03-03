using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(500);

        // Seed data for Students
        builder.HasData(
            new Student { Id = 1, Name = "Ahmed Hassan", Address = "Cairo, Egypt", Phone = "01001234567", DepartmentId = 1 },
            new Student { Id = 2, Name = "Fatima Mohamed", Address = "Giza, Egypt", Phone = "01101234567", DepartmentId = 1 },
            new Student { Id = 3, Name = "Omar Ali", Address = "Alexandria, Egypt", Phone = "01201234567", DepartmentId = 2 },
            new Student { Id = 4, Name = "Layla Ibrahim", Address = "Cairo, Egypt", Phone = "01001234568", DepartmentId = 2 },
            new Student { Id = 5, Name = "Mustafa Karim", Address = "Helwan, Egypt", Phone = "01101234568", DepartmentId = 3 },
            new Student { Id = 6, Name = "Amira Hassan", Address = "New Cairo, Egypt", Phone = "01201234568", DepartmentId = 3 },
            new Student { Id = 7, Name = "Khaled Ahmed", Address = "Cairo, Egypt", Phone = "01001234569", DepartmentId = 4 },
            new Student { Id = 8, Name = "Noor Saleh", Address = "Giza, Egypt", Phone = "01101234569", DepartmentId = 4 },
            new Student { Id = 9, Name = "Youssef Nasr", Address = "Cairo, Egypt", Phone = "01201234569", DepartmentId = 5 },
            new Student { Id = 10, Name = "Dina Khalil", Address = "Helwan, Egypt", Phone = "01001234570", DepartmentId = 5 }
        );

        // Seed data for StudentSubject relationships
        builder.HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity<StudentSubject>(
                build => build.HasData(
                    // Ahmed Hassan (Student 1) - Subjects 1, 2, 3
                    new StudentSubject { Id = 1, StudentId = 1, SubjectId = 1 },
                    new StudentSubject { Id = 2, StudentId = 1, SubjectId = 2 },
                    new StudentSubject { Id = 3, StudentId = 1, SubjectId = 3 },
                    // Fatima Mohamed (Student 2) - Subjects 1, 3
                    new StudentSubject { Id = 4, StudentId = 2, SubjectId = 1 },
                    new StudentSubject { Id = 5, StudentId = 2, SubjectId = 3 },
                    // Omar Ali (Student 3) - Subjects 4, 5
                    new StudentSubject { Id = 6, StudentId = 3, SubjectId = 4 },
                    new StudentSubject { Id = 7, StudentId = 3, SubjectId = 5 },
                    // Layla Ibrahim (Student 4) - Subjects 4, 5
                    new StudentSubject { Id = 8, StudentId = 4, SubjectId = 4 },
                    new StudentSubject { Id = 9, StudentId = 4, SubjectId = 5 },
                    // Mustafa Karim (Student 5) - Subjects 6, 7
                    new StudentSubject { Id = 10, StudentId = 5, SubjectId = 6 },
                    new StudentSubject { Id = 11, StudentId = 5, SubjectId = 7 },
                    // Amira Hassan (Student 6) - Subjects 6, 7
                    new StudentSubject { Id = 12, StudentId = 6, SubjectId = 6 },
                    new StudentSubject { Id = 13, StudentId = 6, SubjectId = 7 },
                    // Khaled Ahmed (Student 7) - Subject 8
                    new StudentSubject { Id = 14, StudentId = 7, SubjectId = 8 },
                    // Noor Saleh (Student 8) - Subject 8
                    new StudentSubject { Id = 15, StudentId = 8, SubjectId = 8 },
                    // Youssef Nasr (Student 9) - Subject 9
                    new StudentSubject { Id = 16, StudentId = 9, SubjectId = 9 },
                    // Dina Khalil (Student 10) - Subject 9
                    new StudentSubject { Id = 17, StudentId = 10, SubjectId = 9 }
                ));
    }
}
