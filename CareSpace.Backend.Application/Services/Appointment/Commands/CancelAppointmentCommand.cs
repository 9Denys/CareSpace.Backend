using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Appointment.Commands
{
    public class CancelAppointmentCommand : IRequest<AppointmentDto>
    {
        public Guid Id { get; set; }

        public CancelAppointmentCommand(Guid id)
        {
            Id = id;
        }
    }

    public class CancelAppointmentCommandHandler
        : IRequestHandler<CancelAppointmentCommand, AppointmentDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CancelAppointmentCommandHandler(
            ICareSpaceDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(
            CancelAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
                throw new UnauthorizedAccessException("User is not authenticated");

            var appointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Centre)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (appointment == null)
                throw new Exception($"Appointment with id {request.Id} was not found");

            if (appointment.UserId != userId.Value)
                throw new UnauthorizedAccessException("You can cancel only your own appointment");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new Exception("Appointment is already cancelled");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new Exception("Completed appointment cannot be cancelled");

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancelledAt = DateTime.UtcNow;
            appointment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}