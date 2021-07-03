using MediatR;
using Net5WebTemplate.Application.Common.Models;

namespace Net5WebTemplate.Application.Auth.Command.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenResult>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
