using Net5WebTemplate.Application.Common.Models;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface ISignInManager
    {
        Task<Result> PasswordSignInAsync(string email, string password, bool isPersistent, bool LockoutOnFailiure);
    }
}
