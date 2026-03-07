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
        builder.HasData(SeedData.Students);

        // Seed data for StudentSubject relationships
        builder.HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity<StudentSubject>(
                build => build.HasData(SeedData.StudentSubjects)
            );
    }
}
