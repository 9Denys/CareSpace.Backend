using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class TimeSlotEntityConfiguration
        : BaseEntityConfiguration<TimeSlot>
    {
        public override void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Date)
                .IsRequired();

            builder.Property(t => t.StartTime)
                .IsRequired();

            builder.Property(t => t.EndTime)
                .IsRequired();

            builder.Property(t => t.StartDateTimeUtc)
                .IsRequired();

            builder.Property(t => t.EndDateTimeUtc)
                .IsRequired();

            builder.Property(t => t.IsAvailable)
                .IsRequired();

            builder.HasMany(t => t.ServiceSchedules)
                .WithOne(ss => ss.Slot)
                .HasForeignKey(ss => ss.SlotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}