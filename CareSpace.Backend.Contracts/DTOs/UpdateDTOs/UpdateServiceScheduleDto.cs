using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.UpdateDTOs
{
    public class UpdateServiceScheduleDto
    {
        public Guid ServiceId { get; set; }

        public Guid SlotId { get; set; }

        public Guid CentreId { get; set; }
    }
}