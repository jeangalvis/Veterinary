using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
{
    public void Configure(EntityTypeBuilder<Veterinarian> builder)
    {
        builder.ToTable("veterinarian");

        builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(150).IsRequired();
        builder.Property(p => p.Telephone).HasMaxLength(150).IsRequired();
        builder.Property(p => p.Speciality).HasMaxLength(150).IsRequired();
    }
}
