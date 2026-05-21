using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class ServiceGetMapping : Profile
    {
        public ServiceGetMapping()
        {
            CreateMap<Service, ServiceDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                );
        }
    }
}