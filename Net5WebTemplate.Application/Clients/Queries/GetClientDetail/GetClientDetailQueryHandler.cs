using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, string>
    {
        public Task<string> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Client Info.");
        }
    }
}