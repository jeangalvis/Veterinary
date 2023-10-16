using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class MedicalTreatmentConfiguration : IEntityTypeConfiguration<MedicalTreatment>
{
    public void Configure(EntityTypeBuilder<MedicalTreatment> builder)
    {
        builder.ToTable("medicalTreatment");

        builder.Property(p => p.Dosage).IsRequired();
        builder.Property(p => p.AdministrationDate).IsRequired();
        builder.Property(p => p.Comment).HasMaxLength(150).IsRequired();

        builder.HasOne(p => p.Appointment)
        .WithMany(p => p.MedicalTreatments)
        .HasForeignKey(p => p.IdAppointmentfk);

        builder.HasOne(p => p.Medicine)
        .WithMany(p => p.MedicalTreatments)
        .HasForeignKey(p => p.IdMedicinefk);
    }
}
