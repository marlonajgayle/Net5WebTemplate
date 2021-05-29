using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResult>
    {
        private readonly IJwtSecurityTokenManager _jwtSecurityTokenManager;

        public RefreshTokenCommandHandler(IJwtSecurityTokenManager jwtSecurityTokenManager)
        {
            _jwtSecurityTokenManager = jwtSecurityTokenManager;
        }

        public async Task<TokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _jwtSecurityTokenManager.RefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            if (!result.Succeeded)
            {
                throw new UnauthorizedException(result.Error);
            }

            return result;
        }
    }
}