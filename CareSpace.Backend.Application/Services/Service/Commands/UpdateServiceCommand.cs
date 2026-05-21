using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Service.Commands
{
    public class UpdateServiceCommand : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }

        public UpdateServiceDto Service { get; set; }

        public UpdateServiceCommand(Guid id, UpdateServiceDto service)
        {
            Id = id;
            Service = service;
        }
    }

    public class UpdateServiceCommandHandler
        : IRequestHandler<UpdateServiceCommand, ServiceDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(
            UpdateServiceCommand request,
            CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (service == null)
            {
                throw new Exception($"Service with id {request.Id} was not found");
            }

            var categoryExists = await _context.ServiceCategories
                .AnyAsync(c => c.Id == request.Service.CategoryId, cancellationToken);

            if (!categoryExists)
            {
                throw new Exception($"Service category with id {request.Service.CategoryId} was not found");
            }

            _mapper.Map(request.Service, service);

            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var updatedService = await _context.Services
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == service.Id, cancellationToken);

            return _mapper.Map<ServiceDto>(updatedService);
        }
    }
}