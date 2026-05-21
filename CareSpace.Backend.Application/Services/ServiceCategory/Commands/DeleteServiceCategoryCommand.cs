using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.ServiceCategory.Commands
{
    public class DeleteServiceCategoryCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteServiceCategoryCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand>
    {
        private readonly ICareSpaceDbContext _context;

        public DeleteServiceCategoryCommandHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var serviceCategory = await _context.ServiceCategories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (serviceCategory == null)
            {
                throw new Exception($"Service category with id {request.Id} was not found");
            }

            serviceCategory.DeletedAt = DateTime.UtcNow;
            serviceCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}