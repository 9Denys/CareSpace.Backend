using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(u => u.Role)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.HasMany(u => u.AvailableServices)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Appointments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}