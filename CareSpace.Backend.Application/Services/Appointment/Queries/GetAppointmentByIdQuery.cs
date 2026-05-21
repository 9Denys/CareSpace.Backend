using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Appointment.Queries
{
    public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
    {
        public Guid Id { get; set; }

        public GetAppointmentByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetAppointmentByIdQueryHandler
        : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(
            GetAppointmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Centre)
                .Include(a => a.Schedule)
                    .ThenInclude(s => s.Slot)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (appointment == null)
                throw new Exception($"Appointment with id {request.Id} was not found");

            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}