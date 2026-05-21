using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class ServiceScheduleGetMapping : Profile
    {
        public ServiceScheduleGetMapping()
        {
            CreateMap<ServiceSchedule, ServiceScheduleDto>()
                .ForMember(
                    dest => dest.ServiceTitle,
                    opt => opt.MapFrom(src => src.Service.Title)
                )
                .ForMember(
                    dest => dest.Date,
                    opt => opt.MapFrom(src => src.Slot.Date)
                )
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.Slot.StartTime)
                )
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.Slot.EndTime)
                )
                .ForMember(
                    dest => dest.IsAvailable,
                    opt => opt.MapFrom(src => src.Slot.IsAvailable)
                )
                .ForMember(
                    dest => dest.CentreAddress,
                    opt => opt.MapFrom(src => src.Centre.Address)
                );
        }
    }
}