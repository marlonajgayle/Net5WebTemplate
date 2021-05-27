using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface ISecurityTokenService
    {
        Task<string> GeneratePasswordResetTokenAsync(string email);
    }
}
