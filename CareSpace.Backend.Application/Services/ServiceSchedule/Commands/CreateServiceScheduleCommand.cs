using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Commands
{
    public class CreateServiceScheduleCommand : IRequest<ServiceScheduleDto>
    {
        public CreateServiceScheduleDto ServiceSchedule { get; set; }

        public CreateServiceScheduleCommand(CreateServiceScheduleDto serviceSchedule)
        {
            ServiceSchedule = serviceSchedule;
        }
    }

    public class CreateServiceScheduleCommandHandler
        : IRequestHandler<CreateServiceScheduleCommand, ServiceScheduleDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateServiceScheduleCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceScheduleDto> Handle(
            CreateServiceScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.ServiceSchedule;

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
                    ss.CentreId == dto.CentreId &&
                    ss.SlotId == dto.SlotId,
                    cancellationToken);

            if (isSlotAlreadyUsed)
                throw new Exception("This time slot is already used in this service centre");

            var serviceSchedule = _mapper.Map<Domain.Entities.ServiceSchedule>(dto);

            serviceSchedule.CreatedAt = DateTime.UtcNow;

            await _context.ServiceSchedules.AddAsync(serviceSchedule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdSchedule = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .AsNoTracking()
                .FirstOrDefaultAsync(ss => ss.Id == serviceSchedule.Id, cancellationToken);

            return _mapper.Map<ServiceScheduleDto>(createdSchedule);
        }
    }
}