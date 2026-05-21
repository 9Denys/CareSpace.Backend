using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCategory.Queries
{
    public class GetAllServiceCategoryQuery : IRequest<List<ServiceCategoryDto>>
    {
    }

    public class GetAllServiceCategoryQueryHandler : IRequestHandler<GetAllServiceCategoryQuery, List<ServiceCategoryDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllServiceCategoryQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceCategoryDto>> Handle(
            GetAllServiceCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCategories = await _context.ServiceCategories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ServiceCategoryDto>>(serviceCategories);
        }
    }
}