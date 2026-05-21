using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class UserGetMapping : AutoMapper.Profile
    {
        public UserGetMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
