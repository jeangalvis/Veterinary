using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.Property(p => p.Username).HasMaxLength(50).IsRequired();


        builder.Property(p => p.Password)
       .HasMaxLength(255)
       .IsRequired();

        builder.Property(p => p.Email)
        .HasMaxLength(100)
        .IsRequired();

        builder
       .HasMany(p => p.Rols)
       .WithMany(r => r.Users)
       .UsingEntity<UserRol>(

           j => j
           .HasOne(pt => pt.Rol)
           .WithMany(t => t.UsersRols)
           .HasForeignKey(ut => ut.IdRolfk),

           j => j
           .HasOne(et => et.Usuario)
           .WithMany(et => et.UsersRols)
           .HasForeignKey(el => el.IdUserfk),

           j =>
           {
               j.ToTable("userRol");
               j.HasKey(t => new { t.IdUserfk, t.IdRolfk });

           });

        builder.HasMany(p => p.RefreshTokens)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.IdUserfk);
    }
}
