using AutoMapper;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Entities;

namespace CareSpace.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class ServiceCategoryGetMapping : Profile
    {
        public ServiceCategoryGetMapping()
        {
            CreateMap<ServiceCategory, ServiceCategoryDto>();
        }
    }
}