using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Forum.Domain.Roles;
using Forum.Domain.Users;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Forum.Infrastructure.Users;
using Forum.Application.Users;
using Forum.Application.Comments.Interfaces;
using Forum.Infrastructure.Comments;
using Forum.Application.Comments;
using Serilog;
using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Interfaces.Repositories;
using Forum.Workers.Bans;
using Forum.Infrastructure.Topics;
using Forum.Application.Topics.Interfaces.Interfaces;
using Forum.Application.Topics;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Workers.Archives;
using Forum.Infrastructure.Topics.Admin;
using Forum.Infrastructure.Users.Admin;

namespace Forum.Workers
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                Console.WriteLine(ex);
            }

            IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ForumContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient);
                    services.AddIdentity<User, Role>().AddEntityFrameworkStores<ForumContext>();
                    services.AddTransient<IUserRepository, UserRepository>();
                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IAdminUserService, AdminUserService>();
                    services.AddTransient<IAdminUserRepository, AdminUserRepository>();
                    services.AddTransient<BanService>();
                    services.AddHostedService<BanWorker>();
                    services.AddTransient<ICommentRepository, CommentRepository>();
                    services.AddTransient<ICommentService, CommentService>();
                    services.AddTransient<ITopicRepository, TopicRepository>();
                    services.AddTransient<IAdminTopicRepository, AdminTopicRepository>();
                    services.AddTransient<ITopicService, TopicService>();
                    services.AddTransient<IAdminTopicService, AdminTopicService>();
                    services.AddTransient<ArchiveService>();
                    services.AddHostedService<ArchiveWorker>();
                });
        }
    }
}
