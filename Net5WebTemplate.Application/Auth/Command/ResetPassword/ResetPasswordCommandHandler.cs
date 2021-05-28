using MediatR;
using Net5WebTemplate.Application.Common.Exceptions;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IUserManager _userManager;

        public ResetPasswordCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.ResetPasswordAsync(request.Email, request.Token, request.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors[0]);
            }

            return Unit.Value;
        }
    }
}