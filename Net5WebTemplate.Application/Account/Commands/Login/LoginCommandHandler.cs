using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ISignInManager _signInManager;
        public LoginCommandHandler(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password,
                false, false);

            if (!result.Succeeded)
            { 
                // throw authentication exception
            }

            return "Login Successfully.";
        }
    }
}