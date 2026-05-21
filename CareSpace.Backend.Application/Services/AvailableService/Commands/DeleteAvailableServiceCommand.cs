using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.AvailableService.Commands
{
    public class DeleteAvailableServiceCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteAvailableServiceCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteAvailableServiceCommandHandler
        : IRequestHandler<DeleteAvailableServiceCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteAvailableServiceCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteAvailableServiceCommand request,
            CancellationToken cancellationToken)
        {
            var availableService = await _context.AvailableServices
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (availableService == null)
                throw new Exception($"Available service with id {request.Id} was not found");

            availableService.DeletedAt = DateTime.UtcNow;
            availableService.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}