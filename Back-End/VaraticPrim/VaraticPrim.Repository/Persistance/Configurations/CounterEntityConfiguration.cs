using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Persistance.Configurations;

public class CounterEntityConfiguration : IEntityTypeConfiguration<CounterEntity>
{
    public void Configure(EntityTypeBuilder<CounterEntity> builder)
    {
        builder.ToTable("counter", schema: "public");
        
        builder.HasOne<LocationEntity>(c => c.Location)
            .WithMany()
            .HasForeignKey(c => c.LocationId);
    }
}