using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Common.Models;
using Net5WebTemplate.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public class JwtSecurityTokenManager : IJwtSecurityTokenManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly INet5WebTemplateDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtSecurityTokenManager(JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters,
            INet5WebTemplateDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<TokenResult> GenerateClaimsTokenAsync(string email, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken;
            var user = await _userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                refreshToken = user.RefreshTokens.Where(t => t.IsActive == true).FirstOrDefault();
            }
            else
            {
                refreshToken = new RefreshToken
                {
                    JwtId = token.Id,
                    CreationDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetime),
                    Token = GenerateRandomString(35) + Guid.NewGuid()
                };

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
                // Update user object
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return new TokenResult()
            {
                Succeeded = true,
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<ClaimsPrincipal> GetPrincipFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // disable token lifetime validation as we are validating against an expired token.
                var tokenValdationParams = _tokenValidationParameters.Clone();
                tokenValdationParams.ValidateLifetime = false;

                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return await Task.Run(() => principal);
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
        {
            var validatedToken = await GetPrincipFromTokenAsync(token);

            if (validatedToken == null)
            {
                return new TokenResult { Succeeded = false, Error = "Invalid token" };
            }

            var expirationDate = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirationDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expirationDate);

            if (expirationDateTimeUtc > DateTime.UtcNow)
            {
                return new TokenResult { Succeeded = false, Error = "This access token hasn't expired" };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new TokenResult { Succeeded = false, Error = "This access token does not exist" };
            }

            if (expirationDateTimeUtc > storedRefreshToken.ExpirationDate)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token has expired" };
            }

            if (!storedRefreshToken.IsActive)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token has already been used" };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new TokenResult { Succeeded = false, Error = "This refresh token does not match this JWT" };
            }

            storedRefreshToken.Revoked = DateTime.UtcNow;
            _dbContext.RefreshTokens.Update(storedRefreshToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var user = await _userManager.FindByEmailAsync(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            var tokenResult = await GenerateClaimsTokenAsync(user.Email, cancellationToken);

            return tokenResult;
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private static string GenerateRandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }
}
