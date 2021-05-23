using Microsoft.AspNetCore.Http;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Security.Claims;

namespace Net5WebTemplate.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }

        public bool IsAuthenticated { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
        }
    }
}
