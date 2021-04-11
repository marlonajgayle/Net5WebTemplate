using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResult>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtSecurityTokenManager _securityTokenManager;

        public LoginCommandHandler(IUserManager userManager, IJwtSecurityTokenManager securityTokenManager)
        {
            _userManager = userManager;
            _securityTokenManager = securityTokenManager;
        }
        public async Task<TokenResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var isAuthenticated = await _userManager.IsEmailandPasswordValid(request.Email, request.Password);

            if (!isAuthenticated)
            {
                throw new UnauthorizedException($"Invalid email or password for {request.Email}");
            }
            
            return await _securityTokenManager.GenerateClaimsTokenAsync(request.Email, cancellationToken);
        }
    }
}