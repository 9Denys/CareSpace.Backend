using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class AvailableServiceDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? UserFullName { get; set; }

        public Guid ServiceId { get; set; }

        public string? ServiceTitle { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}