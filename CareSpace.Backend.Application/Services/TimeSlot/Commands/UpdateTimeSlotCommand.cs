using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.TimeSlot.Commands
{
    public class UpdateTimeSlotCommand : IRequest<TimeSlotDto>
    {
        public Guid Id { get; set; }

        public UpdateTimeSlotDto TimeSlot { get; set; }

        public UpdateTimeSlotCommand(Guid id, UpdateTimeSlotDto timeSlot)
        {
            Id = id;
            TimeSlot = timeSlot;
        }
    }

    public class UpdateTimeSlotCommandHandler
        : IRequestHandler<UpdateTimeSlotCommand, TimeSlotDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTimeSlotCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TimeSlotDto> Handle(
            UpdateTimeSlotCommand request,
            CancellationToken cancellationToken)
        {
            var timeSlot = await _context.TimeSlots
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (timeSlot == null)
            {
                throw new Exception($"Time slot with id {request.Id} was not found");
            }

            _mapper.Map(request.TimeSlot, timeSlot);

            var kyivTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kyiv");

            var localStartDateTime = timeSlot.Date.ToDateTime(timeSlot.StartTime);
            var localEndDateTime = timeSlot.Date.ToDateTime(timeSlot.EndTime);

            timeSlot.StartDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
                localStartDateTime,
                kyivTimeZone);

            timeSlot.EndDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
                localEndDateTime,
                kyivTimeZone);

            timeSlot.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }
    }
}