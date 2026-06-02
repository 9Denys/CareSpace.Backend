using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.TimeSlot.Queries
{
    public class GetAllTimeSlotQuery : IRequest<List<TimeSlotDto>>
    {
    }

    public class GetAllTimeSlotQueryHandler
        : IRequestHandler<GetAllTimeSlotQuery, List<TimeSlotDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTimeSlotQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TimeSlotDto>> Handle(
        GetAllTimeSlotQuery request,
        CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var timeSlots = await _context.TimeSlots
                .Where(ts =>
                    ts.Date.ToDateTime(ts.StartTime) > now)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<TimeSlotDto>>(timeSlots);
        }
    }
}