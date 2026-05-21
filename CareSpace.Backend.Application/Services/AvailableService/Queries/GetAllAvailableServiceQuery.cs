using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.AvailableService.Queries
{
    public class GetAllAvailableServiceQuery : IRequest<List<AvailableServiceDto>>
    {
    }

    public class GetAllAvailableServiceQueryHandler
        : IRequestHandler<GetAllAvailableServiceQuery, List<AvailableServiceDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAvailableServiceQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AvailableServiceDto>> Handle(
            GetAllAvailableServiceQuery request,
            CancellationToken cancellationToken)
        {
            var availableServices = await _context.AvailableServices
                .Include(a => a.User)
                .Include(a => a.Service)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AvailableServiceDto>>(availableServices);
        }
    }
}