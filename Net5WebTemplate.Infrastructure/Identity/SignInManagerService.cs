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

            if (result.IsLockedOut)
            {
                return Result.Failure(new string[] { "Account Locked, too many invalid login attempts." });
            }

            return (result.ToApplicationResult());
        }
    }
}
