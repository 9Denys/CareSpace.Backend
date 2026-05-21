using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.AvailableService.Queries
{
    public class GetAvailableServiceByUserIdQuery : IRequest<List<AvailableServiceDto>>
    {
        public Guid UserId { get; set; }

        public GetAvailableServiceByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetAvailableServiceByUserIdQueryHandler
        : IRequestHandler<GetAvailableServiceByUserIdQuery, List<AvailableServiceDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAvailableServiceByUserIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AvailableServiceDto>> Handle(
            GetAvailableServiceByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var availableServices = await _context.AvailableServices
                .Include(a => a.User)
                .Include(a => a.Service)
                .AsNoTracking()
                .Where(a => a.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AvailableServiceDto>>(availableServices);
        }
    }
}