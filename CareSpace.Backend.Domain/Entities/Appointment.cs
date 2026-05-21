using CareSpace.Backend.Domain.Common;
using CareSpace.Backend.Domain.Enums;

namespace CareSpace.Backend.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid ScheduleId { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;

        public DateTime? CancelledAt { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Service Service { get; set; } = null!;

        public virtual ServiceSchedule Schedule { get; set; } = null!;
    }
}