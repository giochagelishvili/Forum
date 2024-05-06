using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Forum.Shared.Extensions
{
    public static class BuilderExtensions
    {
        public static void UseConfiguredStaticFiles(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(configuration.GetValue<string>("Constants:UploadPath")),
                RequestPath = configuration.GetValue<string>("Constants:RequestPath")
            });
            builder.UseStaticFiles();
        }

        public static void UseConfiguredHealthChecksUI(this IApplicationBuilder builder, IConfiguration config)
        {
            builder.UseHealthChecksUI(opts =>
            {
                opts.UIPath = config.GetValue<string>("Constants:HealthChecksUIPath");
            });
        }

        public static void UseConfiguredHealthChecks(this IEndpointRouteBuilder builder, IConfiguration config)
        {
            builder.MapHealthChecks(config.GetValue<string>("Constants:HealthChecksPath"), new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
