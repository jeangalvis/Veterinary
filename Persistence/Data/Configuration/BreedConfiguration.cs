using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

        builder.HasOne(p => p.Species)
        .WithMany(p => p.Breeds)
        .HasForeignKey(p => p.IdSpeciesfk);

    }
}
