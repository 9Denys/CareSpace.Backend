using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Appointment.Queries
{
    public class GetMyAppointmentQuery : IRequest<List<AppointmentDto>>
    {
    }

    public class GetMyAppointmentQueryHandler
        : IRequestHandler<GetMyAppointmentQuery, List<AppointmentDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public GetMyAppointmentQueryHandler(
            ICareSpaceDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> Handle(
            GetMyAppointmentQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
                throw new UnauthorizedAccessException("User is not authenticated");

            var appointments = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Centre)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .AsNoTracking()
                .Where(a => a.UserId == userId.Value)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}