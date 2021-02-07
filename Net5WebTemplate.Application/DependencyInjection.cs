using Microsoft.Extensions.DependencyInjection;
using Net5WebTemplate.Application.HealthChecks;

namespace Net5WebTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "Net5WebTemplate API");

            return services;
        }
    }
}
