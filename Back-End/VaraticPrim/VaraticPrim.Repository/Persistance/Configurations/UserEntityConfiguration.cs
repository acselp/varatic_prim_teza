using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Persistance.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(u => u.Id)
            .UseSequence();
        
        builder.ToTable("user", schema: "public");
        
        builder.HasOne<ContactEntity>(u => u.Contact)
            .WithMany()
            .HasForeignKey(u => u.ContactId);
    }
}