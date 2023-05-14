using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Persistence.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user", schema: "varatic_prim");
        
        builder.HasOne<ContactEntity>(u => u.Contact)
            .WithMany()
            .HasForeignKey(u => u.ContactId);
    }
}