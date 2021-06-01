using Microsoft.AspNetCore.Http;
using Net5WebTemplate.Application.Common.Interfaces;
using System.Security.Claims;

namespace Net5WebTemplate.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public string Email { get; }
        public bool IsAuthenticated { get; }
        public string IpAddress { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString();
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
        }
    }
}
