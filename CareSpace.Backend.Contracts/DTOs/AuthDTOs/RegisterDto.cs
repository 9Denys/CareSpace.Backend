using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;

namespace CareSpace.Backend.Contracts.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public UserCreateDto User { get; set; }

    }
}