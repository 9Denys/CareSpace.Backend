using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Appointment.Commands
{
    public class CreateAppointmentCommand : IRequest<AppointmentDto>
    {
        public CreateAppointmentDto Appointment { get; set; }

        public CreateAppointmentCommand(CreateAppointmentDto appointment)
        {
            Appointment = appointment;
        }
    }

    public class CreateAppointmentCommandHandler
        : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(
            ICareSpaceDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
                throw new UnauthorizedAccessException("User is not authenticated");

            var schedule = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .FirstOrDefaultAsync(
                    ss => ss.Id == request.Appointment.ScheduleId,
                    cancellationToken);

            if (schedule == null)
                throw new Exception($"Service schedule with id {request.Appointment.ScheduleId} was not found");

            if (!schedule.Service.IsActive)
                throw new Exception("This service is not active");

            if (!schedule.Slot.IsAvailable)
                throw new Exception("This time slot is not available");

            if (schedule.Slot.StartDateTimeUtc <= DateTime.UtcNow)
                throw new Exception("Cannot book appointment for past time slot");

            var alreadyBooked = await _context.Appointments
                .AnyAsync(a =>
                    a.ScheduleId == schedule.Id &&
                    a.Status == AppointmentStatus.Booked,
                    cancellationToken);

            if (alreadyBooked)
                throw new Exception("This appointment slot is already booked");

            var appointment = new Domain.Entities.Appointment
            {
                UserId = userId.Value,
                ServiceId = schedule.ServiceId,
                ScheduleId = schedule.Id,
                Status = AppointmentStatus.Booked,
                CreatedAt = DateTime.UtcNow
            };

            
            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdAppointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Centre)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == appointment.Id, cancellationToken);

            return _mapper.Map<AppointmentDto>(createdAppointment);
        }
    }
}