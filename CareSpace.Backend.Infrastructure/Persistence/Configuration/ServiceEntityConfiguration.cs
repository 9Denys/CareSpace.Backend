using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class ServiceEntityConfiguration
        : BaseEntityConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.Property(s => s.DurationMinutes)
                .IsRequired();

            builder.Property(s => s.IsActive)
                .IsRequired();

            builder.HasMany(s => s.AvailableServices)
                .WithOne(a => a.Service)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.ServiceSchedules)
                .WithOne(ss => ss.Service)
                .HasForeignKey(ss => ss.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Appointments)
                .WithOne(a => a.Service)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}