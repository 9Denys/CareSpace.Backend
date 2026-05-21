using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.TimeSlot.Queries
{
    public class GetTimeSlotByIdQuery : IRequest<TimeSlotDto>
    {
        public Guid Id { get; set; }

        public GetTimeSlotByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetTimeSlotByIdQueryHandler
        : IRequestHandler<GetTimeSlotByIdQuery, TimeSlotDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetTimeSlotByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TimeSlotDto> Handle(
            GetTimeSlotByIdQuery request,
            CancellationToken cancellationToken)
        {
            var timeSlot = await _context.TimeSlots
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (timeSlot == null)
            {
                throw new Exception($"Time slot with id {request.Id} was not found");
            }

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }
    }
}