using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class AvailableServiceGetMapping : Profile
    {
        public AvailableServiceGetMapping()
        {
            CreateMap<AvailableService, AvailableServiceDto>()
                .ForMember(
                    dest => dest.UserFullName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName)
                )
                .ForMember(
                    dest => dest.ServiceTitle,
                    opt => opt.MapFrom(src => src.Service.Title)
                );
        }
    }
}