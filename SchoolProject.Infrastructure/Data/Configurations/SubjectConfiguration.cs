using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(s => s.Students)
            .WithMany(s => s.Subjects)
            .UsingEntity<StudentSubject>(
                l => l.HasOne(ss => ss.Student).WithMany().HasForeignKey(ss => ss.StudentId),
                r => r.HasOne(ss => ss.Subject).WithMany().HasForeignKey(ss => ss.SubjectId),
                j => j.ToTable("StudentSubjects"));

        builder.HasMany(s => s.Departments)
            .WithMany(d => d.Subjects)
            .UsingEntity<DepartmentSubject>(
                l => l.HasOne(ds => ds.Department).WithMany().HasForeignKey(ds => ds.DepartmentId),
                r => r.HasOne(ds => ds.Subject).WithMany().HasForeignKey(ds => ds.SubjectId),
                j => j.ToTable("DepartmentSubjects"));

        // Seed data for Subjects
        builder.HasData(SeedData.Subjects);

        // Seed data for DepartmentSubject relationships
        builder.HasMany(s => s.Departments)
            .WithMany(d => d.Subjects)
            .UsingEntity<DepartmentSubject>(
                build => build.HasData(SeedData.DepartmentSubjects)
            );
    }
}