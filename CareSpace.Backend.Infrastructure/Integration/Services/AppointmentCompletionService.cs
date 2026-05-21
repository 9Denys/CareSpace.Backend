using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Domain.Enums;
using CareSpace.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CareSpace.Backend.Infrastructure.Integration.Services
{
    public class AppointmentCompletionService : IAppointmentCompletionService
    {
        private readonly CareSpaceDbContext _context;
        private readonly ILogger<AppointmentCompletionService> _logger;

        public AppointmentCompletionService(
            CareSpaceDbContext context,
            ILogger<AppointmentCompletionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CompleteExpiredAppointmentsAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var appointments = await _context.Appointments
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .Where(a => a.Status == AppointmentStatus.Booked)
                .ToListAsync(cancellationToken);

            foreach (var appointment in appointments)
            {
                var slot = appointment.Schedule.Slot;

                if (slot.EndDateTimeUtc <= now)
                {
                    appointment.Status = AppointmentStatus.Completed;
                    appointment.UpdatedAt = DateTime.UtcNow;

                    _logger.LogInformation(
                        "Appointment {AppointmentId} marked as Completed",
                        appointment.Id);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Expired appointments completion finished. Checked appointments count: {Count}",
                appointments.Count);
        }
    }
}