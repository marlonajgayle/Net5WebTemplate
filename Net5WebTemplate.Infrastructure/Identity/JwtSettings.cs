using System;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan Expiration { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
    }
}
