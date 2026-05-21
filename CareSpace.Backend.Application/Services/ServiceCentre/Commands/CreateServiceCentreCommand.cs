using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;

namespace CareSpace.Backend.Application.Services.ServiceCentre.Commands
{
    public class CreateServiceCentreCommand : IRequest<ServiceCentreDto>
    {
        public CreateServiceCentreDto ServiceCentre { get; set; }

        public CreateServiceCentreCommand(CreateServiceCentreDto serviceCentre)
        {
            ServiceCentre = serviceCentre;
        }
    }

    public class CreateServiceCentreCommandHandler
        : IRequestHandler<CreateServiceCentreCommand, ServiceCentreDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateServiceCentreCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCentreDto> Handle(
            CreateServiceCentreCommand request,
            CancellationToken cancellationToken)
        {
            var serviceCentre = _mapper.Map<Domain.Entities.ServiceCentre>(request.ServiceCentre);

            serviceCentre.CreatedAt = DateTime.UtcNow;

            await _context.ServiceCentres.AddAsync(serviceCentre, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceCentreDto>(serviceCentre);
        }
    }
}