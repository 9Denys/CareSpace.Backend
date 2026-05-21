using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class ServiceCategory : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}