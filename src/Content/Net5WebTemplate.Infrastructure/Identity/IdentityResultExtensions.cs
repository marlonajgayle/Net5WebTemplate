using Microsoft.AspNetCore.Identity;
using Net5WebTemplate.Application.Common.Models;
using System.Linq;

namespace Net5WebTemplate.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public static Result ToApplicationResult(this SignInResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(new string[] { "Invalid Login Attempt." });
        }
    }
}
