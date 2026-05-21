using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class AppointmentEntityConfiguration
        : BaseEntityConfiguration<Appointment>
    {
        public override void Configure(EntityTypeBuilder<Appointment> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Status)
                .IsRequired();

            builder.Property(a => a.CancelledAt);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Schedule)
                .WithMany(ss => ss.Appointments)
                .HasForeignKey(a => a.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}