using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;

namespace Net5WebTemplate.Infrastructure
{
    static public class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            // Register Infrastrucruter Services
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddScoped<ISignInManager, SignInManagerService>();

            // Register Identity DbContext and Server
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Net5WebTemplateDbConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 3;

            })
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
