using Microsoft.Extensions.DependencyInjection;

namespace Net5WebTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHealthChecks();

            return services;
        }
    }
}
