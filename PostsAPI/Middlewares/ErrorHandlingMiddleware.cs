using System.Net;
using System.Text.Json;

namespace PostsAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "applicatiom/json";
                var errorResponse = new{Message = "Ocurrió un error en el servidor.",Error = ex.Message};
                var errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson);
            }
        }
    }
}