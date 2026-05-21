using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.CreateDTOs
{
    public class CreateServiceCategoryDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
