using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResult>
    {
        private readonly IMediator _mediator;
        private readonly IUserManager _userManager;
        private readonly IJwtSecurityTokenManager _securityTokenManager;

        public LoginCommandHandler(IMediator mediator, IUserManager userManager,
            IJwtSecurityTokenManager securityTokenManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _securityTokenManager = securityTokenManager;
        }

        public async Task<TokenResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginEventNotification loginEvent;
            var isAuthenticated = await _userManager.IsEmailandPasswordValid(request.Email, request.Password);

            if (!isAuthenticated)
            {
                loginEvent = new LoginEventNotification
                {
                    Username = request.Email,
                    Description = "Username and or password was invalid.",
                    IsSuccess = false,
                    IpAddress = "127.0.0.1",
                    Timestamp = DateTime.UtcNow
                };

                await _mediator.Publish(loginEvent, cancellationToken);

                throw new UnauthorizedException($"Invalid email or password for {request.Email}");
            }

            loginEvent = new LoginEventNotification
            {
                Username = request.Email,
                Description = "Login was successful",
                IsSuccess = true,
                IpAddress = "127.0.0.1",
                Timestamp = DateTime.UtcNow
            };

            await _mediator.Publish(loginEvent, cancellationToken);

            return await _securityTokenManager.GenerateClaimsTokenAsync(request.Email, cancellationToken);
        }
    }
}