using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class ServiceScheduleCreateMapping : Profile
    {
        public ServiceScheduleCreateMapping()
        {
            CreateMap<CreateServiceScheduleDto, ServiceSchedule>();
        }
    }
}