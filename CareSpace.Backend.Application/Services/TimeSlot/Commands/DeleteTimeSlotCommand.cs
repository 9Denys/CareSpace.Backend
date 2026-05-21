using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.TimeSlot.Commands
{
    public class DeleteTimeSlotCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteTimeSlotCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteTimeSlotCommandHandler
        : IRequestHandler<DeleteTimeSlotCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteTimeSlotCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteTimeSlotCommand request,
            CancellationToken cancellationToken)
        {
            var timeSlot = await _context.TimeSlots
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (timeSlot == null)
            {
                throw new Exception($"Time slot with id {request.Id} was not found");
            }

            timeSlot.DeletedAt = DateTime.UtcNow;
            timeSlot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}