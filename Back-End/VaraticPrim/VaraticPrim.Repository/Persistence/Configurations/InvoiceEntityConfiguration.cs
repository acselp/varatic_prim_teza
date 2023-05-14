using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Persistence.Configurations;

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.ToTable("invoice", schema: "varatic_prim");
        
        builder.HasOne<ServiceEntity>(c => c.Service)
            .WithMany()
            .HasForeignKey(c => c.ServiceId);
        
        builder.HasOne<LocationEntity>(c => c.Location)
            .WithMany()
            .HasForeignKey(c => c.LocationId);
    }
}