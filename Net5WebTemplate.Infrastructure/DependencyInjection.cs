using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Infrastructure.Identity;
using Net5WebTemplate.Infrastructure.Notifications.Email;
using Net5WebTemplate.Infrastructure.SecurityTokens;
using System;
using System.Net.Mail;
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
            services.AddTransient<IJwtSecurityTokenManager, JwtSecurityTokenManager>();

            // Register FluentEmail Services
            var emailConfig = new EmailConfiguration();
            configuration.GetSection(nameof(EmailConfiguration)).Bind(emailConfig);

            services.AddFluentEmail(defaultFromEmail: emailConfig.Email)
                .AddRazorRenderer()
                .AddSmtpSender(new SmtpClient(emailConfig.Host, emailConfig.Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = @"C:\Users\mgayle\email",
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Credentials = new NetworkCredential(emailConfig.Email, emailConfig.Password),
                    EnableSsl = emailConfig.EnableSsl
                });

            // Add EmailNotification Service
            services.AddScoped<IEmailNotification, EmailNotificationService>();

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
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<PasswordResetTokenProvider<ApplicationUser>>("CustomPasswordReset");

            // Configure Pasword reset Token lifespan
            services.Configure<PasswordResetTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(configuration.GetValue<int>("PasswordResetToken:LifespanInMinutes"));
            });

            // Configure JWT Authentication and Authorization
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                RequireExpirationTime = jwtSettings.RequireExpirationTime,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ClockSkew = jwtSettings.Expiration
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            return services;
        }
    }
}
