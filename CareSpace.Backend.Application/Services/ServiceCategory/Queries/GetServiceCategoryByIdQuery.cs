using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCategory.Queries
{
    public class GetServiceCategoryByIdQuery : IRequest<ServiceCategoryDto>
    {
        public Guid Id { get; set; }

        public GetServiceCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetServiceCategoryByIdQueryHandler : IRequestHandler<GetServiceCategoryByIdQuery, ServiceCategoryDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceCategoryByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCategoryDto> Handle(
            GetServiceCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCategory = await _context.ServiceCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCategory == null)
            {
                throw new Exception($"Service category with id {request.Id} was not found");
            }

            return _mapper.Map<ServiceCategoryDto>(serviceCategory);
        }
    }
}