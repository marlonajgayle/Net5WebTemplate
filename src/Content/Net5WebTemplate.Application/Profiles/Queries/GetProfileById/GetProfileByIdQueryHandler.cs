using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, ProfileDto>
    {
        private readonly INet5WebTemplateDbContext _dbContext;

        public GetProfileByIdQueryHandler(INet5WebTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProfileDto> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Profiles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Profiles), request.Id);
            }

            return new GetProfiles.ProfileDto
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                AddressLine1 = entity.Address.AddressLine1,
                AddressLine2 = entity.Address.AddressLine2,
                Parish = entity.Address.Parish,
                PhoneNumber = entity.PhoneNumber
            };
        }
    }
}
