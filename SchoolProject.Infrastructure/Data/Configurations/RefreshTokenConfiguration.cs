using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities.Identities;

namespace SchoolProject.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Id)
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(rt => rt.TokenHash)
            .HasMaxLength(64);

        builder.Property(rt => rt.CreatedAt)
            .HasDefaultValueSql("getutcdate()");

        builder.HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(rt => rt.IsRevoked)
            .HasDefaultValue(false);

        builder.HasIndex(rt => rt.TokenHash)
            .IsUnique()
            .HasDatabaseName("IX_RefreshTokens_TokenHash");

        builder.HasIndex(rt => rt.FamilyId)
            .HasDatabaseName("IX_RefreshTokens_FamilyId");
    }
}
