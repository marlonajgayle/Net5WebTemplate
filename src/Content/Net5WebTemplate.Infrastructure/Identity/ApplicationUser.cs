using Microsoft.AspNetCore.Identity;
using Net5WebTemplate.Domain.Entities;
using System.Collections.Generic;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}