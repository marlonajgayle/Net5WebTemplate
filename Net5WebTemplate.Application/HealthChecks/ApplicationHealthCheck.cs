using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.HealthChecks
{
    public class ApplicationHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var assembly = Assembly.Load("Net5WebTemplate.Api");
            var versionNumber = assembly.GetName().Version;

            return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
        }
    }
}
