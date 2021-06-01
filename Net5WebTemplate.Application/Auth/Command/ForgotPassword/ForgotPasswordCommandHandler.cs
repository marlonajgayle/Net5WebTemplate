using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserManager _userManager;
        private readonly ISecurityTokenService _securityToken;
        private readonly IMediator _mediator;

        public ForgotPasswordCommandHandler(IUserManager userManager, ISecurityTokenService securityToken,
            IMediator mediator)
        {
            _userManager = userManager;
            _securityToken = securityToken;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var IsValidUser = await _userManager.UserExistAsync(request.Email);

            if (IsValidUser)
            {
                var token = await _securityToken.GeneratePasswordResetTokenAsync(request.Email);

                var forgotPasswordNotification = new ForgotPasswordEventNotification
                {
                    Email = request.Email,
                    Token = token
                };

                await _mediator.Publish(forgotPasswordNotification, cancellationToken);
            }

            return Unit.Value;
        }
    }
}