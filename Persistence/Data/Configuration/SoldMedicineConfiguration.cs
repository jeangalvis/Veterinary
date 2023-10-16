using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class SoldMedicineConfiguration : IEntityTypeConfiguration<SoldMedicine>
{
    public void Configure(EntityTypeBuilder<SoldMedicine> builder)
    {
        builder.ToTable("soldMedicine");

        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.SoldDate).IsRequired();

        builder.HasOne(p => p.Medicine)
        .WithMany(p => p.SoldMedicines)
        .HasForeignKey(p => p.IdMedicinefk);
    }
}
