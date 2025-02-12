using System.Net;
using System.Text.Json;

namespace LibraryAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)//imi lipsea try catch :)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Error = exception.Message,
                StatusCode = response.StatusCode
            };

            var json = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(json);
        }
    }
}
