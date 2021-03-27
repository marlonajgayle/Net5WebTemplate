using MediatR;
using System;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQuery : IRequest<String>
    {
        public int ClientId { get; set; }
    }
}
