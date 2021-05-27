using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;
using System.Threading.Tasks;

namespace Net5WebTemplate.Infrastructure.SecurityTokens
{
    public class SecurityTokenService : ISecurityTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityTokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Generates security token to be used for resetting user's password.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var token = string.Empty;

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                token = await _userManager.GeneratePasswordResetTokenAsync(user);
            }

            return Base64UrlEncoder.Encode(token);
        }
    }
}
