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
        private readonly ISignInManager _signInManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IJwtSecurityTokenManager _securityTokenManager;

        public LoginCommandHandler(IMediator mediator, ISignInManager signInManager,
            IJwtSecurityTokenManager securityTokenManager, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _currentUserService = currentUserService;
            _securityTokenManager = securityTokenManager;
        }

        public async Task<TokenResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginEventNotification loginEvent;
            var signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (!signInResult.Succeeded)
            {
                loginEvent = new LoginEventNotification
                {
                    Username = request.Email,
                    Description = signInResult.Errors[0],
                    IsSuccess = false,
                    IpAddress = _currentUserService.IpAddress,
                    Timestamp = DateTime.UtcNow
                };

                await _mediator.Publish(loginEvent, cancellationToken);

                throw new UnauthorizedException(signInResult.Errors[0]);
            }

            loginEvent = new LoginEventNotification
            {
                Username = request.Email,
                Description = "Login was successful",
                IsSuccess = true,
                IpAddress = _currentUserService.IpAddress,
                Timestamp = DateTime.UtcNow
            };

            await _mediator.Publish(loginEvent, cancellationToken);

            return await _securityTokenManager.GenerateClaimsTokenAsync(request.Email, cancellationToken);
        }
    }
}