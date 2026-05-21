using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCentre.Queries
{
    public class GetServiceCentreByIdQuery : IRequest<ServiceCentreDto>
    {
        public Guid Id { get; set; }

        public GetServiceCentreByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetServiceCentreByIdQueryHandler
        : IRequestHandler<GetServiceCentreByIdQuery, ServiceCentreDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceCentreByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCentreDto> Handle(
            GetServiceCentreByIdQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCentre = await _context.ServiceCentres
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCentre == null)
            {
                throw new Exception($"Service centre with id {request.Id} was not found");
            }

            return _mapper.Map<ServiceCentreDto>(serviceCentre);
        }
    }
}