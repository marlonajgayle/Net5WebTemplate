using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Net5WebTemplate.Api.ConfigOptions;
using Net5WebTemplate.Application;
using Net5WebTemplate.Application.HealthChecks;
using Net5WebTemplate.Infrastructure;
using Net5WebTemplate.Persistence;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Net5WebTemplate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register and configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost")
                        .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE")
                        .AllowCredentials();
                    });
            });

            // Register and Configure API versioning
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            //Register and configure API versioning explorer
            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.SubstituteApiVersionInUrl = true;
            });

            // Swagger OpenAPI Configuration
            var swaggerDocOptions = new SwaggerDocOptions();
            Configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);
            services.AddSwaggerGen();
            services.AddOptions<SwaggerGenOptions>()
                .Configure<IApiVersionDescriptionProvider>((swagger, service) =>
                {

                    foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
                    {
                        swagger.SwaggerDoc(description.GroupName, new OpenApiInfo
                        {
                            Title = swaggerDocOptions.Title,
                            Version = description.ApiVersion.ToString(),
                            Description = swaggerDocOptions.Description,
                            Contact = new OpenApiContact
                            {
                                Name = swaggerDocOptions.Organization,
                                Email = swaggerDocOptions.Email
                            }
                        });
                    }

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    swagger.IncludeXmlComments(xmlPath);
                });

            // Register InvestEdge.Application Service Configurations
            services.AddApplication();

            // Add InvestEdge.Infrastructure Service Configuration
            services.AddPersistence(Configuration);

            // Add InvestEdge.Infrastructure Service Configuration
            services.AddInfrastructure(Configuration, Environment);

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(options =>
                    options.RegisterValidatorsFromAssemblyContaining<Startup>());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Middelware to serve generated Swager as JSON endpoint
                var swaggerOptions = new SwaggerOptions();
                Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

                app.UseSwagger(option =>
                {
                    option.RouteTemplate = swaggerOptions.JsonRoute;
                });

                // Enable Middelware to serve Swagger UI (HTML, JavaScript, CSS etc.)
                app.UseSwaggerUI(option =>
                {
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        option.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            }

            // Enable NWebSec Security Headers
            app.UseXContentTypeOptions();
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.SameOrigin());
            app.UseReferrerPolicy(options => options.NoReferrerWhenDowngrade());

            // Feature-Policy security header
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Feature-Policy", "geolocation 'none'; midi 'none';");
                await next.Invoke();
            });

            // Enable Health Check Middleware
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse()
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Status = x.Value.Status.ToString(),
                            Component = x.Key,
                            Description = x.Value.Description == null && x.Key.Contains("DbContext") ? env.EnvironmentName + "-db" : x.Value.Description
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(text: JsonConvert.SerializeObject(response));
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
