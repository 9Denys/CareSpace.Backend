using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Queries
{
    public class GetServiceScheduleByIdQuery : IRequest<ServiceScheduleDto>
    {
        public Guid Id { get; set; }

        public GetServiceScheduleByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetServiceScheduleByIdQueryHandler
        : IRequestHandler<GetServiceScheduleByIdQuery, ServiceScheduleDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceScheduleByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceScheduleDto> Handle(
            GetServiceScheduleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var schedule = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .AsNoTracking()
                .FirstOrDefaultAsync(ss => ss.Id == request.Id, cancellationToken);

            if (schedule == null)
                throw new Exception($"Service schedule with id {request.Id} was not found");

            return _mapper.Map<ServiceScheduleDto>(schedule);
        }
    }
}