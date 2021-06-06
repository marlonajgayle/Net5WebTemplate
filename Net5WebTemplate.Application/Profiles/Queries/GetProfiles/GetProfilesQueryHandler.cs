using MediatR;
using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfiles
{
    public class GetProfilesQueryHandler : IRequestHandler<GetProfilesQuery, List<ProfileDto>>
    {
        private readonly INet5WebTemplateDbContext _dbContext;

        public GetProfilesQueryHandler(INet5WebTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProfileDto>> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            List<ProfileDto> profiles = _dbContext.Profiles
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
                }).ToList();

            return await Task.FromResult(profiles);
        }
    }
}
