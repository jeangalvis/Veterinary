using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class PurchasedMedicineConfiguration : IEntityTypeConfiguration<PurchasedMedicine>
{
    public void Configure(EntityTypeBuilder<PurchasedMedicine> builder)
    {
        builder.ToTable("purchasedMedicine");

        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.PurchasedDate).IsRequired();

        builder.HasOne(p => p.Supplier)
        .WithMany(p => p.PurchasedMedicines)
        .HasForeignKey(p => p.IdSupplierfk);

        builder.HasOne(p => p.Medicine)
        .WithMany(p => p.PurchasedMedicines)
        .HasForeignKey(p => p.IdMedicinefk);
    }
}
