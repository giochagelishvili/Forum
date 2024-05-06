using Forum.Application.Accounts;
using Forum.Application.Accounts.Interfaces;
using Forum.Application.Comments;
using Forum.Application.Comments.Interfaces;
using Forum.Application.Images;
using Forum.Application.Images.Interfaces;
using Forum.Application.Users;
using Forum.Application.Topics;
using Forum.Infrastructure.Comments;
using Forum.Infrastructure.Images;
using Forum.Infrastructure.Topics;
using Forum.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Forum.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Forum.Domain.Roles;
using Microsoft.Extensions.Configuration;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Infrastructure.Topics.Admin;
using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Interfaces.Repositories;
using Forum.Infrastructure.Users.Admin;

namespace Forum.Shared.Extensions
{
    public static class SharedExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAdminTopicService, AdminTopicService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IAdminUserService, AdminUserService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminTopicRepository, AdminTopicRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        }

        public static void AddCustomValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static async Task SeedAdmin(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            string email = "admin@admin.com";
            string username = "admin";
            string password = "Admin123!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new User { UserName = username, Email = email };

                await userManager.CreateAsync(admin, password);

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        public static async Task SeedRoles(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            var roles = new[] { "Admin", "Member" };

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new Role { Name = role });
        }

        public static void AddDbContextAndIdentity(this IServiceCollection services, IConfiguration configuration, ServiceLifetime contextLifeTime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<ForumContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: contextLifeTime);

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<ForumContext>();
        }

        public static IHealthChecksBuilder AddCommonHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            var health = services.AddHealthChecks()
                .AddSqlServer(config.GetConnectionString("DefaultConnection"));

            services.AddHealthChecksUI(opts =>
            {
                opts.SetEvaluationTimeInSeconds(10);
                opts.SetApiMaxActiveRequests(1);
                opts.AddHealthCheckEndpoint("Feedback", config.GetValue<string>("Constants:HealthChecksPath"));
            }).AddInMemoryStorage();

            return health;
        }
    }
}