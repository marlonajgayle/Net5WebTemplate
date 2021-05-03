using MediatR;
using Microsoft.Extensions.Localization;
using Net5WebTemplate.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, string>
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public GetClientDetailQueryHandler(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;
        }
        public Task<string> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_localizer["Welcome"].Value);
        }
    }
}