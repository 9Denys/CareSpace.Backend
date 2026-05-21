using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Commands
{
    public class DeleteServiceScheduleCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteServiceScheduleCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteServiceScheduleCommandHandler
        : IRequestHandler<DeleteServiceScheduleCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteServiceScheduleCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteServiceScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var serviceSchedule = await _context.ServiceSchedules
                .FirstOrDefaultAsync(ss => ss.Id == request.Id, cancellationToken);

            if (serviceSchedule == null)
                throw new Exception($"Service schedule with id {request.Id} was not found");

            serviceSchedule.DeletedAt = DateTime.UtcNow;
            serviceSchedule.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}