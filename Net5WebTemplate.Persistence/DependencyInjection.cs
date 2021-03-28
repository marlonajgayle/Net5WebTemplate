﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net5WebTemplate.Application.Common.Interfaces;

namespace Net5WebTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Net5WebTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Net5WebTemplateDbConnection")));

            services.AddScoped<INet5WebTemplateDbContext>(provider => 
                provider.GetService<Net5WebTemplateDbContext>());

            return services;
        }
    }
}
