using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class ServiceCentreEntityConfiguration
        : BaseEntityConfiguration<ServiceCentre>
    {
        public override void Configure(EntityTypeBuilder<ServiceCentre> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.OpenTime)
                .IsRequired();

            builder.Property(c => c.CloseTime)
                .IsRequired();

            builder.HasMany(c => c.ServiceSchedules)
                .WithOne(ss => ss.Centre)
                .HasForeignKey(ss => ss.CentreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}