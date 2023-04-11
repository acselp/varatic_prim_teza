using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Persistance.Configurations;

public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable("contact", schema:"varatic_prim");
        
        builder.Property(c => c.UpdatedOnUtc)
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(c => c.CreatedOnUtc)
            .ValueGeneratedOnAdd();
    }
}