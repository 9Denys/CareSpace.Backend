using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class ServiceScheduleEntityConfiguration
        : BaseEntityConfiguration<ServiceSchedule>
    {
        public override void Configure(EntityTypeBuilder<ServiceSchedule> builder)
        {
            base.Configure(builder);

            builder.HasMany(ss => ss.Appointments)
                .WithOne(a => a.Schedule)
                .HasForeignKey(a => a.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}