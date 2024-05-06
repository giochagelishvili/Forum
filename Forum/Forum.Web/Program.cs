using Forum.Web.Infrastructure.Middlewares;
using Serilog;
using Forum.Shared.Extensions;
using Forum.Web.Infrastructure.Extensions;

namespace Forum.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddDbContextAndIdentity(builder.Configuration);

            builder.Services.AddCustomHealthChecks(builder.Configuration);

            builder.Services.AddCustomValidators();

            builder.Services.AddSession();

            builder.Services.AddServices();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseSession();

            app.UseExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseConfiguredHealthChecks(builder.Configuration);
            app.UseConfiguredHealthChecksUI(builder.Configuration);
            app.UseConfiguredStaticFiles(builder.Configuration);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Topic}/{action=Topics}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Topic}/{action=Topics}");
            });

            await app.Services.SeedRoles();
            await app.Services.SeedAdmin();

            app.Run();
        }
    }
}