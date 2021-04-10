using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Net5WebTemplate.Application.Common.Behaviours;
using Net5WebTemplate.Application.HealthChecks;
using System.Reflection;

namespace Net5WebTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "Net5WebTemplate API");

            // Register MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Register MediatR Pipeline Behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            // Register Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
