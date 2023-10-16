using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("medicine");

        builder.Property(p => p.Name).HasMaxLength(110).IsRequired();
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Laboratory).HasMaxLength(110).IsRequired();

        builder.HasOne(p => p.Supplier)
        .WithMany(p => p.Medicines)
        .HasForeignKey(p => p.IdSupplierfk);
    }
}
