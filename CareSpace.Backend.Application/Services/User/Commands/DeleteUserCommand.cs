using CareSpace.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Services.User.Commands
{
    public class DeleteUserCommand : IRequest
    {

    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IUserContextService _userContextService;
        public DeleteUserCommandHandler(ICareSpaceDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);
            if (user == null)
            {
                throw new Exception($"TROUBLE: User with Id {userId} was not found in database");
            }

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
