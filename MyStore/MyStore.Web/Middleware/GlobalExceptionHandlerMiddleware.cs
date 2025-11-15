using System.Net;
using System.Text.Json;

namespace MyStore.Web.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // 1. Log the error using Serilog
            _logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);

            // 2. Prepare a standard JSON response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please contact support.",
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            // 3. Send the response to the client
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}