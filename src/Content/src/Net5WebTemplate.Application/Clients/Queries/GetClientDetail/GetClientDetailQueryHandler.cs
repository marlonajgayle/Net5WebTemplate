﻿using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, string>
    {
        private readonly IStringLocalizer<Messages> _localizer;
        private readonly IMediator _mediator;

        public GetClientDetailQueryHandler(IStringLocalizer<Messages> localizer, IMediator mediator)
        {
            _localizer = localizer;
            _mediator = mediator;
        }
        public async Task<string> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            //await _mediator.Publish(new ConfirmAccountNotification { Email = "test@email.com" }, cancellationToken);

            return _localizer["Welcome"].Value;
        }
    }
}