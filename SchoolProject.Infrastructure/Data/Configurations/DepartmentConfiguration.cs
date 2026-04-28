using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.NameEn).IsRequired().HasMaxLength(500);
        builder.Property(d => d.NameAr).IsRequired().HasMaxLength(500);

        // Seed data
        builder.HasData(SeedData.Departments);
    }
};
