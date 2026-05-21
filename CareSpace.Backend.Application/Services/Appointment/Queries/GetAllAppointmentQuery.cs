using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Appointment.Queries
{
    public class GetAllAppointmentQuery : IRequest<List<AppointmentDto>>
    {
    }

    public class GetAllAppointmentQueryHandler
        : IRequestHandler<GetAllAppointmentQuery, List<AppointmentDto>>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAppointmentQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> Handle(
            GetAllAppointmentQuery request,
            CancellationToken cancellationToken)
        {
            var appointments = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Centre)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}