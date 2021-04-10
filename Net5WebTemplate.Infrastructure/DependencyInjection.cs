﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;
using System.Text;

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

            var identityOptionsConfig = new IdentityOptionsConfig();
            configuration.GetSection(nameof(IdentityOptionsConfig)).Bind(identityOptionsConfig);
            
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = identityOptionsConfig.RequiredLength;
                options.Password.RequireDigit = identityOptionsConfig.RequiredDigit;
                options.Password.RequireLowercase = identityOptionsConfig.RequireLowercase;
                options.Password.RequiredUniqueChars = identityOptionsConfig.RequiredUniqueChars;
                options.Password.RequireUppercase = identityOptionsConfig.RequireUppercase;

            })
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication( options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        RequireExpirationTime = jwtSettings.RequireExpirationTime,
                        ValidateLifetime = jwtSettings.ValidateLifetime
                    };
                });

            return services;
        }
    }
}
