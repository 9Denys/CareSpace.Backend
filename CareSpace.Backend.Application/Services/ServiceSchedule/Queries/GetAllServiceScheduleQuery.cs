using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Queries
{
    public class GetAllServiceScheduleQuery : IRequest<List<ServiceScheduleDto>>
    {
    }

    public class GetAllServiceScheduleQueryHandler
        : IRequestHandler<GetAllServiceScheduleQuery, List<ServiceScheduleDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllServiceScheduleQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceScheduleDto>> Handle(
            GetAllServiceScheduleQuery request,
            CancellationToken cancellationToken)
        {
            var schedules = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ServiceScheduleDto>>(schedules);
        }
    }
}