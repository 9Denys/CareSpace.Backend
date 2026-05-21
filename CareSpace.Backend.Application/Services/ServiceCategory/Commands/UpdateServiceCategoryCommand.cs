using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using CareSpace.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCategory.Commands
{
    public class UpdateServiceCategoryCommand : IRequest<ServiceCategoryDto>
    {
        public Guid Id { get; set; }

        public UpdateServiceCategoryDto ServiceCategory { get; set; }

        public UpdateServiceCategoryCommand(Guid id, UpdateServiceCategoryDto serviceCategory)
        {
            Id = id;
            ServiceCategory = serviceCategory;
        }
    }

    public class UpdateServiceCategoryCommandHandler : IRequestHandler<UpdateServiceCategoryCommand, ServiceCategoryDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceCategoryCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCategoryDto> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var serviceCategory = await _context.ServiceCategories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCategory == null)
            {
                throw new Exception($"Service category with id {request.Id} was not found");
            }

            _mapper.Map(request.ServiceCategory, serviceCategory);

            serviceCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceCategoryDto>(serviceCategory);
        }
    }
}