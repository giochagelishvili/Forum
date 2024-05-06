using Forum.Shared.Classes;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace Forum.Web.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ErrorLog error = new(httpContext, ex);

            var log = JsonConvert.SerializeObject(error);

            Log.Error(log);

            httpContext.Session.Set("ErrorMessage", Encoding.UTF8.GetBytes(error.Message));

            httpContext.Response.Redirect("/Home/Error");
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
