using Net5WebTemplate.Application.Common.Models;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Result Result, string UserId)> CreateUserAsync(string email, string password);
        Task<bool> UserExistAsync(string email);
    }
}