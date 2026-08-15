using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities.Views;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class DepartmentStudentsCountViewConfiguration : IEntityTypeConfiguration<DepartmentStudentsCountView>
{
    public void Configure(EntityTypeBuilder<DepartmentStudentsCountView> builder)
    {
        builder.HasNoKey();
        builder.Ignore(v => v.DepartmentName);
        builder.ToView("vw_DepartmentStudentsCount");
    }
};

