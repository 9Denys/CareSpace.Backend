using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class Service : BaseEntity
    {
        public Guid CategoryId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ServiceCategory Category { get; set; } = null!;

        public virtual ICollection<AvailableService> AvailableServices { get; set; } = new List<AvailableService>();

        public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}