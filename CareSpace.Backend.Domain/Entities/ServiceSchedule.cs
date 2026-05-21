using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class ServiceSchedule : BaseEntity
    {
        public Guid ServiceId { get; set; }

        public Guid SlotId { get; set; }

        public Guid CentreId { get; set; }

        public virtual Service Service { get; set; } = null!;

        public virtual TimeSlot Slot { get; set; } = null!;

        public virtual ServiceCentre Centre { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}