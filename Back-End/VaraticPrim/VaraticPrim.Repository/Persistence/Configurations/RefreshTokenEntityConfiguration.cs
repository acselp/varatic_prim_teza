using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Persistence.Configurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("refresh_token", schema:"varatic_prim");
        
        builder.HasOne<UserEntity>(e => e.UserEntity)
            .WithMany()
            .HasForeignKey(l => l.UserId);

        builder.Ignore(entity => entity.IsExpired);
    }
}