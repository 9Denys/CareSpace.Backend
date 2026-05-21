using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class TimeSlotGetMapping : Profile
    {
        public TimeSlotGetMapping()
        {
            CreateMap<TimeSlot, TimeSlotDto>();
        }
    }
}