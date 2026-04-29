using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERPsystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        // In a real scenario, you can inject an IWebHostEnvironment to return stack trace only in dev
        private readonly bool _isDevelopment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = env.EnvironmentName == "Development";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new 
            {
                Status = context.Response.StatusCode,
                Message = "An internal server error has occurred.",
                ErrorCode = "INTERNAL_SERVER_ERROR",
                Detailed = _isDevelopment ? exception.ToString() : null
            };

            // You can add more exception types here (e.g., ValidationException -> 400 Bad Request)
            
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
