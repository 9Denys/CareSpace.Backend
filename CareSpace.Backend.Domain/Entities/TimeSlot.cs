using CareSpace.Backend.Domain.Common;

namespace CareSpace.Backend.Domain.Entities
{
    public class TimeSlot : BaseEntity
    {
        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public bool IsAvailable { get; set; } = true;

        public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; }
            = new List<ServiceSchedule>();
    }
}