using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("supplier");

            builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Telephone).HasMaxLength(70).IsRequired();
        }
    }
}