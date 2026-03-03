using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name).IsRequired().HasMaxLength(500);

        // Seed data
        builder.HasData(
            new Department { Id = 1, Name = "Computer Science" },
            new Department { Id = 2, Name = "Mathematics" },
            new Department { Id = 3, Name = "Physics" },
            new Department { Id = 4, Name = "Chemistry" },
            new Department { Id = 5, Name = "Biology" }
        );
    }
};
