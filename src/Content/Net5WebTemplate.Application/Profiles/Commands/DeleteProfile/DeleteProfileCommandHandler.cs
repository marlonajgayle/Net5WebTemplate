using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Profiles.Commands.DeleteProfile
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand>
    {
        private readonly INet5WebTemplateDbContext _dbContext;

        public DeleteProfileCommandHandler(INet5WebTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Profiles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Profile), request.Id);
            }

            _dbContext.Profiles.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
