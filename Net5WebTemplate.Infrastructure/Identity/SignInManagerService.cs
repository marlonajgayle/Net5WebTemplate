using Microsoft.AspNetCore.Identity;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System.Threading.Tasks;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public class SignInManagerService : ISignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInManagerService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> PasswordSignInAsync(string email, string password, bool isPersistent, bool LockoutOnFailiure)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent, LockoutOnFailiure);

            return (result.ToApplicationResult());
        }
    }
}
