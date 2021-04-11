using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Account.Commands.RegisterUserAccount
{
    public class CreateUserAccountCommandHandler : IRequestHandler<CreateUserAccountCommand>
    {
        private readonly IUserManager _userManager;

        public CreateUserAccountCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var (Result, userId) = await _userManager.CreateUserAsync(request.Email, request.Password);

            if (!Result.Succeeded)
            { 
                // throw custom exception
            }

            return Unit.Value;
        }
    }
}