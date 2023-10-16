using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointment");

        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.Reason).HasMaxLength(500).IsRequired();

        builder.HasOne(p => p.Pet)
        .WithMany(p => p.Appointments)
        .HasForeignKey(p => p.IdPetfk);

        builder.HasOne(p => p.Veterinarian)
        .WithMany(p => p.Appointments)
        .HasForeignKey(p => p.IdVeterinarianfk);
    }
}
