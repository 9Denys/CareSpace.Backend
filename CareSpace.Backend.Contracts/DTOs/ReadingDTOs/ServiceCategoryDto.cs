using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class ServiceCategoryDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
