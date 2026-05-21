using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.CreateDTOs
{
    public class CreateServiceCentreDto
    {
        public required string Address { get; set; }

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }
    }
}