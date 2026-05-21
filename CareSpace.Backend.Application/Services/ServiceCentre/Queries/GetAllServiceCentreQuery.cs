using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCentre.Queries
{
    public class GetAllServiceCentreQuery : IRequest<List<ServiceCentreDto>>
    {
    }

    public class GetAllServiceCentreQueryHandler
        : IRequestHandler<GetAllServiceCentreQuery, List<ServiceCentreDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllServiceCentreQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceCentreDto>> Handle(
            GetAllServiceCentreQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCentres = await _context.ServiceCentres
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ServiceCentreDto>>(serviceCentres);
        }
    }
}