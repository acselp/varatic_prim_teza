using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Persistence.Configurations;

public class ServiceEntityConfiguration : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.ToTable("service", schema: "varatic_prim");
        builder.Ignore(s => s.CreatedOnUtc);
        builder.Ignore(s => s.UpdatedOnUtc);
    }
}