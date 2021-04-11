using MediatR;
using Net5WebTemplate.Application.Common.Models;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<TokenResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
