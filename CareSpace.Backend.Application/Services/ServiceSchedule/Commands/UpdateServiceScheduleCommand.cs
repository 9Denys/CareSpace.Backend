using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Commands
{
    public class UpdateServiceScheduleCommand : IRequest<ServiceScheduleDto>
    {
        public Guid Id { get; set; }

        public UpdateServiceScheduleDto ServiceSchedule { get; set; }

        public UpdateServiceScheduleCommand(Guid id, UpdateServiceScheduleDto serviceSchedule)
        {
            Id = id;
            ServiceSchedule = serviceSchedule;
        }
    }

    public class UpdateServiceScheduleCommandHandler
        : IRequestHandler<UpdateServiceScheduleCommand, ServiceScheduleDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceScheduleCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceScheduleDto> Handle(
            UpdateServiceScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.ServiceSchedule;

            var serviceSchedule = await _context.ServiceSchedules
                .FirstOrDefaultAsync(ss => ss.Id == request.Id, cancellationToken);

            if (serviceSchedule == null)
                throw new Exception($"Service schedule with id {request.Id} was not found");

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == dto.ServiceId, cancellationToken);

            if (service == null)
                throw new Exception($"Service with id {dto.ServiceId} was not found");

            var slot = await _context.TimeSlots
                .FirstOrDefaultAsync(s => s.Id == dto.SlotId, cancellationToken);

            if (slot == null)
                throw new Exception($"Time slot with id {dto.SlotId} was not found");

            var centre = await _context.ServiceCentres
                .FirstOrDefaultAsync(c => c.Id == dto.CentreId, cancellationToken);

            if (centre == null)
                throw new Exception($"Service centre with id {dto.CentreId} was not found");

            if (slot.StartTime < centre.OpenTime || slot.EndTime > centre.CloseTime)
                throw new Exception("Time slot is outside service centre working hours");

            var slotDuration = (slot.EndTime - slot.StartTime).TotalMinutes;

            if (slotDuration < service.DurationMinutes)
                throw new Exception("Time slot duration is shorter than service duration");

            var isSlotAlreadyUsed = await _context.ServiceSchedules
                .AnyAsync(ss =>
                    ss.Id != request.Id &&
                    ss.CentreId == dto.CentreId &&
                    ss.SlotId == dto.SlotId,
                    cancellationToken);

            if (isSlotAlreadyUsed)
                throw new Exception("This time slot is already used in this service centre");

            _mapper.Map(dto, serviceSchedule);

            serviceSchedule.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var updatedSchedule = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .AsNoTracking()
                .FirstOrDefaultAsync(ss => ss.Id == serviceSchedule.Id, cancellationToken);

            return _mapper.Map<ServiceScheduleDto>(updatedSchedule);
        }
    }
}