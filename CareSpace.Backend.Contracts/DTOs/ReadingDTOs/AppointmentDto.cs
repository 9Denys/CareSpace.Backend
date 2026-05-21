using CareSpace.Backend.Contracts.DTOs.Enums;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? UserFullName { get; set; }

        public Guid ServiceId { get; set; }

        public string? ServiceTitle { get; set; }

        public Guid ScheduleId { get; set; }

        public Guid CentreId { get; set; }

        public string? CentreAddress { get; set; }

        public Guid SlotId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public AppointmentStatus Status { get; set; }

        public DateTime? CancelledAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}