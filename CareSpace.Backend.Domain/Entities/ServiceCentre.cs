using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class ServiceCentre : BaseEntity
    {
        public required string Address { get; set; }

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
    }
}