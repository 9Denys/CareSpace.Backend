using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class UserCreateMapping : AutoMapper.Profile
    {
        public UserCreateMapping()
        {
            CreateMap<UserCreateDto, User>();
        }
    }
}
