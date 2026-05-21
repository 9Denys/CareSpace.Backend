using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.UpdateDTOs
{
    public class UpdateServiceDto
    {
        public Guid CategoryId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; }
    }
}
