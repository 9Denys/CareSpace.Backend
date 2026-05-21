using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.UpdateDTOs
{
    public class UserPasswordUpdateDto
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
