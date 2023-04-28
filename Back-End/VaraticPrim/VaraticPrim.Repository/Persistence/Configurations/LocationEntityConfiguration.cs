﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Persistence.Configurations;

public class LocationEntityConfiguration : IEntityTypeConfiguration<LocationEntity>
{
    public void Configure(EntityTypeBuilder<LocationEntity> builder)
    {
        builder.ToTable("location", schema: "public");
        
        builder.HasOne<UserEntity>(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId);
    }
}