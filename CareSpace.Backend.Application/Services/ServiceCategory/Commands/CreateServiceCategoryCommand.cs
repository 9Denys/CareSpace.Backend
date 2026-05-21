using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.CreateDTOs;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;

namespace CareSpace.Backend.Application.Services.ServiceCategory.Commands
{
    public class CreateServiceCategoryCommand : IRequest<ServiceCategoryDto>
    {
        public CreateServiceCategoryDto ServiceCategory { get; set; }

        public CreateServiceCategoryCommand(CreateServiceCategoryDto serviceCategory)
        {
            ServiceCategory = serviceCategory;
        }
    }

    public class CreateServiceCategoryCommandHandler : IRequestHandler<CreateServiceCategoryCommand, ServiceCategoryDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;

        public CreateServiceCategoryCommandHandler(ICareSpaceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceCategoryDto> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var serviceCategory = _mapper.Map<Domain.Entities.ServiceCategory>(request.ServiceCategory);

            serviceCategory.CreatedAt = DateTime.UtcNow;

            await _context.ServiceCategories.AddAsync(serviceCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceCategoryDto>(serviceCategory);
        }
    }
}