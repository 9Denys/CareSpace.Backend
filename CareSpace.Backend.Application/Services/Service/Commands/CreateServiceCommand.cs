using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.Service.Commands
{
    public class CreateServiceCommand : IRequest<ServiceDto>
    {
        public CreateServiceDto Service { get; set; }

        public CreateServiceCommand(CreateServiceDto service)
        {
            Service = service;
        }
    }

    public class CreateServiceCommandHandler
        : IRequestHandler<CreateServiceCommand, ServiceDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateServiceCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(
            CreateServiceCommand request,
            CancellationToken cancellationToken)
        {
            var categoryExists = await _context.ServiceCategories
                .AnyAsync(c => c.Id == request.Service.CategoryId, cancellationToken);

            if (!categoryExists)
            {
                throw new Exception($"Service category with id {request.Service.CategoryId} was not found");
            }

            var service = _mapper.Map<Domain.Entities.Service>(request.Service);

            service.CreatedAt = DateTime.UtcNow;

            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdService = await _context.Services
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == service.Id, cancellationToken);

            return _mapper.Map<ServiceDto>(createdService);
        }
    }
}