using AdministrationSystem.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdministrationSystem.Infrastructure.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .ValueGeneratedNever();

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.CPF)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(u => u.CEP)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(u => u.DataNascimento)
            .IsRequired();

        builder.Property(u => u.Address)
            .IsRequired();

        builder.Property("_passwordHash")
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasMaxLength(255);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.CPF)
            .IsUnique();
    }
}
