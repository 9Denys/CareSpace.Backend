using AutoMapper;
using CareSpace.Backend.Application.Interfaces;
using CareSpace.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Services.User.Queries
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {

    }

    public class GetUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly ICareSpaceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetUserQueryHandler(ICareSpaceDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            if (user == null)
            {
                throw new Exception($"TROUBLE: User with id {userId} was not found in database.");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
