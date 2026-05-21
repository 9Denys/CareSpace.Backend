using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.UpdateDTOs
{
    public class UpdateServiceCategoryDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
