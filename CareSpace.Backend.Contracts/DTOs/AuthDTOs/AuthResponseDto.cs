using CareSpace.Backend.Contracts.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Contracts.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UserInfoDto User { get; set; }
    }
}
