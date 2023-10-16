using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pet");

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.BirthDate).IsRequired();

        builder.HasOne(p => p.Owner)
        .WithMany(p => p.Pets)
        .HasForeignKey(p => p.IdOwnerfk);

        builder.HasOne(p => p.Breed)
        .WithMany(p => p.Pets)
        .HasForeignKey(p => p.IdBreedfk);
    }
}
