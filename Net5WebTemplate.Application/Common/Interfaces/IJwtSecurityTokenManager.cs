using Net5WebTemplate.Application.Common.Models;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface IJwtSecurityTokenManager
    {
        Task<TokenResult> GenerateClaimsTokenAsync(string email, CancellationToken cancellationToken);
        Task<TokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
        Task<ClaimsPrincipal> GetPrincipFromTokenAsync(string token);
    }
}
