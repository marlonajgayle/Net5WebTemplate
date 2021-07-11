using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Unit>
    {
        public Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}