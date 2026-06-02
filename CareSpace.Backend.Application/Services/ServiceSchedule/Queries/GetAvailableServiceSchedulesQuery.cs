using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceSchedule.Queries
{
    public class GetAvailableServiceSchedulesQuery : IRequest<List<ServiceScheduleDto>>
    {
        public Guid ServiceId { get; set; }
        public Guid CentreId { get; set; }
        public DateOnly Date { get; set; }

        public GetAvailableServiceSchedulesQuery(Guid serviceId, Guid centreId, DateOnly date)
        {
            ServiceId = serviceId;
            CentreId = centreId;
            Date = date;
        }
    }

    public class GetAvailableServiceSchedulesQueryHandler
        : IRequestHandler<GetAvailableServiceSchedulesQuery, List<ServiceScheduleDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAvailableServiceSchedulesQueryHandler(
            ICareSpaceDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceScheduleDto>> Handle(
            GetAvailableServiceSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var schedules = await _context.ServiceSchedules
                .Include(ss => ss.Service)
                .Include(ss => ss.Slot)
                .Include(ss => ss.Centre)
                .Include(ss => ss.Appointments)
                .Where(ss =>
                    ss.ServiceId == request.ServiceId &&
                    ss.CentreId == request.CentreId &&
                    ss.Slot.Date == request.Date &&
                    ss.Slot.IsAvailable &&
                    ss.Slot.Date.ToDateTime(ss.Slot.StartTime) > now &&
                    !ss.Appointments.Any(a =>
                        a.Status == AppointmentStatus.Booked))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ServiceScheduleDto>>(schedules);
        }
    }
}