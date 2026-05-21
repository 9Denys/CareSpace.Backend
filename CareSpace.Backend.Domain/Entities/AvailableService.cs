using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class AvailableService : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Service Service { get; set; } = null!;
    }
}