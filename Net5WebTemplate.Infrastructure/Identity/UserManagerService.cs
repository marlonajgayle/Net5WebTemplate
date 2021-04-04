using Microsoft.AspNetCore.Identity;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using System.Threading.Tasks;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser 
            { 
                Email = userName,
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }
    }
}