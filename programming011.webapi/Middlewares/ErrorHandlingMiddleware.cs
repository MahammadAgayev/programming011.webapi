using programming011.webapi.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace programming011.webapi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);  // Continue the request pipeline
            }
            catch (AppException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new
                {
                    message = ex.Message,
                };

                var responseJson = JsonSerializer.Serialize(response);

                await httpContext.Response.WriteAsync(responseJson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong...");

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    message = "An unexpected error occurred. Please try again later.",
                };

                var responseJson = JsonSerializer.Serialize(response);

                await httpContext.Response.WriteAsync(responseJson);
            }
        }
    }

}
