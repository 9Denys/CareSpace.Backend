using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCentre.Commands
{
    public class UpdateServiceCentreCommand : IRequest<ServiceCentreDto>
    {
        public Guid Id { get; set; }

        public UpdateServiceCentreDto ServiceCentre { get; set; }

        public UpdateServiceCentreCommand(Guid id, UpdateServiceCentreDto serviceCentre)
        {
            Id = id;
            ServiceCentre = serviceCentre;
        }
    }

    public class UpdateServiceCentreCommandHandler
        : IRequestHandler<UpdateServiceCentreCommand, ServiceCentreDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceCentreCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCentreDto> Handle(
            UpdateServiceCentreCommand request,
            CancellationToken cancellationToken)
        {
            var serviceCentre = await _context.ServiceCentres
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCentre == null)
            {
                throw new Exception($"Service centre with id {request.Id} was not found");
            }

            _mapper.Map(request.ServiceCentre, serviceCentre);

            serviceCentre.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceCentreDto>(serviceCentre);
        }
    }
}