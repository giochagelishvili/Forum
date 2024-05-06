namespace Forum.API.Infrastructure.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddSwaggerEndpoints(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
                opts.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
            });
        }
    }
}
