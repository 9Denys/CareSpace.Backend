using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareSpace.Backend.Contracts.DTOs.Enums;

namespace CareSpace.Backend.Contracts.DTOs.AuthDTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
