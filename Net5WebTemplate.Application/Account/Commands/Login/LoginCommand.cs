using MediatR;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
