using MediatR;

namespace Net5WebTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
