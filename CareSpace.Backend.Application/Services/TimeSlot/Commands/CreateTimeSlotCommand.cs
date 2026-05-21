using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;

namespace CareSpace.Backend.Application.Services.TimeSlot.Commands
{
    public class CreateTimeSlotCommand : IRequest<TimeSlotDto>
    {
        public CreateTimeSlotDto TimeSlot { get; set; }

        public CreateTimeSlotCommand(CreateTimeSlotDto timeSlot)
        {
            TimeSlot = timeSlot;
        }
    }

    public class CreateTimeSlotCommandHandler
        : IRequestHandler<CreateTimeSlotCommand, TimeSlotDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateTimeSlotCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TimeSlotDto> Handle(
            CreateTimeSlotCommand request,
            CancellationToken cancellationToken)
        {
            var timeSlot = _mapper.Map<Domain.Entities.TimeSlot>(request.TimeSlot);

            var kyivTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kyiv");

            var localStartDateTime = timeSlot.Date.ToDateTime(timeSlot.StartTime);
            var localEndDateTime = timeSlot.Date.ToDateTime(timeSlot.EndTime);

            timeSlot.StartDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
                localStartDateTime,
                kyivTimeZone);

            timeSlot.EndDateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
                localEndDateTime,
                kyivTimeZone);

            timeSlot.CreatedAt = DateTime.UtcNow;

            await _context.TimeSlots.AddAsync(timeSlot, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }
    }
}