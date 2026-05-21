using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CareSpace.Backend.Application.Services.User.Queries
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly ICareSpaceDbContext _context;

        public GetAllUserQueryHandler(ICareSpaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(
            GetAllUserQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = (CareSpace.Backend.Contracts.DTOs.Enums.Role)u.Role,
                    CreatedAt = u.CreatedAt,
                 
                })
                .ToListAsync(cancellationToken);
        }
    }
}