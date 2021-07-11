using MediatR;
using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Mappings;
using Net5WebTemplate.Application.Common.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfiles
{
    public class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, PaginatedList<ProfileDto>>
    {
        private readonly INet5WebTemplateDbContext _dbContext;

        public GetProfilesQueryHandler(INet5WebTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<ProfileDto>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _dbContext.Profiles
                .AsNoTracking()
                .Select(p => new ProfileDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    AddressLine1 = p.Address.AddressLine1,
                    AddressLine2 = p.Address.AddressLine2,
                    Parish = p.Address.Parish,
                    PhoneNumber = p.PhoneNumber
                }).PaginatedListAsync(request.Offset, request.Limit);

            return profiles;
        }
    }
}
