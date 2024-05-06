using Forum.Shared.Classes;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Forum.API.Infrastructure.Middlewares.ExceptionHandling
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ErrorLog error = new(httpContext, ex);

            var log = JsonConvert.SerializeObject(error);

            Log.Error(log);

            httpContext.Response.Clear();
            httpContext.Response.ContentType = "application/json";

            if (error.Status.HasValue)
                httpContext.Response.StatusCode = error.Status.Value;
            else
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsync(log);
        }
    }
}
