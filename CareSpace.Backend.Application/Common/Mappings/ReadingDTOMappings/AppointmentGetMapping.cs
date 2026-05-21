using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class AppointmentGetMapping : Profile
    {
        public AppointmentGetMapping()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(
                    dest => dest.UserFullName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName)
                )
                .ForMember(
                    dest => dest.ServiceTitle,
                    opt => opt.MapFrom(src => src.Service.Title)
                )
                .ForMember(
                    dest => dest.CentreId,
                    opt => opt.MapFrom(src => src.Schedule.CentreId)
                )
                .ForMember(
                    dest => dest.CentreAddress,
                    opt => opt.MapFrom(src => src.Schedule.Centre.Address)
                )
                .ForMember(
                    dest => dest.SlotId,
                    opt => opt.MapFrom(src => src.Schedule.SlotId)
                )
                .ForMember(
                    dest => dest.Date,
                    opt => opt.MapFrom(src => src.Schedule.Slot.Date)
                )
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.Schedule.Slot.StartTime)
                )
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.Schedule.Slot.EndTime)
                )
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => (Contracts.DTOs.Enums.AppointmentStatus)src.Status)
                );
        }
    }
}