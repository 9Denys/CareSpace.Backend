using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class ServiceDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
