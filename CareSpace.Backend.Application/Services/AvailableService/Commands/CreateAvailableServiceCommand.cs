using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.AvailableService.Commands
{
    public class CreateAvailableServiceCommand : IRequest<AvailableServiceDto>
    {
        public CreateAvailableServiceDto AvailableService { get; set; }

        public CreateAvailableServiceCommand(CreateAvailableServiceDto availableService)
        {
            AvailableService = availableService;
        }
    }

    public class CreateAvailableServiceCommandHandler
        : IRequestHandler<CreateAvailableServiceCommand, AvailableServiceDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateAvailableServiceCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AvailableServiceDto> Handle(
            CreateAvailableServiceCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AvailableService;

            var userExists = await _context.Users
                .AnyAsync(u => u.Id == dto.UserId, cancellationToken);

            if (!userExists)
                throw new Exception($"User with id {dto.UserId} was not found");

            var serviceExists = await _context.Services
                .AnyAsync(s => s.Id == dto.ServiceId, cancellationToken);

            if (!serviceExists)
                throw new Exception($"Service with id {dto.ServiceId} was not found");

            var alreadyExists = await _context.AvailableServices
                .AnyAsync(a =>
                    a.UserId == dto.UserId &&
                    a.ServiceId == dto.ServiceId,
                    cancellationToken);

            if (alreadyExists)
                throw new Exception("This service is already assigned to this user");

            var availableService = _mapper.Map<Domain.Entities.AvailableService>(dto);

            availableService.CreatedAt = DateTime.UtcNow;

            await _context.AvailableServices.AddAsync(availableService, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var created = await _context.AvailableServices
                .Include(a => a.User)
                .Include(a => a.Service)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == availableService.Id, cancellationToken);

            return _mapper.Map<AvailableServiceDto>(created);
        }
    }
}