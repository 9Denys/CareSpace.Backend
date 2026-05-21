using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class ServiceScheduleDto
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public string? ServiceTitle { get; set; }

        public Guid SlotId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsAvailable { get; set; }

        public Guid CentreId { get; set; }

        public string? CentreAddress { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
