using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Service.Queries
{
    public class GetServiceByIdQuery : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }

        public GetServiceByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetServiceByIdQueryHandler
        : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(
            GetServiceByIdQuery request,
            CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with id {request.Id} was not found");
            }

            return _mapper.Map<ServiceDto>(service);
        }
    }
}