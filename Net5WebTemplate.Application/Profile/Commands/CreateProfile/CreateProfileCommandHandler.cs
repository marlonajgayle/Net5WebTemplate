using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Profile.Commands.CreateProfile
{
    class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand>
    {
        private readonly INet5WebTemplateDbContext _dbContext;

        public CreateProfileCommandHandler(INet5WebTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Profile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = new Address(request.AddressLine1, request.AddressLine2, request.Parish),
                PhoneNumber = request.PhoneNumber
            };

            _dbContext.Profiles.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
