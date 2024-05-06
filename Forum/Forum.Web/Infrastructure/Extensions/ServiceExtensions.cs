using Forum.Shared.Extensions;

namespace Forum.Web.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            var host = config.GetValue<string>("Constants:Host");

            services.AddCommonHealthChecks(config)
                .AddUrlGroup(new Uri($"{host}/topic/topics"), "Topics Endpoint");
        }
    }
}
