using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCentre.Commands
{
    public class DeleteServiceCentreCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteServiceCentreCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteServiceCentreCommandHandler
        : IRequestHandler<DeleteServiceCentreCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteServiceCentreCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteServiceCentreCommand request,
            CancellationToken cancellationToken)
        {
            var serviceCentre = await _context.ServiceCentres
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCentre == null)
            {
                throw new Exception($"Service centre with id {request.Id} was not found");
            }

            serviceCentre.DeletedAt = DateTime.UtcNow;
            serviceCentre.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}