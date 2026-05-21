using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.ReadingDTOs
{
    public class ServiceCentreDto
    {
        public Guid Id { get; set; }

        public required string Address { get; set; }

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
