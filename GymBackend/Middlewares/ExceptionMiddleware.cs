using System.Net;
using System.Text.Json;

namespace GymBackend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en {Path}", httpContext.Request.Path);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Ocurrió un error inesperado en el servidor.";

            if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Los datos enviados no son válidos.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "No tienes permiso para realizar esta acción.";
            }
            else if (exception is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = "El recurso solicitado no fue encontrado.";
            }

            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                Detail = _env.IsDevelopment() ? exception.Message : null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}