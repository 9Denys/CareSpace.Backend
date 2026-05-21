using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Service.Queries
{
    public class GetAllServiceQuery : IRequest<List<ServiceDto>>
    {
    }

    public class GetAllServiceQueryHandler
        : IRequestHandler<GetAllServiceQuery, List<ServiceDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllServiceQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceDto>> Handle(
            GetAllServiceQuery request,
            CancellationToken cancellationToken)
        {
            var services = await _context.Services
                .Include(s => s.Category)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ServiceDto>>(services);
        }
    }
}