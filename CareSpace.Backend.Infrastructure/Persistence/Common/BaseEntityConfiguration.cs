using CareSpace.Backend.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareSpace.Backend.Infrastructure.Persistence.Common
{
    internal abstract class BaseEntityConfiguration<TEntity>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.UpdatedAt);

            builder.Property(e => e.DeletedAt);
        }
    }
}