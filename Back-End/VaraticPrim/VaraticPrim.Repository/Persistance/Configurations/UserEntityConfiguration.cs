using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Persistance.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user", schema: "varatic_prim")
            .HasOne<ContactEntity>(c => c.Contact)
            .WithMany()
            .HasForeignKey(u => u.ContactId);
    }
}