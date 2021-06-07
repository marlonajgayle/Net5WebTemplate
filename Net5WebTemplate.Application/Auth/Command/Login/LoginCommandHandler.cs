using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Commands.Login
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
                loginEvent = InitLoginEvent(request.Email, signInResult.Errors[0], false);
                await _mediator.Publish(loginEvent, cancellationToken);

                throw new UnauthorizedException(signInResult.Errors[0]);
            }

            loginEvent = InitLoginEvent(request.Email, "Login was successful", true);
            await _mediator.Publish(loginEvent, cancellationToken);

            return await _securityTokenManager.GenerateClaimsTokenAsync(request.Email, cancellationToken);
        }

        private LoginEventNotification InitLoginEvent(string email, string description, bool isSuccess)
        {
            var loginEvent = new LoginEventNotification
            {
                Username = email,
                Description = description,
                IsSuccess = isSuccess,
                IpAddress = _currentUserService.IpAddress,
                Timestamp = DateTime.UtcNow
            };

            return loginEvent;
        }
    }
}