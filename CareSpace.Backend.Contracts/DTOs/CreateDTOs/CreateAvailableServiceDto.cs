using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.CreateDTOs
{
    public class CreateAvailableServiceDto
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }
    }
}