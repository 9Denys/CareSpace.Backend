using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Service.Commands
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteServiceCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteServiceCommand request,
            CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with id {request.Id} was not found");
            }

            service.DeletedAt = DateTime.UtcNow;
            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}