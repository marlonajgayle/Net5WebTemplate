using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Net5WebTemplate.Application.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Common
{
    public class ExceptionHandlerMiddleware
    {
        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            ErrorMessage details = null;
            var result = string.Empty;
            var jsonSerializerSettings = new JsonSerializerSettings
            { 
                ContractResolver = new DefaultContractResolver 
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "One or more validation errors have occured.",
                        Error = validationException.Failures,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "Invalid request state.",
                        Error = badRequestException.Message,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
                case UnauthorizedException unauthorizedException:
                    code = HttpStatusCode.BadRequest;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "Unauthorized Access",
                        Error = unauthorizedException.Message,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
                case ForbiddenException forbiddenException:
                    code = HttpStatusCode.BadRequest;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "Forbidden Access",
                        Error = forbiddenException.Message,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "The specified resource was not found.",
                        Error = notFoundException.Message,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
                case Exception _: 
                    code = HttpStatusCode.InternalServerError;
                    details = new ErrorMessage
                    {
                        Status = (int)code,
                        Message = "An error occurred while processing your request.",
                        Error = exception.Message,
                        Timestamp = DateTime.UtcNow
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            result = JsonConvert.SerializeObject(details, jsonSerializerSettings);

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

