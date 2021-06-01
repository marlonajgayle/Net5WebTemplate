using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net5WebTemplate.Application.Common.Interfaces;
using System;

namespace Net5WebTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHealthChecks()
                .AddDbContextCheck<Net5WebTemplateDbContext>();

            services.AddDbContext<Net5WebTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Net5WebTemplateDbConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)); // disable for production

            services.AddScoped<INet5WebTemplateDbContext>(provider =>
                provider.GetService<Net5WebTemplateDbContext>());


            return services;
        }
    }
}
