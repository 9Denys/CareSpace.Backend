using CareSpace.Backend.Domain.Entities;
using CareSpace.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Configurations
{
    internal class AvailableServiceEntityConfiguration
        : BaseEntityConfiguration<AvailableService>
    {
        public override void Configure(EntityTypeBuilder<AvailableService> builder)
        {
            base.Configure(builder);
        }
    }
}