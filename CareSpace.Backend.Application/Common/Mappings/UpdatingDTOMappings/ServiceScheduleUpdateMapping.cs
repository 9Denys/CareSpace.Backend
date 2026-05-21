using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class ServiceScheduleUpdateMapping : Profile
    {
        public ServiceScheduleUpdateMapping()
        {
            CreateMap<UpdateServiceScheduleDto, ServiceSchedule>();
        }
    }
}