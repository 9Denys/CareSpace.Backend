using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareSpace.Backend.Contracts.DTOs.Enums;

namespace CareSpace.Backend.Contracts.DTOs.CreateDTOs
{
    public class UserCreateDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; }
        public required string PasswordHash { get; set; }
    }
}
